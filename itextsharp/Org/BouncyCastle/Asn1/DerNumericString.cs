using System;
using System.Text;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200038C RID: 908
	public class DerNumericString : DerStringBase
	{
		// Token: 0x06001F97 RID: 8087 RVA: 0x000BC774 File Offset: 0x000BB774
		public static DerNumericString GetInstance(object obj)
		{
			if (obj == null || obj is DerNumericString)
			{
				return (DerNumericString)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerNumericString(((Asn1OctetString)obj).GetOctets());
			}
			if (obj is Asn1TaggedObject)
			{
				return DerNumericString.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x000BC7DF File Offset: 0x000BB7DF
		public static DerNumericString GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerNumericString.GetInstance(obj.GetObject());
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x000BC7EC File Offset: 0x000BB7EC
		public DerNumericString(byte[] str) : this(Encoding.ASCII.GetString(str, 0, str.Length), false)
		{
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x000BC804 File Offset: 0x000BB804
		public DerNumericString(string str) : this(str, false)
		{
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x000BC80E File Offset: 0x000BB80E
		public DerNumericString(string str, bool validate)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (validate && !DerNumericString.IsNumericString(str))
			{
				throw new ArgumentException("string contains illegal characters", "str");
			}
			this.str = str;
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000BC846 File Offset: 0x000BB846
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000BC84E File Offset: 0x000BB84E
		public byte[] GetOctets()
		{
			return Encoding.ASCII.GetBytes(this.str);
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000BC860 File Offset: 0x000BB860
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(18, this.GetOctets());
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x000BC870 File Offset: 0x000BB870
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerNumericString derNumericString = asn1Object as DerNumericString;
			return derNumericString != null && this.str.Equals(derNumericString.str);
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000BC89C File Offset: 0x000BB89C
		public static bool IsNumericString(string str)
		{
			foreach (char c in str)
			{
				if (c > '\u007f' || (c != ' ' && !char.IsDigit(c)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040015DC RID: 5596
		private readonly string str;
	}
}
