using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000042 RID: 66
	internal struct DirectoryEntry
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x0000433B File Offset: 0x0000253B
		public DirectoryEntry(int relativeVirtualAddress, int size)
		{
			this.RelativeVirtualAddress = relativeVirtualAddress;
			this.Size = size;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000434B File Offset: 0x0000254B
		internal DirectoryEntry(ref PEBinaryReader reader)
		{
			this.RelativeVirtualAddress = reader.ReadInt32();
			this.Size = reader.ReadInt32();
		}

		// Token: 0x04000224 RID: 548
		public readonly int RelativeVirtualAddress;

		// Token: 0x04000225 RID: 549
		public readonly int Size;
	}
}
