using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200016C RID: 364
	public class BeginTransactionInterceptionContext : DbConnectionInterceptionContext<DbTransaction>
	{
		// Token: 0x06000BBE RID: 3006 RVA: 0x000399EA File Offset: 0x00037BEA
		public BeginTransactionInterceptionContext()
		{
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000399FC File Offset: 0x00037BFC
		public BeginTransactionInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
			BeginTransactionInterceptionContext beginTransactionInterceptionContext = copyFrom as BeginTransactionInterceptionContext;
			if (beginTransactionInterceptionContext != null)
			{
				this._isolationLevel = beginTransactionInterceptionContext._isolationLevel;
			}
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00039A39 File Offset: 0x00037C39
		public new BeginTransactionInterceptionContext AsAsync()
		{
			return (BeginTransactionInterceptionContext)base.AsAsync();
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00039A46 File Offset: 0x00037C46
		public IsolationLevel IsolationLevel
		{
			get
			{
				return this._isolationLevel;
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00039A50 File Offset: 0x00037C50
		public BeginTransactionInterceptionContext WithIsolationLevel(IsolationLevel isolationLevel)
		{
			BeginTransactionInterceptionContext beginTransactionInterceptionContext = this.TypedClone();
			beginTransactionInterceptionContext._isolationLevel = isolationLevel;
			return beginTransactionInterceptionContext;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00039A6C File Offset: 0x00037C6C
		private BeginTransactionInterceptionContext TypedClone()
		{
			return (BeginTransactionInterceptionContext)this.Clone();
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00039A79 File Offset: 0x00037C79
		protected override DbInterceptionContext Clone()
		{
			return new BeginTransactionInterceptionContext(this);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00039A81 File Offset: 0x00037C81
		public new BeginTransactionInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (BeginTransactionInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00039A9B File Offset: 0x00037C9B
		public new BeginTransactionInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (BeginTransactionInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00039AB5 File Offset: 0x00037CB5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00039ABD File Offset: 0x00037CBD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00039AC6 File Offset: 0x00037CC6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00039ACE File Offset: 0x00037CCE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400033D RID: 829
		private IsolationLevel _isolationLevel = IsolationLevel.Unspecified;
	}
}
