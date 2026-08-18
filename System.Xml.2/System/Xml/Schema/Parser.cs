using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	// Token: 0x02000252 RID: 594
	internal sealed class Parser
	{
		// Token: 0x06002320 RID: 8992 RVA: 0x000BA6F4 File Offset: 0x000B88F4
		public Parser(SchemaType schemaType, XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler)
		{
			this.schemaType = schemaType;
			this.nameTable = nameTable;
			this.schemaNames = schemaNames;
			this.eventHandler = eventHandler;
			this.xmlResolver = XmlReaderSection.CreateDefaultResolver();
			this.processMarkup = true;
			this.dummyDocument = new XmlDocument();
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x000BA74C File Offset: 0x000B894C
		public SchemaType Parse(XmlReader reader, string targetNamespace)
		{
			this.StartParsing(reader, targetNamespace);
			while (this.ParseReaderNode() && reader.Read())
			{
			}
			return this.FinishParsing();
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000BA76C File Offset: 0x000B896C
		public void StartParsing(XmlReader reader, string targetNamespace)
		{
			this.reader = reader;
			this.positionInfo = PositionInfo.GetPositionInfo(reader);
			this.namespaceManager = reader.NamespaceManager;
			if (this.namespaceManager == null)
			{
				this.namespaceManager = new XmlNamespaceManager(this.nameTable);
				this.isProcessNamespaces = true;
			}
			else
			{
				this.isProcessNamespaces = false;
			}
			while (reader.NodeType != XmlNodeType.Element && reader.Read())
			{
			}
			this.markupDepth = int.MaxValue;
			this.schemaXmlDepth = reader.Depth;
			SchemaType rootType = this.schemaNames.SchemaTypeFromRoot(reader.LocalName, reader.NamespaceURI);
			string res;
			if (!this.CheckSchemaRoot(rootType, out res))
			{
				throw new XmlSchemaException(res, reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition);
			}
			if (this.schemaType == SchemaType.XSD)
			{
				this.schema = new XmlSchema();
				this.schema.BaseUri = new Uri(reader.BaseURI, UriKind.RelativeOrAbsolute);
				this.builder = new XsdBuilder(reader, this.namespaceManager, this.schema, this.nameTable, this.schemaNames, this.eventHandler);
				return;
			}
			this.xdrSchema = new SchemaInfo();
			this.xdrSchema.SchemaType = SchemaType.XDR;
			this.builder = new XdrBuilder(reader, this.namespaceManager, this.xdrSchema, targetNamespace, this.nameTable, this.schemaNames, this.eventHandler);
			((XdrBuilder)this.builder).XmlResolver = this.xmlResolver;
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000BA8E0 File Offset: 0x000B8AE0
		private bool CheckSchemaRoot(SchemaType rootType, out string code)
		{
			code = null;
			if (this.schemaType == SchemaType.None)
			{
				this.schemaType = rootType;
			}
			switch (rootType)
			{
			case SchemaType.None:
			case SchemaType.DTD:
				code = "Sch_SchemaRootExpected";
				if (this.schemaType == SchemaType.XSD)
				{
					code = "Sch_XSDSchemaRootExpected";
				}
				return false;
			case SchemaType.XDR:
				if (this.schemaType == SchemaType.XSD)
				{
					code = "Sch_XSDSchemaOnly";
					return false;
				}
				if (this.schemaType != SchemaType.XDR)
				{
					code = "Sch_MixSchemaTypes";
					return false;
				}
				break;
			case SchemaType.XSD:
				if (this.schemaType != SchemaType.XSD)
				{
					code = "Sch_MixSchemaTypes";
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x000BA967 File Offset: 0x000B8B67
		public SchemaType FinishParsing()
		{
			return this.schemaType;
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x000BA96F File Offset: 0x000B8B6F
		public XmlSchema XmlSchema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (set) Token: 0x06002326 RID: 8998 RVA: 0x000BA977 File Offset: 0x000B8B77
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x000BA980 File Offset: 0x000B8B80
		public SchemaInfo XdrSchema
		{
			get
			{
				return this.xdrSchema;
			}
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000BA988 File Offset: 0x000B8B88
		public bool ParseReaderNode()
		{
			if (this.reader.Depth > this.markupDepth)
			{
				if (this.processMarkup)
				{
					this.ProcessAppInfoDocMarkup(false);
				}
				return true;
			}
			if (this.reader.NodeType == XmlNodeType.Element)
			{
				if (this.builder.ProcessElement(this.reader.Prefix, this.reader.LocalName, this.reader.NamespaceURI))
				{
					this.namespaceManager.PushScope();
					if (this.reader.MoveToFirstAttribute())
					{
						do
						{
							this.builder.ProcessAttribute(this.reader.Prefix, this.reader.LocalName, this.reader.NamespaceURI, this.reader.Value);
							if (Ref.Equal(this.reader.NamespaceURI, this.schemaNames.NsXmlNs) && this.isProcessNamespaces)
							{
								this.namespaceManager.AddNamespace((this.reader.Prefix.Length == 0) ? string.Empty : this.reader.LocalName, this.reader.Value);
							}
						}
						while (this.reader.MoveToNextAttribute());
						this.reader.MoveToElement();
					}
					this.builder.StartChildren();
					if (this.reader.IsEmptyElement)
					{
						this.namespaceManager.PopScope();
						this.builder.EndChildren();
						if (this.reader.Depth == this.schemaXmlDepth)
						{
							return false;
						}
					}
					else if (!this.builder.IsContentParsed())
					{
						this.markupDepth = this.reader.Depth;
						this.processMarkup = true;
						if (this.annotationNSManager == null)
						{
							this.annotationNSManager = new XmlNamespaceManager(this.nameTable);
							this.xmlns = this.nameTable.Add("xmlns");
						}
						this.ProcessAppInfoDocMarkup(true);
					}
				}
				else if (!this.reader.IsEmptyElement)
				{
					this.markupDepth = this.reader.Depth;
					this.processMarkup = false;
				}
			}
			else if (this.reader.NodeType == XmlNodeType.Text)
			{
				if (!this.xmlCharType.IsOnlyWhitespace(this.reader.Value))
				{
					this.builder.ProcessCData(this.reader.Value);
				}
			}
			else if (this.reader.NodeType == XmlNodeType.EntityReference || this.reader.NodeType == XmlNodeType.SignificantWhitespace || this.reader.NodeType == XmlNodeType.CDATA)
			{
				this.builder.ProcessCData(this.reader.Value);
			}
			else if (this.reader.NodeType == XmlNodeType.EndElement)
			{
				if (this.reader.Depth == this.markupDepth)
				{
					if (this.processMarkup)
					{
						XmlNodeList childNodes = this.parentNode.ChildNodes;
						XmlNode[] array = new XmlNode[childNodes.Count];
						for (int i = 0; i < childNodes.Count; i++)
						{
							array[i] = childNodes[i];
						}
						this.builder.ProcessMarkup(array);
						this.namespaceManager.PopScope();
						this.builder.EndChildren();
					}
					this.markupDepth = int.MaxValue;
				}
				else
				{
					this.namespaceManager.PopScope();
					this.builder.EndChildren();
				}
				if (this.reader.Depth == this.schemaXmlDepth)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x000BACE8 File Offset: 0x000B8EE8
		private void ProcessAppInfoDocMarkup(bool root)
		{
			XmlNode newChild = null;
			switch (this.reader.NodeType)
			{
			case XmlNodeType.Element:
				this.annotationNSManager.PushScope();
				newChild = this.LoadElementNode(root);
				return;
			case XmlNodeType.Text:
				newChild = this.dummyDocument.CreateTextNode(this.reader.Value);
				break;
			case XmlNodeType.CDATA:
				newChild = this.dummyDocument.CreateCDataSection(this.reader.Value);
				break;
			case XmlNodeType.EntityReference:
				newChild = this.dummyDocument.CreateEntityReference(this.reader.Name);
				break;
			case XmlNodeType.ProcessingInstruction:
				newChild = this.dummyDocument.CreateProcessingInstruction(this.reader.Name, this.reader.Value);
				break;
			case XmlNodeType.Comment:
				newChild = this.dummyDocument.CreateComment(this.reader.Value);
				break;
			case XmlNodeType.Whitespace:
			case XmlNodeType.EndEntity:
				return;
			case XmlNodeType.SignificantWhitespace:
				newChild = this.dummyDocument.CreateSignificantWhitespace(this.reader.Value);
				break;
			case XmlNodeType.EndElement:
				this.annotationNSManager.PopScope();
				this.parentNode = this.parentNode.ParentNode;
				return;
			}
			this.parentNode.AppendChild(newChild);
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000BAE38 File Offset: 0x000B9038
		private XmlElement LoadElementNode(bool root)
		{
			XmlReader xmlReader = this.reader;
			bool isEmptyElement = xmlReader.IsEmptyElement;
			XmlElement xmlElement = this.dummyDocument.CreateElement(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI);
			xmlElement.IsEmpty = isEmptyElement;
			if (root)
			{
				this.parentNode = xmlElement;
			}
			else
			{
				XmlAttributeCollection attributes = xmlElement.Attributes;
				if (xmlReader.MoveToFirstAttribute())
				{
					do
					{
						if (Ref.Equal(xmlReader.NamespaceURI, this.schemaNames.NsXmlNs))
						{
							this.annotationNSManager.AddNamespace((xmlReader.Prefix.Length == 0) ? string.Empty : this.reader.LocalName, this.reader.Value);
						}
						XmlAttribute node = this.LoadAttributeNode();
						attributes.Append(node);
					}
					while (xmlReader.MoveToNextAttribute());
				}
				xmlReader.MoveToElement();
				string text = this.annotationNSManager.LookupNamespace(xmlReader.Prefix);
				if (text == null)
				{
					XmlAttribute node2 = this.CreateXmlNsAttribute(xmlReader.Prefix, this.namespaceManager.LookupNamespace(xmlReader.Prefix));
					attributes.Append(node2);
				}
				else if (text.Length == 0)
				{
					string text2 = this.namespaceManager.LookupNamespace(xmlReader.Prefix);
					if (text2 != string.Empty)
					{
						XmlAttribute node3 = this.CreateXmlNsAttribute(xmlReader.Prefix, text2);
						attributes.Append(node3);
					}
				}
				while (xmlReader.MoveToNextAttribute())
				{
					if (xmlReader.Prefix.Length != 0 && this.annotationNSManager.LookupNamespace(xmlReader.Prefix) == null)
					{
						XmlAttribute node4 = this.CreateXmlNsAttribute(xmlReader.Prefix, this.namespaceManager.LookupNamespace(xmlReader.Prefix));
						attributes.Append(node4);
					}
				}
				xmlReader.MoveToElement();
				this.parentNode.AppendChild(xmlElement);
				if (!xmlReader.IsEmptyElement)
				{
					this.parentNode = xmlElement;
				}
			}
			return xmlElement;
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000BB008 File Offset: 0x000B9208
		private XmlAttribute CreateXmlNsAttribute(string prefix, string value)
		{
			XmlAttribute xmlAttribute;
			if (prefix.Length == 0)
			{
				xmlAttribute = this.dummyDocument.CreateAttribute(string.Empty, this.xmlns, "http://www.w3.org/2000/xmlns/");
			}
			else
			{
				xmlAttribute = this.dummyDocument.CreateAttribute(this.xmlns, prefix, "http://www.w3.org/2000/xmlns/");
			}
			xmlAttribute.AppendChild(this.dummyDocument.CreateTextNode(value));
			this.annotationNSManager.AddNamespace(prefix, value);
			return xmlAttribute;
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000BB074 File Offset: 0x000B9274
		private XmlAttribute LoadAttributeNode()
		{
			XmlReader xmlReader = this.reader;
			XmlAttribute xmlAttribute = this.dummyDocument.CreateAttribute(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI);
			while (xmlReader.ReadAttributeValue())
			{
				XmlNodeType nodeType = xmlReader.NodeType;
				if (nodeType != XmlNodeType.Text)
				{
					if (nodeType != XmlNodeType.EntityReference)
					{
						throw XmlLoader.UnexpectedNodeType(xmlReader.NodeType);
					}
					xmlAttribute.AppendChild(this.LoadEntityReferenceInAttribute());
				}
				else
				{
					xmlAttribute.AppendChild(this.dummyDocument.CreateTextNode(xmlReader.Value));
				}
			}
			return xmlAttribute;
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000BB0F8 File Offset: 0x000B92F8
		private XmlEntityReference LoadEntityReferenceInAttribute()
		{
			XmlEntityReference xmlEntityReference = this.dummyDocument.CreateEntityReference(this.reader.LocalName);
			if (!this.reader.CanResolveEntity)
			{
				return xmlEntityReference;
			}
			this.reader.ResolveEntity();
			while (this.reader.ReadAttributeValue())
			{
				XmlNodeType nodeType = this.reader.NodeType;
				if (nodeType != XmlNodeType.Text)
				{
					if (nodeType != XmlNodeType.EntityReference)
					{
						if (nodeType != XmlNodeType.EndEntity)
						{
							throw XmlLoader.UnexpectedNodeType(this.reader.NodeType);
						}
						if (xmlEntityReference.ChildNodes.Count == 0)
						{
							xmlEntityReference.AppendChild(this.dummyDocument.CreateTextNode(string.Empty));
						}
						return xmlEntityReference;
					}
					else
					{
						xmlEntityReference.AppendChild(this.LoadEntityReferenceInAttribute());
					}
				}
				else
				{
					xmlEntityReference.AppendChild(this.dummyDocument.CreateTextNode(this.reader.Value));
				}
			}
			return xmlEntityReference;
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x000BB1CC File Offset: 0x000B93CC
		public Task<SchemaType> ParseAsync(XmlReader reader, string targetNamespace)
		{
			Parser.<ParseAsync>d__37 <ParseAsync>d__;
			<ParseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SchemaType>.Create();
			<ParseAsync>d__.<>4__this = this;
			<ParseAsync>d__.reader = reader;
			<ParseAsync>d__.targetNamespace = targetNamespace;
			<ParseAsync>d__.<>1__state = -1;
			<ParseAsync>d__.<>t__builder.Start<Parser.<ParseAsync>d__37>(ref <ParseAsync>d__);
			return <ParseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000BB220 File Offset: 0x000B9420
		public Task StartParsingAsync(XmlReader reader, string targetNamespace)
		{
			Parser.<StartParsingAsync>d__38 <StartParsingAsync>d__;
			<StartParsingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<StartParsingAsync>d__.<>4__this = this;
			<StartParsingAsync>d__.reader = reader;
			<StartParsingAsync>d__.targetNamespace = targetNamespace;
			<StartParsingAsync>d__.<>1__state = -1;
			<StartParsingAsync>d__.<>t__builder.Start<Parser.<StartParsingAsync>d__38>(ref <StartParsingAsync>d__);
			return <StartParsingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000EB5 RID: 3765
		private SchemaType schemaType;

		// Token: 0x04000EB6 RID: 3766
		private XmlNameTable nameTable;

		// Token: 0x04000EB7 RID: 3767
		private SchemaNames schemaNames;

		// Token: 0x04000EB8 RID: 3768
		private ValidationEventHandler eventHandler;

		// Token: 0x04000EB9 RID: 3769
		private XmlNamespaceManager namespaceManager;

		// Token: 0x04000EBA RID: 3770
		private XmlReader reader;

		// Token: 0x04000EBB RID: 3771
		private PositionInfo positionInfo;

		// Token: 0x04000EBC RID: 3772
		private bool isProcessNamespaces;

		// Token: 0x04000EBD RID: 3773
		private int schemaXmlDepth;

		// Token: 0x04000EBE RID: 3774
		private int markupDepth;

		// Token: 0x04000EBF RID: 3775
		private SchemaBuilder builder;

		// Token: 0x04000EC0 RID: 3776
		private XmlSchema schema;

		// Token: 0x04000EC1 RID: 3777
		private SchemaInfo xdrSchema;

		// Token: 0x04000EC2 RID: 3778
		private XmlResolver xmlResolver;

		// Token: 0x04000EC3 RID: 3779
		private XmlDocument dummyDocument;

		// Token: 0x04000EC4 RID: 3780
		private bool processMarkup;

		// Token: 0x04000EC5 RID: 3781
		private XmlNode parentNode;

		// Token: 0x04000EC6 RID: 3782
		private XmlNamespaceManager annotationNSManager;

		// Token: 0x04000EC7 RID: 3783
		private string xmlns;

		// Token: 0x04000EC8 RID: 3784
		private XmlCharType xmlCharType = XmlCharType.Instance;
	}
}
