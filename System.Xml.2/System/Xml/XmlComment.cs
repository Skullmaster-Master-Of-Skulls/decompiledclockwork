using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000100 RID: 256
	public class XmlComment : XmlCharacterData
	{
		// Token: 0x06001199 RID: 4505 RVA: 0x00049EF8 File Offset: 0x000480F8
		protected internal XmlComment(string comment, XmlDocument doc) : base(comment, doc)
		{
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00049F02 File Offset: 0x00048102
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strCommentName;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x00049F0F File Offset: 0x0004810F
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strCommentName;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x00049F1C File Offset: 0x0004811C
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Comment;
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00049F1F File Offset: 0x0004811F
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateComment(this.Data);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00049F32 File Offset: 0x00048132
		public override void WriteTo(XmlWriter w)
		{
			w.WriteComment(this.Data);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00049F40 File Offset: 0x00048140
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00049F42 File Offset: 0x00048142
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Comment;
			}
		}
	}
}
