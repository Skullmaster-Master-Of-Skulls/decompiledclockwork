using System;
using System.Data;
using System.Threading;

namespace System.Xml
{
	// Token: 0x0200008A RID: 138
	internal sealed class XmlBoundElement : XmlElement
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x0004B290 File Offset: 0x0004A690
		internal XmlBoundElement(string prefix, string localName, string namespaceURI, XmlDocument doc) : base(prefix, localName, namespaceURI, doc)
		{
			this.state = ElementState.None;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0004B2B0 File Offset: 0x0004A6B0
		public override XmlAttributeCollection Attributes
		{
			get
			{
				this.AutoFoliate();
				return base.Attributes;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0004B2CC File Offset: 0x0004A6CC
		public override bool HasAttributes
		{
			get
			{
				return this.Attributes.Count > 0;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0004B2E8 File Offset: 0x0004A6E8
		public override XmlNode FirstChild
		{
			get
			{
				this.AutoFoliate();
				return base.FirstChild;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0004B304 File Offset: 0x0004A704
		internal XmlNode SafeFirstChild
		{
			get
			{
				return base.FirstChild;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0004B318 File Offset: 0x0004A718
		public override XmlNode LastChild
		{
			get
			{
				this.AutoFoliate();
				return base.LastChild;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0004B334 File Offset: 0x0004A734
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

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0004B368 File Offset: 0x0004A768
		internal XmlNode SafePreviousSibling
		{
			get
			{
				return base.PreviousSibling;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0004B37C File Offset: 0x0004A77C
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

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0004B3B0 File Offset: 0x0004A7B0
		internal XmlNode SafeNextSibling
		{
			get
			{
				return base.NextSibling;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0004B3C4 File Offset: 0x0004A7C4
		public override bool HasChildNodes
		{
			get
			{
				this.AutoFoliate();
				return base.HasChildNodes;
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0004B3E0 File Offset: 0x0004A7E0
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			this.AutoFoliate();
			return base.InsertBefore(newChild, refChild);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0004B3FC File Offset: 0x0004A7FC
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			this.AutoFoliate();
			return base.InsertAfter(newChild, refChild);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0004B418 File Offset: 0x0004A818
		public override XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			this.AutoFoliate();
			return base.ReplaceChild(newChild, oldChild);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0004B434 File Offset: 0x0004A834
		public override XmlNode AppendChild(XmlNode newChild)
		{
			this.AutoFoliate();
			return base.AppendChild(newChild);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0004B450 File Offset: 0x0004A850
		internal void RemoveAllChildren()
		{
			XmlNode nextSibling;
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = nextSibling)
			{
				nextSibling = xmlNode.NextSibling;
				this.RemoveChild(xmlNode);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0004B47C File Offset: 0x0004A87C
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x0004B490 File Offset: 0x0004A890
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

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0004B4E8 File Offset: 0x0004A8E8
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0004B4FC File Offset: 0x0004A8FC
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

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0004B510 File Offset: 0x0004A910
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0004B544 File Offset: 0x0004A944
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x0004B558 File Offset: 0x0004A958
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

		// Token: 0x0600069B RID: 1691 RVA: 0x0004B56C File Offset: 0x0004A96C
		internal void Foliate(ElementState newState)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			if (xmlDataDocument != null)
			{
				xmlDataDocument.Foliate(this, newState);
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0004B590 File Offset: 0x0004A990
		private void AutoFoliate()
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			if (xmlDataDocument != null)
			{
				xmlDataDocument.Foliate(this, xmlDataDocument.AutoFoliationState);
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0004B5BC File Offset: 0x0004A9BC
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

		// Token: 0x0600069E RID: 1694 RVA: 0x0004B620 File Offset: 0x0004AA20
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

		// Token: 0x0600069F RID: 1695 RVA: 0x0004B674 File Offset: 0x0004AA74
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

		// Token: 0x060006A0 RID: 1696 RVA: 0x0004B6C8 File Offset: 0x0004AAC8
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

		// Token: 0x060006A1 RID: 1697 RVA: 0x0004B798 File Offset: 0x0004AB98
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

		// Token: 0x060006A2 RID: 1698 RVA: 0x0004B808 File Offset: 0x0004AC08
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

		// Token: 0x060006A3 RID: 1699 RVA: 0x0004B83C File Offset: 0x0004AC3C
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

		// Token: 0x060006A4 RID: 1700 RVA: 0x0004B8D4 File Offset: 0x0004ACD4
		public override XmlNodeList GetElementsByTagName(string name)
		{
			XmlNodeList elementsByTagName = base.GetElementsByTagName(name);
			int count = elementsByTagName.Count;
			return elementsByTagName;
		}

		// Token: 0x04000285 RID: 645
		private DataRow row;

		// Token: 0x04000286 RID: 646
		private ElementState state;
	}
}
