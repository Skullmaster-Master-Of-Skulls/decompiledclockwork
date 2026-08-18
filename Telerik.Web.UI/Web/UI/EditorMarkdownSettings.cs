using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000B4A RID: 2890
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class EditorMarkdownSettings : ObjectWithState
	{
		// Token: 0x06006CEB RID: 27883 RVA: 0x00194839 File Offset: 0x00192A39
		public EditorMarkdownSettings(StateBag ownerStateBag) : base("reMarkdown_", ownerStateBag)
		{
		}

		// Token: 0x170023BF RID: 9151
		// (get) Token: 0x06006CEC RID: 27884 RVA: 0x00194847 File Offset: 0x00192A47
		// (set) Token: 0x06006CED RID: 27885 RVA: 0x00194855 File Offset: 0x00192A55
		[NotifyParentProperty(true)]
		[Description("Sets the Markdown convertion style of H1 and H2 elemnets.")]
		[DefaultValue(EditorMarkdownHeaderStyle.setex)]
		public EditorMarkdownHeaderStyle HeaderStyle
		{
			get
			{
				return base.GetViewStateValue<EditorMarkdownHeaderStyle>("h-style", EditorMarkdownHeaderStyle.setex);
			}
			set
			{
				base.ViewState["h-style"] = value;
			}
		}

		// Token: 0x170023C0 RID: 9152
		// (get) Token: 0x06006CEE RID: 27886 RVA: 0x0019486D File Offset: 0x00192A6D
		// (set) Token: 0x06006CEF RID: 27887 RVA: 0x0019487B File Offset: 0x00192A7B
		[Description("Sets the Markdown convertion style of anchor elemnets.")]
		[DefaultValue(EditorMarkdownElemetStyle.markdown)]
		[NotifyParentProperty(true)]
		public EditorMarkdownElemetStyle AnchorStyle
		{
			get
			{
				return base.GetViewStateValue<EditorMarkdownElemetStyle>("a-style", EditorMarkdownElemetStyle.markdown);
			}
			set
			{
				base.ViewState["a-style"] = value;
			}
		}

		// Token: 0x170023C1 RID: 9153
		// (get) Token: 0x06006CF0 RID: 27888 RVA: 0x00194893 File Offset: 0x00192A93
		// (set) Token: 0x06006CF1 RID: 27889 RVA: 0x001948A1 File Offset: 0x00192AA1
		[NotifyParentProperty(true)]
		[DefaultValue(EditorMarkdownElemetStyle.markdown)]
		[Description("Sets the Markdown convertion style of image elemnets.")]
		public EditorMarkdownElemetStyle ImgStyle
		{
			get
			{
				return base.GetViewStateValue<EditorMarkdownElemetStyle>("img-style", EditorMarkdownElemetStyle.markdown);
			}
			set
			{
				base.ViewState["img-style"] = value;
			}
		}

		// Token: 0x170023C2 RID: 9154
		// (get) Token: 0x06006CF2 RID: 27890 RVA: 0x001948B9 File Offset: 0x00192AB9
		// (set) Token: 0x06006CF3 RID: 27891 RVA: 0x001948C7 File Offset: 0x00192AC7
		[NotifyParentProperty(true)]
		[DefaultValue(EditorMarkdownTableStyle.breaktable)]
		[Description("Sets the Markdown convertion style of table elemnets.")]
		public EditorMarkdownTableStyle TableStyle
		{
			get
			{
				return base.GetViewStateValue<EditorMarkdownTableStyle>("table-style", EditorMarkdownTableStyle.breaktable);
			}
			set
			{
				base.ViewState["table-style"] = value;
			}
		}

		// Token: 0x170023C3 RID: 9155
		// (get) Token: 0x06006CF4 RID: 27892 RVA: 0x001948DF File Offset: 0x00192ADF
		// (set) Token: 0x06006CF5 RID: 27893 RVA: 0x001948ED File Offset: 0x00192AED
		[NotifyParentProperty(true)]
		[DefaultValue(EditorMarkdownUnparseablesStyle.strip)]
		[Description("Sets the Markdown convertion style of address, dl, fieldset, form, map, object, script, noscript, elemnets.")]
		public EditorMarkdownUnparseablesStyle UnparseablesStyle
		{
			get
			{
				return base.GetViewStateValue<EditorMarkdownUnparseablesStyle>("table-style", EditorMarkdownUnparseablesStyle.strip);
			}
			set
			{
				base.ViewState["table-style"] = value;
			}
		}
	}
}
