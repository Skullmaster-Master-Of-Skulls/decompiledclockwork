using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AAC RID: 2732
	internal class PnrpRegisterTraceRecord : TraceRecord
	{
		// Token: 0x06006C15 RID: 27669 RVA: 0x00193B3B File Offset: 0x00191D3B
		public PnrpRegisterTraceRecord(string meshId, PnrpPeerResolver.PnrpRegistration global, IEnumerable<PnrpPeerResolver.PnrpRegistration> siteEntries, IEnumerable<PnrpPeerResolver.PnrpRegistration> linkEntries)
		{
			this.meshId = meshId;
			this.siteEntries = siteEntries;
			this.linkEntries = linkEntries;
			this.global = global;
		}

		// Token: 0x17001997 RID: 6551
		// (get) Token: 0x06006C16 RID: 27670 RVA: 0x00193B60 File Offset: 0x00191D60
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/PnrpRegistrationTraceRecord";
			}
		}

		// Token: 0x06006C17 RID: 27671 RVA: 0x00193B68 File Offset: 0x00191D68
		private void WriteEntry(XmlWriter writer, PnrpPeerResolver.PnrpRegistration entry)
		{
			if (entry == null)
			{
				return;
			}
			writer.WriteStartElement("Registration");
			writer.WriteAttributeString("CloudName", entry.CloudName);
			foreach (IPEndPoint ipendPoint in entry.Addresses)
			{
				writer.WriteElementString("Address", ipendPoint.ToString());
			}
			writer.WriteEndElement();
		}

		// Token: 0x06006C18 RID: 27672 RVA: 0x00193BC8 File Offset: 0x00191DC8
		private void WriteEntries(XmlWriter writer, IEnumerable<PnrpPeerResolver.PnrpRegistration> entries)
		{
			if (entries == null)
			{
				return;
			}
			foreach (PnrpPeerResolver.PnrpRegistration entry in entries)
			{
				this.WriteEntry(writer, entry);
			}
		}

		// Token: 0x06006C19 RID: 27673 RVA: 0x00193C18 File Offset: 0x00191E18
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteElementString("MeshId", this.meshId.ToString());
			writer.WriteStartElement("Registrations");
			this.WriteEntry(writer, this.global);
			this.WriteEntries(writer, this.siteEntries);
			this.WriteEntries(writer, this.linkEntries);
			writer.WriteEndElement();
		}

		// Token: 0x04003EB1 RID: 16049
		private string meshId;

		// Token: 0x04003EB2 RID: 16050
		private IEnumerable<PnrpPeerResolver.PnrpRegistration> siteEntries;

		// Token: 0x04003EB3 RID: 16051
		private IEnumerable<PnrpPeerResolver.PnrpRegistration> linkEntries;

		// Token: 0x04003EB4 RID: 16052
		private PnrpPeerResolver.PnrpRegistration global;
	}
}
