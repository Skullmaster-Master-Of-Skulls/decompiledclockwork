using System;
using System.Text;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020000AD RID: 173
	public class DerUtf8String : DerStringBase
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x0001C48C File Offset: 0x0001B48C
		public static DerUtf8String GetInstance(object obj)
		{
			if (obj == null || obj is DerUtf8String)
			{
				return (DerUtf8String)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerUtf8String(((Asn1OctetString)obj).GetOctets());
			}
			if (obj is Asn1TaggedObject)
			{
				return DerUtf8String.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001C4F7 File Offset: 0x0001B4F7
		public static DerUtf8String GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerUtf8String.GetInstance(obj.GetObject());
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001C504 File Offset: 0x0001B504
		internal DerUtf8String(byte[] str) : this(Encoding.UTF8.GetString(str, 0, str.Length))
		{
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001C51B File Offset: 0x0001B51B
		public DerUtf8String(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001C538 File Offset: 0x0001B538
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001C540 File Offset: 0x0001B540
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerUtf8String derUtf8String = asn1Object as DerUtf8String;
			return derUtf8String != null && this.str.Equals(derUtf8String.str);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001C56A File Offset: 0x0001B56A
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(12, Encoding.UTF8.GetBytes(this.str));
		}

		// Token: 0x040002AB RID: 683
		private readonly string str;
	}
}
