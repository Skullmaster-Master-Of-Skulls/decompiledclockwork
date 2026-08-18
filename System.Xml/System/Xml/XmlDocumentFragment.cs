using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000D6 RID: 214
	public class XmlDocumentFragment : XmlNode
	{
		// Token: 0x06000D06 RID: 3334 RVA: 0x0003A086 File Offset: 0x00039086
		protected internal XmlDocumentFragment(XmlDocument ownerDocument)
		{
			if (ownerDocument == null)
			{
				throw new ArgumentException(Res.GetString("Xdom_Node_Null_Doc"));
			}
			this.parentNode = ownerDocument;
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0003A0A8 File Offset: 0x000390A8
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strDocumentFragmentName;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0003A0B5 File Offset: 0x000390B5
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strDocumentFragmentName;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0003A0C2 File Offset: 0x000390C2
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentFragment;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0003A0C6 File Offset: 0x000390C6
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0003A0C9 File Offset: 0x000390C9
		public override XmlDocument OwnerDocument
		{
			get
			{
				return (XmlDocument)this.parentNode;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0003A0D6 File Offset: 0x000390D6
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x0003A0E0 File Offset: 0x000390E0
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

		// Token: 0x06000D0E RID: 3342 RVA: 0x0003A104 File Offset: 0x00039104
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

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0003A12C File Offset: 0x0003912C
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0003A12F File Offset: 0x0003912F
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x0003A137 File Offset: 0x00039137
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

		// Token: 0x06000D12 RID: 3346 RVA: 0x0003A140 File Offset: 0x00039140
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

		// Token: 0x06000D13 RID: 3347 RVA: 0x0003A1B8 File Offset: 0x000391B8
		internal override bool CanInsertAfter(XmlNode newChild, XmlNode refChild)
		{
			return newChild.NodeType != XmlNodeType.XmlDeclaration || (refChild == null && this.LastNode == null);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0003A1D4 File Offset: 0x000391D4
		internal override bool CanInsertBefore(XmlNode newChild, XmlNode refChild)
		{
			return newChild.NodeType != XmlNodeType.XmlDeclaration || refChild == null || refChild == this.FirstChild;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003A1F0 File Offset: 0x000391F0
		public override void WriteTo(XmlWriter w)
		{
			this.WriteContentTo(w);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0003A1FC File Offset: 0x000391FC
		public override void WriteContentTo(XmlWriter w)
		{
			foreach (object obj in this)
			{
				XmlNode xmlNode = (XmlNode)obj;
				xmlNode.WriteTo(w);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0003A250 File Offset: 0x00039250
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Root;
			}
		}

		// Token: 0x04000929 RID: 2345
		private XmlLinkedNode lastChild;
	}
}
