using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200004A RID: 74
	internal sealed class PEHeader
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000044E2 File Offset: 0x000026E2
		public PEMagic Magic { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000044EA File Offset: 0x000026EA
		public byte MajorLinkerVersion { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000044F2 File Offset: 0x000026F2
		public byte MinorLinkerVersion { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000044FA File Offset: 0x000026FA
		public int SizeOfCode { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00004502 File Offset: 0x00002702
		public int SizeOfInitializedData { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000450A File Offset: 0x0000270A
		public int SizeOfUninitializedData { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00004512 File Offset: 0x00002712
		public int AddressOfEntryPoint { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000451A File Offset: 0x0000271A
		public int BaseOfCode { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00004522 File Offset: 0x00002722
		public int BaseOfData { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000452A File Offset: 0x0000272A
		public ulong ImageBase { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00004532 File Offset: 0x00002732
		public int SectionAlignment { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000453A File Offset: 0x0000273A
		public int FileAlignment { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00004542 File Offset: 0x00002742
		public ushort MajorOperatingSystemVersion { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000454A File Offset: 0x0000274A
		public ushort MinorOperatingSystemVersion { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00004552 File Offset: 0x00002752
		public ushort MajorImageVersion { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000455A File Offset: 0x0000275A
		public ushort MinorImageVersion { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00004562 File Offset: 0x00002762
		public ushort MajorSubsystemVersion { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000456A File Offset: 0x0000276A
		public ushort MinorSubsystemVersion { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00004572 File Offset: 0x00002772
		public int SizeOfImage { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000457A File Offset: 0x0000277A
		public int SizeOfHeaders { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00004582 File Offset: 0x00002782
		public uint CheckSum { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000458A File Offset: 0x0000278A
		public Subsystem Subsystem { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00004592 File Offset: 0x00002792
		public DllCharacteristics DllCharacteristics { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000459A File Offset: 0x0000279A
		public ulong SizeOfStackReserve { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000045A2 File Offset: 0x000027A2
		public ulong SizeOfStackCommit { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000045AA File Offset: 0x000027AA
		public ulong SizeOfHeapReserve { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000045B2 File Offset: 0x000027B2
		public ulong SizeOfHeapCommit { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000045BA File Offset: 0x000027BA
		public int NumberOfRvaAndSizes { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000045C2 File Offset: 0x000027C2
		public DirectoryEntry ExportTableDirectory { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000045CA File Offset: 0x000027CA
		public DirectoryEntry ImportTableDirectory { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000045D2 File Offset: 0x000027D2
		public DirectoryEntry ResourceTableDirectory { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000045DA File Offset: 0x000027DA
		public DirectoryEntry ExceptionTableDirectory { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000045E2 File Offset: 0x000027E2
		public DirectoryEntry CertificateTableDirectory { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000045EA File Offset: 0x000027EA
		public DirectoryEntry BaseRelocationTableDirectory { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000045F2 File Offset: 0x000027F2
		public DirectoryEntry DebugTableDirectory { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000045FA File Offset: 0x000027FA
		public DirectoryEntry CopyrightTableDirectory { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00004602 File Offset: 0x00002802
		public DirectoryEntry GlobalPointerTableDirectory { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000460A File Offset: 0x0000280A
		public DirectoryEntry ThreadLocalStorageTableDirectory { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00004612 File Offset: 0x00002812
		public DirectoryEntry LoadConfigTableDirectory { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000461A File Offset: 0x0000281A
		public DirectoryEntry BoundImportTableDirectory { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00004622 File Offset: 0x00002822
		public DirectoryEntry ImportAddressTableDirectory { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000462A File Offset: 0x0000282A
		public DirectoryEntry DelayImportTableDirectory { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00004632 File Offset: 0x00002832
		public DirectoryEntry CorHeaderTableDirectory { get; }

		// Token: 0x060001E1 RID: 481 RVA: 0x0000463A File Offset: 0x0000283A
		internal static int Size(bool is32Bit)
		{
			return 72 + 4 * (is32Bit ? 4 : 8) + 4 + 4 + 128;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00004654 File Offset: 0x00002854
		internal PEHeader(ref PEBinaryReader reader)
		{
			PEMagic pemagic = (PEMagic)reader.ReadUInt16();
			if (pemagic != PEMagic.PE32 && pemagic != PEMagic.PE32Plus)
			{
				throw new BadImageFormatException("UnknownPEMagicValue");
			}
			this.Magic = pemagic;
			this.MajorLinkerVersion = reader.ReadByte();
			this.MinorLinkerVersion = reader.ReadByte();
			this.SizeOfCode = reader.ReadInt32();
			this.SizeOfInitializedData = reader.ReadInt32();
			this.SizeOfUninitializedData = reader.ReadInt32();
			this.AddressOfEntryPoint = reader.ReadInt32();
			this.BaseOfCode = reader.ReadInt32();
			if (pemagic == PEMagic.PE32Plus)
			{
				this.BaseOfData = 0;
			}
			else
			{
				this.BaseOfData = reader.ReadInt32();
			}
			if (pemagic == PEMagic.PE32Plus)
			{
				this.ImageBase = reader.ReadUInt64();
			}
			else
			{
				this.ImageBase = (ulong)reader.ReadUInt32();
			}
			this.SectionAlignment = reader.ReadInt32();
			this.FileAlignment = reader.ReadInt32();
			this.MajorOperatingSystemVersion = reader.ReadUInt16();
			this.MinorOperatingSystemVersion = reader.ReadUInt16();
			this.MajorImageVersion = reader.ReadUInt16();
			this.MinorImageVersion = reader.ReadUInt16();
			this.MajorSubsystemVersion = reader.ReadUInt16();
			this.MinorSubsystemVersion = reader.ReadUInt16();
			reader.ReadUInt32();
			this.SizeOfImage = reader.ReadInt32();
			this.SizeOfHeaders = reader.ReadInt32();
			this.CheckSum = reader.ReadUInt32();
			this.Subsystem = reader.ReadUInt16();
			this.DllCharacteristics = reader.ReadUInt16();
			if (pemagic == PEMagic.PE32Plus)
			{
				this.SizeOfStackReserve = reader.ReadUInt64();
				this.SizeOfStackCommit = reader.ReadUInt64();
				this.SizeOfHeapReserve = reader.ReadUInt64();
				this.SizeOfHeapCommit = reader.ReadUInt64();
			}
			else
			{
				this.SizeOfStackReserve = (ulong)reader.ReadUInt32();
				this.SizeOfStackCommit = (ulong)reader.ReadUInt32();
				this.SizeOfHeapReserve = (ulong)reader.ReadUInt32();
				this.SizeOfHeapCommit = (ulong)reader.ReadUInt32();
			}
			reader.ReadUInt32();
			this.NumberOfRvaAndSizes = reader.ReadInt32();
			this.ExportTableDirectory = new DirectoryEntry(ref reader);
			this.ImportTableDirectory = new DirectoryEntry(ref reader);
			this.ResourceTableDirectory = new DirectoryEntry(ref reader);
			this.ExceptionTableDirectory = new DirectoryEntry(ref reader);
			this.CertificateTableDirectory = new DirectoryEntry(ref reader);
			this.BaseRelocationTableDirectory = new DirectoryEntry(ref reader);
			this.DebugTableDirectory = new DirectoryEntry(ref reader);
			this.CopyrightTableDirectory = new DirectoryEntry(ref reader);
			this.GlobalPointerTableDirectory = new DirectoryEntry(ref reader);
			this.ThreadLocalStorageTableDirectory = new DirectoryEntry(ref reader);
			this.LoadConfigTableDirectory = new DirectoryEntry(ref reader);
			this.BoundImportTableDirectory = new DirectoryEntry(ref reader);
			this.ImportAddressTableDirectory = new DirectoryEntry(ref reader);
			this.DelayImportTableDirectory = new DirectoryEntry(ref reader);
			this.CorHeaderTableDirectory = new DirectoryEntry(ref reader);
			new DirectoryEntry(ref reader);
		}

		// Token: 0x040002CC RID: 716
		internal const int OffsetOfChecksum = 64;
	}
}
