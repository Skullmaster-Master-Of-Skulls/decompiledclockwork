using System;
using System.Data.Common;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200064F RID: 1615
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SqlDataSourceStatusEventArgs : EventArgs
	{
		// Token: 0x06004F44 RID: 20292 RVA: 0x0013F50B File Offset: 0x0013E50B
		public SqlDataSourceStatusEventArgs(DbCommand command, int affectedRows, Exception exception)
		{
			this._command = command;
			this._affectedRows = affectedRows;
			this._exception = exception;
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x06004F45 RID: 20293 RVA: 0x0013F528 File Offset: 0x0013E528
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x06004F46 RID: 20294 RVA: 0x0013F530 File Offset: 0x0013E530
		public DbCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x06004F47 RID: 20295 RVA: 0x0013F538 File Offset: 0x0013E538
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x06004F48 RID: 20296 RVA: 0x0013F540 File Offset: 0x0013E540
		// (set) Token: 0x06004F49 RID: 20297 RVA: 0x0013F548 File Offset: 0x0013E548
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

		// Token: 0x04002CDD RID: 11485
		private DbCommand _command;

		// Token: 0x04002CDE RID: 11486
		private Exception _exception;

		// Token: 0x04002CDF RID: 11487
		private bool _exceptionHandled;

		// Token: 0x04002CE0 RID: 11488
		private int _affectedRows;
	}
}
