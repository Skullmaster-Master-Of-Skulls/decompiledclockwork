using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020001AD RID: 429
	public class MoveProcedureOperation : MigrationOperation
	{
		// Token: 0x06000E6C RID: 3692 RVA: 0x0003F198 File Offset: 0x0003D398
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public MoveProcedureOperation(string name, string newSchema, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._newSchema = newSchema;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0003F1BB File Offset: 0x0003D3BB
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0003F1C3 File Offset: 0x0003D3C3
		public virtual string NewSchema
		{
			get
			{
				return this._newSchema;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0003F1CC File Offset: 0x0003D3CC
		public override MigrationOperation Inverse
		{
			get
			{
				DatabaseName databaseName = DatabaseName.Parse(this._name);
				return new MoveProcedureOperation(new DatabaseName(databaseName.Name, this.NewSchema).ToString(), databaseName.Schema, null);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0003F207 File Offset: 0x0003D407
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040003E4 RID: 996
		private readonly string _name;

		// Token: 0x040003E5 RID: 997
		private readonly string _newSchema;
	}
}
