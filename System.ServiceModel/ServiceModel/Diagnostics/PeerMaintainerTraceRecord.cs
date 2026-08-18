using System;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AAD RID: 2733
	internal class PeerMaintainerTraceRecord : TraceRecord
	{
		// Token: 0x06006C1A RID: 27674 RVA: 0x00193C7A File Offset: 0x00191E7A
		public PeerMaintainerTraceRecord(string activity)
		{
			this.activity = activity;
		}

		// Token: 0x17001998 RID: 6552
		// (get) Token: 0x06006C1B RID: 27675 RVA: 0x00193C89 File Offset: 0x00191E89
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/PeerMaintainerActivityTraceRecord";
			}
		}

		// Token: 0x06006C1C RID: 27676 RVA: 0x00193C90 File Offset: 0x00191E90
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteElementString("Activity", this.activity);
		}

		// Token: 0x04003EB5 RID: 16053
		private string activity;
	}
}
