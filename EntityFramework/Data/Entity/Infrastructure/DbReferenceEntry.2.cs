using System;
using System.Data.Entity.Internal;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000753 RID: 1875
	public class DbReferenceEntry<TEntity, TProperty> : DbMemberEntry<TEntity, TProperty> where TEntity : class
	{
		// Token: 0x06005506 RID: 21766 RVA: 0x00172747 File Offset: 0x00170947
		internal static DbReferenceEntry<TEntity, TProperty> Create(InternalReferenceEntry internalReferenceEntry)
		{
			return (DbReferenceEntry<TEntity, TProperty>)internalReferenceEntry.CreateDbMemberEntry<TEntity, TProperty>();
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x00172754 File Offset: 0x00170954
		internal DbReferenceEntry(InternalReferenceEntry internalReferenceEntry)
		{
			this._internalReferenceEntry = internalReferenceEntry;
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06005508 RID: 21768 RVA: 0x00172763 File Offset: 0x00170963
		public override string Name
		{
			get
			{
				return this._internalReferenceEntry.Name;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06005509 RID: 21769 RVA: 0x00172770 File Offset: 0x00170970
		// (set) Token: 0x0600550A RID: 21770 RVA: 0x00172782 File Offset: 0x00170982
		public override TProperty CurrentValue
		{
			get
			{
				return (TProperty)((object)this._internalReferenceEntry.CurrentValue);
			}
			set
			{
				this._internalReferenceEntry.CurrentValue = value;
			}
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x00172795 File Offset: 0x00170995
		public void Load()
		{
			this._internalReferenceEntry.Load();
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x001727A2 File Offset: 0x001709A2
		public Task LoadAsync()
		{
			return this.LoadAsync(CancellationToken.None);
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x001727AF File Offset: 0x001709AF
		public Task LoadAsync(CancellationToken cancellationToken)
		{
			return this._internalReferenceEntry.LoadAsync(cancellationToken);
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x0600550E RID: 21774 RVA: 0x001727BD File Offset: 0x001709BD
		// (set) Token: 0x0600550F RID: 21775 RVA: 0x001727CA File Offset: 0x001709CA
		public bool IsLoaded
		{
			get
			{
				return this._internalReferenceEntry.IsLoaded;
			}
			set
			{
				this._internalReferenceEntry.IsLoaded = value;
			}
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x001727D8 File Offset: 0x001709D8
		public IQueryable<TProperty> Query()
		{
			return (IQueryable<TProperty>)this._internalReferenceEntry.Query();
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x001727EA File Offset: 0x001709EA
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Intentionally just implicit to reduce API clutter.")]
		public static implicit operator DbReferenceEntry(DbReferenceEntry<TEntity, TProperty> entry)
		{
			return DbReferenceEntry.Create(entry._internalReferenceEntry);
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06005512 RID: 21778 RVA: 0x001727F7 File Offset: 0x001709F7
		internal override InternalMemberEntry InternalMemberEntry
		{
			get
			{
				return this._internalReferenceEntry;
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06005513 RID: 21779 RVA: 0x001727FF File Offset: 0x001709FF
		public override DbEntityEntry<TEntity> EntityEntry
		{
			get
			{
				return new DbEntityEntry<TEntity>(this._internalReferenceEntry.InternalEntityEntry);
			}
		}

		// Token: 0x0400229D RID: 8861
		private readonly InternalReferenceEntry _internalReferenceEntry;
	}
}
