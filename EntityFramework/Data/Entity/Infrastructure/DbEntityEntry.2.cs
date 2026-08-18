using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200074E RID: 1870
	public class DbEntityEntry<TEntity> where TEntity : class
	{
		// Token: 0x060054B8 RID: 21688 RVA: 0x00171EB2 File Offset: 0x001700B2
		internal DbEntityEntry(InternalEntityEntry internalEntityEntry)
		{
			this._internalEntityEntry = internalEntityEntry;
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x060054B9 RID: 21689 RVA: 0x00171EC1 File Offset: 0x001700C1
		public TEntity Entity
		{
			get
			{
				return (TEntity)((object)this._internalEntityEntry.Entity);
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x060054BA RID: 21690 RVA: 0x00171ED3 File Offset: 0x001700D3
		// (set) Token: 0x060054BB RID: 21691 RVA: 0x00171EE0 File Offset: 0x001700E0
		public EntityState State
		{
			get
			{
				return this._internalEntityEntry.State;
			}
			set
			{
				this._internalEntityEntry.State = value;
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x060054BC RID: 21692 RVA: 0x00171EEE File Offset: 0x001700EE
		public DbPropertyValues CurrentValues
		{
			get
			{
				return new DbPropertyValues(this._internalEntityEntry.CurrentValues);
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x060054BD RID: 21693 RVA: 0x00171F00 File Offset: 0x00170100
		public DbPropertyValues OriginalValues
		{
			get
			{
				return new DbPropertyValues(this._internalEntityEntry.OriginalValues);
			}
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x00171F14 File Offset: 0x00170114
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public DbPropertyValues GetDatabaseValues()
		{
			InternalPropertyValues databaseValues = this._internalEntityEntry.GetDatabaseValues();
			if (databaseValues != null)
			{
				return new DbPropertyValues(databaseValues);
			}
			return null;
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x00171F38 File Offset: 0x00170138
		public Task<DbPropertyValues> GetDatabaseValuesAsync()
		{
			return this.GetDatabaseValuesAsync(CancellationToken.None);
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x00172050 File Offset: 0x00170250
		public async Task<DbPropertyValues> GetDatabaseValuesAsync(CancellationToken cancellationToken)
		{
			InternalPropertyValues storeValues = await this._internalEntityEntry.GetDatabaseValuesAsync(cancellationToken).WithCurrentCulture<InternalPropertyValues>();
			return (storeValues == null) ? null : new DbPropertyValues(storeValues);
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x0017209E File Offset: 0x0017029E
		public void Reload()
		{
			this._internalEntityEntry.Reload();
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x001720AB File Offset: 0x001702AB
		public Task ReloadAsync()
		{
			return this._internalEntityEntry.ReloadAsync(CancellationToken.None);
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x001720BD File Offset: 0x001702BD
		public Task ReloadAsync(CancellationToken cancellationToken)
		{
			return this._internalEntityEntry.ReloadAsync(cancellationToken);
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x001720CB File Offset: 0x001702CB
		public DbReferenceEntry Reference(string navigationProperty)
		{
			Check.NotEmpty(navigationProperty, "navigationProperty");
			return DbReferenceEntry.Create(this._internalEntityEntry.Reference(navigationProperty, null));
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x001720EB File Offset: 0x001702EB
		public DbReferenceEntry<TEntity, TProperty> Reference<TProperty>(string navigationProperty) where TProperty : class
		{
			Check.NotEmpty(navigationProperty, "navigationProperty");
			return DbReferenceEntry<TEntity, TProperty>.Create(this._internalEntityEntry.Reference(navigationProperty, typeof(TProperty)));
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x00172114 File Offset: 0x00170314
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DbReferenceEntry<TEntity, TProperty> Reference<TProperty>(Expression<Func<TEntity, TProperty>> navigationProperty) where TProperty : class
		{
			Check.NotNull<Expression<Func<TEntity, TProperty>>>(navigationProperty, "navigationProperty");
			return DbReferenceEntry<TEntity, TProperty>.Create(this._internalEntityEntry.Reference(DbHelpers.ParsePropertySelector<TEntity, TProperty>(navigationProperty, "Reference", "navigationProperty"), typeof(TProperty)));
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x0017214C File Offset: 0x0017034C
		public DbCollectionEntry Collection(string navigationProperty)
		{
			Check.NotEmpty(navigationProperty, "navigationProperty");
			return DbCollectionEntry.Create(this._internalEntityEntry.Collection(navigationProperty, null));
		}

		// Token: 0x060054C8 RID: 21704 RVA: 0x0017216C File Offset: 0x0017036C
		public DbCollectionEntry<TEntity, TElement> Collection<TElement>(string navigationProperty) where TElement : class
		{
			Check.NotEmpty(navigationProperty, "navigationProperty");
			return DbCollectionEntry<TEntity, TElement>.Create(this._internalEntityEntry.Collection(navigationProperty, typeof(TElement)));
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x00172195 File Offset: 0x00170395
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DbCollectionEntry<TEntity, TElement> Collection<TElement>(Expression<Func<TEntity, ICollection<TElement>>> navigationProperty) where TElement : class
		{
			Check.NotNull<Expression<Func<TEntity, ICollection<TElement>>>>(navigationProperty, "navigationProperty");
			return this.Collection<TElement>(DbHelpers.ParsePropertySelector<TEntity, ICollection<TElement>>(navigationProperty, "Collection", "navigationProperty"));
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x001721B9 File Offset: 0x001703B9
		public DbPropertyEntry Property(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbPropertyEntry.Create(this._internalEntityEntry.Property(propertyName, null, false));
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x001721DA File Offset: 0x001703DA
		public DbPropertyEntry<TEntity, TProperty> Property<TProperty>(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbPropertyEntry<TEntity, TProperty>.Create(this._internalEntityEntry.Property(propertyName, typeof(TProperty), false));
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x00172204 File Offset: 0x00170404
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#", Justification = "Rule predates more fluent naming conventions.")]
		public DbPropertyEntry<TEntity, TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> property)
		{
			Check.NotNull<Expression<Func<TEntity, TProperty>>>(property, "property");
			return this.Property<TProperty>(DbHelpers.ParsePropertySelector<TEntity, TProperty>(property, "Property", "property"));
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x00172228 File Offset: 0x00170428
		public DbComplexPropertyEntry ComplexProperty(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbComplexPropertyEntry.Create(this._internalEntityEntry.Property(propertyName, null, true));
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x00172249 File Offset: 0x00170449
		public DbComplexPropertyEntry<TEntity, TComplexProperty> ComplexProperty<TComplexProperty>(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbComplexPropertyEntry<TEntity, TComplexProperty>.Create(this._internalEntityEntry.Property(propertyName, typeof(TComplexProperty), true));
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x00172273 File Offset: 0x00170473
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#", Justification = "Rule predates more fluent naming conventions.")]
		public DbComplexPropertyEntry<TEntity, TComplexProperty> ComplexProperty<TComplexProperty>(Expression<Func<TEntity, TComplexProperty>> property)
		{
			Check.NotNull<Expression<Func<TEntity, TComplexProperty>>>(property, "property");
			return this.ComplexProperty<TComplexProperty>(DbHelpers.ParsePropertySelector<TEntity, TComplexProperty>(property, "Property", "property"));
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x00172297 File Offset: 0x00170497
		public DbMemberEntry Member(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbMemberEntry.Create(this._internalEntityEntry.Member(propertyName, null));
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x001722B7 File Offset: 0x001704B7
		public DbMemberEntry<TEntity, TMember> Member<TMember>(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return this._internalEntityEntry.Member(propertyName, typeof(TMember)).CreateDbMemberEntry<TEntity, TMember>();
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x001722E0 File Offset: 0x001704E0
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Intentionally just implicit to reduce API clutter.")]
		public static implicit operator DbEntityEntry(DbEntityEntry<TEntity> entry)
		{
			return new DbEntityEntry(entry._internalEntityEntry);
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x001722ED File Offset: 0x001704ED
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public DbEntityValidationResult GetValidationResult()
		{
			return this._internalEntityEntry.InternalContext.Owner.CallValidateEntity(this);
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x0017230A File Offset: 0x0017050A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && !(obj.GetType() != typeof(DbEntityEntry<TEntity>)) && this.Equals((DbEntityEntry<TEntity>)obj);
		}

		// Token: 0x060054D5 RID: 21717 RVA: 0x0017233A File Offset: 0x0017053A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Equals(DbEntityEntry<TEntity> other)
		{
			return object.ReferenceEquals(this, other) || (!object.ReferenceEquals(null, other) && this._internalEntityEntry.Equals(other._internalEntityEntry));
		}

		// Token: 0x060054D6 RID: 21718 RVA: 0x00172363 File Offset: 0x00170563
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return this._internalEntityEntry.GetHashCode();
		}

		// Token: 0x060054D7 RID: 21719 RVA: 0x00172370 File Offset: 0x00170570
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060054D8 RID: 21720 RVA: 0x00172378 File Offset: 0x00170578
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002296 RID: 8854
		private readonly InternalEntityEntry _internalEntityEntry;
	}
}
