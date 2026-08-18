using System;
using System.Diagnostics;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000625 RID: 1573
	internal class IcmpPacket
	{
		// Token: 0x0600306E RID: 12398 RVA: 0x000D1743 File Offset: 0x000D0743
		internal IcmpPacket(byte[] buffer)
		{
			this.type = 8;
			this.buffer = buffer;
			ushort num = IcmpPacket.staticSequenceNumber;
			IcmpPacket.staticSequenceNumber = num + 1;
			this.sequenceNumber = num;
			this.checkSum = (ushort)this.GetCheckSum();
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x000D177A File Offset: 0x000D077A
		internal ushort Identifier
		{
			get
			{
				if (IcmpPacket.identifier == 0)
				{
					IcmpPacket.identifier = (ushort)Process.GetCurrentProcess().Id;
				}
				return IcmpPacket.identifier;
			}
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000D1798 File Offset: 0x000D0798
		private uint GetCheckSum()
		{
			uint num = (uint)((ushort)this.type + this.Identifier + this.sequenceNumber);
			for (int i = 0; i < this.buffer.Length; i++)
			{
				num += (uint)((int)this.buffer[i] + ((int)this.buffer[++i] << 8));
			}
			num = (num >> 16) + (num & 65535U);
			num += num >> 16;
			return ~num;
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000D1800 File Offset: 0x000D0800
		internal byte[] GetBytes()
		{
			byte[] array = new byte[this.buffer.Length + 8];
			byte[] bytes = BitConverter.GetBytes(this.checkSum);
			byte[] bytes2 = BitConverter.GetBytes(this.Identifier);
			byte[] bytes3 = BitConverter.GetBytes(this.sequenceNumber);
			array[0] = this.type;
			array[1] = this.subCode;
			Array.Copy(bytes, 0, array, 2, 2);
			Array.Copy(bytes2, 0, array, 4, 2);
			Array.Copy(bytes3, 0, array, 6, 2);
			Array.Copy(this.buffer, 0, array, 8, this.buffer.Length);
			return array;
		}

		// Token: 0x04002E21 RID: 11809
		private static ushort staticSequenceNumber;

		// Token: 0x04002E22 RID: 11810
		internal byte type;

		// Token: 0x04002E23 RID: 11811
		internal byte subCode;

		// Token: 0x04002E24 RID: 11812
		internal ushort checkSum;

		// Token: 0x04002E25 RID: 11813
		internal static ushort identifier;

		// Token: 0x04002E26 RID: 11814
		internal ushort sequenceNumber;

		// Token: 0x04002E27 RID: 11815
		internal byte[] buffer;
	}
}
