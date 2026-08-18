using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000C1 RID: 193
	internal abstract class DocumentXPathNodeIterator_ElemDescendants : XPathNodeIterator
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x00034E3F File Offset: 0x00033E3F
		internal DocumentXPathNodeIterator_ElemDescendants(DocumentXPathNavigator nav)
		{
			this.nav = (DocumentXPathNavigator)nav.Clone();
			this.level = 0;
			this.position = 0;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00034E66 File Offset: 0x00033E66
		internal DocumentXPathNodeIterator_ElemDescendants(DocumentXPathNodeIterator_ElemDescendants other)
		{
			this.nav = (DocumentXPathNavigator)other.nav.Clone();
			this.level = other.level;
			this.position = other.position;
		}

		// Token: 0x06000B6E RID: 2926
		protected abstract bool Match(XmlNode node);

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x00034E9C File Offset: 0x00033E9C
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00034EA4 File Offset: 0x00033EA4
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00034EAC File Offset: 0x00033EAC
		protected void SetPosition(int pos)
		{
			this.position = pos;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00034EB8 File Offset: 0x00033EB8
		public override bool MoveNext()
		{
			for (;;)
			{
				if (this.nav.MoveToFirstChild())
				{
					this.level++;
				}
				else
				{
					if (this.level == 0)
					{
						break;
					}
					while (!this.nav.MoveToNext())
					{
						this.level--;
						if (this.level == 0)
						{
							return false;
						}
						if (!this.nav.MoveToParent())
						{
							return false;
						}
					}
				}
				XmlNode xmlNode = (XmlNode)this.nav.UnderlyingObject;
				if (xmlNode.NodeType == XmlNodeType.Element && this.Match(xmlNode))
				{
					goto Block_5;
				}
			}
			return false;
			Block_5:
			this.position++;
			return true;
		}

		// Token: 0x040008E0 RID: 2272
		private DocumentXPathNavigator nav;

		// Token: 0x040008E1 RID: 2273
		private int level;

		// Token: 0x040008E2 RID: 2274
		private int position;
	}
}
