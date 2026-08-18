using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration
{
	// Token: 0x02000821 RID: 2081
	public class EntityTypeConfiguration<TEntityType> : StructuralTypeConfiguration<TEntityType> where TEntityType : class
	{
		// Token: 0x06005D75 RID: 23925 RVA: 0x00193E88 File Offset: 0x00192088
		public EntityTypeConfiguration() : this(new EntityTypeConfiguration(typeof(TEntityType)))
		{
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x00193E9F File Offset: 0x0019209F
		internal EntityTypeConfiguration(EntityTypeConfiguration entityTypeConfiguration)
		{
			this._entityTypeConfiguration = entityTypeConfiguration;
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06005D77 RID: 23927 RVA: 0x00193EAE File Offset: 0x001920AE
		internal override StructuralTypeConfiguration Configuration
		{
			get
			{
				return this._entityTypeConfiguration;
			}
		}

		// Token: 0x06005D78 RID: 23928 RVA: 0x00193EDA File Offset: 0x001920DA
		internal override TPrimitivePropertyConfiguration Property<TPrimitivePropertyConfiguration>(LambdaExpression lambdaExpression)
		{
			return this.Configuration.Property<TPrimitivePropertyConfiguration>(lambdaExpression.GetComplexPropertyAccess(), delegate()
			{
				TPrimitivePropertyConfiguration result = Activator.CreateInstance<TPrimitivePropertyConfiguration>();
				result.OverridableConfigurationParts = OverridableConfigurationParts.None;
				return result;
			});
		}

		// Token: 0x06005D79 RID: 23929 RVA: 0x00193F01 File Offset: 0x00192101
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public EntityTypeConfiguration<TEntityType> HasKey<TKey>(Expression<Func<TEntityType, TKey>> keyExpression)
		{
			Check.NotNull<Expression<Func<TEntityType, TKey>>>(keyExpression, "keyExpression");
			this._entityTypeConfiguration.Key(from p in keyExpression.GetSimplePropertyAccessList()
			select p.Single<PropertyInfo>());
			return this;
		}

		// Token: 0x06005D7A RID: 23930 RVA: 0x00193F32 File Offset: 0x00192132
		public EntityTypeConfiguration<TEntityType> HasEntitySetName(string entitySetName)
		{
			Check.NotEmpty(entitySetName, "entitySetName");
			this._entityTypeConfiguration.EntitySetName = entitySetName;
			return this;
		}

		// Token: 0x06005D7B RID: 23931 RVA: 0x00193F4D File Offset: 0x0019214D
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public EntityTypeConfiguration<TEntityType> Ignore<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression)
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			this.Configuration.Ignore(propertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>());
			return this;
		}

		// Token: 0x06005D7C RID: 23932 RVA: 0x00193F74 File Offset: 0x00192174
		public EntityTypeConfiguration<TEntityType> ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			DatabaseName databaseName = DatabaseName.Parse(tableName);
			this._entityTypeConfiguration.ToTable(databaseName.Name, databaseName.Schema);
			return this;
		}

		// Token: 0x06005D7D RID: 23933 RVA: 0x00193FAC File Offset: 0x001921AC
		public EntityTypeConfiguration<TEntityType> ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this._entityTypeConfiguration.ToTable(tableName, schemaName);
			return this;
		}

		// Token: 0x06005D7E RID: 23934 RVA: 0x00193FC8 File Offset: 0x001921C8
		public EntityTypeConfiguration<TEntityType> HasTableAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			this._entityTypeConfiguration.SetAnnotation(name, value);
			return this;
		}

		// Token: 0x06005D7F RID: 23935 RVA: 0x00193FE4 File Offset: 0x001921E4
		public EntityTypeConfiguration<TEntityType> MapToStoredProcedures()
		{
			this._entityTypeConfiguration.MapToStoredProcedures();
			return this;
		}

		// Token: 0x06005D80 RID: 23936 RVA: 0x00193FF4 File Offset: 0x001921F4
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public EntityTypeConfiguration<TEntityType> MapToStoredProcedures(Action<ModificationStoredProceduresConfiguration<TEntityType>> modificationStoredProcedureMappingConfigurationAction)
		{
			Check.NotNull<Action<ModificationStoredProceduresConfiguration<TEntityType>>>(modificationStoredProcedureMappingConfigurationAction, "modificationStoredProcedureMappingConfigurationAction");
			ModificationStoredProceduresConfiguration<TEntityType> modificationStoredProceduresConfiguration = new ModificationStoredProceduresConfiguration<TEntityType>();
			modificationStoredProcedureMappingConfigurationAction(modificationStoredProceduresConfiguration);
			this._entityTypeConfiguration.MapToStoredProcedures(modificationStoredProceduresConfiguration.Configuration, true);
			return this;
		}

		// Token: 0x06005D81 RID: 23937 RVA: 0x00194030 File Offset: 0x00192230
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public EntityTypeConfiguration<TEntityType> Map(Action<EntityMappingConfiguration<TEntityType>> entityMappingConfigurationAction)
		{
			Check.NotNull<Action<EntityMappingConfiguration<TEntityType>>>(entityMappingConfigurationAction, "entityMappingConfigurationAction");
			EntityMappingConfiguration<TEntityType> entityMappingConfiguration = new EntityMappingConfiguration<TEntityType>();
			entityMappingConfigurationAction(entityMappingConfiguration);
			this._entityTypeConfiguration.AddMappingConfiguration(entityMappingConfiguration.EntityMappingConfigurationInstance, true);
			return this;
		}

		// Token: 0x06005D82 RID: 23938 RVA: 0x0019406C File Offset: 0x0019226C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public EntityTypeConfiguration<TEntityType> Map<TDerived>(Action<EntityMappingConfiguration<TDerived>> derivedTypeMapConfigurationAction) where TDerived : class, TEntityType
		{
			Check.NotNull<Action<EntityMappingConfiguration<TDerived>>>(derivedTypeMapConfigurationAction, "derivedTypeMapConfigurationAction");
			EntityMappingConfiguration<TDerived> entityMappingConfiguration = new EntityMappingConfiguration<TDerived>();
			DatabaseName tableName = this._entityTypeConfiguration.GetTableName();
			if (tableName != null)
			{
				entityMappingConfiguration.EntityMappingConfigurationInstance.TableName = tableName;
			}
			derivedTypeMapConfigurationAction(entityMappingConfiguration);
			if (typeof(TDerived) == typeof(TEntityType))
			{
				this._entityTypeConfiguration.AddMappingConfiguration(entityMappingConfiguration.EntityMappingConfigurationInstance, true);
			}
			else
			{
				this._entityTypeConfiguration.AddSubTypeMappingConfiguration(typeof(TDerived), entityMappingConfiguration.EntityMappingConfigurationInstance);
			}
			return this;
		}

		// Token: 0x06005D83 RID: 23939 RVA: 0x001940F8 File Offset: 0x001922F8
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public OptionalNavigationPropertyConfiguration<TEntityType, TTargetEntity> HasOptional<TTargetEntity>(Expression<Func<TEntityType, TTargetEntity>> navigationPropertyExpression) where TTargetEntity : class
		{
			Check.NotNull<Expression<Func<TEntityType, TTargetEntity>>>(navigationPropertyExpression, "navigationPropertyExpression");
			return new OptionalNavigationPropertyConfiguration<TEntityType, TTargetEntity>(this._entityTypeConfiguration.Navigation(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>()));
		}

		// Token: 0x06005D84 RID: 23940 RVA: 0x00194121 File Offset: 0x00192321
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public RequiredNavigationPropertyConfiguration<TEntityType, TTargetEntity> HasRequired<TTargetEntity>(Expression<Func<TEntityType, TTargetEntity>> navigationPropertyExpression) where TTargetEntity : class
		{
			Check.NotNull<Expression<Func<TEntityType, TTargetEntity>>>(navigationPropertyExpression, "navigationPropertyExpression");
			return new RequiredNavigationPropertyConfiguration<TEntityType, TTargetEntity>(this._entityTypeConfiguration.Navigation(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>()));
		}

		// Token: 0x06005D85 RID: 23941 RVA: 0x0019414A File Offset: 0x0019234A
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ManyNavigationPropertyConfiguration<TEntityType, TTargetEntity> HasMany<TTargetEntity>(Expression<Func<TEntityType, ICollection<TTargetEntity>>> navigationPropertyExpression) where TTargetEntity : class
		{
			Check.NotNull<Expression<Func<TEntityType, ICollection<TTargetEntity>>>>(navigationPropertyExpression, "navigationPropertyExpression");
			return new ManyNavigationPropertyConfiguration<TEntityType, TTargetEntity>(this._entityTypeConfiguration.Navigation(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>()));
		}

		// Token: 0x06005D86 RID: 23942 RVA: 0x00194173 File Offset: 0x00192373
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005D87 RID: 23943 RVA: 0x0019417B File Offset: 0x0019237B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005D88 RID: 23944 RVA: 0x00194184 File Offset: 0x00192384
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005D89 RID: 23945 RVA: 0x0019418C File Offset: 0x0019238C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040024F4 RID: 9460
		private readonly EntityTypeConfiguration _entityTypeConfiguration;
	}
}
