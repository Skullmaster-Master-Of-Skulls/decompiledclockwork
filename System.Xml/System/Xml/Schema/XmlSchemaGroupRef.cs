using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200025F RID: 607
	public class XmlSchemaGroupRef : XmlSchemaParticle
	{
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x000831FF File Offset: 0x000821FF
		// (set) Token: 0x06001C6B RID: 7275 RVA: 0x00083207 File Offset: 0x00082207
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x00083220 File Offset: 0x00082220
		[XmlIgnore]
		public XmlSchemaGroupBase Particle
		{
			get
			{
				return this.particle;
			}
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00083228 File Offset: 0x00082228
		internal void SetParticle(XmlSchemaGroupBase value)
		{
			this.particle = value;
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x00083231 File Offset: 0x00082231
		// (set) Token: 0x06001C6F RID: 7279 RVA: 0x00083239 File Offset: 0x00082239
		[XmlIgnore]
		internal XmlSchemaGroup Redefined
		{
			get
			{
				return this.refined;
			}
			set
			{
				this.refined = value;
			}
		}

		// Token: 0x04001189 RID: 4489
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x0400118A RID: 4490
		private XmlSchemaGroupBase particle;

		// Token: 0x0400118B RID: 4491
		private XmlSchemaGroup refined;
	}
}
