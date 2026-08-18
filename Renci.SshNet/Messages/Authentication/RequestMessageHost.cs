using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Authentication
{
	// Token: 0x020000C6 RID: 198
	internal class RequestMessageHost : RequestMessage
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0001F4D8 File Offset: 0x0001D6D8
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x0001F4E0 File Offset: 0x0001D6E0
		public byte[] PublicKeyAlgorithm { get; private set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0001F4E9 File Offset: 0x0001D6E9
		// (set) Token: 0x060008E9 RID: 2281 RVA: 0x0001F4F1 File Offset: 0x0001D6F1
		public byte[] PublicHostKey { get; private set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x0001F4FA File Offset: 0x0001D6FA
		// (set) Token: 0x060008EB RID: 2283 RVA: 0x0001F502 File Offset: 0x0001D702
		public byte[] ClientHostName { get; private set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0001F50B File Offset: 0x0001D70B
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x0001F513 File Offset: 0x0001D713
		public byte[] ClientUsername { get; private set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0001F51C File Offset: 0x0001D71C
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x0001F524 File Offset: 0x0001D724
		public byte[] Signature { get; private set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0001F52D File Offset: 0x0001D72D
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.PublicKeyAlgorithm.Length + 4 + this.PublicHostKey.Length + 4 + this.ClientHostName.Length + 4 + this.ClientUsername.Length + 4 + this.Signature.Length;
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001F56C File Offset: 0x0001D76C
		public RequestMessageHost(ServiceName serviceName, string username, string publicKeyAlgorithm, byte[] publicHostKey, string clientHostName, string clientUsername, byte[] signature) : base(serviceName, username, "hostbased")
		{
			this.PublicKeyAlgorithm = SshData.Ascii.GetBytes(publicKeyAlgorithm);
			this.PublicHostKey = publicHostKey;
			this.ClientHostName = SshData.Ascii.GetBytes(clientHostName);
			this.ClientUsername = SshData.Utf8.GetBytes(clientUsername);
			this.Signature = signature;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001F5CC File Offset: 0x0001D7CC
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this.PublicKeyAlgorithm);
			base.WriteBinaryString(this.PublicHostKey);
			base.WriteBinaryString(this.ClientHostName);
			base.WriteBinaryString(this.ClientUsername);
			base.WriteBinaryString(this.Signature);
		}
	}
}
