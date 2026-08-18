using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000933 RID: 2355
	internal static class OperationWithTimeoutComposer
	{
		// Token: 0x06005A80 RID: 23168 RVA: 0x0014C6DB File Offset: 0x0014A8DB
		public static IAsyncResult BeginComposeAsyncOperations(TimeSpan timeout, OperationWithTimeoutBeginCallback[] beginOperations, OperationEndCallback[] endOperations, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutComposer.ComposedAsyncResult(timeout, beginOperations, endOperations, callback, state);
		}

		// Token: 0x06005A81 RID: 23169 RVA: 0x0014C6E8 File Offset: 0x0014A8E8
		public static void EndComposeAsyncOperations(IAsyncResult result)
		{
			OperationWithTimeoutComposer.ComposedAsyncResult.End(result);
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x0014C6F0 File Offset: 0x0014A8F0
		public static TimeSpan RemainingTime(IAsyncResult result)
		{
			return ((OperationWithTimeoutComposer.ComposedAsyncResult)result).RemainingTime();
		}

		// Token: 0x02000DC8 RID: 3528
		private class ComposedAsyncResult : AsyncResult
		{
			// Token: 0x06007FFC RID: 32764 RVA: 0x001DC054 File Offset: 0x001DA254
			internal ComposedAsyncResult(TimeSpan timeout, OperationWithTimeoutBeginCallback[] beginOperations, OperationEndCallback[] endOperations, AsyncCallback callback, object state) : base(callback, state)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.beginOperations = beginOperations;
				this.endOperations = endOperations;
				this.SkipToNextOperation();
				if (this.currentOperation < this.beginOperations.Length)
				{
					this.beginOperations[this.currentOperation](this.RemainingTime(), OperationWithTimeoutComposer.ComposedAsyncResult.onOperationCompleted, this);
					return;
				}
				base.Complete(this.completedSynchronously);
			}

			// Token: 0x06007FFD RID: 32765 RVA: 0x001DC0CE File Offset: 0x001DA2CE
			public TimeSpan RemainingTime()
			{
				return this.timeoutHelper.RemainingTime();
			}

			// Token: 0x06007FFE RID: 32766 RVA: 0x001DC0DB File Offset: 0x001DA2DB
			internal static void End(IAsyncResult result)
			{
				AsyncResult.End<OperationWithTimeoutComposer.ComposedAsyncResult>(result);
			}

			// Token: 0x06007FFF RID: 32767 RVA: 0x001DC0E4 File Offset: 0x001DA2E4
			private void OnOperationCompleted(IAsyncResult result)
			{
				this.completedSynchronously = (this.completedSynchronously && result.CompletedSynchronously);
				Exception ex = null;
				try
				{
					this.endOperations[this.currentOperation](result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					base.Complete(this.completedSynchronously, ex);
					return;
				}
				this.currentOperation++;
				this.SkipToNextOperation();
				if (this.currentOperation < this.beginOperations.Length)
				{
					try
					{
						this.beginOperations[this.currentOperation](this.RemainingTime(), OperationWithTimeoutComposer.ComposedAsyncResult.onOperationCompleted, this);
					}
					catch (Exception ex3)
					{
						if (Fx.IsFatal(ex3))
						{
							throw;
						}
						ex = ex3;
					}
					if (ex != null)
					{
						base.Complete(this.completedSynchronously, ex);
						return;
					}
				}
				else
				{
					base.Complete(this.completedSynchronously);
				}
			}

			// Token: 0x06008000 RID: 32768 RVA: 0x001DC1CC File Offset: 0x001DA3CC
			private static void OnOperationCompletedStatic(IAsyncResult result)
			{
				((OperationWithTimeoutComposer.ComposedAsyncResult)result.AsyncState).OnOperationCompleted(result);
			}

			// Token: 0x06008001 RID: 32769 RVA: 0x001DC1DF File Offset: 0x001DA3DF
			private void SkipToNextOperation()
			{
				while (this.currentOperation < this.beginOperations.Length)
				{
					if (this.beginOperations[this.currentOperation] != null)
					{
						return;
					}
					this.currentOperation++;
				}
			}

			// Token: 0x04004924 RID: 18724
			private OperationWithTimeoutBeginCallback[] beginOperations;

			// Token: 0x04004925 RID: 18725
			private bool completedSynchronously = true;

			// Token: 0x04004926 RID: 18726
			private int currentOperation;

			// Token: 0x04004927 RID: 18727
			private OperationEndCallback[] endOperations;

			// Token: 0x04004928 RID: 18728
			private TimeoutHelper timeoutHelper;

			// Token: 0x04004929 RID: 18729
			private static AsyncCallback onOperationCompleted = Fx.ThunkCallback(new AsyncCallback(OperationWithTimeoutComposer.ComposedAsyncResult.OnOperationCompletedStatic));
		}
	}
}
