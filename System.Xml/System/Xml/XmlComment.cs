using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000D3 RID: 211
	public class XmlComment : XmlCharacterData
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x000380C8 File Offset: 0x000370C8
		protected internal XmlComment(string comment, XmlDocument doc) : base(comment, doc)
		{
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x000380D2 File Offset: 0x000370D2
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strCommentName;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x000380DF File Offset: 0x000370DF
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strCommentName;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x000380EC File Offset: 0x000370EC
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Comment;
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x000380EF File Offset: 0x000370EF
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateComment(this.Data);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00038102 File Offset: 0x00037102
		public override void WriteTo(XmlWriter w)
		{
			w.WriteComment(this.Data);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00038110 File Offset: 0x00037110
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00038112 File Offset: 0x00037112
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Comment;
			}
		}
	}
}
