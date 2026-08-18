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
	// Token: 0x020007C6 RID: 1990
	public class RequiredNavigationPropertyConfiguration<TEntityType, TTargetEntityType> where TEntityType : class where TTargetEntityType : class
	{
		// Token: 0x06005A5E RID: 23134 RVA: 0x00185B54 File Offset: 0x00183D54
		internal RequiredNavigationPropertyConfiguration(NavigationPropertyConfiguration navigationPropertyConfiguration)
		{
			navigationPropertyConfiguration.Reset();
			this._navigationPropertyConfiguration = navigationPropertyConfiguration;
			this._navigationPropertyConfiguration.RelationshipMultiplicity = new RelationshipMultiplicity?(RelationshipMultiplicity.One);
		}

		// Token: 0x06005A5F RID: 23135 RVA: 0x00185B7A File Offset: 0x00183D7A
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public DependentNavigationPropertyConfiguration<TEntityType> WithMany(Expression<Func<TTargetEntityType, ICollection<TEntityType>>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, ICollection<TEntityType>>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithMany();
		}

		// Token: 0x06005A60 RID: 23136 RVA: 0x00185BA4 File Offset: 0x00183DA4
		public DependentNavigationPropertyConfiguration<TEntityType> WithMany()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.Many);
			return new DependentNavigationPropertyConfiguration<TEntityType>(this._navigationPropertyConfiguration);
		}

		// Token: 0x06005A61 RID: 23137 RVA: 0x00185BC2 File Offset: 0x00183DC2
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public ForeignKeyNavigationPropertyConfiguration WithOptional(Expression<Func<TTargetEntityType, TEntityType>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithOptional();
		}

		// Token: 0x06005A62 RID: 23138 RVA: 0x00185BEC File Offset: 0x00183DEC
		public ForeignKeyNavigationPropertyConfiguration WithOptional()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.ZeroOrOne);
			return new ForeignKeyNavigationPropertyConfiguration(this._navigationPropertyConfiguration);
		}

		// Token: 0x06005A63 RID: 23139 RVA: 0x00185C0A File Offset: 0x00183E0A
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ForeignKeyNavigationPropertyConfiguration WithRequiredDependent(Expression<Func<TTargetEntityType, TEntityType>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithRequiredDependent();
		}

		// Token: 0x06005A64 RID: 23140 RVA: 0x00185C34 File Offset: 0x00183E34
		public ForeignKeyNavigationPropertyConfiguration WithRequiredDependent()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.One);
			this._navigationPropertyConfiguration.IsNavigationPropertyDeclaringTypePrincipal = new bool?(false);
			return new ForeignKeyNavigationPropertyConfiguration(this._navigationPropertyConfiguration);
		}

		// Token: 0x06005A65 RID: 23141 RVA: 0x00185C63 File Offset: 0x00183E63
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public ForeignKeyNavigationPropertyConfiguration WithRequiredPrincipal(Expression<Func<TTargetEntityType, TEntityType>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithRequiredPrincipal();
		}

		// Token: 0x06005A66 RID: 23142 RVA: 0x00185C8D File Offset: 0x00183E8D
		public ForeignKeyNavigationPropertyConfiguration WithRequiredPrincipal()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.One);
			this._navigationPropertyConfiguration.IsNavigationPropertyDeclaringTypePrincipal = new bool?(true);
			return new ForeignKeyNavigationPropertyConfiguration(this._navigationPropertyConfiguration);
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x00185CBC File Offset: 0x00183EBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x00185CC4 File Offset: 0x00183EC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x00185CCD File Offset: 0x00183ECD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x00185CD5 File Offset: 0x00183ED5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002412 RID: 9234
		private readonly NavigationPropertyConfiguration _navigationPropertyConfiguration;
	}
}
