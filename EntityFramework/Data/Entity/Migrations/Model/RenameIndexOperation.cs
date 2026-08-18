using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020001AA RID: 426
	public class RenameIndexOperation : MigrationOperation
	{
		// Token: 0x06000E5D RID: 3677 RVA: 0x0003F080 File Offset: 0x0003D280
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public RenameIndexOperation(string table, string name, string newName, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this._table = table;
			this._name = name;
			this._newName = newName;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0003F0CE File Offset: 0x0003D2CE
		public virtual string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0003F0D6 File Offset: 0x0003D2D6
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0003F0DE File Offset: 0x0003D2DE
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x0003F0E6 File Offset: 0x0003D2E6
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

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0003F0EF File Offset: 0x0003D2EF
		public override MigrationOperation Inverse
		{
			get
			{
				return new RenameIndexOperation(this.Table, this.NewName, this.Name, null);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0003F109 File Offset: 0x0003D309
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040003DD RID: 989
		private readonly string _table;

		// Token: 0x040003DE RID: 990
		private readonly string _name;

		// Token: 0x040003DF RID: 991
		private string _newName;
	}
}
