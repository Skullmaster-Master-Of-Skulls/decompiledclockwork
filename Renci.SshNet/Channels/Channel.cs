using System;
using System.Globalization;
using System.Threading;
using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet.Channels
{
	// Token: 0x0200010D RID: 269
	internal abstract class Channel : IChannel, IDisposable
	{
		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06000B6F RID: 2927 RVA: 0x000254C0 File Offset: 0x000236C0
		// (remove) Token: 0x06000B70 RID: 2928 RVA: 0x000254F8 File Offset: 0x000236F8
		public event EventHandler<ExceptionEventArgs> Exception;

		// Token: 0x06000B71 RID: 2929 RVA: 0x00025530 File Offset: 0x00023730
		protected Channel(ISession session, uint localChannelNumber, uint localWindowSize, uint localPacketSize)
		{
			this._session = session;
			this._initialWindowSize = localWindowSize;
			this.LocalChannelNumber = localChannelNumber;
			this.LocalPacketSize = localPacketSize;
			this.LocalWindowSize = localWindowSize;
			this._session.ChannelWindowAdjustReceived += this.OnChannelWindowAdjust;
			this._session.ChannelDataReceived += this.OnChannelData;
			this._session.ChannelExtendedDataReceived += this.OnChannelExtendedData;
			this._session.ChannelEofReceived += this.OnChannelEof;
			this._session.ChannelCloseReceived += this.OnChannelClose;
			this._session.ChannelRequestReceived += this.OnChannelRequest;
			this._session.ChannelSuccessReceived += this.OnChannelSuccess;
			this._session.ChannelFailureReceived += this.OnChannelFailure;
			this._session.ErrorOccured += this.Session_ErrorOccured;
			this._session.Disconnected += this.Session_Disconnected;
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0002567C File Offset: 0x0002387C
		protected ISession Session
		{
			get
			{
				return this._session;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B73 RID: 2931
		public abstract ChannelTypes ChannelType { get; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00025684 File Offset: 0x00023884
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x0002568C File Offset: 0x0002388C
		public uint LocalChannelNumber { get; private set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00025695 File Offset: 0x00023895
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x0002569D File Offset: 0x0002389D
		public uint LocalPacketSize { get; private set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x000256A6 File Offset: 0x000238A6
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x000256AE File Offset: 0x000238AE
		public uint LocalWindowSize { get; private set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x000256B7 File Offset: 0x000238B7
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x000256D7 File Offset: 0x000238D7
		public uint RemoteChannelNumber
		{
			get
			{
				if (this._remoteChannelNumber == null)
				{
					throw Channel.CreateRemoteChannelInfoNotAvailableException();
				}
				return this._remoteChannelNumber.Value;
			}
			private set
			{
				this._remoteChannelNumber = new uint?(value);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x000256E5 File Offset: 0x000238E5
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x00025705 File Offset: 0x00023905
		public uint RemotePacketSize
		{
			get
			{
				if (this._remotePacketSize == null)
				{
					throw Channel.CreateRemoteChannelInfoNotAvailableException();
				}
				return this._remotePacketSize.Value;
			}
			private set
			{
				this._remotePacketSize = new uint?(value);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00025713 File Offset: 0x00023913
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x00025733 File Offset: 0x00023933
		public uint RemoteWindowSize
		{
			get
			{
				if (this._remoteWindowSize == null)
				{
					throw Channel.CreateRemoteChannelInfoNotAvailableException();
				}
				return this._remoteWindowSize.Value;
			}
			private set
			{
				this._remoteWindowSize = new uint?(value);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x00025741 File Offset: 0x00023941
		// (set) Token: 0x06000B81 RID: 2945 RVA: 0x00025749 File Offset: 0x00023949
		public bool IsOpen { get; protected set; }

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06000B82 RID: 2946 RVA: 0x00025754 File Offset: 0x00023954
		// (remove) Token: 0x06000B83 RID: 2947 RVA: 0x0002578C File Offset: 0x0002398C
		public event EventHandler<ChannelDataEventArgs> DataReceived;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06000B84 RID: 2948 RVA: 0x000257C4 File Offset: 0x000239C4
		// (remove) Token: 0x06000B85 RID: 2949 RVA: 0x000257FC File Offset: 0x000239FC
		public event EventHandler<ChannelExtendedDataEventArgs> ExtendedDataReceived;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06000B86 RID: 2950 RVA: 0x00025834 File Offset: 0x00023A34
		// (remove) Token: 0x06000B87 RID: 2951 RVA: 0x0002586C File Offset: 0x00023A6C
		public event EventHandler<ChannelEventArgs> EndOfData;

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06000B88 RID: 2952 RVA: 0x000258A4 File Offset: 0x00023AA4
		// (remove) Token: 0x06000B89 RID: 2953 RVA: 0x000258DC File Offset: 0x00023ADC
		public event EventHandler<ChannelEventArgs> Closed;

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06000B8A RID: 2954 RVA: 0x00025914 File Offset: 0x00023B14
		// (remove) Token: 0x06000B8B RID: 2955 RVA: 0x0002594C File Offset: 0x00023B4C
		public event EventHandler<ChannelRequestEventArgs> RequestReceived;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06000B8C RID: 2956 RVA: 0x00025984 File Offset: 0x00023B84
		// (remove) Token: 0x06000B8D RID: 2957 RVA: 0x000259BC File Offset: 0x00023BBC
		public event EventHandler<ChannelEventArgs> RequestSucceeded;

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06000B8E RID: 2958 RVA: 0x000259F4 File Offset: 0x00023BF4
		// (remove) Token: 0x06000B8F RID: 2959 RVA: 0x00025A2C File Offset: 0x00023C2C
		public event EventHandler<ChannelEventArgs> RequestFailed;

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00025A61 File Offset: 0x00023C61
		protected bool IsConnected
		{
			get
			{
				return this._session.IsConnected;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00025A6E File Offset: 0x00023C6E
		protected IConnectionInfo ConnectionInfo
		{
			get
			{
				return this._session.ConnectionInfo;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00025A7B File Offset: 0x00023C7B
		protected SemaphoreLight SessionSemaphore
		{
			get
			{
				return this._session.SessionSemaphore;
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00025A88 File Offset: 0x00023C88
		protected void InitializeRemoteInfo(uint remoteChannelNumber, uint remoteWindowSize, uint remotePacketSize)
		{
			this.RemoteChannelNumber = remoteChannelNumber;
			this.RemoteWindowSize = remoteWindowSize;
			this.RemotePacketSize = remotePacketSize;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00025A9F File Offset: 0x00023C9F
		public void SendData(byte[] data)
		{
			this.SendData(data, 0, data.Length);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00025AAC File Offset: 0x00023CAC
		public void SendData(byte[] data, int offset, int size)
		{
			if (!this.IsOpen)
			{
				return;
			}
			int i = size;
			while (i > 0)
			{
				int dataLengthThatCanBeSentInMessage = this.GetDataLengthThatCanBeSentInMessage(i);
				ChannelDataMessage message = new ChannelDataMessage(this.RemoteChannelNumber, data, offset, dataLengthThatCanBeSentInMessage);
				this._session.SendMessage(message);
				i -= dataLengthThatCanBeSentInMessage;
				offset += dataLengthThatCanBeSentInMessage;
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00025AF6 File Offset: 0x00023CF6
		public void Close()
		{
			this.Close(true);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00025B00 File Offset: 0x00023D00
		protected virtual void OnWindowAdjust(uint bytesToAdd)
		{
			object serverWindowSizeLock = this._serverWindowSizeLock;
			lock (serverWindowSizeLock)
			{
				this.RemoteWindowSize += bytesToAdd;
			}
			this._channelServerWindowAdjustWaitHandle.Set();
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00025B54 File Offset: 0x00023D54
		protected virtual void OnData(byte[] data)
		{
			this.AdjustDataWindow(data);
			EventHandler<ChannelDataEventArgs> dataReceived = this.DataReceived;
			if (dataReceived != null)
			{
				dataReceived(this, new ChannelDataEventArgs(this.LocalChannelNumber, data));
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00025B88 File Offset: 0x00023D88
		protected virtual void OnExtendedData(byte[] data, uint dataTypeCode)
		{
			this.AdjustDataWindow(data);
			EventHandler<ChannelExtendedDataEventArgs> extendedDataReceived = this.ExtendedDataReceived;
			if (extendedDataReceived != null)
			{
				extendedDataReceived(this, new ChannelExtendedDataEventArgs(this.LocalChannelNumber, data, dataTypeCode));
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00025BBC File Offset: 0x00023DBC
		protected virtual void OnEof()
		{
			this._eofMessageReceived = true;
			EventHandler<ChannelEventArgs> endOfData = this.EndOfData;
			if (endOfData != null)
			{
				endOfData(this, new ChannelEventArgs(this.LocalChannelNumber));
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00025BEC File Offset: 0x00023DEC
		protected virtual void OnClose()
		{
			this._closeMessageReceived = true;
			this.Close(false);
			EventHandler<ChannelEventArgs> closed = this.Closed;
			if (closed != null)
			{
				closed(this, new ChannelEventArgs(this.LocalChannelNumber));
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00025C24 File Offset: 0x00023E24
		protected virtual void OnRequest(RequestInfo info)
		{
			EventHandler<ChannelRequestEventArgs> requestReceived = this.RequestReceived;
			if (requestReceived != null)
			{
				requestReceived(this, new ChannelRequestEventArgs(info));
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00025C48 File Offset: 0x00023E48
		protected virtual void OnSuccess()
		{
			EventHandler<ChannelEventArgs> requestSucceeded = this.RequestSucceeded;
			if (requestSucceeded != null)
			{
				requestSucceeded(this, new ChannelEventArgs(this.LocalChannelNumber));
			}
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00025C74 File Offset: 0x00023E74
		protected virtual void OnFailure()
		{
			EventHandler<ChannelEventArgs> requestFailed = this.RequestFailed;
			if (requestFailed != null)
			{
				requestFailed(this, new ChannelEventArgs(this.LocalChannelNumber));
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00025CA0 File Offset: 0x00023EA0
		protected void RaiseExceptionEvent(Exception exception)
		{
			EventHandler<ExceptionEventArgs> exception2 = this.Exception;
			if (exception2 != null)
			{
				exception2(this, new ExceptionEventArgs(exception));
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00025CC4 File Offset: 0x00023EC4
		private bool TrySendMessage(Message message)
		{
			return this._session.TrySendMessage(message);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00025CD2 File Offset: 0x00023ED2
		protected void SendMessage(Message message)
		{
			if (!this.IsOpen)
			{
				return;
			}
			this._session.SendMessage(message);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00025CE9 File Offset: 0x00023EE9
		public void SendEof()
		{
			if (!this.IsOpen)
			{
				throw Channel.CreateChannelClosedException();
			}
			this._session.SendMessage(new ChannelEofMessage(this.RemoteChannelNumber));
			this._eofMessageSent = 2;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00025D16 File Offset: 0x00023F16
		protected void WaitOnHandle(WaitHandle waitHandle)
		{
			this._session.WaitOnHandle(waitHandle);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00025D24 File Offset: 0x00023F24
		protected virtual void Close(bool wait)
		{
			if (Interlocked.CompareExchange(ref this._eofMessageSent, 1, 0) == 0 && !this._closeMessageReceived && !this._eofMessageReceived && this.IsOpen && this.IsConnected && this.TrySendMessage(new ChannelEofMessage(this.RemoteChannelNumber)))
			{
				this._eofMessageSent = 2;
			}
			if (Interlocked.CompareExchange(ref this._closeMessageSent, 1, 0) == 0 && this.IsOpen && this.IsConnected && this.TrySendMessage(new ChannelCloseMessage(this.RemoteChannelNumber)))
			{
				this._closeMessageSent = 2;
			}
			this.IsOpen = false;
			if (wait && this._closeMessageSent == 2)
			{
				try
				{
					this.WaitOnHandle(this._channelClosedWaitHandle);
				}
				catch (SshConnectionException)
				{
				}
			}
			this._eofMessageSent = 0;
			this._eofMessageReceived = false;
			this._closeMessageReceived = false;
			this._closeMessageSent = 0;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0000262A File Offset: 0x0000082A
		protected virtual void OnDisconnected()
		{
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0000262A File Offset: 0x0000082A
		protected virtual void OnErrorOccured(Exception exp)
		{
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00025E04 File Offset: 0x00024004
		private void Session_Disconnected(object sender, EventArgs e)
		{
			this.IsOpen = false;
			try
			{
				this.OnDisconnected();
			}
			catch (Exception ex)
			{
				this.OnChannelException(ex);
			}
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00025E3C File Offset: 0x0002403C
		protected void OnChannelException(Exception ex)
		{
			this.OnErrorOccured(ex);
			this.RaiseExceptionEvent(ex);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00025E4C File Offset: 0x0002404C
		private void Session_ErrorOccured(object sender, ExceptionEventArgs e)
		{
			try
			{
				this.OnErrorOccured(e.Exception);
				EventWaitHandle errorOccuredWaitHandle = this._errorOccuredWaitHandle;
				if (errorOccuredWaitHandle != null)
				{
					errorOccuredWaitHandle.Set();
				}
			}
			catch (Exception exception)
			{
				this.RaiseExceptionEvent(exception);
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00025E94 File Offset: 0x00024094
		private void OnChannelWindowAdjust(object sender, MessageEventArgs<ChannelWindowAdjustMessage> e)
		{
			if (e.Message.LocalChannelNumber == this.LocalChannelNumber)
			{
				try
				{
					this.OnWindowAdjust(e.Message.BytesToAdd);
				}
				catch (Exception ex)
				{
					this.OnChannelException(ex);
				}
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00025EE4 File Offset: 0x000240E4
		private void OnChannelData(object sender, MessageEventArgs<ChannelDataMessage> e)
		{
			if (e.Message.LocalChannelNumber == this.LocalChannelNumber)
			{
				try
				{
					this.OnData(e.Message.Data);
				}
				catch (Exception ex)
				{
					this.OnChannelException(ex);
				}
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00025F34 File Offset: 0x00024134
		private void OnChannelExtendedData(object sender, MessageEventArgs<ChannelExtendedDataMessage> e)
		{
			if (e.Message.LocalChannelNumber == this.LocalChannelNumber)
			{
				try
				{
					this.OnExtendedData(e.Message.Data, e.Message.DataTypeCode);
				}
				catch (Exception ex)
				{
					this.OnChannelException(ex);
				}
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00025F8C File Offset: 0x0002418C
		private void OnChannelEof(object sender, MessageEventArgs<ChannelEofMessage> e)
		{
			if (e.Message.LocalChannelNumber == this.LocalChannelNumber)
			{
				try
				{
					this.OnEof();
				}
				catch (Exception ex)
				{
					this.OnChannelException(ex);
				}
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00025FD0 File Offset: 0x000241D0
		private void OnChannelClose(object sender, MessageEventArgs<ChannelCloseMessage> e)
		{
			if (e.Message.LocalChannelNumber == this.LocalChannelNumber)
			{
				try
				{
					this.OnClose();
				}
				catch (Exception ex)
				{
					this.OnChannelException(ex);
				}
				EventWaitHandle channelClosedWaitHandle = this._channelClosedWaitHandle;
				if (channelClosedWaitHandle != null)
				{
					channelClosedWaitHandle.Set();
				}
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00026024 File Offset: 0x00024224
		private void OnChannelRequest(object sender, MessageEventArgs<ChannelRequestMessage> e)
		{
			if (e.Message.LocalChannelNumber == this.LocalChannelNumber)
			{
				try
				{
					RequestInfo requestInfo;
					if (!this._session.ConnectionInfo.ChannelRequests.TryGetValue(e.Message.RequestName, out requestInfo))
					{
						throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Request '{0}' is not supported.", new object[]
						{
							e.Message.RequestName
						}));
					}
					requestInfo.Load(e.Message.RequestData);
					this.OnRequest(requestInfo);
				}
				catch (Exception ex)
				{
					this.OnChannelException(ex);
				}
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000260C8 File Offset: 0x000242C8
		private void OnChannelSuccess(object sender, MessageEventArgs<ChannelSuccessMessage> e)
		{
			if (e.Message.LocalChannelNumber == this.LocalChannelNumber)
			{
				try
				{
					this.OnSuccess();
				}
				catch (Exception ex)
				{
					this.OnChannelException(ex);
				}
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002610C File Offset: 0x0002430C
		private void OnChannelFailure(object sender, MessageEventArgs<ChannelFailureMessage> e)
		{
			if (e.Message.LocalChannelNumber == this.LocalChannelNumber)
			{
				try
				{
					this.OnFailure();
				}
				catch (Exception ex)
				{
					this.OnChannelException(ex);
				}
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00026150 File Offset: 0x00024350
		private void AdjustDataWindow(byte[] messageData)
		{
			this.LocalWindowSize -= (uint)messageData.Length;
			if (this.LocalWindowSize < this.LocalPacketSize)
			{
				this.SendMessage(new ChannelWindowAdjustMessage(this.RemoteChannelNumber, this._initialWindowSize - this.LocalWindowSize));
				this.LocalWindowSize = this._initialWindowSize;
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000261A8 File Offset: 0x000243A8
		private int GetDataLengthThatCanBeSentInMessage(int messageLength)
		{
			int result;
			for (;;)
			{
				object serverWindowSizeLock = this._serverWindowSizeLock;
				lock (serverWindowSizeLock)
				{
					uint remoteWindowSize = this.RemoteWindowSize;
					if (remoteWindowSize != 0U)
					{
						uint num = Math.Min(Math.Min(this.RemotePacketSize, (uint)messageLength), remoteWindowSize);
						this.RemoteWindowSize -= num;
						result = (int)num;
						break;
					}
					this._channelServerWindowAdjustWaitHandle.Reset();
				}
				this.WaitOnHandle(this._channelServerWindowAdjustWaitHandle);
			}
			return result;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00026230 File Offset: 0x00024430
		private static InvalidOperationException CreateRemoteChannelInfoNotAvailableException()
		{
			throw new InvalidOperationException("The channel has not been opened, or the open has not yet been confirmed.");
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002623C File Offset: 0x0002443C
		private static InvalidOperationException CreateChannelClosedException()
		{
			throw new InvalidOperationException("The channel is closed.");
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00026248 File Offset: 0x00024448
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00026258 File Offset: 0x00024458
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				this.Close(false);
				ISession session = this._session;
				if (session != null)
				{
					this._session = null;
					session.ChannelWindowAdjustReceived -= this.OnChannelWindowAdjust;
					session.ChannelDataReceived -= this.OnChannelData;
					session.ChannelExtendedDataReceived -= this.OnChannelExtendedData;
					session.ChannelEofReceived -= this.OnChannelEof;
					session.ChannelCloseReceived -= this.OnChannelClose;
					session.ChannelRequestReceived -= this.OnChannelRequest;
					session.ChannelSuccessReceived -= this.OnChannelSuccess;
					session.ChannelFailureReceived -= this.OnChannelFailure;
					session.ErrorOccured -= this.Session_ErrorOccured;
					session.Disconnected -= this.Session_Disconnected;
				}
				EventWaitHandle channelClosedWaitHandle = this._channelClosedWaitHandle;
				if (channelClosedWaitHandle != null)
				{
					this._channelClosedWaitHandle = null;
					channelClosedWaitHandle.Dispose();
				}
				EventWaitHandle channelServerWindowAdjustWaitHandle = this._channelServerWindowAdjustWaitHandle;
				if (channelServerWindowAdjustWaitHandle != null)
				{
					this._channelServerWindowAdjustWaitHandle = null;
					channelServerWindowAdjustWaitHandle.Dispose();
				}
				EventWaitHandle errorOccuredWaitHandle = this._errorOccuredWaitHandle;
				if (errorOccuredWaitHandle != null)
				{
					this._errorOccuredWaitHandle = null;
					errorOccuredWaitHandle.Dispose();
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00026390 File Offset: 0x00024590
		~Channel()
		{
			this.Dispose(false);
		}

		// Token: 0x0400044D RID: 1101
		private const int Initial = 0;

		// Token: 0x0400044E RID: 1102
		private const int Considered = 1;

		// Token: 0x0400044F RID: 1103
		private const int Sent = 2;

		// Token: 0x04000450 RID: 1104
		private EventWaitHandle _channelClosedWaitHandle = new ManualResetEvent(false);

		// Token: 0x04000451 RID: 1105
		private EventWaitHandle _channelServerWindowAdjustWaitHandle = new ManualResetEvent(false);

		// Token: 0x04000452 RID: 1106
		private EventWaitHandle _errorOccuredWaitHandle = new ManualResetEvent(false);

		// Token: 0x04000453 RID: 1107
		private readonly object _serverWindowSizeLock = new object();

		// Token: 0x04000454 RID: 1108
		private readonly uint _initialWindowSize;

		// Token: 0x04000455 RID: 1109
		private uint? _remoteWindowSize;

		// Token: 0x04000456 RID: 1110
		private uint? _remoteChannelNumber;

		// Token: 0x04000457 RID: 1111
		private uint? _remotePacketSize;

		// Token: 0x04000458 RID: 1112
		private ISession _session;

		// Token: 0x04000459 RID: 1113
		private int _closeMessageSent;

		// Token: 0x0400045A RID: 1114
		private bool _closeMessageReceived;

		// Token: 0x0400045B RID: 1115
		private bool _eofMessageReceived;

		// Token: 0x0400045C RID: 1116
		private int _eofMessageSent;

		// Token: 0x04000469 RID: 1129
		private bool _isDisposed;
	}
}
