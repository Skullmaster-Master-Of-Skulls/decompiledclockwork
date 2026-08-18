using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Spatial;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007A7 RID: 1959
	public abstract class StructuralTypeConfiguration<TStructuralType> where TStructuralType : class
	{
		// Token: 0x06005863 RID: 22627 RVA: 0x0017C2BE File Offset: 0x0017A4BE
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public PrimitivePropertyConfiguration Property<T>(Expression<Func<TStructuralType, T>> propertyExpression) where T : struct
		{
			return new PrimitivePropertyConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005864 RID: 22628 RVA: 0x0017C2CC File Offset: 0x0017A4CC
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public PrimitivePropertyConfiguration Property<T>(Expression<Func<TStructuralType, T?>> propertyExpression) where T : struct
		{
			return new PrimitivePropertyConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005865 RID: 22629 RVA: 0x0017C2DA File Offset: 0x0017A4DA
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public PrimitivePropertyConfiguration Property(Expression<Func<TStructuralType, DbGeometry>> propertyExpression)
		{
			return new PrimitivePropertyConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005866 RID: 22630 RVA: 0x0017C2E8 File Offset: 0x0017A4E8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public PrimitivePropertyConfiguration Property(Expression<Func<TStructuralType, DbGeography>> propertyExpression)
		{
			return new PrimitivePropertyConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x0017C2F6 File Offset: 0x0017A4F6
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public StringPropertyConfiguration Property(Expression<Func<TStructuralType, string>> propertyExpression)
		{
			return new StringPropertyConfiguration(this.Property<StringPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005868 RID: 22632 RVA: 0x0017C304 File Offset: 0x0017A504
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public BinaryPropertyConfiguration Property(Expression<Func<TStructuralType, byte[]>> propertyExpression)
		{
			return new BinaryPropertyConfiguration(this.Property<BinaryPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005869 RID: 22633 RVA: 0x0017C312 File Offset: 0x0017A512
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public DecimalPropertyConfiguration Property(Expression<Func<TStructuralType, decimal>> propertyExpression)
		{
			return new DecimalPropertyConfiguration(this.Property<DecimalPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600586A RID: 22634 RVA: 0x0017C320 File Offset: 0x0017A520
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DecimalPropertyConfiguration Property(Expression<Func<TStructuralType, decimal?>> propertyExpression)
		{
			return new DecimalPropertyConfiguration(this.Property<DecimalPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600586B RID: 22635 RVA: 0x0017C32E File Offset: 0x0017A52E
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DateTimePropertyConfiguration Property(Expression<Func<TStructuralType, DateTime>> propertyExpression)
		{
			return new DateTimePropertyConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600586C RID: 22636 RVA: 0x0017C33C File Offset: 0x0017A53C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public DateTimePropertyConfiguration Property(Expression<Func<TStructuralType, DateTime?>> propertyExpression)
		{
			return new DateTimePropertyConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600586D RID: 22637 RVA: 0x0017C34A File Offset: 0x0017A54A
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public DateTimePropertyConfiguration Property(Expression<Func<TStructuralType, DateTimeOffset>> propertyExpression)
		{
			return new DateTimePropertyConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600586E RID: 22638 RVA: 0x0017C358 File Offset: 0x0017A558
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public DateTimePropertyConfiguration Property(Expression<Func<TStructuralType, DateTimeOffset?>> propertyExpression)
		{
			return new DateTimePropertyConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600586F RID: 22639 RVA: 0x0017C366 File Offset: 0x0017A566
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public DateTimePropertyConfiguration Property(Expression<Func<TStructuralType, TimeSpan>> propertyExpression)
		{
			return new DateTimePropertyConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06005870 RID: 22640 RVA: 0x0017C374 File Offset: 0x0017A574
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public DateTimePropertyConfiguration Property(Expression<Func<TStructuralType, TimeSpan?>> propertyExpression)
		{
			return new DateTimePropertyConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06005871 RID: 22641
		internal abstract StructuralTypeConfiguration Configuration { get; }

		// Token: 0x06005872 RID: 22642
		internal abstract TPrimitivePropertyConfiguration Property<TPrimitivePropertyConfiguration>(LambdaExpression lambdaExpression) where TPrimitivePropertyConfiguration : PrimitivePropertyConfiguration, new();

		// Token: 0x06005873 RID: 22643 RVA: 0x0017C382 File Offset: 0x0017A582
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005874 RID: 22644 RVA: 0x0017C38A File Offset: 0x0017A58A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x0017C393 File Offset: 0x0017A593
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x0017C39B File Offset: 0x0017A59B
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
