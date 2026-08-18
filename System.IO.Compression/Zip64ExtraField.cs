using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x0200000A RID: 10
	internal struct Zip64ExtraField
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000440C File Offset: 0x0000260C
		public ushort TotalSize
		{
			get
			{
				return this._size + 4;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004417 File Offset: 0x00002617
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000441F File Offset: 0x0000261F
		public long? UncompressedSize
		{
			get
			{
				return this._uncompressedSize;
			}
			set
			{
				this._uncompressedSize = value;
				this.UpdateSize();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000442E File Offset: 0x0000262E
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00004436 File Offset: 0x00002636
		public long? CompressedSize
		{
			get
			{
				return this._compressedSize;
			}
			set
			{
				this._compressedSize = value;
				this.UpdateSize();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004445 File Offset: 0x00002645
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000444D File Offset: 0x0000264D
		public long? LocalHeaderOffset
		{
			get
			{
				return this._localHeaderOffset;
			}
			set
			{
				this._localHeaderOffset = value;
				this.UpdateSize();
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000445C File Offset: 0x0000265C
		public int? StartDiskNumber
		{
			get
			{
				return this._startDiskNumber;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004464 File Offset: 0x00002664
		private void UpdateSize()
		{
			this._size = 0;
			if (this._uncompressedSize != null)
			{
				this._size += 8;
			}
			if (this._compressedSize != null)
			{
				this._size += 8;
			}
			if (this._localHeaderOffset != null)
			{
				this._size += 8;
			}
			if (this._startDiskNumber != null)
			{
				this._size += 4;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000044E8 File Offset: 0x000026E8
		public static Zip64ExtraField GetJustZip64Block(Stream extraFieldStream, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber)
		{
			Zip64ExtraField result;
			using (BinaryReader binaryReader = new BinaryReader(extraFieldStream))
			{
				ZipGenericExtraField extraField;
				while (ZipGenericExtraField.TryReadBlock(binaryReader, extraFieldStream.Length, out extraField))
				{
					if (Zip64ExtraField.TryGetZip64BlockFromGenericExtraField(extraField, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber, out result))
					{
						return result;
					}
				}
			}
			result = new Zip64ExtraField
			{
				_compressedSize = null,
				_uncompressedSize = null,
				_localHeaderOffset = null,
				_startDiskNumber = null
			};
			return result;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000457C File Offset: 0x0000277C
		private static bool TryGetZip64BlockFromGenericExtraField(ZipGenericExtraField extraField, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber, out Zip64ExtraField zip64Block)
		{
			zip64Block = default(Zip64ExtraField);
			zip64Block._compressedSize = null;
			zip64Block._uncompressedSize = null;
			zip64Block._localHeaderOffset = null;
			zip64Block._startDiskNumber = null;
			if (extraField.Tag != 1)
			{
				return false;
			}
			MemoryStream memoryStream = null;
			bool result;
			try
			{
				memoryStream = new MemoryStream(extraField.Data);
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					memoryStream = null;
					zip64Block._size = extraField.Size;
					ushort num = 0;
					if (readUncompressedSize)
					{
						num += 8;
					}
					if (readCompressedSize)
					{
						num += 8;
					}
					if (readLocalHeaderOffset)
					{
						num += 8;
					}
					if (readStartDiskNumber)
					{
						num += 4;
					}
					if (num != zip64Block._size)
					{
						result = false;
					}
					else
					{
						if (readUncompressedSize)
						{
							zip64Block._uncompressedSize = new long?(binaryReader.ReadInt64());
						}
						if (readCompressedSize)
						{
							zip64Block._compressedSize = new long?(binaryReader.ReadInt64());
						}
						if (readLocalHeaderOffset)
						{
							zip64Block._localHeaderOffset = new long?(binaryReader.ReadInt64());
						}
						if (readStartDiskNumber)
						{
							zip64Block._startDiskNumber = new int?(binaryReader.ReadInt32());
						}
						long? num2 = zip64Block._uncompressedSize;
						long num3 = 0L;
						if (num2.GetValueOrDefault() < num3 & num2 != null)
						{
							throw new InvalidDataException(Messages.FieldTooBigUncompressedSize);
						}
						num2 = zip64Block._compressedSize;
						num3 = 0L;
						if (num2.GetValueOrDefault() < num3 & num2 != null)
						{
							throw new InvalidDataException(Messages.FieldTooBigCompressedSize);
						}
						num2 = zip64Block._localHeaderOffset;
						num3 = 0L;
						if (num2.GetValueOrDefault() < num3 & num2 != null)
						{
							throw new InvalidDataException(Messages.FieldTooBigLocalHeaderOffset);
						}
						int? startDiskNumber = zip64Block._startDiskNumber;
						int num4 = 0;
						if (startDiskNumber.GetValueOrDefault() < num4 & startDiskNumber != null)
						{
							throw new InvalidDataException(Messages.FieldTooBigStartDiskNumber);
						}
						result = true;
					}
				}
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
			}
			return result;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004780 File Offset: 0x00002980
		public static Zip64ExtraField GetAndRemoveZip64Block(List<ZipGenericExtraField> extraFields, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber)
		{
			Zip64ExtraField zip64Field = default(Zip64ExtraField);
			zip64Field._compressedSize = null;
			zip64Field._uncompressedSize = null;
			zip64Field._localHeaderOffset = null;
			zip64Field._startDiskNumber = null;
			bool zip64FieldFound = false;
			extraFields.RemoveAll(delegate(ZipGenericExtraField ef)
			{
				if (ef.Tag == 1)
				{
					if (!zip64FieldFound && Zip64ExtraField.TryGetZip64BlockFromGenericExtraField(ef, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber, out zip64Field))
					{
						zip64FieldFound = true;
					}
					return true;
				}
				return false;
			});
			return zip64Field;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004820 File Offset: 0x00002A20
		public static void RemoveZip64Blocks(List<ZipGenericExtraField> extraFields)
		{
			extraFields.RemoveAll((ZipGenericExtraField field) => field.Tag == 1);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004848 File Offset: 0x00002A48
		public void WriteBlock(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(1);
			binaryWriter.Write(this._size);
			if (this._uncompressedSize != null)
			{
				binaryWriter.Write(this._uncompressedSize.Value);
			}
			if (this._compressedSize != null)
			{
				binaryWriter.Write(this._compressedSize.Value);
			}
			if (this._localHeaderOffset != null)
			{
				binaryWriter.Write(this._localHeaderOffset.Value);
			}
			if (this._startDiskNumber != null)
			{
				binaryWriter.Write(this._startDiskNumber.Value);
			}
		}

		// Token: 0x0400004A RID: 74
		public const int OffsetToFirstField = 4;

		// Token: 0x0400004B RID: 75
		private const ushort TagConstant = 1;

		// Token: 0x0400004C RID: 76
		private ushort _size;

		// Token: 0x0400004D RID: 77
		private long? _uncompressedSize;

		// Token: 0x0400004E RID: 78
		private long? _compressedSize;

		// Token: 0x0400004F RID: 79
		private long? _localHeaderOffset;

		// Token: 0x04000050 RID: 80
		private int? _startDiskNumber;
	}
}
