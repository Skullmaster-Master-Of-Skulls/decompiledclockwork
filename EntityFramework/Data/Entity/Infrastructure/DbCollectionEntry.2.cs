using System;
using System.Collections.Generic;
using System.Data.Entity.Internal;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000744 RID: 1860
	public class DbCollectionEntry<TEntity, TElement> : DbMemberEntry<TEntity, ICollection<TElement>> where TEntity : class
	{
		// Token: 0x0600542E RID: 21550 RVA: 0x00170B83 File Offset: 0x0016ED83
		internal static DbCollectionEntry<TEntity, TElement> Create(InternalCollectionEntry internalCollectionEntry)
		{
			return internalCollectionEntry.CreateDbCollectionEntry<TEntity, TElement>();
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x00170B8B File Offset: 0x0016ED8B
		internal DbCollectionEntry(InternalCollectionEntry internalCollectionEntry)
		{
			this._internalCollectionEntry = internalCollectionEntry;
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06005430 RID: 21552 RVA: 0x00170B9A File Offset: 0x0016ED9A
		public override string Name
		{
			get
			{
				return this._internalCollectionEntry.Name;
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06005431 RID: 21553 RVA: 0x00170BA7 File Offset: 0x0016EDA7
		// (set) Token: 0x06005432 RID: 21554 RVA: 0x00170BB9 File Offset: 0x0016EDB9
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public override ICollection<TElement> CurrentValue
		{
			get
			{
				return (ICollection<TElement>)this._internalCollectionEntry.CurrentValue;
			}
			set
			{
				this._internalCollectionEntry.CurrentValue = value;
			}
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x00170BC7 File Offset: 0x0016EDC7
		public void Load()
		{
			this._internalCollectionEntry.Load();
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x00170BD4 File Offset: 0x0016EDD4
		public Task LoadAsync()
		{
			return this.LoadAsync(CancellationToken.None);
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x00170BE1 File Offset: 0x0016EDE1
		public Task LoadAsync(CancellationToken cancellationToken)
		{
			return this._internalCollectionEntry.LoadAsync(cancellationToken);
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06005436 RID: 21558 RVA: 0x00170BEF File Offset: 0x0016EDEF
		// (set) Token: 0x06005437 RID: 21559 RVA: 0x00170BFC File Offset: 0x0016EDFC
		public bool IsLoaded
		{
			get
			{
				return this._internalCollectionEntry.IsLoaded;
			}
			set
			{
				this._internalCollectionEntry.IsLoaded = value;
			}
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x00170C0A File Offset: 0x0016EE0A
		public IQueryable<TElement> Query()
		{
			return (IQueryable<TElement>)this._internalCollectionEntry.Query();
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x00170C1C File Offset: 0x0016EE1C
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Intentionally just implicit to reduce API clutter.")]
		public static implicit operator DbCollectionEntry(DbCollectionEntry<TEntity, TElement> entry)
		{
			return DbCollectionEntry.Create(entry._internalCollectionEntry);
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x0600543A RID: 21562 RVA: 0x00170C29 File Offset: 0x0016EE29
		internal override InternalMemberEntry InternalMemberEntry
		{
			get
			{
				return this._internalCollectionEntry;
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x0600543B RID: 21563 RVA: 0x00170C31 File Offset: 0x0016EE31
		public override DbEntityEntry<TEntity> EntityEntry
		{
			get
			{
				return new DbEntityEntry<TEntity>(this._internalCollectionEntry.InternalEntityEntry);
			}
		}

		// Token: 0x04002275 RID: 8821
		private readonly InternalCollectionEntry _internalCollectionEntry;
	}
}
