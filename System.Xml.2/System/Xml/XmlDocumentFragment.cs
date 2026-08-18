using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000103 RID: 259
	public class XmlDocumentFragment : XmlNode
	{
		// Token: 0x06001238 RID: 4664 RVA: 0x0004BEBE File Offset: 0x0004A0BE
		protected internal XmlDocumentFragment(XmlDocument ownerDocument)
		{
			if (ownerDocument == null)
			{
				throw new ArgumentException(Res.GetString("Xdom_Node_Null_Doc"));
			}
			this.parentNode = ownerDocument;
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0004BEE0 File Offset: 0x0004A0E0
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strDocumentFragmentName;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0004BEED File Offset: 0x0004A0ED
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strDocumentFragmentName;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x0004BEFA File Offset: 0x0004A0FA
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentFragment;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x0004BEFE File Offset: 0x0004A0FE
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0004BF01 File Offset: 0x0004A101
		public override XmlDocument OwnerDocument
		{
			get
			{
				return (XmlDocument)this.parentNode;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x0004BF0E File Offset: 0x0004A10E
		// (set) Token: 0x0600123F RID: 4671 RVA: 0x0004BF18 File Offset: 0x0004A118
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				this.RemoveAll();
				XmlLoader xmlLoader = new XmlLoader();
				xmlLoader.ParsePartialContent(this, value, XmlNodeType.Element);
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0004BF3C File Offset: 0x0004A13C
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlDocumentFragment xmlDocumentFragment = ownerDocument.CreateDocumentFragment();
			if (deep)
			{
				xmlDocumentFragment.CopyChildren(ownerDocument, this, deep);
			}
			return xmlDocumentFragment;
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x0004BF64 File Offset: 0x0004A164
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0004BF67 File Offset: 0x0004A167
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x0004BF6F File Offset: 0x0004A16F
		internal override XmlLinkedNode LastNode
		{
			get
			{
				return this.lastChild;
			}
			set
			{
				this.lastChild = value;
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0004BF78 File Offset: 0x0004A178
		internal override bool IsValidChildType(XmlNodeType type)
		{
			switch (type)
			{
			case XmlNodeType.Element:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.EntityReference:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				return true;
			case XmlNodeType.XmlDeclaration:
			{
				XmlNode firstChild = this.FirstChild;
				return firstChild == null || firstChild.NodeType != XmlNodeType.XmlDeclaration;
			}
			}
			return false;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0004BFEE File Offset: 0x0004A1EE
		internal override bool CanInsertAfter(XmlNode newChild, XmlNode refChild)
		{
			return newChild.NodeType != XmlNodeType.XmlDeclaration || (refChild == null && this.LastNode == null);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0004C00A File Offset: 0x0004A20A
		internal override bool CanInsertBefore(XmlNode newChild, XmlNode refChild)
		{
			return newChild.NodeType != XmlNodeType.XmlDeclaration || refChild == null || refChild == this.FirstChild;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0004C026 File Offset: 0x0004A226
		public override void WriteTo(XmlWriter w)
		{
			this.WriteContentTo(w);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0004C030 File Offset: 0x0004A230
		public override void WriteContentTo(XmlWriter w)
		{
			foreach (object obj in this)
			{
				XmlNode xmlNode = (XmlNode)obj;
				xmlNode.WriteTo(w);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x0004C084 File Offset: 0x0004A284
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Root;
			}
		}

		// Token: 0x04000508 RID: 1288
		private XmlLinkedNode lastChild;
	}
}
