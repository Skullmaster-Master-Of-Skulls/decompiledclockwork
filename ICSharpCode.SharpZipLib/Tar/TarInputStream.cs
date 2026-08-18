using System;
using System.IO;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000041 RID: 65
	public class TarInputStream : Stream
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x0000FFCB File Offset: 0x0000EFCB
		public TarInputStream(Stream inputStream) : this(inputStream, 20)
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000FFD6 File Offset: 0x0000EFD6
		public TarInputStream(Stream inputStream, int blockFactor)
		{
			this.inputStream = inputStream;
			this.tarBuffer = TarBuffer.CreateInputTarBuffer(inputStream, blockFactor);
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000FFF2 File Offset: 0x0000EFF2
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000FFFF File Offset: 0x0000EFFF
		public bool IsStreamOwner
		{
			get
			{
				return this.tarBuffer.IsStreamOwner;
			}
			set
			{
				this.tarBuffer.IsStreamOwner = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0001000D File Offset: 0x0000F00D
		public override bool CanRead
		{
			get
			{
				return this.inputStream.CanRead;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0001001A File Offset: 0x0000F01A
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0001001D File Offset: 0x0000F01D
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00010020 File Offset: 0x0000F020
		public override long Length
		{
			get
			{
				return this.inputStream.Length;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0001002D File Offset: 0x0000F02D
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0001003A File Offset: 0x0000F03A
		public override long Position
		{
			get
			{
				return this.inputStream.Position;
			}
			set
			{
				throw new NotSupportedException("TarInputStream Seek not supported");
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00010046 File Offset: 0x0000F046
		public override void Flush()
		{
			this.inputStream.Flush();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00010053 File Offset: 0x0000F053
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("TarInputStream Seek not supported");
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001005F File Offset: 0x0000F05F
		public override void SetLength(long value)
		{
			throw new NotSupportedException("TarInputStream SetLength not supported");
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0001006B File Offset: 0x0000F06B
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("TarInputStream Write not supported");
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00010077 File Offset: 0x0000F077
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("TarInputStream WriteByte not supported");
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00010084 File Offset: 0x0000F084
		public override int ReadByte()
		{
			byte[] array = new byte[1];
			int num = this.Read(array, 0, 1);
			if (num <= 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000100AC File Offset: 0x0000F0AC
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = 0;
			if (this.entryOffset >= this.entrySize)
			{
				return 0;
			}
			long num2 = (long)count;
			if (num2 + this.entryOffset > this.entrySize)
			{
				num2 = this.entrySize - this.entryOffset;
			}
			if (this.readBuffer != null)
			{
				int num3 = (num2 > (long)this.readBuffer.Length) ? this.readBuffer.Length : ((int)num2);
				Array.Copy(this.readBuffer, 0, buffer, offset, num3);
				if (num3 >= this.readBuffer.Length)
				{
					this.readBuffer = null;
				}
				else
				{
					int num4 = this.readBuffer.Length - num3;
					byte[] destinationArray = new byte[num4];
					Array.Copy(this.readBuffer, num3, destinationArray, 0, num4);
					this.readBuffer = destinationArray;
				}
				num += num3;
				num2 -= (long)num3;
				offset += num3;
			}
			while (num2 > 0L)
			{
				byte[] array = this.tarBuffer.ReadBlock();
				if (array == null)
				{
					throw new TarException("unexpected EOF with " + num2 + " bytes unread");
				}
				int num5 = (int)num2;
				int num6 = array.Length;
				if (num6 > num5)
				{
					Array.Copy(array, 0, buffer, offset, num5);
					this.readBuffer = new byte[num6 - num5];
					Array.Copy(array, num5, this.readBuffer, 0, num6 - num5);
				}
				else
				{
					num5 = num6;
					Array.Copy(array, 0, buffer, offset, num6);
				}
				num += num5;
				num2 -= (long)num5;
				offset += num5;
			}
			this.entryOffset += (long)num;
			return num;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00010227 File Offset: 0x0000F227
		public override void Close()
		{
			this.tarBuffer.Close();
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00010234 File Offset: 0x0000F234
		public void SetEntryFactory(TarInputStream.IEntryFactory factory)
		{
			this.entryFactory = factory;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0001023D File Offset: 0x0000F23D
		public int RecordSize
		{
			get
			{
				return this.tarBuffer.RecordSize;
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001024A File Offset: 0x0000F24A
		[Obsolete("Use RecordSize property instead")]
		public int GetRecordSize()
		{
			return this.tarBuffer.RecordSize;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00010257 File Offset: 0x0000F257
		public long Available
		{
			get
			{
				return this.entrySize - this.entryOffset;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00010268 File Offset: 0x0000F268
		public void Skip(long skipCount)
		{
			byte[] array = new byte[8192];
			int num2;
			for (long num = skipCount; num > 0L; num -= (long)num2)
			{
				int count = (num > (long)array.Length) ? array.Length : ((int)num);
				num2 = this.Read(array, 0, count);
				if (num2 == -1)
				{
					return;
				}
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002DC RID: 732 RVA: 0x000102AC File Offset: 0x0000F2AC
		public bool IsMarkSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000102AF File Offset: 0x0000F2AF
		public void Mark(int markLimit)
		{
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000102B1 File Offset: 0x0000F2B1
		public void Reset()
		{
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000102B4 File Offset: 0x0000F2B4
		public TarEntry GetNextEntry()
		{
			if (this.hasHitEOF)
			{
				return null;
			}
			if (this.currentEntry != null)
			{
				this.SkipToNextEntry();
			}
			byte[] array = this.tarBuffer.ReadBlock();
			if (array == null)
			{
				this.hasHitEOF = true;
			}
			else if (TarBuffer.IsEndOfArchiveBlock(array))
			{
				this.hasHitEOF = true;
			}
			if (this.hasHitEOF)
			{
				this.currentEntry = null;
			}
			else
			{
				try
				{
					TarHeader tarHeader = new TarHeader();
					tarHeader.ParseBuffer(array);
					if (!tarHeader.IsChecksumValid)
					{
						throw new TarException("Header checksum is invalid");
					}
					this.entryOffset = 0L;
					this.entrySize = tarHeader.Size;
					StringBuilder stringBuilder = null;
					if (tarHeader.TypeFlag == 76)
					{
						byte[] array2 = new byte[512];
						long num = this.entrySize;
						stringBuilder = new StringBuilder();
						while (num > 0L)
						{
							int num2 = this.Read(array2, 0, (num > (long)array2.Length) ? array2.Length : ((int)num));
							if (num2 == -1)
							{
								throw new InvalidHeaderException("Failed to read long name entry");
							}
							stringBuilder.Append(TarHeader.ParseName(array2, 0, num2).ToString());
							num -= (long)num2;
						}
						this.SkipToNextEntry();
						array = this.tarBuffer.ReadBlock();
					}
					else if (tarHeader.TypeFlag == 103)
					{
						this.SkipToNextEntry();
						array = this.tarBuffer.ReadBlock();
					}
					else if (tarHeader.TypeFlag == 120)
					{
						this.SkipToNextEntry();
						array = this.tarBuffer.ReadBlock();
					}
					else if (tarHeader.TypeFlag == 86)
					{
						this.SkipToNextEntry();
						array = this.tarBuffer.ReadBlock();
					}
					else if (tarHeader.TypeFlag != 48 && tarHeader.TypeFlag != 0 && tarHeader.TypeFlag != 49 && tarHeader.TypeFlag != 50 && tarHeader.TypeFlag != 53)
					{
						this.SkipToNextEntry();
						array = this.tarBuffer.ReadBlock();
					}
					if (this.entryFactory == null)
					{
						this.currentEntry = new TarEntry(array);
						if (stringBuilder != null)
						{
							this.currentEntry.Name = stringBuilder.ToString();
						}
					}
					else
					{
						this.currentEntry = this.entryFactory.CreateEntry(array);
					}
					this.entryOffset = 0L;
					this.entrySize = this.currentEntry.Size;
				}
				catch (InvalidHeaderException ex)
				{
					this.entrySize = 0L;
					this.entryOffset = 0L;
					this.currentEntry = null;
					string message = string.Format("Bad header in record {0} block {1} {2}", this.tarBuffer.CurrentRecord, this.tarBuffer.CurrentBlock, ex.Message);
					throw new InvalidHeaderException(message);
				}
			}
			return this.currentEntry;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0001053C File Offset: 0x0000F53C
		public void CopyEntryContents(Stream outputStream)
		{
			byte[] array = new byte[32768];
			for (;;)
			{
				int num = this.Read(array, 0, array.Length);
				if (num <= 0)
				{
					break;
				}
				outputStream.Write(array, 0, num);
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00010570 File Offset: 0x0000F570
		private void SkipToNextEntry()
		{
			long num = this.entrySize - this.entryOffset;
			if (num > 0L)
			{
				this.Skip(num);
			}
			this.readBuffer = null;
		}

		// Token: 0x040001D8 RID: 472
		protected bool hasHitEOF;

		// Token: 0x040001D9 RID: 473
		protected long entrySize;

		// Token: 0x040001DA RID: 474
		protected long entryOffset;

		// Token: 0x040001DB RID: 475
		protected byte[] readBuffer;

		// Token: 0x040001DC RID: 476
		protected TarBuffer tarBuffer;

		// Token: 0x040001DD RID: 477
		private TarEntry currentEntry;

		// Token: 0x040001DE RID: 478
		protected TarInputStream.IEntryFactory entryFactory;

		// Token: 0x040001DF RID: 479
		private readonly Stream inputStream;

		// Token: 0x02000042 RID: 66
		public interface IEntryFactory
		{
			// Token: 0x060002E2 RID: 738
			TarEntry CreateEntry(string name);

			// Token: 0x060002E3 RID: 739
			TarEntry CreateEntryFromFile(string fileName);

			// Token: 0x060002E4 RID: 740
			TarEntry CreateEntry(byte[] headerBuffer);
		}

		// Token: 0x02000043 RID: 67
		public class EntryFactoryAdapter : TarInputStream.IEntryFactory
		{
			// Token: 0x060002E5 RID: 741 RVA: 0x0001059E File Offset: 0x0000F59E
			public TarEntry CreateEntry(string name)
			{
				return TarEntry.CreateTarEntry(name);
			}

			// Token: 0x060002E6 RID: 742 RVA: 0x000105A6 File Offset: 0x0000F5A6
			public TarEntry CreateEntryFromFile(string fileName)
			{
				return TarEntry.CreateEntryFromFile(fileName);
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x000105AE File Offset: 0x0000F5AE
			public TarEntry CreateEntry(byte[] headerBuffer)
			{
				return new TarEntry(headerBuffer);
			}
		}
	}
}
