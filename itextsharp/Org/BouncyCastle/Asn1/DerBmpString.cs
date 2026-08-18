using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020005C0 RID: 1472
	public class DerBmpString : DerStringBase
	{
		// Token: 0x06003293 RID: 12947 RVA: 0x0013962C File Offset: 0x0013862C
		public static DerBmpString GetInstance(object obj)
		{
			if (obj == null || obj is DerBmpString)
			{
				return (DerBmpString)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerBmpString(((Asn1OctetString)obj).GetOctets());
			}
			if (obj is Asn1TaggedObject)
			{
				return DerBmpString.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x00139697 File Offset: 0x00138697
		public static DerBmpString GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerBmpString.GetInstance(obj.GetObject());
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x001396A4 File Offset: 0x001386A4
		public DerBmpString(byte[] str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			char[] array = new char[str.Length / 2];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = (char)((int)str[2 * num] << 8 | (int)(str[2 * num + 1] & byte.MaxValue));
			}
			this.str = new string(array);
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x00139703 File Offset: 0x00138703
		public DerBmpString(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x00139720 File Offset: 0x00138720
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x00139728 File Offset: 0x00138728
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerBmpString derBmpString = asn1Object as DerBmpString;
			return derBmpString != null && this.str.Equals(derBmpString.str);
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x00139754 File Offset: 0x00138754
		internal override void Encode(DerOutputStream derOut)
		{
			char[] array = this.str.ToCharArray();
			byte[] array2 = new byte[array.Length * 2];
			for (int num = 0; num != array.Length; num++)
			{
				array2[2 * num] = (byte)(array[num] >> 8);
				array2[2 * num + 1] = (byte)array[num];
			}
			derOut.WriteEncoded(30, array2);
		}

		// Token: 0x0400228F RID: 8847
		private readonly string str;
	}
}
