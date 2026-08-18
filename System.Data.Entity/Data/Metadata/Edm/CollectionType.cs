using System;
using System.Text;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001C7 RID: 455
	public sealed class CollectionType : EdmType
	{
		// Token: 0x06001F4C RID: 8012 RVA: 0x0006E26E File Offset: 0x0006C46E
		internal CollectionType(EdmType elementType) : this(TypeUsage.Create(elementType))
		{
			base.DataSpace = elementType.DataSpace;
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x0006E288 File Offset: 0x0006C488
		internal CollectionType(TypeUsage elementType) : base(CollectionType.GetIdentity(EntityUtil.GenericCheckArgumentNull<TypeUsage>(elementType, "elementType")), "Transient", elementType.EdmType.DataSpace)
		{
			this._typeUsage = elementType;
			this.SetReadOnly();
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x0006E2BD File Offset: 0x0006C4BD
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.CollectionType;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0006E2C0 File Offset: 0x0006C4C0
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0006E2C8 File Offset: 0x0006C4C8
		private static string GetIdentity(TypeUsage typeUsage)
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			stringBuilder.Append("collection[");
			typeUsage.BuildIdentity(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0006E304 File Offset: 0x0006C504
		internal override bool EdmEquals(MetadataItem item)
		{
			if (this == item)
			{
				return true;
			}
			if (item == null || BuiltInTypeKind.CollectionType != item.BuiltInTypeKind)
			{
				return false;
			}
			CollectionType collectionType = (CollectionType)item;
			return this.TypeUsage.EdmEquals(collectionType.TypeUsage);
		}

		// Token: 0x04000D49 RID: 3401
		private readonly TypeUsage _typeUsage;
	}
}
