using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200009E RID: 158
	public class LinqDataSourceDeleteEventArgs : CancelEventArgs
	{
		// Token: 0x0600070F RID: 1807 RVA: 0x0001CE17 File Offset: 0x0001B017
		public LinqDataSourceDeleteEventArgs(object originalObject)
		{
			this._originalObject = originalObject;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001CE26 File Offset: 0x0001B026
		public LinqDataSourceDeleteEventArgs(LinqDataSourceValidationException exception)
		{
			this._exception = exception;
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001CE35 File Offset: 0x0001B035
		public LinqDataSourceValidationException Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001CE3D File Offset: 0x0001B03D
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0001CE45 File Offset: 0x0001B045
		public bool ExceptionHandled
		{
			get
			{
				return this._exceptionHandled;
			}
			set
			{
				this._exceptionHandled = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001CE4E File Offset: 0x0001B04E
		public object OriginalObject
		{
			get
			{
				return this._originalObject;
			}
		}

		// Token: 0x0400025C RID: 604
		private LinqDataSourceValidationException _exception;

		// Token: 0x0400025D RID: 605
		private bool _exceptionHandled;

		// Token: 0x0400025E RID: 606
		private object _originalObject;
	}
}
