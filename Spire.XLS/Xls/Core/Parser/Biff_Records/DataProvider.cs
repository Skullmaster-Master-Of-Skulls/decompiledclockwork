using System;
using System.Drawing;
using System.IO;
using System.Text;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Parser.Biff_Records
{
	// Token: 0x020002C2 RID: 706
	public abstract class DataProvider : IDisposable
	{
		// Token: 0x06002ABE RID: 10942 RVA: 0x0017DE04 File Offset: 0x0017CE04
		public DataProvider()
		{
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x0017DE18 File Offset: 0x0017CE18
		~DataProvider()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.Dispose();
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x0017DE74 File Offset: 0x0017CE74
		public bool ReadBit(int iOffset, int iBit)
		{
			int a_ = 7;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 2;
					continue;
				case 2:
					if (iBit > 7)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					goto IL_A0;
				case 3:
					goto IL_9E;
				}
				if (iBit < 0)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9E;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
			}
			IL_53:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("吼紾⡀㝂", a_), RecordTableEnumerator.b("缼嘾㕀捂ᕄ⡆㩈≊㥌♎㹐㵒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨ݪ࡬ᱮɰ卲Ŵὶᡸᕺ嵼佾ꆀꞆ力랖ﲜ膠钢认", a_));
			IL_9E:
			goto IL_53;
			IL_A0:
			byte b = this.ReadByte(iOffset);
			return ((int)b & 1 << iBit) == 1 << iBit;
		}

		// Token: 0x06002AC1 RID: 10945
		public abstract byte ReadByte(int iOffset);

		// Token: 0x06002AC2 RID: 10946 RVA: 0x0017DF3C File Offset: 0x0017CF3C
		public bool ReadBoolean(int iOffset)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.ReadByte(iOffset) != 0;
		}

		// Token: 0x06002AC3 RID: 10947
		public abstract short ReadInt16(int iOffset);

		// Token: 0x06002AC4 RID: 10948 RVA: 0x0017DF84 File Offset: 0x0017CF84
		[CLSCompliant(false)]
		public ushort ReadUInt16(int iOffset)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return (ushort)this.ReadInt16(iOffset);
		}

		// Token: 0x06002AC5 RID: 10949
		public abstract int ReadInt32(int iOffset);

		// Token: 0x06002AC6 RID: 10950 RVA: 0x0017DFC8 File Offset: 0x0017CFC8
		[CLSCompliant(false)]
		public uint ReadUInt32(int iOffset)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return (uint)this.ReadInt32(iOffset);
		}

		// Token: 0x06002AC7 RID: 10951
		public abstract long ReadInt64(int iOffset);

		// Token: 0x06002AC8 RID: 10952 RVA: 0x0017E00C File Offset: 0x0017D00C
		public virtual double ReadDouble(int iOffset)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return spr\u2620.ᜀ(this.ReadInt64(iOffset));
		}

		// Token: 0x06002AC9 RID: 10953
		public abstract void CopyTo(int iSourceOffset, byte[] arrDestination, int iDestOffset, int iLength);

		// Token: 0x06002ACA RID: 10954 RVA: 0x0017E054 File Offset: 0x0017D054
		public virtual void CopyTo(int iSourceOffset, DataProvider destination, int iDestOffset, int iLength)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new NotImplementedException();
		}

		// Token: 0x06002ACB RID: 10955
		public abstract void Read(BinaryReader reader, int iOffset, int iLength, byte[] arrBuffer);

		// Token: 0x06002ACC RID: 10956 RVA: 0x0017E094 File Offset: 0x0017D094
		public void Read(BinaryReader reader, int iOffset, int iLength, byte[] arrBuffer, IDecryptor decryptor)
		{
			for (;;)
			{
				bool flag = decryptor != null;
				int num = 2;
				for (;;)
				{
					long num2;
					long streamPosition;
					switch (num)
					{
					case 0:
						if (flag)
						{
							num = 5;
							continue;
						}
						goto IL_B4;
					case 1:
						num2 = reader.BaseStream.Position;
						goto IL_75;
					case 2:
						if (!flag)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						num = 1;
						continue;
					case 3:
						num = 6;
						continue;
					case 4:
						goto IL_B4;
					case 5:
						decryptor.Decrypt(this, iOffset, iLength, streamPosition);
						goto IL_6B;
					case 6:
						num2 = 0L;
						goto IL_75;
					}
					break;
					IL_6B:
					num = 4;
					continue;
					IL_B4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6B;
					default:
						goto IL_CA;
					}
					IL_75:
					streamPosition = num2;
					this.Read(reader, iOffset, iLength, arrBuffer);
					num = 0;
				}
			}
			IL_CA:
			if (false)
			{
			}
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x0017E174 File Offset: 0x0017D174
		public virtual string ReadString16Bit(int iOffset, out int iFullLength)
		{
			switch (0)
			{
			default:
			{
				bool flag;
				int stringLength;
				for (;;)
				{
					ushort num = this.ReadUInt16(iOffset);
					iOffset += 2;
					flag = this.ReadBoolean(iOffset);
					iOffset++;
					int num2 = 5;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							num3 = (int)num;
							goto IL_DE;
						case 1:
							goto IL_87;
						case 2:
							goto IL_74;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_5E;
							default:
								if (false)
								{
								}
								num2 = 1;
								continue;
							}
							break;
						case 4:
							num3 = (int)(num * 2);
							goto IL_DE;
						case 5:
							goto IL_5E;
						case 6:
							num2 = 0;
							continue;
						case 7:
							if (true)
							{
							}
							if (!flag)
							{
								num2 = 3;
								continue;
							}
							num2 = 2;
							continue;
						case 8:
							if (!flag)
							{
								num2 = 6;
								continue;
							}
							num2 = 4;
							continue;
						}
						break;
						IL_5E:
						iFullLength = (int)(flag ? (3 + num * 2) : (3 + num));
						num2 = 8;
						continue;
						IL_DE:
						stringLength = num3;
						num2 = 7;
					}
				}
				IL_74:
				Encoding encoding = Encoding.Unicode;
				goto IL_126;
				IL_87:
				encoding = BiffRecordRaw.LatinEncoding;
				IL_126:
				Encoding encoding2 = encoding;
				return this.ReadString(iOffset, stringLength, encoding2, flag);
			}
			}
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x0017E2B4 File Offset: 0x0017D2B4
		public virtual string ReadString16BitUpdateOffset(ref int iOffset)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			int num;
			string result = this.ReadString16Bit(iOffset, out num);
			iOffset += num;
			return result;
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x0017E304 File Offset: 0x0017D304
		public virtual string ReadString8Bit(int iOffset, out int iFullLength)
		{
			switch (0)
			{
			default:
			{
				int num4;
				byte[] array;
				for (;;)
				{
					IL_37:
					ushort num = (ushort)this.ReadByte(iOffset);
					iOffset++;
					bool flag = this.ReadBoolean(iOffset);
					iOffset++;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_8C:
						num2 = 6;
						break;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num2 = 0;
						break;
					}
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							if (!flag)
							{
								num2 = 4;
								continue;
							}
							num2 = 5;
							continue;
						case 1:
							goto IL_10E;
						case 2:
							num3 = (int)num;
							goto IL_DC;
						case 3:
							goto IL_AB;
						case 4:
							num2 = 2;
							continue;
						case 5:
							num3 = (int)(num * 2);
							goto IL_DC;
						case 6:
							goto IL_98;
						case 7:
							if (!flag)
							{
								num2 = 1;
								continue;
							}
							num2 = 3;
							continue;
						}
						goto IL_37;
						IL_DC:
						num4 = num3;
						iFullLength = 2 + num4;
						array = new byte[num4];
						this.CopyTo(iOffset, array, 0, num4);
						num2 = 7;
					}
					IL_10E:
					goto IL_8C;
				}
				IL_98:
				Encoding encoding = BiffRecordRaw.LatinEncoding;
				goto IL_113;
				IL_AB:
				encoding = Encoding.Unicode;
				IL_113:
				Encoding encoding2 = encoding;
				return encoding2.GetString(array, 0, num4);
			}
			}
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x0017E430 File Offset: 0x0017D430
		public int ReadArray(int iOffset, byte[] arrDest)
		{
			int a_ = 7;
			if (arrDest != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					int num = arrDest.Length;
					this.CopyTo(iOffset, arrDest, 0, num);
					return iOffset + num;
				}
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("尼䴾㍀݂⁄㑆㵈", a_));
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x0017E4A0 File Offset: 0x0017D4A0
		public int ReadArray(int iOffset, byte[] arrDest, int size)
		{
			int a_ = 11;
			if (arrDest != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.CopyTo(iOffset, arrDest, 0, size);
					return iOffset + size;
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⁀ㅂ㝄͆ⱈ㡊㥌", a_));
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x0017E50C File Offset: 0x0017D50C
		public string ReadString(int offset, int iStrLen, out int iBytesInString, bool isByteCounted)
		{
			switch (0)
			{
			default:
			{
				byte[] array;
				for (;;)
				{
					byte b = this.ReadByte(offset);
					int num = 8;
					for (;;)
					{
						int num2;
						bool flag;
						switch (num)
						{
						case 0:
							num = 2;
							continue;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_7A;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								num2 = iStrLen;
								goto IL_C5;
							}
							break;
						case 2:
							goto IL_131;
						case 3:
							goto IL_144;
						case 4:
							if (isByteCounted)
							{
								num = 10;
								continue;
							}
							num = 6;
							continue;
						case 5:
							num = 4;
							continue;
						case 6:
							num2 = 2 * iStrLen;
							goto IL_C5;
						case 7:
							iBytesInString = ((flag && !isByteCounted) ? (iStrLen * 2) : iStrLen);
							array = new byte[iBytesInString];
							this.ReadArray(offset + 1, array);
							num = 9;
							continue;
						case 8:
							if (b != 0)
							{
								num = 5;
								continue;
							}
							goto IL_7A;
						case 9:
							if (!flag)
							{
								num = 0;
								continue;
							}
							num = 3;
							continue;
						case 10:
							goto IL_7A;
						}
						break;
						IL_7A:
						num = 1;
						continue;
						IL_C5:
						int num3 = num2;
						num3 += offset + 1;
						flag = (b != 0);
						num = 7;
					}
				}
				IL_131:
				Encoding encoding = BiffRecordRaw.LatinEncoding;
				goto IL_14B;
				IL_144:
				encoding = Encoding.Unicode;
				IL_14B:
				Encoding encoding2 = encoding;
				return encoding2.GetString(array, 0, array.Length);
			}
			}
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x0017E674 File Offset: 0x0017D674
		public string ReadStringUpdateOffset(ref int offset, int iStrLen)
		{
			if (true)
			{
			}
			if (iStrLen <= 0)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					return string.Empty;
				}
			}
			int num;
			string result = this.ReadString(offset, iStrLen, out num, false);
			offset += num + 1;
			return result;
		}

		// Token: 0x06002AD4 RID: 10964
		public abstract string ReadString(int offset, int stringLength, Encoding encoding, bool isUnicode);

		// Token: 0x06002AD5 RID: 10965 RVA: 0x0017E6D4 File Offset: 0x0017D6D4
		[CLSCompliant(false)]
		internal TAddr ᜆ(int A_0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return new TAddr
			{
				FirstRow = (int)this.ReadUInt16(A_0),
				LastRow = (int)this.ReadUInt16(A_0 + 2),
				FirstCol = (int)this.ReadUInt16(A_0 + 4),
				LastCol = (int)this.ReadUInt16(A_0 + 6)
			};
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x0017E758 File Offset: 0x0017D758
		public Rectangle ReadAddrAsRectangle(int offset)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			int top = (int)this.ReadUInt16(offset);
			int bottom = (int)this.ReadUInt16(offset + 2);
			int left = (int)this.ReadUInt16(offset + 4);
			int right = (int)this.ReadUInt16(offset + 6);
			return Rectangle.FromLTRB(left, top, right, bottom);
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x0017E7C4 File Offset: 0x0017D7C4
		public virtual void WriteInto(BinaryWriter writer, int iOffset, int iSize, byte[] arrBuffer)
		{
			int a_ = 19;
			int num = 0;
			for (;;)
			{
				int num2;
				int val;
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6E;
					default:
						if (false)
						{
						}
						goto IL_AF;
					}
					break;
				case 2:
				{
					if (num2 <= 0)
					{
						num = 5;
						continue;
					}
					int num3 = Math.Min(val, num2);
					this.CopyTo(iOffset, arrBuffer, 0, num3);
					writer.Write(arrBuffer, 0, num3);
					iOffset += num3;
					num2 -= num3;
					num = 4;
					continue;
				}
				case 3:
					goto IL_3C;
				case 4:
					goto IL_AF;
				case 5:
					goto IL_C9;
				}
				if (writer == null)
				{
					num = 3;
					continue;
				}
				IL_6E:
				num2 = iSize;
				val = arrBuffer.Length;
				num = 1;
				continue;
				IL_AF:
				num = 2;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
			IL_C9:
			if (true)
			{
			}
		}

		// Token: 0x06002AD8 RID: 10968
		public abstract void WriteByte(int iOffset, byte value);

		// Token: 0x06002AD9 RID: 10969
		public abstract void WriteInt16(int iOffset, short value);

		// Token: 0x06002ADA RID: 10970 RVA: 0x0017E8B0 File Offset: 0x0017D8B0
		[CLSCompliant(false)]
		public virtual void WriteUInt16(int iOffset, ushort value)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new NotImplementedException();
		}

		// Token: 0x06002ADB RID: 10971
		public abstract void WriteInt32(int iOffset, int value);

		// Token: 0x06002ADC RID: 10972
		public abstract void WriteInt64(int iOffset, long value);

		// Token: 0x06002ADD RID: 10973 RVA: 0x0017E8F0 File Offset: 0x0017D8F0
		[CLSCompliant(false)]
		public void WriteUInt32(int iOffset, uint value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.WriteInt32(iOffset, (int)value);
		}

		// Token: 0x06002ADE RID: 10974
		public abstract void WriteBit(int offset, bool value, int bitPos);

		// Token: 0x06002ADF RID: 10975
		public abstract void WriteDouble(int iOffset, double value);

		// Token: 0x06002AE0 RID: 10976 RVA: 0x0017E934 File Offset: 0x0017D934
		public void WriteString8BitUpdateOffset(ref int offset, string value)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.WriteByte(offset, (byte)value.Length);
			offset++;
			this.WriteStringNoLenUpdateOffset(ref offset, value);
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x0017E98C File Offset: 0x0017D98C
		public void WriteString16BitUpdateOffset(ref int offset, string value)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.WriteString16BitUpdateOffset(ref offset, value, true);
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x0017E9D0 File Offset: 0x0017D9D0
		public void WriteString16BitUpdateOffset(ref int offset, string value, bool isUnicode)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_73;
				case 1:
					goto IL_51;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				if (value == null)
				{
					num = 3;
				}
				else
				{
					num = 1;
				}
			}
			IL_51:
			if (true)
			{
			}
			int num2 = value.Length;
			goto IL_76;
			IL_73:
			num2 = 0;
			IL_76:
			int num3 = num2;
			this.WriteUInt16(offset, (ushort)num3);
			offset += 2;
			this.WriteStringNoLenUpdateOffset(ref offset, value, isUnicode);
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x0017EA70 File Offset: 0x0017DA70
		public int WriteString16Bit(int offset, string value)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.WriteString16Bit(offset, value, true);
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x0017EAB4 File Offset: 0x0017DAB4
		public int WriteString16Bit(int offset, string value, bool isUnicode)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			int num = offset;
			this.WriteString16BitUpdateOffset(ref offset, value, isUnicode);
			return offset - num;
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x0017EB00 File Offset: 0x0017DB00
		public virtual void WriteStringNoLenUpdateOffset(ref int offset, string value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.WriteStringNoLenUpdateOffset(ref offset, value, true);
		}

		// Token: 0x06002AE6 RID: 10982
		public abstract void WriteStringNoLenUpdateOffset(ref int offset, string value, bool bUnicode);

		// Token: 0x06002AE7 RID: 10983 RVA: 0x0017EB44 File Offset: 0x0017DB44
		public void WriteBytes(int offset, byte[] data)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.WriteBytes(offset, data, 0, data.Length);
		}

		// Token: 0x06002AE8 RID: 10984
		public abstract void WriteBytes(int offset, byte[] value, int pos, int length);

		// Token: 0x06002AE9 RID: 10985 RVA: 0x0017EB8C File Offset: 0x0017DB8C
		[CLSCompliant(false)]
		protected internal void WriteAddr(int offset, TAddr addr)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.WriteUInt16(offset, (ushort)addr.FirstRow);
			this.WriteUInt16(offset + 2, (ushort)addr.LastRow);
			this.WriteUInt16(offset + 4, (ushort)addr.FirstCol);
			this.WriteUInt16(offset + 6, (ushort)addr.LastCol);
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x0017EC0C File Offset: 0x0017DC0C
		protected internal void WriteAddr(int offset, Rectangle addr)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.WriteUInt16(offset, (ushort)addr.Top);
			this.WriteUInt16(offset + 2, (ushort)addr.Bottom);
			this.WriteUInt16(offset + 4, (ushort)addr.Left);
			this.WriteUInt16(offset + 6, (ushort)addr.Right);
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06002AEB RID: 10987
		public abstract int Capacity { get; }

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06002AEC RID: 10988
		public abstract bool IsCleared { get; }

		// Token: 0x06002AED RID: 10989
		public abstract void MoveMemory(int iDestOffset, int iSourceOffset, int iMemorySize);

		// Token: 0x06002AEE RID: 10990
		public abstract void CopyMemory(int iDestOffset, int iSourceOffset, int iMemorySize);

		// Token: 0x06002AEF RID: 10991
		public abstract void EnsureCapacity(int size);

		// Token: 0x06002AF0 RID: 10992
		public abstract void EnsureCapacity(int size, int forceAdd);

		// Token: 0x06002AF1 RID: 10993
		public abstract void ZeroMemory();

		// Token: 0x06002AF2 RID: 10994
		public abstract void Clear();

		// Token: 0x06002AF3 RID: 10995
		public abstract DataProvider CreateProvider();

		// Token: 0x06002AF4 RID: 10996 RVA: 0x0017EC8C File Offset: 0x0017DC8C
		public void Dispose()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.OnDispose();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x0017ECD4 File Offset: 0x0017DCD4
		protected virtual void OnDispose()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
		}
	}
}
