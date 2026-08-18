using System;
using System.Text;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200014E RID: 334
	public class DerVisibleString : DerStringBase
	{
		// Token: 0x06000BF6 RID: 3062 RVA: 0x00042798 File Offset: 0x00041798
		public static DerVisibleString GetInstance(object obj)
		{
			if (obj == null || obj is DerVisibleString)
			{
				return (DerVisibleString)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerVisibleString(((Asn1OctetString)obj).GetOctets());
			}
			if (obj is Asn1TaggedObject)
			{
				return DerVisibleString.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00042803 File Offset: 0x00041803
		public static DerVisibleString GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerVisibleString.GetInstance(obj.GetObject());
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00042810 File Offset: 0x00041810
		public DerVisibleString(byte[] str) : this(Encoding.ASCII.GetString(str, 0, str.Length))
		{
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00042827 File Offset: 0x00041827
		public DerVisibleString(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00042844 File Offset: 0x00041844
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0004284C File Offset: 0x0004184C
		public byte[] GetOctets()
		{
			return Encoding.ASCII.GetBytes(this.str);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0004285E File Offset: 0x0004185E
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(26, this.GetOctets());
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00042870 File Offset: 0x00041870
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerVisibleString derVisibleString = asn1Object as DerVisibleString;
			return derVisibleString != null && this.str.Equals(derVisibleString.str);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0004289A File Offset: 0x0004189A
		protected override int Asn1GetHashCode()
		{
			return this.str.GetHashCode();
		}

		// Token: 0x04000982 RID: 2434
		private readonly string str;
	}
}
