using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Types
{
	// Token: 0x020007DE RID: 2014
	internal abstract class StructuralTypeConfiguration : ConfigurationBase
	{
		// Token: 0x06005B97 RID: 23447 RVA: 0x00189E04 File Offset: 0x00188004
		internal static Type GetPropertyConfigurationType(Type propertyType)
		{
			propertyType.TryUnwrapNullableType(out propertyType);
			if (propertyType == typeof(string))
			{
				return typeof(StringPropertyConfiguration);
			}
			if (propertyType == typeof(decimal))
			{
				return typeof(DecimalPropertyConfiguration);
			}
			if (propertyType == typeof(DateTime) || propertyType == typeof(TimeSpan) || propertyType == typeof(DateTimeOffset))
			{
				return typeof(DateTimePropertyConfiguration);
			}
			if (propertyType == typeof(byte[]))
			{
				return typeof(BinaryPropertyConfiguration);
			}
			if (!propertyType.IsValueType() && !(propertyType == typeof(DbGeography)) && !(propertyType == typeof(DbGeometry)))
			{
				return typeof(NavigationPropertyConfiguration);
			}
			return typeof(PrimitivePropertyConfiguration);
		}

		// Token: 0x06005B98 RID: 23448 RVA: 0x00189EF3 File Offset: 0x001880F3
		internal StructuralTypeConfiguration()
		{
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x00189F11 File Offset: 0x00188111
		internal StructuralTypeConfiguration(Type clrType)
		{
			this._clrType = clrType;
		}

		// Token: 0x06005B9A RID: 23450 RVA: 0x00189F58 File Offset: 0x00188158
		internal StructuralTypeConfiguration(StructuralTypeConfiguration source)
		{
			source._primitivePropertyConfigurations.Each(delegate(KeyValuePair<PropertyPath, PrimitivePropertyConfiguration> c)
			{
				this._primitivePropertyConfigurations.Add(c.Key, c.Value.Clone());
			});
			this._ignoredProperties.AddRange(source._ignoredProperties);
			this._clrType = source._clrType;
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06005B9B RID: 23451 RVA: 0x00189FC4 File Offset: 0x001881C4
		internal virtual IEnumerable<PropertyInfo> ConfiguredProperties
		{
			get
			{
				return from p in this._primitivePropertyConfigurations.Keys
				select p.Last<PropertyInfo>();
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06005B9C RID: 23452 RVA: 0x00189FF3 File Offset: 0x001881F3
		internal IEnumerable<PropertyInfo> IgnoredProperties
		{
			get
			{
				return this._ignoredProperties;
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06005B9D RID: 23453 RVA: 0x00189FFB File Offset: 0x001881FB
		internal Type ClrType
		{
			get
			{
				return this._clrType;
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06005B9E RID: 23454 RVA: 0x0018A003 File Offset: 0x00188203
		internal IEnumerable<KeyValuePair<PropertyPath, PrimitivePropertyConfiguration>> PrimitivePropertyConfigurations
		{
			get
			{
				return this._primitivePropertyConfigurations;
			}
		}

		// Token: 0x06005B9F RID: 23455 RVA: 0x0018A00B File Offset: 0x0018820B
		public void Ignore(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			this._ignoredProperties.Add(propertyInfo);
		}

		// Token: 0x06005BA0 RID: 23456 RVA: 0x0018A088 File Offset: 0x00188288
		internal PrimitivePropertyConfiguration Property(PropertyPath propertyPath, OverridableConfigurationParts? overridableConfigurationParts = null)
		{
			return this.Property<PrimitivePropertyConfiguration>(propertyPath, delegate()
			{
				PrimitivePropertyConfiguration primitivePropertyConfiguration = (PrimitivePropertyConfiguration)Activator.CreateInstance(StructuralTypeConfiguration.GetPropertyConfigurationType(propertyPath.Last<PropertyInfo>().PropertyType));
				primitivePropertyConfiguration.TypeConfiguration = this;
				if (overridableConfigurationParts != null)
				{
					primitivePropertyConfiguration.OverridableConfigurationParts = overridableConfigurationParts.Value;
				}
				return primitivePropertyConfiguration;
			});
		}

		// Token: 0x06005BA1 RID: 23457 RVA: 0x0018A0C8 File Offset: 0x001882C8
		internal virtual void RemoveProperty(PropertyPath propertyPath)
		{
			this._primitivePropertyConfigurations.Remove(propertyPath);
		}

		// Token: 0x06005BA2 RID: 23458 RVA: 0x0018A0D8 File Offset: 0x001882D8
		internal TPrimitivePropertyConfiguration Property<TPrimitivePropertyConfiguration>(PropertyPath propertyPath, Func<TPrimitivePropertyConfiguration> primitivePropertyConfigurationCreator) where TPrimitivePropertyConfiguration : PrimitivePropertyConfiguration
		{
			PrimitivePropertyConfiguration primitivePropertyConfiguration;
			if (!this._primitivePropertyConfigurations.TryGetValue(propertyPath, out primitivePropertyConfiguration))
			{
				this._primitivePropertyConfigurations.Add(propertyPath, primitivePropertyConfiguration = primitivePropertyConfigurationCreator());
			}
			return (TPrimitivePropertyConfiguration)((object)primitivePropertyConfiguration);
		}

		// Token: 0x06005BA3 RID: 23459 RVA: 0x0018A190 File Offset: 0x00188390
		internal void ConfigurePropertyMappings(IList<Tuple<ColumnMappingBuilder, EntityType>> propertyMappings, DbProviderManifest providerManifest, bool allowOverride = false)
		{
			foreach (KeyValuePair<PropertyPath, PrimitivePropertyConfiguration> keyValuePair in this.PrimitivePropertyConfigurations)
			{
				PropertyPath propertyPath = keyValuePair.Key;
				PrimitivePropertyConfiguration value = keyValuePair.Value;
				value.Configure(from pm in propertyMappings
				where propertyPath.Equals(new PropertyPath(from p in pm.Item1.PropertyPath.Skip(pm.Item1.PropertyPath.Count - propertyPath.Count)
				select p.GetClrPropertyInfo()))
				select pm, providerManifest, allowOverride, false);
			}
		}

		// Token: 0x06005BA4 RID: 23460 RVA: 0x0018A2A0 File Offset: 0x001884A0
		internal void ConfigureFunctionParameters(IList<ModificationFunctionParameterBinding> parameterBindings)
		{
			foreach (KeyValuePair<PropertyPath, PrimitivePropertyConfiguration> keyValuePair in this.PrimitivePropertyConfigurations)
			{
				PropertyPath propertyPath = keyValuePair.Key;
				PrimitivePropertyConfiguration value = keyValuePair.Value;
				IEnumerable<FunctionParameter> parameters = from pb in parameterBindings.Where(delegate(ModificationFunctionParameterBinding pb)
				{
					if (pb.MemberPath.AssociationSetEnd == null)
					{
						return propertyPath.Equals(new PropertyPath(from m in pb.MemberPath.Members.Skip(pb.MemberPath.Members.Count - propertyPath.Count)
						select m.GetClrPropertyInfo()));
					}
					return false;
				})
				select pb.Parameter;
				value.ConfigureFunctionParameters(parameters);
			}
		}

		// Token: 0x06005BA5 RID: 23461 RVA: 0x0018A344 File Offset: 0x00188544
		internal void Configure(string structuralTypeName, IEnumerable<EdmProperty> properties, ICollection<MetadataProperty> dataModelAnnotations)
		{
			dataModelAnnotations.SetConfiguration(this);
			foreach (KeyValuePair<PropertyPath, PrimitivePropertyConfiguration> keyValuePair in this._primitivePropertyConfigurations)
			{
				PropertyPath key = keyValuePair.Key;
				PrimitivePropertyConfiguration value = keyValuePair.Value;
				StructuralTypeConfiguration.Configure(structuralTypeName, properties, key, value);
			}
		}

		// Token: 0x06005BA6 RID: 23462 RVA: 0x0018A3D0 File Offset: 0x001885D0
		private static void Configure(string structuralTypeName, IEnumerable<EdmProperty> properties, IEnumerable<PropertyInfo> propertyPath, PrimitivePropertyConfiguration propertyConfiguration)
		{
			EdmProperty edmProperty = properties.SingleOrDefault((EdmProperty p) => p.GetClrPropertyInfo().IsSameAs(propertyPath.First<PropertyInfo>()));
			if (edmProperty == null)
			{
				throw Error.PropertyNotFound(propertyPath.First<PropertyInfo>().Name, structuralTypeName);
			}
			if (edmProperty.IsUnderlyingPrimitiveType)
			{
				propertyConfiguration.Configure(edmProperty);
				return;
			}
			StructuralTypeConfiguration.Configure(edmProperty.ComplexType.Name, edmProperty.ComplexType.Properties, new PropertyPath(propertyPath.Skip(1)), propertyConfiguration);
		}

		// Token: 0x04002451 RID: 9297
		private readonly Dictionary<PropertyPath, PrimitivePropertyConfiguration> _primitivePropertyConfigurations = new Dictionary<PropertyPath, PrimitivePropertyConfiguration>();

		// Token: 0x04002452 RID: 9298
		private readonly HashSet<PropertyInfo> _ignoredProperties = new HashSet<PropertyInfo>();

		// Token: 0x04002453 RID: 9299
		private readonly Type _clrType;
	}
}
