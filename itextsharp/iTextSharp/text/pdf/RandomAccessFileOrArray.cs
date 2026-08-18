using System;
using System.IO;
using System.Net;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200026F RID: 623
	public class RandomAccessFileOrArray
	{
		// Token: 0x06001759 RID: 5977 RVA: 0x000862AD File Offset: 0x000852AD
		public RandomAccessFileOrArray(string filename) : this(filename, false)
		{
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000862B8 File Offset: 0x000852B8
		public RandomAccessFileOrArray(string filename, bool forceRead)
		{
			if (!File.Exists(filename))
			{
				if (filename.StartsWith("file:/") || filename.StartsWith("http://") || filename.StartsWith("https://"))
				{
					Stream responseStream = WebRequest.Create(new Uri(filename)).GetResponse().GetResponseStream();
					try
					{
						this.arrayIn = RandomAccessFileOrArray.InputStreamToArray(responseStream);
						return;
					}
					finally
					{
						try
						{
							responseStream.Close();
						}
						catch
						{
						}
					}
				}
				Stream resourceStream = BaseFont.GetResourceStream(filename);
				if (resourceStream == null)
				{
					throw new IOException(MessageLocalization.GetComposedMessage("1.not.found.as.file.or.resource", filename));
				}
				try
				{
					this.arrayIn = RandomAccessFileOrArray.InputStreamToArray(resourceStream);
					return;
				}
				finally
				{
					try
					{
						resourceStream.Close();
					}
					catch
					{
					}
				}
			}
			if (!forceRead)
			{
				this.filename = filename;
				this.rf = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
				return;
			}
			Stream stream = null;
			try
			{
				stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
				this.arrayIn = RandomAccessFileOrArray.InputStreamToArray(stream);
			}
			finally
			{
				try
				{
					if (stream != null)
					{
						stream.Close();
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x000863F4 File Offset: 0x000853F4
		public RandomAccessFileOrArray(Uri url)
		{
			Stream responseStream = WebRequest.Create(url).GetResponse().GetResponseStream();
			try
			{
				this.arrayIn = RandomAccessFileOrArray.InputStreamToArray(responseStream);
			}
			finally
			{
				try
				{
					responseStream.Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00086450 File Offset: 0x00085450
		public RandomAccessFileOrArray(Stream isp)
		{
			this.arrayIn = RandomAccessFileOrArray.InputStreamToArray(isp);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00086464 File Offset: 0x00085464
		public static byte[] InputStreamToArray(Stream isp)
		{
			byte[] array = new byte[8192];
			MemoryStream memoryStream = new MemoryStream();
			for (;;)
			{
				int num = isp.Read(array, 0, array.Length);
				if (num < 1)
				{
					break;
				}
				memoryStream.Write(array, 0, num);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x000864A3 File Offset: 0x000854A3
		public RandomAccessFileOrArray(byte[] arrayIn)
		{
			this.arrayIn = arrayIn;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x000864B2 File Offset: 0x000854B2
		public RandomAccessFileOrArray(RandomAccessFileOrArray file)
		{
			this.filename = file.filename;
			this.arrayIn = file.arrayIn;
			this.startOffset = file.startOffset;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x000864DE File Offset: 0x000854DE
		public void PushBack(byte b)
		{
			this.back = b;
			this.isBack = true;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x000864F0 File Offset: 0x000854F0
		public int Read()
		{
			if (this.isBack)
			{
				this.isBack = false;
				return (int)(this.back & byte.MaxValue);
			}
			if (this.arrayIn == null)
			{
				return this.rf.ReadByte();
			}
			if (this.arrayInPtr >= this.arrayIn.Length)
			{
				return -1;
			}
			return (int)(this.arrayIn[this.arrayInPtr++] & byte.MaxValue);
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00086560 File Offset: 0x00085560
		public int Read(byte[] b, int off, int len)
		{
			if (len == 0)
			{
				return 0;
			}
			int num = 0;
			if (this.isBack)
			{
				this.isBack = false;
				if (len == 1)
				{
					b[off] = this.back;
					return 1;
				}
				num = 1;
				b[off++] = this.back;
				len--;
			}
			if (this.arrayIn == null)
			{
				return this.rf.Read(b, off, len) + num;
			}
			if (this.arrayInPtr >= this.arrayIn.Length)
			{
				return -1;
			}
			if (this.arrayInPtr + len > this.arrayIn.Length)
			{
				len = this.arrayIn.Length - this.arrayInPtr;
			}
			Array.Copy(this.arrayIn, this.arrayInPtr, b, off, len);
			this.arrayInPtr += len;
			return len + num;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0008661A File Offset: 0x0008561A
		public int Read(byte[] b)
		{
			return this.Read(b, 0, b.Length);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00086627 File Offset: 0x00085627
		public void ReadFully(byte[] b)
		{
			this.ReadFully(b, 0, b.Length);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00086634 File Offset: 0x00085634
		public void ReadFully(byte[] b, int off, int len)
		{
			if (len == 0)
			{
				return;
			}
			int num = 0;
			for (;;)
			{
				int num2 = this.Read(b, off + num, len - num);
				if (num2 <= 0)
				{
					break;
				}
				num += num2;
				if (num >= len)
				{
					return;
				}
			}
			throw new EndOfStreamException();
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00086667 File Offset: 0x00085667
		public long Skip(long n)
		{
			return (long)this.SkipBytes((int)n);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00086674 File Offset: 0x00085674
		public int SkipBytes(int n)
		{
			if (n <= 0)
			{
				return 0;
			}
			int num = 0;
			if (this.isBack)
			{
				this.isBack = false;
				if (n == 1)
				{
					return 1;
				}
				n--;
				num = 1;
			}
			int filePointer = this.FilePointer;
			int length = this.Length;
			int num2 = filePointer + n;
			if (num2 > length)
			{
				num2 = length;
			}
			this.Seek(num2);
			return num2 - filePointer + num;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000866C9 File Offset: 0x000856C9
		public void ReOpen()
		{
			if (this.filename != null && this.rf == null)
			{
				this.rf = new FileStream(this.filename, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			this.Seek(0);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x000866F6 File Offset: 0x000856F6
		protected void InsureOpen()
		{
			if (this.filename != null && this.rf == null)
			{
				this.ReOpen();
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0008670E File Offset: 0x0008570E
		public bool IsOpen()
		{
			return this.filename == null || this.rf != null;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00086726 File Offset: 0x00085726
		public void Close()
		{
			this.isBack = false;
			if (this.rf != null)
			{
				this.rf.Close();
				this.rf = null;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x00086749 File Offset: 0x00085749
		public int Length
		{
			get
			{
				if (this.arrayIn == null)
				{
					this.InsureOpen();
					return (int)this.rf.Length - this.startOffset;
				}
				return this.arrayIn.Length - this.startOffset;
			}
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0008677C File Offset: 0x0008577C
		public void Seek(int pos)
		{
			pos += this.startOffset;
			this.isBack = false;
			if (this.arrayIn == null)
			{
				this.InsureOpen();
				this.rf.Position = (long)pos;
				return;
			}
			this.arrayInPtr = pos;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x000867B2 File Offset: 0x000857B2
		public void Seek(long pos)
		{
			this.Seek((int)pos);
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x000867BC File Offset: 0x000857BC
		public int FilePointer
		{
			get
			{
				this.InsureOpen();
				int num = this.isBack ? 1 : 0;
				if (this.arrayIn == null)
				{
					return (int)this.rf.Position - num - this.startOffset;
				}
				return this.arrayInPtr - num - this.startOffset;
			}
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0008680C File Offset: 0x0008580C
		public bool ReadBoolean()
		{
			int num = this.Read();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			return num != 0;
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00086834 File Offset: 0x00085834
		public byte ReadByte()
		{
			int num = this.Read();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			return (byte)num;
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00086854 File Offset: 0x00085854
		public int ReadUnsignedByte()
		{
			int num = this.Read();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			return num;
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00086874 File Offset: 0x00085874
		public short ReadShort()
		{
			int num = this.Read();
			int num2 = this.Read();
			if ((num | num2) < 0)
			{
				throw new EndOfStreamException();
			}
			return (short)((num << 8) + num2);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000868A4 File Offset: 0x000858A4
		public short ReadShortLE()
		{
			int num = this.Read();
			int num2 = this.Read();
			if ((num | num2) < 0)
			{
				throw new EndOfStreamException();
			}
			return (short)((num2 << 8) + num);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000868D4 File Offset: 0x000858D4
		public int ReadUnsignedShort()
		{
			int num = this.Read();
			int num2 = this.Read();
			if ((num | num2) < 0)
			{
				throw new EndOfStreamException();
			}
			return (num << 8) + num2;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00086900 File Offset: 0x00085900
		public int ReadUnsignedShortLE()
		{
			int num = this.Read();
			int num2 = this.Read();
			if ((num | num2) < 0)
			{
				throw new EndOfStreamException();
			}
			return (num2 << 8) + num;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0008692C File Offset: 0x0008592C
		public char ReadChar()
		{
			int num = this.Read();
			int num2 = this.Read();
			if ((num | num2) < 0)
			{
				throw new EndOfStreamException();
			}
			return (char)((num << 8) + num2);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0008695C File Offset: 0x0008595C
		public char ReadCharLE()
		{
			int num = this.Read();
			int num2 = this.Read();
			if ((num | num2) < 0)
			{
				throw new EndOfStreamException();
			}
			return (char)((num2 << 8) + num);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0008698C File Offset: 0x0008598C
		public int ReadInt()
		{
			int num = this.Read();
			int num2 = this.Read();
			int num3 = this.Read();
			int num4 = this.Read();
			if ((num | num2 | num3 | num4) < 0)
			{
				throw new EndOfStreamException();
			}
			return (num << 24) + (num2 << 16) + (num3 << 8) + num4;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x000869D4 File Offset: 0x000859D4
		public int ReadIntLE()
		{
			int num = this.Read();
			int num2 = this.Read();
			int num3 = this.Read();
			int num4 = this.Read();
			if ((num | num2 | num3 | num4) < 0)
			{
				throw new EndOfStreamException();
			}
			return (num4 << 24) + (num3 << 16) + (num2 << 8) + num;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00086A1C File Offset: 0x00085A1C
		public long ReadUnsignedInt()
		{
			long num = (long)this.Read();
			long num2 = (long)this.Read();
			long num3 = (long)this.Read();
			long num4 = (long)this.Read();
			if ((num | num2 | num3 | num4) < 0L)
			{
				throw new EndOfStreamException();
			}
			return (num << 24) + (num2 << 16) + (num3 << 8) + num4;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00086A6C File Offset: 0x00085A6C
		public long ReadUnsignedIntLE()
		{
			long num = (long)this.Read();
			long num2 = (long)this.Read();
			long num3 = (long)this.Read();
			long num4 = (long)this.Read();
			if ((num | num2 | num3 | num4) < 0L)
			{
				throw new EndOfStreamException();
			}
			return (num4 << 24) + (num3 << 16) + (num2 << 8) + num;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00086AB9 File Offset: 0x00085AB9
		public long ReadLong()
		{
			return ((long)this.ReadInt() << 32) + ((long)this.ReadInt() & (long)((ulong)-1));
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00086AD0 File Offset: 0x00085AD0
		public long ReadLongLE()
		{
			int num = this.ReadIntLE();
			int num2 = this.ReadIntLE();
			return ((long)num2 << 32) + ((long)num & (long)((ulong)-1));
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00086AF8 File Offset: 0x00085AF8
		public float ReadFloat()
		{
			int[] src = new int[]
			{
				this.ReadInt()
			};
			float[] array = new float[1];
			float[] array2 = array;
			Buffer.BlockCopy(src, 0, array2, 0, 4);
			return array2[0];
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00086B30 File Offset: 0x00085B30
		public float ReadFloatLE()
		{
			int[] src = new int[]
			{
				this.ReadIntLE()
			};
			float[] array = new float[1];
			float[] array2 = array;
			Buffer.BlockCopy(src, 0, array2, 0, 4);
			return array2[0];
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00086B68 File Offset: 0x00085B68
		public double ReadDouble()
		{
			long[] src = new long[]
			{
				this.ReadLong()
			};
			double[] array = new double[1];
			double[] array2 = array;
			Buffer.BlockCopy(src, 0, array2, 0, 8);
			return array2[0];
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00086BA0 File Offset: 0x00085BA0
		public double ReadDoubleLE()
		{
			long[] src = new long[]
			{
				this.ReadLongLE()
			};
			double[] array = new double[1];
			double[] array2 = array;
			Buffer.BlockCopy(src, 0, array2, 0, 8);
			return array2[0];
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00086BD8 File Offset: 0x00085BD8
		public string ReadLine()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = -1;
			bool flag = false;
			while (!flag)
			{
				int num2;
				num = (num2 = this.Read());
				if (num2 != -1 && num2 != 10)
				{
					if (num2 != 13)
					{
						stringBuilder.Append((char)num);
					}
					else
					{
						flag = true;
						int filePointer = this.FilePointer;
						if (this.Read() != 10)
						{
							this.Seek(filePointer);
						}
					}
				}
				else
				{
					flag = true;
				}
			}
			if (num == -1 && stringBuilder.Length == 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00086C4E File Offset: 0x00085C4E
		// (set) Token: 0x06001785 RID: 6021 RVA: 0x00086C56 File Offset: 0x00085C56
		public int StartOffset
		{
			get
			{
				return this.startOffset;
			}
			set
			{
				this.startOffset = value;
			}
		}

		// Token: 0x04001002 RID: 4098
		internal FileStream rf;

		// Token: 0x04001003 RID: 4099
		internal string filename;

		// Token: 0x04001004 RID: 4100
		internal byte[] arrayIn;

		// Token: 0x04001005 RID: 4101
		internal int arrayInPtr;

		// Token: 0x04001006 RID: 4102
		internal byte back;

		// Token: 0x04001007 RID: 4103
		internal bool isBack;

		// Token: 0x04001008 RID: 4104
		private int startOffset;
	}
}
