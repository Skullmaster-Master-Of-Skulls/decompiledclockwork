using System;

namespace System.ServiceModel.Security
{
	// Token: 0x02000375 RID: 885
	public class WSTrustRequestProcessingErrorEventArgs : EventArgs
	{
		// Token: 0x060020B9 RID: 8377 RVA: 0x00078CA8 File Offset: 0x00076EA8
		public WSTrustRequestProcessingErrorEventArgs(string requestType, Exception exception)
		{
			this._exception = exception;
			this._requestType = requestType;
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x00078CBE File Offset: 0x00076EBE
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x00078CC6 File Offset: 0x00076EC6
		public string RequestType
		{
			get
			{
				return this._requestType;
			}
		}

		// Token: 0x04001F24 RID: 7972
		private Exception _exception;

		// Token: 0x04001F25 RID: 7973
		private string _requestType;
	}
}
