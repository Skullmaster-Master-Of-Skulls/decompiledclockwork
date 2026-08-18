using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000050 RID: 80
	internal struct SectionHeader
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00005BCD File Offset: 0x00003DCD
		public string Name { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00005BD5 File Offset: 0x00003DD5
		public int VirtualSize { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00005BDD File Offset: 0x00003DDD
		public int VirtualAddress { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00005BE5 File Offset: 0x00003DE5
		public int SizeOfRawData { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00005BED File Offset: 0x00003DED
		public int PointerToRawData { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00005BF5 File Offset: 0x00003DF5
		public int PointerToRelocations { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00005BFD File Offset: 0x00003DFD
		public int PointerToLineNumbers { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00005C05 File Offset: 0x00003E05
		public ushort NumberOfRelocations { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00005C0D File Offset: 0x00003E0D
		public ushort NumberOfLineNumbers { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00005C15 File Offset: 0x00003E15
		public SectionCharacteristics SectionCharacteristics { get; }

		// Token: 0x0600022C RID: 556 RVA: 0x00005C20 File Offset: 0x00003E20
		internal SectionHeader(ref PEBinaryReader reader)
		{
			this.Name = reader.ReadNullPaddedUTF8(8);
			this.VirtualSize = reader.ReadInt32();
			this.VirtualAddress = reader.ReadInt32();
			this.SizeOfRawData = reader.ReadInt32();
			this.PointerToRawData = reader.ReadInt32();
			this.PointerToRelocations = reader.ReadInt32();
			this.PointerToLineNumbers = reader.ReadInt32();
			this.NumberOfRelocations = reader.ReadUInt16();
			this.NumberOfLineNumbers = reader.ReadUInt16();
			this.SectionCharacteristics = reader.ReadUInt32();
		}

		// Token: 0x040002F3 RID: 755
		internal const int NameSize = 8;

		// Token: 0x040002F4 RID: 756
		internal const int Size = 40;
	}
}
