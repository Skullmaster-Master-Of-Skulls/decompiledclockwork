using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200003D RID: 61
	public class TarBuffer
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000F43B File Offset: 0x0000E43B
		public int RecordSize
		{
			get
			{
				return this.recordSize;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000F443 File Offset: 0x0000E443
		[Obsolete("Use RecordSize property instead")]
		public int GetRecordSize()
		{
			return this.recordSize;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000F44B File Offset: 0x0000E44B
		public int BlockFactor
		{
			get
			{
				return this.blockFactor;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000F453 File Offset: 0x0000E453
		[Obsolete("Use BlockFactor property instead")]
		public int GetBlockFactor()
		{
			return this.blockFactor;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000F45B File Offset: 0x0000E45B
		protected TarBuffer()
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000F47D File Offset: 0x0000E47D
		public static TarBuffer CreateInputTarBuffer(Stream inputStream)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			return TarBuffer.CreateInputTarBuffer(inputStream, 20);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000F498 File Offset: 0x0000E498
		public static TarBuffer CreateInputTarBuffer(Stream inputStream, int blockFactor)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			if (blockFactor <= 0)
			{
				throw new ArgumentOutOfRangeException("blockFactor", "Factor cannot be negative");
			}
			TarBuffer tarBuffer = new TarBuffer();
			tarBuffer.inputStream = inputStream;
			tarBuffer.outputStream = null;
			tarBuffer.Initialize(blockFactor);
			return tarBuffer;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000F4E3 File Offset: 0x0000E4E3
		public static TarBuffer CreateOutputTarBuffer(Stream outputStream)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			return TarBuffer.CreateOutputTarBuffer(outputStream, 20);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000F4FC File Offset: 0x0000E4FC
		public static TarBuffer CreateOutputTarBuffer(Stream outputStream, int blockFactor)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (blockFactor <= 0)
			{
				throw new ArgumentOutOfRangeException("blockFactor", "Factor cannot be negative");
			}
			TarBuffer tarBuffer = new TarBuffer();
			tarBuffer.inputStream = null;
			tarBuffer.outputStream = outputStream;
			tarBuffer.Initialize(blockFactor);
			return tarBuffer;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000F548 File Offset: 0x0000E548
		private void Initialize(int archiveBlockFactor)
		{
			this.blockFactor = archiveBlockFactor;
			this.recordSize = archiveBlockFactor * 512;
			this.recordBuffer = new byte[this.RecordSize];
			if (this.inputStream != null)
			{
				this.currentRecordIndex = -1;
				this.currentBlockIndex = this.BlockFactor;
				return;
			}
			this.currentRecordIndex = 0;
			this.currentBlockIndex = 0;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000F5A4 File Offset: 0x0000E5A4
		[Obsolete("Use IsEndOfArchiveBlock instead")]
		public bool IsEOFBlock(byte[] block)
		{
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			if (block.Length != 512)
			{
				throw new ArgumentException("block length is invalid");
			}
			for (int i = 0; i < 512; i++)
			{
				if (block[i] != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000F5EC File Offset: 0x0000E5EC
		public static bool IsEndOfArchiveBlock(byte[] block)
		{
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			if (block.Length != 512)
			{
				throw new ArgumentException("block length is invalid");
			}
			for (int i = 0; i < 512; i++)
			{
				if (block[i] != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000F634 File Offset: 0x0000E634
		public void SkipBlock()
		{
			if (this.inputStream == null)
			{
				throw new TarException("no input stream defined");
			}
			if (this.currentBlockIndex >= this.BlockFactor && !this.ReadRecord())
			{
				throw new TarException("Failed to read a record");
			}
			this.currentBlockIndex++;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000F684 File Offset: 0x0000E684
		public byte[] ReadBlock()
		{
			if (this.inputStream == null)
			{
				throw new TarException("TarBuffer.ReadBlock - no input stream defined");
			}
			if (this.currentBlockIndex >= this.BlockFactor && !this.ReadRecord())
			{
				throw new TarException("Failed to read a record");
			}
			byte[] array = new byte[512];
			Array.Copy(this.recordBuffer, this.currentBlockIndex * 512, array, 0, 512);
			this.currentBlockIndex++;
			return array;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000F700 File Offset: 0x0000E700
		private bool ReadRecord()
		{
			if (this.inputStream == null)
			{
				throw new TarException("no input stream stream defined");
			}
			this.currentBlockIndex = 0;
			int num = 0;
			long num2;
			for (int i = this.RecordSize; i > 0; i -= (int)num2)
			{
				num2 = (long)this.inputStream.Read(this.recordBuffer, num, i);
				if (num2 <= 0L)
				{
					break;
				}
				num += (int)num2;
			}
			this.currentRecordIndex++;
			return true;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000F769 File Offset: 0x0000E769
		public int CurrentBlock
		{
			get
			{
				return this.currentBlockIndex;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000F771 File Offset: 0x0000E771
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000F779 File Offset: 0x0000E779
		public bool IsStreamOwner
		{
			get
			{
				return this.isStreamOwner_;
			}
			set
			{
				this.isStreamOwner_ = value;
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000F782 File Offset: 0x0000E782
		[Obsolete("Use CurrentBlock property instead")]
		public int GetCurrentBlockNum()
		{
			return this.currentBlockIndex;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000F78A File Offset: 0x0000E78A
		public int CurrentRecord
		{
			get
			{
				return this.currentRecordIndex;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000F792 File Offset: 0x0000E792
		[Obsolete("Use CurrentRecord property instead")]
		public int GetCurrentRecordNum()
		{
			return this.currentRecordIndex;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000F79C File Offset: 0x0000E79C
		public void WriteBlock(byte[] block)
		{
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			if (this.outputStream == null)
			{
				throw new TarException("TarBuffer.WriteBlock - no output stream defined");
			}
			if (block.Length != 512)
			{
				string message = string.Format("TarBuffer.WriteBlock - block to write has length '{0}' which is not the block size of '{1}'", block.Length, 512);
				throw new TarException(message);
			}
			if (this.currentBlockIndex >= this.BlockFactor)
			{
				this.WriteRecord();
			}
			Array.Copy(block, 0, this.recordBuffer, this.currentBlockIndex * 512, 512);
			this.currentBlockIndex++;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000F838 File Offset: 0x0000E838
		public void WriteBlock(byte[] buffer, int offset)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (this.outputStream == null)
			{
				throw new TarException("TarBuffer.WriteBlock - no output stream stream defined");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + 512 > buffer.Length)
			{
				string message = string.Format("TarBuffer.WriteBlock - record has length '{0}' with offset '{1}' which is less than the record size of '{2}'", buffer.Length, offset, this.recordSize);
				throw new TarException(message);
			}
			if (this.currentBlockIndex >= this.BlockFactor)
			{
				this.WriteRecord();
			}
			Array.Copy(buffer, offset, this.recordBuffer, this.currentBlockIndex * 512, 512);
			this.currentBlockIndex++;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000F8F4 File Offset: 0x0000E8F4
		private void WriteRecord()
		{
			if (this.outputStream == null)
			{
				throw new TarException("TarBuffer.WriteRecord no output stream defined");
			}
			this.outputStream.Write(this.recordBuffer, 0, this.RecordSize);
			this.outputStream.Flush();
			this.currentBlockIndex = 0;
			this.currentRecordIndex++;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000F94C File Offset: 0x0000E94C
		private void WriteFinalRecord()
		{
			if (this.outputStream == null)
			{
				throw new TarException("TarBuffer.WriteFinalRecord no output stream defined");
			}
			if (this.currentBlockIndex > 0)
			{
				int num = this.currentBlockIndex * 512;
				Array.Clear(this.recordBuffer, num, this.RecordSize - num);
				this.WriteRecord();
			}
			this.outputStream.Flush();
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000F9A8 File Offset: 0x0000E9A8
		public void Close()
		{
			if (this.outputStream != null)
			{
				this.WriteFinalRecord();
				if (this.isStreamOwner_)
				{
					this.outputStream.Close();
				}
				this.outputStream = null;
				return;
			}
			if (this.inputStream != null)
			{
				if (this.isStreamOwner_)
				{
					this.inputStream.Close();
				}
				this.inputStream = null;
			}
		}

		// Token: 0x040001C5 RID: 453
		public const int BlockSize = 512;

		// Token: 0x040001C6 RID: 454
		public const int DefaultBlockFactor = 20;

		// Token: 0x040001C7 RID: 455
		public const int DefaultRecordSize = 10240;

		// Token: 0x040001C8 RID: 456
		private Stream inputStream;

		// Token: 0x040001C9 RID: 457
		private Stream outputStream;

		// Token: 0x040001CA RID: 458
		private byte[] recordBuffer;

		// Token: 0x040001CB RID: 459
		private int currentBlockIndex;

		// Token: 0x040001CC RID: 460
		private int currentRecordIndex;

		// Token: 0x040001CD RID: 461
		private int recordSize = 10240;

		// Token: 0x040001CE RID: 462
		private int blockFactor = 20;

		// Token: 0x040001CF RID: 463
		private bool isStreamOwner_ = true;
	}
}
