using System;
using System.Globalization;
using System.Text;

namespace System.Web.Management
{
	// Token: 0x02000199 RID: 409
	public sealed class WebProcessInformation
	{
		// Token: 0x060015B8 RID: 5560 RVA: 0x00042D90 File Offset: 0x00040F90
		internal WebProcessInformation()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (UnsafeNativeMethods.GetModuleFileName(IntPtr.Zero, stringBuilder, 256) == 0)
			{
				this._processName = string.Empty;
			}
			else
			{
				this._processName = stringBuilder.ToString();
				int num = this._processName.LastIndexOf('\\');
				if (num != -1)
				{
					this._processName = this._processName.Substring(num + 1);
				}
			}
			this._processId = SafeNativeMethods.GetCurrentProcessId();
			this._accountName = HttpRuntime.WpUserId;
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00042E15 File Offset: 0x00041015
		public int ProcessID
		{
			get
			{
				return this._processId;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00042E1D File Offset: 0x0004101D
		public string ProcessName
		{
			get
			{
				return this._processName;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x00042E25 File Offset: 0x00041025
		public string AccountName
		{
			get
			{
				if (this._accountName == null)
				{
					return string.Empty;
				}
				return this._accountName;
			}
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00042E3C File Offset: 0x0004103C
		public void FormatToString(WebEventFormatter formatter)
		{
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_process_id", this.ProcessID.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_process_name", this.ProcessName));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_account_name", this.AccountName));
		}

		// Token: 0x04001649 RID: 5705
		private int _processId;

		// Token: 0x0400164A RID: 5706
		private string _processName;

		// Token: 0x0400164B RID: 5707
		private string _accountName;
	}
}
