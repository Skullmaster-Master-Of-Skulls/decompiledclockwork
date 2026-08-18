using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000671 RID: 1649
	public class PivotGridCell : PivotGridTableCell, INamingContainer
	{
		// Token: 0x06003C3E RID: 15422 RVA: 0x000C39E9 File Offset: 0x000C1BE9
		public PivotGridCell(RadPivotGrid ownerGrid)
		{
			this.OwnerPivotGrid = ownerGrid;
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x06003C3F RID: 15423 RVA: 0x000C39F8 File Offset: 0x000C1BF8
		// (set) Token: 0x06003C40 RID: 15424 RVA: 0x000C3A00 File Offset: 0x000C1C00
		public RadPivotGrid OwnerPivotGrid { get; set; }

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x06003C41 RID: 15425 RVA: 0x000C3A09 File Offset: 0x000C1C09
		public virtual bool CanExpand
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06003C42 RID: 15426 RVA: 0x000C3A10 File Offset: 0x000C1C10
		// (set) Token: 0x06003C43 RID: 15427 RVA: 0x000C3A18 File Offset: 0x000C1C18
		public bool HasInstantiatedTemplate { get; set; }

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06003C44 RID: 15428 RVA: 0x000C3A21 File Offset: 0x000C1C21
		// (set) Token: 0x06003C45 RID: 15429 RVA: 0x000C3A29 File Offset: 0x000C1C29
		public bool HasChildren { get; set; }

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06003C46 RID: 15430 RVA: 0x000C3A32 File Offset: 0x000C1C32
		// (set) Token: 0x06003C47 RID: 15431 RVA: 0x000C3A3A File Offset: 0x000C1C3A
		public object DataItem { get; set; }

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x06003C48 RID: 15432 RVA: 0x000C3A43 File Offset: 0x000C1C43
		// (set) Token: 0x06003C49 RID: 15433 RVA: 0x000C3A4B File Offset: 0x000C1C4B
		public PivotGridField Field { get; set; }

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x06003C4A RID: 15434 RVA: 0x000C3A54 File Offset: 0x000C1C54
		// (set) Token: 0x06003C4B RID: 15435 RVA: 0x000C3A5C File Offset: 0x000C1C5C
		internal virtual TemplateType TemplateType { get; set; }

		// Token: 0x06003C4C RID: 15436 RVA: 0x000C3A68 File Offset: 0x000C1C68
		protected virtual void AddStyleAttributes(HtmlTextWriter writer)
		{
			if (base.ControlStyle is TableItemStyle && (base.ControlStyle as TableItemStyle).HorizontalAlign != HorizontalAlign.NotSet)
			{
				base.Style["text-align"] = (base.ControlStyle as TableItemStyle).HorizontalAlign.ToString().ToLower();
				(base.ControlStyle as TableItemStyle).HorizontalAlign = HorizontalAlign.NotSet;
			}
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x000C3AD4 File Offset: 0x000C1CD4
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.AddStyleAttributes(writer);
			base.AddAttributesToRender(writer);
		}
	}
}
