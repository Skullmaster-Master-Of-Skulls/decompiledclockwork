using System;
using System.IO;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Encryption;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x02000010 RID: 16
	public class DeflaterOutputStream : Stream
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000048D0 File Offset: 0x000038D0
		public DeflaterOutputStream(Stream baseOutputStream) : this(baseOutputStream, new Deflater(), 512)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000048E3 File Offset: 0x000038E3
		public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater) : this(baseOutputStream, deflater, 512)
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000048F4 File Offset: 0x000038F4
		public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater, int bufferSize)
		{
			if (baseOutputStream == null)
			{
				throw new ArgumentNullException("baseOutputStream");
			}
			if (!baseOutputStream.CanWrite)
			{
				throw new ArgumentException("Must support writing", "baseOutputStream");
			}
			if (deflater == null)
			{
				throw new ArgumentNullException("deflater");
			}
			if (bufferSize < 512)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			this.baseOutputStream_ = baseOutputStream;
			this.buffer_ = new byte[bufferSize];
			this.deflater_ = deflater;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004970 File Offset: 0x00003970
		public virtual void Finish()
		{
			this.deflater_.Finish();
			while (!this.deflater_.IsFinished)
			{
				int num = this.deflater_.Deflate(this.buffer_, 0, this.buffer_.Length);
				if (num <= 0)
				{
					break;
				}
				if (this.cryptoTransform_ != null)
				{
					this.EncryptBlock(this.buffer_, 0, num);
				}
				this.baseOutputStream_.Write(this.buffer_, 0, num);
			}
			if (!this.deflater_.IsFinished)
			{
				throw new SharpZipBaseException("Can't deflate all input?");
			}
			this.baseOutputStream_.Flush();
			if (this.cryptoTransform_ != null)
			{
				if (this.cryptoTransform_ is ZipAESTransform)
				{
					this.AESAuthCode = ((ZipAESTransform)this.cryptoTransform_).GetAuthCode();
				}
				this.cryptoTransform_.Dispose();
				this.cryptoTransform_ = null;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004A3F File Offset: 0x00003A3F
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00004A47 File Offset: 0x00003A47
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004A50 File Offset: 0x00003A50
		public bool CanPatchEntries
		{
			get
			{
				return this.baseOutputStream_.CanSeek;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004A5D File Offset: 0x00003A5D
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00004A65 File Offset: 0x00003A65
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				if (value != null && value.Length == 0)
				{
					this.password = null;
					return;
				}
				this.password = value;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004A81 File Offset: 0x00003A81
		protected void EncryptBlock(byte[] buffer, int offset, int length)
		{
			this.cryptoTransform_.TransformBlock(buffer, 0, length, buffer, 0);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004A94 File Offset: 0x00003A94
		protected void InitializePassword(string password)
		{
			PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
			byte[] rgbKey = PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(password));
			this.cryptoTransform_ = pkzipClassicManaged.CreateEncryptor(rgbKey, null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004AC4 File Offset: 0x00003AC4
		protected void InitializeAESPassword(ZipEntry entry, string rawPassword, out byte[] salt, out byte[] pwdVerifier)
		{
			salt = new byte[entry.AESSaltLen];
			if (DeflaterOutputStream._aesRnd == null)
			{
				DeflaterOutputStream._aesRnd = new RNGCryptoServiceProvider();
			}
			DeflaterOutputStream._aesRnd.GetBytes(salt);
			int blockSize = entry.AESKeySize / 8;
			this.cryptoTransform_ = new ZipAESTransform(rawPassword, salt, blockSize, true);
			pwdVerifier = ((ZipAESTransform)this.cryptoTransform_).PwdVerifier;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004B28 File Offset: 0x00003B28
		protected void Deflate()
		{
			while (!this.deflater_.IsNeedingInput)
			{
				int num = this.deflater_.Deflate(this.buffer_, 0, this.buffer_.Length);
				if (num <= 0)
				{
					break;
				}
				if (this.cryptoTransform_ != null)
				{
					this.EncryptBlock(this.buffer_, 0, num);
				}
				this.baseOutputStream_.Write(this.buffer_, 0, num);
			}
			if (!this.deflater_.IsNeedingInput)
			{
				throw new SharpZipBaseException("DeflaterOutputStream can't deflate all input?");
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004BA4 File Offset: 0x00003BA4
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004BA7 File Offset: 0x00003BA7
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004BAA File Offset: 0x00003BAA
		public override bool CanWrite
		{
			get
			{
				return this.baseOutputStream_.CanWrite;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004BB7 File Offset: 0x00003BB7
		public override long Length
		{
			get
			{
				return this.baseOutputStream_.Length;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004BC4 File Offset: 0x00003BC4
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004BD1 File Offset: 0x00003BD1
		public override long Position
		{
			get
			{
				return this.baseOutputStream_.Position;
			}
			set
			{
				throw new NotSupportedException("Position property not supported");
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004BDD File Offset: 0x00003BDD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("DeflaterOutputStream Seek not supported");
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004BE9 File Offset: 0x00003BE9
		public override void SetLength(long value)
		{
			throw new NotSupportedException("DeflaterOutputStream SetLength not supported");
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004BF5 File Offset: 0x00003BF5
		public override int ReadByte()
		{
			throw new NotSupportedException("DeflaterOutputStream ReadByte not supported");
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004C01 File Offset: 0x00003C01
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("DeflaterOutputStream Read not supported");
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004C0D File Offset: 0x00003C0D
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException("DeflaterOutputStream BeginRead not currently supported");
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004C19 File Offset: 0x00003C19
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException("BeginWrite is not supported");
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004C25 File Offset: 0x00003C25
		public override void Flush()
		{
			this.deflater_.Flush();
			this.Deflate();
			this.baseOutputStream_.Flush();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004C44 File Offset: 0x00003C44
		public override void Close()
		{
			if (!this.isClosed_)
			{
				this.isClosed_ = true;
				try
				{
					this.Finish();
					if (this.cryptoTransform_ != null)
					{
						this.GetAuthCodeIfAES();
						this.cryptoTransform_.Dispose();
						this.cryptoTransform_ = null;
					}
				}
				finally
				{
					if (this.isStreamOwner_)
					{
						this.baseOutputStream_.Close();
					}
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004CAC File Offset: 0x00003CAC
		private void GetAuthCodeIfAES()
		{
			if (this.cryptoTransform_ is ZipAESTransform)
			{
				this.AESAuthCode = ((ZipAESTransform)this.cryptoTransform_).GetAuthCode();
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004CD4 File Offset: 0x00003CD4
		public override void WriteByte(byte value)
		{
			this.Write(new byte[]
			{
				value
			}, 0, 1);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004CF5 File Offset: 0x00003CF5
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.deflater_.SetInput(buffer, offset, count);
			this.Deflate();
		}

		// Token: 0x0400005D RID: 93
		private string password;

		// Token: 0x0400005E RID: 94
		private ICryptoTransform cryptoTransform_;

		// Token: 0x0400005F RID: 95
		protected byte[] AESAuthCode;

		// Token: 0x04000060 RID: 96
		private byte[] buffer_;

		// Token: 0x04000061 RID: 97
		protected Deflater deflater_;

		// Token: 0x04000062 RID: 98
		protected Stream baseOutputStream_;

		// Token: 0x04000063 RID: 99
		private bool isClosed_;

		// Token: 0x04000064 RID: 100
		private bool isStreamOwner_ = true;

		// Token: 0x04000065 RID: 101
		private static RNGCryptoServiceProvider _aesRnd;
	}
}
