using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x0200025E RID: 606
	internal class SchemaInfo : IDtdInfo
	{
		// Token: 0x0600245B RID: 9307 RVA: 0x000C6680 File Offset: 0x000C4880
		internal SchemaInfo()
		{
			this.schemaType = SchemaType.None;
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x000C66E7 File Offset: 0x000C48E7
		// (set) Token: 0x0600245D RID: 9309 RVA: 0x000C66EF File Offset: 0x000C48EF
		public XmlQualifiedName DocTypeName
		{
			get
			{
				return this.docTypeName;
			}
			set
			{
				this.docTypeName = value;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x000C66F8 File Offset: 0x000C48F8
		// (set) Token: 0x0600245F RID: 9311 RVA: 0x000C6700 File Offset: 0x000C4900
		internal string InternalDtdSubset
		{
			get
			{
				return this.internalDtdSubset;
			}
			set
			{
				this.internalDtdSubset = value;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x000C6709 File Offset: 0x000C4909
		internal Dictionary<XmlQualifiedName, SchemaElementDecl> ElementDecls
		{
			get
			{
				return this.elementDecls;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x000C6711 File Offset: 0x000C4911
		internal Dictionary<XmlQualifiedName, SchemaElementDecl> UndeclaredElementDecls
		{
			get
			{
				return this.undeclaredElementDecls;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x000C6719 File Offset: 0x000C4919
		internal Dictionary<XmlQualifiedName, SchemaEntity> GeneralEntities
		{
			get
			{
				if (this.generalEntities == null)
				{
					this.generalEntities = new Dictionary<XmlQualifiedName, SchemaEntity>();
				}
				return this.generalEntities;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x000C6734 File Offset: 0x000C4934
		internal Dictionary<XmlQualifiedName, SchemaEntity> ParameterEntities
		{
			get
			{
				if (this.parameterEntities == null)
				{
					this.parameterEntities = new Dictionary<XmlQualifiedName, SchemaEntity>();
				}
				return this.parameterEntities;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x000C674F File Offset: 0x000C494F
		// (set) Token: 0x06002465 RID: 9317 RVA: 0x000C6757 File Offset: 0x000C4957
		internal SchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x000C6760 File Offset: 0x000C4960
		internal Dictionary<string, bool> TargetNamespaces
		{
			get
			{
				return this.targetNamespaces;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x000C6768 File Offset: 0x000C4968
		internal Dictionary<XmlQualifiedName, SchemaElementDecl> ElementDeclsByType
		{
			get
			{
				return this.elementDeclsByType;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x000C6770 File Offset: 0x000C4970
		internal Dictionary<XmlQualifiedName, SchemaAttDef> AttributeDecls
		{
			get
			{
				return this.attributeDecls;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x000C6778 File Offset: 0x000C4978
		internal Dictionary<string, SchemaNotation> Notations
		{
			get
			{
				if (this.notations == null)
				{
					this.notations = new Dictionary<string, SchemaNotation>();
				}
				return this.notations;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x000C6793 File Offset: 0x000C4993
		// (set) Token: 0x0600246B RID: 9323 RVA: 0x000C679B File Offset: 0x000C499B
		internal int ErrorCount
		{
			get
			{
				return this.errorCount;
			}
			set
			{
				this.errorCount = value;
			}
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000C67A4 File Offset: 0x000C49A4
		internal SchemaElementDecl GetElementDecl(XmlQualifiedName qname)
		{
			SchemaElementDecl result;
			if (this.elementDecls.TryGetValue(qname, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000C67C4 File Offset: 0x000C49C4
		internal SchemaElementDecl GetTypeDecl(XmlQualifiedName qname)
		{
			SchemaElementDecl result;
			if (this.elementDeclsByType.TryGetValue(qname, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000C67E4 File Offset: 0x000C49E4
		internal XmlSchemaElement GetElement(XmlQualifiedName qname)
		{
			SchemaElementDecl elementDecl = this.GetElementDecl(qname);
			if (elementDecl != null)
			{
				return elementDecl.SchemaElement;
			}
			return null;
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000C6804 File Offset: 0x000C4A04
		internal XmlSchemaAttribute GetAttribute(XmlQualifiedName qname)
		{
			SchemaAttDef schemaAttDef = this.attributeDecls[qname];
			if (schemaAttDef != null)
			{
				return schemaAttDef.SchemaAttribute;
			}
			return null;
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000C682C File Offset: 0x000C4A2C
		internal XmlSchemaElement GetType(XmlQualifiedName qname)
		{
			SchemaElementDecl elementDecl = this.GetElementDecl(qname);
			if (elementDecl != null)
			{
				return elementDecl.SchemaElement;
			}
			return null;
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000C684C File Offset: 0x000C4A4C
		internal bool HasSchema(string ns)
		{
			return this.targetNamespaces.ContainsKey(ns);
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000C685A File Offset: 0x000C4A5A
		internal bool Contains(string ns)
		{
			return this.targetNamespaces.ContainsKey(ns);
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000C6868 File Offset: 0x000C4A68
		internal SchemaAttDef GetAttributeXdr(SchemaElementDecl ed, XmlQualifiedName qname)
		{
			SchemaAttDef schemaAttDef = null;
			if (ed != null)
			{
				schemaAttDef = ed.GetAttDef(qname);
				if (schemaAttDef == null)
				{
					if (!ed.ContentValidator.IsOpen || qname.Namespace.Length == 0)
					{
						throw new XmlSchemaException("Sch_UndeclaredAttribute", qname.ToString());
					}
					if (!this.attributeDecls.TryGetValue(qname, out schemaAttDef) && this.targetNamespaces.ContainsKey(qname.Namespace))
					{
						throw new XmlSchemaException("Sch_UndeclaredAttribute", qname.ToString());
					}
				}
			}
			return schemaAttDef;
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000C68E8 File Offset: 0x000C4AE8
		internal SchemaAttDef GetAttributeXsd(SchemaElementDecl ed, XmlQualifiedName qname, XmlSchemaObject partialValidationType, out AttributeMatchState attributeMatchState)
		{
			SchemaAttDef schemaAttDef = null;
			attributeMatchState = AttributeMatchState.UndeclaredAttribute;
			if (ed != null)
			{
				schemaAttDef = ed.GetAttDef(qname);
				if (schemaAttDef != null)
				{
					attributeMatchState = AttributeMatchState.AttributeFound;
					return schemaAttDef;
				}
				XmlSchemaAnyAttribute anyAttribute = ed.AnyAttribute;
				if (anyAttribute != null)
				{
					if (!anyAttribute.NamespaceList.Allows(qname))
					{
						attributeMatchState = AttributeMatchState.ProhibitedAnyAttribute;
					}
					else if (anyAttribute.ProcessContentsCorrect != XmlSchemaContentProcessing.Skip)
					{
						if (this.attributeDecls.TryGetValue(qname, out schemaAttDef))
						{
							if (schemaAttDef.Datatype.TypeCode == XmlTypeCode.Id)
							{
								attributeMatchState = AttributeMatchState.AnyIdAttributeFound;
							}
							else
							{
								attributeMatchState = AttributeMatchState.AttributeFound;
							}
						}
						else if (anyAttribute.ProcessContentsCorrect == XmlSchemaContentProcessing.Lax)
						{
							attributeMatchState = AttributeMatchState.AnyAttributeLax;
						}
					}
					else
					{
						attributeMatchState = AttributeMatchState.AnyAttributeSkip;
					}
				}
				else if (ed.ProhibitedAttributes.ContainsKey(qname))
				{
					attributeMatchState = AttributeMatchState.ProhibitedAttribute;
				}
			}
			else if (partialValidationType != null)
			{
				XmlSchemaAttribute xmlSchemaAttribute = partialValidationType as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					if (qname.Equals(xmlSchemaAttribute.QualifiedName))
					{
						schemaAttDef = xmlSchemaAttribute.AttDef;
						attributeMatchState = AttributeMatchState.AttributeFound;
					}
					else
					{
						attributeMatchState = AttributeMatchState.AttributeNameMismatch;
					}
				}
				else
				{
					attributeMatchState = AttributeMatchState.ValidateAttributeInvalidCall;
				}
			}
			else if (this.attributeDecls.TryGetValue(qname, out schemaAttDef))
			{
				attributeMatchState = AttributeMatchState.AttributeFound;
			}
			else
			{
				attributeMatchState = AttributeMatchState.UndeclaredElementAndAttribute;
			}
			return schemaAttDef;
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000C69E0 File Offset: 0x000C4BE0
		internal SchemaAttDef GetAttributeXsd(SchemaElementDecl ed, XmlQualifiedName qname, ref bool skip)
		{
			AttributeMatchState attributeMatchState;
			SchemaAttDef attributeXsd = this.GetAttributeXsd(ed, qname, null, out attributeMatchState);
			switch (attributeMatchState)
			{
			case AttributeMatchState.UndeclaredAttribute:
				throw new XmlSchemaException("Sch_UndeclaredAttribute", qname.ToString());
			case AttributeMatchState.AnyAttributeSkip:
				skip = true;
				break;
			case AttributeMatchState.ProhibitedAnyAttribute:
			case AttributeMatchState.ProhibitedAttribute:
				throw new XmlSchemaException("Sch_ProhibitedAttribute", qname.ToString());
			}
			return attributeXsd;
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000C6A48 File Offset: 0x000C4C48
		internal void Add(SchemaInfo sinfo, ValidationEventHandler eventhandler)
		{
			if (this.schemaType == SchemaType.None)
			{
				this.schemaType = sinfo.SchemaType;
			}
			else if (this.schemaType != sinfo.SchemaType)
			{
				if (eventhandler != null)
				{
					eventhandler(this, new ValidationEventArgs(new XmlSchemaException("Sch_MixSchemaTypes", string.Empty)));
				}
				return;
			}
			foreach (string key in sinfo.TargetNamespaces.Keys)
			{
				if (!this.targetNamespaces.ContainsKey(key))
				{
					this.targetNamespaces.Add(key, true);
				}
			}
			foreach (KeyValuePair<XmlQualifiedName, SchemaElementDecl> keyValuePair in sinfo.elementDecls)
			{
				if (!this.elementDecls.ContainsKey(keyValuePair.Key))
				{
					this.elementDecls.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			foreach (KeyValuePair<XmlQualifiedName, SchemaElementDecl> keyValuePair2 in sinfo.elementDeclsByType)
			{
				if (!this.elementDeclsByType.ContainsKey(keyValuePair2.Key))
				{
					this.elementDeclsByType.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			foreach (SchemaAttDef schemaAttDef in sinfo.AttributeDecls.Values)
			{
				if (!this.attributeDecls.ContainsKey(schemaAttDef.Name))
				{
					this.attributeDecls.Add(schemaAttDef.Name, schemaAttDef);
				}
			}
			foreach (SchemaNotation schemaNotation in sinfo.Notations.Values)
			{
				if (!this.Notations.ContainsKey(schemaNotation.Name.Name))
				{
					this.Notations.Add(schemaNotation.Name.Name, schemaNotation);
				}
			}
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000C6CA8 File Offset: 0x000C4EA8
		internal void Finish()
		{
			Dictionary<XmlQualifiedName, SchemaElementDecl> dictionary = this.elementDecls;
			for (int i = 0; i < 2; i++)
			{
				foreach (SchemaElementDecl schemaElementDecl in dictionary.Values)
				{
					if (schemaElementDecl.HasNonCDataAttribute)
					{
						this.hasNonCDataAttributes = true;
					}
					if (schemaElementDecl.DefaultAttDefs != null)
					{
						this.hasDefaultAttributes = true;
					}
				}
				dictionary = this.undeclaredElementDecls;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x000C6D2C File Offset: 0x000C4F2C
		bool IDtdInfo.HasDefaultAttributes
		{
			get
			{
				return this.hasDefaultAttributes;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x000C6D34 File Offset: 0x000C4F34
		bool IDtdInfo.HasNonCDataAttributes
		{
			get
			{
				return this.hasNonCDataAttributes;
			}
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000C6D3C File Offset: 0x000C4F3C
		IDtdAttributeListInfo IDtdInfo.LookupAttributeList(string prefix, string localName)
		{
			XmlQualifiedName key = new XmlQualifiedName(prefix, localName);
			SchemaElementDecl result;
			if (!this.elementDecls.TryGetValue(key, out result))
			{
				this.undeclaredElementDecls.TryGetValue(key, out result);
			}
			return result;
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000C6D71 File Offset: 0x000C4F71
		IEnumerable<IDtdAttributeListInfo> IDtdInfo.GetAttributeLists()
		{
			foreach (SchemaElementDecl schemaElementDecl in this.elementDecls.Values)
			{
				IDtdAttributeListInfo dtdAttributeListInfo = schemaElementDecl;
				yield return dtdAttributeListInfo;
			}
			Dictionary<XmlQualifiedName, SchemaElementDecl>.ValueCollection.Enumerator enumerator = default(Dictionary<XmlQualifiedName, SchemaElementDecl>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000C6D84 File Offset: 0x000C4F84
		IDtdEntityInfo IDtdInfo.LookupEntity(string name)
		{
			if (this.generalEntities == null)
			{
				return null;
			}
			XmlQualifiedName key = new XmlQualifiedName(name);
			SchemaEntity result;
			if (this.generalEntities.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x000C6DB5 File Offset: 0x000C4FB5
		XmlQualifiedName IDtdInfo.Name
		{
			get
			{
				return this.docTypeName;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x000C6DBD File Offset: 0x000C4FBD
		string IDtdInfo.InternalDtdSubset
		{
			get
			{
				return this.internalDtdSubset;
			}
		}

		// Token: 0x04000F3C RID: 3900
		private Dictionary<XmlQualifiedName, SchemaElementDecl> elementDecls = new Dictionary<XmlQualifiedName, SchemaElementDecl>();

		// Token: 0x04000F3D RID: 3901
		private Dictionary<XmlQualifiedName, SchemaElementDecl> undeclaredElementDecls = new Dictionary<XmlQualifiedName, SchemaElementDecl>();

		// Token: 0x04000F3E RID: 3902
		private Dictionary<XmlQualifiedName, SchemaEntity> generalEntities;

		// Token: 0x04000F3F RID: 3903
		private Dictionary<XmlQualifiedName, SchemaEntity> parameterEntities;

		// Token: 0x04000F40 RID: 3904
		private XmlQualifiedName docTypeName = XmlQualifiedName.Empty;

		// Token: 0x04000F41 RID: 3905
		private string internalDtdSubset = string.Empty;

		// Token: 0x04000F42 RID: 3906
		private bool hasNonCDataAttributes;

		// Token: 0x04000F43 RID: 3907
		private bool hasDefaultAttributes;

		// Token: 0x04000F44 RID: 3908
		private Dictionary<string, bool> targetNamespaces = new Dictionary<string, bool>();

		// Token: 0x04000F45 RID: 3909
		private Dictionary<XmlQualifiedName, SchemaAttDef> attributeDecls = new Dictionary<XmlQualifiedName, SchemaAttDef>();

		// Token: 0x04000F46 RID: 3910
		private int errorCount;

		// Token: 0x04000F47 RID: 3911
		private SchemaType schemaType;

		// Token: 0x04000F48 RID: 3912
		private Dictionary<XmlQualifiedName, SchemaElementDecl> elementDeclsByType = new Dictionary<XmlQualifiedName, SchemaElementDecl>();

		// Token: 0x04000F49 RID: 3913
		private Dictionary<string, SchemaNotation> notations;
	}
}
