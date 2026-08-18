using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007DE RID: 2014
	internal abstract class ConnectionOrientedTransportManager<TChannelListener> : TransportManager where TChannelListener : ConnectionOrientedTransportChannelListener
	{
		// Token: 0x06004C27 RID: 19495 RVA: 0x00116064 File Offset: 0x00114264
		protected ConnectionOrientedTransportManager()
		{
			this.addressTable = new UriPrefixTable<TChannelListener>();
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06004C28 RID: 19496 RVA: 0x00116077 File Offset: 0x00114277
		private UriPrefixTable<TChannelListener> AddressTable
		{
			get
			{
				return this.addressTable;
			}
		}

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06004C29 RID: 19497 RVA: 0x0011607F File Offset: 0x0011427F
		protected TimeSpan ChannelInitializationTimeout
		{
			get
			{
				return this.channelInitializationTimeout;
			}
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x00116088 File Offset: 0x00114288
		internal void ApplyListenerSettings(IConnectionOrientedListenerSettings listenerSettings)
		{
			this.connectionBufferSize = listenerSettings.ConnectionBufferSize;
			this.channelInitializationTimeout = listenerSettings.ChannelInitializationTimeout;
			this.maxPendingConnections = listenerSettings.MaxPendingConnections;
			this.maxOutputDelay = listenerSettings.MaxOutputDelay;
			this.maxPendingAccepts = listenerSettings.MaxPendingAccepts;
			this.idleTimeout = listenerSettings.IdleTimeout;
			this.maxPooledConnections = listenerSettings.MaxPooledConnections;
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06004C2B RID: 19499 RVA: 0x001160E9 File Offset: 0x001142E9
		internal int ConnectionBufferSize
		{
			get
			{
				return this.connectionBufferSize;
			}
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06004C2C RID: 19500 RVA: 0x001160F1 File Offset: 0x001142F1
		internal int MaxPendingConnections
		{
			get
			{
				return this.maxPendingConnections;
			}
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06004C2D RID: 19501 RVA: 0x001160F9 File Offset: 0x001142F9
		internal TimeSpan MaxOutputDelay
		{
			get
			{
				return this.maxOutputDelay;
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06004C2E RID: 19502 RVA: 0x00116101 File Offset: 0x00114301
		internal int MaxPendingAccepts
		{
			get
			{
				return this.maxPendingAccepts;
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06004C2F RID: 19503 RVA: 0x00116109 File Offset: 0x00114309
		internal TimeSpan IdleTimeout
		{
			get
			{
				return this.idleTimeout;
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06004C30 RID: 19504 RVA: 0x00116111 File Offset: 0x00114311
		internal int MaxPooledConnections
		{
			get
			{
				return this.maxPooledConnections;
			}
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x0011611C File Offset: 0x0011431C
		internal bool IsCompatible(ConnectionOrientedTransportChannelListener channelListener)
		{
			return channelListener.InheritBaseAddressSettings || (this.ChannelInitializationTimeout == channelListener.ChannelInitializationTimeout && this.ConnectionBufferSize == channelListener.ConnectionBufferSize && this.MaxPendingConnections == channelListener.MaxPendingConnections && this.MaxOutputDelay == channelListener.MaxOutputDelay && this.MaxPendingAccepts == channelListener.MaxPendingAccepts && this.idleTimeout == channelListener.IdleTimeout && this.maxPooledConnections == channelListener.MaxPooledConnections);
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x001161A8 File Offset: 0x001143A8
		private TChannelListener GetChannelListener(Uri via)
		{
			TChannelListener result = default(TChannelListener);
			if (this.AddressTable.TryLookupUri(via, HostNameComparisonMode.StrongWildcard, out result))
			{
				return result;
			}
			if (this.AddressTable.TryLookupUri(via, HostNameComparisonMode.Exact, out result))
			{
				return result;
			}
			this.AddressTable.TryLookupUri(via, HostNameComparisonMode.WeakWildcard, out result);
			return result;
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x001161F4 File Offset: 0x001143F4
		internal void OnDemuxerError(Exception exception)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.Fault<TChannelListener>(this.AddressTable, exception);
			}
		}

		// Token: 0x06004C34 RID: 19508 RVA: 0x0011623C File Offset: 0x0011443C
		internal ISingletonChannelListener OnGetSingletonMessageHandler(ServerSingletonPreambleConnectionReader serverSingletonPreambleReader)
		{
			Uri via = serverSingletonPreambleReader.Via;
			TChannelListener channelListener = this.GetChannelListener(via);
			if (channelListener == null)
			{
				serverSingletonPreambleReader.SendFault("http://schemas.microsoft.com/ws/2006/05/framing/faults/EndpointNotFound");
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("EndpointNotFound", new object[]
				{
					via
				})));
			}
			if (channelListener is IChannelListener<IReplyChannel>)
			{
				channelListener.RaiseMessageReceived();
				return (ISingletonChannelListener)((object)channelListener);
			}
			serverSingletonPreambleReader.SendFault("http://schemas.microsoft.com/ws/2006/05/framing/faults/UnsupportedMode");
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("FramingModeNotSupported", new object[]
			{
				FramingMode.Singleton
			})));
		}

		// Token: 0x06004C35 RID: 19509 RVA: 0x001162E8 File Offset: 0x001144E8
		internal void OnHandleServerSessionPreamble(ServerSessionPreambleConnectionReader serverSessionPreambleReader, ConnectionDemuxer connectionDemuxer)
		{
			Uri via = serverSessionPreambleReader.Via;
			TChannelListener channelListener = this.GetChannelListener(via);
			if (channelListener == null)
			{
				serverSessionPreambleReader.SendFault("http://schemas.microsoft.com/ws/2006/05/framing/faults/EndpointNotFound");
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("DuplexSessionListenerNotFound", new object[]
				{
					via.ToString()
				})));
			}
			ISessionPreambleHandler sessionPreambleHandler = channelListener as ISessionPreambleHandler;
			if (sessionPreambleHandler != null && channelListener is IChannelListener<IDuplexSessionChannel>)
			{
				sessionPreambleHandler.HandleServerSessionPreamble(serverSessionPreambleReader, connectionDemuxer);
				return;
			}
			serverSessionPreambleReader.SendFault("http://schemas.microsoft.com/ws/2006/05/framing/faults/UnsupportedMode");
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("FramingModeNotSupported", new object[]
			{
				FramingMode.Duplex
			})));
		}

		// Token: 0x06004C36 RID: 19510 RVA: 0x00116398 File Offset: 0x00114598
		internal IConnectionOrientedTransportFactorySettings OnGetTransportFactorySettings(Uri via)
		{
			return this.GetChannelListener(via);
		}

		// Token: 0x06004C37 RID: 19511 RVA: 0x001163A6 File Offset: 0x001145A6
		internal override void Register(TransportChannelListener channelListener)
		{
			this.AddressTable.RegisterUri(channelListener.Uri, channelListener.HostNameComparisonModeInternal, (TChannelListener)((object)channelListener));
			channelListener.SetMessageReceivedCallback(new Action(this.OnMessageReceived));
		}

		// Token: 0x06004C38 RID: 19512 RVA: 0x001163D7 File Offset: 0x001145D7
		internal override void Unregister(TransportChannelListener channelListener)
		{
			TransportManager.EnsureRegistered<TChannelListener>(this.AddressTable, (TChannelListener)((object)channelListener), channelListener.HostNameComparisonModeInternal);
			this.AddressTable.UnregisterUri(channelListener.Uri, channelListener.HostNameComparisonModeInternal);
			channelListener.SetMessageReceivedCallback(null);
		}

		// Token: 0x06004C39 RID: 19513 RVA: 0x0011640E File Offset: 0x0011460E
		internal void SetMessageReceivedCallback(Action messageReceivedCallback)
		{
			this.messageReceivedCallback = messageReceivedCallback;
		}

		// Token: 0x06004C3A RID: 19514 RVA: 0x00116418 File Offset: 0x00114618
		private void OnMessageReceived()
		{
			Action action = this.messageReceivedCallback;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x04002F95 RID: 12181
		private UriPrefixTable<TChannelListener> addressTable;

		// Token: 0x04002F96 RID: 12182
		private int connectionBufferSize;

		// Token: 0x04002F97 RID: 12183
		private TimeSpan channelInitializationTimeout;

		// Token: 0x04002F98 RID: 12184
		private int maxPendingConnections;

		// Token: 0x04002F99 RID: 12185
		private TimeSpan maxOutputDelay;

		// Token: 0x04002F9A RID: 12186
		private int maxPendingAccepts;

		// Token: 0x04002F9B RID: 12187
		private TimeSpan idleTimeout;

		// Token: 0x04002F9C RID: 12188
		private int maxPooledConnections;

		// Token: 0x04002F9D RID: 12189
		private Action messageReceivedCallback;
	}
}
