using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration
{
	// Token: 0x020007A8 RID: 1960
	public class ComplexTypeConfiguration<TComplexType> : StructuralTypeConfiguration<TComplexType> where TComplexType : class
	{
		// Token: 0x06005878 RID: 22648 RVA: 0x0017C3AB File Offset: 0x0017A5AB
		public ComplexTypeConfiguration() : this(new ComplexTypeConfiguration(typeof(TComplexType)))
		{
		}

		// Token: 0x06005879 RID: 22649 RVA: 0x0017C3C2 File Offset: 0x0017A5C2
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public ComplexTypeConfiguration<TComplexType> Ignore<TProperty>(Expression<Func<TComplexType, TProperty>> propertyExpression)
		{
			Check.NotNull<Expression<Func<TComplexType, TProperty>>>(propertyExpression, "propertyExpression");
			this.Configuration.Ignore(propertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>());
			return this;
		}

		// Token: 0x0600587A RID: 22650 RVA: 0x0017C3E7 File Offset: 0x0017A5E7
		internal ComplexTypeConfiguration(ComplexTypeConfiguration configuration)
		{
			this._complexTypeConfiguration = configuration;
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x0600587B RID: 22651 RVA: 0x0017C3F6 File Offset: 0x0017A5F6
		internal override StructuralTypeConfiguration Configuration
		{
			get
			{
				return this._complexTypeConfiguration;
			}
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x0017C422 File Offset: 0x0017A622
		internal override TPrimitivePropertyConfiguration Property<TPrimitivePropertyConfiguration>(LambdaExpression lambdaExpression)
		{
			return this.Configuration.Property<TPrimitivePropertyConfiguration>(lambdaExpression.GetSimplePropertyAccess(), delegate()
			{
				TPrimitivePropertyConfiguration result = Activator.CreateInstance<TPrimitivePropertyConfiguration>();
				result.OverridableConfigurationParts = OverridableConfigurationParts.OverridableInSSpace;
				return result;
			});
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x0017C441 File Offset: 0x0017A641
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600587E RID: 22654 RVA: 0x0017C449 File Offset: 0x0017A649
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600587F RID: 22655 RVA: 0x0017C452 File Offset: 0x0017A652
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005880 RID: 22656 RVA: 0x0017C45A File Offset: 0x0017A65A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002381 RID: 9089
		private readonly ComplexTypeConfiguration _complexTypeConfiguration;
	}
}
