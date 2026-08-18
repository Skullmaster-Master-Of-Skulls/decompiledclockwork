using System;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet.Channels
{
	// Token: 0x02000112 RID: 274
	internal abstract class ClientChannel : Channel
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x00026E4B File Offset: 0x0002504B
		protected ClientChannel(ISession session, uint localChannelNumber, uint localWindowSize, uint localPacketSize) : base(session, localChannelNumber, localWindowSize, localPacketSize)
		{
			session.ChannelOpenConfirmationReceived += this.OnChannelOpenConfirmation;
			session.ChannelOpenFailureReceived += this.OnChannelOpenFailure;
		}

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06000BEC RID: 3052 RVA: 0x00026E7C File Offset: 0x0002507C
		// (remove) Token: 0x06000BED RID: 3053 RVA: 0x00026EB4 File Offset: 0x000250B4
		public event EventHandler<ChannelOpenConfirmedEventArgs> OpenConfirmed;

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06000BEE RID: 3054 RVA: 0x00026EEC File Offset: 0x000250EC
		// (remove) Token: 0x06000BEF RID: 3055 RVA: 0x00026F24 File Offset: 0x00025124
		public event EventHandler<ChannelOpenFailedEventArgs> OpenFailed;

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00026F5C File Offset: 0x0002515C
		protected virtual void OnOpenConfirmation(uint remoteChannelNumber, uint initialWindowSize, uint maximumPacketSize)
		{
			base.InitializeRemoteInfo(remoteChannelNumber, initialWindowSize, maximumPacketSize);
			base.IsOpen = true;
			EventHandler<ChannelOpenConfirmedEventArgs> openConfirmed = this.OpenConfirmed;
			if (openConfirmed != null)
			{
				openConfirmed(this, new ChannelOpenConfirmedEventArgs(remoteChannelNumber, initialWindowSize, maximumPacketSize));
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00026F92 File Offset: 0x00025192
		protected void SendMessage(ChannelOpenMessage message)
		{
			base.Session.SendMessage(message);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00026FA0 File Offset: 0x000251A0
		protected virtual void OnOpenFailure(uint reasonCode, string description, string language)
		{
			EventHandler<ChannelOpenFailedEventArgs> openFailed = this.OpenFailed;
			if (openFailed != null)
			{
				openFailed(this, new ChannelOpenFailedEventArgs(base.LocalChannelNumber, reasonCode, description, language));
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00026FCC File Offset: 0x000251CC
		private void OnChannelOpenConfirmation(object sender, MessageEventArgs<ChannelOpenConfirmationMessage> e)
		{
			if (e.Message.LocalChannelNumber == base.LocalChannelNumber)
			{
				try
				{
					this.OnOpenConfirmation(e.Message.RemoteChannelNumber, e.Message.InitialWindowSize, e.Message.MaximumPacketSize);
				}
				catch (Exception ex)
				{
					base.OnChannelException(ex);
				}
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00027030 File Offset: 0x00025230
		private void OnChannelOpenFailure(object sender, MessageEventArgs<ChannelOpenFailureMessage> e)
		{
			if (e.Message.LocalChannelNumber == base.LocalChannelNumber)
			{
				try
				{
					this.OnOpenFailure(e.Message.ReasonCode, e.Message.Description, e.Message.Language);
				}
				catch (Exception ex)
				{
					base.OnChannelException(ex);
				}
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00027094 File Offset: 0x00025294
		protected override void Dispose(bool disposing)
		{
			this.UnsubscribeFromSessionEvents(base.Session);
			base.Dispose(disposing);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x000270A9 File Offset: 0x000252A9
		private void UnsubscribeFromSessionEvents(ISession session)
		{
			if (session == null)
			{
				return;
			}
			session.ChannelOpenConfirmationReceived -= this.OnChannelOpenConfirmation;
			session.ChannelOpenFailureReceived -= this.OnChannelOpenFailure;
		}
	}
}
