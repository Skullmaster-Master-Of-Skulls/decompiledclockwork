using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004F6 RID: 1270
	internal class ShellExecuteHelper
	{
		// Token: 0x06003031 RID: 12337 RVA: 0x000D9CDA File Offset: 0x000D7EDA
		public ShellExecuteHelper(NativeMethods.ShellExecuteInfo executeInfo)
		{
			this._executeInfo = executeInfo;
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x000D9CEC File Offset: 0x000D7EEC
		public void ShellExecuteFunction()
		{
			if (!(this._succeeded = NativeMethods.ShellExecuteEx(this._executeInfo)))
			{
				this._errorCode = Marshal.GetLastWin32Error();
			}
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000D9D1C File Offset: 0x000D7F1C
		public bool ShellExecuteOnSTAThread()
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				ThreadStart start = new ThreadStart(this.ShellExecuteFunction);
				Thread thread = new Thread(start);
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
				thread.Join();
			}
			else
			{
				this.ShellExecuteFunction();
			}
			return this._succeeded;
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06003034 RID: 12340 RVA: 0x000D9D6A File Offset: 0x000D7F6A
		public int ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x0400288A RID: 10378
		private NativeMethods.ShellExecuteInfo _executeInfo;

		// Token: 0x0400288B RID: 10379
		private int _errorCode;

		// Token: 0x0400288C RID: 10380
		private bool _succeeded;
	}
}
