using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200002C RID: 44
	public struct SectionHeader
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00007225 File Offset: 0x00005425
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000722D File Offset: 0x0000542D
		public int VirtualSize
		{
			get
			{
				return this._virtualSize;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00007235 File Offset: 0x00005435
		public int VirtualAddress
		{
			get
			{
				return this._virtualAddress;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000723D File Offset: 0x0000543D
		public int SizeOfRawData
		{
			get
			{
				return this._sizeOfRawData;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00007245 File Offset: 0x00005445
		public int PointerToRawData
		{
			get
			{
				return this._pointerToRawData;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000724D File Offset: 0x0000544D
		public int PointerToRelocations
		{
			get
			{
				return this._pointerToRelocations;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00007255 File Offset: 0x00005455
		public int PointerToLineNumbers
		{
			get
			{
				return this._pointerToLineNumbers;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000725D File Offset: 0x0000545D
		public ushort NumberOfRelocations
		{
			get
			{
				return this._numberOfRelocations;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00007265 File Offset: 0x00005465
		public ushort NumberOfLineNumbers
		{
			get
			{
				return this._numberOfLineNumbers;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000726D File Offset: 0x0000546D
		public SectionCharacteristics SectionCharacteristics
		{
			get
			{
				return this._sectionCharacteristics;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00007278 File Offset: 0x00005478
		internal SectionHeader(ref PEBinaryReader reader)
		{
			this._name = reader.ReadNullPaddedUTF8(8);
			this._virtualSize = reader.ReadInt32();
			this._virtualAddress = reader.ReadInt32();
			this._sizeOfRawData = reader.ReadInt32();
			this._pointerToRawData = reader.ReadInt32();
			this._pointerToRelocations = reader.ReadInt32();
			this._pointerToLineNumbers = reader.ReadInt32();
			this._numberOfRelocations = reader.ReadUInt16();
			this._numberOfLineNumbers = reader.ReadUInt16();
			this._sectionCharacteristics = (SectionCharacteristics)reader.ReadUInt32();
		}

		// Token: 0x0400017E RID: 382
		private readonly int _virtualSize;

		// Token: 0x0400017F RID: 383
		private readonly string _name;

		// Token: 0x04000180 RID: 384
		private readonly int _virtualAddress;

		// Token: 0x04000181 RID: 385
		private readonly int _sizeOfRawData;

		// Token: 0x04000182 RID: 386
		private readonly int _pointerToRawData;

		// Token: 0x04000183 RID: 387
		private readonly int _pointerToRelocations;

		// Token: 0x04000184 RID: 388
		private readonly int _pointerToLineNumbers;

		// Token: 0x04000185 RID: 389
		private readonly ushort _numberOfRelocations;

		// Token: 0x04000186 RID: 390
		private readonly ushort _numberOfLineNumbers;

		// Token: 0x04000187 RID: 391
		private readonly SectionCharacteristics _sectionCharacteristics;
	}
}
