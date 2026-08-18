using System;

namespace System.IO.Compression
{
	// Token: 0x0200000F RID: 15
	internal struct ZipEndOfCentralDirectoryBlock
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00004DD0 File Offset: 0x00002FD0
		public static void WriteBlock(Stream stream, long numberOfEntries, long startOfCentralDirectory, long sizeOfCentralDirectory, byte[] archiveComment)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			ushort value = (numberOfEntries > 65535L) ? ushort.MaxValue : ((ushort)numberOfEntries);
			uint value2 = (startOfCentralDirectory > (long)((ulong)-1)) ? uint.MaxValue : ((uint)startOfCentralDirectory);
			uint value3 = (sizeOfCentralDirectory > (long)((ulong)-1)) ? uint.MaxValue : ((uint)sizeOfCentralDirectory);
			binaryWriter.Write(101010256U);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(value);
			binaryWriter.Write(value);
			binaryWriter.Write(value3);
			binaryWriter.Write(value2);
			binaryWriter.Write((archiveComment != null) ? ((ushort)archiveComment.Length) : 0);
			if (archiveComment != null)
			{
				binaryWriter.Write(archiveComment);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004E60 File Offset: 0x00003060
		public static bool TryReadBlock(BinaryReader reader, out ZipEndOfCentralDirectoryBlock eocdBlock)
		{
			eocdBlock = default(ZipEndOfCentralDirectoryBlock);
			if (reader.ReadUInt32() != 101010256U)
			{
				return false;
			}
			eocdBlock.Signature = 101010256U;
			eocdBlock.NumberOfThisDisk = reader.ReadUInt16();
			eocdBlock.NumberOfTheDiskWithTheStartOfTheCentralDirectory = reader.ReadUInt16();
			eocdBlock.NumberOfEntriesInTheCentralDirectoryOnThisDisk = reader.ReadUInt16();
			eocdBlock.NumberOfEntriesInTheCentralDirectory = reader.ReadUInt16();
			eocdBlock.SizeOfCentralDirectory = reader.ReadUInt32();
			eocdBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber = reader.ReadUInt32();
			ushort count = reader.ReadUInt16();
			eocdBlock.ArchiveComment = reader.ReadBytes((int)count);
			return true;
		}

		// Token: 0x04000079 RID: 121
		public const uint SignatureConstant = 101010256U;

		// Token: 0x0400007A RID: 122
		public const int SizeOfBlockWithoutSignature = 18;

		// Token: 0x0400007B RID: 123
		public uint Signature;

		// Token: 0x0400007C RID: 124
		public ushort NumberOfThisDisk;

		// Token: 0x0400007D RID: 125
		public ushort NumberOfTheDiskWithTheStartOfTheCentralDirectory;

		// Token: 0x0400007E RID: 126
		public ushort NumberOfEntriesInTheCentralDirectoryOnThisDisk;

		// Token: 0x0400007F RID: 127
		public ushort NumberOfEntriesInTheCentralDirectory;

		// Token: 0x04000080 RID: 128
		public uint SizeOfCentralDirectory;

		// Token: 0x04000081 RID: 129
		public uint OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber;

		// Token: 0x04000082 RID: 130
		public byte[] ArchiveComment;
	}
}
