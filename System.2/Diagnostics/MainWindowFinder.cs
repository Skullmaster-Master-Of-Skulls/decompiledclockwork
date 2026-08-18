using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004F7 RID: 1271
	internal class MainWindowFinder
	{
		// Token: 0x06003035 RID: 12341 RVA: 0x000D9D74 File Offset: 0x000D7F74
		public IntPtr FindMainWindow(int processId)
		{
			this.bestHandle = (IntPtr)0;
			this.processId = processId;
			NativeMethods.EnumThreadWindowsCallback enumThreadWindowsCallback = new NativeMethods.EnumThreadWindowsCallback(this.EnumWindowsCallback);
			NativeMethods.EnumWindows(enumThreadWindowsCallback, IntPtr.Zero);
			GC.KeepAlive(enumThreadWindowsCallback);
			return this.bestHandle;
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000D9DB9 File Offset: 0x000D7FB9
		private bool IsMainWindow(IntPtr handle)
		{
			return !(NativeMethods.GetWindow(new HandleRef(this, handle), 4) != (IntPtr)0) && NativeMethods.IsWindowVisible(new HandleRef(this, handle));
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x000D9DE8 File Offset: 0x000D7FE8
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

		// Token: 0x0400288D RID: 10381
		private IntPtr bestHandle;

		// Token: 0x0400288E RID: 10382
		private int processId;
	}
}
