using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000173 RID: 371
	public abstract class MutableInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext
	{
		// Token: 0x06000C30 RID: 3120 RVA: 0x0003A5FD File Offset: 0x000387FD
		protected MutableInterceptionContext()
		{
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0003A610 File Offset: 0x00038810
		protected MutableInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0003A630 File Offset: 0x00038830
		InterceptionContextMutableData IDbMutableInterceptionContext.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0003A638 File Offset: 0x00038838
		internal InterceptionContextMutableData MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0003A640 File Offset: 0x00038840
		public bool IsExecutionSuppressed
		{
			get
			{
				return this._mutableData.IsExecutionSuppressed;
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0003A64D File Offset: 0x0003884D
		public void SuppressExecution()
		{
			this._mutableData.SuppressExecution();
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0003A65A File Offset: 0x0003885A
		public Exception OriginalException
		{
			get
			{
				return this._mutableData.OriginalException;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0003A667 File Offset: 0x00038867
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0003A674 File Offset: 0x00038874
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

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0003A682 File Offset: 0x00038882
		public TaskStatus TaskStatus
		{
			get
			{
				return this._mutableData.TaskStatus;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0003A68F File Offset: 0x0003888F
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x0003A69C File Offset: 0x0003889C
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

		// Token: 0x06000C3C RID: 3132 RVA: 0x0003A6AA File Offset: 0x000388AA
		public new MutableInterceptionContext AsAsync()
		{
			return (MutableInterceptionContext)base.AsAsync();
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0003A6B7 File Offset: 0x000388B7
		public new MutableInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (MutableInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0003A6D1 File Offset: 0x000388D1
		public new MutableInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (MutableInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0003A6EB File Offset: 0x000388EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0003A6F3 File Offset: 0x000388F3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0003A6FC File Offset: 0x000388FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0003A704 File Offset: 0x00038904
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000343 RID: 835
		private readonly InterceptionContextMutableData _mutableData = new InterceptionContextMutableData();
	}
}
