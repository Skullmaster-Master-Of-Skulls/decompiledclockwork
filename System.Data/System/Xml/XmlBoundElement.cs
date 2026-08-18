using System;
using System.Data;
using System.Threading;

namespace System.Xml
{
	// Token: 0x0200038B RID: 907
	internal sealed class XmlBoundElement : XmlElement
	{
		// Token: 0x06002FE4 RID: 12260 RVA: 0x002D6808 File Offset: 0x002D5C08
		internal XmlBoundElement(string prefix, string localName, string namespaceURI, XmlDocument doc) : base(prefix, localName, namespaceURI, doc)
		{
			this.state = ElementState.None;
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x002D6828 File Offset: 0x002D5C28
		public override XmlAttributeCollection Attributes
		{
			get
			{
				this.AutoFoliate();
				return base.Attributes;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x002D6848 File Offset: 0x002D5C48
		public override bool HasAttributes
		{
			get
			{
				return this.Attributes.Count > 0;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x002D6868 File Offset: 0x002D5C68
		public override XmlNode FirstChild
		{
			get
			{
				this.AutoFoliate();
				return base.FirstChild;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x002D6888 File Offset: 0x002D5C88
		internal XmlNode SafeFirstChild
		{
			get
			{
				return base.FirstChild;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x002D68A8 File Offset: 0x002D5CA8
		public override XmlNode LastChild
		{
			get
			{
				this.AutoFoliate();
				return base.LastChild;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x002D68C8 File Offset: 0x002D5CC8
		public override XmlNode PreviousSibling
		{
			get
			{
				XmlNode previousSibling = base.PreviousSibling;
				if (previousSibling == null)
				{
					XmlBoundElement xmlBoundElement = this.ParentNode as XmlBoundElement;
					if (xmlBoundElement != null)
					{
						xmlBoundElement.AutoFoliate();
						return base.PreviousSibling;
					}
				}
				return previousSibling;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002FEB RID: 12267 RVA: 0x002D6908 File Offset: 0x002D5D08
		internal XmlNode SafePreviousSibling
		{
			get
			{
				return base.PreviousSibling;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x002D6928 File Offset: 0x002D5D28
		public override XmlNode NextSibling
		{
			get
			{
				XmlNode nextSibling = base.NextSibling;
				if (nextSibling == null)
				{
					XmlBoundElement xmlBoundElement = this.ParentNode as XmlBoundElement;
					if (xmlBoundElement != null)
					{
						xmlBoundElement.AutoFoliate();
						return base.NextSibling;
					}
				}
				return nextSibling;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x002D6968 File Offset: 0x002D5D68
		internal XmlNode SafeNextSibling
		{
			get
			{
				return base.NextSibling;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x002D6988 File Offset: 0x002D5D88
		public override bool HasChildNodes
		{
			get
			{
				this.AutoFoliate();
				return base.HasChildNodes;
			}
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x002D69A8 File Offset: 0x002D5DA8
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			this.AutoFoliate();
			return base.InsertBefore(newChild, refChild);
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x002D69C8 File Offset: 0x002D5DC8
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			this.AutoFoliate();
			return base.InsertAfter(newChild, refChild);
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x002D69E8 File Offset: 0x002D5DE8
		public override XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			this.AutoFoliate();
			return base.ReplaceChild(newChild, oldChild);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x002D6A08 File Offset: 0x002D5E08
		public override XmlNode AppendChild(XmlNode newChild)
		{
			this.AutoFoliate();
			return base.AppendChild(newChild);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x002D6A28 File Offset: 0x002D5E28
		internal void RemoveAllChildren()
		{
			XmlNode nextSibling;
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = nextSibling)
			{
				nextSibling = xmlNode.NextSibling;
				this.RemoveChild(xmlNode);
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002FF4 RID: 12276 RVA: 0x002D6A58 File Offset: 0x002D5E58
		// (set) Token: 0x06002FF5 RID: 12277 RVA: 0x002D6A78 File Offset: 0x002D5E78
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				this.RemoveAllChildren();
				XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
				bool ignoreXmlEvents = xmlDataDocument.IgnoreXmlEvents;
				bool ignoreDataSetEvents = xmlDataDocument.IgnoreDataSetEvents;
				xmlDataDocument.IgnoreXmlEvents = true;
				xmlDataDocument.IgnoreDataSetEvents = true;
				base.InnerXml = value;
				xmlDataDocument.SyncTree(this);
				xmlDataDocument.IgnoreDataSetEvents = ignoreDataSetEvents;
				xmlDataDocument.IgnoreXmlEvents = ignoreXmlEvents;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002FF6 RID: 12278 RVA: 0x002D6AD8 File Offset: 0x002D5ED8
		// (set) Token: 0x06002FF7 RID: 12279 RVA: 0x002D6AF8 File Offset: 0x002D5EF8
		internal DataRow Row
		{
			get
			{
				return this.row;
			}
			set
			{
				this.row = value;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002FF8 RID: 12280 RVA: 0x002D6B18 File Offset: 0x002D5F18
		internal bool IsFoliated
		{
			get
			{
				while (this.state == ElementState.Foliating || this.state == ElementState.Defoliating)
				{
					Thread.Sleep(0);
				}
				return this.state != ElementState.Defoliated;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002FF9 RID: 12281 RVA: 0x002D6B58 File Offset: 0x002D5F58
		// (set) Token: 0x06002FFA RID: 12282 RVA: 0x002D6B78 File Offset: 0x002D5F78
		internal ElementState ElementState
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x002D6B98 File Offset: 0x002D5F98
		internal void Foliate(ElementState newState)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			if (xmlDataDocument != null)
			{
				xmlDataDocument.Foliate(this, newState);
			}
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x002D6BC8 File Offset: 0x002D5FC8
		private void AutoFoliate()
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			if (xmlDataDocument != null)
			{
				xmlDataDocument.Foliate(this, xmlDataDocument.AutoFoliationState);
			}
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x002D6BF8 File Offset: 0x002D5FF8
		public override XmlNode CloneNode(bool deep)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			ElementState autoFoliationState = xmlDataDocument.AutoFoliationState;
			xmlDataDocument.AutoFoliationState = ElementState.WeakFoliation;
			XmlElement result;
			try
			{
				this.Foliate(ElementState.WeakFoliation);
				result = (XmlElement)base.CloneNode(deep);
			}
			finally
			{
				xmlDataDocument.AutoFoliationState = autoFoliationState;
			}
			return result;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x002D6C68 File Offset: 0x002D6068
		public override void WriteContentTo(XmlWriter w)
		{
			DataPointer dataPointer = new DataPointer((XmlDataDocument)this.OwnerDocument, this);
			try
			{
				dataPointer.AddPointer();
				XmlBoundElement.WriteBoundElementContentTo(dataPointer, w);
			}
			finally
			{
				dataPointer.SetNoLongerUse();
			}
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x002D6CC8 File Offset: 0x002D60C8
		public override void WriteTo(XmlWriter w)
		{
			DataPointer dataPointer = new DataPointer((XmlDataDocument)this.OwnerDocument, this);
			try
			{
				dataPointer.AddPointer();
				this.WriteRootBoundElementTo(dataPointer, w);
			}
			finally
			{
				dataPointer.SetNoLongerUse();
			}
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x002D6D28 File Offset: 0x002D6128
		private void WriteRootBoundElementTo(DataPointer dp, XmlWriter w)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			w.WriteStartElement(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			int attributeCount = dp.AttributeCount;
			bool flag = false;
			if (attributeCount > 0)
			{
				for (int i = 0; i < attributeCount; i++)
				{
					dp.MoveToAttribute(i);
					if (dp.Prefix == "xmlns" && dp.LocalName == "xsi")
					{
						flag = true;
					}
					XmlBoundElement.WriteTo(dp, w);
					dp.MoveToOwnerElement();
				}
			}
			if (!flag && xmlDataDocument.bLoadFromDataSet && xmlDataDocument.bHasXSINIL)
			{
				w.WriteAttributeString("xmlns", "xsi", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2001/XMLSchema-instance");
			}
			XmlBoundElement.WriteBoundElementContentTo(dp, w);
			if (dp.IsEmptyElement)
			{
				w.WriteEndElement();
				return;
			}
			w.WriteFullEndElement();
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x002D6DF8 File Offset: 0x002D61F8
		private static void WriteBoundElementTo(DataPointer dp, XmlWriter w)
		{
			w.WriteStartElement(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			int attributeCount = dp.AttributeCount;
			if (attributeCount > 0)
			{
				for (int i = 0; i < attributeCount; i++)
				{
					dp.MoveToAttribute(i);
					XmlBoundElement.WriteTo(dp, w);
					dp.MoveToOwnerElement();
				}
			}
			XmlBoundElement.WriteBoundElementContentTo(dp, w);
			if (dp.IsEmptyElement)
			{
				w.WriteEndElement();
				return;
			}
			w.WriteFullEndElement();
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x002D6E68 File Offset: 0x002D6268
		private static void WriteBoundElementContentTo(DataPointer dp, XmlWriter w)
		{
			if (!dp.IsEmptyElement && dp.MoveToFirstChild())
			{
				do
				{
					XmlBoundElement.WriteTo(dp, w);
				}
				while (dp.MoveToNextSibling());
				dp.MoveToParent();
			}
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x002D6EA8 File Offset: 0x002D62A8
		private static void WriteTo(DataPointer dp, XmlWriter w)
		{
			switch (dp.NodeType)
			{
			case XmlNodeType.Element:
				XmlBoundElement.WriteBoundElementTo(dp, w);
				return;
			case XmlNodeType.Attribute:
				if (!dp.IsDefault)
				{
					w.WriteStartAttribute(dp.Prefix, dp.LocalName, dp.NamespaceURI);
					if (dp.MoveToFirstChild())
					{
						do
						{
							XmlBoundElement.WriteTo(dp, w);
						}
						while (dp.MoveToNextSibling());
						dp.MoveToParent();
					}
					w.WriteEndAttribute();
					return;
				}
				break;
			case XmlNodeType.Text:
				w.WriteString(dp.Value);
				return;
			default:
				if (dp.GetNode() != null)
				{
					dp.GetNode().WriteTo(w);
				}
				break;
			}
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x002D6F48 File Offset: 0x002D6348
		public override XmlNodeList GetElementsByTagName(string name)
		{
			XmlNodeList elementsByTagName = base.GetElementsByTagName(name);
			int count = elementsByTagName.Count;
			return elementsByTagName;
		}

		// Token: 0x04001DB2 RID: 7602
		private DataRow row;

		// Token: 0x04001DB3 RID: 7603
		private ElementState state;
	}
}
