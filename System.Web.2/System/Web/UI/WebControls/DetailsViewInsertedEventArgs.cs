using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003DD RID: 989
	public class DetailsViewInsertedEventArgs : EventArgs
	{
		// Token: 0x0600303A RID: 12346 RVA: 0x0009E4CE File Offset: 0x0009C6CE
		public DetailsViewInsertedEventArgs(int affectedRows, Exception e)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = e;
			this._keepInInsertMode = false;
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x0009E4F2 File Offset: 0x0009C6F2
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x0009E4FA File Offset: 0x0009C6FA
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x0600303D RID: 12349 RVA: 0x0009E502 File Offset: 0x0009C702
		// (set) Token: 0x0600303E RID: 12350 RVA: 0x0009E50A File Offset: 0x0009C70A
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

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x0009E513 File Offset: 0x0009C713
		// (set) Token: 0x06003040 RID: 12352 RVA: 0x0009E51B File Offset: 0x0009C71B
		public bool KeepInInsertMode
		{
			get
			{
				return this._keepInInsertMode;
			}
			set
			{
				this._keepInInsertMode = value;
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x0009E524 File Offset: 0x0009C724
		public IOrderedDictionary Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new OrderedDictionary();
				}
				return this._values;
			}
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x0009E53F File Offset: 0x0009C73F
		internal void SetValues(IOrderedDictionary values)
		{
			this._values = values;
		}

		// Token: 0x04002078 RID: 8312
		private int _affectedRows;

		// Token: 0x04002079 RID: 8313
		private Exception _exception;

		// Token: 0x0400207A RID: 8314
		private bool _exceptionHandled;

		// Token: 0x0400207B RID: 8315
		private bool _keepInInsertMode;

		// Token: 0x0400207C RID: 8316
		private IOrderedDictionary _values;
	}
}
