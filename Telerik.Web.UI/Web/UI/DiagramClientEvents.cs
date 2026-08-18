using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000226 RID: 550
	public class DiagramClientEvents : StateManager, IDefaultCheck
	{
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x0004621E File Offset: 0x0004441E
		// (set) Token: 0x0600140B RID: 5131 RVA: 0x0004623E File Offset: 0x0004443E
		[ClientControlEvent]
		[ClientPropertyName("load")]
		[DefaultValue("")]
		public string OnLoad
		{
			get
			{
				return (string)(base.ViewState["OnLoad"] ?? "");
			}
			set
			{
				base.ViewState["OnLoad"] = value;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00046251 File Offset: 0x00044451
		// (set) Token: 0x0600140D RID: 5133 RVA: 0x00046271 File Offset: 0x00044471
		[DefaultValue("")]
		public string OnAdd
		{
			get
			{
				return (string)(base.ViewState["OnAdd"] ?? "");
			}
			set
			{
				base.ViewState["OnAdd"] = value;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00046284 File Offset: 0x00044484
		// (set) Token: 0x0600140F RID: 5135 RVA: 0x000462A4 File Offset: 0x000444A4
		[DefaultValue("")]
		public string OnCancel
		{
			get
			{
				return (string)(base.ViewState["OnCancel"] ?? "");
			}
			set
			{
				base.ViewState["OnCancel"] = value;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x000462B7 File Offset: 0x000444B7
		// (set) Token: 0x06001411 RID: 5137 RVA: 0x000462D7 File Offset: 0x000444D7
		[DefaultValue("")]
		public string OnChange
		{
			get
			{
				return (string)(base.ViewState["OnChange"] ?? "");
			}
			set
			{
				base.ViewState["OnChange"] = value;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x000462EA File Offset: 0x000444EA
		// (set) Token: 0x06001413 RID: 5139 RVA: 0x0004630A File Offset: 0x0004450A
		[DefaultValue("")]
		public string OnClick
		{
			get
			{
				return (string)(base.ViewState["OnClick"] ?? "");
			}
			set
			{
				base.ViewState["OnClick"] = value;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0004631D File Offset: 0x0004451D
		// (set) Token: 0x06001415 RID: 5141 RVA: 0x0004633D File Offset: 0x0004453D
		[DefaultValue("")]
		public string OnDataBound
		{
			get
			{
				return (string)(base.ViewState["OnDataBound"] ?? "");
			}
			set
			{
				base.ViewState["OnDataBound"] = value;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00046350 File Offset: 0x00044550
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x00046370 File Offset: 0x00044570
		[DefaultValue("")]
		public string OnDrag
		{
			get
			{
				return (string)(base.ViewState["OnDrag"] ?? "");
			}
			set
			{
				base.ViewState["OnDrag"] = value;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x00046383 File Offset: 0x00044583
		// (set) Token: 0x06001419 RID: 5145 RVA: 0x000463A3 File Offset: 0x000445A3
		[DefaultValue("")]
		public string OnDragEnd
		{
			get
			{
				return (string)(base.ViewState["OnDragEnd"] ?? "");
			}
			set
			{
				base.ViewState["OnDragEnd"] = value;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x000463B6 File Offset: 0x000445B6
		// (set) Token: 0x0600141B RID: 5147 RVA: 0x000463D6 File Offset: 0x000445D6
		[DefaultValue("")]
		public string OnDragStart
		{
			get
			{
				return (string)(base.ViewState["OnDragStart"] ?? "");
			}
			set
			{
				base.ViewState["OnDragStart"] = value;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x000463E9 File Offset: 0x000445E9
		// (set) Token: 0x0600141D RID: 5149 RVA: 0x00046409 File Offset: 0x00044609
		[DefaultValue("")]
		public string OnEdit
		{
			get
			{
				return (string)(base.ViewState["OnEdit"] ?? "");
			}
			set
			{
				base.ViewState["OnEdit"] = value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x0004641C File Offset: 0x0004461C
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x0004643C File Offset: 0x0004463C
		[DefaultValue("")]
		public string OnItemBoundsChange
		{
			get
			{
				return (string)(base.ViewState["OnItemBoundsChange"] ?? "");
			}
			set
			{
				base.ViewState["OnItemBoundsChange"] = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x0004644F File Offset: 0x0004464F
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x0004646F File Offset: 0x0004466F
		[DefaultValue("")]
		public string OnItemRotate
		{
			get
			{
				return (string)(base.ViewState["OnItemRotate"] ?? "");
			}
			set
			{
				base.ViewState["OnItemRotate"] = value;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00046482 File Offset: 0x00044682
		// (set) Token: 0x06001423 RID: 5155 RVA: 0x000464A2 File Offset: 0x000446A2
		[DefaultValue("")]
		public string OnMouseEnter
		{
			get
			{
				return (string)(base.ViewState["OnMouseEnter"] ?? "");
			}
			set
			{
				base.ViewState["OnMouseEnter"] = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x000464B5 File Offset: 0x000446B5
		// (set) Token: 0x06001425 RID: 5157 RVA: 0x000464D5 File Offset: 0x000446D5
		[DefaultValue("")]
		public string OnMouseLeave
		{
			get
			{
				return (string)(base.ViewState["OnMouseLeave"] ?? "");
			}
			set
			{
				base.ViewState["OnMouseLeave"] = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x000464E8 File Offset: 0x000446E8
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x00046508 File Offset: 0x00044708
		[DefaultValue("")]
		public string OnPan
		{
			get
			{
				return (string)(base.ViewState["OnPan"] ?? "");
			}
			set
			{
				base.ViewState["OnPan"] = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0004651B File Offset: 0x0004471B
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x0004653B File Offset: 0x0004473B
		[DefaultValue("")]
		public string OnRemove
		{
			get
			{
				return (string)(base.ViewState["OnRemove"] ?? "");
			}
			set
			{
				base.ViewState["OnRemove"] = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0004654E File Offset: 0x0004474E
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x0004656E File Offset: 0x0004476E
		[DefaultValue("")]
		public string OnSave
		{
			get
			{
				return (string)(base.ViewState["OnSave"] ?? "");
			}
			set
			{
				base.ViewState["OnSave"] = value;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00046581 File Offset: 0x00044781
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x000465A1 File Offset: 0x000447A1
		[DefaultValue("")]
		public string OnSelect
		{
			get
			{
				return (string)(base.ViewState["OnSelect"] ?? "");
			}
			set
			{
				base.ViewState["OnSelect"] = value;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x000465B4 File Offset: 0x000447B4
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x000465D4 File Offset: 0x000447D4
		[DefaultValue("")]
		public string OnToolBarClick
		{
			get
			{
				return (string)(base.ViewState["OnToolBarClick"] ?? "");
			}
			set
			{
				base.ViewState["OnToolBarClick"] = value;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x000465E7 File Offset: 0x000447E7
		// (set) Token: 0x06001431 RID: 5169 RVA: 0x00046607 File Offset: 0x00044807
		[DefaultValue("")]
		public string OnZoomEnd
		{
			get
			{
				return (string)(base.ViewState["OnZoomEnd"] ?? "");
			}
			set
			{
				base.ViewState["OnZoomEnd"] = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0004661A File Offset: 0x0004481A
		// (set) Token: 0x06001433 RID: 5171 RVA: 0x0004663A File Offset: 0x0004483A
		[DefaultValue("")]
		public string OnZoomStart
		{
			get
			{
				return (string)(base.ViewState["OnZoomStart"] ?? "");
			}
			set
			{
				base.ViewState["OnZoomStart"] = value;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x00046650 File Offset: 0x00044850
		public bool IsDefault
		{
			get
			{
				return this.OnAdd == "" && this.OnCancel == "" && this.OnChange == "" && this.OnClick == "" && this.OnDataBound == "" && this.OnDrag == "" && this.OnDragEnd == "" && this.OnDragStart == "" && this.OnEdit == "" && this.OnItemBoundsChange == "" && this.OnItemRotate == "" && this.OnMouseEnter == "" && this.OnMouseLeave == "" && this.OnPan == "" && this.OnRemove == "" && this.OnSave == "" && this.OnSelect == "" && this.OnToolBarClick == "" && this.OnZoomEnd == "" && this.OnZoomStart == "";
			}
		}
	}
}
