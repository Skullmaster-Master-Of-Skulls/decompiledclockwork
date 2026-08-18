using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200041A RID: 1050
	public class GridViewDeletedEventArgs : EventArgs
	{
		// Token: 0x06003375 RID: 13173 RVA: 0x000A8F3A File Offset: 0x000A713A
		public GridViewDeletedEventArgs(int affectedRows, Exception e)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = e;
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x000A8F57 File Offset: 0x000A7157
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x000A8F5F File Offset: 0x000A715F
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06003378 RID: 13176 RVA: 0x000A8F67 File Offset: 0x000A7167
		// (set) Token: 0x06003379 RID: 13177 RVA: 0x000A8F6F File Offset: 0x000A716F
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

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x000A8F78 File Offset: 0x000A7178
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

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x000A8F93 File Offset: 0x000A7193
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

		// Token: 0x0600337C RID: 13180 RVA: 0x000A8FAE File Offset: 0x000A71AE
		internal void SetKeys(IOrderedDictionary keys)
		{
			this._keys = keys;
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000A8FB7 File Offset: 0x000A71B7
		internal void SetValues(IOrderedDictionary values)
		{
			this._values = values;
		}

		// Token: 0x04002164 RID: 8548
		private int _affectedRows;

		// Token: 0x04002165 RID: 8549
		private Exception _exception;

		// Token: 0x04002166 RID: 8550
		private bool _exceptionHandled;

		// Token: 0x04002167 RID: 8551
		private IOrderedDictionary _keys;

		// Token: 0x04002168 RID: 8552
		private IOrderedDictionary _values;
	}
}
