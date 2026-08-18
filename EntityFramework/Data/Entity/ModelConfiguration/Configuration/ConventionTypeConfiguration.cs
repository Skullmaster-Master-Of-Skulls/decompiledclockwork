using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001BB RID: 443
	public class ConventionTypeConfiguration
	{
		// Token: 0x06000EDC RID: 3804 RVA: 0x000400DE File Offset: 0x0003E2DE
		internal ConventionTypeConfiguration(Type type, ModelConfiguration modelConfiguration) : this(type, null, null, modelConfiguration)
		{
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x000400EA File Offset: 0x0003E2EA
		internal ConventionTypeConfiguration(Type type, Func<EntityTypeConfiguration> entityTypeConfiguration, ModelConfiguration modelConfiguration) : this(type, entityTypeConfiguration, null, modelConfiguration)
		{
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x000400F6 File Offset: 0x0003E2F6
		internal ConventionTypeConfiguration(Type type, Func<ComplexTypeConfiguration> complexTypeConfiguration, ModelConfiguration modelConfiguration) : this(type, null, complexTypeConfiguration, modelConfiguration)
		{
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00040102 File Offset: 0x0003E302
		private ConventionTypeConfiguration(Type type, Func<EntityTypeConfiguration> entityTypeConfiguration, Func<ComplexTypeConfiguration> complexTypeConfiguration, ModelConfiguration modelConfiguration)
		{
			this._type = type;
			this._entityTypeConfiguration = entityTypeConfiguration;
			this._complexTypeConfiguration = complexTypeConfiguration;
			this._modelConfiguration = modelConfiguration;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00040127 File Offset: 0x0003E327
		public Type ClrType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00040130 File Offset: 0x0003E330
		public ConventionTypeConfiguration HasEntitySetName(string entitySetName)
		{
			Check.NotEmpty(entitySetName, "entitySetName");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.HasEntitySetName);
			if (this._entityTypeConfiguration != null && this._entityTypeConfiguration().EntitySetName == null)
			{
				this._entityTypeConfiguration().EntitySetName = entitySetName;
			}
			return this;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0004017C File Offset: 0x0003E37C
		public ConventionTypeConfiguration Ignore()
		{
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.IgnoreType);
			if (this._entityTypeConfiguration == null && this._complexTypeConfiguration == null)
			{
				this._modelConfiguration.Ignore(this._type);
			}
			return this;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x000401A7 File Offset: 0x0003E3A7
		public ConventionTypeConfiguration IsComplexType()
		{
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.IsComplexType);
			if (this._entityTypeConfiguration == null && this._complexTypeConfiguration == null)
			{
				this._modelConfiguration.ComplexType(this._type);
			}
			return this;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x000401D4 File Offset: 0x0003E3D4
		public ConventionTypeConfiguration Ignore(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			PropertyInfo instanceProperty = this._type.GetInstanceProperty(propertyName);
			if (instanceProperty == null)
			{
				throw new InvalidOperationException(Strings.NoSuchProperty(propertyName, this._type.Name));
			}
			this.Ignore(instanceProperty);
			return this;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00040224 File Offset: 0x0003E424
		public ConventionTypeConfiguration Ignore(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.Ignore);
			if (propertyInfo != null)
			{
				if (this._entityTypeConfiguration != null)
				{
					this._entityTypeConfiguration().Ignore(propertyInfo);
				}
				if (this._complexTypeConfiguration != null)
				{
					this._complexTypeConfiguration().Ignore(propertyInfo);
				}
			}
			return this;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00040280 File Offset: 0x0003E480
		public ConventionPrimitivePropertyConfiguration Property(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			PropertyInfo instanceProperty = this._type.GetInstanceProperty(propertyName);
			if (instanceProperty == null)
			{
				throw new InvalidOperationException(Strings.NoSuchProperty(propertyName, this._type.Name));
			}
			return this.Property(instanceProperty);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x000402CD File Offset: 0x0003E4CD
		public ConventionPrimitivePropertyConfiguration Property(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			return this.Property(new PropertyPath(propertyInfo));
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x000402F8 File Offset: 0x0003E4F8
		internal ConventionPrimitivePropertyConfiguration Property(PropertyPath propertyPath)
		{
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.Property);
			PropertyInfo propertyInfo = propertyPath.Last<PropertyInfo>();
			if (!propertyInfo.IsValidEdmScalarProperty())
			{
				throw new InvalidOperationException(Strings.LightweightEntityConfiguration_NonScalarProperty(propertyPath));
			}
			PrimitivePropertyConfiguration propertyConfiguration = (this._entityTypeConfiguration != null) ? this._entityTypeConfiguration().Property(propertyPath, null) : ((this._complexTypeConfiguration != null) ? this._complexTypeConfiguration().Property(propertyPath, null) : null);
			return new ConventionPrimitivePropertyConfiguration(propertyInfo, () => propertyConfiguration);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00040390 File Offset: 0x0003E590
		internal ConventionNavigationPropertyConfiguration NavigationProperty(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			PropertyInfo instanceProperty = this._type.GetInstanceProperty(propertyName);
			if (instanceProperty == null)
			{
				throw new InvalidOperationException(Strings.NoSuchProperty(propertyName, this._type.Name));
			}
			return this.NavigationProperty(instanceProperty);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x000403DD File Offset: 0x0003E5DD
		internal ConventionNavigationPropertyConfiguration NavigationProperty(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			return this.NavigationProperty(new PropertyPath(propertyInfo));
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x000403F8 File Offset: 0x0003E5F8
		internal ConventionNavigationPropertyConfiguration NavigationProperty(PropertyPath propertyPath)
		{
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.NavigationProperty);
			PropertyInfo propertyInfo = propertyPath.Last<PropertyInfo>();
			if (!propertyInfo.IsValidEdmNavigationProperty())
			{
				throw new InvalidOperationException(Strings.LightweightEntityConfiguration_InvalidNavigationProperty(propertyPath));
			}
			NavigationPropertyConfiguration configuration = (this._entityTypeConfiguration != null) ? this._entityTypeConfiguration().Navigation(propertyInfo) : null;
			return new ConventionNavigationPropertyConfiguration(configuration, this._modelConfiguration);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00040454 File Offset: 0x0003E654
		public ConventionTypeConfiguration HasKey(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			PropertyInfo instanceProperty = this._type.GetInstanceProperty(propertyName);
			if (instanceProperty == null)
			{
				throw new InvalidOperationException(Strings.NoSuchProperty(propertyName, this._type.Name));
			}
			return this.HasKey(instanceProperty);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x000404A4 File Offset: 0x0003E6A4
		public ConventionTypeConfiguration HasKey(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.HasKey);
			if (this._entityTypeConfiguration != null && !this._entityTypeConfiguration().IsKeyConfigured)
			{
				this._entityTypeConfiguration().Key(propertyInfo);
			}
			return this;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0004052C File Offset: 0x0003E72C
		public ConventionTypeConfiguration HasKey(IEnumerable<string> propertyNames)
		{
			Check.NotNull<IEnumerable<string>>(propertyNames, "propertyNames");
			PropertyInfo[] keyProperties = propertyNames.Select(delegate(string n)
			{
				PropertyInfo instanceProperty = this._type.GetInstanceProperty(n);
				if (instanceProperty == null)
				{
					throw new InvalidOperationException(Strings.NoSuchProperty(n, this._type.Name));
				}
				return instanceProperty;
			}).ToArray<PropertyInfo>();
			return this.HasKey(keyProperties);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00040574 File Offset: 0x0003E774
		public ConventionTypeConfiguration HasKey(IEnumerable<PropertyInfo> keyProperties)
		{
			Check.NotNull<IEnumerable<PropertyInfo>>(keyProperties, "keyProperties");
			EntityUtil.CheckArgumentContainsNull<PropertyInfo>(ref keyProperties, "keyProperties");
			EntityUtil.CheckArgumentEmpty<PropertyInfo>(ref keyProperties, (string p) => Strings.CollectionEmpty(p, "HasKey"), "keyProperties");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.HasKey);
			if (this._entityTypeConfiguration != null && !this._entityTypeConfiguration().IsKeyConfigured)
			{
				this._entityTypeConfiguration().Key(keyProperties);
			}
			return this;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000405F8 File Offset: 0x0003E7F8
		public ConventionTypeConfiguration ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.ToTable);
			if (this._entityTypeConfiguration != null && !this._entityTypeConfiguration().IsTableNameConfigured)
			{
				DatabaseName databaseName = DatabaseName.Parse(tableName);
				this._entityTypeConfiguration().ToTable(databaseName.Name, databaseName.Schema);
			}
			return this;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0004065C File Offset: 0x0003E85C
		public ConventionTypeConfiguration ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.ToTable);
			if (this._entityTypeConfiguration != null && !this._entityTypeConfiguration().IsTableNameConfigured)
			{
				this._entityTypeConfiguration().ToTable(tableName, schemaName);
			}
			return this;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x000406B0 File Offset: 0x0003E8B0
		public ConventionTypeConfiguration HasTableAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.HasTableAnnotation);
			if (this._entityTypeConfiguration != null && !this._entityTypeConfiguration().Annotations.ContainsKey(name))
			{
				this._entityTypeConfiguration().SetAnnotation(name, value);
			}
			return this;
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00040707 File Offset: 0x0003E907
		public ConventionTypeConfiguration MapToStoredProcedures()
		{
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.MapToStoredProcedures);
			if (this._entityTypeConfiguration != null)
			{
				this._entityTypeConfiguration().MapToStoredProcedures();
			}
			return this;
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0004072C File Offset: 0x0003E92C
		public ConventionTypeConfiguration MapToStoredProcedures(Action<ConventionModificationStoredProceduresConfiguration> modificationStoredProceduresConfigurationAction)
		{
			Check.NotNull<Action<ConventionModificationStoredProceduresConfiguration>>(modificationStoredProceduresConfigurationAction, "modificationStoredProceduresConfigurationAction");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.MapToStoredProcedures);
			ConventionModificationStoredProceduresConfiguration conventionModificationStoredProceduresConfiguration = new ConventionModificationStoredProceduresConfiguration(this._type);
			modificationStoredProceduresConfigurationAction(conventionModificationStoredProceduresConfiguration);
			this.MapToStoredProcedures(conventionModificationStoredProceduresConfiguration.Configuration);
			return this;
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0004076D File Offset: 0x0003E96D
		internal void MapToStoredProcedures(ModificationStoredProceduresConfiguration modificationStoredProceduresConfiguration)
		{
			if (this._entityTypeConfiguration != null)
			{
				this._entityTypeConfiguration().MapToStoredProcedures(modificationStoredProceduresConfiguration, false);
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000407EC File Offset: 0x0003E9EC
		private void ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect aspect)
		{
			this._currentConfigurationAspect |= aspect;
			if (this._currentConfigurationAspect.HasFlag(ConventionTypeConfiguration.ConfigurationAspect.IgnoreType) && ConventionTypeConfiguration.ConfigurationAspectsConflictingWithIgnoreType.Any((ConventionTypeConfiguration.ConfigurationAspect ca) => this._currentConfigurationAspect.HasFlag(ca)))
			{
				throw new InvalidOperationException(Strings.LightweightEntityConfiguration_ConfigurationConflict_IgnoreType(ConventionTypeConfiguration.ConfigurationAspectsConflictingWithIgnoreType.First((ConventionTypeConfiguration.ConfigurationAspect ca) => this._currentConfigurationAspect.HasFlag(ca)), this._type.Name));
			}
			if (this._currentConfigurationAspect.HasFlag(ConventionTypeConfiguration.ConfigurationAspect.IsComplexType) && ConventionTypeConfiguration.ConfigurationAspectsConflictingWithComplexType.Any((ConventionTypeConfiguration.ConfigurationAspect ca) => this._currentConfigurationAspect.HasFlag(ca)))
			{
				throw new InvalidOperationException(Strings.LightweightEntityConfiguration_ConfigurationConflict_ComplexType(ConventionTypeConfiguration.ConfigurationAspectsConflictingWithComplexType.First((ConventionTypeConfiguration.ConfigurationAspect ca) => this._currentConfigurationAspect.HasFlag(ca)), this._type.Name));
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x000408D8 File Offset: 0x0003EAD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000408E0 File Offset: 0x0003EAE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000408E9 File Offset: 0x0003EAE9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000408F1 File Offset: 0x0003EAF1
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000404 RID: 1028
		private readonly Type _type;

		// Token: 0x04000405 RID: 1029
		private readonly Func<EntityTypeConfiguration> _entityTypeConfiguration;

		// Token: 0x04000406 RID: 1030
		private readonly ModelConfiguration _modelConfiguration;

		// Token: 0x04000407 RID: 1031
		private readonly Func<ComplexTypeConfiguration> _complexTypeConfiguration;

		// Token: 0x04000408 RID: 1032
		private ConventionTypeConfiguration.ConfigurationAspect _currentConfigurationAspect;

		// Token: 0x04000409 RID: 1033
		private static readonly List<ConventionTypeConfiguration.ConfigurationAspect> ConfigurationAspectsConflictingWithIgnoreType = new List<ConventionTypeConfiguration.ConfigurationAspect>
		{
			ConventionTypeConfiguration.ConfigurationAspect.IsComplexType,
			ConventionTypeConfiguration.ConfigurationAspect.HasEntitySetName,
			ConventionTypeConfiguration.ConfigurationAspect.Ignore,
			ConventionTypeConfiguration.ConfigurationAspect.HasKey,
			ConventionTypeConfiguration.ConfigurationAspect.MapToStoredProcedures,
			ConventionTypeConfiguration.ConfigurationAspect.NavigationProperty,
			ConventionTypeConfiguration.ConfigurationAspect.Property,
			ConventionTypeConfiguration.ConfigurationAspect.ToTable,
			ConventionTypeConfiguration.ConfigurationAspect.HasTableAnnotation
		};

		// Token: 0x0400040A RID: 1034
		private static readonly List<ConventionTypeConfiguration.ConfigurationAspect> ConfigurationAspectsConflictingWithComplexType = new List<ConventionTypeConfiguration.ConfigurationAspect>
		{
			ConventionTypeConfiguration.ConfigurationAspect.HasEntitySetName,
			ConventionTypeConfiguration.ConfigurationAspect.HasKey,
			ConventionTypeConfiguration.ConfigurationAspect.MapToStoredProcedures,
			ConventionTypeConfiguration.ConfigurationAspect.NavigationProperty,
			ConventionTypeConfiguration.ConfigurationAspect.ToTable,
			ConventionTypeConfiguration.ConfigurationAspect.HasTableAnnotation
		};

		// Token: 0x020001BC RID: 444
		[Flags]
		private enum ConfigurationAspect : uint
		{
			// Token: 0x0400040D RID: 1037
			None = 0U,
			// Token: 0x0400040E RID: 1038
			HasEntitySetName = 1U,
			// Token: 0x0400040F RID: 1039
			HasKey = 2U,
			// Token: 0x04000410 RID: 1040
			IgnoreType = 4U,
			// Token: 0x04000411 RID: 1041
			Ignore = 8U,
			// Token: 0x04000412 RID: 1042
			IsComplexType = 16U,
			// Token: 0x04000413 RID: 1043
			MapToStoredProcedures = 32U,
			// Token: 0x04000414 RID: 1044
			Property = 64U,
			// Token: 0x04000415 RID: 1045
			NavigationProperty = 128U,
			// Token: 0x04000416 RID: 1046
			ToTable = 256U,
			// Token: 0x04000417 RID: 1047
			HasTableAnnotation = 512U
		}
	}
}
