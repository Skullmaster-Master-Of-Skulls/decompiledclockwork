using System;
using System.Collections;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x02000070 RID: 112
	public class DebugEventData : BaseEdirEventData
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0001316C File Offset: 0x0001216C
		public int DSTime
		{
			get
			{
				return this.ds_time;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00013184 File Offset: 0x00012184
		public int MilliSeconds
		{
			get
			{
				return this.milli_seconds;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0001319C File Offset: 0x0001219C
		public string PerpetratorDN
		{
			get
			{
				return this.strPerpetratorDN;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000131B4 File Offset: 0x000121B4
		public string FormatString
		{
			get
			{
				return this.strFormatString;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000131CC File Offset: 0x000121CC
		public int Verb
		{
			get
			{
				return this.nVerb;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x000131E4 File Offset: 0x000121E4
		public int ParameterCount
		{
			get
			{
				return this.parameter_count;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000131FC File Offset: 0x000121FC
		public ArrayList Parameters
		{
			get
			{
				return this.parameter_collection;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00013214 File Offset: 0x00012214
		public DebugEventData(EdirEventDataType eventDataType, Asn1Object message) : base(eventDataType, message)
		{
			int[] len = new int[1];
			this.ds_time = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.milli_seconds = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.strPerpetratorDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.strFormatString = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.nVerb = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.parameter_count = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.parameter_collection = new ArrayList();
			if (this.parameter_count > 0)
			{
				Asn1Sequence asn1Sequence = (Asn1Sequence)this.decoder.decode(this.decodedData, len);
				for (int i = 0; i < this.parameter_count; i++)
				{
					this.parameter_collection.Add(new DebugParameter((Asn1Tagged)asn1Sequence.get_Renamed(i)));
				}
			}
			base.DataInitDone();
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001335C File Offset: 0x0001235C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DebugEventData");
			stringBuilder.AppendFormat("(Millseconds={0})", this.milli_seconds);
			stringBuilder.AppendFormat("(DSTime={0})", this.ds_time);
			stringBuilder.AppendFormat("(PerpetratorDN={0})", this.strPerpetratorDN);
			stringBuilder.AppendFormat("(Verb={0})", this.nVerb);
			stringBuilder.AppendFormat("(ParameterCount={0})", this.parameter_count);
			for (int i = 0; i < this.parameter_count; i++)
			{
				stringBuilder.AppendFormat("(Parameter[{0}]={1})", i, this.parameter_collection[i]);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040001E4 RID: 484
		protected int ds_time;

		// Token: 0x040001E5 RID: 485
		protected int milli_seconds;

		// Token: 0x040001E6 RID: 486
		protected string strPerpetratorDN;

		// Token: 0x040001E7 RID: 487
		protected string strFormatString;

		// Token: 0x040001E8 RID: 488
		protected int nVerb;

		// Token: 0x040001E9 RID: 489
		protected int parameter_count;

		// Token: 0x040001EA RID: 490
		protected ArrayList parameter_collection;
	}
}
