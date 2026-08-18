using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x0200002F RID: 47
	public class GZipOutputStream : DeflaterOutputStream
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x0000986E File Offset: 0x0000886E
		public GZipOutputStream(Stream baseOutputStream) : this(baseOutputStream, 4096)
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000987C File Offset: 0x0000887C
		public GZipOutputStream(Stream baseOutputStream, int size) : base(baseOutputStream, new Deflater(-1, true), size)
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00009898 File Offset: 0x00008898
		public void SetLevel(int level)
		{
			if (level < 1)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.deflater_.SetLevel(level);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000098B5 File Offset: 0x000088B5
		public int GetLevel()
		{
			return this.deflater_.GetLevel();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000098C2 File Offset: 0x000088C2
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				this.WriteHeader();
			}
			if (this.state_ != GZipOutputStream.OutputState.Footer)
			{
				throw new InvalidOperationException("Write not permitted in current state");
			}
			this.crc.Update(buffer, offset, count);
			base.Write(buffer, offset, count);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009900 File Offset: 0x00008900
		public override void Close()
		{
			try
			{
				this.Finish();
			}
			finally
			{
				if (this.state_ != GZipOutputStream.OutputState.Closed)
				{
					this.state_ = GZipOutputStream.OutputState.Closed;
					if (base.IsStreamOwner)
					{
						this.baseOutputStream_.Close();
					}
				}
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000994C File Offset: 0x0000894C
		public override void Finish()
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				this.WriteHeader();
			}
			if (this.state_ == GZipOutputStream.OutputState.Footer)
			{
				this.state_ = GZipOutputStream.OutputState.Finished;
				base.Finish();
				uint num = (uint)(this.deflater_.TotalIn & (long)((ulong)-1));
				uint num2 = (uint)(this.crc.Value & (long)((ulong)-1));
				byte[] array = new byte[]
				{
					(byte)num2,
					(byte)(num2 >> 8),
					(byte)(num2 >> 16),
					(byte)(num2 >> 24),
					(byte)num,
					(byte)(num >> 8),
					(byte)(num >> 16),
					(byte)(num >> 24)
				};
				this.baseOutputStream_.Write(array, 0, array.Length);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000099FC File Offset: 0x000089FC
		private void WriteHeader()
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				this.state_ = GZipOutputStream.OutputState.Footer;
				int num = (int)((DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000L);
				byte[] array = new byte[]
				{
					31,
					139,
					8,
					0,
					0,
					0,
					0,
					0,
					0,
					byte.MaxValue
				};
				array[4] = (byte)num;
				array[5] = (byte)(num >> 8);
				array[6] = (byte)(num >> 16);
				array[7] = (byte)(num >> 24);
				byte[] array2 = array;
				this.baseOutputStream_.Write(array2, 0, array2.Length);
			}
		}

		// Token: 0x0400010E RID: 270
		protected Crc32 crc = new Crc32();

		// Token: 0x0400010F RID: 271
		private GZipOutputStream.OutputState state_;

		// Token: 0x02000030 RID: 48
		private enum OutputState
		{
			// Token: 0x04000111 RID: 273
			Header,
			// Token: 0x04000112 RID: 274
			Footer,
			// Token: 0x04000113 RID: 275
			Finished,
			// Token: 0x04000114 RID: 276
			Closed
		}
	}
}
