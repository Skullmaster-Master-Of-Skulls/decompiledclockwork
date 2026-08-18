using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000711 RID: 1809
	public class RenameColumnOperation : MigrationOperation
	{
		// Token: 0x06004951 RID: 18769 RVA: 0x0015F390 File Offset: 0x0015D590
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public RenameColumnOperation(string table, string name, string newName, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this._table = table;
			this._name = name;
			this._newName = newName;
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06004952 RID: 18770 RVA: 0x0015F3DE File Offset: 0x0015D5DE
		public virtual string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06004953 RID: 18771 RVA: 0x0015F3E6 File Offset: 0x0015D5E6
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06004954 RID: 18772 RVA: 0x0015F3EE File Offset: 0x0015D5EE
		// (set) Token: 0x06004955 RID: 18773 RVA: 0x0015F3F6 File Offset: 0x0015D5F6
		public virtual string NewName
		{
			get
			{
				return this._newName;
			}
			internal set
			{
				this._newName = value;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x0015F3FF File Offset: 0x0015D5FF
		public override MigrationOperation Inverse
		{
			get
			{
				return new RenameColumnOperation(this.Table, this.NewName, this.Name, null);
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06004957 RID: 18775 RVA: 0x0015F419 File Offset: 0x0015D619
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B40 RID: 6976
		private readonly string _table;

		// Token: 0x04001B41 RID: 6977
		private readonly string _name;

		// Token: 0x04001B42 RID: 6978
		private string _newName;
	}
}
