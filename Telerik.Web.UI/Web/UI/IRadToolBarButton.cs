using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200093E RID: 2366
	public interface IRadToolBarButton
	{
		// Token: 0x17001DB6 RID: 7606
		// (get) Token: 0x06005A1A RID: 23066
		// (set) Token: 0x06005A1B RID: 23067
		bool PostBack { get; set; }

		// Token: 0x17001DB7 RID: 7607
		// (get) Token: 0x06005A1C RID: 23068
		// (set) Token: 0x06005A1D RID: 23069
		string Value { get; set; }

		// Token: 0x17001DB8 RID: 7608
		// (get) Token: 0x06005A1E RID: 23070
		// (set) Token: 0x06005A1F RID: 23071
		string NavigateUrl { get; set; }

		// Token: 0x17001DB9 RID: 7609
		// (get) Token: 0x06005A20 RID: 23072
		// (set) Token: 0x06005A21 RID: 23073
		string Target { get; set; }

		// Token: 0x17001DBA RID: 7610
		// (get) Token: 0x06005A22 RID: 23074
		// (set) Token: 0x06005A23 RID: 23075
		ITemplate ItemTemplate { get; set; }

		// Token: 0x17001DBB RID: 7611
		// (get) Token: 0x06005A24 RID: 23076
		// (set) Token: 0x06005A25 RID: 23077
		string CommandName { get; set; }

		// Token: 0x17001DBC RID: 7612
		// (get) Token: 0x06005A26 RID: 23078
		// (set) Token: 0x06005A27 RID: 23079
		string CommandArgument { get; set; }

		// Token: 0x17001DBD RID: 7613
		// (get) Token: 0x06005A28 RID: 23080
		// (set) Token: 0x06005A29 RID: 23081
		bool CausesValidation { get; set; }

		// Token: 0x17001DBE RID: 7614
		// (get) Token: 0x06005A2A RID: 23082
		// (set) Token: 0x06005A2B RID: 23083
		string PostBackUrl { get; set; }

		// Token: 0x17001DBF RID: 7615
		// (get) Token: 0x06005A2C RID: 23084
		// (set) Token: 0x06005A2D RID: 23085
		string ValidationGroup { get; set; }

		// Token: 0x17001DC0 RID: 7616
		// (get) Token: 0x06005A2E RID: 23086
		RadToolBar ToolBar { get; }

		// Token: 0x17001DC1 RID: 7617
		// (get) Token: 0x06005A2F RID: 23087
		// (set) Token: 0x06005A30 RID: 23088
		string Text { get; set; }

		// Token: 0x17001DC2 RID: 7618
		// (get) Token: 0x06005A31 RID: 23089
		// (set) Token: 0x06005A32 RID: 23090
		string ImageUrl { get; set; }

		// Token: 0x17001DC3 RID: 7619
		// (get) Token: 0x06005A33 RID: 23091
		// (set) Token: 0x06005A34 RID: 23092
		string HoveredImageUrl { get; set; }

		// Token: 0x17001DC4 RID: 7620
		// (get) Token: 0x06005A35 RID: 23093
		// (set) Token: 0x06005A36 RID: 23094
		string HoveredCssClass { get; set; }

		// Token: 0x17001DC5 RID: 7621
		// (get) Token: 0x06005A37 RID: 23095
		// (set) Token: 0x06005A38 RID: 23096
		string ClickedCssClass { get; set; }

		// Token: 0x17001DC6 RID: 7622
		// (get) Token: 0x06005A39 RID: 23097
		// (set) Token: 0x06005A3A RID: 23098
		string ClickedImageUrl { get; set; }

		// Token: 0x17001DC7 RID: 7623
		// (get) Token: 0x06005A3B RID: 23099
		// (set) Token: 0x06005A3C RID: 23100
		string DisabledImageUrl { get; set; }

		// Token: 0x17001DC8 RID: 7624
		// (get) Token: 0x06005A3D RID: 23101
		// (set) Token: 0x06005A3E RID: 23102
		string DisabledCssClass { get; set; }

		// Token: 0x17001DC9 RID: 7625
		// (get) Token: 0x06005A3F RID: 23103
		// (set) Token: 0x06005A40 RID: 23104
		string FocusedCssClass { get; set; }

		// Token: 0x17001DCA RID: 7626
		// (get) Token: 0x06005A41 RID: 23105
		// (set) Token: 0x06005A42 RID: 23106
		string FocusedImageUrl { get; set; }

		// Token: 0x17001DCB RID: 7627
		// (get) Token: 0x06005A43 RID: 23107
		// (set) Token: 0x06005A44 RID: 23108
		ToolBarImagePosition ImagePosition { get; set; }
	}
}
