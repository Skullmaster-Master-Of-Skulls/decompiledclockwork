using System;
using System.Security.Cryptography;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Security;

namespace Renci.SshNet.Common
{
	// Token: 0x020000F3 RID: 243
	public class HostKeyEventArgs : EventArgs
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x000241A2 File Offset: 0x000223A2
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x000241AA File Offset: 0x000223AA
		public bool CanTrust { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x000241B3 File Offset: 0x000223B3
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x000241BB File Offset: 0x000223BB
		public byte[] HostKey { get; private set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x000241C4 File Offset: 0x000223C4
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x000241CC File Offset: 0x000223CC
		public string HostKeyName { get; private set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x000241D5 File Offset: 0x000223D5
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x000241DD File Offset: 0x000223DD
		public byte[] FingerPrint { get; private set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x000241E6 File Offset: 0x000223E6
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x000241EE File Offset: 0x000223EE
		public int KeyLength { get; private set; }

		// Token: 0x06000A99 RID: 2713 RVA: 0x000241F8 File Offset: 0x000223F8
		public HostKeyEventArgs(KeyHostAlgorithm host)
		{
			this.CanTrust = true;
			this.HostKey = host.Data;
			this.HostKeyName = host.Name;
			this.KeyLength = host.Key.KeyLength;
			using (MD5 md = CryptoAbstraction.CreateMD5())
			{
				this.FingerPrint = md.ComputeHash(host.Data);
			}
		}
	}
}
