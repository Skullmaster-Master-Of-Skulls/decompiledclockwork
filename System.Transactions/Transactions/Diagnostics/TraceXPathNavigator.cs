using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace System.Transactions.Diagnostics
{
	// Token: 0x020000C9 RID: 201
	internal class TraceXPathNavigator : XPathNavigator
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x00042974 File Offset: 0x00041D74
		internal void AddElement(string prefix, string name, string xmlns)
		{
			TraceXPathNavigator.ElementNode item = new TraceXPathNavigator.ElementNode(name, prefix, xmlns, this.current);
			if (this.closed)
			{
				throw new InvalidOperationException(SR.GetString("CannotAddToClosedDocument"));
			}
			if (this.current == null)
			{
				this.root = item;
				this.current = this.root;
				return;
			}
			if (!this.closed)
			{
				this.current.childNodes.Add(item);
				this.current = item;
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000429E4 File Offset: 0x00041DE4
		internal void AddText(string value)
		{
			if (this.closed)
			{
				throw new InvalidOperationException(SR.GetString("CannotAddToClosedDocument"));
			}
			if (this.current == null)
			{
				throw new InvalidOperationException(SR.GetString("OperationInvalidOnAnEmptyDocument"));
			}
			if (this.current.text != null)
			{
				throw new InvalidOperationException(SR.GetString("TextNodeAlreadyPopulated"));
			}
			this.current.text = new TraceXPathNavigator.TextNode(value);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00042A54 File Offset: 0x00041E54
		internal void AddAttribute(string name, string value, string xmlns, string prefix)
		{
			if (this.closed)
			{
				throw new InvalidOperationException(SR.GetString("CannotAddToClosedDocument"));
			}
			if (this.current == null)
			{
				throw new InvalidOperationException(SR.GetString("OperationInvalidOnAnEmptyDocument"));
			}
			TraceXPathNavigator.AttributeNode item = new TraceXPathNavigator.AttributeNode(name, prefix, xmlns, value);
			this.current.attributes.Add(item);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00042AB4 File Offset: 0x00041EB4
		internal void CloseElement()
		{
			if (this.closed)
			{
				throw new InvalidOperationException(SR.GetString("DocumentAlreadyClosed"));
			}
			this.current = this.current.parent;
			if (this.current == null)
			{
				this.closed = true;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00042B04 File Offset: 0x00041F04
		public override string BaseURI
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00042B14 File Offset: 0x00041F14
		public override XPathNavigator Clone()
		{
			return this;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00042B24 File Offset: 0x00041F24
		public override bool IsEmptyElement
		{
			get
			{
				bool result = true;
				if (this.current != null)
				{
					result = (this.current.text != null || this.current.childNodes.Count > 0);
				}
				return result;
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00042B64 File Offset: 0x00041F64
		public override bool IsSamePosition(XPathNavigator other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00042B84 File Offset: 0x00041F84
		public override string LocalName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00042BA4 File Offset: 0x00041FA4
		public override bool MoveTo(XPathNavigator other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00042BC4 File Offset: 0x00041FC4
		public override bool MoveToFirstAttribute()
		{
			if (this.current == null)
			{
				throw new InvalidOperationException(SR.GetString("OperationInvalidOnAnEmptyDocument"));
			}
			bool flag = this.current.MoveToFirstAttribute();
			if (flag)
			{
				this.state = XPathNodeType.Attribute;
			}
			return flag;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00042C04 File Offset: 0x00042004
		public override bool MoveToFirstChild()
		{
			if (this.current == null)
			{
				throw new InvalidOperationException(SR.GetString("OperationInvalidOnAnEmptyDocument"));
			}
			bool result = false;
			if (this.current.childNodes.Count > 0)
			{
				this.current = this.current.childNodes[0];
				this.state = XPathNodeType.Element;
				result = true;
			}
			else if (this.current.childNodes.Count == 0 && this.current.text != null)
			{
				this.state = XPathNodeType.Text;
				this.current.movedToText = true;
				result = true;
			}
			return result;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00042CA4 File Offset: 0x000420A4
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			return false;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00042CB4 File Offset: 0x000420B4
		public override bool MoveToId(string id)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00042CD4 File Offset: 0x000420D4
		public override bool MoveToNext()
		{
			if (this.current == null)
			{
				throw new InvalidOperationException(SR.GetString("OperationInvalidOnAnEmptyDocument"));
			}
			bool result = false;
			if (this.state != XPathNodeType.Text)
			{
				TraceXPathNavigator.ElementNode parent = this.current.parent;
				if (parent != null)
				{
					TraceXPathNavigator.ElementNode elementNode = parent.MoveToNext();
					if (elementNode == null && parent.text != null && !parent.movedToText)
					{
						this.state = XPathNodeType.Text;
						parent.movedToText = true;
						result = true;
					}
					else if (elementNode != null)
					{
						this.state = XPathNodeType.Element;
						result = true;
						this.current = elementNode;
					}
				}
			}
			return result;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00042D54 File Offset: 0x00042154
		public override bool MoveToNextAttribute()
		{
			if (this.current == null)
			{
				throw new InvalidOperationException(SR.GetString("OperationInvalidOnAnEmptyDocument"));
			}
			bool flag = this.current.MoveToNextAttribute();
			if (flag)
			{
				this.state = XPathNodeType.Attribute;
			}
			return flag;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00042D94 File Offset: 0x00042194
		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			return false;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00042DA4 File Offset: 0x000421A4
		public override bool MoveToParent()
		{
			if (this.current == null)
			{
				throw new InvalidOperationException(SR.GetString("OperationInvalidOnAnEmptyDocument"));
			}
			bool result = false;
			switch (this.state)
			{
			case XPathNodeType.Element:
				if (this.current.parent != null)
				{
					this.current = this.current.parent;
					this.state = XPathNodeType.Element;
					result = true;
				}
				break;
			case XPathNodeType.Attribute:
				this.state = XPathNodeType.Element;
				result = true;
				break;
			case XPathNodeType.Namespace:
				this.state = XPathNodeType.Element;
				result = true;
				break;
			case XPathNodeType.Text:
				this.state = XPathNodeType.Element;
				result = true;
				break;
			}
			return result;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00042E44 File Offset: 0x00042244
		public override bool MoveToPrevious()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00042E64 File Offset: 0x00042264
		public override void MoveToRoot()
		{
			this.current = this.root;
			this.state = XPathNodeType.Element;
			this.root.Reset();
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00042E94 File Offset: 0x00042294
		public override string Name
		{
			get
			{
				if (this.current == null)
				{
					throw new InvalidOperationException(SR.GetString("OperationInvalidOnAnEmptyDocument"));
				}
				string result = null;
				switch (this.state)
				{
				case XPathNodeType.Element:
					result = this.current.name;
					break;
				case XPathNodeType.Attribute:
					result = this.current.CurrentAttribute.name;
					break;
				}
				return result;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00042EF4 File Offset: 0x000422F4
		public override XmlNameTable NameTable
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00042F04 File Offset: 0x00042304
		public override string NamespaceURI
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00042F14 File Offset: 0x00042314
		public override XPathNodeType NodeType
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00042F34 File Offset: 0x00042334
		public override string Prefix
		{
			get
			{
				if (this.current == null)
				{
					throw new InvalidOperationException(SR.GetString("OperationInvalidOnAnEmptyDocument"));
				}
				string result = null;
				switch (this.state)
				{
				case XPathNodeType.Element:
					result = this.current.prefix;
					break;
				case XPathNodeType.Attribute:
					result = this.current.CurrentAttribute.prefix;
					break;
				case XPathNodeType.Namespace:
					result = this.current.prefix;
					break;
				}
				return result;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00042FB4 File Offset: 0x000423B4
		public override string Value
		{
			get
			{
				if (this.current == null)
				{
					throw new InvalidOperationException(SR.GetString("OperationInvalidOnAnEmptyDocument"));
				}
				string result = null;
				switch (this.state)
				{
				case XPathNodeType.Attribute:
					result = this.current.CurrentAttribute.nodeValue;
					break;
				case XPathNodeType.Namespace:
					result = this.current.xmlns;
					break;
				case XPathNodeType.Text:
					result = this.current.text.nodeValue;
					break;
				}
				return result;
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00043034 File Offset: 0x00042434
		public override string ToString()
		{
			this.MoveToRoot();
			StringBuilder stringBuilder = new StringBuilder();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(new StringWriter(stringBuilder, CultureInfo.CurrentCulture));
			xmlTextWriter.WriteNode(this, false);
			return stringBuilder.ToString();
		}

		// Token: 0x040002F9 RID: 761
		private TraceXPathNavigator.ElementNode root;

		// Token: 0x040002FA RID: 762
		private TraceXPathNavigator.ElementNode current;

		// Token: 0x040002FB RID: 763
		private bool closed;

		// Token: 0x040002FC RID: 764
		private XPathNodeType state = XPathNodeType.Element;

		// Token: 0x020000CA RID: 202
		private class ElementNode
		{
			// Token: 0x06000565 RID: 1381 RVA: 0x00043094 File Offset: 0x00042494
			internal ElementNode(string name, string prefix, string xmlns, TraceXPathNavigator.ElementNode parent)
			{
				this.name = name;
				this.prefix = prefix;
				this.xmlns = xmlns;
				this.parent = parent;
			}

			// Token: 0x06000566 RID: 1382 RVA: 0x000430E4 File Offset: 0x000424E4
			internal TraceXPathNavigator.ElementNode MoveToNext()
			{
				TraceXPathNavigator.ElementNode result = null;
				if (this.elementIndex + 1 < this.childNodes.Count)
				{
					this.elementIndex++;
					result = this.childNodes[this.elementIndex];
				}
				return result;
			}

			// Token: 0x06000567 RID: 1383 RVA: 0x00043134 File Offset: 0x00042534
			internal bool MoveToFirstAttribute()
			{
				this.attributeIndex = 0;
				return this.attributes.Count > 0;
			}

			// Token: 0x06000568 RID: 1384 RVA: 0x00043164 File Offset: 0x00042564
			internal bool MoveToNextAttribute()
			{
				bool result = false;
				if (this.attributeIndex + 1 < this.attributes.Count)
				{
					this.attributeIndex++;
					result = true;
				}
				return result;
			}

			// Token: 0x06000569 RID: 1385 RVA: 0x000431A4 File Offset: 0x000425A4
			internal void Reset()
			{
				this.attributeIndex = 0;
				this.elementIndex = 0;
				foreach (TraceXPathNavigator.ElementNode elementNode in this.childNodes)
				{
					elementNode.Reset();
				}
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x0600056A RID: 1386 RVA: 0x00043214 File Offset: 0x00042614
			internal TraceXPathNavigator.AttributeNode CurrentAttribute
			{
				get
				{
					return this.attributes[this.attributeIndex];
				}
			}

			// Token: 0x040002FD RID: 765
			internal string name;

			// Token: 0x040002FE RID: 766
			internal string xmlns;

			// Token: 0x040002FF RID: 767
			internal string prefix;

			// Token: 0x04000300 RID: 768
			internal List<TraceXPathNavigator.ElementNode> childNodes = new List<TraceXPathNavigator.ElementNode>();

			// Token: 0x04000301 RID: 769
			internal TraceXPathNavigator.ElementNode parent;

			// Token: 0x04000302 RID: 770
			internal List<TraceXPathNavigator.AttributeNode> attributes = new List<TraceXPathNavigator.AttributeNode>();

			// Token: 0x04000303 RID: 771
			internal TraceXPathNavigator.TextNode text;

			// Token: 0x04000304 RID: 772
			internal bool movedToText;

			// Token: 0x04000305 RID: 773
			private int attributeIndex;

			// Token: 0x04000306 RID: 774
			private int elementIndex;
		}

		// Token: 0x020000CB RID: 203
		private class AttributeNode
		{
			// Token: 0x0600056B RID: 1387 RVA: 0x00043234 File Offset: 0x00042634
			internal AttributeNode(string name, string prefix, string xmlns, string value)
			{
				this.name = name;
				this.prefix = prefix;
				this.xmlns = xmlns;
				this.nodeValue = value;
			}

			// Token: 0x04000307 RID: 775
			internal string name;

			// Token: 0x04000308 RID: 776
			internal string xmlns;

			// Token: 0x04000309 RID: 777
			internal string prefix;

			// Token: 0x0400030A RID: 778
			internal string nodeValue;
		}

		// Token: 0x020000CC RID: 204
		private class TextNode
		{
			// Token: 0x0600056C RID: 1388 RVA: 0x00043264 File Offset: 0x00042664
			internal TextNode(string value)
			{
				this.nodeValue = value;
			}

			// Token: 0x0400030B RID: 779
			internal string nodeValue;
		}
	}
}
