using System;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x02000074 RID: 116
	public class GeneralDSEventData : BaseEdirEventData
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00013A48 File Offset: 0x00012A48
		public int DSTime
		{
			get
			{
				return this.ds_time;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00013A60 File Offset: 0x00012A60
		public int MilliSeconds
		{
			get
			{
				return this.milli_seconds;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00013A78 File Offset: 0x00012A78
		public int Verb
		{
			get
			{
				return this.nVerb;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00013A90 File Offset: 0x00012A90
		public int CurrentProcess
		{
			get
			{
				return this.current_process;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00013AA8 File Offset: 0x00012AA8
		public string PerpetratorDN
		{
			get
			{
				return this.strPerpetratorDN;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00013AC0 File Offset: 0x00012AC0
		public int[] IntegerValues
		{
			get
			{
				return this.integer_values;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00013AD8 File Offset: 0x00012AD8
		public string[] StringValues
		{
			get
			{
				return this.string_values;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00013AF0 File Offset: 0x00012AF0
		public GeneralDSEventData(EdirEventDataType eventDataType, Asn1Object message) : base(eventDataType, message)
		{
			int[] len = new int[1];
			this.ds_time = this.getTaggedIntValue((Asn1Tagged)this.decoder.decode(this.decodedData, len), GeneralEventField.EVT_TAG_GEN_DSTIME);
			this.milli_seconds = this.getTaggedIntValue((Asn1Tagged)this.decoder.decode(this.decodedData, len), GeneralEventField.EVT_TAG_GEN_MILLISEC);
			this.nVerb = this.getTaggedIntValue((Asn1Tagged)this.decoder.decode(this.decodedData, len), GeneralEventField.EVT_TAG_GEN_VERB);
			this.current_process = this.getTaggedIntValue((Asn1Tagged)this.decoder.decode(this.decodedData, len), GeneralEventField.EVT_TAG_GEN_CURRPROC);
			this.strPerpetratorDN = this.getTaggedStringValue((Asn1Tagged)this.decoder.decode(this.decodedData, len), GeneralEventField.EVT_TAG_GEN_PERP);
			Asn1Tagged asn1Tagged = (Asn1Tagged)this.decoder.decode(this.decodedData, len);
			if (asn1Tagged.getIdentifier().Tag == 6)
			{
				Asn1Sequence taggedSequence = this.getTaggedSequence(asn1Tagged, GeneralEventField.EVT_TAG_GEN_INTEGERS);
				Asn1Object[] array = taggedSequence.toArray();
				this.integer_values = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.integer_values[i] = ((Asn1Integer)array[i]).intValue();
				}
				asn1Tagged = (Asn1Tagged)this.decoder.decode(this.decodedData, len);
			}
			else
			{
				this.integer_values = null;
			}
			if (asn1Tagged.getIdentifier().Tag == 7 && asn1Tagged.getIdentifier().Constructed)
			{
				Asn1Sequence taggedSequence2 = this.getTaggedSequence(asn1Tagged, GeneralEventField.EVT_TAG_GEN_STRINGS);
				Asn1Object[] array2 = taggedSequence2.toArray();
				this.string_values = new string[array2.Length];
				for (int j = 0; j < array2.Length; j++)
				{
					this.string_values[j] = ((Asn1OctetString)array2[j]).stringValue();
				}
			}
			else
			{
				this.string_values = null;
			}
			base.DataInitDone();
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00013CC8 File Offset: 0x00012CC8
		protected int getTaggedIntValue(Asn1Tagged tagvalue, GeneralEventField tagid)
		{
			Asn1Object asn1Object = tagvalue.taggedValue();
			if (tagid != (GeneralEventField)tagvalue.getIdentifier().Tag)
			{
				throw new IOException("Unknown Tagged Data");
			}
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)asn1Object).byteValue());
			MemoryStream in_Renamed = new MemoryStream(array);
			LBERDecoder lberdecoder = new LBERDecoder();
			int len = array.Length;
			return (int)lberdecoder.decodeNumeric(in_Renamed, len);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00013D30 File Offset: 0x00012D30
		protected string getTaggedStringValue(Asn1Tagged tagvalue, GeneralEventField tagid)
		{
			Asn1Object asn1Object = tagvalue.taggedValue();
			if (tagid != (GeneralEventField)tagvalue.getIdentifier().Tag)
			{
				throw new IOException("Unknown Tagged Data");
			}
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)asn1Object).byteValue());
			MemoryStream in_Renamed = new MemoryStream(array);
			LBERDecoder lberdecoder = new LBERDecoder();
			int len = array.Length;
			return (string)lberdecoder.decodeCharacterString(in_Renamed, len);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00013D94 File Offset: 0x00012D94
		protected Asn1Sequence getTaggedSequence(Asn1Tagged tagvalue, GeneralEventField tagid)
		{
			Asn1Object asn1Object = tagvalue.taggedValue();
			if (tagid != (GeneralEventField)tagvalue.getIdentifier().Tag)
			{
				throw new IOException("Unknown Tagged Data");
			}
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)asn1Object).byteValue());
			MemoryStream in_Renamed = new MemoryStream(array);
			LBERDecoder dec = new LBERDecoder();
			int len = array.Length;
			return new Asn1Sequence(dec, in_Renamed, len);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00013DF4 File Offset: 0x00012DF4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[GeneralDSEventData");
			stringBuilder.AppendFormat("(DSTime={0})", this.ds_time);
			stringBuilder.AppendFormat("(MilliSeconds={0})", this.milli_seconds);
			stringBuilder.AppendFormat("(verb={0})", this.nVerb);
			stringBuilder.AppendFormat("(currentProcess={0})", this.current_process);
			stringBuilder.AppendFormat("(PerpetartorDN={0})", this.strPerpetratorDN);
			stringBuilder.AppendFormat("(Integer Values={0})", this.integer_values);
			stringBuilder.AppendFormat("(String Values={0})", this.string_values);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040001F7 RID: 503
		protected int ds_time;

		// Token: 0x040001F8 RID: 504
		protected int milli_seconds;

		// Token: 0x040001F9 RID: 505
		protected int nVerb;

		// Token: 0x040001FA RID: 506
		protected int current_process;

		// Token: 0x040001FB RID: 507
		protected string strPerpetratorDN;

		// Token: 0x040001FC RID: 508
		protected int[] integer_values;

		// Token: 0x040001FD RID: 509
		protected string[] string_values;
	}
}
