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
	// Token: 0x020002BD RID: 701
	public class UpdateModificationStoredProcedureConfiguration<TEntityType> : ModificationStoredProcedureConfigurationBase where TEntityType : class
	{
		// Token: 0x060018AB RID: 6315 RVA: 0x0007AA2D File Offset: 0x00078C2D
		internal UpdateModificationStoredProcedureConfiguration()
		{
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x0007AA35 File Offset: 0x00078C35
		public UpdateModificationStoredProcedureConfiguration<TEntityType> HasName(string procedureName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			base.Configuration.HasName(procedureName);
			return this;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0007AA50 File Offset: 0x00078C50
		public UpdateModificationStoredProcedureConfiguration<TEntityType> HasName(string procedureName, string schemaName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			Check.NotEmpty(schemaName, "schemaName");
			base.Configuration.HasName(procedureName, schemaName);
			return this;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0007AA78 File Offset: 0x00078C78
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0007AAA7 File Offset: 0x00078CA7
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0007AAD6 File Offset: 0x00078CD6
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, string>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0007AB05 File Offset: 0x00078D05
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, byte[]>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0007AB34 File Offset: 0x00078D34
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeography>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeography>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0007AB63 File Offset: 0x00078D63
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeometry>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeometry>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0007AB92 File Offset: 0x00078D92
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string currentValueParameterName, string originalValueParameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), currentValueParameterName, originalValueParameterName, false);
			return this;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0007ABCD File Offset: 0x00078DCD
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string currentValueParameterName, string originalValueParameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), currentValueParameterName, originalValueParameterName, false);
			return this;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0007AC08 File Offset: 0x00078E08
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, string>> propertyExpression, string currentValueParameterName, string originalValueParameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), currentValueParameterName, originalValueParameterName, false);
			return this;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0007AC43 File Offset: 0x00078E43
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, byte[]>> propertyExpression, string currentValueParameterName, string originalValueParameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), currentValueParameterName, originalValueParameterName, false);
			return this;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0007AC7E File Offset: 0x00078E7E
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeography>> propertyExpression, string currentValueParameterName, string originalValueParameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeography>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), currentValueParameterName, originalValueParameterName, false);
			return this;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0007ACB9 File Offset: 0x00078EB9
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeometry>> propertyExpression, string currentValueParameterName, string originalValueParameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeometry>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), currentValueParameterName, originalValueParameterName, false);
			return this;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0007ACF4 File Offset: 0x00078EF4
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Result<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string columnName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0007AD21 File Offset: 0x00078F21
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Result<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string columnName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0007AD4E File Offset: 0x00078F4E
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Result(Expression<Func<TEntityType, string>> propertyExpression, string columnName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0007AD7B File Offset: 0x00078F7B
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Result(Expression<Func<TEntityType, byte[]>> propertyExpression, string columnName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0007ADA8 File Offset: 0x00078FA8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Result(Expression<Func<TEntityType, DbGeography>> propertyExpression, string columnName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeography>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0007ADD5 File Offset: 0x00078FD5
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Result(Expression<Func<TEntityType, DbGeometry>> propertyExpression, string columnName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeometry>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0007AE02 File Offset: 0x00079002
		public UpdateModificationStoredProcedureConfiguration<TEntityType> RowsAffectedParameter(string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.RowsAffectedParameter(parameterName);
			return this;
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0007AE20 File Offset: 0x00079020
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Navigation<TPrincipalEntityType>(Expression<Func<TPrincipalEntityType, TEntityType>> navigationPropertyExpression, Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>> associationModificationStoredProcedureConfigurationAction) where TPrincipalEntityType : class
		{
			Check.NotNull<Expression<Func<TPrincipalEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			Check.NotNull<Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>>>(associationModificationStoredProcedureConfigurationAction, "associationModificationStoredProcedureConfigurationAction");
			AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType> obj = new AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>(), base.Configuration);
			associationModificationStoredProcedureConfigurationAction(obj);
			return this;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0007AE64 File Offset: 0x00079064
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Navigation<TPrincipalEntityType>(Expression<Func<TPrincipalEntityType, ICollection<TEntityType>>> navigationPropertyExpression, Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>> associationModificationStoredProcedureConfigurationAction) where TPrincipalEntityType : class
		{
			Check.NotNull<Expression<Func<TPrincipalEntityType, ICollection<TEntityType>>>>(navigationPropertyExpression, "navigationPropertyExpression");
			Check.NotNull<Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>>>(associationModificationStoredProcedureConfigurationAction, "associationModificationStoredProcedureConfigurationAction");
			AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType> obj = new AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>(), base.Configuration);
			associationModificationStoredProcedureConfigurationAction(obj);
			return this;
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0007AEA8 File Offset: 0x000790A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0007AEB0 File Offset: 0x000790B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0007AEB9 File Offset: 0x000790B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0007AEC1 File Offset: 0x000790C1
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
