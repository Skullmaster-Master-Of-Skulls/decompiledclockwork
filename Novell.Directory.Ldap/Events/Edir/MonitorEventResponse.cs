using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x0200008D RID: 141
	public class MonitorEventResponse : LdapExtendedResponse
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00015328 File Offset: 0x00014328
		public EdirEventSpecifier[] SpecifierList
		{
			get
			{
				return this.specifier_list;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00015340 File Offset: 0x00014340
		public MonitorEventResponse(RfcLdapMessage message) : base(message)
		{
			sbyte[] value = this.Value;
			if (value == null)
			{
				throw new LdapException(LdapException.resultCodeToString(this.ResultCode), this.ResultCode, null);
			}
			LBERDecoder lberdecoder = new LBERDecoder();
			Asn1Sequence asn1Sequence = (Asn1Sequence)lberdecoder.decode(value);
			int num = ((Asn1Integer)asn1Sequence.get_Renamed(0)).intValue();
			Asn1Set asn1Set = (Asn1Set)asn1Sequence.get_Renamed(1);
			this.specifier_list = new EdirEventSpecifier[num];
			for (int i = 0; i < num; i++)
			{
				Asn1Sequence asn1Sequence2 = (Asn1Sequence)asn1Set.get_Renamed(i);
				int eventType = ((Asn1Integer)asn1Sequence2.get_Renamed(0)).intValue();
				int eventResultType = ((Asn1Enumerated)asn1Sequence2.get_Renamed(1)).intValue();
				this.specifier_list[i] = new EdirEventSpecifier((EdirEventType)eventType, (EdirEventResultType)eventResultType);
			}
		}

		// Token: 0x0400032E RID: 814
		protected EdirEventSpecifier[] specifier_list;
	}
}
