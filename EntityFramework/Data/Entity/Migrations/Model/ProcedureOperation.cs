using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020001A6 RID: 422
	public abstract class ProcedureOperation : MigrationOperation
	{
		// Token: 0x06000E4B RID: 3659 RVA: 0x0003EEF1 File Offset: 0x0003D0F1
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected ProcedureOperation(string name, string bodySql, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._bodySql = bodySql;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0003EF1F File Offset: 0x0003D11F
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0003EF27 File Offset: 0x0003D127
		public string BodySql
		{
			get
			{
				return this._bodySql;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x0003EF2F File Offset: 0x0003D12F
		public virtual IList<ParameterModel> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0003EF37 File Offset: 0x0003D137
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040003D4 RID: 980
		private readonly string _name;

		// Token: 0x040003D5 RID: 981
		private readonly string _bodySql;

		// Token: 0x040003D6 RID: 982
		private readonly List<ParameterModel> _parameters = new List<ParameterModel>();
	}
}
