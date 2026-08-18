using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D3 RID: 1235
	public class CollectionType : EdmType
	{
		// Token: 0x06002D86 RID: 11654 RVA: 0x000DC37A File Offset: 0x000DA57A
		internal CollectionType()
		{
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000DC382 File Offset: 0x000DA582
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal CollectionType(EdmType elementType) : this(TypeUsage.Create(elementType))
		{
			this.DataSpace = elementType.DataSpace;
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000DC39C File Offset: 0x000DA59C
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal CollectionType(TypeUsage elementType) : base(CollectionType.GetIdentity(Check.NotNull<TypeUsage>(elementType, "elementType")), "Transient", elementType.EdmType.DataSpace)
		{
			this._typeUsage = elementType;
			this.SetReadOnly();
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x000DC3D1 File Offset: 0x000DA5D1
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.CollectionType;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x000DC3D4 File Offset: 0x000DA5D4
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public virtual TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000DC3DC File Offset: 0x000DA5DC
		private static string GetIdentity(TypeUsage typeUsage)
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			stringBuilder.Append("collection[");
			typeUsage.BuildIdentity(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000DC418 File Offset: 0x000DA618
		internal override bool EdmEquals(MetadataItem item)
		{
			if (object.ReferenceEquals(this, item))
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

		// Token: 0x040010DD RID: 4317
		private readonly TypeUsage _typeUsage;
	}
}
