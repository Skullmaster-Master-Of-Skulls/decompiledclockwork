using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200012F RID: 303
	[ToolboxBitmap(typeof(Accessor), "ListSearch.bmp")]
	[Description("Lets users search incrementally within ListBoxes")]
	[RequiredScript(typeof(PopupControlExtender), 1)]
	[Designer(typeof(ListSearchExtenderDesigner))]
	[ClientScriptResource("Sys.Extended.UI.ListSearchBehavior", "ListSearch")]
	[RequiredScript(typeof(CommonToolkitScripts), 0)]
	[TargetControlType(typeof(ListControl))]
	[RequiredScript(typeof(AnimationExtender), 2)]
	public class ListSearchExtender : AnimationExtenderControlBase
	{
		// Token: 0x0600078C RID: 1932 RVA: 0x000143D4 File Offset: 0x000125D4
		public ListSearchExtender()
		{
			base.EnableClientState = true;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000143E3 File Offset: 0x000125E3
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			base.ClientState = ((string.Compare(this.Page.Form.DefaultFocus, base.TargetControlID, StringComparison.OrdinalIgnoreCase) == 0) ? "Focused" : null);
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00014418 File Offset: 0x00012618
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x0001442A File Offset: 0x0001262A
		[Description("The prompt text displayed when user clicks the list")]
		[ClientPropertyName("promptText")]
		[DefaultValue("Type to search")]
		[ExtenderControlProperty]
		public string PromptText
		{
			get
			{
				return base.GetPropertyValue<string>("promptText", "Type to search");
			}
			set
			{
				base.SetPropertyValue<string>("promptText", value);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00014438 File Offset: 0x00012638
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x0001444A File Offset: 0x0001264A
		[ClientPropertyName("promptCssClass")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		[Description("CSS class applied to prompt when user clicks list")]
		public string PromptCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("promptCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("promptCssClass", value);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00014458 File Offset: 0x00012658
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x00014466 File Offset: 0x00012666
		[DefaultValue(ListSearchPromptPosition.Top)]
		[ClientPropertyName("promptPosition")]
		[ExtenderControlProperty]
		[Description("Indicates where you want the prompt message displayed when the user clicks on the list.")]
		public ListSearchPromptPosition PromptPosition
		{
			get
			{
				return base.GetPropertyValue<ListSearchPromptPosition>("promptPosition", ListSearchPromptPosition.Top);
			}
			set
			{
				base.SetPropertyValue<ListSearchPromptPosition>("promptPosition", value);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00014474 File Offset: 0x00012674
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x00014487 File Offset: 0x00012687
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ExtenderControlProperty]
		[DefaultValue(null)]
		[ClientPropertyName("onShow")]
		[Browsable(false)]
		public Animation OnShow
		{
			get
			{
				return base.GetAnimation(ref this._onShow, "OnShow");
			}
			set
			{
				base.SetAnimation(ref this._onShow, "OnShow", value);
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001449B File Offset: 0x0001269B
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x000144AE File Offset: 0x000126AE
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ClientPropertyName("onHide")]
		[ExtenderControlProperty]
		public Animation OnHide
		{
			get
			{
				return base.GetAnimation(ref this._onHide, "OnHide");
			}
			set
			{
				base.SetAnimation(ref this._onHide, "OnHide", value);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x000144C2 File Offset: 0x000126C2
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x000144D0 File Offset: 0x000126D0
		[ClientPropertyName("queryTimeout")]
		[DefaultValue(0)]
		[ExtenderControlProperty]
		public int QueryTimeout
		{
			get
			{
				return base.GetPropertyValue<int>("QueryTimeout", 0);
			}
			set
			{
				base.SetPropertyValue<int>("QueryTimeout", value);
			}
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000144DE File Offset: 0x000126DE
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			base.ResolveControlIDs(this._onShow);
			base.ResolveControlIDs(this._onHide);
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x000144FF File Offset: 0x000126FF
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x0001450D File Offset: 0x0001270D
		[ClientPropertyName("queryPattern")]
		[DefaultValue(ListSearchQueryPattern.StartsWith)]
		[ExtenderControlProperty]
		[Description("Indicates search criteria to be used to find items.")]
		public ListSearchQueryPattern QueryPattern
		{
			get
			{
				return base.GetPropertyValue<ListSearchQueryPattern>("QueryPattern", ListSearchQueryPattern.StartsWith);
			}
			set
			{
				base.SetPropertyValue<ListSearchQueryPattern>("QueryPattern", value);
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0001451B File Offset: 0x0001271B
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x00014529 File Offset: 0x00012729
		[DefaultValue(false)]
		[ClientPropertyName("isSorted")]
		[ExtenderControlProperty]
		public bool IsSorted
		{
			get
			{
				return base.GetPropertyValue<bool>("IsSorted", false);
			}
			set
			{
				base.SetPropertyValue<bool>("IsSorted", value);
			}
		}

		// Token: 0x04000321 RID: 801
		private Animation _onShow;

		// Token: 0x04000322 RID: 802
		private Animation _onHide;
	}
}
