using System;
using System.Reflection.Internal;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000013 RID: 19
	internal sealed class ManagedTextSection
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004762 File Offset: 0x00002962
		public Characteristics ImageCharacteristics { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000476A File Offset: 0x0000296A
		public Machine Machine { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004772 File Offset: 0x00002972
		public bool IsDeterministic { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000477A File Offset: 0x0000297A
		public string PdbPathOpt { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004782 File Offset: 0x00002982
		public int MetadataSize { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000478A File Offset: 0x0000298A
		public int ILStreamSize { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004792 File Offset: 0x00002992
		public int MappedFieldDataSize { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000479A File Offset: 0x0000299A
		public int ResourceDataSize { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000047A2 File Offset: 0x000029A2
		public int StrongNameSignatureSize { get; }

		// Token: 0x06000119 RID: 281 RVA: 0x000047AC File Offset: 0x000029AC
		public ManagedTextSection(int metadataSize, int ilStreamSize, int mappedFieldDataSize, int resourceDataSize, int strongNameSignatureSize, Characteristics imageCharacteristics, Machine machine, string pdbPathOpt, bool isDeterministic)
		{
			this.MetadataSize = metadataSize;
			this.ResourceDataSize = resourceDataSize;
			this.ILStreamSize = ilStreamSize;
			this.MappedFieldDataSize = mappedFieldDataSize;
			this.StrongNameSignatureSize = strongNameSignatureSize;
			this.ImageCharacteristics = imageCharacteristics;
			this.Machine = machine;
			this.PdbPathOpt = pdbPathOpt;
			this.IsDeterministic = isDeterministic;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00004804 File Offset: 0x00002A04
		internal bool RequiresStartupStub
		{
			get
			{
				return this.Machine == Machine.I386 || this.Machine == Machine.Unknown;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000481E File Offset: 0x00002A1E
		internal bool Requires64bits
		{
			get
			{
				return this.Machine == Machine.Amd64 || this.Machine == Machine.IA64;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000483C File Offset: 0x00002A3C
		public bool Is32Bit
		{
			get
			{
				return !this.Requires64bits;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00004847 File Offset: 0x00002A47
		private string CorEntryPointName
		{
			get
			{
				if ((this.ImageCharacteristics & Characteristics.Dll) == (Characteristics)0)
				{
					return "_CorExeMain";
				}
				return "_CorDllMain";
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004862 File Offset: 0x00002A62
		private int SizeOfImportAddressTable
		{
			get
			{
				if (!this.RequiresStartupStub)
				{
					return 0;
				}
				if (!this.Is32Bit)
				{
					return 16;
				}
				return 8;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000487A File Offset: 0x00002A7A
		private int SizeOfImportTable
		{
			get
			{
				return 40 + (this.Is32Bit ? 12 : 16) + 2 + this.CorEntryPointName.Length + 1;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000489D File Offset: 0x00002A9D
		private static int SizeOfNameTable
		{
			get
			{
				return "mscoree.dll".Length + 1 + 2;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000048AD File Offset: 0x00002AAD
		private int SizeOfRuntimeStartupStub
		{
			get
			{
				if (!this.Is32Bit)
				{
					return 16;
				}
				return 8;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000048BC File Offset: 0x00002ABC
		public int CalculateOffsetToMappedFieldDataStream()
		{
			int num = this.ComputeOffsetToImportTable();
			if (this.RequiresStartupStub)
			{
				num += this.SizeOfImportTable + ManagedTextSection.SizeOfNameTable;
				num = BitArithmetic.Align(num, this.Is32Bit ? 4 : 8);
				num += this.SizeOfRuntimeStartupStub;
			}
			return num;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004904 File Offset: 0x00002B04
		private int ComputeOffsetToDebugTable()
		{
			return this.ComputeOffsetToMetadata() + this.MetadataSize + this.ResourceDataSize + this.StrongNameSignatureSize;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004921 File Offset: 0x00002B21
		private int ComputeOffsetToImportTable()
		{
			return this.ComputeOffsetToDebugTable() + this.ComputeSizeOfDebugDirectory();
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004930 File Offset: 0x00002B30
		public int OffsetToILStream
		{
			get
			{
				return this.SizeOfImportAddressTable + 72;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000493B File Offset: 0x00002B3B
		private int ComputeOffsetToMetadata()
		{
			return this.OffsetToILStream + BitArithmetic.Align(this.ILStreamSize, 4);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004950 File Offset: 0x00002B50
		private bool EmitPdb
		{
			get
			{
				return this.PdbPathOpt != null;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000495B File Offset: 0x00002B5B
		private int MinPdbPath
		{
			get
			{
				if (!this.IsDeterministic)
				{
					return 260;
				}
				return 0;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000496C File Offset: 0x00002B6C
		private int ImageDebugDirectoryBaseSize
		{
			get
			{
				return (this.IsDeterministic ? 28 : 0) + (this.EmitPdb ? 28 : 0);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004989 File Offset: 0x00002B89
		private int ComputeSizeOfDebugDirectoryData()
		{
			if (this.EmitPdb)
			{
				return 24 + Math.Max(BlobUtilities.GetUTF8ByteCount(this.PdbPathOpt) + 1, this.MinPdbPath);
			}
			return 0;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000049B0 File Offset: 0x00002BB0
		private int ComputeSizeOfDebugDirectory()
		{
			return this.ImageDebugDirectoryBaseSize + this.ComputeSizeOfDebugDirectoryData();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000049C0 File Offset: 0x00002BC0
		public DirectoryEntry GetDebugDirectoryEntry(int rva)
		{
			if (!this.EmitPdb && !this.IsDeterministic)
			{
				return default(DirectoryEntry);
			}
			return new DirectoryEntry(rva + this.ComputeOffsetToDebugTable(), this.ImageDebugDirectoryBaseSize);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000049FA File Offset: 0x00002BFA
		public int ComputeSizeOfTextSection()
		{
			return this.CalculateOffsetToMappedFieldDataStream() + this.MappedFieldDataSize;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004A09 File Offset: 0x00002C09
		public int GetEntryPointAddress(int rva)
		{
			if (!this.RequiresStartupStub)
			{
				return 0;
			}
			return rva + this.CalculateOffsetToMappedFieldDataStream() - (this.Is32Bit ? 6 : 10);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004A2C File Offset: 0x00002C2C
		public DirectoryEntry GetImportAddressTableDirectoryEntry(int rva)
		{
			if (!this.RequiresStartupStub)
			{
				return default(DirectoryEntry);
			}
			return new DirectoryEntry(rva, this.SizeOfImportAddressTable);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004A58 File Offset: 0x00002C58
		public DirectoryEntry GetImportTableDirectoryEntry(int rva)
		{
			if (!this.RequiresStartupStub)
			{
				return default(DirectoryEntry);
			}
			return new DirectoryEntry(rva + this.ComputeOffsetToImportTable(), (this.Is32Bit ? 66 : 70) + 13);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004A95 File Offset: 0x00002C95
		public DirectoryEntry GetCorHeaderDirectoryEntry(int rva)
		{
			return new DirectoryEntry(rva + this.SizeOfImportAddressTable, 72);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004AA8 File Offset: 0x00002CA8
		public void Serialize(BlobBuilder builder, int relativeVirtualAddess, int entryPointTokenOrRelativeVirtualAddress, CorFlags corFlags, ulong baseAddress, BlobBuilder metadataBuilder, BlobBuilder ilBuilder, BlobBuilder mappedFieldDataBuilder, BlobBuilder resourceBuilder, BlobBuilder debugTableBuilderOpt)
		{
			int relativeVirtualAddress = this.GetImportTableDirectoryEntry(relativeVirtualAddess).RelativeVirtualAddress;
			int relativeVirtualAddress2 = this.GetImportAddressTableDirectoryEntry(relativeVirtualAddess).RelativeVirtualAddress;
			if (this.RequiresStartupStub)
			{
				this.WriteImportAddressTable(builder, relativeVirtualAddress);
			}
			this.WriteCorHeader(builder, relativeVirtualAddess, entryPointTokenOrRelativeVirtualAddress, corFlags);
			ilBuilder.Align(4);
			builder.LinkSuffix(ilBuilder);
			builder.LinkSuffix(metadataBuilder);
			builder.LinkSuffix(resourceBuilder);
			builder.WriteBytes(0, this.StrongNameSignatureSize);
			if (debugTableBuilderOpt != null)
			{
				builder.LinkSuffix(debugTableBuilderOpt);
			}
			if (this.RequiresStartupStub)
			{
				this.WriteImportTable(builder, relativeVirtualAddress, relativeVirtualAddress2);
				ManagedTextSection.WriteNameTable(builder);
				this.WriteRuntimeStartupStub(builder, relativeVirtualAddress2, baseAddress);
			}
			builder.LinkSuffix(mappedFieldDataBuilder);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004B4C File Offset: 0x00002D4C
		private void WriteImportAddressTable(BlobBuilder builder, int importTableRva)
		{
			int count = builder.Count;
			int num = importTableRva + 40 + (this.Is32Bit ? 12 : 16);
			if (this.Is32Bit)
			{
				builder.WriteUInt32((uint)num);
				builder.WriteUInt32(0U);
				return;
			}
			builder.WriteUInt64((ulong)num);
			builder.WriteUInt64(0UL);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004B9C File Offset: 0x00002D9C
		private void WriteImportTable(BlobBuilder builder, int importTableRva, int importAddressTableRva)
		{
			int count = builder.Count;
			int num = importTableRva + 40;
			int num2 = num + (this.Is32Bit ? 12 : 16);
			int value = num2 + 12 + 2;
			builder.WriteUInt32((uint)num);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32((uint)value);
			builder.WriteUInt32((uint)importAddressTableRva);
			builder.WriteBytes(0, 20);
			if (this.Is32Bit)
			{
				builder.WriteUInt32((uint)num2);
				builder.WriteUInt32(0U);
				builder.WriteUInt32(0U);
			}
			else
			{
				builder.WriteUInt64((ulong)num2);
				builder.WriteUInt64(0UL);
			}
			builder.WriteUInt16(0);
			foreach (char c in this.CorEntryPointName)
			{
				builder.WriteByte((byte)c);
			}
			builder.WriteByte(0);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004C68 File Offset: 0x00002E68
		private static void WriteNameTable(BlobBuilder builder)
		{
			int count = builder.Count;
			foreach (char c in "mscoree.dll")
			{
				builder.WriteByte((byte)c);
			}
			builder.WriteByte(0);
			builder.WriteUInt16(0);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004CB4 File Offset: 0x00002EB4
		private void WriteCorHeader(BlobBuilder builder, int textSectionRva, int entryPointTokenOrRva, CorFlags corFlags)
		{
			int num = textSectionRva + this.ComputeOffsetToMetadata();
			int num2 = num + this.MetadataSize;
			int num3 = num2 + this.ResourceDataSize;
			int count = builder.Count;
			builder.WriteUInt32(72U);
			builder.WriteUInt16(2);
			builder.WriteUInt16(5);
			builder.WriteUInt32((uint)num);
			builder.WriteUInt32((uint)this.MetadataSize);
			builder.WriteUInt32((uint)corFlags);
			builder.WriteUInt32((uint)entryPointTokenOrRva);
			builder.WriteUInt32((uint)((this.ResourceDataSize == 0) ? 0 : num2));
			builder.WriteUInt32((uint)this.ResourceDataSize);
			builder.WriteUInt32((uint)((this.StrongNameSignatureSize == 0) ? 0 : num3));
			builder.WriteUInt32((uint)this.StrongNameSignatureSize);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004D8F File Offset: 0x00002F8F
		private static void WriteDebugTableEntry(BlobBuilder writer, byte[] stamp, uint version, uint debugType, uint sizeOfData, uint addressOfRawData, uint pointerToRawData)
		{
			writer.WriteUInt32(0U);
			writer.WriteBytes(stamp);
			writer.WriteUInt32(version);
			writer.WriteUInt32(debugType);
			writer.WriteUInt32(sizeOfData);
			writer.WriteUInt32(addressOfRawData);
			writer.WriteUInt32(pointerToRawData);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004DC8 File Offset: 0x00002FC8
		internal void WriteDebugTable(BlobBuilder builder, PESectionLocation textSectionLocation, ContentId nativePdbContentId, ContentId portablePdbContentId)
		{
			int imageDebugDirectoryBaseSize = this.ImageDebugDirectoryBaseSize;
			int sizeOfData = this.ComputeSizeOfDebugDirectoryData();
			if (this.EmitPdb)
			{
				uint num = (uint)(this.ComputeOffsetToDebugTable() + imageDebugDirectoryBaseSize);
				ManagedTextSection.WriteDebugTableEntry(builder, nativePdbContentId.Stamp ?? portablePdbContentId.Stamp, portablePdbContentId.IsDefault ? 0U : 1347223808U, 2U, (uint)sizeOfData, (uint)(textSectionLocation.RelativeVirtualAddress + (int)num), (uint)(textSectionLocation.PointerToRawData + (int)num));
			}
			if (this.IsDeterministic)
			{
				ManagedTextSection.WriteDebugTableEntry(builder, ManagedTextSection.zeroStamp, 0U, 16U, 0U, 0U, 0U);
			}
			if (this.EmitPdb)
			{
				builder.WriteByte(82);
				builder.WriteByte(83);
				builder.WriteByte(68);
				builder.WriteByte(83);
				builder.WriteBytes(nativePdbContentId.Guid ?? portablePdbContentId.Guid);
				builder.WriteUInt32(1U);
				int count = builder.Count;
				builder.WriteUTF8(this.PdbPathOpt, true);
				builder.WriteByte(0);
				builder.WriteBytes(0, Math.Max(0, this.MinPdbPath - (builder.Count - count)));
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004EC8 File Offset: 0x000030C8
		private void WriteRuntimeStartupStub(BlobBuilder sectionBuilder, int importAddressTableRva, ulong baseAddress)
		{
			if (this.Is32Bit)
			{
				sectionBuilder.Align(4);
				sectionBuilder.WriteUInt16(0);
				sectionBuilder.WriteByte(byte.MaxValue);
				sectionBuilder.WriteByte(37);
				sectionBuilder.WriteUInt32((uint)(importAddressTableRva + (int)((uint)baseAddress)));
				return;
			}
			sectionBuilder.Align(8);
			sectionBuilder.WriteUInt32(0U);
			sectionBuilder.WriteUInt16(0);
			sectionBuilder.WriteByte(byte.MaxValue);
			sectionBuilder.WriteByte(37);
			sectionBuilder.WriteUInt64((ulong)((long)importAddressTableRva + (long)baseAddress));
		}

		// Token: 0x0400005B RID: 91
		public const int ManagedResourcesDataAlignment = 8;

		// Token: 0x0400005C RID: 92
		private const string CorEntryPointDll = "mscoree.dll";

		// Token: 0x0400005D RID: 93
		public const int MappedFieldDataAlignment = 8;

		// Token: 0x0400005E RID: 94
		private const int CorHeaderSize = 72;

		// Token: 0x0400005F RID: 95
		private const int ImageDebugDirectoryEntrySize = 28;

		// Token: 0x04000060 RID: 96
		private static readonly byte[] zeroStamp = new byte[4];
	}
}
