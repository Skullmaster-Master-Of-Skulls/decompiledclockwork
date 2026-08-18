using System;

namespace System.Xml
{
	// Token: 0x020000F0 RID: 240
	internal class XmlUnspecifiedAttribute : XmlAttribute
	{
		// Token: 0x06000EB2 RID: 3762 RVA: 0x000409E1 File Offset: 0x0003F9E1
		protected internal XmlUnspecifiedAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc) : base(prefix, localName, namespaceURI, doc)
		{
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x000409EE File Offset: 0x0003F9EE
		public override bool Specified
		{
			get
			{
				return this.fSpecified;
			}
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x000409F8 File Offset: 0x0003F9F8
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlUnspecifiedAttribute xmlUnspecifiedAttribute = (XmlUnspecifiedAttribute)ownerDocument.CreateDefaultAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			xmlUnspecifiedAttribute.CopyChildren(ownerDocument, this, true);
			xmlUnspecifiedAttribute.fSpecified = true;
			return xmlUnspecifiedAttribute;
		}

		// Token: 0x170003A3 RID: 931
		// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x00040A3B File Offset: 0x0003FA3B
		public override string InnerText
		{
			set
			{
				base.InnerText = value;
				this.fSpecified = true;
			}
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00040A4C File Offset: 0x0003FA4C
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			XmlNode result = base.InsertBefore(newChild, refChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00040A6C File Offset: 0x0003FA6C
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			XmlNode result = base.InsertAfter(newChild, refChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00040A8C File Offset: 0x0003FA8C
		public override XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			XmlNode result = base.ReplaceChild(newChild, oldChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00040AAC File Offset: 0x0003FAAC
		public override XmlNode RemoveChild(XmlNode oldChild)
		{
			XmlNode result = base.RemoveChild(oldChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00040ACC File Offset: 0x0003FACC
		public override XmlNode AppendChild(XmlNode newChild)
		{
			XmlNode result = base.AppendChild(newChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00040AE9 File Offset: 0x0003FAE9
		public override void WriteTo(XmlWriter w)
		{
			if (this.fSpecified)
			{
				base.WriteTo(w);
			}
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00040AFA File Offset: 0x0003FAFA
		internal void SetSpecified(bool f)
		{
			this.fSpecified = f;
		}

		// Token: 0x040009A8 RID: 2472
		private bool fSpecified;
	}
}
