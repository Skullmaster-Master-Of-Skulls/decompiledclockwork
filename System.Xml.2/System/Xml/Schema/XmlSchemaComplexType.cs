using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200027B RID: 635
	public class XmlSchemaComplexType : XmlSchemaType
	{
		// Token: 0x06002618 RID: 9752 RVA: 0x000CDA90 File Offset: 0x000CBC90
		static XmlSchemaComplexType()
		{
			XmlSchemaComplexType.untypedAnyType.SetQualifiedName(new XmlQualifiedName("untypedAny", "http://www.w3.org/2003/11/xpath-datatypes"));
			XmlSchemaComplexType.untypedAnyType.IsMixed = true;
			XmlSchemaComplexType.untypedAnyType.SetContentTypeParticle(XmlSchemaComplexType.anyTypeLax.ContentTypeParticle);
			XmlSchemaComplexType.untypedAnyType.SetContentType(XmlSchemaContentType.Mixed);
			XmlSchemaComplexType.untypedAnyType.ElementDecl = SchemaElementDecl.CreateAnyTypeElementDecl();
			XmlSchemaComplexType.untypedAnyType.ElementDecl.SchemaType = XmlSchemaComplexType.untypedAnyType;
			XmlSchemaComplexType.untypedAnyType.ElementDecl.ContentValidator = XmlSchemaComplexType.AnyTypeContentValidator;
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x000CDB38 File Offset: 0x000CBD38
		private static XmlSchemaComplexType CreateAnyType(XmlSchemaContentProcessing processContents)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.SetQualifiedName(DatatypeImplementation.QnAnyType);
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.MaxOccurs = decimal.MaxValue;
			xmlSchemaAny.ProcessContents = processContents;
			xmlSchemaAny.BuildNamespaceList(null);
			xmlSchemaComplexType.SetContentTypeParticle(new XmlSchemaSequence
			{
				Items = 
				{
					xmlSchemaAny
				}
			});
			xmlSchemaComplexType.SetContentType(XmlSchemaContentType.Mixed);
			xmlSchemaComplexType.ElementDecl = SchemaElementDecl.CreateAnyTypeElementDecl();
			xmlSchemaComplexType.ElementDecl.SchemaType = xmlSchemaComplexType;
			ParticleContentValidator particleContentValidator = new ParticleContentValidator(XmlSchemaContentType.Mixed);
			particleContentValidator.Start();
			particleContentValidator.OpenGroup();
			particleContentValidator.AddNamespaceList(xmlSchemaAny.NamespaceList, xmlSchemaAny);
			particleContentValidator.AddStar();
			particleContentValidator.CloseGroup();
			ContentValidator contentValidator = particleContentValidator.Finish(true);
			xmlSchemaComplexType.ElementDecl.ContentValidator = contentValidator;
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = new XmlSchemaAnyAttribute();
			xmlSchemaAnyAttribute.ProcessContents = processContents;
			xmlSchemaAnyAttribute.BuildNamespaceList(null);
			xmlSchemaComplexType.SetAttributeWildcard(xmlSchemaAnyAttribute);
			xmlSchemaComplexType.ElementDecl.AnyAttribute = xmlSchemaAnyAttribute;
			return xmlSchemaComplexType;
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x000CDC4A File Offset: 0x000CBE4A
		[XmlIgnore]
		internal static XmlSchemaComplexType AnyType
		{
			get
			{
				return XmlSchemaComplexType.anyTypeLax;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x000CDC51 File Offset: 0x000CBE51
		[XmlIgnore]
		internal static XmlSchemaComplexType UntypedAnyType
		{
			get
			{
				return XmlSchemaComplexType.untypedAnyType;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x000CDC58 File Offset: 0x000CBE58
		[XmlIgnore]
		internal static XmlSchemaComplexType AnyTypeSkip
		{
			get
			{
				return XmlSchemaComplexType.anyTypeSkip;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x000CDC5F File Offset: 0x000CBE5F
		internal static ContentValidator AnyTypeContentValidator
		{
			get
			{
				return XmlSchemaComplexType.anyTypeLax.ElementDecl.ContentValidator;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x000CDC70 File Offset: 0x000CBE70
		// (set) Token: 0x06002620 RID: 9760 RVA: 0x000CDC7D File Offset: 0x000CBE7D
		[XmlAttribute("abstract")]
		[DefaultValue(false)]
		public bool IsAbstract
		{
			get
			{
				return (this.pvFlags & 4) > 0;
			}
			set
			{
				if (value)
				{
					this.pvFlags |= 4;
					return;
				}
				this.pvFlags = (byte)((int)this.pvFlags & -5);
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x000CDCA2 File Offset: 0x000CBEA2
		// (set) Token: 0x06002622 RID: 9762 RVA: 0x000CDCAA File Offset: 0x000CBEAA
		[XmlAttribute("block")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		public XmlSchemaDerivationMethod Block
		{
			get
			{
				return this.block;
			}
			set
			{
				this.block = value;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x000CDCB3 File Offset: 0x000CBEB3
		// (set) Token: 0x06002624 RID: 9764 RVA: 0x000CDCC0 File Offset: 0x000CBEC0
		[XmlAttribute("mixed")]
		[DefaultValue(false)]
		public override bool IsMixed
		{
			get
			{
				return (this.pvFlags & 2) > 0;
			}
			set
			{
				if (value)
				{
					this.pvFlags |= 2;
					return;
				}
				this.pvFlags = (byte)((int)this.pvFlags & -3);
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x000CDCE5 File Offset: 0x000CBEE5
		// (set) Token: 0x06002626 RID: 9766 RVA: 0x000CDCED File Offset: 0x000CBEED
		[XmlElement("simpleContent", typeof(XmlSchemaSimpleContent))]
		[XmlElement("complexContent", typeof(XmlSchemaComplexContent))]
		public XmlSchemaContentModel ContentModel
		{
			get
			{
				return this.contentModel;
			}
			set
			{
				this.contentModel = value;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x000CDCF6 File Offset: 0x000CBEF6
		// (set) Token: 0x06002628 RID: 9768 RVA: 0x000CDCFE File Offset: 0x000CBEFE
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		public XmlSchemaParticle Particle
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

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x000CDD07 File Offset: 0x000CBF07
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new XmlSchemaObjectCollection();
				}
				return this.attributes;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x000CDD22 File Offset: 0x000CBF22
		// (set) Token: 0x0600262B RID: 9771 RVA: 0x000CDD2A File Offset: 0x000CBF2A
		[XmlElement("anyAttribute")]
		public XmlSchemaAnyAttribute AnyAttribute
		{
			get
			{
				return this.anyAttribute;
			}
			set
			{
				this.anyAttribute = value;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x000CDD33 File Offset: 0x000CBF33
		[XmlIgnore]
		public XmlSchemaContentType ContentType
		{
			get
			{
				return base.SchemaContentType;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x000CDD3B File Offset: 0x000CBF3B
		[XmlIgnore]
		public XmlSchemaParticle ContentTypeParticle
		{
			get
			{
				return this.contentTypeParticle;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x000CDD43 File Offset: 0x000CBF43
		[XmlIgnore]
		public XmlSchemaDerivationMethod BlockResolved
		{
			get
			{
				return this.blockResolved;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x000CDD4B File Offset: 0x000CBF4B
		[XmlIgnore]
		public XmlSchemaObjectTable AttributeUses
		{
			get
			{
				if (this.attributeUses == null)
				{
					this.attributeUses = new XmlSchemaObjectTable();
				}
				return this.attributeUses;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x000CDD66 File Offset: 0x000CBF66
		[XmlIgnore]
		public XmlSchemaAnyAttribute AttributeWildcard
		{
			get
			{
				return this.attributeWildcard;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x000CDD6E File Offset: 0x000CBF6E
		[XmlIgnore]
		internal XmlSchemaObjectTable LocalElements
		{
			get
			{
				if (this.localElements == null)
				{
					this.localElements = new XmlSchemaObjectTable();
				}
				return this.localElements;
			}
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000CDD89 File Offset: 0x000CBF89
		internal void SetContentTypeParticle(XmlSchemaParticle value)
		{
			this.contentTypeParticle = value;
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x000CDD92 File Offset: 0x000CBF92
		internal void SetBlockResolved(XmlSchemaDerivationMethod value)
		{
			this.blockResolved = value;
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x000CDD9B File Offset: 0x000CBF9B
		internal void SetAttributeWildcard(XmlSchemaAnyAttribute value)
		{
			this.attributeWildcard = value;
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x000CDDA4 File Offset: 0x000CBFA4
		// (set) Token: 0x06002636 RID: 9782 RVA: 0x000CDDB1 File Offset: 0x000CBFB1
		internal bool HasWildCard
		{
			get
			{
				return (this.pvFlags & 1) > 0;
			}
			set
			{
				if (value)
				{
					this.pvFlags |= 1;
					return;
				}
				this.pvFlags = (byte)((int)this.pvFlags & -2);
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x000CDDD8 File Offset: 0x000CBFD8
		internal override XmlQualifiedName DerivedFrom
		{
			get
			{
				if (this.contentModel == null)
				{
					return XmlQualifiedName.Empty;
				}
				if (this.contentModel.Content is XmlSchemaComplexContentRestriction)
				{
					return ((XmlSchemaComplexContentRestriction)this.contentModel.Content).BaseTypeName;
				}
				if (this.contentModel.Content is XmlSchemaComplexContentExtension)
				{
					return ((XmlSchemaComplexContentExtension)this.contentModel.Content).BaseTypeName;
				}
				if (this.contentModel.Content is XmlSchemaSimpleContentRestriction)
				{
					return ((XmlSchemaSimpleContentRestriction)this.contentModel.Content).BaseTypeName;
				}
				if (this.contentModel.Content is XmlSchemaSimpleContentExtension)
				{
					return ((XmlSchemaSimpleContentExtension)this.contentModel.Content).BaseTypeName;
				}
				return XmlQualifiedName.Empty;
			}
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000CDE98 File Offset: 0x000CC098
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x000CDEA4 File Offset: 0x000CC0A4
		internal bool ContainsIdAttribute(bool findAll)
		{
			int num = 0;
			foreach (object obj in this.AttributeUses.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj;
				if (xmlSchemaAttribute.Use != XmlSchemaUse.Prohibited)
				{
					XmlSchemaDatatype datatype = xmlSchemaAttribute.Datatype;
					if (datatype != null && datatype.TypeCode == XmlTypeCode.Id)
					{
						num++;
						if (num > 1)
						{
							break;
						}
					}
				}
			}
			if (!findAll)
			{
				return num > 0;
			}
			return num > 1;
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x000CDF34 File Offset: 0x000CC134
		internal override XmlSchemaObject Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000CDF40 File Offset: 0x000CC140
		internal XmlSchemaObject Clone(XmlSchema parentSchema)
		{
			XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)base.MemberwiseClone();
			if (xmlSchemaComplexType.ContentModel != null)
			{
				XmlSchemaSimpleContent xmlSchemaSimpleContent = xmlSchemaComplexType.ContentModel as XmlSchemaSimpleContent;
				if (xmlSchemaSimpleContent != null)
				{
					XmlSchemaSimpleContent xmlSchemaSimpleContent2 = (XmlSchemaSimpleContent)xmlSchemaSimpleContent.Clone();
					XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = xmlSchemaSimpleContent.Content as XmlSchemaSimpleContentExtension;
					if (xmlSchemaSimpleContentExtension != null)
					{
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension2 = (XmlSchemaSimpleContentExtension)xmlSchemaSimpleContentExtension.Clone();
						xmlSchemaSimpleContentExtension2.BaseTypeName = xmlSchemaSimpleContentExtension.BaseTypeName.Clone();
						xmlSchemaSimpleContentExtension2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaSimpleContentExtension.Attributes));
						xmlSchemaSimpleContent2.Content = xmlSchemaSimpleContentExtension2;
					}
					else
					{
						XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContent.Content;
						XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction2 = (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContentRestriction.Clone();
						xmlSchemaSimpleContentRestriction2.BaseTypeName = xmlSchemaSimpleContentRestriction.BaseTypeName.Clone();
						xmlSchemaSimpleContentRestriction2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaSimpleContentRestriction.Attributes));
						xmlSchemaSimpleContent2.Content = xmlSchemaSimpleContentRestriction2;
					}
					xmlSchemaComplexType.ContentModel = xmlSchemaSimpleContent2;
				}
				else
				{
					XmlSchemaComplexContent xmlSchemaComplexContent = (XmlSchemaComplexContent)xmlSchemaComplexType.ContentModel;
					XmlSchemaComplexContent xmlSchemaComplexContent2 = (XmlSchemaComplexContent)xmlSchemaComplexContent.Clone();
					XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = xmlSchemaComplexContent.Content as XmlSchemaComplexContentExtension;
					if (xmlSchemaComplexContentExtension != null)
					{
						XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension2 = (XmlSchemaComplexContentExtension)xmlSchemaComplexContentExtension.Clone();
						xmlSchemaComplexContentExtension2.BaseTypeName = xmlSchemaComplexContentExtension.BaseTypeName.Clone();
						xmlSchemaComplexContentExtension2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaComplexContentExtension.Attributes));
						if (XmlSchemaComplexType.HasParticleRef(xmlSchemaComplexContentExtension.Particle, parentSchema))
						{
							xmlSchemaComplexContentExtension2.Particle = XmlSchemaComplexType.CloneParticle(xmlSchemaComplexContentExtension.Particle, parentSchema);
						}
						xmlSchemaComplexContent2.Content = xmlSchemaComplexContentExtension2;
					}
					else
					{
						XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = xmlSchemaComplexContent.Content as XmlSchemaComplexContentRestriction;
						XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction2 = (XmlSchemaComplexContentRestriction)xmlSchemaComplexContentRestriction.Clone();
						xmlSchemaComplexContentRestriction2.BaseTypeName = xmlSchemaComplexContentRestriction.BaseTypeName.Clone();
						xmlSchemaComplexContentRestriction2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaComplexContentRestriction.Attributes));
						if (XmlSchemaComplexType.HasParticleRef(xmlSchemaComplexContentRestriction2.Particle, parentSchema))
						{
							xmlSchemaComplexContentRestriction2.Particle = XmlSchemaComplexType.CloneParticle(xmlSchemaComplexContentRestriction2.Particle, parentSchema);
						}
						xmlSchemaComplexContent2.Content = xmlSchemaComplexContentRestriction2;
					}
					xmlSchemaComplexType.ContentModel = xmlSchemaComplexContent2;
				}
			}
			else
			{
				if (XmlSchemaComplexType.HasParticleRef(xmlSchemaComplexType.Particle, parentSchema))
				{
					xmlSchemaComplexType.Particle = XmlSchemaComplexType.CloneParticle(xmlSchemaComplexType.Particle, parentSchema);
				}
				xmlSchemaComplexType.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaComplexType.Attributes));
			}
			xmlSchemaComplexType.ClearCompiledState();
			return xmlSchemaComplexType;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000CE160 File Offset: 0x000CC360
		private void ClearCompiledState()
		{
			this.attributeUses = null;
			this.localElements = null;
			this.attributeWildcard = null;
			this.contentTypeParticle = XmlSchemaParticle.Empty;
			this.blockResolved = XmlSchemaDerivationMethod.None;
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000CE190 File Offset: 0x000CC390
		internal static XmlSchemaObjectCollection CloneAttributes(XmlSchemaObjectCollection attributes)
		{
			if (XmlSchemaComplexType.HasAttributeQNameRef(attributes))
			{
				XmlSchemaObjectCollection xmlSchemaObjectCollection = attributes.Clone();
				for (int i = 0; i < attributes.Count; i++)
				{
					XmlSchemaObject xmlSchemaObject = attributes[i];
					XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = xmlSchemaObject as XmlSchemaAttributeGroupRef;
					if (xmlSchemaAttributeGroupRef != null)
					{
						XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef2 = (XmlSchemaAttributeGroupRef)xmlSchemaAttributeGroupRef.Clone();
						xmlSchemaAttributeGroupRef2.RefName = xmlSchemaAttributeGroupRef.RefName.Clone();
						xmlSchemaObjectCollection[i] = xmlSchemaAttributeGroupRef2;
					}
					else
					{
						XmlSchemaAttribute xmlSchemaAttribute = xmlSchemaObject as XmlSchemaAttribute;
						if (!xmlSchemaAttribute.RefName.IsEmpty || !xmlSchemaAttribute.SchemaTypeName.IsEmpty)
						{
							xmlSchemaObjectCollection[i] = xmlSchemaAttribute.Clone();
						}
					}
				}
				return xmlSchemaObjectCollection;
			}
			return attributes;
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x000CE238 File Offset: 0x000CC438
		private static XmlSchemaObjectCollection CloneGroupBaseParticles(XmlSchemaObjectCollection groupBaseParticles, XmlSchema parentSchema)
		{
			XmlSchemaObjectCollection xmlSchemaObjectCollection = groupBaseParticles.Clone();
			for (int i = 0; i < groupBaseParticles.Count; i++)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)groupBaseParticles[i];
				xmlSchemaObjectCollection[i] = XmlSchemaComplexType.CloneParticle(xmlSchemaParticle, parentSchema);
			}
			return xmlSchemaObjectCollection;
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000CE27C File Offset: 0x000CC47C
		internal static XmlSchemaParticle CloneParticle(XmlSchemaParticle particle, XmlSchema parentSchema)
		{
			XmlSchemaGroupBase xmlSchemaGroupBase = particle as XmlSchemaGroupBase;
			if (xmlSchemaGroupBase != null)
			{
				XmlSchemaObjectCollection items = XmlSchemaComplexType.CloneGroupBaseParticles(xmlSchemaGroupBase.Items, parentSchema);
				XmlSchemaGroupBase xmlSchemaGroupBase2 = (XmlSchemaGroupBase)xmlSchemaGroupBase.Clone();
				xmlSchemaGroupBase2.SetItems(items);
				return xmlSchemaGroupBase2;
			}
			if (particle is XmlSchemaGroupRef)
			{
				XmlSchemaGroupRef xmlSchemaGroupRef = (XmlSchemaGroupRef)particle.Clone();
				xmlSchemaGroupRef.RefName = xmlSchemaGroupRef.RefName.Clone();
				return xmlSchemaGroupRef;
			}
			XmlSchemaElement xmlSchemaElement = particle as XmlSchemaElement;
			if (xmlSchemaElement != null && (!xmlSchemaElement.RefName.IsEmpty || !xmlSchemaElement.SchemaTypeName.IsEmpty || XmlSchemaComplexType.GetResolvedElementForm(parentSchema, xmlSchemaElement) == XmlSchemaForm.Qualified))
			{
				return (XmlSchemaElement)xmlSchemaElement.Clone(parentSchema);
			}
			return particle;
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000CE324 File Offset: 0x000CC524
		private static XmlSchemaForm GetResolvedElementForm(XmlSchema parentSchema, XmlSchemaElement element)
		{
			if (element.Form == XmlSchemaForm.None && parentSchema != null)
			{
				return parentSchema.ElementFormDefault;
			}
			return element.Form;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000CE340 File Offset: 0x000CC540
		internal static bool HasParticleRef(XmlSchemaParticle particle, XmlSchema parentSchema)
		{
			XmlSchemaGroupBase xmlSchemaGroupBase = particle as XmlSchemaGroupBase;
			if (xmlSchemaGroupBase != null)
			{
				bool flag = false;
				int num = 0;
				while (num < xmlSchemaGroupBase.Items.Count && !flag)
				{
					XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaGroupBase.Items[num++];
					if (xmlSchemaParticle is XmlSchemaGroupRef)
					{
						flag = true;
					}
					else
					{
						XmlSchemaElement xmlSchemaElement = xmlSchemaParticle as XmlSchemaElement;
						flag = ((xmlSchemaElement != null && (!xmlSchemaElement.RefName.IsEmpty || !xmlSchemaElement.SchemaTypeName.IsEmpty || XmlSchemaComplexType.GetResolvedElementForm(parentSchema, xmlSchemaElement) == XmlSchemaForm.Qualified)) || XmlSchemaComplexType.HasParticleRef(xmlSchemaParticle, parentSchema));
					}
				}
				return flag;
			}
			return particle is XmlSchemaGroupRef;
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000CE3DC File Offset: 0x000CC5DC
		internal static bool HasAttributeQNameRef(XmlSchemaObjectCollection attributes)
		{
			for (int i = 0; i < attributes.Count; i++)
			{
				if (attributes[i] is XmlSchemaAttributeGroupRef)
				{
					return true;
				}
				XmlSchemaAttribute xmlSchemaAttribute = attributes[i] as XmlSchemaAttribute;
				if (!xmlSchemaAttribute.RefName.IsEmpty || !xmlSchemaAttribute.SchemaTypeName.IsEmpty)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040010A0 RID: 4256
		private XmlSchemaDerivationMethod block = XmlSchemaDerivationMethod.None;

		// Token: 0x040010A1 RID: 4257
		private XmlSchemaContentModel contentModel;

		// Token: 0x040010A2 RID: 4258
		private XmlSchemaParticle particle;

		// Token: 0x040010A3 RID: 4259
		private XmlSchemaObjectCollection attributes;

		// Token: 0x040010A4 RID: 4260
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x040010A5 RID: 4261
		private XmlSchemaParticle contentTypeParticle = XmlSchemaParticle.Empty;

		// Token: 0x040010A6 RID: 4262
		private XmlSchemaDerivationMethod blockResolved;

		// Token: 0x040010A7 RID: 4263
		private XmlSchemaObjectTable localElements;

		// Token: 0x040010A8 RID: 4264
		private XmlSchemaObjectTable attributeUses;

		// Token: 0x040010A9 RID: 4265
		private XmlSchemaAnyAttribute attributeWildcard;

		// Token: 0x040010AA RID: 4266
		private static XmlSchemaComplexType anyTypeLax = XmlSchemaComplexType.CreateAnyType(XmlSchemaContentProcessing.Lax);

		// Token: 0x040010AB RID: 4267
		private static XmlSchemaComplexType anyTypeSkip = XmlSchemaComplexType.CreateAnyType(XmlSchemaContentProcessing.Skip);

		// Token: 0x040010AC RID: 4268
		private static XmlSchemaComplexType untypedAnyType = new XmlSchemaComplexType();

		// Token: 0x040010AD RID: 4269
		private byte pvFlags;

		// Token: 0x040010AE RID: 4270
		private const byte wildCardMask = 1;

		// Token: 0x040010AF RID: 4271
		private const byte isMixedMask = 2;

		// Token: 0x040010B0 RID: 4272
		private const byte isAbstractMask = 4;
	}
}
