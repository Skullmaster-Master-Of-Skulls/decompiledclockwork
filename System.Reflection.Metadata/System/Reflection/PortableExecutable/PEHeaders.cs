using System;
using System.Collections.Immutable;
using System.IO;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000027 RID: 39
	public sealed class PEHeaders
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00006558 File Offset: 0x00004758
		public PEHeaders(Stream peStream) : this(peStream, null)
		{
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00006575 File Offset: 0x00004775
		public PEHeaders(Stream peStream, int size) : this(peStream, new int?(size))
		{
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006584 File Offset: 0x00004784
		private PEHeaders(Stream peStream, int? sizeOpt)
		{
			if (peStream == null)
			{
				throw new ArgumentNullException("peStream");
			}
			if (!peStream.CanRead || !peStream.CanSeek)
			{
				throw new ArgumentException(SR.StreamMustSupportReadAndSeek, "peStream");
			}
			int andValidateSize = PEBinaryReader.GetAndValidateSize(peStream, sizeOpt);
			PEBinaryReader pebinaryReader = new PEBinaryReader(peStream, andValidateSize);
			bool flag;
			this.SkipDosHeader(ref pebinaryReader, out flag);
			this._coffHeaderStartOffset = pebinaryReader.CurrentOffset;
			this._coffHeader = new CoffHeader(ref pebinaryReader);
			if (!flag)
			{
				this._peHeaderStartOffset = pebinaryReader.CurrentOffset;
				this._peHeader = new PEHeader(ref pebinaryReader);
			}
			this._sectionHeaders = this.ReadSectionHeaders(ref pebinaryReader);
			int num;
			if (!flag && this.TryCalculateCorHeaderOffset((long)andValidateSize, out num))
			{
				this._corHeaderStartOffset = num;
				pebinaryReader.Seek(num);
				this._corHeader = new CorHeader(ref pebinaryReader);
			}
			this.CalculateMetadataLocation((long)andValidateSize, out this._metadataStartOffset, out this._metadataSize);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00006680 File Offset: 0x00004880
		public int MetadataStartOffset
		{
			get
			{
				return this._metadataStartOffset;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00006688 File Offset: 0x00004888
		public int MetadataSize
		{
			get
			{
				return this._metadataSize;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00006690 File Offset: 0x00004890
		public CoffHeader CoffHeader
		{
			get
			{
				return this._coffHeader;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00006698 File Offset: 0x00004898
		public int CoffHeaderStartOffset
		{
			get
			{
				return this._coffHeaderStartOffset;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600022E RID: 558 RVA: 0x000066A0 File Offset: 0x000048A0
		public bool IsCoffOnly
		{
			get
			{
				return this._peHeader == null;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000066AB File Offset: 0x000048AB
		public PEHeader PEHeader
		{
			get
			{
				return this._peHeader;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000066B3 File Offset: 0x000048B3
		public int PEHeaderStartOffset
		{
			get
			{
				return this._peHeaderStartOffset;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000066BB File Offset: 0x000048BB
		public ImmutableArray<SectionHeader> SectionHeaders
		{
			get
			{
				return this._sectionHeaders;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000066C3 File Offset: 0x000048C3
		public CorHeader CorHeader
		{
			get
			{
				return this._corHeader;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000066CB File Offset: 0x000048CB
		public int CorHeaderStartOffset
		{
			get
			{
				return this._corHeaderStartOffset;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000066D3 File Offset: 0x000048D3
		public bool IsConsoleApplication
		{
			get
			{
				return this._peHeader != null && this._peHeader.Subsystem == Subsystem.WindowsCui;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000066ED File Offset: 0x000048ED
		public bool IsDll
		{
			get
			{
				return (this._coffHeader.Characteristics & Characteristics.Dll) > (Characteristics)0;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00006703 File Offset: 0x00004903
		public bool IsExe
		{
			get
			{
				return (this._coffHeader.Characteristics & Characteristics.Dll) == (Characteristics)0;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006719 File Offset: 0x00004919
		private bool TryCalculateCorHeaderOffset(long peStreamSize, out int startOffset)
		{
			if (!this.TryGetDirectoryOffset(this._peHeader.CorHeaderTableDirectory, out startOffset))
			{
				startOffset = -1;
				return false;
			}
			if (this._peHeader.CorHeaderTableDirectory.Size < 72)
			{
				throw new BadImageFormatException(SR.InvalidCorHeaderSize);
			}
			return true;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006754 File Offset: 0x00004954
		private void SkipDosHeader(ref PEBinaryReader reader, out bool isCOFFOnly)
		{
			ushort num = reader.ReadUInt16();
			if (num != 23117)
			{
				if (num == 0 && reader.ReadUInt16() == 65535)
				{
					throw new BadImageFormatException(SR.UnknownFileFormat);
				}
				isCOFFOnly = true;
				reader.Seek(0);
			}
			else
			{
				isCOFFOnly = false;
			}
			if (!isCOFFOnly)
			{
				reader.Seek(60);
				int offset = reader.ReadInt32();
				reader.Seek(offset);
				if (reader.ReadUInt32() != 17744U)
				{
					throw new BadImageFormatException(SR.InvalidPESignature);
				}
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000067CC File Offset: 0x000049CC
		private ImmutableArray<SectionHeader> ReadSectionHeaders(ref PEBinaryReader reader)
		{
			int numberOfSections = (int)this._coffHeader.NumberOfSections;
			if (numberOfSections < 0)
			{
				throw new BadImageFormatException(SR.InvalidNumberOfSections);
			}
			ImmutableArray<SectionHeader>.Builder builder = ImmutableArray.CreateBuilder<SectionHeader>(numberOfSections);
			for (int i = 0; i < numberOfSections; i++)
			{
				builder.Add(new SectionHeader(ref reader));
			}
			return builder.ToImmutable();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000681C File Offset: 0x00004A1C
		public bool TryGetDirectoryOffset(DirectoryEntry directory, out int offset)
		{
			int containingSectionIndex = this.GetContainingSectionIndex(directory.RelativeVirtualAddress);
			if (containingSectionIndex < 0)
			{
				offset = -1;
				return false;
			}
			int num = directory.RelativeVirtualAddress - this._sectionHeaders[containingSectionIndex].VirtualAddress;
			if (directory.Size > this._sectionHeaders[containingSectionIndex].VirtualSize - num)
			{
				throw new BadImageFormatException(SR.SectionTooSmall);
			}
			offset = this._sectionHeaders[containingSectionIndex].PointerToRawData + num;
			return true;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000068A8 File Offset: 0x00004AA8
		public int GetContainingSectionIndex(int relativeVirtualAddress)
		{
			for (int i = 0; i < this._sectionHeaders.Length; i++)
			{
				if (this._sectionHeaders[i].VirtualAddress <= relativeVirtualAddress && relativeVirtualAddress < this._sectionHeaders[i].VirtualAddress + this._sectionHeaders[i].VirtualSize)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006920 File Offset: 0x00004B20
		private int IndexOfSection(string name)
		{
			for (int i = 0; i < this.SectionHeaders.Length; i++)
			{
				if (this.SectionHeaders[i].Name.Equals(name, StringComparison.Ordinal))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000696C File Offset: 0x00004B6C
		private void CalculateMetadataLocation(long peImageSize, out int start, out int size)
		{
			if (this.IsCoffOnly)
			{
				int num = this.IndexOfSection(".cormeta");
				if (num == -1)
				{
					start = -1;
					size = 0;
					return;
				}
				start = this.SectionHeaders[num].PointerToRawData;
				size = this.SectionHeaders[num].SizeOfRawData;
			}
			else
			{
				if (this._corHeader == null)
				{
					start = 0;
					size = 0;
					return;
				}
				if (!this.TryGetDirectoryOffset(this._corHeader.MetadataDirectory, out start))
				{
					throw new BadImageFormatException(SR.MissingDataDirectory);
				}
				size = this._corHeader.MetadataDirectory.Size;
			}
			if (start < 0 || (long)start >= peImageSize || size <= 0 || (long)start > peImageSize - (long)size)
			{
				throw new BadImageFormatException(SR.InvalidMetadataSectionSpan);
			}
		}

		// Token: 0x04000169 RID: 361
		private readonly CoffHeader _coffHeader;

		// Token: 0x0400016A RID: 362
		private readonly PEHeader _peHeader;

		// Token: 0x0400016B RID: 363
		private readonly ImmutableArray<SectionHeader> _sectionHeaders;

		// Token: 0x0400016C RID: 364
		private readonly CorHeader _corHeader;

		// Token: 0x0400016D RID: 365
		private readonly int _metadataStartOffset = -1;

		// Token: 0x0400016E RID: 366
		private readonly int _metadataSize;

		// Token: 0x0400016F RID: 367
		private readonly int _coffHeaderStartOffset = -1;

		// Token: 0x04000170 RID: 368
		private readonly int _corHeaderStartOffset = -1;

		// Token: 0x04000171 RID: 369
		private readonly int _peHeaderStartOffset = -1;
	}
}
