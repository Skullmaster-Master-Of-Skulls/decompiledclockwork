using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x02000071 RID: 113
	public class DebugParameter
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00013430 File Offset: 0x00012430
		public DebugParameterType DebugType
		{
			get
			{
				return this.debug_type;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00013448 File Offset: 0x00012448
		public object Data
		{
			get
			{
				return this.objData;
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00013460 File Offset: 0x00012460
		public DebugParameter(Asn1Tagged dseObject)
		{
			switch (dseObject.getIdentifier().Tag)
			{
			case 1:
			case 4:
				this.objData = this.getTaggedIntValue(dseObject);
				break;
			case 2:
				this.objData = ((Asn1OctetString)dseObject.taggedValue()).stringValue();
				break;
			case 3:
				this.objData = ((Asn1OctetString)dseObject.taggedValue()).byteValue();
				break;
			case 5:
				this.objData = new ReferralAddress(this.getTaggedSequence(dseObject));
				break;
			case 6:
				this.objData = new DSETimeStamp(this.getTaggedSequence(dseObject));
				break;
			case 7:
			{
				ArrayList arrayList = new ArrayList();
				Asn1Sequence taggedSequence = this.getTaggedSequence(dseObject);
				int num = ((Asn1Integer)taggedSequence.get_Renamed(0)).intValue();
				if (num > 0)
				{
					Asn1Sequence asn1Sequence = (Asn1Sequence)taggedSequence.get_Renamed(1);
					for (int i = 0; i < num; i++)
					{
						arrayList.Add(new DSETimeStamp((Asn1Sequence)asn1Sequence.get_Renamed(i)));
					}
				}
				this.objData = arrayList;
				break;
			}
			default:
				throw new IOException("Unknown Tag in DebugParameter..");
			}
			this.debug_type = (DebugParameterType)dseObject.getIdentifier().Tag;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000135A4 File Offset: 0x000125A4
		protected int getTaggedIntValue(Asn1Tagged tagVal)
		{
			Asn1Object asn1Object = tagVal.taggedValue();
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)asn1Object).byteValue());
			MemoryStream in_Renamed = new MemoryStream(array);
			LBERDecoder lberdecoder = new LBERDecoder();
			return (int)lberdecoder.decodeNumeric(in_Renamed, array.Length);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000135EC File Offset: 0x000125EC
		protected Asn1Sequence getTaggedSequence(Asn1Tagged tagVal)
		{
			Asn1Object asn1Object = tagVal.taggedValue();
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)asn1Object).byteValue());
			MemoryStream in_Renamed = new MemoryStream(array);
			LBERDecoder dec = new LBERDecoder();
			return new Asn1Sequence(dec, in_Renamed, array.Length);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00013630 File Offset: 0x00012630
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DebugParameter");
			if (Enum.IsDefined(this.debug_type.GetType(), this.debug_type))
			{
				stringBuilder.AppendFormat("(type={0},", this.debug_type);
				stringBuilder.AppendFormat("value={0})", this.objData);
			}
			else
			{
				stringBuilder.Append("(type=Unknown)");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040001EB RID: 491
		protected DebugParameterType debug_type;

		// Token: 0x040001EC RID: 492
		protected object objData;
	}
}
