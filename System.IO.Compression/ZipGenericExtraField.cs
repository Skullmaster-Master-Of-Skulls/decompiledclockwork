using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x02000009 RID: 9
	internal struct ZipGenericExtraField
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004255 File Offset: 0x00002455
		public ushort Tag
		{
			get
			{
				return this._tag;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000425D File Offset: 0x0000245D
		public ushort Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004265 File Offset: 0x00002465
		public byte[] Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004270 File Offset: 0x00002470
		public void WriteBlock(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(this.Tag);
			binaryWriter.Write(this.Size);
			binaryWriter.Write(this.Data);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000042A8 File Offset: 0x000024A8
		public static bool TryReadBlock(BinaryReader reader, long endExtraField, out ZipGenericExtraField field)
		{
			field = default(ZipGenericExtraField);
			if (endExtraField - reader.BaseStream.Position < 4L)
			{
				return false;
			}
			field._tag = reader.ReadUInt16();
			field._size = reader.ReadUInt16();
			if (endExtraField - reader.BaseStream.Position < (long)((ulong)field._size))
			{
				return false;
			}
			field._data = reader.ReadBytes((int)field._size);
			return true;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004314 File Offset: 0x00002514
		public static List<ZipGenericExtraField> ParseExtraField(Stream extraFieldData)
		{
			List<ZipGenericExtraField> list = new List<ZipGenericExtraField>();
			using (BinaryReader binaryReader = new BinaryReader(extraFieldData))
			{
				ZipGenericExtraField item;
				while (ZipGenericExtraField.TryReadBlock(binaryReader, extraFieldData.Length, out item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004364 File Offset: 0x00002564
		public static int TotalSize(List<ZipGenericExtraField> fields)
		{
			int num = 0;
			foreach (ZipGenericExtraField zipGenericExtraField in fields)
			{
				num += (int)(zipGenericExtraField.Size + 4);
			}
			return num;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000043BC File Offset: 0x000025BC
		public static void WriteAllBlocks(List<ZipGenericExtraField> fields, Stream stream)
		{
			foreach (ZipGenericExtraField zipGenericExtraField in fields)
			{
				zipGenericExtraField.WriteBlock(stream);
			}
		}

		// Token: 0x04000046 RID: 70
		private const int SizeOfHeader = 4;

		// Token: 0x04000047 RID: 71
		private ushort _tag;

		// Token: 0x04000048 RID: 72
		private ushort _size;

		// Token: 0x04000049 RID: 73
		private byte[] _data;
	}
}
