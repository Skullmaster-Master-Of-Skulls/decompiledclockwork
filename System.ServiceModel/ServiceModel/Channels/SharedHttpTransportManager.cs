using System;
using System.Diagnostics;
using System.Net;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000874 RID: 2164
	internal class SharedHttpTransportManager : HttpTransportManager
	{
		// Token: 0x060051E3 RID: 20963 RVA: 0x0012D39C File Offset: 0x0012B59C
		internal SharedHttpTransportManager(Uri listenUri, HttpChannelListener channelListener) : base(listenUri, channelListener.HostNameComparisonMode, channelListener.Realm)
		{
			this.onGetContext = Fx.ThunkCallback(new AsyncCallback(this.OnGetContext));
			this.onMessageDequeued = new Action(this.OnMessageDequeued);
			this.unsafeConnectionNtlmAuthentication = channelListener.UnsafeConnectionNtlmAuthentication;
			this.onContextReceived = new AsyncCallback(this.HandleHttpContextReceived);
			this.listenerRWLock = new ReaderWriterLockSlim();
			this.maxPendingAccepts = channelListener.MaxPendingAccepts;
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x0012D41C File Offset: 0x0012B61C
		internal override bool IsCompatible(HttpChannelListener channelListener)
		{
			return channelListener.InheritBaseAddressSettings || (channelListener.IsScopeIdCompatible(base.HostNameComparisonMode, base.ListenUri) && this.maxPendingAccepts == channelListener.MaxPendingAccepts && channelListener.UnsafeConnectionNtlmAuthentication == this.unsafeConnectionNtlmAuthentication && base.IsCompatible(channelListener));
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x0012D470 File Offset: 0x0012B670
		internal override void OnClose(TimeSpan timeout)
		{
			this.Cleanup(false, timeout);
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x0012D47A File Offset: 0x0012B67A
		internal override void OnAbort()
		{
			this.Cleanup(true, TimeSpan.Zero);
			base.OnAbort();
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x0012D490 File Offset: 0x0012B690
		private void Cleanup(bool aborting, TimeSpan timeout)
		{
			using (LockHelper.TakeWriterLock(this.listenerRWLock))
			{
				HttpListener httpListener = this.listener;
				if (httpListener != null)
				{
					try
					{
						httpListener.Stop();
					}
					finally
					{
						try
						{
							httpListener.Close();
						}
						finally
						{
							if (!aborting)
							{
								base.OnClose(timeout);
							}
							else
							{
								base.OnAbort();
							}
						}
					}
					this.listener = null;
				}
			}
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x0012D518 File Offset: 0x0012B718
		[SecuritySafeCritical]
		private IAsyncResult BeginGetContext(bool startListening)
		{
			EventTraceActivity eventTraceActivity = null;
			if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled)
			{
				eventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(true);
				if (TD.HttpGetContextStartIsEnabled())
				{
					TD.HttpGetContextStart(eventTraceActivity);
				}
			}
			Exception ex;
			do
			{
				ex = null;
				try
				{
					try
					{
						if (ExecutionContext.IsFlowSuppressed())
						{
							return this.BeginGetContextCore(eventTraceActivity);
						}
						using (ExecutionContext.SuppressFlow())
						{
							return this.BeginGetContextCore(eventTraceActivity);
						}
					}
					catch (HttpListenerException e)
					{
						if (!this.HandleHttpException(e))
						{
							throw;
						}
					}
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					if (startListening)
					{
						throw;
					}
					ex = ex2;
				}
			}
			while (ex == null);
			base.Fault(ex);
			return null;
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x0012D5D8 File Offset: 0x0012B7D8
		private IAsyncResult BeginGetContextCore(EventTraceActivity eventTraceActivity)
		{
			IAsyncResult result;
			using (LockHelper.TakeReaderLock(this.listenerRWLock))
			{
				if (this.listener == null)
				{
					result = null;
				}
				else
				{
					result = this.listener.BeginGetContext(this.onGetContext, eventTraceActivity);
				}
			}
			return result;
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x0012D630 File Offset: 0x0012B830
		private void OnGetContext(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			this.OnGetContextCore(result);
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x0012D642 File Offset: 0x0012B842
		private void OnCompleteGetContextLater(object state)
		{
			this.OnGetContextCore((IAsyncResult)state);
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x0012D650 File Offset: 0x0012B850
		private void OnGetContextCore(IAsyncResult listenerContextResult)
		{
			bool flag = false;
			while (!flag)
			{
				Exception ex = null;
				try
				{
					try
					{
						flag = this.EnqueueContext(listenerContextResult);
					}
					catch (HttpListenerException e)
					{
						if (!this.HandleHttpException(e))
						{
							throw;
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
					base.Fault(ex);
				}
				if (!flag)
				{
					listenerContextResult = this.BeginGetContext(false);
					if (listenerContextResult == null || !listenerContextResult.CompletedSynchronously)
					{
						return;
					}
				}
			}
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x0012D6D0 File Offset: 0x0012B8D0
		private bool EnqueueContext(IAsyncResult listenerContextResult)
		{
			EventTraceActivity eventTraceActivity = null;
			bool flag = false;
			if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled)
			{
				eventTraceActivity = (EventTraceActivity)listenerContextResult.AsyncState;
				if (eventTraceActivity == null)
				{
					eventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(true);
				}
			}
			HttpListenerContext httpListenerContext;
			using (LockHelper.TakeReaderLock(this.listenerRWLock))
			{
				if (this.listener == null)
				{
					return true;
				}
				httpListenerContext = this.listener.EndGetContext(listenerContextResult);
			}
			using (DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.BoundOperation(base.Activity) : null)
			{
				ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivityWithTransferInOnly(httpListenerContext.Request.RequestTraceIdentifier) : null;
				try
				{
					if (serviceModelActivity != null)
					{
						base.StartReceiveBytesActivity(serviceModelActivity, httpListenerContext.Request.Url);
					}
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceHttpConnectionInformation(httpListenerContext.Request.LocalEndPoint.ToString(), httpListenerContext.Request.RemoteEndPoint.ToString(), this);
					}
					base.TraceMessageReceived(eventTraceActivity, base.ListenUri);
					HttpChannelListener httpChannelListener;
					if (base.TryLookupUri(httpListenerContext.Request.Url, httpListenerContext.Request.HttpMethod, base.HostNameComparisonMode, httpListenerContext.Request.IsWebSocketRequest, out httpChannelListener))
					{
						HttpRequestContext httpRequestContext = HttpRequestContext.CreateContext(httpChannelListener, httpListenerContext, eventTraceActivity);
						IAsyncResult asyncResult = httpChannelListener.BeginHttpContextReceived(httpRequestContext, this.onMessageDequeued, this.onContextReceived, DiagnosticUtility.ShouldUseActivity ? new HttpTransportManager.ActivityHolder(serviceModelActivity, httpRequestContext) : httpRequestContext);
						if (asyncResult.CompletedSynchronously)
						{
							flag = SharedHttpTransportManager.EndHttpContextReceived(asyncResult);
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						SharedHttpTransportManager.HandleMessageReceiveFailed(httpListenerContext);
					}
				}
				finally
				{
					if (DiagnosticUtility.ShouldUseActivity && serviceModelActivity != null && !flag)
					{
						serviceModelActivity.Dispose();
					}
				}
			}
			return flag;
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x0012D8B4 File Offset: 0x0012BAB4
		private void HandleHttpContextReceived(IAsyncResult httpContextReceivedResult)
		{
			if (httpContextReceivedResult.CompletedSynchronously)
			{
				return;
			}
			bool flag = false;
			Exception ex = null;
			try
			{
				try
				{
					flag = SharedHttpTransportManager.EndHttpContextReceived(httpContextReceivedResult);
				}
				catch (HttpListenerException e)
				{
					if (!this.HandleHttpException(e))
					{
						throw;
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
				base.Fault(ex);
			}
			if (!flag)
			{
				IAsyncResult asyncResult = this.BeginGetContext(false);
				if (asyncResult == null || !asyncResult.CompletedSynchronously)
				{
					return;
				}
				this.OnGetContextCore(asyncResult);
			}
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x0012D940 File Offset: 0x0012BB40
		private static bool EndHttpContextReceived(IAsyncResult httpContextReceivedResult)
		{
			bool result;
			using (DiagnosticUtility.ShouldUseActivity ? ((HttpTransportManager.ActivityHolder)httpContextReceivedResult.AsyncState) : null)
			{
				HttpChannelListener httpChannelListener = (DiagnosticUtility.ShouldUseActivity ? ((HttpTransportManager.ActivityHolder)httpContextReceivedResult.AsyncState).context : ((HttpRequestContext)httpContextReceivedResult.AsyncState)).Listener;
				result = httpChannelListener.EndHttpContextReceived(httpContextReceivedResult);
			}
			return result;
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x0012D9B4 File Offset: 0x0012BBB4
		private bool HandleHttpException(HttpListenerException e)
		{
			int errorCode = e.ErrorCode;
			if (errorCode == 8 || errorCode == 14 || errorCode == 1450)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InsufficientMemoryException(SR.GetString("InsufficentMemory"), e));
			}
			return ExceptionHandler.HandleTransportExceptionHelper(e);
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x0012D9FC File Offset: 0x0012BBFC
		private static void HandleMessageReceiveFailed(HttpListenerContext listenerContext)
		{
			SharedHttpTransportManager.TraceMessageReceiveFailed();
			if (string.Compare(listenerContext.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase) != 0)
			{
				listenerContext.Response.StatusCode = 405;
				listenerContext.Response.Headers.Add(HttpResponseHeader.Allow, "POST");
			}
			else
			{
				listenerContext.Response.StatusCode = 404;
			}
			listenerContext.Response.ContentLength64 = 0L;
			listenerContext.Response.Close();
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x0012DA77 File Offset: 0x0012BC77
		private static void TraceMessageReceiveFailed()
		{
			if (TD.HttpMessageReceiveStartIsEnabled())
			{
				TD.HttpMessageReceiveFailed();
			}
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 262155, SR.GetString("TraceCodeHttpChannelMessageReceiveFailed"), null);
			}
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x0012DAA4 File Offset: 0x0012BCA4
		private void StartListening()
		{
			for (int i = 0; i < this.maxPendingAccepts; i++)
			{
				IAsyncResult asyncResult = this.BeginGetContext(true);
				if (asyncResult.CompletedSynchronously)
				{
					if (this.onCompleteGetContextLater == null)
					{
						this.onCompleteGetContextLater = new Action<object>(this.OnCompleteGetContextLater);
					}
					ActionItem.Schedule(this.onCompleteGetContextLater, asyncResult);
				}
			}
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x0012DAF8 File Offset: 0x0012BCF8
		private void OnListening(object state)
		{
			try
			{
				this.StartListening();
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				this.listenStartedException = exception;
			}
			finally
			{
				this.listenStartedEvent.Set();
			}
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x0012DB4C File Offset: 0x0012BD4C
		private void OnMessageDequeued()
		{
			ThreadTrace.Trace("message dequeued");
			IAsyncResult asyncResult = this.BeginGetContext(false);
			if (asyncResult != null && asyncResult.CompletedSynchronously)
			{
				if (this.onCompleteGetContextLater == null)
				{
					this.onCompleteGetContextLater = new Action<object>(this.OnCompleteGetContextLater);
				}
				ActionItem.Schedule(this.onCompleteGetContextLater, asyncResult);
			}
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x0012DB9C File Offset: 0x0012BD9C
		internal override void OnOpen()
		{
			this.listener = new HttpListener();
			string text;
			switch (base.HostNameComparisonMode)
			{
			case HostNameComparisonMode.StrongWildcard:
				text = "+";
				break;
			case HostNameComparisonMode.Exact:
				if (base.ListenUri.HostNameType == UriHostNameType.IPv6)
				{
					text = "[" + base.ListenUri.DnsSafeHost + "]";
				}
				else
				{
					text = base.ListenUri.NormalizedHost();
				}
				break;
			case HostNameComparisonMode.WeakWildcard:
				text = "*";
				break;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnrecognizedHostNameComparisonMode", new object[]
				{
					base.HostNameComparisonMode.ToString()
				})));
			}
			string text2 = base.ListenUri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
			if (!text2.StartsWith("/", StringComparison.Ordinal))
			{
				text2 = "/" + text2;
			}
			if (!text2.EndsWith("/", StringComparison.Ordinal))
			{
				text2 += "/";
			}
			string text3 = string.Concat(new object[]
			{
				this.Scheme,
				"://",
				text,
				":",
				base.ListenUri.Port,
				text2
			});
			this.listener.UnsafeConnectionNtlmAuthentication = this.unsafeConnectionNtlmAuthentication;
			this.listener.AuthenticationSchemeSelectorDelegate = new AuthenticationSchemeSelector(this.SelectAuthenticationScheme);
			if (ExtendedProtectionPolicy.OSSupportsExtendedProtection)
			{
				this.listener.ExtendedProtectionSelectorDelegate = new HttpListener.ExtendedProtectionSelector(this.SelectExtendedProtectionPolicy);
			}
			if (base.Realm != null)
			{
				this.listener.Realm = base.Realm;
			}
			bool flag = false;
			try
			{
				this.listener.Prefixes.Add(text3);
				this.listener.Start();
				bool flag2 = false;
				try
				{
					if (Thread.CurrentThread.IsThreadPoolThread)
					{
						this.StartListening();
					}
					else
					{
						this.listenStartedEvent = new ManualResetEvent(false);
						ActionItem.Schedule(new Action<object>(this.OnListening), null);
						this.listenStartedEvent.WaitOne();
						this.listenStartedEvent.Close();
						this.listenStartedEvent = null;
						if (this.listenStartedException != null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.listenStartedException);
						}
					}
					flag2 = true;
				}
				finally
				{
					if (!flag2)
					{
						this.listener.Stop();
					}
				}
				flag = true;
			}
			catch (HttpListenerException ex)
			{
				int nativeErrorCode = ex.NativeErrorCode;
				if (nativeErrorCode <= 32)
				{
					if (nativeErrorCode == 5)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AddressAccessDeniedException(SR.GetString("HttpRegistrationAccessDenied", new object[]
						{
							text3
						}), ex));
					}
					if (nativeErrorCode == 32)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AddressAlreadyInUseException(SR.GetString("HttpRegistrationPortInUse", new object[]
						{
							text3,
							base.ListenUri.Port
						}), ex));
					}
				}
				else
				{
					if (nativeErrorCode == 87)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("HttpInvalidListenURI", new object[]
						{
							base.ListenUri.OriginalString
						}), ex));
					}
					if (nativeErrorCode == 183)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AddressAlreadyInUseException(SR.GetString("HttpRegistrationAlreadyExists", new object[]
						{
							text3
						}), ex));
					}
					if (nativeErrorCode == 1344)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("HttpRegistrationLimitExceeded", new object[]
						{
							text3
						}), ex));
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateCommunicationException(ex));
			}
			finally
			{
				if (!flag)
				{
					this.listener.Abort();
				}
			}
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x0012DF6C File Offset: 0x0012C16C
		private AuthenticationSchemes SelectAuthenticationScheme(HttpListenerRequest request)
		{
			AuthenticationSchemes result;
			try
			{
				HttpChannelListener httpChannelListener;
				AuthenticationSchemes authenticationSchemes;
				if (base.TryLookupUri(request.Url, request.HttpMethod, base.HostNameComparisonMode, request.IsWebSocketRequest, out httpChannelListener))
				{
					authenticationSchemes = httpChannelListener.AuthenticationScheme;
				}
				else
				{
					authenticationSchemes = AuthenticationSchemes.Anonymous;
				}
				result = authenticationSchemes;
			}
			catch (Exception exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
				throw;
			}
			return result;
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x0012DFCC File Offset: 0x0012C1CC
		private ExtendedProtectionPolicy SelectExtendedProtectionPolicy(HttpListenerRequest request)
		{
			ExtendedProtectionPolicy result;
			try
			{
				HttpChannelListener httpChannelListener;
				ExtendedProtectionPolicy extendedProtectionPolicy;
				if (base.TryLookupUri(request.Url, request.HttpMethod, base.HostNameComparisonMode, request.IsWebSocketRequest, out httpChannelListener))
				{
					extendedProtectionPolicy = httpChannelListener.ExtendedProtectionPolicy;
				}
				else
				{
					extendedProtectionPolicy = ChannelBindingUtility.DisabledPolicy;
				}
				result = extendedProtectionPolicy;
			}
			catch (Exception exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
				throw;
			}
			return result;
		}

		// Token: 0x0400322C RID: 12844
		private int maxPendingAccepts;

		// Token: 0x0400322D RID: 12845
		private HttpListener listener;

		// Token: 0x0400322E RID: 12846
		private ManualResetEvent listenStartedEvent;

		// Token: 0x0400322F RID: 12847
		private Exception listenStartedException;

		// Token: 0x04003230 RID: 12848
		private AsyncCallback onGetContext;

		// Token: 0x04003231 RID: 12849
		private AsyncCallback onContextReceived;

		// Token: 0x04003232 RID: 12850
		private Action onMessageDequeued;

		// Token: 0x04003233 RID: 12851
		private Action<object> onCompleteGetContextLater;

		// Token: 0x04003234 RID: 12852
		private bool unsafeConnectionNtlmAuthentication;

		// Token: 0x04003235 RID: 12853
		private ReaderWriterLockSlim listenerRWLock;
	}
}
