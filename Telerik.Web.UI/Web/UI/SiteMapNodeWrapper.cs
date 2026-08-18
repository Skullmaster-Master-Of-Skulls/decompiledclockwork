using System;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x02001234 RID: 4660
	internal class SiteMapNodeWrapper
	{
		// Token: 0x17003E06 RID: 15878
		// (get) Token: 0x0600C02E RID: 49198 RVA: 0x002AA9D4 File Offset: 0x002A8BD4
		// (set) Token: 0x0600C02F RID: 49199 RVA: 0x002AA9DC File Offset: 0x002A8BDC
		public SiteMapNode Node { get; set; }

		// Token: 0x17003E07 RID: 15879
		// (get) Token: 0x0600C030 RID: 49200 RVA: 0x002AA9E5 File Offset: 0x002A8BE5
		public SiteMapNode ParentNode
		{
			get
			{
				return this.Node.ParentNode;
			}
		}

		// Token: 0x17003E08 RID: 15880
		// (get) Token: 0x0600C031 RID: 49201 RVA: 0x002AA9F2 File Offset: 0x002A8BF2
		public string Description
		{
			get
			{
				return this.Node.Description;
			}
		}

		// Token: 0x17003E09 RID: 15881
		// (get) Token: 0x0600C032 RID: 49202 RVA: 0x002AA9FF File Offset: 0x002A8BFF
		public bool HasChildNodes
		{
			get
			{
				return this.Node.HasChildNodes;
			}
		}

		// Token: 0x17003E0A RID: 15882
		// (get) Token: 0x0600C033 RID: 49203 RVA: 0x002AAA0C File Offset: 0x002A8C0C
		public string Key
		{
			get
			{
				return this.Node.Key;
			}
		}

		// Token: 0x17003E0B RID: 15883
		// (get) Token: 0x0600C034 RID: 49204 RVA: 0x002AAA19 File Offset: 0x002A8C19
		public SiteMapNode NextSibling
		{
			get
			{
				return this.Node.NextSibling;
			}
		}

		// Token: 0x17003E0C RID: 15884
		// (get) Token: 0x0600C035 RID: 49205 RVA: 0x002AAA26 File Offset: 0x002A8C26
		public SiteMapNode PreviousSibling
		{
			get
			{
				return this.Node.PreviousSibling;
			}
		}

		// Token: 0x17003E0D RID: 15885
		// (get) Token: 0x0600C036 RID: 49206 RVA: 0x002AAA33 File Offset: 0x002A8C33
		public SiteMapNode RootNode
		{
			get
			{
				return this.Node.RootNode;
			}
		}

		// Token: 0x17003E0E RID: 15886
		// (get) Token: 0x0600C037 RID: 49207 RVA: 0x002AAA40 File Offset: 0x002A8C40
		public string Title
		{
			get
			{
				return this.Node.Title;
			}
		}

		// Token: 0x17003E0F RID: 15887
		// (get) Token: 0x0600C038 RID: 49208 RVA: 0x002AAA4D File Offset: 0x002A8C4D
		public string Url
		{
			get
			{
				return this.Node.Url;
			}
		}

		// Token: 0x0600C039 RID: 49209 RVA: 0x002AAA5A File Offset: 0x002A8C5A
		public SiteMapNodeWrapper(SiteMapNode node)
		{
			this.Node = node;
		}
	}
}
