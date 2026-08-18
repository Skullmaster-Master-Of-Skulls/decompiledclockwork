using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000EBB RID: 3771
	[ToolboxItem(false)]
	public class ImageEditorTool : ImageEditorToolBase
	{
		// Token: 0x06008FF2 RID: 36850 RVA: 0x00206D41 File Offset: 0x00204F41
		public ImageEditorTool()
		{
		}

		// Token: 0x06008FF3 RID: 36851 RVA: 0x00206D49 File Offset: 0x00204F49
		public ImageEditorTool(string commandName) : this(commandName, string.Empty)
		{
		}

		// Token: 0x06008FF4 RID: 36852 RVA: 0x00206D57 File Offset: 0x00204F57
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ImageEditorTool(string commandName, string shortCut) : this()
		{
			this.CommandName = commandName;
			this.ShortCut = shortCut;
		}

		// Token: 0x17002D95 RID: 11669
		// (get) Token: 0x06008FF5 RID: 36853 RVA: 0x00206D6D File Offset: 0x00204F6D
		// (set) Token: 0x06008FF6 RID: 36854 RVA: 0x00206D70 File Offset: 0x00204F70
		[EditorBrowsable(EditorBrowsableState.Never)]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool IsSeparator
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17002D96 RID: 11670
		// (get) Token: 0x06008FF7 RID: 36855 RVA: 0x00206D72 File Offset: 0x00204F72
		// (set) Token: 0x06008FF8 RID: 36856 RVA: 0x00206D92 File Offset: 0x00204F92
		[Description("Gets or sets the name of the command fired when the tool is clicked.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public virtual string CommandName
		{
			get
			{
				return ((string)base.ViewState["CommandName"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["CommandName"] = value;
			}
		}

		// Token: 0x17002D97 RID: 11671
		// (get) Token: 0x06008FF9 RID: 36857 RVA: 0x00206DA5 File Offset: 0x00204FA5
		// (set) Token: 0x06008FFA RID: 36858 RVA: 0x00206DC5 File Offset: 0x00204FC5
		[DefaultValue("")]
		[Description("Gets or sets the text displayed in the tool.")]
		[Category("Appearance")]
		[Localizable(true)]
		public virtual string Text
		{
			get
			{
				return ((string)base.ViewState["Text"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x17002D98 RID: 11672
		// (get) Token: 0x06008FFB RID: 36859 RVA: 0x00206DD8 File Offset: 0x00204FD8
		// (set) Token: 0x06008FFC RID: 36860 RVA: 0x00206DF8 File Offset: 0x00204FF8
		[DefaultValue("")]
		[Localizable(true)]
		[Category("Behavior")]
		[Description("Gets or sets the ToolTip of the ImageEditor tool")]
		public virtual string ToolTip
		{
			get
			{
				return ((string)base.ViewState["ToolTip"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17002D99 RID: 11673
		// (get) Token: 0x06008FFD RID: 36861 RVA: 0x00206E0B File Offset: 0x0020500B
		// (set) Token: 0x06008FFE RID: 36862 RVA: 0x00206E2B File Offset: 0x0020502B
		[Description("Gets or sets the CSS class applied to the ImageEditor tool.")]
		[CssClassProperty]
		[DefaultValue("")]
		[Category("Appearance")]
		public virtual string CssClass
		{
			get
			{
				return ((string)base.ViewState["CssClass"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["CssClass"] = value;
			}
		}

		// Token: 0x17002D9A RID: 11674
		// (get) Token: 0x06008FFF RID: 36863 RVA: 0x00206E3E File Offset: 0x0020503E
		// (set) Token: 0x06009000 RID: 36864 RVA: 0x00206E5E File Offset: 0x0020505E
		[Description("Gets or sets the location of an image (icon) to display in the ImageEditor tool.")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Bindable(true)]
		[UrlProperty]
		[Category("Appearance")]
		public virtual string ImageUrl
		{
			get
			{
				return ((string)base.ViewState["ImageUrl"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17002D9B RID: 11675
		// (get) Token: 0x06009001 RID: 36865 RVA: 0x00206E71 File Offset: 0x00205071
		// (set) Token: 0x06009002 RID: 36866 RVA: 0x00206E92 File Offset: 0x00205092
		[DefaultValue(true)]
		[Description("Gets or sets a value indicating whether this ImageEditor tool is enabled.")]
		[Category("Behavior")]
		public virtual bool Enabled
		{
			get
			{
				return (bool)(base.ViewState["Enabled"] ?? true);
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17002D9C RID: 11676
		// (get) Token: 0x06009003 RID: 36867 RVA: 0x00206EAA File Offset: 0x002050AA
		// (set) Token: 0x06009004 RID: 36868 RVA: 0x00206ECB File Offset: 0x002050CB
		[Description("Gets or sets a value indicating whether the ImageEditor tool can be toggled or not.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public virtual bool IsToggleButton
		{
			get
			{
				return (bool)(base.ViewState["IsToggleButton"] ?? false);
			}
			set
			{
				base.ViewState["IsToggleButton"] = value;
			}
		}

		// Token: 0x17002D9D RID: 11677
		// (get) Token: 0x06009005 RID: 36869 RVA: 0x00206EE3 File Offset: 0x002050E3
		// (set) Token: 0x06009006 RID: 36870 RVA: 0x00206F03 File Offset: 0x00205103
		[DefaultValue("")]
		[Category("Accessibility")]
		[Description("Gets or sets the keyboard shortcut which will invoke the associated RadImageEditor command.")]
		public virtual string ShortCut
		{
			get
			{
				return ((string)base.ViewState["ShortCut"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ShortCut"] = value;
			}
		}
	}
}
