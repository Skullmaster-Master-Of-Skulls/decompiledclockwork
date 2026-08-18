using System;

namespace System.IO.Compression
{
	// Token: 0x0200000B RID: 11
	internal struct Zip64EndOfCentralDirectoryLocator
	{
		// Token: 0x06000095 RID: 149 RVA: 0x000048E7 File Offset: 0x00002AE7
		public static bool TryReadBlock(BinaryReader reader, out Zip64EndOfCentralDirectoryLocator zip64EOCDLocator)
		{
			zip64EOCDLocator = default(Zip64EndOfCentralDirectoryLocator);
			if (reader.ReadUInt32() != 117853008U)
			{
				return false;
			}
			zip64EOCDLocator.NumberOfDiskWithZip64EOCD = reader.ReadUInt32();
			zip64EOCDLocator.OffsetOfZip64EOCD = reader.ReadUInt64();
			zip64EOCDLocator.TotalNumberOfDisks = reader.ReadUInt32();
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004924 File Offset: 0x00002B24
		public static void WriteBlock(Stream stream, long zip64EOCDRecordStart)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(117853008U);
			binaryWriter.Write(0U);
			binaryWriter.Write(zip64EOCDRecordStart);
			binaryWriter.Write(1U);
		}

		// Token: 0x04000051 RID: 81
		public const uint SignatureConstant = 117853008U;

		// Token: 0x04000052 RID: 82
		public const int SizeOfBlockWithoutSignature = 16;

		// Token: 0x04000053 RID: 83
		public uint NumberOfDiskWithZip64EOCD;

		// Token: 0x04000054 RID: 84
		public ulong OffsetOfZip64EOCD;

		// Token: 0x04000055 RID: 85
		public uint TotalNumberOfDisks;
	}
}
