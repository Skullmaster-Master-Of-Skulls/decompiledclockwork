using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000401 RID: 1025
	public class FormViewDeletedEventArgs : EventArgs
	{
		// Token: 0x060031FF RID: 12799 RVA: 0x000A372A File Offset: 0x000A192A
		public FormViewDeletedEventArgs(int affectedRows, Exception e)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = e;
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x000A3747 File Offset: 0x000A1947
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06003201 RID: 12801 RVA: 0x000A374F File Offset: 0x000A194F
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06003202 RID: 12802 RVA: 0x000A3757 File Offset: 0x000A1957
		// (set) Token: 0x06003203 RID: 12803 RVA: 0x000A375F File Offset: 0x000A195F
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

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06003204 RID: 12804 RVA: 0x000A3768 File Offset: 0x000A1968
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

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x000A3783 File Offset: 0x000A1983
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

		// Token: 0x06003206 RID: 12806 RVA: 0x000A379E File Offset: 0x000A199E
		internal void SetKeys(IOrderedDictionary keys)
		{
			this._keys = keys;
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x000A37A7 File Offset: 0x000A19A7
		internal void SetValues(IOrderedDictionary values)
		{
			this._values = values;
		}

		// Token: 0x040020F2 RID: 8434
		private int _affectedRows;

		// Token: 0x040020F3 RID: 8435
		private Exception _exception;

		// Token: 0x040020F4 RID: 8436
		private bool _exceptionHandled;

		// Token: 0x040020F5 RID: 8437
		private IOrderedDictionary _keys;

		// Token: 0x040020F6 RID: 8438
		private IOrderedDictionary _values;
	}
}
