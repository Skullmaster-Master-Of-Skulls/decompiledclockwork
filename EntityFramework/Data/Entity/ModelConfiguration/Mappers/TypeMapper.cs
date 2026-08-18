using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Mappers
{
	// Token: 0x02000827 RID: 2087
	internal sealed class TypeMapper
	{
		// Token: 0x06005DA7 RID: 23975 RVA: 0x00194A84 File Offset: 0x00192C84
		public TypeMapper(MappingContext mappingContext)
		{
			this._mappingContext = mappingContext;
			this._knownTypes.AddRange((from t in mappingContext.ModelConfiguration.ConfiguredTypes
			select t.Assembly()).Distinct<Assembly>().SelectMany((Assembly a) => from type in a.GetAccessibleTypes()
			where type.IsValidStructuralType()
			select type));
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06005DA8 RID: 23976 RVA: 0x00194B08 File Offset: 0x00192D08
		public MappingContext MappingContext
		{
			get
			{
				return this._mappingContext;
			}
		}

		// Token: 0x06005DA9 RID: 23977 RVA: 0x00194B10 File Offset: 0x00192D10
		public EnumType MapEnumType(Type type)
		{
			EnumType enumType = TypeMapper.GetExistingEdmType<EnumType>(this._mappingContext.Model, type);
			if (enumType == null)
			{
				PrimitiveType underlyingType;
				if (!Enum.GetUnderlyingType(type).IsPrimitiveType(out underlyingType))
				{
					return null;
				}
				enumType = this._mappingContext.Model.AddEnumType(type.Name, this._mappingContext.ModelConfiguration.ModelNamespace);
				enumType.IsFlags = type.GetCustomAttributes(false).Any<FlagsAttribute>();
				enumType.SetClrType(type);
				enumType.UnderlyingType = underlyingType;
				foreach (string text in Enum.GetNames(type))
				{
					enumType.AddMember(new EnumMember(text, Convert.ChangeType(Enum.Parse(type, text), type.GetEnumUnderlyingType(), CultureInfo.InvariantCulture)));
				}
			}
			return enumType;
		}

		// Token: 0x06005DAA RID: 23978 RVA: 0x00194C18 File Offset: 0x00192E18
		public ComplexType MapComplexType(Type type, bool discoverNested = false)
		{
			if (!type.IsValidStructuralType())
			{
				return null;
			}
			this._mappingContext.ConventionsConfiguration.ApplyModelConfiguration(type, this._mappingContext.ModelConfiguration);
			if (this._mappingContext.ModelConfiguration.IsIgnoredType(type) || (!discoverNested && !this._mappingContext.ModelConfiguration.IsComplexType(type)))
			{
				return null;
			}
			ComplexType complexType = TypeMapper.GetExistingEdmType<ComplexType>(this._mappingContext.Model, type);
			if (complexType == null)
			{
				complexType = this._mappingContext.Model.AddComplexType(type.Name, this._mappingContext.ModelConfiguration.ModelNamespace);
				Func<ComplexTypeConfiguration> complexTypeConfiguration = () => this._mappingContext.ModelConfiguration.ComplexType(type);
				this._mappingContext.ConventionsConfiguration.ApplyTypeConfiguration<ComplexTypeConfiguration>(type, complexTypeConfiguration, this._mappingContext.ModelConfiguration);
				this.MapStructuralElements<ComplexTypeConfiguration>(type, complexType.GetMetadataProperties(), delegate(PropertyMapper m, PropertyInfo p)
				{
					m.Map(p, complexType, complexTypeConfiguration);
				}, complexTypeConfiguration);
			}
			return complexType;
		}

		// Token: 0x06005DAB RID: 23979 RVA: 0x00194DD0 File Offset: 0x00192FD0
		public EntityType MapEntityType(Type type)
		{
			if (!type.IsValidStructuralType() || this._mappingContext.ModelConfiguration.IsIgnoredType(type) || this._mappingContext.ModelConfiguration.IsComplexType(type))
			{
				return null;
			}
			EntityType entityType = TypeMapper.GetExistingEdmType<EntityType>(this._mappingContext.Model, type);
			if (entityType == null)
			{
				this._mappingContext.ConventionsConfiguration.ApplyModelConfiguration(type, this._mappingContext.ModelConfiguration);
				if (this._mappingContext.ModelConfiguration.IsIgnoredType(type) || this._mappingContext.ModelConfiguration.IsComplexType(type))
				{
					return null;
				}
				entityType = this._mappingContext.Model.AddEntityType(type.Name, this._mappingContext.ModelConfiguration.ModelNamespace);
				entityType.Abstract = type.IsAbstract();
				EntityType entityType2 = this._mappingContext.Model.GetEntityType(type.BaseType().Name);
				if (entityType2 == null)
				{
					this._mappingContext.Model.AddEntitySet(entityType.Name, entityType, null);
				}
				else if (object.ReferenceEquals(entityType2, entityType))
				{
					throw new NotSupportedException(Strings.SimpleNameCollision(type.FullName, type.BaseType().FullName, type.Name));
				}
				entityType.BaseType = entityType2;
				Func<EntityTypeConfiguration> entityTypeConfiguration = () => this._mappingContext.ModelConfiguration.Entity(type);
				this._mappingContext.ConventionsConfiguration.ApplyTypeConfiguration<EntityTypeConfiguration>(type, entityTypeConfiguration, this._mappingContext.ModelConfiguration);
				List<PropertyInfo> navigationProperties = new List<PropertyInfo>();
				this.MapStructuralElements<EntityTypeConfiguration>(type, entityType.GetMetadataProperties(), delegate(PropertyMapper m, PropertyInfo p)
				{
					if (!m.MapIfNotNavigationProperty(p, entityType, entityTypeConfiguration))
					{
						navigationProperties.Add(p);
					}
				}, entityTypeConfiguration);
				IEnumerable<PropertyInfo> enumerable = navigationProperties;
				if (this._mappingContext.ModelBuilderVersion.IsEF6OrHigher())
				{
					enumerable = from p in enumerable
					orderby p.Name
					select p;
				}
				foreach (PropertyInfo propertyInfo in enumerable)
				{
					new NavigationPropertyMapper(this).Map(propertyInfo, entityType, entityTypeConfiguration);
				}
				if (entityType.BaseType != null)
				{
					this.LiftInheritedProperties(type, entityType);
				}
				this.MapDerivedTypes(type, entityType);
			}
			return entityType;
		}

		// Token: 0x06005DAC RID: 23980 RVA: 0x001950F4 File Offset: 0x001932F4
		private static T GetExistingEdmType<T>(EdmModel model, Type type) where T : EdmType
		{
			EdmType structuralOrEnumType = model.GetStructuralOrEnumType(type.Name);
			if (structuralOrEnumType != null && type != structuralOrEnumType.GetClrType())
			{
				throw new NotSupportedException(Strings.SimpleNameCollision(type.FullName, structuralOrEnumType.GetClrType().FullName, type.Name));
			}
			return structuralOrEnumType as T;
		}

		// Token: 0x06005DAD RID: 23981 RVA: 0x0019514C File Offset: 0x0019334C
		private void MapStructuralElements<TStructuralTypeConfiguration>(Type type, ICollection<MetadataProperty> annotations, Action<PropertyMapper, PropertyInfo> propertyMappingAction, Func<TStructuralTypeConfiguration> structuralTypeConfiguration) where TStructuralTypeConfiguration : StructuralTypeConfiguration
		{
			annotations.SetClrType(type);
			new AttributeMapper(this._mappingContext.AttributeProvider).Map(type, annotations);
			PropertyMapper arg = new PropertyMapper(this);
			List<PropertyInfo> list = new PropertyFilter(this._mappingContext.ModelBuilderVersion).GetProperties(type, false, this._mappingContext.ModelConfiguration.GetConfiguredProperties(type), this._mappingContext.ModelConfiguration.StructuralTypes, false).ToList<PropertyInfo>();
			for (int i = 0; i < list.Count; i++)
			{
				PropertyInfo propertyInfo = list[i];
				this._mappingContext.ConventionsConfiguration.ApplyPropertyConfiguration(propertyInfo, this._mappingContext.ModelConfiguration);
				this._mappingContext.ConventionsConfiguration.ApplyPropertyTypeConfiguration<TStructuralTypeConfiguration>(propertyInfo, structuralTypeConfiguration, this._mappingContext.ModelConfiguration);
				if (!this._mappingContext.ModelConfiguration.IsIgnoredProperty(type, propertyInfo))
				{
					propertyMappingAction(arg, propertyInfo);
				}
			}
		}

		// Token: 0x06005DAE RID: 23982 RVA: 0x00195258 File Offset: 0x00193458
		private void MapDerivedTypes(Type type, EntityType entityType)
		{
			if (type.IsSealed())
			{
				return;
			}
			if (!this._knownTypes.Contains(type))
			{
				this._knownTypes.AddRange(from t in type.Assembly().GetAccessibleTypes()
				where t.IsValidStructuralType()
				select t);
			}
			IEnumerable<Type> source = from t in this._knownTypes
			where t.BaseType() == type
			select t;
			if (this._mappingContext.ModelBuilderVersion.IsEF6OrHigher())
			{
				source = from t in source
				orderby t.FullName
				select t;
			}
			List<Type> list = source.ToList<Type>();
			for (int i = 0; i < list.Count; i++)
			{
				Type type2 = list[i];
				EntityType entityType2 = this.MapEntityType(type2);
				if (entityType2 != null)
				{
					entityType2.BaseType = entityType;
					this.LiftDerivedType(type2, entityType2, entityType);
				}
			}
		}

		// Token: 0x06005DAF RID: 23983 RVA: 0x00195363 File Offset: 0x00193563
		private void LiftDerivedType(Type derivedType, EntityType derivedEntityType, EntityType entityType)
		{
			this._mappingContext.Model.ReplaceEntitySet(derivedEntityType, this._mappingContext.Model.GetEntitySet(entityType));
			this.LiftInheritedProperties(derivedType, derivedEntityType);
		}

		// Token: 0x06005DB0 RID: 23984 RVA: 0x001953A8 File Offset: 0x001935A8
		private void LiftInheritedProperties(Type type, EntityType entityType)
		{
			EntityTypeConfiguration entityTypeConfiguration = this._mappingContext.ModelConfiguration.GetStructuralTypeConfiguration(type) as EntityTypeConfiguration;
			if (entityTypeConfiguration != null)
			{
				entityTypeConfiguration.ClearKey();
				using (IEnumerator<PropertyInfo> enumerator = type.BaseType().GetInstanceProperties().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PropertyInfo property = enumerator.Current;
						if (!this._mappingContext.AttributeProvider.GetAttributes(property).OfType<NotMappedAttribute>().Any<NotMappedAttribute>())
						{
							if (entityTypeConfiguration.IgnoredProperties.Any((PropertyInfo p) => p.IsSameAs(property)))
							{
								throw Error.CannotIgnoreMappedBaseProperty(property.Name, type, property.DeclaringType);
							}
						}
					}
				}
			}
			List<EdmMember> list = entityType.DeclaredMembers.ToList<EdmMember>();
			HashSet<PropertyInfo> hashSet = new HashSet<PropertyInfo>(new PropertyFilter(this._mappingContext.ModelBuilderVersion).GetProperties(type, true, this._mappingContext.ModelConfiguration.GetConfiguredProperties(type), this._mappingContext.ModelConfiguration.StructuralTypes, false));
			foreach (EdmMember edmMember in list)
			{
				PropertyInfo clrPropertyInfo = edmMember.GetClrPropertyInfo();
				if (!hashSet.Contains(clrPropertyInfo))
				{
					NavigationProperty navigationProperty = edmMember as NavigationProperty;
					if (navigationProperty != null)
					{
						this._mappingContext.Model.RemoveAssociationType(navigationProperty.Association);
					}
					entityType.RemoveMember(edmMember);
				}
			}
		}

		// Token: 0x04002502 RID: 9474
		private readonly MappingContext _mappingContext;

		// Token: 0x04002503 RID: 9475
		private readonly List<Type> _knownTypes = new List<Type>();
	}
}
