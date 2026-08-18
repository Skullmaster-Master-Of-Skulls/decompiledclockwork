using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000A1 RID: 161
	public class LinqDataSourceInsertEventArgs : CancelEventArgs
	{
		// Token: 0x0600071A RID: 1818 RVA: 0x0001CF60 File Offset: 0x0001B160
		public LinqDataSourceInsertEventArgs(object newObject)
		{
			this._newObject = newObject;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001CF6F File Offset: 0x0001B16F
		public LinqDataSourceInsertEventArgs(LinqDataSourceValidationException exception)
		{
			this._exception = exception;
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001CF7E File Offset: 0x0001B17E
		public LinqDataSourceValidationException Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001CF86 File Offset: 0x0001B186
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001CF8E File Offset: 0x0001B18E
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

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001CF97 File Offset: 0x0001B197
		public object NewObject
		{
			get
			{
				return this._newObject;
			}
		}

		// Token: 0x04000260 RID: 608
		private LinqDataSourceValidationException _exception;

		// Token: 0x04000261 RID: 609
		private bool _exceptionHandled;

		// Token: 0x04000262 RID: 610
		private object _newObject;
	}
}
