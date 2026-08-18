using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Authentication
{
	// Token: 0x020000CA RID: 202
	public class RequestMessagePublicKey : RequestMessage
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0001F773 File Offset: 0x0001D973
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x0001F77B File Offset: 0x0001D97B
		public byte[] PublicKeyAlgorithmName { get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0001F784 File Offset: 0x0001D984
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x0001F78C File Offset: 0x0001D98C
		public byte[] PublicKeyData { get; private set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0001F795 File Offset: 0x0001D995
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x0001F79D File Offset: 0x0001D99D
		public byte[] Signature { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001F7A8 File Offset: 0x0001D9A8
		protected override int BufferCapacity
		{
			get
			{
				int num = base.BufferCapacity;
				num++;
				num += 4;
				num += this.PublicKeyAlgorithmName.Length;
				num += 4;
				num += this.PublicKeyData.Length;
				if (this.Signature != null)
				{
					num += 4;
					num += this.Signature.Length;
				}
				return num;
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001F7F6 File Offset: 0x0001D9F6
		public RequestMessagePublicKey(ServiceName serviceName, string username, string keyAlgorithmName, byte[] keyData) : base(serviceName, username, "publickey")
		{
			this.PublicKeyAlgorithmName = SshData.Ascii.GetBytes(keyAlgorithmName);
			this.PublicKeyData = keyData;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001F81E File Offset: 0x0001DA1E
		public RequestMessagePublicKey(ServiceName serviceName, string username, string keyAlgorithmName, byte[] keyData, byte[] signature) : this(serviceName, username, keyAlgorithmName, keyData)
		{
			this.Signature = signature;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0001F834 File Offset: 0x0001DA34
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.Signature != null);
			base.WriteBinaryString(this.PublicKeyAlgorithmName);
			base.WriteBinaryString(this.PublicKeyData);
			if (this.Signature != null)
			{
				base.WriteBinaryString(this.Signature);
			}
		}
	}
}
