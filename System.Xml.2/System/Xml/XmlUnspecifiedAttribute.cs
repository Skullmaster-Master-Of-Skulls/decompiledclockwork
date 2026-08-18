using System;

namespace System.Xml
{
	// Token: 0x0200011F RID: 287
	internal class XmlUnspecifiedAttribute : XmlAttribute
	{
		// Token: 0x06001447 RID: 5191 RVA: 0x00053DDD File Offset: 0x00051FDD
		protected internal XmlUnspecifiedAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc) : base(prefix, localName, namespaceURI, doc)
		{
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00053DEA File Offset: 0x00051FEA
		public override bool Specified
		{
			get
			{
				return this.fSpecified;
			}
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00053DF4 File Offset: 0x00051FF4
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlUnspecifiedAttribute xmlUnspecifiedAttribute = (XmlUnspecifiedAttribute)ownerDocument.CreateDefaultAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			xmlUnspecifiedAttribute.CopyChildren(ownerDocument, this, true);
			xmlUnspecifiedAttribute.fSpecified = true;
			return xmlUnspecifiedAttribute;
		}

		// Token: 0x17000463 RID: 1123
		// (set) Token: 0x0600144A RID: 5194 RVA: 0x00053E37 File Offset: 0x00052037
		public override string InnerText
		{
			set
			{
				base.InnerText = value;
				this.fSpecified = true;
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00053E48 File Offset: 0x00052048
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			XmlNode result = base.InsertBefore(newChild, refChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00053E68 File Offset: 0x00052068
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			XmlNode result = base.InsertAfter(newChild, refChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00053E88 File Offset: 0x00052088
		public override XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			XmlNode result = base.ReplaceChild(newChild, oldChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00053EA8 File Offset: 0x000520A8
		public override XmlNode RemoveChild(XmlNode oldChild)
		{
			XmlNode result = base.RemoveChild(oldChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x00053EC8 File Offset: 0x000520C8
		public override XmlNode AppendChild(XmlNode newChild)
		{
			XmlNode result = base.AppendChild(newChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00053EE5 File Offset: 0x000520E5
		public override void WriteTo(XmlWriter w)
		{
			if (this.fSpecified)
			{
				base.WriteTo(w);
			}
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00053EF6 File Offset: 0x000520F6
		internal void SetSpecified(bool f)
		{
			this.fSpecified = f;
		}

		// Token: 0x04000588 RID: 1416
		private bool fSpecified;
	}
}
