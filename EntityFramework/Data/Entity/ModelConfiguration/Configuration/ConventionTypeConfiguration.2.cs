using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001BD RID: 445
	public class ConventionTypeConfiguration<T> where T : class
	{
		// Token: 0x06000F02 RID: 3842 RVA: 0x000409A6 File Offset: 0x0003EBA6
		internal ConventionTypeConfiguration(Type type, Func<EntityTypeConfiguration> entityTypeConfiguration, ModelConfiguration modelConfiguration)
		{
			this._configuration = new ConventionTypeConfiguration(type, entityTypeConfiguration, modelConfiguration);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x000409BC File Offset: 0x0003EBBC
		internal ConventionTypeConfiguration(Type type, Func<ComplexTypeConfiguration> complexTypeConfiguration, ModelConfiguration modelConfiguration)
		{
			this._configuration = new ConventionTypeConfiguration(type, complexTypeConfiguration, modelConfiguration);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x000409D2 File Offset: 0x0003EBD2
		internal ConventionTypeConfiguration(Type type, ModelConfiguration modelConfiguration)
		{
			this._configuration = new ConventionTypeConfiguration(type, modelConfiguration);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x000409E7 File Offset: 0x0003EBE7
		[Conditional("DEBUG")]
		private static void VerifyType(Type type)
		{
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x000409E9 File Offset: 0x0003EBE9
		public Type ClrType
		{
			get
			{
				return this._configuration.ClrType;
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x000409F6 File Offset: 0x0003EBF6
		public ConventionTypeConfiguration<T> HasEntitySetName(string entitySetName)
		{
			this._configuration.HasEntitySetName(entitySetName);
			return this;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00040A06 File Offset: 0x0003EC06
		public ConventionTypeConfiguration<T> Ignore()
		{
			this._configuration.Ignore();
			return this;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00040A15 File Offset: 0x0003EC15
		public ConventionTypeConfiguration<T> IsComplexType()
		{
			this._configuration.IsComplexType();
			return this;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00040A24 File Offset: 0x0003EC24
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ConventionTypeConfiguration<T> Ignore<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
		{
			Check.NotNull<Expression<Func<T, TProperty>>>(propertyExpression, "propertyExpression");
			this._configuration.Ignore(propertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>());
			return this;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00040A4A File Offset: 0x0003EC4A
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ConventionPrimitivePropertyConfiguration Property<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
		{
			Check.NotNull<Expression<Func<T, TProperty>>>(propertyExpression, "propertyExpression");
			return this._configuration.Property(propertyExpression.GetComplexPropertyAccess());
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00040A69 File Offset: 0x0003EC69
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal ConventionNavigationPropertyConfiguration NavigationProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
		{
			Check.NotNull<Expression<Func<T, TProperty>>>(propertyExpression, "propertyExpression");
			return this._configuration.NavigationProperty(propertyExpression.GetComplexPropertyAccess());
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00040A90 File Offset: 0x0003EC90
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ConventionTypeConfiguration<T> HasKey<TProperty>(Expression<Func<T, TProperty>> keyExpression)
		{
			Check.NotNull<Expression<Func<T, TProperty>>>(keyExpression, "keyExpression");
			this._configuration.HasKey(from p in keyExpression.GetSimplePropertyAccessList()
			select p.Single<PropertyInfo>());
			return this;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00040AC2 File Offset: 0x0003ECC2
		public ConventionTypeConfiguration<T> ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			this._configuration.ToTable(tableName);
			return this;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00040ADE File Offset: 0x0003ECDE
		public ConventionTypeConfiguration<T> ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this._configuration.ToTable(tableName, schemaName);
			return this;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00040AFB File Offset: 0x0003ECFB
		public ConventionTypeConfiguration<T> HasTableAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			this._configuration.HasTableAnnotation(name, value);
			return this;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00040B18 File Offset: 0x0003ED18
		public ConventionTypeConfiguration<T> MapToStoredProcedures()
		{
			this._configuration.MapToStoredProcedures();
			return this;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00040B28 File Offset: 0x0003ED28
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ConventionTypeConfiguration<T> MapToStoredProcedures(Action<ModificationStoredProceduresConfiguration<T>> modificationStoredProceduresConfigurationAction)
		{
			Check.NotNull<Action<ModificationStoredProceduresConfiguration<T>>>(modificationStoredProceduresConfigurationAction, "modificationStoredProceduresConfigurationAction");
			ModificationStoredProceduresConfiguration<T> modificationStoredProceduresConfiguration = new ModificationStoredProceduresConfiguration<T>();
			modificationStoredProceduresConfigurationAction(modificationStoredProceduresConfiguration);
			this._configuration.MapToStoredProcedures(modificationStoredProceduresConfiguration.Configuration);
			return this;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00040B60 File Offset: 0x0003ED60
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00040B68 File Offset: 0x0003ED68
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00040B71 File Offset: 0x0003ED71
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00040B79 File Offset: 0x0003ED79
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000418 RID: 1048
		private readonly ConventionTypeConfiguration _configuration;
	}
}
