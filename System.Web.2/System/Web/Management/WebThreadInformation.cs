using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace System.Web.Management
{
	// Token: 0x0200019D RID: 413
	public sealed class WebThreadInformation
	{
		// Token: 0x060015DA RID: 5594 RVA: 0x000434F8 File Offset: 0x000416F8
		internal WebThreadInformation(Exception exception)
		{
			this._threadId = Thread.CurrentThread.ManagedThreadId;
			this._accountName = HttpApplication.GetCurrentWindowsIdentityWithAssert().Name;
			if (exception != null)
			{
				this._stackTrace = new StackTrace(exception, true).ToString();
				this._isImpersonating = exception.Data.Contains("ASPIMPERSONATING");
				return;
			}
			this._stackTrace = string.Empty;
			this._isImpersonating = false;
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x00043569 File Offset: 0x00041769
		public int ThreadID
		{
			get
			{
				return this._threadId;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x00043571 File Offset: 0x00041771
		public string ThreadAccountName
		{
			get
			{
				return this._accountName;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x00043579 File Offset: 0x00041779
		public string StackTrace
		{
			get
			{
				return this._stackTrace;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00043581 File Offset: 0x00041781
		public bool IsImpersonating
		{
			get
			{
				return this._isImpersonating;
			}
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0004358C File Offset: 0x0004178C
		public void FormatToString(WebEventFormatter formatter)
		{
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_thread_id", this.ThreadID.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_thread_account_name", this.ThreadAccountName));
			if (this.IsImpersonating)
			{
				formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_is_impersonating"));
			}
			else
			{
				formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_is_not_impersonating"));
			}
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_stack_trace", this.StackTrace));
		}

		// Token: 0x04001663 RID: 5731
		private int _threadId;

		// Token: 0x04001664 RID: 5732
		private string _accountName;

		// Token: 0x04001665 RID: 5733
		private string _stackTrace;

		// Token: 0x04001666 RID: 5734
		private bool _isImpersonating;

		// Token: 0x04001667 RID: 5735
		internal const string IsImpersonatingKey = "ASPIMPERSONATING";
	}
}
