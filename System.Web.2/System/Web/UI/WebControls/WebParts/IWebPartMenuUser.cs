using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000549 RID: 1353
	internal interface IWebPartMenuUser
	{
		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x060044E0 RID: 17632
		Style CheckImageStyle { get; }

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x060044E1 RID: 17633
		string CheckImageUrl { get; }

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x060044E2 RID: 17634
		string ClientID { get; }

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x060044E3 RID: 17635
		Style ItemHoverStyle { get; }

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x060044E4 RID: 17636
		Style ItemStyle { get; }

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x060044E5 RID: 17637
		Style LabelHoverStyle { get; }

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x060044E6 RID: 17638
		string LabelImageUrl { get; }

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x060044E7 RID: 17639
		Style LabelStyle { get; }

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x060044E8 RID: 17640
		string LabelText { get; }

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x060044E9 RID: 17641
		WebPartMenuStyle MenuPopupStyle { get; }

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x060044EA RID: 17642
		Page Page { get; }

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x060044EB RID: 17643
		string PopupImageUrl { get; }

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x060044EC RID: 17644
		string PostBackTarget { get; }

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x060044ED RID: 17645
		IUrlResolutionService UrlResolver { get; }

		// Token: 0x060044EE RID: 17646
		void OnBeginRender(HtmlTextWriter writer);

		// Token: 0x060044EF RID: 17647
		void OnEndRender(HtmlTextWriter writer);
	}
}
