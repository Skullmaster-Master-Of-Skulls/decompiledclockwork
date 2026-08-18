using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02000B54 RID: 2900
	public class TrackChangesSettings : StateManager
	{
		// Token: 0x170023D4 RID: 9172
		// (get) Token: 0x06006D53 RID: 27987 RVA: 0x001964F5 File Offset: 0x001946F5
		// (set) Token: 0x06006D54 RID: 27988 RVA: 0x00196515 File Offset: 0x00194715
		[DefaultValue("RadEditorUser")]
		[Description("Gets or sets the author of the changes applied on the edited content.")]
		[NotifyParentProperty(true)]
		public string Author
		{
			get
			{
				return ((string)base.ViewState["RadEditorTrackChangesAuthor"]) ?? "RadEditorUser";
			}
			set
			{
				base.ViewState["RadEditorTrackChangesAuthor"] = value;
			}
		}

		// Token: 0x170023D5 RID: 9173
		// (get) Token: 0x06006D55 RID: 27989 RVA: 0x00196528 File Offset: 0x00194728
		// (set) Token: 0x06006D56 RID: 27990 RVA: 0x00196548 File Offset: 0x00194748
		[Description("Gets or sets the suffix of the css class which marks the track changes elements.")]
		[DefaultValue("reU0")]
		[NotifyParentProperty(true)]
		public string UserCssId
		{
			get
			{
				return ((string)base.ViewState["RadEditorTrackChangesUserCssId"]) ?? "reU0";
			}
			set
			{
				base.ViewState["RadEditorTrackChangesUserCssId"] = value;
			}
		}

		// Token: 0x170023D6 RID: 9174
		// (get) Token: 0x06006D57 RID: 27991 RVA: 0x0019655B File Offset: 0x0019475B
		// (set) Token: 0x06006D58 RID: 27992 RVA: 0x00196586 File Offset: 0x00194786
		[Description("Gets or sets the value indicating whether the spell will allow adding custom words.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool CanAcceptTrackChanges
		{
			get
			{
				return base.ViewState["RadEditorTrackChangesCanAcceptTrackChanges"] != null && (bool)base.ViewState["RadEditorTrackChangesCanAcceptTrackChanges"];
			}
			set
			{
				base.ViewState["RadEditorTrackChangesCanAcceptTrackChanges"] = value;
			}
		}
	}
}
