using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000741 RID: 1857
	public abstract class DbMemberEntry
	{
		// Token: 0x06005406 RID: 21510 RVA: 0x00170909 File Offset: 0x0016EB09
		internal static DbMemberEntry Create(InternalMemberEntry internalMemberEntry)
		{
			return internalMemberEntry.CreateDbMemberEntry();
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06005407 RID: 21511
		public abstract string Name { get; }

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06005408 RID: 21512
		// (set) Token: 0x06005409 RID: 21513
		public abstract object CurrentValue { get; set; }

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x0600540A RID: 21514
		public abstract DbEntityEntry EntityEntry { get; }

		// Token: 0x0600540B RID: 21515 RVA: 0x00170911 File Offset: 0x0016EB11
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public ICollection<DbValidationError> GetValidationErrors()
		{
			return this.InternalMemberEntry.GetValidationErrors().ToList<DbValidationError>();
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x00170923 File Offset: 0x0016EB23
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x0017092B File Offset: 0x0016EB2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x00170934 File Offset: 0x0016EB34
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x0017093C File Offset: 0x0016EB3C
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06005410 RID: 21520
		internal abstract InternalMemberEntry InternalMemberEntry { get; }

		// Token: 0x06005411 RID: 21521 RVA: 0x00170944 File Offset: 0x0016EB44
		public DbMemberEntry<TEntity, TProperty> Cast<TEntity, TProperty>() where TEntity : class
		{
			MemberEntryMetadata entryMetadata = this.InternalMemberEntry.EntryMetadata;
			if (!typeof(TEntity).IsAssignableFrom(entryMetadata.DeclaringType) || !typeof(TProperty).IsAssignableFrom(entryMetadata.MemberType))
			{
				throw Error.DbMember_BadTypeForCast(typeof(DbMemberEntry).Name, typeof(TEntity).Name, typeof(TProperty).Name, entryMetadata.DeclaringType.Name, entryMetadata.MemberType.Name);
			}
			return DbMemberEntry<TEntity, TProperty>.Create(this.InternalMemberEntry);
		}
	}
}
