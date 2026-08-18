using System;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Transport;

namespace Renci.SshNet.Security
{
	// Token: 0x02000067 RID: 103
	public abstract class KeyExchangeDiffieHellmanGroupExchangeShaBase : KeyExchangeDiffieHellman
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x00013A40 File Offset: 0x00011C40
		protected override byte[] CalculateHash()
		{
			byte[] bytes = new GroupExchangeHashData
			{
				ClientVersion = base.Session.ClientVersion,
				ServerVersion = base.Session.ServerVersion,
				ClientPayload = this._clientPayload,
				ServerPayload = this._serverPayload,
				HostKey = this._hostKey,
				MinimumGroupSize = 1024U,
				PreferredGroupSize = 1024U,
				MaximumGroupSize = 8192U,
				Prime = this._prime,
				SubGroup = this._group,
				ClientExchangeValue = this._clientExchangeValue,
				ServerExchangeValue = this._serverExchangeValue,
				SharedKey = base.SharedKey
			}.GetBytes();
			return this.Hash(bytes);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00013B04 File Offset: 0x00011D04
		public override void Start(Session session, KeyExchangeInitMessage message)
		{
			base.Start(session, message);
			base.Session.RegisterMessage("SSH_MSG_KEX_DH_GEX_GROUP");
			base.Session.MessageReceived += this.Session_MessageReceived;
			base.SendMessage(new KeyExchangeDhGroupExchangeRequest(1024U, 1024U, 8192U));
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00013B5A File Offset: 0x00011D5A
		public override void Finish()
		{
			base.Finish();
			base.Session.MessageReceived -= this.Session_MessageReceived;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00013B7C File Offset: 0x00011D7C
		private void Session_MessageReceived(object sender, MessageEventArgs<Message> e)
		{
			KeyExchangeDhGroupExchangeGroup keyExchangeDhGroupExchangeGroup = e.Message as KeyExchangeDhGroupExchangeGroup;
			if (keyExchangeDhGroupExchangeGroup != null)
			{
				base.Session.UnRegisterMessage("SSH_MSG_KEX_DH_GEX_GROUP");
				base.Session.RegisterMessage("SSH_MSG_KEX_DH_GEX_REPLY");
				this._prime = keyExchangeDhGroupExchangeGroup.SafePrime;
				this._group = keyExchangeDhGroupExchangeGroup.SubGroup;
				base.PopulateClientExchangeValue();
				base.SendMessage(new KeyExchangeDhGroupExchangeInit(this._clientExchangeValue));
				return;
			}
			KeyExchangeDhGroupExchangeReply keyExchangeDhGroupExchangeReply = e.Message as KeyExchangeDhGroupExchangeReply;
			if (keyExchangeDhGroupExchangeReply != null)
			{
				base.Session.UnRegisterMessage("SSH_MSG_KEX_DH_GEX_REPLY");
				this.HandleServerDhReply(keyExchangeDhGroupExchangeReply.HostKey, keyExchangeDhGroupExchangeReply.F, keyExchangeDhGroupExchangeReply.Signature);
				this.Finish();
			}
		}

		// Token: 0x0400023E RID: 574
		private const int MinimumGroupSize = 1024;

		// Token: 0x0400023F RID: 575
		private const int PreferredGroupSize = 1024;

		// Token: 0x04000240 RID: 576
		private const int MaximumProupSize = 8192;
	}
}
