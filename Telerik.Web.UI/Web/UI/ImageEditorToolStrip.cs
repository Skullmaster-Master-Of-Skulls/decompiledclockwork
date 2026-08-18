using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000E9E RID: 3742
	[ToolboxItem(false)]
	[ParseChildren(true, "Tools")]
	public class ImageEditorToolStrip : ImageEditorToolBase
	{
		// Token: 0x06008EAE RID: 36526 RVA: 0x002028F5 File Offset: 0x00200AF5
		public ImageEditorToolStrip()
		{
		}

		// Token: 0x06008EAF RID: 36527 RVA: 0x002028FD File Offset: 0x00200AFD
		public ImageEditorToolStrip(string commandName) : this(commandName, string.Empty)
		{
		}

		// Token: 0x06008EB0 RID: 36528 RVA: 0x0020290B File Offset: 0x00200B0B
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ImageEditorToolStrip(string commandName, string shortCut) : this()
		{
			this.CommandName = commandName;
			this.ShortCut = shortCut;
		}

		// Token: 0x17002D29 RID: 11561
		// (get) Token: 0x06008EB1 RID: 36529 RVA: 0x00202921 File Offset: 0x00200B21
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual ImageEditorToolCollection Tools
		{
			get
			{
				if (this._tools == null)
				{
					this._tools = new ImageEditorToolCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._tools).TrackViewState();
					}
				}
				return this._tools;
			}
		}

		// Token: 0x17002D2A RID: 11562
		// (get) Token: 0x06008EB2 RID: 36530 RVA: 0x0020294F File Offset: 0x00200B4F
		// (set) Token: 0x06008EB3 RID: 36531 RVA: 0x00202952 File Offset: 0x00200B52
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17002D2B RID: 11563
		// (get) Token: 0x06008EB4 RID: 36532 RVA: 0x00202954 File Offset: 0x00200B54
		// (set) Token: 0x06008EB5 RID: 36533 RVA: 0x00202974 File Offset: 0x00200B74
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Gets or sets the name of the command fired when the tool is clicked.")]
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

		// Token: 0x17002D2C RID: 11564
		// (get) Token: 0x06008EB6 RID: 36534 RVA: 0x00202987 File Offset: 0x00200B87
		// (set) Token: 0x06008EB7 RID: 36535 RVA: 0x002029A7 File Offset: 0x00200BA7
		[DefaultValue("")]
		[Localizable(true)]
		[Description("Gets or sets the text displayed in the tool.")]
		[Category("Appearance")]
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

		// Token: 0x17002D2D RID: 11565
		// (get) Token: 0x06008EB8 RID: 36536 RVA: 0x002029BA File Offset: 0x00200BBA
		// (set) Token: 0x06008EB9 RID: 36537 RVA: 0x002029DA File Offset: 0x00200BDA
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

		// Token: 0x17002D2E RID: 11566
		// (get) Token: 0x06008EBA RID: 36538 RVA: 0x002029ED File Offset: 0x00200BED
		// (set) Token: 0x06008EBB RID: 36539 RVA: 0x00202A0D File Offset: 0x00200C0D
		[CssClassProperty]
		[Category("Appearance")]
		[Description("Gets or sets the CSS class applied to the ImageEditor tool.")]
		[DefaultValue("")]
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

		// Token: 0x17002D2F RID: 11567
		// (get) Token: 0x06008EBC RID: 36540 RVA: 0x00202A20 File Offset: 0x00200C20
		// (set) Token: 0x06008EBD RID: 36541 RVA: 0x00202A40 File Offset: 0x00200C40
		[Category("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("Gets or sets the location of an image (icon) to display in the ImageEditor tool.")]
		[Bindable(true)]
		[UrlProperty]
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

		// Token: 0x17002D30 RID: 11568
		// (get) Token: 0x06008EBE RID: 36542 RVA: 0x00202A53 File Offset: 0x00200C53
		// (set) Token: 0x06008EBF RID: 36543 RVA: 0x00202A74 File Offset: 0x00200C74
		[Description("Gets or sets a value indicating whether this ImageEditor tool is enabled.")]
		[Category("Behavior")]
		[DefaultValue(true)]
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

		// Token: 0x17002D31 RID: 11569
		// (get) Token: 0x06008EC0 RID: 36544 RVA: 0x00202A8C File Offset: 0x00200C8C
		// (set) Token: 0x06008EC1 RID: 36545 RVA: 0x00202AAD File Offset: 0x00200CAD
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Enables the use of default tools. Last selected becomes the default.")]
		public virtual bool EnableDefaultTool
		{
			get
			{
				return (bool)(base.ViewState["EnableDefaultTool"] ?? false);
			}
			set
			{
				base.ViewState["EnableDefaultTool"] = value;
			}
		}

		// Token: 0x17002D32 RID: 11570
		// (get) Token: 0x06008EC2 RID: 36546 RVA: 0x00202AC5 File Offset: 0x00200CC5
		// (set) Token: 0x06008EC3 RID: 36547 RVA: 0x00202AE5 File Offset: 0x00200CE5
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

		// Token: 0x06008EC4 RID: 36548 RVA: 0x00202AF8 File Offset: 0x00200CF8
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Tools).LoadViewState(array[1]);
		}

		// Token: 0x06008EC5 RID: 36549 RVA: 0x00202B24 File Offset: 0x00200D24
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Tools).SaveViewState()
			};
		}

		// Token: 0x06008EC6 RID: 36550 RVA: 0x00202B52 File Offset: 0x00200D52
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Tools).TrackViewState();
		}

		// Token: 0x06008EC7 RID: 36551 RVA: 0x00202B65 File Offset: 0x00200D65
		internal override void SetDirty()
		{
			base.SetDirty();
			this.Tools.SetDirty();
		}

		// Token: 0x040027AC RID: 10156
		private ImageEditorToolCollection _tools;
	}
}
