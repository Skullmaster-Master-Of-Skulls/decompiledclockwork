using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Events.Edir.EventData;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x02000083 RID: 131
	public class EdirEventIntermediateResponse : LdapIntermediateResponse
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x000147F0 File Offset: 0x000137F0
		public EdirEventType EventType
		{
			get
			{
				return this.event_type;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00014808 File Offset: 0x00013808
		public EdirEventResultType EventResultType
		{
			get
			{
				return this.event_result_type;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00014820 File Offset: 0x00013820
		public BaseEdirEventData EventResponseDataObject
		{
			get
			{
				return this.event_response_data;
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00014838 File Offset: 0x00013838
		public EdirEventIntermediateResponse(RfcLdapMessage message) : base(message)
		{
			this.ProcessMessage(base.getValue());
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00014858 File Offset: 0x00013858
		public EdirEventIntermediateResponse(byte[] message) : base(new RfcLdapMessage(new Asn1Sequence()))
		{
			this.ProcessMessage(SupportClass.ToSByteArray(message));
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00014884 File Offset: 0x00013884
		[CLSCompliant(false)]
		protected void ProcessMessage(sbyte[] returnedValue)
		{
			LBERDecoder lberdecoder = new LBERDecoder();
			Asn1Sequence asn1Sequence = (Asn1Sequence)lberdecoder.decode(returnedValue);
			this.event_type = (EdirEventType)((Asn1Integer)asn1Sequence.get_Renamed(0)).intValue();
			this.event_result_type = (EdirEventResultType)((Asn1Integer)asn1Sequence.get_Renamed(1)).intValue();
			if (asn1Sequence.size() > 2)
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)asn1Sequence.get_Renamed(2);
				switch (asn1Tagged.getIdentifier().Tag)
				{
				case 1:
					this.event_response_data = new EntryEventData(EdirEventDataType.EDIR_TAG_ENTRY_EVENT_DATA, asn1Tagged.taggedValue());
					goto IL_19D;
				case 2:
					this.event_response_data = new ValueEventData(EdirEventDataType.EDIR_TAG_VALUE_EVENT_DATA, asn1Tagged.taggedValue());
					goto IL_19D;
				case 3:
					this.event_response_data = new GeneralDSEventData(EdirEventDataType.EDIR_TAG_GENERAL_EVENT_DATA, asn1Tagged.taggedValue());
					goto IL_19D;
				case 4:
					this.event_response_data = null;
					goto IL_19D;
				case 5:
					this.event_response_data = new BinderyObjectEventData(EdirEventDataType.EDIR_TAG_BINDERY_EVENT_DATA, asn1Tagged.taggedValue());
					goto IL_19D;
				case 6:
					this.event_response_data = new SecurityEquivalenceEventData(EdirEventDataType.EDIR_TAG_DSESEV_INFO, asn1Tagged.taggedValue());
					goto IL_19D;
				case 7:
					this.event_response_data = new ModuleStateEventData(EdirEventDataType.EDIR_TAG_MODULE_STATE_DATA, asn1Tagged.taggedValue());
					goto IL_19D;
				case 8:
					this.event_response_data = new NetworkAddressEventData(EdirEventDataType.EDIR_TAG_NETWORK_ADDRESS, asn1Tagged.taggedValue());
					goto IL_19D;
				case 9:
					this.event_response_data = new ConnectionStateEventData(EdirEventDataType.EDIR_TAG_CONNECTION_STATE, asn1Tagged.taggedValue());
					goto IL_19D;
				case 10:
					this.event_response_data = new ChangeAddressEventData(EdirEventDataType.EDIR_TAG_CHANGE_SERVER_ADDRESS, asn1Tagged.taggedValue());
					goto IL_19D;
				case 12:
					this.event_response_data = null;
					goto IL_19D;
				case 14:
					this.event_response_data = new DebugEventData(EdirEventDataType.EDIR_TAG_DEBUG_EVENT_DATA, asn1Tagged.taggedValue());
					goto IL_19D;
				}
				throw new IOException();
				IL_19D:;
			}
			else
			{
				this.event_response_data = null;
			}
		}

		// Token: 0x04000314 RID: 788
		protected EdirEventType event_type;

		// Token: 0x04000315 RID: 789
		protected EdirEventResultType event_result_type;

		// Token: 0x04000316 RID: 790
		protected BaseEdirEventData event_response_data;
	}
}
