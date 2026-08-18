using System;

namespace System.Runtime
{
	// Token: 0x02000027 RID: 39
	internal abstract class ScheduleActionItemAsyncResult : AsyncResult
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00005CB5 File Offset: 0x00003EB5
		protected ScheduleActionItemAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005CBF File Offset: 0x00003EBF
		protected void Schedule()
		{
			ActionItem.Schedule(ScheduleActionItemAsyncResult.doWork, this);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005CCC File Offset: 0x00003ECC
		private static void DoWork(object state)
		{
			ScheduleActionItemAsyncResult scheduleActionItemAsyncResult = (ScheduleActionItemAsyncResult)state;
			Exception exception = null;
			try
			{
				scheduleActionItemAsyncResult.OnDoWork();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				exception = ex;
			}
			scheduleActionItemAsyncResult.Complete(false, exception);
		}

		// Token: 0x06000140 RID: 320
		protected abstract void OnDoWork();

		// Token: 0x06000141 RID: 321 RVA: 0x00005D14 File Offset: 0x00003F14
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ScheduleActionItemAsyncResult>(result);
		}

		// Token: 0x04000097 RID: 151
		private static Action<object> doWork = new Action<object>(ScheduleActionItemAsyncResult.DoWork);
	}
}
