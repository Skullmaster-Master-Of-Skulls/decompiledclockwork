using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001E4 RID: 484
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	internal sealed class MetadataPropertyAttribute : Attribute
	{
		// Token: 0x060020A8 RID: 8360 RVA: 0x0007242D File Offset: 0x0007062D
		internal MetadataPropertyAttribute(BuiltInTypeKind builtInTypeKind, bool isCollectionType) : this(MetadataItem.GetBuiltInType(builtInTypeKind), isCollectionType)
		{
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0007243C File Offset: 0x0007063C
		internal MetadataPropertyAttribute(PrimitiveTypeKind primitiveTypeKind, bool isCollectionType) : this(MetadataItem.EdmProviderManifest.GetPrimitiveType(primitiveTypeKind), isCollectionType)
		{
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x00072450 File Offset: 0x00070650
		internal MetadataPropertyAttribute(Type type, bool isCollection) : this(ClrComplexType.CreateReadonlyClrComplexType(type, type.Namespace ?? string.Empty, type.Name), isCollection)
		{
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x00072474 File Offset: 0x00070674
		private MetadataPropertyAttribute(EdmType type, bool isCollectionType)
		{
			this._type = type;
			this._isCollectionType = isCollectionType;
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x0007248A File Offset: 0x0007068A
		internal EdmType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x00072492 File Offset: 0x00070692
		internal bool IsCollectionType
		{
			get
			{
				return this._isCollectionType;
			}
		}

		// Token: 0x04000E54 RID: 3668
		private readonly EdmType _type;

		// Token: 0x04000E55 RID: 3669
		private readonly bool _isCollectionType;
	}
}
