using System;
using System.Text;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000264 RID: 612
	public class DerGeneralString : DerStringBase
	{
		// Token: 0x0600171D RID: 5917 RVA: 0x000854D0 File Offset: 0x000844D0
		public static DerGeneralString GetInstance(object obj)
		{
			if (obj == null || obj is DerGeneralString)
			{
				return (DerGeneralString)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerGeneralString(((Asn1OctetString)obj).GetOctets());
			}
			if (obj is Asn1TaggedObject)
			{
				return DerGeneralString.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0008553B File Offset: 0x0008453B
		public static DerGeneralString GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerGeneralString.GetInstance(obj.GetObject());
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00085548 File Offset: 0x00084548
		public DerGeneralString(byte[] str) : this(Encoding.ASCII.GetString(str, 0, str.Length))
		{
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0008555F File Offset: 0x0008455F
		public DerGeneralString(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0008557C File Offset: 0x0008457C
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00085584 File Offset: 0x00084584
		public byte[] GetOctets()
		{
			return Encoding.ASCII.GetBytes(this.str);
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00085596 File Offset: 0x00084596
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(27, this.GetOctets());
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x000855A8 File Offset: 0x000845A8
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerGeneralString derGeneralString = asn1Object as DerGeneralString;
			return derGeneralString != null && this.str.Equals(derGeneralString.str);
		}

		// Token: 0x04000FCF RID: 4047
		private readonly string str;
	}
}
