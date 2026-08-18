using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x02000073 RID: 115
	public class EntryEventData : BaseEdirEventData
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x000137D0 File Offset: 0x000127D0
		public string PerpetratorDN
		{
			get
			{
				return this.strPerpetratorDN;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x000137E8 File Offset: 0x000127E8
		public string Entry
		{
			get
			{
				return this.strEntry;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00013800 File Offset: 0x00012800
		public string NewDN
		{
			get
			{
				return this.strNewDN;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00013818 File Offset: 0x00012818
		public string ClassId
		{
			get
			{
				return this.strClassId;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00013830 File Offset: 0x00012830
		public int Verb
		{
			get
			{
				return this.nVerb;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00013848 File Offset: 0x00012848
		public int Flags
		{
			get
			{
				return this.nFlags;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00013860 File Offset: 0x00012860
		public DSETimeStamp TimeStamp
		{
			get
			{
				return this.timeStampObj;
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00013878 File Offset: 0x00012878
		public EntryEventData(EdirEventDataType eventDataType, Asn1Object message) : base(eventDataType, message)
		{
			int[] len = new int[1];
			this.strPerpetratorDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.strEntry = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.strClassId = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.timeStampObj = new DSETimeStamp((Asn1Sequence)this.decoder.decode(this.decodedData, len));
			this.nVerb = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.nFlags = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.strNewDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00013988 File Offset: 0x00012988
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EntryEventData[");
			stringBuilder.AppendFormat("(Entry={0})", this.strEntry);
			stringBuilder.AppendFormat("(Prepetrator={0})", this.strPerpetratorDN);
			stringBuilder.AppendFormat("(ClassId={0})", this.strClassId);
			stringBuilder.AppendFormat("(Verb={0})", this.nVerb);
			stringBuilder.AppendFormat("(Flags={0})", this.nFlags);
			stringBuilder.AppendFormat("(NewDN={0})", this.strNewDN);
			stringBuilder.AppendFormat("(TimeStamp={0})", this.timeStampObj);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040001F0 RID: 496
		protected string strPerpetratorDN;

		// Token: 0x040001F1 RID: 497
		protected string strEntry;

		// Token: 0x040001F2 RID: 498
		protected string strNewDN;

		// Token: 0x040001F3 RID: 499
		protected string strClassId;

		// Token: 0x040001F4 RID: 500
		protected int nVerb;

		// Token: 0x040001F5 RID: 501
		protected int nFlags;

		// Token: 0x040001F6 RID: 502
		protected DSETimeStamp timeStampObj;
	}
}
