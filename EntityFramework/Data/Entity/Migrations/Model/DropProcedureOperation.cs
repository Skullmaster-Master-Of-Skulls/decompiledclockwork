using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020001B2 RID: 434
	public class DropProcedureOperation : MigrationOperation
	{
		// Token: 0x06000E94 RID: 3732 RVA: 0x0003F738 File Offset: 0x0003D938
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public DropProcedureOperation(string name, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0003F754 File Offset: 0x0003D954
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0003F75C File Offset: 0x0003D95C
		public override MigrationOperation Inverse
		{
			get
			{
				return NotSupportedOperation.Instance;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0003F763 File Offset: 0x0003D963
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040003F5 RID: 1013
		private readonly string _name;
	}
}
