using System;
using System.Text;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002E7 RID: 743
	[__DynamicallyInvokable]
	public class PhysicalAddress
	{
		// Token: 0x06001A19 RID: 6681 RVA: 0x0007EA95 File Offset: 0x0007CC95
		[__DynamicallyInvokable]
		public PhysicalAddress(byte[] address)
		{
			this.address = address;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0007EAAC File Offset: 0x0007CCAC
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this.changed)
			{
				this.changed = false;
				this.hash = 0;
				int num = this.address.Length & -4;
				int i;
				for (i = 0; i < num; i += 4)
				{
					this.hash ^= ((int)this.address[i] | (int)this.address[i + 1] << 8 | (int)this.address[i + 2] << 16 | (int)this.address[i + 3] << 24);
				}
				if ((this.address.Length & 3) != 0)
				{
					int num2 = 0;
					int num3 = 0;
					while (i < this.address.Length)
					{
						num2 |= (int)this.address[i] << num3;
						num3 += 8;
						i++;
					}
					this.hash ^= num2;
				}
			}
			return this.hash;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0007EB74 File Offset: 0x0007CD74
		[__DynamicallyInvokable]
		public override bool Equals(object comparand)
		{
			PhysicalAddress physicalAddress = comparand as PhysicalAddress;
			if (physicalAddress == null)
			{
				return false;
			}
			if (this.address.Length != physicalAddress.address.Length)
			{
				return false;
			}
			for (int i = 0; i < physicalAddress.address.Length; i++)
			{
				if (this.address[i] != physicalAddress.address[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0007EBCC File Offset: 0x0007CDCC
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in this.address)
			{
				int num = b >> 4 & 15;
				for (int j = 0; j < 2; j++)
				{
					if (num < 10)
					{
						stringBuilder.Append((char)(num + 48));
					}
					else
					{
						stringBuilder.Append((char)(num + 55));
					}
					num = (int)(b & 15);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0007EC40 File Offset: 0x0007CE40
		[__DynamicallyInvokable]
		public byte[] GetAddressBytes()
		{
			byte[] array = new byte[this.address.Length];
			Buffer.BlockCopy(this.address, 0, array, 0, this.address.Length);
			return array;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0007EC74 File Offset: 0x0007CE74
		[__DynamicallyInvokable]
		public static PhysicalAddress Parse(string address)
		{
			int num = 0;
			bool flag = false;
			if (address == null)
			{
				return PhysicalAddress.None;
			}
			byte[] array;
			if (address.IndexOf('-') >= 0)
			{
				flag = true;
				array = new byte[(address.Length + 1) / 3];
			}
			else
			{
				if (address.Length % 2 > 0)
				{
					throw new FormatException(SR.GetString("net_bad_mac_address"));
				}
				array = new byte[address.Length / 2];
			}
			int num2 = 0;
			int i = 0;
			while (i < address.Length)
			{
				int num3 = (int)address[i];
				if (num3 >= 48 && num3 <= 57)
				{
					num3 -= 48;
					goto IL_C3;
				}
				if (num3 >= 65 && num3 <= 70)
				{
					num3 -= 55;
					goto IL_C3;
				}
				if (num3 != 45)
				{
					throw new FormatException(SR.GetString("net_bad_mac_address"));
				}
				if (num != 2)
				{
					throw new FormatException(SR.GetString("net_bad_mac_address"));
				}
				num = 0;
				IL_100:
				i++;
				continue;
				IL_C3:
				if (flag && num >= 2)
				{
					throw new FormatException(SR.GetString("net_bad_mac_address"));
				}
				if (num % 2 == 0)
				{
					array[num2] = (byte)(num3 << 4);
				}
				else
				{
					byte[] array2 = array;
					int num4 = num2++;
					array2[num4] |= (byte)num3;
				}
				num++;
				goto IL_100;
			}
			if (num < 2)
			{
				throw new FormatException(SR.GetString("net_bad_mac_address"));
			}
			return new PhysicalAddress(array);
		}

		// Token: 0x04001A6E RID: 6766
		private byte[] address;

		// Token: 0x04001A6F RID: 6767
		private bool changed = true;

		// Token: 0x04001A70 RID: 6768
		private int hash;

		// Token: 0x04001A71 RID: 6769
		[__DynamicallyInvokable]
		public static readonly PhysicalAddress None = new PhysicalAddress(new byte[0]);
	}
}
