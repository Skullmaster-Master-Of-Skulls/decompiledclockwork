using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000265 RID: 613
	public class XmlSchemaImport : XmlSchemaExternal
	{
		// Token: 0x06001C85 RID: 7301 RVA: 0x0008332D File Offset: 0x0008232D
		public XmlSchemaImport()
		{
			base.Compositor = Compositor.Import;
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x0008333C File Offset: 0x0008233C
		// (set) Token: 0x06001C87 RID: 7303 RVA: 0x00083344 File Offset: 0x00082344
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

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x0008334D File Offset: 0x0008234D
		// (set) Token: 0x06001C89 RID: 7305 RVA: 0x00083355 File Offset: 0x00082355
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

		// Token: 0x06001C8A RID: 7306 RVA: 0x0008335E File Offset: 0x0008235E
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x04001193 RID: 4499
		private string ns;

		// Token: 0x04001194 RID: 4500
		private XmlSchemaAnnotation annotation;
	}
}
