using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x0200077F RID: 1919
	internal class MainWindowFinder
	{
		// Token: 0x06003B50 RID: 15184 RVA: 0x000FC3EC File Offset: 0x000FB3EC
		public IntPtr FindMainWindow(int processId)
		{
			this.bestHandle = (IntPtr)0;
			this.processId = processId;
			NativeMethods.EnumThreadWindowsCallback enumThreadWindowsCallback = new NativeMethods.EnumThreadWindowsCallback(this.EnumWindowsCallback);
			NativeMethods.EnumWindows(enumThreadWindowsCallback, IntPtr.Zero);
			GC.KeepAlive(enumThreadWindowsCallback);
			return this.bestHandle;
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000FC431 File Offset: 0x000FB431
		private bool IsMainWindow(IntPtr handle)
		{
			return !(NativeMethods.GetWindow(new HandleRef(this, handle), 4) != (IntPtr)0) && NativeMethods.IsWindowVisible(new HandleRef(this, handle));
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x000FC460 File Offset: 0x000FB460
		private bool EnumWindowsCallback(IntPtr handle, IntPtr extraParameter)
		{
			int num;
			NativeMethods.GetWindowThreadProcessId(new HandleRef(this, handle), out num);
			if (num == this.processId && this.IsMainWindow(handle))
			{
				this.bestHandle = handle;
				return false;
			}
			return true;
		}

		// Token: 0x040033EC RID: 13292
		private IntPtr bestHandle;

		// Token: 0x040033ED RID: 13293
		private int processId;
	}
}
