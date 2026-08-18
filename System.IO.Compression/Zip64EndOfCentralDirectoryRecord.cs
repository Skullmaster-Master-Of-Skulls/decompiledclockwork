using System;

namespace System.IO.Compression
{
	// Token: 0x0200000C RID: 12
	internal struct Zip64EndOfCentralDirectoryRecord
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00004958 File Offset: 0x00002B58
		public static bool TryReadBlock(BinaryReader reader, out Zip64EndOfCentralDirectoryRecord zip64EOCDRecord)
		{
			zip64EOCDRecord = default(Zip64EndOfCentralDirectoryRecord);
			if (reader.ReadUInt32() != 101075792U)
			{
				return false;
			}
			zip64EOCDRecord.SizeOfThisRecord = reader.ReadUInt64();
			zip64EOCDRecord.VersionMadeBy = reader.ReadUInt16();
			zip64EOCDRecord.VersionNeededToExtract = reader.ReadUInt16();
			zip64EOCDRecord.NumberOfThisDisk = reader.ReadUInt32();
			zip64EOCDRecord.NumberOfDiskWithStartOfCD = reader.ReadUInt32();
			zip64EOCDRecord.NumberOfEntriesOnThisDisk = reader.ReadUInt64();
			zip64EOCDRecord.NumberOfEntriesTotal = reader.ReadUInt64();
			zip64EOCDRecord.SizeOfCentralDirectory = reader.ReadUInt64();
			zip64EOCDRecord.OffsetOfCentralDirectory = reader.ReadUInt64();
			return true;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000049E8 File Offset: 0x00002BE8
		public static void WriteBlock(Stream stream, long numberOfEntries, long startOfCentralDirectory, long sizeOfCentralDirectory)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(101075792U);
			binaryWriter.Write(44UL);
			binaryWriter.Write(45);
			binaryWriter.Write(45);
			binaryWriter.Write(0U);
			binaryWriter.Write(0U);
			binaryWriter.Write(numberOfEntries);
			binaryWriter.Write(numberOfEntries);
			binaryWriter.Write(sizeOfCentralDirectory);
			binaryWriter.Write(startOfCentralDirectory);
		}

		// Token: 0x04000056 RID: 86
		private const uint SignatureConstant = 101075792U;

		// Token: 0x04000057 RID: 87
		private const ulong NormalSize = 44UL;

		// Token: 0x04000058 RID: 88
		public ulong SizeOfThisRecord;

		// Token: 0x04000059 RID: 89
		public ushort VersionMadeBy;

		// Token: 0x0400005A RID: 90
		public ushort VersionNeededToExtract;

		// Token: 0x0400005B RID: 91
		public uint NumberOfThisDisk;

		// Token: 0x0400005C RID: 92
		public uint NumberOfDiskWithStartOfCD;

		// Token: 0x0400005D RID: 93
		public ulong NumberOfEntriesOnThisDisk;

		// Token: 0x0400005E RID: 94
		public ulong NumberOfEntriesTotal;

		// Token: 0x0400005F RID: 95
		public ulong SizeOfCentralDirectory;

		// Token: 0x04000060 RID: 96
		public ulong OffsetOfCentralDirectory;
	}
}
