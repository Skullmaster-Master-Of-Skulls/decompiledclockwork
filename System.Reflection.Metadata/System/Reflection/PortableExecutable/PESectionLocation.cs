using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000016 RID: 22
	internal struct PESectionLocation
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00005A70 File Offset: 0x00003C70
		public int RelativeVirtualAddress { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00005A78 File Offset: 0x00003C78
		public int PointerToRawData { get; }

		// Token: 0x06000183 RID: 387 RVA: 0x00005A80 File Offset: 0x00003C80
		public PESectionLocation(int relativeVirtualAddress, int pointerToRawData)
		{
			this.RelativeVirtualAddress = relativeVirtualAddress;
			this.PointerToRawData = pointerToRawData;
		}
	}
}
