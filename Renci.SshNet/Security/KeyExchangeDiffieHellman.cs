using System;
using System.Text;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Transport;

namespace Renci.SshNet.Security
{
	// Token: 0x02000070 RID: 112
	public abstract class KeyExchangeDiffieHellman : KeyExchange
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x00014BF4 File Offset: 0x00012DF4
		protected override bool ValidateExchangeHash()
		{
			byte[] data = this.CalculateHash();
			uint count = (uint)((int)this._hostKey[0] << 24 | (int)this._hostKey[1] << 16 | (int)this._hostKey[2] << 8 | (int)this._hostKey[3]);
			string @string = Encoding.UTF8.GetString(this._hostKey, 4, (int)count);
			KeyHostAlgorithm keyHostAlgorithm = base.Session.ConnectionInfo.HostKeyAlgorithms[@string](this._hostKey);
			base.Session.ConnectionInfo.CurrentHostKeyAlgorithm = @string;
			return base.CanTrustHostKey(keyHostAlgorithm) && keyHostAlgorithm.VerifySignature(data, this._signature);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00014C92 File Offset: 0x00012E92
		public override void Start(Session session, KeyExchangeInitMessage message)
		{
			base.Start(session, message);
			this._serverPayload = message.GetBytes();
			this._clientPayload = base.Session.ClientInitMessage.GetBytes();
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00014CC0 File Offset: 0x00012EC0
		protected void PopulateClientExchangeValue()
		{
			if (this._group.IsZero)
			{
				throw new ArgumentNullException("_group");
			}
			if (this._prime.IsZero)
			{
				throw new ArgumentNullException("_prime");
			}
			int bitLength = this._prime.BitLength;
			do
			{
				this._randomValue = BigInteger.Random(bitLength);
				this._clientExchangeValue = BigInteger.ModPow(this._group, this._randomValue, this._prime);
			}
			while (this._clientExchangeValue < 1L || this._clientExchangeValue > this._prime - 1);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00014D5F File Offset: 0x00012F5F
		protected virtual void HandleServerDhReply(byte[] hostKey, BigInteger serverExchangeValue, byte[] signature)
		{
			this._serverExchangeValue = serverExchangeValue;
			this._hostKey = hostKey;
			base.SharedKey = BigInteger.ModPow(serverExchangeValue, this._randomValue, this._prime);
			this._signature = signature;
		}

		// Token: 0x04000251 RID: 593
		protected BigInteger _group;

		// Token: 0x04000252 RID: 594
		protected BigInteger _prime;

		// Token: 0x04000253 RID: 595
		protected byte[] _clientPayload;

		// Token: 0x04000254 RID: 596
		protected byte[] _serverPayload;

		// Token: 0x04000255 RID: 597
		protected BigInteger _clientExchangeValue;

		// Token: 0x04000256 RID: 598
		protected BigInteger _serverExchangeValue;

		// Token: 0x04000257 RID: 599
		protected BigInteger _randomValue;

		// Token: 0x04000258 RID: 600
		protected byte[] _hostKey;

		// Token: 0x04000259 RID: 601
		protected byte[] _signature;
	}
}
