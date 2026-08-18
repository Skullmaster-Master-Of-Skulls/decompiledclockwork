using System;
using System.Data.Common;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004D9 RID: 1241
	public class SqlDataSourceStatusEventArgs : EventArgs
	{
		// Token: 0x06003D9D RID: 15773 RVA: 0x000C6572 File Offset: 0x000C4772
		public SqlDataSourceStatusEventArgs(DbCommand command, int affectedRows, Exception exception)
		{
			this._command = command;
			this._affectedRows = affectedRows;
			this._exception = exception;
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06003D9E RID: 15774 RVA: 0x000C658F File Offset: 0x000C478F
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06003D9F RID: 15775 RVA: 0x000C6597 File Offset: 0x000C4797
		public DbCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06003DA0 RID: 15776 RVA: 0x000C659F File Offset: 0x000C479F
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06003DA1 RID: 15777 RVA: 0x000C65A7 File Offset: 0x000C47A7
		// (set) Token: 0x06003DA2 RID: 15778 RVA: 0x000C65AF File Offset: 0x000C47AF
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

		// Token: 0x040023CA RID: 9162
		private DbCommand _command;

		// Token: 0x040023CB RID: 9163
		private Exception _exception;

		// Token: 0x040023CC RID: 9164
		private bool _exceptionHandled;

		// Token: 0x040023CD RID: 9165
		private int _affectedRows;
	}
}
