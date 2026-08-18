using System;
using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Transport;

namespace Renci.SshNet.Security
{
	// Token: 0x02000068 RID: 104
	public abstract class KeyExchangeDiffieHellmanGroupSha1 : KeyExchangeDiffieHellman
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000642 RID: 1602
		public abstract BigInteger GroupPrime { get; }

		// Token: 0x06000643 RID: 1603 RVA: 0x00013C30 File Offset: 0x00011E30
		protected override byte[] CalculateHash()
		{
			byte[] bytes = new KeyExchangeDiffieHellmanGroupSha1._ExchangeHashData
			{
				ClientVersion = base.Session.ClientVersion,
				ServerVersion = base.Session.ServerVersion,
				ClientPayload = this._clientPayload,
				ServerPayload = this._serverPayload,
				HostKey = this._hostKey,
				ClientExchangeValue = this._clientExchangeValue,
				ServerExchangeValue = this._serverExchangeValue,
				SharedKey = base.SharedKey
			}.GetBytes();
			return this.Hash(bytes);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00013CBC File Offset: 0x00011EBC
		public override void Start(Session session, KeyExchangeInitMessage message)
		{
			base.Start(session, message);
			base.Session.RegisterMessage("SSH_MSG_KEXDH_REPLY");
			base.Session.MessageReceived += this.Session_MessageReceived;
			this._prime = this.GroupPrime;
			this._group = new BigInteger(new byte[]
			{
				2
			});
			base.PopulateClientExchangeValue();
			base.SendMessage(new KeyExchangeDhInitMessage(this._clientExchangeValue));
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00013D30 File Offset: 0x00011F30
		public override void Finish()
		{
			base.Finish();
			base.Session.MessageReceived -= this.Session_MessageReceived;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00013D50 File Offset: 0x00011F50
		private void Session_MessageReceived(object sender, MessageEventArgs<Message> e)
		{
			KeyExchangeDhReplyMessage keyExchangeDhReplyMessage = e.Message as KeyExchangeDhReplyMessage;
			if (keyExchangeDhReplyMessage != null)
			{
				base.Session.UnRegisterMessage("SSH_MSG_KEXDH_REPLY");
				this.HandleServerDhReply(keyExchangeDhReplyMessage.HostKey, keyExchangeDhReplyMessage.F, keyExchangeDhReplyMessage.Signature);
				this.Finish();
			}
		}

		// Token: 0x0200016B RID: 363
		private class _ExchangeHashData : SshData
		{
			// Token: 0x170002DD RID: 733
			// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00028912 File Offset: 0x00026B12
			// (set) Token: 0x06000CEC RID: 3308 RVA: 0x0002892D File Offset: 0x00026B2D
			public string ServerVersion
			{
				private get
				{
					return SshData.Utf8.GetString(this._serverVersion, 0, this._serverVersion.Length);
				}
				set
				{
					this._serverVersion = SshData.Utf8.GetBytes(value);
				}
			}

			// Token: 0x170002DE RID: 734
			// (get) Token: 0x06000CED RID: 3309 RVA: 0x00028940 File Offset: 0x00026B40
			// (set) Token: 0x06000CEE RID: 3310 RVA: 0x0002895B File Offset: 0x00026B5B
			public string ClientVersion
			{
				private get
				{
					return SshData.Utf8.GetString(this._clientVersion, 0, this._clientVersion.Length);
				}
				set
				{
					this._clientVersion = SshData.Utf8.GetBytes(value);
				}
			}

			// Token: 0x170002DF RID: 735
			// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0002896E File Offset: 0x00026B6E
			// (set) Token: 0x06000CF0 RID: 3312 RVA: 0x00028976 File Offset: 0x00026B76
			public byte[] ClientPayload { get; set; }

			// Token: 0x170002E0 RID: 736
			// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0002897F File Offset: 0x00026B7F
			// (set) Token: 0x06000CF2 RID: 3314 RVA: 0x00028987 File Offset: 0x00026B87
			public byte[] ServerPayload { get; set; }

			// Token: 0x170002E1 RID: 737
			// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00028990 File Offset: 0x00026B90
			// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x00028998 File Offset: 0x00026B98
			public byte[] HostKey { get; set; }

			// Token: 0x170002E2 RID: 738
			// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x000289A1 File Offset: 0x00026BA1
			// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x000289AE File Offset: 0x00026BAE
			public BigInteger ClientExchangeValue
			{
				private get
				{
					return this._clientExchangeValue.ToBigInteger();
				}
				set
				{
					this._clientExchangeValue = value.ToByteArray().Reverse<byte>();
				}
			}

			// Token: 0x170002E3 RID: 739
			// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x000289C2 File Offset: 0x00026BC2
			// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x000289CF File Offset: 0x00026BCF
			public BigInteger ServerExchangeValue
			{
				private get
				{
					return this._serverExchangeValue.ToBigInteger();
				}
				set
				{
					this._serverExchangeValue = value.ToByteArray().Reverse<byte>();
				}
			}

			// Token: 0x170002E4 RID: 740
			// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x000289E3 File Offset: 0x00026BE3
			// (set) Token: 0x06000CFA RID: 3322 RVA: 0x000289F0 File Offset: 0x00026BF0
			public BigInteger SharedKey
			{
				private get
				{
					return this._sharedKey.ToBigInteger();
				}
				set
				{
					this._sharedKey = value.ToByteArray().Reverse<byte>();
				}
			}

			// Token: 0x170002E5 RID: 741
			// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00028A04 File Offset: 0x00026C04
			protected override int BufferCapacity
			{
				get
				{
					return base.BufferCapacity + 4 + this._clientVersion.Length + 4 + this._serverVersion.Length + 4 + this.ClientPayload.Length + 4 + this.ServerPayload.Length + 4 + this.HostKey.Length + 4 + this._clientExchangeValue.Length + 4 + this._serverExchangeValue.Length + 4 + this._sharedKey.Length;
				}
			}

			// Token: 0x06000CFC RID: 3324 RVA: 0x0000B8A3 File Offset: 0x00009AA3
			protected override void LoadData()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000CFD RID: 3325 RVA: 0x00028A70 File Offset: 0x00026C70
			protected override void SaveData()
			{
				base.WriteBinaryString(this._clientVersion);
				base.WriteBinaryString(this._serverVersion);
				base.WriteBinaryString(this.ClientPayload);
				base.WriteBinaryString(this.ServerPayload);
				base.WriteBinaryString(this.HostKey);
				base.WriteBinaryString(this._clientExchangeValue);
				base.WriteBinaryString(this._serverExchangeValue);
				base.WriteBinaryString(this._sharedKey);
			}

			// Token: 0x0400056E RID: 1390
			private byte[] _serverVersion;

			// Token: 0x0400056F RID: 1391
			private byte[] _clientVersion;

			// Token: 0x04000570 RID: 1392
			private byte[] _clientExchangeValue;

			// Token: 0x04000571 RID: 1393
			private byte[] _serverExchangeValue;

			// Token: 0x04000572 RID: 1394
			private byte[] _sharedKey;
		}
	}
}
