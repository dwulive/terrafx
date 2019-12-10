// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Threading;
using TerraFX.Interop;
using TerraFX.UI;
using TerraFX.Utilities;
using static TerraFX.Interop.User32;
using static TerraFX.Interop.Windows;
using static TerraFX.Utilities.AssertionUtilities;
using static TerraFX.Utilities.ExceptionUtilities;

namespace TerraFX.Provider.Win32.UI
{
    /// <summary>Provides a means of dispatching events for a thread.</summary>
    public sealed unsafe class Dispatcher : IDispatcher
    {
        private readonly DispatchProvider _dispatchProvider;
        private readonly Thread _parentThread;

        internal Dispatcher(DispatchProvider dispatchProvider, Thread parentThread)
        {
            Assert(dispatchProvider != null, Resources.ArgumentNullExceptionMessage, nameof(dispatchProvider));
            Assert(parentThread != null, Resources.ArgumentNullExceptionMessage, nameof(parentThread));

            _dispatchProvider = dispatchProvider!;
            _parentThread = parentThread!;
        }

        /// <inheritdoc />
        public event EventHandler? ExitRequested;

        /// <inheritdoc />
        public IDispatchProvider DispatchProvider => _dispatchProvider;

        /// <inheritdoc />
        public Thread ParentThread => _parentThread;

        /// <inheritdoc />
        public void DispatchPending()
        {
            ThrowIfNotThread(_parentThread);

            MSG msg;
            while (PeekMessageW(&msg, hWnd: IntPtr.Zero, wMsgFilterMin: WM_NULL, wMsgFilterMax: WM_NULL, wRemoveMsg: PM_REMOVE) != FALSE)
            {
                if (msg.message != WM_QUIT)
                {
                    _ = DispatchMessageW(&msg);
                }
                else
                {
                    OnExitRequested();
                }
            }
        }

        private void OnExitRequested() => ExitRequested?.Invoke(this, EventArgs.Empty);
    }
}
