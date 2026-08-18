using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200042A RID: 1066
	public class GridViewUpdatedEventArgs : EventArgs
	{
		// Token: 0x060033C4 RID: 13252 RVA: 0x000A91EC File Offset: 0x000A73EC
		public GridViewUpdatedEventArgs(int affectedRows, Exception e)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = e;
			this._keepInEditMode = false;
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x000A9210 File Offset: 0x000A7410
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x000A9218 File Offset: 0x000A7418
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x000A9220 File Offset: 0x000A7420
		// (set) Token: 0x060033C8 RID: 13256 RVA: 0x000A9228 File Offset: 0x000A7428
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

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x060033C9 RID: 13257 RVA: 0x000A9231 File Offset: 0x000A7431
		// (set) Token: 0x060033CA RID: 13258 RVA: 0x000A9239 File Offset: 0x000A7439
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

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x060033CB RID: 13259 RVA: 0x000A9242 File Offset: 0x000A7442
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

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x000A925D File Offset: 0x000A745D
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

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x000A9278 File Offset: 0x000A7478
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

		// Token: 0x060033CE RID: 13262 RVA: 0x000A9293 File Offset: 0x000A7493
		internal void SetKeys(IOrderedDictionary keys)
		{
			this._keys = keys;
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000A929C File Offset: 0x000A749C
		internal void SetNewValues(IOrderedDictionary newValues)
		{
			this._values = newValues;
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000A92A5 File Offset: 0x000A74A5
		internal void SetOldValues(IOrderedDictionary oldValues)
		{
			this._oldValues = oldValues;
		}

		// Token: 0x04002178 RID: 8568
		private int _affectedRows;

		// Token: 0x04002179 RID: 8569
		private Exception _exception;

		// Token: 0x0400217A RID: 8570
		private bool _exceptionHandled;

		// Token: 0x0400217B RID: 8571
		private IOrderedDictionary _values;

		// Token: 0x0400217C RID: 8572
		private IOrderedDictionary _keys;

		// Token: 0x0400217D RID: 8573
		private IOrderedDictionary _oldValues;

		// Token: 0x0400217E RID: 8574
		private bool _keepInEditMode;
	}
}
