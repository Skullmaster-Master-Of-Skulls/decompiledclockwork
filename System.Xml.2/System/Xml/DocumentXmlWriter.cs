using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020000EF RID: 239
	internal sealed class DocumentXmlWriter : XmlRawWriter, IXmlNamespaceResolver
	{
		// Token: 0x0600107A RID: 4218 RVA: 0x00045B48 File Offset: 0x00043D48
		public DocumentXmlWriter(DocumentXmlWriterType type, XmlNode start, XmlDocument document)
		{
			this.type = type;
			this.start = start;
			this.document = document;
			this.state = this.StartState();
			this.fragment = new List<XmlNode>();
			this.settings = new XmlWriterSettings();
			this.settings.ReadOnly = false;
			this.settings.CheckCharacters = false;
			this.settings.CloseOutput = false;
			this.settings.ConformanceLevel = ((this.state == DocumentXmlWriter.State.Prolog) ? ConformanceLevel.Document : ConformanceLevel.Fragment);
			this.settings.ReadOnly = true;
		}

		// Token: 0x17000321 RID: 801
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x00045BDA File Offset: 0x00043DDA
		public XmlNamespaceManager NamespaceManager
		{
			set
			{
				this.namespaceManager = value;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x00045BE3 File Offset: 0x00043DE3
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00045BEB File Offset: 0x00043DEB
		internal void SetSettings(XmlWriterSettings value)
		{
			this.settings = value;
		}

		// Token: 0x17000323 RID: 803
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x00045BF4 File Offset: 0x00043DF4
		public DocumentXPathNavigator Navigator
		{
			set
			{
				this.navigator = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x00045BFD File Offset: 0x00043DFD
		public XmlNode EndNode
		{
			set
			{
				this.end = value;
			}
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00045C08 File Offset: 0x00043E08
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteXmlDeclaration);
			if (standalone != XmlStandalone.Omit)
			{
				XmlNode node = this.document.CreateXmlDeclaration("1.0", string.Empty, (standalone == XmlStandalone.Yes) ? "yes" : "no");
				this.AddChild(node, this.write);
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00045C54 File Offset: 0x00043E54
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteXmlDeclaration);
			string version;
			string encoding;
			string standalone;
			XmlLoader.ParseXmlDeclarationValue(xmldecl, out version, out encoding, out standalone);
			XmlNode node = this.document.CreateXmlDeclaration(version, encoding, standalone);
			this.AddChild(node, this.write);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00045C90 File Offset: 0x00043E90
		public override void WriteStartDocument()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartDocument);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00045C99 File Offset: 0x00043E99
		public override void WriteStartDocument(bool standalone)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartDocument);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00045CA2 File Offset: 0x00043EA2
		public override void WriteEndDocument()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEndDocument);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00045CAC File Offset: 0x00043EAC
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteDocType);
			XmlNode node = this.document.CreateDocumentType(name, pubid, sysid, subset);
			this.AddChild(node, this.write);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00045CE0 File Offset: 0x00043EE0
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartElement);
			XmlNode node = this.document.CreateElement(prefix, localName, ns);
			this.AddChild(node, this.write);
			this.write = node;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00045D17 File Offset: 0x00043F17
		public override void WriteEndElement()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEndElement);
			if (this.write == null)
			{
				throw new InvalidOperationException();
			}
			this.write = this.write.ParentNode;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00045D3F File Offset: 0x00043F3F
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.WriteEndElement();
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00045D48 File Offset: 0x00043F48
		public override void WriteFullEndElement()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteFullEndElement);
			XmlElement xmlElement = this.write as XmlElement;
			if (xmlElement == null)
			{
				throw new InvalidOperationException();
			}
			xmlElement.IsEmpty = false;
			this.write = xmlElement.ParentNode;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00045D84 File Offset: 0x00043F84
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.WriteFullEndElement();
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00045D8C File Offset: 0x00043F8C
		internal override void StartElementContent()
		{
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00045D90 File Offset: 0x00043F90
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartAttribute);
			XmlAttribute attr = this.document.CreateAttribute(prefix, localName, ns);
			this.AddAttribute(attr, this.write);
			this.write = attr;
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00045DC8 File Offset: 0x00043FC8
		public override void WriteEndAttribute()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEndAttribute);
			XmlAttribute xmlAttribute = this.write as XmlAttribute;
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException();
			}
			if (!xmlAttribute.HasChildNodes)
			{
				XmlNode node = this.document.CreateTextNode(string.Empty);
				this.AddChild(node, xmlAttribute);
			}
			this.write = xmlAttribute.OwnerElement;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00045E1E File Offset: 0x0004401E
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.WriteStartNamespaceDeclaration(prefix);
			this.WriteString(ns);
			this.WriteEndNamespaceDeclaration();
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00045E34 File Offset: 0x00044034
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00045E38 File Offset: 0x00044038
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartNamespaceDeclaration);
			XmlAttribute attr;
			if (prefix.Length == 0)
			{
				attr = this.document.CreateAttribute(prefix, this.document.strXmlns, this.document.strReservedXmlns);
			}
			else
			{
				attr = this.document.CreateAttribute(this.document.strXmlns, prefix, this.document.strReservedXmlns);
			}
			this.AddAttribute(attr, this.write);
			this.write = attr;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00045EB4 File Offset: 0x000440B4
		internal override void WriteEndNamespaceDeclaration()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEndNamespaceDeclaration);
			XmlAttribute xmlAttribute = this.write as XmlAttribute;
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException();
			}
			if (!xmlAttribute.HasChildNodes)
			{
				XmlNode node = this.document.CreateTextNode(string.Empty);
				this.AddChild(node, xmlAttribute);
			}
			this.write = xmlAttribute.OwnerElement;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00045F0C File Offset: 0x0004410C
		public override void WriteCData(string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteCData);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			XmlNode node = this.document.CreateCDataSection(text);
			this.AddChild(node, this.write);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00045F44 File Offset: 0x00044144
		public override void WriteComment(string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteComment);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			XmlNode node = this.document.CreateComment(text);
			this.AddChild(node, this.write);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00045F7C File Offset: 0x0004417C
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteProcessingInstruction);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			XmlNode node = this.document.CreateProcessingInstruction(name, text);
			this.AddChild(node, this.write);
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00045FB4 File Offset: 0x000441B4
		public override void WriteEntityRef(string name)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEntityRef);
			XmlNode node = this.document.CreateEntityReference(name);
			this.AddChild(node, this.write);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00045FE3 File Offset: 0x000441E3
		public override void WriteCharEntity(char ch)
		{
			this.WriteString(new string(ch, 1));
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00045FF4 File Offset: 0x000441F4
		public override void WriteWhitespace(string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteWhitespace);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			if (this.document.PreserveWhitespace)
			{
				XmlNode node = this.document.CreateWhitespace(text);
				this.AddChild(node, this.write);
			}
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00046038 File Offset: 0x00044238
		public override void WriteString(string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteString);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			XmlNode node = this.document.CreateTextNode(text);
			this.AddChild(node, this.write);
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0004606E File Offset: 0x0004426E
		public override void WriteSurrogateCharEntity(char lowCh, char highCh)
		{
			this.WriteString(new string(new char[]
			{
				highCh,
				lowCh
			}));
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00046089 File Offset: 0x00044289
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00046099 File Offset: 0x00044299
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000460A9 File Offset: 0x000442A9
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x000460B2 File Offset: 0x000442B2
		public override void Close()
		{
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000460B4 File Offset: 0x000442B4
		internal override void Close(WriteState currentState)
		{
			if (currentState == WriteState.Error)
			{
				return;
			}
			try
			{
				switch (this.type)
				{
				case DocumentXmlWriterType.InsertSiblingAfter:
				{
					XmlNode parentNode = this.start.ParentNode;
					if (parentNode == null)
					{
						throw new InvalidOperationException(Res.GetString("Xpn_MissingParent"));
					}
					for (int i = this.fragment.Count - 1; i >= 0; i--)
					{
						parentNode.InsertAfter(this.fragment[i], this.start);
					}
					break;
				}
				case DocumentXmlWriterType.InsertSiblingBefore:
				{
					XmlNode parentNode = this.start.ParentNode;
					if (parentNode == null)
					{
						throw new InvalidOperationException(Res.GetString("Xpn_MissingParent"));
					}
					for (int j = 0; j < this.fragment.Count; j++)
					{
						parentNode.InsertBefore(this.fragment[j], this.start);
					}
					break;
				}
				case DocumentXmlWriterType.PrependChild:
					for (int k = this.fragment.Count - 1; k >= 0; k--)
					{
						this.start.PrependChild(this.fragment[k]);
					}
					break;
				case DocumentXmlWriterType.AppendChild:
					for (int l = 0; l < this.fragment.Count; l++)
					{
						this.start.AppendChild(this.fragment[l]);
					}
					break;
				case DocumentXmlWriterType.AppendAttribute:
					this.CloseWithAppendAttribute();
					break;
				case DocumentXmlWriterType.ReplaceToFollowingSibling:
					if (this.fragment.Count == 0)
					{
						throw new InvalidOperationException(Res.GetString("Xpn_NoContent"));
					}
					this.CloseWithReplaceToFollowingSibling();
					break;
				}
			}
			finally
			{
				this.fragment.Clear();
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0004625C File Offset: 0x0004445C
		private void CloseWithAppendAttribute()
		{
			XmlElement xmlElement = this.start as XmlElement;
			XmlAttributeCollection attributes = xmlElement.Attributes;
			for (int i = 0; i < this.fragment.Count; i++)
			{
				XmlAttribute xmlAttribute = this.fragment[i] as XmlAttribute;
				int num = attributes.FindNodeOffsetNS(xmlAttribute);
				if (num != -1 && ((XmlAttribute)attributes.nodes[num]).Specified)
				{
					throw new XmlException("Xml_DupAttributeName", (xmlAttribute.Prefix.Length == 0) ? xmlAttribute.LocalName : (xmlAttribute.Prefix + ":" + xmlAttribute.LocalName));
				}
			}
			for (int j = 0; j < this.fragment.Count; j++)
			{
				XmlAttribute node = this.fragment[j] as XmlAttribute;
				attributes.Append(node);
			}
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0004633C File Offset: 0x0004453C
		private void CloseWithReplaceToFollowingSibling()
		{
			XmlNode parentNode = this.start.ParentNode;
			if (parentNode == null)
			{
				throw new InvalidOperationException(Res.GetString("Xpn_MissingParent"));
			}
			if (this.start != this.end)
			{
				if (!DocumentXPathNavigator.IsFollowingSibling(this.start, this.end))
				{
					throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
				}
				if (this.start.IsReadOnly)
				{
					throw new InvalidOperationException(Res.GetString("Xdom_Node_Modify_ReadOnly"));
				}
				DocumentXPathNavigator.DeleteToFollowingSibling(this.start.NextSibling, this.end);
			}
			XmlNode xmlNode = this.fragment[0];
			parentNode.ReplaceChild(xmlNode, this.start);
			for (int i = this.fragment.Count - 1; i >= 1; i--)
			{
				parentNode.InsertAfter(this.fragment[i], xmlNode);
			}
			this.navigator.ResetPosition(xmlNode);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0004641F File Offset: 0x0004461F
		public override void Flush()
		{
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00046421 File Offset: 0x00044621
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.namespaceManager.GetNamespacesInScope(scope);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0004642F File Offset: 0x0004462F
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.namespaceManager.LookupNamespace(prefix);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0004643D File Offset: 0x0004463D
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.namespaceManager.LookupPrefix(namespaceName);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0004644C File Offset: 0x0004464C
		private void AddAttribute(XmlAttribute attr, XmlNode parent)
		{
			if (parent == null)
			{
				this.fragment.Add(attr);
				return;
			}
			XmlElement xmlElement = parent as XmlElement;
			if (xmlElement == null)
			{
				throw new InvalidOperationException();
			}
			xmlElement.Attributes.Append(attr);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00046486 File Offset: 0x00044686
		private void AddChild(XmlNode node, XmlNode parent)
		{
			if (parent == null)
			{
				this.fragment.Add(node);
				return;
			}
			parent.AppendChild(node);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x000464A0 File Offset: 0x000446A0
		private DocumentXmlWriter.State StartState()
		{
			XmlNodeType xmlNodeType = XmlNodeType.None;
			switch (this.type)
			{
			case DocumentXmlWriterType.InsertSiblingAfter:
			case DocumentXmlWriterType.InsertSiblingBefore:
			{
				XmlNode parentNode = this.start.ParentNode;
				if (parentNode != null)
				{
					xmlNodeType = parentNode.NodeType;
				}
				if (xmlNodeType == XmlNodeType.Document)
				{
					return DocumentXmlWriter.State.Prolog;
				}
				if (xmlNodeType == XmlNodeType.DocumentFragment)
				{
					return DocumentXmlWriter.State.Fragment;
				}
				break;
			}
			case DocumentXmlWriterType.PrependChild:
			case DocumentXmlWriterType.AppendChild:
				xmlNodeType = this.start.NodeType;
				if (xmlNodeType == XmlNodeType.Document)
				{
					return DocumentXmlWriter.State.Prolog;
				}
				if (xmlNodeType == XmlNodeType.DocumentFragment)
				{
					return DocumentXmlWriter.State.Fragment;
				}
				break;
			case DocumentXmlWriterType.AppendAttribute:
				return DocumentXmlWriter.State.Attribute;
			}
			return DocumentXmlWriter.State.Content;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00046517 File Offset: 0x00044717
		private void VerifyState(DocumentXmlWriter.Method method)
		{
			this.state = DocumentXmlWriter.changeState[(int)(method * DocumentXmlWriter.Method.WriteEndElement + (int)this.state)];
			if (this.state == DocumentXmlWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("Xml_ClosedOrError"));
			}
		}

		// Token: 0x040004B3 RID: 1203
		private DocumentXmlWriterType type;

		// Token: 0x040004B4 RID: 1204
		private XmlNode start;

		// Token: 0x040004B5 RID: 1205
		private XmlDocument document;

		// Token: 0x040004B6 RID: 1206
		private XmlNamespaceManager namespaceManager;

		// Token: 0x040004B7 RID: 1207
		private DocumentXmlWriter.State state;

		// Token: 0x040004B8 RID: 1208
		private XmlNode write;

		// Token: 0x040004B9 RID: 1209
		private List<XmlNode> fragment;

		// Token: 0x040004BA RID: 1210
		private XmlWriterSettings settings;

		// Token: 0x040004BB RID: 1211
		private DocumentXPathNavigator navigator;

		// Token: 0x040004BC RID: 1212
		private XmlNode end;

		// Token: 0x040004BD RID: 1213
		private static DocumentXmlWriter.State[] changeState = new DocumentXmlWriter.State[]
		{
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content
		};

		// Token: 0x02000437 RID: 1079
		private enum State
		{
			// Token: 0x04001C20 RID: 7200
			Error,
			// Token: 0x04001C21 RID: 7201
			Attribute,
			// Token: 0x04001C22 RID: 7202
			Prolog,
			// Token: 0x04001C23 RID: 7203
			Fragment,
			// Token: 0x04001C24 RID: 7204
			Content,
			// Token: 0x04001C25 RID: 7205
			Last
		}

		// Token: 0x02000438 RID: 1080
		private enum Method
		{
			// Token: 0x04001C27 RID: 7207
			WriteXmlDeclaration,
			// Token: 0x04001C28 RID: 7208
			WriteStartDocument,
			// Token: 0x04001C29 RID: 7209
			WriteEndDocument,
			// Token: 0x04001C2A RID: 7210
			WriteDocType,
			// Token: 0x04001C2B RID: 7211
			WriteStartElement,
			// Token: 0x04001C2C RID: 7212
			WriteEndElement,
			// Token: 0x04001C2D RID: 7213
			WriteFullEndElement,
			// Token: 0x04001C2E RID: 7214
			WriteStartAttribute,
			// Token: 0x04001C2F RID: 7215
			WriteEndAttribute,
			// Token: 0x04001C30 RID: 7216
			WriteStartNamespaceDeclaration,
			// Token: 0x04001C31 RID: 7217
			WriteEndNamespaceDeclaration,
			// Token: 0x04001C32 RID: 7218
			WriteCData,
			// Token: 0x04001C33 RID: 7219
			WriteComment,
			// Token: 0x04001C34 RID: 7220
			WriteProcessingInstruction,
			// Token: 0x04001C35 RID: 7221
			WriteEntityRef,
			// Token: 0x04001C36 RID: 7222
			WriteWhitespace,
			// Token: 0x04001C37 RID: 7223
			WriteString
		}
	}
}
