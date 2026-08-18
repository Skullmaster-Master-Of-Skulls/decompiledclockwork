using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200009C RID: 156
	public class GetBindDNResponse : LdapExtendedResponse
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00015C90 File Offset: 0x00014C90
		public virtual string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00015CA8 File Offset: 0x00014CA8
		public GetBindDNResponse(RfcLdapMessage rfcMessage) : base(rfcMessage)
		{
			if (this.ResultCode == 0)
			{
				sbyte[] value = this.Value;
				if (value == null)
				{
					throw new IOException("No returned value");
				}
				LBERDecoder lberdecoder = new LBERDecoder();
				if (lberdecoder == null)
				{
					throw new IOException("Decoding error");
				}
				Asn1OctetString asn1OctetString = (Asn1OctetString)lberdecoder.decode(value);
				if (asn1OctetString == null)
				{
					throw new IOException("Decoding error");
				}
				this.identity = asn1OctetString.stringValue();
				if (this.identity == null)
				{
					throw new IOException("Decoding error");
				}
			}
			else
			{
				this.identity = "";
			}
		}

		// Token: 0x0400034B RID: 843
		private string identity;
	}
}
