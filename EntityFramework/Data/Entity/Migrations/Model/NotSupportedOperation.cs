using System;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020001AF RID: 431
	public class NotSupportedOperation : MigrationOperation
	{
		// Token: 0x06000E76 RID: 3702 RVA: 0x0003F296 File Offset: 0x0003D496
		private NotSupportedOperation() : base(null)
		{
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0003F29F File Offset: 0x0003D49F
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040003E8 RID: 1000
		internal static readonly NotSupportedOperation Instance = new NotSupportedOperation();
	}
}
