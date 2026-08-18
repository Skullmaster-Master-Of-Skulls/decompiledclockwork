using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000026 RID: 38
	public sealed class PEHeader
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00005FCA File Offset: 0x000041CA
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00005FD2 File Offset: 0x000041D2
		public PEMagic Magic { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00005FDB File Offset: 0x000041DB
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00005FE3 File Offset: 0x000041E3
		public byte MajorLinkerVersion { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00005FEC File Offset: 0x000041EC
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00005FF4 File Offset: 0x000041F4
		public byte MinorLinkerVersion { get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00005FFD File Offset: 0x000041FD
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00006005 File Offset: 0x00004205
		public int SizeOfCode { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000600E File Offset: 0x0000420E
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00006016 File Offset: 0x00004216
		public int SizeOfInitializedData { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000601F File Offset: 0x0000421F
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00006027 File Offset: 0x00004227
		public int SizeOfUninitializedData { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00006030 File Offset: 0x00004230
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00006038 File Offset: 0x00004238
		public int AddressOfEntryPoint { get; private set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00006041 File Offset: 0x00004241
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00006049 File Offset: 0x00004249
		public int BaseOfCode { get; private set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00006052 File Offset: 0x00004252
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000605A File Offset: 0x0000425A
		public int BaseOfData { get; private set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00006063 File Offset: 0x00004263
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000606B File Offset: 0x0000426B
		public ulong ImageBase { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00006074 File Offset: 0x00004274
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000607C File Offset: 0x0000427C
		public int SectionAlignment { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00006085 File Offset: 0x00004285
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000608D File Offset: 0x0000428D
		public int FileAlignment { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00006096 File Offset: 0x00004296
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000609E File Offset: 0x0000429E
		public ushort MajorOperatingSystemVersion { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000060A7 File Offset: 0x000042A7
		// (set) Token: 0x060001EB RID: 491 RVA: 0x000060AF File Offset: 0x000042AF
		public ushort MinorOperatingSystemVersion { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000060B8 File Offset: 0x000042B8
		// (set) Token: 0x060001ED RID: 493 RVA: 0x000060C0 File Offset: 0x000042C0
		public ushort MajorImageVersion { get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000060C9 File Offset: 0x000042C9
		// (set) Token: 0x060001EF RID: 495 RVA: 0x000060D1 File Offset: 0x000042D1
		public ushort MinorImageVersion { get; private set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000060DA File Offset: 0x000042DA
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x000060E2 File Offset: 0x000042E2
		public ushort MajorSubsystemVersion { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000060EB File Offset: 0x000042EB
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x000060F3 File Offset: 0x000042F3
		public ushort MinorSubsystemVersion { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000060FC File Offset: 0x000042FC
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00006104 File Offset: 0x00004304
		public int SizeOfImage { get; private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000610D File Offset: 0x0000430D
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00006115 File Offset: 0x00004315
		public int SizeOfHeaders { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000611E File Offset: 0x0000431E
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00006126 File Offset: 0x00004326
		public uint CheckSum { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000612F File Offset: 0x0000432F
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00006137 File Offset: 0x00004337
		public Subsystem Subsystem { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00006140 File Offset: 0x00004340
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00006148 File Offset: 0x00004348
		public DllCharacteristics DllCharacteristics { get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00006151 File Offset: 0x00004351
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00006159 File Offset: 0x00004359
		public ulong SizeOfStackReserve { get; private set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00006162 File Offset: 0x00004362
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000616A File Offset: 0x0000436A
		public ulong SizeOfStackCommit { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00006173 File Offset: 0x00004373
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000617B File Offset: 0x0000437B
		public ulong SizeOfHeapReserve { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00006184 File Offset: 0x00004384
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000618C File Offset: 0x0000438C
		public ulong SizeOfHeapCommit { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00006195 File Offset: 0x00004395
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000619D File Offset: 0x0000439D
		public int NumberOfRvaAndSizes { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000061A6 File Offset: 0x000043A6
		// (set) Token: 0x06000209 RID: 521 RVA: 0x000061AE File Offset: 0x000043AE
		public DirectoryEntry ExportTableDirectory { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000061B7 File Offset: 0x000043B7
		// (set) Token: 0x0600020B RID: 523 RVA: 0x000061BF File Offset: 0x000043BF
		public DirectoryEntry ImportTableDirectory { get; private set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000061C8 File Offset: 0x000043C8
		// (set) Token: 0x0600020D RID: 525 RVA: 0x000061D0 File Offset: 0x000043D0
		public DirectoryEntry ResourceTableDirectory { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000061D9 File Offset: 0x000043D9
		// (set) Token: 0x0600020F RID: 527 RVA: 0x000061E1 File Offset: 0x000043E1
		public DirectoryEntry ExceptionTableDirectory { get; private set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000061EA File Offset: 0x000043EA
		// (set) Token: 0x06000211 RID: 529 RVA: 0x000061F2 File Offset: 0x000043F2
		public DirectoryEntry CertificateTableDirectory { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000061FB File Offset: 0x000043FB
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00006203 File Offset: 0x00004403
		public DirectoryEntry BaseRelocationTableDirectory { get; private set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000620C File Offset: 0x0000440C
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00006214 File Offset: 0x00004414
		public DirectoryEntry DebugTableDirectory { get; private set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000621D File Offset: 0x0000441D
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00006225 File Offset: 0x00004425
		public DirectoryEntry CopyrightTableDirectory { get; private set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000622E File Offset: 0x0000442E
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00006236 File Offset: 0x00004436
		public DirectoryEntry GlobalPointerTableDirectory { get; private set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000623F File Offset: 0x0000443F
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00006247 File Offset: 0x00004447
		public DirectoryEntry ThreadLocalStorageTableDirectory { get; private set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00006250 File Offset: 0x00004450
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00006258 File Offset: 0x00004458
		public DirectoryEntry LoadConfigTableDirectory { get; private set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00006261 File Offset: 0x00004461
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00006269 File Offset: 0x00004469
		public DirectoryEntry BoundImportTableDirectory { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00006272 File Offset: 0x00004472
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000627A File Offset: 0x0000447A
		public DirectoryEntry ImportAddressTableDirectory { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00006283 File Offset: 0x00004483
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000628B File Offset: 0x0000448B
		public DirectoryEntry DelayImportTableDirectory { get; private set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00006294 File Offset: 0x00004494
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000629C File Offset: 0x0000449C
		public DirectoryEntry CorHeaderTableDirectory { get; private set; }

		// Token: 0x06000226 RID: 550 RVA: 0x000062A8 File Offset: 0x000044A8
		internal PEHeader(ref PEBinaryReader reader)
		{
			PEMagic pemagic = (PEMagic)reader.ReadUInt16();
			if (pemagic != PEMagic.PE32 && pemagic != PEMagic.PE32Plus)
			{
				throw new BadImageFormatException(SR.UnknownPEMagicValue);
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
			this.Subsystem = (Subsystem)reader.ReadUInt16();
			this.DllCharacteristics = (DllCharacteristics)reader.ReadUInt16();
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
	}
}
