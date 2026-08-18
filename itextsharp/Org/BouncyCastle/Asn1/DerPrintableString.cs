using System;
using System.Text;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200040D RID: 1037
	public class DerPrintableString : DerStringBase
	{
		// Token: 0x06002344 RID: 9028 RVA: 0x000D9070 File Offset: 0x000D8070
		public static DerPrintableString GetInstance(object obj)
		{
			if (obj == null || obj is DerPrintableString)
			{
				return (DerPrintableString)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerPrintableString(((Asn1OctetString)obj).GetOctets());
			}
			if (obj is Asn1TaggedObject)
			{
				return DerPrintableString.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x000D90DB File Offset: 0x000D80DB
		public static DerPrintableString GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerPrintableString.GetInstance(obj.GetObject());
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x000D90E8 File Offset: 0x000D80E8
		public DerPrintableString(byte[] str) : this(Encoding.ASCII.GetString(str, 0, str.Length), false)
		{
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000D9100 File Offset: 0x000D8100
		public DerPrintableString(string str) : this(str, false)
		{
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000D910A File Offset: 0x000D810A
		public DerPrintableString(string str, bool validate)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (validate && !DerPrintableString.IsPrintableString(str))
			{
				throw new ArgumentException("string contains illegal characters", "str");
			}
			this.str = str;
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000D9142 File Offset: 0x000D8142
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x000D914A File Offset: 0x000D814A
		public byte[] GetOctets()
		{
			return Encoding.ASCII.GetBytes(this.str);
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000D915C File Offset: 0x000D815C
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(19, this.GetOctets());
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000D916C File Offset: 0x000D816C
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerPrintableString derPrintableString = asn1Object as DerPrintableString;
			return derPrintableString != null && this.str.Equals(derPrintableString.str);
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000D9198 File Offset: 0x000D8198
		public static bool IsPrintableString(string str)
		{
			int i = 0;
			while (i < str.Length)
			{
				char c = str[i];
				if (c <= '\u007f')
				{
					if (!char.IsLetterOrDigit(c))
					{
						char c2 = c;
						switch (c2)
						{
						case ' ':
						case '\'':
						case '(':
						case ')':
						case '+':
						case ',':
						case '-':
						case '.':
						case '/':
							goto IL_92;
						case '!':
						case '"':
						case '#':
						case '$':
						case '%':
						case '&':
						case '*':
							break;
						default:
							if (c2 == ':')
							{
								goto IL_92;
							}
							switch (c2)
							{
							case '=':
							case '?':
								goto IL_92;
							}
							break;
						}
						return false;
					}
					IL_92:
					i++;
					continue;
				}
				return false;
			}
			return true;
		}

		// Token: 0x04001872 RID: 6258
		private readonly string str;
	}
}
