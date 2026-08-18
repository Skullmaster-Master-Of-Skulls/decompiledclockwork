using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Internal;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000014 RID: 20
	internal sealed class PEBuilder
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004F48 File Offset: 0x00003148
		public Machine Machine { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004F50 File Offset: 0x00003150
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00004F58 File Offset: 0x00003158
		public Characteristics ImageCharacteristics { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004F61 File Offset: 0x00003161
		public bool IsDeterministic { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00004F69 File Offset: 0x00003169
		public byte MajorLinkerVersion { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004F71 File Offset: 0x00003171
		public byte MinorLinkerVersion { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004F79 File Offset: 0x00003179
		public ulong ImageBase { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004F81 File Offset: 0x00003181
		public int SectionAlignment { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00004F89 File Offset: 0x00003189
		public int FileAlignment { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004F91 File Offset: 0x00003191
		public ushort MajorOperatingSystemVersion { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00004F99 File Offset: 0x00003199
		public ushort MinorOperatingSystemVersion { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004FA1 File Offset: 0x000031A1
		public ushort MajorImageVersion { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00004FA9 File Offset: 0x000031A9
		public ushort MinorImageVersion { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00004FB1 File Offset: 0x000031B1
		public ushort MajorSubsystemVersion { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00004FB9 File Offset: 0x000031B9
		public ushort MinorSubsystemVersion { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00004FC1 File Offset: 0x000031C1
		public Subsystem Subsystem { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00004FC9 File Offset: 0x000031C9
		public DllCharacteristics DllCharacteristics { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004FD1 File Offset: 0x000031D1
		public ulong SizeOfStackReserve { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00004FD9 File Offset: 0x000031D9
		public ulong SizeOfStackCommit { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00004FE1 File Offset: 0x000031E1
		public ulong SizeOfHeapReserve { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00004FE9 File Offset: 0x000031E9
		public ulong SizeOfHeapCommit { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00004FF1 File Offset: 0x000031F1
		public Func<BlobBuilder, ContentId> IdProvider { get; }

		// Token: 0x06000151 RID: 337 RVA: 0x00004FFC File Offset: 0x000031FC
		public PEBuilder(Machine machine, int sectionAlignment, int fileAlignment, ulong imageBase, byte majorLinkerVersion, byte minorLinkerVersion, ushort majorOperatingSystemVersion, ushort minorOperatingSystemVersion, ushort majorImageVersion, ushort minorImageVersion, ushort majorSubsystemVersion, ushort minorSubsystemVersion, Subsystem subsystem, DllCharacteristics dllCharacteristics, Characteristics imageCharacteristics, ulong sizeOfStackReserve, ulong sizeOfStackCommit, ulong sizeOfHeapReserve, ulong sizeOfHeapCommit, Func<BlobBuilder, ContentId> deterministicIdProvider = null)
		{
			this.Machine = machine;
			this.SectionAlignment = sectionAlignment;
			this.FileAlignment = fileAlignment;
			this.ImageBase = imageBase;
			this.MajorLinkerVersion = majorLinkerVersion;
			this.MinorLinkerVersion = minorLinkerVersion;
			this.MajorOperatingSystemVersion = majorOperatingSystemVersion;
			this.MinorOperatingSystemVersion = minorOperatingSystemVersion;
			this.MajorImageVersion = majorImageVersion;
			this.MinorImageVersion = minorImageVersion;
			this.MajorSubsystemVersion = majorSubsystemVersion;
			this.MinorSubsystemVersion = minorSubsystemVersion;
			this.Subsystem = subsystem;
			this.DllCharacteristics = dllCharacteristics;
			this.ImageCharacteristics = imageCharacteristics;
			this.SizeOfStackReserve = sizeOfStackReserve;
			this.SizeOfStackCommit = sizeOfStackCommit;
			this.SizeOfHeapReserve = sizeOfHeapReserve;
			this.SizeOfHeapCommit = sizeOfHeapCommit;
			this.IsDeterministic = (deterministicIdProvider != null);
			this.IdProvider = (deterministicIdProvider ?? PEBuilder.GetCurrentTimeBasedIdProvider());
			this._sections = new List<PEBuilder.Section>();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000050CC File Offset: 0x000032CC
		private static Func<BlobBuilder, ContentId> GetCurrentTimeBasedIdProvider()
		{
			int timestamp = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
			return (BlobBuilder content) => new ContentId(Guid.NewGuid(), timestamp);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000510E File Offset: 0x0000330E
		private bool Is32Bit
		{
			get
			{
				return this.Machine != Machine.Amd64 && this.Machine != Machine.IA64;
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000512F File Offset: 0x0000332F
		public void AddSection(string name, SectionCharacteristics characteristics, Func<PESectionLocation, BlobBuilder> builder)
		{
			this._sections.Add(new PEBuilder.Section(name, characteristics, builder));
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005144 File Offset: 0x00003344
		public void Serialize(BlobBuilder builder, PEDirectoriesBuilder headers, out ContentId contentId)
		{
			ImmutableArray<PEBuilder.SerializedSection> immutableArray = this.SerializeSections();
			this.WritePESignature(builder);
			Blob blob;
			this.WriteCoffHeader(builder, immutableArray, out blob);
			this.WritePEHeader(builder, headers, immutableArray);
			this.WriteSectionHeaders(builder, immutableArray);
			builder.Align(this.FileAlignment);
			foreach (PEBuilder.SerializedSection serializedSection in immutableArray)
			{
				builder.LinkSuffix(serializedSection.Builder);
				builder.Align(this.FileAlignment);
			}
			contentId = this.IdProvider(builder);
			BlobWriter blobWriter = new BlobWriter(blob);
			blobWriter.WriteBytes(contentId.Stamp);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000051E4 File Offset: 0x000033E4
		private ImmutableArray<PEBuilder.SerializedSection> SerializeSections()
		{
			ImmutableArray<PEBuilder.SerializedSection>.Builder builder = ImmutableArray.CreateBuilder<PEBuilder.SerializedSection>(this._sections.Count);
			int position = PEBuilder.ComputeSizeOfPeHeaders(this._sections.Count, this.Is32Bit);
			int relativeVirtualAddress = BitArithmetic.Align(position, this.SectionAlignment);
			int pointerToRawData = BitArithmetic.Align(position, this.FileAlignment);
			foreach (PEBuilder.Section section in this._sections)
			{
				BlobBuilder blobBuilder = section.Builder(new PESectionLocation(relativeVirtualAddress, pointerToRawData));
				PEBuilder.SerializedSection serializedSection = new PEBuilder.SerializedSection(blobBuilder, section.Name, section.Characteristics, relativeVirtualAddress, BitArithmetic.Align(blobBuilder.Count, this.FileAlignment), pointerToRawData);
				builder.Add(serializedSection);
				relativeVirtualAddress = BitArithmetic.Align(serializedSection.RelativeVirtualAddress + serializedSection.VirtualSize, this.SectionAlignment);
				pointerToRawData = serializedSection.PointerToRawData + serializedSection.SizeOfRawData;
			}
			return builder.MoveToImmutable();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000052F0 File Offset: 0x000034F0
		private static int ComputeSizeOfPeHeaders(int sectionCount, bool is32Bit)
		{
			int num = 376 + 40 * sectionCount;
			if (!is32Bit)
			{
				num += 16;
			}
			return num;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005311 File Offset: 0x00003511
		private void WritePESignature(BlobBuilder builder)
		{
			builder.WriteBytes(PEBuilder.s_dosHeader);
			builder.WriteUInt32(17744U);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000532C File Offset: 0x0000352C
		private void WriteCoffHeader(BlobBuilder builder, ImmutableArray<PEBuilder.SerializedSection> sections, out Blob stampFixup)
		{
			builder.WriteUInt16((ushort)((this.Machine == Machine.Unknown) ? Machine.I386 : this.Machine));
			builder.WriteUInt16((ushort)sections.Length);
			stampFixup = builder.ReserveBytes(4);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt16(this.Is32Bit ? 224 : 240);
			builder.WriteUInt16((ushort)this.ImageCharacteristics);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000053A4 File Offset: 0x000035A4
		private void WritePEHeader(BlobBuilder builder, PEDirectoriesBuilder headers, ImmutableArray<PEBuilder.SerializedSection> sections)
		{
			builder.WriteUInt16(this.Is32Bit ? 267 : 523);
			builder.WriteByte(this.MajorLinkerVersion);
			builder.WriteByte(this.MinorLinkerVersion);
			builder.WriteUInt32((uint)PEBuilder.SumRawDataSizes(sections, SectionCharacteristics.ContainsCode));
			builder.WriteUInt32((uint)PEBuilder.SumRawDataSizes(sections, SectionCharacteristics.ContainsInitializedData));
			builder.WriteUInt32((uint)PEBuilder.SumRawDataSizes(sections, SectionCharacteristics.ContainsUninitializedData));
			builder.WriteUInt32((uint)headers.AddressOfEntryPoint);
			int num = PEBuilder.IndexOfSection(sections, SectionCharacteristics.ContainsCode);
			builder.WriteUInt32((uint)((num != -1) ? sections[num].RelativeVirtualAddress : 0));
			if (this.Is32Bit)
			{
				int num2 = PEBuilder.IndexOfSection(sections, SectionCharacteristics.ContainsInitializedData);
				builder.WriteUInt32((uint)((num2 != -1) ? sections[num2].RelativeVirtualAddress : 0));
				builder.WriteUInt32((uint)this.ImageBase);
			}
			else
			{
				builder.WriteUInt64(this.ImageBase);
			}
			builder.WriteUInt32((uint)this.SectionAlignment);
			builder.WriteUInt32((uint)this.FileAlignment);
			builder.WriteUInt16(this.MajorOperatingSystemVersion);
			builder.WriteUInt16(this.MinorOperatingSystemVersion);
			builder.WriteUInt16(this.MajorImageVersion);
			builder.WriteUInt16(this.MinorImageVersion);
			builder.WriteUInt16(this.MajorSubsystemVersion);
			builder.WriteUInt16(this.MinorSubsystemVersion);
			builder.WriteUInt32(0U);
			PEBuilder.SerializedSection serializedSection = sections[sections.Length - 1];
			builder.WriteUInt32((uint)BitArithmetic.Align(serializedSection.RelativeVirtualAddress + serializedSection.VirtualSize, this.SectionAlignment));
			builder.WriteUInt32((uint)BitArithmetic.Align(PEBuilder.ComputeSizeOfPeHeaders(sections.Length, this.Is32Bit), this.FileAlignment));
			builder.WriteUInt32(0U);
			builder.WriteUInt16((ushort)this.Subsystem);
			builder.WriteUInt16((ushort)this.DllCharacteristics);
			if (this.Is32Bit)
			{
				builder.WriteUInt32((uint)this.SizeOfStackReserve);
				builder.WriteUInt32((uint)this.SizeOfStackCommit);
				builder.WriteUInt32((uint)this.SizeOfHeapReserve);
				builder.WriteUInt32((uint)this.SizeOfHeapCommit);
			}
			else
			{
				builder.WriteUInt64(this.SizeOfStackReserve);
				builder.WriteUInt64(this.SizeOfStackCommit);
				builder.WriteUInt64(this.SizeOfHeapReserve);
				builder.WriteUInt64(this.SizeOfHeapCommit);
			}
			builder.WriteUInt32(0U);
			builder.WriteUInt32(16U);
			builder.WriteUInt32((uint)headers.ExportTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.ExportTable.Size);
			builder.WriteUInt32((uint)headers.ImportTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.ImportTable.Size);
			builder.WriteUInt32((uint)headers.ResourceTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.ResourceTable.Size);
			builder.WriteUInt32((uint)headers.ExceptionTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.ExceptionTable.Size);
			builder.WriteUInt32((uint)headers.CertificateTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.CertificateTable.Size);
			builder.WriteUInt32((uint)headers.BaseRelocationTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.BaseRelocationTable.Size);
			builder.WriteUInt32((uint)headers.DebugTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.DebugTable.Size);
			builder.WriteUInt32((uint)headers.CopyrightTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.CopyrightTable.Size);
			builder.WriteUInt32((uint)headers.GlobalPointerTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.GlobalPointerTable.Size);
			builder.WriteUInt32((uint)headers.ThreadLocalStorageTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.ThreadLocalStorageTable.Size);
			builder.WriteUInt32((uint)headers.LoadConfigTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.LoadConfigTable.Size);
			builder.WriteUInt32((uint)headers.BoundImportTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.BoundImportTable.Size);
			builder.WriteUInt32((uint)headers.ImportAddressTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.ImportAddressTable.Size);
			builder.WriteUInt32((uint)headers.DelayImportTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.DelayImportTable.Size);
			builder.WriteUInt32((uint)headers.CorHeaderTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)headers.CorHeaderTable.Size);
			builder.WriteUInt64(0UL);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000057E0 File Offset: 0x000039E0
		private void WriteSectionHeaders(BlobBuilder builder, ImmutableArray<PEBuilder.SerializedSection> serializedSections)
		{
			for (int i = 0; i < serializedSections.Length; i++)
			{
				PEBuilder.WriteSectionHeader(builder, this._sections[i], serializedSections[i]);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000581C File Offset: 0x00003A1C
		private static void WriteSectionHeader(BlobBuilder builder, PEBuilder.Section section, PEBuilder.SerializedSection serializedSection)
		{
			if (serializedSection.VirtualSize == 0)
			{
				return;
			}
			int i = 0;
			int length = section.Name.Length;
			while (i < 8)
			{
				if (i < length)
				{
					builder.WriteByte((byte)section.Name[i]);
				}
				else
				{
					builder.WriteByte(0);
				}
				i++;
			}
			builder.WriteUInt32((uint)serializedSection.VirtualSize);
			builder.WriteUInt32((uint)serializedSection.RelativeVirtualAddress);
			builder.WriteUInt32((uint)serializedSection.SizeOfRawData);
			builder.WriteUInt32((uint)serializedSection.PointerToRawData);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt16(0);
			builder.WriteUInt16(0);
			builder.WriteUInt32((uint)section.Characteristics);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000058C4 File Offset: 0x00003AC4
		private static int IndexOfSection(ImmutableArray<PEBuilder.SerializedSection> sections, SectionCharacteristics characteristics)
		{
			for (int i = 0; i < sections.Length; i++)
			{
				if ((sections[i].Characteristics & characteristics) == characteristics)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000058F8 File Offset: 0x00003AF8
		private static int SumRawDataSizes(ImmutableArray<PEBuilder.SerializedSection> sections, SectionCharacteristics characteristics)
		{
			int num = 0;
			for (int i = 0; i < sections.Length; i++)
			{
				if ((sections[i].Characteristics & characteristics) == characteristics)
				{
					num += sections[i].SizeOfRawData;
				}
			}
			return num;
		}

		// Token: 0x04000076 RID: 118
		private readonly List<PEBuilder.Section> _sections;

		// Token: 0x04000077 RID: 119
		private static readonly byte[] s_dosHeader = new byte[]
		{
			77,
			90,
			144,
			0,
			3,
			0,
			0,
			0,
			4,
			0,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			0,
			184,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			64,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			128,
			0,
			0,
			0,
			14,
			31,
			186,
			14,
			0,
			180,
			9,
			205,
			33,
			184,
			1,
			76,
			205,
			33,
			84,
			104,
			105,
			115,
			32,
			112,
			114,
			111,
			103,
			114,
			97,
			109,
			32,
			99,
			97,
			110,
			110,
			111,
			116,
			32,
			98,
			101,
			32,
			114,
			117,
			110,
			32,
			105,
			110,
			32,
			68,
			79,
			83,
			32,
			109,
			111,
			100,
			101,
			46,
			13,
			13,
			10,
			36,
			0,
			0,
			0,
			0,
			0,
			0,
			0
		};

		// Token: 0x0200016F RID: 367
		private struct Section
		{
			// Token: 0x06000B69 RID: 2921 RVA: 0x00020D2D File Offset: 0x0001EF2D
			public Section(string name, SectionCharacteristics characteristics, Func<PESectionLocation, BlobBuilder> builder)
			{
				this.Name = name;
				this.Characteristics = characteristics;
				this.Builder = builder;
			}

			// Token: 0x04000956 RID: 2390
			public readonly string Name;

			// Token: 0x04000957 RID: 2391
			public readonly SectionCharacteristics Characteristics;

			// Token: 0x04000958 RID: 2392
			public readonly Func<PESectionLocation, BlobBuilder> Builder;
		}

		// Token: 0x02000170 RID: 368
		private struct SerializedSection
		{
			// Token: 0x06000B6A RID: 2922 RVA: 0x00020D44 File Offset: 0x0001EF44
			public SerializedSection(BlobBuilder builder, string name, SectionCharacteristics characteristics, int relativeVirtualAddress, int sizeOfRawData, int pointerToRawData)
			{
				this.Name = name;
				this.Characteristics = characteristics;
				this.Builder = builder;
				this.RelativeVirtualAddress = relativeVirtualAddress;
				this.SizeOfRawData = sizeOfRawData;
				this.PointerToRawData = pointerToRawData;
			}

			// Token: 0x170002C4 RID: 708
			// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00020D73 File Offset: 0x0001EF73
			public int VirtualSize
			{
				get
				{
					return this.Builder.Count;
				}
			}

			// Token: 0x04000959 RID: 2393
			public readonly BlobBuilder Builder;

			// Token: 0x0400095A RID: 2394
			public readonly string Name;

			// Token: 0x0400095B RID: 2395
			public readonly SectionCharacteristics Characteristics;

			// Token: 0x0400095C RID: 2396
			public readonly int RelativeVirtualAddress;

			// Token: 0x0400095D RID: 2397
			public readonly int SizeOfRawData;

			// Token: 0x0400095E RID: 2398
			public readonly int PointerToRawData;
		}
	}
}
