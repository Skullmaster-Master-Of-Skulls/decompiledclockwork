using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime;
using System.ServiceModel.Channels;
using System.Threading;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000014 RID: 20
	internal abstract class DiscoveryClientChannelBase<TChannel> : ChannelBase where TChannel : class, IChannel
	{
		// Token: 0x0600012B RID: 299 RVA: 0x000053D0 File Offset: 0x000035D0
		public DiscoveryClientChannelBase(ChannelManagerBase channelManagerBase, IChannelFactory<TChannel> innerChannelFactory, FindCriteria findCriteria, DiscoveryEndpointProvider discoveryEndpointProvider) : base(channelManagerBase)
		{
			this.innerChannelFactory = innerChannelFactory;
			this.findCriteria = findCriteria;
			this.discoveryEndpointProvider = discoveryEndpointProvider;
			this.discoveredEndpoints = new InputQueue<EndpointDiscoveryMetadata>();
			this.totalExpectedEndpoints = int.MaxValue;
			this.totalDiscoveredEndpoints = 0;
			this.discoveryCompleted = false;
			this.thisLock = new object();
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005429 File Offset: 0x00003629
		protected TChannel InnerChannel
		{
			get
			{
				return this.innerChannel;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005434 File Offset: 0x00003634
		public override T GetProperty<T>()
		{
			T property = base.GetProperty<T>();
			if (property != null)
			{
				return property;
			}
			if (this.innerChannel != null)
			{
				return this.InnerChannel.GetProperty<T>();
			}
			return default(T);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005479 File Offset: 0x00003679
		protected override void OnOpen(TimeSpan timeout)
		{
			this.innerChannel = this.BuildChannel(timeout);
			this.innerChannel.Faulted += this.OnInnerChannelFaulted;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000054A4 File Offset: 0x000036A4
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000054AF File Offset: 0x000036AF
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.innerChannel = DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult.End(result);
			this.innerChannel.Faulted += this.OnInnerChannelFaulted;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000054D9 File Offset: 0x000036D9
		protected override void OnClosing()
		{
			if (this.innerChannel != null)
			{
				this.innerChannel.Faulted -= this.OnInnerChannelFaulted;
			}
			base.OnClosing();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000550A File Offset: 0x0000370A
		protected override void OnClose(TimeSpan timeout)
		{
			if (this.innerChannel != null)
			{
				this.innerChannel.Close(timeout);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000552A File Offset: 0x0000372A
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new DiscoveryClientChannelBase<TChannel>.CloseAsyncResult(this.innerChannel, timeout, callback, state);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000553A File Offset: 0x0000373A
		protected override void OnEndClose(IAsyncResult result)
		{
			DiscoveryClientChannelBase<TChannel>.CloseAsyncResult.End(result);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005542 File Offset: 0x00003742
		protected override void OnAbort()
		{
			if (this.innerChannel != null)
			{
				this.innerChannel.Abort();
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005561 File Offset: 0x00003761
		private void OnInnerChannelFaulted(object sender, EventArgs e)
		{
			base.Fault();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000556C File Offset: 0x0000376C
		public TChannel BuildChannel(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.InitializeAndFindAsync();
			TChannel result = default(TChannel);
			bool flag = false;
			EndpointDiscoveryMetadata endpointDiscoveryMetadata = null;
			try
			{
				for (;;)
				{
					try
					{
						endpointDiscoveryMetadata = this.discoveredEndpoints.Dequeue(timeoutHelper.RemainingTime());
					}
					catch (TimeoutException innerException)
					{
						throw FxTrace.Exception.AsError(new TimeoutException(SR.DiscoveryClientChannelOpenTimeout(timeoutHelper.OriginalTimeout), innerException));
					}
					if (endpointDiscoveryMetadata == null)
					{
						break;
					}
					if (timeoutHelper.RemainingTime() == TimeSpan.Zero)
					{
						goto Block_5;
					}
					if (endpointDiscoveryMetadata.ListenUris.Count == 0)
					{
						flag = this.CreateChannel(ref result, endpointDiscoveryMetadata.Address, endpointDiscoveryMetadata.Address.Uri, timeoutHelper);
					}
					else
					{
						foreach (Uri via in endpointDiscoveryMetadata.ListenUris)
						{
							flag = this.CreateChannel(ref result, endpointDiscoveryMetadata.Address, via, timeoutHelper);
							if (flag)
							{
								break;
							}
						}
					}
					if (flag)
					{
						goto Block_8;
					}
				}
				if (this.totalDiscoveredEndpoints < 1)
				{
					throw FxTrace.Exception.AsError(new EndpointNotFoundException(SR.DiscoveryClientChannelEndpointNotFound, this.exception));
				}
				throw FxTrace.Exception.AsError(new EndpointNotFoundException(SR.DiscoveryClientChannelCreationFailed(this.totalDiscoveredEndpoints), this.exception));
				Block_5:
				throw FxTrace.Exception.AsError(new TimeoutException(SR.DiscoveryClientChannelOpenTimeout(timeoutHelper.OriginalTimeout)));
				Block_8:;
			}
			finally
			{
				if (flag && TD.InnerChannelOpenSucceededIsEnabled())
				{
					TD.InnerChannelOpenSucceeded(endpointDiscoveryMetadata.Address.ToString(), DiscoveryClientChannelBase<TChannel>.GetVia(result).ToString());
				}
				this.Cleanup(timeoutHelper.RemainingTime());
			}
			return result;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005748 File Offset: 0x00003948
		private bool CreateChannel(ref TChannel innerChannel, EndpointAddress to, Uri via, TimeoutHelper timeoutHelper)
		{
			bool flag = false;
			Exception ex = null;
			try
			{
				innerChannel = this.innerChannelFactory.CreateChannel(to, via);
				innerChannel.Open(timeoutHelper.RemainingTime());
				flag = true;
			}
			catch (TimeoutException innerException)
			{
				throw FxTrace.Exception.AsError(new TimeoutException(SR.DiscoveryClientChannelOpenTimeout(timeoutHelper.OriginalTimeout), innerException));
			}
			catch (CommunicationException ex2)
			{
				ex = ex2;
			}
			catch (ArgumentException ex3)
			{
				ex = ex3;
			}
			catch (InvalidOperationException ex4)
			{
				ex = ex4;
			}
			finally
			{
				if (ex != null)
				{
					DiscoveryClientChannelBase<TChannel>.TraceInnerChannelFailure(innerChannel, to, via, ex);
				}
				if (!flag && innerChannel != null)
				{
					innerChannel.Abort();
					innerChannel = default(TChannel);
				}
			}
			return flag;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005830 File Offset: 0x00003A30
		private void OnFindProgressChanged(object sender, FindProgressChangedEventArgs e)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (!this.discoveryCompleted)
				{
					this.discoveredEndpoints.EnqueueAndDispatch(e.EndpointDiscoveryMetadata, null, false);
					int num = this.totalDiscoveredEndpoints + 1;
					this.totalDiscoveredEndpoints = num;
					if (num == this.totalExpectedEndpoints)
					{
						this.discoveryCompleted = true;
						this.discoveredEndpoints.Shutdown();
					}
				}
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000058B0 File Offset: 0x00003AB0
		private void OnFindCompleted(object sender, FindCompletedEventArgs e)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (!this.discoveryCompleted)
				{
					if (e.Error != null || e.Cancelled || this.totalDiscoveredEndpoints == e.Result.Endpoints.Count)
					{
						this.exception = e.Error;
						this.discoveryCompleted = true;
						this.discoveredEndpoints.Shutdown();
					}
					else
					{
						this.totalExpectedEndpoints = e.Result.Endpoints.Count;
					}
				}
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005950 File Offset: 0x00003B50
		private void InitializeAndFindAsync()
		{
			DiscoveryEndpoint discoveryEndpoint = this.discoveryEndpointProvider.GetDiscoveryEndpoint();
			if (discoveryEndpoint == null)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryMethodImplementationReturnsNull("GetDiscoveryEndpoint", this.discoveryEndpointProvider.GetType())));
			}
			this.discoveryClient = new DiscoveryClient(discoveryEndpoint);
			this.discoveryClient.FindProgressChanged += this.OnFindProgressChanged;
			this.discoveryClient.FindCompleted += this.OnFindCompleted;
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			if (synchronizationContext != null)
			{
				SynchronizationContext.SetSynchronizationContext(null);
				if (TD.SynchronizationContextSetToNullIsEnabled())
				{
					TD.SynchronizationContextSetToNull();
				}
			}
			try
			{
				this.discoveryClient.FindAsync(this.findCriteria, this);
			}
			finally
			{
				if (synchronizationContext != null)
				{
					SynchronizationContext.SetSynchronizationContext(synchronizationContext);
					if (TD.SynchronizationContextResetIsEnabled())
					{
						TD.SynchronizationContextReset(synchronizationContext.GetType().ToString());
					}
				}
			}
			if (TD.FindInitiatedInDiscoveryClientChannelIsEnabled())
			{
				TD.FindInitiatedInDiscoveryClientChannel();
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005A38 File Offset: 0x00003C38
		private void Cleanup(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			Exception ex = null;
			object obj = this.thisLock;
			lock (obj)
			{
				this.discoveryCompleted = true;
			}
			try
			{
				this.discoveryClient.CancelAsync(this);
				((ICommunicationObject)this.discoveryClient).Close(timeoutHelper.RemainingTime());
			}
			catch (TimeoutException ex2)
			{
				ex = ex2;
			}
			catch (CommunicationException ex3)
			{
				ex = ex3;
			}
			finally
			{
				if (ex != null && TD.DiscoveryClientInClientChannelFailedToCloseIsEnabled())
				{
					TD.DiscoveryClientInClientChannelFailedToClose(ex);
				}
			}
			this.discoveredEndpoints.Dispose();
			this.discoveryClient = null;
			this.discoveredEndpoints = null;
			this.findCriteria = null;
			this.discoveryEndpointProvider = null;
			this.innerChannelFactory = null;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005B18 File Offset: 0x00003D18
		private static void TraceInnerChannelFailure(TChannel innerChannel, EndpointAddress to, Uri via, Exception exception)
		{
			if (innerChannel == null && TD.InnerChannelCreationFailedIsEnabled())
			{
				TD.InnerChannelCreationFailed(to.ToString(), via.ToString(), exception);
				return;
			}
			if (innerChannel != null && TD.InnerChannelOpenFailedIsEnabled())
			{
				TD.InnerChannelOpenFailed(to.ToString(), via.ToString(), exception);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005B68 File Offset: 0x00003D68
		private static Uri GetVia(TChannel innerChannel)
		{
			IOutputChannel outputChannel = innerChannel as IOutputChannel;
			if (outputChannel != null)
			{
				return outputChannel.Via;
			}
			IRequestChannel requestChannel = innerChannel as IRequestChannel;
			if (requestChannel != null)
			{
				return requestChannel.Via;
			}
			return null;
		}

		// Token: 0x04000050 RID: 80
		private TChannel innerChannel;

		// Token: 0x04000051 RID: 81
		private IChannelFactory<TChannel> innerChannelFactory;

		// Token: 0x04000052 RID: 82
		private FindCriteria findCriteria;

		// Token: 0x04000053 RID: 83
		private DiscoveryEndpointProvider discoveryEndpointProvider;

		// Token: 0x04000054 RID: 84
		private DiscoveryClient discoveryClient;

		// Token: 0x04000055 RID: 85
		private InputQueue<EndpointDiscoveryMetadata> discoveredEndpoints;

		// Token: 0x04000056 RID: 86
		private Exception exception;

		// Token: 0x04000057 RID: 87
		private int totalExpectedEndpoints;

		// Token: 0x04000058 RID: 88
		private int totalDiscoveredEndpoints;

		// Token: 0x04000059 RID: 89
		private bool discoveryCompleted;

		// Token: 0x0400005A RID: 90
		private object thisLock;

		// Token: 0x020000CB RID: 203
		private sealed class DiscoveryChannelBuilderAsyncResult : IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>
		{
			// Token: 0x060007D9 RID: 2009 RVA: 0x000147C4 File Offset: 0x000129C4
			public DiscoveryChannelBuilderAsyncResult(DiscoveryClientChannelBase<TChannel> discoveryClientChannelBase, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.discoveryClientChannelBase = discoveryClientChannelBase;
				base.Start(this, timeout);
			}

			// Token: 0x060007DA RID: 2010 RVA: 0x000147E0 File Offset: 0x000129E0
			public static TChannel End(IAsyncResult result)
			{
				DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult discoveryChannelBuilderAsyncResult = AsyncResult.End<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>(result);
				discoveryChannelBuilderAsyncResult.discoveryClientChannelBase.Cleanup(discoveryChannelBuilderAsyncResult.RemainingTime());
				return discoveryChannelBuilderAsyncResult.innerChannel;
			}

			// Token: 0x060007DB RID: 2011 RVA: 0x0001480B File Offset: 0x00012A0B
			protected override IEnumerator<IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.AsyncStep> GetAsyncSteps()
			{
				this.discoveryClientChannelBase.InitializeAndFindAsync();
				Exception ex;
				for (;;)
				{
					this.currentEndpointDiscoveryMetadata = null;
					yield return DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult.GetDequeueStep();
					ex = this.CheckEndpointDiscoveryMetadataAndGetException();
					if (ex != null)
					{
						break;
					}
					bool checkListenUris = this.currentEndpointDiscoveryMetadata.ListenUris.Count > 0;
					int index = 0;
					do
					{
						if (checkListenUris)
						{
							EndpointAddress address = this.currentEndpointDiscoveryMetadata.Address;
							Collection<Uri> listenUris = this.currentEndpointDiscoveryMetadata.ListenUris;
							int num = index;
							index = num + 1;
							this.CreateChannel(address, listenUris[num]);
						}
						else
						{
							this.CreateChannel(this.currentEndpointDiscoveryMetadata.Address, this.currentEndpointDiscoveryMetadata.Address.Uri);
						}
						if (this.innerChannel != null)
						{
							yield return DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult.GetOpenStep();
							if (this.innerChannel != null)
							{
								goto Block_4;
							}
						}
					}
					while (index < this.currentEndpointDiscoveryMetadata.ListenUris.Count);
				}
				base.CompleteOnce(ex);
				yield break;
				Block_4:
				if (TD.InnerChannelOpenSucceededIsEnabled())
				{
					TD.InnerChannelOpenSucceeded(this.currentEndpointDiscoveryMetadata.Address.ToString(), DiscoveryClientChannelBase<TChannel>.GetVia(this.innerChannel).ToString());
				}
				yield break;
				yield break;
			}

			// Token: 0x060007DC RID: 2012 RVA: 0x0001481C File Offset: 0x00012A1C
			private static IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.AsyncStep GetDequeueStep()
			{
				if (DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult.dequeueStep == null)
				{
					DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult.dequeueStep = IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.CallAsync((DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult thisPtr, TimeSpan t, AsyncCallback c, object s) => thisPtr.discoveryClientChannelBase.discoveredEndpoints.BeginDequeue(thisPtr.RemainingTime(), c, s), delegate(DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult thisPtr, IAsyncResult r)
					{
						thisPtr.currentEndpointDiscoveryMetadata = thisPtr.discoveryClientChannelBase.discoveredEndpoints.EndDequeue(r);
					}, new IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.IAsyncCatch[]
					{
						new IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.AsyncCatch<TimeoutException>(new IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.ExceptionHandler<TimeoutException>(DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult.HandleTimeoutException))
					});
				}
				return DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult.dequeueStep;
			}

			// Token: 0x060007DD RID: 2013 RVA: 0x00014898 File Offset: 0x00012A98
			private static IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.AsyncStep GetOpenStep()
			{
				if (DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult.openStep == null)
				{
					DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult.openStep = IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.CallAsync((DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult thisPtr, TimeSpan t, AsyncCallback c, object s) => thisPtr.innerChannel.BeginOpen(thisPtr.RemainingTime(), c, s), delegate(DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult thisPtr, IAsyncResult r)
					{
						thisPtr.innerChannel.EndOpen(r);
					}, new IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.IAsyncCatch[]
					{
						new IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.AsyncCatch<TimeoutException>(new IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.ExceptionHandler<TimeoutException>(DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult.HandleTimeoutException)),
						new IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.AsyncCatch<CommunicationException>(new IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.ExceptionHandler<CommunicationException>(DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult.HandleCommunicationException)),
						new IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.AsyncCatch<Exception>(new IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.ExceptionHandler<Exception>(DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult.HandleException))
					});
				}
				return DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult.openStep;
			}

			// Token: 0x060007DE RID: 2014 RVA: 0x00014940 File Offset: 0x00012B40
			private static Exception HandleTimeoutException(DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult thisPtr, TimeoutException e)
			{
				if (thisPtr.innerChannel != null)
				{
					thisPtr.innerChannel.Abort();
					thisPtr.innerChannel = default(TChannel);
				}
				return new TimeoutException(SR.DiscoveryClientChannelOpenTimeout(thisPtr.OriginalTimeout), e);
			}

			// Token: 0x060007DF RID: 2015 RVA: 0x0001498C File Offset: 0x00012B8C
			private static Exception HandleException(DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult thisPtr, Exception e)
			{
				if (thisPtr.innerChannel != null)
				{
					thisPtr.innerChannel.Abort();
					thisPtr.innerChannel = default(TChannel);
				}
				return e;
			}

			// Token: 0x060007E0 RID: 2016 RVA: 0x000149B8 File Offset: 0x00012BB8
			private static Exception HandleCommunicationException(DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult thisPtr, CommunicationException e)
			{
				if (thisPtr.innerChannel != null)
				{
					thisPtr.innerChannel.Abort();
					if (TD.InnerChannelOpenFailedIsEnabled())
					{
						TD.InnerChannelOpenFailed(thisPtr.currentEndpointDiscoveryMetadata.Address.ToString(), DiscoveryClientChannelBase<TChannel>.GetVia(thisPtr.innerChannel).ToString(), e);
					}
					thisPtr.innerChannel = default(TChannel);
				}
				return null;
			}

			// Token: 0x060007E1 RID: 2017 RVA: 0x00014A1C File Offset: 0x00012C1C
			private void CreateChannel(EndpointAddress address, Uri listenUri)
			{
				Exception ex = null;
				try
				{
					this.innerChannel = this.discoveryClientChannelBase.innerChannelFactory.CreateChannel(address, listenUri);
				}
				catch (ArgumentException ex2)
				{
					ex = ex2;
				}
				catch (InvalidOperationException ex3)
				{
					ex = ex3;
				}
				catch (CommunicationException ex4)
				{
					ex = ex4;
					base.CompleteOnce(ex4);
				}
				finally
				{
					if (ex != null && TD.InnerChannelCreationFailedIsEnabled())
					{
						TD.InnerChannelCreationFailed(address.ToString(), listenUri.ToString(), ex);
					}
				}
			}

			// Token: 0x060007E2 RID: 2018 RVA: 0x00014AAC File Offset: 0x00012CAC
			private Exception CheckEndpointDiscoveryMetadataAndGetException()
			{
				if (base.RemainingTime() == TimeSpan.Zero)
				{
					return new TimeoutException(SR.DiscoveryClientChannelOpenTimeout(base.OriginalTimeout));
				}
				if (this.currentEndpointDiscoveryMetadata == null)
				{
					string message = (this.discoveryClientChannelBase.totalDiscoveredEndpoints < 1) ? SR.DiscoveryClientChannelEndpointNotFound : SR.DiscoveryClientChannelCreationFailed(this.discoveryClientChannelBase.totalDiscoveredEndpoints);
					return new EndpointNotFoundException(message, this.discoveryClientChannelBase.exception);
				}
				return null;
			}

			// Token: 0x040001F0 RID: 496
			private static IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.AsyncStep openStep;

			// Token: 0x040001F1 RID: 497
			private static IteratorAsyncResult<DiscoveryClientChannelBase<TChannel>.DiscoveryChannelBuilderAsyncResult>.AsyncStep dequeueStep;

			// Token: 0x040001F2 RID: 498
			private TChannel innerChannel;

			// Token: 0x040001F3 RID: 499
			private EndpointDiscoveryMetadata currentEndpointDiscoveryMetadata;

			// Token: 0x040001F4 RID: 500
			private DiscoveryClientChannelBase<TChannel> discoveryClientChannelBase;
		}

		// Token: 0x020000CC RID: 204
		private class CloseAsyncResult : AsyncResult
		{
			// Token: 0x060007E3 RID: 2019 RVA: 0x00014B28 File Offset: 0x00012D28
			public CloseAsyncResult(TChannel innerChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.innerChannel = innerChannel;
				if (this.innerChannel != null)
				{
					IAsyncResult asyncResult = this.innerChannel.BeginClose(timeout, base.PrepareAsyncCompletion(new AsyncResult.AsyncCompletion(this.OnCloseCompleted)), this);
					if (asyncResult.CompletedSynchronously && this.OnCloseCompleted(asyncResult))
					{
						base.Complete(true);
						return;
					}
				}
				else
				{
					base.Complete(true);
				}
			}

			// Token: 0x060007E4 RID: 2020 RVA: 0x00014B97 File Offset: 0x00012D97
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<DiscoveryClientChannelBase<TChannel>.CloseAsyncResult>(result);
			}

			// Token: 0x060007E5 RID: 2021 RVA: 0x00014BA0 File Offset: 0x00012DA0
			private bool OnCloseCompleted(IAsyncResult result)
			{
				this.innerChannel.EndClose(result);
				return true;
			}

			// Token: 0x040001F5 RID: 501
			private TChannel innerChannel;
		}
	}
}
