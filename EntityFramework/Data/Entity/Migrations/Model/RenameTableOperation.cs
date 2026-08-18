using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000712 RID: 1810
	public class RenameTableOperation : MigrationOperation
	{
		// Token: 0x06004958 RID: 18776 RVA: 0x0015F41C File Offset: 0x0015D61C
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public RenameTableOperation(string name, string newName, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this._name = name;
			this._newName = newName;
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06004959 RID: 18777 RVA: 0x0015F44B File Offset: 0x0015D64B
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x0600495A RID: 18778 RVA: 0x0015F453 File Offset: 0x0015D653
		// (set) Token: 0x0600495B RID: 18779 RVA: 0x0015F45B File Offset: 0x0015D65B
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

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x0015F464 File Offset: 0x0015D664
		public override MigrationOperation Inverse
		{
			get
			{
				DatabaseName databaseName = DatabaseName.Parse(this._name);
				string name = DatabaseName.Parse(this._newName).Name;
				return new RenameTableOperation(new DatabaseName(name, databaseName.Schema).ToString(), databaseName.Name, null);
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x0600495D RID: 18781 RVA: 0x0015F4AB File Offset: 0x0015D6AB
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B43 RID: 6979
		private readonly string _name;

		// Token: 0x04001B44 RID: 6980
		private string _newName;
	}
}
