using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000266 RID: 614
	public class XmlSchemaInclude : XmlSchemaExternal
	{
		// Token: 0x06001C8B RID: 7307 RVA: 0x00083367 File Offset: 0x00082367
		public XmlSchemaInclude()
		{
			base.Compositor = Compositor.Include;
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x00083376 File Offset: 0x00082376
		// (set) Token: 0x06001C8D RID: 7309 RVA: 0x0008337E File Offset: 0x0008237E
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

		// Token: 0x06001C8E RID: 7310 RVA: 0x00083387 File Offset: 0x00082387
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x04001195 RID: 4501
		private XmlSchemaAnnotation annotation;
	}
}
