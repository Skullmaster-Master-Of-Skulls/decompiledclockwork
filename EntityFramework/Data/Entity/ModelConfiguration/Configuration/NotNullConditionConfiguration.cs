using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Mapping;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007B9 RID: 1977
	public class NotNullConditionConfiguration
	{
		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x0600596E RID: 22894 RVA: 0x001810AF File Offset: 0x0017F2AF
		// (set) Token: 0x0600596F RID: 22895 RVA: 0x001810B7 File Offset: 0x0017F2B7
		internal PropertyPath PropertyPath { get; set; }

		// Token: 0x06005970 RID: 22896 RVA: 0x001810C0 File Offset: 0x0017F2C0
		internal NotNullConditionConfiguration(EntityMappingConfiguration entityMapConfiguration, PropertyPath propertyPath)
		{
			this._entityMappingConfiguration = entityMapConfiguration;
			this.PropertyPath = propertyPath;
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x001810D6 File Offset: 0x0017F2D6
		private NotNullConditionConfiguration(EntityMappingConfiguration owner, NotNullConditionConfiguration source)
		{
			this._entityMappingConfiguration = owner;
			this.PropertyPath = source.PropertyPath;
		}

		// Token: 0x06005972 RID: 22898 RVA: 0x001810F1 File Offset: 0x0017F2F1
		internal virtual NotNullConditionConfiguration Clone(EntityMappingConfiguration owner)
		{
			return new NotNullConditionConfiguration(owner, this);
		}

		// Token: 0x06005973 RID: 22899 RVA: 0x001810FA File Offset: 0x0017F2FA
		public void HasValue()
		{
			this._entityMappingConfiguration.AddNullabilityCondition(this);
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x00181130 File Offset: 0x0017F330
		internal void Configure(DbDatabaseMapping databaseMapping, MappingFragment fragment, EntityType entityType)
		{
			IEnumerable<EdmPropertyPath> edmPropertyPath = EntityMappingConfiguration.PropertyPathToEdmPropertyPath(this.PropertyPath, entityType);
			if (edmPropertyPath.Count<EdmPropertyPath>() > 1)
			{
				throw Error.InvalidNotNullCondition(this.PropertyPath.ToString(), entityType.Name);
			}
			EdmProperty edmProperty = (from pm in fragment.ColumnMappings
			where pm.PropertyPath.SequenceEqual(edmPropertyPath.Single<EdmPropertyPath>())
			select pm.ColumnProperty).SingleOrDefault<EdmProperty>();
			if (edmProperty == null || !fragment.Table.Properties.Contains(edmProperty))
			{
				throw Error.InvalidNotNullCondition(this.PropertyPath.ToString(), entityType.Name);
			}
			if (ValueConditionConfiguration.AnyBaseTypeToTableWithoutColumnCondition(databaseMapping, entityType, fragment.Table, edmProperty))
			{
				edmProperty.Nullable = true;
			}
			PrimitivePropertyConfiguration primitivePropertyConfiguration = new PrimitivePropertyConfiguration
			{
				IsNullable = new bool?(false),
				OverridableConfigurationParts = OverridableConfigurationParts.OverridableInSSpace
			};
			primitivePropertyConfiguration.Configure(edmPropertyPath.Single<EdmPropertyPath>().Last<EdmProperty>());
			fragment.AddNullabilityCondition(edmProperty, false);
		}

		// Token: 0x06005975 RID: 22901 RVA: 0x00181236 File Offset: 0x0017F436
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x0018123E File Offset: 0x0017F43E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005977 RID: 22903 RVA: 0x00181247 File Offset: 0x0017F447
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005978 RID: 22904 RVA: 0x0018124F File Offset: 0x0017F44F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040023B7 RID: 9143
		private readonly EntityMappingConfiguration _entityMappingConfiguration;
	}
}
