using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Internal;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200004B RID: 75
	internal sealed class PEHeaders
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00004903 File Offset: 0x00002B03
		public PEHeaders(Stream peStream) : this(peStream, 0)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000490D File Offset: 0x00002B0D
		public PEHeaders(Stream peStream, int size) : this(peStream, size, false)
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00004918 File Offset: 0x00002B18
		public PEHeaders(Stream peStream, int size, bool isLoadedImage)
		{
			if (peStream == null)
			{
				throw new ArgumentNullException("peStream");
			}
			if (!peStream.CanRead || !peStream.CanSeek)
			{
				throw new ArgumentException("StreamMustSupportReadAndSeek", "peStream");
			}
			this._isLoadedImage = isLoadedImage;
			int andValidateSize = StreamExtensions.GetAndValidateSize(peStream, size, "peStream");
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00004A20 File Offset: 0x00002C20
		public int MetadataStartOffset
		{
			get
			{
				return this._metadataStartOffset;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00004A28 File Offset: 0x00002C28
		public int MetadataSize
		{
			get
			{
				return this._metadataSize;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00004A30 File Offset: 0x00002C30
		public CoffHeader CoffHeader
		{
			get
			{
				return this._coffHeader;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00004A38 File Offset: 0x00002C38
		public int CoffHeaderStartOffset
		{
			get
			{
				return this._coffHeaderStartOffset;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00004A40 File Offset: 0x00002C40
		public bool IsCoffOnly
		{
			get
			{
				return this._peHeader == null;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00004A4B File Offset: 0x00002C4B
		public PEHeader PEHeader
		{
			get
			{
				return this._peHeader;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00004A53 File Offset: 0x00002C53
		public int PEHeaderStartOffset
		{
			get
			{
				return this._peHeaderStartOffset;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00004A5B File Offset: 0x00002C5B
		public ImmutableArray<SectionHeader> SectionHeaders
		{
			get
			{
				return this._sectionHeaders;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00004A63 File Offset: 0x00002C63
		public CorHeader CorHeader
		{
			get
			{
				return this._corHeader;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00004A6B File Offset: 0x00002C6B
		public int CorHeaderStartOffset
		{
			get
			{
				return this._corHeaderStartOffset;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00004A73 File Offset: 0x00002C73
		public bool IsConsoleApplication
		{
			get
			{
				return this._peHeader != null && this._peHeader.Subsystem == Subsystem.WindowsCui;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00004A8D File Offset: 0x00002C8D
		public bool IsDll
		{
			get
			{
				return (this._coffHeader.Characteristics & Characteristics.Dll) > (Characteristics)0;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00004AA3 File Offset: 0x00002CA3
		public bool IsExe
		{
			get
			{
				return (this._coffHeader.Characteristics & Characteristics.Dll) == (Characteristics)0;
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00004ABC File Offset: 0x00002CBC
		private bool TryCalculateCorHeaderOffset(long peStreamSize, out int startOffset)
		{
			if (!this.TryGetDirectoryOffset(this._peHeader.CorHeaderTableDirectory, out startOffset, false))
			{
				startOffset = -1;
				return false;
			}
			int size = this._peHeader.CorHeaderTableDirectory.Size;
			if (size < 72)
			{
				throw new BadImageFormatException("InvalidCorHeaderSize");
			}
			return true;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00004B08 File Offset: 0x00002D08
		private void SkipDosHeader(ref PEBinaryReader reader, out bool isCOFFOnly)
		{
			ushort num = reader.ReadUInt16();
			if (num != 23117)
			{
				if (num == 0 && reader.ReadUInt16() == 65535)
				{
					throw new BadImageFormatException("UnknownFileFormat");
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
				uint num2 = reader.ReadUInt32();
				if (num2 != 17744U)
				{
					throw new BadImageFormatException("InvalidPESignature");
				}
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00004B84 File Offset: 0x00002D84
		private ImmutableArray<SectionHeader> ReadSectionHeaders(ref PEBinaryReader reader)
		{
			int numberOfSections = (int)this._coffHeader.NumberOfSections;
			if (numberOfSections < 0)
			{
				throw new BadImageFormatException("InvalidNumberOfSections");
			}
			ImmutableArray<SectionHeader>.Builder builder = ImmutableArray.CreateBuilder<SectionHeader>(numberOfSections);
			for (int i = 0; i < numberOfSections; i++)
			{
				builder.Add(new SectionHeader(ref reader));
			}
			return builder.MoveToImmutable();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00004BD1 File Offset: 0x00002DD1
		public bool TryGetDirectoryOffset(DirectoryEntry directory, out int offset)
		{
			return this.TryGetDirectoryOffset(directory, out offset, true);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00004BDC File Offset: 0x00002DDC
		internal bool TryGetDirectoryOffset(DirectoryEntry directory, out int offset, bool canCrossSectionBoundary)
		{
			int containingSectionIndex = this.GetContainingSectionIndex(directory.RelativeVirtualAddress);
			if (containingSectionIndex < 0)
			{
				offset = -1;
				return false;
			}
			int num = directory.RelativeVirtualAddress - this._sectionHeaders[containingSectionIndex].VirtualAddress;
			if (!canCrossSectionBoundary && directory.Size > this._sectionHeaders[containingSectionIndex].VirtualSize - num)
			{
				throw new BadImageFormatException("SectionTooSmall");
			}
			offset = (this._isLoadedImage ? directory.RelativeVirtualAddress : (this._sectionHeaders[containingSectionIndex].PointerToRawData + num));
			return true;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00004C7C File Offset: 0x00002E7C
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

		// Token: 0x060001F9 RID: 505 RVA: 0x00004CF4 File Offset: 0x00002EF4
		internal int IndexOfSection(string name)
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

		// Token: 0x060001FA RID: 506 RVA: 0x00004D40 File Offset: 0x00002F40
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
				if (this._isLoadedImage)
				{
					start = this.SectionHeaders[num].VirtualAddress;
					size = this.SectionHeaders[num].VirtualSize;
				}
				else
				{
					start = this.SectionHeaders[num].PointerToRawData;
					size = this.SectionHeaders[num].SizeOfRawData;
				}
			}
			else
			{
				if (this._corHeader == null)
				{
					start = 0;
					size = 0;
					return;
				}
				if (!this.TryGetDirectoryOffset(this._corHeader.MetadataDirectory, out start, false))
				{
					throw new BadImageFormatException("MissingDataDirectory");
				}
				size = this._corHeader.MetadataDirectory.Size;
			}
			if (start < 0 || (long)start >= peImageSize || size <= 0 || (long)start > peImageSize - (long)size)
			{
				throw new BadImageFormatException("InvalidMetadataSectionSpan");
			}
		}

		// Token: 0x040002CD RID: 717
		private readonly CoffHeader _coffHeader;

		// Token: 0x040002CE RID: 718
		private readonly PEHeader _peHeader;

		// Token: 0x040002CF RID: 719
		private readonly ImmutableArray<SectionHeader> _sectionHeaders;

		// Token: 0x040002D0 RID: 720
		private readonly CorHeader _corHeader;

		// Token: 0x040002D1 RID: 721
		private readonly bool _isLoadedImage;

		// Token: 0x040002D2 RID: 722
		private readonly int _metadataStartOffset = -1;

		// Token: 0x040002D3 RID: 723
		private readonly int _metadataSize;

		// Token: 0x040002D4 RID: 724
		private readonly int _coffHeaderStartOffset = -1;

		// Token: 0x040002D5 RID: 725
		private readonly int _corHeaderStartOffset = -1;

		// Token: 0x040002D6 RID: 726
		private readonly int _peHeaderStartOffset = -1;

		// Token: 0x040002D7 RID: 727
		internal const ushort DosSignature = 23117;

		// Token: 0x040002D8 RID: 728
		internal const int PESignatureOffsetLocation = 60;

		// Token: 0x040002D9 RID: 729
		internal const uint PESignature = 17744U;

		// Token: 0x040002DA RID: 730
		internal const int PESignatureSize = 4;
	}
}
