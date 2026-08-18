using System;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet.Channels
{
	// Token: 0x02000113 RID: 275
	internal abstract class ServerChannel : Channel
	{
		// Token: 0x06000BF7 RID: 3063 RVA: 0x000270D3 File Offset: 0x000252D3
		protected ServerChannel(ISession session, uint localChannelNumber, uint localWindowSize, uint localPacketSize, uint remoteChannelNumber, uint remoteWindowSize, uint remotePacketSize) : base(session, localChannelNumber, localWindowSize, localPacketSize)
		{
			base.InitializeRemoteInfo(remoteChannelNumber, remoteWindowSize, remotePacketSize);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000270EC File Offset: 0x000252EC
		protected void SendMessage(ChannelOpenConfirmationMessage message)
		{
			base.Session.SendMessage(message);
			base.IsOpen = true;
		}
	}
}
