using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.CSharp.RuntimeBinder;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;
using Renci.SshNet.Compression;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Authentication;
using Renci.SshNet.Messages.Connection;
using Renci.SshNet.Messages.Transport;
using Renci.SshNet.Security;
using Renci.SshNet.Security.Cryptography;

namespace Renci.SshNet
{
	// Token: 0x02000029 RID: 41
	public class Session : ISession, IDisposable
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000080CC File Offset: 0x000062CC
		public SemaphoreLight SessionSemaphore
		{
			get
			{
				if (this._sessionSemaphore == null)
				{
					lock (this)
					{
						if (this._sessionSemaphore == null)
						{
							this._sessionSemaphore = new SemaphoreLight(this.ConnectionInfo.MaxSessions);
						}
					}
				}
				return this._sessionSemaphore;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00008130 File Offset: 0x00006330
		private uint NextChannelNumber
		{
			get
			{
				uint result;
				lock (this)
				{
					uint nextChannelNumber = this._nextChannelNumber;
					this._nextChannelNumber = nextChannelNumber + 1U;
					result = nextChannelNumber;
				}
				return result;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00008178 File Offset: 0x00006378
		public bool IsConnected
		{
			get
			{
				if (this._disposed || this._isDisconnectMessageSent || !this._isAuthenticated)
				{
					return false;
				}
				if (this._messageListenerCompleted == null || this._messageListenerCompleted.WaitOne(0))
				{
					return false;
				}
				bool result = false;
				this.IsSocketConnected(ref result);
				return result;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000081C2 File Offset: 0x000063C2
		// (set) Token: 0x06000221 RID: 545 RVA: 0x000081CA File Offset: 0x000063CA
		public byte[] SessionId { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000081D4 File Offset: 0x000063D4
		public Message ClientInitMessage
		{
			get
			{
				if (this._clientInitMessage == null)
				{
					this._clientInitMessage = new KeyExchangeInitMessage
					{
						KeyExchangeAlgorithms = this.ConnectionInfo.KeyExchangeAlgorithms.Keys.ToArray<string>(),
						ServerHostKeyAlgorithms = this.ConnectionInfo.HostKeyAlgorithms.Keys.ToArray<string>(),
						EncryptionAlgorithmsClientToServer = this.ConnectionInfo.Encryptions.Keys.ToArray<string>(),
						EncryptionAlgorithmsServerToClient = this.ConnectionInfo.Encryptions.Keys.ToArray<string>(),
						MacAlgorithmsClientToServer = this.ConnectionInfo.HmacAlgorithms.Keys.ToArray<string>(),
						MacAlgorithmsServerToClient = this.ConnectionInfo.HmacAlgorithms.Keys.ToArray<string>(),
						CompressionAlgorithmsClientToServer = this.ConnectionInfo.CompressionAlgorithms.Keys.ToArray<string>(),
						CompressionAlgorithmsServerToClient = this.ConnectionInfo.CompressionAlgorithms.Keys.ToArray<string>(),
						LanguagesClientToServer = new string[]
						{
							string.Empty
						},
						LanguagesServerToClient = new string[]
						{
							string.Empty
						},
						FirstKexPacketFollows = false,
						Reserved = 0U
					};
				}
				return this._clientInitMessage;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000830D File Offset: 0x0000650D
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00008315 File Offset: 0x00006515
		public string ServerVersion { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000831E File Offset: 0x0000651E
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00008326 File Offset: 0x00006526
		public string ClientVersion { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000832F File Offset: 0x0000652F
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00008337 File Offset: 0x00006537
		public ConnectionInfo ConnectionInfo { get; private set; }

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000229 RID: 553 RVA: 0x00008340 File Offset: 0x00006540
		// (remove) Token: 0x0600022A RID: 554 RVA: 0x00008378 File Offset: 0x00006578
		public event EventHandler<ExceptionEventArgs> ErrorOccured;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600022B RID: 555 RVA: 0x000083B0 File Offset: 0x000065B0
		// (remove) Token: 0x0600022C RID: 556 RVA: 0x000083E8 File Offset: 0x000065E8
		public event EventHandler<EventArgs> Disconnected;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600022D RID: 557 RVA: 0x00008420 File Offset: 0x00006620
		// (remove) Token: 0x0600022E RID: 558 RVA: 0x00008458 File Offset: 0x00006658
		public event EventHandler<HostKeyEventArgs> HostKeyReceived;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600022F RID: 559 RVA: 0x00008490 File Offset: 0x00006690
		// (remove) Token: 0x06000230 RID: 560 RVA: 0x000084C8 File Offset: 0x000066C8
		public event EventHandler<MessageEventArgs<BannerMessage>> UserAuthenticationBannerReceived;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000231 RID: 561 RVA: 0x00008500 File Offset: 0x00006700
		// (remove) Token: 0x06000232 RID: 562 RVA: 0x00008538 File Offset: 0x00006738
		internal event EventHandler<MessageEventArgs<DisconnectMessage>> DisconnectReceived;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000233 RID: 563 RVA: 0x00008570 File Offset: 0x00006770
		// (remove) Token: 0x06000234 RID: 564 RVA: 0x000085A8 File Offset: 0x000067A8
		internal event EventHandler<MessageEventArgs<IgnoreMessage>> IgnoreReceived;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000235 RID: 565 RVA: 0x000085E0 File Offset: 0x000067E0
		// (remove) Token: 0x06000236 RID: 566 RVA: 0x00008618 File Offset: 0x00006818
		internal event EventHandler<MessageEventArgs<UnimplementedMessage>> UnimplementedReceived;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000237 RID: 567 RVA: 0x00008650 File Offset: 0x00006850
		// (remove) Token: 0x06000238 RID: 568 RVA: 0x00008688 File Offset: 0x00006888
		internal event EventHandler<MessageEventArgs<DebugMessage>> DebugReceived;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000239 RID: 569 RVA: 0x000086C0 File Offset: 0x000068C0
		// (remove) Token: 0x0600023A RID: 570 RVA: 0x000086F8 File Offset: 0x000068F8
		internal event EventHandler<MessageEventArgs<ServiceRequestMessage>> ServiceRequestReceived;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600023B RID: 571 RVA: 0x00008730 File Offset: 0x00006930
		// (remove) Token: 0x0600023C RID: 572 RVA: 0x00008768 File Offset: 0x00006968
		internal event EventHandler<MessageEventArgs<ServiceAcceptMessage>> ServiceAcceptReceived;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600023D RID: 573 RVA: 0x000087A0 File Offset: 0x000069A0
		// (remove) Token: 0x0600023E RID: 574 RVA: 0x000087D8 File Offset: 0x000069D8
		internal event EventHandler<MessageEventArgs<KeyExchangeInitMessage>> KeyExchangeInitReceived;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600023F RID: 575 RVA: 0x00008810 File Offset: 0x00006A10
		// (remove) Token: 0x06000240 RID: 576 RVA: 0x00008848 File Offset: 0x00006A48
		internal event EventHandler<MessageEventArgs<NewKeysMessage>> NewKeysReceived;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000241 RID: 577 RVA: 0x00008880 File Offset: 0x00006A80
		// (remove) Token: 0x06000242 RID: 578 RVA: 0x000088B8 File Offset: 0x00006AB8
		internal event EventHandler<MessageEventArgs<RequestMessage>> UserAuthenticationRequestReceived;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000243 RID: 579 RVA: 0x000088F0 File Offset: 0x00006AF0
		// (remove) Token: 0x06000244 RID: 580 RVA: 0x00008928 File Offset: 0x00006B28
		internal event EventHandler<MessageEventArgs<FailureMessage>> UserAuthenticationFailureReceived;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000245 RID: 581 RVA: 0x00008960 File Offset: 0x00006B60
		// (remove) Token: 0x06000246 RID: 582 RVA: 0x00008998 File Offset: 0x00006B98
		internal event EventHandler<MessageEventArgs<SuccessMessage>> UserAuthenticationSuccessReceived;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000247 RID: 583 RVA: 0x000089D0 File Offset: 0x00006BD0
		// (remove) Token: 0x06000248 RID: 584 RVA: 0x00008A08 File Offset: 0x00006C08
		internal event EventHandler<MessageEventArgs<GlobalRequestMessage>> GlobalRequestReceived;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000249 RID: 585 RVA: 0x00008A40 File Offset: 0x00006C40
		// (remove) Token: 0x0600024A RID: 586 RVA: 0x00008A78 File Offset: 0x00006C78
		public event EventHandler<MessageEventArgs<RequestSuccessMessage>> RequestSuccessReceived;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x0600024B RID: 587 RVA: 0x00008AB0 File Offset: 0x00006CB0
		// (remove) Token: 0x0600024C RID: 588 RVA: 0x00008AE8 File Offset: 0x00006CE8
		public event EventHandler<MessageEventArgs<RequestFailureMessage>> RequestFailureReceived;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x0600024D RID: 589 RVA: 0x00008B20 File Offset: 0x00006D20
		// (remove) Token: 0x0600024E RID: 590 RVA: 0x00008B58 File Offset: 0x00006D58
		public event EventHandler<MessageEventArgs<ChannelOpenMessage>> ChannelOpenReceived;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x0600024F RID: 591 RVA: 0x00008B90 File Offset: 0x00006D90
		// (remove) Token: 0x06000250 RID: 592 RVA: 0x00008BC8 File Offset: 0x00006DC8
		public event EventHandler<MessageEventArgs<ChannelOpenConfirmationMessage>> ChannelOpenConfirmationReceived;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06000251 RID: 593 RVA: 0x00008C00 File Offset: 0x00006E00
		// (remove) Token: 0x06000252 RID: 594 RVA: 0x00008C38 File Offset: 0x00006E38
		public event EventHandler<MessageEventArgs<ChannelOpenFailureMessage>> ChannelOpenFailureReceived;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06000253 RID: 595 RVA: 0x00008C70 File Offset: 0x00006E70
		// (remove) Token: 0x06000254 RID: 596 RVA: 0x00008CA8 File Offset: 0x00006EA8
		public event EventHandler<MessageEventArgs<ChannelWindowAdjustMessage>> ChannelWindowAdjustReceived;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000255 RID: 597 RVA: 0x00008CE0 File Offset: 0x00006EE0
		// (remove) Token: 0x06000256 RID: 598 RVA: 0x00008D18 File Offset: 0x00006F18
		public event EventHandler<MessageEventArgs<ChannelDataMessage>> ChannelDataReceived;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000257 RID: 599 RVA: 0x00008D50 File Offset: 0x00006F50
		// (remove) Token: 0x06000258 RID: 600 RVA: 0x00008D88 File Offset: 0x00006F88
		public event EventHandler<MessageEventArgs<ChannelExtendedDataMessage>> ChannelExtendedDataReceived;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000259 RID: 601 RVA: 0x00008DC0 File Offset: 0x00006FC0
		// (remove) Token: 0x0600025A RID: 602 RVA: 0x00008DF8 File Offset: 0x00006FF8
		public event EventHandler<MessageEventArgs<ChannelEofMessage>> ChannelEofReceived;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x0600025B RID: 603 RVA: 0x00008E30 File Offset: 0x00007030
		// (remove) Token: 0x0600025C RID: 604 RVA: 0x00008E68 File Offset: 0x00007068
		public event EventHandler<MessageEventArgs<ChannelCloseMessage>> ChannelCloseReceived;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x0600025D RID: 605 RVA: 0x00008EA0 File Offset: 0x000070A0
		// (remove) Token: 0x0600025E RID: 606 RVA: 0x00008ED8 File Offset: 0x000070D8
		public event EventHandler<MessageEventArgs<ChannelRequestMessage>> ChannelRequestReceived;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x0600025F RID: 607 RVA: 0x00008F10 File Offset: 0x00007110
		// (remove) Token: 0x06000260 RID: 608 RVA: 0x00008F48 File Offset: 0x00007148
		public event EventHandler<MessageEventArgs<ChannelSuccessMessage>> ChannelSuccessReceived;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000261 RID: 609 RVA: 0x00008F80 File Offset: 0x00007180
		// (remove) Token: 0x06000262 RID: 610 RVA: 0x00008FB8 File Offset: 0x000071B8
		public event EventHandler<MessageEventArgs<ChannelFailureMessage>> ChannelFailureReceived;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000263 RID: 611 RVA: 0x00008FF0 File Offset: 0x000071F0
		// (remove) Token: 0x06000264 RID: 612 RVA: 0x00009028 File Offset: 0x00007228
		internal event EventHandler<MessageEventArgs<Message>> MessageReceived;

		// Token: 0x06000265 RID: 613 RVA: 0x00009060 File Offset: 0x00007260
		internal Session(ConnectionInfo connectionInfo, IServiceFactory serviceFactory)
		{
			if (connectionInfo == null)
			{
				throw new ArgumentNullException("connectionInfo");
			}
			if (serviceFactory == null)
			{
				throw new ArgumentNullException("serviceFactory");
			}
			this.ClientVersion = "SSH-2.0-Renci.SshNet.SshClient.0.0.1";
			this.ConnectionInfo = connectionInfo;
			this._serviceFactory = serviceFactory;
			this._messageListenerCompleted = new ManualResetEvent(true);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000090FC File Offset: 0x000072FC
		public void Connect()
		{
			if (this.IsConnected)
			{
				return;
			}
			try
			{
				Session.AuthenticationConnection.Wait();
				if (!this.IsConnected)
				{
					lock (this)
					{
						if (!this.IsConnected)
						{
							this.Reset();
							this._sshMessageFactory = new SshMessageFactory();
							switch (this.ConnectionInfo.ProxyType)
							{
							case ProxyTypes.None:
								this.SocketConnect(this.ConnectionInfo.Host, this.ConnectionInfo.Port);
								break;
							case ProxyTypes.Socks4:
								this.SocketConnect(this.ConnectionInfo.ProxyHost, this.ConnectionInfo.ProxyPort);
								this.ConnectSocks4();
								break;
							case ProxyTypes.Socks5:
								this.SocketConnect(this.ConnectionInfo.ProxyHost, this.ConnectionInfo.ProxyPort);
								this.ConnectSocks5();
								break;
							case ProxyTypes.Http:
								this.SocketConnect(this.ConnectionInfo.ProxyHost, this.ConnectionInfo.ProxyPort);
								this.ConnectHttp();
								break;
							}
							string text;
							Match match;
							for (;;)
							{
								text = this.SocketReadLine(this.ConnectionInfo.Timeout);
								if (text == null)
								{
									break;
								}
								match = Session.ServerVersionRe.Match(text);
								if (match.Success)
								{
									goto Block_10;
								}
							}
							throw new SshConnectionException("Server response does not contain SSH protocol identification.", DisconnectReason.ProtocolError);
							Block_10:
							this.ServerVersion = text;
							this.ConnectionInfo.ServerVersion = this.ServerVersion;
							this.ConnectionInfo.ClientVersion = this.ClientVersion;
							string text2 = match.Result("${protoversion}");
							match.Result("${softwareversion}");
							if (!text2.Equals("2.0") && !text2.Equals("1.99"))
							{
								throw new SshConnectionException(string.Format(CultureInfo.CurrentCulture, "Server version '{0}' is not supported.", new object[]
								{
									text2
								}), DisconnectReason.ProtocolVersionNotSupported);
							}
							SocketAbstraction.Send(this._socket, Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0}\r\n", new object[]
							{
								this.ClientVersion
							})));
							this.RegisterMessage("SSH_MSG_DISCONNECT");
							this.RegisterMessage("SSH_MSG_IGNORE");
							this.RegisterMessage("SSH_MSG_UNIMPLEMENTED");
							this.RegisterMessage("SSH_MSG_DEBUG");
							this.RegisterMessage("SSH_MSG_SERVICE_ACCEPT");
							this.RegisterMessage("SSH_MSG_KEXINIT");
							this.RegisterMessage("SSH_MSG_NEWKEYS");
							this.RegisterMessage("SSH_MSG_USERAUTH_BANNER");
							this._messageListenerCompleted.Reset();
							ThreadAbstraction.ExecuteThread(new Action(this.MessageListener));
							this.WaitOnHandle(this._keyExchangeCompletedWaitHandle);
							if (this.SessionId == null)
							{
								this.Disconnect();
							}
							else
							{
								this.SendMessage(new ServiceRequestMessage(ServiceName.UserAuthentication));
								this.WaitOnHandle(this._serviceAccepted);
								if (string.IsNullOrEmpty(this.ConnectionInfo.Username))
								{
									throw new SshException("Username is not specified.");
								}
								this.RegisterMessage("SSH_MSG_GLOBAL_REQUEST");
								this.ConnectionInfo.Authenticate(this, this._serviceFactory);
								this._isAuthenticated = true;
								this.RegisterMessage("SSH_MSG_REQUEST_SUCCESS");
								this.RegisterMessage("SSH_MSG_REQUEST_FAILURE");
								this.RegisterMessage("SSH_MSG_CHANNEL_OPEN_CONFIRMATION");
								this.RegisterMessage("SSH_MSG_CHANNEL_OPEN_FAILURE");
								this.RegisterMessage("SSH_MSG_CHANNEL_WINDOW_ADJUST");
								this.RegisterMessage("SSH_MSG_CHANNEL_EXTENDED_DATA");
								this.RegisterMessage("SSH_MSG_CHANNEL_REQUEST");
								this.RegisterMessage("SSH_MSG_CHANNEL_SUCCESS");
								this.RegisterMessage("SSH_MSG_CHANNEL_FAILURE");
								this.RegisterMessage("SSH_MSG_CHANNEL_DATA");
								this.RegisterMessage("SSH_MSG_CHANNEL_EOF");
								this.RegisterMessage("SSH_MSG_CHANNEL_CLOSE");
								Monitor.Pulse(this);
							}
						}
					}
				}
			}
			finally
			{
				Session.AuthenticationConnection.Release();
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000094B0 File Offset: 0x000076B0
		public void Disconnect()
		{
			this.Disconnect(DisconnectReason.ByApplication, "Connection terminated by the client.");
			if (this._messageListenerCompleted != null)
			{
				this._messageListenerCompleted.WaitOne();
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000094D3 File Offset: 0x000076D3
		private void Disconnect(DisconnectReason reason, string message)
		{
			this._isDisconnecting = true;
			if (reason == DisconnectReason.ByApplication)
			{
				this.SendDisconnect(reason, message);
			}
			this.SocketDisconnectAndDispose();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000094EF File Offset: 0x000076EF
		void ISession.WaitOnHandle(WaitHandle waitHandle)
		{
			this.WaitOnHandle(waitHandle, this.ConnectionInfo.Timeout);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000094EF File Offset: 0x000076EF
		internal void WaitOnHandle(WaitHandle waitHandle)
		{
			this.WaitOnHandle(waitHandle, this.ConnectionInfo.Timeout);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009504 File Offset: 0x00007704
		internal void WaitOnHandle(WaitHandle waitHandle, TimeSpan timeout)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			int num = WaitHandle.WaitAny(new WaitHandle[]
			{
				this._exceptionWaitHandle,
				this._messageListenerCompleted,
				waitHandle
			}, timeout);
			if (num == 0)
			{
				throw this._exception;
			}
			if (num == 1)
			{
				throw new SshConnectionException("Client not connected.");
			}
			if (num != 258)
			{
				return;
			}
			if (!this._isDisconnecting)
			{
				throw new SshOperationTimeoutException("Session operation has timed out");
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009578 File Offset: 0x00007778
		internal void SendMessage(Message message)
		{
			if (this._socket == null || !this._socket.CanWrite())
			{
				throw new SshConnectionException("Client not connected.");
			}
			if (this._keyExchangeInProgress && !(message is IKeyExchangedAllowed))
			{
				this.WaitOnHandle(this._keyExchangeCompletedWaitHandle);
			}
			byte paddingMultiplier = (this._clientCipher == null) ? 8 : Math.Max(8, this._serverCipher.MinimumSize);
			byte[] array = message.GetPacket(paddingMultiplier, this._clientCompression);
			object socketLock = this._socketLock;
			lock (socketLock)
			{
				if (this._socket == null || !this._socket.Connected)
				{
					throw new SshConnectionException("Client not connected.");
				}
				byte[] array2 = null;
				int num = 4;
				if (this._clientMac != null)
				{
					this._outboundPacketSequence.Write(array, 0);
					array2 = this._clientMac.ComputeHash(array);
				}
				if (this._clientCipher != null)
				{
					array = this._clientCipher.Encrypt(array, num, array.Length - num);
					num = 0;
				}
				if (array.Length > 68536)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Packet is too big. Maximum packet size is {0} bytes.", new object[]
					{
						68536
					}));
				}
				int num2 = array.Length - num;
				if (array2 == null)
				{
					SocketAbstraction.Send(this._socket, array, num, num2);
				}
				else
				{
					byte[] array3 = new byte[num2 + this._clientMac.HashSize / 8];
					Buffer.BlockCopy(array, num, array3, 0, num2);
					Buffer.BlockCopy(array2, 0, array3, num2, array2.Length);
					SocketAbstraction.Send(this._socket, array3, 0, array3.Length);
				}
				this._outboundPacketSequence += 1U;
				Monitor.Pulse(this._socketLock);
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009744 File Offset: 0x00007944
		private bool TrySendMessage(Message message)
		{
			bool result;
			try
			{
				this.SendMessage(message);
				result = true;
			}
			catch (SshException)
			{
				result = false;
			}
			catch (SocketException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009784 File Offset: 0x00007984
		private Message ReceiveMessage()
		{
			byte b = (this._serverCipher == null) ? 8 : Math.Max(8, this._serverCipher.MinimumSize);
			byte[] array = this.Read((int)b);
			if (this._serverCipher != null)
			{
				array = this._serverCipher.Decrypt(array);
			}
			uint num = (uint)((int)array[0] << 24 | (int)array[1] << 16 | (int)array[2] << 8 | (int)array[3]);
			if ((ulong)num < (ulong)((long)(Math.Max(16, b) - 4)) || num > 68532U)
			{
				throw new SshConnectionException(string.Format(CultureInfo.CurrentCulture, "Bad packet length: {0}.", new object[]
				{
					num
				}), DisconnectReason.ProtocolError);
			}
			int num2 = (int)((ulong)num - (ulong)((long)(b - 4)));
			byte[] array2 = new byte[num2 + (int)b + 4];
			this._inboundPacketSequence.Write(array2, 0);
			Buffer.BlockCopy(array, 0, array2, 4, array.Length);
			byte[] array3 = null;
			if (this._serverMac != null)
			{
				array3 = new byte[this._serverMac.HashSize / 8];
				num2 += array3.Length;
			}
			if (num2 > 0)
			{
				byte[] array4 = this.Read(num2);
				if (array3 != null)
				{
					Buffer.BlockCopy(array4, array4.Length - array3.Length, array3, 0, array3.Length);
					array4 = array4.Take(array4.Length - array3.Length);
				}
				if (array4.Length != 0)
				{
					if (this._serverCipher != null)
					{
						array4 = this._serverCipher.Decrypt(array4);
					}
					array4.CopyTo(array2, (int)(b + 4));
				}
			}
			byte b2 = array2[8];
			int length = (int)(num - (uint)b2 - 1U);
			if (this._serverMac != null)
			{
				byte[] right = this._serverMac.ComputeHash(array2);
				if (!array3.IsEqualTo(right))
				{
					throw new SshConnectionException("MAC error", DisconnectReason.MacError);
				}
			}
			if (this._serverDecompression != null)
			{
				array2 = this._serverDecompression.Decompress(array2, 9, length);
			}
			this._inboundPacketSequence += 1U;
			return this.LoadMessage(array2, 9);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009948 File Offset: 0x00007B48
		private void SendDisconnect(DisconnectReason reasonCode, string message)
		{
			if (this._isDisconnectMessageSent || !this.IsConnected)
			{
				return;
			}
			DisconnectMessage message2 = new DisconnectMessage(reasonCode, message);
			this.TrySendMessage(message2);
			this._isDisconnectMessageSent = true;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009980 File Offset: 0x00007B80
		private void HandleMessageCore(Message message)
		{
			if (Session.<>o__161.<>p__0 == null)
			{
				Session.<>o__161.<>p__0 = CallSite<Action<CallSite, Session, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "HandleMessage", null, typeof(Session), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			Session.<>o__161.<>p__0.Target(Session.<>o__161.<>p__0, this, message);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000099E8 File Offset: 0x00007BE8
		private void HandleMessage<T>(T message) where T : Message
		{
			this.OnMessageReceived(message);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000099F6 File Offset: 0x00007BF6
		private void HandleMessage(DisconnectMessage message)
		{
			this.OnDisconnectReceived(message);
			this.Disconnect(message.ReasonCode, message.Description);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009A11 File Offset: 0x00007C11
		private void HandleMessage(IgnoreMessage message)
		{
			this.OnIgnoreReceived(message);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009A1A File Offset: 0x00007C1A
		private void HandleMessage(UnimplementedMessage message)
		{
			this.OnUnimplementedReceived(message);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009A23 File Offset: 0x00007C23
		private void HandleMessage(DebugMessage message)
		{
			this.OnDebugReceived(message);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009A2C File Offset: 0x00007C2C
		private void HandleMessage(ServiceRequestMessage message)
		{
			this.OnServiceRequestReceived(message);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009A35 File Offset: 0x00007C35
		private void HandleMessage(ServiceAcceptMessage message)
		{
			this.OnServiceAcceptReceived(message);
			this._serviceAccepted.Set();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00009A4A File Offset: 0x00007C4A
		private void HandleMessage(KeyExchangeInitMessage message)
		{
			this.OnKeyExchangeInitReceived(message);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009A53 File Offset: 0x00007C53
		private void HandleMessage(NewKeysMessage message)
		{
			this.OnNewKeysReceived(message);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009A5C File Offset: 0x00007C5C
		private void HandleMessage(RequestMessage message)
		{
			this.OnUserAuthenticationRequestReceived(message);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00009A65 File Offset: 0x00007C65
		private void HandleMessage(FailureMessage message)
		{
			this.OnUserAuthenticationFailureReceived(message);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009A6E File Offset: 0x00007C6E
		private void HandleMessage(SuccessMessage message)
		{
			this.OnUserAuthenticationSuccessReceived(message);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009A77 File Offset: 0x00007C77
		private void HandleMessage(BannerMessage message)
		{
			this.OnUserAuthenticationBannerReceived(message);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009A80 File Offset: 0x00007C80
		private void HandleMessage(GlobalRequestMessage message)
		{
			this.OnGlobalRequestReceived(message);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009A89 File Offset: 0x00007C89
		private void HandleMessage(RequestSuccessMessage message)
		{
			this.OnRequestSuccessReceived(message);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009A92 File Offset: 0x00007C92
		private void HandleMessage(RequestFailureMessage message)
		{
			this.OnRequestFailureReceived(message);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009A9B File Offset: 0x00007C9B
		private void HandleMessage(ChannelOpenMessage message)
		{
			this.OnChannelOpenReceived(message);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009AA4 File Offset: 0x00007CA4
		private void HandleMessage(ChannelOpenConfirmationMessage message)
		{
			this.OnChannelOpenConfirmationReceived(message);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009AAD File Offset: 0x00007CAD
		private void HandleMessage(ChannelOpenFailureMessage message)
		{
			this.OnChannelOpenFailureReceived(message);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009AB6 File Offset: 0x00007CB6
		private void HandleMessage(ChannelWindowAdjustMessage message)
		{
			this.OnChannelWindowAdjustReceived(message);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009ABF File Offset: 0x00007CBF
		private void HandleMessage(ChannelDataMessage message)
		{
			this.OnChannelDataReceived(message);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00009AC8 File Offset: 0x00007CC8
		private void HandleMessage(ChannelExtendedDataMessage message)
		{
			this.OnChannelExtendedDataReceived(message);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009AD1 File Offset: 0x00007CD1
		private void HandleMessage(ChannelEofMessage message)
		{
			this.OnChannelEofReceived(message);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00009ADA File Offset: 0x00007CDA
		private void HandleMessage(ChannelCloseMessage message)
		{
			this.OnChannelCloseReceived(message);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00009AE3 File Offset: 0x00007CE3
		private void HandleMessage(ChannelRequestMessage message)
		{
			this.OnChannelRequestReceived(message);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00009AEC File Offset: 0x00007CEC
		private void HandleMessage(ChannelSuccessMessage message)
		{
			this.OnChannelSuccessReceived(message);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009AF5 File Offset: 0x00007CF5
		private void HandleMessage(ChannelFailureMessage message)
		{
			this.OnChannelFailureReceived(message);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00009B00 File Offset: 0x00007D00
		protected virtual void OnDisconnectReceived(DisconnectMessage message)
		{
			this._exception = new SshConnectionException(string.Format(CultureInfo.InvariantCulture, "The connection was closed by the server: {0} ({1}).", new object[]
			{
				message.Description,
				message.ReasonCode
			}), message.ReasonCode);
			this._exceptionWaitHandle.Set();
			EventHandler<MessageEventArgs<DisconnectMessage>> disconnectReceived = this.DisconnectReceived;
			if (disconnectReceived != null)
			{
				disconnectReceived(this, new MessageEventArgs<DisconnectMessage>(message));
			}
			EventHandler<EventArgs> disconnected = this.Disconnected;
			if (disconnected != null)
			{
				disconnected(this, new EventArgs());
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009B84 File Offset: 0x00007D84
		protected virtual void OnIgnoreReceived(IgnoreMessage message)
		{
			EventHandler<MessageEventArgs<IgnoreMessage>> ignoreReceived = this.IgnoreReceived;
			if (ignoreReceived != null)
			{
				ignoreReceived(this, new MessageEventArgs<IgnoreMessage>(message));
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009BA8 File Offset: 0x00007DA8
		protected virtual void OnUnimplementedReceived(UnimplementedMessage message)
		{
			EventHandler<MessageEventArgs<UnimplementedMessage>> unimplementedReceived = this.UnimplementedReceived;
			if (unimplementedReceived != null)
			{
				unimplementedReceived(this, new MessageEventArgs<UnimplementedMessage>(message));
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009BCC File Offset: 0x00007DCC
		protected virtual void OnDebugReceived(DebugMessage message)
		{
			EventHandler<MessageEventArgs<DebugMessage>> debugReceived = this.DebugReceived;
			if (debugReceived != null)
			{
				debugReceived(this, new MessageEventArgs<DebugMessage>(message));
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009BF0 File Offset: 0x00007DF0
		protected virtual void OnServiceRequestReceived(ServiceRequestMessage message)
		{
			EventHandler<MessageEventArgs<ServiceRequestMessage>> serviceRequestReceived = this.ServiceRequestReceived;
			if (serviceRequestReceived != null)
			{
				serviceRequestReceived(this, new MessageEventArgs<ServiceRequestMessage>(message));
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009C14 File Offset: 0x00007E14
		protected virtual void OnServiceAcceptReceived(ServiceAcceptMessage message)
		{
			EventHandler<MessageEventArgs<ServiceAcceptMessage>> serviceAcceptReceived = this.ServiceAcceptReceived;
			if (serviceAcceptReceived != null)
			{
				serviceAcceptReceived(this, new MessageEventArgs<ServiceAcceptMessage>(message));
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009C38 File Offset: 0x00007E38
		protected virtual void OnKeyExchangeInitReceived(KeyExchangeInitMessage message)
		{
			this._keyExchangeInProgress = true;
			this._keyExchangeCompletedWaitHandle.Reset();
			this._sshMessageFactory.DisableNonKeyExchangeMessages();
			this._keyExchange = this._serviceFactory.CreateKeyExchange(this.ConnectionInfo.KeyExchangeAlgorithms, message.KeyExchangeAlgorithms);
			this.ConnectionInfo.CurrentKeyExchangeAlgorithm = this._keyExchange.Name;
			this._keyExchange.HostKeyReceived += this.KeyExchange_HostKeyReceived;
			this._keyExchange.Start(this, message);
			EventHandler<MessageEventArgs<KeyExchangeInitMessage>> keyExchangeInitReceived = this.KeyExchangeInitReceived;
			if (keyExchangeInitReceived != null)
			{
				keyExchangeInitReceived(this, new MessageEventArgs<KeyExchangeInitMessage>(message));
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009CD8 File Offset: 0x00007ED8
		protected virtual void OnNewKeysReceived(NewKeysMessage message)
		{
			if (this.SessionId == null)
			{
				this.SessionId = this._keyExchange.ExchangeHash;
			}
			if (this._serverMac != null)
			{
				this._serverMac.Dispose();
				this._serverMac = null;
			}
			if (this._clientMac != null)
			{
				this._clientMac.Dispose();
				this._clientMac = null;
			}
			this._serverCipher = this._keyExchange.CreateServerCipher();
			this._clientCipher = this._keyExchange.CreateClientCipher();
			this._serverMac = this._keyExchange.CreateServerHash();
			this._clientMac = this._keyExchange.CreateClientHash();
			this._clientCompression = this._keyExchange.CreateCompressor();
			this._serverDecompression = this._keyExchange.CreateDecompressor();
			if (this._keyExchange != null)
			{
				this._keyExchange.HostKeyReceived -= this.KeyExchange_HostKeyReceived;
				this._keyExchange.Dispose();
				this._keyExchange = null;
			}
			this._sshMessageFactory.EnableActivatedMessages();
			EventHandler<MessageEventArgs<NewKeysMessage>> newKeysReceived = this.NewKeysReceived;
			if (newKeysReceived != null)
			{
				newKeysReceived(this, new MessageEventArgs<NewKeysMessage>(message));
			}
			this._keyExchangeCompletedWaitHandle.Set();
			this._keyExchangeInProgress = false;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00009DFE File Offset: 0x00007FFE
		void ISession.OnDisconnecting()
		{
			this._isDisconnecting = true;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00009E08 File Offset: 0x00008008
		protected virtual void OnUserAuthenticationRequestReceived(RequestMessage message)
		{
			EventHandler<MessageEventArgs<RequestMessage>> userAuthenticationRequestReceived = this.UserAuthenticationRequestReceived;
			if (userAuthenticationRequestReceived != null)
			{
				userAuthenticationRequestReceived(this, new MessageEventArgs<RequestMessage>(message));
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00009E2C File Offset: 0x0000802C
		protected virtual void OnUserAuthenticationFailureReceived(FailureMessage message)
		{
			EventHandler<MessageEventArgs<FailureMessage>> userAuthenticationFailureReceived = this.UserAuthenticationFailureReceived;
			if (userAuthenticationFailureReceived != null)
			{
				userAuthenticationFailureReceived(this, new MessageEventArgs<FailureMessage>(message));
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00009E50 File Offset: 0x00008050
		protected virtual void OnUserAuthenticationSuccessReceived(SuccessMessage message)
		{
			EventHandler<MessageEventArgs<SuccessMessage>> userAuthenticationSuccessReceived = this.UserAuthenticationSuccessReceived;
			if (userAuthenticationSuccessReceived != null)
			{
				userAuthenticationSuccessReceived(this, new MessageEventArgs<SuccessMessage>(message));
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00009E74 File Offset: 0x00008074
		protected virtual void OnUserAuthenticationBannerReceived(BannerMessage message)
		{
			EventHandler<MessageEventArgs<BannerMessage>> userAuthenticationBannerReceived = this.UserAuthenticationBannerReceived;
			if (userAuthenticationBannerReceived != null)
			{
				userAuthenticationBannerReceived(this, new MessageEventArgs<BannerMessage>(message));
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00009E98 File Offset: 0x00008098
		protected virtual void OnGlobalRequestReceived(GlobalRequestMessage message)
		{
			EventHandler<MessageEventArgs<GlobalRequestMessage>> globalRequestReceived = this.GlobalRequestReceived;
			if (globalRequestReceived != null)
			{
				globalRequestReceived(this, new MessageEventArgs<GlobalRequestMessage>(message));
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009EBC File Offset: 0x000080BC
		protected virtual void OnRequestSuccessReceived(RequestSuccessMessage message)
		{
			EventHandler<MessageEventArgs<RequestSuccessMessage>> requestSuccessReceived = this.RequestSuccessReceived;
			if (requestSuccessReceived != null)
			{
				requestSuccessReceived(this, new MessageEventArgs<RequestSuccessMessage>(message));
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009EE0 File Offset: 0x000080E0
		protected virtual void OnRequestFailureReceived(RequestFailureMessage message)
		{
			EventHandler<MessageEventArgs<RequestFailureMessage>> requestFailureReceived = this.RequestFailureReceived;
			if (requestFailureReceived != null)
			{
				requestFailureReceived(this, new MessageEventArgs<RequestFailureMessage>(message));
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009F04 File Offset: 0x00008104
		protected virtual void OnChannelOpenReceived(ChannelOpenMessage message)
		{
			EventHandler<MessageEventArgs<ChannelOpenMessage>> channelOpenReceived = this.ChannelOpenReceived;
			if (channelOpenReceived != null)
			{
				channelOpenReceived(this, new MessageEventArgs<ChannelOpenMessage>(message));
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009F28 File Offset: 0x00008128
		protected virtual void OnChannelOpenConfirmationReceived(ChannelOpenConfirmationMessage message)
		{
			EventHandler<MessageEventArgs<ChannelOpenConfirmationMessage>> channelOpenConfirmationReceived = this.ChannelOpenConfirmationReceived;
			if (channelOpenConfirmationReceived != null)
			{
				channelOpenConfirmationReceived(this, new MessageEventArgs<ChannelOpenConfirmationMessage>(message));
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009F4C File Offset: 0x0000814C
		protected virtual void OnChannelOpenFailureReceived(ChannelOpenFailureMessage message)
		{
			EventHandler<MessageEventArgs<ChannelOpenFailureMessage>> channelOpenFailureReceived = this.ChannelOpenFailureReceived;
			if (channelOpenFailureReceived != null)
			{
				channelOpenFailureReceived(this, new MessageEventArgs<ChannelOpenFailureMessage>(message));
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00009F70 File Offset: 0x00008170
		protected virtual void OnChannelWindowAdjustReceived(ChannelWindowAdjustMessage message)
		{
			EventHandler<MessageEventArgs<ChannelWindowAdjustMessage>> channelWindowAdjustReceived = this.ChannelWindowAdjustReceived;
			if (channelWindowAdjustReceived != null)
			{
				channelWindowAdjustReceived(this, new MessageEventArgs<ChannelWindowAdjustMessage>(message));
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00009F94 File Offset: 0x00008194
		protected virtual void OnChannelDataReceived(ChannelDataMessage message)
		{
			EventHandler<MessageEventArgs<ChannelDataMessage>> channelDataReceived = this.ChannelDataReceived;
			if (channelDataReceived != null)
			{
				channelDataReceived(this, new MessageEventArgs<ChannelDataMessage>(message));
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00009FB8 File Offset: 0x000081B8
		protected virtual void OnChannelExtendedDataReceived(ChannelExtendedDataMessage message)
		{
			EventHandler<MessageEventArgs<ChannelExtendedDataMessage>> channelExtendedDataReceived = this.ChannelExtendedDataReceived;
			if (channelExtendedDataReceived != null)
			{
				channelExtendedDataReceived(this, new MessageEventArgs<ChannelExtendedDataMessage>(message));
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009FDC File Offset: 0x000081DC
		protected virtual void OnChannelEofReceived(ChannelEofMessage message)
		{
			EventHandler<MessageEventArgs<ChannelEofMessage>> channelEofReceived = this.ChannelEofReceived;
			if (channelEofReceived != null)
			{
				channelEofReceived(this, new MessageEventArgs<ChannelEofMessage>(message));
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A000 File Offset: 0x00008200
		protected virtual void OnChannelCloseReceived(ChannelCloseMessage message)
		{
			EventHandler<MessageEventArgs<ChannelCloseMessage>> channelCloseReceived = this.ChannelCloseReceived;
			if (channelCloseReceived != null)
			{
				channelCloseReceived(this, new MessageEventArgs<ChannelCloseMessage>(message));
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A024 File Offset: 0x00008224
		protected virtual void OnChannelRequestReceived(ChannelRequestMessage message)
		{
			EventHandler<MessageEventArgs<ChannelRequestMessage>> channelRequestReceived = this.ChannelRequestReceived;
			if (channelRequestReceived != null)
			{
				channelRequestReceived(this, new MessageEventArgs<ChannelRequestMessage>(message));
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A048 File Offset: 0x00008248
		protected virtual void OnChannelSuccessReceived(ChannelSuccessMessage message)
		{
			EventHandler<MessageEventArgs<ChannelSuccessMessage>> channelSuccessReceived = this.ChannelSuccessReceived;
			if (channelSuccessReceived != null)
			{
				channelSuccessReceived(this, new MessageEventArgs<ChannelSuccessMessage>(message));
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000A06C File Offset: 0x0000826C
		protected virtual void OnChannelFailureReceived(ChannelFailureMessage message)
		{
			EventHandler<MessageEventArgs<ChannelFailureMessage>> channelFailureReceived = this.ChannelFailureReceived;
			if (channelFailureReceived != null)
			{
				channelFailureReceived(this, new MessageEventArgs<ChannelFailureMessage>(message));
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A090 File Offset: 0x00008290
		protected virtual void OnMessageReceived(Message message)
		{
			EventHandler<MessageEventArgs<Message>> messageReceived = this.MessageReceived;
			if (messageReceived != null)
			{
				messageReceived(this, new MessageEventArgs<Message>(message));
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A0B4 File Offset: 0x000082B4
		private void KeyExchange_HostKeyReceived(object sender, HostKeyEventArgs e)
		{
			EventHandler<HostKeyEventArgs> hostKeyReceived = this.HostKeyReceived;
			if (hostKeyReceived != null)
			{
				hostKeyReceived(this, e);
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000A0D4 File Offset: 0x000082D4
		private byte[] Read(int length)
		{
			byte[] array = new byte[length];
			this.SocketRead(length, array);
			return array;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000A0F1 File Offset: 0x000082F1
		public void RegisterMessage(string messageName)
		{
			this._sshMessageFactory.EnableAndActivateMessage(messageName);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000A0FF File Offset: 0x000082FF
		public void UnRegisterMessage(string messageName)
		{
			this._sshMessageFactory.DisableAndDeactivateMessage(messageName);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000A110 File Offset: 0x00008310
		private Message LoadMessage(byte[] data, int offset)
		{
			byte messageNumber = data[offset];
			Message message = this._sshMessageFactory.Create(messageNumber);
			message.Load(data, offset);
			return message;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000A138 File Offset: 0x00008338
		private void IsSocketConnected(ref bool isConnected)
		{
			isConnected = (this._socket != null && this._socket.Connected);
			if (isConnected)
			{
				object socketReadLock = this._socketReadLock;
				lock (socketReadLock)
				{
					this._bytesReadFromSocket.Reset();
					bool flag2 = this._socket.Poll(1000, SelectMode.SelectRead);
					isConnected = (!flag2 || this._socket.Available != 0);
					if (!isConnected)
					{
						isConnected = this._bytesReadFromSocket.WaitOne(500);
					}
				}
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000A1D8 File Offset: 0x000083D8
		private void SocketConnect(string host, int port)
		{
			IPEndPoint remoteEndpoint = new IPEndPoint(DnsAbstraction.GetHostAddresses(host)[0], port);
			this._socket = SocketAbstraction.Connect(remoteEndpoint, this.ConnectionInfo.Timeout);
			this._socket.SendBufferSize = 137072;
			this._socket.ReceiveBufferSize = 137072;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A22C File Offset: 0x0000842C
		private void SocketRead(int length, byte[] buffer)
		{
			if (SocketAbstraction.Read(this._socket, buffer, 0, length, Session.InfiniteTimeSpan) > 0)
			{
				this._bytesReadFromSocket.Set();
				return;
			}
			if (this._isDisconnecting)
			{
				throw new SshConnectionException("An established connection was aborted by the software in your host machine.", DisconnectReason.ConnectionLost);
			}
			throw new SshConnectionException("An established connection was aborted by the server.", DisconnectReason.ConnectionLost);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A280 File Offset: 0x00008480
		private string SocketReadLine(TimeSpan timeout)
		{
			Encoding ascii = SshData.Ascii;
			List<byte> list = new List<byte>();
			byte[] array = new byte[1];
			while (SocketAbstraction.Read(this._socket, array, 0, array.Length, timeout) != 0)
			{
				list.Add(array[0]);
				if (list.Count > 0 && (list[list.Count - 1] == 10 || list[list.Count - 1] == 0))
				{
					break;
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count == 1 && list[list.Count - 1] == 0)
			{
				return string.Empty;
			}
			if (list.Count > 1 && list[list.Count - 2] == 13)
			{
				return ascii.GetString(list.ToArray(), 0, list.Count - 2);
			}
			if (list.Count > 1 && list[list.Count - 1] == 10)
			{
				return ascii.GetString(list.ToArray(), 0, list.Count - 1);
			}
			return ascii.GetString(list.ToArray(), 0, list.Count);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A388 File Offset: 0x00008588
		private void SocketDisconnectAndDispose()
		{
			if (this._socket != null)
			{
				object socketLock = this._socketLock;
				lock (socketLock)
				{
					if (this._socket != null)
					{
						if (this._socket.Connected)
						{
							this._socket.Shutdown(SocketShutdown.Send);
							SocketAbstraction.ClearReadBuffer(this._socket);
						}
						this._socket.Dispose();
						this._socket = null;
					}
				}
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A408 File Offset: 0x00008608
		private void MessageListener()
		{
			try
			{
				while (this._socket != null && this._socket.Connected)
				{
					Message message = this.ReceiveMessage();
					this.HandleMessageCore(message);
				}
			}
			catch (SocketException ex)
			{
				this.RaiseError(new SshConnectionException(ex.Message, DisconnectReason.ConnectionLost, ex));
			}
			catch (Exception exp)
			{
				this.RaiseError(exp);
			}
			finally
			{
				this._messageListenerCompleted.Set();
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A494 File Offset: 0x00008694
		private byte SocketReadByte()
		{
			byte[] array = new byte[1];
			this.SocketRead(1, array);
			return array[0];
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000A4B3 File Offset: 0x000086B3
		private void SocketWriteByte(byte data)
		{
			SocketAbstraction.Send(this._socket, new byte[]
			{
				data
			});
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A4CC File Offset: 0x000086CC
		private void ConnectSocks4()
		{
			this.SocketWriteByte(4);
			this.SocketWriteByte(1);
			this.SocketWriteByte((byte)(this.ConnectionInfo.Port / 255));
			this.SocketWriteByte((byte)(this.ConnectionInfo.Port % 255));
			IPAddress ipaddress = DnsAbstraction.GetHostAddresses(this.ConnectionInfo.Host)[0];
			SocketAbstraction.Send(this._socket, ipaddress.GetAddressBytes());
			byte[] bytes = SshData.Ascii.GetBytes(this.ConnectionInfo.ProxyUsername);
			SocketAbstraction.Send(this._socket, bytes);
			this.SocketWriteByte(0);
			if (this.SocketReadByte() != 0)
			{
				throw new ProxyException("SOCKS4: Null is expected.");
			}
			switch (this.SocketReadByte())
			{
			case 90:
			{
				byte[] buffer = new byte[4];
				this.SocketRead(2, buffer);
				this.SocketRead(4, buffer);
				return;
			}
			case 91:
				throw new ProxyException("SOCKS4: Connection rejected.");
			case 92:
				throw new ProxyException("SOCKS4: Client is not running identd or not reachable from the server.");
			case 93:
				throw new ProxyException("SOCKS4: Client's identd could not confirm the user ID string in the request.");
			default:
				throw new ProxyException("SOCKS4: Not valid response.");
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000A5DC File Offset: 0x000087DC
		private void ConnectSocks5()
		{
			this.SocketWriteByte(5);
			this.SocketWriteByte(2);
			this.SocketWriteByte(0);
			this.SocketWriteByte(2);
			byte b = this.SocketReadByte();
			if (b != 5)
			{
				throw new ProxyException(string.Format("SOCKS Version '{0}' is not supported.", b));
			}
			byte b2 = this.SocketReadByte();
			if (b2 != 0)
			{
				if (b2 != 2)
				{
					if (b2 == 255)
					{
						throw new ProxyException("SOCKS5: No acceptable authentication methods were offered.");
					}
				}
				else
				{
					this.SocketWriteByte(1);
					byte[] bytes = SshData.Ascii.GetBytes(this.ConnectionInfo.ProxyUsername);
					if (bytes.Length > 255)
					{
						throw new ProxyException("Proxy username is too long.");
					}
					this.SocketWriteByte((byte)bytes.Length);
					SocketAbstraction.Send(this._socket, bytes);
					byte[] bytes2 = SshData.Ascii.GetBytes(this.ConnectionInfo.ProxyPassword);
					if (bytes2.Length > 255)
					{
						throw new ProxyException("Proxy password is too long.");
					}
					this.SocketWriteByte((byte)bytes2.Length);
					SocketAbstraction.Send(this._socket, bytes2);
					if (this.SocketReadByte() != 1)
					{
						throw new ProxyException("SOCKS5: Server authentication version is not valid.");
					}
					if (this.SocketReadByte() != 0)
					{
						throw new ProxyException("SOCKS5: Username/Password authentication failed.");
					}
				}
			}
			this.SocketWriteByte(5);
			this.SocketWriteByte(1);
			this.SocketWriteByte(0);
			IPAddress ipaddress = DnsAbstraction.GetHostAddresses(this.ConnectionInfo.Host)[0];
			if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
			{
				this.SocketWriteByte(1);
				byte[] addressBytes = ipaddress.GetAddressBytes();
				SocketAbstraction.Send(this._socket, addressBytes);
			}
			else
			{
				if (ipaddress.AddressFamily != AddressFamily.InterNetworkV6)
				{
					throw new ProxyException(string.Format("SOCKS5: IP address '{0}' is not supported.", ipaddress));
				}
				this.SocketWriteByte(4);
				byte[] addressBytes2 = ipaddress.GetAddressBytes();
				SocketAbstraction.Send(this._socket, addressBytes2);
			}
			this.SocketWriteByte((byte)(this.ConnectionInfo.Port / 255));
			this.SocketWriteByte((byte)(this.ConnectionInfo.Port % 255));
			if (this.SocketReadByte() != 5)
			{
				throw new ProxyException("SOCKS5: Version 5 is expected.");
			}
			switch (this.SocketReadByte())
			{
			case 0:
			{
				if (this.SocketReadByte() != 0)
				{
					throw new ProxyException("SOCKS5: 0 byte is expected.");
				}
				byte b3 = this.SocketReadByte();
				byte[] buffer = new byte[16];
				if (b3 != 1)
				{
					if (b3 != 4)
					{
						throw new ProxyException(string.Format("Address type '{0}' is not supported.", b3));
					}
					this.SocketRead(16, buffer);
				}
				else
				{
					this.SocketRead(4, buffer);
				}
				byte[] buffer2 = new byte[2];
				this.SocketRead(2, buffer2);
				return;
			}
			case 1:
				throw new ProxyException("SOCKS5: General failure.");
			case 2:
				throw new ProxyException("SOCKS5: Connection not allowed by ruleset.");
			case 3:
				throw new ProxyException("SOCKS5: Network unreachable.");
			case 4:
				throw new ProxyException("SOCKS5: Host unreachable.");
			case 5:
				throw new ProxyException("SOCKS5: Connection refused by destination host.");
			case 6:
				throw new ProxyException("SOCKS5: TTL expired.");
			case 7:
				throw new ProxyException("SOCKS5: Command not supported or protocol error.");
			case 8:
				throw new ProxyException("SOCKS5: Address type not supported.");
			default:
				throw new ProxyException("SOCKS4: Not valid response.");
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000A8DC File Offset: 0x00008ADC
		private void ConnectHttp()
		{
			Regex regex = new Regex("HTTP/(?<version>\\d[.]\\d) (?<statusCode>\\d{3}) (?<reasonPhrase>.+)$");
			Regex regex2 = new Regex("(?<fieldName>[^\\[\\]()<>@,;:\\\"/?={} \\t]+):(?<fieldValue>.+)?");
			SocketAbstraction.Send(this._socket, SshData.Ascii.GetBytes(string.Format("CONNECT {0}:{1} HTTP/1.0\r\n", this.ConnectionInfo.Host, this.ConnectionInfo.Port)));
			if (!string.IsNullOrEmpty(this.ConnectionInfo.ProxyUsername))
			{
				string s = string.Format("Proxy-Authorization: Basic {0}\r\n", Convert.ToBase64String(SshData.Ascii.GetBytes(string.Format("{0}:{1}", this.ConnectionInfo.ProxyUsername, this.ConnectionInfo.ProxyPassword))));
				SocketAbstraction.Send(this._socket, SshData.Ascii.GetBytes(s));
			}
			SocketAbstraction.Send(this._socket, SshData.Ascii.GetBytes("\r\n"));
			HttpStatusCode? httpStatusCode = null;
			int num = 0;
			Match match;
			string text2;
			for (;;)
			{
				string text = this.SocketReadLine(this.ConnectionInfo.Timeout);
				if (text == null)
				{
					goto IL_1D7;
				}
				if (httpStatusCode == null)
				{
					match = regex.Match(text);
					if (match.Success)
					{
						text2 = match.Result("${statusCode}");
						httpStatusCode = new HttpStatusCode?((HttpStatusCode)int.Parse(text2));
						if (httpStatusCode != HttpStatusCode.OK)
						{
							break;
						}
					}
				}
				else
				{
					Match match2 = regex2.Match(text);
					if (match2.Success)
					{
						if (match2.Result("${fieldName}").Equals("Content-Length", StringComparison.OrdinalIgnoreCase))
						{
							num = int.Parse(match2.Result("${fieldValue}"));
						}
					}
					else if (text.Length == 0)
					{
						goto Block_9;
					}
				}
			}
			string arg = match.Result("${reasonPhrase}");
			throw new ProxyException(string.Format("HTTP: Status code {0}, \"{1}\"", text2, arg));
			Block_9:
			if (num > 0)
			{
				byte[] buffer = new byte[num];
				this.SocketRead(num, buffer);
			}
			IL_1D7:
			if (httpStatusCode == null)
			{
				throw new ProxyException("HTTP response does not contain status line.");
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		private void RaiseError(Exception exp)
		{
			SshConnectionException ex = exp as SshConnectionException;
			if (this._isDisconnecting)
			{
				if (ex != null)
				{
					return;
				}
				SocketException ex2 = exp as SocketException;
				if (ex2 != null && ex2.SocketErrorCode == SocketError.TimedOut)
				{
					return;
				}
			}
			this._exception = exp;
			this._exceptionWaitHandle.Set();
			EventHandler<ExceptionEventArgs> errorOccured = this.ErrorOccured;
			if (errorOccured != null)
			{
				errorOccured(this, new ExceptionEventArgs(exp));
			}
			if (ex != null)
			{
				this.Disconnect(ex.DisconnectReason, exp.ToString());
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000AB4C File Offset: 0x00008D4C
		private void Reset()
		{
			if (this._exceptionWaitHandle != null)
			{
				this._exceptionWaitHandle.Reset();
			}
			if (this._keyExchangeCompletedWaitHandle != null)
			{
				this._keyExchangeCompletedWaitHandle.Reset();
			}
			if (this._messageListenerCompleted != null)
			{
				this._messageListenerCompleted.Set();
			}
			this.SessionId = null;
			this._isDisconnectMessageSent = false;
			this._isDisconnecting = false;
			this._isAuthenticated = false;
			this._exception = null;
			this._keyExchangeInProgress = false;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000ABBF File Offset: 0x00008DBF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		protected virtual void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this.Disconnect();
				EventWaitHandle serviceAccepted = this._serviceAccepted;
				if (serviceAccepted != null)
				{
					serviceAccepted.Dispose();
					this._serviceAccepted = null;
				}
				EventWaitHandle exceptionWaitHandle = this._exceptionWaitHandle;
				if (exceptionWaitHandle != null)
				{
					exceptionWaitHandle.Dispose();
					this._exceptionWaitHandle = null;
				}
				EventWaitHandle keyExchangeCompletedWaitHandle = this._keyExchangeCompletedWaitHandle;
				if (keyExchangeCompletedWaitHandle != null)
				{
					keyExchangeCompletedWaitHandle.Dispose();
					this._keyExchangeCompletedWaitHandle = null;
				}
				HashAlgorithm serverMac = this._serverMac;
				if (serverMac != null)
				{
					serverMac.Dispose();
					this._serverMac = null;
				}
				HashAlgorithm clientMac = this._clientMac;
				if (clientMac != null)
				{
					clientMac.Dispose();
					this._clientMac = null;
				}
				IKeyExchange keyExchange = this._keyExchange;
				if (keyExchange != null)
				{
					keyExchange.HostKeyReceived -= this.KeyExchange_HostKeyReceived;
					keyExchange.Dispose();
					this._keyExchange = null;
				}
				EventWaitHandle bytesReadFromSocket = this._bytesReadFromSocket;
				if (bytesReadFromSocket != null)
				{
					bytesReadFromSocket.Dispose();
					this._bytesReadFromSocket = null;
				}
				EventWaitHandle messageListenerCompleted = this._messageListenerCompleted;
				if (messageListenerCompleted != null)
				{
					messageListenerCompleted.Dispose();
					this._messageListenerCompleted = null;
				}
				this._disposed = true;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		~Session()
		{
			this.Dispose(false);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000AD00 File Offset: 0x00008F00
		IConnectionInfo ISession.ConnectionInfo
		{
			get
			{
				return this.ConnectionInfo;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000AD08 File Offset: 0x00008F08
		WaitHandle ISession.MessageListenerCompleted
		{
			get
			{
				return this._messageListenerCompleted;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000AD10 File Offset: 0x00008F10
		IChannelSession ISession.CreateChannelSession()
		{
			return new ChannelSession(this, this.NextChannelNumber, 2097152U, 65536U);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000AD28 File Offset: 0x00008F28
		IChannelDirectTcpip ISession.CreateChannelDirectTcpip()
		{
			return new ChannelDirectTcpip(this, this.NextChannelNumber, 2097152U, 65536U);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000AD40 File Offset: 0x00008F40
		IChannelForwardedTcpip ISession.CreateChannelForwardedTcpip(uint remoteChannelNumber, uint remoteWindowSize, uint remoteChannelDataPacketSize)
		{
			return new ChannelForwardedTcpip(this, this.NextChannelNumber, 2097152U, 65536U, remoteChannelNumber, remoteWindowSize, remoteChannelDataPacketSize);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000AD5B File Offset: 0x00008F5B
		void ISession.SendMessage(Message message)
		{
			this.SendMessage(message);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000AD64 File Offset: 0x00008F64
		bool ISession.TrySendMessage(Message message)
		{
			return this.TrySendMessage(message);
		}

		// Token: 0x04000095 RID: 149
		private const byte Null = 0;

		// Token: 0x04000096 RID: 150
		private const byte CarriageReturn = 13;

		// Token: 0x04000097 RID: 151
		private const byte LineFeed = 10;

		// Token: 0x04000098 RID: 152
		internal static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);

		// Token: 0x04000099 RID: 153
		internal static readonly int Infinite = -1;

		// Token: 0x0400009A RID: 154
		private const int MaximumSshPacketSize = 68536;

		// Token: 0x0400009B RID: 155
		private const int InitialLocalWindowSize = 2097152;

		// Token: 0x0400009C RID: 156
		private const int LocalChannelDataPacketSize = 65536;

		// Token: 0x0400009D RID: 157
		private static readonly Regex ServerVersionRe = new Regex("^SSH-(?<protoversion>[^-]+)-(?<softwareversion>.+)( SP.+)?$", RegexOptions.Compiled);

		// Token: 0x0400009E RID: 158
		private static readonly SemaphoreLight AuthenticationConnection = new SemaphoreLight(3);

		// Token: 0x0400009F RID: 159
		private SshMessageFactory _sshMessageFactory;

		// Token: 0x040000A0 RID: 160
		private Socket _socket;

		// Token: 0x040000A1 RID: 161
		private readonly object _socketLock = new object();

		// Token: 0x040000A2 RID: 162
		private EventWaitHandle _messageListenerCompleted;

		// Token: 0x040000A3 RID: 163
		private volatile uint _outboundPacketSequence;

		// Token: 0x040000A4 RID: 164
		private uint _inboundPacketSequence;

		// Token: 0x040000A5 RID: 165
		private EventWaitHandle _serviceAccepted = new AutoResetEvent(false);

		// Token: 0x040000A6 RID: 166
		private EventWaitHandle _exceptionWaitHandle = new ManualResetEvent(false);

		// Token: 0x040000A7 RID: 167
		private EventWaitHandle _keyExchangeCompletedWaitHandle = new ManualResetEvent(false);

		// Token: 0x040000A8 RID: 168
		private EventWaitHandle _bytesReadFromSocket = new ManualResetEvent(false);

		// Token: 0x040000A9 RID: 169
		private bool _keyExchangeInProgress;

		// Token: 0x040000AA RID: 170
		private Exception _exception;

		// Token: 0x040000AB RID: 171
		private bool _isAuthenticated;

		// Token: 0x040000AC RID: 172
		private bool _isDisconnecting;

		// Token: 0x040000AD RID: 173
		private IKeyExchange _keyExchange;

		// Token: 0x040000AE RID: 174
		private HashAlgorithm _serverMac;

		// Token: 0x040000AF RID: 175
		private HashAlgorithm _clientMac;

		// Token: 0x040000B0 RID: 176
		private Cipher _clientCipher;

		// Token: 0x040000B1 RID: 177
		private Cipher _serverCipher;

		// Token: 0x040000B2 RID: 178
		private Compressor _serverDecompression;

		// Token: 0x040000B3 RID: 179
		private Compressor _clientCompression;

		// Token: 0x040000B4 RID: 180
		private SemaphoreLight _sessionSemaphore;

		// Token: 0x040000B5 RID: 181
		private readonly IServiceFactory _serviceFactory;

		// Token: 0x040000B6 RID: 182
		private bool _isDisconnectMessageSent;

		// Token: 0x040000B7 RID: 183
		private uint _nextChannelNumber;

		// Token: 0x040000B9 RID: 185
		private Message _clientInitMessage;

		// Token: 0x040000DB RID: 219
		private bool _disposed;

		// Token: 0x040000DC RID: 220
		private readonly object _socketReadLock = new object();
	}
}
