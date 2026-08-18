using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200025E RID: 606
	public class XmlSchemaGroup : XmlSchemaAnnotated
	{
		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x0008312C File Offset: 0x0008212C
		// (set) Token: 0x06001C5B RID: 7259 RVA: 0x00083134 File Offset: 0x00082134
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x0008313D File Offset: 0x0008213D
		// (set) Token: 0x06001C5D RID: 7261 RVA: 0x00083145 File Offset: 0x00082145
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		public XmlSchemaGroupBase Particle
		{
			get
			{
				return this.particle;
			}
			set
			{
				this.particle = value;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0008314E File Offset: 0x0008214E
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00083156 File Offset: 0x00082156
		// (set) Token: 0x06001C60 RID: 7264 RVA: 0x0008315E File Offset: 0x0008215E
		[XmlIgnore]
		internal XmlSchemaParticle CanonicalParticle
		{
			get
			{
				return this.canonicalParticle;
			}
			set
			{
				this.canonicalParticle = value;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00083167 File Offset: 0x00082167
		// (set) Token: 0x06001C62 RID: 7266 RVA: 0x0008316F File Offset: 0x0008216F
		[XmlIgnore]
		internal XmlSchemaGroup Redefined
		{
			get
			{
				return this.redefined;
			}
			set
			{
				this.redefined = value;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x00083178 File Offset: 0x00082178
		// (set) Token: 0x06001C64 RID: 7268 RVA: 0x00083180 File Offset: 0x00082180
		[XmlIgnore]
		internal int SelfReferenceCount
		{
			get
			{
				return this.selfReferenceCount;
			}
			set
			{
				this.selfReferenceCount = value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x00083189 File Offset: 0x00082189
		// (set) Token: 0x06001C66 RID: 7270 RVA: 0x00083191 File Offset: 0x00082191
		[XmlIgnore]
		internal override string NameAttribute
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x0008319A File Offset: 0x0008219A
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qname = value;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x000831A4 File Offset: 0x000821A4
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaGroup xmlSchemaGroup = (XmlSchemaGroup)base.MemberwiseClone();
			if (XmlSchemaComplexType.HasParticleRef(this.particle))
			{
				xmlSchemaGroup.particle = (XmlSchemaComplexType.CloneParticle(this.particle) as XmlSchemaGroupBase);
			}
			xmlSchemaGroup.canonicalParticle = XmlSchemaParticle.Empty;
			return xmlSchemaGroup;
		}

		// Token: 0x04001183 RID: 4483
		private string name;

		// Token: 0x04001184 RID: 4484
		private XmlSchemaGroupBase particle;

		// Token: 0x04001185 RID: 4485
		private XmlSchemaParticle canonicalParticle;

		// Token: 0x04001186 RID: 4486
		private XmlQualifiedName qname = XmlQualifiedName.Empty;

		// Token: 0x04001187 RID: 4487
		private XmlSchemaGroup redefined;

		// Token: 0x04001188 RID: 4488
		private int selfReferenceCount;
	}
}
