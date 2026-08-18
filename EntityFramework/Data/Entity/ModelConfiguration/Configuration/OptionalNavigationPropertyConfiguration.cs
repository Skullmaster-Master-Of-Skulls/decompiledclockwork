using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007C5 RID: 1989
	public class OptionalNavigationPropertyConfiguration<TEntityType, TTargetEntityType> where TEntityType : class where TTargetEntityType : class
	{
		// Token: 0x06005A51 RID: 23121 RVA: 0x001859AB File Offset: 0x00183BAB
		internal OptionalNavigationPropertyConfiguration(NavigationPropertyConfiguration navigationPropertyConfiguration)
		{
			navigationPropertyConfiguration.Reset();
			this._navigationPropertyConfiguration = navigationPropertyConfiguration;
			this._navigationPropertyConfiguration.RelationshipMultiplicity = new RelationshipMultiplicity?(RelationshipMultiplicity.ZeroOrOne);
		}

		// Token: 0x06005A52 RID: 23122 RVA: 0x001859D1 File Offset: 0x00183BD1
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DependentNavigationPropertyConfiguration<TEntityType> WithMany(Expression<Func<TTargetEntityType, ICollection<TEntityType>>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, ICollection<TEntityType>>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithMany();
		}

		// Token: 0x06005A53 RID: 23123 RVA: 0x001859FB File Offset: 0x00183BFB
		public DependentNavigationPropertyConfiguration<TEntityType> WithMany()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.Many);
			return new DependentNavigationPropertyConfiguration<TEntityType>(this._navigationPropertyConfiguration);
		}

		// Token: 0x06005A54 RID: 23124 RVA: 0x00185A19 File Offset: 0x00183C19
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public ForeignKeyNavigationPropertyConfiguration WithRequired(Expression<Func<TTargetEntityType, TEntityType>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithRequired();
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x00185A43 File Offset: 0x00183C43
		public ForeignKeyNavigationPropertyConfiguration WithRequired()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.One);
			return new ForeignKeyNavigationPropertyConfiguration(this._navigationPropertyConfiguration);
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x00185A61 File Offset: 0x00183C61
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ForeignKeyNavigationPropertyConfiguration WithOptionalDependent(Expression<Func<TTargetEntityType, TEntityType>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithOptionalDependent();
		}

		// Token: 0x06005A57 RID: 23127 RVA: 0x00185A8B File Offset: 0x00183C8B
		public ForeignKeyNavigationPropertyConfiguration WithOptionalDependent()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.ZeroOrOne);
			this._navigationPropertyConfiguration.Constraint = IndependentConstraintConfiguration.Instance;
			this._navigationPropertyConfiguration.IsNavigationPropertyDeclaringTypePrincipal = new bool?(false);
			return new ForeignKeyNavigationPropertyConfiguration(this._navigationPropertyConfiguration);
		}

		// Token: 0x06005A58 RID: 23128 RVA: 0x00185ACA File Offset: 0x00183CCA
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public ForeignKeyNavigationPropertyConfiguration WithOptionalPrincipal(Expression<Func<TTargetEntityType, TEntityType>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithOptionalPrincipal();
		}

		// Token: 0x06005A59 RID: 23129 RVA: 0x00185AF4 File Offset: 0x00183CF4
		public ForeignKeyNavigationPropertyConfiguration WithOptionalPrincipal()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.ZeroOrOne);
			this._navigationPropertyConfiguration.Constraint = IndependentConstraintConfiguration.Instance;
			this._navigationPropertyConfiguration.IsNavigationPropertyDeclaringTypePrincipal = new bool?(true);
			return new ForeignKeyNavigationPropertyConfiguration(this._navigationPropertyConfiguration);
		}

		// Token: 0x06005A5A RID: 23130 RVA: 0x00185B33 File Offset: 0x00183D33
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005A5B RID: 23131 RVA: 0x00185B3B File Offset: 0x00183D3B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005A5C RID: 23132 RVA: 0x00185B44 File Offset: 0x00183D44
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005A5D RID: 23133 RVA: 0x00185B4C File Offset: 0x00183D4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002411 RID: 9233
		private readonly NavigationPropertyConfiguration _navigationPropertyConfiguration;
	}
}
