using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000752 RID: 1874
	public class DbReferenceEntry : DbMemberEntry
	{
		// Token: 0x060054F8 RID: 21752 RVA: 0x001725FB File Offset: 0x001707FB
		internal static DbReferenceEntry Create(InternalReferenceEntry internalReferenceEntry)
		{
			return (DbReferenceEntry)internalReferenceEntry.CreateDbMemberEntry();
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x00172608 File Offset: 0x00170808
		internal DbReferenceEntry(InternalReferenceEntry internalReferenceEntry)
		{
			this._internalReferenceEntry = internalReferenceEntry;
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x060054FA RID: 21754 RVA: 0x00172617 File Offset: 0x00170817
		public override string Name
		{
			get
			{
				return this._internalReferenceEntry.Name;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x060054FB RID: 21755 RVA: 0x00172624 File Offset: 0x00170824
		// (set) Token: 0x060054FC RID: 21756 RVA: 0x00172631 File Offset: 0x00170831
		public override object CurrentValue
		{
			get
			{
				return this._internalReferenceEntry.CurrentValue;
			}
			set
			{
				this._internalReferenceEntry.CurrentValue = value;
			}
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x0017263F File Offset: 0x0017083F
		public void Load()
		{
			this._internalReferenceEntry.Load();
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x0017264C File Offset: 0x0017084C
		public Task LoadAsync()
		{
			return this.LoadAsync(CancellationToken.None);
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x00172659 File Offset: 0x00170859
		public Task LoadAsync(CancellationToken cancellationToken)
		{
			return this._internalReferenceEntry.LoadAsync(cancellationToken);
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06005500 RID: 21760 RVA: 0x00172667 File Offset: 0x00170867
		// (set) Token: 0x06005501 RID: 21761 RVA: 0x00172674 File Offset: 0x00170874
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

		// Token: 0x06005502 RID: 21762 RVA: 0x00172682 File Offset: 0x00170882
		public IQueryable Query()
		{
			return this._internalReferenceEntry.Query();
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06005503 RID: 21763 RVA: 0x0017268F File Offset: 0x0017088F
		public override DbEntityEntry EntityEntry
		{
			get
			{
				return new DbEntityEntry(this._internalReferenceEntry.InternalEntityEntry);
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06005504 RID: 21764 RVA: 0x001726A1 File Offset: 0x001708A1
		internal override InternalMemberEntry InternalMemberEntry
		{
			get
			{
				return this._internalReferenceEntry;
			}
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x001726AC File Offset: 0x001708AC
		public new DbReferenceEntry<TEntity, TProperty> Cast<TEntity, TProperty>() where TEntity : class
		{
			MemberEntryMetadata entryMetadata = this._internalReferenceEntry.EntryMetadata;
			if (!typeof(TEntity).IsAssignableFrom(entryMetadata.DeclaringType) || !typeof(TProperty).IsAssignableFrom(entryMetadata.ElementType))
			{
				throw Error.DbMember_BadTypeForCast(typeof(DbReferenceEntry).Name, typeof(TEntity).Name, typeof(TProperty).Name, entryMetadata.DeclaringType.Name, entryMetadata.MemberType.Name);
			}
			return DbReferenceEntry<TEntity, TProperty>.Create(this._internalReferenceEntry);
		}

		// Token: 0x0400229C RID: 8860
		private readonly InternalReferenceEntry _internalReferenceEntry;
	}
}
