using System;
using System.Globalization;
using System.Net;
using System.Threading;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet
{
	// Token: 0x02000025 RID: 37
	public class ForwardedPortRemote : ForwardedPort, IDisposable
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000071FB File Offset: 0x000053FB
		public override bool IsStarted
		{
			get
			{
				return this._status == ForwardedPortStatus.Started;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000720D File Offset: 0x0000540D
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00007215 File Offset: 0x00005415
		public IPAddress BoundHostAddress { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000721E File Offset: 0x0000541E
		public string BoundHost
		{
			get
			{
				return this.BoundHostAddress.ToString();
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000722B File Offset: 0x0000542B
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00007233 File Offset: 0x00005433
		public uint BoundPort { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000723C File Offset: 0x0000543C
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00007244 File Offset: 0x00005444
		public IPAddress HostAddress { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000724D File Offset: 0x0000544D
		public string Host
		{
			get
			{
				return this.HostAddress.ToString();
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000725A File Offset: 0x0000545A
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00007262 File Offset: 0x00005462
		public uint Port { get; private set; }

		// Token: 0x060001EC RID: 492 RVA: 0x0000726C File Offset: 0x0000546C
		public ForwardedPortRemote(IPAddress boundHostAddress, uint boundPort, IPAddress hostAddress, uint port)
		{
			if (boundHostAddress == null)
			{
				throw new ArgumentNullException("boundHostAddress");
			}
			if (hostAddress == null)
			{
				throw new ArgumentNullException("hostAddress");
			}
			boundPort.ValidatePort("boundPort");
			port.ValidatePort("port");
			this.BoundHostAddress = boundHostAddress;
			this.BoundPort = boundPort;
			this.HostAddress = hostAddress;
			this.Port = port;
			this._status = ForwardedPortStatus.Stopped;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000072E6 File Offset: 0x000054E6
		public ForwardedPortRemote(uint boundPort, string host, uint port) : this(string.Empty, boundPort, host, port)
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000072F6 File Offset: 0x000054F6
		public ForwardedPortRemote(string boundHost, uint boundPort, string host, uint port) : this(DnsAbstraction.GetHostAddresses(boundHost)[0], boundPort, DnsAbstraction.GetHostAddresses(host)[0], port)
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00007314 File Offset: 0x00005514
		protected override void StartPort()
		{
			if (!ForwardedPortStatus.ToStarting(ref this._status))
			{
				return;
			}
			this.InitializePendingChannelCountdown();
			try
			{
				base.Session.RegisterMessage("SSH_MSG_REQUEST_FAILURE");
				base.Session.RegisterMessage("SSH_MSG_REQUEST_SUCCESS");
				base.Session.RegisterMessage("SSH_MSG_CHANNEL_OPEN");
				base.Session.RequestSuccessReceived += this.Session_RequestSuccess;
				base.Session.RequestFailureReceived += new EventHandler<MessageEventArgs<RequestFailureMessage>>(this.Session_RequestFailure);
				base.Session.ChannelOpenReceived += this.Session_ChannelOpening;
				base.Session.SendMessage(new GlobalRequestMessage(GlobalRequestName.TcpIpForward, true, this.BoundHost, this.BoundPort));
				base.Session.WaitOnHandle(this._globalRequestResponse);
				if (!this._requestStatus)
				{
					throw new SshException(string.Format(CultureInfo.CurrentCulture, "Port forwarding for '{0}' port '{1}' failed to start.", new object[]
					{
						this.Host,
						this.Port
					}));
				}
			}
			catch (Exception)
			{
				this._status = ForwardedPortStatus.Stopped;
				base.Session.RequestSuccessReceived -= this.Session_RequestSuccess;
				base.Session.RequestFailureReceived -= new EventHandler<MessageEventArgs<RequestFailureMessage>>(this.Session_RequestFailure);
				base.Session.ChannelOpenReceived -= this.Session_ChannelOpening;
				throw;
			}
			this._status = ForwardedPortStatus.Started;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007484 File Offset: 0x00005684
		protected override void StopPort(TimeSpan timeout)
		{
			if (!ForwardedPortStatus.ToStopping(ref this._status))
			{
				return;
			}
			base.StopPort(timeout);
			base.Session.SendMessage(new GlobalRequestMessage(GlobalRequestName.CancelTcpIpForward, true, this.BoundHost, this.BoundPort));
			WaitHandle.WaitAny(new WaitHandle[]
			{
				this._globalRequestResponse,
				base.Session.MessageListenerCompleted
			}, timeout);
			base.Session.RequestSuccessReceived -= this.Session_RequestSuccess;
			base.Session.RequestFailureReceived -= new EventHandler<MessageEventArgs<RequestFailureMessage>>(this.Session_RequestFailure);
			base.Session.ChannelOpenReceived -= this.Session_ChannelOpening;
			this._pendingChannelCountdown.Signal();
			this._pendingChannelCountdown.Wait(timeout);
			this._status = ForwardedPortStatus.Stopped;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007551 File Offset: 0x00005751
		protected override void CheckDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000756C File Offset: 0x0000576C
		private void Session_ChannelOpening(object sender, MessageEventArgs<ChannelOpenMessage> e)
		{
			ChannelOpenMessage channelOpenMessage = e.Message;
			ForwardedTcpipChannelInfo info = channelOpenMessage.Info as ForwardedTcpipChannelInfo;
			if (info != null && info.ConnectedAddress == this.BoundHost && info.ConnectedPort == this.BoundPort)
			{
				if (!this.IsStarted)
				{
					base.Session.SendMessage(new ChannelOpenFailureMessage(channelOpenMessage.LocalChannelNumber, "", 1U));
					return;
				}
				ThreadAbstraction.ExecuteThread(delegate
				{
					CountdownEvent pendingChannelCountdown = this._pendingChannelCountdown;
					pendingChannelCountdown.AddCount();
					try
					{
						this.RaiseRequestReceived(info.OriginatorAddress, info.OriginatorPort);
						using (IChannelForwardedTcpip channelForwardedTcpip = this.Session.CreateChannelForwardedTcpip(channelOpenMessage.LocalChannelNumber, channelOpenMessage.InitialWindowSize, channelOpenMessage.MaximumPacketSize))
						{
							channelForwardedTcpip.Exception += this.Channel_Exception;
							channelForwardedTcpip.Bind(new IPEndPoint(this.HostAddress, (int)this.Port), this);
							channelForwardedTcpip.Close();
						}
					}
					catch (Exception exception)
					{
						this.RaiseExceptionEvent(exception);
					}
					finally
					{
						try
						{
							pendingChannelCountdown.Signal();
						}
						catch (ObjectDisposedException)
						{
						}
					}
				});
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007618 File Offset: 0x00005818
		private void InitializePendingChannelCountdown()
		{
			CountdownEvent countdownEvent = Interlocked.Exchange<CountdownEvent>(ref this._pendingChannelCountdown, new CountdownEvent(1));
			if (countdownEvent != null)
			{
				countdownEvent.Dispose();
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00004F48 File Offset: 0x00003148
		private void Channel_Exception(object sender, ExceptionEventArgs exceptionEventArgs)
		{
			base.RaiseExceptionEvent(exceptionEventArgs.Exception);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007640 File Offset: 0x00005840
		private void Session_RequestFailure(object sender, EventArgs e)
		{
			this._requestStatus = false;
			this._globalRequestResponse.Set();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007658 File Offset: 0x00005858
		private void Session_RequestSuccess(object sender, MessageEventArgs<RequestSuccessMessage> e)
		{
			this._requestStatus = true;
			if (this.BoundPort == 0U)
			{
				this.BoundPort = ((e.Message.BoundPort == null) ? 0U : e.Message.BoundPort.Value);
			}
			this._globalRequestResponse.Set();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00004B6A File Offset: 0x00002D6A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000076B4 File Offset: 0x000058B4
		protected override void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			base.Dispose(disposing);
			if (disposing)
			{
				ISession session = base.Session;
				if (session != null)
				{
					base.Session = null;
					session.RequestSuccessReceived -= this.Session_RequestSuccess;
					session.RequestFailureReceived -= new EventHandler<MessageEventArgs<RequestFailureMessage>>(this.Session_RequestFailure);
					session.ChannelOpenReceived -= this.Session_ChannelOpening;
				}
				EventWaitHandle globalRequestResponse = this._globalRequestResponse;
				if (globalRequestResponse != null)
				{
					this._globalRequestResponse = null;
					globalRequestResponse.Dispose();
				}
				CountdownEvent pendingChannelCountdown = this._pendingChannelCountdown;
				if (pendingChannelCountdown != null)
				{
					this._pendingChannelCountdown = null;
					pendingChannelCountdown.Dispose();
				}
			}
			this._isDisposed = true;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007750 File Offset: 0x00005950
		~ForwardedPortRemote()
		{
			this.Dispose(false);
		}

		// Token: 0x04000084 RID: 132
		private ForwardedPortStatus _status;

		// Token: 0x04000085 RID: 133
		private bool _requestStatus;

		// Token: 0x04000086 RID: 134
		private EventWaitHandle _globalRequestResponse = new AutoResetEvent(false);

		// Token: 0x04000087 RID: 135
		private CountdownEvent _pendingChannelCountdown;

		// Token: 0x0400008C RID: 140
		private bool _isDisposed;
	}
}
