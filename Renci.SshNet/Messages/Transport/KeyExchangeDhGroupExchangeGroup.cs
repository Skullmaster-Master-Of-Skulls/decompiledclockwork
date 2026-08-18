using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000D3 RID: 211
	[Message("SSH_MSG_KEX_DH_GEX_GROUP", 31)]
	public class KeyExchangeDhGroupExchangeGroup : Message
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0001FBF8 File Offset: 0x0001DDF8
		public BigInteger SafePrime
		{
			get
			{
				return this._safePrime.ToBigInteger();
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0001FC05 File Offset: 0x0001DE05
		public BigInteger SubGroup
		{
			get
			{
				return this._subGroup.ToBigInteger();
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0001FC12 File Offset: 0x0001DE12
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._safePrime.Length + 4 + this._subGroup.Length;
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001FC30 File Offset: 0x0001DE30
		protected override void LoadData()
		{
			this._safePrime = base.ReadBinary();
			this._subGroup = base.ReadBinary();
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001FC4A File Offset: 0x0001DE4A
		protected override void SaveData()
		{
			base.WriteBinaryString(this._safePrime);
			base.WriteBinaryString(this._subGroup);
		}

		// Token: 0x0400039A RID: 922
		internal const byte MessageNumber = 31;

		// Token: 0x0400039B RID: 923
		private byte[] _safePrime;

		// Token: 0x0400039C RID: 924
		private byte[] _subGroup;
	}
}
