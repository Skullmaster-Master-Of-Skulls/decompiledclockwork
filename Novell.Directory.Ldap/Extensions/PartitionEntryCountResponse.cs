using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000AB RID: 171
	public class PartitionEntryCountResponse : LdapExtendedResponse
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00016ED4 File Offset: 0x00015ED4
		public virtual int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00016EEC File Offset: 0x00015EEC
		public PartitionEntryCountResponse(RfcLdapMessage rfcMessage) : base(rfcMessage)
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
				this.count = asn1Integer.intValue();
			}
			else
			{
				this.count = -1;
			}
		}

		// Token: 0x040003A6 RID: 934
		private int count;
	}
}
