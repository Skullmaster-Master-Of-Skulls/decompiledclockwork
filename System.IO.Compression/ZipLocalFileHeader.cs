using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x0200000D RID: 13
	internal struct ZipLocalFileHeader
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00004A4C File Offset: 0x00002C4C
		public static List<ZipGenericExtraField> GetExtraFields(BinaryReader reader)
		{
			reader.BaseStream.Seek(26L, SeekOrigin.Current);
			ushort num = reader.ReadUInt16();
			ushort num2 = reader.ReadUInt16();
			reader.BaseStream.Seek((long)((ulong)num), SeekOrigin.Current);
			List<ZipGenericExtraField> list;
			using (Stream stream = new SubReadStream(reader.BaseStream, reader.BaseStream.Position, (long)((ulong)num2)))
			{
				list = ZipGenericExtraField.ParseExtraField(stream);
			}
			Zip64ExtraField.RemoveZip64Blocks(list);
			return list;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004ACC File Offset: 0x00002CCC
		public static bool TrySkipBlock(BinaryReader reader)
		{
			if (reader.ReadUInt32() != 67324752U)
			{
				return false;
			}
			if (reader.BaseStream.Length < reader.BaseStream.Position + 22L)
			{
				return false;
			}
			reader.BaseStream.Seek(22L, SeekOrigin.Current);
			ushort num = reader.ReadUInt16();
			ushort num2 = reader.ReadUInt16();
			if (reader.BaseStream.Length < reader.BaseStream.Position + (long)((ulong)num) + (long)((ulong)num2))
			{
				return false;
			}
			reader.BaseStream.Seek((long)(num + num2), SeekOrigin.Current);
			return true;
		}

		// Token: 0x04000061 RID: 97
		public const uint DataDescriptorSignature = 134695760U;

		// Token: 0x04000062 RID: 98
		public const uint SignatureConstant = 67324752U;

		// Token: 0x04000063 RID: 99
		public const int OffsetToCrcFromHeaderStart = 14;

		// Token: 0x04000064 RID: 100
		public const int OffsetToBitFlagFromHeaderStart = 6;

		// Token: 0x04000065 RID: 101
		public const int SizeOfLocalHeader = 30;
	}
}
