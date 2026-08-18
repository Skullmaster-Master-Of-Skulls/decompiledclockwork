using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000B1 RID: 177
	public class ListViewInsertedEventArgs : EventArgs
	{
		// Token: 0x060008C3 RID: 2243 RVA: 0x00022366 File Offset: 0x00020566
		public ListViewInsertedEventArgs(int affectedRows, Exception exception)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = exception;
			this._keepInInsertMode = false;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0002238A File Offset: 0x0002058A
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00022392 File Offset: 0x00020592
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0002239A File Offset: 0x0002059A
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x000223A2 File Offset: 0x000205A2
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

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x000223AB File Offset: 0x000205AB
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x000223B3 File Offset: 0x000205B3
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

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x000223BC File Offset: 0x000205BC
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

		// Token: 0x060008CB RID: 2251 RVA: 0x000223D7 File Offset: 0x000205D7
		internal void SetValues(IOrderedDictionary values)
		{
			this._values = values;
		}

		// Token: 0x040002E4 RID: 740
		private int _affectedRows;

		// Token: 0x040002E5 RID: 741
		private Exception _exception;

		// Token: 0x040002E6 RID: 742
		private bool _exceptionHandled;

		// Token: 0x040002E7 RID: 743
		private IOrderedDictionary _values;

		// Token: 0x040002E8 RID: 744
		private bool _keepInInsertMode;
	}
}
