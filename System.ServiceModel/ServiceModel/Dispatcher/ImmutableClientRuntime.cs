using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Transactions;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200059A RID: 1434
	internal class ImmutableClientRuntime
	{
		// Token: 0x0600378B RID: 14219 RVA: 0x000D63AC File Offset: 0x000D45AC
		internal ImmutableClientRuntime(ClientRuntime behavior)
		{
			this.channelInitializers = EmptyArray<IChannelInitializer>.ToArray(behavior.ChannelInitializers);
			this.interactiveChannelInitializers = EmptyArray<IInteractiveChannelInitializer>.ToArray(behavior.InteractiveChannelInitializers);
			this.messageInspectors = EmptyArray<IClientMessageInspector>.ToArray(behavior.MessageInspectors);
			this.operationSelector = behavior.OperationSelector;
			this.useSynchronizationContext = behavior.UseSynchronizationContext;
			this.validateMustUnderstand = behavior.ValidateMustUnderstand;
			this.unhandled = new ProxyOperationRuntime(behavior.UnhandledClientOperation, this);
			this.addTransactionFlowProperties = behavior.AddTransactionFlowProperties;
			this.operations = new Dictionary<string, ProxyOperationRuntime>();
			for (int i = 0; i < behavior.Operations.Count; i++)
			{
				ClientOperation clientOperation = behavior.Operations[i];
				ProxyOperationRuntime value = new ProxyOperationRuntime(clientOperation, this);
				this.operations.Add(clientOperation.Name, value);
			}
			this.correlationCount = this.messageInspectors.Length + behavior.MaxParameterInspectors;
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x0600378C RID: 14220 RVA: 0x000D6491 File Offset: 0x000D4691
		internal int MessageInspectorCorrelationOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x0600378D RID: 14221 RVA: 0x000D6494 File Offset: 0x000D4694
		internal int ParameterInspectorCorrelationOffset
		{
			get
			{
				return this.messageInspectors.Length;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x0600378E RID: 14222 RVA: 0x000D649E File Offset: 0x000D469E
		internal int CorrelationCount
		{
			get
			{
				return this.correlationCount;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x0600378F RID: 14223 RVA: 0x000D64A6 File Offset: 0x000D46A6
		internal IClientOperationSelector OperationSelector
		{
			get
			{
				return this.operationSelector;
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06003790 RID: 14224 RVA: 0x000D64AE File Offset: 0x000D46AE
		internal ProxyOperationRuntime UnhandledProxyOperation
		{
			get
			{
				return this.unhandled;
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x000D64B6 File Offset: 0x000D46B6
		internal bool UseSynchronizationContext
		{
			get
			{
				return this.useSynchronizationContext;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x000D64BE File Offset: 0x000D46BE
		// (set) Token: 0x06003793 RID: 14227 RVA: 0x000D64C6 File Offset: 0x000D46C6
		internal bool ValidateMustUnderstand
		{
			get
			{
				return this.validateMustUnderstand;
			}
			set
			{
				this.validateMustUnderstand = value;
			}
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x000D64D0 File Offset: 0x000D46D0
		internal void AfterReceiveReply(ref ProxyRpc rpc)
		{
			int messageInspectorCorrelationOffset = this.MessageInspectorCorrelationOffset;
			bool flag = DS.MessageInspectorIsEnabled();
			Stopwatch stopwatch = null;
			if (flag)
			{
				stopwatch = new Stopwatch();
			}
			try
			{
				for (int i = 0; i < this.messageInspectors.Length; i++)
				{
					if (flag)
					{
						stopwatch.Restart();
					}
					this.messageInspectors[i].AfterReceiveReply(ref rpc.Reply, rpc.Correlation[messageInspectorCorrelationOffset + i]);
					if (flag)
					{
						DS.ClientMessageInspectorAfterReceive(this.messageInspectors[i].GetType(), stopwatch.Elapsed);
					}
					if (TD.ClientMessageInspectorAfterReceiveInvokedIsEnabled())
					{
						TD.ClientMessageInspectorAfterReceiveInvoked(rpc.EventTraceActivity, this.messageInspectors[i].GetType().FullName);
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (ErrorBehavior.ShouldRethrowClientSideExceptionAsIs(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
			}
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000D65A8 File Offset: 0x000D47A8
		internal void BeforeSendRequest(ref ProxyRpc rpc)
		{
			int messageInspectorCorrelationOffset = this.MessageInspectorCorrelationOffset;
			try
			{
				bool flag = DS.MessageInspectorIsEnabled();
				Stopwatch stopwatch = null;
				if (flag)
				{
					stopwatch = new Stopwatch();
				}
				for (int i = 0; i < this.messageInspectors.Length; i++)
				{
					if (flag)
					{
						stopwatch.Restart();
					}
					rpc.Correlation[messageInspectorCorrelationOffset + i] = this.messageInspectors[i].BeforeSendRequest(ref rpc.Request, (IClientChannel)rpc.Channel.Proxy);
					if (flag)
					{
						DS.ClientMessageInspectorBeforeSend(this.messageInspectors[i].GetType(), stopwatch.Elapsed);
					}
					if (TD.ClientMessageInspectorBeforeSendInvokedIsEnabled())
					{
						TD.ClientMessageInspectorBeforeSendInvoked(rpc.EventTraceActivity, this.messageInspectors[i].GetType().FullName);
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (ErrorBehavior.ShouldRethrowClientSideExceptionAsIs(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
			}
			if (this.addTransactionFlowProperties)
			{
				ImmutableClientRuntime.SendTransaction(ref rpc);
			}
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000D66A0 File Offset: 0x000D48A0
		internal void DisplayInitializationUI(ServiceChannel channel)
		{
			this.EndDisplayInitializationUI(this.BeginDisplayInitializationUI(channel, null, null));
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000D66B1 File Offset: 0x000D48B1
		internal IAsyncResult BeginDisplayInitializationUI(ServiceChannel channel, AsyncCallback callback, object state)
		{
			return new ImmutableClientRuntime.DisplayInitializationUIAsyncResult(channel, this.interactiveChannelInitializers, callback, state);
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000D66C1 File Offset: 0x000D48C1
		internal void EndDisplayInitializationUI(IAsyncResult result)
		{
			ImmutableClientRuntime.DisplayInitializationUIAsyncResult.End(result);
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000D66C9 File Offset: 0x000D48C9
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void SendTransaction(ref ProxyRpc rpc)
		{
			TransactionFlowProperty.Set(Transaction.Current, rpc.Request);
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x000D66DC File Offset: 0x000D48DC
		internal void InitializeChannel(IClientChannel channel)
		{
			try
			{
				for (int i = 0; i < this.channelInitializers.Length; i++)
				{
					this.channelInitializers[i].Initialize(channel);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (ErrorBehavior.ShouldRethrowClientSideExceptionAsIs(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
			}
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000D6740 File Offset: 0x000D4940
		internal ProxyOperationRuntime GetOperation(MethodBase methodBase, object[] args, out bool canCacheResult)
		{
			if (this.operationSelector == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SFxNeedProxyBehaviorOperationSelector2", new object[]
				{
					methodBase.Name,
					methodBase.DeclaringType.Name
				})));
			}
			ProxyOperationRuntime result;
			try
			{
				if (this.operationSelector.AreParametersRequiredForSelection)
				{
					canCacheResult = false;
				}
				else
				{
					args = null;
					canCacheResult = true;
				}
				bool flag = DS.OperationSelectorIsEnabled();
				Stopwatch stopwatch = null;
				if (flag)
				{
					stopwatch = Stopwatch.StartNew();
				}
				string text = this.operationSelector.SelectOperation(methodBase, args);
				if (flag)
				{
					DS.ClientSelectOperation(this.operationSelector.GetType(), text, stopwatch.Elapsed);
				}
				ProxyOperationRuntime proxyOperationRuntime;
				if (text != null && this.operations.TryGetValue(text, out proxyOperationRuntime))
				{
					result = proxyOperationRuntime;
				}
				else
				{
					result = null;
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (ErrorBehavior.ShouldRethrowClientSideExceptionAsIs(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
			}
			return result;
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000D6834 File Offset: 0x000D4A34
		internal ProxyOperationRuntime GetOperationByName(string operationName)
		{
			ProxyOperationRuntime result = null;
			if (this.operations.TryGetValue(operationName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x04002954 RID: 10580
		private int correlationCount;

		// Token: 0x04002955 RID: 10581
		private bool addTransactionFlowProperties;

		// Token: 0x04002956 RID: 10582
		private IInteractiveChannelInitializer[] interactiveChannelInitializers;

		// Token: 0x04002957 RID: 10583
		private IClientOperationSelector operationSelector;

		// Token: 0x04002958 RID: 10584
		private IChannelInitializer[] channelInitializers;

		// Token: 0x04002959 RID: 10585
		private IClientMessageInspector[] messageInspectors;

		// Token: 0x0400295A RID: 10586
		private Dictionary<string, ProxyOperationRuntime> operations;

		// Token: 0x0400295B RID: 10587
		private ProxyOperationRuntime unhandled;

		// Token: 0x0400295C RID: 10588
		private bool useSynchronizationContext;

		// Token: 0x0400295D RID: 10589
		private bool validateMustUnderstand;

		// Token: 0x02000CA4 RID: 3236
		private class DisplayInitializationUIAsyncResult : AsyncResult
		{
			// Token: 0x06007928 RID: 31016 RVA: 0x001C454C File Offset: 0x001C274C
			internal DisplayInitializationUIAsyncResult(ServiceChannel channel, IInteractiveChannelInitializer[] initializers, AsyncCallback callback, object state) : base(callback, state)
			{
				this.channel = channel;
				this.initializers = initializers;
				this.proxy = (channel.Proxy as IClientChannel);
				this.CallBegin(true);
			}

			// Token: 0x06007929 RID: 31017 RVA: 0x001C4584 File Offset: 0x001C2784
			private void CallBegin(bool completedSynchronously)
			{
				Exception ex;
				for (;;)
				{
					int num = this.index + 1;
					this.index = num;
					if (num >= this.initializers.Length)
					{
						goto Block_5;
					}
					IAsyncResult asyncResult = null;
					ex = null;
					try
					{
						asyncResult = this.initializers[this.index].BeginDisplayInitializationUI(this.proxy, ImmutableClientRuntime.DisplayInitializationUIAsyncResult.callback, this);
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
					if (ex == null)
					{
						if (!asyncResult.CompletedSynchronously)
						{
							break;
						}
						this.CallEnd(asyncResult, out ex);
					}
					if (ex != null)
					{
						goto Block_4;
					}
				}
				return;
				Block_4:
				this.CallComplete(completedSynchronously, ex);
				return;
				Block_5:
				this.CallComplete(completedSynchronously, null);
			}

			// Token: 0x0600792A RID: 31018 RVA: 0x001C461C File Offset: 0x001C281C
			private static void Callback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ImmutableClientRuntime.DisplayInitializationUIAsyncResult displayInitializationUIAsyncResult = (ImmutableClientRuntime.DisplayInitializationUIAsyncResult)result.AsyncState;
				Exception ex = null;
				displayInitializationUIAsyncResult.CallEnd(result, out ex);
				if (ex != null)
				{
					displayInitializationUIAsyncResult.CallComplete(false, ex);
					return;
				}
				displayInitializationUIAsyncResult.CallBegin(false);
			}

			// Token: 0x0600792B RID: 31019 RVA: 0x001C465C File Offset: 0x001C285C
			private void CallEnd(IAsyncResult result, out Exception exception)
			{
				try
				{
					this.initializers[this.index].EndDisplayInitializationUI(result);
					exception = null;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
			}

			// Token: 0x0600792C RID: 31020 RVA: 0x001C46A4 File Offset: 0x001C28A4
			private void CallComplete(bool completedSynchronously, Exception exception)
			{
				base.Complete(completedSynchronously, exception);
			}

			// Token: 0x0600792D RID: 31021 RVA: 0x001C46AE File Offset: 0x001C28AE
			internal static void End(IAsyncResult result)
			{
				AsyncResult.End<ImmutableClientRuntime.DisplayInitializationUIAsyncResult>(result);
			}

			// Token: 0x040044FC RID: 17660
			private ServiceChannel channel;

			// Token: 0x040044FD RID: 17661
			private int index = -1;

			// Token: 0x040044FE RID: 17662
			private IInteractiveChannelInitializer[] initializers;

			// Token: 0x040044FF RID: 17663
			private IClientChannel proxy;

			// Token: 0x04004500 RID: 17664
			private static AsyncCallback callback = Fx.ThunkCallback(new AsyncCallback(ImmutableClientRuntime.DisplayInitializationUIAsyncResult.Callback));
		}
	}
}
