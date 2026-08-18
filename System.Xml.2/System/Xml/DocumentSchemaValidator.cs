using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000105 RID: 261
	internal sealed class DocumentSchemaValidator : IXmlNamespaceResolver
	{
		// Token: 0x0600125B RID: 4699 RVA: 0x0004C1B8 File Offset: 0x0004A3B8
		public DocumentSchemaValidator(XmlDocument ownerDocument, XmlSchemaSet schemas, ValidationEventHandler eventHandler)
		{
			this.schemas = schemas;
			this.eventHandler = eventHandler;
			this.document = ownerDocument;
			this.internalEventHandler = new ValidationEventHandler(this.InternalValidationCallBack);
			this.nameTable = this.document.NameTable;
			this.nsManager = new XmlNamespaceManager(this.nameTable);
			this.nodeValueGetter = new XmlValueGetter(this.GetNodeValue);
			this.psviAugmentation = true;
			this.NsXmlNs = this.nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXsi = this.nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.XsiType = this.nameTable.Add("type");
			this.XsiNil = this.nameTable.Add("nil");
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0004C285 File Offset: 0x0004A485
		// (set) Token: 0x0600125D RID: 4701 RVA: 0x0004C28D File Offset: 0x0004A48D
		public bool PsviAugmentation
		{
			get
			{
				return this.psviAugmentation;
			}
			set
			{
				this.psviAugmentation = value;
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0004C298 File Offset: 0x0004A498
		public bool Validate(XmlNode nodeToValidate)
		{
			XmlSchemaObject xmlSchemaObject = null;
			XmlSchemaValidationFlags xmlSchemaValidationFlags = XmlSchemaValidationFlags.AllowXmlAttributes;
			this.startNode = nodeToValidate;
			XmlNodeType nodeType = nodeToValidate.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.Attribute)
					{
						if (nodeToValidate.XPNodeType != XPathNodeType.Namespace)
						{
							xmlSchemaObject = nodeToValidate.SchemaInfo.SchemaAttribute;
							if (xmlSchemaObject != null)
							{
								goto IL_10F;
							}
							xmlSchemaObject = this.FindSchemaInfo(nodeToValidate as XmlAttribute);
							if (xmlSchemaObject == null)
							{
								throw new XmlSchemaValidationException("XmlDocument_NoNodeSchemaInfo", null, nodeToValidate);
							}
							goto IL_10F;
						}
					}
				}
				else
				{
					IXmlSchemaInfo xmlSchemaInfo = nodeToValidate.SchemaInfo;
					XmlSchemaElement schemaElement = xmlSchemaInfo.SchemaElement;
					if (schemaElement != null)
					{
						if (!schemaElement.RefName.IsEmpty)
						{
							xmlSchemaObject = this.schemas.GlobalElements[schemaElement.QualifiedName];
							goto IL_10F;
						}
						xmlSchemaObject = schemaElement;
						goto IL_10F;
					}
					else
					{
						xmlSchemaObject = xmlSchemaInfo.SchemaType;
						if (xmlSchemaObject != null)
						{
							goto IL_10F;
						}
						if (nodeToValidate.ParentNode.NodeType == XmlNodeType.Document)
						{
							nodeToValidate = nodeToValidate.ParentNode;
							goto IL_10F;
						}
						xmlSchemaObject = this.FindSchemaInfo(nodeToValidate as XmlElement);
						if (xmlSchemaObject == null)
						{
							throw new XmlSchemaValidationException("XmlDocument_NoNodeSchemaInfo", null, nodeToValidate);
						}
						goto IL_10F;
					}
				}
			}
			else
			{
				if (nodeType == XmlNodeType.Document)
				{
					xmlSchemaValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;
					goto IL_10F;
				}
				if (nodeType == XmlNodeType.DocumentFragment)
				{
					goto IL_10F;
				}
			}
			throw new InvalidOperationException(Res.GetString("XmlDocument_ValidateInvalidNodeType", null));
			IL_10F:
			this.isValid = true;
			this.CreateValidator(xmlSchemaObject, xmlSchemaValidationFlags);
			if (this.psviAugmentation)
			{
				if (this.schemaInfo == null)
				{
					this.schemaInfo = new XmlSchemaInfo();
				}
				this.attributeSchemaInfo = new XmlSchemaInfo();
			}
			this.ValidateNode(nodeToValidate);
			this.validator.EndValidation();
			return this.isValid;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0004C404 File Offset: 0x0004A604
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			IDictionary<string, string> namespacesInScope = this.nsManager.GetNamespacesInScope(scope);
			if (scope != XmlNamespaceScope.Local)
			{
				XmlNode xmlNode = this.startNode;
				while (xmlNode != null)
				{
					XmlNodeType nodeType = xmlNode.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.Attribute)
						{
							xmlNode = xmlNode.ParentNode;
						}
						else
						{
							xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
						}
					}
					else
					{
						XmlElement xmlElement = (XmlElement)xmlNode;
						if (xmlElement.HasAttributes)
						{
							XmlAttributeCollection attributes = xmlElement.Attributes;
							for (int i = 0; i < attributes.Count; i++)
							{
								XmlAttribute xmlAttribute = attributes[i];
								if (Ref.Equal(xmlAttribute.NamespaceURI, this.document.strReservedXmlns))
								{
									if (xmlAttribute.Prefix.Length == 0)
									{
										if (!namespacesInScope.ContainsKey(string.Empty))
										{
											namespacesInScope.Add(string.Empty, xmlAttribute.Value);
										}
									}
									else if (!namespacesInScope.ContainsKey(xmlAttribute.LocalName))
									{
										namespacesInScope.Add(xmlAttribute.LocalName, xmlAttribute.Value);
									}
								}
							}
						}
						xmlNode = xmlNode.ParentNode;
					}
				}
			}
			return namespacesInScope;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0004C518 File Offset: 0x0004A718
		public string LookupNamespace(string prefix)
		{
			string text = this.nsManager.LookupNamespace(prefix);
			if (text == null)
			{
				text = this.startNode.GetNamespaceOfPrefixStrict(prefix);
			}
			return text;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0004C544 File Offset: 0x0004A744
		public string LookupPrefix(string namespaceName)
		{
			string text = this.nsManager.LookupPrefix(namespaceName);
			if (text == null)
			{
				text = this.startNode.GetPrefixOfNamespaceStrict(namespaceName);
			}
			return text;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0004C56F File Offset: 0x0004A76F
		private IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				if (this.startNode == this.document)
				{
					return this.nsManager;
				}
				return this;
			}
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0004C588 File Offset: 0x0004A788
		private void CreateValidator(XmlSchemaObject partialValidationType, XmlSchemaValidationFlags validationFlags)
		{
			this.validator = new XmlSchemaValidator(this.nameTable, this.schemas, this.NamespaceResolver, validationFlags);
			this.validator.SourceUri = XmlConvert.ToUri(this.document.BaseURI);
			this.validator.XmlResolver = null;
			this.validator.ValidationEventHandler += this.internalEventHandler;
			this.validator.ValidationEventSender = this;
			if (partialValidationType != null)
			{
				this.validator.Initialize(partialValidationType);
				return;
			}
			this.validator.Initialize();
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0004C614 File Offset: 0x0004A814
		private void ValidateNode(XmlNode node)
		{
			this.currentNode = node;
			switch (this.currentNode.NodeType)
			{
			case XmlNodeType.Element:
				this.ValidateElement();
				return;
			case XmlNodeType.Attribute:
			{
				XmlAttribute xmlAttribute = this.currentNode as XmlAttribute;
				this.validator.ValidateAttribute(xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.nodeValueGetter, this.attributeSchemaInfo);
				if (this.psviAugmentation)
				{
					xmlAttribute.XmlName = this.document.AddAttrXmlName(xmlAttribute.Prefix, xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.attributeSchemaInfo);
					return;
				}
				return;
			}
			case XmlNodeType.Text:
				this.validator.ValidateText(this.nodeValueGetter);
				return;
			case XmlNodeType.CDATA:
				this.validator.ValidateText(this.nodeValueGetter);
				return;
			case XmlNodeType.EntityReference:
			case XmlNodeType.DocumentFragment:
				for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					this.ValidateNode(xmlNode);
				}
				return;
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
				return;
			case XmlNodeType.Document:
			{
				XmlElement documentElement = ((XmlDocument)node).DocumentElement;
				if (documentElement == null)
				{
					throw new InvalidOperationException(Res.GetString("Xml_InvalidXmlDocument", new object[]
					{
						Res.GetString("Xdom_NoRootEle")
					}));
				}
				this.ValidateNode(documentElement);
				return;
			}
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.validator.ValidateWhitespace(this.nodeValueGetter);
				return;
			}
			string name = "Xml_UnexpectedNodeType";
			object[] args = new string[]
			{
				this.currentNode.NodeType.ToString()
			};
			throw new InvalidOperationException(Res.GetString(name, args));
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004C7A8 File Offset: 0x0004A9A8
		private void ValidateElement()
		{
			this.nsManager.PushScope();
			XmlElement xmlElement = this.currentNode as XmlElement;
			XmlAttributeCollection attributes = xmlElement.Attributes;
			string xsiNil = null;
			string xsiType = null;
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlAttribute xmlAttribute = attributes[i];
				string namespaceURI = xmlAttribute.NamespaceURI;
				string localName = xmlAttribute.LocalName;
				if (Ref.Equal(namespaceURI, this.NsXsi))
				{
					if (Ref.Equal(localName, this.XsiType))
					{
						xsiType = xmlAttribute.Value;
					}
					else if (Ref.Equal(localName, this.XsiNil))
					{
						xsiNil = xmlAttribute.Value;
					}
				}
				else if (Ref.Equal(namespaceURI, this.NsXmlNs))
				{
					this.nsManager.AddNamespace((xmlAttribute.Prefix.Length == 0) ? string.Empty : xmlAttribute.LocalName, xmlAttribute.Value);
				}
			}
			this.validator.ValidateElement(xmlElement.LocalName, xmlElement.NamespaceURI, this.schemaInfo, xsiType, xsiNil, null, null);
			this.ValidateAttributes(xmlElement);
			this.validator.ValidateEndOfAttributes(this.schemaInfo);
			for (XmlNode xmlNode = xmlElement.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.ValidateNode(xmlNode);
			}
			this.currentNode = xmlElement;
			this.validator.ValidateEndElement(this.schemaInfo);
			if (this.psviAugmentation)
			{
				xmlElement.XmlName = this.document.AddXmlName(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI, this.schemaInfo);
				if (this.schemaInfo.IsDefault)
				{
					XmlText newChild = this.document.CreateTextNode(this.schemaInfo.SchemaElement.ElementDecl.DefaultValueRaw);
					xmlElement.AppendChild(newChild);
				}
			}
			this.nsManager.PopScope();
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0004C970 File Offset: 0x0004AB70
		private void ValidateAttributes(XmlElement elementNode)
		{
			XmlAttributeCollection attributes = elementNode.Attributes;
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlAttribute xmlAttribute = attributes[i];
				this.currentNode = xmlAttribute;
				if (!Ref.Equal(xmlAttribute.NamespaceURI, this.NsXmlNs))
				{
					this.validator.ValidateAttribute(xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.nodeValueGetter, this.attributeSchemaInfo);
					if (this.psviAugmentation)
					{
						xmlAttribute.XmlName = this.document.AddAttrXmlName(xmlAttribute.Prefix, xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.attributeSchemaInfo);
					}
				}
			}
			if (this.psviAugmentation)
			{
				if (this.defaultAttributes == null)
				{
					this.defaultAttributes = new ArrayList();
				}
				else
				{
					this.defaultAttributes.Clear();
				}
				this.validator.GetUnspecifiedDefaultAttributes(this.defaultAttributes);
				for (int j = 0; j < this.defaultAttributes.Count; j++)
				{
					XmlSchemaAttribute xmlSchemaAttribute = this.defaultAttributes[j] as XmlSchemaAttribute;
					XmlQualifiedName qualifiedName = xmlSchemaAttribute.QualifiedName;
					XmlAttribute xmlAttribute = this.document.CreateDefaultAttribute(this.GetDefaultPrefix(qualifiedName.Namespace), qualifiedName.Name, qualifiedName.Namespace);
					this.SetDefaultAttributeSchemaInfo(xmlSchemaAttribute);
					xmlAttribute.XmlName = this.document.AddAttrXmlName(xmlAttribute.Prefix, xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.attributeSchemaInfo);
					xmlAttribute.AppendChild(this.document.CreateTextNode(xmlSchemaAttribute.AttDef.DefaultValueRaw));
					attributes.Append(xmlAttribute);
					XmlUnspecifiedAttribute xmlUnspecifiedAttribute = xmlAttribute as XmlUnspecifiedAttribute;
					if (xmlUnspecifiedAttribute != null)
					{
						xmlUnspecifiedAttribute.SetSpecified(false);
					}
				}
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0004CB1C File Offset: 0x0004AD1C
		private void SetDefaultAttributeSchemaInfo(XmlSchemaAttribute schemaAttribute)
		{
			this.attributeSchemaInfo.Clear();
			this.attributeSchemaInfo.IsDefault = true;
			this.attributeSchemaInfo.IsNil = false;
			this.attributeSchemaInfo.SchemaType = schemaAttribute.AttributeSchemaType;
			this.attributeSchemaInfo.SchemaAttribute = schemaAttribute;
			SchemaAttDef attDef = schemaAttribute.AttDef;
			if (attDef.Datatype.Variety == XmlSchemaDatatypeVariety.Union)
			{
				XsdSimpleValue xsdSimpleValue = attDef.DefaultValueTyped as XsdSimpleValue;
				this.attributeSchemaInfo.MemberType = xsdSimpleValue.XmlType;
			}
			this.attributeSchemaInfo.Validity = XmlSchemaValidity.Valid;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0004CBA8 File Offset: 0x0004ADA8
		private string GetDefaultPrefix(string attributeNS)
		{
			IDictionary<string, string> namespacesInScope = this.NamespaceResolver.GetNamespacesInScope(XmlNamespaceScope.All);
			string text = null;
			attributeNS = this.nameTable.Add(attributeNS);
			foreach (KeyValuePair<string, string> keyValuePair in namespacesInScope)
			{
				string text2 = this.nameTable.Add(keyValuePair.Value);
				if (text2 == attributeNS)
				{
					text = keyValuePair.Key;
					if (text.Length != 0)
					{
						return text;
					}
				}
			}
			return text;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0004CC38 File Offset: 0x0004AE38
		private object GetNodeValue()
		{
			return this.currentNode.Value;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0004CC48 File Offset: 0x0004AE48
		private XmlSchemaObject FindSchemaInfo(XmlElement elementToValidate)
		{
			this.isPartialTreeValid = true;
			int num = 0;
			XmlNode parentNode = elementToValidate.ParentNode;
			IXmlSchemaInfo xmlSchemaInfo;
			do
			{
				xmlSchemaInfo = parentNode.SchemaInfo;
				if (xmlSchemaInfo.SchemaElement != null || xmlSchemaInfo.SchemaType != null)
				{
					break;
				}
				this.CheckNodeSequenceCapacity(num);
				this.nodeSequenceToValidate[num++] = parentNode;
				parentNode = parentNode.ParentNode;
			}
			while (parentNode != null);
			if (parentNode == null)
			{
				num--;
				this.nodeSequenceToValidate[num] = null;
				return this.GetTypeFromAncestors(elementToValidate, null, num);
			}
			this.CheckNodeSequenceCapacity(num);
			this.nodeSequenceToValidate[num++] = parentNode;
			XmlSchemaObject xmlSchemaObject = xmlSchemaInfo.SchemaElement;
			if (xmlSchemaObject == null)
			{
				xmlSchemaObject = xmlSchemaInfo.SchemaType;
			}
			return this.GetTypeFromAncestors(elementToValidate, xmlSchemaObject, num);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0004CCEC File Offset: 0x0004AEEC
		private void CheckNodeSequenceCapacity(int currentIndex)
		{
			if (this.nodeSequenceToValidate == null)
			{
				this.nodeSequenceToValidate = new XmlNode[4];
				return;
			}
			if (currentIndex >= this.nodeSequenceToValidate.Length - 1)
			{
				XmlNode[] destinationArray = new XmlNode[this.nodeSequenceToValidate.Length * 2];
				Array.Copy(this.nodeSequenceToValidate, 0, destinationArray, 0, this.nodeSequenceToValidate.Length);
				this.nodeSequenceToValidate = destinationArray;
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0004CD48 File Offset: 0x0004AF48
		private XmlSchemaAttribute FindSchemaInfo(XmlAttribute attributeToValidate)
		{
			XmlElement ownerElement = attributeToValidate.OwnerElement;
			XmlSchemaObject schemaObject = this.FindSchemaInfo(ownerElement);
			XmlSchemaComplexType complexType = this.GetComplexType(schemaObject);
			if (complexType == null)
			{
				return null;
			}
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(attributeToValidate.LocalName, attributeToValidate.NamespaceURI);
			XmlSchemaAttribute xmlSchemaAttribute = complexType.AttributeUses[xmlQualifiedName] as XmlSchemaAttribute;
			if (xmlSchemaAttribute == null)
			{
				XmlSchemaAnyAttribute attributeWildcard = complexType.AttributeWildcard;
				if (attributeWildcard != null && attributeWildcard.NamespaceList.Allows(xmlQualifiedName))
				{
					xmlSchemaAttribute = (this.schemas.GlobalAttributes[xmlQualifiedName] as XmlSchemaAttribute);
				}
			}
			return xmlSchemaAttribute;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0004CDD0 File Offset: 0x0004AFD0
		private XmlSchemaObject GetTypeFromAncestors(XmlElement elementToValidate, XmlSchemaObject ancestorType, int ancestorsCount)
		{
			this.validator = this.CreateTypeFinderValidator(ancestorType);
			this.schemaInfo = new XmlSchemaInfo();
			int num = ancestorsCount - 1;
			bool flag = this.AncestorTypeHasWildcard(ancestorType);
			for (int i = num; i >= 0; i--)
			{
				XmlNode xmlNode = this.nodeSequenceToValidate[i];
				XmlElement xmlElement = xmlNode as XmlElement;
				this.ValidateSingleElement(xmlElement, false, this.schemaInfo);
				if (!flag)
				{
					xmlElement.XmlName = this.document.AddXmlName(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI, this.schemaInfo);
					flag = this.AncestorTypeHasWildcard(this.schemaInfo.SchemaElement);
				}
				this.validator.ValidateEndOfAttributes(null);
				if (i > 0)
				{
					this.ValidateChildrenTillNextAncestor(xmlNode, this.nodeSequenceToValidate[i - 1]);
				}
				else
				{
					this.ValidateChildrenTillNextAncestor(xmlNode, elementToValidate);
				}
			}
			this.ValidateSingleElement(elementToValidate, false, this.schemaInfo);
			XmlSchemaObject xmlSchemaObject;
			if (this.schemaInfo.SchemaElement != null)
			{
				xmlSchemaObject = this.schemaInfo.SchemaElement;
			}
			else
			{
				xmlSchemaObject = this.schemaInfo.SchemaType;
			}
			if (xmlSchemaObject == null)
			{
				if (this.validator.CurrentProcessContents == XmlSchemaContentProcessing.Skip)
				{
					if (this.isPartialTreeValid)
					{
						return XmlSchemaComplexType.AnyTypeSkip;
					}
				}
				else if (this.validator.CurrentProcessContents == XmlSchemaContentProcessing.Lax)
				{
					return XmlSchemaComplexType.AnyType;
				}
			}
			return xmlSchemaObject;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0004CF10 File Offset: 0x0004B110
		private bool AncestorTypeHasWildcard(XmlSchemaObject ancestorType)
		{
			XmlSchemaComplexType complexType = this.GetComplexType(ancestorType);
			return ancestorType != null && complexType.HasWildCard;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0004CF30 File Offset: 0x0004B130
		private XmlSchemaComplexType GetComplexType(XmlSchemaObject schemaObject)
		{
			if (schemaObject == null)
			{
				return null;
			}
			XmlSchemaElement xmlSchemaElement = schemaObject as XmlSchemaElement;
			XmlSchemaComplexType result;
			if (xmlSchemaElement != null)
			{
				result = (xmlSchemaElement.ElementSchemaType as XmlSchemaComplexType);
			}
			else
			{
				result = (schemaObject as XmlSchemaComplexType);
			}
			return result;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0004CF64 File Offset: 0x0004B164
		private void ValidateSingleElement(XmlElement elementNode, bool skipToEnd, XmlSchemaInfo newSchemaInfo)
		{
			this.nsManager.PushScope();
			XmlAttributeCollection attributes = elementNode.Attributes;
			string xsiNil = null;
			string xsiType = null;
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlAttribute xmlAttribute = attributes[i];
				string namespaceURI = xmlAttribute.NamespaceURI;
				string localName = xmlAttribute.LocalName;
				if (Ref.Equal(namespaceURI, this.NsXsi))
				{
					if (Ref.Equal(localName, this.XsiType))
					{
						xsiType = xmlAttribute.Value;
					}
					else if (Ref.Equal(localName, this.XsiNil))
					{
						xsiNil = xmlAttribute.Value;
					}
				}
				else if (Ref.Equal(namespaceURI, this.NsXmlNs))
				{
					this.nsManager.AddNamespace((xmlAttribute.Prefix.Length == 0) ? string.Empty : xmlAttribute.LocalName, xmlAttribute.Value);
				}
			}
			this.validator.ValidateElement(elementNode.LocalName, elementNode.NamespaceURI, newSchemaInfo, xsiType, xsiNil, null, null);
			if (skipToEnd)
			{
				this.validator.ValidateEndOfAttributes(newSchemaInfo);
				this.validator.SkipToEndElement(newSchemaInfo);
				this.nsManager.PopScope();
			}
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0004D07C File Offset: 0x0004B27C
		private void ValidateChildrenTillNextAncestor(XmlNode parentNode, XmlNode childToStopAt)
		{
			XmlNode xmlNode = parentNode.FirstChild;
			while (xmlNode != null && xmlNode != childToStopAt)
			{
				switch (xmlNode.NodeType)
				{
				case XmlNodeType.Element:
					this.ValidateSingleElement(xmlNode as XmlElement, true, null);
					break;
				case XmlNodeType.Attribute:
				case XmlNodeType.Entity:
				case XmlNodeType.Document:
				case XmlNodeType.DocumentType:
				case XmlNodeType.DocumentFragment:
				case XmlNodeType.Notation:
					goto IL_9C;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					this.validator.ValidateText(xmlNode.Value);
					break;
				case XmlNodeType.EntityReference:
					this.ValidateChildrenTillNextAncestor(xmlNode, childToStopAt);
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this.validator.ValidateWhitespace(xmlNode.Value);
					break;
				default:
					goto IL_9C;
				}
				xmlNode = xmlNode.NextSibling;
				continue;
				IL_9C:
				string name = "Xml_UnexpectedNodeType";
				object[] args = new string[]
				{
					this.currentNode.NodeType.ToString()
				};
				throw new InvalidOperationException(Res.GetString(name, args));
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0004D168 File Offset: 0x0004B368
		private XmlSchemaValidator CreateTypeFinderValidator(XmlSchemaObject partialValidationType)
		{
			XmlSchemaValidator xmlSchemaValidator = new XmlSchemaValidator(this.document.NameTable, this.document.Schemas, this.nsManager, XmlSchemaValidationFlags.None);
			xmlSchemaValidator.ValidationEventHandler += this.TypeFinderCallBack;
			if (partialValidationType != null)
			{
				xmlSchemaValidator.Initialize(partialValidationType);
			}
			else
			{
				xmlSchemaValidator.Initialize();
			}
			return xmlSchemaValidator;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0004D1BD File Offset: 0x0004B3BD
		private void TypeFinderCallBack(object sender, ValidationEventArgs arg)
		{
			if (arg.Severity == XmlSeverityType.Error)
			{
				this.isPartialTreeValid = false;
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0004D1D0 File Offset: 0x0004B3D0
		private void InternalValidationCallBack(object sender, ValidationEventArgs arg)
		{
			if (arg.Severity == XmlSeverityType.Error)
			{
				this.isValid = false;
			}
			XmlSchemaValidationException ex = arg.Exception as XmlSchemaValidationException;
			ex.SetSourceObject(this.currentNode);
			if (this.eventHandler != null)
			{
				this.eventHandler(sender, arg);
				return;
			}
			if (arg.Severity == XmlSeverityType.Error)
			{
				throw ex;
			}
		}

		// Token: 0x04000511 RID: 1297
		private XmlSchemaValidator validator;

		// Token: 0x04000512 RID: 1298
		private XmlSchemaSet schemas;

		// Token: 0x04000513 RID: 1299
		private XmlNamespaceManager nsManager;

		// Token: 0x04000514 RID: 1300
		private XmlNameTable nameTable;

		// Token: 0x04000515 RID: 1301
		private ArrayList defaultAttributes;

		// Token: 0x04000516 RID: 1302
		private XmlValueGetter nodeValueGetter;

		// Token: 0x04000517 RID: 1303
		private XmlSchemaInfo attributeSchemaInfo;

		// Token: 0x04000518 RID: 1304
		private XmlSchemaInfo schemaInfo;

		// Token: 0x04000519 RID: 1305
		private ValidationEventHandler eventHandler;

		// Token: 0x0400051A RID: 1306
		private ValidationEventHandler internalEventHandler;

		// Token: 0x0400051B RID: 1307
		private XmlNode startNode;

		// Token: 0x0400051C RID: 1308
		private XmlNode currentNode;

		// Token: 0x0400051D RID: 1309
		private XmlDocument document;

		// Token: 0x0400051E RID: 1310
		private XmlNode[] nodeSequenceToValidate;

		// Token: 0x0400051F RID: 1311
		private bool isPartialTreeValid;

		// Token: 0x04000520 RID: 1312
		private bool psviAugmentation;

		// Token: 0x04000521 RID: 1313
		private bool isValid;

		// Token: 0x04000522 RID: 1314
		private string NsXmlNs;

		// Token: 0x04000523 RID: 1315
		private string NsXsi;

		// Token: 0x04000524 RID: 1316
		private string XsiType;

		// Token: 0x04000525 RID: 1317
		private string XsiNil;
	}
}
