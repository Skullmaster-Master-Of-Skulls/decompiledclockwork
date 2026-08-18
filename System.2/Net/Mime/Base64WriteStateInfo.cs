using System;

namespace System.Net.Mime
{
	// Token: 0x0200023D RID: 573
	internal class Base64WriteStateInfo : WriteStateInfoBase
	{
		// Token: 0x060015BC RID: 5564 RVA: 0x00070A26 File Offset: 0x0006EC26
		internal Base64WriteStateInfo()
		{
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00070A2E File Offset: 0x0006EC2E
		internal Base64WriteStateInfo(int bufferSize, byte[] header, byte[] footer, int maxLineLength, int mimeHeaderLength) : base(bufferSize, header, footer, maxLineLength, mimeHeaderLength)
		{
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x00070A3D File Offset: 0x0006EC3D
		// (set) Token: 0x060015BF RID: 5567 RVA: 0x00070A45 File Offset: 0x0006EC45
		internal int Padding { get; set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00070A4E File Offset: 0x0006EC4E
		// (set) Token: 0x060015C1 RID: 5569 RVA: 0x00070A56 File Offset: 0x0006EC56
		internal byte LastBits { get; set; }
	}
}
