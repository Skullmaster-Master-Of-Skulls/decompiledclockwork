using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Mapping;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Edm.Services;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007C0 RID: 1984
	[DebuggerDisplay("{Discriminator}")]
	public class ValueConditionConfiguration
	{
		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x060059C5 RID: 22981 RVA: 0x001832EF File Offset: 0x001814EF
		// (set) Token: 0x060059C6 RID: 22982 RVA: 0x001832F7 File Offset: 0x001814F7
		internal string Discriminator { get; set; }

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x060059C7 RID: 22983 RVA: 0x00183300 File Offset: 0x00181500
		// (set) Token: 0x060059C8 RID: 22984 RVA: 0x00183308 File Offset: 0x00181508
		internal object Value { get; set; }

		// Token: 0x060059C9 RID: 22985 RVA: 0x00183311 File Offset: 0x00181511
		internal ValueConditionConfiguration(EntityMappingConfiguration entityMapConfiguration, string discriminator)
		{
			this._entityMappingConfiguration = entityMapConfiguration;
			this.Discriminator = discriminator;
		}

		// Token: 0x060059CA RID: 22986 RVA: 0x00183328 File Offset: 0x00181528
		private ValueConditionConfiguration(EntityMappingConfiguration owner, ValueConditionConfiguration source)
		{
			this._entityMappingConfiguration = owner;
			this.Discriminator = source.Discriminator;
			this.Value = source.Value;
			this._configuration = ((source._configuration == null) ? null : source._configuration.Clone());
		}

		// Token: 0x060059CB RID: 22987 RVA: 0x00183376 File Offset: 0x00181576
		internal virtual ValueConditionConfiguration Clone(EntityMappingConfiguration owner)
		{
			return new ValueConditionConfiguration(owner, this);
		}

		// Token: 0x060059CC RID: 22988 RVA: 0x00183380 File Offset: 0x00181580
		private T GetOrCreateConfiguration<T>() where T : PrimitivePropertyConfiguration, new()
		{
			if (this._configuration == null)
			{
				this._configuration = Activator.CreateInstance<T>();
			}
			else if (!(this._configuration is T))
			{
				T t = Activator.CreateInstance<T>();
				t.CopyFrom(this._configuration);
				this._configuration = t;
			}
			this._configuration.OverridableConfigurationParts = OverridableConfigurationParts.None;
			return (T)((object)this._configuration);
		}

		// Token: 0x060059CD RID: 22989 RVA: 0x001833F0 File Offset: 0x001815F0
		public PrimitiveColumnConfiguration HasValue<T>(T value) where T : struct
		{
			ValueConditionConfiguration.ValidateValueType(value);
			this.Value = value;
			this._entityMappingConfiguration.AddValueCondition(this);
			return new PrimitiveColumnConfiguration(this.GetOrCreateConfiguration<PrimitivePropertyConfiguration>());
		}

		// Token: 0x060059CE RID: 22990 RVA: 0x00183420 File Offset: 0x00181620
		public PrimitiveColumnConfiguration HasValue<T>(T? value) where T : struct
		{
			ValueConditionConfiguration.ValidateValueType(value);
			this.Value = value;
			this._entityMappingConfiguration.AddValueCondition(this);
			return new PrimitiveColumnConfiguration(this.GetOrCreateConfiguration<PrimitivePropertyConfiguration>());
		}

		// Token: 0x060059CF RID: 22991 RVA: 0x00183450 File Offset: 0x00181650
		public StringColumnConfiguration HasValue(string value)
		{
			this.Value = value;
			this._entityMappingConfiguration.AddValueCondition(this);
			return new StringColumnConfiguration(this.GetOrCreateConfiguration<StringPropertyConfiguration>());
		}

		// Token: 0x060059D0 RID: 22992 RVA: 0x00183470 File Offset: 0x00181670
		private static void ValidateValueType(object value)
		{
			PrimitiveType primitiveType;
			if (value != null && !value.GetType().IsPrimitiveType(out primitiveType))
			{
				throw Error.InvalidDiscriminatorType(value.GetType().Name);
			}
		}

		// Token: 0x060059D1 RID: 22993 RVA: 0x001834E0 File Offset: 0x001816E0
		internal static IEnumerable<MappingFragment> GetMappingFragmentsWithColumnAsDefaultDiscriminator(DbDatabaseMapping databaseMapping, EntityType table, EdmProperty column)
		{
			return from tmf in databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.EntitySetMappings).SelectMany((EntitySetMapping esm) => esm.EntityTypeMappings).SelectMany((EntityTypeMapping etm) => etm.MappingFragments)
			where tmf.Table == table && tmf.GetDefaultDiscriminator() == column
			select tmf;
		}

		// Token: 0x060059D2 RID: 22994 RVA: 0x001835BC File Offset: 0x001817BC
		internal static bool AnyBaseTypeToTableWithoutColumnCondition(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType table, EdmProperty column)
		{
			for (EdmType baseType = entityType.BaseType; baseType != null; baseType = baseType.BaseType)
			{
				if (!baseType.Abstract)
				{
					List<MappingFragment> source = (from tmf in databaseMapping.GetEntityTypeMappings((EntityType)baseType).SelectMany((EntityTypeMapping etm) => etm.MappingFragments)
					where tmf.Table == table
					select tmf).ToList<MappingFragment>();
					if (source.Any<MappingFragment>())
					{
						if (source.SelectMany((MappingFragment etmf) => etmf.ColumnConditions).All((ConditionPropertyMapping cc) => cc.Column != column))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060059D3 RID: 22995 RVA: 0x001836B8 File Offset: 0x001818B8
		internal void Configure(DbDatabaseMapping databaseMapping, MappingFragment fragment, EntityType entityType, DbProviderManifest providerManifest)
		{
			EdmProperty edmProperty = fragment.Table.Properties.SingleOrDefault((EdmProperty c) => string.Equals(c.Name, this.Discriminator, StringComparison.Ordinal));
			if (edmProperty != null && ValueConditionConfiguration.GetMappingFragmentsWithColumnAsDefaultDiscriminator(databaseMapping, fragment.Table, edmProperty).Any<MappingFragment>())
			{
				edmProperty.Name = (from p in fragment.Table.Properties
				select p.Name).Uniquify(edmProperty.Name);
				edmProperty = null;
			}
			if (edmProperty == null)
			{
				TypeUsage storeType = providerManifest.GetStoreType(DatabaseMappingGenerator.DiscriminatorTypeUsage);
				edmProperty = new EdmProperty(this.Discriminator, storeType)
				{
					Nullable = false
				};
				TablePrimitiveOperations.AddColumn(fragment.Table, edmProperty);
			}
			if (ValueConditionConfiguration.AnyBaseTypeToTableWithoutColumnCondition(databaseMapping, entityType, fragment.Table, edmProperty))
			{
				edmProperty.Nullable = true;
			}
			PrimitivePropertyConfiguration primitivePropertyConfiguration = edmProperty.GetConfiguration() as PrimitivePropertyConfiguration;
			if (this.Value != null)
			{
				this.ConfigureColumnType(providerManifest, primitivePropertyConfiguration, edmProperty);
				fragment.AddDiscriminatorCondition(edmProperty, this.Value);
			}
			else
			{
				if (string.IsNullOrWhiteSpace(edmProperty.TypeName))
				{
					TypeUsage storeType2 = providerManifest.GetStoreType(DatabaseMappingGenerator.DiscriminatorTypeUsage);
					edmProperty.PrimitiveType = (PrimitiveType)storeType2.EdmType;
					edmProperty.MaxLength = new int?(128);
					edmProperty.Nullable = false;
				}
				this.GetOrCreateConfiguration<PrimitivePropertyConfiguration>().IsNullable = new bool?(true);
				fragment.AddNullabilityCondition(edmProperty, true);
			}
			if (this._configuration == null)
			{
				return;
			}
			string p2;
			if (primitivePropertyConfiguration != null && (primitivePropertyConfiguration.OverridableConfigurationParts & OverridableConfigurationParts.OverridableInCSpace) != OverridableConfigurationParts.OverridableInCSpace && !primitivePropertyConfiguration.IsCompatible(this._configuration, true, out p2))
			{
				throw Error.ConflictingColumnConfiguration(edmProperty, fragment.Table, p2);
			}
			if (this._configuration.IsNullable != null)
			{
				edmProperty.Nullable = this._configuration.IsNullable.Value;
			}
			this._configuration.Configure(edmProperty, fragment.Table, providerManifest, false, false);
		}

		// Token: 0x060059D4 RID: 22996 RVA: 0x0018388C File Offset: 0x00181A8C
		private void ConfigureColumnType(DbProviderManifest providerManifest, PrimitivePropertyConfiguration existingConfiguration, EdmProperty discriminatorColumn)
		{
			if ((existingConfiguration != null && existingConfiguration.ColumnType != null) || (this._configuration != null && this._configuration.ColumnType != null))
			{
				return;
			}
			PrimitiveType primitiveType;
			this.Value.GetType().IsPrimitiveType(out primitiveType);
			PrimitiveType primitiveType2 = (PrimitiveType)providerManifest.GetStoreType((primitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.String)) ? DatabaseMappingGenerator.DiscriminatorTypeUsage : TypeUsage.Create(PrimitiveType.GetEdmPrimitiveType(primitiveType.PrimitiveTypeKind))).EdmType;
			if (existingConfiguration != null && !discriminatorColumn.TypeName.Equals(primitiveType2.Name, StringComparison.OrdinalIgnoreCase))
			{
				throw Error.ConflictingInferredColumnType(discriminatorColumn.Name, discriminatorColumn.TypeName, primitiveType2.Name);
			}
			discriminatorColumn.PrimitiveType = primitiveType2;
		}

		// Token: 0x060059D5 RID: 22997 RVA: 0x00183935 File Offset: 0x00181B35
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060059D6 RID: 22998 RVA: 0x0018393D File Offset: 0x00181B3D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060059D7 RID: 22999 RVA: 0x00183946 File Offset: 0x00181B46
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060059D8 RID: 23000 RVA: 0x0018394E File Offset: 0x00181B4E
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040023DC RID: 9180
		private readonly EntityMappingConfiguration _entityMappingConfiguration;

		// Token: 0x040023DD RID: 9181
		private PrimitivePropertyConfiguration _configuration;
	}
}
