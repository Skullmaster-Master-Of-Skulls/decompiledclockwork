using System;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000188 RID: 392
	internal class InterceptionContextMutableData<TResult> : InterceptionContextMutableData
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0003C39C File Offset: 0x0003A59C
		// (set) Token: 0x06000D6C RID: 3436 RVA: 0x0003C3A4 File Offset: 0x0003A5A4
		public TResult OriginalResult { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0003C3AD File Offset: 0x0003A5AD
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x0003C3B5 File Offset: 0x0003A5B5
		public TResult Result
		{
			get
			{
				return this._result;
			}
			set
			{
				if (!base.HasExecuted)
				{
					base.SuppressExecution();
				}
				this._result = value;
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0003C3CC File Offset: 0x0003A5CC
		public void SetExecuted(TResult result)
		{
			base.HasExecuted = true;
			this.OriginalResult = result;
			this.Result = result;
		}

		// Token: 0x040003A7 RID: 935
		private TResult _result;
	}
}
