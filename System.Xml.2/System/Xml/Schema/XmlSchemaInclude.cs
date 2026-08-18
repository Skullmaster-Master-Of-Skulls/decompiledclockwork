using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200029F RID: 671
	public class XmlSchemaInclude : XmlSchemaExternal
	{
		// Token: 0x06002716 RID: 10006 RVA: 0x000CF4A1 File Offset: 0x000CD6A1
		public XmlSchemaInclude()
		{
			base.Compositor = Compositor.Include;
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x000CF4B0 File Offset: 0x000CD6B0
		// (set) Token: 0x06002718 RID: 10008 RVA: 0x000CF4B8 File Offset: 0x000CD6B8
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

		// Token: 0x06002719 RID: 10009 RVA: 0x000CF4C1 File Offset: 0x000CD6C1
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x04001111 RID: 4369
		private XmlSchemaAnnotation annotation;
	}
}
