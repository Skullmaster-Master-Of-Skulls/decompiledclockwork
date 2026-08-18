using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Common;
using Renci.SshNet.Compression;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Transport;
using Renci.SshNet.Security.Cryptography;

namespace Renci.SshNet.Security
{
	// Token: 0x0200006F RID: 111
	public abstract class KeyExchange : Algorithm, IKeyExchange, IDisposable
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x000142C4 File Offset: 0x000124C4
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x000142CC File Offset: 0x000124CC
		private protected Session Session { protected get; private set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x000142D5 File Offset: 0x000124D5
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x000142DD File Offset: 0x000124DD
		public BigInteger SharedKey { get; protected set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x000142E6 File Offset: 0x000124E6
		public byte[] ExchangeHash
		{
			get
			{
				if (this._exchangeHash == null)
				{
					this._exchangeHash = this.CalculateHash();
				}
				return this._exchangeHash;
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06000683 RID: 1667 RVA: 0x00014304 File Offset: 0x00012504
		// (remove) Token: 0x06000684 RID: 1668 RVA: 0x0001433C File Offset: 0x0001253C
		public event EventHandler<HostKeyEventArgs> HostKeyReceived;

		// Token: 0x06000685 RID: 1669 RVA: 0x00014374 File Offset: 0x00012574
		public virtual void Start(Session session, KeyExchangeInitMessage message)
		{
			this.Session = session;
			this.SendMessage(session.ClientInitMessage);
			string text = (from b in session.ConnectionInfo.Encryptions.Keys
			from a in message.EncryptionAlgorithmsClientToServer
			where a == b
			select a).FirstOrDefault<string>();
			if (string.IsNullOrEmpty(text))
			{
				throw new SshConnectionException("Client encryption algorithm not found", DisconnectReason.KeyExchangeFailed);
			}
			session.ConnectionInfo.CurrentClientEncryption = text;
			string text2 = (from b in session.ConnectionInfo.Encryptions.Keys
			from a in message.EncryptionAlgorithmsServerToClient
			where a == b
			select a).FirstOrDefault<string>();
			if (string.IsNullOrEmpty(text2))
			{
				throw new SshConnectionException("Server decryption algorithm not found", DisconnectReason.KeyExchangeFailed);
			}
			session.ConnectionInfo.CurrentServerEncryption = text2;
			string text3 = (from b in session.ConnectionInfo.HmacAlgorithms.Keys
			from a in message.MacAlgorithmsClientToServer
			where a == b
			select a).FirstOrDefault<string>();
			if (string.IsNullOrEmpty(text3))
			{
				throw new SshConnectionException("Server HMAC algorithm not found", DisconnectReason.KeyExchangeFailed);
			}
			session.ConnectionInfo.CurrentClientHmacAlgorithm = text3;
			string text4 = (from b in session.ConnectionInfo.HmacAlgorithms.Keys
			from a in message.MacAlgorithmsServerToClient
			where a == b
			select a).FirstOrDefault<string>();
			if (string.IsNullOrEmpty(text4))
			{
				throw new SshConnectionException("Server HMAC algorithm not found", DisconnectReason.KeyExchangeFailed);
			}
			session.ConnectionInfo.CurrentServerHmacAlgorithm = text4;
			string text5 = (from b in session.ConnectionInfo.CompressionAlgorithms.Keys
			from a in message.CompressionAlgorithmsClientToServer
			where a == b
			select a).LastOrDefault<string>();
			if (string.IsNullOrEmpty(text5))
			{
				throw new SshConnectionException("Compression algorithm not found", DisconnectReason.KeyExchangeFailed);
			}
			session.ConnectionInfo.CurrentClientCompressionAlgorithm = text5;
			string text6 = (from b in session.ConnectionInfo.CompressionAlgorithms.Keys
			from a in message.CompressionAlgorithmsServerToClient
			where a == b
			select a).LastOrDefault<string>();
			if (string.IsNullOrEmpty(text6))
			{
				throw new SshConnectionException("Decompression algorithm not found", DisconnectReason.KeyExchangeFailed);
			}
			session.ConnectionInfo.CurrentServerCompressionAlgorithm = text6;
			this._clientCipherInfo = session.ConnectionInfo.Encryptions[text];
			this._serverCipherInfo = session.ConnectionInfo.Encryptions[text2];
			this._clientHashInfo = session.ConnectionInfo.HmacAlgorithms[text3];
			this._serverHashInfo = session.ConnectionInfo.HmacAlgorithms[text4];
			this._compressionType = session.ConnectionInfo.CompressionAlgorithms[text5];
			this._decompressionType = session.ConnectionInfo.CompressionAlgorithms[text6];
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001484B File Offset: 0x00012A4B
		public virtual void Finish()
		{
			if (this.ValidateExchangeHash())
			{
				this.SendMessage(new NewKeysMessage());
				return;
			}
			throw new SshConnectionException("Key exchange negotiation failed.", DisconnectReason.KeyExchangeFailed);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001486C File Offset: 0x00012A6C
		public Cipher CreateServerCipher()
		{
			byte[] sessionId = this.Session.SessionId ?? this.ExchangeHash;
			byte[] arg = this.Hash(KeyExchange.GenerateSessionKey(this.SharedKey, this.ExchangeHash, 'B', sessionId));
			byte[] array = this.Hash(KeyExchange.GenerateSessionKey(this.SharedKey, this.ExchangeHash, 'D', sessionId));
			array = this.GenerateSessionKey(this.SharedKey, this.ExchangeHash, array, this._serverCipherInfo.KeySize / 8);
			return this._serverCipherInfo.Cipher(array, arg);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000148F8 File Offset: 0x00012AF8
		public Cipher CreateClientCipher()
		{
			byte[] sessionId = this.Session.SessionId ?? this.ExchangeHash;
			byte[] arg = this.Hash(KeyExchange.GenerateSessionKey(this.SharedKey, this.ExchangeHash, 'A', sessionId));
			byte[] array = this.Hash(KeyExchange.GenerateSessionKey(this.SharedKey, this.ExchangeHash, 'C', sessionId));
			array = this.GenerateSessionKey(this.SharedKey, this.ExchangeHash, array, this._clientCipherInfo.KeySize / 8);
			return this._clientCipherInfo.Cipher(array, arg);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00014984 File Offset: 0x00012B84
		public HashAlgorithm CreateServerHash()
		{
			byte[] sessionId = this.Session.SessionId ?? this.ExchangeHash;
			byte[] array = this.Hash(KeyExchange.GenerateSessionKey(this.SharedKey, this.ExchangeHash, 'F', sessionId));
			array = this.GenerateSessionKey(this.SharedKey, this.ExchangeHash, array, this._serverHashInfo.KeySize / 8);
			return this._serverHashInfo.HashAlgorithm(array);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000149F4 File Offset: 0x00012BF4
		public HashAlgorithm CreateClientHash()
		{
			byte[] sessionId = this.Session.SessionId ?? this.ExchangeHash;
			byte[] array = this.Hash(KeyExchange.GenerateSessionKey(this.SharedKey, this.ExchangeHash, 'E', sessionId));
			array = this.GenerateSessionKey(this.SharedKey, this.ExchangeHash, array, this._clientHashInfo.KeySize / 8);
			return this._clientHashInfo.HashAlgorithm(array);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00014A64 File Offset: 0x00012C64
		public Compressor CreateCompressor()
		{
			if (this._compressionType == null)
			{
				return null;
			}
			Compressor compressor = this._compressionType.CreateInstance<Compressor>();
			compressor.Init(this.Session);
			return compressor;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00014A8D File Offset: 0x00012C8D
		public Compressor CreateDecompressor()
		{
			if (this._compressionType == null)
			{
				return null;
			}
			Compressor compressor = this._decompressionType.CreateInstance<Compressor>();
			compressor.Init(this.Session);
			return compressor;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00014AB8 File Offset: 0x00012CB8
		protected bool CanTrustHostKey(KeyHostAlgorithm host)
		{
			EventHandler<HostKeyEventArgs> hostKeyReceived = this.HostKeyReceived;
			if (hostKeyReceived != null)
			{
				HostKeyEventArgs hostKeyEventArgs = new HostKeyEventArgs(host);
				hostKeyReceived(this, hostKeyEventArgs);
				return hostKeyEventArgs.CanTrust;
			}
			return true;
		}

		// Token: 0x0600068E RID: 1678
		protected abstract bool ValidateExchangeHash();

		// Token: 0x0600068F RID: 1679
		protected abstract byte[] CalculateHash();

		// Token: 0x06000690 RID: 1680 RVA: 0x00014AE8 File Offset: 0x00012CE8
		protected virtual byte[] Hash(byte[] hashData)
		{
			byte[] result;
			using (SHA1 sha = CryptoAbstraction.CreateSHA1())
			{
				result = sha.ComputeHash(hashData, 0, hashData.Length);
			}
			return result;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00014B24 File Offset: 0x00012D24
		protected void SendMessage(Message message)
		{
			this.Session.SendMessage(message);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00014B34 File Offset: 0x00012D34
		private byte[] GenerateSessionKey(BigInteger sharedKey, byte[] exchangeHash, byte[] key, int size)
		{
			List<byte> list = new List<byte>(key);
			while (size > list.Count)
			{
				list.AddRange(this.Hash(new KeyExchange._SessionKeyAdjustment
				{
					SharedKey = sharedKey,
					ExchangeHash = exchangeHash,
					Key = key
				}.GetBytes()));
			}
			return list.ToArray();
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00014B85 File Offset: 0x00012D85
		private static byte[] GenerateSessionKey(BigInteger sharedKey, byte[] exchangeHash, char p, byte[] sessionId)
		{
			return new KeyExchange._SessionKeyGeneration
			{
				SharedKey = sharedKey,
				ExchangeHash = exchangeHash,
				Char = p,
				SessionId = sessionId
			}.GetBytes();
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00014BAD File Offset: 0x00012DAD
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0000262A File Offset: 0x0000082A
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00014BBC File Offset: 0x00012DBC
		~KeyExchange()
		{
			this.Dispose(false);
		}

		// Token: 0x04000247 RID: 583
		private CipherInfo _clientCipherInfo;

		// Token: 0x04000248 RID: 584
		private CipherInfo _serverCipherInfo;

		// Token: 0x04000249 RID: 585
		private HashInfo _clientHashInfo;

		// Token: 0x0400024A RID: 586
		private HashInfo _serverHashInfo;

		// Token: 0x0400024B RID: 587
		private Type _compressionType;

		// Token: 0x0400024C RID: 588
		private Type _decompressionType;

		// Token: 0x0400024F RID: 591
		private byte[] _exchangeHash;

		// Token: 0x0200016C RID: 364
		private class _SessionKeyGeneration : SshData
		{
			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00028ADD File Offset: 0x00026CDD
			// (set) Token: 0x06000D00 RID: 3328 RVA: 0x00028AEA File Offset: 0x00026CEA
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

			// Token: 0x170002E7 RID: 743
			// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00028AFE File Offset: 0x00026CFE
			// (set) Token: 0x06000D02 RID: 3330 RVA: 0x00028B06 File Offset: 0x00026D06
			public byte[] ExchangeHash { get; set; }

			// Token: 0x170002E8 RID: 744
			// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00028B0F File Offset: 0x00026D0F
			// (set) Token: 0x06000D04 RID: 3332 RVA: 0x00028B17 File Offset: 0x00026D17
			public char Char { get; set; }

			// Token: 0x170002E9 RID: 745
			// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00028B20 File Offset: 0x00026D20
			// (set) Token: 0x06000D06 RID: 3334 RVA: 0x00028B28 File Offset: 0x00026D28
			public byte[] SessionId { get; set; }

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00028B31 File Offset: 0x00026D31
			protected override int BufferCapacity
			{
				get
				{
					return base.BufferCapacity + 4 + this._sharedKey.Length + this.ExchangeHash.Length + 1 + this.SessionId.Length;
				}
			}

			// Token: 0x06000D08 RID: 3336 RVA: 0x0000B8A3 File Offset: 0x00009AA3
			protected override void LoadData()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000D09 RID: 3337 RVA: 0x00028B58 File Offset: 0x00026D58
			protected override void SaveData()
			{
				base.WriteBinaryString(this._sharedKey);
				base.Write(this.ExchangeHash);
				base.Write((byte)this.Char);
				base.Write(this.SessionId);
			}

			// Token: 0x04000576 RID: 1398
			private byte[] _sharedKey;
		}

		// Token: 0x0200016D RID: 365
		private class _SessionKeyAdjustment : SshData
		{
			// Token: 0x170002EB RID: 747
			// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00028B8B File Offset: 0x00026D8B
			// (set) Token: 0x06000D0C RID: 3340 RVA: 0x00028B98 File Offset: 0x00026D98
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

			// Token: 0x170002EC RID: 748
			// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00028BAC File Offset: 0x00026DAC
			// (set) Token: 0x06000D0E RID: 3342 RVA: 0x00028BB4 File Offset: 0x00026DB4
			public byte[] ExchangeHash { get; set; }

			// Token: 0x170002ED RID: 749
			// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00028BBD File Offset: 0x00026DBD
			// (set) Token: 0x06000D10 RID: 3344 RVA: 0x00028BC5 File Offset: 0x00026DC5
			public byte[] Key { get; set; }

			// Token: 0x170002EE RID: 750
			// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00028BCE File Offset: 0x00026DCE
			protected override int BufferCapacity
			{
				get
				{
					return base.BufferCapacity + 4 + this._sharedKey.Length + this.ExchangeHash.Length + this.Key.Length;
				}
			}

			// Token: 0x06000D12 RID: 3346 RVA: 0x0000B8A3 File Offset: 0x00009AA3
			protected override void LoadData()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000D13 RID: 3347 RVA: 0x00028BF3 File Offset: 0x00026DF3
			protected override void SaveData()
			{
				base.WriteBinaryString(this._sharedKey);
				base.Write(this.ExchangeHash);
				base.Write(this.Key);
			}

			// Token: 0x0400057A RID: 1402
			private byte[] _sharedKey;
		}
	}
}
