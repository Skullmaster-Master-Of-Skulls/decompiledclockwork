using System;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000CD RID: 205
	[Message("SSH_MSG_KEXECDH_REPLY", 31)]
	public class KeyExchangeEcdhReplyMessage : Message
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0001F92D File Offset: 0x0001DB2D
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0001F935 File Offset: 0x0001DB35
		public byte[] KS { get; private set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0001F93E File Offset: 0x0001DB3E
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x0001F946 File Offset: 0x0001DB46
		public byte[] QS { get; private set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0001F94F File Offset: 0x0001DB4F
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0001F957 File Offset: 0x0001DB57
		public byte[] Signature { get; private set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0001F960 File Offset: 0x0001DB60
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.KS.Length + 4 + this.QS.Length + 4 + this.Signature.Length;
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001F989 File Offset: 0x0001DB89
		protected override void LoadData()
		{
			base.ResetReader();
			this.KS = base.ReadBinary();
			this.QS = base.ReadBinary();
			this.Signature = base.ReadBinary();
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001F9B5 File Offset: 0x0001DBB5
		protected override void SaveData()
		{
			base.WriteBinaryString(this.KS);
			base.WriteBinaryString(this.QS);
			base.WriteBinaryString(this.Signature);
		}
	}
}
