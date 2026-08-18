using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Mappers
{
	// Token: 0x02000825 RID: 2085
	internal sealed class PropertyFilter
	{
		// Token: 0x06005D97 RID: 23959 RVA: 0x001943D1 File Offset: 0x001925D1
		public PropertyFilter(DbModelBuilderVersion modelBuilderVersion = DbModelBuilderVersion.Latest)
		{
			this._modelBuilderVersion = modelBuilderVersion;
		}

		// Token: 0x06005D98 RID: 23960 RVA: 0x00194654 File Offset: 0x00192854
		public IEnumerable<PropertyInfo> GetProperties(Type type, bool declaredOnly, IEnumerable<PropertyInfo> explicitlyMappedProperties = null, IEnumerable<Type> knownTypes = null, bool includePrivate = false)
		{
			explicitlyMappedProperties = (explicitlyMappedProperties ?? Enumerable.Empty<PropertyInfo>());
			knownTypes = (knownTypes ?? Enumerable.Empty<Type>());
			this.ValidatePropertiesForModelVersion(type, explicitlyMappedProperties);
			return from p in declaredOnly ? type.GetDeclaredProperties() : type.GetNonHiddenProperties()
			where !p.IsStatic() && p.IsValidStructuralProperty()
			let m = p.Getter()
			where (includePrivate || m.IsPublic || explicitlyMappedProperties.Contains(p) || knownTypes.Contains(p.PropertyType)) && (!declaredOnly || type.BaseType().GetInstanceProperties().All((PropertyInfo bp) => bp.Name != p.Name)) && (this.EdmV3FeaturesSupported || (!PropertyFilter.IsEnumType(p.PropertyType) && !PropertyFilter.IsSpatialType(p.PropertyType))) && (this.Ef6FeaturesSupported || !p.PropertyType.IsNested)
			select p;
		}

		// Token: 0x06005D99 RID: 23961 RVA: 0x00194784 File Offset: 0x00192984
		public void ValidatePropertiesForModelVersion(Type type, IEnumerable<PropertyInfo> explicitlyMappedProperties)
		{
			if (this._modelBuilderVersion == DbModelBuilderVersion.Latest)
			{
				return;
			}
			if (!this.EdmV3FeaturesSupported)
			{
				PropertyInfo propertyInfo = explicitlyMappedProperties.FirstOrDefault((PropertyInfo p) => PropertyFilter.IsEnumType(p.PropertyType) || PropertyFilter.IsSpatialType(p.PropertyType));
				if (propertyInfo != null)
				{
					throw Error.UnsupportedUseOfV3Type(type.Name, propertyInfo.Name);
				}
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06005D9A RID: 23962 RVA: 0x001947E1 File Offset: 0x001929E1
		public bool EdmV3FeaturesSupported
		{
			get
			{
				return this._modelBuilderVersion.GetEdmVersion() >= 3.0;
			}
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06005D9B RID: 23963 RVA: 0x001947FC File Offset: 0x001929FC
		public bool Ef6FeaturesSupported
		{
			get
			{
				return this._modelBuilderVersion == DbModelBuilderVersion.Latest || this._modelBuilderVersion >= DbModelBuilderVersion.V6_0;
			}
		}

		// Token: 0x06005D9C RID: 23964 RVA: 0x00194814 File Offset: 0x00192A14
		private static bool IsEnumType(Type type)
		{
			type.TryUnwrapNullableType(out type);
			return type.IsEnum();
		}

		// Token: 0x06005D9D RID: 23965 RVA: 0x00194825 File Offset: 0x00192A25
		private static bool IsSpatialType(Type type)
		{
			type.TryUnwrapNullableType(out type);
			return type == typeof(DbGeometry) || type == typeof(DbGeography);
		}

		// Token: 0x040024FC RID: 9468
		private readonly DbModelBuilderVersion _modelBuilderVersion;
	}
}
