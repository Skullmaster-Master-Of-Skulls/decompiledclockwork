using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200016A RID: 362
	public abstract class MutableInterceptionContext<TResult> : DbInterceptionContext, IDbMutableInterceptionContext<TResult>, IDbMutableInterceptionContext
	{
		// Token: 0x06000B9E RID: 2974 RVA: 0x0003982C File Offset: 0x00037A2C
		protected MutableInterceptionContext()
		{
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0003983F File Offset: 0x00037A3F
		protected MutableInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0003985F File Offset: 0x00037A5F
		InterceptionContextMutableData<TResult> IDbMutableInterceptionContext<!0>.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00039867 File Offset: 0x00037A67
		InterceptionContextMutableData IDbMutableInterceptionContext.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0003986F File Offset: 0x00037A6F
		public TResult OriginalResult
		{
			get
			{
				return this._mutableData.OriginalResult;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0003987C File Offset: 0x00037A7C
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x00039889 File Offset: 0x00037A89
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00039897 File Offset: 0x00037A97
		public bool IsExecutionSuppressed
		{
			get
			{
				return this._mutableData.IsExecutionSuppressed;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x000398A4 File Offset: 0x00037AA4
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x000398B1 File Offset: 0x00037AB1
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

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000398BF File Offset: 0x00037ABF
		public void SuppressExecution()
		{
			this._mutableData.SuppressExecution();
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x000398CC File Offset: 0x00037ACC
		public Exception OriginalException
		{
			get
			{
				return this._mutableData.OriginalException;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x000398D9 File Offset: 0x00037AD9
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x000398E6 File Offset: 0x00037AE6
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x000398F4 File Offset: 0x00037AF4
		public TaskStatus TaskStatus
		{
			get
			{
				return this._mutableData.TaskStatus;
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00039901 File Offset: 0x00037B01
		public new MutableInterceptionContext<TResult> AsAsync()
		{
			return (MutableInterceptionContext<TResult>)base.AsAsync();
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0003990E File Offset: 0x00037B0E
		public new MutableInterceptionContext<TResult> WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (MutableInterceptionContext<TResult>)base.WithDbContext(context);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00039928 File Offset: 0x00037B28
		public new MutableInterceptionContext<TResult> WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (MutableInterceptionContext<TResult>)base.WithObjectContext(context);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00039942 File Offset: 0x00037B42
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0003994A File Offset: 0x00037B4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00039953 File Offset: 0x00037B53
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0003995B File Offset: 0x00037B5B
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400033C RID: 828
		private readonly InterceptionContextMutableData<TResult> _mutableData = new InterceptionContextMutableData<TResult>();
	}
}
