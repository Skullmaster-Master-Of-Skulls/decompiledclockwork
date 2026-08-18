using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000072 RID: 114
	public class DerBitString : DerStringBase
	{
		// Token: 0x060003AB RID: 939 RVA: 0x00013AEC File Offset: 0x00012AEC
		internal static int GetPadBits(int bitString)
		{
			int num = 0;
			for (int i = 3; i >= 0; i--)
			{
				if (i != 0)
				{
					if (bitString >> i * 8 != 0)
					{
						num = (bitString >> i * 8 & 255);
						break;
					}
				}
				else if (bitString != 0)
				{
					num = (bitString & 255);
					break;
				}
			}
			if (num == 0)
			{
				return 7;
			}
			int num2 = 1;
			while (((num <<= 1) & 255) != 0)
			{
				num2++;
			}
			return 8 - num2;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00013B50 File Offset: 0x00012B50
		internal static byte[] GetBytes(int bitString)
		{
			int num = 4;
			int num2 = 3;
			while (num2 >= 1 && (bitString & 255 << num2 * 8) == 0)
			{
				num--;
				num2--;
			}
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (byte)(bitString >> i * 8 & 255);
			}
			return array;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00013BA8 File Offset: 0x00012BA8
		public static DerBitString GetInstance(object obj)
		{
			if (obj == null || obj is DerBitString)
			{
				return (DerBitString)obj;
			}
			if (obj is Asn1OctetString)
			{
				byte[] octets = ((Asn1OctetString)obj).GetOctets();
				int num = (int)octets[0];
				byte[] destinationArray = new byte[octets.Length - 1];
				Array.Copy(octets, 1, destinationArray, 0, octets.Length - 1);
				return new DerBitString(destinationArray, num);
			}
			if (obj is Asn1TaggedObject)
			{
				return DerBitString.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00013C33 File Offset: 0x00012C33
		public static DerBitString GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerBitString.GetInstance(obj.GetObject());
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00013C40 File Offset: 0x00012C40
		internal DerBitString(byte data, int padBits)
		{
			this.data = new byte[]
			{
				data
			};
			this.padBits = padBits;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00013C6C File Offset: 0x00012C6C
		public DerBitString(byte[] data, int padBits)
		{
			this.data = data;
			this.padBits = padBits;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00013C82 File Offset: 0x00012C82
		public DerBitString(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00013C91 File Offset: 0x00012C91
		public DerBitString(Asn1Encodable obj)
		{
			this.data = obj.GetDerEncoded();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00013CA5 File Offset: 0x00012CA5
		public byte[] GetBytes()
		{
			return this.data;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00013CAD File Offset: 0x00012CAD
		public int PadBits
		{
			get
			{
				return this.padBits;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00013CB8 File Offset: 0x00012CB8
		public int IntValue
		{
			get
			{
				int num = 0;
				int num2 = 0;
				while (num2 != this.data.Length && num2 != 4)
				{
					num |= (int)(this.data[num2] & byte.MaxValue) << 8 * num2;
					num2++;
				}
				return num;
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00013CF8 File Offset: 0x00012CF8
		internal override void Encode(DerOutputStream derOut)
		{
			byte[] array = new byte[this.GetBytes().Length + 1];
			array[0] = (byte)this.PadBits;
			Array.Copy(this.GetBytes(), 0, array, 1, array.Length - 1);
			derOut.WriteEncoded(3, array);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00013D3C File Offset: 0x00012D3C
		protected override int Asn1GetHashCode()
		{
			return this.padBits.GetHashCode() ^ Arrays.GetHashCode(this.data);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00013D64 File Offset: 0x00012D64
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerBitString derBitString = asn1Object as DerBitString;
			return derBitString != null && this.padBits == derBitString.padBits && Arrays.AreEqual(this.data, derBitString.data);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00013DA0 File Offset: 0x00012DA0
		public override string GetString()
		{
			StringBuilder stringBuilder = new StringBuilder("#");
			byte[] derEncoded = base.GetDerEncoded();
			for (int num = 0; num != derEncoded.Length; num++)
			{
				uint num2 = (uint)derEncoded[num];
				stringBuilder.Append(DerBitString.table[(int)((UIntPtr)(num2 >> 4 & 15U))]);
				stringBuilder.Append(DerBitString.table[(int)(derEncoded[num] & 15)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040001FB RID: 507
		private static readonly char[] table = new char[]
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
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};

		// Token: 0x040001FC RID: 508
		private readonly byte[] data;

		// Token: 0x040001FD RID: 509
		private readonly int padBits;
	}
}
