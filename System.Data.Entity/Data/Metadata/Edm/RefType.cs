using System;
using System.Text;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001F1 RID: 497
	public sealed class RefType : EdmType
	{
		// Token: 0x0600210F RID: 8463 RVA: 0x0007475D File Offset: 0x0007295D
		internal RefType(EntityType entityType) : base(RefType.GetIdentity(EntityUtil.GenericCheckArgumentNull<EntityType>(entityType, "entityType")), "Transient", entityType.DataSpace)
		{
			this._elementType = entityType;
			this.SetReadOnly();
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x0007478D File Offset: 0x0007298D
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.RefType;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x00074791 File Offset: 0x00072991
		[MetadataProperty(BuiltInTypeKind.EntityTypeBase, false)]
		public EntityTypeBase ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x0007479C File Offset: 0x0007299C
		private static string GetIdentity(EntityTypeBase entityTypeBase)
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			stringBuilder.Append("reference[");
			entityTypeBase.BuildIdentity(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000EA1 RID: 3745
		private readonly EntityTypeBase _elementType;
	}
}
