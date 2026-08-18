using System;
using System.IO;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x02000051 RID: 81
	public class InflaterInputBuffer
	{
		// Token: 0x06000384 RID: 900 RVA: 0x00014A1C File Offset: 0x00013A1C
		public InflaterInputBuffer(Stream stream) : this(stream, 4096)
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00014A2A File Offset: 0x00013A2A
		public InflaterInputBuffer(Stream stream, int bufferSize)
		{
			this.inputStream = stream;
			if (bufferSize < 1024)
			{
				bufferSize = 1024;
			}
			this.rawData = new byte[bufferSize];
			this.clearText = this.rawData;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00014A60 File Offset: 0x00013A60
		public int RawLength
		{
			get
			{
				return this.rawLength;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00014A68 File Offset: 0x00013A68
		public byte[] RawData
		{
			get
			{
				return this.rawData;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00014A70 File Offset: 0x00013A70
		public int ClearTextLength
		{
			get
			{
				return this.clearTextLength;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00014A78 File Offset: 0x00013A78
		public byte[] ClearText
		{
			get
			{
				return this.clearText;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00014A80 File Offset: 0x00013A80
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00014A88 File Offset: 0x00013A88
		public int Available
		{
			get
			{
				return this.available;
			}
			set
			{
				this.available = value;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00014A91 File Offset: 0x00013A91
		public void SetInflaterInput(Inflater inflater)
		{
			if (this.available > 0)
			{
				inflater.SetInput(this.clearText, this.clearTextLength - this.available, this.available);
				this.available = 0;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00014AC4 File Offset: 0x00013AC4
		public void Fill()
		{
			this.rawLength = 0;
			int num;
			for (int i = this.rawData.Length; i > 0; i -= num)
			{
				num = this.inputStream.Read(this.rawData, this.rawLength, i);
				if (num <= 0)
				{
					break;
				}
				this.rawLength += num;
			}
			if (this.cryptoTransform != null)
			{
				this.clearTextLength = this.cryptoTransform.TransformBlock(this.rawData, 0, this.rawLength, this.clearText, 0);
			}
			else
			{
				this.clearTextLength = this.rawLength;
			}
			this.available = this.clearTextLength;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00014B5D File Offset: 0x00013B5D
		public int ReadRawBuffer(byte[] buffer)
		{
			return this.ReadRawBuffer(buffer, 0, buffer.Length);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00014B6C File Offset: 0x00013B6C
		public int ReadRawBuffer(byte[] outBuffer, int offset, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = offset;
			int i = length;
			while (i > 0)
			{
				if (this.available <= 0)
				{
					this.Fill();
					if (this.available <= 0)
					{
						return 0;
					}
				}
				int num2 = Math.Min(i, this.available);
				Array.Copy(this.rawData, this.rawLength - this.available, outBuffer, num, num2);
				num += num2;
				i -= num2;
				this.available -= num2;
			}
			return length;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00014BEC File Offset: 0x00013BEC
		public int ReadClearTextBuffer(byte[] outBuffer, int offset, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = offset;
			int i = length;
			while (i > 0)
			{
				if (this.available <= 0)
				{
					this.Fill();
					if (this.available <= 0)
					{
						return 0;
					}
				}
				int num2 = Math.Min(i, this.available);
				Array.Copy(this.clearText, this.clearTextLength - this.available, outBuffer, num, num2);
				num += num2;
				i -= num2;
				this.available -= num2;
			}
			return length;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00014C6C File Offset: 0x00013C6C
		public int ReadLeByte()
		{
			if (this.available <= 0)
			{
				this.Fill();
				if (this.available <= 0)
				{
					throw new ZipException("EOF in header");
				}
			}
			byte result = this.rawData[this.rawLength - this.available];
			this.available--;
			return (int)result;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00014CC0 File Offset: 0x00013CC0
		public int ReadLeShort()
		{
			return this.ReadLeByte() | this.ReadLeByte() << 8;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00014CD1 File Offset: 0x00013CD1
		public int ReadLeInt()
		{
			return this.ReadLeShort() | this.ReadLeShort() << 16;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00014CE3 File Offset: 0x00013CE3
		public long ReadLeLong()
		{
			return (long)((ulong)this.ReadLeInt() | (ulong)((ulong)((long)this.ReadLeInt()) << 32));
		}

		// Token: 0x170000D0 RID: 208
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00014CF8 File Offset: 0x00013CF8
		public ICryptoTransform CryptoTransform
		{
			set
			{
				this.cryptoTransform = value;
				if (this.cryptoTransform != null)
				{
					if (this.rawData == this.clearText)
					{
						if (this.internalClearText == null)
						{
							this.internalClearText = new byte[this.rawData.Length];
						}
						this.clearText = this.internalClearText;
					}
					this.clearTextLength = this.rawLength;
					if (this.available > 0)
					{
						this.cryptoTransform.TransformBlock(this.rawData, this.rawLength - this.available, this.available, this.clearText, this.rawLength - this.available);
						return;
					}
				}
				else
				{
					this.clearText = this.rawData;
					this.clearTextLength = this.rawLength;
				}
			}
		}

		// Token: 0x04000295 RID: 661
		private int rawLength;

		// Token: 0x04000296 RID: 662
		private byte[] rawData;

		// Token: 0x04000297 RID: 663
		private int clearTextLength;

		// Token: 0x04000298 RID: 664
		private byte[] clearText;

		// Token: 0x04000299 RID: 665
		private byte[] internalClearText;

		// Token: 0x0400029A RID: 666
		private int available;

		// Token: 0x0400029B RID: 667
		private ICryptoTransform cryptoTransform;

		// Token: 0x0400029C RID: 668
		private Stream inputStream;
	}
}
