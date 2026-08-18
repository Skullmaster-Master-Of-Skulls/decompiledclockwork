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
	// Token: 0x020007C4 RID: 1988
	public class ManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> where TEntityType : class where TTargetEntityType : class
	{
		// Token: 0x06005A46 RID: 23110 RVA: 0x0018588C File Offset: 0x00183A8C
		internal ManyNavigationPropertyConfiguration(NavigationPropertyConfiguration navigationPropertyConfiguration)
		{
			navigationPropertyConfiguration.Reset();
			this._navigationPropertyConfiguration = navigationPropertyConfiguration;
			this._navigationPropertyConfiguration.RelationshipMultiplicity = new RelationshipMultiplicity?(RelationshipMultiplicity.Many);
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x001858B2 File Offset: 0x00183AB2
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> WithMany(Expression<Func<TTargetEntityType, ICollection<TEntityType>>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, ICollection<TEntityType>>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithMany();
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x001858DC File Offset: 0x00183ADC
		public ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> WithMany()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.Many);
			return new ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType>(this._navigationPropertyConfiguration);
		}

		// Token: 0x06005A49 RID: 23113 RVA: 0x001858FA File Offset: 0x00183AFA
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DependentNavigationPropertyConfiguration<TTargetEntityType> WithRequired(Expression<Func<TTargetEntityType, TEntityType>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithRequired();
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x00185924 File Offset: 0x00183B24
		public DependentNavigationPropertyConfiguration<TTargetEntityType> WithRequired()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.One);
			return new DependentNavigationPropertyConfiguration<TTargetEntityType>(this._navigationPropertyConfiguration);
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x00185942 File Offset: 0x00183B42
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DependentNavigationPropertyConfiguration<TTargetEntityType> WithOptional(Expression<Func<TTargetEntityType, TEntityType>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithOptional();
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x0018596C File Offset: 0x00183B6C
		public DependentNavigationPropertyConfiguration<TTargetEntityType> WithOptional()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.ZeroOrOne);
			return new DependentNavigationPropertyConfiguration<TTargetEntityType>(this._navigationPropertyConfiguration);
		}

		// Token: 0x06005A4D RID: 23117 RVA: 0x0018598A File Offset: 0x00183B8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005A4E RID: 23118 RVA: 0x00185992 File Offset: 0x00183B92
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005A4F RID: 23119 RVA: 0x0018599B File Offset: 0x00183B9B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005A50 RID: 23120 RVA: 0x001859A3 File Offset: 0x00183BA3
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002410 RID: 9232
		private readonly NavigationPropertyConfiguration _navigationPropertyConfiguration;
	}
}
