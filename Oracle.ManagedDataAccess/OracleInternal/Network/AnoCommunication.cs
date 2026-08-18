using System;
using OracleInternal.Common;
using OracleInternal.I18N;

namespace OracleInternal.Network
{
	// Token: 0x02000147 RID: 327
	internal class AnoCommunication
	{
		// Token: 0x06000CCE RID: 3278 RVA: 0x0008DEBC File Offset: 0x0008C0BC
		internal AnoCommunication(SessionContext sessCtx)
		{
			this.sessCtx = sessCtx;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0008DECC File Offset: 0x0008C0CC
		internal long GetVersion()
		{
			return 186647040L;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0008DED4 File Offset: 0x0008C0D4
		internal void FlushData()
		{
			this.sessCtx.m_writerStream.Flush();
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0008DEE8 File Offset: 0x0008C0E8
		internal void SendUB1(short num)
		{
			this.SendPacketHeader(1, 2);
			this.sessCtx.m_writerStream.Write((byte)(255 & num));
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0008DF0C File Offset: 0x0008C10C
		internal void SendUB2(int num)
		{
			this.SendPacketHeader(2, 3);
			this.WriteUB2(num);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0008DF20 File Offset: 0x0008C120
		internal void SendUB4(long num)
		{
			this.SendPacketHeader(4, 4);
			this.WriteUB4(num);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0008DF34 File Offset: 0x0008C134
		internal void SendUB2Array(int[] nArray)
		{
			int num = nArray.Length;
			this.SendPacketHeader(10 + num * 2, 1);
			this.WriteUB4((long)((ulong)-559038737));
			this.WriteUB2(3);
			this.WriteUB4((long)nArray.Length);
			for (int i = 0; i < num; i++)
			{
				this.WriteUB2(nArray[i] & 65535);
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0008DF8C File Offset: 0x0008C18C
		internal void SendStatus(int status)
		{
			this.SendPacketHeader(2, 6);
			this.WriteUB2(status);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0008DFA0 File Offset: 0x0008C1A0
		internal void SendVersion()
		{
			this.SendPacketHeader(4, 5);
			this.WriteUB4(this.GetVersion());
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0008DFB8 File Offset: 0x0008C1B8
		internal void SendString(string str)
		{
			this.SendPacketHeader(str.Length, 0);
			this.sessCtx.m_writerStream.Write(Conv.GetInstance(871).ConvertStringToBytes(str, 0, str.Length, true));
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0008DFF0 File Offset: 0x0008C1F0
		internal void SendRaw(byte[] rawData)
		{
			this.SendPacketHeader(rawData.Length, 1);
			this.sessCtx.m_writerStream.Write(rawData);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0008E010 File Offset: 0x0008C210
		internal void SendPacketHeader(int length, int type)
		{
			this.ValidateType(length, type);
			this.WriteUB2(length);
			this.WriteUB2(type);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0008E028 File Offset: 0x0008C228
		internal void WriteVersion()
		{
			this.WriteUB4(this.GetVersion());
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0008E038 File Offset: 0x0008C238
		internal void WriteUB1(short num)
		{
			this.sessCtx.m_writerStream.Write((byte)(255 & num));
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0008E054 File Offset: 0x0008C254
		internal void WriteUB2(int num)
		{
			byte[] array = new byte[2];
			byte length = this.Value2Buffer((int)((short)(65535 & num)), array);
			this.sessCtx.m_writerStream.Write(array, 0, (int)length);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0008E08C File Offset: 0x0008C28C
		internal void WriteUB4(long num)
		{
			byte[] array = new byte[4];
			byte length = this.Value2Buffer((int)((ulong)-1 & (ulong)num), array);
			this.sessCtx.m_writerStream.Write(array, 0, (int)length);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0008E0C0 File Offset: 0x0008C2C0
		internal byte Value2Buffer(int value, byte[] tmpBuffer)
		{
			byte b = 0;
			for (int i = tmpBuffer.Length - 1; i >= 0; i--)
			{
				byte b2 = b;
				b = b2 + 1;
				tmpBuffer[(int)b2] = (byte)HelperClass.URShift(value, 8 * i & 255);
			}
			return b;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0008E0F8 File Offset: 0x0008C2F8
		internal short ReceiveUB1()
		{
			this.ReceivePacketHeader(2);
			return this.ReadUB1();
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0008E108 File Offset: 0x0008C308
		internal int ReceiveUB2()
		{
			this.ReceivePacketHeader(3);
			int num = this.ReadUB2();
			return num & 65535;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0008E12C File Offset: 0x0008C32C
		internal long ReceiveUB4()
		{
			this.ReceivePacketHeader(4);
			return this.ReadUB4();
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0008E13C File Offset: 0x0008C33C
		internal int[] receiveUB2Array()
		{
			this.ReceivePacketHeader(1);
			long num = this.ReadUB4();
			int num2 = this.ReadUB2();
			long num3 = this.ReadUB4();
			int[] array = new int[(int)num3];
			if (num != (long)((ulong)-559038737) || num2 != 3)
			{
				throw new NetworkException(-6310);
			}
			int num4 = 0;
			while ((long)num4 < num3)
			{
				array[num4] = this.ReadUB2();
				num4++;
			}
			return array;
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0008E1A4 File Offset: 0x0008C3A4
		internal int ReceiveStatus()
		{
			this.ReceivePacketHeader(6);
			return this.ReadUB2();
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0008E1B4 File Offset: 0x0008C3B4
		internal long ReceiveVersion()
		{
			this.ReceivePacketHeader(5);
			return this.ReadUB4();
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0008E1C4 File Offset: 0x0008C3C4
		internal string ReceiveString()
		{
			int size = this.ReceivePacketHeader(0);
			byte[] array = this.ReceiveByteArray(size);
			return Conv.GetInstance(871).ConvertBytesToString(array, 0, array.Length, null, true);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0008E1F8 File Offset: 0x0008C3F8
		internal byte[] ReceiveRaw()
		{
			int size = this.ReceivePacketHeader(1);
			return this.ReceiveByteArray(size);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0008E214 File Offset: 0x0008C414
		internal short ReadUB1()
		{
			short result = 0;
			try
			{
				if ((result = (short)this.sessCtx.m_readerStream.ReadOne()) < 0)
				{
					throw new NetworkException(12637);
				}
			}
			catch (Exception)
			{
				throw new NetworkException(1);
			}
			return result;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0008E260 File Offset: 0x0008C460
		internal int ReadUB2()
		{
			byte[] tmpBuffer = new byte[2];
			int num = (int)this.Buffer2Value(tmpBuffer);
			return num & 65535;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0008E284 File Offset: 0x0008C484
		internal long ReadUB4()
		{
			byte[] tmpBuffer = new byte[4];
			return this.Buffer2Value(tmpBuffer);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0008E2A4 File Offset: 0x0008C4A4
		internal byte[] ReceiveByteArray(int size)
		{
			byte[] array = new byte[size];
			try
			{
				if (this.sessCtx.m_readerStream.Read(array) < 0)
				{
					throw new NetworkException(12637);
				}
			}
			catch (Exception)
			{
				throw new NetworkException(1);
			}
			return array;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0008E2F4 File Offset: 0x0008C4F4
		internal int ReceiveByteArray(byte[] buffer, int offset, int length)
		{
			int result = 0;
			try
			{
				if ((result = this.sessCtx.m_readerStream.Read(buffer, offset, length)) < 0)
				{
					throw new NetworkException(12637);
				}
			}
			catch (Exception)
			{
				throw new NetworkException(1);
			}
			return result;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0008E344 File Offset: 0x0008C544
		internal int ReceivePacketHeader(int type)
		{
			int num = this.ReadUB2();
			int receivedType = this.ReadUB2();
			this.ValidateReceivedType(num, receivedType, type);
			return num;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0008E36C File Offset: 0x0008C56C
		internal void ValidateReceivedType(int length, int receivedType, int type)
		{
			if (receivedType < 0 || receivedType > 7)
			{
				throw new NetworkException(-6313);
			}
			if (receivedType != type)
			{
				throw new NetworkException(-6314);
			}
			switch (type)
			{
			case 0:
			case 1:
				break;
			case 2:
				if (length > 1)
				{
					throw new NetworkException(-6312);
				}
				break;
			case 3:
			case 6:
				if (length > 2)
				{
					throw new NetworkException(-6312);
				}
				break;
			case 4:
			case 5:
				if (length > 4)
				{
					throw new NetworkException(-6312);
				}
				break;
			case 7:
				if (length < 10)
				{
					throw new NetworkException(-6312);
				}
				break;
			default:
				throw new NetworkException(-6313);
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0008E410 File Offset: 0x0008C610
		internal long Buffer2Value(byte[] tmpBuffer)
		{
			long num = 0L;
			try
			{
				if (this.sessCtx.m_readerStream.Read(tmpBuffer) < 0)
				{
					throw new NetworkException(12637);
				}
			}
			catch (Exception)
			{
				throw new NetworkException(12637);
			}
			for (int i = 0; i < tmpBuffer.Length; i++)
			{
				num |= (long)((long)(tmpBuffer[i] & byte.MaxValue) << 8 * (tmpBuffer.Length - 1 - i));
			}
			num &= (long)((ulong)-1);
			return num;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0008E48C File Offset: 0x0008C68C
		internal void ValidateType(int length, int type)
		{
			if (type < 0 || type > 7)
			{
				throw new NetworkException(-6313);
			}
			switch (type)
			{
			case 0:
			case 1:
				break;
			case 2:
				if (length > 1)
				{
					throw new NetworkException(-6312);
				}
				break;
			case 3:
			case 6:
				if (length > 2)
				{
					throw new NetworkException(-6312);
				}
				break;
			case 4:
			case 5:
				if (length > 4)
				{
					throw new NetworkException(-6312);
				}
				break;
			case 7:
				if (length < 10)
				{
					throw new NetworkException(-6312);
				}
				break;
			default:
				throw new NetworkException(-6313);
			}
		}

		// Token: 0x04000DE6 RID: 3558
		internal const int STRING_TYPE = 0;

		// Token: 0x04000DE7 RID: 3559
		internal const int RAW_TYPE = 1;

		// Token: 0x04000DE8 RID: 3560
		internal const int UB1_TYPE = 2;

		// Token: 0x04000DE9 RID: 3561
		internal const int UB2_TYPE = 3;

		// Token: 0x04000DEA RID: 3562
		internal const int UB4_TYPE = 4;

		// Token: 0x04000DEB RID: 3563
		internal const int VERSION_TYPE = 5;

		// Token: 0x04000DEC RID: 3564
		internal const int STATUS_TYPE = 6;

		// Token: 0x04000DED RID: 3565
		internal const int ARRAY_TYPE = 7;

		// Token: 0x04000DEE RID: 3566
		internal const int MIN_TYPE = 0;

		// Token: 0x04000DEF RID: 3567
		internal const int MAX_TYPE = 7;

		// Token: 0x04000DF0 RID: 3568
		internal const int UB1_LENGTH = 1;

		// Token: 0x04000DF1 RID: 3569
		internal const int UB2_LENGTH = 2;

		// Token: 0x04000DF2 RID: 3570
		internal const int UB4_LENGTH = 4;

		// Token: 0x04000DF3 RID: 3571
		internal const int VERSION_LENGTH = 4;

		// Token: 0x04000DF4 RID: 3572
		internal const int STATUS_LENGTH = 2;

		// Token: 0x04000DF5 RID: 3573
		internal const int NA_MAGIC_SIZE = 4;

		// Token: 0x04000DF6 RID: 3574
		internal const long DEADBEEF = 3735928559L;

		// Token: 0x04000DF7 RID: 3575
		internal const int NA_HEADER_SIZE = 13;

		// Token: 0x04000DF8 RID: 3576
		internal const int ARRAY_PACKET_HEADER_LENGTH = 10;

		// Token: 0x04000DF9 RID: 3577
		internal const int SERVICE_HEADER_LENGTH = 8;

		// Token: 0x04000DFA RID: 3578
		internal const int SUBPACKET_LENGTH = 4;

		// Token: 0x04000DFB RID: 3579
		internal const long NA_MAGIC = 3735928559L;

		// Token: 0x04000DFC RID: 3580
		internal const int VERSION = 11;

		// Token: 0x04000DFD RID: 3581
		internal const int RELEASE = 2;

		// Token: 0x04000DFE RID: 3582
		internal const int UPDATE = 0;

		// Token: 0x04000DFF RID: 3583
		internal const int PORT = 2;

		// Token: 0x04000E00 RID: 3584
		internal const int PORTUPDATE = 0;

		// Token: 0x04000E01 RID: 3585
		internal const short NO_ERROR = 0;

		// Token: 0x04000E02 RID: 3586
		internal SessionContext sessCtx;
	}
}
