using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020002BC RID: 700
	public class DerT61String : DerStringBase
	{
		// Token: 0x06001A5E RID: 6750 RVA: 0x0009BE90 File Offset: 0x0009AE90
		public static DerT61String GetInstance(object obj)
		{
			if (obj == null || obj is DerT61String)
			{
				return (DerT61String)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerT61String(((Asn1OctetString)obj).GetOctets());
			}
			if (obj is Asn1TaggedObject)
			{
				return DerT61String.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x0009BEFB File Offset: 0x0009AEFB
		public static DerT61String GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerT61String.GetInstance(obj.GetObject());
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0009BF08 File Offset: 0x0009AF08
		public DerT61String(byte[] str) : this(Strings.FromByteArray(str))
		{
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0009BF16 File Offset: 0x0009AF16
		public DerT61String(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0009BF33 File Offset: 0x0009AF33
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0009BF3B File Offset: 0x0009AF3B
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(20, this.GetOctets());
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0009BF4B File Offset: 0x0009AF4B
		public byte[] GetOctets()
		{
			return Strings.ToByteArray(this.str);
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0009BF58 File Offset: 0x0009AF58
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerT61String derT61String = asn1Object as DerT61String;
			return derT61String != null && this.str.Equals(derT61String.str);
		}

		// Token: 0x040011A1 RID: 4513
		private readonly string str;
	}
}
