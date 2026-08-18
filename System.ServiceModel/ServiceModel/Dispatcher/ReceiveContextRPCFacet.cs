using System;
using System.Runtime;
using System.ServiceModel.Channels;
using System.Transactions;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000595 RID: 1429
	internal class ReceiveContextRPCFacet
	{
		// Token: 0x0600373B RID: 14139 RVA: 0x000D523D File Offset: 0x000D343D
		private ReceiveContextRPCFacet(ReceiveContext receiveContext)
		{
			this.receiveContext = receiveContext;
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x000D524C File Offset: 0x000D344C
		public static void CreateIfRequired(ImmutableDispatchRuntime dispatchRuntime, ref MessageRpc messageRpc)
		{
			if (messageRpc.Operation.ReceiveContextAcknowledgementMode == ReceiveContextAcknowledgementMode.ManualAcknowledgement)
			{
				return;
			}
			ReceiveContext receiveContext = null;
			if (!ReceiveContext.TryGet(messageRpc.Request, out receiveContext))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxReceiveContextPropertyMissing", new object[]
				{
					typeof(ReceiveContext).Name
				})));
			}
			messageRpc.Request.Properties.Remove(ReceiveContext.Name);
			if (messageRpc.Operation.ReceiveContextAcknowledgementMode == ReceiveContextAcknowledgementMode.AutoAcknowledgeOnReceive && !messageRpc.Operation.TransactionRequired)
			{
				IAsyncResult asyncResult = new ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult(receiveContext, TimeSpan.MaxValue, ref messageRpc, null, ReceiveContextRPCFacet.handleEndComplete, new ReceiveContextRPCFacet.AcknowledgementCompleteCallbackState
				{
					DispatchRuntime = dispatchRuntime,
					Rpc = messageRpc
				});
				if (asyncResult.CompletedSynchronously)
				{
					ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult.End(asyncResult);
				}
				return;
			}
			messageRpc.ReceiveContext = new ReceiveContextRPCFacet(receiveContext);
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000D5324 File Offset: 0x000D3524
		public void Complete(ImmutableDispatchRuntime dispatchRuntime, ref MessageRpc rpc, TimeSpan timeout, Transaction transaction)
		{
			IAsyncResult asyncResult = new ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult(this.receiveContext, timeout, ref rpc, transaction, ReceiveContextRPCFacet.handleEndComplete, new ReceiveContextRPCFacet.AcknowledgementCompleteCallbackState
			{
				DispatchRuntime = dispatchRuntime,
				Rpc = rpc
			});
			if (asyncResult.CompletedSynchronously)
			{
				ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult.End(asyncResult);
			}
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000D536C File Offset: 0x000D356C
		public IAsyncResult BeginComplete(TimeSpan timeout, Transaction transaction, ChannelHandler channelHandler, AsyncCallback callback, object state)
		{
			IAsyncResult result = null;
			if (transaction != null)
			{
				using (TransactionScope transactionScope = new TransactionScope(transaction))
				{
					ReceiveContextRPCFacet.TransactionOutcomeListener.EnsureReceiveContextAbandonOnTransactionRollback(this.receiveContext, transaction, channelHandler);
					result = this.receiveContext.BeginComplete(timeout, callback, state);
					transactionScope.Complete();
					return result;
				}
			}
			result = this.receiveContext.BeginComplete(timeout, callback, state);
			return result;
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000D53E0 File Offset: 0x000D35E0
		public void EndComplete(IAsyncResult result)
		{
			this.receiveContext.EndComplete(result);
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000D53EE File Offset: 0x000D35EE
		public IAsyncResult BeginAbandon(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.receiveContext.BeginAbandon(timeout, callback, state);
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000D53FE File Offset: 0x000D35FE
		public void EndAbandon(IAsyncResult result)
		{
			this.receiveContext.EndAbandon(result);
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000D540C File Offset: 0x000D360C
		private static void HandleEndComplete(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			try
			{
				ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult.End(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				ReceiveContextRPCFacet.AcknowledgementCompleteCallbackState acknowledgementCompleteCallbackState = (ReceiveContextRPCFacet.AcknowledgementCompleteCallbackState)result.AsyncState;
				MessageRpc rpc = acknowledgementCompleteCallbackState.Rpc;
				rpc.Error = ex;
				acknowledgementCompleteCallbackState.DispatchRuntime.ErrorBehavior.HandleError(ref rpc);
			}
		}

		// Token: 0x04002919 RID: 10521
		private static AsyncCallback handleEndComplete = Fx.ThunkCallback(new AsyncCallback(ReceiveContextRPCFacet.HandleEndComplete));

		// Token: 0x0400291A RID: 10522
		private ReceiveContext receiveContext;

		// Token: 0x02000CA1 RID: 3233
		private class AcknowledgementCompleteCallbackState
		{
			// Token: 0x17001B82 RID: 7042
			// (get) Token: 0x06007918 RID: 31000 RVA: 0x001C421D File Offset: 0x001C241D
			// (set) Token: 0x06007919 RID: 31001 RVA: 0x001C4225 File Offset: 0x001C2425
			public ImmutableDispatchRuntime DispatchRuntime { get; set; }

			// Token: 0x17001B83 RID: 7043
			// (get) Token: 0x0600791A RID: 31002 RVA: 0x001C422E File Offset: 0x001C242E
			// (set) Token: 0x0600791B RID: 31003 RVA: 0x001C4236 File Offset: 0x001C2436
			public MessageRpc Rpc { get; set; }
		}

		// Token: 0x02000CA2 RID: 3234
		private class AcknowledgementCompleteAsyncResult : AsyncResult
		{
			// Token: 0x0600791D RID: 31005 RVA: 0x001C4248 File Offset: 0x001C2448
			public AcknowledgementCompleteAsyncResult(ReceiveContext receiveContext, TimeSpan timeout, ref MessageRpc rpc, Transaction transaction, AsyncCallback callback, object state) : base(callback, state)
			{
				this.receiveContext = receiveContext;
				this.currentTransaction = transaction;
				this.channelHandler = rpc.channelHandler;
				this.resumableRPC = rpc.Pause();
				bool flag = true;
				try
				{
					bool flag2 = this.Complete(timeout);
					flag = false;
					if (flag2)
					{
						this.resumableRPC = null;
						rpc.UnPause();
						base.Complete(true);
					}
				}
				finally
				{
					if (flag)
					{
						rpc.UnPause();
					}
				}
			}

			// Token: 0x0600791E RID: 31006 RVA: 0x001C42C8 File Offset: 0x001C24C8
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult>(result);
			}

			// Token: 0x0600791F RID: 31007 RVA: 0x001C42D4 File Offset: 0x001C24D4
			private bool Complete(TimeSpan timeout)
			{
				IAsyncResult asyncResult = null;
				if (this.currentTransaction != null)
				{
					using (TransactionScope transactionScope = new TransactionScope(this.currentTransaction))
					{
						ReceiveContextRPCFacet.TransactionOutcomeListener.EnsureReceiveContextAbandonOnTransactionRollback(this.receiveContext, this.currentTransaction, this.channelHandler);
						asyncResult = this.receiveContext.BeginComplete(timeout, ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult.completeCallback, this);
						transactionScope.Complete();
						goto IL_6B;
					}
				}
				asyncResult = this.receiveContext.BeginComplete(timeout, ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult.completeCallback, this);
				IL_6B:
				return asyncResult.CompletedSynchronously && ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult.HandleComplete(asyncResult);
			}

			// Token: 0x06007920 RID: 31008 RVA: 0x001C436C File Offset: 0x001C256C
			private static bool HandleComplete(IAsyncResult result)
			{
				ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult acknowledgementCompleteAsyncResult = (ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult)result.AsyncState;
				acknowledgementCompleteAsyncResult.receiveContext.EndComplete(result);
				return true;
			}

			// Token: 0x06007921 RID: 31009 RVA: 0x001C4394 File Offset: 0x001C2594
			private static void CompleteCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception exception = null;
				bool flag = true;
				try
				{
					flag = ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult.HandleComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				if (flag)
				{
					ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult acknowledgementCompleteAsyncResult = (ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult)result.AsyncState;
					acknowledgementCompleteAsyncResult.resumableRPC.Resume();
					acknowledgementCompleteAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x040044F4 RID: 17652
			private static AsyncCallback completeCallback = Fx.ThunkCallback(new AsyncCallback(ReceiveContextRPCFacet.AcknowledgementCompleteAsyncResult.CompleteCallback));

			// Token: 0x040044F5 RID: 17653
			private IResumeMessageRpc resumableRPC;

			// Token: 0x040044F6 RID: 17654
			private ReceiveContext receiveContext;

			// Token: 0x040044F7 RID: 17655
			private Transaction currentTransaction;

			// Token: 0x040044F8 RID: 17656
			private ChannelHandler channelHandler;
		}

		// Token: 0x02000CA3 RID: 3235
		private class TransactionOutcomeListener
		{
			// Token: 0x06007923 RID: 31011 RVA: 0x001C4410 File Offset: 0x001C2610
			public TransactionOutcomeListener(ReceiveContext receiveContext, Transaction transaction, ChannelHandler handler)
			{
				this.receiveContext = receiveContext;
				transaction.TransactionCompleted += this.OnTransactionComplete;
				this.channelHandler = handler;
			}

			// Token: 0x06007924 RID: 31012 RVA: 0x001C4438 File Offset: 0x001C2638
			public static void EnsureReceiveContextAbandonOnTransactionRollback(ReceiveContext receiveContext, Transaction transaction, ChannelHandler channelHandler)
			{
				new ReceiveContextRPCFacet.TransactionOutcomeListener(receiveContext, transaction, channelHandler);
			}

			// Token: 0x06007925 RID: 31013 RVA: 0x001C4444 File Offset: 0x001C2644
			private void OnTransactionComplete(object sender, TransactionEventArgs e)
			{
				if (e.Transaction.TransactionInformation.Status == TransactionStatus.Aborted)
				{
					try
					{
						IAsyncResult asyncResult = this.receiveContext.BeginAbandon(TimeSpan.MaxValue, ReceiveContextRPCFacet.TransactionOutcomeListener.abandonCallback, new ReceiveContextRPCFacet.TransactionOutcomeListener.CallbackState
						{
							ChannelHandler = this.channelHandler,
							ReceiveContext = this.receiveContext
						});
						if (asyncResult.CompletedSynchronously)
						{
							this.receiveContext.EndAbandon(asyncResult);
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						this.channelHandler.HandleError(ex);
					}
				}
			}

			// Token: 0x06007926 RID: 31014 RVA: 0x001C44D8 File Offset: 0x001C26D8
			private static void AbandonCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ReceiveContextRPCFacet.TransactionOutcomeListener.CallbackState callbackState = (ReceiveContextRPCFacet.TransactionOutcomeListener.CallbackState)result.AsyncState;
				try
				{
					callbackState.ReceiveContext.EndAbandon(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					callbackState.ChannelHandler.HandleError(ex);
				}
			}

			// Token: 0x040044F9 RID: 17657
			private static AsyncCallback abandonCallback = Fx.ThunkCallback(new AsyncCallback(ReceiveContextRPCFacet.TransactionOutcomeListener.AbandonCallback));

			// Token: 0x040044FA RID: 17658
			private ReceiveContext receiveContext;

			// Token: 0x040044FB RID: 17659
			private ChannelHandler channelHandler;

			// Token: 0x02000F3F RID: 3903
			private class CallbackState
			{
				// Token: 0x17001D88 RID: 7560
				// (get) Token: 0x060086A8 RID: 34472 RVA: 0x001F2F2C File Offset: 0x001F112C
				// (set) Token: 0x060086A9 RID: 34473 RVA: 0x001F2F34 File Offset: 0x001F1134
				public ChannelHandler ChannelHandler { get; set; }

				// Token: 0x17001D89 RID: 7561
				// (get) Token: 0x060086AA RID: 34474 RVA: 0x001F2F3D File Offset: 0x001F113D
				// (set) Token: 0x060086AB RID: 34475 RVA: 0x001F2F45 File Offset: 0x001F1145
				public ReceiveContext ReceiveContext { get; set; }
			}
		}
	}
}
