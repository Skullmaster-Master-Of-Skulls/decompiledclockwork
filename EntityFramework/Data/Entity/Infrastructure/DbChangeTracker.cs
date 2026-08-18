using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000740 RID: 1856
	public class DbChangeTracker
	{
		// Token: 0x060053FB RID: 21499 RVA: 0x00170846 File Offset: 0x0016EA46
		internal DbChangeTracker(InternalContext internalContext)
		{
			this._internalContext = internalContext;
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x00170868 File Offset: 0x0016EA68
		public IEnumerable<DbEntityEntry> Entries()
		{
			return from e in this._internalContext.GetStateEntries()
			select new DbEntityEntry(new InternalEntityEntry(this._internalContext, e));
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x00170899 File Offset: 0x0016EA99
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public IEnumerable<DbEntityEntry<TEntity>> Entries<TEntity>() where TEntity : class
		{
			return from e in this._internalContext.GetStateEntries<TEntity>()
			select new DbEntityEntry<TEntity>(new InternalEntityEntry(this._internalContext, e));
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x001708B7 File Offset: 0x0016EAB7
		public bool HasChanges()
		{
			this._internalContext.DetectChanges(false);
			return this._internalContext.ObjectContext.ObjectStateManager.HasChanges();
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x001708DA File Offset: 0x0016EADA
		public void DetectChanges()
		{
			this._internalContext.DetectChanges(true);
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x001708E8 File Offset: 0x0016EAE8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x001708F0 File Offset: 0x0016EAF0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x001708F9 File Offset: 0x0016EAF9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x00170901 File Offset: 0x0016EB01
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002273 RID: 8819
		private readonly InternalContext _internalContext;
	}
}
