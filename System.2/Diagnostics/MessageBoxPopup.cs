using System;
using System.Security;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x02000492 RID: 1170
	internal class MessageBoxPopup
	{
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x000C51F5 File Offset: 0x000C33F5
		// (set) Token: 0x06002B5F RID: 11103 RVA: 0x000C51FD File Offset: 0x000C33FD
		public int ReturnValue { get; set; }

		// Token: 0x06002B60 RID: 11104 RVA: 0x000C5206 File Offset: 0x000C3406
		[SecurityCritical]
		public MessageBoxPopup(string body, string title, int flags)
		{
			this.m_Event = new AutoResetEvent(false);
			this.m_Body = body;
			this.m_Title = title;
			this.m_Flags = flags;
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x000C5230 File Offset: 0x000C3430
		public int ShowMessageBox()
		{
			Thread thread = new Thread(new ThreadStart(this.DoPopup));
			thread.Start();
			this.m_Event.WaitOne();
			return this.ReturnValue;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000C5267 File Offset: 0x000C3467
		[SecuritySafeCritical]
		public void DoPopup()
		{
			this.ReturnValue = SafeNativeMethods.MessageBox(IntPtr.Zero, this.m_Body, this.m_Title, this.m_Flags);
			this.m_Event.Set();
		}

		// Token: 0x04002686 RID: 9862
		private AutoResetEvent m_Event;

		// Token: 0x04002687 RID: 9863
		private string m_Body;

		// Token: 0x04002688 RID: 9864
		private string m_Title;

		// Token: 0x04002689 RID: 9865
		private int m_Flags;
	}
}
