using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000410 RID: 1040
	public class FormViewUpdatedEventArgs : EventArgs
	{
		// Token: 0x06003240 RID: 12864 RVA: 0x000A39ED File Offset: 0x000A1BED
		public FormViewUpdatedEventArgs(int affectedRows, Exception e)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = e;
			this._keepInEditMode = false;
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x000A3A11 File Offset: 0x000A1C11
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06003242 RID: 12866 RVA: 0x000A3A19 File Offset: 0x000A1C19
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x000A3A21 File Offset: 0x000A1C21
		// (set) Token: 0x06003244 RID: 12868 RVA: 0x000A3A29 File Offset: 0x000A1C29
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

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003245 RID: 12869 RVA: 0x000A3A32 File Offset: 0x000A1C32
		// (set) Token: 0x06003246 RID: 12870 RVA: 0x000A3A3A File Offset: 0x000A1C3A
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

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x000A3A43 File Offset: 0x000A1C43
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

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003248 RID: 12872 RVA: 0x000A3A5E File Offset: 0x000A1C5E
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

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06003249 RID: 12873 RVA: 0x000A3A79 File Offset: 0x000A1C79
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

		// Token: 0x0600324A RID: 12874 RVA: 0x000A3A94 File Offset: 0x000A1C94
		internal void SetKeys(IOrderedDictionary keys)
		{
			this._keys = keys;
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000A3A9D File Offset: 0x000A1C9D
		internal void SetNewValues(IOrderedDictionary newValues)
		{
			this._values = newValues;
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000A3AA6 File Offset: 0x000A1CA6
		internal void SetOldValues(IOrderedDictionary oldValues)
		{
			this._oldValues = oldValues;
		}

		// Token: 0x0400210C RID: 8460
		private int _affectedRows;

		// Token: 0x0400210D RID: 8461
		private Exception _exception;

		// Token: 0x0400210E RID: 8462
		private bool _exceptionHandled;

		// Token: 0x0400210F RID: 8463
		private bool _keepInEditMode;

		// Token: 0x04002110 RID: 8464
		private IOrderedDictionary _values;

		// Token: 0x04002111 RID: 8465
		private IOrderedDictionary _keys;

		// Token: 0x04002112 RID: 8466
		private IOrderedDictionary _oldValues;
	}
}
