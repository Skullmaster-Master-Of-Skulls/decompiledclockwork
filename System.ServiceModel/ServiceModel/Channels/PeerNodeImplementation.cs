using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime;
using System.Runtime.Serialization;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A1F RID: 2591
	internal class PeerNodeImplementation : IPeerNodeMessageHandling
	{
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x060066B0 RID: 26288 RVA: 0x0017EBA4 File Offset: 0x0017CDA4
		// (remove) Token: 0x060066B1 RID: 26289 RVA: 0x0017EBDC File Offset: 0x0017CDDC
		public event EventHandler Offline;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x060066B2 RID: 26290 RVA: 0x0017EC14 File Offset: 0x0017CE14
		// (remove) Token: 0x060066B3 RID: 26291 RVA: 0x0017EC4C File Offset: 0x0017CE4C
		public event EventHandler Online;

		// Token: 0x060066B4 RID: 26292 RVA: 0x0017EC84 File Offset: 0x0017CE84
		public PeerNodeImplementation()
		{
			this.connectTimeout = 60000;
			this.maxReceivedMessageSize = 65536L;
			this.minNeighbors = 2;
			this.idealNeighbors = 3;
			this.maxNeighbors = 7;
			this.maxReferrals = 10;
			this.port = 0;
			this.connectCompletedEvent = new ManualResetEvent(false);
			this.encoder = new BinaryMessageEncodingBindingElement().CreateMessageEncoderFactory().Encoder;
			this.messageFilters = new Dictionary<object, PeerNodeImplementation.MessageFilterRegistration>();
			this.stateManager = new PeerNodeImplementation.SimpleStateManager(this);
			this.uri2SecurityProtocol = new Dictionary<Uri, PeerNodeImplementation.RefCountedSecurityProtocol>();
			this.readerQuotas = new XmlDictionaryReaderQuotas();
			this.maxBufferPoolSize = 524288L;
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x060066B5 RID: 26293 RVA: 0x0017ED58 File Offset: 0x0017CF58
		// (remove) Token: 0x060066B6 RID: 26294 RVA: 0x0017ED90 File Offset: 0x0017CF90
		public event EventHandler<PeerNeighborCloseEventArgs> NeighborClosed;

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x060066B7 RID: 26295 RVA: 0x0017EDC8 File Offset: 0x0017CFC8
		// (remove) Token: 0x060066B8 RID: 26296 RVA: 0x0017EE00 File Offset: 0x0017D000
		public event EventHandler<PeerNeighborCloseEventArgs> NeighborClosing;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x060066B9 RID: 26297 RVA: 0x0017EE38 File Offset: 0x0017D038
		// (remove) Token: 0x060066BA RID: 26298 RVA: 0x0017EE70 File Offset: 0x0017D070
		public event EventHandler NeighborConnected;

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060066BB RID: 26299 RVA: 0x0017EEA8 File Offset: 0x0017D0A8
		// (remove) Token: 0x060066BC RID: 26300 RVA: 0x0017EEE0 File Offset: 0x0017D0E0
		public event EventHandler NeighborOpened;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060066BD RID: 26301 RVA: 0x0017EF18 File Offset: 0x0017D118
		// (remove) Token: 0x060066BE RID: 26302 RVA: 0x0017EF50 File Offset: 0x0017D150
		public event EventHandler Aborted;

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x060066BF RID: 26303 RVA: 0x0017EF85 File Offset: 0x0017D185
		// (set) Token: 0x060066C0 RID: 26304 RVA: 0x0017EF8D File Offset: 0x0017D18D
		public PeerNodeConfig Config
		{
			get
			{
				return this.config;
			}
			private set
			{
				this.config = value;
			}
		}

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x060066C1 RID: 26305 RVA: 0x0017EF98 File Offset: 0x0017D198
		public bool IsOnline
		{
			get
			{
				object obj = this.ThisLock;
				bool result;
				lock (obj)
				{
					if (this.isOpen)
					{
						result = this.neighborManager.IsOnline;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}
		}

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x060066C2 RID: 26306 RVA: 0x0017EFF0 File Offset: 0x0017D1F0
		internal bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x060066C3 RID: 26307 RVA: 0x0017EFFA File Offset: 0x0017D1FA
		// (set) Token: 0x060066C4 RID: 26308 RVA: 0x0017F004 File Offset: 0x0017D204
		public IPAddress ListenIPAddress
		{
			get
			{
				return this.listenIPAddress;
			}
			set
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					this.ThrowIfOpen();
					this.listenIPAddress = value;
				}
			}
		}

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x060066C5 RID: 26309 RVA: 0x0017F04C File Offset: 0x0017D24C
		// (set) Token: 0x060066C6 RID: 26310 RVA: 0x0017F054 File Offset: 0x0017D254
		public Uri ListenUri
		{
			get
			{
				return this.listenUri;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value.Scheme != "net.p2p")
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("InvalidUriScheme", new object[]
					{
						value.Scheme,
						"net.p2p"
					}));
				}
				object obj = this.ThisLock;
				lock (obj)
				{
					this.ThrowIfOpen();
					this.listenUri = value;
				}
			}
		}

		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x060066C7 RID: 26311 RVA: 0x0017F0F8 File Offset: 0x0017D2F8
		// (set) Token: 0x060066C8 RID: 26312 RVA: 0x0017F100 File Offset: 0x0017D300
		public long MaxBufferPoolSize
		{
			get
			{
				return this.maxBufferPoolSize;
			}
			set
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					this.ThrowIfOpen();
					this.maxBufferPoolSize = value;
				}
			}
		}

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x060066C9 RID: 26313 RVA: 0x0017F148 File Offset: 0x0017D348
		// (set) Token: 0x060066CA RID: 26314 RVA: 0x0017F150 File Offset: 0x0017D350
		public long MaxReceivedMessageSize
		{
			get
			{
				return this.maxReceivedMessageSize;
			}
			set
			{
				if (value < 16384L)
				{
					throw Fx.AssertAndThrow("invalid MaxReceivedMessageSize");
				}
				object obj = this.ThisLock;
				lock (obj)
				{
					this.ThrowIfOpen();
					this.maxReceivedMessageSize = value;
				}
			}
		}

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x060066CB RID: 26315 RVA: 0x0017F1AC File Offset: 0x0017D3AC
		public string MeshId
		{
			get
			{
				object obj = this.ThisLock;
				string result;
				lock (obj)
				{
					this.ThrowIfNotOpen();
					result = this.meshId;
				}
				return result;
			}
		}

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x060066CC RID: 26316 RVA: 0x0017F1F4 File Offset: 0x0017D3F4
		// (set) Token: 0x060066CD RID: 26317 RVA: 0x0017F1FC File Offset: 0x0017D3FC
		public PeerMessagePropagationFilter MessagePropagationFilter
		{
			get
			{
				return this.messagePropagationFilter;
			}
			set
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					this.messagePropagationFilter = value;
					this.messagePropagationFilterContext = ThreadBehavior.GetCurrentSynchronizationContext();
				}
			}
		}

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x060066CE RID: 26318 RVA: 0x0017F248 File Offset: 0x0017D448
		public PeerNeighborManager NeighborManager
		{
			get
			{
				return this.neighborManager;
			}
		}

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x060066CF RID: 26319 RVA: 0x0017F250 File Offset: 0x0017D450
		public ulong NodeId
		{
			get
			{
				this.ThrowIfNotOpen();
				return this.config.NodeId;
			}
		}

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x060066D0 RID: 26320 RVA: 0x0017F263 File Offset: 0x0017D463
		// (set) Token: 0x060066D1 RID: 26321 RVA: 0x0017F26C File Offset: 0x0017D46C
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					this.ThrowIfOpen();
					this.port = value;
				}
			}
		}

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x060066D2 RID: 26322 RVA: 0x0017F2B4 File Offset: 0x0017D4B4
		public int ListenerPort
		{
			get
			{
				this.ThrowIfNotOpen();
				return this.config.ListenerPort;
			}
		}

		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x060066D3 RID: 26323 RVA: 0x0017F2C7 File Offset: 0x0017D4C7
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.readerQuotas;
			}
		}

		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x060066D4 RID: 26324 RVA: 0x0017F2CF File Offset: 0x0017D4CF
		// (set) Token: 0x060066D5 RID: 26325 RVA: 0x0017F2D8 File Offset: 0x0017D4D8
		public PeerResolver Resolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					this.ThrowIfOpen();
					this.resolver = value;
				}
			}
		}

		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x060066D6 RID: 26326 RVA: 0x0017F320 File Offset: 0x0017D520
		// (set) Token: 0x060066D7 RID: 26327 RVA: 0x0017F328 File Offset: 0x0017D528
		public PeerSecurityManager SecurityManager
		{
			get
			{
				return this.securityManager;
			}
			set
			{
				this.securityManager = value;
			}
		}

		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x060066D8 RID: 26328 RVA: 0x0017F331 File Offset: 0x0017D531
		// (set) Token: 0x060066D9 RID: 26329 RVA: 0x0017F33C File Offset: 0x0017D53C
		internal PeerService Service
		{
			get
			{
				return this.service;
			}
			set
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					this.ThrowIfNotOpen();
					this.service = value;
				}
			}
		}

		// Token: 0x170018BA RID: 6330
		// (get) Token: 0x060066DA RID: 26330 RVA: 0x0017F384 File Offset: 0x0017D584
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x060066DB RID: 26331 RVA: 0x0017F38C File Offset: 0x0017D58C
		public void Abort()
		{
			this.stateManager.Abort();
		}

		// Token: 0x060066DC RID: 26332 RVA: 0x0017F399 File Offset: 0x0017D599
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.stateManager.BeginClose(timeout, callback, state);
		}

		// Token: 0x060066DD RID: 26333 RVA: 0x0017F3A9 File Offset: 0x0017D5A9
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state, bool waitForOnline)
		{
			return this.stateManager.BeginOpen(timeout, callback, state, waitForOnline);
		}

		// Token: 0x060066DE RID: 26334 RVA: 0x0017F3BC File Offset: 0x0017D5BC
		public Guid ProcessOutgoingMessage(Message message, Uri via)
		{
			Guid guid = Guid.NewGuid();
			UniqueId messageId = new UniqueId(guid);
			if (-1 != message.Headers.FindHeader("MessageID", "http://schemas.microsoft.com/net/2006/05/peer"))
			{
				PeerExceptionHelper.ThrowInvalidOperation_ConflictingHeader("MessageID");
			}
			if (-1 != message.Headers.FindHeader("PeerTo", "http://schemas.microsoft.com/net/2006/05/peer"))
			{
				PeerExceptionHelper.ThrowInvalidOperation_ConflictingHeader("PeerTo");
			}
			if (-1 != message.Headers.FindHeader("PeerVia", "http://schemas.microsoft.com/net/2006/05/peer"))
			{
				PeerExceptionHelper.ThrowInvalidOperation_ConflictingHeader("PeerVia");
			}
			if (-1 != message.Headers.FindHeader("FloodMessage", "http://schemas.microsoft.com/net/2006/05/peer", new string[]
			{
				"PeerFlooder"
			}))
			{
				PeerExceptionHelper.ThrowInvalidOperation_ConflictingHeader("FloodMessage");
			}
			message.Headers.Add(PeerDictionaryHeader.CreateMessageIdHeader(messageId));
			message.Properties.Via = via;
			message.Headers.Add(MessageHeader.CreateHeader("PeerTo", "http://schemas.microsoft.com/net/2006/05/peer", message.Headers.To));
			message.Headers.Add(PeerDictionaryHeader.CreateViaHeader(via));
			message.Headers.Add(PeerDictionaryHeader.CreateFloodRole());
			return guid;
		}

		// Token: 0x060066DF RID: 26335 RVA: 0x0017F4D0 File Offset: 0x0017D6D0
		public void SecureOutgoingMessage(ref Message message, Uri via, TimeSpan timeout, SecurityProtocol securityProtocol)
		{
			if (securityProtocol != null)
			{
				securityProtocol.SecureOutgoingMessage(ref message, timeout);
			}
		}

		// Token: 0x060066E0 RID: 26336 RVA: 0x0017F4E0 File Offset: 0x0017D6E0
		public IAsyncResult BeginSend(object registrant, Message message, Uri via, ITransportFactorySettings settings, TimeSpan timeout, AsyncCallback callback, object state, SecurityProtocol securityProtocol)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			MessageBuffer messageBuffer = null;
			Message message2 = null;
			ulong maxValue = ulong.MaxValue;
			PeerMessagePropagation peerMessagePropagation = PeerMessagePropagation.LocalAndRemote;
			int messageSize = -1;
			PeerNodeImplementation.SendAsyncResult sendAsyncResult = new PeerNodeImplementation.SendAsyncResult(callback, state);
			AsyncCallback callback2 = Fx.ThunkCallback(new AsyncCallback(sendAsyncResult.OnFloodComplete));
			try
			{
				object obj = this.ThisLock;
				PeerFlooder peerFlooder;
				lock (obj)
				{
					this.ThrowIfNotOpen();
					peerFlooder = this.flooder;
				}
				int maxBufferSize = (int)Math.Min(this.maxReceivedMessageSize, settings.MaxReceivedMessageSize);
				Guid guid = this.ProcessOutgoingMessage(message, via);
				this.SecureOutgoingMessage(ref message, via, timeout, securityProtocol);
				byte[] id;
				if (message is SecurityAppliedMessage)
				{
					ArraySegment<byte> buffer = this.encoder.WriteMessage(message, int.MaxValue, this.bufferManager);
					message2 = this.encoder.ReadMessage(buffer, this.bufferManager);
					id = (message as SecurityAppliedMessage).PrimarySignatureValue;
					messageSize = buffer.Count;
				}
				else
				{
					message2 = message;
					id = guid.ToByteArray();
				}
				messageBuffer = message2.CreateBufferedCopy(maxBufferSize);
				string contentType = settings.MessageEncoderFactory.Encoder.ContentType;
				if (this.messagePropagationFilter != null)
				{
					using (Message message3 = messageBuffer.CreateMessage())
					{
						peerMessagePropagation = ((IPeerNodeMessageHandling)this).DetermineMessagePropagation(message3, PeerMessageOrigination.Local);
					}
				}
				if ((peerMessagePropagation & PeerMessagePropagation.Remote) != PeerMessagePropagation.None && maxValue == 0UL)
				{
					peerMessagePropagation &= (PeerMessagePropagation)(-3);
				}
				if ((peerMessagePropagation & PeerMessagePropagation.Remote) != PeerMessagePropagation.None)
				{
					IAsyncResult asyncResult = peerFlooder.BeginFloodEncodedMessage(id, messageBuffer, timeoutHelper.RemainingTime(), callback2, null);
					if (DiagnosticUtility.ShouldTraceVerbose)
					{
						TraceUtility.TraceEvent(TraceEventType.Verbose, 262206, SR.GetString("TraceCodePeerChannelMessageSent"), this, message);
					}
				}
				else
				{
					IAsyncResult asyncResult = new CompletedAsyncResult(callback2, null);
				}
				if ((peerMessagePropagation & PeerMessagePropagation.Local) != PeerMessagePropagation.None)
				{
					using (Message message4 = messageBuffer.CreateMessage())
					{
						int num = message4.Headers.FindHeader("Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
						if (num >= 0)
						{
							message4.Headers.AddUnderstood(num);
						}
						using (MessageBuffer messageBuffer2 = message4.CreateBufferedCopy(maxBufferSize))
						{
							this.DeliverMessageToClientChannels(registrant, messageBuffer2, via, message.Headers.To, contentType, messageSize, -1, null);
						}
					}
				}
				sendAsyncResult.OnLocalDispatchComplete(sendAsyncResult);
			}
			finally
			{
				message.Close();
				if (message2 != null)
				{
					message2.Close();
				}
				if (messageBuffer != null)
				{
					messageBuffer.Close();
				}
			}
			return sendAsyncResult;
		}

		// Token: 0x060066E1 RID: 26337 RVA: 0x0017F798 File Offset: 0x0017D998
		public void Close(TimeSpan timeout)
		{
			this.stateManager.Close(timeout);
		}

		// Token: 0x060066E2 RID: 26338 RVA: 0x0017F7A8 File Offset: 0x0017D9A8
		private void CloseCore(TimeSpan timeout, bool graceful)
		{
			Exception ex = null;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262211, SR.GetString("TraceCodePeerNodeClosing"), this.traceRecord, this, null);
			}
			object obj = this.ThisLock;
			PeerMaintainer peerMaintainer;
			PeerNeighborManager peerNeighborManager;
			PeerConnector peerConnector;
			PeerIPHelper peerIPHelper;
			PeerService peerService;
			PeerNodeConfig peerNodeConfig;
			PeerFlooder peerFlooder;
			lock (obj)
			{
				this.isOpen = false;
				peerMaintainer = this.maintainer;
				peerNeighborManager = this.neighborManager;
				peerConnector = this.connector;
				peerIPHelper = this.ipHelper;
				peerService = this.service;
				peerNodeConfig = this.config;
				peerFlooder = this.flooder;
			}
			try
			{
				if (graceful)
				{
					this.UnregisterAddress(timeout);
				}
				else if (peerNodeConfig != null)
				{
					ActionItem.Schedule(new Action<object>(this.UnregisterAddress), peerNodeConfig.UnregisterTimeout);
				}
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
				if (ex == null)
				{
					ex = ex2;
				}
			}
			try
			{
				if (peerConnector != null)
				{
					peerConnector.Closing();
				}
				if (peerService != null)
				{
					try
					{
						peerService.Abort();
					}
					catch (Exception ex3)
					{
						if (Fx.IsFatal(ex3))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(ex3, TraceEventType.Information);
						if (ex == null)
						{
							ex = ex3;
						}
					}
				}
				if (peerMaintainer != null)
				{
					try
					{
						peerMaintainer.Close();
					}
					catch (Exception ex4)
					{
						if (Fx.IsFatal(ex4))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(ex4, TraceEventType.Information);
						if (ex == null)
						{
							ex = ex4;
						}
					}
				}
				if (peerIPHelper != null)
				{
					try
					{
						peerIPHelper.Close();
						peerIPHelper.AddressChanged -= this.stateManager.OnIPAddressesChanged;
					}
					catch (Exception ex5)
					{
						if (Fx.IsFatal(ex5))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(ex5, TraceEventType.Information);
						if (ex == null)
						{
							ex = ex5;
						}
					}
				}
				if (peerNeighborManager != null)
				{
					peerNeighborManager.NeighborConnected -= this.OnNeighborConnected;
					peerNeighborManager.NeighborOpened -= this.securityManager.OnNeighborOpened;
					PeerSecurityManager peerSecurityManager = this.securityManager;
					peerSecurityManager.OnNeighborAuthenticated = (EventHandler)Delegate.Remove(peerSecurityManager.OnNeighborAuthenticated, new EventHandler(this.OnNeighborAuthenticated));
					peerNeighborManager.Online -= this.FireOnline;
					peerNeighborManager.Offline -= this.FireOffline;
					try
					{
						peerNeighborManager.Shutdown(graceful, timeoutHelper.RemainingTime());
					}
					catch (Exception ex6)
					{
						if (Fx.IsFatal(ex6))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(ex6, TraceEventType.Information);
						if (ex == null)
						{
							ex = ex6;
						}
					}
					peerNeighborManager.NeighborClosed -= this.OnNeighborClosed;
					peerNeighborManager.NeighborClosing -= this.OnNeighborClosing;
					peerNeighborManager.Close();
				}
				if (peerConnector != null)
				{
					try
					{
						peerConnector.Close();
					}
					catch (Exception ex7)
					{
						if (Fx.IsFatal(ex7))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(ex7, TraceEventType.Information);
						if (ex == null)
						{
							ex = ex7;
						}
					}
				}
				if (peerFlooder != null)
				{
					try
					{
						peerFlooder.Close();
					}
					catch (Exception ex8)
					{
						if (Fx.IsFatal(ex8))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(ex8, TraceEventType.Information);
						if (ex == null)
						{
							ex = ex8;
						}
					}
				}
			}
			catch (Exception ex9)
			{
				if (Fx.IsFatal(ex9))
				{
					throw;
				}
				if (ex == null)
				{
					ex = ex9;
				}
			}
			EventHandler eventHandler = null;
			object obj2 = this.ThisLock;
			lock (obj2)
			{
				this.neighborManager = null;
				this.connector = null;
				this.maintainer = null;
				this.flooder = null;
				this.ipHelper = null;
				this.service = null;
				this.config = null;
				this.meshId = null;
				eventHandler = this.Aborted;
			}
			if (!graceful && eventHandler != null)
			{
				try
				{
					eventHandler(this, EventArgs.Empty);
				}
				catch (Exception ex10)
				{
					if (Fx.IsFatal(ex10))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(ex10, TraceEventType.Information);
					if (ex == null)
					{
						ex = ex10;
					}
				}
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262212, SR.GetString("TraceCodePeerNodeClosed"), this.traceRecord, this, null);
			}
			if (ex != null && graceful)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
			}
		}

		// Token: 0x060066E3 RID: 26339 RVA: 0x0017FC54 File Offset: 0x0017DE54
		private bool CompareVia(Uri via1, Uri via2)
		{
			return Uri.Compare(via1, via2, UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x0017FC64 File Offset: 0x0017DE64
		public static void EndClose(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			PeerNodeImplementation.SimpleStateManager.EndClose(result);
		}

		// Token: 0x060066E5 RID: 26341 RVA: 0x0017FC7F File Offset: 0x0017DE7F
		public static void EndOpen(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			PeerNodeImplementation.SimpleStateManager.EndOpen(result);
		}

		// Token: 0x060066E6 RID: 26342 RVA: 0x0017FC9A File Offset: 0x0017DE9A
		public static void EndSend(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			PeerNodeImplementation.SendAsyncResult.End(result);
		}

		// Token: 0x060066E7 RID: 26343 RVA: 0x0017FCB8 File Offset: 0x0017DEB8
		private void FireOffline(object sender, EventArgs e)
		{
			if (!this.isOpen)
			{
				return;
			}
			EventHandler offline = this.Offline;
			if (offline != null)
			{
				offline(this, EventArgs.Empty);
			}
		}

		// Token: 0x060066E8 RID: 26344 RVA: 0x0017FCE8 File Offset: 0x0017DEE8
		private void FireOnline(object sender, EventArgs e)
		{
			if (!this.isOpen)
			{
				return;
			}
			EventHandler online = this.Online;
			if (online != null)
			{
				online(this, EventArgs.Empty);
			}
		}

		// Token: 0x060066E9 RID: 26345 RVA: 0x0017FD18 File Offset: 0x0017DF18
		internal static PeerNodeImplementation Get(Uri listenUri)
		{
			PeerNodeImplementation result = null;
			if (!PeerNodeImplementation.TryGet(listenUri, out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoTransportManagerForUri", new object[]
				{
					listenUri
				})));
			}
			return result;
		}

		// Token: 0x060066EA RID: 26346 RVA: 0x0017FD58 File Offset: 0x0017DF58
		protected internal static bool TryGet(Uri listenUri, out PeerNodeImplementation result)
		{
			if (listenUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("listenUri");
			}
			if (listenUri.Scheme != "net.p2p")
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("listenUri", SR.GetString("InvalidUriScheme", new object[]
				{
					listenUri.Scheme,
					"net.p2p"
				}));
			}
			result = null;
			bool result2 = false;
			Uri uri = new UriBuilder("net.p2p", listenUri.Host).Uri;
			Dictionary<Uri, PeerNodeImplementation> obj = PeerNodeImplementation.peerNodes;
			lock (obj)
			{
				if (PeerNodeImplementation.peerNodes.ContainsKey(uri))
				{
					result = PeerNodeImplementation.peerNodes[uri];
					result2 = true;
				}
			}
			return result2;
		}

		// Token: 0x060066EB RID: 26347 RVA: 0x0017FE28 File Offset: 0x0017E028
		public static bool TryGet(string meshId, out PeerNodeImplementation result)
		{
			return PeerNodeImplementation.TryGet(new UriBuilder
			{
				Host = meshId,
				Scheme = "net.p2p"
			}.Uri, out result);
		}

		// Token: 0x060066EC RID: 26348 RVA: 0x0017FE5C File Offset: 0x0017E05C
		public static PeerNodeImplementation Get(Uri listenUri, PeerNodeImplementation.Registration registration)
		{
			if (listenUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("listenUri");
			}
			if (listenUri.Scheme != "net.p2p")
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("listenUri", SR.GetString("InvalidUriScheme", new object[]
				{
					listenUri.Scheme,
					"net.p2p"
				}));
			}
			Uri uri = new UriBuilder("net.p2p", listenUri.Host).Uri;
			Dictionary<Uri, PeerNodeImplementation> obj = PeerNodeImplementation.peerNodes;
			PeerNodeImplementation result;
			lock (obj)
			{
				PeerNodeImplementation peerNodeImplementation = null;
				if (PeerNodeImplementation.peerNodes.TryGetValue(uri, out peerNodeImplementation))
				{
					PeerNodeImplementation peerNodeImplementation2 = peerNodeImplementation;
					registration.CheckIfCompatible(peerNodeImplementation2, listenUri);
					peerNodeImplementation2.refCount++;
					result = peerNodeImplementation2;
				}
				else
				{
					PeerNodeImplementation peerNodeImplementation2 = registration.CreatePeerNode();
					PeerNodeImplementation.peerNodes[uri] = peerNodeImplementation2;
					peerNodeImplementation2.refCount = 1;
					result = peerNodeImplementation2;
				}
			}
			return result;
		}

		// Token: 0x060066ED RID: 26349 RVA: 0x0017FF58 File Offset: 0x0017E158
		private void InternalClose(TimeSpan timeout, bool graceful)
		{
			this.CloseCore(timeout, graceful);
			object obj = this.ThisLock;
			lock (obj)
			{
				this.messageFilters.Clear();
			}
		}

		// Token: 0x060066EE RID: 26350 RVA: 0x0017FFA8 File Offset: 0x0017E1A8
		protected void OnAbort()
		{
			this.InternalClose(TimeSpan.FromTicks(0L), false);
		}

		// Token: 0x060066EF RID: 26351 RVA: 0x0017FFB8 File Offset: 0x0017E1B8
		protected void OnClose(TimeSpan timeout)
		{
			this.InternalClose(timeout, true);
		}

		// Token: 0x060066F0 RID: 26352 RVA: 0x0017FFC4 File Offset: 0x0017E1C4
		private void OnConnectionAttemptCompleted(Exception e)
		{
			this.openException = e;
			if (this.openException == null && DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262209, SR.GetString("TraceCodePeerNodeOpened"), this.completeTraceRecord, this, null);
			}
			else if (this.openException != null && DiagnosticUtility.ShouldTraceError)
			{
				TraceUtility.TraceEvent(TraceEventType.Error, 262210, SR.GetString("TraceCodePeerNodeOpenFailed"), this.completeTraceRecord, this, e);
			}
			this.connectCompletedEvent.Set();
		}

		// Token: 0x060066F1 RID: 26353 RVA: 0x00180040 File Offset: 0x0017E240
		bool IPeerNodeMessageHandling.ValidateIncomingMessage(ref Message message, Uri via)
		{
			SecurityProtocol securityProtocol = null;
			if (via == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerMessageMustHaveVia", new object[]
				{
					message.Headers.Action
				})));
			}
			if (this.TryGetSecurityProtocol(via, out securityProtocol))
			{
				securityProtocol.VerifyIncomingMessage(ref message, ServiceDefaults.SendTimeout, null);
				return true;
			}
			return false;
		}

		// Token: 0x060066F2 RID: 26354 RVA: 0x001800A4 File Offset: 0x0017E2A4
		internal bool TryGetSecurityProtocol(Uri via, out SecurityProtocol protocol)
		{
			object obj = this.ThisLock;
			bool result;
			lock (obj)
			{
				PeerNodeImplementation.RefCountedSecurityProtocol refCountedSecurityProtocol = null;
				bool flag2 = false;
				protocol = null;
				if (this.uri2SecurityProtocol.TryGetValue(via, out refCountedSecurityProtocol))
				{
					protocol = refCountedSecurityProtocol.Protocol;
					flag2 = true;
				}
				result = flag2;
			}
			return result;
		}

		// Token: 0x060066F3 RID: 26355 RVA: 0x00180104 File Offset: 0x0017E304
		void IPeerNodeMessageHandling.HandleIncomingMessage(MessageBuffer messageBuffer, PeerMessagePropagation propagateFlags, int index, MessageHeader hopHeader, Uri via, Uri to)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262213, SR.GetString("TraceCodePeerFloodedMessageReceived"), this.traceRecord, this, null);
			}
			if (via == null)
			{
				using (Message message = messageBuffer.CreateMessage())
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerMessageMustHaveVia", new object[]
					{
						message.Headers.Action
					})));
				}
			}
			if ((propagateFlags & PeerMessagePropagation.Local) != PeerMessagePropagation.None)
			{
				this.DeliverMessageToClientChannels(null, messageBuffer, via, to, messageBuffer.MessageContentType, (int)this.maxReceivedMessageSize, index, hopHeader);
				messageBuffer = null;
				return;
			}
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				using (Message message2 = messageBuffer.CreateMessage())
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 262214, SR.GetString("TraceCodePeerFloodedMessageNotPropagated"), this.traceRecord, this, null, message2);
				}
			}
		}

		// Token: 0x060066F4 RID: 26356 RVA: 0x001801FC File Offset: 0x0017E3FC
		PeerMessagePropagation IPeerNodeMessageHandling.DetermineMessagePropagation(Message message, PeerMessageOrigination origination)
		{
			PeerMessagePropagation propagateFlags = PeerMessagePropagation.LocalAndRemote;
			PeerMessagePropagationFilter filter = this.MessagePropagationFilter;
			if (filter != null)
			{
				try
				{
					SynchronizationContext synchronizationContext = this.messagePropagationFilterContext;
					if (synchronizationContext != null)
					{
						synchronizationContext.Send(delegate(object state)
						{
							propagateFlags = filter.ShouldMessagePropagate(message, origination);
						}, null);
					}
					else
					{
						propagateFlags = filter.ShouldMessagePropagate(message, origination);
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(SR.GetString("MessagePropagationException"), ex);
				}
			}
			if (!this.isOpen)
			{
				propagateFlags = PeerMessagePropagation.None;
			}
			return propagateFlags;
		}

		// Token: 0x060066F5 RID: 26357 RVA: 0x001802C0 File Offset: 0x0017E4C0
		private void OnIPAddressChange()
		{
			string lclMeshId = null;
			PeerNodeAddress peerNodeAddress = null;
			object registrationId = null;
			bool flag = false;
			PeerIPHelper peerIPHelper = this.ipHelper;
			PeerNodeConfig peerNodeConfig = this.config;
			bool flag2 = false;
			TimeoutHelper timeoutHelper = new TimeoutHelper(ServiceDefaults.SendTimeout);
			if (peerIPHelper != null && this.config != null)
			{
				peerNodeAddress = peerNodeConfig.GetListenAddress(false);
				flag2 = peerIPHelper.AddressesChanged(peerNodeAddress.IPAddresses);
				if (flag2)
				{
					peerNodeAddress = new PeerNodeAddress(peerNodeAddress.EndpointAddress, peerIPHelper.GetLocalAddresses());
				}
			}
			object obj = this.ThisLock;
			lock (obj)
			{
				if (!flag2 || !this.isOpen)
				{
					return;
				}
				lclMeshId = this.meshId;
				registrationId = this.resolverRegistrationId;
				flag = this.registered;
				this.config.SetListenAddress(peerNodeAddress);
				this.completeTraceRecord = new PeerNodeTraceRecord(this.config.NodeId, this.meshId, peerNodeAddress);
			}
			try
			{
				if (peerNodeAddress.IPAddresses.Count > 0)
				{
					if (flag)
					{
						this.resolver.Update(registrationId, peerNodeAddress, timeoutHelper.RemainingTime());
					}
					else
					{
						this.RegisterAddress(lclMeshId, peerNodeAddress, timeoutHelper.RemainingTime());
					}
				}
				else
				{
					this.UnregisterAddress(timeoutHelper.RemainingTime());
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
			}
			this.PingConnections();
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262207, SR.GetString("TraceCodePeerNodeAddressChanged"), this.completeTraceRecord, this, null);
			}
		}

		// Token: 0x060066F6 RID: 26358 RVA: 0x00180448 File Offset: 0x0017E648
		private void RegisterAddress(string lclMeshId, PeerNodeAddress nodeAddress, TimeSpan timeout)
		{
			if (nodeAddress.IPAddresses.Count > 0)
			{
				object obj = null;
				try
				{
					obj = this.resolver.Register(lclMeshId, nodeAddress, timeout);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("ResolverException"), ex));
				}
				object obj2 = this.ThisLock;
				lock (obj2)
				{
					if (this.registered)
					{
						throw Fx.AssertAndThrow("registered expected to be false");
					}
					this.registered = true;
					this.resolverRegistrationId = obj;
				}
			}
		}

		// Token: 0x060066F7 RID: 26359 RVA: 0x001804F8 File Offset: 0x0017E6F8
		private void UnregisterAddress(object timeout)
		{
			try
			{
				this.UnregisterAddress((TimeSpan)timeout);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
			}
		}

		// Token: 0x060066F8 RID: 26360 RVA: 0x00180538 File Offset: 0x0017E738
		private void UnregisterAddress(TimeSpan timeout)
		{
			bool flag = false;
			object registrationId = null;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.registered)
				{
					flag = true;
					registrationId = this.resolverRegistrationId;
					this.registered = false;
				}
				this.resolverRegistrationId = null;
			}
			if (flag)
			{
				try
				{
					this.resolver.Unregister(registrationId, timeout);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("ResolverException"), ex));
				}
			}
		}

		// Token: 0x060066F9 RID: 26361 RVA: 0x001805DC File Offset: 0x0017E7DC
		private void OnNeighborClosed(object sender, PeerNeighborCloseEventArgs e)
		{
			IPeerNeighbor neighbor = (IPeerNeighbor)sender;
			PeerConnector peerConnector = this.connector;
			PeerMaintainer peerMaintainer = this.maintainer;
			PeerFlooder peerFlooder = this.flooder;
			UtilityExtension.OnNeighborClosed(neighbor);
			PeerChannelAuthenticatorExtension.OnNeighborClosed(neighbor);
			if (peerConnector != null)
			{
				peerConnector.OnNeighborClosed(neighbor);
			}
			if (peerMaintainer != null)
			{
				peerMaintainer.OnNeighborClosed(neighbor);
			}
			if (peerFlooder != null)
			{
				peerFlooder.OnNeighborClosed(neighbor);
			}
			EventHandler<PeerNeighborCloseEventArgs> neighborClosed = this.NeighborClosed;
			if (neighborClosed != null)
			{
				neighborClosed(this, e);
			}
		}

		// Token: 0x060066FA RID: 26362 RVA: 0x00180644 File Offset: 0x0017E844
		private void OnNeighborClosing(object sender, PeerNeighborCloseEventArgs e)
		{
			IPeerNeighbor neighbor = (IPeerNeighbor)sender;
			PeerConnector peerConnector = this.connector;
			if (peerConnector != null)
			{
				peerConnector.OnNeighborClosing(neighbor, e.Reason);
			}
			EventHandler<PeerNeighborCloseEventArgs> neighborClosing = this.NeighborClosing;
			if (neighborClosing != null)
			{
				neighborClosing(this, e);
			}
		}

		// Token: 0x060066FB RID: 26363 RVA: 0x00180684 File Offset: 0x0017E884
		private void OnNeighborConnected(object sender, EventArgs e)
		{
			IPeerNeighbor neighbor = (IPeerNeighbor)sender;
			PeerMaintainer peerMaintainer = this.maintainer;
			PeerFlooder peerFlooder = this.flooder;
			if (peerFlooder != null)
			{
				peerFlooder.OnNeighborConnected(neighbor);
			}
			if (peerMaintainer != null)
			{
				peerMaintainer.OnNeighborConnected(neighbor);
			}
			UtilityExtension.OnNeighborConnected(neighbor);
			EventHandler neighborConnected = this.NeighborConnected;
			if (neighborConnected != null)
			{
				neighborConnected(this, EventArgs.Empty);
			}
		}

		// Token: 0x060066FC RID: 26364 RVA: 0x001806D8 File Offset: 0x0017E8D8
		private void OnNeighborAuthenticated(object sender, EventArgs e)
		{
			IPeerNeighbor neighbor = (IPeerNeighbor)sender;
			PeerConnector peerConnector = this.connector;
			if (peerConnector != null)
			{
				this.connector.OnNeighborAuthenticated(neighbor);
			}
			EventHandler neighborOpened = this.NeighborOpened;
			if (neighborOpened != null)
			{
				neighborOpened(this, EventArgs.Empty);
			}
		}

		// Token: 0x060066FD RID: 26365 RVA: 0x00180718 File Offset: 0x0017E918
		private void OnOpen(TimeSpan timeout, bool waitForOnline)
		{
			bool aborted = false;
			EventHandler value = delegate(object source, EventArgs args)
			{
				this.connectCompletedEvent.Set();
			};
			EventHandler value2 = delegate(object source, EventArgs args)
			{
				aborted = true;
				this.connectCompletedEvent.Set();
			};
			this.openException = null;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			try
			{
				this.NeighborConnected += value;
				this.Aborted += value2;
				this.OpenCore(timeout);
				if (waitForOnline && !TimeoutHelper.WaitOne(this.connectCompletedEvent, timeoutHelper.RemainingTime()))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException());
				}
				if (aborted)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationObjectAbortedException(SR.GetString("PeerNodeAborted")));
				}
				if (this.isOpen)
				{
					if (this.openException != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.openException);
					}
					string lclMeshId = null;
					PeerNodeConfig peerNodeConfig = null;
					object obj = this.ThisLock;
					lock (obj)
					{
						lclMeshId = this.meshId;
						peerNodeConfig = this.config;
					}
					this.RegisterAddress(lclMeshId, peerNodeConfig.GetListenAddress(false), timeoutHelper.RemainingTime());
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				this.CloseCore(TimeSpan.FromTicks(0L), false);
				throw;
			}
			finally
			{
				this.NeighborConnected -= value;
				this.Aborted -= value2;
			}
		}

		// Token: 0x060066FE RID: 26366 RVA: 0x00180888 File Offset: 0x0017EA88
		internal void Open(TimeSpan timeout, bool waitForOnline)
		{
			this.stateManager.Open(timeout, waitForOnline);
		}

		// Token: 0x060066FF RID: 26367 RVA: 0x00180898 File Offset: 0x0017EA98
		private void OpenCore(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			object obj = this.ThisLock;
			PeerMaintainer peerMaintainer;
			lock (obj)
			{
				if (this.ListenUri == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ListenUriNotSet", new object[]
					{
						base.GetType()
					})));
				}
				this.meshId = this.ListenUri.Host;
				byte[] array = new byte[8];
				ulong num = 0UL;
				do
				{
					CryptoHelper.FillRandomBytes(array);
					for (int i = 0; i < 8; i++)
					{
						num |= (ulong)array[i] << i * 8;
					}
				}
				while (num == 0UL);
				this.traceRecord = new PeerNodeTraceRecord(num, this.meshId);
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 262208, SR.GetString("TraceCodePeerNodeOpening"), this.traceRecord, this, null);
				}
				this.config = new PeerNodeConfig(this.meshId, num, this.resolver, this.messagePropagationFilter, this.encoder, this.ListenUri, this.listenIPAddress, this.port, this.maxReceivedMessageSize, this.minNeighbors, this.idealNeighbors, this.maxNeighbors, this.maxReferrals, this.connectTimeout, this.maintainerInterval, this.securityManager, this.readerQuotas, this.maxBufferPoolSize, this.MaxSendQueue, this.MaxReceiveQueue);
				if (this.listenIPAddress != null)
				{
					this.ipHelper = new PeerIPHelper(this.listenIPAddress);
				}
				else
				{
					this.ipHelper = new PeerIPHelper();
				}
				this.bufferManager = BufferManager.CreateBufferManager(64L * this.config.MaxReceivedMessageSize, (int)this.config.MaxReceivedMessageSize);
				this.neighborManager = new PeerNeighborManager(this.ipHelper, this.config, this);
				this.flooder = PeerFlooder.CreateFlooder(this.config, this.neighborManager, this);
				this.maintainer = new PeerMaintainer(this.config, this.neighborManager, this.flooder);
				this.connector = new PeerConnector(this.config, this.neighborManager, this.maintainer);
				Dictionary<Type, object> dictionary = this.serviceHandlers;
				if (dictionary == null)
				{
					dictionary = new Dictionary<Type, object>();
					dictionary.Add(typeof(IPeerConnectorContract), this.connector);
					dictionary.Add(typeof(IPeerFlooderContract<Message, UtilityInfo>), this.flooder);
				}
				this.service = new PeerService(this.config, new PeerService.ChannelCallback(this.neighborManager.ProcessIncomingChannel), new PeerService.GetNeighborCallback(this.neighborManager.GetNeighborFromProxy), dictionary, this);
				this.securityManager.MeshId = this.meshId;
				this.service.Open(timeoutHelper.RemainingTime());
				this.neighborManager.NeighborClosed += this.OnNeighborClosed;
				this.neighborManager.NeighborClosing += this.OnNeighborClosing;
				this.neighborManager.NeighborConnected += this.OnNeighborConnected;
				this.neighborManager.NeighborOpened += this.SecurityManager.OnNeighborOpened;
				PeerSecurityManager peerSecurityManager = this.securityManager;
				peerSecurityManager.OnNeighborAuthenticated = (EventHandler)Delegate.Combine(peerSecurityManager.OnNeighborAuthenticated, new EventHandler(this.OnNeighborAuthenticated));
				this.neighborManager.Online += this.FireOnline;
				this.neighborManager.Offline += this.FireOffline;
				this.ipHelper.AddressChanged += this.stateManager.OnIPAddressesChanged;
				this.ipHelper.Open();
				PeerNodeAddress peerNodeAddress = new PeerNodeAddress(this.service.GetListenAddress(), this.ipHelper.GetLocalAddresses());
				this.config.SetListenAddress(peerNodeAddress);
				this.neighborManager.Open(this.service.Binding, this.service);
				this.connector.Open();
				this.maintainer.Open();
				this.flooder.Open();
				this.isOpen = true;
				this.completeTraceRecord = new PeerNodeTraceRecord(num, this.meshId, peerNodeAddress);
				peerMaintainer = this.maintainer;
				string text = this.meshId;
				PeerNodeConfig peerNodeConfig = this.config;
				this.openException = null;
			}
			if (this.isOpen)
			{
				peerMaintainer.ScheduleConnect(new PeerMaintainerBase<ConnectAlgorithms>.ConnectCallback(this.OnConnectionAttemptCompleted));
			}
		}

		// Token: 0x06006700 RID: 26368 RVA: 0x00180D10 File Offset: 0x0017EF10
		private void DeliverMessageToClientChannels(object registrant, MessageBuffer messageBuffer, Uri via, Uri peerTo, string contentType, int messageSize, int index, MessageHeader hopHeader)
		{
			Message message = null;
			try
			{
				ArrayList arrayList = new ArrayList();
				if (this.isOpen)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (this.isOpen)
						{
							foreach (PeerNodeImplementation.MessageFilterRegistration messageFilterRegistration in this.messageFilters.Values)
							{
								bool flag2 = this.CompareVia(via, messageFilterRegistration.via);
								if (messageSize < 0)
								{
									if (message == null)
									{
										message = messageBuffer.CreateMessage();
									}
									if (registrant != null)
									{
										messageSize = this.encoder.WriteMessage(message, int.MaxValue, this.bufferManager).Count;
									}
								}
								flag2 = (flag2 && (long)messageSize <= messageFilterRegistration.settings.MaxReceivedMessageSize);
								if (flag2 && messageFilterRegistration.filters != null)
								{
									int num = 0;
									while (flag2 && num < messageFilterRegistration.filters.Length)
									{
										flag2 = messageFilterRegistration.filters[num].Match(via, peerTo);
										num++;
									}
								}
								if (flag2)
								{
									arrayList.Add(messageFilterRegistration.callback);
								}
							}
						}
					}
				}
				foreach (object obj2 in arrayList)
				{
					PeerNodeImplementation.MessageAvailableCallback messageAvailableCallback = (PeerNodeImplementation.MessageAvailableCallback)obj2;
					try
					{
						Message message2 = messageBuffer.CreateMessage();
						message2.Properties.Via = via;
						message2.Headers.To = peerTo;
						try
						{
							int num2 = message2.Headers.FindHeader("Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
							if (num2 >= 0)
							{
								message2.Headers.AddUnderstood(num2);
							}
						}
						catch (MessageHeaderException exception)
						{
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
						}
						catch (SerializationException exception2)
						{
							DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Warning);
						}
						catch (XmlException exception3)
						{
							DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Warning);
						}
						if (index != -1)
						{
							message2.Headers.ReplaceAt(index, hopHeader);
						}
						messageAvailableCallback(message2);
					}
					catch (ObjectDisposedException exception4)
					{
						DiagnosticUtility.TraceHandledException(exception4, TraceEventType.Information);
					}
					catch (CommunicationObjectAbortedException exception5)
					{
						DiagnosticUtility.TraceHandledException(exception5, TraceEventType.Information);
					}
					catch (CommunicationObjectFaultedException exception6)
					{
						DiagnosticUtility.TraceHandledException(exception6, TraceEventType.Information);
					}
				}
			}
			finally
			{
				if (message != null)
				{
					message.Close();
				}
			}
		}

		// Token: 0x06006701 RID: 26369 RVA: 0x00181040 File Offset: 0x0017F240
		public void RefreshConnection()
		{
			PeerMaintainer peerMaintainer = null;
			object obj = this.ThisLock;
			lock (obj)
			{
				this.ThrowIfNotOpen();
				peerMaintainer = this.maintainer;
			}
			if (peerMaintainer != null)
			{
				peerMaintainer.RefreshConnection();
			}
		}

		// Token: 0x06006702 RID: 26370 RVA: 0x00181094 File Offset: 0x0017F294
		public void PingConnections()
		{
			PeerMaintainer peerMaintainer = null;
			object obj = this.ThisLock;
			lock (obj)
			{
				peerMaintainer = this.maintainer;
			}
			if (peerMaintainer != null)
			{
				peerMaintainer.PingConnections();
			}
		}

		// Token: 0x06006703 RID: 26371 RVA: 0x001810E0 File Offset: 0x0017F2E0
		internal void RegisterMessageFilter(object registrant, Uri via, PeerMessageFilter[] filters, ITransportFactorySettings settings, PeerNodeImplementation.MessageAvailableCallback callback, SecurityProtocol securityProtocol)
		{
			PeerNodeImplementation.MessageFilterRegistration messageFilterRegistration = new PeerNodeImplementation.MessageFilterRegistration();
			messageFilterRegistration.registrant = registrant;
			messageFilterRegistration.via = via;
			messageFilterRegistration.filters = filters;
			messageFilterRegistration.settings = settings;
			messageFilterRegistration.callback = callback;
			messageFilterRegistration.securityProtocol = securityProtocol;
			object obj = this.ThisLock;
			lock (obj)
			{
				this.messageFilters.Add(registrant, messageFilterRegistration);
				PeerNodeImplementation.RefCountedSecurityProtocol refCountedSecurityProtocol = null;
				if (!this.uri2SecurityProtocol.TryGetValue(via, out refCountedSecurityProtocol))
				{
					refCountedSecurityProtocol = new PeerNodeImplementation.RefCountedSecurityProtocol(securityProtocol);
					this.uri2SecurityProtocol.Add(via, refCountedSecurityProtocol);
				}
				else
				{
					refCountedSecurityProtocol.AddRef();
				}
			}
		}

		// Token: 0x06006704 RID: 26372 RVA: 0x0018118C File Offset: 0x0017F38C
		internal void Release()
		{
			Dictionary<Uri, PeerNodeImplementation> obj = PeerNodeImplementation.peerNodes;
			lock (obj)
			{
				if (PeerNodeImplementation.peerNodes.ContainsValue(this))
				{
					int num = this.refCount - 1;
					this.refCount = num;
					if (num == 0)
					{
						PeerNodeImplementation.peerNodes.Remove(this.listenUri);
					}
				}
			}
		}

		// Token: 0x06006705 RID: 26373 RVA: 0x001811F8 File Offset: 0x0017F3F8
		public void SetServiceHandlers(Dictionary<Type, object> services)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.serviceHandlers = services;
			}
		}

		// Token: 0x06006706 RID: 26374 RVA: 0x0018123C File Offset: 0x0017F43C
		private void ThrowIfNotOpen()
		{
			if (!this.isOpen)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TransportManagerNotOpen")));
			}
		}

		// Token: 0x06006707 RID: 26375 RVA: 0x00181262 File Offset: 0x0017F462
		private void ThrowIfOpen()
		{
			if (this.isOpen)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TransportManagerOpen")));
			}
		}

		// Token: 0x06006708 RID: 26376 RVA: 0x00181288 File Offset: 0x0017F488
		public override string ToString()
		{
			object obj = this.ThisLock;
			string result;
			lock (obj)
			{
				if (this.isOpen)
				{
					result = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", new object[]
					{
						this.MeshId,
						this.NodeId
					});
				}
				else
				{
					result = base.GetType().ToString();
				}
			}
			return result;
		}

		// Token: 0x06006709 RID: 26377 RVA: 0x00181308 File Offset: 0x0017F508
		internal void UnregisterMessageFilter(object registrant, Uri via)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.messageFilters.Remove(registrant);
				PeerNodeImplementation.RefCountedSecurityProtocol refCountedSecurityProtocol = null;
				if (this.uri2SecurityProtocol.TryGetValue(via, out refCountedSecurityProtocol) && refCountedSecurityProtocol.Release() == 0)
				{
					this.uri2SecurityProtocol.Remove(via);
				}
			}
		}

		// Token: 0x0600670A RID: 26378 RVA: 0x00181378 File Offset: 0x0017F578
		internal static void ValidateVia(Uri uri)
		{
			int byteCount = Encoding.UTF8.GetByteCount(uri.OriginalString);
			if (byteCount > 4096)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataException(SR.GetString("PeerChannelViaTooLong", new object[]
				{
					uri,
					byteCount,
					4096
				})));
			}
		}

		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x0600670B RID: 26379 RVA: 0x001813D8 File Offset: 0x0017F5D8
		bool IPeerNodeMessageHandling.HasMessagePropagation
		{
			get
			{
				return this.messagePropagationFilter != null;
			}
		}

		// Token: 0x0600670C RID: 26380 RVA: 0x001813E4 File Offset: 0x0017F5E4
		bool IPeerNodeMessageHandling.IsKnownVia(Uri via)
		{
			bool result = false;
			object obj = this.ThisLock;
			lock (obj)
			{
				result = this.uri2SecurityProtocol.ContainsKey(via);
			}
			return result;
		}

		// Token: 0x0600670D RID: 26381 RVA: 0x00181430 File Offset: 0x0017F630
		bool IPeerNodeMessageHandling.IsNotSeenBefore(Message message, out byte[] id, out int cacheMiss)
		{
			PeerFlooder peerFlooder = this.flooder;
			id = PeerNodeImplementation.DefaultId;
			cacheMiss = -1;
			return peerFlooder != null && peerFlooder.IsNotSeenBefore(message, out id, out cacheMiss);
		}

		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x0600670E RID: 26382 RVA: 0x0018145C File Offset: 0x0017F65C
		public MessageEncodingBindingElement EncodingBindingElement
		{
			get
			{
				return this.EncodingElement;
			}
		}

		// Token: 0x04003B09 RID: 15113
		private const int maxViaSize = 4096;

		// Token: 0x04003B0A RID: 15114
		private int connectTimeout;

		// Token: 0x04003B0B RID: 15115
		private IPAddress listenIPAddress;

		// Token: 0x04003B0C RID: 15116
		private Uri listenUri;

		// Token: 0x04003B0D RID: 15117
		private int port;

		// Token: 0x04003B0E RID: 15118
		private long maxReceivedMessageSize;

		// Token: 0x04003B0F RID: 15119
		private int minNeighbors;

		// Token: 0x04003B10 RID: 15120
		private int idealNeighbors;

		// Token: 0x04003B11 RID: 15121
		private int maxNeighbors;

		// Token: 0x04003B12 RID: 15122
		private int maxReferrals;

		// Token: 0x04003B13 RID: 15123
		private string meshId;

		// Token: 0x04003B14 RID: 15124
		private PeerMessagePropagationFilter messagePropagationFilter;

		// Token: 0x04003B15 RID: 15125
		private SynchronizationContext messagePropagationFilterContext;

		// Token: 0x04003B16 RID: 15126
		private int maintainerInterval = 300000;

		// Token: 0x04003B17 RID: 15127
		private PeerResolver resolver;

		// Token: 0x04003B18 RID: 15128
		private PeerNodeConfig config;

		// Token: 0x04003B19 RID: 15129
		private PeerSecurityManager securityManager;

		// Token: 0x04003B1A RID: 15130
		internal MessageEncodingBindingElement EncodingElement;

		// Token: 0x04003B1B RID: 15131
		private ManualResetEvent connectCompletedEvent;

		// Token: 0x04003B1C RID: 15132
		private MessageEncoder encoder;

		// Token: 0x04003B1D RID: 15133
		private volatile bool isOpen;

		// Token: 0x04003B1E RID: 15134
		private Exception openException;

		// Token: 0x04003B1F RID: 15135
		private Dictionary<object, PeerNodeImplementation.MessageFilterRegistration> messageFilters;

		// Token: 0x04003B20 RID: 15136
		private int refCount;

		// Token: 0x04003B21 RID: 15137
		private PeerNodeImplementation.SimpleStateManager stateManager;

		// Token: 0x04003B22 RID: 15138
		private object thisLock = new object();

		// Token: 0x04003B23 RID: 15139
		private PeerNodeTraceRecord traceRecord;

		// Token: 0x04003B24 RID: 15140
		private PeerNodeTraceRecord completeTraceRecord;

		// Token: 0x04003B25 RID: 15141
		internal PeerConnector connector;

		// Token: 0x04003B26 RID: 15142
		private PeerMaintainer maintainer;

		// Token: 0x04003B27 RID: 15143
		internal PeerFlooder flooder;

		// Token: 0x04003B28 RID: 15144
		private PeerNeighborManager neighborManager;

		// Token: 0x04003B29 RID: 15145
		private PeerIPHelper ipHelper;

		// Token: 0x04003B2A RID: 15146
		private PeerService service;

		// Token: 0x04003B2B RID: 15147
		private object resolverRegistrationId;

		// Token: 0x04003B2C RID: 15148
		private bool registered;

		// Token: 0x04003B2F RID: 15151
		private Dictionary<Uri, PeerNodeImplementation.RefCountedSecurityProtocol> uri2SecurityProtocol;

		// Token: 0x04003B30 RID: 15152
		private Dictionary<Type, object> serviceHandlers;

		// Token: 0x04003B31 RID: 15153
		private BufferManager bufferManager;

		// Token: 0x04003B32 RID: 15154
		internal static byte[] DefaultId = new byte[0];

		// Token: 0x04003B33 RID: 15155
		private XmlDictionaryReaderQuotas readerQuotas;

		// Token: 0x04003B34 RID: 15156
		private long maxBufferPoolSize;

		// Token: 0x04003B35 RID: 15157
		internal int MaxSendQueue = 128;

		// Token: 0x04003B36 RID: 15158
		internal int MaxReceiveQueue = 128;

		// Token: 0x04003B3C RID: 15164
		internal static Dictionary<Uri, PeerNodeImplementation> peerNodes = new Dictionary<Uri, PeerNodeImplementation>();

		// Token: 0x02000E66 RID: 3686
		// (Invoke) Token: 0x060083AE RID: 33710
		public delegate void MessageAvailableCallback(Message message);

		// Token: 0x02000E67 RID: 3687
		private class RefCountedSecurityProtocol
		{
			// Token: 0x060083B1 RID: 33713 RVA: 0x001E716B File Offset: 0x001E536B
			public RefCountedSecurityProtocol(SecurityProtocol securityProtocol)
			{
				this.Protocol = securityProtocol;
				this.refCount = 1;
			}

			// Token: 0x060083B2 RID: 33714 RVA: 0x001E7184 File Offset: 0x001E5384
			public int AddRef()
			{
				int result = this.refCount + 1;
				this.refCount = result;
				return result;
			}

			// Token: 0x060083B3 RID: 33715 RVA: 0x001E71A4 File Offset: 0x001E53A4
			public int Release()
			{
				int result = this.refCount - 1;
				this.refCount = result;
				return result;
			}

			// Token: 0x04004AEF RID: 19183
			private int refCount;

			// Token: 0x04004AF0 RID: 19184
			public SecurityProtocol Protocol;
		}

		// Token: 0x02000E68 RID: 3688
		internal class ChannelRegistration
		{
			// Token: 0x04004AF1 RID: 19185
			public object registrant;

			// Token: 0x04004AF2 RID: 19186
			public Uri via;

			// Token: 0x04004AF3 RID: 19187
			public ITransportFactorySettings settings;

			// Token: 0x04004AF4 RID: 19188
			public SecurityProtocol securityProtocol;

			// Token: 0x04004AF5 RID: 19189
			public Type channelType;
		}

		// Token: 0x02000E69 RID: 3689
		private class MessageFilterRegistration : PeerNodeImplementation.ChannelRegistration
		{
			// Token: 0x04004AF6 RID: 19190
			public PeerMessageFilter[] filters;

			// Token: 0x04004AF7 RID: 19191
			public PeerNodeImplementation.MessageAvailableCallback callback;
		}

		// Token: 0x02000E6A RID: 3690
		internal class Registration
		{
			// Token: 0x060083B6 RID: 33718 RVA: 0x001E71D4 File Offset: 0x001E53D4
			public Registration(Uri listenUri, IPeerFactory factory)
			{
				if (factory.Resolver == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerResolverRequired")));
				}
				if (factory.ListenIPAddress != null)
				{
					this.listenIPAddress = factory.ListenIPAddress;
				}
				this.listenUri = new UriBuilder("net.p2p", listenUri.Host).Uri;
				this.port = factory.Port;
				this.maxReceivedMessageSize = factory.MaxReceivedMessageSize;
				this.resolver = factory.Resolver;
				this.securityManager = factory.SecurityManager;
				this.readerQuotas = new XmlDictionaryReaderQuotas();
				factory.ReaderQuotas.CopyTo(this.readerQuotas);
				this.maxBufferPoolSize = factory.MaxBufferPoolSize;
			}

			// Token: 0x060083B7 RID: 33719 RVA: 0x001E7290 File Offset: 0x001E5490
			private bool HasMismatchedReaderQuotas(XmlDictionaryReaderQuotas existingOne, XmlDictionaryReaderQuotas newOne, out string result)
			{
				result = null;
				if (existingOne.MaxArrayLength != newOne.MaxArrayLength)
				{
					result = PeerBindingPropertyNames.ReaderQuotasDotArrayLength;
				}
				else if (existingOne.MaxStringContentLength != newOne.MaxStringContentLength)
				{
					result = PeerBindingPropertyNames.ReaderQuotasDotStringLength;
				}
				else if (existingOne.MaxDepth != newOne.MaxDepth)
				{
					result = PeerBindingPropertyNames.ReaderQuotasDotMaxDepth;
				}
				else if (existingOne.MaxNameTableCharCount != newOne.MaxNameTableCharCount)
				{
					result = PeerBindingPropertyNames.ReaderQuotasDotMaxCharCount;
				}
				else if (existingOne.MaxBytesPerRead != newOne.MaxBytesPerRead)
				{
					result = PeerBindingPropertyNames.ReaderQuotasDotMaxBytesPerRead;
				}
				return result != null;
			}

			// Token: 0x060083B8 RID: 33720 RVA: 0x001E7318 File Offset: 0x001E5518
			public void CheckIfCompatible(PeerNodeImplementation peerNode, Uri via)
			{
				string text = null;
				if (this.listenUri != peerNode.ListenUri)
				{
					text = PeerBindingPropertyNames.ListenUri;
				}
				else if (this.port != peerNode.Port)
				{
					text = PeerBindingPropertyNames.Port;
				}
				else if (this.maxReceivedMessageSize != peerNode.MaxReceivedMessageSize)
				{
					text = PeerBindingPropertyNames.MaxReceivedMessageSize;
				}
				else if (this.maxBufferPoolSize != peerNode.MaxBufferPoolSize)
				{
					text = PeerBindingPropertyNames.MaxBufferPoolSize;
				}
				else if (!this.HasMismatchedReaderQuotas(peerNode.ReaderQuotas, this.readerQuotas, out text))
				{
					if (this.resolver.GetType() != peerNode.Resolver.GetType())
					{
						text = PeerBindingPropertyNames.Resolver;
					}
					else if (!this.resolver.Equals(peerNode.Resolver))
					{
						text = PeerBindingPropertyNames.ResolverSettings;
					}
					else if (this.listenIPAddress != peerNode.ListenIPAddress)
					{
						if (this.listenIPAddress == null || peerNode.ListenIPAddress == null || !this.listenIPAddress.Equals(peerNode.ListenIPAddress))
						{
							text = PeerBindingPropertyNames.ListenIPAddress;
						}
					}
					else if (this.securityManager == null && peerNode.SecurityManager != null)
					{
						text = PeerBindingPropertyNames.Security;
					}
				}
				if (text != null)
				{
					PeerExceptionHelper.ThrowInvalidOperation_PeerConflictingPeerNodeSettings(text);
				}
				this.securityManager.CheckIfCompatibleNodeSettings(peerNode.SecurityManager);
			}

			// Token: 0x060083B9 RID: 33721 RVA: 0x001E7454 File Offset: 0x001E5654
			public PeerNodeImplementation CreatePeerNode()
			{
				PeerNodeImplementation peerNodeImplementation = new PeerNodeImplementation();
				peerNodeImplementation.ListenIPAddress = this.listenIPAddress;
				peerNodeImplementation.ListenUri = this.listenUri;
				peerNodeImplementation.MaxReceivedMessageSize = this.maxReceivedMessageSize;
				peerNodeImplementation.Port = this.port;
				peerNodeImplementation.Resolver = this.resolver;
				peerNodeImplementation.SecurityManager = this.securityManager;
				this.readerQuotas.CopyTo(peerNodeImplementation.readerQuotas);
				peerNodeImplementation.MaxBufferPoolSize = this.maxBufferPoolSize;
				return peerNodeImplementation;
			}

			// Token: 0x04004AF8 RID: 19192
			private IPAddress listenIPAddress;

			// Token: 0x04004AF9 RID: 19193
			private Uri listenUri;

			// Token: 0x04004AFA RID: 19194
			private long maxReceivedMessageSize;

			// Token: 0x04004AFB RID: 19195
			private int port;

			// Token: 0x04004AFC RID: 19196
			private PeerResolver resolver;

			// Token: 0x04004AFD RID: 19197
			private PeerSecurityManager securityManager;

			// Token: 0x04004AFE RID: 19198
			private XmlDictionaryReaderQuotas readerQuotas;

			// Token: 0x04004AFF RID: 19199
			private long maxBufferPoolSize;
		}

		// Token: 0x02000E6B RID: 3691
		private class SendAsyncResult : AsyncResult
		{
			// Token: 0x17001D1B RID: 7451
			// (get) Token: 0x060083BA RID: 33722 RVA: 0x001E74CD File Offset: 0x001E56CD
			private object ThisLock
			{
				get
				{
					return this.thisLock;
				}
			}

			// Token: 0x060083BB RID: 33723 RVA: 0x001E74D5 File Offset: 0x001E56D5
			public SendAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
			}

			// Token: 0x060083BC RID: 33724 RVA: 0x001E74EC File Offset: 0x001E56EC
			public void OnFloodComplete(IAsyncResult result)
			{
				if (this.floodComplete || base.IsCompleted)
				{
					return;
				}
				bool flag = false;
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.localDispatchComplete)
					{
						flag = true;
					}
					this.floodComplete = true;
				}
				try
				{
					PeerFlooderBase<Message, UtilityInfo>.EndFloodEncodedMessage(result);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					this.floodException = exception;
				}
				if (flag)
				{
					base.Complete(result.CompletedSynchronously, this.floodException);
				}
			}

			// Token: 0x060083BD RID: 33725 RVA: 0x001E7590 File Offset: 0x001E5790
			public void OnLocalDispatchComplete(IAsyncResult result)
			{
				PeerNodeImplementation.SendAsyncResult sendAsyncResult = (PeerNodeImplementation.SendAsyncResult)result;
				if (this.localDispatchComplete || base.IsCompleted)
				{
					return;
				}
				bool flag = false;
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.floodComplete)
					{
						flag = true;
					}
					this.localDispatchComplete = true;
				}
				if (flag)
				{
					base.Complete(true, this.floodException);
				}
			}

			// Token: 0x060083BE RID: 33726 RVA: 0x001E7608 File Offset: 0x001E5808
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<PeerNodeImplementation.SendAsyncResult>(result);
			}

			// Token: 0x04004B00 RID: 19200
			private bool floodComplete;

			// Token: 0x04004B01 RID: 19201
			private bool localDispatchComplete;

			// Token: 0x04004B02 RID: 19202
			private object thisLock = new object();

			// Token: 0x04004B03 RID: 19203
			private Exception floodException;
		}

		// Token: 0x02000E6C RID: 3692
		private class SimpleStateManager
		{
			// Token: 0x060083BF RID: 33727 RVA: 0x001E7611 File Offset: 0x001E5811
			public SimpleStateManager(PeerNodeImplementation peerNode)
			{
				this.peerNode = peerNode;
			}

			// Token: 0x17001D1C RID: 7452
			// (get) Token: 0x060083C0 RID: 33728 RVA: 0x001E7636 File Offset: 0x001E5836
			private object ThisLock
			{
				get
				{
					return this.thisLock;
				}
			}

			// Token: 0x060083C1 RID: 33729 RVA: 0x001E7640 File Offset: 0x001E5840
			public void Abort()
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					bool flag2 = false;
					if (this.openCount <= 1 && this.currentState != PeerNodeImplementation.SimpleStateManager.State.NotOpened)
					{
						flag2 = true;
					}
					if (this.openCount > 0)
					{
						this.openCount--;
					}
					if (flag2)
					{
						try
						{
							this.peerNode.OnAbort();
						}
						finally
						{
							this.currentState = PeerNodeImplementation.SimpleStateManager.State.NotOpened;
						}
					}
				}
			}

			// Token: 0x060083C2 RID: 33730 RVA: 0x001E76CC File Offset: 0x001E58CC
			public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				PeerNodeImplementation.SimpleStateManager.CloseOperation closeOperation = null;
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.openCount > 0)
					{
						this.openCount--;
					}
					if (this.openCount > 0)
					{
						return new CompletedAsyncResult(callback, state);
					}
					closeOperation = new PeerNodeImplementation.SimpleStateManager.CloseOperation(this, this.peerNode, timeout, callback, state);
					this.queue.Enqueue(closeOperation);
					this.RunQueue();
				}
				return closeOperation;
			}

			// Token: 0x060083C3 RID: 33731 RVA: 0x001E7758 File Offset: 0x001E5958
			public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state, bool waitForOnline)
			{
				bool flag = false;
				PeerNodeImplementation.SimpleStateManager.OpenOperation openOperation = null;
				object obj = this.ThisLock;
				lock (obj)
				{
					this.openCount++;
					if (this.openCount > 1 && this.currentState == PeerNodeImplementation.SimpleStateManager.State.Opened)
					{
						flag = true;
					}
					else
					{
						openOperation = new PeerNodeImplementation.SimpleStateManager.OpenOperation(this, this.peerNode, timeout, callback, state, waitForOnline);
						this.queue.Enqueue(openOperation);
						this.RunQueue();
					}
				}
				if (flag)
				{
					return new CompletedAsyncResult(callback, state);
				}
				return openOperation;
			}

			// Token: 0x060083C4 RID: 33732 RVA: 0x001E77EC File Offset: 0x001E59EC
			public void Close(TimeSpan timeout)
			{
				PeerNodeImplementation.SimpleStateManager.EndClose(this.BeginClose(timeout, null, null));
			}

			// Token: 0x060083C5 RID: 33733 RVA: 0x001E77FC File Offset: 0x001E59FC
			public static void EndOpen(IAsyncResult result)
			{
				if (result is CompletedAsyncResult)
				{
					CompletedAsyncResult.End(result);
					return;
				}
				PeerNodeImplementation.SimpleStateManager.OperationBase.End(result);
			}

			// Token: 0x060083C6 RID: 33734 RVA: 0x001E7813 File Offset: 0x001E5A13
			public static void EndClose(IAsyncResult result)
			{
				if (result is CompletedAsyncResult)
				{
					CompletedAsyncResult.End(result);
					return;
				}
				PeerNodeImplementation.SimpleStateManager.OperationBase.End(result);
			}

			// Token: 0x060083C7 RID: 33735 RVA: 0x001E782C File Offset: 0x001E5A2C
			public void OnIPAddressesChanged(object sender, EventArgs e)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					PeerNodeImplementation.SimpleStateManager.IPAddressChangeOperation item = new PeerNodeImplementation.SimpleStateManager.IPAddressChangeOperation(this.peerNode);
					this.queue.Enqueue(item);
					this.RunQueue();
				}
			}

			// Token: 0x060083C8 RID: 33736 RVA: 0x001E7888 File Offset: 0x001E5A88
			public void Open(TimeSpan timeout, bool waitForOnline)
			{
				PeerNodeImplementation.SimpleStateManager.EndOpen(this.BeginOpen(timeout, null, null, waitForOnline));
			}

			// Token: 0x060083C9 RID: 33737 RVA: 0x001E7899 File Offset: 0x001E5A99
			private void RunQueue()
			{
				if (this.queueRunning)
				{
					return;
				}
				this.queueRunning = true;
				ActionItem.Schedule(new Action<object>(this.RunQueueCallback), null);
			}

			// Token: 0x060083CA RID: 33738 RVA: 0x001E78C0 File Offset: 0x001E5AC0
			private void RunQueueCallback(object state)
			{
				object obj = this.ThisLock;
				PeerNodeImplementation.SimpleStateManager.IOperation operation;
				lock (obj)
				{
					operation = this.queue.Dequeue();
				}
				try
				{
					operation.Run();
				}
				finally
				{
					object obj2 = this.ThisLock;
					lock (obj2)
					{
						if (this.queue.Count > 0)
						{
							try
							{
								ActionItem.Schedule(new Action<object>(this.RunQueueCallback), null);
								goto IL_91;
							}
							catch (Exception exception)
							{
								if (Fx.IsFatal(exception))
								{
									throw;
								}
								DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
								goto IL_91;
							}
						}
						this.queueRunning = false;
					}
					IL_91:;
				}
			}

			// Token: 0x04004B04 RID: 19204
			private PeerNodeImplementation.SimpleStateManager.State currentState;

			// Token: 0x04004B05 RID: 19205
			private object thisLock = new object();

			// Token: 0x04004B06 RID: 19206
			private Queue<PeerNodeImplementation.SimpleStateManager.IOperation> queue = new Queue<PeerNodeImplementation.SimpleStateManager.IOperation>();

			// Token: 0x04004B07 RID: 19207
			private bool queueRunning;

			// Token: 0x04004B08 RID: 19208
			private int openCount;

			// Token: 0x04004B09 RID: 19209
			private PeerNodeImplementation peerNode;

			// Token: 0x02000F93 RID: 3987
			internal enum State
			{
				// Token: 0x04004F9D RID: 20381
				NotOpened,
				// Token: 0x04004F9E RID: 20382
				Opening,
				// Token: 0x04004F9F RID: 20383
				Opened,
				// Token: 0x04004FA0 RID: 20384
				Closing
			}

			// Token: 0x02000F94 RID: 3988
			private interface IOperation
			{
				// Token: 0x06008867 RID: 34919
				void Run();
			}

			// Token: 0x02000F95 RID: 3989
			private class CloseOperation : PeerNodeImplementation.SimpleStateManager.OperationBase
			{
				// Token: 0x06008868 RID: 34920 RVA: 0x001FB178 File Offset: 0x001F9378
				public CloseOperation(PeerNodeImplementation.SimpleStateManager stateManager, PeerNodeImplementation peerNode, TimeSpan timeout, AsyncCallback callback, object state) : base(stateManager, timeout, callback, state)
				{
					this.peerNode = peerNode;
				}

				// Token: 0x06008869 RID: 34921 RVA: 0x001FB190 File Offset: 0x001F9390
				protected override void Run()
				{
					Exception exception = null;
					try
					{
						object thisLock = base.ThisLock;
						lock (thisLock)
						{
							if (this.stateManager.openCount > 0)
							{
								this.invokeOperation = false;
							}
							else if (this.stateManager.currentState == PeerNodeImplementation.SimpleStateManager.State.NotOpened)
							{
								this.invokeOperation = false;
							}
							else
							{
								if (this.timeoutHelper.RemainingTime() <= TimeSpan.Zero)
								{
									this.invokeOperation = false;
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException());
								}
								if (this.stateManager.currentState == PeerNodeImplementation.SimpleStateManager.State.Opening || this.stateManager.currentState == PeerNodeImplementation.SimpleStateManager.State.Closing)
								{
									throw Fx.AssertAndThrow("Open and close are serialized by queue We should not be either in Closing or Opening state at this point");
								}
								if (this.stateManager.currentState != PeerNodeImplementation.SimpleStateManager.State.NotOpened)
								{
									this.stateManager.currentState = PeerNodeImplementation.SimpleStateManager.State.Closing;
									this.invokeOperation = true;
								}
							}
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
						exception = ex;
					}
					if (this.invokeOperation)
					{
						try
						{
							this.peerNode.OnClose(this.timeoutHelper.RemainingTime());
						}
						catch (Exception ex2)
						{
							if (Fx.IsFatal(ex2))
							{
								throw;
							}
							DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
							exception = ex2;
						}
						object thisLock2 = base.ThisLock;
						lock (thisLock2)
						{
							this.stateManager.currentState = PeerNodeImplementation.SimpleStateManager.State.NotOpened;
						}
					}
					base.Complete(exception);
				}

				// Token: 0x04004FA1 RID: 20385
				private PeerNodeImplementation peerNode;
			}

			// Token: 0x02000F96 RID: 3990
			private class OpenOperation : PeerNodeImplementation.SimpleStateManager.OperationBase
			{
				// Token: 0x0600886A RID: 34922 RVA: 0x001FB31C File Offset: 0x001F951C
				public OpenOperation(PeerNodeImplementation.SimpleStateManager stateManager, PeerNodeImplementation peerNode, TimeSpan timeout, AsyncCallback callback, object state, bool waitForOnline) : base(stateManager, timeout, callback, state)
				{
					this.peerNode = peerNode;
					this.waitForOnline = waitForOnline;
				}

				// Token: 0x0600886B RID: 34923 RVA: 0x001FB33C File Offset: 0x001F953C
				protected override void Run()
				{
					Exception exception = null;
					try
					{
						object thisLock = base.ThisLock;
						lock (thisLock)
						{
							if (this.stateManager.openCount < 1)
							{
								this.invokeOperation = false;
							}
							else if (this.stateManager.currentState == PeerNodeImplementation.SimpleStateManager.State.Opened)
							{
								this.invokeOperation = false;
							}
							else
							{
								if (this.timeoutHelper.RemainingTime() <= TimeSpan.Zero)
								{
									this.invokeOperation = false;
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException());
								}
								if (this.stateManager.currentState == PeerNodeImplementation.SimpleStateManager.State.Opening || this.stateManager.currentState == PeerNodeImplementation.SimpleStateManager.State.Closing)
								{
									throw Fx.AssertAndThrow("Open and close are serialized by queue We should not be either in Closing or Opening state at this point");
								}
								if (this.stateManager.currentState != PeerNodeImplementation.SimpleStateManager.State.Opened)
								{
									this.stateManager.currentState = PeerNodeImplementation.SimpleStateManager.State.Opening;
									this.invokeOperation = true;
								}
							}
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
						exception = ex;
					}
					if (this.invokeOperation)
					{
						try
						{
							this.peerNode.OnOpen(this.timeoutHelper.RemainingTime(), this.waitForOnline);
							object thisLock2 = base.ThisLock;
							lock (thisLock2)
							{
								this.stateManager.currentState = PeerNodeImplementation.SimpleStateManager.State.Opened;
							}
						}
						catch (Exception ex2)
						{
							if (Fx.IsFatal(ex2))
							{
								throw;
							}
							object thisLock3 = base.ThisLock;
							lock (thisLock3)
							{
								this.stateManager.currentState = PeerNodeImplementation.SimpleStateManager.State.NotOpened;
								this.stateManager.openCount--;
							}
							exception = ex2;
							DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
						}
					}
					base.Complete(exception);
				}

				// Token: 0x04004FA2 RID: 20386
				private PeerNodeImplementation peerNode;

				// Token: 0x04004FA3 RID: 20387
				private bool waitForOnline;
			}

			// Token: 0x02000F97 RID: 3991
			private abstract class OperationBase : AsyncResult, PeerNodeImplementation.SimpleStateManager.IOperation
			{
				// Token: 0x0600886C RID: 34924 RVA: 0x001FB520 File Offset: 0x001F9720
				public OperationBase(PeerNodeImplementation.SimpleStateManager stateManager, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.stateManager = stateManager;
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.callback = callback;
					this.invokeOperation = false;
					this.completed = false;
				}

				// Token: 0x0600886D RID: 34925 RVA: 0x001FB558 File Offset: 0x001F9758
				private void AsyncComplete(object o)
				{
					try
					{
						base.Complete(false, (Exception)o);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(SR.GetString("AsyncCallbackException"), ex);
					}
				}

				// Token: 0x0600886E RID: 34926
				protected abstract void Run();

				// Token: 0x0600886F RID: 34927 RVA: 0x001FB5A8 File Offset: 0x001F97A8
				void PeerNodeImplementation.SimpleStateManager.IOperation.Run()
				{
					this.Run();
				}

				// Token: 0x06008870 RID: 34928 RVA: 0x001FB5B0 File Offset: 0x001F97B0
				protected void Complete(Exception exception)
				{
					if (this.completed)
					{
						return;
					}
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.completed)
						{
							return;
						}
						this.completed = true;
					}
					try
					{
						if (this.callback != null)
						{
							ActionItem.Schedule(new Action<object>(this.AsyncComplete), exception);
						}
						else
						{
							this.AsyncComplete(exception);
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(SR.GetString("MessagePropagationException"), ex);
					}
				}

				// Token: 0x17001DAC RID: 7596
				// (get) Token: 0x06008871 RID: 34929 RVA: 0x001FB660 File Offset: 0x001F9860
				protected object ThisLock
				{
					get
					{
						return this.stateManager.thisLock;
					}
				}

				// Token: 0x06008872 RID: 34930 RVA: 0x001FB66D File Offset: 0x001F986D
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<PeerNodeImplementation.SimpleStateManager.OperationBase>(result);
				}

				// Token: 0x04004FA4 RID: 20388
				protected PeerNodeImplementation.SimpleStateManager stateManager;

				// Token: 0x04004FA5 RID: 20389
				protected TimeoutHelper timeoutHelper;

				// Token: 0x04004FA6 RID: 20390
				private AsyncCallback callback;

				// Token: 0x04004FA7 RID: 20391
				protected bool invokeOperation;

				// Token: 0x04004FA8 RID: 20392
				private volatile bool completed;
			}

			// Token: 0x02000F98 RID: 3992
			private class IPAddressChangeOperation : PeerNodeImplementation.SimpleStateManager.IOperation
			{
				// Token: 0x06008873 RID: 34931 RVA: 0x001FB676 File Offset: 0x001F9876
				public IPAddressChangeOperation(PeerNodeImplementation peerNode)
				{
					this.peerNode = peerNode;
				}

				// Token: 0x06008874 RID: 34932 RVA: 0x001FB685 File Offset: 0x001F9885
				void PeerNodeImplementation.SimpleStateManager.IOperation.Run()
				{
					this.peerNode.OnIPAddressChange();
				}

				// Token: 0x04004FA9 RID: 20393
				private PeerNodeImplementation peerNode;
			}
		}
	}
}
