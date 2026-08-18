using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000A4 RID: 164
	public class LinqDataSourceUpdateEventArgs : CancelEventArgs
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x0001D087 File Offset: 0x0001B287
		public LinqDataSourceUpdateEventArgs(object originalObject, object newObject)
		{
			this._originalObject = originalObject;
			this._newObject = newObject;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001D09D File Offset: 0x0001B29D
		public LinqDataSourceUpdateEventArgs(LinqDataSourceValidationException exception)
		{
			this._exception = exception;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001D0AC File Offset: 0x0001B2AC
		public LinqDataSourceValidationException Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001D0B4 File Offset: 0x0001B2B4
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001D0BC File Offset: 0x0001B2BC
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

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001D0C5 File Offset: 0x0001B2C5
		public object OriginalObject
		{
			get
			{
				return this._originalObject;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0001D0CD File Offset: 0x0001B2CD
		public object NewObject
		{
			get
			{
				return this._newObject;
			}
		}

		// Token: 0x0400026E RID: 622
		private LinqDataSourceValidationException _exception;

		// Token: 0x0400026F RID: 623
		private bool _exceptionHandled;

		// Token: 0x04000270 RID: 624
		private object _originalObject;

		// Token: 0x04000271 RID: 625
		private object _newObject;
	}
}
