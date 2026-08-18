using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200029E RID: 670
	public class XmlSchemaImport : XmlSchemaExternal
	{
		// Token: 0x06002710 RID: 10000 RVA: 0x000CF467 File Offset: 0x000CD667
		public XmlSchemaImport()
		{
			base.Compositor = Compositor.Import;
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x000CF476 File Offset: 0x000CD676
		// (set) Token: 0x06002712 RID: 10002 RVA: 0x000CF47E File Offset: 0x000CD67E
		[XmlAttribute("namespace", DataType = "anyURI")]
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002713 RID: 10003 RVA: 0x000CF487 File Offset: 0x000CD687
		// (set) Token: 0x06002714 RID: 10004 RVA: 0x000CF48F File Offset: 0x000CD68F
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		public XmlSchemaAnnotation Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x000CF498 File Offset: 0x000CD698
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x0400110F RID: 4367
		private string ns;

		// Token: 0x04001110 RID: 4368
		private XmlSchemaAnnotation annotation;
	}
}
