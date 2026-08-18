using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x020001A9 RID: 425
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[XmlRoot(ElementName = "service", Namespace = "http://www.w3.org/2007/app")]
	public class AtomPub10ServiceDocumentFormatter<TServiceDocument> : AtomPub10ServiceDocumentFormatter where TServiceDocument : ServiceDocument, new()
	{
		// Token: 0x06000E06 RID: 3590 RVA: 0x0003278F File Offset: 0x0003098F
		public AtomPub10ServiceDocumentFormatter() : base(typeof(TServiceDocument))
		{
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x000327A1 File Offset: 0x000309A1
		public AtomPub10ServiceDocumentFormatter(TServiceDocument documentToWrite) : base(documentToWrite)
		{
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x000327AF File Offset: 0x000309AF
		protected override ServiceDocument CreateDocumentInstance()
		{
			return Activator.CreateInstance<TServiceDocument>();
		}
	}
}
