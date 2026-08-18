using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004EC RID: 1260
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	internal sealed class MetadataPropertyAttribute : Attribute
	{
		// Token: 0x06002EFA RID: 12026 RVA: 0x000E084A File Offset: 0x000DEA4A
		internal MetadataPropertyAttribute(BuiltInTypeKind builtInTypeKind, bool isCollectionType) : this(MetadataItem.GetBuiltInType(builtInTypeKind), isCollectionType)
		{
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000E0859 File Offset: 0x000DEA59
		internal MetadataPropertyAttribute(PrimitiveTypeKind primitiveTypeKind, bool isCollectionType) : this(MetadataItem.EdmProviderManifest.GetPrimitiveType(primitiveTypeKind), isCollectionType)
		{
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x000E086D File Offset: 0x000DEA6D
		internal MetadataPropertyAttribute(Type type, bool isCollection) : this(ClrComplexType.CreateReadonlyClrComplexType(type, type.NestingNamespace() ?? string.Empty, type.Name), isCollection)
		{
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x000E0891 File Offset: 0x000DEA91
		private MetadataPropertyAttribute(EdmType type, bool isCollectionType)
		{
			this._type = type;
			this._isCollectionType = isCollectionType;
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002EFE RID: 12030 RVA: 0x000E08A7 File Offset: 0x000DEAA7
		internal EdmType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002EFF RID: 12031 RVA: 0x000E08AF File Offset: 0x000DEAAF
		internal bool IsCollectionType
		{
			get
			{
				return this._isCollectionType;
			}
		}

		// Token: 0x040011D3 RID: 4563
		private readonly EdmType _type;

		// Token: 0x040011D4 RID: 4564
		private readonly bool _isCollectionType;
	}
}
