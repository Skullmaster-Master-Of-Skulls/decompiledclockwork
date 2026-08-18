using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Security
{
	// Token: 0x02000065 RID: 101
	internal class GroupExchangeHashData : SshData
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x000137A3 File Offset: 0x000119A3
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x000137BE File Offset: 0x000119BE
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

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x000137D1 File Offset: 0x000119D1
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x000137EC File Offset: 0x000119EC
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x000137FF File Offset: 0x000119FF
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x00013807 File Offset: 0x00011A07
		public byte[] ClientPayload { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00013810 File Offset: 0x00011A10
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x00013818 File Offset: 0x00011A18
		public byte[] ServerPayload { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00013821 File Offset: 0x00011A21
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x00013829 File Offset: 0x00011A29
		public byte[] HostKey { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00013832 File Offset: 0x00011A32
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0001383A File Offset: 0x00011A3A
		public uint MinimumGroupSize { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00013843 File Offset: 0x00011A43
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x0001384B File Offset: 0x00011A4B
		public uint PreferredGroupSize { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00013854 File Offset: 0x00011A54
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x0001385C File Offset: 0x00011A5C
		public uint MaximumGroupSize { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00013865 File Offset: 0x00011A65
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x00013872 File Offset: 0x00011A72
		public BigInteger Prime
		{
			private get
			{
				return this._prime.ToBigInteger();
			}
			set
			{
				this._prime = value.ToByteArray().Reverse<byte>();
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00013886 File Offset: 0x00011A86
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x00013893 File Offset: 0x00011A93
		public BigInteger SubGroup
		{
			private get
			{
				return this._subGroup.ToBigInteger();
			}
			set
			{
				this._subGroup = value.ToByteArray().Reverse<byte>();
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x000138A7 File Offset: 0x00011AA7
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x000138B4 File Offset: 0x00011AB4
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

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000138C8 File Offset: 0x00011AC8
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x000138D5 File Offset: 0x00011AD5
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

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000138E9 File Offset: 0x00011AE9
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x000138F6 File Offset: 0x00011AF6
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

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0001390C File Offset: 0x00011B0C
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._clientVersion.Length + 4 + this._serverVersion.Length + 4 + this.ClientPayload.Length + 4 + this.ServerPayload.Length + 4 + this.HostKey.Length + 4 + 4 + 4 + 4 + this._prime.Length + 4 + this._subGroup.Length + 4 + this._clientExchangeValue.Length + 4 + this._serverExchangeValue.Length + 4 + this._sharedKey.Length;
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		protected override void LoadData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00013994 File Offset: 0x00011B94
		protected override void SaveData()
		{
			base.WriteBinaryString(this._clientVersion);
			base.WriteBinaryString(this._serverVersion);
			base.WriteBinaryString(this.ClientPayload);
			base.WriteBinaryString(this.ServerPayload);
			base.WriteBinaryString(this.HostKey);
			base.Write(this.MinimumGroupSize);
			base.Write(this.PreferredGroupSize);
			base.Write(this.MaximumGroupSize);
			base.WriteBinaryString(this._prime);
			base.WriteBinaryString(this._subGroup);
			base.WriteBinaryString(this._clientExchangeValue);
			base.WriteBinaryString(this._serverExchangeValue);
			base.WriteBinaryString(this._sharedKey);
		}

		// Token: 0x04000231 RID: 561
		private byte[] _serverVersion;

		// Token: 0x04000232 RID: 562
		private byte[] _clientVersion;

		// Token: 0x04000233 RID: 563
		private byte[] _prime;

		// Token: 0x04000234 RID: 564
		private byte[] _subGroup;

		// Token: 0x04000235 RID: 565
		private byte[] _clientExchangeValue;

		// Token: 0x04000236 RID: 566
		private byte[] _serverExchangeValue;

		// Token: 0x04000237 RID: 567
		private byte[] _sharedKey;
	}
}
