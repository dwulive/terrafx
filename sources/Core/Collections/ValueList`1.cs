// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// This file includes code based on the List<T> class from https://github.com/dotnet/runtime/
// The original code is Copyright © .NET Foundation and Contributors. All rights reserved. Licensed under the MIT License (MIT).

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static TerraFX.Utilities.ExceptionUtilities;
using static TerraFX.Utilities.MathUtilities;

namespace TerraFX.Collections
{
    /// <summary>Represents a list of items that can be accessed by index.</summary>
    /// <typeparam name="T">The type of the items contained in the list.</typeparam>
    /// <remarks>This type is meant to be used as an implementation detail of another type and should not be part of your public surface area.</remarks>
    [DebuggerDisplay("Capacity = {Capacity}; Count = {Count}")]
    [DebuggerTypeProxy(typeof(ValueList<>.DebugView))]
    public partial struct ValueList<T>
    {
        private T[] _items;
        private int _count;
        private int _version;

        /// <summary>Initializes a new instance of the <see cref="ValueList{T}" /> struct.</summary>
        /// <param name="capacity">The initial capacity of the list.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is <c>negative</c>.</exception>
        public ValueList(int capacity)
        {
            ThrowIfNegative(capacity, nameof(capacity));

            if (capacity != 0)
            {
                _items = GC.AllocateUninitializedArray<T>(capacity);
            }
            else
            {
                _items = Array.Empty<T>();
            }

            _count = 0;
            _version = 0;
        }

        /// <summary>Initializes a new instance of the <see cref="ValueList{T}" /> struct.</summary>
        /// <param name="source">The enumerable that is used to populate the list.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c>.</exception>
        public ValueList(IEnumerable<T> source)
        {
            // This is an extension method and throws ArgumentNullException if null
            _items = source.ToArray();

            _count = _items.Length;
            _version = 0;
        }

        /// <summary>Initializes a new instance of the <see cref="ValueList{T}" /> struct.</summary>
        /// <param name="span">The span that is used to populate the list.</param>
        public ValueList(ReadOnlySpan<T> span)
        {
            if (span.Length != 0)
            {
                var items = GC.AllocateUninitializedArray<T>(span.Length);
                span.CopyTo(items);
                _items = items;
            }
            else
            {
                _items = Array.Empty<T>();
            }

            _count = span.Length;
            _version = 0;
        }

        /// <summary>Initializes a new instance of the <see cref="ValueList{T}" /> struct.</summary>
        /// <param name="array">The array that is used to populate the list.</param>
        /// <param name="takeOwnership"><c>true</c> if the list should take ownership of the array; otherwise, <c>false</c>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array" /> is <c>null</c>.</exception>
        public ValueList(T[] array, bool takeOwnership = false)
        {
            ThrowIfNull(array, nameof(array));

            if (takeOwnership)
            {
                _items = array;
            }
            else
            {
                var items = GC.AllocateUninitializedArray<T>(array.Length);
                Array.Copy(array, items, array.Length);
                _items = items;
            }

            _count = array.Length;
            _version = 0;
        }

        /// <summary>Gets the number of items that can be contained by the list without being resized.</summary>
        public readonly int Capacity
        {
            get
            {
                var items = _items;

                if (items is not null)
                {
                    return _items.Length;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>Gets the number of items contained in the list.</summary>
        public readonly int Count => _count;

        /// <summary>Gets or sets the item at the specified index in the list.</summary>
        /// <param name="index">The index of the item to get or set.</param>
        /// <returns>The item that exists at <paramref name="index" /> in the list.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is negative or greater than or equal to <see cref="Count" />.</exception>
        public T this[int index]
        {
            readonly get
            {
                ThrowIfNotInBounds(index, Count, nameof(index), nameof(Count));
                return _items[index];
            }

            set
            {
                ThrowIfNotInBounds(index, Count, nameof(index), nameof(Count));
                _version++;

                _items[index] = value;
            }
        }

        /// <summary>Adds an item to the list.</summary>
        /// <param name="item">The item to add to the list.</param>
        public void Add(T item)
        {
            var count = Count;
            var newCount = count + 1;

            if (newCount < Capacity)
            {
                _version++;
            }
            else
            {
                EnsureCapacity(count + 1);
            }

            _count = newCount;
            _items[count] = item;
        }

        /// <summary>Removes all items from the list.</summary>
        public void Clear()
        {
            _version++;

            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                Array.Clear(_items, 0, Count);
            }

            _count = 0;
        }

        /// <summary>Checks whether the list contains a specified item.</summary>
        /// <param name="item">The item to check for in the list.</param>
        /// <returns><c>true</c> if <paramref name="item" /> was found in the list; otherwise, <c>false</c>.</returns>
        public readonly bool Contains(T item) => IndexOf(item) != -1;

        /// <summary>Copies the items of the list to a span.</summary>
        /// <param name="destination">The span to which the items will be copied.</param>
        /// <exception cref="ArgumentException"><see cref="Count" /> is greater than the length of <paramref name="destination" />.</exception>
        public readonly void CopyTo(Span<T> destination) => _items.AsSpan(0, Count).CopyTo(destination);

        /// <summary>Ensures the capacity of the list is at least the specified value.</summary>
        /// <param name="capacity">The minimum capacity the list should support.</param>
        /// <remarks>This method does not throw if <paramref name="capacity" /> is negative and is instead does nothing.</remarks>
        public void EnsureCapacity(int capacity)
        {
            var currentCapacity = Capacity;

            if (capacity > currentCapacity)
            {
                var newCapacity = Max(capacity, currentCapacity * 2);

                var newItems = GC.AllocateUninitializedArray<T>(newCapacity);
                var items = _items;

                if (items is not null)
                {
                    Array.Copy(items, newItems, currentCapacity);
                }

                _version++;
                _items = newItems;
            }
        }

        /// <summary>Gets the index of the first occurence of an item in the list.</summary>
        /// <param name="item">The item to find in the list.</param>
        /// <returns>The index of <paramref name="item" /> if it was found in the list; otherwise, <c>-1</c>.</returns>
        public readonly int IndexOf(T item)
        {
            var items = _items;

            if (items is not null)
            {
                return Array.IndexOf(items, item, 0, Count);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>Inserts an item into list at the specified index.</summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> is inserted.</param>
        /// <param name="item">The item to insert into the list.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is negative or greater than <see cref="Count" />.</exception>
        public void Insert(int index, T item)
        {
            var count = Count;
            ThrowIfNotInInsertBounds(index, count, nameof(index), nameof(Count));

            if (index != count)
            {
                _version++;
            }
            else
            {
                var newCount = count + 1;
                EnsureCapacity(newCount);
                _count = newCount;
            }

            _items[index] = item;
        }

        /// <summary>Removes the first occurence of an item from the list.</summary>
        /// <param name="item">The item to remove from the list.</param>
        /// <returns><c>true</c> if <paramref name="item" /> was removed from the list; otherwise, <c>false</c>.</returns>
        public bool Remove(T item)
        {
            var index = IndexOf(item);

            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>Removes the item at the specified index from the list.</summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is negative or greater than or equal to <see cref="Count" />.</exception>
        public void RemoveAt(int index)
        {
            var count = Count;
            ThrowIfNotInBounds(index, count, nameof(index), nameof(Count));

            var newCount = count - 1;
            var items = _items;

            _version++;

            if (index < newCount)
            {
                Array.Copy(items, index + 1, items, index, newCount - index);
            }

            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                items[newCount] = default!;
            }

            _count = newCount;
        }
    }
}
