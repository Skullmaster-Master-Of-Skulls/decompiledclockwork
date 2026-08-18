using System;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A94 RID: 2708
	internal class WmiPutTraceRecord : TraceRecord
	{
		// Token: 0x06006B32 RID: 27442 RVA: 0x0018F7C8 File Offset: 0x0018D9C8
		internal WmiPutTraceRecord(string valueName, object originalValue, object newValue)
		{
			this.valueName = valueName;
			this.originalValue = ((originalValue == null) ? SR.GetString("ConfigNull") : originalValue.ToString());
			this.newValue = ((newValue == null) ? SR.GetString("ConfigNull") : newValue.ToString());
		}

		// Token: 0x17001972 RID: 6514
		// (get) Token: 0x06006B33 RID: 27443 RVA: 0x0018F818 File Offset: 0x0018DA18
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("WmiPut");
			}
		}

		// Token: 0x06006B34 RID: 27444 RVA: 0x0018F825 File Offset: 0x0018DA25
		internal override void WriteTo(XmlWriter xml)
		{
			xml.WriteElementString("ValueName", this.valueName);
			xml.WriteElementString("OriginalValue", this.originalValue);
			xml.WriteElementString("NewValue", this.newValue);
		}

		// Token: 0x04003CDB RID: 15579
		private string originalValue;

		// Token: 0x04003CDC RID: 15580
		private string newValue;

		// Token: 0x04003CDD RID: 15581
		private string valueName;
	}
}
