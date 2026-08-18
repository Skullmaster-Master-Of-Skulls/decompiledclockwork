using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000405 RID: 1029
	public class FormViewInsertedEventArgs : EventArgs
	{
		// Token: 0x06003214 RID: 12820 RVA: 0x000A37FE File Offset: 0x000A19FE
		public FormViewInsertedEventArgs(int affectedRows, Exception e)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = e;
			this._keepInInsertMode = false;
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x000A3822 File Offset: 0x000A1A22
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x000A382A File Offset: 0x000A1A2A
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x000A3832 File Offset: 0x000A1A32
		// (set) Token: 0x06003218 RID: 12824 RVA: 0x000A383A File Offset: 0x000A1A3A
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

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000A3843 File Offset: 0x000A1A43
		// (set) Token: 0x0600321A RID: 12826 RVA: 0x000A384B File Offset: 0x000A1A4B
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

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x000A3854 File Offset: 0x000A1A54
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

		// Token: 0x0600321C RID: 12828 RVA: 0x000A386F File Offset: 0x000A1A6F
		internal void SetValues(IOrderedDictionary values)
		{
			this._values = values;
		}

		// Token: 0x040020FA RID: 8442
		private int _affectedRows;

		// Token: 0x040020FB RID: 8443
		private Exception _exception;

		// Token: 0x040020FC RID: 8444
		private bool _exceptionHandled;

		// Token: 0x040020FD RID: 8445
		private bool _keepInInsertMode;

		// Token: 0x040020FE RID: 8446
		private IOrderedDictionary _values;
	}
}
