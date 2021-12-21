// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Diagnostics;
using TerraFX.Threading;
using static TerraFX.Utilities.ExceptionUtilities;

namespace TerraFX.Collections;

/// <summary>Represents a pool of unmanaged items.</summary>
/// <typeparam name="T">The type of the unmanaged items contained in the pool.</typeparam>
/// <remarks>This type is meant to be used as an implementation detail of another type and should not be part of your public surface area.</remarks>
[DebuggerDisplay("Capacity = {Capacity}; Count = {Count}")]
[DebuggerTypeProxy(typeof(UnmanagedValuePool<>.DebugView))]
public unsafe partial struct UnmanagedValuePool<T>
    where T : unmanaged
{
    private UnmanagedValueQueue<T> _availableItems;
    private UnmanagedValueList<T> _items;

    /// <summary>Initializes a new instance of the <see cref="UnmanagedValuePool{T}" /> struct.</summary>
    /// <param name="capacity">The initial capacity of the pool.</param>
    public UnmanagedValuePool(nuint capacity)
    {
        _availableItems = new UnmanagedValueQueue<T>(capacity);
        _items = new UnmanagedValueList<T>(capacity);
    }

    /// <summary>Gets the number of items available in the pool.</summary>
    public readonly nuint AvailableCount => _availableItems.Count;

    /// <summary>Gets the number of items that can be contained by the pool without being resized.</summary>
    public readonly nuint Capacity => _items.Capacity;

    /// <summary>Gets the number of items contained in the pool.</summary>
    public readonly nuint Count => _items.Count;

    /// <summary>Rents an item from the pool, creating a new item if none are available.</summary>
    /// <param name="createItem">A pointer to the function to invoke if a new item needs to be created.</param>
    /// <returns>A rented item.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="createItem" /> is <c>null</c>.</exception>
    public T Rent(delegate*<T> createItem)
    {
        ThrowIfNull(createItem);

        if (!_availableItems.TryDequeue(out var item))
        {
            item = createItem();
            _items.Add(item);
        }

        return item;
    }

    /// <summary>Rents an item from the pool, creating a new item if none are available.</summary>
    /// <param name="createItem">A pointer to the function to invoke if a new item needs to be created.</param>
    /// <param name="mutex">The mutex to use when renting an item from the list.</param>
    /// <returns>A rented item.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="createItem" /> is <c>null</c>.</exception>
    public T Rent(delegate*<T> createItem, ValueMutex mutex)
    {
        using var disposableMutex = new DisposableMutex(mutex, isExternallySynchronized: false);
        return Rent(createItem);
    }

    /// <summary>Returns an item to the pool.</summary>
    /// <param name="item">The item that should be returned to the pool.</param>
    public void Return(T item)
    {
        _availableItems.Enqueue(item);
    }

    /// <summary>Returns an item to the pool.</summary>
    /// <param name="item">The item that should be returned to the pool.</param>
    /// <param name="mutex">The mutex to use when renting an item from the list.</param>
    /// <exception cref="ArgumentNullException"><paramref name="item" /> is <c>null</c>.</exception>
    public void Return(T item, ValueMutex mutex)
    {
        using var disposableMutex = new DisposableMutex(mutex, isExternallySynchronized: false);
        Return(item);
    }
}
