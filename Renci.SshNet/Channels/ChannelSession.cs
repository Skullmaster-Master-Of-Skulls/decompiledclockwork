using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet.Channels
{
	// Token: 0x02000110 RID: 272
	internal class ChannelSession : ClientChannel, IChannelSession, IChannel, IDisposable
	{
		// Token: 0x06000BD1 RID: 3025 RVA: 0x000269AD File Offset: 0x00024BAD
		public ChannelSession(ISession session, uint localChannelNumber, uint localWindowSize, uint localPacketSize) : base(session, localChannelNumber, localWindowSize, localPacketSize)
		{
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0000CAD2 File Offset: 0x0000ACD2
		public override ChannelTypes ChannelType
		{
			get
			{
				return ChannelTypes.Session;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000269D4 File Offset: 0x00024BD4
		public virtual void Open()
		{
			if (!base.IsOpen)
			{
				while (!base.IsOpen && this._failedOpenAttempts < base.ConnectionInfo.RetryAttempts)
				{
					this.SendChannelOpenMessage();
					try
					{
						base.WaitOnHandle(this._channelOpenResponseWaitHandle);
					}
					catch (Exception)
					{
						this.ReleaseSemaphore();
						throw;
					}
				}
				if (!base.IsOpen)
				{
					throw new SshException(string.Format(CultureInfo.CurrentCulture, "Failed to open a channel after {0} attempts.", new object[]
					{
						this._failedOpenAttempts
					}));
				}
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00026A64 File Offset: 0x00024C64
		protected override void OnOpenConfirmation(uint remoteChannelNumber, uint initialWindowSize, uint maximumPacketSize)
		{
			base.OnOpenConfirmation(remoteChannelNumber, initialWindowSize, maximumPacketSize);
			this._channelOpenResponseWaitHandle.Set();
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00026A7B File Offset: 0x00024C7B
		protected override void OnOpenFailure(uint reasonCode, string description, string language)
		{
			this._failedOpenAttempts++;
			this.ReleaseSemaphore();
			this._channelOpenResponseWaitHandle.Set();
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00026A9D File Offset: 0x00024C9D
		protected override void OnClose()
		{
			base.OnClose();
			ThreadAbstraction.Sleep(100);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00026AAC File Offset: 0x00024CAC
		protected override void Close(bool wait)
		{
			base.Close(wait);
			this.ReleaseSemaphore();
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00026ABB File Offset: 0x00024CBB
		public bool SendPseudoTerminalRequest(string environmentVariable, uint columns, uint rows, uint width, uint height, IDictionary<TerminalModes, uint> terminalModeValues)
		{
			this._channelRequestResponse.Reset();
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new PseudoTerminalRequestInfo(environmentVariable, columns, rows, width, height, terminalModeValues)));
			base.WaitOnHandle(this._channelRequestResponse);
			return this._channelRequestSucces;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00026AFA File Offset: 0x00024CFA
		public bool SendX11ForwardingRequest(bool isSingleConnection, string protocol, byte[] cookie, uint screenNumber)
		{
			this._channelRequestResponse.Reset();
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new X11ForwardingRequestInfo(isSingleConnection, protocol, cookie, screenNumber)));
			base.WaitOnHandle(this._channelRequestResponse);
			return this._channelRequestSucces;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00026B35 File Offset: 0x00024D35
		public bool SendEnvironmentVariableRequest(string variableName, string variableValue)
		{
			this._channelRequestResponse.Reset();
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new EnvironmentVariableRequestInfo(variableName, variableValue)));
			base.WaitOnHandle(this._channelRequestResponse);
			return this._channelRequestSucces;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00026B6D File Offset: 0x00024D6D
		public bool SendShellRequest()
		{
			this._channelRequestResponse.Reset();
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new ShellRequestInfo()));
			base.WaitOnHandle(this._channelRequestResponse);
			return this._channelRequestSucces;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00026BA4 File Offset: 0x00024DA4
		public bool SendExecRequest(string command)
		{
			this._channelRequestResponse.Reset();
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new ExecRequestInfo(command, base.ConnectionInfo.Encoding)));
			base.WaitOnHandle(this._channelRequestResponse);
			return this._channelRequestSucces;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00026BF1 File Offset: 0x00024DF1
		public bool SendBreakRequest(uint breakLength)
		{
			this._channelRequestResponse.Reset();
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new BreakRequestInfo(breakLength)));
			base.WaitOnHandle(this._channelRequestResponse);
			return this._channelRequestSucces;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00026C28 File Offset: 0x00024E28
		public bool SendSubsystemRequest(string subsystem)
		{
			this._channelRequestResponse.Reset();
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new SubsystemRequestInfo(subsystem)));
			base.WaitOnHandle(this._channelRequestResponse);
			return this._channelRequestSucces;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00026C5F File Offset: 0x00024E5F
		public bool SendWindowChangeRequest(uint columns, uint rows, uint width, uint height)
		{
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new WindowChangeRequestInfo(columns, rows, width, height)));
			return true;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00026C7D File Offset: 0x00024E7D
		public bool SendLocalFlowRequest(bool clientCanDo)
		{
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new XonXoffRequestInfo(clientCanDo)));
			return true;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00026C97 File Offset: 0x00024E97
		public bool SendSignalRequest(string signalName)
		{
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new SignalRequestInfo(signalName)));
			return true;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00026CB1 File Offset: 0x00024EB1
		public bool SendExitStatusRequest(uint exitStatus)
		{
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new ExitStatusRequestInfo(exitStatus)));
			return true;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00026CCB File Offset: 0x00024ECB
		public bool SendExitSignalRequest(string signalName, bool coreDumped, string errorMessage, string language)
		{
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new ExitSignalRequestInfo(signalName, coreDumped, errorMessage, language)));
			return true;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00026CE9 File Offset: 0x00024EE9
		public bool SendEndOfWriteRequest()
		{
			this._channelRequestResponse.Reset();
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new EndOfWriteRequestInfo()));
			base.WaitOnHandle(this._channelRequestResponse);
			return this._channelRequestSucces;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00026D1F File Offset: 0x00024F1F
		public bool SendKeepAliveRequest()
		{
			this._channelRequestResponse.Reset();
			base.SendMessage(new ChannelRequestMessage(base.RemoteChannelNumber, new KeepAliveRequestInfo()));
			base.WaitOnHandle(this._channelRequestResponse);
			return this._channelRequestSucces;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00026D58 File Offset: 0x00024F58
		protected override void OnSuccess()
		{
			base.OnSuccess();
			this._channelRequestSucces = true;
			EventWaitHandle channelRequestResponse = this._channelRequestResponse;
			if (channelRequestResponse != null)
			{
				channelRequestResponse.Set();
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00026D84 File Offset: 0x00024F84
		protected override void OnFailure()
		{
			base.OnFailure();
			this._channelRequestSucces = false;
			EventWaitHandle channelRequestResponse = this._channelRequestResponse;
			if (channelRequestResponse != null)
			{
				channelRequestResponse.Set();
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00026DAF File Offset: 0x00024FAF
		protected void SendChannelOpenMessage()
		{
			if (Interlocked.CompareExchange(ref this._sessionSemaphoreObtained, 1, 0) == 0)
			{
				base.SessionSemaphore.Wait();
				base.SendMessage(new ChannelOpenMessage(base.LocalChannelNumber, base.LocalWindowSize, base.LocalPacketSize, new SessionChannelOpenInfo()));
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00026DED File Offset: 0x00024FED
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this._channelOpenResponseWaitHandle != null)
				{
					this._channelOpenResponseWaitHandle.Dispose();
					this._channelOpenResponseWaitHandle = null;
				}
				if (this._channelRequestResponse != null)
				{
					this._channelRequestResponse.Dispose();
					this._channelRequestResponse = null;
				}
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00026E2D File Offset: 0x0002502D
		private void ReleaseSemaphore()
		{
			if (Interlocked.CompareExchange(ref this._sessionSemaphoreObtained, 0, 1) == 1)
			{
				base.SessionSemaphore.Release();
			}
		}

		// Token: 0x04000472 RID: 1138
		private int _failedOpenAttempts;

		// Token: 0x04000473 RID: 1139
		private int _sessionSemaphoreObtained;

		// Token: 0x04000474 RID: 1140
		private EventWaitHandle _channelOpenResponseWaitHandle = new AutoResetEvent(false);

		// Token: 0x04000475 RID: 1141
		private EventWaitHandle _channelRequestResponse = new ManualResetEvent(false);

		// Token: 0x04000476 RID: 1142
		private bool _channelRequestSucces;
	}
}
