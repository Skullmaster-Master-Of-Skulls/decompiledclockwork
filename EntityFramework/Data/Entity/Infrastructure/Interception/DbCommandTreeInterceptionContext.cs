using System;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200017C RID: 380
	public class DbCommandTreeInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext<DbCommandTree>, IDbMutableInterceptionContext
	{
		// Token: 0x06000CBD RID: 3261 RVA: 0x0003B07D File Offset: 0x0003927D
		public DbCommandTreeInterceptionContext()
		{
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0003B090 File Offset: 0x00039290
		public DbCommandTreeInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0003B0B0 File Offset: 0x000392B0
		internal InterceptionContextMutableData<DbCommandTree> MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0003B0B8 File Offset: 0x000392B8
		InterceptionContextMutableData<DbCommandTree> IDbMutableInterceptionContext<DbCommandTree>.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0003B0C0 File Offset: 0x000392C0
		InterceptionContextMutableData IDbMutableInterceptionContext.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0003B0C8 File Offset: 0x000392C8
		public DbCommandTree OriginalResult
		{
			get
			{
				return this._mutableData.OriginalResult;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0003B0D5 File Offset: 0x000392D5
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x0003B0E2 File Offset: 0x000392E2
		public DbCommandTree Result
		{
			get
			{
				return this._mutableData.Result;
			}
			set
			{
				this._mutableData.Result = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0003B0F0 File Offset: 0x000392F0
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x0003B0FD File Offset: 0x000392FD
		public object UserState
		{
			get
			{
				return this._mutableData.UserState;
			}
			set
			{
				this._mutableData.UserState = value;
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0003B10B File Offset: 0x0003930B
		protected override DbInterceptionContext Clone()
		{
			return new DbCommandTreeInterceptionContext(this);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0003B113 File Offset: 0x00039313
		public new DbCommandTreeInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbCommandTreeInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0003B12D File Offset: 0x0003932D
		public new DbCommandTreeInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbCommandTreeInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0003B147 File Offset: 0x00039347
		public new DbCommandTreeInterceptionContext AsAsync()
		{
			return (DbCommandTreeInterceptionContext)base.AsAsync();
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0003B154 File Offset: 0x00039354
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0003B15C File Offset: 0x0003935C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0003B165 File Offset: 0x00039365
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0003B16D File Offset: 0x0003936D
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400035D RID: 861
		private readonly InterceptionContextMutableData<DbCommandTree> _mutableData = new InterceptionContextMutableData<DbCommandTree>();
	}
}
