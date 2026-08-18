using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Spatial;
using System.Linq;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001EA RID: 490
	public sealed class PrimitiveType : SimpleType
	{
		// Token: 0x060020BF RID: 8383 RVA: 0x00072696 File Offset: 0x00070896
		internal PrimitiveType()
		{
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x0007269E File Offset: 0x0007089E
		internal PrimitiveType(string name, string namespaceName, DataSpace dataSpace, PrimitiveType baseType, DbProviderManifest providerManifest) : base(name, namespaceName, dataSpace)
		{
			EntityUtil.GenericCheckArgumentNull<PrimitiveType>(baseType, "baseType");
			EntityUtil.GenericCheckArgumentNull<DbProviderManifest>(providerManifest, "providerManifest");
			base.BaseType = baseType;
			PrimitiveType.Initialize(this, baseType.PrimitiveTypeKind, false, providerManifest);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000726DB File Offset: 0x000708DB
		internal PrimitiveType(Type clrType, PrimitiveType baseType, DbProviderManifest providerManifest) : this(EntityUtil.GenericCheckArgumentNull<Type>(clrType, "clrType").Name, clrType.Namespace, DataSpace.OSpace, baseType, providerManifest)
		{
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x000726FC File Offset: 0x000708FC
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.PrimitiveType;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x00072700 File Offset: 0x00070900
		internal override Type ClrType
		{
			get
			{
				return this.ClrEquivalentType;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x00072708 File Offset: 0x00070908
		// (set) Token: 0x060020C5 RID: 8389 RVA: 0x00072710 File Offset: 0x00070910
		[MetadataProperty(BuiltInTypeKind.PrimitiveTypeKind, false)]
		public PrimitiveTypeKind PrimitiveTypeKind
		{
			get
			{
				return this._primitiveTypeKind;
			}
			internal set
			{
				this._primitiveTypeKind = value;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x00072719 File Offset: 0x00070919
		// (set) Token: 0x060020C7 RID: 8391 RVA: 0x00072721 File Offset: 0x00070921
		internal DbProviderManifest ProviderManifest
		{
			get
			{
				return this._providerManifest;
			}
			set
			{
				this._providerManifest = value;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x0007272A File Offset: 0x0007092A
		public ReadOnlyCollection<FacetDescription> FacetDescriptions
		{
			get
			{
				return this.ProviderManifest.GetFacetDescriptions(this);
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x00072738 File Offset: 0x00070938
		public Type ClrEquivalentType
		{
			get
			{
				switch (this.PrimitiveTypeKind)
				{
				case PrimitiveTypeKind.Binary:
					return typeof(byte[]);
				case PrimitiveTypeKind.Boolean:
					return typeof(bool);
				case PrimitiveTypeKind.Byte:
					return typeof(byte);
				case PrimitiveTypeKind.DateTime:
					return typeof(DateTime);
				case PrimitiveTypeKind.Decimal:
					return typeof(decimal);
				case PrimitiveTypeKind.Double:
					return typeof(double);
				case PrimitiveTypeKind.Guid:
					return typeof(Guid);
				case PrimitiveTypeKind.Single:
					return typeof(float);
				case PrimitiveTypeKind.SByte:
					return typeof(sbyte);
				case PrimitiveTypeKind.Int16:
					return typeof(short);
				case PrimitiveTypeKind.Int32:
					return typeof(int);
				case PrimitiveTypeKind.Int64:
					return typeof(long);
				case PrimitiveTypeKind.String:
					return typeof(string);
				case PrimitiveTypeKind.Time:
					return typeof(TimeSpan);
				case PrimitiveTypeKind.DateTimeOffset:
					return typeof(DateTimeOffset);
				case PrimitiveTypeKind.Geometry:
				case PrimitiveTypeKind.GeometryPoint:
				case PrimitiveTypeKind.GeometryLineString:
				case PrimitiveTypeKind.GeometryPolygon:
				case PrimitiveTypeKind.GeometryMultiPoint:
				case PrimitiveTypeKind.GeometryMultiLineString:
				case PrimitiveTypeKind.GeometryMultiPolygon:
				case PrimitiveTypeKind.GeometryCollection:
					return typeof(DbGeometry);
				case PrimitiveTypeKind.Geography:
				case PrimitiveTypeKind.GeographyPoint:
				case PrimitiveTypeKind.GeographyLineString:
				case PrimitiveTypeKind.GeographyPolygon:
				case PrimitiveTypeKind.GeographyMultiPoint:
				case PrimitiveTypeKind.GeographyMultiLineString:
				case PrimitiveTypeKind.GeographyMultiPolygon:
				case PrimitiveTypeKind.GeographyCollection:
					return typeof(DbGeography);
				default:
					return null;
				}
			}
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x0007288F File Offset: 0x00070A8F
		internal override IEnumerable<FacetDescription> GetAssociatedFacetDescriptions()
		{
			return base.GetAssociatedFacetDescriptions().Concat(this.FacetDescriptions);
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000728A2 File Offset: 0x00070AA2
		internal static void Initialize(PrimitiveType primitiveType, PrimitiveTypeKind primitiveTypeKind, bool isDefaultType, DbProviderManifest providerManifest)
		{
			primitiveType._primitiveTypeKind = primitiveTypeKind;
			primitiveType._providerManifest = providerManifest;
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000728B2 File Offset: 0x00070AB2
		public EdmType GetEdmPrimitiveType()
		{
			return MetadataItem.EdmProviderManifest.GetPrimitiveType(this.PrimitiveTypeKind);
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000728C4 File Offset: 0x00070AC4
		public static ReadOnlyCollection<PrimitiveType> GetEdmPrimitiveTypes()
		{
			return MetadataItem.EdmProviderManifest.GetStoreTypes();
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000728D0 File Offset: 0x00070AD0
		public static PrimitiveType GetEdmPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			return MetadataItem.EdmProviderManifest.GetPrimitiveType(primitiveTypeKind);
		}

		// Token: 0x04000E69 RID: 3689
		private PrimitiveTypeKind _primitiveTypeKind;

		// Token: 0x04000E6A RID: 3690
		private DbProviderManifest _providerManifest;
	}
}
