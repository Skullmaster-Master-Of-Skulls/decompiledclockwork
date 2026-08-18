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
	// Token: 0x02000114 RID: 276
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	public abstract class XmlNode : ICloneable, IEnumerable, IXPathNavigable
	{
		// Token: 0x06001338 RID: 4920 RVA: 0x000501A7 File Offset: 0x0004E3A7
		internal XmlNode()
		{
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x000501AF File Offset: 0x0004E3AF
		internal XmlNode(XmlDocument doc)
		{
			if (doc == null)
			{
				throw new ArgumentException(Res.GetString("Xdom_Node_Null_Doc"));
			}
			this.parentNode = doc;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000501D4 File Offset: 0x0004E3D4
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

		// Token: 0x0600133B RID: 4923 RVA: 0x00050204 File Offset: 0x0004E404
		public XmlNode SelectSingleNode(string xpath)
		{
			XmlNodeList xmlNodeList = this.SelectNodes(xpath);
			if (xmlNodeList == null)
			{
				return null;
			}
			return xmlNodeList[0];
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00050228 File Offset: 0x0004E428
		public XmlNode SelectSingleNode(string xpath, XmlNamespaceManager nsmgr)
		{
			XPathNavigator xpathNavigator = this.CreateNavigator();
			if (xpathNavigator == null)
			{
				return null;
			}
			XPathExpression xpathExpression = xpathNavigator.Compile(xpath);
			xpathExpression.SetContext(nsmgr);
			return new XPathNodeList(xpathNavigator.Select(xpathExpression))[0];
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00050264 File Offset: 0x0004E464
		public XmlNodeList SelectNodes(string xpath)
		{
			XPathNavigator xpathNavigator = this.CreateNavigator();
			if (xpathNavigator == null)
			{
				return null;
			}
			return new XPathNodeList(xpathNavigator.Select(xpath));
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0005028C File Offset: 0x0004E48C
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

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600133F RID: 4927
		public abstract string Name { get; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x000502C0 File Offset: 0x0004E4C0
		// (set) Token: 0x06001341 RID: 4929 RVA: 0x000502C4 File Offset: 0x0004E4C4
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

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001342 RID: 4930
		public abstract XmlNodeType NodeType { get; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x00050308 File Offset: 0x0004E508
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

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x0005035B File Offset: 0x0004E55B
		public virtual XmlNodeList ChildNodes
		{
			get
			{
				return new XmlChildNodes(this);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x00050363 File Offset: 0x0004E563
		public virtual XmlNode PreviousSibling
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00050366 File Offset: 0x0004E566
		public virtual XmlNode NextSibling
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00050369 File Offset: 0x0004E569
		public virtual XmlAttributeCollection Attributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0005036C File Offset: 0x0004E56C
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

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00050394 File Offset: 0x0004E594
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

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x000503B3 File Offset: 0x0004E5B3
		public virtual XmlNode LastChild
		{
			get
			{
				return this.LastNode;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x000503BB File Offset: 0x0004E5BB
		internal virtual bool IsContainer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x000503BE File Offset: 0x0004E5BE
		// (set) Token: 0x0600134D RID: 4941 RVA: 0x000503C1 File Offset: 0x0004E5C1
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

		// Token: 0x0600134E RID: 4942 RVA: 0x000503C4 File Offset: 0x0004E5C4
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

		// Token: 0x0600134F RID: 4943 RVA: 0x000503F0 File Offset: 0x0004E5F0
		internal bool IsConnected()
		{
			XmlNode xmlNode = this.ParentNode;
			while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Document)
			{
				xmlNode = xmlNode.ParentNode;
			}
			return xmlNode != null;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00050420 File Offset: 0x0004E620
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

		// Token: 0x06001351 RID: 4945 RVA: 0x00050640 File Offset: 0x0004E840
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

		// Token: 0x06001352 RID: 4946 RVA: 0x00050870 File Offset: 0x0004EA70
		public virtual XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			XmlNode nextSibling = oldChild.NextSibling;
			this.RemoveChild(oldChild);
			XmlNode xmlNode = this.InsertBefore(newChild, nextSibling);
			return oldChild;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00050898 File Offset: 0x0004EA98
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

		// Token: 0x06001354 RID: 4948 RVA: 0x000509FF File Offset: 0x0004EBFF
		public virtual XmlNode PrependChild(XmlNode newChild)
		{
			return this.InsertBefore(newChild, this.FirstChild);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00050A10 File Offset: 0x0004EC10
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

		// Token: 0x06001356 RID: 4950 RVA: 0x00050BC0 File Offset: 0x0004EDC0
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

		// Token: 0x06001357 RID: 4951 RVA: 0x00050C4D File Offset: 0x0004EE4D
		internal virtual bool IsValidChildType(XmlNodeType type)
		{
			return false;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00050C50 File Offset: 0x0004EE50
		internal virtual bool CanInsertBefore(XmlNode newChild, XmlNode refChild)
		{
			return true;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00050C53 File Offset: 0x0004EE53
		internal virtual bool CanInsertAfter(XmlNode newChild, XmlNode refChild)
		{
			return true;
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x00050C56 File Offset: 0x0004EE56
		public virtual bool HasChildNodes
		{
			get
			{
				return this.LastNode != null;
			}
		}

		// Token: 0x0600135B RID: 4955
		public abstract XmlNode CloneNode(bool deep);

		// Token: 0x0600135C RID: 4956 RVA: 0x00050C64 File Offset: 0x0004EE64
		internal virtual void CopyChildren(XmlDocument doc, XmlNode container, bool deep)
		{
			for (XmlNode xmlNode = container.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.AppendChildForLoad(xmlNode.CloneNode(deep), doc);
			}
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00050C94 File Offset: 0x0004EE94
		public virtual void Normalize()
		{
			XmlNode xmlNode = null;
			StringBuilder stringBuilder = new StringBuilder();
			XmlNode xmlNode2 = this.FirstChild;
			while (xmlNode2 != null)
			{
				XmlNode nextSibling = xmlNode2.NextSibling;
				XmlNodeType nodeType = xmlNode2.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					xmlNode2.Normalize();
					goto IL_6D;
				}
				if (nodeType != XmlNodeType.Text && nodeType - XmlNodeType.Whitespace > 1)
				{
					goto IL_6D;
				}
				stringBuilder.Append(xmlNode2.Value);
				XmlNode xmlNode3 = this.NormalizeWinner(xmlNode, xmlNode2);
				if (xmlNode3 == xmlNode)
				{
					this.RemoveChild(xmlNode2);
				}
				else
				{
					if (xmlNode != null)
					{
						this.RemoveChild(xmlNode);
					}
					xmlNode = xmlNode2;
				}
				IL_8C:
				xmlNode2 = nextSibling;
				continue;
				IL_6D:
				if (xmlNode != null)
				{
					xmlNode.Value = stringBuilder.ToString();
					xmlNode = null;
				}
				stringBuilder.Remove(0, stringBuilder.Length);
				goto IL_8C;
			}
			if (xmlNode != null && stringBuilder.Length > 0)
			{
				xmlNode.Value = stringBuilder.ToString();
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00050D4C File Offset: 0x0004EF4C
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

		// Token: 0x0600135F RID: 4959 RVA: 0x00050DA5 File Offset: 0x0004EFA5
		public virtual bool Supports(string feature, string version)
		{
			return string.Compare("XML", feature, StringComparison.OrdinalIgnoreCase) == 0 && (version == null || version == "1.0" || version == "2.0");
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x00050DD5 File Offset: 0x0004EFD5
		public virtual string NamespaceURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x00050DDC File Offset: 0x0004EFDC
		// (set) Token: 0x06001362 RID: 4962 RVA: 0x00050DE3 File Offset: 0x0004EFE3
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001363 RID: 4963
		public abstract string LocalName { get; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x00050DE8 File Offset: 0x0004EFE8
		public virtual bool IsReadOnly
		{
			get
			{
				XmlDocument ownerDocument = this.OwnerDocument;
				return XmlNode.HasReadOnlyParent(this);
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00050E04 File Offset: 0x0004F004
		internal static bool HasReadOnlyParent(XmlNode n)
		{
			while (n != null)
			{
				XmlNodeType nodeType = n.NodeType;
				if (nodeType != XmlNodeType.Attribute)
				{
					if (nodeType - XmlNodeType.EntityReference <= 1)
					{
						return true;
					}
					n = n.ParentNode;
				}
				else
				{
					n = ((XmlAttribute)n).OwnerElement;
				}
			}
			return false;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00050E41 File Offset: 0x0004F041
		public virtual XmlNode Clone()
		{
			return this.CloneNode(true);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00050E4A File Offset: 0x0004F04A
		object ICloneable.Clone()
		{
			return this.CloneNode(true);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00050E53 File Offset: 0x0004F053
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new XmlChildEnumerator(this);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00050E5B File Offset: 0x0004F05B
		public IEnumerator GetEnumerator()
		{
			return new XmlChildEnumerator(this);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00050E64 File Offset: 0x0004F064
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

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x00050EC8 File Offset: 0x0004F0C8
		// (set) Token: 0x0600136C RID: 4972 RVA: 0x00050F1C File Offset: 0x0004F11C
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
					if (nodeType - XmlNodeType.Text <= 1 || nodeType - XmlNodeType.Whitespace <= 1)
					{
						return firstChild.Value;
					}
				}
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

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x00050F68 File Offset: 0x0004F168
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

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x00050FB0 File Offset: 0x0004F1B0
		// (set) Token: 0x0600136F RID: 4975 RVA: 0x00050FF8 File Offset: 0x0004F1F8
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

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x00051009 File Offset: 0x0004F209
		public virtual IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return XmlDocument.NotKnownSchemaInfo;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x00051010 File Offset: 0x0004F210
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

		// Token: 0x06001372 RID: 4978
		public abstract void WriteTo(XmlWriter w);

		// Token: 0x06001373 RID: 4979
		public abstract void WriteContentTo(XmlWriter w);

		// Token: 0x06001374 RID: 4980 RVA: 0x00051060 File Offset: 0x0004F260
		public virtual void RemoveAll()
		{
			XmlNode nextSibling;
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = nextSibling)
			{
				nextSibling = xmlNode.NextSibling;
				this.RemoveChild(xmlNode);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x0005108C File Offset: 0x0004F28C
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

		// Token: 0x06001376 RID: 4982 RVA: 0x000510A8 File Offset: 0x0004F2A8
		public virtual string GetNamespaceOfPrefix(string prefix)
		{
			string namespaceOfPrefixStrict = this.GetNamespaceOfPrefixStrict(prefix);
			if (namespaceOfPrefixStrict == null)
			{
				return string.Empty;
			}
			return namespaceOfPrefixStrict;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x000510C8 File Offset: 0x0004F2C8
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

		// Token: 0x06001378 RID: 4984 RVA: 0x00051244 File Offset: 0x0004F444
		public virtual string GetPrefixOfNamespace(string namespaceURI)
		{
			string prefixOfNamespaceStrict = this.GetPrefixOfNamespaceStrict(namespaceURI);
			if (prefixOfNamespaceStrict == null)
			{
				return string.Empty;
			}
			return prefixOfNamespaceStrict;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00051264 File Offset: 0x0004F464
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
				if (Ref.Equal(document.strReservedXml, namespaceURI))
				{
					return document.strXml;
				}
				if (Ref.Equal(document.strReservedXmlns, namespaceURI))
				{
					return document.strXmlns;
				}
			}
			return null;
		}

		// Token: 0x17000406 RID: 1030
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

		// Token: 0x17000407 RID: 1031
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

		// Token: 0x0600137C RID: 4988 RVA: 0x00051451 File Offset: 0x0004F651
		internal virtual void SetParent(XmlNode node)
		{
			if (node == null)
			{
				this.parentNode = this.OwnerDocument;
				return;
			}
			this.parentNode = node;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0005146A File Offset: 0x0004F66A
		internal virtual void SetParentForLoad(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00051474 File Offset: 0x0004F674
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

		// Token: 0x0600137F RID: 4991 RVA: 0x000514BC File Offset: 0x0004F6BC
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

		// Token: 0x06001380 RID: 4992 RVA: 0x000514E8 File Offset: 0x0004F6E8
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

		// Token: 0x06001381 RID: 4993 RVA: 0x0005153E File Offset: 0x0004F73E
		internal virtual void BeforeEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				this.OwnerDocument.BeforeEvent(args);
			}
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0005154F File Offset: 0x0004F74F
		internal virtual void AfterEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				this.OwnerDocument.AfterEvent(args);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x00051560 File Offset: 0x0004F760
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
						string a = XmlConvert.TrimString(xmlElement.GetAttribute("xml:space"));
						if (a == "default")
						{
							break;
						}
						if (a == "preserve")
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

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x000515C4 File Offset: 0x0004F7C4
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

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x00051607 File Offset: 0x0004F807
		internal virtual XPathNodeType XPNodeType
		{
			get
			{
				return (XPathNodeType)(-1);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x0005160A File Offset: 0x0004F80A
		internal virtual string XPLocalName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00051611 File Offset: 0x0004F811
		internal virtual string GetXPAttribute(string localName, string namespaceURI)
		{
			return string.Empty;
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x00051618 File Offset: 0x0004F818
		internal virtual bool IsText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x0005161B File Offset: 0x0004F81B
		public virtual XmlNode PreviousText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0005161E File Offset: 0x0004F81E
		internal static void NestTextNodes(XmlNode prevNode, XmlNode nextNode)
		{
			nextNode.parentNode = prevNode;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00051627 File Offset: 0x0004F827
		internal static void UnnestTextNodes(XmlNode prevNode, XmlNode nextNode)
		{
			nextNode.parentNode = prevNode.ParentNode;
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x00051635 File Offset: 0x0004F835
		private object debuggerDisplayProxy
		{
			get
			{
				return new DebuggerDisplayXmlNodeProxy(this);
			}
		}

		// Token: 0x0400055C RID: 1372
		internal XmlNode parentNode;
	}
}
