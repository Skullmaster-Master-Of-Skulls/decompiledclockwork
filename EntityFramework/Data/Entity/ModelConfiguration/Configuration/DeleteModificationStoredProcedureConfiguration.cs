using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002BB RID: 699
	public class DeleteModificationStoredProcedureConfiguration<TEntityType> : ModificationStoredProcedureConfigurationBase where TEntityType : class
	{
		// Token: 0x06001886 RID: 6278 RVA: 0x0007A4E5 File Offset: 0x000786E5
		internal DeleteModificationStoredProcedureConfiguration()
		{
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0007A4ED File Offset: 0x000786ED
		public DeleteModificationStoredProcedureConfiguration<TEntityType> HasName(string procedureName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			base.Configuration.HasName(procedureName);
			return this;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0007A508 File Offset: 0x00078708
		public DeleteModificationStoredProcedureConfiguration<TEntityType> HasName(string procedureName, string schemaName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			Check.NotEmpty(schemaName, "schemaName");
			base.Configuration.HasName(procedureName, schemaName);
			return this;
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0007A530 File Offset: 0x00078730
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0007A55F File Offset: 0x0007875F
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0007A58E File Offset: 0x0007878E
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, string>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0007A5BD File Offset: 0x000787BD
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, byte[]>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0007A5EC File Offset: 0x000787EC
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeography>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeography>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0007A61B File Offset: 0x0007881B
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeometry>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeometry>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0007A64A File Offset: 0x0007884A
		public DeleteModificationStoredProcedureConfiguration<TEntityType> RowsAffectedParameter(string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.RowsAffectedParameter(parameterName);
			return this;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0007A668 File Offset: 0x00078868
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Navigation<TPrincipalEntityType>(Expression<Func<TPrincipalEntityType, TEntityType>> navigationPropertyExpression, Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>> associationModificationStoredProcedureConfigurationAction) where TPrincipalEntityType : class
		{
			Check.NotNull<Expression<Func<TPrincipalEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			Check.NotNull<Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>>>(associationModificationStoredProcedureConfigurationAction, "associationModificationStoredProcedureConfigurationAction");
			AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType> obj = new AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>(), base.Configuration);
			associationModificationStoredProcedureConfigurationAction(obj);
			return this;
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0007A6AC File Offset: 0x000788AC
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Navigation<TPrincipalEntityType>(Expression<Func<TPrincipalEntityType, ICollection<TEntityType>>> navigationPropertyExpression, Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>> associationModificationStoredProcedureConfigurationAction) where TPrincipalEntityType : class
		{
			Check.NotNull<Expression<Func<TPrincipalEntityType, ICollection<TEntityType>>>>(navigationPropertyExpression, "navigationPropertyExpression");
			Check.NotNull<Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>>>(associationModificationStoredProcedureConfigurationAction, "associationModificationStoredProcedureConfigurationAction");
			AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType> obj = new AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>(), base.Configuration);
			associationModificationStoredProcedureConfigurationAction(obj);
			return this;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0007A6F0 File Offset: 0x000788F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0007A6F8 File Offset: 0x000788F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0007A701 File Offset: 0x00078901
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0007A709 File Offset: 0x00078909
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
