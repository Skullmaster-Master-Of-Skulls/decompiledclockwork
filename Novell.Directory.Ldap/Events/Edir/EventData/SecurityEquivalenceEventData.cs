using System;
using System.Collections;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x02000078 RID: 120
	public class SecurityEquivalenceEventData : BaseEdirEventData
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00014200 File Offset: 0x00013200
		public string EntryDN
		{
			get
			{
				return this.strEntryDN;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00014218 File Offset: 0x00013218
		public int RetryCount
		{
			get
			{
				return this.retry_count;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00014230 File Offset: 0x00013230
		public string ValueDN
		{
			get
			{
				return this.strValueDN;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00014248 File Offset: 0x00013248
		public int ReferralCount
		{
			get
			{
				return this.referral_count;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00014260 File Offset: 0x00013260
		public ArrayList ReferralList
		{
			get
			{
				return this.referral_list;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00014278 File Offset: 0x00013278
		public SecurityEquivalenceEventData(EdirEventDataType eventDataType, Asn1Object message) : base(eventDataType, message)
		{
			int[] len = new int[1];
			this.strEntryDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.retry_count = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.strValueDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			Asn1Sequence asn1Sequence = (Asn1Sequence)this.decoder.decode(this.decodedData, len);
			this.referral_count = ((Asn1Integer)asn1Sequence.get_Renamed(0)).intValue();
			this.referral_list = new ArrayList();
			if (this.referral_count > 0)
			{
				Asn1Sequence asn1Sequence2 = (Asn1Sequence)asn1Sequence.get_Renamed(1);
				for (int i = 0; i < this.referral_count; i++)
				{
					this.referral_list.Add(new ReferralAddress((Asn1Sequence)asn1Sequence2.get_Renamed(i)));
				}
			}
			base.DataInitDone();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00014380 File Offset: 0x00013380
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SecurityEquivalenceEventData");
			stringBuilder.AppendFormat("(EntryDN={0})", this.strEntryDN);
			stringBuilder.AppendFormat("(RetryCount={0})", this.retry_count);
			stringBuilder.AppendFormat("(valueDN={0})", this.strValueDN);
			stringBuilder.AppendFormat("(referralCount={0})", this.referral_count);
			stringBuilder.AppendFormat("(Referral Lists={0})", this.referral_list);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000207 RID: 519
		protected string strEntryDN;

		// Token: 0x04000208 RID: 520
		protected int retry_count;

		// Token: 0x04000209 RID: 521
		protected string strValueDN;

		// Token: 0x0400020A RID: 522
		protected int referral_count;

		// Token: 0x0400020B RID: 523
		protected ArrayList referral_list;
	}
}
