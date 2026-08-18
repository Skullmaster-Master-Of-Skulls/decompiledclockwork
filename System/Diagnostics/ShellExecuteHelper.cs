using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x0200077E RID: 1918
	internal class ShellExecuteHelper
	{
		// Token: 0x06003B4C RID: 15180 RVA: 0x000FC353 File Offset: 0x000FB353
		public ShellExecuteHelper(NativeMethods.ShellExecuteInfo executeInfo)
		{
			this._executeInfo = executeInfo;
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x000FC364 File Offset: 0x000FB364
		public void ShellExecuteFunction()
		{
			if (!(this._succeeded = NativeMethods.ShellExecuteEx(this._executeInfo)))
			{
				this._errorCode = Marshal.GetLastWin32Error();
			}
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x000FC394 File Offset: 0x000FB394
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

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06003B4F RID: 15183 RVA: 0x000FC3E2 File Offset: 0x000FB3E2
		public int ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x040033E9 RID: 13289
		private NativeMethods.ShellExecuteInfo _executeInfo;

		// Token: 0x040033EA RID: 13290
		private int _errorCode;

		// Token: 0x040033EB RID: 13291
		private bool _succeeded;
	}
}
