using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000298 RID: 664
	public class XmlSchemaGroupRef : XmlSchemaParticle
	{
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x000CF339 File Offset: 0x000CD539
		// (set) Token: 0x060026F6 RID: 9974 RVA: 0x000CF341 File Offset: 0x000CD541
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

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x000CF35A File Offset: 0x000CD55A
		[XmlIgnore]
		public XmlSchemaGroupBase Particle
		{
			get
			{
				return this.particle;
			}
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000CF362 File Offset: 0x000CD562
		internal void SetParticle(XmlSchemaGroupBase value)
		{
			this.particle = value;
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x000CF36B File Offset: 0x000CD56B
		// (set) Token: 0x060026FA RID: 9978 RVA: 0x000CF373 File Offset: 0x000CD573
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

		// Token: 0x04001105 RID: 4357
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x04001106 RID: 4358
		private XmlSchemaGroupBase particle;

		// Token: 0x04001107 RID: 4359
		private XmlSchemaGroup refined;
	}
}
