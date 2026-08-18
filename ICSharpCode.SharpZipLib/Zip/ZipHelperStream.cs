using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000072 RID: 114
	internal class ZipHelperStream : Stream
	{
		// Token: 0x06000467 RID: 1127 RVA: 0x000170E0 File Offset: 0x000160E0
		public ZipHelperStream(string name)
		{
			this.stream_ = new FileStream(name, FileMode.Open, FileAccess.ReadWrite);
			this.isOwner_ = true;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000170FD File Offset: 0x000160FD
		public ZipHelperStream(Stream stream)
		{
			this.stream_ = stream;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0001710C File Offset: 0x0001610C
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00017114 File Offset: 0x00016114
		public bool IsStreamOwner
		{
			get
			{
				return this.isOwner_;
			}
			set
			{
				this.isOwner_ = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0001711D File Offset: 0x0001611D
		public override bool CanRead
		{
			get
			{
				return this.stream_.CanRead;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001712A File Offset: 0x0001612A
		public override bool CanSeek
		{
			get
			{
				return this.stream_.CanSeek;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00017137 File Offset: 0x00016137
		public override bool CanTimeout
		{
			get
			{
				return this.stream_.CanTimeout;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00017144 File Offset: 0x00016144
		public override long Length
		{
			get
			{
				return this.stream_.Length;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00017151 File Offset: 0x00016151
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0001715E File Offset: 0x0001615E
		public override long Position
		{
			get
			{
				return this.stream_.Position;
			}
			set
			{
				this.stream_.Position = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001716C File Offset: 0x0001616C
		public override bool CanWrite
		{
			get
			{
				return this.stream_.CanWrite;
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00017179 File Offset: 0x00016179
		public override void Flush()
		{
			this.stream_.Flush();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00017186 File Offset: 0x00016186
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream_.Seek(offset, origin);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00017195 File Offset: 0x00016195
		public override void SetLength(long value)
		{
			this.stream_.SetLength(value);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000171A3 File Offset: 0x000161A3
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream_.Read(buffer, offset, count);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000171B3 File Offset: 0x000161B3
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream_.Write(buffer, offset, count);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000171C4 File Offset: 0x000161C4
		public override void Close()
		{
			Stream stream = this.stream_;
			this.stream_ = null;
			if (this.isOwner_ && stream != null)
			{
				this.isOwner_ = false;
				stream.Close();
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000171F8 File Offset: 0x000161F8
		private void WriteLocalHeader(ZipEntry entry, EntryPatchData patchData)
		{
			CompressionMethod compressionMethod = entry.CompressionMethod;
			bool flag = true;
			bool flag2 = false;
			this.WriteLEInt(67324752);
			this.WriteLEShort(entry.Version);
			this.WriteLEShort(entry.Flags);
			this.WriteLEShort((int)((byte)compressionMethod));
			this.WriteLEInt((int)entry.DosTime);
			if (flag)
			{
				this.WriteLEInt((int)entry.Crc);
				if (entry.LocalHeaderRequiresZip64)
				{
					this.WriteLEInt(-1);
					this.WriteLEInt(-1);
				}
				else
				{
					this.WriteLEInt(entry.IsCrypted ? ((int)entry.CompressedSize + 12) : ((int)entry.CompressedSize));
					this.WriteLEInt((int)entry.Size);
				}
			}
			else
			{
				if (patchData != null)
				{
					patchData.CrcPatchOffset = this.stream_.Position;
				}
				this.WriteLEInt(0);
				if (patchData != null)
				{
					patchData.SizePatchOffset = this.stream_.Position;
				}
				if (entry.LocalHeaderRequiresZip64 && flag2)
				{
					this.WriteLEInt(-1);
					this.WriteLEInt(-1);
				}
				else
				{
					this.WriteLEInt(0);
					this.WriteLEInt(0);
				}
			}
			byte[] array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
			if (array.Length > 65535)
			{
				throw new ZipException("Entry name too long.");
			}
			ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
			if (entry.LocalHeaderRequiresZip64 && (flag || flag2))
			{
				zipExtraData.StartNewEntry();
				if (flag)
				{
					zipExtraData.AddLeLong(entry.Size);
					zipExtraData.AddLeLong(entry.CompressedSize);
				}
				else
				{
					zipExtraData.AddLeLong(-1L);
					zipExtraData.AddLeLong(-1L);
				}
				zipExtraData.AddNewEntry(1);
				if (!zipExtraData.Find(1))
				{
					throw new ZipException("Internal error cant find extra data");
				}
				if (patchData != null)
				{
					patchData.SizePatchOffset = (long)zipExtraData.CurrentReadIndex;
				}
			}
			else
			{
				zipExtraData.Delete(1);
			}
			byte[] entryData = zipExtraData.GetEntryData();
			this.WriteLEShort(array.Length);
			this.WriteLEShort(entryData.Length);
			if (array.Length > 0)
			{
				this.stream_.Write(array, 0, array.Length);
			}
			if (entry.LocalHeaderRequiresZip64 && flag2)
			{
				patchData.SizePatchOffset += this.stream_.Position;
			}
			if (entryData.Length > 0)
			{
				this.stream_.Write(entryData, 0, entryData.Length);
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001741C File Offset: 0x0001641C
		public long LocateBlockWithSignature(int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
		{
			long num = endLocation - (long)minimumBlockSize;
			if (num < 0L)
			{
				return -1L;
			}
			long num2 = Math.Max(num - (long)maximumVariableData, 0L);
			while (num >= num2)
			{
				long num3 = num;
				num = num3 - 1L;
				this.Seek(num3, SeekOrigin.Begin);
				if (this.ReadLEInt() == signature)
				{
					return this.Position;
				}
			}
			return -1L;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00017468 File Offset: 0x00016468
		public void WriteZip64EndOfCentralDirectory(long noOfEntries, long sizeEntries, long centralDirOffset)
		{
			long position = this.stream_.Position;
			this.WriteLEInt(101075792);
			this.WriteLELong(44L);
			this.WriteLEShort(51);
			this.WriteLEShort(45);
			this.WriteLEInt(0);
			this.WriteLEInt(0);
			this.WriteLELong(noOfEntries);
			this.WriteLELong(noOfEntries);
			this.WriteLELong(sizeEntries);
			this.WriteLELong(centralDirOffset);
			this.WriteLEInt(117853008);
			this.WriteLEInt(0);
			this.WriteLELong(position);
			this.WriteLEInt(1);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000174F0 File Offset: 0x000164F0
		public void WriteEndOfCentralDirectory(long noOfEntries, long sizeEntries, long startOfCentralDirectory, byte[] comment)
		{
			if (noOfEntries >= 65535L || startOfCentralDirectory >= (long)((ulong)-1) || sizeEntries >= (long)((ulong)-1))
			{
				this.WriteZip64EndOfCentralDirectory(noOfEntries, sizeEntries, startOfCentralDirectory);
			}
			this.WriteLEInt(101010256);
			this.WriteLEShort(0);
			this.WriteLEShort(0);
			if (noOfEntries >= 65535L)
			{
				this.WriteLEUshort(ushort.MaxValue);
				this.WriteLEUshort(ushort.MaxValue);
			}
			else
			{
				this.WriteLEShort((int)((short)noOfEntries));
				this.WriteLEShort((int)((short)noOfEntries));
			}
			if (sizeEntries >= (long)((ulong)-1))
			{
				this.WriteLEUint(uint.MaxValue);
			}
			else
			{
				this.WriteLEInt((int)sizeEntries);
			}
			if (startOfCentralDirectory >= (long)((ulong)-1))
			{
				this.WriteLEUint(uint.MaxValue);
			}
			else
			{
				this.WriteLEInt((int)startOfCentralDirectory);
			}
			int num = (comment != null) ? comment.Length : 0;
			if (num > 65535)
			{
				throw new ZipException(string.Format("Comment length({0}) is too long can only be 64K", num));
			}
			this.WriteLEShort(num);
			if (num > 0)
			{
				this.Write(comment, 0, comment.Length);
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000175D4 File Offset: 0x000165D4
		public int ReadLEShort()
		{
			int num = this.stream_.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			int num2 = this.stream_.ReadByte();
			if (num2 < 0)
			{
				throw new EndOfStreamException();
			}
			return num | num2 << 8;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00017612 File Offset: 0x00016612
		public int ReadLEInt()
		{
			return this.ReadLEShort() | this.ReadLEShort() << 16;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00017624 File Offset: 0x00016624
		public long ReadLELong()
		{
			return (long)((ulong)this.ReadLEInt() | (ulong)((ulong)((long)this.ReadLEInt()) << 32));
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00017638 File Offset: 0x00016638
		public void WriteLEShort(int value)
		{
			this.stream_.WriteByte((byte)(value & 255));
			this.stream_.WriteByte((byte)(value >> 8 & 255));
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00017662 File Offset: 0x00016662
		public void WriteLEUshort(ushort value)
		{
			this.stream_.WriteByte((byte)(value & 255));
			this.stream_.WriteByte((byte)(value >> 8));
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00017686 File Offset: 0x00016686
		public void WriteLEInt(int value)
		{
			this.WriteLEShort(value);
			this.WriteLEShort(value >> 16);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00017699 File Offset: 0x00016699
		public void WriteLEUint(uint value)
		{
			this.WriteLEUshort((ushort)(value & 65535U));
			this.WriteLEUshort((ushort)(value >> 16));
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000176B4 File Offset: 0x000166B4
		public void WriteLELong(long value)
		{
			this.WriteLEInt((int)value);
			this.WriteLEInt((int)(value >> 32));
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000176C9 File Offset: 0x000166C9
		public void WriteLEUlong(ulong value)
		{
			this.WriteLEUint((uint)(value & (ulong)-1));
			this.WriteLEUint((uint)(value >> 32));
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000176E4 File Offset: 0x000166E4
		public int WriteDataDescriptor(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			int num = 0;
			if ((entry.Flags & 8) != 0)
			{
				this.WriteLEInt(134695760);
				this.WriteLEInt((int)entry.Crc);
				num += 8;
				if (entry.LocalHeaderRequiresZip64)
				{
					this.WriteLELong(entry.CompressedSize);
					this.WriteLELong(entry.Size);
					num += 16;
				}
				else
				{
					this.WriteLEInt((int)entry.CompressedSize);
					this.WriteLEInt((int)entry.Size);
					num += 8;
				}
			}
			return num;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00017770 File Offset: 0x00016770
		public void ReadDataDescriptor(bool zip64, DescriptorData data)
		{
			int num = this.ReadLEInt();
			if (num != 134695760)
			{
				throw new ZipException("Data descriptor signature not found");
			}
			data.Crc = (long)this.ReadLEInt();
			if (zip64)
			{
				data.CompressedSize = this.ReadLELong();
				data.Size = this.ReadLELong();
				return;
			}
			data.CompressedSize = (long)this.ReadLEInt();
			data.Size = (long)this.ReadLEInt();
		}

		// Token: 0x040002E9 RID: 745
		private bool isOwner_;

		// Token: 0x040002EA RID: 746
		private Stream stream_;
	}
}
