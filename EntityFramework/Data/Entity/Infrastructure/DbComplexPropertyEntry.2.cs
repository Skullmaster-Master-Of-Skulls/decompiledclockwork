using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000749 RID: 1865
	public class DbComplexPropertyEntry<TEntity, TComplexProperty> : DbPropertyEntry<TEntity, TComplexProperty> where TEntity : class
	{
		// Token: 0x06005466 RID: 21606 RVA: 0x00171298 File Offset: 0x0016F498
		internal new static DbComplexPropertyEntry<TEntity, TComplexProperty> Create(InternalPropertyEntry internalPropertyEntry)
		{
			return (DbComplexPropertyEntry<TEntity, TComplexProperty>)internalPropertyEntry.CreateDbMemberEntry<TEntity, TComplexProperty>();
		}

		// Token: 0x06005467 RID: 21607 RVA: 0x001712A5 File Offset: 0x0016F4A5
		internal DbComplexPropertyEntry(InternalPropertyEntry internalPropertyEntry) : base(internalPropertyEntry)
		{
		}

		// Token: 0x06005468 RID: 21608 RVA: 0x001712AE File Offset: 0x0016F4AE
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Intentionally just implicit to reduce API clutter.")]
		public static implicit operator DbComplexPropertyEntry(DbComplexPropertyEntry<TEntity, TComplexProperty> entry)
		{
			return DbComplexPropertyEntry.Create(entry.InternalPropertyEntry);
		}

		// Token: 0x06005469 RID: 21609 RVA: 0x001712BB File Offset: 0x0016F4BB
		public DbPropertyEntry Property(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbPropertyEntry.Create(base.InternalPropertyEntry.Property(propertyName, null, false));
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x001712DC File Offset: 0x0016F4DC
		public DbPropertyEntry<TEntity, TNestedProperty> Property<TNestedProperty>(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbPropertyEntry<TEntity, TNestedProperty>.Create(base.InternalPropertyEntry.Property(propertyName, typeof(TNestedProperty), false));
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x00171306 File Offset: 0x0016F506
		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#", Justification = "Rule predates more fluent naming conventions.")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DbPropertyEntry<TEntity, TNestedProperty> Property<TNestedProperty>(Expression<Func<TComplexProperty, TNestedProperty>> property)
		{
			Check.NotNull<Expression<Func<TComplexProperty, TNestedProperty>>>(property, "property");
			return this.Property<TNestedProperty>(DbHelpers.ParsePropertySelector<TComplexProperty, TNestedProperty>(property, "Property", "property"));
		}

		// Token: 0x0600546C RID: 21612 RVA: 0x0017132A File Offset: 0x0016F52A
		public DbComplexPropertyEntry ComplexProperty(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbComplexPropertyEntry.Create(base.InternalPropertyEntry.Property(propertyName, null, true));
		}

		// Token: 0x0600546D RID: 21613 RVA: 0x0017134B File Offset: 0x0016F54B
		public DbComplexPropertyEntry<TEntity, TNestedComplexProperty> ComplexProperty<TNestedComplexProperty>(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbComplexPropertyEntry<TEntity, TNestedComplexProperty>.Create(base.InternalPropertyEntry.Property(propertyName, typeof(TNestedComplexProperty), true));
		}

		// Token: 0x0600546E RID: 21614 RVA: 0x00171375 File Offset: 0x0016F575
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#", Justification = "Rule predates more fluent naming conventions.")]
		public DbComplexPropertyEntry<TEntity, TNestedComplexProperty> ComplexProperty<TNestedComplexProperty>(Expression<Func<TComplexProperty, TNestedComplexProperty>> property)
		{
			Check.NotNull<Expression<Func<TComplexProperty, TNestedComplexProperty>>>(property, "property");
			return this.ComplexProperty<TNestedComplexProperty>(DbHelpers.ParsePropertySelector<TComplexProperty, TNestedComplexProperty>(property, "Property", "property"));
		}
	}
}
