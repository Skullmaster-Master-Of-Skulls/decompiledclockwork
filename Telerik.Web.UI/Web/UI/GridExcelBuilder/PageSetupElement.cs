using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02000F7E RID: 3966
	public class PageSetupElement : ElementBase
	{
		// Token: 0x17003000 RID: 12288
		// (get) Token: 0x060097E4 RID: 38884 RVA: 0x00220670 File Offset: 0x0021E870
		public PageMarginsElement PageMarginsElement
		{
			get
			{
				PageMarginsElement result;
				if ((result = this._pageMarginsElement) == null)
				{
					result = (this._pageMarginsElement = new PageMarginsElement());
				}
				return result;
			}
		}

		// Token: 0x17003001 RID: 12289
		// (get) Token: 0x060097E5 RID: 38885 RVA: 0x00220698 File Offset: 0x0021E898
		public PageLayoutElement PageLayoutElement
		{
			get
			{
				PageLayoutElement result;
				if ((result = this._pageLayoutElement) == null)
				{
					result = (this._pageLayoutElement = new PageLayoutElement());
				}
				return result;
			}
		}

		// Token: 0x17003002 RID: 12290
		// (get) Token: 0x060097E6 RID: 38886 RVA: 0x002206C0 File Offset: 0x0021E8C0
		public PageFooterElement PageFooterElement
		{
			get
			{
				PageFooterElement result;
				if ((result = this._pageFooterElement) == null)
				{
					result = (this._pageFooterElement = new PageFooterElement());
				}
				return result;
			}
		}

		// Token: 0x17003003 RID: 12291
		// (get) Token: 0x060097E7 RID: 38887 RVA: 0x002206E8 File Offset: 0x0021E8E8
		public PageHeaderElement PageHeaderElement
		{
			get
			{
				PageHeaderElement result;
				if ((result = this._pageHeaderElement) == null)
				{
					result = (this._pageHeaderElement = new PageHeaderElement());
				}
				return result;
			}
		}

		// Token: 0x17003004 RID: 12292
		// (get) Token: 0x060097E8 RID: 38888 RVA: 0x0022070D File Offset: 0x0021E90D
		protected override string EndTag
		{
			get
			{
				return "</PageSetup>";
			}
		}

		// Token: 0x17003005 RID: 12293
		// (get) Token: 0x060097E9 RID: 38889 RVA: 0x00220714 File Offset: 0x0021E914
		protected override string StartTag
		{
			get
			{
				return "<PageSetup>";
			}
		}

		// Token: 0x060097EA RID: 38890 RVA: 0x0022071C File Offset: 0x0021E91C
		protected override void RenderChildElements(StringBuilder sb)
		{
			if (this._pageHeaderElement != null)
			{
				this.PageHeaderElement.Render(sb);
			}
			if (this._pageFooterElement != null)
			{
				this.PageFooterElement.Render(sb);
			}
			if (this._pageLayoutElement != null)
			{
				this.PageLayoutElement.Render(sb);
			}
			if (this._pageMarginsElement != null)
			{
				this.PageMarginsElement.Render(sb);
			}
			base.RenderChildElements(sb);
		}

		// Token: 0x04002B66 RID: 11110
		private PageFooterElement _pageFooterElement;

		// Token: 0x04002B67 RID: 11111
		private PageHeaderElement _pageHeaderElement;

		// Token: 0x04002B68 RID: 11112
		private PageLayoutElement _pageLayoutElement;

		// Token: 0x04002B69 RID: 11113
		private PageMarginsElement _pageMarginsElement;
	}
}
