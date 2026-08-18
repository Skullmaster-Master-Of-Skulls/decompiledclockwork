using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000713 RID: 1811
	public class SqlOperation : MigrationOperation
	{
		// Token: 0x0600495E RID: 18782 RVA: 0x0015F4AE File Offset: 0x0015D6AE
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public SqlOperation(string sql, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(sql, "sql");
			this._sql = sql;
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600495F RID: 18783 RVA: 0x0015F4CA File Offset: 0x0015D6CA
		public virtual string Sql
		{
			get
			{
				return this._sql;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06004960 RID: 18784 RVA: 0x0015F4D2 File Offset: 0x0015D6D2
		// (set) Token: 0x06004961 RID: 18785 RVA: 0x0015F4DA File Offset: 0x0015D6DA
		public virtual bool SuppressTransaction { get; set; }

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06004962 RID: 18786 RVA: 0x0015F4E3 File Offset: 0x0015D6E3
		public override bool IsDestructiveChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001B45 RID: 6981
		private readonly string _sql;
	}
}
