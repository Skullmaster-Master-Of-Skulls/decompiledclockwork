using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000630 RID: 1584
	[ToolboxItem(false)]
	public class OrgChartGroupItemCollectionRendererBase : WebControl, IOrgChartFieldsRenderer
	{
		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x0600398B RID: 14731 RVA: 0x000BD1AC File Offset: 0x000BB3AC
		// (set) Token: 0x0600398C RID: 14732 RVA: 0x000BD1B4 File Offset: 0x000BB3B4
		public bool IsGroup { get; set; }

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x000BD1BD File Offset: 0x000BB3BD
		// (set) Token: 0x0600398E RID: 14734 RVA: 0x000BD1C5 File Offset: 0x000BB3C5
		internal bool IsSimpleBinding { get; set; }

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x0600398F RID: 14735 RVA: 0x000BD1CE File Offset: 0x000BB3CE
		// (set) Token: 0x06003990 RID: 14736 RVA: 0x000BD1D6 File Offset: 0x000BB3D6
		internal bool EnableCollapsing { get; set; }

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06003991 RID: 14737 RVA: 0x000BD1DF File Offset: 0x000BB3DF
		// (set) Token: 0x06003992 RID: 14738 RVA: 0x000BD1E7 File Offset: 0x000BB3E7
		internal bool EnableGroupCollapsing { get; set; }

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000BD1F0 File Offset: 0x000BB3F0
		// (set) Token: 0x06003994 RID: 14740 RVA: 0x000BD1F8 File Offset: 0x000BB3F8
		internal bool Collapsed { get; set; }

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06003995 RID: 14741 RVA: 0x000BD201 File Offset: 0x000BB401
		// (set) Token: 0x06003996 RID: 14742 RVA: 0x000BD209 File Offset: 0x000BB409
		internal bool GroupCollapsed { get; set; }

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06003997 RID: 14743 RVA: 0x000BD212 File Offset: 0x000BB412
		// (set) Token: 0x06003998 RID: 14744 RVA: 0x000BD21A File Offset: 0x000BB41A
		internal bool HasNodes { get; set; }

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x000BD223 File Offset: 0x000BB423
		// (set) Token: 0x0600399A RID: 14746 RVA: 0x000BD22B File Offset: 0x000BB42B
		internal bool HasNodesForLoad { get; set; }

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x0600399B RID: 14747 RVA: 0x000BD234 File Offset: 0x000BB434
		// (set) Token: 0x0600399C RID: 14748 RVA: 0x000BD23C File Offset: 0x000BB43C
		internal int GroupItemsCount { get; set; }

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x000BD245 File Offset: 0x000BB445
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x000BD24C File Offset: 0x000BB44C
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (!this.IsSimpleBinding)
			{
				string text = string.Empty;
				switch (this.GroupItemsCount)
				{
				case 0:
				{
					string str = "rocEmptyGroup";
					if (this.HasNodesForLoad)
					{
						str = "rocCollapsedGroup";
					}
					text = ("rocGroup " + str).Trim();
					break;
				}
				case 1:
				{
					string str2 = "rocPseudoGroup";
					if (this.HasNodesForLoad && this.GroupCollapsed)
					{
						str2 = "rocCollapsedGroup";
					}
					text = ((this.RenderedFields.Count > 0) ? "rocGroup" : ("rocGroup " + str2)).Trim();
					break;
				}
				default:
					text = "rocGroup";
					if (this.EnableGroupCollapsing)
					{
						text += (this.GroupCollapsed ? " rocCollapsedGroup" : " rocExpandedGroup");
					}
					break;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
				base.RenderBeginTag(writer);
			}
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x000BD328 File Offset: 0x000BB528
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (!this.IsSimpleBinding)
			{
				if (this.EnableGroupCollapsing && this.GroupItemsCount > 1)
				{
					string arrowCollapsedState = this.GroupCollapsed ? "rocExpandGroupArrow" : "rocCollapseGroupArrow";
					this.RenderGroupExpandCollapseArrow(arrowCollapsedState, this.GroupCollapsed, writer);
				}
				else if (this.HasNodesForLoad && this.GroupItemsCount == 0)
				{
					this.RenderGroupExpandCollapseArrow("rocExpandGroupArrow", true, writer);
				}
				else if (this.HasNodesForLoad && this.GroupItemsCount == 1 && this.GroupCollapsed)
				{
					this.RenderGroupExpandCollapseArrow("rocExpandGroupArrow", true, writer);
				}
				this.RenderFields(writer);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocItemList");
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				if (this.GroupItemsCount == 0 && !this.HasNodesForLoad)
				{
					string value = string.Format("{0} {1} {2} {3}", new object[]
					{
						"rocItemWrap",
						"rocFirst",
						"rocLast",
						"rocOnly"
					}).Trim();
					writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
					writer.RenderBeginTag(HtmlTextWriterTag.Li);
					string value2 = string.Format("{0} {1}", "rocItem", "rocEmptyItem");
					writer.AddAttribute(HtmlTextWriterAttribute.Class, value2);
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
				else if (this.GroupItemsCount == 0 && this.HasNodesForLoad)
				{
					string value3 = string.Format("{0} {1} {2}", "rocItemWrap", "rocFirst", "rocEmptyItemWrap").Trim();
					writer.AddAttribute(HtmlTextWriterAttribute.Class, value3);
					writer.RenderBeginTag(HtmlTextWriterTag.Li);
					string value4 = string.Format("{0} {1}", "rocItem", "rocNotLoadedItem");
					writer.AddAttribute(HtmlTextWriterAttribute.Class, value4);
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocItemContent");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocItemText");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					writer.Write("Expand to load items");
					writer.RenderEndTag();
					writer.RenderEndTag();
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
				base.RenderContents(writer);
				writer.RenderEndTag();
				if (this.EnableCollapsing && this.HasNodes)
				{
					string arrowCollapsedState2 = this.Collapsed ? "rocExpandArrow" : "rocCollapseArrow";
					this.RenderExpandCollapseArrow(arrowCollapsedState2, this.Collapsed, writer);
					return;
				}
				if (this.HasNodesForLoad)
				{
					this.RenderExpandCollapseArrow("rocExpandArrow", true, writer);
					return;
				}
			}
			else
			{
				base.RenderContents(writer);
			}
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000BD58E File Offset: 0x000BB78E
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (!this.IsSimpleBinding)
			{
				base.RenderEndTag(writer);
			}
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000BD5A0 File Offset: 0x000BB7A0
		private void RenderFields(HtmlTextWriter writer)
		{
			if (this.RenderedFields.Count > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocNodeFields");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				foreach (OrgChartRenderedField orgChartRenderedField in this.RenderedFields)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocItemField");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					writer.Write(orgChartRenderedField.TextToRender);
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
			}
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x000BD63C File Offset: 0x000BB83C
		protected virtual void RenderExpandCollapseArrow(string arrowCollapsedState, bool collapsed, HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, arrowCollapsedState);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			string value = collapsed ? "+" : "-";
			writer.Write(value);
			writer.RenderEndTag();
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x000BD678 File Offset: 0x000BB878
		protected virtual void RenderGroupExpandCollapseArrow(string arrowCollapsedState, bool groupCollapsed, HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, arrowCollapsedState);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			string value = groupCollapsed ? "+" : "-";
			writer.Write(value);
			writer.RenderEndTag();
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x000BD6B2 File Offset: 0x000BB8B2
		public OrgChartRenderedFieldCollection RenderedFields
		{
			get
			{
				if (this._renderedFields == null)
				{
					this._renderedFields = new OrgChartRenderedFieldCollection();
				}
				return this._renderedFields;
			}
		}

		// Token: 0x04000F50 RID: 3920
		private OrgChartRenderedFieldCollection _renderedFields;
	}
}
