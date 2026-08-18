using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000F2 RID: 242
	internal abstract class DocumentXPathNodeIterator_ElemDescendants : XPathNodeIterator
	{
		// Token: 0x0600110A RID: 4362 RVA: 0x00048666 File Offset: 0x00046866
		internal DocumentXPathNodeIterator_ElemDescendants(DocumentXPathNavigator nav)
		{
			this.nav = (DocumentXPathNavigator)nav.Clone();
			this.level = 0;
			this.position = 0;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0004868D File Offset: 0x0004688D
		internal DocumentXPathNodeIterator_ElemDescendants(DocumentXPathNodeIterator_ElemDescendants other)
		{
			this.nav = (DocumentXPathNavigator)other.nav.Clone();
			this.level = other.level;
			this.position = other.position;
		}

		// Token: 0x0600110C RID: 4364
		protected abstract bool Match(XmlNode node);

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x000486C3 File Offset: 0x000468C3
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x000486CB File Offset: 0x000468CB
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x000486D3 File Offset: 0x000468D3
		protected void SetPosition(int pos)
		{
			this.position = pos;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x000486DC File Offset: 0x000468DC
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

		// Token: 0x040004C3 RID: 1219
		private DocumentXPathNavigator nav;

		// Token: 0x040004C4 RID: 1220
		private int level;

		// Token: 0x040004C5 RID: 1221
		private int position;
	}
}
