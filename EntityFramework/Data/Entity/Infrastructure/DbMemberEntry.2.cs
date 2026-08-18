using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000743 RID: 1859
	public abstract class DbMemberEntry<TEntity, TProperty> where TEntity : class
	{
		// Token: 0x06005421 RID: 21537 RVA: 0x00170B33 File Offset: 0x0016ED33
		internal static DbMemberEntry<TEntity, TProperty> Create(InternalMemberEntry internalMemberEntry)
		{
			return internalMemberEntry.CreateDbMemberEntry<TEntity, TProperty>();
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06005422 RID: 21538
		public abstract string Name { get; }

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06005423 RID: 21539
		// (set) Token: 0x06005424 RID: 21540
		public abstract TProperty CurrentValue { get; set; }

		// Token: 0x06005425 RID: 21541 RVA: 0x00170B3B File Offset: 0x0016ED3B
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Intentionally just implicit to reduce API clutter.")]
		public static implicit operator DbMemberEntry(DbMemberEntry<TEntity, TProperty> entry)
		{
			return DbMemberEntry.Create(entry.InternalMemberEntry);
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06005426 RID: 21542
		internal abstract InternalMemberEntry InternalMemberEntry { get; }

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06005427 RID: 21543
		public abstract DbEntityEntry<TEntity> EntityEntry { get; }

		// Token: 0x06005428 RID: 21544 RVA: 0x00170B48 File Offset: 0x0016ED48
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public ICollection<DbValidationError> GetValidationErrors()
		{
			return this.InternalMemberEntry.GetValidationErrors().ToList<DbValidationError>();
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x00170B5A File Offset: 0x0016ED5A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x00170B62 File Offset: 0x0016ED62
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600542B RID: 21547 RVA: 0x00170B6B File Offset: 0x0016ED6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x00170B73 File Offset: 0x0016ED73
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
