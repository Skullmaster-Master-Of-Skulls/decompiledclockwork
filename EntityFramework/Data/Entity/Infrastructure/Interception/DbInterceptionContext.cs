using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200014C RID: 332
	public class DbInterceptionContext
	{
		// Token: 0x06000AD0 RID: 2768 RVA: 0x00037088 File Offset: 0x00035288
		public DbInterceptionContext()
		{
			this._dbContexts = new List<DbContext>();
			this._objectContexts = new List<ObjectContext>();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000370CC File Offset: 0x000352CC
		protected DbInterceptionContext(DbInterceptionContext copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
			this._dbContexts = (from c in copyFrom.DbContexts
			where c.InternalContext == null || !c.InternalContext.IsDisposed
			select c).ToList<DbContext>();
			this._objectContexts = (from c in copyFrom.ObjectContexts
			where !c.IsDisposed
			select c).ToList<ObjectContext>();
			this._isAsync = copyFrom._isAsync;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00037190 File Offset: 0x00035390
		private DbInterceptionContext(IEnumerable<DbInterceptionContext> copyFrom)
		{
			this._dbContexts = (from c in copyFrom.SelectMany((DbInterceptionContext c) => c.DbContexts).Distinct<DbContext>()
			where !c.InternalContext.IsDisposed
			select c).ToList<DbContext>();
			this._objectContexts = (from c in copyFrom.SelectMany((DbInterceptionContext c) => c.ObjectContexts).Distinct<ObjectContext>()
			where !c.IsDisposed
			select c).ToList<ObjectContext>();
			this._isAsync = copyFrom.Any((DbInterceptionContext c) => c.IsAsync);
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00037276 File Offset: 0x00035476
		public IEnumerable<DbContext> DbContexts
		{
			get
			{
				return this._dbContexts;
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00037280 File Offset: 0x00035480
		public DbInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			DbInterceptionContext dbInterceptionContext = this.Clone();
			if (!dbInterceptionContext._dbContexts.Contains(context, ObjectReferenceEqualityComparer.Default))
			{
				dbInterceptionContext._dbContexts.Add(context);
			}
			return dbInterceptionContext;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x000372C0 File Offset: 0x000354C0
		public IEnumerable<ObjectContext> ObjectContexts
		{
			get
			{
				return this._objectContexts;
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000372C8 File Offset: 0x000354C8
		public DbInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			DbInterceptionContext dbInterceptionContext = this.Clone();
			if (!dbInterceptionContext._objectContexts.Contains(context, ObjectReferenceEqualityComparer.Default))
			{
				dbInterceptionContext._objectContexts.Add(context);
			}
			return dbInterceptionContext;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00037308 File Offset: 0x00035508
		public bool IsAsync
		{
			get
			{
				return this._isAsync;
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00037310 File Offset: 0x00035510
		public DbInterceptionContext AsAsync()
		{
			DbInterceptionContext dbInterceptionContext = this.Clone();
			dbInterceptionContext._isAsync = true;
			return dbInterceptionContext;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0003732C File Offset: 0x0003552C
		protected virtual DbInterceptionContext Clone()
		{
			return new DbInterceptionContext(this);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00037334 File Offset: 0x00035534
		internal static DbInterceptionContext Combine(IEnumerable<DbInterceptionContext> interceptionContexts)
		{
			return new DbInterceptionContext(interceptionContexts);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0003733C File Offset: 0x0003553C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00037344 File Offset: 0x00035544
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0003734D File Offset: 0x0003554D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00037355 File Offset: 0x00035555
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040002E8 RID: 744
		private readonly IList<DbContext> _dbContexts;

		// Token: 0x040002E9 RID: 745
		private readonly IList<ObjectContext> _objectContexts;

		// Token: 0x040002EA RID: 746
		private bool _isAsync;
	}
}
