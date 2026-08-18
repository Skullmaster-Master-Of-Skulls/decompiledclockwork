using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x020005C8 RID: 1480
	[ComVisible(true)]
	[Serializable]
	public class StreamReader : TextReader
	{
		// Token: 0x060036E4 RID: 14052 RVA: 0x000B989F File Offset: 0x000B889F
		internal StreamReader()
		{
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000B98A7 File Offset: 0x000B88A7
		public StreamReader(Stream stream) : this(stream, true)
		{
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000B98B1 File Offset: 0x000B88B1
		public StreamReader(Stream stream, bool detectEncodingFromByteOrderMarks) : this(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000B98C5 File Offset: 0x000B88C5
		public StreamReader(Stream stream, Encoding encoding) : this(stream, encoding, true, 1024)
		{
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000B98D5 File Offset: 0x000B88D5
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks) : this(stream, encoding, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000B98E8 File Offset: 0x000B88E8
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
		{
			if (stream == null || encoding == null)
			{
				throw new ArgumentNullException((stream == null) ? "stream" : "encoding");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"));
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize);
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000B9953 File Offset: 0x000B8953
		internal StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool closable) : this(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize)
		{
			this._closable = closable;
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000B9968 File Offset: 0x000B8968
		public StreamReader(string path) : this(path, true)
		{
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000B9972 File Offset: 0x000B8972
		public StreamReader(string path, bool detectEncodingFromByteOrderMarks) : this(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000B9986 File Offset: 0x000B8986
		public StreamReader(string path, Encoding encoding) : this(path, encoding, true, 1024)
		{
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000B9996 File Offset: 0x000B8996
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks) : this(path, encoding, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000B99A8 File Offset: 0x000B89A8
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
		{
			if (path == null || encoding == null)
			{
				throw new ArgumentNullException((path == null) ? "path" : "encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan);
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize);
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000B9A28 File Offset: 0x000B8A28
		private void Init(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
		{
			this.stream = stream;
			this.encoding = encoding;
			this.decoder = encoding.GetDecoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this.byteBuffer = new byte[bufferSize];
			this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
			this.charBuffer = new char[this._maxCharsPerBuffer];
			this.byteLen = 0;
			this.bytePos = 0;
			this._detectEncoding = detectEncodingFromByteOrderMarks;
			this._preamble = encoding.GetPreamble();
			this._checkPreamble = (this._preamble.Length > 0);
			this._isBlocked = false;
			this._closable = true;
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000B9ACB File Offset: 0x000B8ACB
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000B9AD4 File Offset: 0x000B8AD4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.Closable && disposing && this.stream != null)
				{
					this.stream.Close();
				}
			}
			finally
			{
				if (this.Closable && this.stream != null)
				{
					this.stream = null;
					this.encoding = null;
					this.decoder = null;
					this.byteBuffer = null;
					this.charBuffer = null;
					this.charPos = 0;
					this.charLen = 0;
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060036F3 RID: 14067 RVA: 0x000B9B5C File Offset: 0x000B8B5C
		public virtual Encoding CurrentEncoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060036F4 RID: 14068 RVA: 0x000B9B64 File Offset: 0x000B8B64
		public virtual Stream BaseStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x000B9B6C File Offset: 0x000B8B6C
		internal bool Closable
		{
			get
			{
				return this._closable;
			}
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000B9B74 File Offset: 0x000B8B74
		public void DiscardBufferedData()
		{
			this.byteLen = 0;
			this.charLen = 0;
			this.charPos = 0;
			this.decoder = this.encoding.GetDecoder();
			this._isBlocked = false;
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x000B9BA4 File Offset: 0x000B8BA4
		public bool EndOfStream
		{
			get
			{
				if (this.stream == null)
				{
					__Error.ReaderClosed();
				}
				if (this.charPos < this.charLen)
				{
					return false;
				}
				int num = this.ReadBuffer();
				return num == 0;
			}
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x000B9BD9 File Offset: 0x000B8BD9
		public override int Peek()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			if (this.charPos == this.charLen && (this._isBlocked || this.ReadBuffer() == 0))
			{
				return -1;
			}
			return (int)this.charBuffer[this.charPos];
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000B9C18 File Offset: 0x000B8C18
		public override int Read()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			if (this.charPos == this.charLen && this.ReadBuffer() == 0)
			{
				return -1;
			}
			int result = (int)this.charBuffer[this.charPos];
			this.charPos++;
			return result;
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x000B9C68 File Offset: 0x000B8C68
		public override int Read([In] [Out] char[] buffer, int index, int count)
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int num = 0;
			bool flag = false;
			while (count > 0)
			{
				int num2 = this.charLen - this.charPos;
				if (num2 == 0)
				{
					num2 = this.ReadBuffer(buffer, index + num, count, out flag);
				}
				if (num2 == 0)
				{
					break;
				}
				if (num2 > count)
				{
					num2 = count;
				}
				if (!flag)
				{
					Buffer.InternalBlockCopy(this.charBuffer, this.charPos * 2, buffer, (index + num) * 2, num2 * 2);
					this.charPos += num2;
				}
				num += num2;
				count -= num2;
				if (this._isBlocked)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x000B9D4C File Offset: 0x000B8D4C
		public override string ReadToEnd()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			StringBuilder stringBuilder = new StringBuilder(this.charLen - this.charPos);
			do
			{
				stringBuilder.Append(this.charBuffer, this.charPos, this.charLen - this.charPos);
				this.charPos = this.charLen;
				this.ReadBuffer();
			}
			while (this.charLen > 0);
			return stringBuilder.ToString();
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x000B9DBB File Offset: 0x000B8DBB
		private void CompressBuffer(int n)
		{
			Buffer.InternalBlockCopy(this.byteBuffer, n, this.byteBuffer, 0, this.byteLen - n);
			this.byteLen -= n;
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x000B9DE8 File Offset: 0x000B8DE8
		private void DetectEncoding()
		{
			if (this.byteLen < 2)
			{
				return;
			}
			this._detectEncoding = false;
			bool flag = false;
			if (this.byteBuffer[0] == 254 && this.byteBuffer[1] == 255)
			{
				this.encoding = new UnicodeEncoding(true, true);
				this.CompressBuffer(2);
				flag = true;
			}
			else if (this.byteBuffer[0] == 255 && this.byteBuffer[1] == 254)
			{
				if (this.byteLen >= 4 && this.byteBuffer[2] == 0 && this.byteBuffer[3] == 0)
				{
					this.encoding = new UTF32Encoding(false, true);
					this.CompressBuffer(4);
				}
				else
				{
					this.encoding = new UnicodeEncoding(false, true);
					this.CompressBuffer(2);
				}
				flag = true;
			}
			else if (this.byteLen >= 3 && this.byteBuffer[0] == 239 && this.byteBuffer[1] == 187 && this.byteBuffer[2] == 191)
			{
				this.encoding = Encoding.UTF8;
				this.CompressBuffer(3);
				flag = true;
			}
			else if (this.byteLen >= 4 && this.byteBuffer[0] == 0 && this.byteBuffer[1] == 0 && this.byteBuffer[2] == 254 && this.byteBuffer[3] == 255)
			{
				this.encoding = new UTF32Encoding(true, true);
				flag = true;
			}
			else if (this.byteLen == 2)
			{
				this._detectEncoding = true;
			}
			if (flag)
			{
				this.decoder = this.encoding.GetDecoder();
				this._maxCharsPerBuffer = this.encoding.GetMaxCharCount(this.byteBuffer.Length);
				this.charBuffer = new char[this._maxCharsPerBuffer];
			}
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x000B9F94 File Offset: 0x000B8F94
		private bool IsPreamble()
		{
			if (!this._checkPreamble)
			{
				return this._checkPreamble;
			}
			int num = (this.byteLen >= this._preamble.Length) ? (this._preamble.Length - this.bytePos) : (this.byteLen - this.bytePos);
			int i = 0;
			while (i < num)
			{
				if (this.byteBuffer[this.bytePos] != this._preamble[this.bytePos])
				{
					this.bytePos = 0;
					this._checkPreamble = false;
					break;
				}
				i++;
				this.bytePos++;
			}
			if (this._checkPreamble && this.bytePos == this._preamble.Length)
			{
				this.CompressBuffer(this._preamble.Length);
				this.bytePos = 0;
				this._checkPreamble = false;
				this._detectEncoding = false;
			}
			return this._checkPreamble;
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x000BA068 File Offset: 0x000B9068
		private int ReadBuffer()
		{
			this.charLen = 0;
			this.charPos = 0;
			if (!this._checkPreamble)
			{
				this.byteLen = 0;
			}
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
					if (num == 0)
					{
						break;
					}
					this.byteLen += num;
				}
				else
				{
					this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
					if (this.byteLen == 0)
					{
						goto Block_5;
					}
				}
				this._isBlocked = (this.byteLen < this.byteBuffer.Length);
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this.byteLen >= 2)
					{
						this.DetectEncoding();
					}
					this.charLen += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
				}
				if (this.charLen != 0)
				{
					goto Block_9;
				}
			}
			if (this.byteLen > 0)
			{
				this.charLen += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
			}
			return this.charLen;
			Block_5:
			return this.charLen;
			Block_9:
			return this.charLen;
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000BA1BC File Offset: 0x000B91BC
		private int ReadBuffer(char[] userBuffer, int userOffset, int desiredChars, out bool readToUserBuffer)
		{
			this.charLen = 0;
			this.charPos = 0;
			if (!this._checkPreamble)
			{
				this.byteLen = 0;
			}
			int num = 0;
			readToUserBuffer = (desiredChars >= this._maxCharsPerBuffer);
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num2 = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
					if (num2 == 0)
					{
						break;
					}
					this.byteLen += num2;
				}
				else
				{
					this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
					if (this.byteLen == 0)
					{
						return num;
					}
				}
				this._isBlocked = (this.byteLen < this.byteBuffer.Length);
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this.byteLen >= 2)
					{
						this.DetectEncoding();
						readToUserBuffer = (desiredChars >= this._maxCharsPerBuffer);
					}
					this.charPos = 0;
					if (readToUserBuffer)
					{
						num += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + num);
						this.charLen = 0;
					}
					else
					{
						num = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, num);
						this.charLen += num;
					}
				}
				if (num != 0)
				{
					goto Block_11;
				}
			}
			if (this.byteLen > 0)
			{
				if (readToUserBuffer)
				{
					num += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + num);
					this.charLen = 0;
				}
				else
				{
					num = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, num);
					this.charLen += num;
				}
			}
			return num;
			Block_11:
			this._isBlocked &= (num < desiredChars);
			return num;
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000BA390 File Offset: 0x000B9390
		public override string ReadLine()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			if (this.charPos == this.charLen && this.ReadBuffer() == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int num;
			char c;
			for (;;)
			{
				num = this.charPos;
				do
				{
					c = this.charBuffer[num];
					if (c == '\r' || c == '\n')
					{
						goto IL_44;
					}
					num++;
				}
				while (num < this.charLen);
				num = this.charLen - this.charPos;
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(num + 80);
				}
				stringBuilder.Append(this.charBuffer, this.charPos, num);
				if (this.ReadBuffer() <= 0)
				{
					goto Block_11;
				}
			}
			IL_44:
			string result;
			if (stringBuilder != null)
			{
				stringBuilder.Append(this.charBuffer, this.charPos, num - this.charPos);
				result = stringBuilder.ToString();
			}
			else
			{
				result = new string(this.charBuffer, this.charPos, num - this.charPos);
			}
			this.charPos = num + 1;
			if (c == '\r' && (this.charPos < this.charLen || this.ReadBuffer() > 0) && this.charBuffer[this.charPos] == '\n')
			{
				this.charPos++;
			}
			return result;
			Block_11:
			return stringBuilder.ToString();
		}

		// Token: 0x04001CAF RID: 7343
		internal const int DefaultBufferSize = 1024;

		// Token: 0x04001CB0 RID: 7344
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x04001CB1 RID: 7345
		private const int MinBufferSize = 128;

		// Token: 0x04001CB2 RID: 7346
		public new static readonly StreamReader Null = new StreamReader.NullStreamReader();

		// Token: 0x04001CB3 RID: 7347
		private bool _closable;

		// Token: 0x04001CB4 RID: 7348
		private Stream stream;

		// Token: 0x04001CB5 RID: 7349
		private Encoding encoding;

		// Token: 0x04001CB6 RID: 7350
		private Decoder decoder;

		// Token: 0x04001CB7 RID: 7351
		private byte[] byteBuffer;

		// Token: 0x04001CB8 RID: 7352
		private char[] charBuffer;

		// Token: 0x04001CB9 RID: 7353
		private byte[] _preamble;

		// Token: 0x04001CBA RID: 7354
		private int charPos;

		// Token: 0x04001CBB RID: 7355
		private int charLen;

		// Token: 0x04001CBC RID: 7356
		private int byteLen;

		// Token: 0x04001CBD RID: 7357
		private int bytePos;

		// Token: 0x04001CBE RID: 7358
		private int _maxCharsPerBuffer;

		// Token: 0x04001CBF RID: 7359
		private bool _detectEncoding;

		// Token: 0x04001CC0 RID: 7360
		private bool _checkPreamble;

		// Token: 0x04001CC1 RID: 7361
		private bool _isBlocked;

		// Token: 0x020005C9 RID: 1481
		private class NullStreamReader : StreamReader
		{
			// Token: 0x06003703 RID: 14083 RVA: 0x000BA4C5 File Offset: 0x000B94C5
			internal NullStreamReader() : base(Stream.Null, Encoding.Unicode, false, 1)
			{
			}

			// Token: 0x1700094C RID: 2380
			// (get) Token: 0x06003704 RID: 14084 RVA: 0x000BA4D9 File Offset: 0x000B94D9
			public override Stream BaseStream
			{
				get
				{
					return Stream.Null;
				}
			}

			// Token: 0x1700094D RID: 2381
			// (get) Token: 0x06003705 RID: 14085 RVA: 0x000BA4E0 File Offset: 0x000B94E0
			public override Encoding CurrentEncoding
			{
				get
				{
					return Encoding.Unicode;
				}
			}

			// Token: 0x06003706 RID: 14086 RVA: 0x000BA4E7 File Offset: 0x000B94E7
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x06003707 RID: 14087 RVA: 0x000BA4E9 File Offset: 0x000B94E9
			public override int Peek()
			{
				return -1;
			}

			// Token: 0x06003708 RID: 14088 RVA: 0x000BA4EC File Offset: 0x000B94EC
			public override int Read()
			{
				return -1;
			}

			// Token: 0x06003709 RID: 14089 RVA: 0x000BA4EF File Offset: 0x000B94EF
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x0600370A RID: 14090 RVA: 0x000BA4F2 File Offset: 0x000B94F2
			public override string ReadLine()
			{
				return null;
			}

			// Token: 0x0600370B RID: 14091 RVA: 0x000BA4F5 File Offset: 0x000B94F5
			public override string ReadToEnd()
			{
				return string.Empty;
			}
		}
	}
}
