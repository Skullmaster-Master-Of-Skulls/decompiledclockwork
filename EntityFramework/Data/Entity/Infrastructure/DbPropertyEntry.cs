using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000746 RID: 1862
	public class DbPropertyEntry : DbMemberEntry
	{
		// Token: 0x06005446 RID: 21574 RVA: 0x00170F59 File Offset: 0x0016F159
		internal static DbPropertyEntry Create(InternalPropertyEntry internalPropertyEntry)
		{
			return (DbPropertyEntry)internalPropertyEntry.CreateDbMemberEntry();
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x00170F66 File Offset: 0x0016F166
		internal DbPropertyEntry(InternalPropertyEntry internalPropertyEntry)
		{
			this._internalPropertyEntry = internalPropertyEntry;
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06005448 RID: 21576 RVA: 0x00170F75 File Offset: 0x0016F175
		public override string Name
		{
			get
			{
				return this._internalPropertyEntry.Name;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06005449 RID: 21577 RVA: 0x00170F82 File Offset: 0x0016F182
		// (set) Token: 0x0600544A RID: 21578 RVA: 0x00170F8F File Offset: 0x0016F18F
		public object OriginalValue
		{
			get
			{
				return this._internalPropertyEntry.OriginalValue;
			}
			set
			{
				this._internalPropertyEntry.OriginalValue = value;
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x0600544B RID: 21579 RVA: 0x00170F9D File Offset: 0x0016F19D
		// (set) Token: 0x0600544C RID: 21580 RVA: 0x00170FAA File Offset: 0x0016F1AA
		public override object CurrentValue
		{
			get
			{
				return this._internalPropertyEntry.CurrentValue;
			}
			set
			{
				this._internalPropertyEntry.CurrentValue = value;
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x0600544D RID: 21581 RVA: 0x00170FB8 File Offset: 0x0016F1B8
		// (set) Token: 0x0600544E RID: 21582 RVA: 0x00170FC5 File Offset: 0x0016F1C5
		public bool IsModified
		{
			get
			{
				return this._internalPropertyEntry.IsModified;
			}
			set
			{
				this._internalPropertyEntry.IsModified = value;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x0600544F RID: 21583 RVA: 0x00170FD3 File Offset: 0x0016F1D3
		public override DbEntityEntry EntityEntry
		{
			get
			{
				return new DbEntityEntry(this._internalPropertyEntry.InternalEntityEntry);
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06005450 RID: 21584 RVA: 0x00170FE8 File Offset: 0x0016F1E8
		public DbComplexPropertyEntry ParentProperty
		{
			get
			{
				InternalPropertyEntry parentPropertyEntry = this._internalPropertyEntry.ParentPropertyEntry;
				if (parentPropertyEntry == null)
				{
					return null;
				}
				return DbComplexPropertyEntry.Create(parentPropertyEntry);
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06005451 RID: 21585 RVA: 0x0017100C File Offset: 0x0016F20C
		internal override InternalMemberEntry InternalMemberEntry
		{
			get
			{
				return this._internalPropertyEntry;
			}
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x00171014 File Offset: 0x0016F214
		public new DbPropertyEntry<TEntity, TProperty> Cast<TEntity, TProperty>() where TEntity : class
		{
			PropertyEntryMetadata entryMetadata = this._internalPropertyEntry.EntryMetadata;
			if (!typeof(TEntity).IsAssignableFrom(entryMetadata.DeclaringType) || !typeof(TProperty).IsAssignableFrom(entryMetadata.ElementType))
			{
				throw Error.DbMember_BadTypeForCast(typeof(DbPropertyEntry).Name, typeof(TEntity).Name, typeof(TProperty).Name, entryMetadata.DeclaringType.Name, entryMetadata.MemberType.Name);
			}
			return DbPropertyEntry<TEntity, TProperty>.Create(this._internalPropertyEntry);
		}

		// Token: 0x0400227B RID: 8827
		private readonly InternalPropertyEntry _internalPropertyEntry;
	}
}
