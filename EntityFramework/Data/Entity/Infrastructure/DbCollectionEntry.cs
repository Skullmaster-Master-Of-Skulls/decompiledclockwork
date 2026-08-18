using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000742 RID: 1858
	public class DbCollectionEntry : DbMemberEntry
	{
		// Token: 0x06005413 RID: 21523 RVA: 0x001709E7 File Offset: 0x0016EBE7
		internal static DbCollectionEntry Create(InternalCollectionEntry internalCollectionEntry)
		{
			return (DbCollectionEntry)internalCollectionEntry.CreateDbMemberEntry();
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x001709F4 File Offset: 0x0016EBF4
		internal DbCollectionEntry(InternalCollectionEntry internalCollectionEntry)
		{
			this._internalCollectionEntry = internalCollectionEntry;
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06005415 RID: 21525 RVA: 0x00170A03 File Offset: 0x0016EC03
		public override string Name
		{
			get
			{
				return this._internalCollectionEntry.Name;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06005416 RID: 21526 RVA: 0x00170A10 File Offset: 0x0016EC10
		// (set) Token: 0x06005417 RID: 21527 RVA: 0x00170A1D File Offset: 0x0016EC1D
		public override object CurrentValue
		{
			get
			{
				return this._internalCollectionEntry.CurrentValue;
			}
			set
			{
				this._internalCollectionEntry.CurrentValue = value;
			}
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x00170A2B File Offset: 0x0016EC2B
		public void Load()
		{
			this._internalCollectionEntry.Load();
		}

		// Token: 0x06005419 RID: 21529 RVA: 0x00170A38 File Offset: 0x0016EC38
		public Task LoadAsync()
		{
			return this.LoadAsync(CancellationToken.None);
		}

		// Token: 0x0600541A RID: 21530 RVA: 0x00170A45 File Offset: 0x0016EC45
		public Task LoadAsync(CancellationToken cancellationToken)
		{
			return this._internalCollectionEntry.LoadAsync(cancellationToken);
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x0600541B RID: 21531 RVA: 0x00170A53 File Offset: 0x0016EC53
		// (set) Token: 0x0600541C RID: 21532 RVA: 0x00170A60 File Offset: 0x0016EC60
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

		// Token: 0x0600541D RID: 21533 RVA: 0x00170A6E File Offset: 0x0016EC6E
		public IQueryable Query()
		{
			return this._internalCollectionEntry.Query();
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x0600541E RID: 21534 RVA: 0x00170A7B File Offset: 0x0016EC7B
		public override DbEntityEntry EntityEntry
		{
			get
			{
				return new DbEntityEntry(this._internalCollectionEntry.InternalEntityEntry);
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x0600541F RID: 21535 RVA: 0x00170A8D File Offset: 0x0016EC8D
		internal override InternalMemberEntry InternalMemberEntry
		{
			get
			{
				return this._internalCollectionEntry;
			}
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x00170A98 File Offset: 0x0016EC98
		public new DbCollectionEntry<TEntity, TElement> Cast<TEntity, TElement>() where TEntity : class
		{
			MemberEntryMetadata entryMetadata = this._internalCollectionEntry.EntryMetadata;
			if (!typeof(TEntity).IsAssignableFrom(entryMetadata.DeclaringType) || !typeof(TElement).IsAssignableFrom(entryMetadata.ElementType))
			{
				throw Error.DbMember_BadTypeForCast(typeof(DbCollectionEntry).Name, typeof(TEntity).Name, typeof(TElement).Name, entryMetadata.DeclaringType.Name, entryMetadata.ElementType.Name);
			}
			return DbCollectionEntry<TEntity, TElement>.Create(this._internalCollectionEntry);
		}

		// Token: 0x04002274 RID: 8820
		private readonly InternalCollectionEntry _internalCollectionEntry;
	}
}
