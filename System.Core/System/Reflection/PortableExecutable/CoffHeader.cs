using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200003C RID: 60
	internal sealed class CoffHeader
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000040F7 File Offset: 0x000022F7
		public Machine Machine { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000040FF File Offset: 0x000022FF
		public short NumberOfSections { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00004107 File Offset: 0x00002307
		public int TimeDateStamp { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000410F File Offset: 0x0000230F
		public int PointerToSymbolTable { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00004117 File Offset: 0x00002317
		public int NumberOfSymbols { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000411F File Offset: 0x0000231F
		public short SizeOfOptionalHeader { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00004127 File Offset: 0x00002327
		public Characteristics Characteristics { get; }

		// Token: 0x0600018D RID: 397 RVA: 0x00004130 File Offset: 0x00002330
		internal CoffHeader(ref PEBinaryReader reader)
		{
			this.Machine = reader.ReadUInt16();
			this.NumberOfSections = reader.ReadInt16();
			this.TimeDateStamp = reader.ReadInt32();
			this.PointerToSymbolTable = reader.ReadInt32();
			this.NumberOfSymbols = reader.ReadInt32();
			this.SizeOfOptionalHeader = reader.ReadInt16();
			this.Characteristics = reader.ReadUInt16();
		}

		// Token: 0x040001FF RID: 511
		internal const int Size = 20;
	}
}
