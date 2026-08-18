using System;

namespace System.Web.Management
{
	// Token: 0x02000008 RID: 8
	public class WebServiceErrorEvent : WebRequestErrorEvent
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002728 File Offset: 0x00000928
		public static int WebServiceErrorEventCode
		{
			get
			{
				return 100001;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000272F File Offset: 0x0000092F
		protected internal WebServiceErrorEvent(string message, object eventSource, Exception exception) : base(message, eventSource, WebServiceErrorEvent.WebServiceErrorEventCode, exception)
		{
		}

		// Token: 0x0400000D RID: 13
		private const int _webServiceErrorEventCode = 100001;
	}
}
