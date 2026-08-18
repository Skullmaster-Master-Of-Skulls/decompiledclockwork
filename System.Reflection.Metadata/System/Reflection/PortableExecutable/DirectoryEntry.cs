using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200001D RID: 29
	public struct DirectoryEntry
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00005DC2 File Offset: 0x00003FC2
		public DirectoryEntry(int relativeVirtualAddress, int size)
		{
			this.RelativeVirtualAddress = relativeVirtualAddress;
			this.Size = size;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005DD2 File Offset: 0x00003FD2
		internal DirectoryEntry(ref PEBinaryReader reader)
		{
			this.RelativeVirtualAddress = reader.ReadInt32();
			this.Size = reader.ReadInt32();
		}

		// Token: 0x040000B3 RID: 179
		public readonly int RelativeVirtualAddress;

		// Token: 0x040000B4 RID: 180
		public readonly int Size;
	}
}
