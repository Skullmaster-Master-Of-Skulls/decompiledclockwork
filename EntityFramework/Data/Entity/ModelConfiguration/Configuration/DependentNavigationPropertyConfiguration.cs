using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007CD RID: 1997
	public class DependentNavigationPropertyConfiguration<TDependentEntityType> : ForeignKeyNavigationPropertyConfiguration where TDependentEntityType : class
	{
		// Token: 0x06005AA1 RID: 23201 RVA: 0x00186790 File Offset: 0x00184990
		internal DependentNavigationPropertyConfiguration(NavigationPropertyConfiguration navigationPropertyConfiguration) : base(navigationPropertyConfiguration)
		{
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x001867A1 File Offset: 0x001849A1
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public CascadableNavigationPropertyConfiguration HasForeignKey<TKey>(Expression<Func<TDependentEntityType, TKey>> foreignKeyExpression)
		{
			Check.NotNull<Expression<Func<TDependentEntityType, TKey>>>(foreignKeyExpression, "foreignKeyExpression");
			base.NavigationPropertyConfiguration.Constraint = new ForeignKeyConstraintConfiguration(from p in foreignKeyExpression.GetSimplePropertyAccessList()
			select p.Single<PropertyInfo>());
			return this;
		}

		// Token: 0x06005AA3 RID: 23203 RVA: 0x001867D7 File Offset: 0x001849D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x001867DF File Offset: 0x001849DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x001867E8 File Offset: 0x001849E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005AA6 RID: 23206 RVA: 0x001867F0 File Offset: 0x001849F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
