using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200009E RID: 158
	public class GetEffectivePrivilegesResponse : LdapExtendedResponse
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00015E2C File Offset: 0x00014E2C
		public virtual int Privileges
		{
			get
			{
				return this.privileges;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00015E44 File Offset: 0x00014E44
		public GetEffectivePrivilegesResponse(RfcLdapMessage rfcMessage) : base(rfcMessage)
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
				Asn1Integer asn1Integer = (Asn1Integer)lberdecoder.decode(value);
				if (asn1Integer == null)
				{
					throw new IOException("Decoding error");
				}
				this.privileges = asn1Integer.intValue();
			}
			else
			{
				this.privileges = 0;
			}
		}

		// Token: 0x0400034C RID: 844
		private int privileges;
	}
}
