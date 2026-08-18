using System;
using System.Text;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200038D RID: 909
	public class DerIA5String : DerStringBase
	{
		// Token: 0x06001FA1 RID: 8097 RVA: 0x000BC8E0 File Offset: 0x000BB8E0
		public static DerIA5String GetInstance(object obj)
		{
			if (obj == null || obj is DerIA5String)
			{
				return (DerIA5String)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerIA5String(((Asn1OctetString)obj).GetOctets());
			}
			if (obj is Asn1TaggedObject)
			{
				return DerIA5String.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000BC94B File Offset: 0x000BB94B
		public static DerIA5String GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerIA5String.GetInstance(obj.GetObject());
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000BC958 File Offset: 0x000BB958
		public DerIA5String(byte[] str) : this(Encoding.ASCII.GetString(str, 0, str.Length), false)
		{
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x000BC970 File Offset: 0x000BB970
		public DerIA5String(string str) : this(str, false)
		{
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x000BC97A File Offset: 0x000BB97A
		public DerIA5String(string str, bool validate)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (validate && !DerIA5String.IsIA5String(str))
			{
				throw new ArgumentException("string contains illegal characters", "str");
			}
			this.str = str;
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000BC9B2 File Offset: 0x000BB9B2
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000BC9BA File Offset: 0x000BB9BA
		public byte[] GetOctets()
		{
			return Encoding.ASCII.GetBytes(this.str);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x000BC9CC File Offset: 0x000BB9CC
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(22, this.GetOctets());
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x000BC9DC File Offset: 0x000BB9DC
		protected override int Asn1GetHashCode()
		{
			return this.str.GetHashCode();
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x000BC9EC File Offset: 0x000BB9EC
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerIA5String derIA5String = asn1Object as DerIA5String;
			return derIA5String != null && this.str.Equals(derIA5String.str);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x000BCA18 File Offset: 0x000BBA18
		public static bool IsIA5String(string str)
		{
			foreach (char c in str)
			{
				if (c > '\u007f')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040015DD RID: 5597
		private readonly string str;
	}
}
