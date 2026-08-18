using System;
using System.Data.Entity.Resources;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000187 RID: 391
	internal class InterceptionContextMutableData
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0003C2EE File Offset: 0x0003A4EE
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x0003C2F6 File Offset: 0x0003A4F6
		public bool HasExecuted { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0003C2FF File Offset: 0x0003A4FF
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x0003C307 File Offset: 0x0003A507
		public Exception OriginalException { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0003C310 File Offset: 0x0003A510
		// (set) Token: 0x06000D62 RID: 3426 RVA: 0x0003C318 File Offset: 0x0003A518
		public TaskStatus TaskStatus { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x0003C321 File Offset: 0x0003A521
		// (set) Token: 0x06000D64 RID: 3428 RVA: 0x0003C329 File Offset: 0x0003A529
		public object UserState { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0003C332 File Offset: 0x0003A532
		public bool IsExecutionSuppressed
		{
			get
			{
				return this._isSuppressed;
			}
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0003C33A File Offset: 0x0003A53A
		public void SuppressExecution()
		{
			if (!this._isSuppressed && this.HasExecuted)
			{
				throw new InvalidOperationException(Strings.SuppressionAfterExecution);
			}
			this._isSuppressed = true;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x0003C35E File Offset: 0x0003A55E
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x0003C366 File Offset: 0x0003A566
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
			set
			{
				if (!this.HasExecuted)
				{
					this.SuppressExecution();
				}
				this._exception = value;
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0003C37D File Offset: 0x0003A57D
		public void SetExceptionThrown(Exception exception)
		{
			this.HasExecuted = true;
			this.OriginalException = exception;
			this.Exception = exception;
		}

		// Token: 0x040003A1 RID: 929
		private Exception _exception;

		// Token: 0x040003A2 RID: 930
		private bool _isSuppressed;
	}
}
