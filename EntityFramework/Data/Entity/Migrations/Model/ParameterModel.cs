using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020001B1 RID: 433
	public class ParameterModel : PropertyModel
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x0003F713 File Offset: 0x0003D913
		public ParameterModel(PrimitiveTypeKind type) : this(type, null)
		{
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003F71D File Offset: 0x0003D91D
		public ParameterModel(PrimitiveTypeKind type, TypeUsage typeUsage) : base(type, typeUsage)
		{
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0003F727 File Offset: 0x0003D927
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x0003F72F File Offset: 0x0003D92F
		public bool IsOutParameter { get; set; }
	}
}
