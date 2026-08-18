using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F5 RID: 1269
	public class PrimitiveType : SimpleType
	{
		// Token: 0x06002F28 RID: 12072 RVA: 0x000E0EB0 File Offset: 0x000DF0B0
		internal PrimitiveType()
		{
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000E0EB8 File Offset: 0x000DF0B8
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal PrimitiveType(string name, string namespaceName, DataSpace dataSpace, PrimitiveType baseType, DbProviderManifest providerManifest) : base(name, namespaceName, dataSpace)
		{
			Check.NotNull<PrimitiveType>(baseType, "baseType");
			Check.NotNull<DbProviderManifest>(providerManifest, "providerManifest");
			this.BaseType = baseType;
			PrimitiveType.Initialize(this, baseType.PrimitiveTypeKind, providerManifest);
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000E0EF4 File Offset: 0x000DF0F4
		internal PrimitiveType(Type clrType, PrimitiveType baseType, DbProviderManifest providerManifest) : this(Check.NotNull<Type>(clrType, "clrType").Name, clrType.NestingNamespace(), DataSpace.OSpace, baseType, providerManifest)
		{
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x000E0F15 File Offset: 0x000DF115
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.PrimitiveType;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x000E0F19 File Offset: 0x000DF119
		internal override Type ClrType
		{
			get
			{
				return this.ClrEquivalentType;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x000E0F21 File Offset: 0x000DF121
		// (set) Token: 0x06002F2E RID: 12078 RVA: 0x000E0F29 File Offset: 0x000DF129
		[MetadataProperty(BuiltInTypeKind.PrimitiveTypeKind, false)]
		public virtual PrimitiveTypeKind PrimitiveTypeKind
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

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x000E0F32 File Offset: 0x000DF132
		// (set) Token: 0x06002F30 RID: 12080 RVA: 0x000E0F3A File Offset: 0x000DF13A
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

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x000E0F43 File Offset: 0x000DF143
		public virtual ReadOnlyCollection<FacetDescription> FacetDescriptions
		{
			get
			{
				return this.ProviderManifest.GetFacetDescriptions(this);
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002F32 RID: 12082 RVA: 0x000E0F54 File Offset: 0x000DF154
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

		// Token: 0x06002F33 RID: 12083 RVA: 0x000E10AB File Offset: 0x000DF2AB
		internal override IEnumerable<FacetDescription> GetAssociatedFacetDescriptions()
		{
			return base.GetAssociatedFacetDescriptions().Concat(this.FacetDescriptions);
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x000E10BE File Offset: 0x000DF2BE
		internal static void Initialize(PrimitiveType primitiveType, PrimitiveTypeKind primitiveTypeKind, DbProviderManifest providerManifest)
		{
			primitiveType._primitiveTypeKind = primitiveTypeKind;
			primitiveType._providerManifest = providerManifest;
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000E10CE File Offset: 0x000DF2CE
		public EdmType GetEdmPrimitiveType()
		{
			return MetadataItem.EdmProviderManifest.GetPrimitiveType(this.PrimitiveTypeKind);
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x000E10E0 File Offset: 0x000DF2E0
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public static ReadOnlyCollection<PrimitiveType> GetEdmPrimitiveTypes()
		{
			return MetadataItem.EdmProviderManifest.GetStoreTypes();
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x000E10EC File Offset: 0x000DF2EC
		public static PrimitiveType GetEdmPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			return MetadataItem.EdmProviderManifest.GetPrimitiveType(primitiveTypeKind);
		}

		// Token: 0x040011F0 RID: 4592
		private PrimitiveTypeKind _primitiveTypeKind;

		// Token: 0x040011F1 RID: 4593
		private DbProviderManifest _providerManifest;
	}
}
