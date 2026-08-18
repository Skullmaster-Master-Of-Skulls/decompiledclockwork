using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x0200000E RID: 14
	internal struct ZipCentralDirectoryFileHeader
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00004B58 File Offset: 0x00002D58
		public static bool TryReadBlock(BinaryReader reader, bool saveExtraFieldsAndComments, out ZipCentralDirectoryFileHeader header)
		{
			header = default(ZipCentralDirectoryFileHeader);
			if (reader.ReadUInt32() != 33639248U)
			{
				return false;
			}
			header.VersionMadeBy = reader.ReadUInt16();
			header.VersionNeededToExtract = reader.ReadUInt16();
			header.GeneralPurposeBitFlag = reader.ReadUInt16();
			header.CompressionMethod = reader.ReadUInt16();
			header.LastModified = reader.ReadUInt32();
			header.Crc32 = reader.ReadUInt32();
			uint num = reader.ReadUInt32();
			uint num2 = reader.ReadUInt32();
			header.FilenameLength = reader.ReadUInt16();
			header.ExtraFieldLength = reader.ReadUInt16();
			header.FileCommentLength = reader.ReadUInt16();
			ushort num3 = reader.ReadUInt16();
			header.InternalFileAttributes = reader.ReadUInt16();
			header.ExternalFileAttributes = reader.ReadUInt32();
			uint num4 = reader.ReadUInt32();
			header.Filename = reader.ReadBytes((int)header.FilenameLength);
			bool readUncompressedSize = num2 == uint.MaxValue;
			bool readCompressedSize = num == uint.MaxValue;
			bool readLocalHeaderOffset = num4 == uint.MaxValue;
			bool readStartDiskNumber = num3 == ushort.MaxValue;
			long position = reader.BaseStream.Position + (long)((ulong)header.ExtraFieldLength);
			Zip64ExtraField zip64ExtraField;
			using (Stream stream = new SubReadStream(reader.BaseStream, reader.BaseStream.Position, (long)((ulong)header.ExtraFieldLength)))
			{
				if (saveExtraFieldsAndComments)
				{
					header.ExtraFields = ZipGenericExtraField.ParseExtraField(stream);
					zip64ExtraField = Zip64ExtraField.GetAndRemoveZip64Block(header.ExtraFields, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber);
				}
				else
				{
					header.ExtraFields = null;
					zip64ExtraField = Zip64ExtraField.GetJustZip64Block(stream, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber);
				}
			}
			reader.BaseStream.AdvanceToPosition(position);
			if (saveExtraFieldsAndComments)
			{
				header.FileComment = reader.ReadBytes((int)header.FileCommentLength);
			}
			else
			{
				reader.BaseStream.Position += (long)((ulong)header.FileCommentLength);
				header.FileComment = null;
			}
			header.UncompressedSize = (long)((zip64ExtraField.UncompressedSize == null) ? ((ulong)num2) : ((ulong)zip64ExtraField.UncompressedSize.Value));
			header.CompressedSize = (long)((zip64ExtraField.CompressedSize == null) ? ((ulong)num) : ((ulong)zip64ExtraField.CompressedSize.Value));
			header.RelativeOffsetOfLocalHeader = (long)((zip64ExtraField.LocalHeaderOffset == null) ? ((ulong)num4) : ((ulong)zip64ExtraField.LocalHeaderOffset.Value));
			header.DiskNumberStart = ((zip64ExtraField.StartDiskNumber == null) ? ((int)num3) : zip64ExtraField.StartDiskNumber.Value);
			return true;
		}

		// Token: 0x04000066 RID: 102
		public const uint SignatureConstant = 33639248U;

		// Token: 0x04000067 RID: 103
		public ushort VersionMadeBy;

		// Token: 0x04000068 RID: 104
		public ushort VersionNeededToExtract;

		// Token: 0x04000069 RID: 105
		public ushort GeneralPurposeBitFlag;

		// Token: 0x0400006A RID: 106
		public ushort CompressionMethod;

		// Token: 0x0400006B RID: 107
		public uint LastModified;

		// Token: 0x0400006C RID: 108
		public uint Crc32;

		// Token: 0x0400006D RID: 109
		public long CompressedSize;

		// Token: 0x0400006E RID: 110
		public long UncompressedSize;

		// Token: 0x0400006F RID: 111
		public ushort FilenameLength;

		// Token: 0x04000070 RID: 112
		public ushort ExtraFieldLength;

		// Token: 0x04000071 RID: 113
		public ushort FileCommentLength;

		// Token: 0x04000072 RID: 114
		public int DiskNumberStart;

		// Token: 0x04000073 RID: 115
		public ushort InternalFileAttributes;

		// Token: 0x04000074 RID: 116
		public uint ExternalFileAttributes;

		// Token: 0x04000075 RID: 117
		public long RelativeOffsetOfLocalHeader;

		// Token: 0x04000076 RID: 118
		public byte[] Filename;

		// Token: 0x04000077 RID: 119
		public byte[] FileComment;

		// Token: 0x04000078 RID: 120
		public List<ZipGenericExtraField> ExtraFields;
	}
}
