using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000150 RID: 336
	public class DerUniversalString : DerStringBase
	{
		// Token: 0x06000C0F RID: 3087 RVA: 0x00042BC8 File Offset: 0x00041BC8
		public static DerUniversalString GetInstance(object obj)
		{
			if (obj == null || obj is DerUniversalString)
			{
				return (DerUniversalString)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerUniversalString(((Asn1OctetString)obj).GetOctets());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00042C1A File Offset: 0x00041C1A
		public static DerUniversalString GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerUniversalString.GetInstance(obj.GetObject());
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00042C27 File Offset: 0x00041C27
		public DerUniversalString(byte[] str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00042C44 File Offset: 0x00041C44
		public override string GetString()
		{
			StringBuilder stringBuilder = new StringBuilder("#");
			byte[] derEncoded = base.GetDerEncoded();
			for (int num = 0; num != derEncoded.Length; num++)
			{
				uint num2 = (uint)derEncoded[num];
				stringBuilder.Append(DerUniversalString.table[(int)((UIntPtr)(num2 >> 4 & 15U))]);
				stringBuilder.Append(DerUniversalString.table[(int)(derEncoded[num] & 15)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00042CA2 File Offset: 0x00041CA2
		public byte[] GetOctets()
		{
			return (byte[])this.str.Clone();
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00042CB4 File Offset: 0x00041CB4
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(28, this.str);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00042CC4 File Offset: 0x00041CC4
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerUniversalString derUniversalString = asn1Object as DerUniversalString;
			return derUniversalString != null && Arrays.AreEqual(this.str, derUniversalString.str);
		}

		// Token: 0x04000984 RID: 2436
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

		// Token: 0x04000985 RID: 2437
		private readonly byte[] str;
	}
}
