using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003E9 RID: 1001
	public class DetailsViewUpdatedEventArgs : EventArgs
	{
		// Token: 0x0600306C RID: 12396 RVA: 0x0009E6A5 File Offset: 0x0009C8A5
		public DetailsViewUpdatedEventArgs(int affectedRows, Exception e)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = e;
			this._keepInEditMode = false;
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x0600306D RID: 12397 RVA: 0x0009E6C9 File Offset: 0x0009C8C9
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x0009E6D1 File Offset: 0x0009C8D1
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x0009E6D9 File Offset: 0x0009C8D9
		// (set) Token: 0x06003070 RID: 12400 RVA: 0x0009E6E1 File Offset: 0x0009C8E1
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

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x0009E6EA File Offset: 0x0009C8EA
		// (set) Token: 0x06003072 RID: 12402 RVA: 0x0009E6F2 File Offset: 0x0009C8F2
		public bool KeepInEditMode
		{
			get
			{
				return this._keepInEditMode;
			}
			set
			{
				this._keepInEditMode = value;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06003073 RID: 12403 RVA: 0x0009E6FB File Offset: 0x0009C8FB
		public IOrderedDictionary Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new OrderedDictionary();
				}
				return this._keys;
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x0009E716 File Offset: 0x0009C916
		public IOrderedDictionary NewValues
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

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06003075 RID: 12405 RVA: 0x0009E731 File Offset: 0x0009C931
		public IOrderedDictionary OldValues
		{
			get
			{
				if (this._oldValues == null)
				{
					this._oldValues = new OrderedDictionary();
				}
				return this._oldValues;
			}
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x0009E74C File Offset: 0x0009C94C
		internal void SetKeys(IOrderedDictionary keys)
		{
			this._keys = keys;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x0009E755 File Offset: 0x0009C955
		internal void SetNewValues(IOrderedDictionary newValues)
		{
			this._values = newValues;
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x0009E75E File Offset: 0x0009C95E
		internal void SetOldValues(IOrderedDictionary oldValues)
		{
			this._oldValues = oldValues;
		}

		// Token: 0x0400208A RID: 8330
		private int _affectedRows;

		// Token: 0x0400208B RID: 8331
		private Exception _exception;

		// Token: 0x0400208C RID: 8332
		private bool _exceptionHandled;

		// Token: 0x0400208D RID: 8333
		private bool _keepInEditMode;

		// Token: 0x0400208E RID: 8334
		private IOrderedDictionary _values;

		// Token: 0x0400208F RID: 8335
		private IOrderedDictionary _keys;

		// Token: 0x04002090 RID: 8336
		private IOrderedDictionary _oldValues;
	}
}
