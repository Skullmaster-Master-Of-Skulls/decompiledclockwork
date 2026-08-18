using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.ServiceProcess;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000821 RID: 2081
	internal sealed class SharedConnectionListener : IConnectionListener, IDisposable
	{
		// Token: 0x06004DC8 RID: 19912 RVA: 0x0011C530 File Offset: 0x0011A730
		internal SharedConnectionListener(BaseUriWithWildcard baseAddress, int queueId, Guid token, Func<Uri, int> onDuplicatedViaCallback)
		{
			this.baseAddress = baseAddress;
			this.queueId = queueId;
			this.token = token;
			this.onDuplicatedViaCallback = onDuplicatedViaCallback;
			this.connectionQueue = TraceUtility.CreateInputQueue<SharedConnectionListener.DuplicateConnectionAsyncResult>();
			this.state = CommunicationState.Created;
			this.reconnectEvent = new ManualResetEvent(true);
			this.StartListen(false);
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06004DC9 RID: 19913 RVA: 0x0011C590 File Offset: 0x0011A790
		private object ThisLock
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x06004DCA RID: 19914 RVA: 0x0011C598 File Offset: 0x0011A798
		void IConnectionListener.Listen()
		{
		}

		// Token: 0x06004DCB RID: 19915 RVA: 0x0011C59A File Offset: 0x0011A79A
		IAsyncResult IConnectionListener.BeginAccept(AsyncCallback callback, object state)
		{
			return this.connectionQueue.BeginDequeue(TimeSpan.MaxValue, callback, state);
		}

		// Token: 0x06004DCC RID: 19916 RVA: 0x0011C5AE File Offset: 0x0011A7AE
		public void Stop(TimeSpan timeout)
		{
			this.Stop(false, timeout);
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x0011C5B8 File Offset: 0x0011A7B8
		public void Stop(bool aborting, TimeSpan timeout)
		{
			bool flag = false;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.state == CommunicationState.Closing || this.state == CommunicationState.Closed)
				{
					return;
				}
				if (this.state == CommunicationState.Opening && !aborting)
				{
					flag = true;
				}
				this.state = CommunicationState.Closing;
			}
			bool flag3 = false;
			try
			{
				if (flag && !this.reconnectEvent.WaitOne(timeoutHelper.RemainingTime()))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new System.ServiceProcess.TimeoutException(SR.GetString("TimeoutOnClose", new object[]
					{
						timeoutHelper.OriginalTimeout
					})));
				}
				flag3 = true;
			}
			finally
			{
				if (this.listenerProxy != null)
				{
					if (aborting || !flag3)
					{
						this.listenerProxy.Abort();
					}
					else
					{
						this.listenerProxy.Close(timeoutHelper.RemainingTime());
					}
				}
			}
		}

		// Token: 0x06004DCE RID: 19918 RVA: 0x0011C6B0 File Offset: 0x0011A8B0
		private void Close()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.state == CommunicationState.Closed)
				{
					return;
				}
				this.state = CommunicationState.Closed;
			}
			if (this.connectionQueue != null)
			{
				this.connectionQueue.Close();
			}
			if (this.reconnectEvent != null)
			{
				this.reconnectEvent.Close();
			}
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x0011C724 File Offset: 0x0011A924
		public void Abort()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.state == CommunicationState.Closed)
				{
					return;
				}
				if (this.reconnectEvent != null)
				{
					this.reconnectEvent.Set();
				}
				this.Stop(true, TimeSpan.Zero);
			}
			this.Close();
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x0011C790 File Offset: 0x0011A990
		private void OnConnectionAvailable(SharedConnectionListener.DuplicateConnectionAsyncResult result)
		{
			this.connectionQueue.EnqueueAndDispatch(result, null, false);
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x0011C7A0 File Offset: 0x0011A9A0
		private static string GetServiceName(bool isTcp)
		{
			if (!isTcp)
			{
				return "NetPipeActivator";
			}
			return "NetTcpPortSharing";
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x0011C7B0 File Offset: 0x0011A9B0
		IConnection IConnectionListener.EndAccept(IAsyncResult result)
		{
			object thisLock = this.ThisLock;
			IConnection result2;
			lock (thisLock)
			{
				if (this.state != CommunicationState.Opening && this.state != CommunicationState.Opened)
				{
					result2 = null;
				}
				else
				{
					SharedConnectionListener.DuplicateConnectionAsyncResult duplicateConnectionAsyncResult = this.connectionQueue.EndDequeue(result);
					duplicateConnectionAsyncResult.CompleteOperation();
					result2 = duplicateConnectionAsyncResult.Connection;
				}
			}
			return result2;
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x0011C81C File Offset: 0x0011AA1C
		private void OnListenerFaulted(bool shouldReconnect)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.state == CommunicationState.Closing || this.state == CommunicationState.Closed)
				{
					return;
				}
				this.listenerProxy.Abort();
				if (shouldReconnect)
				{
					this.state = CommunicationState.Opening;
					this.reconnectEvent.Reset();
				}
				else
				{
					this.state = CommunicationState.Faulted;
				}
			}
			if (shouldReconnect)
			{
				if (this.reconnectCallback == null)
				{
					this.reconnectCallback = new Action<object>(this.ReconnectCallback);
				}
				ActionItem.Schedule(this.reconnectCallback, this);
			}
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x0011C8C0 File Offset: 0x0011AAC0
		private void StartListen(bool isReconnecting)
		{
			this.listenerProxy = new SharedConnectionListener.SharedListenerProxy(this);
			if (isReconnecting)
			{
				this.reconnectEvent.Set();
			}
			this.listenerProxy.Open(isReconnecting);
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.state == CommunicationState.Created || this.state == CommunicationState.Opening)
				{
					this.state = CommunicationState.Opened;
				}
			}
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x0011C93C File Offset: 0x0011AB3C
		private void ReconnectCallback(object state)
		{
			BackoffTimeoutHelper backoffTimeoutHelper = new BackoffTimeoutHelper(TimeSpan.MaxValue, TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(30.0));
			while (this.state == CommunicationState.Opening)
			{
				try
				{
					this.StartListen(true);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
				}
				if (this.state == CommunicationState.Opening)
				{
					backoffTimeoutHelper.WaitAndBackoff();
				}
			}
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x0011C9BC File Offset: 0x0011ABBC
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x040030A7 RID: 12455
		private BaseUriWithWildcard baseAddress;

		// Token: 0x040030A8 RID: 12456
		private int queueId;

		// Token: 0x040030A9 RID: 12457
		private Guid token;

		// Token: 0x040030AA RID: 12458
		private InputQueue<SharedConnectionListener.DuplicateConnectionAsyncResult> connectionQueue;

		// Token: 0x040030AB RID: 12459
		private SharedConnectionListener.SharedListenerProxy listenerProxy;

		// Token: 0x040030AC RID: 12460
		private Action<object> reconnectCallback;

		// Token: 0x040030AD RID: 12461
		private object syncRoot = new object();

		// Token: 0x040030AE RID: 12462
		private CommunicationState state;

		// Token: 0x040030AF RID: 12463
		private ManualResetEvent reconnectEvent;

		// Token: 0x040030B0 RID: 12464
		private Func<Uri, int> onDuplicatedViaCallback;

		// Token: 0x040030B1 RID: 12465
		private static readonly Version ProtocolVersion = new Version(3, 0, 0, 0);

		// Token: 0x02000D24 RID: 3364
		[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
		private class SharedListenerProxy : IConnectionDuplicator, IInputSessionShutdown
		{
			// Token: 0x06007BB4 RID: 31668 RVA: 0x001CD84C File Offset: 0x001CBA4C
			public SharedListenerProxy(SharedConnectionListener parent)
			{
				this.parent = parent;
				this.baseAddress = parent.baseAddress;
				this.queueId = parent.queueId;
				this.token = parent.token;
				this.onDuplicatedViaCallback = parent.onDuplicatedViaCallback;
				this.isTcp = parent.baseAddress.BaseAddress.Scheme.Equals(Uri.UriSchemeNetTcp);
				this.securityEventName = Guid.NewGuid().ToString();
				this.serviceName = SharedConnectionListener.GetServiceName(this.isTcp);
				this.readerWriterLock = new ReaderWriterLockSlim();
				this.validateUriCallThrottle = new ThreadNeutralSemaphore(10 * Environment.ProcessorCount, () => null);
			}

			// Token: 0x06007BB5 RID: 31669 RVA: 0x001CD920 File Offset: 0x001CBB20
			public void Open(bool isReconnecting)
			{
				if (this.closed)
				{
					return;
				}
				this.listenerEndPoint = this.HandleServiceStart(isReconnecting);
				if (string.IsNullOrEmpty(this.listenerEndPoint))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("Sharing_EmptyListenerEndpoint", new object[]
					{
						this.serviceName
					})));
				}
				if (this.closed)
				{
					return;
				}
				this.LookupListenerSid();
				EventWaitHandle eventWaitHandle = null;
				bool flag = false;
				using (LockHelper.TakeWriterLock(this.readerWriterLock))
				{
					try
					{
						this.CreateControlProxy();
						EventWaitHandleSecurity eventWaitHandleSecurity = new EventWaitHandleSecurity();
						eventWaitHandleSecurity.AddAccessRule(new EventWaitHandleAccessRule(this.listenerUniqueSid, EventWaitHandleRights.Modify, AccessControlType.Allow));
						bool flag2;
						eventWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset, "Global\\" + this.securityEventName, ref flag2, eventWaitHandleSecurity);
						if (!flag2)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SharedManagerBase", new object[]
							{
								this.serviceName,
								SR.GetString("SharedManagerServiceSecurityFailed")
							})));
						}
						this.Register();
						if (!eventWaitHandle.WaitOne(0, false))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SharedManagerBase", new object[]
							{
								this.serviceName,
								SR.GetString("SharedManagerServiceSecurityFailed")
							})));
						}
						if (DiagnosticUtility.ShouldTraceInformation)
						{
							TraceUtility.TraceEvent(TraceEventType.Information, 655365, SR.GetString("TraceCodePortSharingListening"));
						}
						this.opened = true;
						flag = true;
					}
					finally
					{
						if (eventWaitHandle != null)
						{
							eventWaitHandle.Close();
						}
						if (!flag)
						{
							this.Cleanup(true, TimeSpan.Zero);
							this.closed = true;
						}
					}
				}
			}

			// Token: 0x06007BB6 RID: 31670 RVA: 0x001CDADC File Offset: 0x001CBCDC
			public void Close(TimeSpan timeout)
			{
				this.Close(false, timeout);
			}

			// Token: 0x06007BB7 RID: 31671 RVA: 0x001CDAE8 File Offset: 0x001CBCE8
			private void Close(bool isAborting, TimeSpan timeout)
			{
				using (LockHelper.TakeWriterLock(this.readerWriterLock))
				{
					if (this.closed)
					{
						return;
					}
					bool flag = false;
					try
					{
						this.Cleanup(isAborting, timeout);
						flag = true;
					}
					finally
					{
						if (!flag && !isAborting)
						{
							this.Abort();
						}
						this.closed = true;
					}
				}
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 655361, SR.GetString("TraceCodePortSharingClosed"));
				}
			}

			// Token: 0x06007BB8 RID: 31672 RVA: 0x001CDB70 File Offset: 0x001CBD70
			private void Cleanup(bool isAborting, TimeSpan timeout)
			{
				this.validateUriCallThrottle.Abort();
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				bool flag = false;
				if (this.controlSessionWithListener != null)
				{
					if (!isAborting)
					{
						try
						{
							this.Unregister(timeoutHelper.RemainingTime());
							this.controlSessionWithListener.Close(timeoutHelper.RemainingTime());
							flag = true;
						}
						catch (Exception exception)
						{
							if (Fx.IsFatal(exception))
							{
								throw;
							}
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
						}
					}
					if (isAborting || !flag)
					{
						this.controlSessionWithListener.Abort();
					}
				}
				if (this.channelFactory != null)
				{
					flag = false;
					if (!isAborting)
					{
						try
						{
							this.channelFactory.Close(timeoutHelper.RemainingTime());
							flag = true;
						}
						catch (Exception exception2)
						{
							if (Fx.IsFatal(exception2))
							{
								throw;
							}
							DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Error);
						}
					}
					if (isAborting || !flag)
					{
						this.channelFactory.Abort();
					}
				}
				if (this.allowContext != null)
				{
					this.allowContext.Dispose();
				}
			}

			// Token: 0x06007BB9 RID: 31673 RVA: 0x001CDC5C File Offset: 0x001CBE5C
			public void Abort()
			{
				this.Close(true, TimeSpan.Zero);
			}

			// Token: 0x06007BBA RID: 31674 RVA: 0x001CDC6A File Offset: 0x001CBE6A
			private void Unregister(TimeSpan timeout)
			{
				this.controlSessionWithListener.OperationTimeout = timeout;
				((IConnectionRegister)this.controlSessionWithListener).Unregister();
			}

			// Token: 0x06007BBB RID: 31675 RVA: 0x001CDC88 File Offset: 0x001CBE88
			private void LookupListenerSid()
			{
				if (OSEnvironmentHelper.IsVistaOrGreater)
				{
					try
					{
						this.listenerUniqueSid = Utility.GetWindowsServiceSid(this.serviceName);
						return;
					}
					catch (Win32Exception ex)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SharedManagerBase", new object[]
						{
							this.serviceName,
							SR.GetString("SharedManagerServiceSidLookupFailure", new object[]
							{
								ex.NativeErrorCode
							})
						}), ex));
					}
				}
				int pidForService;
				try
				{
					pidForService = Utility.GetPidForService(this.serviceName);
				}
				catch (Win32Exception ex2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SharedManagerBase", new object[]
					{
						this.serviceName,
						SR.GetString("SharedManagerServiceLookupFailure", new object[]
						{
							ex2.NativeErrorCode
						})
					}), ex2));
				}
				try
				{
					this.listenerUserSid = Utility.GetUserSidForPid(pidForService);
				}
				catch (Win32Exception ex3)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SharedManagerBase", new object[]
					{
						this.serviceName,
						SR.GetString("SharedManagerUserSidLookupFailure", new object[]
						{
							ex3.NativeErrorCode
						})
					}), ex3));
				}
				try
				{
					this.listenerUniqueSid = Utility.GetLogonSidForPid(pidForService);
				}
				catch (Win32Exception ex4)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SharedManagerBase", new object[]
					{
						this.serviceName,
						SR.GetString("SharedManagerLogonSidLookupFailure", new object[]
						{
							ex4.NativeErrorCode
						})
					}), ex4));
				}
			}

			// Token: 0x06007BBC RID: 31676 RVA: 0x001CDE48 File Offset: 0x001CC048
			private void CreateControlProxy()
			{
				EndpointAddress remoteAddress = new EndpointAddress(Utility.FormatListenerEndpoint(this.serviceName, this.listenerEndPoint), new AddressHeader[0]);
				NamedPipeTransportBindingElement namedPipeTransportBindingElement = new NamedPipeTransportBindingElement();
				CustomBinding binding = new CustomBinding(new BindingElement[]
				{
					namedPipeTransportBindingElement
				});
				InstanceContext callbackInstance = new InstanceContext(null, this, false);
				ChannelFactory<IConnectionRegisterAsync> channelFactory = new DuplexChannelFactory<IConnectionRegisterAsync>(callbackInstance, binding, remoteAddress);
				channelFactory.Endpoint.Behaviors.Add(new SharedConnectionListener.SharedListenerProxy.SharedListenerProxyBehavior(this));
				IConnectionRegister connectionRegister = channelFactory.CreateChannel();
				this.channelFactory = channelFactory;
				this.controlSessionWithListener = (connectionRegister as IDuplexContextChannel);
			}

			// Token: 0x06007BBD RID: 31677 RVA: 0x001CDED0 File Offset: 0x001CC0D0
			private void Register()
			{
				if (TD.SharedListenerProxyRegisterStartIsEnabled())
				{
					TD.SharedListenerProxyRegisterStart((this.baseAddress != null) ? this.baseAddress.ToString() : string.Empty);
				}
				Version protocolVersion = SharedConnectionListener.ProtocolVersion;
				int id = Process.GetCurrentProcess().Id;
				this.HandleAllowDupHandlePermission(id);
				ListenerExceptionStatus listenerExceptionStatus = ((IConnectionRegister)this.controlSessionWithListener).Register(protocolVersion, id, this.baseAddress, this.queueId, this.token, this.securityEventName);
				if (listenerExceptionStatus == ListenerExceptionStatus.Success)
				{
					if (TD.SharedListenerProxyRegisterStopIsEnabled())
					{
						TD.SharedListenerProxyRegisterStop();
					}
					return;
				}
				if (TD.SharedListenerProxyRegisterFailedIsEnabled())
				{
					TD.SharedListenerProxyRegisterFailed(listenerExceptionStatus.ToString());
				}
				if (listenerExceptionStatus == ListenerExceptionStatus.ConflictingRegistration)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AddressAlreadyInUseException(SR.GetString("SharedManagerBase", new object[]
					{
						this.serviceName,
						SR.GetString("SharedManagerConflictingRegistration")
					})));
				}
				if (listenerExceptionStatus != ListenerExceptionStatus.FailedToListen)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SharedManagerBase", new object[]
					{
						this.serviceName,
						SR.GetString("SharedManager" + listenerExceptionStatus.ToString())
					})));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AddressAlreadyInUseException(SR.GetString("SharedManagerBase", new object[]
				{
					this.serviceName,
					SR.GetString("SharedManagerFailedToListen")
				})));
			}

			// Token: 0x06007BBE RID: 31678 RVA: 0x001CE030 File Offset: 0x001CC230
			private void HandleAllowDupHandlePermission(int myPid)
			{
				bool flag = !OSEnvironmentHelper.IsVistaOrGreater && this.listenerUserSid.Equals(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null));
				if (flag)
				{
					return;
				}
				SecurityIdentifier userSidForPid;
				try
				{
					userSidForPid = Utility.GetUserSidForPid(myPid);
				}
				catch (Win32Exception ex)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SharedManagerBase", new object[]
					{
						this.serviceName,
						SR.GetString("SharedManagerCurrentUserSidLookupFailure", new object[]
						{
							ex.NativeErrorCode
						})
					}), ex));
				}
				flag = (!OSEnvironmentHelper.IsVistaOrGreater && userSidForPid.Equals(this.listenerUserSid));
				if (flag)
				{
					return;
				}
				try
				{
					this.allowContext = AllowHelper.TryAllow(this.listenerUniqueSid.Value);
				}
				catch (Win32Exception innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SharedManagerBase", new object[]
					{
						this.serviceName,
						SR.GetString("SharedManagerAllowDupHandleFailed", new object[]
						{
							this.listenerUniqueSid.Value
						})
					}), innerException));
				}
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 655363, SR.GetString("TraceCodePortSharingDupHandleGranted", new object[]
					{
						this.serviceName,
						this.listenerUniqueSid.Value
					}));
				}
			}

			// Token: 0x06007BBF RID: 31679 RVA: 0x001CE18C File Offset: 0x001CC38C
			private IConnection BuildDuplicatedNamedPipeConnection(NamedPipeDuplicateContext duplicateContext, int connectionBufferSize)
			{
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 655362, SR.GetString("TraceCodePortSharingDuplicatedPipe"));
				}
				PipeHandle pipe = new PipeHandle(duplicateContext.Handle);
				PipeConnection innerConnection = new PipeConnection(pipe, connectionBufferSize, false, true);
				return new SharedConnectionListener.SharedListenerProxy.NamedPipeValidatingConnection(new PreReadConnection(innerConnection, duplicateContext.ReadData), this);
			}

			// Token: 0x06007BC0 RID: 31680 RVA: 0x001CE1E0 File Offset: 0x001CC3E0
			private ConnectionBufferPool EnsureConnectionBufferPool(int connectionBufferSize, bool alreadyHoldingLock)
			{
				if (alreadyHoldingLock)
				{
					return this.EnsureConnectionBufferPoolCore(connectionBufferSize);
				}
				ConnectionBufferPool result;
				using (LockHelper.TakeWriterLock(this.readerWriterLock))
				{
					result = this.EnsureConnectionBufferPoolCore(connectionBufferSize);
				}
				return result;
			}

			// Token: 0x06007BC1 RID: 31681 RVA: 0x001CE22C File Offset: 0x001CC42C
			private ConnectionBufferPool EnsureConnectionBufferPoolCore(int connectionBufferSize)
			{
				if (this.connectionBufferPool != null && connectionBufferSize == this.connectionBufferPool.BufferSize)
				{
					return this.connectionBufferPool;
				}
				this.connectionBufferPool = new ConnectionBufferPool(connectionBufferSize);
				return this.connectionBufferPool;
			}

			// Token: 0x06007BC2 RID: 31682 RVA: 0x001CE260 File Offset: 0x001CC460
			private IConnection BuildDuplicatedTcpConnection(TcpDuplicateContext duplicateContext, int connectionBufferSize, bool alreadyHoldingLock)
			{
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 655364, SR.GetString("TraceCodePortSharingDuplicatedSocket"));
				}
				if (TD.PortSharingDuplicatedSocketIsEnabled())
				{
					EventTraceActivity eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(OperationContext.Current.IncomingMessage);
					TD.PortSharingDuplicatedSocket(eventTraceActivity, (duplicateContext.Via != null) ? duplicateContext.Via.ToString() : string.Empty);
				}
				Socket socket = new Socket(duplicateContext.SocketInformation);
				SocketConnection innerConnection = new SocketConnection(socket, this.EnsureConnectionBufferPool(connectionBufferSize, alreadyHoldingLock), true);
				return new SharedConnectionListener.SharedListenerProxy.TcpValidatingConnection(new PreReadConnection(innerConnection, duplicateContext.ReadData), this);
			}

			// Token: 0x06007BC3 RID: 31683 RVA: 0x001CE2F5 File Offset: 0x001CC4F5
			private IAsyncResult BeginValidateUriRoute(Uri uri, IPAddress address, int port, AsyncCallback callback, object state)
			{
				return new SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult(this, uri, address, port, callback, state);
			}

			// Token: 0x06007BC4 RID: 31684 RVA: 0x001CE304 File Offset: 0x001CC504
			private bool EndValidateUriRoute(IAsyncResult result)
			{
				CompletedAsyncResult<bool> completedAsyncResult = result as CompletedAsyncResult<bool>;
				if (completedAsyncResult != null)
				{
					CompletedAsyncResult<bool>.End(completedAsyncResult);
				}
				bool flag = !this.closed;
				bool result2;
				try
				{
					result2 = TypedAsyncResult<bool>.End(result);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception) || !flag)
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
					result2 = false;
				}
				return result2;
			}

			// Token: 0x06007BC5 RID: 31685 RVA: 0x001CE360 File Offset: 0x001CC560
			private bool ReadEndpoint(string sharedMemoryName, out string listenerEndpoint)
			{
				bool result;
				try
				{
					if (SharedMemory.Read(sharedMemoryName, out listenerEndpoint))
					{
						result = true;
					}
					else
					{
						if (DiagnosticUtility.ShouldTraceInformation)
						{
							TraceUtility.TraceEvent(TraceEventType.Information, 655374, SR.GetString("TraceCodeSharedManagerServiceEndpointNotExist", new object[]
							{
								this.serviceName
							}), null, null);
						}
						result = false;
					}
				}
				catch (Win32Exception exception)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.WrapEndpointReadingException(exception));
				}
				return result;
			}

			// Token: 0x06007BC6 RID: 31686 RVA: 0x001CE3D0 File Offset: 0x001CC5D0
			private Exception WrapEndpointReadingException(Win32Exception exception)
			{
				string @string;
				if (exception.NativeErrorCode == 2)
				{
					@string = SR.GetString("SharedEndpointReadNotFound", new object[]
					{
						this.baseAddress.BaseAddress.ToString(),
						this.serviceName
					});
				}
				else if (exception.NativeErrorCode == 5)
				{
					@string = SR.GetString("SharedEndpointReadDenied", new object[]
					{
						this.baseAddress.BaseAddress.ToString()
					});
				}
				else
				{
					@string = SR.GetString("SharedManagerBase", new object[]
					{
						this.serviceName,
						SR.GetString("SharedManagerServiceEndpointReadFailure", new object[]
						{
							exception.NativeErrorCode
						})
					});
				}
				return new CommunicationException(@string, exception);
			}

			// Token: 0x06007BC7 RID: 31687 RVA: 0x001CE488 File Offset: 0x001CC688
			private string HandleServiceStart(bool isReconnecting)
			{
				string result = null;
				string text = this.isTcp ? "NetTcpPortSharing/endpoint" : "NetPipeActivator/endpoint";
				this.serviceName = SharedConnectionListener.GetServiceName(this.isTcp);
				if (!isReconnecting && this.ReadEndpoint(text, out result))
				{
					return result;
				}
				ServiceController serviceController = new ServiceController(this.serviceName);
				try
				{
					ServiceControllerStatus serviceControllerStatus = serviceController.Status;
					if (isReconnecting && serviceControllerStatus == ServiceControllerStatus.Running)
					{
						try
						{
							string text2 = SharedMemory.Read(text);
							if (this.listenerEndPoint != text2)
							{
								return text2;
							}
						}
						catch (Win32Exception exception)
						{
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
						}
						serviceControllerStatus = this.ExitServiceStatus(serviceController, 50, 50, ServiceControllerStatus.Running);
					}
					if (serviceControllerStatus != ServiceControllerStatus.Running)
					{
						if (!isReconnecting)
						{
							try
							{
								serviceController.Start();
								goto IL_1D5;
							}
							catch (InvalidOperationException ex)
							{
								Win32Exception ex2 = ex.InnerException as Win32Exception;
								if (ex2 == null)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SharedManagerBase", new object[]
									{
										this.serviceName,
										SR.GetString("SharedManagerServiceStartFailureNoError")
									}), ex));
								}
								if (ex2.NativeErrorCode == 1058)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SharedManagerBase", new object[]
									{
										this.serviceName,
										SR.GetString("SharedManagerServiceStartFailureDisabled", new object[]
										{
											this.serviceName
										})
									}), ex));
								}
								if (ex2.NativeErrorCode != 1056)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SharedManagerBase", new object[]
									{
										this.serviceName,
										SR.GetString("SharedManagerServiceStartFailure", new object[]
										{
											ex2.NativeErrorCode
										})
									}), ex));
								}
								goto IL_1D5;
							}
						}
						if (serviceControllerStatus != ServiceControllerStatus.StartPending)
						{
							if (serviceControllerStatus == ServiceControllerStatus.StopPending)
							{
								serviceControllerStatus = this.ExitServiceStatus(serviceController, 50, 1000, serviceControllerStatus);
							}
							if (serviceControllerStatus == ServiceControllerStatus.Stopped)
							{
								serviceControllerStatus = this.ExitServiceStatus(serviceController, 50, 1000, serviceControllerStatus);
							}
						}
						IL_1D5:
						serviceController.Refresh();
						serviceControllerStatus = serviceController.Status;
						if (serviceControllerStatus == ServiceControllerStatus.StartPending)
						{
							serviceControllerStatus = this.ExitServiceStatus(serviceController, 50, 50, ServiceControllerStatus.StartPending);
						}
					}
					if (serviceControllerStatus != ServiceControllerStatus.Running)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SharedManagerBase", new object[]
						{
							this.serviceName,
							SR.GetString("SharedManagerServiceStartFailureNoError")
						})));
					}
				}
				finally
				{
					serviceController.Close();
				}
				string result2;
				try
				{
					result2 = SharedMemory.Read(text);
				}
				catch (Win32Exception exception2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.WrapEndpointReadingException(exception2));
				}
				return result2;
			}

			// Token: 0x06007BC8 RID: 31688 RVA: 0x001CE754 File Offset: 0x001CC954
			private ServiceControllerStatus ExitServiceStatus(ServiceController service, int pollMin, int pollMax, ServiceControllerStatus status)
			{
				BackoffTimeoutHelper backoffTimeoutHelper = new BackoffTimeoutHelper(TimeSpan.MaxValue, TimeSpan.FromMilliseconds((double)pollMax), TimeSpan.FromMilliseconds((double)pollMin));
				while (!this.closed)
				{
					backoffTimeoutHelper.WaitAndBackoff();
					service.Refresh();
					ServiceControllerStatus status2 = service.Status;
					if (status2 != status)
					{
						return status2;
					}
				}
				return service.Status;
			}

			// Token: 0x06007BC9 RID: 31689 RVA: 0x001CE7A4 File Offset: 0x001CC9A4
			private void SendFault(IConnection connection, string faultCode)
			{
				try
				{
					if (SharedConnectionListener.SharedListenerProxy.drainBuffer == null)
					{
						SharedConnectionListener.SharedListenerProxy.drainBuffer = new byte[1024];
					}
					InitialServerConnectionReader.SendFault(connection, faultCode, SharedConnectionListener.SharedListenerProxy.drainBuffer, ListenerConstants.SharedSendTimeout, 65536);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
				}
			}

			// Token: 0x06007BCA RID: 31690 RVA: 0x001CE804 File Offset: 0x001CCA04
			private bool HandleOnVia(DuplicateContext duplicateContext)
			{
				if (this.onDuplicatedViaCallback == null)
				{
					return true;
				}
				using (LockHelper.TakeWriterLock(this.readerWriterLock))
				{
					if (this.onDuplicatedViaCallback == null)
					{
						return true;
					}
					if (this.onDuplicatedViaCallback != null)
					{
						try
						{
							int num = this.onDuplicatedViaCallback(duplicateContext.Via);
							this.connectionBufferSize = num;
							this.onDuplicatedViaCallback = null;
						}
						catch (Exception ex)
						{
							DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
							string text = null;
							if (ex is ServiceActivationException)
							{
								text = "http://schemas.microsoft.com/ws/2006/05/framing/faults/ServiceActivationFailed";
							}
							else if (ex is EndpointNotFoundException)
							{
								text = "http://schemas.microsoft.com/ws/2006/05/framing/faults/EndpointNotFound";
							}
							IConnection connection = this.BuildConnectionFromData(duplicateContext, 8192, true);
							if (text != null)
							{
								this.SendFault(connection, text);
								return false;
							}
							connection.Abort();
							if (ex is CommunicationObjectAbortedException)
							{
								return false;
							}
							throw;
						}
					}
				}
				return true;
			}

			// Token: 0x06007BCB RID: 31691 RVA: 0x001CE8F0 File Offset: 0x001CCAF0
			private IConnection BuildConnectionFromData(DuplicateContext duplicateContext, int connectionBufferSize, bool alreadyHoldingLock)
			{
				if (this.isTcp)
				{
					return this.BuildDuplicatedTcpConnection((TcpDuplicateContext)duplicateContext, connectionBufferSize, alreadyHoldingLock);
				}
				return this.BuildDuplicatedNamedPipeConnection((NamedPipeDuplicateContext)duplicateContext, connectionBufferSize);
			}

			// Token: 0x06007BCC RID: 31692 RVA: 0x001CE918 File Offset: 0x001CCB18
			IAsyncResult IConnectionDuplicator.BeginDuplicate(DuplicateContext duplicateContext, AsyncCallback callback, object state)
			{
				IAsyncResult result;
				try
				{
					if (!this.HandleOnVia(duplicateContext))
					{
						result = new SharedConnectionListener.DuplicateConnectionAsyncResult(callback, state);
					}
					else
					{
						SharedConnectionListener.DuplicateConnectionAsyncResult duplicateConnectionAsyncResult = new SharedConnectionListener.DuplicateConnectionAsyncResult(this.BuildConnectionFromData(duplicateContext, this.connectionBufferSize, false), callback, state);
						this.parent.OnConnectionAvailable(duplicateConnectionAsyncResult);
						result = duplicateConnectionAsyncResult;
					}
				}
				catch (Exception exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
					if (Fx.IsFatal(exception) || ServiceModelAppSettings.FailOnSocketDuplicationError)
					{
						throw;
					}
					result = new SharedConnectionListener.DuplicateConnectionAsyncResult(callback, state);
				}
				return result;
			}

			// Token: 0x06007BCD RID: 31693 RVA: 0x001CE994 File Offset: 0x001CCB94
			void IConnectionDuplicator.EndDuplicate(IAsyncResult result)
			{
				SharedConnectionListener.DuplicateConnectionAsyncResult.End(result);
			}

			// Token: 0x06007BCE RID: 31694 RVA: 0x001CE99C File Offset: 0x001CCB9C
			void IInputSessionShutdown.ChannelFaulted(IDuplexContextChannel channel)
			{
				this.OnControlChannelShutdown();
			}

			// Token: 0x06007BCF RID: 31695 RVA: 0x001CE9A4 File Offset: 0x001CCBA4
			void IInputSessionShutdown.DoneReceiving(IDuplexContextChannel channel)
			{
				this.OnControlChannelShutdown();
			}

			// Token: 0x06007BD0 RID: 31696 RVA: 0x001CE9AC File Offset: 0x001CCBAC
			private void OnControlChannelShutdown()
			{
				if (this.listenerClosed || !this.opened)
				{
					return;
				}
				using (LockHelper.TakeWriterLock(this.readerWriterLock))
				{
					if (this.listenerClosed || !this.opened)
					{
						return;
					}
					this.listenerClosed = true;
				}
				this.parent.OnListenerFaulted(this.queueId == 0);
			}

			// Token: 0x040046FF RID: 18175
			private const int MaxPendingValidateUriRouteCallsPerProcessor = 10;

			// Token: 0x04004700 RID: 18176
			private static byte[] drainBuffer;

			// Token: 0x04004701 RID: 18177
			private SharedConnectionListener parent;

			// Token: 0x04004702 RID: 18178
			private BaseUriWithWildcard baseAddress;

			// Token: 0x04004703 RID: 18179
			private int queueId;

			// Token: 0x04004704 RID: 18180
			private Guid token;

			// Token: 0x04004705 RID: 18181
			private bool isTcp;

			// Token: 0x04004706 RID: 18182
			private string serviceName;

			// Token: 0x04004707 RID: 18183
			private string listenerEndPoint;

			// Token: 0x04004708 RID: 18184
			private SecurityIdentifier listenerUniqueSid;

			// Token: 0x04004709 RID: 18185
			private SecurityIdentifier listenerUserSid;

			// Token: 0x0400470A RID: 18186
			private ChannelFactory channelFactory;

			// Token: 0x0400470B RID: 18187
			private IDuplexContextChannel controlSessionWithListener;

			// Token: 0x0400470C RID: 18188
			private IDisposable allowContext;

			// Token: 0x0400470D RID: 18189
			private string securityEventName;

			// Token: 0x0400470E RID: 18190
			private ReaderWriterLockSlim readerWriterLock;

			// Token: 0x0400470F RID: 18191
			private int connectionBufferSize;

			// Token: 0x04004710 RID: 18192
			private Func<Uri, int> onDuplicatedViaCallback;

			// Token: 0x04004711 RID: 18193
			private bool listenerClosed;

			// Token: 0x04004712 RID: 18194
			private bool closed;

			// Token: 0x04004713 RID: 18195
			private bool opened;

			// Token: 0x04004714 RID: 18196
			private ConnectionBufferPool connectionBufferPool;

			// Token: 0x04004715 RID: 18197
			private ThreadNeutralSemaphore validateUriCallThrottle;

			// Token: 0x02000F4B RID: 3915
			private class ValidateUriRouteAsyncResult : TypedAsyncResult<bool>
			{
				// Token: 0x060086EF RID: 34543 RVA: 0x001F4560 File Offset: 0x001F2760
				public ValidateUriRouteAsyncResult(SharedConnectionListener.SharedListenerProxy proxy, Uri uri, IPAddress address, int port, AsyncCallback callback, object state) : base(callback, state)
				{
					this.proxy = proxy;
					this.uri = uri;
					this.address = address;
					this.port = port;
					bool isValidUriRoute = false;
					bool flag = false;
					Exception completionException = null;
					try
					{
						flag = this.BeginEnterThrottle(out isValidUriRoute);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						isValidUriRoute = false;
						if (!SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult.ShouldHandleException(ex))
						{
							completionException = ex;
						}
						flag = true;
					}
					if (flag)
					{
						this.Complete(true, isValidUriRoute, completionException);
					}
				}

				// Token: 0x060086F0 RID: 34544 RVA: 0x001F45DC File Offset: 0x001F27DC
				private void Cleanup()
				{
					if (this.enteredThrottle)
					{
						this.enteredThrottle = false;
						this.proxy.validateUriCallThrottle.Exit();
					}
				}

				// Token: 0x060086F1 RID: 34545 RVA: 0x001F4600 File Offset: 0x001F2800
				private bool BeginEnterThrottle(out bool isValidUriRoute)
				{
					isValidUriRoute = false;
					if (this.proxy.closed)
					{
						return true;
					}
					if (SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult.onEnterThrottle == null)
					{
						SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult.onEnterThrottle = new FastAsyncCallback(SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult.OnEnterThrottle);
					}
					if (this.proxy.validateUriCallThrottle.EnterAsync(TimeSpan.MaxValue, SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult.onEnterThrottle, this))
					{
						this.enteredThrottle = true;
						return this.BeginValidateUriRoute(out isValidUriRoute);
					}
					return false;
				}

				// Token: 0x060086F2 RID: 34546 RVA: 0x001F4664 File Offset: 0x001F2864
				private bool BeginValidateUriRoute(out bool isValidUriRoute)
				{
					isValidUriRoute = false;
					if (SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult.onValidateUriRoute == null)
					{
						SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult.onValidateUriRoute = Fx.ThunkCallback(new AsyncCallback(SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult.OnValidateUriRoute));
					}
					using (LockHelper.TakeReaderLock(this.proxy.readerWriterLock))
					{
						if (this.proxy.closed)
						{
							return true;
						}
						IAsyncResult asyncResult = ((IConnectionRegisterAsync)this.proxy.controlSessionWithListener).BeginValidateUriRoute(this.uri, this.address, this.port, SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult.onValidateUriRoute, this);
						if (asyncResult.CompletedSynchronously)
						{
							return this.HandleValidateUriRoute(asyncResult, out isValidUriRoute);
						}
					}
					return false;
				}

				// Token: 0x060086F3 RID: 34547 RVA: 0x001F4714 File Offset: 0x001F2914
				private static void OnEnterThrottle(object state, Exception completionException)
				{
					SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult validateUriRouteAsyncResult = (SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult)state;
					validateUriRouteAsyncResult.enteredThrottle = true;
					bool flag = completionException != null;
					bool isValidUriRoute = false;
					if (!flag)
					{
						try
						{
							flag = validateUriRouteAsyncResult.BeginValidateUriRoute(out isValidUriRoute);
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							flag = true;
							completionException = ex;
						}
					}
					if (flag)
					{
						validateUriRouteAsyncResult.Complete(false, isValidUriRoute, completionException);
					}
				}

				// Token: 0x060086F4 RID: 34548 RVA: 0x001F4774 File Offset: 0x001F2974
				private static void OnValidateUriRoute(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					Exception completionException = null;
					SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult validateUriRouteAsyncResult = (SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult)result.AsyncState;
					bool isValidUriRoute;
					bool flag;
					try
					{
						flag = validateUriRouteAsyncResult.HandleValidateUriRoute(result, out isValidUriRoute);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						flag = true;
						isValidUriRoute = false;
						if (!SharedConnectionListener.SharedListenerProxy.ValidateUriRouteAsyncResult.ShouldHandleException(ex))
						{
							completionException = ex;
						}
					}
					if (flag)
					{
						validateUriRouteAsyncResult.Complete(false, isValidUriRoute, completionException);
					}
				}

				// Token: 0x060086F5 RID: 34549 RVA: 0x001F47E0 File Offset: 0x001F29E0
				private bool HandleValidateUriRoute(IAsyncResult result, out bool isValidUriRoute)
				{
					isValidUriRoute = ((IConnectionRegisterAsync)this.proxy.controlSessionWithListener).EndValidateUriRoute(result);
					return true;
				}

				// Token: 0x060086F6 RID: 34550 RVA: 0x001F47FC File Offset: 0x001F29FC
				private static bool ShouldHandleException(Exception exception)
				{
					bool result = false;
					if (exception is CommunicationException || exception is System.ServiceProcess.TimeoutException)
					{
						result = true;
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
					}
					return result;
				}

				// Token: 0x060086F7 RID: 34551 RVA: 0x001F4825 File Offset: 0x001F2A25
				private void Complete(bool completedSynchronously, bool isValidUriRoute, Exception completionException)
				{
					this.Cleanup();
					if (completionException != null)
					{
						base.Complete(completedSynchronously, completionException);
						return;
					}
					base.Complete(isValidUriRoute, completedSynchronously);
				}

				// Token: 0x04004E79 RID: 20089
				private static AsyncCallback onValidateUriRoute;

				// Token: 0x04004E7A RID: 20090
				private static FastAsyncCallback onEnterThrottle;

				// Token: 0x04004E7B RID: 20091
				private SharedConnectionListener.SharedListenerProxy proxy;

				// Token: 0x04004E7C RID: 20092
				private Uri uri;

				// Token: 0x04004E7D RID: 20093
				private IPAddress address;

				// Token: 0x04004E7E RID: 20094
				private int port;

				// Token: 0x04004E7F RID: 20095
				private bool enteredThrottle;
			}

			// Token: 0x02000F4C RID: 3916
			private class NamedPipeValidatingConnection : DelegatingConnection
			{
				// Token: 0x060086F8 RID: 34552 RVA: 0x001F4841 File Offset: 0x001F2A41
				public NamedPipeValidatingConnection(IConnection connection, SharedConnectionListener.SharedListenerProxy listenerProxy) : base(connection)
				{
					this.listenerProxy = listenerProxy;
					this.initialValidation = true;
				}

				// Token: 0x060086F9 RID: 34553 RVA: 0x001F4858 File Offset: 0x001F2A58
				public override IAsyncResult BeginValidate(Uri uri, AsyncCallback callback, object state)
				{
					if (this.initialValidation)
					{
						this.initialValidation = false;
						return new CompletedAsyncResult<bool>(true, callback, state);
					}
					return this.listenerProxy.BeginValidateUriRoute(uri, null, -1, callback, state);
				}

				// Token: 0x060086FA RID: 34554 RVA: 0x001F4882 File Offset: 0x001F2A82
				public override bool EndValidate(IAsyncResult result)
				{
					if (result is CompletedAsyncResult<bool>)
					{
						return CompletedAsyncResult<bool>.End(result);
					}
					return this.listenerProxy.EndValidateUriRoute(result);
				}

				// Token: 0x04004E80 RID: 20096
				private SharedConnectionListener.SharedListenerProxy listenerProxy;

				// Token: 0x04004E81 RID: 20097
				private bool initialValidation;
			}

			// Token: 0x02000F4D RID: 3917
			private class TcpValidatingConnection : DelegatingConnection
			{
				// Token: 0x060086FB RID: 34555 RVA: 0x001F48A0 File Offset: 0x001F2AA0
				public TcpValidatingConnection(IConnection connection, SharedConnectionListener.SharedListenerProxy listenerProxy) : base(connection)
				{
					this.listenerProxy = listenerProxy;
					Socket socket = (Socket)connection.GetCoreTransport();
					this.ipAddress = ((IPEndPoint)socket.LocalEndPoint).Address;
					this.port = ((IPEndPoint)socket.LocalEndPoint).Port;
					this.initialValidation = true;
				}

				// Token: 0x060086FC RID: 34556 RVA: 0x001F48FA File Offset: 0x001F2AFA
				public override IAsyncResult BeginValidate(Uri uri, AsyncCallback callback, object state)
				{
					if (this.initialValidation)
					{
						this.initialValidation = false;
						return new CompletedAsyncResult<bool>(true, callback, state);
					}
					return this.listenerProxy.BeginValidateUriRoute(uri, this.ipAddress, this.port, callback, state);
				}

				// Token: 0x060086FD RID: 34557 RVA: 0x001F492E File Offset: 0x001F2B2E
				public override bool EndValidate(IAsyncResult result)
				{
					if (result is CompletedAsyncResult<bool>)
					{
						return CompletedAsyncResult<bool>.End(result);
					}
					return this.listenerProxy.EndValidateUriRoute(result);
				}

				// Token: 0x04004E82 RID: 20098
				private IPAddress ipAddress;

				// Token: 0x04004E83 RID: 20099
				private int port;

				// Token: 0x04004E84 RID: 20100
				private SharedConnectionListener.SharedListenerProxy listenerProxy;

				// Token: 0x04004E85 RID: 20101
				private bool initialValidation;
			}

			// Token: 0x02000F4E RID: 3918
			private class SharedListenerProxyBehavior : IEndpointBehavior
			{
				// Token: 0x060086FE RID: 34558 RVA: 0x001F494B File Offset: 0x001F2B4B
				public SharedListenerProxyBehavior(SharedConnectionListener.SharedListenerProxy proxy)
				{
					this.proxy = proxy;
				}

				// Token: 0x060086FF RID: 34559 RVA: 0x001F495A File Offset: 0x001F2B5A
				public void Validate(ServiceEndpoint serviceEndpoint)
				{
				}

				// Token: 0x06008700 RID: 34560 RVA: 0x001F495C File Offset: 0x001F2B5C
				public void AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
				{
				}

				// Token: 0x06008701 RID: 34561 RVA: 0x001F495E File Offset: 0x001F2B5E
				public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
				{
				}

				// Token: 0x06008702 RID: 34562 RVA: 0x001F4960 File Offset: 0x001F2B60
				public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
				{
					behavior.DispatchRuntime.InputSessionShutdownHandlers.Add(this.proxy);
				}

				// Token: 0x04004E86 RID: 20102
				private SharedConnectionListener.SharedListenerProxy proxy;
			}
		}

		// Token: 0x02000D25 RID: 3365
		private class DuplicateConnectionAsyncResult : AsyncResult
		{
			// Token: 0x06007BD1 RID: 31697 RVA: 0x001CEA20 File Offset: 0x001CCC20
			public DuplicateConnectionAsyncResult(IConnection connection, AsyncCallback callback, object state) : base(callback, state)
			{
				this.connection = connection;
			}

			// Token: 0x06007BD2 RID: 31698 RVA: 0x001CEA31 File Offset: 0x001CCC31
			public DuplicateConnectionAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
				base.Complete(true);
			}

			// Token: 0x17001BD2 RID: 7122
			// (get) Token: 0x06007BD3 RID: 31699 RVA: 0x001CEA42 File Offset: 0x001CCC42
			public IConnection Connection
			{
				get
				{
					return this.connection;
				}
			}

			// Token: 0x06007BD4 RID: 31700 RVA: 0x001CEA4A File Offset: 0x001CCC4A
			public void CompleteOperation()
			{
				base.Complete(false);
			}

			// Token: 0x06007BD5 RID: 31701 RVA: 0x001CEA53 File Offset: 0x001CCC53
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<SharedConnectionListener.DuplicateConnectionAsyncResult>(result);
			}

			// Token: 0x04004716 RID: 18198
			private IConnection connection;
		}
	}
}
