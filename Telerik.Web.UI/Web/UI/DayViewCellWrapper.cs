using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020012CF RID: 4815
	internal class DayViewCellWrapper : WebControl
	{
		// Token: 0x1700416D RID: 16749
		// (get) Token: 0x0600CA60 RID: 51808 RVA: 0x002D2A67 File Offset: 0x002D0C67
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x1700416E RID: 16750
		// (get) Token: 0x0600CA61 RID: 51809 RVA: 0x002D2A6C File Offset: 0x002D0C6C
		// (set) Token: 0x0600CA62 RID: 51810 RVA: 0x002D2A95 File Offset: 0x002D0C95
		public int ZIndex
		{
			get
			{
				string text = base.Style["z-index"];
				if (text != null)
				{
					return int.Parse(text);
				}
				return 0;
			}
			set
			{
				base.Style["z-index"] = value.ToString();
			}
		}

		// Token: 0x1700416F RID: 16751
		// (get) Token: 0x0600CA63 RID: 51811 RVA: 0x002D2AAE File Offset: 0x002D0CAE
		// (set) Token: 0x0600CA64 RID: 51812 RVA: 0x002D2AB6 File Offset: 0x002D0CB6
		public bool RenderEmptySpace { get; set; }

		// Token: 0x0600CA65 RID: 51813 RVA: 0x002D2ABF File Offset: 0x002D0CBF
		public DayViewCellWrapper(int zIndex) : this(zIndex, true)
		{
		}

		// Token: 0x0600CA66 RID: 51814 RVA: 0x002D2AC9 File Offset: 0x002D0CC9
		public DayViewCellWrapper(int zIndex, bool renderEmptySpace)
		{
			this.CssClass = "rsWrap";
			this.ZIndex = zIndex;
			this.RenderEmptySpace = renderEmptySpace;
		}

		// Token: 0x0600CA67 RID: 51815 RVA: 0x002D2AEA File Offset: 0x002D0CEA
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.HasControls())
			{
				base.Render(writer);
				return;
			}
			if (this.RenderEmptySpace)
			{
				writer.Write("&nbsp;");
			}
		}
	}
}
