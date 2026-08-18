using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using AjaxControlToolkit.Design;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000EC RID: 236
	[ToolboxItem(false)]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.CommonButton", "HtmlEditor.ToolbarButtons.CommonButton")]
	public abstract class CommonButton : ScriptControlBase
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x00012DB0 File Offset: 0x00010FB0
		protected CommonButton(HtmlTextWriterTag tag) : base(false, tag)
		{
			base.CssClass = "ajax__htmleditor_toolbar_button";
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00012DC5 File Offset: 0x00010FC5
		protected CommonButton() : base(false, HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00012DD0 File Offset: 0x00010FD0
		protected bool IsDesign
		{
			get
			{
				bool result;
				try
				{
					bool flag = this.Context == null || (base.Site != null && base.Site.DesignMode);
					result = flag;
				}
				catch
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00012E20 File Offset: 0x00011020
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00012E28 File Offset: 0x00011028
		internal new Page Page
		{
			get
			{
				return base.Page;
			}
			set
			{
				base.Page = value;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00012E31 File Offset: 0x00011031
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Collection<ActiveModeType> ActiveModes
		{
			get
			{
				if (this._activeModes == null)
				{
					this._activeModes = new Collection<ActiveModeType>();
				}
				return this._activeModes;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00012E4C File Offset: 0x0001104C
		internal Collection<Control> ExportedControls
		{
			get
			{
				if (this._exportedControls == null)
				{
					this._exportedControls = new Collection<Control>();
				}
				return this._exportedControls;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00012E67 File Offset: 0x00011067
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00012E88 File Offset: 0x00011088
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("preservePlace")]
		public bool PreservePlace
		{
			get
			{
				return (bool)(this.ViewState["PreservePlace"] ?? false);
			}
			set
			{
				this.ViewState["PreservePlace"] = value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00012EA0 File Offset: 0x000110A0
		[DefaultValue("ajax__htmleditor_toolbar_button")]
		public override string CssClass
		{
			get
			{
				return "ajax__htmleditor_toolbar_button";
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00012EA7 File Offset: 0x000110A7
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x00012EAF File Offset: 0x000110AF
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool IgnoreTab
		{
			get
			{
				return this._ignoreTab;
			}
			set
			{
				this._ignoreTab = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00012EB8 File Offset: 0x000110B8
		[ExtenderControlProperty]
		[ClientPropertyName("activeModesIds")]
		[Browsable(false)]
		public string ActiveModesIds
		{
			get
			{
				string text = string.Empty;
				for (int i = 0; i < this.ActiveModes.Count; i++)
				{
					if (i > 0)
					{
						text += ";";
					}
					text += ((int)this.ActiveModes[i]).ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
				}
				return text;
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00012F17 File Offset: 0x00011117
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeActiveModesIds()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00012F1F File Offset: 0x0001111F
		protected string GetFromResource(string name)
		{
			return "";
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00012F26 File Offset: 0x00011126
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00012F30 File Offset: 0x00011130
		protected override void OnPreRender(EventArgs e)
		{
			try
			{
				base.OnPreRender(e);
			}
			catch
			{
			}
			this._wasPreRender = true;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00012F60 File Offset: 0x00011160
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this._wasPreRender)
			{
				this.OnPreRender(new EventArgs());
			}
			base.Render(writer);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00012F7C File Offset: 0x0001117C
		internal virtual void CreateChilds(DesignerWithMapPath designer)
		{
			this._designer = designer;
			this.Controls.Clear();
			this.CreateChildControls();
		}

		// Token: 0x04000300 RID: 768
		private Collection<ActiveModeType> _activeModes;

		// Token: 0x04000301 RID: 769
		private Collection<Control> _exportedControls;

		// Token: 0x04000302 RID: 770
		private bool _wasPreRender;

		// Token: 0x04000303 RID: 771
		private bool _ignoreTab;

		// Token: 0x04000304 RID: 772
		internal DesignerWithMapPath _designer;
	}
}
