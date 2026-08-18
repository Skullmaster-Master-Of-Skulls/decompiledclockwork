using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000706 RID: 1798
	public class ColumnModel : PropertyModel
	{
		// Token: 0x060048EF RID: 18671 RVA: 0x0015E7B6 File Offset: 0x0015C9B6
		public ColumnModel(PrimitiveTypeKind type) : this(type, null)
		{
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x0015E7C0 File Offset: 0x0015C9C0
		public ColumnModel(PrimitiveTypeKind type, TypeUsage typeUsage) : base(type, typeUsage)
		{
			this._clrType = PrimitiveType.GetEdmPrimitiveType(type).ClrEquivalentType;
			this._clrDefaultValue = this.CreateDefaultValue();
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x0015E7F4 File Offset: 0x0015C9F4
		private object CreateDefaultValue()
		{
			if (this._clrType.IsValueType())
			{
				return Activator.CreateInstance(this._clrType);
			}
			if (this._clrType == typeof(string))
			{
				return string.Empty;
			}
			if (this._clrType == typeof(DbGeography))
			{
				return DbGeography.FromText("POINT(0 0)");
			}
			if (this._clrType == typeof(DbGeometry))
			{
				return DbGeometry.FromText("POINT(0 0)");
			}
			return new byte[0];
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060048F2 RID: 18674 RVA: 0x0015E881 File Offset: 0x0015CA81
		public virtual Type ClrType
		{
			get
			{
				return this._clrType;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060048F3 RID: 18675 RVA: 0x0015E889 File Offset: 0x0015CA89
		public virtual object ClrDefaultValue
		{
			get
			{
				return this._clrDefaultValue;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060048F4 RID: 18676 RVA: 0x0015E891 File Offset: 0x0015CA91
		// (set) Token: 0x060048F5 RID: 18677 RVA: 0x0015E899 File Offset: 0x0015CA99
		public virtual bool? IsNullable { get; set; }

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060048F6 RID: 18678 RVA: 0x0015E8A2 File Offset: 0x0015CAA2
		// (set) Token: 0x060048F7 RID: 18679 RVA: 0x0015E8AA File Offset: 0x0015CAAA
		public virtual bool IsIdentity { get; set; }

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060048F8 RID: 18680 RVA: 0x0015E8B3 File Offset: 0x0015CAB3
		// (set) Token: 0x060048F9 RID: 18681 RVA: 0x0015E8BB File Offset: 0x0015CABB
		public virtual bool IsTimestamp { get; set; }

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x060048FA RID: 18682 RVA: 0x0015E8C4 File Offset: 0x0015CAC4
		// (set) Token: 0x060048FB RID: 18683 RVA: 0x0015E8CC File Offset: 0x0015CACC
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public IDictionary<string, AnnotationValues> Annotations
		{
			get
			{
				return this._annotations;
			}
			set
			{
				this._annotations = (value ?? new Dictionary<string, AnnotationValues>());
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x060048FC RID: 18684 RVA: 0x0015E8DE File Offset: 0x0015CADE
		// (set) Token: 0x060048FD RID: 18685 RVA: 0x0015E8E6 File Offset: 0x0015CAE6
		internal PropertyInfo ApiPropertyInfo
		{
			get
			{
				return this._apiPropertyInfo;
			}
			set
			{
				this._apiPropertyInfo = value;
			}
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x0015E8F0 File Offset: 0x0015CAF0
		public bool IsNarrowerThan(ColumnModel column, DbProviderManifest providerManifest)
		{
			Check.NotNull<ColumnModel>(column, "column");
			Check.NotNull<DbProviderManifest>(providerManifest, "providerManifest");
			TypeUsage storeType = providerManifest.GetStoreType(base.TypeUsage);
			TypeUsage storeType2 = providerManifest.GetStoreType(column.TypeUsage);
			return ColumnModel._typeSize[this.Type] < ColumnModel._typeSize[column.Type] || (!(this.IsUnicode ?? true) && (column.IsUnicode ?? true)) || (!(this.IsNullable ?? true) && (column.IsNullable ?? true)) || ColumnModel.IsNarrowerThan(storeType, storeType2);
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x0015E9CC File Offset: 0x0015CBCC
		private static bool IsNarrowerThan(TypeUsage typeUsage, TypeUsage other)
		{
			foreach (string identity in new string[]
			{
				"MaxLength",
				"Precision",
				"Scale"
			})
			{
				Facet facet;
				Facet facet2;
				if (typeUsage.Facets.TryGetValue(identity, true, out facet) && other.Facets.TryGetValue(facet.Name, true, out facet2) && facet.Value != facet2.Value)
				{
					int num = Convert.ToInt32(facet.Value, CultureInfo.InvariantCulture);
					int num2 = Convert.ToInt32(facet2.Value, CultureInfo.InvariantCulture);
					if (num < num2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x0015EA80 File Offset: 0x0015CC80
		internal override FacetValues ToFacetValues()
		{
			FacetValues facetValues = base.ToFacetValues();
			if (this.IsNullable != null)
			{
				facetValues.Nullable = new bool?(this.IsNullable.Value);
			}
			if (this.IsIdentity)
			{
				facetValues.StoreGeneratedPattern = new StoreGeneratedPattern?(StoreGeneratedPattern.Identity);
			}
			return facetValues;
		}

		// Token: 0x04001B18 RID: 6936
		private readonly Type _clrType;

		// Token: 0x04001B19 RID: 6937
		private readonly object _clrDefaultValue;

		// Token: 0x04001B1A RID: 6938
		private PropertyInfo _apiPropertyInfo;

		// Token: 0x04001B1B RID: 6939
		private IDictionary<string, AnnotationValues> _annotations = new Dictionary<string, AnnotationValues>();

		// Token: 0x04001B1C RID: 6940
		private static readonly Dictionary<PrimitiveTypeKind, int> _typeSize = new Dictionary<PrimitiveTypeKind, int>
		{
			{
				PrimitiveTypeKind.Binary,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.Boolean,
				1
			},
			{
				PrimitiveTypeKind.Byte,
				1
			},
			{
				PrimitiveTypeKind.DateTime,
				8
			},
			{
				PrimitiveTypeKind.DateTimeOffset,
				10
			},
			{
				PrimitiveTypeKind.Decimal,
				17
			},
			{
				PrimitiveTypeKind.Double,
				53
			},
			{
				PrimitiveTypeKind.Guid,
				16
			},
			{
				PrimitiveTypeKind.Int16,
				2
			},
			{
				PrimitiveTypeKind.Int32,
				4
			},
			{
				PrimitiveTypeKind.Int64,
				8
			},
			{
				PrimitiveTypeKind.SByte,
				1
			},
			{
				PrimitiveTypeKind.Single,
				4
			},
			{
				PrimitiveTypeKind.String,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.Time,
				5
			},
			{
				PrimitiveTypeKind.Geometry,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.Geography,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryPoint,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryLineString,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryPolygon,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryMultiPoint,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryMultiLineString,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryMultiPolygon,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryCollection,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyPoint,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyLineString,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyPolygon,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyMultiPoint,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyMultiLineString,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyMultiPolygon,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyCollection,
				int.MaxValue
			}
		};
	}
}
