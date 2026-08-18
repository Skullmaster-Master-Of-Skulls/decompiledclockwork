using System;
using System.Globalization;
using System.IO;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020005F7 RID: 1527
	public class ByteBuffer : Stream
	{
		// Token: 0x060033EE RID: 13294 RVA: 0x00141C38 File Offset: 0x00140C38
		public ByteBuffer() : this(128)
		{
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x00141C45 File Offset: 0x00140C45
		public ByteBuffer(int size)
		{
			if (size < 1)
			{
				size = 128;
			}
			this.buf = new byte[size];
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x00141C64 File Offset: 0x00140C64
		public static void SetCacheSize(int size)
		{
			if (size > 3276700)
			{
				size = 3276700;
			}
			if (size <= ByteBuffer.byteCacheSize)
			{
				return;
			}
			byte[][] destinationArray = new byte[size][];
			Array.Copy(ByteBuffer.byteCache, 0, destinationArray, 0, ByteBuffer.byteCacheSize);
			ByteBuffer.byteCache = destinationArray;
			ByteBuffer.byteCacheSize = size;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x00141CB0 File Offset: 0x00140CB0
		public static void FillCache(int decimals)
		{
			int num = 1;
			switch (decimals)
			{
			case 0:
				num = 100;
				break;
			case 1:
				num = 10;
				break;
			}
			for (int i = 1; i < ByteBuffer.byteCacheSize; i += num)
			{
				if (ByteBuffer.byteCache[i] == null)
				{
					ByteBuffer.byteCache[i] = ByteBuffer.ConvertToBytes(i);
				}
			}
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x00141D00 File Offset: 0x00140D00
		private static byte[] ConvertToBytes(int i)
		{
			int num = (int)Math.Floor(Math.Log((double)i) / Math.Log(10.0));
			if (i % 100 != 0)
			{
				num += 2;
			}
			if (i % 10 != 0)
			{
				num++;
			}
			if (i < 100)
			{
				num++;
				if (i < 10)
				{
					num++;
				}
			}
			num--;
			byte[] array = new byte[num];
			num--;
			if (i < 100)
			{
				array[0] = 48;
			}
			if (i % 10 != 0)
			{
				array[num--] = ByteBuffer.bytes[i % 10];
			}
			if (i % 100 != 0)
			{
				array[num--] = ByteBuffer.bytes[i / 10 % 10];
				array[num--] = 46;
			}
			num = (int)Math.Floor(Math.Log((double)i) / Math.Log(10.0)) - 1;
			for (int j = 0; j < num; j++)
			{
				array[j] = ByteBuffer.bytes[i / (int)Math.Pow(10.0, (double)(num - j + 1)) % 10];
			}
			return array;
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x00141DF4 File Offset: 0x00140DF4
		public ByteBuffer Append_i(int b)
		{
			int num = this.count + 1;
			if (num > this.buf.Length)
			{
				byte[] destinationArray = new byte[Math.Max(this.buf.Length << 1, num)];
				Array.Copy(this.buf, 0, destinationArray, 0, this.count);
				this.buf = destinationArray;
			}
			this.buf[this.count] = (byte)b;
			this.count = num;
			return this;
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x00141E60 File Offset: 0x00140E60
		public ByteBuffer Append(byte[] b, int off, int len)
		{
			if (off < 0 || off > b.Length || len < 0 || off + len > b.Length || off + len < 0 || len == 0)
			{
				return this;
			}
			int num = this.count + len;
			if (num > this.buf.Length)
			{
				byte[] destinationArray = new byte[Math.Max(this.buf.Length << 1, num)];
				Array.Copy(this.buf, 0, destinationArray, 0, this.count);
				this.buf = destinationArray;
			}
			Array.Copy(b, off, this.buf, this.count, len);
			this.count = num;
			return this;
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x00141EEF File Offset: 0x00140EEF
		public ByteBuffer Append(byte[] b)
		{
			return this.Append(b, 0, b.Length);
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x00141EFC File Offset: 0x00140EFC
		public ByteBuffer Append(string str)
		{
			if (str != null)
			{
				return this.Append(DocWriter.GetISOBytes(str));
			}
			return this;
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x00141F0F File Offset: 0x00140F0F
		public ByteBuffer Append(char c)
		{
			return this.Append_i((int)c);
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x00141F18 File Offset: 0x00140F18
		public ByteBuffer Append(ByteBuffer buf)
		{
			return this.Append(buf.buf, 0, buf.count);
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x00141F2D File Offset: 0x00140F2D
		public ByteBuffer Append(int i)
		{
			return this.Append((double)i);
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x00141F37 File Offset: 0x00140F37
		public ByteBuffer Append(byte b)
		{
			return this.Append_i((int)b);
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x00141F40 File Offset: 0x00140F40
		public ByteBuffer AppendHex(byte b)
		{
			this.Append(ByteBuffer.bytes[b >> 4 & 15]);
			return this.Append(ByteBuffer.bytes[(int)(b & 15)]);
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x00141F65 File Offset: 0x00140F65
		public ByteBuffer Append(float i)
		{
			return this.Append((double)i);
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x00141F6F File Offset: 0x00140F6F
		public ByteBuffer Append(double d)
		{
			this.Append(ByteBuffer.FormatDouble(d, this));
			return this;
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x00141F80 File Offset: 0x00140F80
		public static string FormatDouble(double d)
		{
			return ByteBuffer.FormatDouble(d, null);
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x00141F8C File Offset: 0x00140F8C
		public static string FormatDouble(double d, ByteBuffer buf)
		{
			if (ByteBuffer.HIGH_PRECISION)
			{
				string text = d.ToString("0.######", CultureInfo.InvariantCulture);
				if (buf == null)
				{
					return text;
				}
				buf.Append(text);
				return null;
			}
			else
			{
				bool flag = false;
				if (Math.Abs(d) < 1.5E-05)
				{
					if (buf != null)
					{
						buf.Append(48);
						return null;
					}
					return "0";
				}
				else
				{
					if (d < 0.0)
					{
						flag = true;
						d = -d;
					}
					if (d < 1.0)
					{
						d += 5E-06;
						if (d >= 1.0)
						{
							if (flag)
							{
								if (buf != null)
								{
									buf.Append(45);
									buf.Append(49);
									return null;
								}
								return "-1";
							}
							else
							{
								if (buf != null)
								{
									buf.Append(49);
									return null;
								}
								return "1";
							}
						}
						else
						{
							if (buf != null)
							{
								int num = (int)(d * 100000.0);
								if (flag)
								{
									buf.Append(45);
								}
								buf.Append(48);
								buf.Append(46);
								buf.Append((byte)(num / 10000 + 48));
								if (num % 10000 != 0)
								{
									buf.Append((byte)(num / 1000 % 10 + 48));
									if (num % 1000 != 0)
									{
										buf.Append((byte)(num / 100 % 10 + 48));
										if (num % 100 != 0)
										{
											buf.Append((byte)(num / 10 % 10 + 48));
											if (num % 10 != 0)
											{
												buf.Append((byte)(num % 10 + 48));
											}
										}
									}
								}
								return null;
							}
							int num2 = 100000;
							int i = (int)(d * (double)num2);
							StringBuilder stringBuilder = new StringBuilder();
							if (flag)
							{
								stringBuilder.Append('-');
							}
							stringBuilder.Append("0.");
							while (i < num2 / 10)
							{
								stringBuilder.Append('0');
								num2 /= 10;
							}
							stringBuilder.Append(i);
							int num3 = stringBuilder.Length - 1;
							while (stringBuilder[num3] == '0')
							{
								num3--;
							}
							stringBuilder.Length = num3 + 1;
							return stringBuilder.ToString();
						}
					}
					else
					{
						if (d > 32767.0)
						{
							StringBuilder stringBuilder2 = new StringBuilder();
							if (flag)
							{
								stringBuilder2.Append('-');
							}
							d += 0.5;
							long value = (long)d;
							return stringBuilder2.Append(value).ToString();
						}
						d += 0.005;
						int num4 = (int)(d * 100.0);
						if (num4 < ByteBuffer.byteCacheSize && ByteBuffer.byteCache[num4] != null)
						{
							if (buf != null)
							{
								if (flag)
								{
									buf.Append(45);
								}
								buf.Append(ByteBuffer.byteCache[num4]);
								return null;
							}
							string text2 = PdfEncodings.ConvertToString(ByteBuffer.byteCache[num4], null);
							if (flag)
							{
								text2 = "-" + text2;
							}
							return text2;
						}
						else
						{
							if (buf != null)
							{
								if (num4 < ByteBuffer.byteCacheSize)
								{
									int num5 = 0;
									if (num4 >= 1000000)
									{
										num5 += 5;
									}
									else if (num4 >= 100000)
									{
										num5 += 4;
									}
									else if (num4 >= 10000)
									{
										num5 += 3;
									}
									else if (num4 >= 1000)
									{
										num5 += 2;
									}
									else if (num4 >= 100)
									{
										num5++;
									}
									if (num4 % 100 != 0)
									{
										num5 += 2;
									}
									if (num4 % 10 != 0)
									{
										num5++;
									}
									byte[] array = new byte[num5];
									int num6 = 0;
									if (num4 >= 1000000)
									{
										array[num6++] = ByteBuffer.bytes[num4 / 1000000];
									}
									if (num4 >= 100000)
									{
										array[num6++] = ByteBuffer.bytes[num4 / 100000 % 10];
									}
									if (num4 >= 10000)
									{
										array[num6++] = ByteBuffer.bytes[num4 / 10000 % 10];
									}
									if (num4 >= 1000)
									{
										array[num6++] = ByteBuffer.bytes[num4 / 1000 % 10];
									}
									if (num4 >= 100)
									{
										array[num6++] = ByteBuffer.bytes[num4 / 100 % 10];
									}
									if (num4 % 100 != 0)
									{
										array[num6++] = 46;
										array[num6++] = ByteBuffer.bytes[num4 / 10 % 10];
										if (num4 % 10 != 0)
										{
											array[num6++] = ByteBuffer.bytes[num4 % 10];
										}
									}
									ByteBuffer.byteCache[num4] = array;
								}
								if (flag)
								{
									buf.Append(45);
								}
								if (num4 >= 1000000)
								{
									buf.Append(ByteBuffer.bytes[num4 / 1000000]);
								}
								if (num4 >= 100000)
								{
									buf.Append(ByteBuffer.bytes[num4 / 100000 % 10]);
								}
								if (num4 >= 10000)
								{
									buf.Append(ByteBuffer.bytes[num4 / 10000 % 10]);
								}
								if (num4 >= 1000)
								{
									buf.Append(ByteBuffer.bytes[num4 / 1000 % 10]);
								}
								if (num4 >= 100)
								{
									buf.Append(ByteBuffer.bytes[num4 / 100 % 10]);
								}
								if (num4 % 100 != 0)
								{
									buf.Append(46);
									buf.Append(ByteBuffer.bytes[num4 / 10 % 10]);
									if (num4 % 10 != 0)
									{
										buf.Append(ByteBuffer.bytes[num4 % 10]);
									}
								}
								return null;
							}
							StringBuilder stringBuilder3 = new StringBuilder();
							if (flag)
							{
								stringBuilder3.Append('-');
							}
							if (num4 >= 1000000)
							{
								stringBuilder3.Append(ByteBuffer.chars[num4 / 1000000]);
							}
							if (num4 >= 100000)
							{
								stringBuilder3.Append(ByteBuffer.chars[num4 / 100000 % 10]);
							}
							if (num4 >= 10000)
							{
								stringBuilder3.Append(ByteBuffer.chars[num4 / 10000 % 10]);
							}
							if (num4 >= 1000)
							{
								stringBuilder3.Append(ByteBuffer.chars[num4 / 1000 % 10]);
							}
							if (num4 >= 100)
							{
								stringBuilder3.Append(ByteBuffer.chars[num4 / 100 % 10]);
							}
							if (num4 % 100 != 0)
							{
								stringBuilder3.Append('.');
								stringBuilder3.Append(ByteBuffer.chars[num4 / 10 % 10]);
								if (num4 % 10 != 0)
								{
									stringBuilder3.Append(ByteBuffer.chars[num4 % 10]);
								}
							}
							return stringBuilder3.ToString();
						}
					}
				}
			}
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x001425A6 File Offset: 0x001415A6
		public void Reset()
		{
			this.count = 0;
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x001425B0 File Offset: 0x001415B0
		public byte[] ToByteArray()
		{
			byte[] array = new byte[this.count];
			Array.Copy(this.buf, 0, array, 0, this.count);
			return array;
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06003402 RID: 13314 RVA: 0x001425DE File Offset: 0x001415DE
		// (set) Token: 0x06003403 RID: 13315 RVA: 0x001425E6 File Offset: 0x001415E6
		public int Size
		{
			get
			{
				return this.count;
			}
			set
			{
				if (value > this.count || value < 0)
				{
					throw new ArgumentOutOfRangeException(MessageLocalization.GetComposedMessage("the.new.size.must.be.positive.and.lt.eq.of.the.current.size"));
				}
				this.count = value;
			}
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x0014260C File Offset: 0x0014160C
		public override string ToString()
		{
			char[] value = this.ConvertToChar(this.buf);
			return new string(value, 0, this.count);
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x00142633 File Offset: 0x00141633
		public void WriteTo(Stream str)
		{
			str.Write(this.buf, 0, this.count);
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x00142648 File Offset: 0x00141648
		private char[] ConvertToChar(byte[] buf)
		{
			char[] array = new char[this.count + 1];
			for (int i = 0; i <= this.count; i++)
			{
				array[i] = (char)buf[i];
			}
			return array;
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06003407 RID: 13319 RVA: 0x0014267B File Offset: 0x0014167B
		public byte[] Buffer
		{
			get
			{
				return this.buf;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06003408 RID: 13320 RVA: 0x00142683 File Offset: 0x00141683
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06003409 RID: 13321 RVA: 0x00142686 File Offset: 0x00141686
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600340A RID: 13322 RVA: 0x00142689 File Offset: 0x00141689
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600340B RID: 13323 RVA: 0x0014268C File Offset: 0x0014168C
		public override long Length
		{
			get
			{
				return (long)this.count;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x0600340C RID: 13324 RVA: 0x00142695 File Offset: 0x00141695
		// (set) Token: 0x0600340D RID: 13325 RVA: 0x0014269E File Offset: 0x0014169E
		public override long Position
		{
			get
			{
				return (long)this.count;
			}
			set
			{
			}
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x001426A0 File Offset: 0x001416A0
		public override void Flush()
		{
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x001426A2 File Offset: 0x001416A2
		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x001426A5 File Offset: 0x001416A5
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x001426A9 File Offset: 0x001416A9
		public override void SetLength(long value)
		{
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x001426AB File Offset: 0x001416AB
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.Append(buffer, offset, count);
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x001426B7 File Offset: 0x001416B7
		public override void WriteByte(byte value)
		{
			this.Append(value);
		}

		// Token: 0x04002306 RID: 8966
		public const byte ZERO = 48;

		// Token: 0x04002307 RID: 8967
		protected int count;

		// Token: 0x04002308 RID: 8968
		protected byte[] buf;

		// Token: 0x04002309 RID: 8969
		private static int byteCacheSize = 0;

		// Token: 0x0400230A RID: 8970
		private static byte[][] byteCache = new byte[ByteBuffer.byteCacheSize][];

		// Token: 0x0400230B RID: 8971
		private static char[] chars = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9'
		};

		// Token: 0x0400230C RID: 8972
		private static byte[] bytes = new byte[]
		{
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			97,
			98,
			99,
			100,
			101,
			102
		};

		// Token: 0x0400230D RID: 8973
		public static bool HIGH_PRECISION = false;
	}
}
