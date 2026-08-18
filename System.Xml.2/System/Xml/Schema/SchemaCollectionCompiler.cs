using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Schema
{
	// Token: 0x02000258 RID: 600
	internal sealed class SchemaCollectionCompiler : BaseProcessor
	{
		// Token: 0x0600238F RID: 9103 RVA: 0x000BEACC File Offset: 0x000BCCCC
		public SchemaCollectionCompiler(XmlNameTable nameTable, ValidationEventHandler eventHandler) : base(nameTable, null, eventHandler)
		{
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000BEAED File Offset: 0x000BCCED
		public bool Execute(XmlSchema schema, SchemaInfo schemaInfo, bool compileContentModel)
		{
			this.compileContentModel = compileContentModel;
			this.schema = schema;
			this.Prepare();
			this.Cleanup();
			this.Compile();
			if (!base.HasErrors)
			{
				this.Output(schemaInfo);
			}
			return !base.HasErrors;
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000BEB28 File Offset: 0x000BCD28
		private void Prepare()
		{
			foreach (object obj in this.schema.Elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (!xmlSchemaElement.SubstitutionGroup.IsEmpty)
				{
					XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)this.examplars[xmlSchemaElement.SubstitutionGroup];
					if (xmlSchemaSubstitutionGroup == null)
					{
						xmlSchemaSubstitutionGroup = new XmlSchemaSubstitutionGroupV1Compat();
						xmlSchemaSubstitutionGroup.Examplar = xmlSchemaElement.SubstitutionGroup;
						this.examplars.Add(xmlSchemaElement.SubstitutionGroup, xmlSchemaSubstitutionGroup);
					}
					ArrayList members = xmlSchemaSubstitutionGroup.Members;
					members.Add(xmlSchemaElement);
				}
			}
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000BEBE4 File Offset: 0x000BCDE4
		private void Cleanup()
		{
			foreach (object obj in this.schema.Groups.Values)
			{
				XmlSchemaGroup group = (XmlSchemaGroup)obj;
				SchemaCollectionCompiler.CleanupGroup(group);
			}
			foreach (object obj2 in this.schema.AttributeGroups.Values)
			{
				XmlSchemaAttributeGroup attributeGroup = (XmlSchemaAttributeGroup)obj2;
				SchemaCollectionCompiler.CleanupAttributeGroup(attributeGroup);
			}
			foreach (object obj3 in this.schema.SchemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj3;
				if (xmlSchemaType is XmlSchemaComplexType)
				{
					SchemaCollectionCompiler.CleanupComplexType((XmlSchemaComplexType)xmlSchemaType);
				}
				else
				{
					SchemaCollectionCompiler.CleanupSimpleType((XmlSchemaSimpleType)xmlSchemaType);
				}
			}
			foreach (object obj4 in this.schema.Elements.Values)
			{
				XmlSchemaElement element = (XmlSchemaElement)obj4;
				SchemaCollectionCompiler.CleanupElement(element);
			}
			foreach (object obj5 in this.schema.Attributes.Values)
			{
				XmlSchemaAttribute attribute = (XmlSchemaAttribute)obj5;
				SchemaCollectionCompiler.CleanupAttribute(attribute);
			}
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000BEDBC File Offset: 0x000BCFBC
		internal static void Cleanup(XmlSchema schema)
		{
			for (int i = 0; i < schema.Includes.Count; i++)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
				if (xmlSchemaExternal.Schema != null)
				{
					SchemaCollectionCompiler.Cleanup(xmlSchemaExternal.Schema);
				}
				XmlSchemaRedefine xmlSchemaRedefine = xmlSchemaExternal as XmlSchemaRedefine;
				if (xmlSchemaRedefine != null)
				{
					xmlSchemaRedefine.AttributeGroups.Clear();
					xmlSchemaRedefine.Groups.Clear();
					xmlSchemaRedefine.SchemaTypes.Clear();
					for (int j = 0; j < xmlSchemaRedefine.Items.Count; j++)
					{
						object obj = xmlSchemaRedefine.Items[j];
						XmlSchemaAttribute attribute;
						XmlSchemaAttributeGroup attributeGroup;
						XmlSchemaComplexType complexType;
						XmlSchemaSimpleType simpleType;
						XmlSchemaElement element;
						XmlSchemaGroup group;
						if ((attribute = (obj as XmlSchemaAttribute)) != null)
						{
							SchemaCollectionCompiler.CleanupAttribute(attribute);
						}
						else if ((attributeGroup = (obj as XmlSchemaAttributeGroup)) != null)
						{
							SchemaCollectionCompiler.CleanupAttributeGroup(attributeGroup);
						}
						else if ((complexType = (obj as XmlSchemaComplexType)) != null)
						{
							SchemaCollectionCompiler.CleanupComplexType(complexType);
						}
						else if ((simpleType = (obj as XmlSchemaSimpleType)) != null)
						{
							SchemaCollectionCompiler.CleanupSimpleType(simpleType);
						}
						else if ((element = (obj as XmlSchemaElement)) != null)
						{
							SchemaCollectionCompiler.CleanupElement(element);
						}
						else if ((group = (obj as XmlSchemaGroup)) != null)
						{
							SchemaCollectionCompiler.CleanupGroup(group);
						}
					}
				}
			}
			for (int k = 0; k < schema.Items.Count; k++)
			{
				object obj2 = schema.Items[k];
				XmlSchemaAttribute attribute2;
				XmlSchemaAttributeGroup attributeGroup2;
				XmlSchemaComplexType complexType2;
				XmlSchemaSimpleType simpleType2;
				XmlSchemaElement element2;
				XmlSchemaGroup group2;
				if ((attribute2 = (obj2 as XmlSchemaAttribute)) != null)
				{
					SchemaCollectionCompiler.CleanupAttribute(attribute2);
				}
				else if ((attributeGroup2 = (schema.Items[k] as XmlSchemaAttributeGroup)) != null)
				{
					SchemaCollectionCompiler.CleanupAttributeGroup(attributeGroup2);
				}
				else if ((complexType2 = (schema.Items[k] as XmlSchemaComplexType)) != null)
				{
					SchemaCollectionCompiler.CleanupComplexType(complexType2);
				}
				else if ((simpleType2 = (schema.Items[k] as XmlSchemaSimpleType)) != null)
				{
					SchemaCollectionCompiler.CleanupSimpleType(simpleType2);
				}
				else if ((element2 = (schema.Items[k] as XmlSchemaElement)) != null)
				{
					SchemaCollectionCompiler.CleanupElement(element2);
				}
				else if ((group2 = (schema.Items[k] as XmlSchemaGroup)) != null)
				{
					SchemaCollectionCompiler.CleanupGroup(group2);
				}
			}
			schema.Attributes.Clear();
			schema.AttributeGroups.Clear();
			schema.SchemaTypes.Clear();
			schema.Elements.Clear();
			schema.Groups.Clear();
			schema.Notations.Clear();
			schema.Ids.Clear();
			schema.IdentityConstraints.Clear();
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000BF01C File Offset: 0x000BD21C
		private void Compile()
		{
			this.schema.SchemaTypes.Insert(DatatypeImplementation.QnAnyType, XmlSchemaComplexType.AnyType);
			foreach (object obj in this.examplars.Values)
			{
				XmlSchemaSubstitutionGroupV1Compat substitutionGroup = (XmlSchemaSubstitutionGroupV1Compat)obj;
				this.CompileSubstitutionGroup(substitutionGroup);
			}
			foreach (object obj2 in this.schema.Groups.Values)
			{
				XmlSchemaGroup group = (XmlSchemaGroup)obj2;
				this.CompileGroup(group);
			}
			foreach (object obj3 in this.schema.AttributeGroups.Values)
			{
				XmlSchemaAttributeGroup attributeGroup = (XmlSchemaAttributeGroup)obj3;
				this.CompileAttributeGroup(attributeGroup);
			}
			foreach (object obj4 in this.schema.SchemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj4;
				if (xmlSchemaType is XmlSchemaComplexType)
				{
					this.CompileComplexType((XmlSchemaComplexType)xmlSchemaType);
				}
				else
				{
					this.CompileSimpleType((XmlSchemaSimpleType)xmlSchemaType);
				}
			}
			foreach (object obj5 in this.schema.Elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj5;
				if (xmlSchemaElement.ElementDecl == null)
				{
					this.CompileElement(xmlSchemaElement);
				}
			}
			foreach (object obj6 in this.schema.Attributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj6;
				if (xmlSchemaAttribute.AttDef == null)
				{
					this.CompileAttribute(xmlSchemaAttribute);
				}
			}
			using (IEnumerator enumerator7 = this.schema.IdentityConstraints.Values.GetEnumerator())
			{
				while (enumerator7.MoveNext())
				{
					object obj7 = enumerator7.Current;
					XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)obj7;
					if (xmlSchemaIdentityConstraint.CompiledConstraint == null)
					{
						this.CompileIdentityConstraint(xmlSchemaIdentityConstraint);
					}
				}
				goto IL_271;
			}
			IL_257:
			XmlSchemaComplexType complexType = (XmlSchemaComplexType)this.complexTypeStack.Pop();
			this.CompileCompexTypeElements(complexType);
			IL_271:
			if (this.complexTypeStack.Count <= 0)
			{
				foreach (object obj8 in this.schema.SchemaTypes.Values)
				{
					XmlSchemaType xmlSchemaType2 = (XmlSchemaType)obj8;
					if (xmlSchemaType2 is XmlSchemaComplexType)
					{
						this.CheckParticleDerivation((XmlSchemaComplexType)xmlSchemaType2);
					}
				}
				foreach (object obj9 in this.schema.Elements.Values)
				{
					XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)obj9;
					if (xmlSchemaElement2.ElementSchemaType is XmlSchemaComplexType && xmlSchemaElement2.SchemaTypeName == XmlQualifiedName.Empty)
					{
						this.CheckParticleDerivation((XmlSchemaComplexType)xmlSchemaElement2.ElementSchemaType);
					}
				}
				foreach (object obj10 in this.examplars.Values)
				{
					XmlSchemaSubstitutionGroup substitutionGroup2 = (XmlSchemaSubstitutionGroup)obj10;
					this.CheckSubstitutionGroup(substitutionGroup2);
				}
				this.schema.SchemaTypes.Remove(DatatypeImplementation.QnAnyType);
				return;
			}
			goto IL_257;
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000BF454 File Offset: 0x000BD654
		private void Output(SchemaInfo schemaInfo)
		{
			foreach (object obj in this.schema.Elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				schemaInfo.TargetNamespaces[xmlSchemaElement.QualifiedName.Namespace] = true;
				schemaInfo.ElementDecls.Add(xmlSchemaElement.QualifiedName, xmlSchemaElement.ElementDecl);
			}
			foreach (object obj2 in this.schema.Attributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj2;
				schemaInfo.TargetNamespaces[xmlSchemaAttribute.QualifiedName.Namespace] = true;
				schemaInfo.AttributeDecls.Add(xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute.AttDef);
			}
			foreach (object obj3 in this.schema.SchemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj3;
				schemaInfo.TargetNamespaces[xmlSchemaType.QualifiedName.Namespace] = true;
				XmlSchemaComplexType xmlSchemaComplexType = xmlSchemaType as XmlSchemaComplexType;
				if (xmlSchemaComplexType == null || (!xmlSchemaComplexType.IsAbstract && xmlSchemaType != XmlSchemaComplexType.AnyType))
				{
					schemaInfo.ElementDeclsByType.Add(xmlSchemaType.QualifiedName, xmlSchemaType.ElementDecl);
				}
			}
			foreach (object obj4 in this.schema.Notations.Values)
			{
				XmlSchemaNotation xmlSchemaNotation = (XmlSchemaNotation)obj4;
				schemaInfo.TargetNamespaces[xmlSchemaNotation.QualifiedName.Namespace] = true;
				SchemaNotation schemaNotation = new SchemaNotation(xmlSchemaNotation.QualifiedName);
				schemaNotation.SystemLiteral = xmlSchemaNotation.System;
				schemaNotation.Pubid = xmlSchemaNotation.Public;
				if (!schemaInfo.Notations.ContainsKey(schemaNotation.Name.Name))
				{
					schemaInfo.Notations.Add(schemaNotation.Name.Name, schemaNotation);
				}
			}
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000BF6C8 File Offset: 0x000BD8C8
		private static void CleanupAttribute(XmlSchemaAttribute attribute)
		{
			if (attribute.SchemaType != null)
			{
				SchemaCollectionCompiler.CleanupSimpleType(attribute.SchemaType);
			}
			attribute.AttDef = null;
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000BF6E4 File Offset: 0x000BD8E4
		private static void CleanupAttributeGroup(XmlSchemaAttributeGroup attributeGroup)
		{
			SchemaCollectionCompiler.CleanupAttributes(attributeGroup.Attributes);
			attributeGroup.AttributeUses.Clear();
			attributeGroup.AttributeWildcard = null;
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000BF704 File Offset: 0x000BD904
		private static void CleanupComplexType(XmlSchemaComplexType complexType)
		{
			if (complexType.ContentModel != null)
			{
				if (complexType.ContentModel is XmlSchemaSimpleContent)
				{
					XmlSchemaSimpleContent xmlSchemaSimpleContent = (XmlSchemaSimpleContent)complexType.ContentModel;
					if (xmlSchemaSimpleContent.Content is XmlSchemaSimpleContentExtension)
					{
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = (XmlSchemaSimpleContentExtension)xmlSchemaSimpleContent.Content;
						SchemaCollectionCompiler.CleanupAttributes(xmlSchemaSimpleContentExtension.Attributes);
					}
					else
					{
						XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContent.Content;
						SchemaCollectionCompiler.CleanupAttributes(xmlSchemaSimpleContentRestriction.Attributes);
					}
				}
				else
				{
					XmlSchemaComplexContent xmlSchemaComplexContent = (XmlSchemaComplexContent)complexType.ContentModel;
					if (xmlSchemaComplexContent.Content is XmlSchemaComplexContentExtension)
					{
						XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = (XmlSchemaComplexContentExtension)xmlSchemaComplexContent.Content;
						SchemaCollectionCompiler.CleanupParticle(xmlSchemaComplexContentExtension.Particle);
						SchemaCollectionCompiler.CleanupAttributes(xmlSchemaComplexContentExtension.Attributes);
					}
					else
					{
						XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = (XmlSchemaComplexContentRestriction)xmlSchemaComplexContent.Content;
						SchemaCollectionCompiler.CleanupParticle(xmlSchemaComplexContentRestriction.Particle);
						SchemaCollectionCompiler.CleanupAttributes(xmlSchemaComplexContentRestriction.Attributes);
					}
				}
			}
			else
			{
				SchemaCollectionCompiler.CleanupParticle(complexType.Particle);
				SchemaCollectionCompiler.CleanupAttributes(complexType.Attributes);
			}
			complexType.LocalElements.Clear();
			complexType.AttributeUses.Clear();
			complexType.SetAttributeWildcard(null);
			complexType.SetContentTypeParticle(XmlSchemaParticle.Empty);
			complexType.ElementDecl = null;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000BF823 File Offset: 0x000BDA23
		private static void CleanupSimpleType(XmlSchemaSimpleType simpleType)
		{
			simpleType.ElementDecl = null;
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000BF82C File Offset: 0x000BDA2C
		private static void CleanupElement(XmlSchemaElement element)
		{
			if (element.SchemaType != null)
			{
				XmlSchemaComplexType xmlSchemaComplexType = element.SchemaType as XmlSchemaComplexType;
				if (xmlSchemaComplexType != null)
				{
					SchemaCollectionCompiler.CleanupComplexType(xmlSchemaComplexType);
				}
				else
				{
					SchemaCollectionCompiler.CleanupSimpleType((XmlSchemaSimpleType)element.SchemaType);
				}
			}
			for (int i = 0; i < element.Constraints.Count; i++)
			{
				((XmlSchemaIdentityConstraint)element.Constraints[i]).CompiledConstraint = null;
			}
			element.ElementDecl = null;
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000BF89C File Offset: 0x000BDA9C
		private static void CleanupAttributes(XmlSchemaObjectCollection attributes)
		{
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlSchemaAttribute xmlSchemaAttribute = attributes[i] as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					SchemaCollectionCompiler.CleanupAttribute(xmlSchemaAttribute);
				}
			}
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000BF8D0 File Offset: 0x000BDAD0
		private static void CleanupGroup(XmlSchemaGroup group)
		{
			SchemaCollectionCompiler.CleanupParticle(group.Particle);
			group.CanonicalParticle = null;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000BF8E4 File Offset: 0x000BDAE4
		private static void CleanupParticle(XmlSchemaParticle particle)
		{
			if (particle is XmlSchemaElement)
			{
				SchemaCollectionCompiler.CleanupElement((XmlSchemaElement)particle);
				return;
			}
			if (particle is XmlSchemaGroupBase)
			{
				XmlSchemaObjectCollection items = ((XmlSchemaGroupBase)particle).Items;
				for (int i = 0; i < items.Count; i++)
				{
					SchemaCollectionCompiler.CleanupParticle((XmlSchemaParticle)items[i]);
				}
			}
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000BF93C File Offset: 0x000BDB3C
		private void CompileSubstitutionGroup(XmlSchemaSubstitutionGroupV1Compat substitutionGroup)
		{
			if (substitutionGroup.IsProcessing && substitutionGroup.Members.Count > 0)
			{
				base.SendValidationEvent("Sch_SubstitutionCircularRef", (XmlSchemaElement)substitutionGroup.Members[0]);
				return;
			}
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.schema.Elements[substitutionGroup.Examplar];
			if (substitutionGroup.Members.Contains(xmlSchemaElement))
			{
				return;
			}
			substitutionGroup.IsProcessing = true;
			if (xmlSchemaElement != null)
			{
				if (xmlSchemaElement.FinalResolved == XmlSchemaDerivationMethod.All)
				{
					base.SendValidationEvent("Sch_InvalidExamplar", xmlSchemaElement);
				}
				for (int i = 0; i < substitutionGroup.Members.Count; i++)
				{
					XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)substitutionGroup.Members[i];
					XmlSchemaSubstitutionGroupV1Compat xmlSchemaSubstitutionGroupV1Compat = (XmlSchemaSubstitutionGroupV1Compat)this.examplars[xmlSchemaElement2.QualifiedName];
					if (xmlSchemaSubstitutionGroupV1Compat != null)
					{
						this.CompileSubstitutionGroup(xmlSchemaSubstitutionGroupV1Compat);
						for (int j = 0; j < xmlSchemaSubstitutionGroupV1Compat.Choice.Items.Count; j++)
						{
							substitutionGroup.Choice.Items.Add(xmlSchemaSubstitutionGroupV1Compat.Choice.Items[j]);
						}
					}
					else
					{
						substitutionGroup.Choice.Items.Add(xmlSchemaElement2);
					}
				}
				substitutionGroup.Choice.Items.Add(xmlSchemaElement);
				substitutionGroup.Members.Add(xmlSchemaElement);
			}
			else if (substitutionGroup.Members.Count > 0)
			{
				base.SendValidationEvent("Sch_NoExamplar", (XmlSchemaElement)substitutionGroup.Members[0]);
			}
			substitutionGroup.IsProcessing = false;
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000BFAC4 File Offset: 0x000BDCC4
		private void CheckSubstitutionGroup(XmlSchemaSubstitutionGroup substitutionGroup)
		{
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.schema.Elements[substitutionGroup.Examplar];
			if (xmlSchemaElement != null)
			{
				for (int i = 0; i < substitutionGroup.Members.Count; i++)
				{
					XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)substitutionGroup.Members[i];
					if (xmlSchemaElement2 != xmlSchemaElement && !XmlSchemaType.IsDerivedFrom(xmlSchemaElement2.ElementSchemaType, xmlSchemaElement.ElementSchemaType, xmlSchemaElement.FinalResolved))
					{
						base.SendValidationEvent("Sch_InvalidSubstitutionMember", xmlSchemaElement2.QualifiedName.ToString(), xmlSchemaElement.QualifiedName.ToString(), xmlSchemaElement2);
					}
				}
			}
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x000BFB58 File Offset: 0x000BDD58
		private void CompileGroup(XmlSchemaGroup group)
		{
			if (group.IsProcessing)
			{
				base.SendValidationEvent("Sch_GroupCircularRef", group);
				group.CanonicalParticle = XmlSchemaParticle.Empty;
				return;
			}
			group.IsProcessing = true;
			if (group.CanonicalParticle == null)
			{
				group.CanonicalParticle = this.CannonicalizeParticle(group.Particle, true, true);
			}
			group.IsProcessing = false;
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000BFBB0 File Offset: 0x000BDDB0
		private void CompileSimpleType(XmlSchemaSimpleType simpleType)
		{
			if (simpleType.IsProcessing)
			{
				throw new XmlSchemaException("Sch_TypeCircularRef", simpleType);
			}
			if (simpleType.ElementDecl != null)
			{
				return;
			}
			simpleType.IsProcessing = true;
			try
			{
				if (simpleType.Content is XmlSchemaSimpleTypeList)
				{
					XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = (XmlSchemaSimpleTypeList)simpleType.Content;
					simpleType.SetBaseSchemaType(DatatypeImplementation.AnySimpleType);
					XmlSchemaDatatype datatype;
					if (xmlSchemaSimpleTypeList.ItemTypeName.IsEmpty)
					{
						this.CompileSimpleType(xmlSchemaSimpleTypeList.ItemType);
						xmlSchemaSimpleTypeList.BaseItemType = xmlSchemaSimpleTypeList.ItemType;
						datatype = xmlSchemaSimpleTypeList.ItemType.Datatype;
					}
					else
					{
						XmlSchemaSimpleType simpleType2 = this.GetSimpleType(xmlSchemaSimpleTypeList.ItemTypeName);
						if (simpleType2 == null)
						{
							throw new XmlSchemaException("Sch_UndeclaredSimpleType", xmlSchemaSimpleTypeList.ItemTypeName.ToString(), simpleType);
						}
						if ((simpleType2.FinalResolved & XmlSchemaDerivationMethod.List) != XmlSchemaDerivationMethod.Empty)
						{
							base.SendValidationEvent("Sch_BaseFinalList", simpleType);
						}
						xmlSchemaSimpleTypeList.BaseItemType = simpleType2;
						datatype = simpleType2.Datatype;
					}
					simpleType.SetDatatype(datatype.DeriveByList(simpleType));
					simpleType.SetDerivedBy(XmlSchemaDerivationMethod.List);
				}
				else if (simpleType.Content is XmlSchemaSimpleTypeRestriction)
				{
					XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)simpleType.Content;
					XmlSchemaDatatype datatype2;
					if (xmlSchemaSimpleTypeRestriction.BaseTypeName.IsEmpty)
					{
						this.CompileSimpleType(xmlSchemaSimpleTypeRestriction.BaseType);
						simpleType.SetBaseSchemaType(xmlSchemaSimpleTypeRestriction.BaseType);
						datatype2 = xmlSchemaSimpleTypeRestriction.BaseType.Datatype;
					}
					else if (simpleType.Redefined != null && xmlSchemaSimpleTypeRestriction.BaseTypeName == simpleType.Redefined.QualifiedName)
					{
						this.CompileSimpleType((XmlSchemaSimpleType)simpleType.Redefined);
						simpleType.SetBaseSchemaType(simpleType.Redefined.BaseXmlSchemaType);
						datatype2 = simpleType.Redefined.Datatype;
					}
					else
					{
						if (xmlSchemaSimpleTypeRestriction.BaseTypeName.Equals(DatatypeImplementation.QnAnySimpleType))
						{
							throw new XmlSchemaException("Sch_InvalidSimpleTypeRestriction", xmlSchemaSimpleTypeRestriction.BaseTypeName.ToString(), simpleType);
						}
						XmlSchemaSimpleType simpleType3 = this.GetSimpleType(xmlSchemaSimpleTypeRestriction.BaseTypeName);
						if (simpleType3 == null)
						{
							throw new XmlSchemaException("Sch_UndeclaredSimpleType", xmlSchemaSimpleTypeRestriction.BaseTypeName.ToString(), simpleType);
						}
						if ((simpleType3.FinalResolved & XmlSchemaDerivationMethod.Restriction) != XmlSchemaDerivationMethod.Empty)
						{
							base.SendValidationEvent("Sch_BaseFinalRestriction", simpleType);
						}
						simpleType.SetBaseSchemaType(simpleType3);
						datatype2 = simpleType3.Datatype;
					}
					simpleType.SetDatatype(datatype2.DeriveByRestriction(xmlSchemaSimpleTypeRestriction.Facets, base.NameTable, simpleType));
					simpleType.SetDerivedBy(XmlSchemaDerivationMethod.Restriction);
				}
				else
				{
					XmlSchemaSimpleType[] types = this.CompileBaseMemberTypes(simpleType);
					simpleType.SetBaseSchemaType(DatatypeImplementation.AnySimpleType);
					simpleType.SetDatatype(XmlSchemaDatatype.DeriveByUnion(types, simpleType));
					simpleType.SetDerivedBy(XmlSchemaDerivationMethod.Union);
				}
			}
			catch (XmlSchemaException ex)
			{
				if (ex.SourceSchemaObject == null)
				{
					ex.SetSource(simpleType);
				}
				base.SendValidationEvent(ex);
				simpleType.SetDatatype(DatatypeImplementation.AnySimpleType.Datatype);
			}
			finally
			{
				simpleType.ElementDecl = new SchemaElementDecl
				{
					ContentValidator = ContentValidator.TextOnly,
					SchemaType = simpleType,
					Datatype = simpleType.Datatype
				};
				simpleType.IsProcessing = false;
			}
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000BFEAC File Offset: 0x000BE0AC
		private XmlSchemaSimpleType[] CompileBaseMemberTypes(XmlSchemaSimpleType simpleType)
		{
			ArrayList arrayList = new ArrayList();
			XmlSchemaSimpleTypeUnion xmlSchemaSimpleTypeUnion = (XmlSchemaSimpleTypeUnion)simpleType.Content;
			XmlQualifiedName[] memberTypes = xmlSchemaSimpleTypeUnion.MemberTypes;
			if (memberTypes != null)
			{
				for (int i = 0; i < memberTypes.Length; i++)
				{
					XmlSchemaSimpleType simpleType2 = this.GetSimpleType(memberTypes[i]);
					if (simpleType2 == null)
					{
						throw new XmlSchemaException("Sch_UndeclaredSimpleType", memberTypes[i].ToString(), simpleType);
					}
					if (simpleType2.Datatype.Variety == XmlSchemaDatatypeVariety.Union)
					{
						this.CheckUnionType(simpleType2, arrayList, simpleType);
					}
					else
					{
						arrayList.Add(simpleType2);
					}
					if ((simpleType2.FinalResolved & XmlSchemaDerivationMethod.Union) != XmlSchemaDerivationMethod.Empty)
					{
						base.SendValidationEvent("Sch_BaseFinalUnion", simpleType);
					}
				}
			}
			XmlSchemaObjectCollection baseTypes = xmlSchemaSimpleTypeUnion.BaseTypes;
			if (baseTypes != null)
			{
				for (int j = 0; j < baseTypes.Count; j++)
				{
					XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)baseTypes[j];
					this.CompileSimpleType(xmlSchemaSimpleType);
					if (xmlSchemaSimpleType.Datatype.Variety == XmlSchemaDatatypeVariety.Union)
					{
						this.CheckUnionType(xmlSchemaSimpleType, arrayList, simpleType);
					}
					else
					{
						arrayList.Add(xmlSchemaSimpleType);
					}
				}
			}
			xmlSchemaSimpleTypeUnion.SetBaseMemberTypes(arrayList.ToArray(typeof(XmlSchemaSimpleType)) as XmlSchemaSimpleType[]);
			return xmlSchemaSimpleTypeUnion.BaseMemberTypes;
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000BFFC4 File Offset: 0x000BE1C4
		private void CheckUnionType(XmlSchemaSimpleType unionMember, ArrayList memberTypeDefinitions, XmlSchemaSimpleType parentType)
		{
			XmlSchemaDatatype datatype = unionMember.Datatype;
			if (unionMember.DerivedBy == XmlSchemaDerivationMethod.Restriction && (datatype.HasLexicalFacets || datatype.HasValueFacets))
			{
				base.SendValidationEvent("Sch_UnionFromUnion", parentType);
				return;
			}
			Datatype_union datatype_union = unionMember.Datatype as Datatype_union;
			memberTypeDefinitions.AddRange(datatype_union.BaseMemberTypes);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000C0018 File Offset: 0x000BE218
		private void CompileComplexType(XmlSchemaComplexType complexType)
		{
			if (complexType.ElementDecl != null)
			{
				return;
			}
			if (complexType.IsProcessing)
			{
				base.SendValidationEvent("Sch_TypeCircularRef", complexType);
				return;
			}
			complexType.IsProcessing = true;
			if (complexType.ContentModel != null)
			{
				if (complexType.ContentModel is XmlSchemaSimpleContent)
				{
					XmlSchemaSimpleContent xmlSchemaSimpleContent = (XmlSchemaSimpleContent)complexType.ContentModel;
					complexType.SetContentType(XmlSchemaContentType.TextOnly);
					if (xmlSchemaSimpleContent.Content is XmlSchemaSimpleContentExtension)
					{
						this.CompileSimpleContentExtension(complexType, (XmlSchemaSimpleContentExtension)xmlSchemaSimpleContent.Content);
					}
					else
					{
						this.CompileSimpleContentRestriction(complexType, (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContent.Content);
					}
				}
				else
				{
					XmlSchemaComplexContent xmlSchemaComplexContent = (XmlSchemaComplexContent)complexType.ContentModel;
					if (xmlSchemaComplexContent.Content is XmlSchemaComplexContentExtension)
					{
						this.CompileComplexContentExtension(complexType, xmlSchemaComplexContent, (XmlSchemaComplexContentExtension)xmlSchemaComplexContent.Content);
					}
					else
					{
						this.CompileComplexContentRestriction(complexType, xmlSchemaComplexContent, (XmlSchemaComplexContentRestriction)xmlSchemaComplexContent.Content);
					}
				}
			}
			else
			{
				complexType.SetBaseSchemaType(XmlSchemaComplexType.AnyType);
				this.CompileLocalAttributes(XmlSchemaComplexType.AnyType, complexType, complexType.Attributes, complexType.AnyAttribute, XmlSchemaDerivationMethod.Restriction);
				complexType.SetDerivedBy(XmlSchemaDerivationMethod.Restriction);
				complexType.SetContentTypeParticle(this.CompileContentTypeParticle(complexType.Particle, true));
				complexType.SetContentType(this.GetSchemaContentType(complexType, null, complexType.ContentTypeParticle));
			}
			bool flag = false;
			foreach (object obj in complexType.AttributeUses.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj;
				if (xmlSchemaAttribute.Use != XmlSchemaUse.Prohibited)
				{
					XmlSchemaDatatype datatype = xmlSchemaAttribute.Datatype;
					if (datatype != null && datatype.TokenizedType == XmlTokenizedType.ID)
					{
						if (flag)
						{
							base.SendValidationEvent("Sch_TwoIdAttrUses", complexType);
						}
						else
						{
							flag = true;
						}
					}
				}
			}
			SchemaElementDecl schemaElementDecl = new SchemaElementDecl();
			schemaElementDecl.ContentValidator = this.CompileComplexContent(complexType);
			schemaElementDecl.SchemaType = complexType;
			schemaElementDecl.IsAbstract = complexType.IsAbstract;
			schemaElementDecl.Datatype = complexType.Datatype;
			schemaElementDecl.Block = complexType.BlockResolved;
			schemaElementDecl.AnyAttribute = complexType.AttributeWildcard;
			foreach (object obj2 in complexType.AttributeUses.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute2 = (XmlSchemaAttribute)obj2;
				if (xmlSchemaAttribute2.Use == XmlSchemaUse.Prohibited)
				{
					if (!schemaElementDecl.ProhibitedAttributes.ContainsKey(xmlSchemaAttribute2.QualifiedName))
					{
						schemaElementDecl.ProhibitedAttributes.Add(xmlSchemaAttribute2.QualifiedName, xmlSchemaAttribute2.QualifiedName);
					}
				}
				else if (!schemaElementDecl.AttDefs.ContainsKey(xmlSchemaAttribute2.QualifiedName) && xmlSchemaAttribute2.AttDef != null && xmlSchemaAttribute2.AttDef.Name != XmlQualifiedName.Empty && xmlSchemaAttribute2.AttDef != SchemaAttDef.Empty)
				{
					schemaElementDecl.AddAttDef(xmlSchemaAttribute2.AttDef);
				}
			}
			complexType.ElementDecl = schemaElementDecl;
			complexType.IsProcessing = false;
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000C0304 File Offset: 0x000BE504
		private void CompileSimpleContentExtension(XmlSchemaComplexType complexType, XmlSchemaSimpleContentExtension simpleExtension)
		{
			XmlSchemaComplexType xmlSchemaComplexType;
			if (complexType.Redefined != null && simpleExtension.BaseTypeName == complexType.Redefined.QualifiedName)
			{
				xmlSchemaComplexType = (XmlSchemaComplexType)complexType.Redefined;
				this.CompileComplexType(xmlSchemaComplexType);
				complexType.SetBaseSchemaType(xmlSchemaComplexType);
				complexType.SetDatatype(xmlSchemaComplexType.Datatype);
			}
			else
			{
				XmlSchemaType anySchemaType = this.GetAnySchemaType(simpleExtension.BaseTypeName);
				if (anySchemaType == null)
				{
					base.SendValidationEvent("Sch_UndeclaredType", simpleExtension.BaseTypeName.ToString(), complexType);
				}
				else
				{
					complexType.SetBaseSchemaType(anySchemaType);
					complexType.SetDatatype(anySchemaType.Datatype);
				}
				xmlSchemaComplexType = (anySchemaType as XmlSchemaComplexType);
			}
			if (xmlSchemaComplexType != null)
			{
				if ((xmlSchemaComplexType.FinalResolved & XmlSchemaDerivationMethod.Extension) != XmlSchemaDerivationMethod.Empty)
				{
					base.SendValidationEvent("Sch_BaseFinalExtension", complexType);
				}
				if (xmlSchemaComplexType.ContentType != XmlSchemaContentType.TextOnly)
				{
					base.SendValidationEvent("Sch_NotSimpleContent", complexType);
				}
			}
			complexType.SetDerivedBy(XmlSchemaDerivationMethod.Extension);
			this.CompileLocalAttributes(xmlSchemaComplexType, complexType, simpleExtension.Attributes, simpleExtension.AnyAttribute, XmlSchemaDerivationMethod.Extension);
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x000C03E8 File Offset: 0x000BE5E8
		private void CompileSimpleContentRestriction(XmlSchemaComplexType complexType, XmlSchemaSimpleContentRestriction simpleRestriction)
		{
			XmlSchemaComplexType xmlSchemaComplexType = null;
			XmlSchemaDatatype xmlSchemaDatatype = null;
			if (complexType.Redefined != null && simpleRestriction.BaseTypeName == complexType.Redefined.QualifiedName)
			{
				xmlSchemaComplexType = (XmlSchemaComplexType)complexType.Redefined;
				this.CompileComplexType(xmlSchemaComplexType);
				xmlSchemaDatatype = xmlSchemaComplexType.Datatype;
			}
			else
			{
				xmlSchemaComplexType = this.GetComplexType(simpleRestriction.BaseTypeName);
				if (xmlSchemaComplexType == null)
				{
					base.SendValidationEvent("Sch_UndefBaseRestriction", simpleRestriction.BaseTypeName.ToString(), simpleRestriction);
					return;
				}
				if (xmlSchemaComplexType.ContentType == XmlSchemaContentType.TextOnly)
				{
					if (simpleRestriction.BaseType == null)
					{
						xmlSchemaDatatype = xmlSchemaComplexType.Datatype;
					}
					else
					{
						this.CompileSimpleType(simpleRestriction.BaseType);
						if (!XmlSchemaType.IsDerivedFromDatatype(simpleRestriction.BaseType.Datatype, xmlSchemaComplexType.Datatype, XmlSchemaDerivationMethod.None))
						{
							base.SendValidationEvent("Sch_DerivedNotFromBase", simpleRestriction);
						}
						xmlSchemaDatatype = simpleRestriction.BaseType.Datatype;
					}
				}
				else if (xmlSchemaComplexType.ContentType == XmlSchemaContentType.Mixed && xmlSchemaComplexType.ElementDecl.ContentValidator.IsEmptiable)
				{
					if (simpleRestriction.BaseType != null)
					{
						this.CompileSimpleType(simpleRestriction.BaseType);
						complexType.SetBaseSchemaType(simpleRestriction.BaseType);
						xmlSchemaDatatype = simpleRestriction.BaseType.Datatype;
					}
					else
					{
						base.SendValidationEvent("Sch_NeedSimpleTypeChild", simpleRestriction);
					}
				}
				else
				{
					base.SendValidationEvent("Sch_NotSimpleContent", complexType);
				}
			}
			if (xmlSchemaComplexType != null && xmlSchemaComplexType.ElementDecl != null && (xmlSchemaComplexType.FinalResolved & XmlSchemaDerivationMethod.Restriction) != XmlSchemaDerivationMethod.Empty)
			{
				base.SendValidationEvent("Sch_BaseFinalRestriction", complexType);
			}
			if (xmlSchemaComplexType != null)
			{
				complexType.SetBaseSchemaType(xmlSchemaComplexType);
			}
			if (xmlSchemaDatatype != null)
			{
				try
				{
					complexType.SetDatatype(xmlSchemaDatatype.DeriveByRestriction(simpleRestriction.Facets, base.NameTable, complexType));
				}
				catch (XmlSchemaException ex)
				{
					if (ex.SourceSchemaObject == null)
					{
						ex.SetSource(complexType);
					}
					base.SendValidationEvent(ex);
					complexType.SetDatatype(DatatypeImplementation.AnySimpleType.Datatype);
				}
			}
			complexType.SetDerivedBy(XmlSchemaDerivationMethod.Restriction);
			this.CompileLocalAttributes(xmlSchemaComplexType, complexType, simpleRestriction.Attributes, simpleRestriction.AnyAttribute, XmlSchemaDerivationMethod.Restriction);
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000C05C0 File Offset: 0x000BE7C0
		private void CompileComplexContentExtension(XmlSchemaComplexType complexType, XmlSchemaComplexContent complexContent, XmlSchemaComplexContentExtension complexExtension)
		{
			XmlSchemaComplexType xmlSchemaComplexType;
			if (complexType.Redefined != null && complexExtension.BaseTypeName == complexType.Redefined.QualifiedName)
			{
				xmlSchemaComplexType = (XmlSchemaComplexType)complexType.Redefined;
				this.CompileComplexType(xmlSchemaComplexType);
			}
			else
			{
				xmlSchemaComplexType = this.GetComplexType(complexExtension.BaseTypeName);
				if (xmlSchemaComplexType == null)
				{
					base.SendValidationEvent("Sch_UndefBaseExtension", complexExtension.BaseTypeName.ToString(), complexExtension);
					return;
				}
			}
			if (xmlSchemaComplexType != null && xmlSchemaComplexType.ElementDecl != null && xmlSchemaComplexType.ContentType == XmlSchemaContentType.TextOnly)
			{
				base.SendValidationEvent("Sch_NotComplexContent", complexType);
				return;
			}
			complexType.SetBaseSchemaType(xmlSchemaComplexType);
			if ((xmlSchemaComplexType.FinalResolved & XmlSchemaDerivationMethod.Extension) != XmlSchemaDerivationMethod.Empty)
			{
				base.SendValidationEvent("Sch_BaseFinalExtension", complexType);
			}
			this.CompileLocalAttributes(xmlSchemaComplexType, complexType, complexExtension.Attributes, complexExtension.AnyAttribute, XmlSchemaDerivationMethod.Extension);
			XmlSchemaParticle contentTypeParticle = xmlSchemaComplexType.ContentTypeParticle;
			XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle(complexExtension.Particle, true, true);
			if (contentTypeParticle != XmlSchemaParticle.Empty)
			{
				if (xmlSchemaParticle != XmlSchemaParticle.Empty)
				{
					complexType.SetContentTypeParticle(this.CompileContentTypeParticle(new XmlSchemaSequence
					{
						Items = 
						{
							contentTypeParticle,
							xmlSchemaParticle
						}
					}, false));
				}
				else
				{
					complexType.SetContentTypeParticle(contentTypeParticle);
				}
				XmlSchemaContentType xmlSchemaContentType = this.GetSchemaContentType(complexType, complexContent, xmlSchemaParticle);
				if (xmlSchemaContentType == XmlSchemaContentType.Empty)
				{
					xmlSchemaContentType = xmlSchemaComplexType.ContentType;
				}
				complexType.SetContentType(xmlSchemaContentType);
				if (complexType.ContentType != xmlSchemaComplexType.ContentType)
				{
					base.SendValidationEvent("Sch_DifContentType", complexType);
				}
			}
			else
			{
				complexType.SetContentTypeParticle(xmlSchemaParticle);
				complexType.SetContentType(this.GetSchemaContentType(complexType, complexContent, complexType.ContentTypeParticle));
			}
			complexType.SetDerivedBy(XmlSchemaDerivationMethod.Extension);
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x000C073C File Offset: 0x000BE93C
		private void CompileComplexContentRestriction(XmlSchemaComplexType complexType, XmlSchemaComplexContent complexContent, XmlSchemaComplexContentRestriction complexRestriction)
		{
			XmlSchemaComplexType xmlSchemaComplexType;
			if (complexType.Redefined != null && complexRestriction.BaseTypeName == complexType.Redefined.QualifiedName)
			{
				xmlSchemaComplexType = (XmlSchemaComplexType)complexType.Redefined;
				this.CompileComplexType(xmlSchemaComplexType);
			}
			else
			{
				xmlSchemaComplexType = this.GetComplexType(complexRestriction.BaseTypeName);
				if (xmlSchemaComplexType == null)
				{
					base.SendValidationEvent("Sch_UndefBaseRestriction", complexRestriction.BaseTypeName.ToString(), complexRestriction);
					return;
				}
			}
			if (xmlSchemaComplexType != null && xmlSchemaComplexType.ElementDecl != null && xmlSchemaComplexType.ContentType == XmlSchemaContentType.TextOnly)
			{
				base.SendValidationEvent("Sch_NotComplexContent", complexType);
				return;
			}
			complexType.SetBaseSchemaType(xmlSchemaComplexType);
			if ((xmlSchemaComplexType.FinalResolved & XmlSchemaDerivationMethod.Restriction) != XmlSchemaDerivationMethod.Empty)
			{
				base.SendValidationEvent("Sch_BaseFinalRestriction", complexType);
			}
			this.CompileLocalAttributes(xmlSchemaComplexType, complexType, complexRestriction.Attributes, complexRestriction.AnyAttribute, XmlSchemaDerivationMethod.Restriction);
			complexType.SetContentTypeParticle(this.CompileContentTypeParticle(complexRestriction.Particle, true));
			complexType.SetContentType(this.GetSchemaContentType(complexType, complexContent, complexType.ContentTypeParticle));
			if (complexType.ContentType == XmlSchemaContentType.Empty)
			{
				SchemaElementDecl elementDecl = xmlSchemaComplexType.ElementDecl;
				if (xmlSchemaComplexType.ElementDecl != null && !xmlSchemaComplexType.ElementDecl.ContentValidator.IsEmptiable)
				{
					base.SendValidationEvent("Sch_InvalidContentRestriction", complexType);
				}
			}
			complexType.SetDerivedBy(XmlSchemaDerivationMethod.Restriction);
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000C0860 File Offset: 0x000BEA60
		private void CheckParticleDerivation(XmlSchemaComplexType complexType)
		{
			XmlSchemaComplexType xmlSchemaComplexType = complexType.BaseXmlSchemaType as XmlSchemaComplexType;
			if (xmlSchemaComplexType != null && xmlSchemaComplexType != XmlSchemaComplexType.AnyType && complexType.DerivedBy == XmlSchemaDerivationMethod.Restriction && !this.IsValidRestriction(complexType.ContentTypeParticle, xmlSchemaComplexType.ContentTypeParticle))
			{
				base.SendValidationEvent("Sch_InvalidParticleRestriction", complexType);
			}
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000C08B0 File Offset: 0x000BEAB0
		private XmlSchemaParticle CompileContentTypeParticle(XmlSchemaParticle particle, bool substitution)
		{
			XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle(particle, true, substitution);
			XmlSchemaChoice xmlSchemaChoice = xmlSchemaParticle as XmlSchemaChoice;
			if (xmlSchemaChoice != null && xmlSchemaChoice.Items.Count == 0)
			{
				if (xmlSchemaChoice.MinOccurs != 0m)
				{
					base.SendValidationEvent("Sch_EmptyChoice", xmlSchemaChoice, XmlSeverityType.Warning);
				}
				return XmlSchemaParticle.Empty;
			}
			return xmlSchemaParticle;
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000C0904 File Offset: 0x000BEB04
		private XmlSchemaParticle CannonicalizeParticle(XmlSchemaParticle particle, bool root, bool substitution)
		{
			if (particle == null || particle.IsEmpty)
			{
				return XmlSchemaParticle.Empty;
			}
			if (particle is XmlSchemaElement)
			{
				return this.CannonicalizeElement((XmlSchemaElement)particle, substitution);
			}
			if (particle is XmlSchemaGroupRef)
			{
				return this.CannonicalizeGroupRef((XmlSchemaGroupRef)particle, root, substitution);
			}
			if (particle is XmlSchemaAll)
			{
				return this.CannonicalizeAll((XmlSchemaAll)particle, root, substitution);
			}
			if (particle is XmlSchemaChoice)
			{
				return this.CannonicalizeChoice((XmlSchemaChoice)particle, root, substitution);
			}
			if (particle is XmlSchemaSequence)
			{
				return this.CannonicalizeSequence((XmlSchemaSequence)particle, root, substitution);
			}
			return particle;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000C0998 File Offset: 0x000BEB98
		private XmlSchemaParticle CannonicalizeElement(XmlSchemaElement element, bool substitution)
		{
			if (element.RefName.IsEmpty || !substitution || (element.BlockResolved & XmlSchemaDerivationMethod.Substitution) != XmlSchemaDerivationMethod.Empty)
			{
				return element;
			}
			XmlSchemaSubstitutionGroupV1Compat xmlSchemaSubstitutionGroupV1Compat = (XmlSchemaSubstitutionGroupV1Compat)this.examplars[element.QualifiedName];
			if (xmlSchemaSubstitutionGroupV1Compat == null)
			{
				return element;
			}
			XmlSchemaChoice xmlSchemaChoice = (XmlSchemaChoice)xmlSchemaSubstitutionGroupV1Compat.Choice.Clone();
			xmlSchemaChoice.MinOccurs = element.MinOccurs;
			xmlSchemaChoice.MaxOccurs = element.MaxOccurs;
			return xmlSchemaChoice;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000C0A0C File Offset: 0x000BEC0C
		private XmlSchemaParticle CannonicalizeGroupRef(XmlSchemaGroupRef groupRef, bool root, bool substitution)
		{
			XmlSchemaGroup xmlSchemaGroup;
			if (groupRef.Redefined != null)
			{
				xmlSchemaGroup = groupRef.Redefined;
			}
			else
			{
				xmlSchemaGroup = (XmlSchemaGroup)this.schema.Groups[groupRef.RefName];
			}
			if (xmlSchemaGroup == null)
			{
				base.SendValidationEvent("Sch_UndefGroupRef", groupRef.RefName.ToString(), groupRef);
				return XmlSchemaParticle.Empty;
			}
			if (xmlSchemaGroup.CanonicalParticle == null)
			{
				this.CompileGroup(xmlSchemaGroup);
			}
			if (xmlSchemaGroup.CanonicalParticle == XmlSchemaParticle.Empty)
			{
				return XmlSchemaParticle.Empty;
			}
			XmlSchemaGroupBase xmlSchemaGroupBase = (XmlSchemaGroupBase)xmlSchemaGroup.CanonicalParticle;
			if (xmlSchemaGroupBase is XmlSchemaAll)
			{
				if (!root)
				{
					base.SendValidationEvent("Sch_AllRefNotRoot", "", groupRef);
					return XmlSchemaParticle.Empty;
				}
				if (groupRef.MinOccurs != 1m || groupRef.MaxOccurs != 1m)
				{
					base.SendValidationEvent("Sch_AllRefMinMax", groupRef);
					return XmlSchemaParticle.Empty;
				}
			}
			else if (xmlSchemaGroupBase is XmlSchemaChoice && xmlSchemaGroupBase.Items.Count == 0)
			{
				if (groupRef.MinOccurs != 0m)
				{
					base.SendValidationEvent("Sch_EmptyChoice", groupRef, XmlSeverityType.Warning);
				}
				return XmlSchemaParticle.Empty;
			}
			XmlSchemaGroupBase xmlSchemaGroupBase2 = (xmlSchemaGroupBase is XmlSchemaSequence) ? new XmlSchemaSequence() : ((xmlSchemaGroupBase is XmlSchemaChoice) ? new XmlSchemaChoice() : new XmlSchemaAll());
			xmlSchemaGroupBase2.MinOccurs = groupRef.MinOccurs;
			xmlSchemaGroupBase2.MaxOccurs = groupRef.MaxOccurs;
			for (int i = 0; i < xmlSchemaGroupBase.Items.Count; i++)
			{
				xmlSchemaGroupBase2.Items.Add((XmlSchemaParticle)xmlSchemaGroupBase.Items[i]);
			}
			groupRef.SetParticle(xmlSchemaGroupBase2);
			return xmlSchemaGroupBase2;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000C0BA0 File Offset: 0x000BEDA0
		private XmlSchemaParticle CannonicalizeAll(XmlSchemaAll all, bool root, bool substitution)
		{
			if (all.Items.Count > 0)
			{
				XmlSchemaAll xmlSchemaAll = new XmlSchemaAll();
				xmlSchemaAll.MinOccurs = all.MinOccurs;
				xmlSchemaAll.MaxOccurs = all.MaxOccurs;
				xmlSchemaAll.SourceUri = all.SourceUri;
				xmlSchemaAll.LineNumber = all.LineNumber;
				xmlSchemaAll.LinePosition = all.LinePosition;
				for (int i = 0; i < all.Items.Count; i++)
				{
					XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle((XmlSchemaElement)all.Items[i], false, substitution);
					if (xmlSchemaParticle != XmlSchemaParticle.Empty)
					{
						xmlSchemaAll.Items.Add(xmlSchemaParticle);
					}
				}
				all = xmlSchemaAll;
			}
			if (all.Items.Count == 0)
			{
				return XmlSchemaParticle.Empty;
			}
			if (root && all.Items.Count == 1)
			{
				return new XmlSchemaSequence
				{
					MinOccurs = all.MinOccurs,
					MaxOccurs = all.MaxOccurs,
					Items = 
					{
						(XmlSchemaParticle)all.Items[0]
					}
				};
			}
			if (!root && all.Items.Count == 1 && all.MinOccurs == 1m && all.MaxOccurs == 1m)
			{
				return (XmlSchemaParticle)all.Items[0];
			}
			if (!root)
			{
				base.SendValidationEvent("Sch_NotAllAlone", all);
				return XmlSchemaParticle.Empty;
			}
			return all;
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000C0D08 File Offset: 0x000BEF08
		private XmlSchemaParticle CannonicalizeChoice(XmlSchemaChoice choice, bool root, bool substitution)
		{
			XmlSchemaChoice source = choice;
			if (choice.Items.Count > 0)
			{
				XmlSchemaChoice xmlSchemaChoice = new XmlSchemaChoice();
				xmlSchemaChoice.MinOccurs = choice.MinOccurs;
				xmlSchemaChoice.MaxOccurs = choice.MaxOccurs;
				for (int i = 0; i < choice.Items.Count; i++)
				{
					XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle((XmlSchemaParticle)choice.Items[i], false, substitution);
					if (xmlSchemaParticle != XmlSchemaParticle.Empty)
					{
						if (xmlSchemaParticle.MinOccurs == 1m && xmlSchemaParticle.MaxOccurs == 1m && xmlSchemaParticle is XmlSchemaChoice)
						{
							XmlSchemaChoice xmlSchemaChoice2 = (XmlSchemaChoice)xmlSchemaParticle;
							for (int j = 0; j < xmlSchemaChoice2.Items.Count; j++)
							{
								xmlSchemaChoice.Items.Add(xmlSchemaChoice2.Items[j]);
							}
						}
						else
						{
							xmlSchemaChoice.Items.Add(xmlSchemaParticle);
						}
					}
				}
				choice = xmlSchemaChoice;
			}
			if (!root && choice.Items.Count == 0)
			{
				if (choice.MinOccurs != 0m)
				{
					base.SendValidationEvent("Sch_EmptyChoice", source, XmlSeverityType.Warning);
				}
				return XmlSchemaParticle.Empty;
			}
			if (!root && choice.Items.Count == 1 && choice.MinOccurs == 1m && choice.MaxOccurs == 1m)
			{
				return (XmlSchemaParticle)choice.Items[0];
			}
			return choice;
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000C0E7C File Offset: 0x000BF07C
		private XmlSchemaParticle CannonicalizeSequence(XmlSchemaSequence sequence, bool root, bool substitution)
		{
			if (sequence.Items.Count > 0)
			{
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				xmlSchemaSequence.MinOccurs = sequence.MinOccurs;
				xmlSchemaSequence.MaxOccurs = sequence.MaxOccurs;
				for (int i = 0; i < sequence.Items.Count; i++)
				{
					XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle((XmlSchemaParticle)sequence.Items[i], false, substitution);
					if (xmlSchemaParticle != XmlSchemaParticle.Empty)
					{
						if (xmlSchemaParticle.MinOccurs == 1m && xmlSchemaParticle.MaxOccurs == 1m && xmlSchemaParticle is XmlSchemaSequence)
						{
							XmlSchemaSequence xmlSchemaSequence2 = (XmlSchemaSequence)xmlSchemaParticle;
							for (int j = 0; j < xmlSchemaSequence2.Items.Count; j++)
							{
								xmlSchemaSequence.Items.Add(xmlSchemaSequence2.Items[j]);
							}
						}
						else
						{
							xmlSchemaSequence.Items.Add(xmlSchemaParticle);
						}
					}
				}
				sequence = xmlSchemaSequence;
			}
			if (sequence.Items.Count == 0)
			{
				return XmlSchemaParticle.Empty;
			}
			if (!root && sequence.Items.Count == 1 && sequence.MinOccurs == 1m && sequence.MaxOccurs == 1m)
			{
				return (XmlSchemaParticle)sequence.Items[0];
			}
			return sequence;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000C0FCC File Offset: 0x000BF1CC
		private bool IsValidRestriction(XmlSchemaParticle derivedParticle, XmlSchemaParticle baseParticle)
		{
			if (derivedParticle == baseParticle)
			{
				return true;
			}
			if (derivedParticle == null || derivedParticle == XmlSchemaParticle.Empty)
			{
				return this.IsParticleEmptiable(baseParticle);
			}
			if (baseParticle == null || baseParticle == XmlSchemaParticle.Empty)
			{
				return false;
			}
			if (baseParticle is XmlSchemaElement)
			{
				return derivedParticle is XmlSchemaElement && this.IsElementFromElement((XmlSchemaElement)derivedParticle, (XmlSchemaElement)baseParticle);
			}
			if (!(baseParticle is XmlSchemaAny))
			{
				if (baseParticle is XmlSchemaAll)
				{
					if (derivedParticle is XmlSchemaElement)
					{
						return this.IsElementFromGroupBase((XmlSchemaElement)derivedParticle, (XmlSchemaGroupBase)baseParticle, true);
					}
					if (derivedParticle is XmlSchemaAll)
					{
						return this.IsGroupBaseFromGroupBase((XmlSchemaGroupBase)derivedParticle, (XmlSchemaGroupBase)baseParticle, true);
					}
					if (derivedParticle is XmlSchemaSequence)
					{
						return this.IsSequenceFromAll((XmlSchemaSequence)derivedParticle, (XmlSchemaAll)baseParticle);
					}
				}
				else if (baseParticle is XmlSchemaChoice)
				{
					if (derivedParticle is XmlSchemaElement)
					{
						return this.IsElementFromGroupBase((XmlSchemaElement)derivedParticle, (XmlSchemaGroupBase)baseParticle, false);
					}
					if (derivedParticle is XmlSchemaChoice)
					{
						return this.IsGroupBaseFromGroupBase((XmlSchemaGroupBase)derivedParticle, (XmlSchemaGroupBase)baseParticle, false);
					}
					if (derivedParticle is XmlSchemaSequence)
					{
						return this.IsSequenceFromChoice((XmlSchemaSequence)derivedParticle, (XmlSchemaChoice)baseParticle);
					}
				}
				else if (baseParticle is XmlSchemaSequence)
				{
					if (derivedParticle is XmlSchemaElement)
					{
						return this.IsElementFromGroupBase((XmlSchemaElement)derivedParticle, (XmlSchemaGroupBase)baseParticle, true);
					}
					if (derivedParticle is XmlSchemaSequence)
					{
						return this.IsGroupBaseFromGroupBase((XmlSchemaGroupBase)derivedParticle, (XmlSchemaGroupBase)baseParticle, true);
					}
				}
				return false;
			}
			if (derivedParticle is XmlSchemaElement)
			{
				return this.IsElementFromAny((XmlSchemaElement)derivedParticle, (XmlSchemaAny)baseParticle);
			}
			if (derivedParticle is XmlSchemaAny)
			{
				return this.IsAnyFromAny((XmlSchemaAny)derivedParticle, (XmlSchemaAny)baseParticle);
			}
			return this.IsGroupBaseFromAny((XmlSchemaGroupBase)derivedParticle, (XmlSchemaAny)baseParticle);
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000C1170 File Offset: 0x000BF370
		private bool IsElementFromElement(XmlSchemaElement derivedElement, XmlSchemaElement baseElement)
		{
			return derivedElement.QualifiedName == baseElement.QualifiedName && derivedElement.IsNillable == baseElement.IsNillable && this.IsValidOccurrenceRangeRestriction(derivedElement, baseElement) && (baseElement.FixedValue == null || baseElement.FixedValue == derivedElement.FixedValue) && (derivedElement.BlockResolved | baseElement.BlockResolved) == derivedElement.BlockResolved && derivedElement.ElementSchemaType != null && baseElement.ElementSchemaType != null && XmlSchemaType.IsDerivedFrom(derivedElement.ElementSchemaType, baseElement.ElementSchemaType, ~XmlSchemaDerivationMethod.Restriction);
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000C11FD File Offset: 0x000BF3FD
		private bool IsElementFromAny(XmlSchemaElement derivedElement, XmlSchemaAny baseAny)
		{
			return baseAny.Allows(derivedElement.QualifiedName) && this.IsValidOccurrenceRangeRestriction(derivedElement, baseAny);
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000C1217 File Offset: 0x000BF417
		private bool IsAnyFromAny(XmlSchemaAny derivedAny, XmlSchemaAny baseAny)
		{
			return this.IsValidOccurrenceRangeRestriction(derivedAny, baseAny) && NamespaceList.IsSubset(derivedAny.NamespaceList, baseAny.NamespaceList);
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x000C1238 File Offset: 0x000BF438
		private bool IsGroupBaseFromAny(XmlSchemaGroupBase derivedGroupBase, XmlSchemaAny baseAny)
		{
			decimal minOccurs;
			decimal maxOccurs;
			this.CalculateEffectiveTotalRange(derivedGroupBase, out minOccurs, out maxOccurs);
			if (!this.IsValidOccurrenceRangeRestriction(minOccurs, maxOccurs, baseAny.MinOccurs, baseAny.MaxOccurs))
			{
				return false;
			}
			string minOccursString = baseAny.MinOccursString;
			baseAny.MinOccurs = 0m;
			for (int i = 0; i < derivedGroupBase.Items.Count; i++)
			{
				if (!this.IsValidRestriction((XmlSchemaParticle)derivedGroupBase.Items[i], baseAny))
				{
					baseAny.MinOccursString = minOccursString;
					return false;
				}
			}
			baseAny.MinOccursString = minOccursString;
			return true;
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000C12BC File Offset: 0x000BF4BC
		private bool IsElementFromGroupBase(XmlSchemaElement derivedElement, XmlSchemaGroupBase baseGroupBase, bool skipEmptableOnly)
		{
			bool flag = false;
			for (int i = 0; i < baseGroupBase.Items.Count; i++)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)baseGroupBase.Items[i];
				if (!flag)
				{
					string minOccursString = xmlSchemaParticle.MinOccursString;
					string maxOccursString = xmlSchemaParticle.MaxOccursString;
					xmlSchemaParticle.MinOccurs *= baseGroupBase.MinOccurs;
					if (xmlSchemaParticle.MaxOccurs != 79228162514264337593543950335m)
					{
						if (baseGroupBase.MaxOccurs == 79228162514264337593543950335m)
						{
							xmlSchemaParticle.MaxOccurs = decimal.MaxValue;
						}
						else
						{
							xmlSchemaParticle.MaxOccurs *= baseGroupBase.MaxOccurs;
						}
					}
					flag = this.IsValidRestriction(derivedElement, xmlSchemaParticle);
					xmlSchemaParticle.MinOccursString = minOccursString;
					xmlSchemaParticle.MaxOccursString = maxOccursString;
				}
				else if (skipEmptableOnly && !this.IsParticleEmptiable(xmlSchemaParticle))
				{
					return false;
				}
			}
			return flag;
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x000C13A8 File Offset: 0x000BF5A8
		private bool IsGroupBaseFromGroupBase(XmlSchemaGroupBase derivedGroupBase, XmlSchemaGroupBase baseGroupBase, bool skipEmptableOnly)
		{
			if (!this.IsValidOccurrenceRangeRestriction(derivedGroupBase, baseGroupBase) || derivedGroupBase.Items.Count > baseGroupBase.Items.Count)
			{
				return false;
			}
			int num = 0;
			for (int i = 0; i < baseGroupBase.Items.Count; i++)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)baseGroupBase.Items[i];
				if (num < derivedGroupBase.Items.Count && this.IsValidRestriction((XmlSchemaParticle)derivedGroupBase.Items[num], xmlSchemaParticle))
				{
					num++;
				}
				else if (skipEmptableOnly && !this.IsParticleEmptiable(xmlSchemaParticle))
				{
					return false;
				}
			}
			return num >= derivedGroupBase.Items.Count;
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000C1450 File Offset: 0x000BF650
		private bool IsSequenceFromAll(XmlSchemaSequence derivedSequence, XmlSchemaAll baseAll)
		{
			if (!this.IsValidOccurrenceRangeRestriction(derivedSequence, baseAll) || derivedSequence.Items.Count > baseAll.Items.Count)
			{
				return false;
			}
			BitSet bitSet = new BitSet(baseAll.Items.Count);
			for (int i = 0; i < derivedSequence.Items.Count; i++)
			{
				int mappingParticle = this.GetMappingParticle((XmlSchemaParticle)derivedSequence.Items[i], baseAll.Items);
				if (mappingParticle < 0)
				{
					return false;
				}
				if (bitSet[mappingParticle])
				{
					return false;
				}
				bitSet.Set(mappingParticle);
			}
			for (int j = 0; j < baseAll.Items.Count; j++)
			{
				if (!bitSet[j] && !this.IsParticleEmptiable((XmlSchemaParticle)baseAll.Items[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000C151C File Offset: 0x000BF71C
		private bool IsSequenceFromChoice(XmlSchemaSequence derivedSequence, XmlSchemaChoice baseChoice)
		{
			decimal minOccurs;
			decimal maxOccurs;
			this.CalculateSequenceRange(derivedSequence, out minOccurs, out maxOccurs);
			if (!this.IsValidOccurrenceRangeRestriction(minOccurs, maxOccurs, baseChoice.MinOccurs, baseChoice.MaxOccurs) || derivedSequence.Items.Count > baseChoice.Items.Count)
			{
				return false;
			}
			for (int i = 0; i < derivedSequence.Items.Count; i++)
			{
				if (this.GetMappingParticle((XmlSchemaParticle)derivedSequence.Items[i], baseChoice.Items) < 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000C15A0 File Offset: 0x000BF7A0
		private void CalculateSequenceRange(XmlSchemaSequence sequence, out decimal minOccurs, out decimal maxOccurs)
		{
			minOccurs = 0m;
			maxOccurs = 0m;
			for (int i = 0; i < sequence.Items.Count; i++)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)sequence.Items[i];
				minOccurs += xmlSchemaParticle.MinOccurs;
				if (xmlSchemaParticle.MaxOccurs == 79228162514264337593543950335m)
				{
					maxOccurs = decimal.MaxValue;
				}
				else if (maxOccurs != 79228162514264337593543950335m)
				{
					maxOccurs += xmlSchemaParticle.MaxOccurs;
				}
			}
			minOccurs *= sequence.MinOccurs;
			if (sequence.MaxOccurs == 79228162514264337593543950335m)
			{
				maxOccurs = decimal.MaxValue;
				return;
			}
			if (maxOccurs != 79228162514264337593543950335m)
			{
				maxOccurs *= sequence.MaxOccurs;
			}
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000C16C4 File Offset: 0x000BF8C4
		private bool IsValidOccurrenceRangeRestriction(XmlSchemaParticle derivedParticle, XmlSchemaParticle baseParticle)
		{
			return this.IsValidOccurrenceRangeRestriction(derivedParticle.MinOccurs, derivedParticle.MaxOccurs, baseParticle.MinOccurs, baseParticle.MaxOccurs);
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000C16E4 File Offset: 0x000BF8E4
		private bool IsValidOccurrenceRangeRestriction(decimal minOccurs, decimal maxOccurs, decimal baseMinOccurs, decimal baseMaxOccurs)
		{
			return baseMinOccurs <= minOccurs && maxOccurs <= baseMaxOccurs;
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000C16FC File Offset: 0x000BF8FC
		private int GetMappingParticle(XmlSchemaParticle particle, XmlSchemaObjectCollection collection)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				if (this.IsValidRestriction(particle, (XmlSchemaParticle)collection[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000C1734 File Offset: 0x000BF934
		private bool IsParticleEmptiable(XmlSchemaParticle particle)
		{
			decimal d;
			decimal num;
			this.CalculateEffectiveTotalRange(particle, out d, out num);
			return d == 0m;
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000C1758 File Offset: 0x000BF958
		private void CalculateEffectiveTotalRange(XmlSchemaParticle particle, out decimal minOccurs, out decimal maxOccurs)
		{
			if (particle is XmlSchemaElement || particle is XmlSchemaAny)
			{
				minOccurs = particle.MinOccurs;
				maxOccurs = particle.MaxOccurs;
				return;
			}
			if (particle is XmlSchemaChoice)
			{
				if (((XmlSchemaChoice)particle).Items.Count == 0)
				{
					minOccurs = (maxOccurs = 0m);
					return;
				}
				minOccurs = decimal.MaxValue;
				maxOccurs = 0m;
				XmlSchemaChoice xmlSchemaChoice = (XmlSchemaChoice)particle;
				for (int i = 0; i < xmlSchemaChoice.Items.Count; i++)
				{
					decimal num;
					decimal num2;
					this.CalculateEffectiveTotalRange((XmlSchemaParticle)xmlSchemaChoice.Items[i], out num, out num2);
					if (num < minOccurs)
					{
						minOccurs = num;
					}
					if (num2 > maxOccurs)
					{
						maxOccurs = num2;
					}
				}
				minOccurs *= particle.MinOccurs;
				if (maxOccurs != 79228162514264337593543950335m)
				{
					if (particle.MaxOccurs == 79228162514264337593543950335m)
					{
						maxOccurs = decimal.MaxValue;
						return;
					}
					maxOccurs *= particle.MaxOccurs;
					return;
				}
			}
			else
			{
				XmlSchemaObjectCollection items = ((XmlSchemaGroupBase)particle).Items;
				if (items.Count == 0)
				{
					minOccurs = (maxOccurs = 0m);
					return;
				}
				minOccurs = 0m;
				maxOccurs = 0m;
				for (int j = 0; j < items.Count; j++)
				{
					decimal d;
					decimal num3;
					this.CalculateEffectiveTotalRange((XmlSchemaParticle)items[j], out d, out num3);
					minOccurs += d;
					if (maxOccurs != 79228162514264337593543950335m)
					{
						if (num3 == 79228162514264337593543950335m)
						{
							maxOccurs = decimal.MaxValue;
						}
						else
						{
							maxOccurs += num3;
						}
					}
				}
				minOccurs *= particle.MinOccurs;
				if (maxOccurs != 79228162514264337593543950335m)
				{
					if (particle.MaxOccurs == 79228162514264337593543950335m)
					{
						maxOccurs = decimal.MaxValue;
						return;
					}
					maxOccurs *= particle.MaxOccurs;
				}
			}
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000C19E8 File Offset: 0x000BFBE8
		private void PushComplexType(XmlSchemaComplexType complexType)
		{
			this.complexTypeStack.Push(complexType);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000C19F6 File Offset: 0x000BFBF6
		private XmlSchemaContentType GetSchemaContentType(XmlSchemaComplexType complexType, XmlSchemaComplexContent complexContent, XmlSchemaParticle particle)
		{
			if ((complexContent != null && complexContent.IsMixed) || (complexContent == null && complexType.IsMixed))
			{
				return XmlSchemaContentType.Mixed;
			}
			if (particle != null && !particle.IsEmpty)
			{
				return XmlSchemaContentType.ElementOnly;
			}
			return XmlSchemaContentType.Empty;
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000C1A20 File Offset: 0x000BFC20
		private void CompileAttributeGroup(XmlSchemaAttributeGroup attributeGroup)
		{
			if (attributeGroup.IsProcessing)
			{
				base.SendValidationEvent("Sch_AttributeGroupCircularRef", attributeGroup);
				return;
			}
			if (attributeGroup.AttributeUses.Count > 0)
			{
				return;
			}
			attributeGroup.IsProcessing = true;
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = attributeGroup.AnyAttribute;
			for (int i = 0; i < attributeGroup.Attributes.Count; i++)
			{
				XmlSchemaAttribute xmlSchemaAttribute = attributeGroup.Attributes[i] as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					if (xmlSchemaAttribute.Use != XmlSchemaUse.Prohibited)
					{
						this.CompileAttribute(xmlSchemaAttribute);
					}
					if (attributeGroup.AttributeUses[xmlSchemaAttribute.QualifiedName] == null)
					{
						attributeGroup.AttributeUses.Add(xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute);
					}
					else
					{
						base.SendValidationEvent("Sch_DupAttributeUse", xmlSchemaAttribute.QualifiedName.ToString(), xmlSchemaAttribute);
					}
				}
				else
				{
					XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = (XmlSchemaAttributeGroupRef)attributeGroup.Attributes[i];
					XmlSchemaAttributeGroup xmlSchemaAttributeGroup;
					if (attributeGroup.Redefined != null && xmlSchemaAttributeGroupRef.RefName == attributeGroup.Redefined.QualifiedName)
					{
						xmlSchemaAttributeGroup = attributeGroup.Redefined;
					}
					else
					{
						xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)this.schema.AttributeGroups[xmlSchemaAttributeGroupRef.RefName];
					}
					if (xmlSchemaAttributeGroup != null)
					{
						this.CompileAttributeGroup(xmlSchemaAttributeGroup);
						foreach (object obj in xmlSchemaAttributeGroup.AttributeUses.Values)
						{
							XmlSchemaAttribute xmlSchemaAttribute2 = (XmlSchemaAttribute)obj;
							if (attributeGroup.AttributeUses[xmlSchemaAttribute2.QualifiedName] == null)
							{
								attributeGroup.AttributeUses.Add(xmlSchemaAttribute2.QualifiedName, xmlSchemaAttribute2);
							}
							else
							{
								base.SendValidationEvent("Sch_DupAttributeUse", xmlSchemaAttribute2.QualifiedName.ToString(), xmlSchemaAttribute2);
							}
						}
						xmlSchemaAnyAttribute = this.CompileAnyAttributeIntersection(xmlSchemaAnyAttribute, xmlSchemaAttributeGroup.AttributeWildcard);
					}
					else
					{
						base.SendValidationEvent("Sch_UndefAttributeGroupRef", xmlSchemaAttributeGroupRef.RefName.ToString(), xmlSchemaAttributeGroupRef);
					}
				}
			}
			attributeGroup.AttributeWildcard = xmlSchemaAnyAttribute;
			attributeGroup.IsProcessing = false;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000C1C1C File Offset: 0x000BFE1C
		private void CompileLocalAttributes(XmlSchemaComplexType baseType, XmlSchemaComplexType derivedType, XmlSchemaObjectCollection attributes, XmlSchemaAnyAttribute anyAttribute, XmlSchemaDerivationMethod derivedBy)
		{
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = (baseType != null) ? baseType.AttributeWildcard : null;
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlSchemaAttribute xmlSchemaAttribute = attributes[i] as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					if (xmlSchemaAttribute.Use != XmlSchemaUse.Prohibited)
					{
						this.CompileAttribute(xmlSchemaAttribute);
					}
					if (xmlSchemaAttribute.Use != XmlSchemaUse.Prohibited || (xmlSchemaAttribute.Use == XmlSchemaUse.Prohibited && derivedBy == XmlSchemaDerivationMethod.Restriction && baseType != XmlSchemaComplexType.AnyType))
					{
						if (derivedType.AttributeUses[xmlSchemaAttribute.QualifiedName] == null)
						{
							derivedType.AttributeUses.Add(xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute);
						}
						else
						{
							base.SendValidationEvent("Sch_DupAttributeUse", xmlSchemaAttribute.QualifiedName.ToString(), xmlSchemaAttribute);
						}
					}
					else
					{
						base.SendValidationEvent("Sch_AttributeIgnored", xmlSchemaAttribute.QualifiedName.ToString(), xmlSchemaAttribute, XmlSeverityType.Warning);
					}
				}
				else
				{
					XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = (XmlSchemaAttributeGroupRef)attributes[i];
					XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)this.schema.AttributeGroups[xmlSchemaAttributeGroupRef.RefName];
					if (xmlSchemaAttributeGroup != null)
					{
						this.CompileAttributeGroup(xmlSchemaAttributeGroup);
						foreach (object obj in xmlSchemaAttributeGroup.AttributeUses.Values)
						{
							XmlSchemaAttribute xmlSchemaAttribute2 = (XmlSchemaAttribute)obj;
							if (xmlSchemaAttribute2.Use != XmlSchemaUse.Prohibited || (xmlSchemaAttribute2.Use == XmlSchemaUse.Prohibited && derivedBy == XmlSchemaDerivationMethod.Restriction && baseType != XmlSchemaComplexType.AnyType))
							{
								if (derivedType.AttributeUses[xmlSchemaAttribute2.QualifiedName] == null)
								{
									derivedType.AttributeUses.Add(xmlSchemaAttribute2.QualifiedName, xmlSchemaAttribute2);
								}
								else
								{
									base.SendValidationEvent("Sch_DupAttributeUse", xmlSchemaAttribute2.QualifiedName.ToString(), xmlSchemaAttributeGroupRef);
								}
							}
							else
							{
								base.SendValidationEvent("Sch_AttributeIgnored", xmlSchemaAttribute2.QualifiedName.ToString(), xmlSchemaAttribute2, XmlSeverityType.Warning);
							}
						}
						anyAttribute = this.CompileAnyAttributeIntersection(anyAttribute, xmlSchemaAttributeGroup.AttributeWildcard);
					}
					else
					{
						base.SendValidationEvent("Sch_UndefAttributeGroupRef", xmlSchemaAttributeGroupRef.RefName.ToString(), xmlSchemaAttributeGroupRef);
					}
				}
			}
			if (baseType != null)
			{
				if (derivedBy == XmlSchemaDerivationMethod.Extension)
				{
					derivedType.SetAttributeWildcard(this.CompileAnyAttributeUnion(anyAttribute, xmlSchemaAnyAttribute));
					using (IEnumerator enumerator2 = baseType.AttributeUses.Values.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							XmlSchemaAttribute xmlSchemaAttribute3 = (XmlSchemaAttribute)obj2;
							XmlSchemaAttribute xmlSchemaAttribute4 = (XmlSchemaAttribute)derivedType.AttributeUses[xmlSchemaAttribute3.QualifiedName];
							if (xmlSchemaAttribute4 != null)
							{
								if (xmlSchemaAttribute4.AttributeSchemaType != xmlSchemaAttribute3.AttributeSchemaType || xmlSchemaAttribute3.Use == XmlSchemaUse.Prohibited)
								{
									base.SendValidationEvent("Sch_InvalidAttributeExtension", xmlSchemaAttribute4);
								}
							}
							else
							{
								derivedType.AttributeUses.Add(xmlSchemaAttribute3.QualifiedName, xmlSchemaAttribute3);
							}
						}
						return;
					}
				}
				if (anyAttribute != null && (xmlSchemaAnyAttribute == null || !XmlSchemaAnyAttribute.IsSubset(anyAttribute, xmlSchemaAnyAttribute)))
				{
					base.SendValidationEvent("Sch_InvalidAnyAttributeRestriction", derivedType);
				}
				else
				{
					derivedType.SetAttributeWildcard(anyAttribute);
				}
				foreach (object obj3 in baseType.AttributeUses.Values)
				{
					XmlSchemaAttribute xmlSchemaAttribute5 = (XmlSchemaAttribute)obj3;
					XmlSchemaAttribute xmlSchemaAttribute6 = (XmlSchemaAttribute)derivedType.AttributeUses[xmlSchemaAttribute5.QualifiedName];
					if (xmlSchemaAttribute6 == null)
					{
						derivedType.AttributeUses.Add(xmlSchemaAttribute5.QualifiedName, xmlSchemaAttribute5);
					}
					else if (xmlSchemaAttribute5.Use == XmlSchemaUse.Prohibited && xmlSchemaAttribute6.Use != XmlSchemaUse.Prohibited)
					{
						base.SendValidationEvent("Sch_AttributeRestrictionProhibited", xmlSchemaAttribute6);
					}
					else if (xmlSchemaAttribute6.Use != XmlSchemaUse.Prohibited && (xmlSchemaAttribute5.AttributeSchemaType == null || xmlSchemaAttribute6.AttributeSchemaType == null || !XmlSchemaType.IsDerivedFrom(xmlSchemaAttribute6.AttributeSchemaType, xmlSchemaAttribute5.AttributeSchemaType, XmlSchemaDerivationMethod.Empty)))
					{
						base.SendValidationEvent("Sch_AttributeRestrictionInvalid", xmlSchemaAttribute6);
					}
				}
				using (IEnumerator enumerator4 = derivedType.AttributeUses.Values.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						object obj4 = enumerator4.Current;
						XmlSchemaAttribute xmlSchemaAttribute7 = (XmlSchemaAttribute)obj4;
						if ((XmlSchemaAttribute)baseType.AttributeUses[xmlSchemaAttribute7.QualifiedName] == null && (xmlSchemaAnyAttribute == null || !xmlSchemaAnyAttribute.Allows(xmlSchemaAttribute7.QualifiedName)))
						{
							base.SendValidationEvent("Sch_AttributeRestrictionInvalidFromWildcard", xmlSchemaAttribute7);
						}
					}
					return;
				}
			}
			derivedType.SetAttributeWildcard(anyAttribute);
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000C2098 File Offset: 0x000C0298
		private XmlSchemaAnyAttribute CompileAnyAttributeUnion(XmlSchemaAnyAttribute a, XmlSchemaAnyAttribute b)
		{
			if (a == null)
			{
				return b;
			}
			if (b == null)
			{
				return a;
			}
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = XmlSchemaAnyAttribute.Union(a, b, true);
			if (xmlSchemaAnyAttribute == null)
			{
				base.SendValidationEvent("Sch_UnexpressibleAnyAttribute", a);
			}
			return xmlSchemaAnyAttribute;
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000C20C8 File Offset: 0x000C02C8
		private XmlSchemaAnyAttribute CompileAnyAttributeIntersection(XmlSchemaAnyAttribute a, XmlSchemaAnyAttribute b)
		{
			if (a == null)
			{
				return b;
			}
			if (b == null)
			{
				return a;
			}
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = XmlSchemaAnyAttribute.Intersection(a, b, true);
			if (xmlSchemaAnyAttribute == null)
			{
				base.SendValidationEvent("Sch_UnexpressibleAnyAttribute", a);
			}
			return xmlSchemaAnyAttribute;
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000C20F8 File Offset: 0x000C02F8
		private void CompileAttribute(XmlSchemaAttribute xa)
		{
			if (xa.IsProcessing)
			{
				base.SendValidationEvent("Sch_AttributeCircularRef", xa);
				return;
			}
			if (xa.AttDef != null)
			{
				return;
			}
			xa.IsProcessing = true;
			try
			{
				SchemaAttDef schemaAttDef;
				if (!xa.RefName.IsEmpty)
				{
					XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)this.schema.Attributes[xa.RefName];
					if (xmlSchemaAttribute == null)
					{
						throw new XmlSchemaException("Sch_UndeclaredAttribute", xa.RefName.ToString(), xa);
					}
					this.CompileAttribute(xmlSchemaAttribute);
					if (xmlSchemaAttribute.AttDef == null)
					{
						throw new XmlSchemaException("Sch_RefInvalidAttribute", xa.RefName.ToString(), xa);
					}
					schemaAttDef = xmlSchemaAttribute.AttDef.Clone();
					if (schemaAttDef.Datatype != null)
					{
						if (xmlSchemaAttribute.FixedValue != null)
						{
							if (xa.DefaultValue != null)
							{
								throw new XmlSchemaException("Sch_FixedDefaultInRef", xa.RefName.ToString(), xa);
							}
							if (xa.FixedValue != null)
							{
								if (xa.FixedValue != xmlSchemaAttribute.FixedValue)
								{
									throw new XmlSchemaException("Sch_FixedInRef", xa.RefName.ToString(), xa);
								}
							}
							else
							{
								schemaAttDef.Presence = SchemaDeclBase.Use.Fixed;
								schemaAttDef.DefaultValueRaw = (schemaAttDef.DefaultValueExpanded = xmlSchemaAttribute.FixedValue);
								schemaAttDef.DefaultValueTyped = schemaAttDef.Datatype.ParseValue(schemaAttDef.DefaultValueRaw, base.NameTable, new SchemaNamespaceManager(xa), true);
							}
						}
						else if (xmlSchemaAttribute.DefaultValue != null && xa.DefaultValue == null && xa.FixedValue == null)
						{
							schemaAttDef.Presence = SchemaDeclBase.Use.Default;
							schemaAttDef.DefaultValueRaw = (schemaAttDef.DefaultValueExpanded = xmlSchemaAttribute.DefaultValue);
							schemaAttDef.DefaultValueTyped = schemaAttDef.Datatype.ParseValue(schemaAttDef.DefaultValueRaw, base.NameTable, new SchemaNamespaceManager(xa), true);
						}
					}
					xa.SetAttributeType(xmlSchemaAttribute.AttributeSchemaType);
				}
				else
				{
					schemaAttDef = new SchemaAttDef(xa.QualifiedName);
					if (xa.SchemaType != null)
					{
						this.CompileSimpleType(xa.SchemaType);
						xa.SetAttributeType(xa.SchemaType);
						schemaAttDef.SchemaType = xa.SchemaType;
						schemaAttDef.Datatype = xa.SchemaType.Datatype;
					}
					else if (!xa.SchemaTypeName.IsEmpty)
					{
						XmlSchemaSimpleType simpleType = this.GetSimpleType(xa.SchemaTypeName);
						if (simpleType == null)
						{
							throw new XmlSchemaException("Sch_UndeclaredSimpleType", xa.SchemaTypeName.ToString(), xa);
						}
						xa.SetAttributeType(simpleType);
						schemaAttDef.Datatype = simpleType.Datatype;
						schemaAttDef.SchemaType = simpleType;
					}
					else
					{
						schemaAttDef.SchemaType = DatatypeImplementation.AnySimpleType;
						schemaAttDef.Datatype = DatatypeImplementation.AnySimpleType.Datatype;
						xa.SetAttributeType(DatatypeImplementation.AnySimpleType);
					}
				}
				if (schemaAttDef.Datatype != null)
				{
					schemaAttDef.Datatype.VerifySchemaValid(this.schema.Notations, xa);
				}
				if (xa.DefaultValue != null || xa.FixedValue != null)
				{
					if (xa.DefaultValue != null)
					{
						schemaAttDef.Presence = SchemaDeclBase.Use.Default;
						schemaAttDef.DefaultValueRaw = (schemaAttDef.DefaultValueExpanded = xa.DefaultValue);
					}
					else
					{
						schemaAttDef.Presence = SchemaDeclBase.Use.Fixed;
						schemaAttDef.DefaultValueRaw = (schemaAttDef.DefaultValueExpanded = xa.FixedValue);
					}
					if (schemaAttDef.Datatype != null)
					{
						schemaAttDef.DefaultValueTyped = schemaAttDef.Datatype.ParseValue(schemaAttDef.DefaultValueRaw, base.NameTable, new SchemaNamespaceManager(xa), true);
					}
				}
				else
				{
					switch (xa.Use)
					{
					case XmlSchemaUse.None:
					case XmlSchemaUse.Optional:
						schemaAttDef.Presence = SchemaDeclBase.Use.Implied;
						break;
					case XmlSchemaUse.Required:
						schemaAttDef.Presence = SchemaDeclBase.Use.Required;
						break;
					}
				}
				schemaAttDef.SchemaAttribute = xa;
				xa.AttDef = schemaAttDef;
			}
			catch (XmlSchemaException ex)
			{
				if (ex.SourceSchemaObject == null)
				{
					ex.SetSource(xa);
				}
				base.SendValidationEvent(ex);
				xa.AttDef = SchemaAttDef.Empty;
			}
			finally
			{
				xa.IsProcessing = false;
			}
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000C24C0 File Offset: 0x000C06C0
		private void CompileIdentityConstraint(XmlSchemaIdentityConstraint xi)
		{
			if (xi.IsProcessing)
			{
				xi.CompiledConstraint = CompiledIdentityConstraint.Empty;
				base.SendValidationEvent("Sch_IdentityConstraintCircularRef", xi);
				return;
			}
			if (xi.CompiledConstraint != null)
			{
				return;
			}
			xi.IsProcessing = true;
			try
			{
				SchemaNamespaceManager nsmgr = new SchemaNamespaceManager(xi);
				CompiledIdentityConstraint compiledConstraint = new CompiledIdentityConstraint(xi, nsmgr);
				if (xi is XmlSchemaKeyref)
				{
					XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)this.schema.IdentityConstraints[((XmlSchemaKeyref)xi).Refer];
					if (xmlSchemaIdentityConstraint == null)
					{
						throw new XmlSchemaException("Sch_UndeclaredIdentityConstraint", ((XmlSchemaKeyref)xi).Refer.ToString(), xi);
					}
					this.CompileIdentityConstraint(xmlSchemaIdentityConstraint);
					if (xmlSchemaIdentityConstraint.CompiledConstraint == null)
					{
						throw new XmlSchemaException("Sch_RefInvalidIdentityConstraint", ((XmlSchemaKeyref)xi).Refer.ToString(), xi);
					}
					if (xmlSchemaIdentityConstraint.Fields.Count != xi.Fields.Count)
					{
						throw new XmlSchemaException("Sch_RefInvalidCardin", xi.QualifiedName.ToString(), xi);
					}
					if (xmlSchemaIdentityConstraint.CompiledConstraint.Role == CompiledIdentityConstraint.ConstraintRole.Keyref)
					{
						throw new XmlSchemaException("Sch_ReftoKeyref", xi.QualifiedName.ToString(), xi);
					}
				}
				xi.CompiledConstraint = compiledConstraint;
			}
			catch (XmlSchemaException ex)
			{
				if (ex.SourceSchemaObject == null)
				{
					ex.SetSource(xi);
				}
				base.SendValidationEvent(ex);
				xi.CompiledConstraint = CompiledIdentityConstraint.Empty;
			}
			finally
			{
				xi.IsProcessing = false;
			}
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000C2644 File Offset: 0x000C0844
		private void CompileElement(XmlSchemaElement xe)
		{
			if (xe.IsProcessing)
			{
				base.SendValidationEvent("Sch_ElementCircularRef", xe);
				return;
			}
			if (xe.ElementDecl != null)
			{
				return;
			}
			xe.IsProcessing = true;
			SchemaElementDecl schemaElementDecl = null;
			try
			{
				if (!xe.RefName.IsEmpty)
				{
					XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.schema.Elements[xe.RefName];
					if (xmlSchemaElement == null)
					{
						throw new XmlSchemaException("Sch_UndeclaredElement", xe.RefName.ToString(), xe);
					}
					this.CompileElement(xmlSchemaElement);
					if (xmlSchemaElement.ElementDecl == null)
					{
						throw new XmlSchemaException("Sch_RefInvalidElement", xe.RefName.ToString(), xe);
					}
					xe.SetElementType(xmlSchemaElement.ElementSchemaType);
					schemaElementDecl = xmlSchemaElement.ElementDecl.Clone();
				}
				else
				{
					if (xe.SchemaType != null)
					{
						xe.SetElementType(xe.SchemaType);
					}
					else if (!xe.SchemaTypeName.IsEmpty)
					{
						xe.SetElementType(this.GetAnySchemaType(xe.SchemaTypeName));
						if (xe.ElementSchemaType == null)
						{
							throw new XmlSchemaException("Sch_UndeclaredType", xe.SchemaTypeName.ToString(), xe);
						}
					}
					else if (!xe.SubstitutionGroup.IsEmpty)
					{
						XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)this.schema.Elements[xe.SubstitutionGroup];
						if (xmlSchemaElement2 == null)
						{
							throw new XmlSchemaException("Sch_UndeclaredEquivClass", xe.SubstitutionGroup.Name.ToString(CultureInfo.InvariantCulture), xe);
						}
						if (xmlSchemaElement2.IsProcessing)
						{
							return;
						}
						this.CompileElement(xmlSchemaElement2);
						if (xmlSchemaElement2.ElementDecl == null)
						{
							xe.SetElementType(XmlSchemaComplexType.AnyType);
							schemaElementDecl = XmlSchemaComplexType.AnyType.ElementDecl.Clone();
						}
						else
						{
							xe.SetElementType(xmlSchemaElement2.ElementSchemaType);
							schemaElementDecl = xmlSchemaElement2.ElementDecl.Clone();
						}
					}
					else
					{
						xe.SetElementType(XmlSchemaComplexType.AnyType);
						schemaElementDecl = XmlSchemaComplexType.AnyType.ElementDecl.Clone();
					}
					if (schemaElementDecl == null)
					{
						if (xe.ElementSchemaType is XmlSchemaComplexType)
						{
							XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)xe.ElementSchemaType;
							this.CompileComplexType(xmlSchemaComplexType);
							if (xmlSchemaComplexType.ElementDecl != null)
							{
								schemaElementDecl = xmlSchemaComplexType.ElementDecl.Clone();
							}
						}
						else if (xe.ElementSchemaType is XmlSchemaSimpleType)
						{
							XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)xe.ElementSchemaType;
							this.CompileSimpleType(xmlSchemaSimpleType);
							if (xmlSchemaSimpleType.ElementDecl != null)
							{
								schemaElementDecl = xmlSchemaSimpleType.ElementDecl.Clone();
							}
						}
					}
					schemaElementDecl.Name = xe.QualifiedName;
					schemaElementDecl.IsAbstract = xe.IsAbstract;
					XmlSchemaComplexType xmlSchemaComplexType2 = xe.ElementSchemaType as XmlSchemaComplexType;
					if (xmlSchemaComplexType2 != null)
					{
						schemaElementDecl.IsAbstract |= xmlSchemaComplexType2.IsAbstract;
					}
					schemaElementDecl.IsNillable = xe.IsNillable;
					schemaElementDecl.Block |= xe.BlockResolved;
				}
				if (schemaElementDecl.Datatype != null)
				{
					schemaElementDecl.Datatype.VerifySchemaValid(this.schema.Notations, xe);
				}
				if ((xe.DefaultValue != null || xe.FixedValue != null) && schemaElementDecl.ContentValidator != null)
				{
					if (schemaElementDecl.ContentValidator.ContentType == XmlSchemaContentType.TextOnly)
					{
						if (xe.DefaultValue != null)
						{
							schemaElementDecl.Presence = SchemaDeclBase.Use.Default;
							schemaElementDecl.DefaultValueRaw = xe.DefaultValue;
						}
						else
						{
							schemaElementDecl.Presence = SchemaDeclBase.Use.Fixed;
							schemaElementDecl.DefaultValueRaw = xe.FixedValue;
						}
						if (schemaElementDecl.Datatype != null)
						{
							schemaElementDecl.DefaultValueTyped = schemaElementDecl.Datatype.ParseValue(schemaElementDecl.DefaultValueRaw, base.NameTable, new SchemaNamespaceManager(xe), true);
						}
					}
					else if (schemaElementDecl.ContentValidator.ContentType != XmlSchemaContentType.Mixed || !schemaElementDecl.ContentValidator.IsEmptiable)
					{
						throw new XmlSchemaException("Sch_ElementCannotHaveValue", xe);
					}
				}
				if (xe.HasConstraints)
				{
					XmlSchemaObjectCollection constraints = xe.Constraints;
					CompiledIdentityConstraint[] array = new CompiledIdentityConstraint[constraints.Count];
					int num = 0;
					for (int i = 0; i < constraints.Count; i++)
					{
						XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)constraints[i];
						this.CompileIdentityConstraint(xmlSchemaIdentityConstraint);
						array[num++] = xmlSchemaIdentityConstraint.CompiledConstraint;
					}
					schemaElementDecl.Constraints = array;
				}
				schemaElementDecl.SchemaElement = xe;
				xe.ElementDecl = schemaElementDecl;
			}
			catch (XmlSchemaException ex)
			{
				if (ex.SourceSchemaObject == null)
				{
					ex.SetSource(xe);
				}
				base.SendValidationEvent(ex);
				xe.ElementDecl = SchemaElementDecl.Empty;
			}
			finally
			{
				xe.IsProcessing = false;
			}
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000C2A90 File Offset: 0x000C0C90
		private ContentValidator CompileComplexContent(XmlSchemaComplexType complexType)
		{
			if (complexType.ContentType == XmlSchemaContentType.Empty)
			{
				return ContentValidator.Empty;
			}
			if (complexType.ContentType == XmlSchemaContentType.TextOnly)
			{
				return ContentValidator.TextOnly;
			}
			XmlSchemaParticle contentTypeParticle = complexType.ContentTypeParticle;
			if (contentTypeParticle == null || contentTypeParticle == XmlSchemaParticle.Empty)
			{
				if (complexType.ContentType == XmlSchemaContentType.ElementOnly)
				{
					return ContentValidator.Empty;
				}
				return ContentValidator.Mixed;
			}
			else
			{
				this.PushComplexType(complexType);
				if (contentTypeParticle is XmlSchemaAll)
				{
					XmlSchemaAll xmlSchemaAll = (XmlSchemaAll)contentTypeParticle;
					AllElementsContentValidator allElementsContentValidator = new AllElementsContentValidator(complexType.ContentType, xmlSchemaAll.Items.Count, xmlSchemaAll.MinOccurs == 0m);
					for (int i = 0; i < xmlSchemaAll.Items.Count; i++)
					{
						XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)xmlSchemaAll.Items[i];
						if (!allElementsContentValidator.AddElement(xmlSchemaElement.QualifiedName, xmlSchemaElement, xmlSchemaElement.MinOccurs == 0m))
						{
							base.SendValidationEvent("Sch_DupElement", xmlSchemaElement.QualifiedName.ToString(), xmlSchemaElement);
						}
					}
					return allElementsContentValidator;
				}
				ParticleContentValidator particleContentValidator = new ParticleContentValidator(complexType.ContentType);
				ContentValidator result;
				try
				{
					particleContentValidator.Start();
					this.BuildParticleContentModel(particleContentValidator, contentTypeParticle);
					result = particleContentValidator.Finish(this.compileContentModel);
				}
				catch (UpaException ex)
				{
					if (ex.Particle1 is XmlSchemaElement)
					{
						if (ex.Particle2 is XmlSchemaElement)
						{
							base.SendValidationEvent("Sch_NonDeterministic", ((XmlSchemaElement)ex.Particle1).QualifiedName.ToString(), (XmlSchemaElement)ex.Particle2);
						}
						else
						{
							base.SendValidationEvent("Sch_NonDeterministicAnyEx", ((XmlSchemaAny)ex.Particle2).NamespaceList.ToString(), ((XmlSchemaElement)ex.Particle1).QualifiedName.ToString(), (XmlSchemaAny)ex.Particle2);
						}
					}
					else if (ex.Particle2 is XmlSchemaElement)
					{
						base.SendValidationEvent("Sch_NonDeterministicAnyEx", ((XmlSchemaAny)ex.Particle1).NamespaceList.ToString(), ((XmlSchemaElement)ex.Particle2).QualifiedName.ToString(), (XmlSchemaAny)ex.Particle1);
					}
					else
					{
						base.SendValidationEvent("Sch_NonDeterministicAnyAny", ((XmlSchemaAny)ex.Particle1).NamespaceList.ToString(), ((XmlSchemaAny)ex.Particle2).NamespaceList.ToString(), (XmlSchemaAny)ex.Particle1);
					}
					result = XmlSchemaComplexType.AnyTypeContentValidator;
				}
				catch (NotSupportedException)
				{
					base.SendValidationEvent("Sch_ComplexContentModel", complexType, XmlSeverityType.Warning);
					result = XmlSchemaComplexType.AnyTypeContentValidator;
				}
				return result;
			}
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000C2D40 File Offset: 0x000C0F40
		private void BuildParticleContentModel(ParticleContentValidator contentValidator, XmlSchemaParticle particle)
		{
			if (particle is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)particle;
				contentValidator.AddName(xmlSchemaElement.QualifiedName, xmlSchemaElement);
			}
			else if (particle is XmlSchemaAny)
			{
				XmlSchemaAny xmlSchemaAny = (XmlSchemaAny)particle;
				contentValidator.AddNamespaceList(xmlSchemaAny.NamespaceList, xmlSchemaAny);
			}
			else if (particle is XmlSchemaGroupBase)
			{
				XmlSchemaObjectCollection items = ((XmlSchemaGroupBase)particle).Items;
				bool flag = particle is XmlSchemaChoice;
				contentValidator.OpenGroup();
				bool flag2 = true;
				for (int i = 0; i < items.Count; i++)
				{
					XmlSchemaParticle particle2 = (XmlSchemaParticle)items[i];
					if (flag2)
					{
						flag2 = false;
					}
					else if (flag)
					{
						contentValidator.AddChoice();
					}
					else
					{
						contentValidator.AddSequence();
					}
					this.BuildParticleContentModel(contentValidator, particle2);
				}
				contentValidator.CloseGroup();
			}
			if (!(particle.MinOccurs == 1m) || !(particle.MaxOccurs == 1m))
			{
				if (particle.MinOccurs == 0m && particle.MaxOccurs == 1m)
				{
					contentValidator.AddQMark();
					return;
				}
				if (particle.MinOccurs == 0m && particle.MaxOccurs == 79228162514264337593543950335m)
				{
					contentValidator.AddStar();
					return;
				}
				if (particle.MinOccurs == 1m && particle.MaxOccurs == 79228162514264337593543950335m)
				{
					contentValidator.AddPlus();
					return;
				}
				contentValidator.AddLeafRange(particle.MinOccurs, particle.MaxOccurs);
			}
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000C2EC4 File Offset: 0x000C10C4
		private void CompileParticleElements(XmlSchemaComplexType complexType, XmlSchemaParticle particle)
		{
			if (particle is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)particle;
				this.CompileElement(xmlSchemaElement);
				if (complexType.LocalElements[xmlSchemaElement.QualifiedName] == null)
				{
					complexType.LocalElements.Add(xmlSchemaElement.QualifiedName, xmlSchemaElement);
					return;
				}
				XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)complexType.LocalElements[xmlSchemaElement.QualifiedName];
				if (xmlSchemaElement2.ElementSchemaType != xmlSchemaElement.ElementSchemaType)
				{
					base.SendValidationEvent("Sch_ElementTypeCollision", particle);
					return;
				}
			}
			else if (particle is XmlSchemaGroupBase)
			{
				XmlSchemaObjectCollection items = ((XmlSchemaGroupBase)particle).Items;
				for (int i = 0; i < items.Count; i++)
				{
					this.CompileParticleElements(complexType, (XmlSchemaParticle)items[i]);
				}
			}
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000C2F77 File Offset: 0x000C1177
		private void CompileCompexTypeElements(XmlSchemaComplexType complexType)
		{
			if (complexType.IsProcessing)
			{
				base.SendValidationEvent("Sch_TypeCircularRef", complexType);
				return;
			}
			complexType.IsProcessing = true;
			if (complexType.ContentTypeParticle != XmlSchemaParticle.Empty)
			{
				this.CompileParticleElements(complexType, complexType.ContentTypeParticle);
			}
			complexType.IsProcessing = false;
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000C2FB8 File Offset: 0x000C11B8
		private XmlSchemaSimpleType GetSimpleType(XmlQualifiedName name)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = this.schema.SchemaTypes[name] as XmlSchemaSimpleType;
			if (xmlSchemaSimpleType != null)
			{
				this.CompileSimpleType(xmlSchemaSimpleType);
			}
			else
			{
				xmlSchemaSimpleType = DatatypeImplementation.GetSimpleTypeFromXsdType(name);
				if (xmlSchemaSimpleType != null)
				{
					if (xmlSchemaSimpleType.TypeCode == XmlTypeCode.NormalizedString)
					{
						xmlSchemaSimpleType = DatatypeImplementation.GetNormalizedStringTypeV1Compat();
					}
					else if (xmlSchemaSimpleType.TypeCode == XmlTypeCode.Token)
					{
						xmlSchemaSimpleType = DatatypeImplementation.GetTokenTypeV1Compat();
					}
				}
			}
			return xmlSchemaSimpleType;
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000C3018 File Offset: 0x000C1218
		private XmlSchemaComplexType GetComplexType(XmlQualifiedName name)
		{
			XmlSchemaComplexType xmlSchemaComplexType = this.schema.SchemaTypes[name] as XmlSchemaComplexType;
			if (xmlSchemaComplexType != null)
			{
				this.CompileComplexType(xmlSchemaComplexType);
			}
			return xmlSchemaComplexType;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000C3048 File Offset: 0x000C1248
		private XmlSchemaType GetAnySchemaType(XmlQualifiedName name)
		{
			XmlSchemaType xmlSchemaType = (XmlSchemaType)this.schema.SchemaTypes[name];
			if (xmlSchemaType != null)
			{
				if (xmlSchemaType is XmlSchemaComplexType)
				{
					this.CompileComplexType((XmlSchemaComplexType)xmlSchemaType);
				}
				else
				{
					this.CompileSimpleType((XmlSchemaSimpleType)xmlSchemaType);
				}
				return xmlSchemaType;
			}
			return DatatypeImplementation.GetSimpleTypeFromXsdType(name);
		}

		// Token: 0x04000EF4 RID: 3828
		private bool compileContentModel;

		// Token: 0x04000EF5 RID: 3829
		private XmlSchemaObjectTable examplars = new XmlSchemaObjectTable();

		// Token: 0x04000EF6 RID: 3830
		private Stack complexTypeStack = new Stack();

		// Token: 0x04000EF7 RID: 3831
		private XmlSchema schema;
	}
}
