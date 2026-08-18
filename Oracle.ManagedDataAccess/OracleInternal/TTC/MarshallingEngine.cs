using System;
using System.Collections;
using System.Collections.Generic;
using OracleInternal.Common;
using OracleInternal.I18N;
using OracleInternal.Network;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC
{
	// Token: 0x02000214 RID: 532
	internal class MarshallingEngine
	{
		// Token: 0x0600139F RID: 5023 RVA: 0x000CFCC8 File Offset: 0x000CDEC8
		internal MarshallingEngine(OracleCommunication communication, OracleConnectionImpl connImplReference)
		{
			this.m_connImplReference = connImplReference;
			this.m_oracleCommunication = communication;
			this.m_oraBufWriter = new OraBufWriter(this, communication.m_sessionCtx.m_writerStream, communication);
			this.m_oraBufRdr = new OraBufReader(communication.m_sessionCtx.m_readerStream, this.m_oraBufWriter);
			this.m_typeRepresentation = new TTCTypeRepresentation();
			this.m_typeRepresentation.m_representationArray[1] = 2;
			this.m_charArrayPooler = new CharArrayPooler(2, 32768);
			if (65536 > communication.SDU)
			{
				this.m_numOBThresholdForSends = 65536 / communication.SDU;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x000CFDC4 File Offset: 0x000CDFC4
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x000CFDCC File Offset: 0x000CDFCC
		internal byte NegotiatedTTCVersion
		{
			get
			{
				return this.m_negotiatedTTCVersion;
			}
			set
			{
				this.m_negotiatedTTCVersion = value;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x000CFDD8 File Offset: 0x000CDFD8
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x000CFDF4 File Offset: 0x000CDFF4
		internal TTCError TTCErrorObject
		{
			get
			{
				if (this.m_ttcError == null)
				{
					this.m_ttcError = new TTCError(this);
				}
				return this.m_ttcError;
			}
			set
			{
				this.m_ttcError = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x000CFE0C File Offset: 0x000CE00C
		// (set) Token: 0x060013A4 RID: 5028 RVA: 0x000CFE00 File Offset: 0x000CE000
		internal bool HasFSAPCapability
		{
			get
			{
				return this.m_bHasFSAPCapability;
			}
			set
			{
				this.m_bHasFSAPCapability = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x000CFE20 File Offset: 0x000CE020
		// (set) Token: 0x060013A6 RID: 5030 RVA: 0x000CFE14 File Offset: 0x000CE014
		internal bool HasEOCSCapability
		{
			get
			{
				return this.m_hasEOCSCapability;
			}
			set
			{
				this.m_hasEOCSCapability = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x000CFE34 File Offset: 0x000CE034
		// (set) Token: 0x060013A8 RID: 5032 RVA: 0x000CFE28 File Offset: 0x000CE028
		internal short DBVersion
		{
			get
			{
				return this.m_DBVersion;
			}
			set
			{
				this.m_DBVersion = value;
			}
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x000CFE3C File Offset: 0x000CE03C
		internal void MarshalUB1(short val)
		{
			if (this.m_oraBufWriter.m_currentOB.Space - this.m_oraBufWriter.m_positionInCurrentOB >= 1)
			{
				this.m_oraBufWriter.m_currentObBuffer[this.m_oraBufWriter.m_positionInCurrentOB] = (byte)val;
				this.m_oraBufWriter.m_positionInCurrentOB++;
				this.m_oraBufWriter.m_lengthForDataSegment++;
				return;
			}
			this.m_oraBufWriter.Write((byte)(val & 255));
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x000CFEBC File Offset: 0x000CE0BC
		internal void MarshalUB2(int val)
		{
			this.MarshalSB2((short)(val & 65535));
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x000CFECC File Offset: 0x000CE0CC
		internal void MarshalUB4(long val)
		{
			this.MarshalSB4((int)(val & (long)((ulong)-1)));
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x000CFEDC File Offset: 0x000CE0DC
		internal void MarshalO2U(bool notnull)
		{
			if (notnull)
			{
				this.AddPointer(1);
				return;
			}
			this.AddPointer(0);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x000CFEF0 File Offset: 0x000CE0F0
		internal void MarshalPointer()
		{
			this.AddPointer(1);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x000CFEFC File Offset: 0x000CE0FC
		internal void MarshalNullPointer()
		{
			this.AddPointer(0);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x000CFF08 File Offset: 0x000CE108
		internal short UnmarshalUB1(bool bIgnoreData = false)
		{
			return (short)this.m_oraBufRdr.Read(bIgnoreData);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x000CFF18 File Offset: 0x000CE118
		internal void MarshalSB2(short val)
		{
			byte b = this.ValueToBuffer((long)val, this.tmpBuffer2, 1);
			if (b != 0)
			{
				this.m_oraBufWriter.Write(this.tmpBuffer2, 0, (int)b);
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x000CFF4C File Offset: 0x000CE14C
		internal void MarshalSB4(int val)
		{
			byte b = this.ValueToBuffer((long)val, this.tmpBuffer4, 2);
			if (b != 0)
			{
				this.m_oraBufWriter.Write(this.tmpBuffer4, 0, (int)b);
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000CFF80 File Offset: 0x000CE180
		internal void MarshalSB8(long val)
		{
			byte b = this.ValueToBuffer(val, this.tmpBuffer8, 3);
			if (b != 0)
			{
				this.m_oraBufWriter.Write(this.tmpBuffer8, 0, (int)b);
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x000CFFB4 File Offset: 0x000CE1B4
		internal void MarshalSWORD(int value)
		{
			this.MarshalSB4(value);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x000CFFC0 File Offset: 0x000CE1C0
		internal void MarshalDALC(byte[] buffer)
		{
			if (buffer == null || buffer.Length < 1)
			{
				this.m_oraBufWriter.Write(0);
				return;
			}
			this.MarshalSB4((int)((ulong)-1 & (ulong)((long)buffer.Length)));
			this.MarshalCLR(buffer, buffer.Length);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x000CFFF0 File Offset: 0x000CE1F0
		internal void MarshalCHR(byte[] value)
		{
			this.MarshalCHR(value, 0, value.Length);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x000D0000 File Offset: 0x000CE200
		internal void MarshalCHR(byte[] value, int offset, int length)
		{
			if (length > 0)
			{
				if (this.m_typeRepresentation.ConversionRequired)
				{
					this.MarshalCLR(value, offset, length);
					return;
				}
				this.m_oraBufWriter.Write(value, offset, length);
			}
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x000D002C File Offset: 0x000CE22C
		internal void MarshalUB4Array(long[] value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				this.MarshalSB4((int)(value[i] & (long)((ulong)-1)));
			}
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x000D0054 File Offset: 0x000CE254
		internal void MarshalKEYVAL(byte[][] keys, byte[][] values, byte[] kvalflg, int nb)
		{
			for (int i = 0; i < nb; i++)
			{
				if (keys[i] != null && keys[i].Length > 0)
				{
					this.MarshalUB4((long)keys[i].Length);
					this.MarshalCLR(keys[i], 0, keys[i].Length);
				}
				else
				{
					this.MarshalUB4(0L);
				}
				if (values[i] != null && values[i].Length > 0)
				{
					this.MarshalUB4((long)values[i].Length);
					this.MarshalCLR(values[i], 0, values[i].Length);
				}
				else
				{
					this.MarshalUB4(0L);
				}
				if (kvalflg[i] != 0)
				{
					this.MarshalUB4(1L);
				}
				else
				{
					this.MarshalUB4(0L);
				}
			}
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x000D00EC File Offset: 0x000CE2EC
		internal void MarshalCLR(byte[] value, int valueLen)
		{
			this.MarshalCLR(value, 0, valueLen);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x000D00F8 File Offset: 0x000CE2F8
		internal byte[] UnmarshalCLR(int buflen, int[] intArray)
		{
			byte[] array = new byte[buflen];
			this.UnmarshalCLR(array, 0, intArray, buflen);
			return array;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x000D0118 File Offset: 0x000CE318
		internal void MarshalCLR(byte[] value, int offset, int valueLen)
		{
			if (valueLen > 252)
			{
				int num = 0;
				this.m_oraBufWriter.Write(254);
				do
				{
					int num2 = valueLen - num;
					int num3 = (num2 > this.m_effectiveTTCC_MXIN) ? this.m_effectiveTTCC_MXIN : num2;
					if (this.m_bUseBigCLRChunks)
					{
						this.MarshalSB4(num3);
					}
					this.m_oraBufWriter.WriteLengthAndDataToOraBuf(!this.m_bUseBigCLRChunks, num3, value, offset + num, num3);
					num += num3;
				}
				while (num < valueLen);
				this.m_oraBufWriter.Write(0);
				return;
			}
			if (value.Length != 0)
			{
				this.m_oraBufWriter.WriteLengthAndDataToOraBuf(true, valueLen, value, offset, valueLen);
				return;
			}
			this.m_oraBufWriter.Write((byte)(valueLen & 255));
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000D01C0 File Offset: 0x000CE3C0
		internal void UnmarshalCLR(byte[] bytes, int offsetRow, int[] intArray)
		{
			this.UnmarshalCLR(bytes, offsetRow, intArray, int.MaxValue);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x000D01D0 File Offset: 0x000CE3D0
		internal void UnmarshalCLR(byte[] bytes, int offsetRow, int[] intArray, int maxSize)
		{
			int num = offsetRow;
			int num2 = 0;
			bool flag = false;
			int num3 = (int)this.UnmarshalUB1(false);
			if (num3 < 0)
			{
				throw new Exception("TTC Error");
			}
			if (num3 == 0)
			{
				intArray[0] = 0;
				return;
			}
			if (this.EscapeSequenceNull(num3))
			{
				intArray[0] = 0;
				return;
			}
			if (num3 != 254)
			{
				if (num3 > 0)
				{
					int num4 = (maxSize - num2 < num3) ? (maxSize - num2) : num3;
					num = this.UnmarshalBuffer(bytes, num, num4);
					num2 += num4;
					int num5 = num3 - num4;
					if (num5 > 0)
					{
						this.UnmarshalBuffer(this.ignored, 0, num5);
					}
				}
			}
			else
			{
				int num6 = -1;
				bool flag2 = false;
				while (!flag2)
				{
					if (num6 != -1)
					{
						if (this.m_bUseBigCLRChunks)
						{
							num3 = this.UnmarshalSB4();
						}
						else
						{
							num3 = (int)this.UnmarshalUB1(false);
						}
						if (num3 <= 0)
						{
							flag2 = true;
							continue;
						}
					}
					if (num3 == 254)
					{
						switch (num6)
						{
						case -1:
							num6 = 1;
							continue;
						case 0:
							if (!flag)
							{
								num6 = 0;
								continue;
							}
							break;
						}
					}
					if (num == -1)
					{
						this.UnmarshalBuffer(this.ignored, 0, num3);
					}
					else
					{
						int num7 = num3;
						if (num7 > 0)
						{
							int num4 = (maxSize - num2 < num7) ? (maxSize - num2) : num7;
							num = this.UnmarshalBuffer(bytes, num, num4);
							num2 += num4;
							int num8 = num7 - num4;
							if (num8 > 0)
							{
								this.UnmarshalBuffer(this.ignored, 0, num8);
							}
						}
					}
					num6 = 0;
					if (num3 > 252)
					{
						flag = true;
					}
				}
			}
			if (intArray != null)
			{
				if (num != -1)
				{
					intArray[0] = num2;
					return;
				}
				intArray[0] = bytes.Length - offsetRow;
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x000D035C File Offset: 0x000CE55C
		internal int UnmarshalBuffer(byte[] _byteValue, int offset, int len)
		{
			if (len <= 0)
			{
				return offset;
			}
			if (_byteValue.Length < offset + len)
			{
				this.UnmarshalNBytes(_byteValue, offset, _byteValue.Length - offset);
				this.UnmarshalNBytes(this.ignored, 0, offset + len - _byteValue.Length);
				offset = -1;
			}
			else
			{
				this.UnmarshalNBytes(_byteValue, offset, len);
				offset += len;
			}
			return offset;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000D03B0 File Offset: 0x000CE5B0
		internal int UnmarshalSB4()
		{
			return (int)this.UnmarshalUB4(false);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x000D03BC File Offset: 0x000CE5BC
		internal long UnmarshalSB8()
		{
			return this.BufferToValue(3, false);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000D03C8 File Offset: 0x000CE5C8
		internal int[] UnmarshalKEYVAL(byte[][] keys, byte[][] values, int nb)
		{
			byte[] array = new byte[1000];
			int[] array2 = new int[1];
			int[] array3 = new int[nb];
			for (int i = 0; i < nb; i++)
			{
				int num = this.UnmarshalSB4();
				if (num > 0)
				{
					this.UnmarshalCLR(array, 0, array2);
					keys[i] = new byte[array2[0]];
					Buffer.BlockCopy(array, 0, keys[i], 0, array2[0]);
				}
				num = this.UnmarshalSB4();
				if (num > 0)
				{
					this.UnmarshalCLR(array, 0, array2);
					values[i] = new byte[array2[0]];
					Buffer.BlockCopy(array, 0, values[i], 0, array2[0]);
				}
				array3[i] = this.UnmarshalSB4();
			}
			return array3;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000D0468 File Offset: 0x000CE668
		internal byte UnmarshalSB1()
		{
			return (byte)this.UnmarshalUB1(false);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x000D0474 File Offset: 0x000CE674
		internal void MarshalB1Array(byte[] inputBuffer)
		{
			if (inputBuffer.Length > 0)
			{
				this.m_oraBufWriter.Write(inputBuffer);
			}
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x000D0488 File Offset: 0x000CE688
		internal void MarshalB1Array(byte[] value, int off, int len)
		{
			if (value.Length > 0)
			{
				this.m_oraBufWriter.Write(value, off, len);
			}
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x000D04A0 File Offset: 0x000CE6A0
		internal short UnmarshalSB2()
		{
			return (short)this.UnmarshalUB2(false);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x000D04AC File Offset: 0x000CE6AC
		internal int UnmarshalUB2(bool bIgnoreData = false)
		{
			return (int)(this.BufferToValue(1, bIgnoreData) & 65535L);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x000D04C0 File Offset: 0x000CE6C0
		internal long UnmarshalUB4(bool bIgnoreData = false)
		{
			return this.BufferToValue(2, bIgnoreData);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x000D04CC File Offset: 0x000CE6CC
		internal byte[] UnmarshalTEXT(int length)
		{
			int i = 0;
			byte[] array = new byte[length];
			while (i < length)
			{
				if (this.m_oraBufRdr.Read(array, i, 1) < 0)
				{
					throw new Exception("TTC Error");
				}
				if (array[i++] == 0)
				{
					break;
				}
			}
			byte[] array2;
			if (array.Length == --i)
			{
				array2 = array;
			}
			else
			{
				array2 = new byte[i];
				Buffer.BlockCopy(array, 0, array2, 0, i);
			}
			return array2;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x000D0530 File Offset: 0x000CE730
		internal byte[] UnmarshalCHR(int retLength)
		{
			byte[] array;
			if (this.m_typeRepresentation.ConversionRequired)
			{
				array = this.UnmarshalCLR(retLength, this.retLen);
				if (array.Length != this.retLen[0])
				{
					byte[] array2 = new byte[this.retLen[0]];
					Array.Copy(array, 0, array2, 0, this.retLen[0]);
					array = array2;
				}
			}
			else
			{
				array = this.GetNBytes(retLength);
			}
			return array;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x000D0594 File Offset: 0x000CE794
		internal int GetNBytes(byte[] buf, int off, int len)
		{
			int result;
			if ((result = this.m_oraBufRdr.Read(buf, off, len)) < 0)
			{
				throw new Exception("TTC Error");
			}
			return result;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x000D05C4 File Offset: 0x000CE7C4
		internal byte[] GetNBytes(int n)
		{
			byte[] array = new byte[n];
			if (this.m_oraBufRdr.Read(array) < 0)
			{
				throw new Exception("TTC Error");
			}
			return array;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x000D05F4 File Offset: 0x000CE7F4
		internal byte[] UnmarshalNBytes(int length)
		{
			byte[] array = new byte[length];
			if (length > 0 && this.m_oraBufRdr.Read(array) < 0)
			{
				throw new Exception("TTC Error");
			}
			return array;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x000D0628 File Offset: 0x000CE828
		internal int UnmarshalNBytes(byte[] buf, int off, int n)
		{
			int i;
			for (i = 0; i < n; i += this.GetNBytes(buf, off + i, n - i))
			{
			}
			return i;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x000D0650 File Offset: 0x000CE850
		internal int UnmarshalUCS2(byte[] ucs2Char, long offset)
		{
			int num = this.UnmarshalUB2(false);
			this.tmpBuffer2[0] = (byte)((num & 65280) >> 8);
			this.tmpBuffer2[1] = (byte)(num & 255);
			if (offset + 1L < (long)ucs2Char.Length)
			{
				ucs2Char[(int)offset] = this.tmpBuffer2[0];
				ucs2Char[(int)offset + 1] = this.tmpBuffer2[1];
			}
			if (this.tmpBuffer2[0] != 0)
			{
				return 3;
			}
			if (this.tmpBuffer2[1] != 0)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x000D06C4 File Offset: 0x000CE8C4
		internal byte[] UnmarshalDALC(bool bIgnoreData, int[] actualLen = null)
		{
			byte[] array = null;
			long num = this.UnmarshalUB4(false);
			num &= (long)((ulong)-1 & (ulong)num);
			if (num > 0L)
			{
				if (bIgnoreData)
				{
					int num2 = 0;
					List<ArraySegment<byte>> list;
					this.UnmarshalCLR_ScanOnly((int)num, out list, ref num2);
				}
				else
				{
					if (actualLen == null)
					{
						actualLen = this.retLen;
					}
					array = this.UnmarshalCLR((int)num, actualLen);
					if (array == null)
					{
						throw new Exception("TTC Error");
					}
				}
			}
			return array;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x000D0720 File Offset: 0x000CE920
		internal byte[] UnmarshalCLRforREFS(bool bIgnoreData = false)
		{
			int num = 0;
			byte[] array = null;
			ArrayList arrayList = null;
			if (!bIgnoreData)
			{
				arrayList = new ArrayList();
			}
			short num2 = this.UnmarshalUB1(false);
			if (num2 < 0)
			{
				throw new Exception("TTC Error");
			}
			if (num2 == 0)
			{
				return null;
			}
			if (!this.EscapeSequenceNull((int)num2))
			{
				if (num2 == 254)
				{
					int num3;
					while ((num3 = (this.m_bUseBigCLRChunks ? this.UnmarshalSB4() : ((int)this.UnmarshalUB1(false)))) > 0)
					{
						if (bIgnoreData)
						{
							this.UnmarshalBuffer_ScanOnly(num3);
						}
						else
						{
							num += num3;
							byte[] array2 = new byte[num3];
							this.UnmarshalBuffer(array2, 0, num3);
							arrayList.Add(array2);
						}
					}
				}
				else if (bIgnoreData)
				{
					this.UnmarshalBuffer_ScanOnly((int)num2);
				}
				else
				{
					num = (int)num2;
					byte[] array3 = new byte[(int)num2];
					this.UnmarshalBuffer(array3, 0, (int)num2);
					arrayList.Add(array3);
				}
				if (!bIgnoreData)
				{
					array = new byte[num];
					int num4 = 0;
					for (int i = 0; i < arrayList.Count; i++)
					{
						int num5 = ((byte[])arrayList[i]).Length;
						Buffer.BlockCopy((byte[])arrayList[i], 0, array, num4, num5);
						num4 += num5;
					}
				}
			}
			else
			{
				array = null;
			}
			return array;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x000D0848 File Offset: 0x000CEA48
		internal void UnmarshalKeywordValuePair(out int keyword, out string stringValue, out byte[] binaryValue)
		{
			keyword = 0;
			stringValue = null;
			binaryValue = null;
			int[] array = new int[1];
			int num = this.UnmarshalUB2(false);
			if (num != 0)
			{
				byte[] bytes = new byte[num];
				this.UnmarshalCLR(bytes, 0, array);
				stringValue = this.m_dbCharSetConv.ConvertBytesToString(bytes, 0, array[0], null, true);
			}
			int num2 = this.UnmarshalUB2(false);
			if (num2 != 0)
			{
				binaryValue = new byte[num2];
				this.UnmarshalCLR(binaryValue, 0, array);
			}
			keyword = this.UnmarshalUB2(false);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x000D08BC File Offset: 0x000CEABC
		private void AddPointer(byte val)
		{
			if ((this.m_typeRepresentation.m_representationArray[4] & 1) > 0)
			{
				this.m_oraBufWriter.Write(val);
				return;
			}
			byte b = this.ValueToBuffer((long)((ulong)val), this.tmpBuffer4, 4);
			if (b != 0)
			{
				this.m_oraBufWriter.Write(this.tmpBuffer4, 0, (int)b);
			}
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x000D0910 File Offset: 0x000CEB10
		private long BufferToValue(byte repOffset, bool bIgnoreData = false)
		{
			byte[] array = null;
			int num = 0;
			int num2 = 0;
			long num3 = 0L;
			byte b = this.m_typeRepresentation.m_representationArray[(int)repOffset];
			bool flag = this.m_oraBufRdr.ReadLengthAndData(repOffset, b, out array, ref num, ref num2, bIgnoreData);
			if (bIgnoreData)
			{
				return num3;
			}
			switch (num2)
			{
			case 1:
				num3 = (long)((ulong)array[num]);
				break;
			case 2:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8);
				}
				else
				{
					num3 = (long)((int)array[num + 1] | (int)array[num] << 8);
				}
				break;
			case 3:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8 | (int)array[num + 2] << 16);
				}
				else
				{
					num3 = (long)((int)array[num + 2] | (int)array[num + 1] << 8 | (int)array[num] << 16);
				}
				break;
			case 4:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8 | (int)array[num + 2] << 16 | (int)array[num + 3] << 24);
				}
				else
				{
					num3 = (long)((int)array[num + 3] | (int)array[num + 2] << 8 | (int)array[num + 1] << 16 | (int)array[num] << 24);
				}
				break;
			case 5:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8 | (int)array[num + 2] << 16 | (int)array[num + 3] << 24 | (int)array[num + 4]);
				}
				else
				{
					num3 = (long)((int)array[num + 4] | (int)array[num + 3] << 8 | (int)array[num + 2] << 16 | (int)array[num + 1] << 24 | (int)array[num]);
				}
				break;
			case 6:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8 | (int)array[num + 2] << 16 | (int)array[num + 3] << 24 | (int)array[num + 4] | (int)array[num + 5] << 8);
				}
				else
				{
					num3 = (long)((int)array[num + 5] | (int)array[num + 4] << 8 | (int)array[num + 3] << 16 | (int)array[num + 2] << 24 | (int)array[num + 1] | (int)array[num] << 8);
				}
				break;
			case 7:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8 | (int)array[num + 2] << 16 | (int)array[num + 3] << 24 | (int)array[num + 4] | (int)array[num + 5] << 8 | (int)array[num + 6] << 16);
				}
				else
				{
					num3 = (long)((int)array[num + 6] | (int)array[num + 5] << 8 | (int)array[num + 4] << 16 | (int)array[num + 3] << 24 | (int)array[num + 2] | (int)array[num + 1] << 8 | (int)array[num] << 16);
				}
				break;
			case 8:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8 | (int)array[num + 2] << 16 | (int)array[num + 3] << 24 | (int)array[num + 4] | (int)array[num + 5] << 8 | (int)array[num + 6] << 16 | (int)array[num + 7] << 24);
				}
				else
				{
					num3 = (long)((int)array[num + 7] | (int)array[num + 6] << 8 | (int)array[num + 5] << 16 | (int)array[num + 4] << 24 | (int)array[num + 3] | (int)array[num + 2] << 8 | (int)array[num + 1] << 16 | (int)array[num] << 24);
				}
				break;
			}
			if (flag)
			{
				num3 = -num3;
			}
			return num3;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x000D0C18 File Offset: 0x000CEE18
		internal byte GetNoOfBytesToBeWritten(int value, byte repOffset)
		{
			bool flag = true;
			byte b = 0;
			for (int i = 3; i >= 0; i--)
			{
				byte b2 = (byte)HelperClass.URShift(value, 8 * i);
				if ((this.m_typeRepresentation.m_representationArray[(int)repOffset] & 1) > 0)
				{
					if (!flag || b2 != 0)
					{
						flag = false;
						b += 1;
					}
				}
				else
				{
					b += 1;
				}
			}
			return b;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x000D0C6C File Offset: 0x000CEE6C
		private byte ValueToBuffer(long value, byte[] outBuffer, byte repOffset)
		{
			bool flag = true;
			byte b = 0;
			for (int i = outBuffer.Length - 1; i >= 0; i--)
			{
				outBuffer[(int)b] = (byte)HelperClass.URShift(value, 8 * i);
				if ((this.m_typeRepresentation.m_representationArray[(int)repOffset] & 1) > 0)
				{
					if (!flag || outBuffer[(int)b] != 0)
					{
						flag = false;
						b += 1;
					}
				}
				else
				{
					b += 1;
				}
			}
			if ((this.m_typeRepresentation.m_representationArray[(int)repOffset] & 1) > 0)
			{
				this.m_oraBufWriter.Write(b);
			}
			if ((this.m_typeRepresentation.m_representationArray[(int)repOffset] & 2) > 0)
			{
				this.ReverseArray(outBuffer, (int)b, 0);
			}
			return b;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x000D0CFC File Offset: 0x000CEEFC
		internal bool EscapeSequenceNull(int bytes)
		{
			bool result = false;
			if (bytes != 0)
			{
				switch (bytes)
				{
				case 253:
					throw new Exception("TTC Error");
				case 255:
					result = true;
					break;
				}
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x000D0D40 File Offset: 0x000CEF40
		private void ReverseArray(byte[] buffer, int bytes, int offset)
		{
			bytes += 2 * offset;
			for (int i = offset; i < bytes / 2; i++)
			{
				byte b = buffer[i];
				buffer[i] = buffer[bytes - 1 - i];
				buffer[bytes - 1 - i] = b;
			}
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x000D0D78 File Offset: 0x000CEF78
		internal int ProcessIndicator(bool isNull, int dataSize)
		{
			short num = this.UnmarshalSB2();
			int result = -1;
			if (!isNull)
			{
				if (num == 0)
				{
					result = dataSize;
				}
				else if (-1 == num)
				{
					result = (int)num;
				}
				else
				{
					result = (int)num;
				}
			}
			return result;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x000D0DA4 File Offset: 0x000CEFA4
		internal void UnmarshalCLR_ColData(int maxSize)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			int num3 = (int)this.UnmarshalUB1(false);
			if (num3 < 0)
			{
				throw new Exception("TTC Error");
			}
			if (this.EscapeSequenceNull(num3))
			{
				this.m_oraBufRdr.m_colDataStartOffset[this.m_oraBufRdr.m_colDataStartOffsetIndexToUpdate] = -1;
			}
			else if (num3 != 254)
			{
				if (num3 > 0)
				{
					int num4 = (maxSize < num3) ? maxSize : num3;
					num = this.UnmarshalBuffer_ScanOnly(num4);
					num2 += num4;
					int num5 = num3 - num4;
					if (num5 > 0)
					{
						this.UnmarshalBuffer_ScanOnly(num5);
					}
				}
			}
			else
			{
				int num6 = -1;
				bool flag2 = false;
				while (!flag2)
				{
					if (num6 != -1)
					{
						if (this.m_bUseBigCLRChunks)
						{
							num3 = this.UnmarshalSB4();
						}
						else
						{
							num3 = (int)this.UnmarshalUB1(false);
						}
						if (num3 <= 0)
						{
							flag2 = true;
							continue;
						}
					}
					if (num3 == 254)
					{
						switch (num6)
						{
						case -1:
							num6 = 1;
							continue;
						case 0:
							if (!flag)
							{
								num6 = 0;
								continue;
							}
							break;
						}
					}
					if (num == -1)
					{
						this.UnmarshalBuffer_ScanOnly(num3);
					}
					else
					{
						int num7 = num3;
						if (num7 > 0)
						{
							int num4 = (maxSize - num2 < num7) ? (maxSize - num2) : num7;
							num = this.UnmarshalBuffer_ScanOnly(num4);
							num2 += num4;
							int num8 = num7 - num4;
							if (num8 > 0)
							{
								this.UnmarshalBuffer_ScanOnly(num8);
							}
						}
					}
					num6 = 0;
					if (num3 > 252)
					{
						flag = true;
					}
				}
			}
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x000D0F10 File Offset: 0x000CF110
		internal void UnmarshalCLR_ScanOnly(int maxSize, out List<ArraySegment<byte>> dataSegments, ref int length)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			try
			{
				dataSegments = null;
				length = 0;
				int num3 = (int)this.UnmarshalUB1(false);
				if (num3 < 0)
				{
					throw new Exception("TTC Error");
				}
				if (this.EscapeSequenceNull(num3))
				{
					length = 0;
				}
				else if (num3 != 254)
				{
					if (num3 > 0)
					{
						int num4 = (maxSize < num3) ? maxSize : num3;
						num = this.UnmarshalBuffer_ScanOnly(num4);
						num2 += num4;
						int num5 = num3 - num4;
						if (num5 > 0)
						{
							this.UnmarshalBuffer_ScanOnly(num5);
						}
					}
				}
				else
				{
					int num6 = -1;
					bool flag2 = false;
					while (!flag2)
					{
						if (num6 != -1)
						{
							if (this.m_bUseBigCLRChunks)
							{
								num3 = this.UnmarshalSB4();
							}
							else
							{
								num3 = (int)this.UnmarshalUB1(false);
							}
							if (num3 <= 0)
							{
								flag2 = true;
								continue;
							}
						}
						if (num3 == 254)
						{
							switch (num6)
							{
							case -1:
								num6 = 1;
								continue;
							case 0:
								if (!flag)
								{
									num6 = 0;
									continue;
								}
								break;
							}
						}
						if (num == -1)
						{
							this.UnmarshalBuffer_ScanOnly(num3);
						}
						else
						{
							int num7 = num3;
							if (num7 > 0)
							{
								int num4 = (maxSize - num2 < num7) ? (maxSize - num2) : num7;
								num = this.UnmarshalBuffer_ScanOnly(num4);
								num2 += num4;
								int num8 = num7 - num4;
								if (num8 > 0)
								{
									this.UnmarshalBuffer_ScanOnly(num8);
								}
							}
						}
						num6 = 0;
						if (num3 > 252)
						{
							flag = true;
						}
					}
				}
			}
			finally
			{
				length = num2;
				dataSegments = this.m_oraBufRdr.m_dataSegments;
			}
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x000D109C File Offset: 0x000CF29C
		internal int GetNBytes_ScanOnly(int len)
		{
			int result;
			if ((result = this.m_oraBufRdr.Read(null, 0, len)) < 0)
			{
				throw new Exception("TTC Error");
			}
			return result;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x000D10CC File Offset: 0x000CF2CC
		internal int UnmarshalNBytes_ScanOnly(int n)
		{
			int i;
			for (i = 0; i < n; i += this.GetNBytes_ScanOnly(n))
			{
			}
			return i;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x000D10EC File Offset: 0x000CF2EC
		internal int UnmarshalBuffer_ScanOnly(int len)
		{
			int result;
			if ((result = this.m_oraBufRdr.Read(null, 0, len)) < 0)
			{
				throw new Exception("TTC Error");
			}
			return result;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x000D111C File Offset: 0x000CF31C
		internal void ProcessReset()
		{
			try
			{
				this.m_oracleCommunication.Reset();
				byte b = this.UnmarshalSB1();
				byte b2 = b;
				if (b2 != 4)
				{
					throw new Exception("OraBufWriter:ReadResetResponse - Unexpected Packet received.");
				}
				this.TTCErrorObject.Initialize();
				this.TTCErrorObject.ReadErrorMessage();
			}
			catch (NetworkException)
			{
				throw;
			}
		}

		// Token: 0x040014C1 RID: 5313
		private const int TTCC_MXL = 252;

		// Token: 0x040014C2 RID: 5314
		internal const int TTCC_ESC = 253;

		// Token: 0x040014C3 RID: 5315
		internal const int TTCC_LNG = 254;

		// Token: 0x040014C4 RID: 5316
		internal const int TTCC_ERR = 255;

		// Token: 0x040014C5 RID: 5317
		internal const int TTCC_MXIN_NEW = 32767;

		// Token: 0x040014C6 RID: 5318
		internal const int TTCC_MXIN_OLD = 64;

		// Token: 0x040014C7 RID: 5319
		internal const byte TTCLXMULTI = 1;

		// Token: 0x040014C8 RID: 5320
		internal const byte TTCLXMCONV = 2;

		// Token: 0x040014C9 RID: 5321
		private const int FLUSH_DATA_SIZE_THRESHOLD = 65536;

		// Token: 0x040014CA RID: 5322
		internal int m_effectiveTTCC_MXIN = 64;

		// Token: 0x040014CB RID: 5323
		internal int m_numOBThresholdForSends = 1;

		// Token: 0x040014CC RID: 5324
		private short m_DBVersion;

		// Token: 0x040014CD RID: 5325
		private bool m_bHasFSAPCapability;

		// Token: 0x040014CE RID: 5326
		internal bool m_hasEOCSCapability;

		// Token: 0x040014CF RID: 5327
		internal bool m_bUseBigCLRChunks;

		// Token: 0x040014D0 RID: 5328
		internal bool m_bServerUsingBigSCN;

		// Token: 0x040014D1 RID: 5329
		internal TTCTypeRepresentation m_typeRepresentation;

		// Token: 0x040014D2 RID: 5330
		internal OracleCommunication m_oracleCommunication;

		// Token: 0x040014D3 RID: 5331
		internal byte m_negotiatedTTCVersion;

		// Token: 0x040014D4 RID: 5332
		internal long m_endOfCallStatus;

		// Token: 0x040014D5 RID: 5333
		internal bool m_bDRCPConnection;

		// Token: 0x040014D6 RID: 5334
		internal bool m_bDRCPSessionAttached;

		// Token: 0x040014D7 RID: 5335
		internal int m_endToEndECIDSequenceNumber;

		// Token: 0x040014D8 RID: 5336
		internal byte[] m_ltxId;

		// Token: 0x040014D9 RID: 5337
		internal byte[] ignored = new byte[255];

		// Token: 0x040014DA RID: 5338
		internal byte[] tmpBuffer2 = new byte[2];

		// Token: 0x040014DB RID: 5339
		internal byte[] tmpBuffer4 = new byte[4];

		// Token: 0x040014DC RID: 5340
		internal byte[] tmpBuffer6 = new byte[6];

		// Token: 0x040014DD RID: 5341
		internal byte[] tmpBuffer8 = new byte[8];

		// Token: 0x040014DE RID: 5342
		internal int[] retLen = new int[1];

		// Token: 0x040014DF RID: 5343
		private TTCError m_ttcError;

		// Token: 0x040014E0 RID: 5344
		internal OraBufReader m_oraBufRdr;

		// Token: 0x040014E1 RID: 5345
		internal OraBufWriter m_oraBufWriter;

		// Token: 0x040014E2 RID: 5346
		internal TTCSessionGet m_drcpSessionGet;

		// Token: 0x040014E3 RID: 5347
		internal TTCSessionRelease m_drcpSessionRelease;

		// Token: 0x040014E4 RID: 5348
		internal TTCSessionReturnValues m_drcpSessionReturnValues;

		// Token: 0x040014E5 RID: 5349
		internal Conv m_dbCharSetConv;

		// Token: 0x040014E6 RID: 5350
		internal Conv m_nCharSetConv;

		// Token: 0x040014E7 RID: 5351
		internal bool m_bSvrCSMultibyte;

		// Token: 0x040014E8 RID: 5352
		internal CharArrayPooler m_charArrayPooler;

		// Token: 0x040014E9 RID: 5353
		internal OracleConnectionImpl m_connImplReference;
	}
}
