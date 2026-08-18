using System;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000011 RID: 17
	public class ZipOutputStream : DeflaterOutputStream
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00004D0C File Offset: 0x00003D0C
		public ZipOutputStream(Stream baseOutputStream) : base(baseOutputStream, new Deflater(-1, true))
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004D70 File Offset: 0x00003D70
		public ZipOutputStream(Stream baseOutputStream, int bufferSize) : base(baseOutputStream, new Deflater(-1, true), bufferSize)
		{
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004DD3 File Offset: 0x00003DD3
		public bool IsFinished
		{
			get
			{
				return this.entries == null;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004DE0 File Offset: 0x00003DE0
		public void SetComment(string comment)
		{
			byte[] array = ZipConstants.ConvertToArray(comment);
			if (array.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("comment");
			}
			this.zipComment = array;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004E10 File Offset: 0x00003E10
		public void SetLevel(int level)
		{
			this.deflater_.SetLevel(level);
			this.defaultCompressionLevel = level;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004E25 File Offset: 0x00003E25
		public int GetLevel()
		{
			return this.deflater_.GetLevel();
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004E32 File Offset: 0x00003E32
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00004E3A File Offset: 0x00003E3A
		public UseZip64 UseZip64
		{
			get
			{
				return this.useZip64_;
			}
			set
			{
				this.useZip64_ = value;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004E43 File Offset: 0x00003E43
		private void WriteLeShort(int value)
		{
			this.baseOutputStream_.WriteByte((byte)(value & 255));
			this.baseOutputStream_.WriteByte((byte)(value >> 8 & 255));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004E6D File Offset: 0x00003E6D
		private void WriteLeInt(int value)
		{
			this.WriteLeShort(value);
			this.WriteLeShort(value >> 16);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004E80 File Offset: 0x00003E80
		private void WriteLeLong(long value)
		{
			this.WriteLeInt((int)value);
			this.WriteLeInt((int)(value >> 32));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004E98 File Offset: 0x00003E98
		public void PutNextEntry(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (this.entries == null)
			{
				throw new InvalidOperationException("ZipOutputStream was finished");
			}
			if (this.curEntry != null)
			{
				this.CloseEntry();
			}
			if (this.entries.Count == 2147483647)
			{
				throw new ZipException("Too many entries for Zip file");
			}
			CompressionMethod compressionMethod = entry.CompressionMethod;
			int level = this.defaultCompressionLevel;
			entry.Flags &= 2048;
			this.patchEntryHeader = false;
			bool flag;
			if (entry.Size == 0L)
			{
				entry.CompressedSize = entry.Size;
				entry.Crc = 0L;
				compressionMethod = CompressionMethod.Stored;
				flag = true;
			}
			else
			{
				flag = (entry.Size >= 0L && entry.HasCrc && entry.CompressedSize >= 0L);
				if (compressionMethod == CompressionMethod.Stored)
				{
					if (!flag)
					{
						if (!base.CanPatchEntries)
						{
							compressionMethod = CompressionMethod.Deflated;
							level = 0;
						}
					}
					else
					{
						entry.CompressedSize = entry.Size;
						flag = entry.HasCrc;
					}
				}
			}
			if (!flag)
			{
				if (!base.CanPatchEntries)
				{
					entry.Flags |= 8;
				}
				else
				{
					this.patchEntryHeader = true;
				}
			}
			if (base.Password != null)
			{
				entry.IsCrypted = true;
				if (entry.Crc < 0L)
				{
					entry.Flags |= 8;
				}
			}
			entry.Offset = this.offset;
			entry.CompressionMethod = compressionMethod;
			this.curMethod = compressionMethod;
			this.sizePatchPos = -1L;
			if (this.useZip64_ == UseZip64.On || (entry.Size < 0L && this.useZip64_ == UseZip64.Dynamic))
			{
				entry.ForceZip64();
			}
			this.WriteLeInt(67324752);
			this.WriteLeShort(entry.Version);
			this.WriteLeShort(entry.Flags);
			this.WriteLeShort((int)((byte)entry.CompressionMethodForHeader));
			this.WriteLeInt((int)entry.DosTime);
			if (flag)
			{
				this.WriteLeInt((int)entry.Crc);
				if (entry.LocalHeaderRequiresZip64)
				{
					this.WriteLeInt(-1);
					this.WriteLeInt(-1);
				}
				else
				{
					this.WriteLeInt(entry.IsCrypted ? ((int)entry.CompressedSize + 12) : ((int)entry.CompressedSize));
					this.WriteLeInt((int)entry.Size);
				}
			}
			else
			{
				if (this.patchEntryHeader)
				{
					this.crcPatchPos = this.baseOutputStream_.Position;
				}
				this.WriteLeInt(0);
				if (this.patchEntryHeader)
				{
					this.sizePatchPos = this.baseOutputStream_.Position;
				}
				if (entry.LocalHeaderRequiresZip64 || this.patchEntryHeader)
				{
					this.WriteLeInt(-1);
					this.WriteLeInt(-1);
				}
				else
				{
					this.WriteLeInt(0);
					this.WriteLeInt(0);
				}
			}
			byte[] array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
			if (array.Length > 65535)
			{
				throw new ZipException("Entry name too long.");
			}
			ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
			if (entry.LocalHeaderRequiresZip64)
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
				if (this.patchEntryHeader)
				{
					this.sizePatchPos = (long)zipExtraData.CurrentReadIndex;
				}
			}
			else
			{
				zipExtraData.Delete(1);
			}
			if (entry.AESKeySize > 0)
			{
				ZipOutputStream.AddExtraDataAES(entry, zipExtraData);
			}
			byte[] entryData = zipExtraData.GetEntryData();
			this.WriteLeShort(array.Length);
			this.WriteLeShort(entryData.Length);
			if (array.Length > 0)
			{
				this.baseOutputStream_.Write(array, 0, array.Length);
			}
			if (entry.LocalHeaderRequiresZip64 && this.patchEntryHeader)
			{
				this.sizePatchPos += this.baseOutputStream_.Position;
			}
			if (entryData.Length > 0)
			{
				this.baseOutputStream_.Write(entryData, 0, entryData.Length);
			}
			this.offset += (long)(30 + array.Length + entryData.Length);
			if (entry.AESKeySize > 0)
			{
				this.offset += (long)entry.AESOverheadSize;
			}
			this.curEntry = entry;
			this.crc.Reset();
			if (compressionMethod == CompressionMethod.Deflated)
			{
				this.deflater_.Reset();
				this.deflater_.SetLevel(level);
			}
			this.size = 0L;
			if (entry.IsCrypted)
			{
				if (entry.AESKeySize > 0)
				{
					this.WriteAESHeader(entry);
					return;
				}
				if (entry.Crc < 0L)
				{
					this.WriteEncryptionHeader(entry.DosTime << 16);
					return;
				}
				this.WriteEncryptionHeader(entry.Crc);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000052F4 File Offset: 0x000042F4
		public void CloseEntry()
		{
			if (this.curEntry == null)
			{
				throw new InvalidOperationException("No open entry");
			}
			long totalOut = this.size;
			if (this.curMethod == CompressionMethod.Deflated)
			{
				if (this.size >= 0L)
				{
					base.Finish();
					totalOut = this.deflater_.TotalOut;
				}
				else
				{
					this.deflater_.Reset();
				}
			}
			if (this.curEntry.AESKeySize > 0)
			{
				this.baseOutputStream_.Write(this.AESAuthCode, 0, 10);
			}
			if (this.curEntry.Size < 0L)
			{
				this.curEntry.Size = this.size;
			}
			else if (this.curEntry.Size != this.size)
			{
				throw new ZipException(string.Concat(new object[]
				{
					"size was ",
					this.size,
					", but I expected ",
					this.curEntry.Size
				}));
			}
			if (this.curEntry.CompressedSize < 0L)
			{
				this.curEntry.CompressedSize = totalOut;
			}
			else if (this.curEntry.CompressedSize != totalOut)
			{
				throw new ZipException(string.Concat(new object[]
				{
					"compressed size was ",
					totalOut,
					", but I expected ",
					this.curEntry.CompressedSize
				}));
			}
			if (this.curEntry.Crc < 0L)
			{
				this.curEntry.Crc = this.crc.Value;
			}
			else if (this.curEntry.Crc != this.crc.Value)
			{
				throw new ZipException(string.Concat(new object[]
				{
					"crc was ",
					this.crc.Value,
					", but I expected ",
					this.curEntry.Crc
				}));
			}
			this.offset += totalOut;
			if (this.curEntry.IsCrypted)
			{
				if (this.curEntry.AESKeySize > 0)
				{
					this.curEntry.CompressedSize += (long)this.curEntry.AESOverheadSize;
				}
				else
				{
					this.curEntry.CompressedSize += 12L;
				}
			}
			if (this.patchEntryHeader)
			{
				this.patchEntryHeader = false;
				long position = this.baseOutputStream_.Position;
				this.baseOutputStream_.Seek(this.crcPatchPos, SeekOrigin.Begin);
				this.WriteLeInt((int)this.curEntry.Crc);
				if (this.curEntry.LocalHeaderRequiresZip64)
				{
					if (this.sizePatchPos == -1L)
					{
						throw new ZipException("Entry requires zip64 but this has been turned off");
					}
					this.baseOutputStream_.Seek(this.sizePatchPos, SeekOrigin.Begin);
					this.WriteLeLong(this.curEntry.Size);
					this.WriteLeLong(this.curEntry.CompressedSize);
				}
				else
				{
					this.WriteLeInt((int)this.curEntry.CompressedSize);
					this.WriteLeInt((int)this.curEntry.Size);
				}
				this.baseOutputStream_.Seek(position, SeekOrigin.Begin);
			}
			if ((this.curEntry.Flags & 8) != 0)
			{
				this.WriteLeInt(134695760);
				this.WriteLeInt((int)this.curEntry.Crc);
				if (this.curEntry.LocalHeaderRequiresZip64)
				{
					this.WriteLeLong(this.curEntry.CompressedSize);
					this.WriteLeLong(this.curEntry.Size);
					this.offset += 24L;
				}
				else
				{
					this.WriteLeInt((int)this.curEntry.CompressedSize);
					this.WriteLeInt((int)this.curEntry.Size);
					this.offset += 16L;
				}
			}
			this.entries.Add(this.curEntry);
			this.curEntry = null;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000056CC File Offset: 0x000046CC
		private void WriteEncryptionHeader(long crcValue)
		{
			this.offset += 12L;
			base.InitializePassword(base.Password);
			byte[] array = new byte[12];
			Random random = new Random();
			random.NextBytes(array);
			array[11] = (byte)(crcValue >> 24);
			base.EncryptBlock(array, 0, array.Length);
			this.baseOutputStream_.Write(array, 0, array.Length);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000572E File Offset: 0x0000472E
		private static void AddExtraDataAES(ZipEntry entry, ZipExtraData extraData)
		{
			extraData.StartNewEntry();
			extraData.AddLeShort(2);
			extraData.AddLeShort(17729);
			extraData.AddData(entry.AESEncryptionStrength);
			extraData.AddLeShort((int)entry.CompressionMethod);
			extraData.AddNewEntry(39169);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000576C File Offset: 0x0000476C
		private void WriteAESHeader(ZipEntry entry)
		{
			byte[] array;
			byte[] array2;
			base.InitializeAESPassword(entry, base.Password, out array, out array2);
			this.baseOutputStream_.Write(array, 0, array.Length);
			this.baseOutputStream_.Write(array2, 0, array2.Length);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000057AC File Offset: 0x000047AC
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.curEntry == null)
			{
				throw new InvalidOperationException("No open entry.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Invalid offset/count combination");
			}
			this.crc.Update(buffer, offset, count);
			this.size += (long)count;
			CompressionMethod compressionMethod = this.curMethod;
			if (compressionMethod != CompressionMethod.Stored)
			{
				if (compressionMethod != CompressionMethod.Deflated)
				{
					return;
				}
				base.Write(buffer, offset, count);
				return;
			}
			else
			{
				if (base.Password != null)
				{
					this.CopyAndEncrypt(buffer, offset, count);
					return;
				}
				this.baseOutputStream_.Write(buffer, offset, count);
				return;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000586C File Offset: 0x0000486C
		private void CopyAndEncrypt(byte[] buffer, int offset, int count)
		{
			byte[] array = new byte[4096];
			while (count > 0)
			{
				int num = (count < 4096) ? count : 4096;
				Array.Copy(buffer, offset, array, 0, num);
				base.EncryptBlock(array, 0, num);
				this.baseOutputStream_.Write(array, 0, num);
				count -= num;
				offset += num;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000058C8 File Offset: 0x000048C8
		public override void Finish()
		{
			if (this.entries == null)
			{
				return;
			}
			if (this.curEntry != null)
			{
				this.CloseEntry();
			}
			long noOfEntries = (long)this.entries.Count;
			long num = 0L;
			foreach (object obj in this.entries)
			{
				ZipEntry zipEntry = (ZipEntry)obj;
				this.WriteLeInt(33639248);
				this.WriteLeShort(51);
				this.WriteLeShort(zipEntry.Version);
				this.WriteLeShort(zipEntry.Flags);
				this.WriteLeShort((int)((short)zipEntry.CompressionMethodForHeader));
				this.WriteLeInt((int)zipEntry.DosTime);
				this.WriteLeInt((int)zipEntry.Crc);
				if (zipEntry.IsZip64Forced() || zipEntry.CompressedSize >= (long)((ulong)-1))
				{
					this.WriteLeInt(-1);
				}
				else
				{
					this.WriteLeInt((int)zipEntry.CompressedSize);
				}
				if (zipEntry.IsZip64Forced() || zipEntry.Size >= (long)((ulong)-1))
				{
					this.WriteLeInt(-1);
				}
				else
				{
					this.WriteLeInt((int)zipEntry.Size);
				}
				byte[] array = ZipConstants.ConvertToArray(zipEntry.Flags, zipEntry.Name);
				if (array.Length > 65535)
				{
					throw new ZipException("Name too long.");
				}
				ZipExtraData zipExtraData = new ZipExtraData(zipEntry.ExtraData);
				if (zipEntry.CentralHeaderRequiresZip64)
				{
					zipExtraData.StartNewEntry();
					if (zipEntry.IsZip64Forced() || zipEntry.Size >= (long)((ulong)-1))
					{
						zipExtraData.AddLeLong(zipEntry.Size);
					}
					if (zipEntry.IsZip64Forced() || zipEntry.CompressedSize >= (long)((ulong)-1))
					{
						zipExtraData.AddLeLong(zipEntry.CompressedSize);
					}
					if (zipEntry.Offset >= (long)((ulong)-1))
					{
						zipExtraData.AddLeLong(zipEntry.Offset);
					}
					zipExtraData.AddNewEntry(1);
				}
				else
				{
					zipExtraData.Delete(1);
				}
				if (zipEntry.AESKeySize > 0)
				{
					ZipOutputStream.AddExtraDataAES(zipEntry, zipExtraData);
				}
				byte[] entryData = zipExtraData.GetEntryData();
				byte[] array2 = (zipEntry.Comment != null) ? ZipConstants.ConvertToArray(zipEntry.Flags, zipEntry.Comment) : new byte[0];
				if (array2.Length > 65535)
				{
					throw new ZipException("Comment too long.");
				}
				this.WriteLeShort(array.Length);
				this.WriteLeShort(entryData.Length);
				this.WriteLeShort(array2.Length);
				this.WriteLeShort(0);
				this.WriteLeShort(0);
				if (zipEntry.ExternalFileAttributes != -1)
				{
					this.WriteLeInt(zipEntry.ExternalFileAttributes);
				}
				else if (zipEntry.IsDirectory)
				{
					this.WriteLeInt(16);
				}
				else
				{
					this.WriteLeInt(0);
				}
				if (zipEntry.Offset >= (long)((ulong)-1))
				{
					this.WriteLeInt(-1);
				}
				else
				{
					this.WriteLeInt((int)zipEntry.Offset);
				}
				if (array.Length > 0)
				{
					this.baseOutputStream_.Write(array, 0, array.Length);
				}
				if (entryData.Length > 0)
				{
					this.baseOutputStream_.Write(entryData, 0, entryData.Length);
				}
				if (array2.Length > 0)
				{
					this.baseOutputStream_.Write(array2, 0, array2.Length);
				}
				num += (long)(46 + array.Length + entryData.Length + array2.Length);
			}
			using (ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseOutputStream_))
			{
				zipHelperStream.WriteEndOfCentralDirectory(noOfEntries, num, this.offset, this.zipComment);
			}
			this.entries = null;
		}

		// Token: 0x04000066 RID: 102
		private ArrayList entries = new ArrayList();

		// Token: 0x04000067 RID: 103
		private Crc32 crc = new Crc32();

		// Token: 0x04000068 RID: 104
		private ZipEntry curEntry;

		// Token: 0x04000069 RID: 105
		private int defaultCompressionLevel = -1;

		// Token: 0x0400006A RID: 106
		private CompressionMethod curMethod = CompressionMethod.Deflated;

		// Token: 0x0400006B RID: 107
		private long size;

		// Token: 0x0400006C RID: 108
		private long offset;

		// Token: 0x0400006D RID: 109
		private byte[] zipComment = new byte[0];

		// Token: 0x0400006E RID: 110
		private bool patchEntryHeader;

		// Token: 0x0400006F RID: 111
		private long crcPatchPos = -1L;

		// Token: 0x04000070 RID: 112
		private long sizePatchPos = -1L;

		// Token: 0x04000071 RID: 113
		private UseZip64 useZip64_ = UseZip64.Dynamic;
	}
}
