using System;

namespace Renci.SshNet.Messages.Authentication
{
	// Token: 0x020000C4 RID: 196
	[Message("SSH_MSG_USERAUTH_PK_OK", 60)]
	internal class PublicKeyMessage : Message
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0001F3B4 File Offset: 0x0001D5B4
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x0001F3BC File Offset: 0x0001D5BC
		public byte[] PublicKeyAlgorithmName { get; private set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0001F3C5 File Offset: 0x0001D5C5
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x0001F3CD File Offset: 0x0001D5CD
		public byte[] PublicKeyData { get; private set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0001F3D6 File Offset: 0x0001D5D6
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.PublicKeyAlgorithmName.Length + 4 + this.PublicKeyData.Length;
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001F3F4 File Offset: 0x0001D5F4
		protected override void LoadData()
		{
			this.PublicKeyAlgorithmName = base.ReadBinary();
			this.PublicKeyData = base.ReadBinary();
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001F40E File Offset: 0x0001D60E
		protected override void SaveData()
		{
			base.WriteBinaryString(this.PublicKeyAlgorithmName);
			base.WriteBinaryString(this.PublicKeyData);
		}
	}
}
