using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000CC RID: 204
	[Message("SSH_MSG_KEXECDH_INIT", 30)]
	internal class KeyExchangeEcdhInitMessage : Message, IKeyExchangedAllowed
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0001F882 File Offset: 0x0001DA82
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x0001F88A File Offset: 0x0001DA8A
		public byte[] QC { get; private set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0001F893 File Offset: 0x0001DA93
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.QC.Length;
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001F8A8 File Offset: 0x0001DAA8
		public KeyExchangeEcdhInitMessage(BigInteger d, BigInteger q)
		{
			byte[] array = d.ToByteArray().Reverse<byte>();
			byte[] array2 = q.ToByteArray().Reverse<byte>();
			byte[] array3 = new byte[array.Length + array2.Length + 1];
			array3[0] = 4;
			Buffer.BlockCopy(array, 0, array3, 1, array.Length);
			Buffer.BlockCopy(array2, 0, array3, array.Length + 1, array2.Length);
			this.QC = array3;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001F90B File Offset: 0x0001DB0B
		protected override void LoadData()
		{
			base.ResetReader();
			this.QC = base.ReadBinary();
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001F91F File Offset: 0x0001DB1F
		protected override void SaveData()
		{
			base.WriteBinaryString(this.QC);
		}
	}
}
