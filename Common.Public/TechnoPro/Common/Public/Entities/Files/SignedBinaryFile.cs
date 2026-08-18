using System;

namespace TechnoPro.Common.Public.Entities.Files
{
	// Token: 0x02000336 RID: 822
	public class SignedBinaryFile : BinaryFile
	{
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x0001E19D File Offset: 0x0001C39D
		// (set) Token: 0x060019BF RID: 6591 RVA: 0x0001E1A5 File Offset: 0x0001C3A5
		public byte[] DigitalSignature { get; set; }
	}
}
