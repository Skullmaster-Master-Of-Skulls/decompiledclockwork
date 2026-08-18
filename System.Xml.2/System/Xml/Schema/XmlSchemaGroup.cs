using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000296 RID: 662
	public class XmlSchemaGroup : XmlSchemaAnnotated
	{
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060026E1 RID: 9953 RVA: 0x000CF254 File Offset: 0x000CD454
		// (set) Token: 0x060026E2 RID: 9954 RVA: 0x000CF25C File Offset: 0x000CD45C
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

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060026E3 RID: 9955 RVA: 0x000CF265 File Offset: 0x000CD465
		// (set) Token: 0x060026E4 RID: 9956 RVA: 0x000CF26D File Offset: 0x000CD46D
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

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x000CF276 File Offset: 0x000CD476
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x000CF27E File Offset: 0x000CD47E
		// (set) Token: 0x060026E7 RID: 9959 RVA: 0x000CF286 File Offset: 0x000CD486
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

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x000CF28F File Offset: 0x000CD48F
		// (set) Token: 0x060026E9 RID: 9961 RVA: 0x000CF297 File Offset: 0x000CD497
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

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060026EA RID: 9962 RVA: 0x000CF2A0 File Offset: 0x000CD4A0
		// (set) Token: 0x060026EB RID: 9963 RVA: 0x000CF2A8 File Offset: 0x000CD4A8
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

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x000CF2B1 File Offset: 0x000CD4B1
		// (set) Token: 0x060026ED RID: 9965 RVA: 0x000CF2B9 File Offset: 0x000CD4B9
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

		// Token: 0x060026EE RID: 9966 RVA: 0x000CF2C2 File Offset: 0x000CD4C2
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qname = value;
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000CF2CB File Offset: 0x000CD4CB
		internal override XmlSchemaObject Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x000CF2D4 File Offset: 0x000CD4D4
		internal XmlSchemaObject Clone(XmlSchema parentSchema)
		{
			XmlSchemaGroup xmlSchemaGroup = (XmlSchemaGroup)base.MemberwiseClone();
			if (XmlSchemaComplexType.HasParticleRef(this.particle, parentSchema))
			{
				xmlSchemaGroup.particle = (XmlSchemaComplexType.CloneParticle(this.particle, parentSchema) as XmlSchemaGroupBase);
			}
			xmlSchemaGroup.canonicalParticle = XmlSchemaParticle.Empty;
			return xmlSchemaGroup;
		}

		// Token: 0x040010FF RID: 4351
		private string name;

		// Token: 0x04001100 RID: 4352
		private XmlSchemaGroupBase particle;

		// Token: 0x04001101 RID: 4353
		private XmlSchemaParticle canonicalParticle;

		// Token: 0x04001102 RID: 4354
		private XmlQualifiedName qname = XmlQualifiedName.Empty;

		// Token: 0x04001103 RID: 4355
		private XmlSchemaGroup redefined;

		// Token: 0x04001104 RID: 4356
		private int selfReferenceCount;
	}
}
