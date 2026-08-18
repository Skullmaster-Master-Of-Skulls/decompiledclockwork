using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000179 RID: 377
	public class DbCommandInterceptionContext<TResult> : DbCommandInterceptionContext, IDbMutableInterceptionContext<!0>, IDbMutableInterceptionContext
	{
		// Token: 0x06000C99 RID: 3225 RVA: 0x0003AECB File Offset: 0x000390CB
		public DbCommandInterceptionContext()
		{
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0003AEDE File Offset: 0x000390DE
		public DbCommandInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0003AEF2 File Offset: 0x000390F2
		InterceptionContextMutableData IDbMutableInterceptionContext.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0003AEFA File Offset: 0x000390FA
		InterceptionContextMutableData<TResult> IDbMutableInterceptionContext<!0>.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0003AF02 File Offset: 0x00039102
		internal InterceptionContextMutableData<TResult> MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0003AF0A File Offset: 0x0003910A
		public TResult OriginalResult
		{
			get
			{
				return this._mutableData.OriginalResult;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0003AF17 File Offset: 0x00039117
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x0003AF24 File Offset: 0x00039124
		public TResult Result
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0003AF32 File Offset: 0x00039132
		public bool IsExecutionSuppressed
		{
			get
			{
				return this._mutableData.IsExecutionSuppressed;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0003AF3F File Offset: 0x0003913F
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0003AF4C File Offset: 0x0003914C
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

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0003AF5A File Offset: 0x0003915A
		public void SuppressExecution()
		{
			this._mutableData.SuppressExecution();
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0003AF67 File Offset: 0x00039167
		public Exception OriginalException
		{
			get
			{
				return this._mutableData.OriginalException;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0003AF74 File Offset: 0x00039174
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x0003AF81 File Offset: 0x00039181
		public Exception Exception
		{
			get
			{
				return this._mutableData.Exception;
			}
			set
			{
				this._mutableData.Exception = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0003AF8F File Offset: 0x0003918F
		public TaskStatus TaskStatus
		{
			get
			{
				return this._mutableData.TaskStatus;
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0003AF9C File Offset: 0x0003919C
		public new DbCommandInterceptionContext<TResult> AsAsync()
		{
			return (DbCommandInterceptionContext<TResult>)base.AsAsync();
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0003AFA9 File Offset: 0x000391A9
		public new DbCommandInterceptionContext<TResult> WithCommandBehavior(CommandBehavior commandBehavior)
		{
			return (DbCommandInterceptionContext<TResult>)base.WithCommandBehavior(commandBehavior);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0003AFB7 File Offset: 0x000391B7
		protected override DbInterceptionContext Clone()
		{
			return new DbCommandInterceptionContext<TResult>(this);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0003AFBF File Offset: 0x000391BF
		public new DbCommandInterceptionContext<TResult> WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbCommandInterceptionContext<TResult>)base.WithDbContext(context);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0003AFD9 File Offset: 0x000391D9
		public new DbCommandInterceptionContext<TResult> WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbCommandInterceptionContext<TResult>)base.WithObjectContext(context);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0003AFF3 File Offset: 0x000391F3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0003AFFB File Offset: 0x000391FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0003B004 File Offset: 0x00039204
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0003B00C File Offset: 0x0003920C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400035A RID: 858
		private readonly InterceptionContextMutableData<TResult> _mutableData = new InterceptionContextMutableData<TResult>();
	}
}
