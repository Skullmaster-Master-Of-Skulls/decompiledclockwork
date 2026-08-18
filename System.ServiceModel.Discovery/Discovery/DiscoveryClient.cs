using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Discovery.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000012 RID: 18
	public sealed class DiscoveryClient : ICommunicationObject, IDiscoveryInnerClientResponse, IDisposable
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00003A79 File Offset: 0x00001C79
		public DiscoveryClient() : this("*")
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003A88 File Offset: 0x00001C88
		public DiscoveryClient(string endpointConfigurationName)
		{
			if (endpointConfigurationName == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpointConfigurationName");
			}
			DiscoveryEndpoint discoveryEndpoint = ConfigurationUtility.LookupEndpointFromClientSection<DiscoveryEndpoint>(endpointConfigurationName);
			this.Initialize(discoveryEndpoint);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003ABC File Offset: 0x00001CBC
		public DiscoveryClient(DiscoveryEndpoint discoveryEndpoint)
		{
			if (discoveryEndpoint == null)
			{
				throw FxTrace.Exception.ArgumentNull("serviceDiscoveryEndpoint");
			}
			this.Initialize(discoveryEndpoint);
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060000BF RID: 191 RVA: 0x00003AE0 File Offset: 0x00001CE0
		// (remove) Token: 0x060000C0 RID: 192 RVA: 0x00003B18 File Offset: 0x00001D18
		public event EventHandler<FindCompletedEventArgs> FindCompleted;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060000C1 RID: 193 RVA: 0x00003B50 File Offset: 0x00001D50
		// (remove) Token: 0x060000C2 RID: 194 RVA: 0x00003B88 File Offset: 0x00001D88
		public event EventHandler<FindProgressChangedEventArgs> FindProgressChanged;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060000C3 RID: 195 RVA: 0x00003BC0 File Offset: 0x00001DC0
		// (remove) Token: 0x060000C4 RID: 196 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public event EventHandler<AnnouncementEventArgs> ProxyAvailable;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060000C5 RID: 197 RVA: 0x00003C30 File Offset: 0x00001E30
		// (remove) Token: 0x060000C6 RID: 198 RVA: 0x00003C68 File Offset: 0x00001E68
		public event EventHandler<ResolveCompletedEventArgs> ResolveCompleted;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060000C7 RID: 199 RVA: 0x00003C9D File Offset: 0x00001E9D
		// (remove) Token: 0x060000C8 RID: 200 RVA: 0x00003CC5 File Offset: 0x00001EC5
		event EventHandler ICommunicationObject.Opening
		{
			add
			{
				if (this.InternalOpening == null)
				{
					this.InnerCommunicationObject.Opening += this.OnInnerCommunicationObjectOpening;
				}
				this.InternalOpening += value;
			}
			remove
			{
				this.InternalOpening -= value;
				if (this.InternalOpening == null)
				{
					this.InnerCommunicationObject.Opening -= this.OnInnerCommunicationObjectOpening;
				}
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060000C9 RID: 201 RVA: 0x00003CED File Offset: 0x00001EED
		// (remove) Token: 0x060000CA RID: 202 RVA: 0x00003D15 File Offset: 0x00001F15
		event EventHandler ICommunicationObject.Opened
		{
			add
			{
				if (this.InternalOpened == null)
				{
					this.InnerCommunicationObject.Opened += this.OnInnerCommunicationObjectOpened;
				}
				this.InternalOpened += value;
			}
			remove
			{
				this.InternalOpened -= value;
				if (this.InternalOpened == null)
				{
					this.InnerCommunicationObject.Opened -= this.OnInnerCommunicationObjectOpened;
				}
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060000CB RID: 203 RVA: 0x00003D3D File Offset: 0x00001F3D
		// (remove) Token: 0x060000CC RID: 204 RVA: 0x00003D65 File Offset: 0x00001F65
		event EventHandler ICommunicationObject.Closing
		{
			add
			{
				if (this.InternalClosing == null)
				{
					this.InnerCommunicationObject.Closing += this.OnInnerCommunicationObjectClosing;
				}
				this.InternalClosing += value;
			}
			remove
			{
				this.InternalClosing -= value;
				if (this.InternalClosing == null)
				{
					this.InnerCommunicationObject.Closing -= this.OnInnerCommunicationObjectClosing;
				}
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060000CD RID: 205 RVA: 0x00003D8D File Offset: 0x00001F8D
		// (remove) Token: 0x060000CE RID: 206 RVA: 0x00003DB5 File Offset: 0x00001FB5
		event EventHandler ICommunicationObject.Closed
		{
			add
			{
				if (this.InternalClosed == null)
				{
					this.InnerCommunicationObject.Closed += this.OnInnerCommunicationObjectClosed;
				}
				this.InternalClosed += value;
			}
			remove
			{
				this.InternalClosed -= value;
				if (this.InternalClosed == null)
				{
					this.InnerCommunicationObject.Closed -= this.OnInnerCommunicationObjectClosed;
				}
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060000CF RID: 207 RVA: 0x00003DDD File Offset: 0x00001FDD
		// (remove) Token: 0x060000D0 RID: 208 RVA: 0x00003E05 File Offset: 0x00002005
		event EventHandler ICommunicationObject.Faulted
		{
			add
			{
				if (this.InternalFaulted == null)
				{
					this.InnerCommunicationObject.Faulted += this.OnInnerCommunicationObjectFaulted;
				}
				this.InternalFaulted += value;
			}
			remove
			{
				this.InternalFaulted -= value;
				if (this.InternalFaulted == null)
				{
					this.InnerCommunicationObject.Faulted -= this.OnInnerCommunicationObjectFaulted;
				}
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060000D1 RID: 209 RVA: 0x00003E30 File Offset: 0x00002030
		// (remove) Token: 0x060000D2 RID: 210 RVA: 0x00003E68 File Offset: 0x00002068
		private event EventHandler InternalOpening;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060000D3 RID: 211 RVA: 0x00003EA0 File Offset: 0x000020A0
		// (remove) Token: 0x060000D4 RID: 212 RVA: 0x00003ED8 File Offset: 0x000020D8
		private event EventHandler InternalOpened;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060000D5 RID: 213 RVA: 0x00003F10 File Offset: 0x00002110
		// (remove) Token: 0x060000D6 RID: 214 RVA: 0x00003F48 File Offset: 0x00002148
		private event EventHandler InternalClosing;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060000D7 RID: 215 RVA: 0x00003F80 File Offset: 0x00002180
		// (remove) Token: 0x060000D8 RID: 216 RVA: 0x00003FB8 File Offset: 0x000021B8
		private event EventHandler InternalClosed;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060000D9 RID: 217 RVA: 0x00003FF0 File Offset: 0x000021F0
		// (remove) Token: 0x060000DA RID: 218 RVA: 0x00004028 File Offset: 0x00002228
		private event EventHandler InternalFaulted;

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000405D File Offset: 0x0000225D
		public ChannelFactory ChannelFactory
		{
			get
			{
				return this.InnerClient.ChannelFactory;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000406A File Offset: 0x0000226A
		public ClientCredentials ClientCredentials
		{
			get
			{
				return this.InnerClient.ClientCredentials;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004077 File Offset: 0x00002277
		public ServiceEndpoint Endpoint
		{
			get
			{
				return this.InnerClient.Endpoint;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004084 File Offset: 0x00002284
		public IClientChannel InnerChannel
		{
			get
			{
				return this.InnerClient.InnerChannel;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004091 File Offset: 0x00002291
		CommunicationState ICommunicationObject.State
		{
			get
			{
				return this.InnerCommunicationObject.State;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000409E File Offset: 0x0000229E
		private IDiscoveryInnerClient InnerClient
		{
			get
			{
				return this.innerClient;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000040A6 File Offset: 0x000022A6
		private ICommunicationObject InnerCommunicationObject
		{
			get
			{
				return this.InnerClient.InnerCommunicationObject;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000040B3 File Offset: 0x000022B3
		void ICommunicationObject.Open()
		{
			this.InnerCommunicationObject.Open();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000040C0 File Offset: 0x000022C0
		void ICommunicationObject.Open(TimeSpan timeout)
		{
			this.InnerCommunicationObject.Open(timeout);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000040CE File Offset: 0x000022CE
		IAsyncResult ICommunicationObject.BeginOpen(AsyncCallback callback, object state)
		{
			return this.InnerCommunicationObject.BeginOpen(callback, state);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000040DD File Offset: 0x000022DD
		IAsyncResult ICommunicationObject.BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.InnerCommunicationObject.BeginOpen(timeout, callback, state);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000040ED File Offset: 0x000022ED
		void ICommunicationObject.EndOpen(IAsyncResult result)
		{
			this.InnerCommunicationObject.EndOpen(result);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000040FB File Offset: 0x000022FB
		void ICommunicationObject.Close()
		{
			((ICommunicationObject)this).Close(DiscoveryClient.defaultCloseDuration);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004108 File Offset: 0x00002308
		void ICommunicationObject.Close(TimeSpan timeout)
		{
			if (this.IsCloseOrAbortCalled())
			{
				return;
			}
			TimeoutException ex = null;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			try
			{
				this.asyncOperationsLifetimeManager.Close(timeoutHelper.RemainingTime());
			}
			catch (TimeoutException ex2)
			{
				ex = ex2;
			}
			if (ex != null)
			{
				((ICommunicationObject)this).Abort();
				throw FxTrace.Exception.AsError(new TimeoutException(SR.DiscoveryCloseTimedOut(timeout), ex));
			}
			try
			{
				this.InnerCommunicationObject.Close(timeoutHelper.RemainingTime());
			}
			catch (ProtocolException exception)
			{
				if (TD.DiscoveryClientProtocolExceptionSuppressedIsEnabled())
				{
					TD.DiscoveryClientProtocolExceptionSuppressed(exception);
				}
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000041A8 File Offset: 0x000023A8
		IAsyncResult ICommunicationObject.BeginClose(AsyncCallback callback, object state)
		{
			return ((ICommunicationObject)this).BeginClose(DiscoveryClient.defaultCloseDuration, callback, state);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000041B7 File Offset: 0x000023B7
		IAsyncResult ICommunicationObject.BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.IsCloseOrAbortCalled())
			{
				return new DiscoveryClient.CloseAsyncResult(callback, state);
			}
			return new DiscoveryClient.CloseAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000041D2 File Offset: 0x000023D2
		void ICommunicationObject.EndClose(IAsyncResult result)
		{
			DiscoveryClient.CloseAsyncResult.End(result);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000041DA File Offset: 0x000023DA
		void ICommunicationObject.Abort()
		{
			this.InnerCommunicationObject.Abort();
			this.AbortActiveOperations();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000041ED File Offset: 0x000023ED
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002696 File Offset: 0x00000896
		public void Open()
		{
			((ICommunicationObject)this).Open();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000041F8 File Offset: 0x000023F8
		public FindResponse Find(FindCriteria criteria)
		{
			if (criteria == null)
			{
				throw FxTrace.Exception.ArgumentNull("criteria");
			}
			if (criteria.MaxResults == 2147483647 && criteria.Duration.Equals(TimeSpan.MaxValue))
			{
				throw FxTrace.Exception.AsError(new ArgumentException(SR.DiscoveryFindCanNeverComplete));
			}
			SyncOperationState syncOperationState = new SyncOperationState();
			this.FindAsync(criteria, syncOperationState);
			syncOperationState.WaitEvent.WaitOne();
			return ((FindCompletedEventArgs)syncOperationState.EventArgs).Result;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004279 File Offset: 0x00002479
		public void FindAsync(FindCriteria criteria)
		{
			this.FindAsync(criteria, null);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004284 File Offset: 0x00002484
		public void FindAsync(FindCriteria criteria, object userState)
		{
			if (criteria == null)
			{
				throw FxTrace.Exception.ArgumentNull("criteria");
			}
			using (new DiscoveryClient.DiscoveryOperationContextScope(this.InnerChannel))
			{
				this.FindAsyncOperation(criteria, userState);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000042D4 File Offset: 0x000024D4
		public Task<FindResponse> FindTaskAsync(FindCriteria criteria)
		{
			return this.FindTaskAsync(criteria, CancellationToken.None);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000042E4 File Offset: 0x000024E4
		public Task<FindResponse> FindTaskAsync(FindCriteria criteria, CancellationToken cancellationToken)
		{
			if (criteria == null)
			{
				throw FxTrace.Exception.ArgumentNull("criteria");
			}
			TaskCompletionSource<FindResponse> taskCompletionSource = new TaskCompletionSource<FindResponse>();
			DiscoveryClient.TaskAsyncOperationState<FindResponse> userState = new DiscoveryClient.TaskAsyncOperationState<FindResponse>(this, taskCompletionSource, cancellationToken);
			Task<FindResponse> task = taskCompletionSource.Task;
			this.FindAsync(criteria, userState);
			return task;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004323 File Offset: 0x00002523
		public Task<ResolveResponse> ResolveTaskAsync(ResolveCriteria criteria)
		{
			return this.ResolveTaskAsync(criteria, CancellationToken.None);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004334 File Offset: 0x00002534
		public Task<ResolveResponse> ResolveTaskAsync(ResolveCriteria criteria, CancellationToken cancellationToken)
		{
			if (criteria == null)
			{
				throw FxTrace.Exception.ArgumentNull("criteria");
			}
			TaskCompletionSource<ResolveResponse> taskCompletionSource = new TaskCompletionSource<ResolveResponse>();
			DiscoveryClient.TaskAsyncOperationState<ResolveResponse> userState = new DiscoveryClient.TaskAsyncOperationState<ResolveResponse>(this, taskCompletionSource, cancellationToken);
			Task<ResolveResponse> task = taskCompletionSource.Task;
			this.ResolveAsync(criteria, userState);
			return task;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004374 File Offset: 0x00002574
		public ResolveResponse Resolve(ResolveCriteria criteria)
		{
			SyncOperationState syncOperationState = new SyncOperationState();
			this.ResolveAsync(criteria, syncOperationState);
			syncOperationState.WaitEvent.WaitOne();
			return ((ResolveCompletedEventArgs)syncOperationState.EventArgs).Result;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000043AB File Offset: 0x000025AB
		public void ResolveAsync(ResolveCriteria criteria)
		{
			this.ResolveAsync(criteria, null);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000043B8 File Offset: 0x000025B8
		public void ResolveAsync(ResolveCriteria criteria, object userState)
		{
			if (criteria == null)
			{
				throw FxTrace.Exception.ArgumentNull("criteria");
			}
			using (new DiscoveryClient.DiscoveryOperationContextScope(this.InnerChannel))
			{
				this.ResolveAsyncOperation(criteria, userState);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004408 File Offset: 0x00002608
		public void CancelAsync(object userState)
		{
			if (userState == null)
			{
				throw FxTrace.Exception.ArgumentNull("userState");
			}
			AsyncOperationContext asyncOperationContext = null;
			if (this.asyncOperationsLifetimeManager.TryRemoveUnique(userState, out asyncOperationContext))
			{
				if (asyncOperationContext is DiscoveryClient.FindAsyncOperationContext)
				{
					this.PostFindCompleted((DiscoveryClient.FindAsyncOperationContext)asyncOperationContext, true, null);
					return;
				}
				this.PostResolveCompleted((DiscoveryClient.ResolveAsyncOperationContext)asyncOperationContext, true, null);
				return;
			}
			else
			{
				if (asyncOperationContext != null)
				{
					throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryMultiplePendingOperationsPerUserState));
				}
				return;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002926 File Offset: 0x00000B26
		public void Close()
		{
			((ICommunicationObject)this).Close();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004478 File Offset: 0x00002678
		void IDiscoveryInnerClientResponse.PostFindCompletedAndRemove(UniqueId operationId, bool cancelled, Exception error)
		{
			DiscoveryClient.FindAsyncOperationContext findAsyncOperationContext = this.asyncOperationsLifetimeManager.Remove<DiscoveryClient.FindAsyncOperationContext>(operationId);
			if (findAsyncOperationContext != null)
			{
				this.PostFindCompleted(findAsyncOperationContext, cancelled, error);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000044A0 File Offset: 0x000026A0
		void IDiscoveryInnerClientResponse.PostResolveCompletedAndRemove(UniqueId operationId, bool cancelled, Exception error)
		{
			DiscoveryClient.ResolveAsyncOperationContext resolveAsyncOperationContext = this.asyncOperationsLifetimeManager.Remove<DiscoveryClient.ResolveAsyncOperationContext>(operationId);
			if (resolveAsyncOperationContext != null)
			{
				this.PostResolveCompleted(resolveAsyncOperationContext, cancelled, error);
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000044C8 File Offset: 0x000026C8
		void IDiscoveryInnerClientResponse.ProbeMatchOperation(UniqueId relatesTo, DiscoveryMessageSequence discoveryMessageSequence, Collection<EndpointDiscoveryMetadata> endpointDiscoveryMetadataCollection, bool findCompleted)
		{
			EventTraceActivity eventTraceActivity = null;
			OperationContext operationContext = OperationContext.Current;
			if (Fx.Trace.IsEtwProviderEnabled && operationContext != null)
			{
				eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(operationContext.IncomingMessage);
			}
			if (relatesTo == null)
			{
				if (TD.DiscoveryMessageWithNullRelatesToIsEnabled() && operationContext != null)
				{
					TD.DiscoveryMessageWithNullRelatesTo(eventTraceActivity, "ProbeMatches", operationContext.IncomingMessageHeaders.MessageId.ToString());
				}
				return;
			}
			DiscoveryClient.FindAsyncOperationContext findAsyncOperationContext = null;
			if (!this.asyncOperationsLifetimeManager.TryLookup<DiscoveryClient.FindAsyncOperationContext>(relatesTo, out findAsyncOperationContext))
			{
				if (TD.DiscoveryMessageWithInvalidRelatesToOrOperationCompletedIsEnabled() && operationContext != null)
				{
					TD.DiscoveryMessageWithInvalidRelatesToOrOperationCompleted(eventTraceActivity, "ProbeMatches", operationContext.IncomingMessageHeaders.MessageId.ToString(), relatesTo.ToString(), "Find");
				}
				return;
			}
			bool flag = false;
			object syncRoot = findAsyncOperationContext.SyncRoot;
			lock (syncRoot)
			{
				if (!findAsyncOperationContext.IsCompleted && findAsyncOperationContext.Result.Endpoints.Count < findAsyncOperationContext.MaxResults)
				{
					bool flag3 = !findAsyncOperationContext.IsSyncOperation && !findAsyncOperationContext.IsTaskBasedOperation && this.FindProgressChanged != null;
					using (IEnumerator<EndpointDiscoveryMetadata> enumerator = endpointDiscoveryMetadataCollection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							EndpointDiscoveryMetadata endpointDiscoveryMetadata = enumerator.Current;
							findAsyncOperationContext.Result.AddDiscoveredEndpoint(endpointDiscoveryMetadata, discoveryMessageSequence);
							if (flag3)
							{
								findAsyncOperationContext.AsyncOperation.Post(this.findProgressChangedDelegate, new FindProgressChangedEventArgs(findAsyncOperationContext.Progress, findAsyncOperationContext.UserState, endpointDiscoveryMetadata, discoveryMessageSequence));
							}
							if (findAsyncOperationContext.Result.Endpoints.Count == findAsyncOperationContext.MaxResults)
							{
								flag = true;
								break;
							}
						}
						goto IL_1A4;
					}
				}
				if (TD.DiscoveryMessageReceivedAfterOperationCompletedIsEnabled() && operationContext != null)
				{
					TD.DiscoveryMessageReceivedAfterOperationCompleted(eventTraceActivity, "ProbeMatches", operationContext.IncomingMessageHeaders.MessageId.ToString(), "Find");
				}
			}
			IL_1A4:
			if (flag || findCompleted)
			{
				((IDiscoveryInnerClientResponse)this).PostFindCompletedAndRemove(findAsyncOperationContext.OperationId, false, null);
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000046AC File Offset: 0x000028AC
		void IDiscoveryInnerClientResponse.ResolveMatchOperation(UniqueId relatesTo, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			EventTraceActivity eventTraceActivity = null;
			OperationContext operationContext = OperationContext.Current;
			if (Fx.Trace.IsEtwProviderEnabled && operationContext != null)
			{
				eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(operationContext.IncomingMessage);
			}
			if (relatesTo == null)
			{
				if (TD.DiscoveryMessageWithNullRelatesToIsEnabled() && operationContext != null)
				{
					TD.DiscoveryMessageWithNullRelatesTo(eventTraceActivity, "ResolveMatches", operationContext.IncomingMessageHeaders.MessageId.ToString());
				}
				return;
			}
			DiscoveryClient.ResolveAsyncOperationContext resolveAsyncOperationContext = null;
			if (!this.asyncOperationsLifetimeManager.TryLookup<DiscoveryClient.ResolveAsyncOperationContext>(relatesTo, out resolveAsyncOperationContext))
			{
				if (TD.DiscoveryMessageWithInvalidRelatesToOrOperationCompletedIsEnabled() && operationContext != null)
				{
					TD.DiscoveryMessageWithInvalidRelatesToOrOperationCompleted(eventTraceActivity, "ResolveMatches", operationContext.IncomingMessageHeaders.MessageId.ToString(), relatesTo.ToString(), "Resolve");
				}
				return;
			}
			bool flag = false;
			object syncRoot = resolveAsyncOperationContext.SyncRoot;
			lock (syncRoot)
			{
				if (!resolveAsyncOperationContext.IsCompleted && resolveAsyncOperationContext.Result.EndpointDiscoveryMetadata == null)
				{
					resolveAsyncOperationContext.Result.EndpointDiscoveryMetadata = endpointDiscoveryMetadata;
					resolveAsyncOperationContext.Result.MessageSequence = discoveryMessageSequence;
					flag = true;
				}
				else if (TD.DiscoveryMessageReceivedAfterOperationCompletedIsEnabled() && operationContext != null)
				{
					TD.DiscoveryMessageReceivedAfterOperationCompleted(eventTraceActivity, "ResolveMatches", operationContext.IncomingMessageHeaders.MessageId.ToString(), "Resolve");
				}
			}
			if (flag)
			{
				((IDiscoveryInnerClientResponse)this).PostResolveCompletedAndRemove(resolveAsyncOperationContext.OperationId, false, null);
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000047F0 File Offset: 0x000029F0
		void IDiscoveryInnerClientResponse.HelloOperation(UniqueId relatesTo, DiscoveryMessageSequence proxyMessageSequence, EndpointDiscoveryMetadata proxyEndpointMetadata)
		{
			EventTraceActivity eventTraceActivity = null;
			OperationContext operationContext = OperationContext.Current;
			if (Fx.Trace.IsEtwProviderEnabled && operationContext != null)
			{
				eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(operationContext.IncomingMessage);
			}
			if (relatesTo == null)
			{
				if (TD.DiscoveryMessageWithNullRelatesToIsEnabled() && operationContext != null)
				{
					TD.DiscoveryMessageWithNullRelatesTo(eventTraceActivity, "Hello", operationContext.IncomingMessageHeaders.MessageId.ToString());
				}
				return;
			}
			AsyncOperationContext context = null;
			if (!this.asyncOperationsLifetimeManager.TryLookup(relatesTo, out context))
			{
				if (TD.DiscoveryMessageWithInvalidRelatesToOrOperationCompletedIsEnabled() && operationContext != null)
				{
					TD.DiscoveryMessageWithInvalidRelatesToOrOperationCompleted(eventTraceActivity, "Hello", operationContext.IncomingMessageHeaders.MessageId.ToString(), relatesTo.ToString(), string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
					{
						"Find",
						"Resolve"
					}));
				}
				return;
			}
			this.PostProxyAvailable(context, proxyEndpointMetadata, proxyMessageSequence);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000048BC File Offset: 0x00002ABC
		private void Initialize(DiscoveryEndpoint discoveryEndpoint)
		{
			if (discoveryEndpoint.Binding != null && discoveryEndpoint.Binding.MessageVersion.Addressing == AddressingVersion.None)
			{
				throw FxTrace.Exception.Argument("discoveryEndpoint", SR.EndpointWithInvalidMessageVersion(discoveryEndpoint.GetType().Name, AddressingVersion.None, base.GetType().Name, AddressingVersion.WSAddressing10, AddressingVersion.WSAddressingAugust2004));
			}
			this.innerClient = discoveryEndpoint.DiscoveryVersion.Implementation.CreateDiscoveryInnerClient(discoveryEndpoint, this);
			this.asyncOperationsLifetimeManager = new AsyncOperationLifetimeManager();
			this.findCompletedDelegate = Fx.ThunkCallback(new SendOrPostCallback(this.RaiseFindCompleted));
			this.findProgressChangedDelegate = Fx.ThunkCallback(new SendOrPostCallback(this.RaiseFindProgressChanged));
			this.resolveCompletedDelegate = Fx.ThunkCallback(new SendOrPostCallback(this.RaiseResolveCompleted));
			this.proxyAvailableDelegate = Fx.ThunkCallback(new SendOrPostCallback(this.RaiseProxyAvailable));
			this.findOperationTimeoutCallbackDelegate = new Action<object>(this.FindOperationTimeoutCallback);
			this.resolveOperationTimeoutCallbackDelegate = new Action<object>(this.ResolveOperationTimeoutCallback);
			this.probeOperationCallbackDelegate = Fx.ThunkCallback(new AsyncCallback(this.ProbeOperationCompletedCallback));
			this.resolveOperationCallbackDelegate = Fx.ThunkCallback(new AsyncCallback(this.ResolveOperationCompletedCallback));
			this.cancelTaskCallbackDelegate = Fx.ThunkCallback<object>(new Action<object>(this.CancelAsync));
			this.closeCalled = 0;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004A11 File Offset: 0x00002C11
		private void OnInnerCommunicationObjectOpened(object sender, EventArgs e)
		{
			this.RaiseCommunicationObjectEvent(this.InternalOpened, e);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004A20 File Offset: 0x00002C20
		private void OnInnerCommunicationObjectOpening(object sender, EventArgs e)
		{
			this.RaiseCommunicationObjectEvent(this.InternalOpening, e);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004A2F File Offset: 0x00002C2F
		private void OnInnerCommunicationObjectClosing(object sender, EventArgs e)
		{
			this.RaiseCommunicationObjectEvent(this.InternalClosing, e);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004A3E File Offset: 0x00002C3E
		private void OnInnerCommunicationObjectClosed(object sender, EventArgs e)
		{
			this.RaiseCommunicationObjectEvent(this.InternalClosed, e);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004A4D File Offset: 0x00002C4D
		private void OnInnerCommunicationObjectFaulted(object sender, EventArgs e)
		{
			this.RaiseCommunicationObjectEvent(this.InternalFaulted, e);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002A51 File Offset: 0x00000C51
		private void RaiseCommunicationObjectEvent(EventHandler handler, EventArgs e)
		{
			if (handler != null)
			{
				handler(this, e);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004A5C File Offset: 0x00002C5C
		private void FindAsyncOperation(FindCriteria criteria, object userState)
		{
			AsyncOperationContext asyncOperationContext = new DiscoveryClient.FindAsyncOperationContext(OperationContext.Current.OutgoingMessageHeaders.MessageId, criteria.MaxResults, criteria.Duration, userState);
			this.InitializeAsyncOperation(asyncOperationContext);
			Exception ex = null;
			try
			{
				if (!asyncOperationContext.IsCompleted)
				{
					if (asyncOperationContext.IsSyncOperation)
					{
						this.InnerClient.ProbeOperation(criteria);
						this.StartTimer(asyncOperationContext, this.findOperationTimeoutCallbackDelegate);
					}
					else
					{
						IAsyncResult asyncResult = this.InnerClient.BeginProbeOperation(criteria, this.probeOperationCallbackDelegate, asyncOperationContext);
						if (asyncResult.CompletedSynchronously)
						{
							this.CompleteProbeOperation(asyncResult);
						}
					}
				}
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
				((IDiscoveryInnerClientResponse)this).PostFindCompletedAndRemove(asyncOperationContext.OperationId, false, ex);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004B14 File Offset: 0x00002D14
		private void ResolveAsyncOperation(ResolveCriteria criteria, object userState)
		{
			AsyncOperationContext asyncOperationContext = new DiscoveryClient.ResolveAsyncOperationContext(OperationContext.Current.OutgoingMessageHeaders.MessageId, criteria.Duration, userState);
			this.InitializeAsyncOperation(asyncOperationContext);
			Exception ex = null;
			try
			{
				if (asyncOperationContext.IsSyncOperation)
				{
					this.InnerClient.ResolveOperation(criteria);
					this.StartTimer(asyncOperationContext, this.resolveOperationTimeoutCallbackDelegate);
				}
				else
				{
					IAsyncResult asyncResult = this.InnerClient.BeginResolveOperation(criteria, this.resolveOperationCallbackDelegate, asyncOperationContext);
					if (asyncResult.CompletedSynchronously)
					{
						this.CompleteResolveOperation(asyncResult);
					}
				}
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
				((IDiscoveryInnerClientResponse)this).PostResolveCompletedAndRemove(asyncOperationContext.OperationId, false, ex);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004BC0 File Offset: 0x00002DC0
		private void InitializeAsyncOperation(AsyncOperationContext context)
		{
			context.AsyncOperation = AsyncOperationManager.CreateOperation(context.UserState);
			if (this.asyncOperationsLifetimeManager.TryAdd(context))
			{
				return;
			}
			if (this.asyncOperationsLifetimeManager.IsClosed || this.asyncOperationsLifetimeManager.IsAborted)
			{
				throw FxTrace.Exception.AsError(new ObjectDisposedException(base.GetType().Name));
			}
			throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryDuplicateOperationId(context.OperationId)));
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004C3C File Offset: 0x00002E3C
		private bool IsCloseOrAbortCalled()
		{
			return Interlocked.CompareExchange(ref this.closeCalled, 1, 0) == 1 || this.asyncOperationsLifetimeManager.IsAborted;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004C5B File Offset: 0x00002E5B
		private void ProbeOperationCompletedCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			this.CompleteProbeOperation(result);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004C70 File Offset: 0x00002E70
		private void FindOperationTimeoutCallback(object state)
		{
			AsyncOperationContext asyncOperationContext = (AsyncOperationContext)state;
			((IDiscoveryInnerClientResponse)this).PostFindCompletedAndRemove(asyncOperationContext.OperationId, false, null);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004C94 File Offset: 0x00002E94
		private void CompleteProbeOperation(IAsyncResult result)
		{
			AsyncOperationContext asyncOperationContext = (AsyncOperationContext)result.AsyncState;
			Exception ex = null;
			try
			{
				this.InnerClient.EndProbeOperation(result);
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
				((IDiscoveryInnerClientResponse)this).PostFindCompletedAndRemove(asyncOperationContext.OperationId, false, ex);
				return;
			}
			this.StartTimer(asyncOperationContext, this.findOperationTimeoutCallbackDelegate);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004CFC File Offset: 0x00002EFC
		private void PostFindCompleted(DiscoveryClient.FindAsyncOperationContext context, bool cancelled, Exception error)
		{
			bool flag = false;
			object syncRoot = context.SyncRoot;
			lock (syncRoot)
			{
				if (!context.IsCompleted)
				{
					context.Complete();
					flag = true;
				}
			}
			if (flag)
			{
				FindCompletedEventArgs findCompletedEventArgs = new FindCompletedEventArgs(error, cancelled, context.UserState, context.Result);
				if (this.DispatchToSyncOperation(findCompletedEventArgs) || this.DispatchToTaskAyncOperation<FindResponse>(context.UserState, context.Result, error, cancelled) || this.FindCompleted == null)
				{
					context.AsyncOperation.OperationCompleted();
					return;
				}
				context.AsyncOperation.PostOperationCompleted(this.findCompletedDelegate, findCompletedEventArgs);
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004DA4 File Offset: 0x00002FA4
		private void RaiseFindCompleted(object state)
		{
			EventHandler<FindCompletedEventArgs> findCompleted = this.FindCompleted;
			if (findCompleted != null)
			{
				findCompleted(this, (FindCompletedEventArgs)state);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004DC8 File Offset: 0x00002FC8
		private void RaiseFindProgressChanged(object state)
		{
			EventHandler<FindProgressChangedEventArgs> findProgressChanged = this.FindProgressChanged;
			if (findProgressChanged != null)
			{
				findProgressChanged(this, (FindProgressChangedEventArgs)state);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004DEC File Offset: 0x00002FEC
		private void ResolveOperationCompletedCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			this.CompleteResolveOperation(result);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004E00 File Offset: 0x00003000
		private void ResolveOperationTimeoutCallback(object state)
		{
			AsyncOperationContext asyncOperationContext = (AsyncOperationContext)state;
			((IDiscoveryInnerClientResponse)this).PostResolveCompletedAndRemove(asyncOperationContext.OperationId, false, null);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004E24 File Offset: 0x00003024
		private void CompleteResolveOperation(IAsyncResult result)
		{
			AsyncOperationContext asyncOperationContext = (AsyncOperationContext)result.AsyncState;
			Exception ex = null;
			try
			{
				this.InnerClient.EndResolveOperation(result);
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
				((IDiscoveryInnerClientResponse)this).PostResolveCompletedAndRemove(asyncOperationContext.OperationId, false, ex);
				return;
			}
			this.StartTimer(asyncOperationContext, this.resolveOperationTimeoutCallbackDelegate);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004E8C File Offset: 0x0000308C
		private void PostResolveCompleted(DiscoveryClient.ResolveAsyncOperationContext context, bool cancelled, Exception error)
		{
			bool flag = false;
			object syncRoot = context.SyncRoot;
			lock (syncRoot)
			{
				if (!context.IsCompleted)
				{
					context.Complete();
					flag = true;
				}
			}
			if (flag)
			{
				ResolveCompletedEventArgs resolveCompletedEventArgs = new ResolveCompletedEventArgs(error, cancelled, context.UserState, context.Result);
				if (this.DispatchToSyncOperation(resolveCompletedEventArgs) || this.DispatchToTaskAyncOperation<ResolveResponse>(context.UserState, context.Result, error, cancelled) || this.ResolveCompleted == null)
				{
					context.AsyncOperation.OperationCompleted();
					return;
				}
				context.AsyncOperation.PostOperationCompleted(this.resolveCompletedDelegate, resolveCompletedEventArgs);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004F34 File Offset: 0x00003134
		private void RaiseResolveCompleted(object state)
		{
			EventHandler<ResolveCompletedEventArgs> resolveCompleted = this.ResolveCompleted;
			if (resolveCompleted != null)
			{
				resolveCompleted(this, (ResolveCompletedEventArgs)state);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004F58 File Offset: 0x00003158
		private void PostProxyAvailable(AsyncOperationContext context, EndpointDiscoveryMetadata proxyEndpointMetadata, DiscoveryMessageSequence proxyMessageSequence)
		{
			if (TD.DiscoveryClientReceivedMulticastSuppressionIsEnabled())
			{
				TD.DiscoveryClientReceivedMulticastSuppression();
			}
			if (this.ProxyAvailable != null)
			{
				object syncRoot = context.SyncRoot;
				lock (syncRoot)
				{
					if (!context.IsCompleted)
					{
						AnnouncementEventArgs arg = new AnnouncementEventArgs(proxyMessageSequence, proxyEndpointMetadata);
						context.AsyncOperation.Post(this.proxyAvailableDelegate, arg);
					}
				}
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004FC8 File Offset: 0x000031C8
		private void RaiseProxyAvailable(object state)
		{
			EventHandler<AnnouncementEventArgs> proxyAvailable = this.ProxyAvailable;
			if (proxyAvailable != null)
			{
				proxyAvailable(this, (AnnouncementEventArgs)state);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004FEC File Offset: 0x000031EC
		private void StartTimer(AsyncOperationContext context, Action<object> operationTimeoutCallbackDelegate)
		{
			if (!this.InnerClient.IsRequestResponse)
			{
				object syncRoot = context.SyncRoot;
				lock (syncRoot)
				{
					if (!context.IsCompleted)
					{
						context.StartTimer(operationTimeoutCallbackDelegate);
					}
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005044 File Offset: 0x00003244
		private bool DispatchToSyncOperation(AsyncCompletedEventArgs e)
		{
			if (e.UserState is SyncOperationState)
			{
				SyncOperationState syncOperationState = (SyncOperationState)e.UserState;
				syncOperationState.EventArgs = e;
				syncOperationState.WaitEvent.Set();
				return true;
			}
			return false;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005080 File Offset: 0x00003280
		private bool DispatchToTaskAyncOperation<TResult>(object userState, TResult result, Exception error, bool cancelled)
		{
			DiscoveryClient.TaskAsyncOperationState<TResult> taskAsyncOperationState = userState as DiscoveryClient.TaskAsyncOperationState<TResult>;
			if (taskAsyncOperationState != null)
			{
				taskAsyncOperationState.Complete(result, error, cancelled);
				return true;
			}
			return false;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000050A4 File Offset: 0x000032A4
		private void AbortActiveOperations()
		{
			AsyncOperationContext[] array = this.asyncOperationsLifetimeManager.Abort();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] is DiscoveryClient.FindAsyncOperationContext)
				{
					this.PostFindCompleted((DiscoveryClient.FindAsyncOperationContext)array[i], true, null);
				}
				else
				{
					this.PostResolveCompleted((DiscoveryClient.ResolveAsyncOperationContext)array[i], true, null);
				}
			}
		}

		// Token: 0x04000037 RID: 55
		private static TimeSpan defaultCloseDuration = TimeSpan.FromSeconds(60.0);

		// Token: 0x04000038 RID: 56
		private SendOrPostCallback findCompletedDelegate;

		// Token: 0x04000039 RID: 57
		private SendOrPostCallback findProgressChangedDelegate;

		// Token: 0x0400003A RID: 58
		private SendOrPostCallback resolveCompletedDelegate;

		// Token: 0x0400003B RID: 59
		private SendOrPostCallback proxyAvailableDelegate;

		// Token: 0x0400003C RID: 60
		private Action<object> findOperationTimeoutCallbackDelegate;

		// Token: 0x0400003D RID: 61
		private Action<object> resolveOperationTimeoutCallbackDelegate;

		// Token: 0x0400003E RID: 62
		private AsyncCallback probeOperationCallbackDelegate;

		// Token: 0x0400003F RID: 63
		private AsyncCallback resolveOperationCallbackDelegate;

		// Token: 0x04000040 RID: 64
		private Action<object> cancelTaskCallbackDelegate;

		// Token: 0x04000041 RID: 65
		private IDiscoveryInnerClient innerClient;

		// Token: 0x04000042 RID: 66
		private AsyncOperationLifetimeManager asyncOperationsLifetimeManager;

		// Token: 0x04000043 RID: 67
		private int closeCalled;

		// Token: 0x020000C6 RID: 198
		private class CloseAsyncResult : AsyncResult
		{
			// Token: 0x060007C7 RID: 1991 RVA: 0x00014394 File Offset: 0x00012594
			internal CloseAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
				base.Complete(true);
			}

			// Token: 0x060007C8 RID: 1992 RVA: 0x000143A8 File Offset: 0x000125A8
			internal CloseAsyncResult(DiscoveryClient client, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.client = client;
				this.timeoutHelper = new TimeoutHelper(timeout);
				IAsyncResult asyncResult = this.client.asyncOperationsLifetimeManager.BeginClose(this.timeoutHelper.RemainingTime(), base.PrepareAsyncCompletion(DiscoveryClient.CloseAsyncResult.onAsyncLifetimeManangerCloseCompleted), this);
				if (asyncResult.CompletedSynchronously && DiscoveryClient.CloseAsyncResult.OnAsyncLifetimeManagerCloseCompleted(asyncResult))
				{
					base.Complete(true);
				}
			}

			// Token: 0x060007C9 RID: 1993 RVA: 0x00014410 File Offset: 0x00012610
			internal static void End(IAsyncResult result)
			{
				AsyncResult.End<DiscoveryClient.CloseAsyncResult>(result);
			}

			// Token: 0x060007CA RID: 1994 RVA: 0x0001441C File Offset: 0x0001261C
			private static bool OnAsyncLifetimeManagerCloseCompleted(IAsyncResult result)
			{
				DiscoveryClient.CloseAsyncResult closeAsyncResult = (DiscoveryClient.CloseAsyncResult)result.AsyncState;
				Exception ex = null;
				try
				{
					closeAsyncResult.client.asyncOperationsLifetimeManager.EndClose(result);
				}
				catch (TimeoutException ex2)
				{
					ex = ex2;
				}
				if (ex != null)
				{
					((ICommunicationObject)closeAsyncResult.client).Abort();
					throw FxTrace.Exception.AsError(new TimeoutException(SR.DiscoveryCloseTimedOut(closeAsyncResult.timeoutHelper.OriginalTimeout), ex));
				}
				IAsyncResult asyncResult = closeAsyncResult.client.InnerCommunicationObject.BeginClose(closeAsyncResult.timeoutHelper.RemainingTime(), closeAsyncResult.PrepareAsyncCompletion(DiscoveryClient.CloseAsyncResult.onInnerCommunicationObjectCloseCompleted), closeAsyncResult);
				return asyncResult.CompletedSynchronously && DiscoveryClient.CloseAsyncResult.OnInnerCommunicationObjectCloseCompleted(asyncResult);
			}

			// Token: 0x060007CB RID: 1995 RVA: 0x000144CC File Offset: 0x000126CC
			private static bool OnInnerCommunicationObjectCloseCompleted(IAsyncResult result)
			{
				DiscoveryClient.CloseAsyncResult closeAsyncResult = (DiscoveryClient.CloseAsyncResult)result.AsyncState;
				closeAsyncResult.client.InnerCommunicationObject.EndClose(result);
				return true;
			}

			// Token: 0x040001E2 RID: 482
			private static AsyncResult.AsyncCompletion onAsyncLifetimeManangerCloseCompleted = new AsyncResult.AsyncCompletion(DiscoveryClient.CloseAsyncResult.OnAsyncLifetimeManagerCloseCompleted);

			// Token: 0x040001E3 RID: 483
			private static AsyncResult.AsyncCompletion onInnerCommunicationObjectCloseCompleted = new AsyncResult.AsyncCompletion(DiscoveryClient.CloseAsyncResult.OnInnerCommunicationObjectCloseCompleted);

			// Token: 0x040001E4 RID: 484
			private DiscoveryClient client;

			// Token: 0x040001E5 RID: 485
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x020000C7 RID: 199
		private sealed class DiscoveryOperationContextScope : IDisposable
		{
			// Token: 0x060007CD RID: 1997 RVA: 0x0001451C File Offset: 0x0001271C
			public DiscoveryOperationContextScope(IClientChannel clientChannel)
			{
				if (DiscoveryUtility.IsCompatible(OperationContext.Current, clientChannel))
				{
					this.originalMessageId = OperationContext.Current.OutgoingMessageHeaders.MessageId;
					this.originalReplyTo = OperationContext.Current.OutgoingMessageHeaders.ReplyTo;
					this.originalTo = OperationContext.Current.OutgoingMessageHeaders.To;
				}
				else
				{
					this.operationContextScope = new OperationContextScope(clientChannel);
				}
				if (this.originalMessageId == null)
				{
					OperationContext.Current.OutgoingMessageHeaders.MessageId = new UniqueId();
				}
				OperationContext.Current.OutgoingMessageHeaders.ReplyTo = clientChannel.LocalAddress;
				OperationContext.Current.OutgoingMessageHeaders.To = clientChannel.RemoteAddress.Uri;
			}

			// Token: 0x060007CE RID: 1998 RVA: 0x000145DC File Offset: 0x000127DC
			public void Dispose()
			{
				if (this.operationContextScope != null)
				{
					this.operationContextScope.Dispose();
					return;
				}
				OperationContext.Current.OutgoingMessageHeaders.MessageId = this.originalMessageId;
				OperationContext.Current.OutgoingMessageHeaders.ReplyTo = this.originalReplyTo;
				OperationContext.Current.OutgoingMessageHeaders.To = this.originalTo;
			}

			// Token: 0x040001E6 RID: 486
			private OperationContextScope operationContextScope;

			// Token: 0x040001E7 RID: 487
			private UniqueId originalMessageId;

			// Token: 0x040001E8 RID: 488
			private EndpointAddress originalReplyTo;

			// Token: 0x040001E9 RID: 489
			private Uri originalTo;
		}

		// Token: 0x020000C8 RID: 200
		private class FindAsyncOperationContext : AsyncOperationContext
		{
			// Token: 0x060007CF RID: 1999 RVA: 0x0001463C File Offset: 0x0001283C
			internal FindAsyncOperationContext(UniqueId operationId, int maxResults, TimeSpan duration, object userState) : base(operationId, maxResults, duration, userState)
			{
				this.result = new FindResponse();
				if (base.UserState != null)
				{
					Type type = base.UserState.GetType();
					if (type.IsGenericType && type.GetGenericTypeDefinition() == DiscoveryClient.FindAsyncOperationContext.TaskAsyncOperationStateType)
					{
						this.IsTaskBasedOperation = true;
					}
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00014694 File Offset: 0x00012894
			public FindResponse Result
			{
				get
				{
					return this.result;
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0001469C File Offset: 0x0001289C
			// (set) Token: 0x060007D2 RID: 2002 RVA: 0x000146A4 File Offset: 0x000128A4
			public bool IsTaskBasedOperation { get; private set; }

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x060007D3 RID: 2003 RVA: 0x000146B0 File Offset: 0x000128B0
			public int Progress
			{
				get
				{
					int num = 0;
					if (base.MaxResults != 2147483647)
					{
						num = (int)((float)this.Result.Endpoints.Count / (float)base.MaxResults * 100f);
					}
					else if (base.StartedAt != null)
					{
						num = (int)(DateTime.UtcNow.Subtract(base.StartedAt.Value).TotalMilliseconds / base.Duration.TotalMilliseconds * 100.0);
					}
					return num;
				}
			}

			// Token: 0x040001EA RID: 490
			private static Type TaskAsyncOperationStateType = typeof(DiscoveryClient.TaskAsyncOperationState<>);

			// Token: 0x040001EB RID: 491
			private FindResponse result;
		}

		// Token: 0x020000C9 RID: 201
		private class ResolveAsyncOperationContext : AsyncOperationContext
		{
			// Token: 0x060007D5 RID: 2005 RVA: 0x00014750 File Offset: 0x00012950
			internal ResolveAsyncOperationContext(UniqueId operationId, TimeSpan duration, object userState) : base(operationId, 1, duration, userState)
			{
				this.result = new ResolveResponse();
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00014767 File Offset: 0x00012967
			public ResolveResponse Result
			{
				get
				{
					return this.result;
				}
			}

			// Token: 0x040001ED RID: 493
			private ResolveResponse result;
		}

		// Token: 0x020000CA RID: 202
		private class TaskAsyncOperationState<TResult>
		{
			// Token: 0x060007D7 RID: 2007 RVA: 0x0001476F File Offset: 0x0001296F
			internal TaskAsyncOperationState(DiscoveryClient discoveryClient, TaskCompletionSource<TResult> taskCompletionSource, CancellationToken cancellationToken)
			{
				this.taskCompletionSource = taskCompletionSource;
				this.cancellationToken = cancellationToken;
				cancellationToken.Register(discoveryClient.cancelTaskCallbackDelegate, this);
			}

			// Token: 0x060007D8 RID: 2008 RVA: 0x00014794 File Offset: 0x00012994
			internal void Complete(TResult result, Exception error, bool cancelled)
			{
				if (cancelled)
				{
					this.taskCompletionSource.TrySetCanceled();
					return;
				}
				if (error != null)
				{
					this.taskCompletionSource.TrySetException(error);
					return;
				}
				this.taskCompletionSource.TrySetResult(result);
			}

			// Token: 0x040001EE RID: 494
			private TaskCompletionSource<TResult> taskCompletionSource;

			// Token: 0x040001EF RID: 495
			private CancellationToken cancellationToken;
		}
	}
}
