using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020002D3 RID: 723
	public sealed class XmlSchemaInference
	{
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x000E3906 File Offset: 0x000E1B06
		// (set) Token: 0x06002B59 RID: 11097 RVA: 0x000E38FD File Offset: 0x000E1AFD
		public XmlSchemaInference.InferenceOption Occurrence
		{
			get
			{
				return this.occurrence;
			}
			set
			{
				this.occurrence = value;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x000E3917 File Offset: 0x000E1B17
		// (set) Token: 0x06002B5B RID: 11099 RVA: 0x000E390E File Offset: 0x000E1B0E
		public XmlSchemaInference.InferenceOption TypeInference
		{
			get
			{
				return this.typeInference;
			}
			set
			{
				this.typeInference = value;
			}
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000E3920 File Offset: 0x000E1B20
		public XmlSchemaInference()
		{
			this.nametable = new NameTable();
			this.NamespaceManager = new XmlNamespaceManager(this.nametable);
			this.NamespaceManager.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
			this.schemaList = new ArrayList();
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x000E396F File Offset: 0x000E1B6F
		public XmlSchemaSet InferSchema(XmlReader instanceDocument)
		{
			return this.InferSchema1(instanceDocument, new XmlSchemaSet(this.nametable));
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000E3983 File Offset: 0x000E1B83
		public XmlSchemaSet InferSchema(XmlReader instanceDocument, XmlSchemaSet schemas)
		{
			if (schemas == null)
			{
				schemas = new XmlSchemaSet(this.nametable);
			}
			return this.InferSchema1(instanceDocument, schemas);
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x000E39A0 File Offset: 0x000E1BA0
		internal XmlSchemaSet InferSchema1(XmlReader instanceDocument, XmlSchemaSet schemas)
		{
			if (instanceDocument == null)
			{
				throw new ArgumentNullException("instanceDocument");
			}
			this.rootSchema = null;
			this.xtr = instanceDocument;
			schemas.Compile();
			this.schemaSet = schemas;
			while (this.xtr.NodeType != XmlNodeType.Element && this.xtr.Read())
			{
			}
			if (this.xtr.NodeType != XmlNodeType.Element)
			{
				throw new XmlSchemaInferenceException("SchInf_NoElement", 0, 0);
			}
			this.TargetNamespace = this.xtr.NamespaceURI;
			if (this.xtr.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
			{
				throw new XmlSchemaInferenceException("SchInf_schema", 0, 0);
			}
			XmlSchemaElement xse = null;
			foreach (object obj in schemas.GlobalElements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (xmlSchemaElement.Name == this.xtr.LocalName && xmlSchemaElement.QualifiedName.Namespace == this.xtr.NamespaceURI)
				{
					this.rootSchema = (xmlSchemaElement.Parent as XmlSchema);
					xse = xmlSchemaElement;
					break;
				}
			}
			if (this.rootSchema == null)
			{
				xse = this.AddElement(this.xtr.LocalName, this.xtr.Prefix, this.xtr.NamespaceURI, null, null, -1);
			}
			else
			{
				this.InferElement(xse, false, this.rootSchema);
			}
			foreach (object obj2 in this.NamespaceManager)
			{
				string text = (string)obj2;
				if (!text.Equals("xml") && !text.Equals("xmlns"))
				{
					string text2 = this.NamespaceManager.LookupNamespace(this.nametable.Get(text));
					if (text2.Length != 0)
					{
						this.rootSchema.Namespaces.Add(text, text2);
					}
				}
			}
			schemas.Reprocess(this.rootSchema);
			schemas.Compile();
			return schemas;
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x000E3BCC File Offset: 0x000E1DCC
		private XmlSchemaAttribute AddAttribute(string localName, string prefix, string childURI, string attrValue, bool bCreatingNewType, XmlSchema parentSchema, XmlSchemaObjectCollection addLocation, XmlSchemaObjectTable compiledAttributes)
		{
			if (childURI == "http://www.w3.org/2001/XMLSchema")
			{
				throw new XmlSchemaInferenceException("SchInf_schema", 0, 0);
			}
			int lineNumber = -1;
			XmlSchema xmlSchema = null;
			bool flag = true;
			ICollection attributes;
			ICollection collection;
			if (compiledAttributes.Count > 0)
			{
				attributes = compiledAttributes.Values;
				collection = addLocation;
			}
			else
			{
				attributes = addLocation;
				collection = null;
			}
			XmlSchemaAttribute result;
			if (childURI == "http://www.w3.org/XML/1998/namespace")
			{
				XmlSchemaAttribute xmlSchemaAttribute = this.FindAttributeRef(attributes, localName, childURI);
				if (xmlSchemaAttribute == null && collection != null)
				{
					xmlSchemaAttribute = this.FindAttributeRef(collection, localName, childURI);
				}
				if (xmlSchemaAttribute == null)
				{
					xmlSchemaAttribute = new XmlSchemaAttribute();
					xmlSchemaAttribute.RefName = new XmlQualifiedName(localName, childURI);
					if (bCreatingNewType && this.Occurrence == XmlSchemaInference.InferenceOption.Restricted)
					{
						xmlSchemaAttribute.Use = XmlSchemaUse.Required;
					}
					else
					{
						xmlSchemaAttribute.Use = XmlSchemaUse.Optional;
					}
					addLocation.Add(xmlSchemaAttribute);
				}
				result = xmlSchemaAttribute;
			}
			else
			{
				if (childURI.Length == 0)
				{
					xmlSchema = parentSchema;
					flag = false;
				}
				else if (childURI != null && !this.schemaSet.Contains(childURI))
				{
					xmlSchema = new XmlSchema();
					xmlSchema.AttributeFormDefault = XmlSchemaForm.Unqualified;
					xmlSchema.ElementFormDefault = XmlSchemaForm.Qualified;
					if (childURI.Length != 0)
					{
						xmlSchema.TargetNamespace = childURI;
					}
					this.schemaSet.Add(xmlSchema);
					if (prefix.Length != 0 && string.Compare(prefix, "xml", StringComparison.OrdinalIgnoreCase) != 0)
					{
						this.NamespaceManager.AddNamespace(prefix, childURI);
					}
				}
				else
				{
					ArrayList arrayList = this.schemaSet.Schemas(childURI) as ArrayList;
					if (arrayList != null && arrayList.Count > 0)
					{
						xmlSchema = (arrayList[0] as XmlSchema);
					}
				}
				if (childURI.Length != 0)
				{
					XmlSchemaAttribute xmlSchemaAttribute2 = this.FindAttributeRef(attributes, localName, childURI);
					if (xmlSchemaAttribute2 == null & collection != null)
					{
						xmlSchemaAttribute2 = this.FindAttributeRef(collection, localName, childURI);
					}
					if (xmlSchemaAttribute2 == null)
					{
						xmlSchemaAttribute2 = new XmlSchemaAttribute();
						xmlSchemaAttribute2.RefName = new XmlQualifiedName(localName, childURI);
						if (bCreatingNewType && this.Occurrence == XmlSchemaInference.InferenceOption.Restricted)
						{
							xmlSchemaAttribute2.Use = XmlSchemaUse.Required;
						}
						else
						{
							xmlSchemaAttribute2.Use = XmlSchemaUse.Optional;
						}
						addLocation.Add(xmlSchemaAttribute2);
					}
					result = xmlSchemaAttribute2;
					XmlSchemaAttribute xmlSchemaAttribute3 = this.FindAttribute(xmlSchema.Items, localName);
					if (xmlSchemaAttribute3 == null)
					{
						xmlSchemaAttribute3 = new XmlSchemaAttribute();
						xmlSchemaAttribute3.Name = localName;
						xmlSchemaAttribute3.SchemaTypeName = this.RefineSimpleType(attrValue, ref lineNumber);
						xmlSchemaAttribute3.LineNumber = lineNumber;
						xmlSchema.Items.Add(xmlSchemaAttribute3);
					}
					else
					{
						if (xmlSchemaAttribute3.Parent == null)
						{
							lineNumber = xmlSchemaAttribute3.LineNumber;
						}
						else
						{
							lineNumber = XmlSchemaInference.GetSchemaType(xmlSchemaAttribute3.SchemaTypeName);
							xmlSchemaAttribute3.Parent = null;
						}
						xmlSchemaAttribute3.SchemaTypeName = this.RefineSimpleType(attrValue, ref lineNumber);
						xmlSchemaAttribute3.LineNumber = lineNumber;
					}
				}
				else
				{
					XmlSchemaAttribute xmlSchemaAttribute3 = this.FindAttribute(attributes, localName);
					if (xmlSchemaAttribute3 == null && collection != null)
					{
						xmlSchemaAttribute3 = this.FindAttribute(collection, localName);
					}
					if (xmlSchemaAttribute3 == null)
					{
						xmlSchemaAttribute3 = new XmlSchemaAttribute();
						xmlSchemaAttribute3.Name = localName;
						xmlSchemaAttribute3.SchemaTypeName = this.RefineSimpleType(attrValue, ref lineNumber);
						xmlSchemaAttribute3.LineNumber = lineNumber;
						if (bCreatingNewType && this.Occurrence == XmlSchemaInference.InferenceOption.Restricted)
						{
							xmlSchemaAttribute3.Use = XmlSchemaUse.Required;
						}
						else
						{
							xmlSchemaAttribute3.Use = XmlSchemaUse.Optional;
						}
						addLocation.Add(xmlSchemaAttribute3);
						if (xmlSchema.AttributeFormDefault != XmlSchemaForm.Unqualified)
						{
							xmlSchemaAttribute3.Form = XmlSchemaForm.Unqualified;
						}
					}
					else
					{
						if (xmlSchemaAttribute3.Parent == null)
						{
							lineNumber = xmlSchemaAttribute3.LineNumber;
						}
						else
						{
							lineNumber = XmlSchemaInference.GetSchemaType(xmlSchemaAttribute3.SchemaTypeName);
							xmlSchemaAttribute3.Parent = null;
						}
						xmlSchemaAttribute3.SchemaTypeName = this.RefineSimpleType(attrValue, ref lineNumber);
						xmlSchemaAttribute3.LineNumber = lineNumber;
					}
					result = xmlSchemaAttribute3;
				}
			}
			string @namespace = null;
			if (flag && childURI != parentSchema.TargetNamespace)
			{
				for (int i = 0; i < parentSchema.Includes.Count; i++)
				{
					XmlSchemaImport xmlSchemaImport = parentSchema.Includes[i] as XmlSchemaImport;
					if (xmlSchemaImport != null && xmlSchemaImport.Namespace == childURI)
					{
						flag = false;
					}
				}
				if (flag)
				{
					XmlSchemaImport xmlSchemaImport2 = new XmlSchemaImport();
					xmlSchemaImport2.Schema = xmlSchema;
					if (childURI.Length != 0)
					{
						@namespace = childURI;
					}
					xmlSchemaImport2.Namespace = @namespace;
					parentSchema.Includes.Add(xmlSchemaImport2);
				}
			}
			return result;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000E3F8C File Offset: 0x000E218C
		private XmlSchema CreateXmlSchema(string targetNS)
		{
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.AttributeFormDefault = XmlSchemaForm.Unqualified;
			xmlSchema.ElementFormDefault = XmlSchemaForm.Qualified;
			xmlSchema.TargetNamespace = targetNS;
			this.schemaSet.Add(xmlSchema);
			return xmlSchema;
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000E3FC4 File Offset: 0x000E21C4
		private XmlSchemaElement AddElement(string localName, string prefix, string childURI, XmlSchema parentSchema, XmlSchemaObjectCollection addLocation, int positionWithinCollection)
		{
			if (childURI == "http://www.w3.org/2001/XMLSchema")
			{
				throw new XmlSchemaInferenceException("SchInf_schema", 0, 0);
			}
			XmlSchema xmlSchema = null;
			bool bCreatingNewType = true;
			if (childURI == string.Empty)
			{
				childURI = null;
			}
			XmlSchemaElement xmlSchemaElement;
			if (parentSchema != null && childURI == parentSchema.TargetNamespace)
			{
				xmlSchemaElement = new XmlSchemaElement();
				xmlSchemaElement.Name = localName;
				xmlSchema = parentSchema;
				if (xmlSchema.ElementFormDefault != XmlSchemaForm.Qualified && addLocation != null)
				{
					xmlSchemaElement.Form = XmlSchemaForm.Qualified;
				}
			}
			else if (this.schemaSet.Contains(childURI))
			{
				xmlSchemaElement = this.FindGlobalElement(childURI, localName, out xmlSchema);
				if (xmlSchemaElement == null)
				{
					ArrayList arrayList = this.schemaSet.Schemas(childURI) as ArrayList;
					if (arrayList != null && arrayList.Count > 0)
					{
						xmlSchema = (arrayList[0] as XmlSchema);
					}
					xmlSchemaElement = new XmlSchemaElement();
					xmlSchemaElement.Name = localName;
					xmlSchema.Items.Add(xmlSchemaElement);
				}
				else
				{
					bCreatingNewType = false;
				}
			}
			else
			{
				xmlSchema = this.CreateXmlSchema(childURI);
				if (prefix.Length != 0)
				{
					this.NamespaceManager.AddNamespace(prefix, childURI);
				}
				xmlSchemaElement = new XmlSchemaElement();
				xmlSchemaElement.Name = localName;
				xmlSchema.Items.Add(xmlSchemaElement);
			}
			if (parentSchema == null)
			{
				parentSchema = xmlSchema;
				this.rootSchema = parentSchema;
			}
			if (childURI != parentSchema.TargetNamespace)
			{
				bool flag = true;
				for (int i = 0; i < parentSchema.Includes.Count; i++)
				{
					XmlSchemaImport xmlSchemaImport = parentSchema.Includes[i] as XmlSchemaImport;
					if (xmlSchemaImport != null && xmlSchemaImport.Namespace == childURI)
					{
						flag = false;
					}
				}
				if (flag)
				{
					XmlSchemaImport xmlSchemaImport2 = new XmlSchemaImport();
					xmlSchemaImport2.Schema = xmlSchema;
					xmlSchemaImport2.Namespace = childURI;
					parentSchema.Includes.Add(xmlSchemaImport2);
				}
			}
			XmlSchemaElement result = xmlSchemaElement;
			if (addLocation != null)
			{
				if (childURI == parentSchema.TargetNamespace)
				{
					if (this.Occurrence == XmlSchemaInference.InferenceOption.Relaxed)
					{
						xmlSchemaElement.MinOccurs = 0m;
					}
					if (positionWithinCollection == -1)
					{
						positionWithinCollection = addLocation.Add(xmlSchemaElement);
					}
					else
					{
						addLocation.Insert(positionWithinCollection, xmlSchemaElement);
					}
				}
				else
				{
					XmlSchemaElement xmlSchemaElement2 = new XmlSchemaElement();
					xmlSchemaElement2.RefName = new XmlQualifiedName(localName, childURI);
					if (this.Occurrence == XmlSchemaInference.InferenceOption.Relaxed)
					{
						xmlSchemaElement2.MinOccurs = 0m;
					}
					if (positionWithinCollection == -1)
					{
						positionWithinCollection = addLocation.Add(xmlSchemaElement2);
					}
					else
					{
						addLocation.Insert(positionWithinCollection, xmlSchemaElement2);
					}
					result = xmlSchemaElement2;
				}
			}
			this.InferElement(xmlSchemaElement, bCreatingNewType, xmlSchema);
			return result;
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000E4218 File Offset: 0x000E2418
		internal void InferElement(XmlSchemaElement xse, bool bCreatingNewType, XmlSchema parentSchema)
		{
			bool isEmptyElement = this.xtr.IsEmptyElement;
			int num = -1;
			Hashtable hashtable = new Hashtable();
			XmlSchemaType effectiveSchemaType = this.GetEffectiveSchemaType(xse, bCreatingNewType);
			XmlSchemaComplexType xmlSchemaComplexType = effectiveSchemaType as XmlSchemaComplexType;
			if (this.xtr.MoveToFirstAttribute())
			{
				this.ProcessAttributes(ref xse, effectiveSchemaType, bCreatingNewType, parentSchema);
			}
			else if (!bCreatingNewType && xmlSchemaComplexType != null)
			{
				this.MakeExistingAttributesOptional(xmlSchemaComplexType, null);
			}
			if (xmlSchemaComplexType == null || xmlSchemaComplexType == XmlSchemaComplexType.AnyType)
			{
				xmlSchemaComplexType = (xse.SchemaType as XmlSchemaComplexType);
			}
			if (isEmptyElement)
			{
				if (!bCreatingNewType)
				{
					if (xmlSchemaComplexType != null)
					{
						if (xmlSchemaComplexType.Particle != null)
						{
							xmlSchemaComplexType.Particle.MinOccurs = 0m;
							return;
						}
						if (xmlSchemaComplexType.ContentModel != null)
						{
							XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = this.CheckSimpleContentExtension(xmlSchemaComplexType);
							xmlSchemaSimpleContentExtension.BaseTypeName = XmlSchemaInference.ST_string;
							xmlSchemaSimpleContentExtension.LineNumber = 262144;
							return;
						}
					}
					else if (!xse.SchemaTypeName.IsEmpty)
					{
						xse.LineNumber = 262144;
						xse.SchemaTypeName = XmlSchemaInference.ST_string;
						return;
					}
				}
				else
				{
					xse.LineNumber = 262144;
				}
				return;
			}
			bool flag = false;
			for (;;)
			{
				this.xtr.Read();
				if (this.xtr.NodeType == XmlNodeType.Whitespace)
				{
					flag = true;
				}
				if (this.xtr.NodeType == XmlNodeType.EntityReference)
				{
					break;
				}
				if (this.xtr.EOF || this.xtr.NodeType == XmlNodeType.EndElement || this.xtr.NodeType == XmlNodeType.CDATA || this.xtr.NodeType == XmlNodeType.Element || this.xtr.NodeType == XmlNodeType.Text)
				{
					goto IL_16C;
				}
			}
			throw new XmlSchemaInferenceException("SchInf_entity", 0, 0);
			IL_16C:
			if (this.xtr.NodeType != XmlNodeType.EndElement)
			{
				int num2 = 0;
				bool flag2 = false;
				IL_81D:
				while (!this.xtr.EOF && this.xtr.NodeType != XmlNodeType.EndElement)
				{
					bool flag3 = false;
					num2++;
					if (this.xtr.NodeType == XmlNodeType.Text || this.xtr.NodeType == XmlNodeType.CDATA)
					{
						if (xmlSchemaComplexType != null)
						{
							if (xmlSchemaComplexType.Particle != null)
							{
								xmlSchemaComplexType.IsMixed = true;
								if (num2 == 1)
								{
									do
									{
										this.xtr.Read();
									}
									while (!this.xtr.EOF && (this.xtr.NodeType == XmlNodeType.CDATA || this.xtr.NodeType == XmlNodeType.Text || this.xtr.NodeType == XmlNodeType.Comment || this.xtr.NodeType == XmlNodeType.ProcessingInstruction || this.xtr.NodeType == XmlNodeType.Whitespace || this.xtr.NodeType == XmlNodeType.SignificantWhitespace || this.xtr.NodeType == XmlNodeType.XmlDeclaration));
									flag3 = true;
									if (this.xtr.NodeType == XmlNodeType.EndElement)
									{
										xmlSchemaComplexType.Particle.MinOccurs = 0m;
									}
								}
							}
							else if (xmlSchemaComplexType.ContentModel != null)
							{
								XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension2 = this.CheckSimpleContentExtension(xmlSchemaComplexType);
								if (this.xtr.NodeType == XmlNodeType.Text && num2 == 1)
								{
									int lineNumber = -1;
									if (xse.Parent == null)
									{
										lineNumber = xmlSchemaSimpleContentExtension2.LineNumber;
									}
									else
									{
										lineNumber = XmlSchemaInference.GetSchemaType(xmlSchemaSimpleContentExtension2.BaseTypeName);
										xse.Parent = null;
									}
									xmlSchemaSimpleContentExtension2.BaseTypeName = this.RefineSimpleType(this.xtr.Value, ref lineNumber);
									xmlSchemaSimpleContentExtension2.LineNumber = lineNumber;
								}
								else
								{
									xmlSchemaSimpleContentExtension2.BaseTypeName = XmlSchemaInference.ST_string;
									xmlSchemaSimpleContentExtension2.LineNumber = 262144;
								}
							}
							else
							{
								XmlSchemaSimpleContent xmlSchemaSimpleContent = new XmlSchemaSimpleContent();
								xmlSchemaComplexType.ContentModel = xmlSchemaSimpleContent;
								XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension3 = new XmlSchemaSimpleContentExtension();
								xmlSchemaSimpleContent.Content = xmlSchemaSimpleContentExtension3;
								this.MoveAttributes(xmlSchemaComplexType, xmlSchemaSimpleContentExtension3, bCreatingNewType);
								if (this.xtr.NodeType == XmlNodeType.Text)
								{
									int lineNumber2;
									if (!bCreatingNewType)
									{
										lineNumber2 = 262144;
									}
									else
									{
										lineNumber2 = -1;
									}
									xmlSchemaSimpleContentExtension3.BaseTypeName = this.RefineSimpleType(this.xtr.Value, ref lineNumber2);
									xmlSchemaSimpleContentExtension3.LineNumber = lineNumber2;
								}
								else
								{
									xmlSchemaSimpleContentExtension3.BaseTypeName = XmlSchemaInference.ST_string;
									xmlSchemaSimpleContentExtension3.LineNumber = 262144;
								}
							}
						}
						else if (num2 > 1)
						{
							xse.SchemaTypeName = XmlSchemaInference.ST_string;
							xse.LineNumber = 262144;
						}
						else
						{
							int num3 = -1;
							if (bCreatingNewType)
							{
								if (this.xtr.NodeType == XmlNodeType.Text)
								{
									xse.SchemaTypeName = this.RefineSimpleType(this.xtr.Value, ref num3);
									xse.LineNumber = num3;
								}
								else
								{
									xse.SchemaTypeName = XmlSchemaInference.ST_string;
									xse.LineNumber = 262144;
								}
							}
							else if (this.xtr.NodeType == XmlNodeType.Text)
							{
								if (xse.Parent == null)
								{
									num3 = xse.LineNumber;
								}
								else
								{
									num3 = XmlSchemaInference.GetSchemaType(xse.SchemaTypeName);
									if (num3 == -1 && xse.LineNumber == 262144)
									{
										num3 = 262144;
									}
									xse.Parent = null;
								}
								xse.SchemaTypeName = this.RefineSimpleType(this.xtr.Value, ref num3);
								xse.LineNumber = num3;
							}
							else
							{
								xse.SchemaTypeName = XmlSchemaInference.ST_string;
								xse.LineNumber = 262144;
							}
						}
					}
					else if (this.xtr.NodeType == XmlNodeType.Element)
					{
						XmlQualifiedName key = new XmlQualifiedName(this.xtr.LocalName, this.xtr.NamespaceURI);
						bool setMaxoccurs = false;
						if (hashtable.Contains(key))
						{
							setMaxoccurs = true;
						}
						else
						{
							hashtable.Add(key, null);
						}
						if (xmlSchemaComplexType == null)
						{
							xmlSchemaComplexType = new XmlSchemaComplexType();
							xse.SchemaType = xmlSchemaComplexType;
							if (!xse.SchemaTypeName.IsEmpty)
							{
								xmlSchemaComplexType.IsMixed = true;
								xse.SchemaTypeName = XmlQualifiedName.Empty;
							}
						}
						if (xmlSchemaComplexType.ContentModel != null)
						{
							XmlSchemaSimpleContentExtension scExtension = this.CheckSimpleContentExtension(xmlSchemaComplexType);
							this.MoveAttributes(scExtension, xmlSchemaComplexType);
							xmlSchemaComplexType.ContentModel = null;
							xmlSchemaComplexType.IsMixed = true;
							if (xmlSchemaComplexType.Particle != null)
							{
								throw new XmlSchemaInferenceException("SchInf_particle", 0, 0);
							}
							xmlSchemaComplexType.Particle = new XmlSchemaSequence();
							flag2 = true;
							XmlSchemaElement xmlSchemaElement = this.AddElement(this.xtr.LocalName, this.xtr.Prefix, this.xtr.NamespaceURI, parentSchema, ((XmlSchemaSequence)xmlSchemaComplexType.Particle).Items, -1);
							num = 0;
							if (!bCreatingNewType)
							{
								xmlSchemaComplexType.Particle.MinOccurs = 0m;
							}
						}
						else if (xmlSchemaComplexType.Particle == null)
						{
							xmlSchemaComplexType.Particle = new XmlSchemaSequence();
							flag2 = true;
							XmlSchemaElement xmlSchemaElement2 = this.AddElement(this.xtr.LocalName, this.xtr.Prefix, this.xtr.NamespaceURI, parentSchema, ((XmlSchemaSequence)xmlSchemaComplexType.Particle).Items, -1);
							if (!bCreatingNewType)
							{
								((XmlSchemaSequence)xmlSchemaComplexType.Particle).MinOccurs = 0m;
							}
							num = 0;
						}
						else
						{
							bool flag4 = false;
							XmlSchemaElement xmlSchemaElement3 = this.FindMatchingElement(bCreatingNewType || flag2, this.xtr, xmlSchemaComplexType, ref num, ref flag4, parentSchema, setMaxoccurs);
						}
					}
					else if (this.xtr.NodeType == XmlNodeType.Text)
					{
						if (xmlSchemaComplexType == null)
						{
							throw new XmlSchemaInferenceException("SchInf_ct", 0, 0);
						}
						xmlSchemaComplexType.IsMixed = true;
					}
					while (this.xtr.NodeType != XmlNodeType.EntityReference)
					{
						if (!flag3)
						{
							this.xtr.Read();
						}
						else
						{
							flag3 = false;
						}
						if (this.xtr.EOF || this.xtr.NodeType == XmlNodeType.EndElement || this.xtr.NodeType == XmlNodeType.CDATA || this.xtr.NodeType == XmlNodeType.Element || this.xtr.NodeType == XmlNodeType.Text)
						{
							goto IL_81D;
						}
					}
					throw new XmlSchemaInferenceException("SchInf_entity", 0, 0);
				}
				if (num != -1)
				{
					while (++num < ((XmlSchemaSequence)xmlSchemaComplexType.Particle).Items.Count)
					{
						if (((XmlSchemaSequence)xmlSchemaComplexType.Particle).Items[num].GetType() != typeof(XmlSchemaElement))
						{
							throw new XmlSchemaInferenceException("SchInf_seq", 0, 0);
						}
						XmlSchemaElement xmlSchemaElement4 = (XmlSchemaElement)((XmlSchemaSequence)xmlSchemaComplexType.Particle).Items[num];
						xmlSchemaElement4.MinOccurs = 0m;
					}
				}
				return;
			}
			if (flag)
			{
				if (xmlSchemaComplexType != null)
				{
					if (xmlSchemaComplexType.ContentModel != null)
					{
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension4 = this.CheckSimpleContentExtension(xmlSchemaComplexType);
						xmlSchemaSimpleContentExtension4.BaseTypeName = XmlSchemaInference.ST_string;
						xmlSchemaSimpleContentExtension4.LineNumber = 262144;
					}
					else if (bCreatingNewType)
					{
						XmlSchemaSimpleContent xmlSchemaSimpleContent2 = new XmlSchemaSimpleContent();
						xmlSchemaComplexType.ContentModel = xmlSchemaSimpleContent2;
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension5 = new XmlSchemaSimpleContentExtension();
						xmlSchemaSimpleContent2.Content = xmlSchemaSimpleContentExtension5;
						this.MoveAttributes(xmlSchemaComplexType, xmlSchemaSimpleContentExtension5, bCreatingNewType);
						xmlSchemaSimpleContentExtension5.BaseTypeName = XmlSchemaInference.ST_string;
						xmlSchemaSimpleContentExtension5.LineNumber = 262144;
					}
					else
					{
						xmlSchemaComplexType.IsMixed = true;
					}
				}
				else
				{
					xse.SchemaTypeName = XmlSchemaInference.ST_string;
					xse.LineNumber = 262144;
				}
			}
			if (bCreatingNewType)
			{
				xse.LineNumber = 262144;
				return;
			}
			if (xmlSchemaComplexType != null)
			{
				if (xmlSchemaComplexType.Particle != null)
				{
					xmlSchemaComplexType.Particle.MinOccurs = 0m;
					return;
				}
				if (xmlSchemaComplexType.ContentModel != null)
				{
					XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension6 = this.CheckSimpleContentExtension(xmlSchemaComplexType);
					xmlSchemaSimpleContentExtension6.BaseTypeName = XmlSchemaInference.ST_string;
					xmlSchemaSimpleContentExtension6.LineNumber = 262144;
					return;
				}
			}
			else if (!xse.SchemaTypeName.IsEmpty)
			{
				xse.LineNumber = 262144;
				xse.SchemaTypeName = XmlSchemaInference.ST_string;
			}
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000E4AF0 File Offset: 0x000E2CF0
		private XmlSchemaSimpleContentExtension CheckSimpleContentExtension(XmlSchemaComplexType ct)
		{
			XmlSchemaSimpleContent xmlSchemaSimpleContent = ct.ContentModel as XmlSchemaSimpleContent;
			if (xmlSchemaSimpleContent == null)
			{
				throw new XmlSchemaInferenceException("SchInf_simplecontent", 0, 0);
			}
			XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = xmlSchemaSimpleContent.Content as XmlSchemaSimpleContentExtension;
			if (xmlSchemaSimpleContentExtension == null)
			{
				throw new XmlSchemaInferenceException("SchInf_extension", 0, 0);
			}
			return xmlSchemaSimpleContentExtension;
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000E4B38 File Offset: 0x000E2D38
		private XmlSchemaType GetEffectiveSchemaType(XmlSchemaElement elem, bool bCreatingNewType)
		{
			XmlSchemaType xmlSchemaType = null;
			if (!bCreatingNewType && elem.ElementSchemaType != null)
			{
				xmlSchemaType = elem.ElementSchemaType;
			}
			else if (elem.SchemaType != null)
			{
				xmlSchemaType = elem.SchemaType;
			}
			else if (elem.SchemaTypeName != XmlQualifiedName.Empty)
			{
				xmlSchemaType = (this.schemaSet.GlobalTypes[elem.SchemaTypeName] as XmlSchemaType);
				if (xmlSchemaType == null)
				{
					xmlSchemaType = XmlSchemaType.GetBuiltInSimpleType(elem.SchemaTypeName);
				}
				if (xmlSchemaType == null)
				{
					xmlSchemaType = XmlSchemaType.GetBuiltInComplexType(elem.SchemaTypeName);
				}
			}
			return xmlSchemaType;
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000E4BBC File Offset: 0x000E2DBC
		internal XmlSchemaElement FindMatchingElement(bool bCreatingNewType, XmlReader xtr, XmlSchemaComplexType ct, ref int lastUsedSeqItem, ref bool bParticleChanged, XmlSchema parentSchema, bool setMaxoccurs)
		{
			if (xtr.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
			{
				throw new XmlSchemaInferenceException("SchInf_schema", 0, 0);
			}
			bool flag = lastUsedSeqItem == -1;
			XmlSchemaObjectCollection xmlSchemaObjectCollection = new XmlSchemaObjectCollection();
			if (!(ct.Particle.GetType() == typeof(XmlSchemaSequence)))
			{
				throw new XmlSchemaInferenceException("SchInf_noseq", 0, 0);
			}
			string text = xtr.NamespaceURI;
			if (text.Length == 0)
			{
				text = null;
			}
			XmlSchemaSequence xmlSchemaSequence = (XmlSchemaSequence)ct.Particle;
			if (xmlSchemaSequence.Items.Count < 1 && !bCreatingNewType)
			{
				lastUsedSeqItem = 0;
				XmlSchemaElement xmlSchemaElement = this.AddElement(xtr.LocalName, xtr.Prefix, xtr.NamespaceURI, parentSchema, xmlSchemaSequence.Items, -1);
				xmlSchemaElement.MinOccurs = 0m;
				return xmlSchemaElement;
			}
			if (xmlSchemaSequence.Items[0].GetType() == typeof(XmlSchemaChoice))
			{
				XmlSchemaChoice xmlSchemaChoice = (XmlSchemaChoice)xmlSchemaSequence.Items[0];
				for (int i = 0; i < xmlSchemaChoice.Items.Count; i++)
				{
					XmlSchemaElement xmlSchemaElement2 = xmlSchemaChoice.Items[i] as XmlSchemaElement;
					if (xmlSchemaElement2 == null)
					{
						throw new XmlSchemaInferenceException("SchInf_UnknownParticle", 0, 0);
					}
					if (xmlSchemaElement2.Name == xtr.LocalName && parentSchema.TargetNamespace == text)
					{
						this.InferElement(xmlSchemaElement2, false, parentSchema);
						this.SetMinMaxOccurs(xmlSchemaElement2, setMaxoccurs);
						return xmlSchemaElement2;
					}
					if (xmlSchemaElement2.RefName.Name == xtr.LocalName && xmlSchemaElement2.RefName.Namespace == xtr.NamespaceURI)
					{
						XmlSchemaElement xmlSchemaElement3 = this.FindGlobalElement(text, xtr.LocalName, out parentSchema);
						this.InferElement(xmlSchemaElement3, false, parentSchema);
						this.SetMinMaxOccurs(xmlSchemaElement2, setMaxoccurs);
						return xmlSchemaElement3;
					}
				}
				return this.AddElement(xtr.LocalName, xtr.Prefix, xtr.NamespaceURI, parentSchema, xmlSchemaChoice.Items, -1);
			}
			int j = 0;
			if (lastUsedSeqItem >= 0)
			{
				j = lastUsedSeqItem;
			}
			XmlSchemaParticle xmlSchemaParticle = xmlSchemaSequence.Items[j] as XmlSchemaParticle;
			XmlSchemaElement xmlSchemaElement4 = xmlSchemaParticle as XmlSchemaElement;
			if (xmlSchemaElement4 == null)
			{
				throw new XmlSchemaInferenceException("SchInf_UnknownParticle", 0, 0);
			}
			if (xmlSchemaElement4.Name == xtr.LocalName && parentSchema.TargetNamespace == text)
			{
				if (!flag)
				{
					xmlSchemaElement4.MaxOccurs = decimal.MaxValue;
				}
				lastUsedSeqItem = j;
				this.InferElement(xmlSchemaElement4, false, parentSchema);
				this.SetMinMaxOccurs(xmlSchemaElement4, false);
				return xmlSchemaElement4;
			}
			if (xmlSchemaElement4.RefName.Name == xtr.LocalName && xmlSchemaElement4.RefName.Namespace == xtr.NamespaceURI)
			{
				if (!flag)
				{
					xmlSchemaElement4.MaxOccurs = decimal.MaxValue;
				}
				lastUsedSeqItem = j;
				XmlSchemaElement xse = this.FindGlobalElement(text, xtr.LocalName, out parentSchema);
				this.InferElement(xse, false, parentSchema);
				this.SetMinMaxOccurs(xmlSchemaElement4, false);
				return xmlSchemaElement4;
			}
			if (flag && xmlSchemaElement4.MinOccurs != 0m)
			{
				xmlSchemaObjectCollection.Add(xmlSchemaElement4);
			}
			for (j++; j < xmlSchemaSequence.Items.Count; j++)
			{
				xmlSchemaParticle = (xmlSchemaSequence.Items[j] as XmlSchemaParticle);
				xmlSchemaElement4 = (xmlSchemaParticle as XmlSchemaElement);
				if (xmlSchemaElement4 == null)
				{
					throw new XmlSchemaInferenceException("SchInf_UnknownParticle", 0, 0);
				}
				if (xmlSchemaElement4.Name == xtr.LocalName && parentSchema.TargetNamespace == text)
				{
					lastUsedSeqItem = j;
					for (int k = 0; k < xmlSchemaObjectCollection.Count; k++)
					{
						((XmlSchemaElement)xmlSchemaObjectCollection[k]).MinOccurs = 0m;
					}
					this.InferElement(xmlSchemaElement4, false, parentSchema);
					this.SetMinMaxOccurs(xmlSchemaElement4, setMaxoccurs);
					return xmlSchemaElement4;
				}
				if (xmlSchemaElement4.RefName.Name == xtr.LocalName && xmlSchemaElement4.RefName.Namespace == xtr.NamespaceURI)
				{
					lastUsedSeqItem = j;
					for (int l = 0; l < xmlSchemaObjectCollection.Count; l++)
					{
						((XmlSchemaElement)xmlSchemaObjectCollection[l]).MinOccurs = 0m;
					}
					XmlSchemaElement xmlSchemaElement5 = this.FindGlobalElement(text, xtr.LocalName, out parentSchema);
					this.InferElement(xmlSchemaElement5, false, parentSchema);
					this.SetMinMaxOccurs(xmlSchemaElement4, setMaxoccurs);
					return xmlSchemaElement5;
				}
				xmlSchemaObjectCollection.Add(xmlSchemaElement4);
			}
			XmlSchemaElement xse2 = null;
			XmlSchemaElement xmlSchemaElement6;
			if (parentSchema.TargetNamespace == text)
			{
				xmlSchemaElement6 = this.FindElement(xmlSchemaSequence.Items, xtr.LocalName);
				xse2 = xmlSchemaElement6;
			}
			else
			{
				xmlSchemaElement6 = this.FindElementRef(xmlSchemaSequence.Items, xtr.LocalName, xtr.NamespaceURI);
				if (xmlSchemaElement6 != null)
				{
					xse2 = this.FindGlobalElement(text, xtr.LocalName, out parentSchema);
				}
			}
			if (xmlSchemaElement6 != null)
			{
				XmlSchemaChoice xmlSchemaChoice2 = new XmlSchemaChoice();
				xmlSchemaChoice2.MaxOccurs = decimal.MaxValue;
				this.SetMinMaxOccurs(xmlSchemaElement6, setMaxoccurs);
				this.InferElement(xse2, false, parentSchema);
				for (int m = 0; m < xmlSchemaSequence.Items.Count; m++)
				{
					xmlSchemaChoice2.Items.Add(this.CreateNewElementforChoice((XmlSchemaElement)xmlSchemaSequence.Items[m]));
				}
				xmlSchemaSequence.Items.Clear();
				xmlSchemaSequence.Items.Add(xmlSchemaChoice2);
				return xmlSchemaElement6;
			}
			string localName = xtr.LocalName;
			string prefix = xtr.Prefix;
			string namespaceURI = xtr.NamespaceURI;
			XmlSchema parentSchema2 = parentSchema;
			XmlSchemaObjectCollection items = xmlSchemaSequence.Items;
			int num = lastUsedSeqItem + 1;
			lastUsedSeqItem = num;
			xmlSchemaElement6 = this.AddElement(localName, prefix, namespaceURI, parentSchema2, items, num);
			if (!bCreatingNewType)
			{
				xmlSchemaElement6.MinOccurs = 0m;
			}
			return xmlSchemaElement6;
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000E5168 File Offset: 0x000E3368
		internal void ProcessAttributes(ref XmlSchemaElement xse, XmlSchemaType effectiveSchemaType, bool bCreatingNewType, XmlSchema parentSchema)
		{
			XmlSchemaObjectCollection xmlSchemaObjectCollection = new XmlSchemaObjectCollection();
			XmlSchemaComplexType xmlSchemaComplexType = effectiveSchemaType as XmlSchemaComplexType;
			while (!(this.xtr.NamespaceURI == "http://www.w3.org/2001/XMLSchema"))
			{
				if (this.xtr.NamespaceURI == "http://www.w3.org/2000/xmlns/")
				{
					if (this.xtr.Prefix == "xmlns")
					{
						this.NamespaceManager.AddNamespace(this.xtr.LocalName, this.xtr.Value);
					}
				}
				else if (this.xtr.NamespaceURI == "http://www.w3.org/2001/XMLSchema-instance")
				{
					string localName = this.xtr.LocalName;
					if (localName == "nil")
					{
						xse.IsNillable = true;
					}
					else if (localName != "type" && localName != "schemaLocation" && localName != "noNamespaceSchemaLocation")
					{
						throw new XmlSchemaInferenceException("Sch_NotXsiAttribute", localName);
					}
				}
				else
				{
					if (xmlSchemaComplexType == null || xmlSchemaComplexType == XmlSchemaComplexType.AnyType)
					{
						xmlSchemaComplexType = new XmlSchemaComplexType();
						xse.SchemaType = xmlSchemaComplexType;
					}
					if (effectiveSchemaType != null && effectiveSchemaType.Datatype != null && !xse.SchemaTypeName.IsEmpty)
					{
						XmlSchemaSimpleContent xmlSchemaSimpleContent = new XmlSchemaSimpleContent();
						xmlSchemaComplexType.ContentModel = xmlSchemaSimpleContent;
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = new XmlSchemaSimpleContentExtension();
						xmlSchemaSimpleContent.Content = xmlSchemaSimpleContentExtension;
						xmlSchemaSimpleContentExtension.BaseTypeName = xse.SchemaTypeName;
						xmlSchemaSimpleContentExtension.LineNumber = xse.LineNumber;
						xse.LineNumber = 0;
						xse.SchemaTypeName = XmlQualifiedName.Empty;
					}
					XmlSchemaAttribute xmlSchemaAttribute;
					if (xmlSchemaComplexType.ContentModel != null)
					{
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension2 = this.CheckSimpleContentExtension(xmlSchemaComplexType);
						xmlSchemaAttribute = this.AddAttribute(this.xtr.LocalName, this.xtr.Prefix, this.xtr.NamespaceURI, this.xtr.Value, bCreatingNewType, parentSchema, xmlSchemaSimpleContentExtension2.Attributes, xmlSchemaComplexType.AttributeUses);
					}
					else
					{
						xmlSchemaAttribute = this.AddAttribute(this.xtr.LocalName, this.xtr.Prefix, this.xtr.NamespaceURI, this.xtr.Value, bCreatingNewType, parentSchema, xmlSchemaComplexType.Attributes, xmlSchemaComplexType.AttributeUses);
					}
					if (xmlSchemaAttribute != null)
					{
						xmlSchemaObjectCollection.Add(xmlSchemaAttribute);
					}
				}
				if (!this.xtr.MoveToNextAttribute())
				{
					if (!bCreatingNewType && xmlSchemaComplexType != null)
					{
						this.MakeExistingAttributesOptional(xmlSchemaComplexType, xmlSchemaObjectCollection);
					}
					return;
				}
			}
			throw new XmlSchemaInferenceException("SchInf_schema", 0, 0);
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000E53BC File Offset: 0x000E35BC
		private void MoveAttributes(XmlSchemaSimpleContentExtension scExtension, XmlSchemaComplexType ct)
		{
			for (int i = 0; i < scExtension.Attributes.Count; i++)
			{
				ct.Attributes.Add(scExtension.Attributes[i]);
			}
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000E53F8 File Offset: 0x000E35F8
		private void MoveAttributes(XmlSchemaComplexType ct, XmlSchemaSimpleContentExtension simpleContentExtension, bool bCreatingNewType)
		{
			ICollection collection;
			if (!bCreatingNewType && ct.AttributeUses.Count > 0)
			{
				collection = ct.AttributeUses.Values;
			}
			else
			{
				collection = ct.Attributes;
			}
			foreach (object obj in collection)
			{
				XmlSchemaAttribute item = (XmlSchemaAttribute)obj;
				simpleContentExtension.Attributes.Add(item);
			}
			ct.Attributes.Clear();
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x000E5484 File Offset: 0x000E3684
		internal XmlSchemaAttribute FindAttribute(ICollection attributes, string attrName)
		{
			foreach (object obj in attributes)
			{
				XmlSchemaObject xmlSchemaObject = (XmlSchemaObject)obj;
				XmlSchemaAttribute xmlSchemaAttribute = xmlSchemaObject as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null && xmlSchemaAttribute.Name == attrName)
				{
					return xmlSchemaAttribute;
				}
			}
			return null;
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x000E54F4 File Offset: 0x000E36F4
		internal XmlSchemaElement FindGlobalElement(string namespaceURI, string localName, out XmlSchema parentSchema)
		{
			ICollection collection = this.schemaSet.Schemas(namespaceURI);
			parentSchema = null;
			foreach (object obj in collection)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				XmlSchemaElement xmlSchemaElement = this.FindElement(xmlSchema.Items, localName);
				if (xmlSchemaElement != null)
				{
					parentSchema = xmlSchema;
					return xmlSchemaElement;
				}
			}
			return null;
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000E5574 File Offset: 0x000E3774
		internal XmlSchemaElement FindElement(XmlSchemaObjectCollection elements, string elementName)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				XmlSchemaElement xmlSchemaElement = elements[i] as XmlSchemaElement;
				if (xmlSchemaElement != null && xmlSchemaElement.RefName != null && xmlSchemaElement.Name == elementName)
				{
					return xmlSchemaElement;
				}
			}
			return null;
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000E55C4 File Offset: 0x000E37C4
		internal XmlSchemaAttribute FindAttributeRef(ICollection attributes, string attributeName, string nsURI)
		{
			foreach (object obj in attributes)
			{
				XmlSchemaObject xmlSchemaObject = (XmlSchemaObject)obj;
				XmlSchemaAttribute xmlSchemaAttribute = xmlSchemaObject as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null && xmlSchemaAttribute.RefName.Name == attributeName && xmlSchemaAttribute.RefName.Namespace == nsURI)
				{
					return xmlSchemaAttribute;
				}
			}
			return null;
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000E564C File Offset: 0x000E384C
		internal XmlSchemaElement FindElementRef(XmlSchemaObjectCollection elements, string elementName, string nsURI)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				XmlSchemaElement xmlSchemaElement = elements[i] as XmlSchemaElement;
				if (xmlSchemaElement != null && xmlSchemaElement.RefName != null && xmlSchemaElement.RefName.Name == elementName && xmlSchemaElement.RefName.Namespace == nsURI)
				{
					return xmlSchemaElement;
				}
			}
			return null;
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000E56B4 File Offset: 0x000E38B4
		internal void MakeExistingAttributesOptional(XmlSchemaComplexType ct, XmlSchemaObjectCollection attributesInInstance)
		{
			if (ct == null)
			{
				throw new XmlSchemaInferenceException("SchInf_noct", 0, 0);
			}
			if (ct.ContentModel != null)
			{
				XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = this.CheckSimpleContentExtension(ct);
				this.SwitchUseToOptional(xmlSchemaSimpleContentExtension.Attributes, attributesInInstance);
				return;
			}
			this.SwitchUseToOptional(ct.Attributes, attributesInInstance);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000E56FC File Offset: 0x000E38FC
		private void SwitchUseToOptional(XmlSchemaObjectCollection attributes, XmlSchemaObjectCollection attributesInInstance)
		{
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlSchemaAttribute xmlSchemaAttribute = attributes[i] as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					if (attributesInInstance != null)
					{
						if (xmlSchemaAttribute.RefName.Name.Length == 0)
						{
							if (this.FindAttribute(attributesInInstance, xmlSchemaAttribute.Name) == null)
							{
								xmlSchemaAttribute.Use = XmlSchemaUse.Optional;
							}
						}
						else if (this.FindAttributeRef(attributesInInstance, xmlSchemaAttribute.RefName.Name, xmlSchemaAttribute.RefName.Namespace) == null)
						{
							xmlSchemaAttribute.Use = XmlSchemaUse.Optional;
						}
					}
					else
					{
						xmlSchemaAttribute.Use = XmlSchemaUse.Optional;
					}
				}
			}
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000E5788 File Offset: 0x000E3988
		internal XmlQualifiedName RefineSimpleType(string s, ref int iTypeFlags)
		{
			bool flag = false;
			s = s.Trim();
			if (iTypeFlags == 262144 || this.typeInference == XmlSchemaInference.InferenceOption.Relaxed)
			{
				return XmlSchemaInference.ST_string;
			}
			iTypeFlags &= XmlSchemaInference.InferSimpleType(s, ref flag);
			if (iTypeFlags == 262144)
			{
				return XmlSchemaInference.ST_string;
			}
			if (flag)
			{
				if ((iTypeFlags & 2) != 0)
				{
					try
					{
						XmlConvert.ToSByte(s);
						if ((iTypeFlags & 4) != 0)
						{
							return XmlSchemaInference.ST_unsignedByte;
						}
						return XmlSchemaInference.ST_byte;
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					iTypeFlags &= -3;
				}
				if ((iTypeFlags & 4) != 0)
				{
					try
					{
						XmlConvert.ToByte(s);
						return XmlSchemaInference.ST_unsignedByte;
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					iTypeFlags &= -5;
				}
				if ((iTypeFlags & 8) != 0)
				{
					try
					{
						XmlConvert.ToInt16(s);
						if ((iTypeFlags & 16) != 0)
						{
							return XmlSchemaInference.ST_unsignedShort;
						}
						return XmlSchemaInference.ST_short;
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					iTypeFlags &= -9;
				}
				if ((iTypeFlags & 16) != 0)
				{
					try
					{
						XmlConvert.ToUInt16(s);
						return XmlSchemaInference.ST_unsignedShort;
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					iTypeFlags &= -17;
				}
				if ((iTypeFlags & 32) != 0)
				{
					try
					{
						XmlConvert.ToInt32(s);
						if ((iTypeFlags & 64) != 0)
						{
							return XmlSchemaInference.ST_unsignedInt;
						}
						return XmlSchemaInference.ST_int;
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					iTypeFlags &= -33;
				}
				if ((iTypeFlags & 64) != 0)
				{
					try
					{
						XmlConvert.ToUInt32(s);
						return XmlSchemaInference.ST_unsignedInt;
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					iTypeFlags &= -65;
				}
				if ((iTypeFlags & 128) != 0)
				{
					try
					{
						XmlConvert.ToInt64(s);
						if ((iTypeFlags & 256) != 0)
						{
							return XmlSchemaInference.ST_unsignedLong;
						}
						return XmlSchemaInference.ST_long;
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					iTypeFlags &= -129;
				}
				if ((iTypeFlags & 256) != 0)
				{
					try
					{
						XmlConvert.ToUInt64(s);
						return XmlSchemaInference.ST_unsignedLong;
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					iTypeFlags &= -257;
				}
				if ((iTypeFlags & 4096) != 0)
				{
					try
					{
						double value = XmlConvert.ToDouble(s);
						if ((iTypeFlags & 512) != 0)
						{
							return XmlSchemaInference.ST_integer;
						}
						if ((iTypeFlags & 1024) != 0)
						{
							return XmlSchemaInference.ST_decimal;
						}
						if ((iTypeFlags & 2048) != 0)
						{
							try
							{
								float value2 = XmlConvert.ToSingle(s);
								if (string.Compare(XmlConvert.ToString(value2), XmlConvert.ToString(value), StringComparison.OrdinalIgnoreCase) == 0)
								{
									return XmlSchemaInference.ST_float;
								}
							}
							catch (FormatException)
							{
							}
							catch (OverflowException)
							{
							}
						}
						iTypeFlags &= -2049;
						return XmlSchemaInference.ST_double;
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					iTypeFlags &= -4097;
				}
				if ((iTypeFlags & 2048) != 0)
				{
					try
					{
						XmlConvert.ToSingle(s);
						if ((iTypeFlags & 512) != 0)
						{
							return XmlSchemaInference.ST_integer;
						}
						if ((iTypeFlags & 1024) != 0)
						{
							return XmlSchemaInference.ST_decimal;
						}
						return XmlSchemaInference.ST_float;
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					iTypeFlags &= -2049;
				}
				if ((iTypeFlags & 512) != 0)
				{
					return XmlSchemaInference.ST_integer;
				}
				if ((iTypeFlags & 1024) != 0)
				{
					return XmlSchemaInference.ST_decimal;
				}
				if (iTypeFlags == 393216)
				{
					try
					{
						XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.RoundtripKind);
						return XmlSchemaInference.ST_gYearMonth;
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					iTypeFlags = 262144;
					return XmlSchemaInference.ST_string;
				}
				if (iTypeFlags == 270336)
				{
					try
					{
						XmlConvert.ToTimeSpan(s);
						return XmlSchemaInference.ST_duration;
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					iTypeFlags = 262144;
					return XmlSchemaInference.ST_string;
				}
				if (iTypeFlags == 262145)
				{
					return XmlSchemaInference.ST_boolean;
				}
			}
			int num = iTypeFlags;
			if (num <= 4096)
			{
				if (num <= 64)
				{
					if (num <= 8)
					{
						switch (num)
						{
						case 1:
							return XmlSchemaInference.ST_boolean;
						case 2:
							return XmlSchemaInference.ST_byte;
						case 3:
							break;
						case 4:
							return XmlSchemaInference.ST_unsignedByte;
						default:
							if (num == 8)
							{
								return XmlSchemaInference.ST_short;
							}
							break;
						}
					}
					else
					{
						if (num == 16)
						{
							return XmlSchemaInference.ST_unsignedShort;
						}
						if (num == 32)
						{
							return XmlSchemaInference.ST_int;
						}
						if (num == 64)
						{
							return XmlSchemaInference.ST_unsignedInt;
						}
					}
				}
				else if (num <= 512)
				{
					if (num == 128)
					{
						return XmlSchemaInference.ST_long;
					}
					if (num == 256)
					{
						return XmlSchemaInference.ST_unsignedLong;
					}
					if (num == 512)
					{
						return XmlSchemaInference.ST_integer;
					}
				}
				else
				{
					if (num == 1024)
					{
						return XmlSchemaInference.ST_decimal;
					}
					if (num == 2048)
					{
						return XmlSchemaInference.ST_float;
					}
					if (num == 4096)
					{
						return XmlSchemaInference.ST_double;
					}
				}
			}
			else if (num <= 262144)
			{
				if (num <= 32768)
				{
					if (num == 8192)
					{
						return XmlSchemaInference.ST_duration;
					}
					if (num == 16384)
					{
						return XmlSchemaInference.ST_dateTime;
					}
					if (num == 32768)
					{
						return XmlSchemaInference.ST_time;
					}
				}
				else
				{
					if (num == 65536)
					{
						return XmlSchemaInference.ST_date;
					}
					if (num == 131072)
					{
						return XmlSchemaInference.ST_gYearMonth;
					}
					if (num == 262144)
					{
						return XmlSchemaInference.ST_string;
					}
				}
			}
			else if (num <= 268288)
			{
				if (num == 262145)
				{
					return XmlSchemaInference.ST_boolean;
				}
				if (num == 266240)
				{
					return XmlSchemaInference.ST_double;
				}
				if (num == 268288)
				{
					return XmlSchemaInference.ST_float;
				}
			}
			else
			{
				if (num == 278528)
				{
					return XmlSchemaInference.ST_dateTime;
				}
				if (num == 294912)
				{
					return XmlSchemaInference.ST_time;
				}
				if (num == 327680)
				{
					return XmlSchemaInference.ST_date;
				}
			}
			return XmlSchemaInference.ST_string;
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000E5F64 File Offset: 0x000E4164
		internal static int InferSimpleType(string s, ref bool bNeedsRangeCheck)
		{
			bool flag = false;
			bool flag2 = false;
			bool bDate = false;
			bool bTime = false;
			bool flag3 = false;
			if (s.Length == 0)
			{
				return 262144;
			}
			int num = 0;
			char c = s[num];
			if (c <= 'N')
			{
				switch (c)
				{
				case '+':
				{
					flag2 = true;
					num++;
					if (num == s.Length)
					{
						return 262144;
					}
					char c2 = s[num];
					if (c2 == '.')
					{
						goto IL_100;
					}
					if (c2 == 'P')
					{
						goto IL_30B;
					}
					if (s[num] < '0' || s[num] > '9')
					{
						return 262144;
					}
					goto IL_742;
				}
				case ',':
				case '/':
					return 262144;
				case '-':
				{
					flag = true;
					num++;
					if (num == s.Length)
					{
						return 262144;
					}
					char c3 = s[num];
					if (c3 == '.')
					{
						goto IL_100;
					}
					if (c3 != 'I')
					{
						if (c3 == 'P')
						{
							goto IL_30B;
						}
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
						goto IL_742;
					}
					break;
				}
				case '.':
					goto IL_100;
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					goto IL_742;
				default:
					if (c != 'I')
					{
						if (c != 'N')
						{
							return 262144;
						}
						if (s == "NaN")
						{
							return 268288;
						}
						return 262144;
					}
					break;
				}
				if (s.Substring(num) == "INF")
				{
					return 268288;
				}
				return 262144;
				IL_100:
				bNeedsRangeCheck = true;
				num++;
				if (num == s.Length)
				{
					if (num == 1 || (num == 2 && (flag2 || flag)))
					{
						return 262144;
					}
					return 269312;
				}
				else
				{
					char c4 = s[num];
					if (c4 != 'E' && c4 != 'e')
					{
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
						for (;;)
						{
							num++;
							if (num == s.Length)
							{
								break;
							}
							char c5 = s[num];
							if (c5 == 'E' || c5 == 'e')
							{
								goto IL_1AC;
							}
							if (s[num] < '0' || s[num] > '9')
							{
								return 262144;
							}
						}
						return 269312;
					}
					IL_1AC:
					num++;
					if (num == s.Length)
					{
						return 262144;
					}
					char c6 = s[num];
					if (c6 != '+' && c6 != '-')
					{
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
					}
					else
					{
						num++;
						if (num == s.Length)
						{
							return 262144;
						}
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
					}
					for (;;)
					{
						num++;
						if (num == s.Length)
						{
							break;
						}
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
					}
					return 268288;
				}
				IL_742:
				num++;
				if (num == s.Length)
				{
					bNeedsRangeCheck = true;
					if (flag || flag2)
					{
						return 269994;
					}
					if (s == "0" || s == "1")
					{
						return 270335;
					}
					return 270334;
				}
				else
				{
					char c7 = s[num];
					if (c7 == '.')
					{
						goto IL_100;
					}
					if (c7 == 'E' || c7 == 'e')
					{
						bNeedsRangeCheck = true;
						return 268288;
					}
					if (s[num] < '0' || s[num] > '9')
					{
						return 262144;
					}
					num++;
					if (num == s.Length)
					{
						bNeedsRangeCheck = true;
						if (flag || flag2)
						{
							return 269994;
						}
						return 270334;
					}
					else
					{
						char c8 = s[num];
						if (c8 <= ':')
						{
							if (c8 == '.')
							{
								goto IL_100;
							}
							if (c8 == ':')
							{
								bTime = true;
								goto IL_CA4;
							}
						}
						else if (c8 == 'E' || c8 == 'e')
						{
							bNeedsRangeCheck = true;
							return 268288;
						}
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
						num++;
						if (num == s.Length)
						{
							bNeedsRangeCheck = true;
							if (flag || flag2)
							{
								return 269994;
							}
							return 270334;
						}
						else
						{
							char c9 = s[num];
							if (c9 == '.')
							{
								goto IL_100;
							}
							if (c9 == 'E' || c9 == 'e')
							{
								bNeedsRangeCheck = true;
								return 268288;
							}
							if (s[num] < '0' || s[num] > '9')
							{
								return 262144;
							}
							for (;;)
							{
								num++;
								if (num == s.Length)
								{
									break;
								}
								char c10 = s[num];
								if (c10 <= '.')
								{
									if (c10 == '-')
									{
										goto IL_90A;
									}
									if (c10 == '.')
									{
										goto IL_100;
									}
								}
								else if (c10 == 'E' || c10 == 'e')
								{
									goto IL_90E;
								}
								if (s[num] < '0' || s[num] > '9')
								{
									return 262144;
								}
							}
							bNeedsRangeCheck = true;
							if (flag || flag2)
							{
								return 269994;
							}
							return 270334;
							IL_90A:
							bDate = true;
							num++;
							if (num == s.Length)
							{
								return 262144;
							}
							if (s[num] < '0' || s[num] > '9')
							{
								return 262144;
							}
							num++;
							if (num == s.Length)
							{
								return 262144;
							}
							if (s[num] < '0' || s[num] > '9')
							{
								return 262144;
							}
							num++;
							if (num == s.Length)
							{
								bNeedsRangeCheck = true;
								return 393216;
							}
							char c11 = s[num];
							if (c11 <= '-')
							{
								if (c11 == '+')
								{
									flag3 = true;
									goto IL_AF0;
								}
								if (c11 == '-')
								{
									num++;
									if (num == s.Length)
									{
										return 262144;
									}
									if (s[num] < '0' || s[num] > '9')
									{
										return 262144;
									}
									num++;
									if (num == s.Length)
									{
										return 262144;
									}
									if (s[num] < '0' || s[num] > '9')
									{
										return 262144;
									}
									num++;
									if (num == s.Length)
									{
										return XmlSchemaInference.DateTime(s, bDate, bTime);
									}
									char c12 = s[num];
									if (c12 <= ':')
									{
										if (c12 == '+' || c12 == '-')
										{
											goto IL_AF0;
										}
										if (c12 == ':')
										{
											flag3 = true;
											goto IL_B80;
										}
									}
									else if (c12 != 'T')
									{
										if (c12 == 'Z' || c12 == 'z')
										{
											goto IL_AC4;
										}
									}
									else
									{
										bTime = true;
										num++;
										if (num == s.Length)
										{
											return 262144;
										}
										if (s[num] < '0' || s[num] > '9')
										{
											return 262144;
										}
										num++;
										if (num == s.Length)
										{
											return 262144;
										}
										if (s[num] < '0' || s[num] > '9')
										{
											return 262144;
										}
										num++;
										if (num == s.Length)
										{
											return 262144;
										}
										if (s[num] != ':')
										{
											return 262144;
										}
										goto IL_CA4;
									}
									return 262144;
								}
							}
							else if (c11 == 'Z' || c11 == 'z')
							{
								flag3 = true;
								goto IL_AC4;
							}
							return 262144;
							IL_90E:
							bNeedsRangeCheck = true;
							return 268288;
						}
						IL_AC4:
						num++;
						if (num != s.Length)
						{
							return 262144;
						}
						if (flag3)
						{
							bNeedsRangeCheck = true;
							return 393216;
						}
						return XmlSchemaInference.DateTime(s, bDate, bTime);
						IL_AF0:
						num++;
						if (num == s.Length)
						{
							return 262144;
						}
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
						num++;
						if (num == s.Length)
						{
							return 262144;
						}
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
						num++;
						if (num == s.Length)
						{
							return 262144;
						}
						if (s[num] != ':')
						{
							return 262144;
						}
						IL_B80:
						num++;
						if (num == s.Length)
						{
							return 262144;
						}
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
						num++;
						if (num == s.Length)
						{
							return 262144;
						}
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
						num++;
						if (num != s.Length)
						{
							return 262144;
						}
						if (flag3)
						{
							bNeedsRangeCheck = true;
							return 393216;
						}
						return XmlSchemaInference.DateTime(s, bDate, bTime);
						IL_CA4:
						num++;
						if (num == s.Length)
						{
							return 262144;
						}
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
						num++;
						if (num == s.Length)
						{
							return 262144;
						}
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
						num++;
						if (num == s.Length)
						{
							return 262144;
						}
						if (s[num] != ':')
						{
							return 262144;
						}
						num++;
						if (num == s.Length)
						{
							return 262144;
						}
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
						num++;
						if (num == s.Length)
						{
							return 262144;
						}
						if (s[num] < '0' || s[num] > '9')
						{
							return 262144;
						}
						num++;
						if (num == s.Length)
						{
							return XmlSchemaInference.DateTime(s, bDate, bTime);
						}
						char c13 = s[num];
						switch (c13)
						{
						case '+':
						case '-':
							goto IL_AF0;
						case ',':
							break;
						case '.':
							num++;
							if (num == s.Length)
							{
								return 262144;
							}
							if (s[num] < '0' || s[num] > '9')
							{
								return 262144;
							}
							for (;;)
							{
								num++;
								if (num == s.Length)
								{
									break;
								}
								char c14 = s[num];
								if (c14 <= '-')
								{
									if (c14 == '+' || c14 == '-')
									{
										goto IL_AF0;
									}
								}
								else if (c14 == 'Z' || c14 == 'z')
								{
									goto IL_AC4;
								}
								if (s[num] < '0' || s[num] > '9')
								{
									return 262144;
								}
							}
							return XmlSchemaInference.DateTime(s, bDate, bTime);
						default:
							if (c13 == 'Z' || c13 == 'z')
							{
								goto IL_AC4;
							}
							break;
						}
						return 262144;
					}
				}
			}
			else if (c != 'P')
			{
				if (c != 'f' && c != 't')
				{
					return 262144;
				}
				if (s == "true")
				{
					return 262145;
				}
				if (s == "false")
				{
					return 262145;
				}
				return 262144;
			}
			IL_30B:
			num++;
			if (num == s.Length)
			{
				return 262144;
			}
			char c15 = s[num];
			if (c15 != 'T')
			{
				if (s[num] < '0' || s[num] > '9')
				{
					return 262144;
				}
				for (;;)
				{
					num++;
					if (num == s.Length)
					{
						break;
					}
					char c16 = s[num];
					if (c16 == 'D')
					{
						goto IL_4CD;
					}
					if (c16 == 'M')
					{
						goto IL_43F;
					}
					if (c16 == 'Y')
					{
						goto IL_3A8;
					}
					if (s[num] < '0' || s[num] > '9')
					{
						return 262144;
					}
				}
				return 262144;
				IL_3A8:
				num++;
				if (num == s.Length)
				{
					bNeedsRangeCheck = true;
					return 270336;
				}
				char c17 = s[num];
				if (c17 == 'T')
				{
					goto IL_4FC;
				}
				if (s[num] < '0' || s[num] > '9')
				{
					return 262144;
				}
				for (;;)
				{
					num++;
					if (num == s.Length)
					{
						break;
					}
					char c18 = s[num];
					if (c18 == 'D')
					{
						goto IL_4CD;
					}
					if (c18 == 'M')
					{
						goto IL_43F;
					}
					if (s[num] < '0' || s[num] > '9')
					{
						return 262144;
					}
				}
				return 262144;
				IL_43F:
				num++;
				if (num == s.Length)
				{
					bNeedsRangeCheck = true;
					return 270336;
				}
				char c19 = s[num];
				if (c19 == 'T')
				{
					goto IL_4FC;
				}
				if (s[num] < '0' || s[num] > '9')
				{
					return 262144;
				}
				for (;;)
				{
					num++;
					if (num == s.Length)
					{
						break;
					}
					char c20 = s[num];
					if (c20 == 'D')
					{
						goto IL_4CD;
					}
					if (s[num] < '0' || s[num] > '9')
					{
						return 262144;
					}
				}
				return 262144;
				IL_4CD:
				num++;
				if (num == s.Length)
				{
					bNeedsRangeCheck = true;
					return 270336;
				}
				char c21 = s[num];
				if (c21 != 'T')
				{
					return 262144;
				}
			}
			IL_4FC:
			num++;
			if (num == s.Length)
			{
				return 262144;
			}
			if (s[num] < '0' || s[num] > '9')
			{
				return 262144;
			}
			for (;;)
			{
				num++;
				if (num == s.Length)
				{
					break;
				}
				char c22 = s[num];
				if (c22 <= 'H')
				{
					if (c22 == '.')
					{
						goto IL_6A8;
					}
					if (c22 == 'H')
					{
						goto IL_597;
					}
				}
				else
				{
					if (c22 == 'M')
					{
						goto IL_624;
					}
					if (c22 == 'S')
					{
						goto IL_723;
					}
				}
				if (s[num] < '0' || s[num] > '9')
				{
					return 262144;
				}
			}
			return 262144;
			IL_597:
			num++;
			if (num == s.Length)
			{
				bNeedsRangeCheck = true;
				return 270336;
			}
			if (s[num] < '0' || s[num] > '9')
			{
				return 262144;
			}
			for (;;)
			{
				num++;
				if (num == s.Length)
				{
					break;
				}
				char c23 = s[num];
				if (c23 == '.')
				{
					goto IL_6A8;
				}
				if (c23 == 'M')
				{
					goto IL_624;
				}
				if (c23 == 'S')
				{
					goto IL_723;
				}
				if (s[num] < '0' || s[num] > '9')
				{
					return 262144;
				}
			}
			return 262144;
			IL_624:
			num++;
			if (num == s.Length)
			{
				bNeedsRangeCheck = true;
				return 270336;
			}
			if (s[num] < '0' || s[num] > '9')
			{
				return 262144;
			}
			for (;;)
			{
				num++;
				if (num == s.Length)
				{
					break;
				}
				char c24 = s[num];
				if (c24 == '.')
				{
					goto IL_6A8;
				}
				if (c24 == 'S')
				{
					goto IL_723;
				}
				if (s[num] < '0' || s[num] > '9')
				{
					return 262144;
				}
			}
			return 262144;
			IL_6A8:
			num++;
			if (num == s.Length)
			{
				bNeedsRangeCheck = true;
				return 270336;
			}
			if (s[num] < '0' || s[num] > '9')
			{
				return 262144;
			}
			for (;;)
			{
				num++;
				if (num == s.Length)
				{
					break;
				}
				char c25 = s[num];
				if (c25 == 'S')
				{
					goto IL_723;
				}
				if (s[num] < '0' || s[num] > '9')
				{
					return 262144;
				}
			}
			return 262144;
			IL_723:
			num++;
			if (num == s.Length)
			{
				bNeedsRangeCheck = true;
				return 270336;
			}
			return 262144;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000E6E08 File Offset: 0x000E5008
		internal static int DateTime(string s, bool bDate, bool bTime)
		{
			try
			{
				XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.RoundtripKind);
			}
			catch (FormatException)
			{
				return 262144;
			}
			if (bDate && bTime)
			{
				return 278528;
			}
			if (bDate)
			{
				return 327680;
			}
			if (bTime)
			{
				return 294912;
			}
			return 262144;
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000E6E5C File Offset: 0x000E505C
		private XmlSchemaElement CreateNewElementforChoice(XmlSchemaElement copyElement)
		{
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Annotation = copyElement.Annotation;
			xmlSchemaElement.Block = copyElement.Block;
			xmlSchemaElement.DefaultValue = copyElement.DefaultValue;
			xmlSchemaElement.Final = copyElement.Final;
			xmlSchemaElement.FixedValue = copyElement.FixedValue;
			xmlSchemaElement.Form = copyElement.Form;
			xmlSchemaElement.Id = copyElement.Id;
			if (copyElement.IsNillable)
			{
				xmlSchemaElement.IsNillable = copyElement.IsNillable;
			}
			xmlSchemaElement.LineNumber = copyElement.LineNumber;
			xmlSchemaElement.LinePosition = copyElement.LinePosition;
			xmlSchemaElement.Name = copyElement.Name;
			xmlSchemaElement.Namespaces = copyElement.Namespaces;
			xmlSchemaElement.RefName = copyElement.RefName;
			xmlSchemaElement.SchemaType = copyElement.SchemaType;
			xmlSchemaElement.SchemaTypeName = copyElement.SchemaTypeName;
			xmlSchemaElement.SourceUri = copyElement.SourceUri;
			xmlSchemaElement.SubstitutionGroup = copyElement.SubstitutionGroup;
			xmlSchemaElement.UnhandledAttributes = copyElement.UnhandledAttributes;
			if (copyElement.MinOccurs != 1m && this.Occurrence == XmlSchemaInference.InferenceOption.Relaxed)
			{
				xmlSchemaElement.MinOccurs = copyElement.MinOccurs;
			}
			if (copyElement.MaxOccurs != 1m)
			{
				xmlSchemaElement.MaxOccurs = copyElement.MaxOccurs;
			}
			return xmlSchemaElement;
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000E6F98 File Offset: 0x000E5198
		private static int GetSchemaType(XmlQualifiedName qname)
		{
			if (qname == XmlSchemaInference.SimpleTypes[0])
			{
				return 262145;
			}
			if (qname == XmlSchemaInference.SimpleTypes[1])
			{
				return 269994;
			}
			if (qname == XmlSchemaInference.SimpleTypes[2])
			{
				return 270334;
			}
			if (qname == XmlSchemaInference.SimpleTypes[3])
			{
				return 269992;
			}
			if (qname == XmlSchemaInference.SimpleTypes[4])
			{
				return 270328;
			}
			if (qname == XmlSchemaInference.SimpleTypes[5])
			{
				return 269984;
			}
			if (qname == XmlSchemaInference.SimpleTypes[6])
			{
				return 270304;
			}
			if (qname == XmlSchemaInference.SimpleTypes[7])
			{
				return 269952;
			}
			if (qname == XmlSchemaInference.SimpleTypes[8])
			{
				return 270208;
			}
			if (qname == XmlSchemaInference.SimpleTypes[9])
			{
				return 269824;
			}
			if (qname == XmlSchemaInference.SimpleTypes[10])
			{
				return 269312;
			}
			if (qname == XmlSchemaInference.SimpleTypes[11])
			{
				return 268288;
			}
			if (qname == XmlSchemaInference.SimpleTypes[12])
			{
				return 266240;
			}
			if (qname == XmlSchemaInference.SimpleTypes[13])
			{
				return 270336;
			}
			if (qname == XmlSchemaInference.SimpleTypes[14])
			{
				return 278528;
			}
			if (qname == XmlSchemaInference.SimpleTypes[15])
			{
				return 294912;
			}
			if (qname == XmlSchemaInference.SimpleTypes[16])
			{
				return 65536;
			}
			if (qname == XmlSchemaInference.SimpleTypes[17])
			{
				return 131072;
			}
			if (qname == XmlSchemaInference.SimpleTypes[18])
			{
				return 262144;
			}
			if (qname == null || qname.IsEmpty)
			{
				return -1;
			}
			throw new XmlSchemaInferenceException("SchInf_schematype", 0, 0);
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000E7160 File Offset: 0x000E5360
		internal void SetMinMaxOccurs(XmlSchemaElement el, bool setMaxOccurs)
		{
			if (this.Occurrence == XmlSchemaInference.InferenceOption.Relaxed)
			{
				if (setMaxOccurs || el.MaxOccurs > 1m)
				{
					el.MaxOccurs = decimal.MaxValue;
				}
				el.MinOccurs = 0m;
				return;
			}
			if (el.MinOccurs > 1m)
			{
				el.MinOccurs = 1m;
			}
		}

		// Token: 0x040012DE RID: 4830
		internal static XmlQualifiedName ST_boolean = new XmlQualifiedName("boolean", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012DF RID: 4831
		internal static XmlQualifiedName ST_byte = new XmlQualifiedName("byte", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012E0 RID: 4832
		internal static XmlQualifiedName ST_unsignedByte = new XmlQualifiedName("unsignedByte", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012E1 RID: 4833
		internal static XmlQualifiedName ST_short = new XmlQualifiedName("short", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012E2 RID: 4834
		internal static XmlQualifiedName ST_unsignedShort = new XmlQualifiedName("unsignedShort", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012E3 RID: 4835
		internal static XmlQualifiedName ST_int = new XmlQualifiedName("int", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012E4 RID: 4836
		internal static XmlQualifiedName ST_unsignedInt = new XmlQualifiedName("unsignedInt", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012E5 RID: 4837
		internal static XmlQualifiedName ST_long = new XmlQualifiedName("long", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012E6 RID: 4838
		internal static XmlQualifiedName ST_unsignedLong = new XmlQualifiedName("unsignedLong", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012E7 RID: 4839
		internal static XmlQualifiedName ST_integer = new XmlQualifiedName("integer", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012E8 RID: 4840
		internal static XmlQualifiedName ST_decimal = new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012E9 RID: 4841
		internal static XmlQualifiedName ST_float = new XmlQualifiedName("float", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012EA RID: 4842
		internal static XmlQualifiedName ST_double = new XmlQualifiedName("double", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012EB RID: 4843
		internal static XmlQualifiedName ST_duration = new XmlQualifiedName("duration", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012EC RID: 4844
		internal static XmlQualifiedName ST_dateTime = new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012ED RID: 4845
		internal static XmlQualifiedName ST_time = new XmlQualifiedName("time", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012EE RID: 4846
		internal static XmlQualifiedName ST_date = new XmlQualifiedName("date", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012EF RID: 4847
		internal static XmlQualifiedName ST_gYearMonth = new XmlQualifiedName("gYearMonth", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012F0 RID: 4848
		internal static XmlQualifiedName ST_string = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012F1 RID: 4849
		internal static XmlQualifiedName ST_anySimpleType = new XmlQualifiedName("anySimpleType", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040012F2 RID: 4850
		internal static XmlQualifiedName[] SimpleTypes = new XmlQualifiedName[]
		{
			XmlSchemaInference.ST_boolean,
			XmlSchemaInference.ST_byte,
			XmlSchemaInference.ST_unsignedByte,
			XmlSchemaInference.ST_short,
			XmlSchemaInference.ST_unsignedShort,
			XmlSchemaInference.ST_int,
			XmlSchemaInference.ST_unsignedInt,
			XmlSchemaInference.ST_long,
			XmlSchemaInference.ST_unsignedLong,
			XmlSchemaInference.ST_integer,
			XmlSchemaInference.ST_decimal,
			XmlSchemaInference.ST_float,
			XmlSchemaInference.ST_double,
			XmlSchemaInference.ST_duration,
			XmlSchemaInference.ST_dateTime,
			XmlSchemaInference.ST_time,
			XmlSchemaInference.ST_date,
			XmlSchemaInference.ST_gYearMonth,
			XmlSchemaInference.ST_string
		};

		// Token: 0x040012F3 RID: 4851
		internal const short HC_ST_boolean = 0;

		// Token: 0x040012F4 RID: 4852
		internal const short HC_ST_byte = 1;

		// Token: 0x040012F5 RID: 4853
		internal const short HC_ST_unsignedByte = 2;

		// Token: 0x040012F6 RID: 4854
		internal const short HC_ST_short = 3;

		// Token: 0x040012F7 RID: 4855
		internal const short HC_ST_unsignedShort = 4;

		// Token: 0x040012F8 RID: 4856
		internal const short HC_ST_int = 5;

		// Token: 0x040012F9 RID: 4857
		internal const short HC_ST_unsignedInt = 6;

		// Token: 0x040012FA RID: 4858
		internal const short HC_ST_long = 7;

		// Token: 0x040012FB RID: 4859
		internal const short HC_ST_unsignedLong = 8;

		// Token: 0x040012FC RID: 4860
		internal const short HC_ST_integer = 9;

		// Token: 0x040012FD RID: 4861
		internal const short HC_ST_decimal = 10;

		// Token: 0x040012FE RID: 4862
		internal const short HC_ST_float = 11;

		// Token: 0x040012FF RID: 4863
		internal const short HC_ST_double = 12;

		// Token: 0x04001300 RID: 4864
		internal const short HC_ST_duration = 13;

		// Token: 0x04001301 RID: 4865
		internal const short HC_ST_dateTime = 14;

		// Token: 0x04001302 RID: 4866
		internal const short HC_ST_time = 15;

		// Token: 0x04001303 RID: 4867
		internal const short HC_ST_date = 16;

		// Token: 0x04001304 RID: 4868
		internal const short HC_ST_gYearMonth = 17;

		// Token: 0x04001305 RID: 4869
		internal const short HC_ST_string = 18;

		// Token: 0x04001306 RID: 4870
		internal const short HC_ST_Count = 19;

		// Token: 0x04001307 RID: 4871
		internal const int TF_boolean = 1;

		// Token: 0x04001308 RID: 4872
		internal const int TF_byte = 2;

		// Token: 0x04001309 RID: 4873
		internal const int TF_unsignedByte = 4;

		// Token: 0x0400130A RID: 4874
		internal const int TF_short = 8;

		// Token: 0x0400130B RID: 4875
		internal const int TF_unsignedShort = 16;

		// Token: 0x0400130C RID: 4876
		internal const int TF_int = 32;

		// Token: 0x0400130D RID: 4877
		internal const int TF_unsignedInt = 64;

		// Token: 0x0400130E RID: 4878
		internal const int TF_long = 128;

		// Token: 0x0400130F RID: 4879
		internal const int TF_unsignedLong = 256;

		// Token: 0x04001310 RID: 4880
		internal const int TF_integer = 512;

		// Token: 0x04001311 RID: 4881
		internal const int TF_decimal = 1024;

		// Token: 0x04001312 RID: 4882
		internal const int TF_float = 2048;

		// Token: 0x04001313 RID: 4883
		internal const int TF_double = 4096;

		// Token: 0x04001314 RID: 4884
		internal const int TF_duration = 8192;

		// Token: 0x04001315 RID: 4885
		internal const int TF_dateTime = 16384;

		// Token: 0x04001316 RID: 4886
		internal const int TF_time = 32768;

		// Token: 0x04001317 RID: 4887
		internal const int TF_date = 65536;

		// Token: 0x04001318 RID: 4888
		internal const int TF_gYearMonth = 131072;

		// Token: 0x04001319 RID: 4889
		internal const int TF_string = 262144;

		// Token: 0x0400131A RID: 4890
		private XmlSchema rootSchema;

		// Token: 0x0400131B RID: 4891
		private XmlSchemaSet schemaSet;

		// Token: 0x0400131C RID: 4892
		private XmlReader xtr;

		// Token: 0x0400131D RID: 4893
		private NameTable nametable;

		// Token: 0x0400131E RID: 4894
		private string TargetNamespace;

		// Token: 0x0400131F RID: 4895
		private XmlNamespaceManager NamespaceManager;

		// Token: 0x04001320 RID: 4896
		private ArrayList schemaList;

		// Token: 0x04001321 RID: 4897
		private XmlSchemaInference.InferenceOption occurrence;

		// Token: 0x04001322 RID: 4898
		private XmlSchemaInference.InferenceOption typeInference;

		// Token: 0x020004B9 RID: 1209
		public enum InferenceOption
		{
			// Token: 0x04001F81 RID: 8065
			Restricted,
			// Token: 0x04001F82 RID: 8066
			Relaxed
		}
	}
}
