using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020001AE RID: 430
	public class RenameProcedureOperation : MigrationOperation
	{
		// Token: 0x06000E71 RID: 3697 RVA: 0x0003F20A File Offset: 0x0003D40A
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public RenameProcedureOperation(string name, string newName, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this._name = name;
			this._newName = newName;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0003F239 File Offset: 0x0003D439
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0003F241 File Offset: 0x0003D441
		public virtual string NewName
		{
			get
			{
				return this._newName;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0003F24C File Offset: 0x0003D44C
		public override MigrationOperation Inverse
		{
			get
			{
				DatabaseName databaseName = DatabaseName.Parse(this._name);
				string name = DatabaseName.Parse(this._newName).Name;
				return new RenameProcedureOperation(new DatabaseName(name, databaseName.Schema).ToString(), databaseName.Name, null);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0003F293 File Offset: 0x0003D493
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040003E6 RID: 998
		private readonly string _name;

		// Token: 0x040003E7 RID: 999
		private readonly string _newName;
	}
}
