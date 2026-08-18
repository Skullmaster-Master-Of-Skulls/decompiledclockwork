using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Schema
{
	// Token: 0x020001E3 RID: 483
	internal sealed class Compiler : BaseProcessor
	{
		// Token: 0x06002015 RID: 8213 RVA: 0x000ACC00 File Offset: 0x000AAE00
		public Compiler(XmlNameTable nameTable, ValidationEventHandler eventHandler, XmlSchema schemaForSchema, XmlSchemaCompilationSettings compilationSettings) : base(nameTable, null, eventHandler, compilationSettings)
		{
			this.schemaForSchema = schemaForSchema;
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x000ACC98 File Offset: 0x000AAE98
		public bool Execute(XmlSchemaSet schemaSet, SchemaInfo schemaCompiledInfo)
		{
			this.Compile();
			if (!base.HasErrors)
			{
				this.Output(schemaCompiledInfo);
				schemaSet.elements = this.elements;
				schemaSet.attributes = this.attributes;
				schemaSet.schemaTypes = this.schemaTypes;
				schemaSet.substitutionGroups = this.examplars;
			}
			return !base.HasErrors;
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x000ACCF4 File Offset: 0x000AAEF4
		internal void Prepare(XmlSchema schema, bool cleanup)
		{
			if (this.schemasToCompile[schema] != null)
			{
				return;
			}
			this.schemasToCompile.Add(schema, schema);
			foreach (object obj in schema.Elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (cleanup)
				{
					this.CleanupElement(xmlSchemaElement);
				}
				base.AddToTable(this.elements, xmlSchemaElement.QualifiedName, xmlSchemaElement);
			}
			foreach (object obj2 in schema.Attributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj2;
				if (cleanup)
				{
					this.CleanupAttribute(xmlSchemaAttribute);
				}
				base.AddToTable(this.attributes, xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute);
			}
			foreach (object obj3 in schema.Groups.Values)
			{
				XmlSchemaGroup xmlSchemaGroup = (XmlSchemaGroup)obj3;
				if (cleanup)
				{
					this.CleanupGroup(xmlSchemaGroup);
				}
				base.AddToTable(this.groups, xmlSchemaGroup.QualifiedName, xmlSchemaGroup);
			}
			foreach (object obj4 in schema.AttributeGroups.Values)
			{
				XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)obj4;
				if (cleanup)
				{
					this.CleanupAttributeGroup(xmlSchemaAttributeGroup);
				}
				base.AddToTable(this.attributeGroups, xmlSchemaAttributeGroup.QualifiedName, xmlSchemaAttributeGroup);
			}
			foreach (object obj5 in schema.SchemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj5;
				if (cleanup)
				{
					XmlSchemaComplexType xmlSchemaComplexType = xmlSchemaType as XmlSchemaComplexType;
					if (xmlSchemaComplexType != null)
					{
						this.CleanupComplexType(xmlSchemaComplexType);
					}
					else
					{
						this.CleanupSimpleType(xmlSchemaType as XmlSchemaSimpleType);
					}
				}
				base.AddToTable(this.schemaTypes, xmlSchemaType.QualifiedName, xmlSchemaType);
			}
			foreach (object obj6 in schema.Notations.Values)
			{
				XmlSchemaNotation xmlSchemaNotation = (XmlSchemaNotation)obj6;
				base.AddToTable(this.notations, xmlSchemaNotation.QualifiedName, xmlSchemaNotation);
			}
			foreach (object obj7 in schema.IdentityConstraints.Values)
			{
				XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)obj7;
				base.AddToTable(this.identityConstraints, xmlSchemaIdentityConstraint.QualifiedName, xmlSchemaIdentityConstraint);
			}
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x000AD008 File Offset: 0x000AB208
		private void UpdateSForSSimpleTypes()
		{
			XmlSchemaSimpleType[] builtInTypes = DatatypeImplementation.GetBuiltInTypes();
			int num = builtInTypes.Length - 3;
			for (int i = 12; i < num; i++)
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = builtInTypes[i];
				this.schemaForSchema.SchemaTypes.Replace(xmlSchemaSimpleType.QualifiedName, xmlSchemaSimpleType);
				this.schemaTypes.Replace(xmlSchemaSimpleType.QualifiedName, xmlSchemaSimpleType);
			}
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x000AD05C File Offset: 0x000AB25C
		private void Output(SchemaInfo schemaInfo)
		{
			foreach (object obj in this.schemasToCompile.Values)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				string text = xmlSchema.TargetNamespace;
				if (text == null)
				{
					text = string.Empty;
				}
				schemaInfo.TargetNamespaces[text] = true;
			}
			foreach (object obj2 in this.elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj2;
				schemaInfo.ElementDecls.Add(xmlSchemaElement.QualifiedName, xmlSchemaElement.ElementDecl);
			}
			foreach (object obj3 in this.attributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj3;
				schemaInfo.AttributeDecls.Add(xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute.AttDef);
			}
			foreach (object obj4 in this.schemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj4;
				schemaInfo.ElementDeclsByType.Add(xmlSchemaType.QualifiedName, xmlSchemaType.ElementDecl);
			}
			foreach (object obj5 in this.notations.Values)
			{
				XmlSchemaNotation xmlSchemaNotation = (XmlSchemaNotation)obj5;
				SchemaNotation schemaNotation = new SchemaNotation(xmlSchemaNotation.QualifiedName);
				schemaNotation.SystemLiteral = xmlSchemaNotation.System;
				schemaNotation.Pubid = xmlSchemaNotation.Public;
				if (!schemaInfo.Notations.ContainsKey(schemaNotation.Name.Name))
				{
					schemaInfo.Notations.Add(schemaNotation.Name.Name, schemaNotation);
				}
			}
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000AD2A4 File Offset: 0x000AB4A4
		internal void ImportAllCompiledSchemas(XmlSchemaSet schemaSet)
		{
			SortedList sortedSchemas = schemaSet.SortedSchemas;
			for (int i = 0; i < sortedSchemas.Count; i++)
			{
				XmlSchema xmlSchema = (XmlSchema)sortedSchemas.GetByIndex(i);
				if (xmlSchema.IsCompiledBySet)
				{
					this.Prepare(xmlSchema, false);
				}
			}
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x000AD2E8 File Offset: 0x000AB4E8
		internal bool Compile()
		{
			this.schemaTypes.Insert(DatatypeImplementation.QnAnyType, XmlSchemaComplexType.AnyType);
			if (this.schemaForSchema != null)
			{
				this.schemaForSchema.SchemaTypes.Replace(DatatypeImplementation.QnAnyType, XmlSchemaComplexType.AnyType);
				this.UpdateSForSSimpleTypes();
			}
			foreach (object obj in this.groups.Values)
			{
				XmlSchemaGroup group = (XmlSchemaGroup)obj;
				this.CompileGroup(group);
			}
			foreach (object obj2 in this.attributeGroups.Values)
			{
				XmlSchemaAttributeGroup attributeGroup = (XmlSchemaAttributeGroup)obj2;
				this.CompileAttributeGroup(attributeGroup);
			}
			foreach (object obj3 in this.schemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj3;
				XmlSchemaComplexType xmlSchemaComplexType = xmlSchemaType as XmlSchemaComplexType;
				if (xmlSchemaComplexType != null)
				{
					this.CompileComplexType(xmlSchemaComplexType);
				}
				else
				{
					this.CompileSimpleType((XmlSchemaSimpleType)xmlSchemaType);
				}
			}
			foreach (object obj4 in this.elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj4;
				if (xmlSchemaElement.ElementDecl == null)
				{
					this.CompileElement(xmlSchemaElement);
				}
			}
			foreach (object obj5 in this.attributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj5;
				if (xmlSchemaAttribute.AttDef == null)
				{
					this.CompileAttribute(xmlSchemaAttribute);
				}
			}
			using (IEnumerator enumerator6 = this.identityConstraints.Values.GetEnumerator())
			{
				while (enumerator6.MoveNext())
				{
					object obj6 = enumerator6.Current;
					XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)obj6;
					if (xmlSchemaIdentityConstraint.CompiledConstraint == null)
					{
						this.CompileIdentityConstraint(xmlSchemaIdentityConstraint);
					}
				}
				goto IL_22E;
			}
			IL_214:
			XmlSchemaComplexType complexType = (XmlSchemaComplexType)this.complexTypeStack.Pop();
			this.CompileComplexTypeElements(complexType);
			IL_22E:
			if (this.complexTypeStack.Count <= 0)
			{
				this.ProcessSubstitutionGroups();
				foreach (object obj7 in this.schemaTypes.Values)
				{
					XmlSchemaType xmlSchemaType2 = (XmlSchemaType)obj7;
					XmlSchemaComplexType xmlSchemaComplexType2 = xmlSchemaType2 as XmlSchemaComplexType;
					if (xmlSchemaComplexType2 != null)
					{
						this.CheckParticleDerivation(xmlSchemaComplexType2);
					}
				}
				foreach (object obj8 in this.elements.Values)
				{
					XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)obj8;
					XmlSchemaComplexType xmlSchemaComplexType3 = xmlSchemaElement2.ElementSchemaType as XmlSchemaComplexType;
					if (xmlSchemaComplexType3 != null && xmlSchemaElement2.SchemaTypeName == XmlQualifiedName.Empty)
					{
						this.CheckParticleDerivation(xmlSchemaComplexType3);
					}
				}
				foreach (object obj9 in this.groups.Values)
				{
					XmlSchemaGroup xmlSchemaGroup = (XmlSchemaGroup)obj9;
					XmlSchemaGroup redefined = xmlSchemaGroup.Redefined;
					if (redefined != null)
					{
						this.RecursivelyCheckRedefinedGroups(xmlSchemaGroup, redefined);
					}
				}
				foreach (object obj10 in this.attributeGroups.Values)
				{
					XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)obj10;
					XmlSchemaAttributeGroup redefined2 = xmlSchemaAttributeGroup.Redefined;
					if (redefined2 != null)
					{
						this.RecursivelyCheckRedefinedAttributeGroups(xmlSchemaAttributeGroup, redefined2);
					}
				}
				return !base.HasErrors;
			}
			goto IL_214;
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x000AD728 File Offset: 0x000AB928
		private void CleanupAttribute(XmlSchemaAttribute attribute)
		{
			if (attribute.SchemaType != null)
			{
				this.CleanupSimpleType(attribute.SchemaType);
			}
			attribute.AttDef = null;
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x000AD745 File Offset: 0x000AB945
		private void CleanupAttributeGroup(XmlSchemaAttributeGroup attributeGroup)
		{
			this.CleanupAttributes(attributeGroup.Attributes);
			attributeGroup.AttributeUses.Clear();
			attributeGroup.AttributeWildcard = null;
			if (attributeGroup.Redefined != null)
			{
				this.CleanupAttributeGroup(attributeGroup.Redefined);
			}
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x000AD77C File Offset: 0x000AB97C
		private void CleanupComplexType(XmlSchemaComplexType complexType)
		{
			if (complexType.QualifiedName == DatatypeImplementation.QnAnyType)
			{
				return;
			}
			if (complexType.ContentModel != null)
			{
				if (complexType.ContentModel is XmlSchemaSimpleContent)
				{
					XmlSchemaSimpleContent xmlSchemaSimpleContent = (XmlSchemaSimpleContent)complexType.ContentModel;
					if (xmlSchemaSimpleContent.Content is XmlSchemaSimpleContentExtension)
					{
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = (XmlSchemaSimpleContentExtension)xmlSchemaSimpleContent.Content;
						this.CleanupAttributes(xmlSchemaSimpleContentExtension.Attributes);
					}
					else
					{
						XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContent.Content;
						this.CleanupAttributes(xmlSchemaSimpleContentRestriction.Attributes);
					}
				}
				else
				{
					XmlSchemaComplexContent xmlSchemaComplexContent = (XmlSchemaComplexContent)complexType.ContentModel;
					if (xmlSchemaComplexContent.Content is XmlSchemaComplexContentExtension)
					{
						XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = (XmlSchemaComplexContentExtension)xmlSchemaComplexContent.Content;
						this.CleanupParticle(xmlSchemaComplexContentExtension.Particle);
						this.CleanupAttributes(xmlSchemaComplexContentExtension.Attributes);
					}
					else
					{
						XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = (XmlSchemaComplexContentRestriction)xmlSchemaComplexContent.Content;
						this.CleanupParticle(xmlSchemaComplexContentRestriction.Particle);
						this.CleanupAttributes(xmlSchemaComplexContentRestriction.Attributes);
					}
				}
			}
			else
			{
				this.CleanupParticle(complexType.Particle);
				this.CleanupAttributes(complexType.Attributes);
			}
			complexType.LocalElements.Clear();
			complexType.AttributeUses.Clear();
			complexType.SetAttributeWildcard(null);
			complexType.SetContentTypeParticle(XmlSchemaParticle.Empty);
			complexType.ElementDecl = null;
			complexType.HasWildCard = false;
			if (complexType.Redefined != null)
			{
				this.CleanupComplexType(complexType.Redefined as XmlSchemaComplexType);
			}
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x000AD8D9 File Offset: 0x000ABAD9
		private void CleanupSimpleType(XmlSchemaSimpleType simpleType)
		{
			if (simpleType == XmlSchemaType.GetBuiltInSimpleType(simpleType.TypeCode))
			{
				return;
			}
			simpleType.ElementDecl = null;
			if (simpleType.Redefined != null)
			{
				this.CleanupSimpleType(simpleType.Redefined as XmlSchemaSimpleType);
			}
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x000AD90C File Offset: 0x000ABB0C
		private void CleanupElement(XmlSchemaElement element)
		{
			if (element.SchemaType != null)
			{
				XmlSchemaComplexType xmlSchemaComplexType = element.SchemaType as XmlSchemaComplexType;
				if (xmlSchemaComplexType != null)
				{
					this.CleanupComplexType(xmlSchemaComplexType);
				}
				else
				{
					this.CleanupSimpleType((XmlSchemaSimpleType)element.SchemaType);
				}
			}
			for (int i = 0; i < element.Constraints.Count; i++)
			{
				((XmlSchemaIdentityConstraint)element.Constraints[i]).CompiledConstraint = null;
			}
			element.ElementDecl = null;
			element.IsLocalTypeDerivationChecked = false;
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x000AD988 File Offset: 0x000ABB88
		private void CleanupAttributes(XmlSchemaObjectCollection attributes)
		{
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlSchemaAttribute xmlSchemaAttribute = attributes[i] as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					this.CleanupAttribute(xmlSchemaAttribute);
				}
			}
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x000AD9BD File Offset: 0x000ABBBD
		private void CleanupGroup(XmlSchemaGroup group)
		{
			this.CleanupParticle(group.Particle);
			group.CanonicalParticle = null;
			if (group.Redefined != null)
			{
				this.CleanupGroup(group.Redefined);
			}
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x000AD9E8 File Offset: 0x000ABBE8
		private void CleanupParticle(XmlSchemaParticle particle)
		{
			XmlSchemaElement xmlSchemaElement = particle as XmlSchemaElement;
			if (xmlSchemaElement != null)
			{
				this.CleanupElement(xmlSchemaElement);
				return;
			}
			XmlSchemaGroupBase xmlSchemaGroupBase = particle as XmlSchemaGroupBase;
			if (xmlSchemaGroupBase != null)
			{
				for (int i = 0; i < xmlSchemaGroupBase.Items.Count; i++)
				{
					this.CleanupParticle((XmlSchemaParticle)xmlSchemaGroupBase.Items[i]);
				}
			}
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x000ADA40 File Offset: 0x000ABC40
		private void ProcessSubstitutionGroups()
		{
			foreach (object obj in this.elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (!xmlSchemaElement.SubstitutionGroup.IsEmpty)
				{
					XmlSchemaElement xmlSchemaElement2 = this.elements[xmlSchemaElement.SubstitutionGroup] as XmlSchemaElement;
					if (xmlSchemaElement2 == null)
					{
						base.SendValidationEvent("Sch_NoExamplar", xmlSchemaElement);
					}
					else
					{
						if (!XmlSchemaType.IsDerivedFrom(xmlSchemaElement.ElementSchemaType, xmlSchemaElement2.ElementSchemaType, xmlSchemaElement2.FinalResolved))
						{
							base.SendValidationEvent("Sch_InvalidSubstitutionMember", xmlSchemaElement.QualifiedName.ToString(), xmlSchemaElement2.QualifiedName.ToString(), xmlSchemaElement);
						}
						XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)this.examplars[xmlSchemaElement.SubstitutionGroup];
						if (xmlSchemaSubstitutionGroup == null)
						{
							xmlSchemaSubstitutionGroup = new XmlSchemaSubstitutionGroup();
							xmlSchemaSubstitutionGroup.Examplar = xmlSchemaElement.SubstitutionGroup;
							this.examplars.Add(xmlSchemaElement.SubstitutionGroup, xmlSchemaSubstitutionGroup);
						}
						ArrayList members = xmlSchemaSubstitutionGroup.Members;
						if (!members.Contains(xmlSchemaElement))
						{
							members.Add(xmlSchemaElement);
						}
					}
				}
			}
			foreach (object obj2 in this.examplars.Values)
			{
				XmlSchemaSubstitutionGroup substitutionGroup = (XmlSchemaSubstitutionGroup)obj2;
				this.CompileSubstitutionGroup(substitutionGroup);
			}
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x000ADBC8 File Offset: 0x000ABDC8
		private void CompileSubstitutionGroup(XmlSchemaSubstitutionGroup substitutionGroup)
		{
			if (substitutionGroup.IsProcessing && substitutionGroup.Members.Count > 0)
			{
				base.SendValidationEvent("Sch_SubstitutionCircularRef", (XmlSchemaElement)substitutionGroup.Members[0]);
				return;
			}
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.elements[substitutionGroup.Examplar];
			if (substitutionGroup.Members.Contains(xmlSchemaElement))
			{
				return;
			}
			substitutionGroup.IsProcessing = true;
			try
			{
				if (xmlSchemaElement.FinalResolved == XmlSchemaDerivationMethod.All)
				{
					base.SendValidationEvent("Sch_InvalidExamplar", xmlSchemaElement);
				}
				ArrayList arrayList = null;
				for (int i = 0; i < substitutionGroup.Members.Count; i++)
				{
					XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)substitutionGroup.Members[i];
					if ((xmlSchemaElement2.ElementDecl.Block & XmlSchemaDerivationMethod.Substitution) == XmlSchemaDerivationMethod.Empty)
					{
						XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)this.examplars[xmlSchemaElement2.QualifiedName];
						if (xmlSchemaSubstitutionGroup != null)
						{
							this.CompileSubstitutionGroup(xmlSchemaSubstitutionGroup);
							for (int j = 0; j < xmlSchemaSubstitutionGroup.Members.Count; j++)
							{
								if (xmlSchemaSubstitutionGroup.Members[j] != xmlSchemaElement2)
								{
									if (arrayList == null)
									{
										arrayList = new ArrayList();
									}
									arrayList.Add(xmlSchemaSubstitutionGroup.Members[j]);
								}
							}
						}
					}
				}
				if (arrayList != null)
				{
					for (int k = 0; k < arrayList.Count; k++)
					{
						substitutionGroup.Members.Add(arrayList[k]);
					}
				}
				substitutionGroup.Members.Add(xmlSchemaElement);
			}
			finally
			{
				substitutionGroup.IsProcessing = false;
			}
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000ADD5C File Offset: 0x000ABF5C
		private void RecursivelyCheckRedefinedGroups(XmlSchemaGroup redefinedGroup, XmlSchemaGroup baseGroup)
		{
			if (baseGroup.Redefined != null)
			{
				this.RecursivelyCheckRedefinedGroups(baseGroup, baseGroup.Redefined);
			}
			if (redefinedGroup.SelfReferenceCount == 0)
			{
				if (baseGroup.CanonicalParticle == null)
				{
					baseGroup.CanonicalParticle = this.CannonicalizeParticle(baseGroup.Particle, true);
				}
				if (redefinedGroup.CanonicalParticle == null)
				{
					redefinedGroup.CanonicalParticle = this.CannonicalizeParticle(redefinedGroup.Particle, true);
				}
				this.CompileParticleElements(redefinedGroup.CanonicalParticle);
				this.CompileParticleElements(baseGroup.CanonicalParticle);
				this.CheckParticleDerivation(redefinedGroup.CanonicalParticle, baseGroup.CanonicalParticle);
			}
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x000ADDE6 File Offset: 0x000ABFE6
		private void RecursivelyCheckRedefinedAttributeGroups(XmlSchemaAttributeGroup attributeGroup, XmlSchemaAttributeGroup baseAttributeGroup)
		{
			if (baseAttributeGroup.Redefined != null)
			{
				this.RecursivelyCheckRedefinedAttributeGroups(baseAttributeGroup, baseAttributeGroup.Redefined);
			}
			if (attributeGroup.SelfReferenceCount == 0)
			{
				this.CompileAttributeGroup(baseAttributeGroup);
				this.CompileAttributeGroup(attributeGroup);
				this.CheckAtrributeGroupRestriction(baseAttributeGroup, attributeGroup);
			}
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x000ADE1C File Offset: 0x000AC01C
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
				group.CanonicalParticle = this.CannonicalizeParticle(group.Particle, true);
			}
			group.IsProcessing = false;
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x000ADE74 File Offset: 0x000AC074
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
							throw new XmlSchemaException("Sch_UndeclaredSimpleType", xmlSchemaSimpleTypeList.ItemTypeName.ToString(), xmlSchemaSimpleTypeList);
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
							XmlSchema parentSchema = Preprocessor.GetParentSchema(simpleType);
							if (parentSchema.TargetNamespace != "http://www.w3.org/2001/XMLSchema")
							{
								throw new XmlSchemaException("Sch_InvalidSimpleTypeRestriction", xmlSchemaSimpleTypeRestriction.BaseTypeName.ToString(), simpleType);
							}
						}
						XmlSchemaSimpleType simpleType3 = this.GetSimpleType(xmlSchemaSimpleTypeRestriction.BaseTypeName);
						if (simpleType3 == null)
						{
							throw new XmlSchemaException("Sch_UndeclaredSimpleType", xmlSchemaSimpleTypeRestriction.BaseTypeName.ToString(), xmlSchemaSimpleTypeRestriction);
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

		// Token: 0x0600202A RID: 8234 RVA: 0x000AE18C File Offset: 0x000AC38C
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
						throw new XmlSchemaException("Sch_UndeclaredSimpleType", memberTypes[i].ToString(), xmlSchemaSimpleTypeUnion);
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

		// Token: 0x0600202B RID: 8235 RVA: 0x000AE2A4 File Offset: 0x000AC4A4
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

		// Token: 0x0600202C RID: 8236 RVA: 0x000AE2F8 File Offset: 0x000AC4F8
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
			try
			{
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
					complexType.SetContentTypeParticle(this.CompileContentTypeParticle(complexType.Particle));
					complexType.SetContentType(this.GetSchemaContentType(complexType, null, complexType.ContentTypeParticle));
				}
				if (complexType.ContainsIdAttribute(true))
				{
					base.SendValidationEvent("Sch_TwoIdAttrUses", complexType);
				}
				SchemaElementDecl schemaElementDecl = new SchemaElementDecl();
				schemaElementDecl.ContentValidator = this.CompileComplexContent(complexType);
				schemaElementDecl.SchemaType = complexType;
				schemaElementDecl.IsAbstract = complexType.IsAbstract;
				schemaElementDecl.Datatype = complexType.Datatype;
				schemaElementDecl.Block = complexType.BlockResolved;
				schemaElementDecl.AnyAttribute = complexType.AttributeWildcard;
				foreach (object obj in complexType.AttributeUses.Values)
				{
					XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj;
					if (xmlSchemaAttribute.Use == XmlSchemaUse.Prohibited)
					{
						if (!schemaElementDecl.ProhibitedAttributes.ContainsKey(xmlSchemaAttribute.QualifiedName))
						{
							schemaElementDecl.ProhibitedAttributes.Add(xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute.QualifiedName);
						}
					}
					else if (!schemaElementDecl.AttDefs.ContainsKey(xmlSchemaAttribute.QualifiedName) && xmlSchemaAttribute.AttDef != null && xmlSchemaAttribute.AttDef.Name != XmlQualifiedName.Empty && xmlSchemaAttribute.AttDef != SchemaAttDef.Empty)
					{
						schemaElementDecl.AddAttDef(xmlSchemaAttribute.AttDef);
					}
				}
				complexType.ElementDecl = schemaElementDecl;
			}
			finally
			{
				complexType.IsProcessing = false;
			}
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x000AE598 File Offset: 0x000AC798
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
					base.SendValidationEvent("Sch_UndeclaredType", simpleExtension.BaseTypeName.ToString(), simpleExtension);
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

		// Token: 0x0600202E RID: 8238 RVA: 0x000AE67C File Offset: 0x000AC87C
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

		// Token: 0x0600202F RID: 8239 RVA: 0x000AE854 File Offset: 0x000ACA54
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
			if ((xmlSchemaComplexType.FinalResolved & XmlSchemaDerivationMethod.Extension) != XmlSchemaDerivationMethod.Empty)
			{
				base.SendValidationEvent("Sch_BaseFinalExtension", complexType);
			}
			this.CompileLocalAttributes(xmlSchemaComplexType, complexType, complexExtension.Attributes, complexExtension.AnyAttribute, XmlSchemaDerivationMethod.Extension);
			XmlSchemaParticle contentTypeParticle = xmlSchemaComplexType.ContentTypeParticle;
			XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle(complexExtension.Particle, true);
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
					}));
				}
				else
				{
					complexType.SetContentTypeParticle(contentTypeParticle);
				}
			}
			else
			{
				complexType.SetContentTypeParticle(xmlSchemaParticle);
			}
			XmlSchemaContentType xmlSchemaContentType = this.GetSchemaContentType(complexType, complexContent, xmlSchemaParticle);
			if (xmlSchemaContentType == XmlSchemaContentType.Empty)
			{
				xmlSchemaContentType = xmlSchemaComplexType.ContentType;
				if (xmlSchemaContentType == XmlSchemaContentType.TextOnly)
				{
					complexType.SetDatatype(xmlSchemaComplexType.Datatype);
				}
			}
			complexType.SetContentType(xmlSchemaContentType);
			if (xmlSchemaComplexType.ContentType != XmlSchemaContentType.Empty && complexType.ContentType != xmlSchemaComplexType.ContentType)
			{
				base.SendValidationEvent("Sch_DifContentType", complexType);
				return;
			}
			complexType.SetBaseSchemaType(xmlSchemaComplexType);
			complexType.SetDerivedBy(XmlSchemaDerivationMethod.Extension);
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x000AE9B0 File Offset: 0x000ACBB0
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
			complexType.SetBaseSchemaType(xmlSchemaComplexType);
			if ((xmlSchemaComplexType.FinalResolved & XmlSchemaDerivationMethod.Restriction) != XmlSchemaDerivationMethod.Empty)
			{
				base.SendValidationEvent("Sch_BaseFinalRestriction", complexType);
			}
			this.CompileLocalAttributes(xmlSchemaComplexType, complexType, complexRestriction.Attributes, complexRestriction.AnyAttribute, XmlSchemaDerivationMethod.Restriction);
			complexType.SetContentTypeParticle(this.CompileContentTypeParticle(complexRestriction.Particle));
			XmlSchemaContentType schemaContentType = this.GetSchemaContentType(complexType, complexContent, complexType.ContentTypeParticle);
			complexType.SetContentType(schemaContentType);
			if (schemaContentType != XmlSchemaContentType.Empty)
			{
				if (schemaContentType == XmlSchemaContentType.Mixed)
				{
					if (xmlSchemaComplexType.ContentType != XmlSchemaContentType.Mixed)
					{
						base.SendValidationEvent("Sch_InvalidContentRestrictionDetailed", Res.GetString("Sch_InvalidBaseToMixed"), complexType);
					}
				}
			}
			else if (xmlSchemaComplexType.ElementDecl != null && !xmlSchemaComplexType.ElementDecl.ContentValidator.IsEmptiable)
			{
				base.SendValidationEvent("Sch_InvalidContentRestrictionDetailed", Res.GetString("Sch_InvalidBaseToEmpty"), complexType);
			}
			complexType.SetDerivedBy(XmlSchemaDerivationMethod.Restriction);
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x000AEAD8 File Offset: 0x000ACCD8
		private void CheckParticleDerivation(XmlSchemaComplexType complexType)
		{
			XmlSchemaComplexType xmlSchemaComplexType = complexType.BaseXmlSchemaType as XmlSchemaComplexType;
			this.restrictionErrorMsg = null;
			if (xmlSchemaComplexType != null && xmlSchemaComplexType != XmlSchemaComplexType.AnyType && complexType.DerivedBy == XmlSchemaDerivationMethod.Restriction)
			{
				XmlSchemaParticle derivedParticle = this.CannonicalizePointlessRoot(complexType.ContentTypeParticle);
				XmlSchemaParticle baseParticle = this.CannonicalizePointlessRoot(xmlSchemaComplexType.ContentTypeParticle);
				if (!this.IsValidRestriction(derivedParticle, baseParticle))
				{
					if (this.restrictionErrorMsg != null)
					{
						base.SendValidationEvent("Sch_InvalidParticleRestrictionDetailed", this.restrictionErrorMsg, complexType);
						return;
					}
					base.SendValidationEvent("Sch_InvalidParticleRestriction", complexType);
					return;
				}
			}
			else if (xmlSchemaComplexType == XmlSchemaComplexType.AnyType)
			{
				foreach (object obj in complexType.LocalElements.Values)
				{
					XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
					if (!xmlSchemaElement.IsLocalTypeDerivationChecked)
					{
						XmlSchemaComplexType xmlSchemaComplexType2 = xmlSchemaElement.ElementSchemaType as XmlSchemaComplexType;
						if (xmlSchemaComplexType2 != null && xmlSchemaElement.SchemaTypeName == XmlQualifiedName.Empty && xmlSchemaElement.RefName == XmlQualifiedName.Empty)
						{
							xmlSchemaElement.IsLocalTypeDerivationChecked = true;
							this.CheckParticleDerivation(xmlSchemaComplexType2);
						}
					}
				}
			}
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x000AEC08 File Offset: 0x000ACE08
		private void CheckParticleDerivation(XmlSchemaParticle derivedParticle, XmlSchemaParticle baseParticle)
		{
			this.restrictionErrorMsg = null;
			derivedParticle = this.CannonicalizePointlessRoot(derivedParticle);
			baseParticle = this.CannonicalizePointlessRoot(baseParticle);
			if (!this.IsValidRestriction(derivedParticle, baseParticle))
			{
				if (this.restrictionErrorMsg != null)
				{
					base.SendValidationEvent("Sch_InvalidParticleRestrictionDetailed", this.restrictionErrorMsg, derivedParticle);
					return;
				}
				base.SendValidationEvent("Sch_InvalidParticleRestriction", derivedParticle);
			}
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x000AEC60 File Offset: 0x000ACE60
		private XmlSchemaParticle CompileContentTypeParticle(XmlSchemaParticle particle)
		{
			XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle(particle, true);
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

		// Token: 0x06002034 RID: 8244 RVA: 0x000AECB4 File Offset: 0x000ACEB4
		private XmlSchemaParticle CannonicalizeParticle(XmlSchemaParticle particle, bool root)
		{
			if (particle == null || particle.IsEmpty)
			{
				return XmlSchemaParticle.Empty;
			}
			if (particle is XmlSchemaElement)
			{
				return particle;
			}
			if (particle is XmlSchemaGroupRef)
			{
				return this.CannonicalizeGroupRef((XmlSchemaGroupRef)particle, root);
			}
			if (particle is XmlSchemaAll)
			{
				return this.CannonicalizeAll((XmlSchemaAll)particle, root);
			}
			if (particle is XmlSchemaChoice)
			{
				return this.CannonicalizeChoice((XmlSchemaChoice)particle, root);
			}
			if (particle is XmlSchemaSequence)
			{
				return this.CannonicalizeSequence((XmlSchemaSequence)particle, root);
			}
			return particle;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x000AED38 File Offset: 0x000ACF38
		private XmlSchemaParticle CannonicalizeElement(XmlSchemaElement element)
		{
			if (element.RefName.IsEmpty || (element.ElementDecl.Block & XmlSchemaDerivationMethod.Substitution) != XmlSchemaDerivationMethod.Empty)
			{
				return element;
			}
			XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)this.examplars[element.QualifiedName];
			if (xmlSchemaSubstitutionGroup == null)
			{
				return element;
			}
			XmlSchemaChoice xmlSchemaChoice = new XmlSchemaChoice();
			for (int i = 0; i < xmlSchemaSubstitutionGroup.Members.Count; i++)
			{
				xmlSchemaChoice.Items.Add((XmlSchemaElement)xmlSchemaSubstitutionGroup.Members[i]);
			}
			xmlSchemaChoice.MinOccurs = element.MinOccurs;
			xmlSchemaChoice.MaxOccurs = element.MaxOccurs;
			this.CopyPosition(xmlSchemaChoice, element, false);
			return xmlSchemaChoice;
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x000AEDE0 File Offset: 0x000ACFE0
		private XmlSchemaParticle CannonicalizeGroupRef(XmlSchemaGroupRef groupRef, bool root)
		{
			XmlSchemaGroup xmlSchemaGroup;
			if (groupRef.Redefined != null)
			{
				xmlSchemaGroup = groupRef.Redefined;
			}
			else
			{
				xmlSchemaGroup = (XmlSchemaGroup)this.groups[groupRef.RefName];
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
				if (groupRef.MinOccurs > 1m || groupRef.MaxOccurs != 1m)
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
			this.CopyPosition(xmlSchemaGroupBase2, groupRef, true);
			for (int i = 0; i < xmlSchemaGroupBase.Items.Count; i++)
			{
				xmlSchemaGroupBase2.Items.Add(xmlSchemaGroupBase.Items[i]);
			}
			groupRef.SetParticle(xmlSchemaGroupBase2);
			return xmlSchemaGroupBase2;
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x000AEF70 File Offset: 0x000AD170
		private XmlSchemaParticle CannonicalizeAll(XmlSchemaAll all, bool root)
		{
			if (all.Items.Count > 0)
			{
				XmlSchemaAll xmlSchemaAll = new XmlSchemaAll();
				xmlSchemaAll.MinOccurs = all.MinOccurs;
				xmlSchemaAll.MaxOccurs = all.MaxOccurs;
				this.CopyPosition(xmlSchemaAll, all, true);
				for (int i = 0; i < all.Items.Count; i++)
				{
					XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle((XmlSchemaElement)all.Items[i], false);
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
			if (!root)
			{
				base.SendValidationEvent("Sch_NotAllAlone", all);
				return XmlSchemaParticle.Empty;
			}
			return all;
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x000AF024 File Offset: 0x000AD224
		private XmlSchemaParticle CannonicalizeChoice(XmlSchemaChoice choice, bool root)
		{
			XmlSchemaChoice source = choice;
			if (choice.Items.Count > 0)
			{
				XmlSchemaChoice xmlSchemaChoice = new XmlSchemaChoice();
				xmlSchemaChoice.MinOccurs = choice.MinOccurs;
				xmlSchemaChoice.MaxOccurs = choice.MaxOccurs;
				this.CopyPosition(xmlSchemaChoice, choice, true);
				for (int i = 0; i < choice.Items.Count; i++)
				{
					XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle((XmlSchemaParticle)choice.Items[i], false);
					if (xmlSchemaParticle != XmlSchemaParticle.Empty)
					{
						if (xmlSchemaParticle.MinOccurs == 1m && xmlSchemaParticle.MaxOccurs == 1m && xmlSchemaParticle is XmlSchemaChoice)
						{
							XmlSchemaChoice xmlSchemaChoice2 = xmlSchemaParticle as XmlSchemaChoice;
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

		// Token: 0x06002039 RID: 8249 RVA: 0x000AF1A0 File Offset: 0x000AD3A0
		private XmlSchemaParticle CannonicalizeSequence(XmlSchemaSequence sequence, bool root)
		{
			if (sequence.Items.Count > 0)
			{
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				xmlSchemaSequence.MinOccurs = sequence.MinOccurs;
				xmlSchemaSequence.MaxOccurs = sequence.MaxOccurs;
				this.CopyPosition(xmlSchemaSequence, sequence, true);
				for (int i = 0; i < sequence.Items.Count; i++)
				{
					XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle((XmlSchemaParticle)sequence.Items[i], false);
					if (xmlSchemaParticle != XmlSchemaParticle.Empty)
					{
						XmlSchemaSequence xmlSchemaSequence2 = xmlSchemaParticle as XmlSchemaSequence;
						if (xmlSchemaParticle.MinOccurs == 1m && xmlSchemaParticle.MaxOccurs == 1m && xmlSchemaSequence2 != null)
						{
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

		// Token: 0x0600203A RID: 8250 RVA: 0x000AF2F0 File Offset: 0x000AD4F0
		private XmlSchemaParticle CannonicalizePointlessRoot(XmlSchemaParticle particle)
		{
			if (particle == null)
			{
				return null;
			}
			decimal d = 1m;
			XmlSchemaSequence xmlSchemaSequence;
			XmlSchemaChoice xmlSchemaChoice;
			XmlSchemaAll xmlSchemaAll;
			if ((xmlSchemaSequence = (particle as XmlSchemaSequence)) != null)
			{
				XmlSchemaObjectCollection items = xmlSchemaSequence.Items;
				int count = items.Count;
				if (count == 1 && xmlSchemaSequence.MinOccurs == d && xmlSchemaSequence.MaxOccurs == d)
				{
					return (XmlSchemaParticle)items[0];
				}
			}
			else if ((xmlSchemaChoice = (particle as XmlSchemaChoice)) != null)
			{
				XmlSchemaObjectCollection items2 = xmlSchemaChoice.Items;
				int count2 = items2.Count;
				if (count2 == 1)
				{
					if (xmlSchemaChoice.MinOccurs == d && xmlSchemaChoice.MaxOccurs == d)
					{
						return (XmlSchemaParticle)items2[0];
					}
				}
				else if (count2 == 0)
				{
					return XmlSchemaParticle.Empty;
				}
			}
			else if ((xmlSchemaAll = (particle as XmlSchemaAll)) != null)
			{
				XmlSchemaObjectCollection items3 = xmlSchemaAll.Items;
				int count3 = items3.Count;
				if (count3 == 1 && xmlSchemaAll.MinOccurs == d && xmlSchemaAll.MaxOccurs == d)
				{
					return (XmlSchemaParticle)items3[0];
				}
			}
			return particle;
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x000AF3FC File Offset: 0x000AD5FC
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
			if (derivedParticle is XmlSchemaElement)
			{
				XmlSchemaElement element = (XmlSchemaElement)derivedParticle;
				derivedParticle = this.CannonicalizeElement(element);
			}
			if (baseParticle is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)baseParticle;
				XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeElement(xmlSchemaElement);
				if (xmlSchemaParticle is XmlSchemaChoice)
				{
					return this.IsValidRestriction(derivedParticle, xmlSchemaParticle);
				}
				if (derivedParticle is XmlSchemaElement)
				{
					return this.IsElementFromElement((XmlSchemaElement)derivedParticle, xmlSchemaElement);
				}
				this.restrictionErrorMsg = Res.GetString("Sch_ForbiddenDerivedParticleForElem");
				return false;
			}
			else if (baseParticle is XmlSchemaAny)
			{
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
			else if (baseParticle is XmlSchemaAll)
			{
				if (derivedParticle is XmlSchemaElement)
				{
					return this.IsElementFromGroupBase((XmlSchemaElement)derivedParticle, (XmlSchemaGroupBase)baseParticle);
				}
				if (derivedParticle is XmlSchemaAll)
				{
					if (this.IsGroupBaseFromGroupBase((XmlSchemaGroupBase)derivedParticle, (XmlSchemaGroupBase)baseParticle, true))
					{
						return true;
					}
				}
				else if (derivedParticle is XmlSchemaSequence)
				{
					if (this.IsSequenceFromAll((XmlSchemaSequence)derivedParticle, (XmlSchemaAll)baseParticle))
					{
						return true;
					}
					this.restrictionErrorMsg = Res.GetString("Sch_SeqFromAll", new object[]
					{
						derivedParticle.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
						derivedParticle.LinePosition.ToString(NumberFormatInfo.InvariantInfo),
						baseParticle.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
						baseParticle.LinePosition.ToString(NumberFormatInfo.InvariantInfo)
					});
				}
				else if (derivedParticle is XmlSchemaChoice || derivedParticle is XmlSchemaAny)
				{
					this.restrictionErrorMsg = Res.GetString("Sch_ForbiddenDerivedParticleForAll");
				}
				return false;
			}
			else if (baseParticle is XmlSchemaChoice)
			{
				if (derivedParticle is XmlSchemaElement)
				{
					return this.IsElementFromGroupBase((XmlSchemaElement)derivedParticle, (XmlSchemaGroupBase)baseParticle);
				}
				if (derivedParticle is XmlSchemaChoice)
				{
					XmlSchemaChoice xmlSchemaChoice = baseParticle as XmlSchemaChoice;
					XmlSchemaChoice xmlSchemaChoice2 = derivedParticle as XmlSchemaChoice;
					if (xmlSchemaChoice.Parent == null || xmlSchemaChoice2.Parent == null)
					{
						return this.IsChoiceFromChoiceSubstGroup(xmlSchemaChoice2, xmlSchemaChoice);
					}
					if (this.IsGroupBaseFromGroupBase(xmlSchemaChoice2, xmlSchemaChoice, false))
					{
						return true;
					}
				}
				else if (derivedParticle is XmlSchemaSequence)
				{
					if (this.IsSequenceFromChoice((XmlSchemaSequence)derivedParticle, (XmlSchemaChoice)baseParticle))
					{
						return true;
					}
					this.restrictionErrorMsg = Res.GetString("Sch_SeqFromChoice", new object[]
					{
						derivedParticle.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
						derivedParticle.LinePosition.ToString(NumberFormatInfo.InvariantInfo),
						baseParticle.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
						baseParticle.LinePosition.ToString(NumberFormatInfo.InvariantInfo)
					});
				}
				else
				{
					this.restrictionErrorMsg = Res.GetString("Sch_ForbiddenDerivedParticleForChoice");
				}
				return false;
			}
			else
			{
				if (!(baseParticle is XmlSchemaSequence))
				{
					return false;
				}
				if (derivedParticle is XmlSchemaElement)
				{
					return this.IsElementFromGroupBase((XmlSchemaElement)derivedParticle, (XmlSchemaGroupBase)baseParticle);
				}
				if (derivedParticle is XmlSchemaSequence || (derivedParticle is XmlSchemaAll && ((XmlSchemaGroupBase)derivedParticle).Items.Count == 1))
				{
					if (this.IsGroupBaseFromGroupBase((XmlSchemaGroupBase)derivedParticle, (XmlSchemaGroupBase)baseParticle, true))
					{
						return true;
					}
				}
				else
				{
					this.restrictionErrorMsg = Res.GetString("Sch_ForbiddenDerivedParticleForSeq");
				}
				return false;
			}
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x000AF764 File Offset: 0x000AD964
		private bool IsElementFromElement(XmlSchemaElement derivedElement, XmlSchemaElement baseElement)
		{
			XmlSchemaDerivationMethod xmlSchemaDerivationMethod = (baseElement.ElementDecl.Block == XmlSchemaDerivationMethod.All) ? (XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction) : baseElement.ElementDecl.Block;
			XmlSchemaDerivationMethod xmlSchemaDerivationMethod2 = (derivedElement.ElementDecl.Block == XmlSchemaDerivationMethod.All) ? (XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction) : derivedElement.ElementDecl.Block;
			if (!(derivedElement.QualifiedName == baseElement.QualifiedName) || (!baseElement.IsNillable && derivedElement.IsNillable) || (!this.IsValidOccurrenceRangeRestriction(derivedElement, baseElement) || (baseElement.FixedValue != null && !this.IsFixedEqual(baseElement.ElementDecl, derivedElement.ElementDecl))) || (xmlSchemaDerivationMethod2 | xmlSchemaDerivationMethod) != xmlSchemaDerivationMethod2 || derivedElement.ElementSchemaType == null || baseElement.ElementSchemaType == null || !XmlSchemaType.IsDerivedFrom(derivedElement.ElementSchemaType, baseElement.ElementSchemaType, ~(XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union)))
			{
				this.restrictionErrorMsg = Res.GetString("Sch_ElementFromElement", new object[]
				{
					derivedElement.QualifiedName,
					baseElement.QualifiedName
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x000AF854 File Offset: 0x000ADA54
		private bool IsElementFromAny(XmlSchemaElement derivedElement, XmlSchemaAny baseAny)
		{
			if (!baseAny.Allows(derivedElement.QualifiedName))
			{
				this.restrictionErrorMsg = Res.GetString("Sch_ElementFromAnyRule1", new object[]
				{
					derivedElement.QualifiedName.ToString()
				});
				return false;
			}
			if (!this.IsValidOccurrenceRangeRestriction(derivedElement, baseAny))
			{
				this.restrictionErrorMsg = Res.GetString("Sch_ElementFromAnyRule2", new object[]
				{
					derivedElement.QualifiedName.ToString()
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000AF8C8 File Offset: 0x000ADAC8
		private bool IsAnyFromAny(XmlSchemaAny derivedAny, XmlSchemaAny baseAny)
		{
			if (!this.IsValidOccurrenceRangeRestriction(derivedAny, baseAny))
			{
				this.restrictionErrorMsg = Res.GetString("Sch_AnyFromAnyRule1");
				return false;
			}
			if (!NamespaceList.IsSubset(derivedAny.NamespaceList, baseAny.NamespaceList))
			{
				this.restrictionErrorMsg = Res.GetString("Sch_AnyFromAnyRule2");
				return false;
			}
			if (derivedAny.ProcessContentsCorrect < baseAny.ProcessContentsCorrect)
			{
				this.restrictionErrorMsg = Res.GetString("Sch_AnyFromAnyRule3");
				return false;
			}
			return true;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x000AF938 File Offset: 0x000ADB38
		private bool IsGroupBaseFromAny(XmlSchemaGroupBase derivedGroupBase, XmlSchemaAny baseAny)
		{
			decimal minOccurs;
			decimal maxOccurs;
			this.CalculateEffectiveTotalRange(derivedGroupBase, out minOccurs, out maxOccurs);
			if (!this.IsValidOccurrenceRangeRestriction(minOccurs, maxOccurs, baseAny.MinOccurs, baseAny.MaxOccurs))
			{
				this.restrictionErrorMsg = Res.GetString("Sch_GroupBaseFromAny2", new object[]
				{
					derivedGroupBase.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					derivedGroupBase.LinePosition.ToString(NumberFormatInfo.InvariantInfo),
					baseAny.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					baseAny.LinePosition.ToString(NumberFormatInfo.InvariantInfo)
				});
				return false;
			}
			string minOccursString = baseAny.MinOccursString;
			baseAny.MinOccurs = 0m;
			for (int i = 0; i < derivedGroupBase.Items.Count; i++)
			{
				if (!this.IsValidRestriction((XmlSchemaParticle)derivedGroupBase.Items[i], baseAny))
				{
					this.restrictionErrorMsg = Res.GetString("Sch_GroupBaseFromAny1");
					baseAny.MinOccursString = minOccursString;
					return false;
				}
			}
			baseAny.MinOccursString = minOccursString;
			return true;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x000AFA40 File Offset: 0x000ADC40
		private bool IsElementFromGroupBase(XmlSchemaElement derivedElement, XmlSchemaGroupBase baseGroupBase)
		{
			if (baseGroupBase is XmlSchemaSequence)
			{
				if (this.IsGroupBaseFromGroupBase(new XmlSchemaSequence
				{
					MinOccurs = 1m,
					MaxOccurs = 1m,
					Items = 
					{
						derivedElement
					}
				}, baseGroupBase, true))
				{
					return true;
				}
				this.restrictionErrorMsg = Res.GetString("Sch_ElementFromGroupBase1", new object[]
				{
					derivedElement.QualifiedName.ToString(),
					derivedElement.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					derivedElement.LinePosition.ToString(NumberFormatInfo.InvariantInfo),
					baseGroupBase.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					baseGroupBase.LinePosition.ToString(NumberFormatInfo.InvariantInfo)
				});
			}
			else if (baseGroupBase is XmlSchemaChoice)
			{
				if (this.IsGroupBaseFromGroupBase(new XmlSchemaChoice
				{
					MinOccurs = 1m,
					MaxOccurs = 1m,
					Items = 
					{
						derivedElement
					}
				}, baseGroupBase, false))
				{
					return true;
				}
				this.restrictionErrorMsg = Res.GetString("Sch_ElementFromGroupBase2", new object[]
				{
					derivedElement.QualifiedName.ToString(),
					derivedElement.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					derivedElement.LinePosition.ToString(NumberFormatInfo.InvariantInfo),
					baseGroupBase.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					baseGroupBase.LinePosition.ToString(NumberFormatInfo.InvariantInfo)
				});
			}
			else if (baseGroupBase is XmlSchemaAll)
			{
				if (this.IsGroupBaseFromGroupBase(new XmlSchemaAll
				{
					MinOccurs = 1m,
					MaxOccurs = 1m,
					Items = 
					{
						derivedElement
					}
				}, baseGroupBase, true))
				{
					return true;
				}
				this.restrictionErrorMsg = Res.GetString("Sch_ElementFromGroupBase3", new object[]
				{
					derivedElement.QualifiedName.ToString(),
					derivedElement.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					derivedElement.LinePosition.ToString(NumberFormatInfo.InvariantInfo),
					baseGroupBase.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					baseGroupBase.LinePosition.ToString(NumberFormatInfo.InvariantInfo)
				});
			}
			return false;
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000AFC90 File Offset: 0x000ADE90
		private bool IsChoiceFromChoiceSubstGroup(XmlSchemaChoice derivedChoice, XmlSchemaChoice baseChoice)
		{
			if (!this.IsValidOccurrenceRangeRestriction(derivedChoice, baseChoice))
			{
				this.restrictionErrorMsg = Res.GetString("Sch_GroupBaseRestRangeInvalid");
				return false;
			}
			for (int i = 0; i < derivedChoice.Items.Count; i++)
			{
				if (this.GetMappingParticle((XmlSchemaParticle)derivedChoice.Items[i], baseChoice.Items) < 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000AFCF4 File Offset: 0x000ADEF4
		private bool IsGroupBaseFromGroupBase(XmlSchemaGroupBase derivedGroupBase, XmlSchemaGroupBase baseGroupBase, bool skipEmptableOnly)
		{
			if (!this.IsValidOccurrenceRangeRestriction(derivedGroupBase, baseGroupBase))
			{
				this.restrictionErrorMsg = Res.GetString("Sch_GroupBaseRestRangeInvalid");
				return false;
			}
			if (derivedGroupBase.Items.Count > baseGroupBase.Items.Count)
			{
				this.restrictionErrorMsg = Res.GetString("Sch_GroupBaseRestNoMap");
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
					if (this.restrictionErrorMsg == null)
					{
						this.restrictionErrorMsg = Res.GetString("Sch_GroupBaseRestNotEmptiable");
					}
					return false;
				}
			}
			return num >= derivedGroupBase.Items.Count;
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x000AFDD8 File Offset: 0x000ADFD8
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

		// Token: 0x06002044 RID: 8260 RVA: 0x000AFEA4 File Offset: 0x000AE0A4
		private bool IsSequenceFromChoice(XmlSchemaSequence derivedSequence, XmlSchemaChoice baseChoice)
		{
			decimal minOccurs = derivedSequence.MinOccurs * derivedSequence.Items.Count;
			decimal maxOccurs;
			if (derivedSequence.MaxOccurs == 79228162514264337593543950335m)
			{
				maxOccurs = decimal.MaxValue;
			}
			else
			{
				maxOccurs = derivedSequence.MaxOccurs * derivedSequence.Items.Count;
			}
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

		// Token: 0x06002045 RID: 8261 RVA: 0x000AFF77 File Offset: 0x000AE177
		private bool IsValidOccurrenceRangeRestriction(XmlSchemaParticle derivedParticle, XmlSchemaParticle baseParticle)
		{
			return this.IsValidOccurrenceRangeRestriction(derivedParticle.MinOccurs, derivedParticle.MaxOccurs, baseParticle.MinOccurs, baseParticle.MaxOccurs);
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x000AFF97 File Offset: 0x000AE197
		private bool IsValidOccurrenceRangeRestriction(decimal minOccurs, decimal maxOccurs, decimal baseMinOccurs, decimal baseMaxOccurs)
		{
			return baseMinOccurs <= minOccurs && maxOccurs <= baseMaxOccurs;
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000AFFAC File Offset: 0x000AE1AC
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

		// Token: 0x06002048 RID: 8264 RVA: 0x000AFFE4 File Offset: 0x000AE1E4
		private bool IsParticleEmptiable(XmlSchemaParticle particle)
		{
			decimal d;
			decimal num;
			this.CalculateEffectiveTotalRange(particle, out d, out num);
			return d == 0m;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000B0008 File Offset: 0x000AE208
		private void CalculateEffectiveTotalRange(XmlSchemaParticle particle, out decimal minOccurs, out decimal maxOccurs)
		{
			XmlSchemaChoice xmlSchemaChoice = particle as XmlSchemaChoice;
			if (particle is XmlSchemaElement || particle is XmlSchemaAny)
			{
				minOccurs = particle.MinOccurs;
				maxOccurs = particle.MaxOccurs;
				return;
			}
			if (xmlSchemaChoice != null)
			{
				if (xmlSchemaChoice.Items.Count == 0)
				{
					minOccurs = (maxOccurs = 0m);
					return;
				}
				minOccurs = decimal.MaxValue;
				maxOccurs = 0m;
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

		// Token: 0x0600204A RID: 8266 RVA: 0x000B028E File Offset: 0x000AE48E
		private void PushComplexType(XmlSchemaComplexType complexType)
		{
			this.complexTypeStack.Push(complexType);
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x000B029C File Offset: 0x000AE49C
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

		// Token: 0x0600204C RID: 8268 RVA: 0x000B02C4 File Offset: 0x000AE4C4
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
			try
			{
				for (int i = 0; i < attributeGroup.Attributes.Count; i++)
				{
					XmlSchemaAttribute xmlSchemaAttribute = attributeGroup.Attributes[i] as XmlSchemaAttribute;
					if (xmlSchemaAttribute != null)
					{
						if (xmlSchemaAttribute.Use != XmlSchemaUse.Prohibited)
						{
							this.CompileAttribute(xmlSchemaAttribute);
							if (attributeGroup.AttributeUses[xmlSchemaAttribute.QualifiedName] == null)
							{
								attributeGroup.AttributeUses.Add(xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute);
							}
							else
							{
								base.SendValidationEvent("Sch_DupAttributeUse", xmlSchemaAttribute.QualifiedName.ToString(), xmlSchemaAttribute);
							}
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
							xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)this.attributeGroups[xmlSchemaAttributeGroupRef.RefName];
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
			}
			finally
			{
				attributeGroup.IsProcessing = false;
			}
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x000B04E4 File Offset: 0x000AE6E4
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
					XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)this.attributeGroups[xmlSchemaAttributeGroupRef.RefName];
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
							if (xmlSchemaAttribute4 == null)
							{
								derivedType.AttributeUses.Add(xmlSchemaAttribute3.QualifiedName, xmlSchemaAttribute3);
							}
							else if (xmlSchemaAttribute3.Use != XmlSchemaUse.Prohibited && xmlSchemaAttribute4.AttributeSchemaType != xmlSchemaAttribute3.AttributeSchemaType)
							{
								base.SendValidationEvent("Sch_InvalidAttributeExtension", xmlSchemaAttribute4);
							}
						}
						return;
					}
				}
				if (anyAttribute != null && (xmlSchemaAnyAttribute == null || !XmlSchemaAnyAttribute.IsSubset(anyAttribute, xmlSchemaAnyAttribute) || !this.IsProcessContentsRestricted(baseType, anyAttribute, xmlSchemaAnyAttribute)))
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
					else if (xmlSchemaAttribute5.Use == XmlSchemaUse.Required && xmlSchemaAttribute6.Use != XmlSchemaUse.Required)
					{
						base.SendValidationEvent("Sch_AttributeUseInvalid", xmlSchemaAttribute6);
					}
					else if (xmlSchemaAttribute6.Use != XmlSchemaUse.Prohibited)
					{
						if (xmlSchemaAttribute5.AttributeSchemaType == null || xmlSchemaAttribute6.AttributeSchemaType == null || !XmlSchemaType.IsDerivedFrom(xmlSchemaAttribute6.AttributeSchemaType, xmlSchemaAttribute5.AttributeSchemaType, XmlSchemaDerivationMethod.Empty))
						{
							base.SendValidationEvent("Sch_AttributeRestrictionInvalid", xmlSchemaAttribute6);
						}
						else if (!this.IsFixedEqual(xmlSchemaAttribute5.AttDef, xmlSchemaAttribute6.AttDef))
						{
							base.SendValidationEvent("Sch_AttributeFixedInvalid", xmlSchemaAttribute6);
						}
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

		// Token: 0x0600204E RID: 8270 RVA: 0x000B09E4 File Offset: 0x000AEBE4
		private void CheckAtrributeGroupRestriction(XmlSchemaAttributeGroup baseAttributeGroup, XmlSchemaAttributeGroup derivedAttributeGroup)
		{
			XmlSchemaAnyAttribute attributeWildcard = baseAttributeGroup.AttributeWildcard;
			XmlSchemaAnyAttribute attributeWildcard2 = derivedAttributeGroup.AttributeWildcard;
			if (attributeWildcard2 != null && (attributeWildcard == null || !XmlSchemaAnyAttribute.IsSubset(attributeWildcard2, attributeWildcard) || !this.IsProcessContentsRestricted(null, attributeWildcard2, attributeWildcard)))
			{
				base.SendValidationEvent("Sch_InvalidAnyAttributeRestriction", derivedAttributeGroup);
			}
			foreach (object obj in baseAttributeGroup.AttributeUses.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj;
				XmlSchemaAttribute xmlSchemaAttribute2 = (XmlSchemaAttribute)derivedAttributeGroup.AttributeUses[xmlSchemaAttribute.QualifiedName];
				if (xmlSchemaAttribute2 != null)
				{
					if (xmlSchemaAttribute.Use == XmlSchemaUse.Prohibited && xmlSchemaAttribute2.Use != XmlSchemaUse.Prohibited)
					{
						base.SendValidationEvent("Sch_AttributeRestrictionProhibited", xmlSchemaAttribute2);
					}
					else if (xmlSchemaAttribute.Use == XmlSchemaUse.Required && xmlSchemaAttribute2.Use != XmlSchemaUse.Required)
					{
						base.SendValidationEvent("Sch_AttributeUseInvalid", xmlSchemaAttribute2);
					}
					else if (xmlSchemaAttribute2.Use != XmlSchemaUse.Prohibited)
					{
						if (xmlSchemaAttribute.AttributeSchemaType == null || xmlSchemaAttribute2.AttributeSchemaType == null || !XmlSchemaType.IsDerivedFrom(xmlSchemaAttribute2.AttributeSchemaType, xmlSchemaAttribute.AttributeSchemaType, XmlSchemaDerivationMethod.Empty))
						{
							base.SendValidationEvent("Sch_AttributeRestrictionInvalid", xmlSchemaAttribute2);
						}
						else if (!this.IsFixedEqual(xmlSchemaAttribute.AttDef, xmlSchemaAttribute2.AttDef))
						{
							base.SendValidationEvent("Sch_AttributeFixedInvalid", xmlSchemaAttribute2);
						}
					}
				}
				else if (xmlSchemaAttribute.Use == XmlSchemaUse.Required)
				{
					base.SendValidationEvent("Sch_NoDerivedAttribute", xmlSchemaAttribute.QualifiedName.ToString(), baseAttributeGroup.QualifiedName.ToString(), derivedAttributeGroup);
				}
			}
			foreach (object obj2 in derivedAttributeGroup.AttributeUses.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute3 = (XmlSchemaAttribute)obj2;
				if ((XmlSchemaAttribute)baseAttributeGroup.AttributeUses[xmlSchemaAttribute3.QualifiedName] == null && (attributeWildcard == null || !attributeWildcard.Allows(xmlSchemaAttribute3.QualifiedName)))
				{
					base.SendValidationEvent("Sch_AttributeRestrictionInvalidFromWildcard", xmlSchemaAttribute3);
				}
			}
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x000B0C14 File Offset: 0x000AEE14
		private bool IsProcessContentsRestricted(XmlSchemaComplexType baseType, XmlSchemaAnyAttribute derivedAttributeWildcard, XmlSchemaAnyAttribute baseAttributeWildcard)
		{
			return baseType == XmlSchemaComplexType.AnyType || derivedAttributeWildcard.ProcessContentsCorrect >= baseAttributeWildcard.ProcessContentsCorrect;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000B0C34 File Offset: 0x000AEE34
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
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = XmlSchemaAnyAttribute.Union(a, b, false);
			if (xmlSchemaAnyAttribute == null)
			{
				base.SendValidationEvent("Sch_UnexpressibleAnyAttribute", a);
			}
			return xmlSchemaAnyAttribute;
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000B0C64 File Offset: 0x000AEE64
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
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = XmlSchemaAnyAttribute.Intersection(a, b, false);
			if (xmlSchemaAnyAttribute == null)
			{
				base.SendValidationEvent("Sch_UnexpressibleAnyAttribute", a);
			}
			return xmlSchemaAnyAttribute;
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x000B0C94 File Offset: 0x000AEE94
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
					XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)this.attributes[xa.RefName];
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
					XmlSchemaDatatype datatype = schemaAttDef.Datatype;
					if (datatype != null)
					{
						if (xmlSchemaAttribute.FixedValue == null && xmlSchemaAttribute.DefaultValue == null)
						{
							this.SetDefaultFixed(xa, schemaAttDef);
						}
						else if (xmlSchemaAttribute.FixedValue != null)
						{
							if (xa.DefaultValue != null)
							{
								throw new XmlSchemaException("Sch_FixedDefaultInRef", xa.RefName.ToString(), xa);
							}
							if (xa.FixedValue != null)
							{
								object o = datatype.ParseValue(xa.FixedValue, base.NameTable, new SchemaNamespaceManager(xa), true);
								if (!datatype.IsEqual(schemaAttDef.DefaultValueTyped, o))
								{
									throw new XmlSchemaException("Sch_FixedInRef", xa.RefName.ToString(), xa);
								}
							}
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
					if (schemaAttDef.Datatype != null)
					{
						schemaAttDef.Datatype.VerifySchemaValid(this.notations, xa);
					}
					this.SetDefaultFixed(xa, schemaAttDef);
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

		// Token: 0x06002053 RID: 8275 RVA: 0x000B0F40 File Offset: 0x000AF140
		private void SetDefaultFixed(XmlSchemaAttribute xa, SchemaAttDef decl)
		{
			if (xa.DefaultValue != null || xa.FixedValue != null)
			{
				if (xa.DefaultValue != null)
				{
					decl.Presence = SchemaDeclBase.Use.Default;
					decl.DefaultValueRaw = (decl.DefaultValueExpanded = xa.DefaultValue);
				}
				else
				{
					if (xa.Use == XmlSchemaUse.Required)
					{
						decl.Presence = SchemaDeclBase.Use.RequiredFixed;
					}
					else
					{
						decl.Presence = SchemaDeclBase.Use.Fixed;
					}
					decl.DefaultValueRaw = (decl.DefaultValueExpanded = xa.FixedValue);
				}
				if (decl.Datatype != null)
				{
					if (decl.Datatype.TypeCode == XmlTypeCode.Id)
					{
						base.SendValidationEvent("Sch_DefaultIdValue", xa);
						return;
					}
					decl.DefaultValueTyped = decl.Datatype.ParseValue(decl.DefaultValueRaw, base.NameTable, new SchemaNamespaceManager(xa), true);
					return;
				}
			}
			else
			{
				switch (xa.Use)
				{
				case XmlSchemaUse.None:
				case XmlSchemaUse.Optional:
					decl.Presence = SchemaDeclBase.Use.Implied;
					return;
				case XmlSchemaUse.Prohibited:
					break;
				case XmlSchemaUse.Required:
					decl.Presence = SchemaDeclBase.Use.Required;
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000B102C File Offset: 0x000AF22C
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
					XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)this.identityConstraints[((XmlSchemaKeyref)xi).Refer];
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

		// Token: 0x06002055 RID: 8277 RVA: 0x000B11A8 File Offset: 0x000AF3A8
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
					XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.elements[xe.RefName];
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
						XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)this.elements[xe.SubstitutionGroup];
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
					schemaElementDecl.Datatype.VerifySchemaValid(this.notations, xe);
				}
				if ((xe.DefaultValue != null || xe.FixedValue != null) && schemaElementDecl.ContentValidator != null)
				{
					if (schemaElementDecl.ContentValidator.ContentType != XmlSchemaContentType.TextOnly && (schemaElementDecl.ContentValidator.ContentType != XmlSchemaContentType.Mixed || !schemaElementDecl.ContentValidator.IsEmptiable))
					{
						throw new XmlSchemaException("Sch_ElementCannotHaveValue", xe);
					}
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
						if (schemaElementDecl.Datatype.TypeCode == XmlTypeCode.Id)
						{
							base.SendValidationEvent("Sch_DefaultIdValue", xe);
						}
						else
						{
							schemaElementDecl.DefaultValueTyped = schemaElementDecl.Datatype.ParseValue(schemaElementDecl.DefaultValueRaw, base.NameTable, new SchemaNamespaceManager(xe), true);
						}
					}
					else
					{
						schemaElementDecl.DefaultValueTyped = DatatypeImplementation.AnySimpleType.Datatype.ParseValue(schemaElementDecl.DefaultValueRaw, base.NameTable, new SchemaNamespaceManager(xe));
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

		// Token: 0x06002056 RID: 8278 RVA: 0x000B1630 File Offset: 0x000AF830
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
				ParticleContentValidator particleContentValidator = new ParticleContentValidator(complexType.ContentType, base.CompilationSettings.EnableUpaCheck);
				ContentValidator result;
				try
				{
					particleContentValidator.Start();
					complexType.HasWildCard = this.BuildParticleContentModel(particleContentValidator, contentTypeParticle);
					result = particleContentValidator.Finish(true);
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
							base.SendValidationEvent("Sch_NonDeterministicAnyEx", ((XmlSchemaAny)ex.Particle2).ResolvedNamespace, ((XmlSchemaElement)ex.Particle1).QualifiedName.ToString(), (XmlSchemaAny)ex.Particle2);
						}
					}
					else if (ex.Particle2 is XmlSchemaElement)
					{
						base.SendValidationEvent("Sch_NonDeterministicAnyEx", ((XmlSchemaAny)ex.Particle1).ResolvedNamespace, ((XmlSchemaElement)ex.Particle2).QualifiedName.ToString(), (XmlSchemaElement)ex.Particle2);
					}
					else
					{
						base.SendValidationEvent("Sch_NonDeterministicAnyAny", ((XmlSchemaAny)ex.Particle1).ResolvedNamespace, ((XmlSchemaAny)ex.Particle2).ResolvedNamespace, (XmlSchemaAny)ex.Particle2);
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

		// Token: 0x06002057 RID: 8279 RVA: 0x000B18D8 File Offset: 0x000AFAD8
		private bool BuildParticleContentModel(ParticleContentValidator contentValidator, XmlSchemaParticle particle)
		{
			bool result = false;
			if (particle is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)particle;
				contentValidator.AddName(xmlSchemaElement.QualifiedName, xmlSchemaElement);
			}
			else if (particle is XmlSchemaAny)
			{
				result = true;
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
					result = this.BuildParticleContentModel(contentValidator, (XmlSchemaParticle)items[i]);
				}
				contentValidator.CloseGroup();
			}
			if (!(particle.MinOccurs == 1m) || !(particle.MaxOccurs == 1m))
			{
				if (particle.MinOccurs == 0m && particle.MaxOccurs == 1m)
				{
					contentValidator.AddQMark();
				}
				else if (particle.MinOccurs == 0m && particle.MaxOccurs == 79228162514264337593543950335m)
				{
					contentValidator.AddStar();
				}
				else if (particle.MinOccurs == 1m && particle.MaxOccurs == 79228162514264337593543950335m)
				{
					contentValidator.AddPlus();
				}
				else
				{
					contentValidator.AddLeafRange(particle.MinOccurs, particle.MaxOccurs);
				}
			}
			return result;
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000B1A64 File Offset: 0x000AFC64
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

		// Token: 0x06002059 RID: 8281 RVA: 0x000B1B18 File Offset: 0x000AFD18
		private void CompileParticleElements(XmlSchemaParticle particle)
		{
			if (particle is XmlSchemaElement)
			{
				XmlSchemaElement xe = (XmlSchemaElement)particle;
				this.CompileElement(xe);
				return;
			}
			if (particle is XmlSchemaGroupBase)
			{
				XmlSchemaObjectCollection items = ((XmlSchemaGroupBase)particle).Items;
				for (int i = 0; i < items.Count; i++)
				{
					this.CompileParticleElements((XmlSchemaParticle)items[i]);
				}
			}
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000B1B74 File Offset: 0x000AFD74
		private void CompileComplexTypeElements(XmlSchemaComplexType complexType)
		{
			if (complexType.IsProcessing)
			{
				base.SendValidationEvent("Sch_TypeCircularRef", complexType);
				return;
			}
			complexType.IsProcessing = true;
			try
			{
				if (complexType.ContentTypeParticle != XmlSchemaParticle.Empty)
				{
					this.CompileParticleElements(complexType, complexType.ContentTypeParticle);
				}
			}
			finally
			{
				complexType.IsProcessing = false;
			}
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000B1BD4 File Offset: 0x000AFDD4
		private XmlSchemaSimpleType GetSimpleType(XmlQualifiedName name)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = this.schemaTypes[name] as XmlSchemaSimpleType;
			if (xmlSchemaSimpleType != null)
			{
				this.CompileSimpleType(xmlSchemaSimpleType);
			}
			else
			{
				xmlSchemaSimpleType = DatatypeImplementation.GetSimpleTypeFromXsdType(name);
			}
			return xmlSchemaSimpleType;
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000B1C08 File Offset: 0x000AFE08
		private XmlSchemaComplexType GetComplexType(XmlQualifiedName name)
		{
			XmlSchemaComplexType xmlSchemaComplexType = this.schemaTypes[name] as XmlSchemaComplexType;
			if (xmlSchemaComplexType != null)
			{
				this.CompileComplexType(xmlSchemaComplexType);
			}
			return xmlSchemaComplexType;
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000B1C34 File Offset: 0x000AFE34
		private XmlSchemaType GetAnySchemaType(XmlQualifiedName name)
		{
			XmlSchemaType xmlSchemaType = (XmlSchemaType)this.schemaTypes[name];
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

		// Token: 0x0600205E RID: 8286 RVA: 0x000B1C84 File Offset: 0x000AFE84
		private void CopyPosition(XmlSchemaAnnotated to, XmlSchemaAnnotated from, bool copyParent)
		{
			to.SourceUri = from.SourceUri;
			to.LinePosition = from.LinePosition;
			to.LineNumber = from.LineNumber;
			to.SetUnhandledAttributes(from.UnhandledAttributes);
			if (copyParent)
			{
				to.Parent = from.Parent;
			}
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000B1CD0 File Offset: 0x000AFED0
		private bool IsFixedEqual(SchemaDeclBase baseDecl, SchemaDeclBase derivedDecl)
		{
			if (baseDecl.Presence == SchemaDeclBase.Use.Fixed || baseDecl.Presence == SchemaDeclBase.Use.RequiredFixed)
			{
				object defaultValueTyped = baseDecl.DefaultValueTyped;
				object defaultValueTyped2 = derivedDecl.DefaultValueTyped;
				if (derivedDecl.Presence != SchemaDeclBase.Use.Fixed && derivedDecl.Presence != SchemaDeclBase.Use.RequiredFixed)
				{
					return false;
				}
				XmlSchemaDatatype datatype = baseDecl.Datatype;
				XmlSchemaDatatype datatype2 = derivedDecl.Datatype;
				if (datatype.Variety == XmlSchemaDatatypeVariety.Union)
				{
					if (datatype2.Variety == XmlSchemaDatatypeVariety.Union)
					{
						if (!datatype2.IsEqual(defaultValueTyped, defaultValueTyped2))
						{
							return false;
						}
					}
					else
					{
						XsdSimpleValue xsdSimpleValue = baseDecl.DefaultValueTyped as XsdSimpleValue;
						XmlSchemaDatatype datatype3 = xsdSimpleValue.XmlType.Datatype;
						if (!datatype3.IsComparable(datatype2) || !datatype2.IsEqual(xsdSimpleValue.TypedValue, defaultValueTyped2))
						{
							return false;
						}
					}
				}
				else if (!datatype2.IsEqual(defaultValueTyped, defaultValueTyped2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000D88 RID: 3464
		private string restrictionErrorMsg;

		// Token: 0x04000D89 RID: 3465
		private XmlSchemaObjectTable attributes = new XmlSchemaObjectTable();

		// Token: 0x04000D8A RID: 3466
		private XmlSchemaObjectTable attributeGroups = new XmlSchemaObjectTable();

		// Token: 0x04000D8B RID: 3467
		private XmlSchemaObjectTable elements = new XmlSchemaObjectTable();

		// Token: 0x04000D8C RID: 3468
		private XmlSchemaObjectTable schemaTypes = new XmlSchemaObjectTable();

		// Token: 0x04000D8D RID: 3469
		private XmlSchemaObjectTable groups = new XmlSchemaObjectTable();

		// Token: 0x04000D8E RID: 3470
		private XmlSchemaObjectTable notations = new XmlSchemaObjectTable();

		// Token: 0x04000D8F RID: 3471
		private XmlSchemaObjectTable examplars = new XmlSchemaObjectTable();

		// Token: 0x04000D90 RID: 3472
		private XmlSchemaObjectTable identityConstraints = new XmlSchemaObjectTable();

		// Token: 0x04000D91 RID: 3473
		private Stack complexTypeStack = new Stack();

		// Token: 0x04000D92 RID: 3474
		private Hashtable schemasToCompile = new Hashtable();

		// Token: 0x04000D93 RID: 3475
		private Hashtable importedSchemas = new Hashtable();

		// Token: 0x04000D94 RID: 3476
		private XmlSchema schemaForSchema;
	}
}
