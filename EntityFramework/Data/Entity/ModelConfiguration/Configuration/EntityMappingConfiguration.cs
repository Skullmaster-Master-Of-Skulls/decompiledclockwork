using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Mapping;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007B0 RID: 1968
	public class EntityMappingConfiguration<TEntityType> where TEntityType : class
	{
		// Token: 0x06005913 RID: 22803 RVA: 0x0017F5DC File Offset: 0x0017D7DC
		public EntityMappingConfiguration() : this(new EntityMappingConfiguration())
		{
		}

		// Token: 0x06005914 RID: 22804 RVA: 0x0017F5E9 File Offset: 0x0017D7E9
		internal EntityMappingConfiguration(EntityMappingConfiguration entityMappingConfiguration)
		{
			this._entityMappingConfiguration = entityMappingConfiguration;
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06005915 RID: 22805 RVA: 0x0017F5F8 File Offset: 0x0017D7F8
		internal EntityMappingConfiguration EntityMappingConfigurationInstance
		{
			get
			{
				return this._entityMappingConfiguration;
			}
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x0017F600 File Offset: 0x0017D800
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public void Properties<TObject>(Expression<Func<TEntityType, TObject>> propertiesExpression)
		{
			Check.NotNull<Expression<Func<TEntityType, TObject>>>(propertiesExpression, "propertiesExpression");
			this._entityMappingConfiguration.Properties = propertiesExpression.GetComplexPropertyAccessList().ToList<PropertyPath>();
		}

		// Token: 0x06005917 RID: 22807 RVA: 0x0017F624 File Offset: 0x0017D824
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public PropertyMappingConfiguration Property<T>(Expression<Func<TEntityType, T>> propertyExpression) where T : struct
		{
			return new PropertyMappingConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005918 RID: 22808 RVA: 0x0017F632 File Offset: 0x0017D832
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public PropertyMappingConfiguration Property<T>(Expression<Func<TEntityType, T?>> propertyExpression) where T : struct
		{
			return new PropertyMappingConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005919 RID: 22809 RVA: 0x0017F640 File Offset: 0x0017D840
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, DbGeometry>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600591A RID: 22810 RVA: 0x0017F64E File Offset: 0x0017D84E
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, DbGeography>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600591B RID: 22811 RVA: 0x0017F65C File Offset: 0x0017D85C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, string>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<StringPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600591C RID: 22812 RVA: 0x0017F66A File Offset: 0x0017D86A
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, byte[]>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<BinaryPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600591D RID: 22813 RVA: 0x0017F678 File Offset: 0x0017D878
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, decimal>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DecimalPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600591E RID: 22814 RVA: 0x0017F686 File Offset: 0x0017D886
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, decimal?>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DecimalPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x0017F694 File Offset: 0x0017D894
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, DateTime>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005920 RID: 22816 RVA: 0x0017F6A2 File Offset: 0x0017D8A2
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, DateTime?>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x0017F6B0 File Offset: 0x0017D8B0
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, DateTimeOffset>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x0017F6BE File Offset: 0x0017D8BE
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, DateTimeOffset?>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x0017F6CC File Offset: 0x0017D8CC
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, TimeSpan>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005924 RID: 22820 RVA: 0x0017F6DA File Offset: 0x0017D8DA
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, TimeSpan?>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x0017F70A File Offset: 0x0017D90A
		internal TPrimitivePropertyConfiguration Property<TPrimitivePropertyConfiguration>(LambdaExpression lambdaExpression) where TPrimitivePropertyConfiguration : PrimitivePropertyConfiguration, new()
		{
			return this._entityMappingConfiguration.Property<TPrimitivePropertyConfiguration>(lambdaExpression.GetComplexPropertyAccess(), delegate()
			{
				TPrimitivePropertyConfiguration result = Activator.CreateInstance<TPrimitivePropertyConfiguration>();
				result.OverridableConfigurationParts = OverridableConfigurationParts.None;
				return result;
			});
		}

		// Token: 0x06005926 RID: 22822 RVA: 0x0017F729 File Offset: 0x0017D929
		public EntityMappingConfiguration<TEntityType> MapInheritedProperties()
		{
			this._entityMappingConfiguration.MapInheritedProperties = true;
			return this;
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x0017F738 File Offset: 0x0017D938
		public EntityMappingConfiguration<TEntityType> ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			DatabaseName databaseName = DatabaseName.Parse(tableName);
			this.ToTable(databaseName.Name, databaseName.Schema);
			return this;
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x0017F76C File Offset: 0x0017D96C
		public EntityMappingConfiguration<TEntityType> ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this._entityMappingConfiguration.TableName = new DatabaseName(tableName, schemaName);
			return this;
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x0017F78D File Offset: 0x0017D98D
		public EntityMappingConfiguration<TEntityType> HasTableAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			this._entityMappingConfiguration.SetAnnotation(name, value);
			return this;
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x0017F7A9 File Offset: 0x0017D9A9
		public ValueConditionConfiguration Requires(string discriminator)
		{
			Check.NotEmpty(discriminator, "discriminator");
			return new ValueConditionConfiguration(this._entityMappingConfiguration, discriminator);
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x0017F7C3 File Offset: 0x0017D9C3
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public NotNullConditionConfiguration Requires<TProperty>(Expression<Func<TEntityType, TProperty>> property)
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(property, "property");
			return new NotNullConditionConfiguration(this._entityMappingConfiguration, property.GetComplexPropertyAccess());
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x0017F7E2 File Offset: 0x0017D9E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x0017F7EA File Offset: 0x0017D9EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x0017F7F3 File Offset: 0x0017D9F3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x0017F7FB File Offset: 0x0017D9FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040023AB RID: 9131
		private readonly EntityMappingConfiguration _entityMappingConfiguration;
	}
}
