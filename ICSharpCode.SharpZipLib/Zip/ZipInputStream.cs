using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Encryption;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200000E RID: 14
	public class ZipInputStream : InflaterInputStream
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00003E09 File Offset: 0x00002E09
		public ZipInputStream(Stream baseInputStream) : base(baseInputStream, new Inflater(true))
		{
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003E35 File Offset: 0x00002E35
		public ZipInputStream(Stream baseInputStream, int bufferSize) : base(baseInputStream, new Inflater(true), bufferSize)
		{
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003E62 File Offset: 0x00002E62
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003E6A File Offset: 0x00002E6A
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003E73 File Offset: 0x00002E73
		public bool CanDecompressEntry
		{
			get
			{
				return this.entry != null && this.entry.CanDecompress;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003E8C File Offset: 0x00002E8C
		public ZipEntry GetNextEntry()
		{
			if (this.crc == null)
			{
				throw new InvalidOperationException("Closed.");
			}
			if (this.entry != null)
			{
				this.CloseEntry();
			}
			int num = this.inputBuffer.ReadLeInt();
			if (num == 33639248 || num == 101010256 || num == 84233040 || num == 117853008 || num == 101075792)
			{
				this.Close();
				return null;
			}
			if (num == 808471376 || num == 134695760)
			{
				num = this.inputBuffer.ReadLeInt();
			}
			if (num != 67324752)
			{
				throw new ZipException("Wrong Local header signature: 0x" + string.Format("{0:X}", num));
			}
			short versionRequiredToExtract = (short)this.inputBuffer.ReadLeShort();
			this.flags = this.inputBuffer.ReadLeShort();
			this.method = this.inputBuffer.ReadLeShort();
			uint num2 = (uint)this.inputBuffer.ReadLeInt();
			int num3 = this.inputBuffer.ReadLeInt();
			this.csize = (long)this.inputBuffer.ReadLeInt();
			this.size = (long)this.inputBuffer.ReadLeInt();
			int num4 = this.inputBuffer.ReadLeShort();
			int num5 = this.inputBuffer.ReadLeShort();
			bool flag = (this.flags & 1) == 1;
			byte[] array = new byte[num4];
			this.inputBuffer.ReadRawBuffer(array);
			string name = ZipConstants.ConvertToStringExt(this.flags, array);
			this.entry = new ZipEntry(name, (int)versionRequiredToExtract);
			this.entry.Flags = this.flags;
			this.entry.CompressionMethod = (CompressionMethod)this.method;
			if ((this.flags & 8) == 0)
			{
				this.entry.Crc = ((long)num3 & (long)((ulong)-1));
				this.entry.Size = (this.size & (long)((ulong)-1));
				this.entry.CompressedSize = (this.csize & (long)((ulong)-1));
				this.entry.CryptoCheckValue = (byte)(num3 >> 24 & 255);
			}
			else
			{
				if (num3 != 0)
				{
					this.entry.Crc = ((long)num3 & (long)((ulong)-1));
				}
				if (this.size != 0L)
				{
					this.entry.Size = (this.size & (long)((ulong)-1));
				}
				if (this.csize != 0L)
				{
					this.entry.CompressedSize = (this.csize & (long)((ulong)-1));
				}
				this.entry.CryptoCheckValue = (byte)(num2 >> 8 & 255U);
			}
			this.entry.DosTime = (long)((ulong)num2);
			if (num5 > 0)
			{
				byte[] array2 = new byte[num5];
				this.inputBuffer.ReadRawBuffer(array2);
				this.entry.ExtraData = array2;
			}
			this.entry.ProcessExtraData(true);
			if (this.entry.CompressedSize >= 0L)
			{
				this.csize = this.entry.CompressedSize;
			}
			if (this.entry.Size >= 0L)
			{
				this.size = this.entry.Size;
			}
			if (this.method == 0 && ((!flag && this.csize != this.size) || (flag && this.csize - 12L != this.size)))
			{
				throw new ZipException("Stored, but compressed != uncompressed");
			}
			if (this.entry.IsCompressionMethodSupported())
			{
				this.internalReader = new ZipInputStream.ReadDataHandler(this.InitialRead);
			}
			else
			{
				this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotSupported);
			}
			return this.entry;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000041D8 File Offset: 0x000031D8
		private void ReadDataDescriptor()
		{
			if (this.inputBuffer.ReadLeInt() != 134695760)
			{
				throw new ZipException("Data descriptor signature not found");
			}
			this.entry.Crc = ((long)this.inputBuffer.ReadLeInt() & (long)((ulong)-1));
			if (this.entry.LocalHeaderRequiresZip64)
			{
				this.csize = this.inputBuffer.ReadLeLong();
				this.size = this.inputBuffer.ReadLeLong();
			}
			else
			{
				this.csize = (long)this.inputBuffer.ReadLeInt();
				this.size = (long)this.inputBuffer.ReadLeInt();
			}
			this.entry.CompressedSize = this.csize;
			this.entry.Size = this.size;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004294 File Offset: 0x00003294
		private void CompleteCloseEntry(bool testCrc)
		{
			base.StopDecrypting();
			if ((this.flags & 8) != 0)
			{
				this.ReadDataDescriptor();
			}
			this.size = 0L;
			if (testCrc && (this.crc.Value & (long)((ulong)-1)) != this.entry.Crc && this.entry.Crc != -1L)
			{
				throw new ZipException("CRC mismatch");
			}
			this.crc.Reset();
			if (this.method == 8)
			{
				this.inf.Reset();
			}
			this.entry = null;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004320 File Offset: 0x00003320
		public void CloseEntry()
		{
			if (this.crc == null)
			{
				throw new InvalidOperationException("Closed");
			}
			if (this.entry == null)
			{
				return;
			}
			if (this.method == 8)
			{
				if ((this.flags & 8) != 0)
				{
					byte[] array = new byte[4096];
					while (this.Read(array, 0, array.Length) > 0)
					{
					}
					return;
				}
				this.csize -= this.inf.TotalIn;
				this.inputBuffer.Available += this.inf.RemainingInput;
			}
			if ((long)this.inputBuffer.Available > this.csize && this.csize >= 0L)
			{
				this.inputBuffer.Available = (int)((long)this.inputBuffer.Available - this.csize);
			}
			else
			{
				this.csize -= (long)this.inputBuffer.Available;
				this.inputBuffer.Available = 0;
				while (this.csize != 0L)
				{
					long num = base.Skip(this.csize);
					if (num <= 0L)
					{
						throw new ZipException("Zip archive ends early.");
					}
					this.csize -= num;
				}
			}
			this.CompleteCloseEntry(false);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000444D File Offset: 0x0000344D
		public override int Available
		{
			get
			{
				if (this.entry == null)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000445A File Offset: 0x0000345A
		public override long Length
		{
			get
			{
				if (this.entry == null)
				{
					throw new InvalidOperationException("No current entry");
				}
				if (this.entry.Size >= 0L)
				{
					return this.entry.Size;
				}
				throw new ZipException("Length not available for the current entry");
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004494 File Offset: 0x00003494
		public override int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)(array[0] & byte.MaxValue);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000044BF File Offset: 0x000034BF
		private int ReadingNotAvailable(byte[] destination, int offset, int count)
		{
			throw new InvalidOperationException("Unable to read from this stream");
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000044CB File Offset: 0x000034CB
		private int ReadingNotSupported(byte[] destination, int offset, int count)
		{
			throw new ZipException("The compression method for this entry is not supported");
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000044D8 File Offset: 0x000034D8
		private int InitialRead(byte[] destination, int offset, int count)
		{
			if (!this.CanDecompressEntry)
			{
				throw new ZipException("Library cannot extract this entry. Version required is (" + this.entry.Version.ToString() + ")");
			}
			if (this.entry.IsCrypted)
			{
				if (this.password == null)
				{
					throw new ZipException("No password set.");
				}
				PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
				byte[] rgbKey = PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(this.password));
				this.inputBuffer.CryptoTransform = pkzipClassicManaged.CreateDecryptor(rgbKey, null);
				byte[] array = new byte[12];
				this.inputBuffer.ReadClearTextBuffer(array, 0, 12);
				if (array[11] != this.entry.CryptoCheckValue)
				{
					throw new ZipException("Invalid password");
				}
				if (this.csize >= 12L)
				{
					this.csize -= 12L;
				}
				else if ((this.entry.Flags & 8) == 0)
				{
					throw new ZipException(string.Format("Entry compressed size {0} too small for encryption", this.csize));
				}
			}
			else
			{
				this.inputBuffer.CryptoTransform = null;
			}
			if (this.csize > 0L || (this.flags & 8) != 0)
			{
				if (this.method == 8 && this.inputBuffer.Available > 0)
				{
					this.inputBuffer.SetInflaterInput(this.inf);
				}
				this.internalReader = new ZipInputStream.ReadDataHandler(this.BodyRead);
				return this.BodyRead(destination, offset, count);
			}
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
			return 0;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004654 File Offset: 0x00003654
		public override int Read(byte[] buffer, int offset, int count)
		{
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
			return this.internalReader(buffer, offset, count);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000046B8 File Offset: 0x000036B8
		private int BodyRead(byte[] buffer, int offset, int count)
		{
			if (this.crc == null)
			{
				throw new InvalidOperationException("Closed");
			}
			if (this.entry == null || count <= 0)
			{
				return 0;
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentException("Offset + count exceeds buffer size");
			}
			bool flag = false;
			int num = this.method;
			if (num != 0)
			{
				if (num == 8)
				{
					count = base.Read(buffer, offset, count);
					if (count <= 0)
					{
						if (!this.inf.IsFinished)
						{
							throw new ZipException("Inflater not finished!");
						}
						this.inputBuffer.Available = this.inf.RemainingInput;
						if ((this.flags & 8) == 0 && ((this.inf.TotalIn != this.csize && this.csize != (long)((ulong)-1) && this.csize != -1L) || this.inf.TotalOut != this.size))
						{
							throw new ZipException(string.Concat(new object[]
							{
								"Size mismatch: ",
								this.csize,
								";",
								this.size,
								" <-> ",
								this.inf.TotalIn,
								";",
								this.inf.TotalOut
							}));
						}
						this.inf.Reset();
						flag = true;
					}
				}
			}
			else
			{
				if ((long)count > this.csize && this.csize >= 0L)
				{
					count = (int)this.csize;
				}
				if (count > 0)
				{
					count = this.inputBuffer.ReadClearTextBuffer(buffer, offset, count);
					if (count > 0)
					{
						this.csize -= (long)count;
						this.size -= (long)count;
					}
				}
				if (this.csize == 0L)
				{
					flag = true;
				}
				else if (count < 0)
				{
					throw new ZipException("EOF in stored block");
				}
			}
			if (count > 0)
			{
				this.crc.Update(buffer, offset, count);
			}
			if (flag)
			{
				this.CompleteCloseEntry(true);
			}
			return count;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000048A8 File Offset: 0x000038A8
		public override void Close()
		{
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
			this.crc = null;
			this.entry = null;
			base.Close();
		}

		// Token: 0x04000056 RID: 86
		private ZipInputStream.ReadDataHandler internalReader;

		// Token: 0x04000057 RID: 87
		private Crc32 crc = new Crc32();

		// Token: 0x04000058 RID: 88
		private ZipEntry entry;

		// Token: 0x04000059 RID: 89
		private long size;

		// Token: 0x0400005A RID: 90
		private int method;

		// Token: 0x0400005B RID: 91
		private int flags;

		// Token: 0x0400005C RID: 92
		private string password;

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x0600009C RID: 156
		private delegate int ReadDataHandler(byte[] b, int offset, int length);
	}
}
