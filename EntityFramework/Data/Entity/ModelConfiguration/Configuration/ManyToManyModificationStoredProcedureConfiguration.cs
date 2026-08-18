using System;
using System.ComponentModel;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002B9 RID: 697
	public class ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> : ModificationStoredProcedureConfigurationBase where TEntityType : class where TTargetEntityType : class
	{
		// Token: 0x0600186F RID: 6255 RVA: 0x0007A254 File Offset: 0x00078454
		internal ManyToManyModificationStoredProcedureConfiguration()
		{
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0007A25C File Offset: 0x0007845C
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> HasName(string procedureName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			base.Configuration.HasName(procedureName);
			return this;
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0007A277 File Offset: 0x00078477
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> HasName(string procedureName, string schemaName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			Check.NotEmpty(schemaName, "schemaName");
			base.Configuration.HasName(procedureName, schemaName);
			return this;
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0007A29F File Offset: 0x0007849F
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> LeftKeyParameter<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0007A2CE File Offset: 0x000784CE
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> LeftKeyParameter<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0007A2FD File Offset: 0x000784FD
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> LeftKeyParameter(Expression<Func<TEntityType, string>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0007A32C File Offset: 0x0007852C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> LeftKeyParameter(Expression<Func<TEntityType, byte[]>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0007A35B File Offset: 0x0007855B
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> RightKeyParameter<TProperty>(Expression<Func<TTargetEntityType, TProperty>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, true);
			return this;
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0007A38A File Offset: 0x0007858A
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> RightKeyParameter<TProperty>(Expression<Func<TTargetEntityType, TProperty?>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, true);
			return this;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0007A3B9 File Offset: 0x000785B9
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> RightKeyParameter(Expression<Func<TTargetEntityType, string>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, true);
			return this;
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0007A3E8 File Offset: 0x000785E8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> RightKeyParameter(Expression<Func<TTargetEntityType, byte[]>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, true);
			return this;
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0007A417 File Offset: 0x00078617
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x0007A41F File Offset: 0x0007861F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0007A428 File Offset: 0x00078628
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0007A430 File Offset: 0x00078630
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
