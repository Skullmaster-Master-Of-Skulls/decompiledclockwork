using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000BB RID: 187
	public class ListViewUpdatedEventArgs : EventArgs
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x00022738 File Offset: 0x00020938
		public ListViewUpdatedEventArgs(int affectedRows, Exception exception)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = exception;
			this._keepInEditMode = false;
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0002275C File Offset: 0x0002095C
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00022764 File Offset: 0x00020964
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0002276C File Offset: 0x0002096C
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x00022774 File Offset: 0x00020974
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

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0002277D File Offset: 0x0002097D
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00022785 File Offset: 0x00020985
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

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0002278E File Offset: 0x0002098E
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

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x000227A9 File Offset: 0x000209A9
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

		// Token: 0x06000905 RID: 2309 RVA: 0x000227C4 File Offset: 0x000209C4
		internal void SetKeys(IOrderedDictionary keys)
		{
			this._keys = keys;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000227CD File Offset: 0x000209CD
		internal void SetNewValues(IOrderedDictionary newValues)
		{
			this._values = newValues;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000227D6 File Offset: 0x000209D6
		internal void SetOldValues(IOrderedDictionary oldValues)
		{
			this._oldValues = oldValues;
		}

		// Token: 0x040002FA RID: 762
		private int _affectedRows;

		// Token: 0x040002FB RID: 763
		private Exception _exception;

		// Token: 0x040002FC RID: 764
		private bool _exceptionHandled;

		// Token: 0x040002FD RID: 765
		private IOrderedDictionary _values;

		// Token: 0x040002FE RID: 766
		private IOrderedDictionary _keys;

		// Token: 0x040002FF RID: 767
		private IOrderedDictionary _oldValues;

		// Token: 0x04000300 RID: 768
		private bool _keepInEditMode;
	}
}
