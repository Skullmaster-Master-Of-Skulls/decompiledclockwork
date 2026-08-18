using System;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AAE RID: 2734
	internal class PnrpResolveExceptionTraceRecord : TraceRecord
	{
		// Token: 0x06006C1D RID: 27677 RVA: 0x00193CAA File Offset: 0x00191EAA
		public PnrpResolveExceptionTraceRecord(string peerName, string cloudName, Exception exception)
		{
			this.peerName = peerName;
			this.cloudName = cloudName;
			this.exception = exception;
		}

		// Token: 0x17001999 RID: 6553
		// (get) Token: 0x06006C1E RID: 27678 RVA: 0x00193CC7 File Offset: 0x00191EC7
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/PnrpResolveExceptionTraceRecord";
			}
		}

		// Token: 0x06006C1F RID: 27679 RVA: 0x00193CD0 File Offset: 0x00191ED0
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteElementString("PeerName", this.peerName);
			writer.WriteElementString("CloudName", this.cloudName);
			writer.WriteElementString("Exception", this.exception.ToString());
		}

		// Token: 0x04003EB6 RID: 16054
		private string peerName;

		// Token: 0x04003EB7 RID: 16055
		private string cloudName;

		// Token: 0x04003EB8 RID: 16056
		private Exception exception;
	}
}
