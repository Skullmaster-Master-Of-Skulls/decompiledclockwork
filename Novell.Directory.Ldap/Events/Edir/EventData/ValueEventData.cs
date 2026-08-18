using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x02000079 RID: 121
	public class ValueEventData : BaseEdirEventData
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0001441C File Offset: 0x0001341C
		public string Attribute
		{
			get
			{
				return this.strAttribute;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00014434 File Offset: 0x00013434
		public string ClassId
		{
			get
			{
				return this.strClassId;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0001444C File Offset: 0x0001344C
		public string Data
		{
			get
			{
				return this.strData;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00014464 File Offset: 0x00013464
		public byte[] BinaryData
		{
			get
			{
				return this.binData;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0001447C File Offset: 0x0001347C
		public string Entry
		{
			get
			{
				return this.strEntry;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00014494 File Offset: 0x00013494
		public string PerpetratorDN
		{
			get
			{
				return this.strPerpetratorDN;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x000144AC File Offset: 0x000134AC
		public string Syntax
		{
			get
			{
				return this.strSyntax;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x000144C4 File Offset: 0x000134C4
		public DSETimeStamp TimeStamp
		{
			get
			{
				return this.timeStampObj;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x000144DC File Offset: 0x000134DC
		public int Verb
		{
			get
			{
				return this.nVerb;
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000144F4 File Offset: 0x000134F4
		public ValueEventData(EdirEventDataType eventDataType, Asn1Object message) : base(eventDataType, message)
		{
			int[] len = new int[1];
			this.strPerpetratorDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.strEntry = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.strAttribute = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.strSyntax = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.strClassId = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.timeStampObj = new DSETimeStamp((Asn1Sequence)this.decoder.decode(this.decodedData, len));
			Asn1OctetString asn1OctetString = (Asn1OctetString)this.decoder.decode(this.decodedData, len);
			this.strData = asn1OctetString.stringValue();
			this.binData = SupportClass.ToByteArray(asn1OctetString.byteValue());
			this.nVerb = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			base.DataInitDone();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001463C File Offset: 0x0001363C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ValueEventData");
			stringBuilder.AppendFormat("(Attribute={0})", this.strAttribute);
			stringBuilder.AppendFormat("(Classid={0})", this.strClassId);
			stringBuilder.AppendFormat("(Data={0})", this.strData);
			stringBuilder.AppendFormat("(Data={0})", this.binData);
			stringBuilder.AppendFormat("(Entry={0})", this.strEntry);
			stringBuilder.AppendFormat("(Perpetrator={0})", this.strPerpetratorDN);
			stringBuilder.AppendFormat("(Syntax={0})", this.strSyntax);
			stringBuilder.AppendFormat("(TimeStamp={0})", this.timeStampObj);
			stringBuilder.AppendFormat("(Verb={0})", this.nVerb);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400020C RID: 524
		protected string strAttribute;

		// Token: 0x0400020D RID: 525
		protected string strClassId;

		// Token: 0x0400020E RID: 526
		protected string strData;

		// Token: 0x0400020F RID: 527
		protected byte[] binData;

		// Token: 0x04000210 RID: 528
		protected string strEntry;

		// Token: 0x04000211 RID: 529
		protected string strPerpetratorDN;

		// Token: 0x04000212 RID: 530
		protected string strSyntax;

		// Token: 0x04000213 RID: 531
		protected DSETimeStamp timeStampObj;

		// Token: 0x04000214 RID: 532
		protected int nVerb;
	}
}
