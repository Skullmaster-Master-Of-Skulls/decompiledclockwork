using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000AE RID: 174
	public class ListViewDeletedEventArgs : EventArgs
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x0002227A File Offset: 0x0002047A
		public ListViewDeletedEventArgs(int affectedRows, Exception exception)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = exception;
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00022297 File Offset: 0x00020497
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0002229F File Offset: 0x0002049F
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x000222A7 File Offset: 0x000204A7
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x000222AF File Offset: 0x000204AF
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

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x000222B8 File Offset: 0x000204B8
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

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x000222D3 File Offset: 0x000204D3
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

		// Token: 0x060008BB RID: 2235 RVA: 0x000222EE File Offset: 0x000204EE
		internal void SetKeys(IOrderedDictionary keys)
		{
			this._keys = keys;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000222F7 File Offset: 0x000204F7
		internal void SetValues(IOrderedDictionary values)
		{
			this._values = values;
		}

		// Token: 0x040002DB RID: 731
		private int _affectedRows;

		// Token: 0x040002DC RID: 732
		private Exception _exception;

		// Token: 0x040002DD RID: 733
		private bool _exceptionHandled;

		// Token: 0x040002DE RID: 734
		private IOrderedDictionary _keys;

		// Token: 0x040002DF RID: 735
		private IOrderedDictionary _values;
	}
}
