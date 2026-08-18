using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003D9 RID: 985
	public class DetailsViewDeletedEventArgs : EventArgs
	{
		// Token: 0x06003025 RID: 12325 RVA: 0x0009E3FA File Offset: 0x0009C5FA
		public DetailsViewDeletedEventArgs(int affectedRows, Exception e)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = e;
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003026 RID: 12326 RVA: 0x0009E417 File Offset: 0x0009C617
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06003027 RID: 12327 RVA: 0x0009E41F File Offset: 0x0009C61F
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06003028 RID: 12328 RVA: 0x0009E427 File Offset: 0x0009C627
		// (set) Token: 0x06003029 RID: 12329 RVA: 0x0009E42F File Offset: 0x0009C62F
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

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x0600302A RID: 12330 RVA: 0x0009E438 File Offset: 0x0009C638
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

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x0600302B RID: 12331 RVA: 0x0009E453 File Offset: 0x0009C653
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

		// Token: 0x0600302C RID: 12332 RVA: 0x0009E46E File Offset: 0x0009C66E
		internal void SetKeys(IOrderedDictionary keys)
		{
			this._keys = keys;
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x0009E477 File Offset: 0x0009C677
		internal void SetValues(IOrderedDictionary values)
		{
			this._values = values;
		}

		// Token: 0x04002070 RID: 8304
		private int _affectedRows;

		// Token: 0x04002071 RID: 8305
		private Exception _exception;

		// Token: 0x04002072 RID: 8306
		private bool _exceptionHandled;

		// Token: 0x04002073 RID: 8307
		private IOrderedDictionary _keys;

		// Token: 0x04002074 RID: 8308
		private IOrderedDictionary _values;
	}
}
