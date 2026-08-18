using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.codec
{
	// Token: 0x020000F0 RID: 240
	public class TIFFDirectory
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x0003000B File Offset: 0x0002F00B
		private TIFFDirectory()
		{
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00030026 File Offset: 0x0002F026
		private static bool IsValidEndianTag(int endian)
		{
			return endian == 18761 || endian == 19789;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0003003C File Offset: 0x0002F03C
		public TIFFDirectory(RandomAccessFileOrArray stream, int directory)
		{
			long pos = (long)stream.FilePointer;
			stream.Seek(0L);
			int num = stream.ReadUnsignedShort();
			if (!TIFFDirectory.IsValidEndianTag(num))
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("bad.endianness.tag.not.0x4949.or.0x4d4d"));
			}
			this.isBigEndian = (num == 19789);
			int num2 = this.ReadUnsignedShort(stream);
			if (num2 != 42)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("bad.magic.number.should.be.42"));
			}
			long num3 = this.ReadUnsignedInt(stream);
			for (int i = 0; i < directory; i++)
			{
				if (num3 == 0L)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("directory.number.too.large"));
				}
				stream.Seek(num3);
				int num4 = this.ReadUnsignedShort(stream);
				stream.Skip((long)(12 * num4));
				num3 = this.ReadUnsignedInt(stream);
			}
			stream.Seek(num3);
			this.Initialize(stream);
			stream.Seek(pos);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00030124 File Offset: 0x0002F124
		public TIFFDirectory(RandomAccessFileOrArray stream, long ifd_offset, int directory)
		{
			long pos = (long)stream.FilePointer;
			stream.Seek(0L);
			int num = stream.ReadUnsignedShort();
			if (!TIFFDirectory.IsValidEndianTag(num))
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("bad.endianness.tag.not.0x4949.or.0x4d4d"));
			}
			this.isBigEndian = (num == 19789);
			stream.Seek(ifd_offset);
			for (int i = 0; i < directory; i++)
			{
				int num2 = this.ReadUnsignedShort(stream);
				stream.Seek(ifd_offset + (long)(12 * num2));
				ifd_offset = this.ReadUnsignedInt(stream);
				stream.Seek(ifd_offset);
			}
			this.Initialize(stream);
			stream.Seek(pos);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x000301D0 File Offset: 0x0002F1D0
		private void Initialize(RandomAccessFileOrArray stream)
		{
			long num = 0L;
			long num2 = (long)stream.Length;
			this.IFDOffset = (long)stream.FilePointer;
			this.numEntries = this.ReadUnsignedShort(stream);
			this.fields = new TIFFField[this.numEntries];
			for (int i = 0; i < this.numEntries; i++)
			{
				if (num >= num2)
				{
					break;
				}
				int num3 = this.ReadUnsignedShort(stream);
				int num4 = this.ReadUnsignedShort(stream);
				int num5 = (int)this.ReadUnsignedInt(stream);
				bool flag = true;
				num = (long)(stream.FilePointer + 4);
				try
				{
					if (num5 * TIFFDirectory.sizeOfType[num4] > 4)
					{
						long num6 = this.ReadUnsignedInt(stream);
						if (num6 < num2)
						{
							stream.Seek(num6);
						}
						else
						{
							flag = false;
						}
					}
				}
				catch (ArgumentOutOfRangeException)
				{
					flag = false;
				}
				if (flag)
				{
					this.fieldIndex[num3] = i;
					object data = null;
					switch (num4)
					{
					case 1:
					case 2:
					case 6:
					case 7:
					{
						byte[] array = new byte[num5];
						stream.ReadFully(array, 0, num5);
						if (num4 == 2)
						{
							int j = 0;
							int num7 = 0;
							List<string> list = new List<string>();
							while (j < num5)
							{
								while (j < num5 && array[j++] != 0)
								{
								}
								char[] array2 = new char[j - num7];
								Array.Copy(array, num7, array2, 0, j - num7);
								list.Add(new string(array2));
								num7 = j;
							}
							num5 = list.Count;
							string[] array3 = new string[num5];
							for (int k = 0; k < num5; k++)
							{
								array3[k] = list[k];
							}
							data = array3;
						}
						else
						{
							data = array;
						}
						break;
					}
					case 3:
					{
						char[] array4 = new char[num5];
						for (int l = 0; l < num5; l++)
						{
							array4[l] = (char)this.ReadUnsignedShort(stream);
						}
						data = array4;
						break;
					}
					case 4:
					{
						long[] array5 = new long[num5];
						for (int l = 0; l < num5; l++)
						{
							array5[l] = this.ReadUnsignedInt(stream);
						}
						data = array5;
						break;
					}
					case 5:
					{
						long[][] array6 = new long[num5][];
						for (int l = 0; l < num5; l++)
						{
							long num8 = this.ReadUnsignedInt(stream);
							long num9 = this.ReadUnsignedInt(stream);
							array6[l] = new long[]
							{
								num8,
								num9
							};
						}
						data = array6;
						break;
					}
					case 8:
					{
						short[] array7 = new short[num5];
						for (int l = 0; l < num5; l++)
						{
							array7[l] = this.ReadShort(stream);
						}
						data = array7;
						break;
					}
					case 9:
					{
						int[] array8 = new int[num5];
						for (int l = 0; l < num5; l++)
						{
							array8[l] = this.ReadInt(stream);
						}
						data = array8;
						break;
					}
					case 10:
					{
						int[,] array9 = new int[num5, 2];
						for (int l = 0; l < num5; l++)
						{
							array9[l, 0] = this.ReadInt(stream);
							array9[l, 1] = this.ReadInt(stream);
						}
						data = array9;
						break;
					}
					case 11:
					{
						float[] array10 = new float[num5];
						for (int l = 0; l < num5; l++)
						{
							array10[l] = this.ReadFloat(stream);
						}
						data = array10;
						break;
					}
					case 12:
					{
						double[] array11 = new double[num5];
						for (int l = 0; l < num5; l++)
						{
							array11[l] = this.ReadDouble(stream);
						}
						data = array11;
						break;
					}
					}
					this.fields[i] = new TIFFField(num3, num4, num5, data);
				}
				stream.Seek(num);
			}
			try
			{
				this.nextIFDOffset = this.ReadUnsignedInt(stream);
			}
			catch
			{
				this.nextIFDOffset = 0L;
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00030568 File Offset: 0x0002F568
		public int GetNumEntries()
		{
			return this.numEntries;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00030570 File Offset: 0x0002F570
		public TIFFField GetField(int tag)
		{
			int num;
			if (this.fieldIndex.TryGetValue(tag, out num))
			{
				return this.fields[num];
			}
			return null;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00030597 File Offset: 0x0002F597
		public bool IsTagPresent(int tag)
		{
			return this.fieldIndex.ContainsKey(tag);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x000305A8 File Offset: 0x0002F5A8
		public int[] GetTags()
		{
			int[] array = new int[this.fieldIndex.Count];
			this.fieldIndex.Keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x000305D9 File Offset: 0x0002F5D9
		public TIFFField[] GetFields()
		{
			return this.fields;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x000305E4 File Offset: 0x0002F5E4
		public byte GetFieldAsByte(int tag, int index)
		{
			int num = this.fieldIndex[tag];
			byte[] asBytes = this.fields[num].GetAsBytes();
			return asBytes[index];
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0003060F File Offset: 0x0002F60F
		public byte GetFieldAsByte(int tag)
		{
			return this.GetFieldAsByte(tag, 0);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0003061C File Offset: 0x0002F61C
		public long GetFieldAsLong(int tag, int index)
		{
			int num = this.fieldIndex[tag];
			return this.fields[num].GetAsLong(index);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00030644 File Offset: 0x0002F644
		public long GetFieldAsLong(int tag)
		{
			return this.GetFieldAsLong(tag, 0);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00030650 File Offset: 0x0002F650
		public float GetFieldAsFloat(int tag, int index)
		{
			int num = this.fieldIndex[tag];
			return this.fields[num].GetAsFloat(index);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00030678 File Offset: 0x0002F678
		public float GetFieldAsFloat(int tag)
		{
			return this.GetFieldAsFloat(tag, 0);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00030684 File Offset: 0x0002F684
		public double GetFieldAsDouble(int tag, int index)
		{
			int num = this.fieldIndex[tag];
			return this.fields[num].GetAsDouble(index);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000306AC File Offset: 0x0002F6AC
		public double GetFieldAsDouble(int tag)
		{
			return this.GetFieldAsDouble(tag, 0);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x000306B6 File Offset: 0x0002F6B6
		private short ReadShort(RandomAccessFileOrArray stream)
		{
			if (this.isBigEndian)
			{
				return stream.ReadShort();
			}
			return stream.ReadShortLE();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000306CD File Offset: 0x0002F6CD
		private int ReadUnsignedShort(RandomAccessFileOrArray stream)
		{
			if (this.isBigEndian)
			{
				return stream.ReadUnsignedShort();
			}
			return stream.ReadUnsignedShortLE();
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x000306E4 File Offset: 0x0002F6E4
		private int ReadInt(RandomAccessFileOrArray stream)
		{
			if (this.isBigEndian)
			{
				return stream.ReadInt();
			}
			return stream.ReadIntLE();
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x000306FB File Offset: 0x0002F6FB
		private long ReadUnsignedInt(RandomAccessFileOrArray stream)
		{
			if (this.isBigEndian)
			{
				return stream.ReadUnsignedInt();
			}
			return stream.ReadUnsignedIntLE();
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00030712 File Offset: 0x0002F712
		private long ReadLong(RandomAccessFileOrArray stream)
		{
			if (this.isBigEndian)
			{
				return stream.ReadLong();
			}
			return stream.ReadLongLE();
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00030729 File Offset: 0x0002F729
		private float ReadFloat(RandomAccessFileOrArray stream)
		{
			if (this.isBigEndian)
			{
				return stream.ReadFloat();
			}
			return stream.ReadFloatLE();
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00030740 File Offset: 0x0002F740
		private double ReadDouble(RandomAccessFileOrArray stream)
		{
			if (this.isBigEndian)
			{
				return stream.ReadDouble();
			}
			return stream.ReadDoubleLE();
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00030757 File Offset: 0x0002F757
		private static int ReadUnsignedShort(RandomAccessFileOrArray stream, bool isBigEndian)
		{
			if (isBigEndian)
			{
				return stream.ReadUnsignedShort();
			}
			return stream.ReadUnsignedShortLE();
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00030769 File Offset: 0x0002F769
		private static long ReadUnsignedInt(RandomAccessFileOrArray stream, bool isBigEndian)
		{
			if (isBigEndian)
			{
				return stream.ReadUnsignedInt();
			}
			return stream.ReadUnsignedIntLE();
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0003077C File Offset: 0x0002F77C
		public static int GetNumDirectories(RandomAccessFileOrArray stream)
		{
			long pos = (long)stream.FilePointer;
			stream.Seek(0L);
			int num = stream.ReadUnsignedShort();
			if (!TIFFDirectory.IsValidEndianTag(num))
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("bad.endianness.tag.not.0x4949.or.0x4d4d"));
			}
			bool flag = num == 19789;
			int num2 = TIFFDirectory.ReadUnsignedShort(stream, flag);
			if (num2 != 42)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("bad.magic.number.should.be.42"));
			}
			stream.Seek(4L);
			long num3 = TIFFDirectory.ReadUnsignedInt(stream, flag);
			int num4 = 0;
			while (num3 != 0L)
			{
				num4++;
				try
				{
					stream.Seek(num3);
					int num5 = TIFFDirectory.ReadUnsignedShort(stream, flag);
					stream.Skip((long)(12 * num5));
					num3 = TIFFDirectory.ReadUnsignedInt(stream, flag);
				}
				catch (EndOfStreamException)
				{
					break;
				}
			}
			stream.Seek(pos);
			return num4;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00030848 File Offset: 0x0002F848
		public bool IsBigEndian()
		{
			return this.isBigEndian;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00030850 File Offset: 0x0002F850
		public long GetIFDOffset()
		{
			return this.IFDOffset;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00030858 File Offset: 0x0002F858
		public long GetNextIFDOffset()
		{
			return this.nextIFDOffset;
		}

		// Token: 0x0400079C RID: 1948
		private bool isBigEndian;

		// Token: 0x0400079D RID: 1949
		private int numEntries;

		// Token: 0x0400079E RID: 1950
		private TIFFField[] fields;

		// Token: 0x0400079F RID: 1951
		private Dictionary<int, int> fieldIndex = new Dictionary<int, int>();

		// Token: 0x040007A0 RID: 1952
		private long IFDOffset = 8L;

		// Token: 0x040007A1 RID: 1953
		private long nextIFDOffset;

		// Token: 0x040007A2 RID: 1954
		private static int[] sizeOfType = new int[]
		{
			0,
			1,
			1,
			2,
			4,
			8,
			1,
			1,
			2,
			4,
			8,
			4,
			8
		};
	}
}
