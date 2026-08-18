using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000114 RID: 276
	internal sealed class SqlDataSourceQuery
	{
		// Token: 0x06000A18 RID: 2584 RVA: 0x0003F86C File Offset: 0x0003DA6C
		public SqlDataSourceQuery(string command, SqlDataSourceCommandType commandType, ICollection parameters)
		{
			this._command = command;
			this._commandType = commandType;
			this._parameters = parameters;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0003F889 File Offset: 0x0003DA89
		public string Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0003F891 File Offset: 0x0003DA91
		public SqlDataSourceCommandType CommandType
		{
			get
			{
				return this._commandType;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0003F899 File Offset: 0x0003DA99
		public ICollection Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x04000604 RID: 1540
		private string _command;

		// Token: 0x04000605 RID: 1541
		private SqlDataSourceCommandType _commandType;

		// Token: 0x04000606 RID: 1542
		private ICollection _parameters;
	}
}
