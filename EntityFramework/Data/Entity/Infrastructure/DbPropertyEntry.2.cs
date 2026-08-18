using System;
using System.Data.Entity.Internal;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000748 RID: 1864
	public class DbPropertyEntry<TEntity, TProperty> : DbMemberEntry<TEntity, TProperty> where TEntity : class
	{
		// Token: 0x06005458 RID: 21592 RVA: 0x001711B4 File Offset: 0x0016F3B4
		internal static DbPropertyEntry<TEntity, TProperty> Create(InternalPropertyEntry internalPropertyEntry)
		{
			return (DbPropertyEntry<TEntity, TProperty>)internalPropertyEntry.CreateDbMemberEntry<TEntity, TProperty>();
		}

		// Token: 0x06005459 RID: 21593 RVA: 0x001711C1 File Offset: 0x0016F3C1
		internal DbPropertyEntry(InternalPropertyEntry internalPropertyEntry)
		{
			this._internalPropertyEntry = internalPropertyEntry;
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x0600545A RID: 21594 RVA: 0x001711D0 File Offset: 0x0016F3D0
		public override string Name
		{
			get
			{
				return this._internalPropertyEntry.Name;
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x0600545B RID: 21595 RVA: 0x001711DD File Offset: 0x0016F3DD
		// (set) Token: 0x0600545C RID: 21596 RVA: 0x001711EF File Offset: 0x0016F3EF
		public TProperty OriginalValue
		{
			get
			{
				return (TProperty)((object)this._internalPropertyEntry.OriginalValue);
			}
			set
			{
				this._internalPropertyEntry.OriginalValue = value;
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x0600545D RID: 21597 RVA: 0x00171202 File Offset: 0x0016F402
		// (set) Token: 0x0600545E RID: 21598 RVA: 0x00171214 File Offset: 0x0016F414
		public override TProperty CurrentValue
		{
			get
			{
				return (TProperty)((object)this._internalPropertyEntry.CurrentValue);
			}
			set
			{
				this._internalPropertyEntry.CurrentValue = value;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x0600545F RID: 21599 RVA: 0x00171227 File Offset: 0x0016F427
		// (set) Token: 0x06005460 RID: 21600 RVA: 0x00171234 File Offset: 0x0016F434
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

		// Token: 0x06005461 RID: 21601 RVA: 0x00171242 File Offset: 0x0016F442
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Intentionally just implicit to reduce API clutter.")]
		public static implicit operator DbPropertyEntry(DbPropertyEntry<TEntity, TProperty> entry)
		{
			return DbPropertyEntry.Create(entry._internalPropertyEntry);
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06005462 RID: 21602 RVA: 0x0017124F File Offset: 0x0016F44F
		public override DbEntityEntry<TEntity> EntityEntry
		{
			get
			{
				return new DbEntityEntry<TEntity>(this._internalPropertyEntry.InternalEntityEntry);
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06005463 RID: 21603 RVA: 0x00171264 File Offset: 0x0016F464
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

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06005464 RID: 21604 RVA: 0x00171288 File Offset: 0x0016F488
		internal InternalPropertyEntry InternalPropertyEntry
		{
			get
			{
				return this._internalPropertyEntry;
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06005465 RID: 21605 RVA: 0x00171290 File Offset: 0x0016F490
		internal override InternalMemberEntry InternalMemberEntry
		{
			get
			{
				return this.InternalPropertyEntry;
			}
		}

		// Token: 0x0400227C RID: 8828
		private readonly InternalPropertyEntry _internalPropertyEntry;
	}
}
