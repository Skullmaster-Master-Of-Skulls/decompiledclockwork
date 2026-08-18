using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x0200006C RID: 108
	public class BaseEdirEventData
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00012B40 File Offset: 0x00011B40
		public EdirEventDataType EventDataType
		{
			get
			{
				return this.event_data_type;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00012B58 File Offset: 0x00011B58
		public BaseEdirEventData(EdirEventDataType eventDataType, Asn1Object message)
		{
			this.event_data_type = eventDataType;
			byte[] buffer = SupportClass.ToByteArray(((Asn1OctetString)message).byteValue());
			this.decodedData = new MemoryStream(buffer);
			this.decoder = new LBERDecoder();
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00012BA8 File Offset: 0x00011BA8
		protected void DataInitDone()
		{
			this.decodedData = null;
			this.decoder = null;
		}

		// Token: 0x040001D2 RID: 466
		protected MemoryStream decodedData = null;

		// Token: 0x040001D3 RID: 467
		protected LBERDecoder decoder = null;

		// Token: 0x040001D4 RID: 468
		protected EdirEventDataType event_data_type;
	}
}
