using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000C9 RID: 201
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	public abstract class XmlNode : ICloneable, IEnumerable, IXPathNavigable
	{
		// Token: 0x06000B8F RID: 2959 RVA: 0x000353C4 File Offset: 0x000343C4
		internal XmlNode()
		{
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000353CC File Offset: 0x000343CC
		internal XmlNode(XmlDocument doc)
		{
			if (doc == null)
			{
				throw new ArgumentException(Res.GetString("Xdom_Node_Null_Doc"));
			}
			this.parentNode = doc;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000353F0 File Offset: 0x000343F0
		public virtual XPathNavigator CreateNavigator()
		{
			XmlDocument xmlDocument = this as XmlDocument;
			if (xmlDocument != null)
			{
				return xmlDocument.CreateNavigator(this);
			}
			XmlDocument ownerDocument = this.OwnerDocument;
			return ownerDocument.CreateNavigator(this);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00035420 File Offset: 0x00034420
		public XmlNode SelectSingleNode(string xpath)
		{
			XmlNodeList xmlNodeList = this.SelectNodes(xpath);
			if (xmlNodeList != null && xmlNodeList.Count > 0)
			{
				return xmlNodeList[0];
			}
			return null;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0003544C File Offset: 0x0003444C
		public XmlNode SelectSingleNode(string xpath, XmlNamespaceManager nsmgr)
		{
			XPathNavigator xpathNavigator = this.CreateNavigator();
			if (xpathNavigator == null)
			{
				return null;
			}
			XPathExpression xpathExpression = xpathNavigator.Compile(xpath);
			xpathExpression.SetContext(nsmgr);
			XmlNodeList xmlNodeList = new XPathNodeList(xpathNavigator.Select(xpathExpression));
			if (xmlNodeList.Count > 0)
			{
				return xmlNodeList[0];
			}
			return null;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00035494 File Offset: 0x00034494
		public XmlNodeList SelectNodes(string xpath)
		{
			XPathNavigator xpathNavigator = this.CreateNavigator();
			if (xpathNavigator == null)
			{
				return null;
			}
			return new XPathNodeList(xpathNavigator.Select(xpath));
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000354BC File Offset: 0x000344BC
		public XmlNodeList SelectNodes(string xpath, XmlNamespaceManager nsmgr)
		{
			XPathNavigator xpathNavigator = this.CreateNavigator();
			if (xpathNavigator == null)
			{
				return null;
			}
			XPathExpression xpathExpression = xpathNavigator.Compile(xpath);
			xpathExpression.SetContext(nsmgr);
			return new XPathNodeList(xpathNavigator.Select(xpathExpression));
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000B96 RID: 2966
		public abstract string Name { get; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x000354F0 File Offset: 0x000344F0
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x000354F4 File Offset: 0x000344F4
		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Res.GetString("Xdom_Node_SetVal"), new object[]
				{
					this.NodeType.ToString()
				}));
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000B99 RID: 2969
		public abstract XmlNodeType NodeType { get; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00035538 File Offset: 0x00034538
		public virtual XmlNode ParentNode
		{
			get
			{
				if (this.parentNode.NodeType != XmlNodeType.Document)
				{
					return this.parentNode;
				}
				XmlLinkedNode xmlLinkedNode = this.parentNode.FirstChild as XmlLinkedNode;
				if (xmlLinkedNode != null)
				{
					XmlLinkedNode xmlLinkedNode2 = xmlLinkedNode;
					while (xmlLinkedNode2 != this)
					{
						xmlLinkedNode2 = xmlLinkedNode2.next;
						if (xmlLinkedNode2 == null || xmlLinkedNode2 == xmlLinkedNode)
						{
							goto IL_45;
						}
					}
					return this.parentNode;
				}
				IL_45:
				return null;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0003558B File Offset: 0x0003458B
		public virtual XmlNodeList ChildNodes
		{
			get
			{
				return new XmlChildNodes(this);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00035593 File Offset: 0x00034593
		public virtual XmlNode PreviousSibling
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x00035596 File Offset: 0x00034596
		public virtual XmlNode NextSibling
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00035599 File Offset: 0x00034599
		public virtual XmlAttributeCollection Attributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0003559C File Offset: 0x0003459C
		public virtual XmlDocument OwnerDocument
		{
			get
			{
				if (this.parentNode.NodeType == XmlNodeType.Document)
				{
					return (XmlDocument)this.parentNode;
				}
				return this.parentNode.OwnerDocument;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x000355C4 File Offset: 0x000345C4
		public virtual XmlNode FirstChild
		{
			get
			{
				XmlLinkedNode lastNode = this.LastNode;
				if (lastNode != null)
				{
					return lastNode.next;
				}
				return null;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x000355E3 File Offset: 0x000345E3
		public virtual XmlNode LastChild
		{
			get
			{
				return this.LastNode;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x000355EB File Offset: 0x000345EB
		internal virtual bool IsContainer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x000355EE File Offset: 0x000345EE
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x000355F1 File Offset: 0x000345F1
		internal virtual XmlLinkedNode LastNode
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000355F4 File Offset: 0x000345F4
		internal bool AncestorNode(XmlNode node)
		{
			XmlNode xmlNode = this.ParentNode;
			while (xmlNode != null && xmlNode != this)
			{
				if (xmlNode == node)
				{
					return true;
				}
				xmlNode = xmlNode.ParentNode;
			}
			return false;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00035620 File Offset: 0x00034620
		internal bool IsConnected()
		{
			XmlNode xmlNode = this.ParentNode;
			while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Document)
			{
				xmlNode = xmlNode.ParentNode;
			}
			return xmlNode != null;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00035654 File Offset: 0x00034654
		public virtual XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			if (this == newChild || this.AncestorNode(newChild))
			{
				throw new ArgumentException(Res.GetString("Xdom_Node_Insert_Child"));
			}
			if (refChild == null)
			{
				return this.AppendChild(newChild);
			}
			if (!this.IsContainer)
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Insert_Contain"));
			}
			if (refChild.ParentNode != this)
			{
				throw new ArgumentException(Res.GetString("Xdom_Node_Insert_Path"));
			}
			if (newChild == refChild)
			{
				return newChild;
			}
			XmlDocument ownerDocument = newChild.OwnerDocument;
			XmlDocument ownerDocument2 = this.OwnerDocument;
			if (ownerDocument != null && ownerDocument != ownerDocument2 && ownerDocument != this)
			{
				throw new ArgumentException(Res.GetString("Xdom_Node_Insert_Context"));
			}
			if (!this.CanInsertBefore(newChild, refChild))
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Insert_Location"));
			}
			if (newChild.ParentNode != null)
			{
				newChild.ParentNode.RemoveChild(newChild);
			}
			if (newChild.NodeType == XmlNodeType.DocumentFragment)
			{
				XmlNode firstChild = newChild.FirstChild;
				XmlNode xmlNode = firstChild;
				if (xmlNode != null)
				{
					newChild.RemoveChild(xmlNode);
					this.InsertBefore(xmlNode, refChild);
					this.InsertAfter(newChild, xmlNode);
				}
				return firstChild;
			}
			if (!(newChild is XmlLinkedNode) || !this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Insert_TypeConflict"));
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			XmlLinkedNode xmlLinkedNode2 = (XmlLinkedNode)refChild;
			string value = newChild.Value;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(newChild, newChild.ParentNode, this, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			if (xmlLinkedNode2 == this.FirstChild)
			{
				xmlLinkedNode.next = xmlLinkedNode2;
				this.LastNode.next = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (xmlLinkedNode.IsText && xmlLinkedNode2.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode, xmlLinkedNode2);
				}
			}
			else
			{
				XmlLinkedNode xmlLinkedNode3 = (XmlLinkedNode)xmlLinkedNode2.PreviousSibling;
				xmlLinkedNode.next = xmlLinkedNode2;
				xmlLinkedNode3.next = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (xmlLinkedNode3.IsText)
				{
					if (xmlLinkedNode.IsText)
					{
						XmlNode.NestTextNodes(xmlLinkedNode3, xmlLinkedNode);
						if (xmlLinkedNode2.IsText)
						{
							XmlNode.NestTextNodes(xmlLinkedNode, xmlLinkedNode2);
						}
					}
					else if (xmlLinkedNode2.IsText)
					{
						XmlNode.UnnestTextNodes(xmlLinkedNode3, xmlLinkedNode2);
					}
				}
				else if (xmlLinkedNode.IsText && xmlLinkedNode2.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode, xmlLinkedNode2);
				}
			}
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
			return xmlLinkedNode;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0003588C File Offset: 0x0003488C
		public virtual XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			if (this == newChild || this.AncestorNode(newChild))
			{
				throw new ArgumentException(Res.GetString("Xdom_Node_Insert_Child"));
			}
			if (refChild == null)
			{
				return this.PrependChild(newChild);
			}
			if (!this.IsContainer)
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Insert_Contain"));
			}
			if (refChild.ParentNode != this)
			{
				throw new ArgumentException(Res.GetString("Xdom_Node_Insert_Path"));
			}
			if (newChild == refChild)
			{
				return newChild;
			}
			XmlDocument ownerDocument = newChild.OwnerDocument;
			XmlDocument ownerDocument2 = this.OwnerDocument;
			if (ownerDocument != null && ownerDocument != ownerDocument2 && ownerDocument != this)
			{
				throw new ArgumentException(Res.GetString("Xdom_Node_Insert_Context"));
			}
			if (!this.CanInsertAfter(newChild, refChild))
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Insert_Location"));
			}
			if (newChild.ParentNode != null)
			{
				newChild.ParentNode.RemoveChild(newChild);
			}
			if (newChild.NodeType == XmlNodeType.DocumentFragment)
			{
				XmlNode refChild2 = refChild;
				XmlNode firstChild = newChild.FirstChild;
				XmlNode nextSibling;
				for (XmlNode xmlNode = firstChild; xmlNode != null; xmlNode = nextSibling)
				{
					nextSibling = xmlNode.NextSibling;
					newChild.RemoveChild(xmlNode);
					this.InsertAfter(xmlNode, refChild2);
					refChild2 = xmlNode;
				}
				return firstChild;
			}
			if (!(newChild is XmlLinkedNode) || !this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Insert_TypeConflict"));
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			XmlLinkedNode xmlLinkedNode2 = (XmlLinkedNode)refChild;
			string value = newChild.Value;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(newChild, newChild.ParentNode, this, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			if (xmlLinkedNode2 == this.LastNode)
			{
				xmlLinkedNode.next = xmlLinkedNode2.next;
				xmlLinkedNode2.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (xmlLinkedNode2.IsText && xmlLinkedNode.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode2, xmlLinkedNode);
				}
			}
			else
			{
				XmlLinkedNode next = xmlLinkedNode2.next;
				xmlLinkedNode.next = next;
				xmlLinkedNode2.next = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (xmlLinkedNode2.IsText)
				{
					if (xmlLinkedNode.IsText)
					{
						XmlNode.NestTextNodes(xmlLinkedNode2, xmlLinkedNode);
						if (next.IsText)
						{
							XmlNode.NestTextNodes(xmlLinkedNode, next);
						}
					}
					else if (next.IsText)
					{
						XmlNode.UnnestTextNodes(xmlLinkedNode2, next);
					}
				}
				else if (xmlLinkedNode.IsText && next.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode, next);
				}
			}
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
			return xmlLinkedNode;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00035AD8 File Offset: 0x00034AD8
		public virtual XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			XmlNode nextSibling = oldChild.NextSibling;
			this.RemoveChild(oldChild);
			this.InsertBefore(newChild, nextSibling);
			return oldChild;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00035B00 File Offset: 0x00034B00
		public virtual XmlNode RemoveChild(XmlNode oldChild)
		{
			if (!this.IsContainer)
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Remove_Contain"));
			}
			if (oldChild.ParentNode != this)
			{
				throw new ArgumentException(Res.GetString("Xdom_Node_Remove_Child"));
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)oldChild;
			string value = xmlLinkedNode.Value;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(xmlLinkedNode, this, null, value, value, XmlNodeChangedAction.Remove);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			XmlLinkedNode lastNode = this.LastNode;
			if (xmlLinkedNode == this.FirstChild)
			{
				if (xmlLinkedNode == lastNode)
				{
					this.LastNode = null;
					xmlLinkedNode.next = null;
					xmlLinkedNode.SetParent(null);
				}
				else
				{
					XmlLinkedNode next = xmlLinkedNode.next;
					if (next.IsText && xmlLinkedNode.IsText)
					{
						XmlNode.UnnestTextNodes(xmlLinkedNode, next);
					}
					lastNode.next = next;
					xmlLinkedNode.next = null;
					xmlLinkedNode.SetParent(null);
				}
			}
			else if (xmlLinkedNode == lastNode)
			{
				XmlLinkedNode xmlLinkedNode2 = (XmlLinkedNode)xmlLinkedNode.PreviousSibling;
				xmlLinkedNode2.next = xmlLinkedNode.next;
				this.LastNode = xmlLinkedNode2;
				xmlLinkedNode.next = null;
				xmlLinkedNode.SetParent(null);
			}
			else
			{
				XmlLinkedNode xmlLinkedNode3 = (XmlLinkedNode)xmlLinkedNode.PreviousSibling;
				XmlLinkedNode next2 = xmlLinkedNode.next;
				if (next2.IsText)
				{
					if (xmlLinkedNode3.IsText)
					{
						XmlNode.NestTextNodes(xmlLinkedNode3, next2);
					}
					else if (xmlLinkedNode.IsText)
					{
						XmlNode.UnnestTextNodes(xmlLinkedNode, next2);
					}
				}
				xmlLinkedNode3.next = next2;
				xmlLinkedNode.next = null;
				xmlLinkedNode.SetParent(null);
			}
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
			return oldChild;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00035C67 File Offset: 0x00034C67
		public virtual XmlNode PrependChild(XmlNode newChild)
		{
			return this.InsertBefore(newChild, this.FirstChild);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00035C78 File Offset: 0x00034C78
		public virtual XmlNode AppendChild(XmlNode newChild)
		{
			XmlDocument xmlDocument = this.OwnerDocument;
			if (xmlDocument == null)
			{
				xmlDocument = (this as XmlDocument);
			}
			if (!this.IsContainer)
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Insert_Contain"));
			}
			if (this == newChild || this.AncestorNode(newChild))
			{
				throw new ArgumentException(Res.GetString("Xdom_Node_Insert_Child"));
			}
			if (newChild.ParentNode != null)
			{
				newChild.ParentNode.RemoveChild(newChild);
			}
			XmlDocument ownerDocument = newChild.OwnerDocument;
			if (ownerDocument != null && ownerDocument != xmlDocument && ownerDocument != this)
			{
				throw new ArgumentException(Res.GetString("Xdom_Node_Insert_Context"));
			}
			if (newChild.NodeType == XmlNodeType.DocumentFragment)
			{
				XmlNode firstChild = newChild.FirstChild;
				XmlNode nextSibling;
				for (XmlNode xmlNode = firstChild; xmlNode != null; xmlNode = nextSibling)
				{
					nextSibling = xmlNode.NextSibling;
					newChild.RemoveChild(xmlNode);
					this.AppendChild(xmlNode);
				}
				return firstChild;
			}
			if (!(newChild is XmlLinkedNode) || !this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Insert_TypeConflict"));
			}
			if (!this.CanInsertAfter(newChild, this.LastChild))
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Insert_Location"));
			}
			string value = newChild.Value;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(newChild, newChild.ParentNode, this, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			XmlLinkedNode lastNode = this.LastNode;
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (lastNode == null)
			{
				xmlLinkedNode.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
			}
			else
			{
				xmlLinkedNode.next = lastNode.next;
				lastNode.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (lastNode.IsText && xmlLinkedNode.IsText)
				{
					XmlNode.NestTextNodes(lastNode, xmlLinkedNode);
				}
			}
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
			return xmlLinkedNode;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00035E24 File Offset: 0x00034E24
		internal virtual XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode lastNode = this.LastNode;
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (lastNode == null)
			{
				xmlLinkedNode.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				xmlLinkedNode.SetParentForLoad(this);
			}
			else
			{
				xmlLinkedNode.next = lastNode.next;
				lastNode.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				if (lastNode.IsText && xmlLinkedNode.IsText)
				{
					XmlNode.NestTextNodes(lastNode, xmlLinkedNode);
				}
				else
				{
					xmlLinkedNode.SetParentForLoad(this);
				}
			}
			if (insertEventArgsForLoad != null)
			{
				doc.AfterEvent(insertEventArgsForLoad);
			}
			return xmlLinkedNode;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00035EB1 File Offset: 0x00034EB1
		internal virtual bool IsValidChildType(XmlNodeType type)
		{
			return false;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00035EB4 File Offset: 0x00034EB4
		internal virtual bool CanInsertBefore(XmlNode newChild, XmlNode refChild)
		{
			return true;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00035EB7 File Offset: 0x00034EB7
		internal virtual bool CanInsertAfter(XmlNode newChild, XmlNode refChild)
		{
			return true;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x00035EBA File Offset: 0x00034EBA
		public virtual bool HasChildNodes
		{
			get
			{
				return this.LastNode != null;
			}
		}

		// Token: 0x06000BB2 RID: 2994
		public abstract XmlNode CloneNode(bool deep);

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00035EC8 File Offset: 0x00034EC8
		internal virtual void CopyChildren(XmlDocument doc, XmlNode container, bool deep)
		{
			for (XmlNode xmlNode = container.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.AppendChildForLoad(xmlNode.CloneNode(deep), doc);
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00035EF8 File Offset: 0x00034EF8
		public virtual void Normalize()
		{
			XmlNode xmlNode = null;
			StringBuilder stringBuilder = new StringBuilder();
			XmlNode xmlNode2 = this.FirstChild;
			while (xmlNode2 != null)
			{
				XmlNode nextSibling = xmlNode2.NextSibling;
				XmlNodeType nodeType = xmlNode2.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					xmlNode2.Normalize();
					goto IL_87;
				case XmlNodeType.Attribute:
					goto IL_87;
				case XmlNodeType.Text:
					goto IL_4C;
				default:
					switch (nodeType)
					{
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						goto IL_4C;
					default:
						goto IL_87;
					}
					break;
				}
				IL_A6:
				xmlNode2 = nextSibling;
				continue;
				IL_4C:
				stringBuilder.Append(xmlNode2.Value);
				XmlNode xmlNode3 = this.NormalizeWinner(xmlNode, xmlNode2);
				if (xmlNode3 == xmlNode)
				{
					this.RemoveChild(xmlNode2);
					goto IL_A6;
				}
				if (xmlNode != null)
				{
					this.RemoveChild(xmlNode);
				}
				xmlNode = xmlNode2;
				goto IL_A6;
				IL_87:
				if (xmlNode != null)
				{
					xmlNode.Value = stringBuilder.ToString();
					xmlNode = null;
				}
				stringBuilder.Remove(0, stringBuilder.Length);
				goto IL_A6;
			}
			if (xmlNode != null && stringBuilder.Length > 0)
			{
				xmlNode.Value = stringBuilder.ToString();
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00035FCC File Offset: 0x00034FCC
		private XmlNode NormalizeWinner(XmlNode firstNode, XmlNode secondNode)
		{
			if (firstNode == null)
			{
				return secondNode;
			}
			if (firstNode.NodeType == XmlNodeType.Text)
			{
				return firstNode;
			}
			if (secondNode.NodeType == XmlNodeType.Text)
			{
				return secondNode;
			}
			if (firstNode.NodeType == XmlNodeType.SignificantWhitespace)
			{
				return firstNode;
			}
			if (secondNode.NodeType == XmlNodeType.SignificantWhitespace)
			{
				return secondNode;
			}
			if (firstNode.NodeType == XmlNodeType.Whitespace)
			{
				return firstNode;
			}
			if (secondNode.NodeType == XmlNodeType.Whitespace)
			{
				return secondNode;
			}
			return null;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00036025 File Offset: 0x00035025
		public virtual bool Supports(string feature, string version)
		{
			return string.Compare("XML", feature, StringComparison.OrdinalIgnoreCase) == 0 && (version == null || version == "1.0" || version == "2.0");
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x00036055 File Offset: 0x00035055
		public virtual string NamespaceURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0003605C File Offset: 0x0003505C
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x00036063 File Offset: 0x00035063
		public virtual string Prefix
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000BBA RID: 3002
		public abstract string LocalName { get; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00036065 File Offset: 0x00035065
		public virtual bool IsReadOnly
		{
			get
			{
				XmlDocument ownerDocument = this.OwnerDocument;
				return XmlNode.HasReadOnlyParent(this);
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00036074 File Offset: 0x00035074
		internal static bool HasReadOnlyParent(XmlNode n)
		{
			while (n != null)
			{
				switch (n.NodeType)
				{
				case XmlNodeType.Attribute:
					n = ((XmlAttribute)n).OwnerElement;
					continue;
				case XmlNodeType.EntityReference:
				case XmlNodeType.Entity:
					return true;
				}
				n = n.ParentNode;
			}
			return false;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000360C5 File Offset: 0x000350C5
		public virtual XmlNode Clone()
		{
			return this.CloneNode(true);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000360CE File Offset: 0x000350CE
		object ICloneable.Clone()
		{
			return this.CloneNode(true);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000360D7 File Offset: 0x000350D7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new XmlChildEnumerator(this);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000360DF File Offset: 0x000350DF
		public IEnumerator GetEnumerator()
		{
			return new XmlChildEnumerator(this);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000360E8 File Offset: 0x000350E8
		private void AppendChildText(StringBuilder builder)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.FirstChild == null)
				{
					if (xmlNode.NodeType == XmlNodeType.Text || xmlNode.NodeType == XmlNodeType.CDATA || xmlNode.NodeType == XmlNodeType.Whitespace || xmlNode.NodeType == XmlNodeType.SignificantWhitespace)
					{
						builder.Append(xmlNode.InnerText);
					}
				}
				else
				{
					xmlNode.AppendChildText(builder);
				}
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0003614C File Offset: 0x0003514C
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x000361B8 File Offset: 0x000351B8
		public virtual string InnerText
		{
			get
			{
				XmlNode firstChild = this.FirstChild;
				if (firstChild == null)
				{
					return string.Empty;
				}
				if (firstChild.NextSibling == null)
				{
					XmlNodeType nodeType = firstChild.NodeType;
					XmlNodeType xmlNodeType = nodeType;
					switch (xmlNodeType)
					{
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						break;
					default:
						switch (xmlNodeType)
						{
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							break;
						default:
							goto IL_4B;
						}
						break;
					}
					return firstChild.Value;
				}
				IL_4B:
				StringBuilder stringBuilder = new StringBuilder();
				this.AppendChildText(stringBuilder);
				return stringBuilder.ToString();
			}
			set
			{
				XmlNode firstChild = this.FirstChild;
				if (firstChild != null && firstChild.NextSibling == null && firstChild.NodeType == XmlNodeType.Text)
				{
					firstChild.Value = value;
					return;
				}
				this.RemoveAll();
				this.AppendChild(this.OwnerDocument.CreateTextNode(value));
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00036204 File Offset: 0x00035204
		public virtual string OuterXml
		{
			get
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(stringWriter);
				try
				{
					this.WriteTo(xmlDOMTextWriter);
				}
				finally
				{
					xmlDOMTextWriter.Close();
				}
				return stringWriter.ToString();
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0003624C File Offset: 0x0003524C
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x00036294 File Offset: 0x00035294
		public virtual string InnerXml
		{
			get
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(stringWriter);
				try
				{
					this.WriteContentTo(xmlDOMTextWriter);
				}
				finally
				{
					xmlDOMTextWriter.Close();
				}
				return stringWriter.ToString();
			}
			set
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Set_InnerXml"));
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x000362A5 File Offset: 0x000352A5
		public virtual IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return XmlDocument.NotKnownSchemaInfo;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x000362AC File Offset: 0x000352AC
		public virtual string BaseURI
		{
			get
			{
				for (XmlNode xmlNode = this.ParentNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
				{
					XmlNodeType nodeType = xmlNode.NodeType;
					if (nodeType == XmlNodeType.EntityReference)
					{
						return ((XmlEntityReference)xmlNode).ChildBaseURI;
					}
					if (nodeType == XmlNodeType.Document || nodeType == XmlNodeType.Entity || nodeType == XmlNodeType.Attribute)
					{
						return xmlNode.BaseURI;
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x06000BC9 RID: 3017
		public abstract void WriteTo(XmlWriter w);

		// Token: 0x06000BCA RID: 3018
		public abstract void WriteContentTo(XmlWriter w);

		// Token: 0x06000BCB RID: 3019 RVA: 0x000362FC File Offset: 0x000352FC
		public virtual void RemoveAll()
		{
			XmlNode nextSibling;
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = nextSibling)
			{
				nextSibling = xmlNode.NextSibling;
				this.RemoveChild(xmlNode);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00036328 File Offset: 0x00035328
		internal XmlDocument Document
		{
			get
			{
				if (this.NodeType == XmlNodeType.Document)
				{
					return (XmlDocument)this;
				}
				return this.OwnerDocument;
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00036344 File Offset: 0x00035344
		public virtual string GetNamespaceOfPrefix(string prefix)
		{
			string namespaceOfPrefixStrict = this.GetNamespaceOfPrefixStrict(prefix);
			if (namespaceOfPrefixStrict == null)
			{
				return string.Empty;
			}
			return namespaceOfPrefixStrict;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00036364 File Offset: 0x00035364
		internal string GetNamespaceOfPrefixStrict(string prefix)
		{
			XmlDocument document = this.Document;
			if (document != null)
			{
				prefix = document.NameTable.Get(prefix);
				if (prefix == null)
				{
					return null;
				}
				XmlNode xmlNode = this;
				while (xmlNode != null)
				{
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						XmlElement xmlElement = (XmlElement)xmlNode;
						if (xmlElement.HasAttributes)
						{
							XmlAttributeCollection attributes = xmlElement.Attributes;
							if (prefix.Length == 0)
							{
								for (int i = 0; i < attributes.Count; i++)
								{
									XmlAttribute xmlAttribute = attributes[i];
									if (xmlAttribute.Prefix.Length == 0 && Ref.Equal(xmlAttribute.LocalName, document.strXmlns))
									{
										return xmlAttribute.Value;
									}
								}
							}
							else
							{
								for (int j = 0; j < attributes.Count; j++)
								{
									XmlAttribute xmlAttribute2 = attributes[j];
									if (Ref.Equal(xmlAttribute2.Prefix, document.strXmlns))
									{
										if (Ref.Equal(xmlAttribute2.LocalName, prefix))
										{
											return xmlAttribute2.Value;
										}
									}
									else if (Ref.Equal(xmlAttribute2.Prefix, prefix))
									{
										return xmlAttribute2.NamespaceURI;
									}
								}
							}
						}
						if (Ref.Equal(xmlNode.Prefix, prefix))
						{
							return xmlNode.NamespaceURI;
						}
						xmlNode = xmlNode.ParentNode;
					}
					else if (xmlNode.NodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					}
					else
					{
						xmlNode = xmlNode.ParentNode;
					}
				}
				if (Ref.Equal(document.strXml, prefix))
				{
					return document.strReservedXml;
				}
				if (Ref.Equal(document.strXmlns, prefix))
				{
					return document.strReservedXmlns;
				}
			}
			return null;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x000364E0 File Offset: 0x000354E0
		public virtual string GetPrefixOfNamespace(string namespaceURI)
		{
			string prefixOfNamespaceStrict = this.GetPrefixOfNamespaceStrict(namespaceURI);
			if (prefixOfNamespaceStrict == null)
			{
				return string.Empty;
			}
			return prefixOfNamespaceStrict;
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00036500 File Offset: 0x00035500
		internal string GetPrefixOfNamespaceStrict(string namespaceURI)
		{
			XmlDocument document = this.Document;
			if (document != null)
			{
				namespaceURI = document.NameTable.Add(namespaceURI);
				XmlNode xmlNode = this;
				while (xmlNode != null)
				{
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						XmlElement xmlElement = (XmlElement)xmlNode;
						if (xmlElement.HasAttributes)
						{
							XmlAttributeCollection attributes = xmlElement.Attributes;
							for (int i = 0; i < attributes.Count; i++)
							{
								XmlAttribute xmlAttribute = attributes[i];
								if (xmlAttribute.Prefix.Length == 0)
								{
									if (Ref.Equal(xmlAttribute.LocalName, document.strXmlns) && xmlAttribute.Value == namespaceURI)
									{
										return string.Empty;
									}
								}
								else if (Ref.Equal(xmlAttribute.Prefix, document.strXmlns))
								{
									if (xmlAttribute.Value == namespaceURI)
									{
										return xmlAttribute.LocalName;
									}
								}
								else if (Ref.Equal(xmlAttribute.NamespaceURI, namespaceURI))
								{
									return xmlAttribute.Prefix;
								}
							}
						}
						if (Ref.Equal(xmlNode.NamespaceURI, namespaceURI))
						{
							return xmlNode.Prefix;
						}
						xmlNode = xmlNode.ParentNode;
					}
					else if (xmlNode.NodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					}
					else
					{
						xmlNode = xmlNode.ParentNode;
					}
				}
				if (object.Equals(document.strReservedXml, namespaceURI))
				{
					return document.strXml;
				}
				if (object.Equals(document.strReservedXmlns, namespaceURI))
				{
					return document.strXmlns;
				}
			}
			return null;
		}

		// Token: 0x17000295 RID: 661
		public virtual XmlElement this[string name]
		{
			get
			{
				for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.Name == name)
					{
						return (XmlElement)xmlNode;
					}
				}
				return null;
			}
		}

		// Token: 0x17000296 RID: 662
		public virtual XmlElement this[string localname, string ns]
		{
			get
			{
				for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.LocalName == localname && xmlNode.NamespaceURI == ns)
					{
						return (XmlElement)xmlNode;
					}
				}
				return null;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000366ED File Offset: 0x000356ED
		internal virtual void SetParent(XmlNode node)
		{
			if (node == null)
			{
				this.parentNode = this.OwnerDocument;
				return;
			}
			this.parentNode = node;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00036706 File Offset: 0x00035706
		internal virtual void SetParentForLoad(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00036710 File Offset: 0x00035710
		internal static void SplitName(string name, out string prefix, out string localName)
		{
			int num = name.IndexOf(':');
			if (-1 == num || num == 0 || name.Length - 1 == num)
			{
				prefix = string.Empty;
				localName = name;
				return;
			}
			prefix = name.Substring(0, num);
			localName = name.Substring(num + 1);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00036758 File Offset: 0x00035758
		internal virtual XmlNode FindChild(XmlNodeType type)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == type)
				{
					return xmlNode;
				}
			}
			return null;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00036784 File Offset: 0x00035784
		internal virtual XmlNodeChangedEventArgs GetEventArgs(XmlNode node, XmlNode oldParent, XmlNode newParent, string oldValue, string newValue, XmlNodeChangedAction action)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			if (ownerDocument == null)
			{
				return null;
			}
			if (!ownerDocument.IsLoading && ((newParent != null && newParent.IsReadOnly) || (oldParent != null && oldParent.IsReadOnly)))
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Modify_ReadOnly"));
			}
			return ownerDocument.GetEventArgs(node, oldParent, newParent, oldValue, newValue, action);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000367DA File Offset: 0x000357DA
		internal virtual void BeforeEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				this.OwnerDocument.BeforeEvent(args);
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000367EB File Offset: 0x000357EB
		internal virtual void AfterEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				this.OwnerDocument.AfterEvent(args);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x000367FC File Offset: 0x000357FC
		internal virtual XmlSpace XmlSpace
		{
			get
			{
				XmlNode xmlNode = this;
				for (;;)
				{
					XmlElement xmlElement = xmlNode as XmlElement;
					if (xmlElement != null && xmlElement.HasAttribute("xml:space"))
					{
						string attribute = xmlElement.GetAttribute("xml:space");
						if (string.Compare(attribute, "default", StringComparison.OrdinalIgnoreCase) == 0)
						{
							break;
						}
						if (string.Compare(attribute, "preserve", StringComparison.OrdinalIgnoreCase) == 0)
						{
							return XmlSpace.Preserve;
						}
					}
					xmlNode = xmlNode.ParentNode;
					if (xmlNode == null)
					{
						return XmlSpace.None;
					}
				}
				return XmlSpace.Default;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0003685C File Offset: 0x0003585C
		internal virtual string XmlLang
		{
			get
			{
				XmlNode xmlNode = this;
				XmlElement xmlElement;
				for (;;)
				{
					xmlElement = (xmlNode as XmlElement);
					if (xmlElement != null && xmlElement.HasAttribute("xml:lang"))
					{
						break;
					}
					xmlNode = xmlNode.ParentNode;
					if (xmlNode == null)
					{
						goto Block_3;
					}
				}
				return xmlElement.GetAttribute("xml:lang");
				Block_3:
				return string.Empty;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0003689F File Offset: 0x0003589F
		internal virtual XPathNodeType XPNodeType
		{
			get
			{
				return (XPathNodeType)(-1);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x000368A2 File Offset: 0x000358A2
		internal virtual string XPLocalName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000368A9 File Offset: 0x000358A9
		internal virtual string GetXPAttribute(string localName, string namespaceURI)
		{
			return string.Empty;
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x000368B0 File Offset: 0x000358B0
		internal virtual bool IsText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x000368B3 File Offset: 0x000358B3
		internal virtual XmlNode PreviousText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000368B6 File Offset: 0x000358B6
		internal static void NestTextNodes(XmlNode prevNode, XmlNode nextNode)
		{
			nextNode.parentNode = prevNode;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x000368BF File Offset: 0x000358BF
		internal static void UnnestTextNodes(XmlNode prevNode, XmlNode nextNode)
		{
			nextNode.parentNode = prevNode.ParentNode;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x000368CD File Offset: 0x000358CD
		private object debuggerDisplayProxy
		{
			get
			{
				return new DebuggerDisplayXmlNodeProxy(this);
			}
		}

		// Token: 0x040008EC RID: 2284
		internal XmlNode parentNode;
	}
}
