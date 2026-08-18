using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02000F7C RID: 3964
	public class PageLayoutElement : ElementBase
	{
		// Token: 0x060097CE RID: 38862 RVA: 0x002203C2 File Offset: 0x0021E5C2
		public PageLayoutElement()
		{
			this.PageOrientation = PageOrientationType.Portrait;
		}

		// Token: 0x17002FF5 RID: 12277
		// (get) Token: 0x060097CF RID: 38863 RVA: 0x002203D1 File Offset: 0x0021E5D1
		// (set) Token: 0x060097D0 RID: 38864 RVA: 0x002203D9 File Offset: 0x0021E5D9
		public bool IsCenteredVertical { get; set; }

		// Token: 0x17002FF6 RID: 12278
		// (get) Token: 0x060097D1 RID: 38865 RVA: 0x002203E2 File Offset: 0x0021E5E2
		// (set) Token: 0x060097D2 RID: 38866 RVA: 0x002203EA File Offset: 0x0021E5EA
		public bool IsCenteredHorizontal { get; set; }

		// Token: 0x17002FF7 RID: 12279
		// (get) Token: 0x060097D3 RID: 38867 RVA: 0x002203F3 File Offset: 0x0021E5F3
		// (set) Token: 0x060097D4 RID: 38868 RVA: 0x002203FB File Offset: 0x0021E5FB
		public PageOrientationType PageOrientation { get; set; }

		// Token: 0x17002FF8 RID: 12280
		// (get) Token: 0x060097D5 RID: 38869 RVA: 0x00220404 File Offset: 0x0021E604
		protected override string EndTag
		{
			get
			{
				return "</Layout>";
			}
		}

		// Token: 0x17002FF9 RID: 12281
		// (get) Token: 0x060097D6 RID: 38870 RVA: 0x0022040B File Offset: 0x0021E60B
		protected override string StartTag
		{
			get
			{
				return "<Layout{0}>";
			}
		}

		// Token: 0x060097D7 RID: 38871 RVA: 0x00220414 File Offset: 0x0021E614
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.PageOrientation == PageOrientationType.Landscape)
			{
				base.Attributes.Add("x:Orientation", this.PageOrientation.ToString());
			}
			if (this.IsCenteredHorizontal)
			{
				base.Attributes.Add("x:CenterHorizonal", "1");
			}
			if (this.IsCenteredVertical)
			{
				base.Attributes.Add("x:CenterVertical", "1");
			}
			base.AppendAttributes(sb);
		}
	}
}
