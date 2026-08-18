using System;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002B2 RID: 690
	public class AssociationModificationStoredProcedureConfiguration<TEntityType> where TEntityType : class
	{
		// Token: 0x0600183A RID: 6202 RVA: 0x00079BD2 File Offset: 0x00077DD2
		internal AssociationModificationStoredProcedureConfiguration(PropertyInfo navigationPropertyInfo, ModificationStoredProcedureConfiguration configuration)
		{
			this._navigationPropertyInfo = navigationPropertyInfo;
			this._configuration = configuration;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00079BE8 File Offset: 0x00077DE8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public AssociationModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			this._configuration.Parameter(new PropertyPath(new PropertyInfo[]
			{
				this._navigationPropertyInfo
			}.Concat(propertyExpression.GetSimplePropertyAccess())), parameterName, null, false);
			return this;
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00079C40 File Offset: 0x00077E40
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public AssociationModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			this._configuration.Parameter(new PropertyPath(new PropertyInfo[]
			{
				this._navigationPropertyInfo
			}.Concat(propertyExpression.GetSimplePropertyAccess())), parameterName, null, false);
			return this;
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00079C98 File Offset: 0x00077E98
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public AssociationModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, string>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			this._configuration.Parameter(new PropertyPath(new PropertyInfo[]
			{
				this._navigationPropertyInfo
			}.Concat(propertyExpression.GetSimplePropertyAccess())), parameterName, null, false);
			return this;
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00079CF0 File Offset: 0x00077EF0
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public AssociationModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, byte[]>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			this._configuration.Parameter(new PropertyPath(new PropertyInfo[]
			{
				this._navigationPropertyInfo
			}.Concat(propertyExpression.GetSimplePropertyAccess())), parameterName, null, false);
			return this;
		}

		// Token: 0x04000877 RID: 2167
		private readonly PropertyInfo _navigationPropertyInfo;

		// Token: 0x04000878 RID: 2168
		private readonly ModificationStoredProcedureConfiguration _configuration;
	}
}
