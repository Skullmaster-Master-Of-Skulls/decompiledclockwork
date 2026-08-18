using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000190 RID: 400
	[Designer(typeof(TabContainerDesigner))]
	[ToolboxBitmap(typeof(Accessor), "Tabs.bmp")]
	[ParseChildren(typeof(TabPanel))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientCssResource("Tabs")]
	[ClientScriptResource("Sys.Extended.UI.TabContainer", "Tabs")]
	public class TabContainer : ScriptControlBase, IPostBackEventHandler
	{
		// Token: 0x06000B3A RID: 2874 RVA: 0x0001C991 File Offset: 0x0001AB91
		public TabContainer() : base(true, HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000B3B RID: 2875 RVA: 0x0001C9C6 File Offset: 0x0001ABC6
		// (remove) Token: 0x06000B3C RID: 2876 RVA: 0x0001C9D9 File Offset: 0x0001ABD9
		[Category("Behavior")]
		public event EventHandler ActiveTabChanged
		{
			add
			{
				base.Events.AddHandler(TabContainer.EventActiveTabChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabContainer.EventActiveTabChanged, value);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0001C9EC File Offset: 0x0001ABEC
		[ClientPropertyName("activeTabIndex")]
		[DefaultValue(-1)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Behavior")]
		[ExtenderControlProperty]
		public int ActiveTabIndexForClient
		{
			get
			{
				int num = this.ActiveTabIndex;
				int num2 = 0;
				while (num2 <= this.ActiveTabIndex && num2 < this.Tabs.Count)
				{
					if (!this.Tabs[num2].Visible)
					{
						num--;
					}
					num2++;
				}
				if (num < 0)
				{
					return 0;
				}
				return num;
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0001CA3D File Offset: 0x0001AC3D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeActiveTabIndexForClient()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0001CA45 File Offset: 0x0001AC45
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x0001CA6C File Offset: 0x0001AC6C
		[Category("Behavior")]
		[DefaultValue(-1)]
		public virtual int ActiveTabIndex
		{
			get
			{
				if (this._cachedActiveTabIndex > -1)
				{
					return this._cachedActiveTabIndex;
				}
				if (this.Tabs.Count == 0)
				{
					return -1;
				}
				return this._activeTabIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.Tabs.Count == 0 && !this._initialized)
				{
					this._cachedActiveTabIndex = value;
					return;
				}
				if (this.ActiveTabIndex == value)
				{
					return;
				}
				if (this.ActiveTabIndex != -1 && this.ActiveTabIndex < this.Tabs.Count)
				{
					this.Tabs[this.ActiveTabIndex].Active = false;
				}
				if (value >= this.Tabs.Count)
				{
					this._activeTabIndex = this.Tabs.Count - 1;
					this._cachedActiveTabIndex = value;
				}
				else
				{
					this._activeTabIndex = value;
					this._cachedActiveTabIndex = -1;
				}
				if (this.ActiveTabIndex != -1 && this.ActiveTabIndex < this.Tabs.Count)
				{
					this.Tabs[this.ActiveTabIndex].Active = true;
				}
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0001CB4D File Offset: 0x0001AD4D
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x0001CB6E File Offset: 0x0001AD6E
		private int LastActiveTabIndex
		{
			get
			{
				return (int)(this.ViewState["LastActiveTabIndex"] ?? -1);
			}
			set
			{
				this.ViewState["LastActiveTabIndex"] = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0001CB86 File Offset: 0x0001AD86
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TabPanelCollection Tabs
		{
			get
			{
				return (TabPanelCollection)this.Controls;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0001CB94 File Offset: 0x0001AD94
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x0001CBD0 File Offset: 0x0001ADD0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TabPanel ActiveTab
		{
			get
			{
				int activeTabIndex = this.ActiveTabIndex;
				if (activeTabIndex < 0 || activeTabIndex >= this.Tabs.Count)
				{
					return null;
				}
				this.EnsureActiveTab();
				return this.Tabs[activeTabIndex];
			}
			set
			{
				int num = this.Tabs.IndexOf(value);
				if (num < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ActiveTabIndex = num;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0001CC00 File Offset: 0x0001AE00
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x0001CC08 File Offset: 0x0001AE08
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool AutoPostBack
		{
			get
			{
				return this._autoPostBack;
			}
			set
			{
				this._autoPostBack = value;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0001CC11 File Offset: 0x0001AE11
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x0001CC19 File Offset: 0x0001AE19
		[DefaultValue(typeof(Unit), "")]
		[Category("Appearance")]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0001CC22 File Offset: 0x0001AE22
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x0001CC2A File Offset: 0x0001AE2A
		[DefaultValue(typeof(Unit), "")]
		[Category("Appearance")]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0001CC33 File Offset: 0x0001AE33
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x0001CC54 File Offset: 0x0001AE54
		[DefaultValue(TabCssTheme.XP)]
		[Category("Appearance")]
		[ExtenderControlProperty]
		[ClientPropertyName("cssTheme")]
		public TabCssTheme CssTheme
		{
			get
			{
				return (TabCssTheme)(this.ViewState["CssTheme"] ?? TabCssTheme.XP);
			}
			set
			{
				this.ViewState["CssTheme"] = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0001CC6C File Offset: 0x0001AE6C
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x0001CC8D File Offset: 0x0001AE8D
		[DefaultValue(ScrollBars.None)]
		[Category("Behavior")]
		[ExtenderControlProperty]
		[ClientPropertyName("scrollBars")]
		public ScrollBars ScrollBars
		{
			get
			{
				return (ScrollBars)(this.ViewState["ScrollBars"] ?? ScrollBars.None);
			}
			set
			{
				this.ViewState["ScrollBars"] = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0001CCA5 File Offset: 0x0001AEA5
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x0001CCAD File Offset: 0x0001AEAD
		[DefaultValue(TabStripPlacement.Top)]
		[Category("Appearance")]
		[ClientPropertyName("tabStripPlacement")]
		public TabStripPlacement TabStripPlacement
		{
			get
			{
				return this._tabStripPlacement;
			}
			set
			{
				this._tabStripPlacement = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0001CCB6 File Offset: 0x0001AEB6
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0001CCD6 File Offset: 0x0001AED6
		[ClientPropertyName("activeTabChanged")]
		[Category("Behavior")]
		[ExtenderControlEvent]
		[DefaultValue("")]
		public string OnClientActiveTabChanged
		{
			get
			{
				return (string)(this.ViewState["OnClientActiveTabChanged"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientActiveTabChanged"] = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0001CCE9 File Offset: 0x0001AEE9
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x0001CCF1 File Offset: 0x0001AEF1
		[ClientPropertyName("autoPostBackId")]
		[ExtenderControlProperty]
		public new string UniqueID
		{
			get
			{
				return base.UniqueID;
			}
			set
			{
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0001CCF3 File Offset: 0x0001AEF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeUniqueID()
		{
			return base.IsRenderingScript && this.AutoPostBack;
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0001CD05 File Offset: 0x0001AF05
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0001CD0D File Offset: 0x0001AF0D
		[DefaultValue(false)]
		[Description("Change tab header placement vertically when value set to true")]
		[Category("Appearance")]
		[ClientPropertyName("useVerticalStripPlacement")]
		public bool UseVerticalStripPlacement
		{
			get
			{
				return this._useVerticalStripPlacement;
			}
			set
			{
				this._useVerticalStripPlacement = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0001CD16 File Offset: 0x0001AF16
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x0001CD1E File Offset: 0x0001AF1E
		[Description("Set width of tab strips when UseVerticalStripPlacement is set to true. Size must be in pixel")]
		[DefaultValue(typeof(Unit), "120px")]
		[Category("Appearance")]
		public Unit VerticalStripWidth
		{
			get
			{
				return this._verticalStripWidth;
			}
			set
			{
				if (!value.IsEmpty && value.Type != UnitType.Pixel)
				{
					throw new ArgumentOutOfRangeException("value", "VerticalStripWidth must be set in pixels only, or Empty.");
				}
				this._verticalStripWidth = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0001CD4A File Offset: 0x0001AF4A
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x0001CD52 File Offset: 0x0001AF52
		[ClientPropertyName("onDemand")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool OnDemand
		{
			get
			{
				return this._onDemand;
			}
			set
			{
				this._onDemand = value;
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0001CD5C File Offset: 0x0001AF5C
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.RegisterRequiresControlState(this);
			this._initialized = true;
			if (this._cachedActiveTabIndex > -1)
			{
				this.ActiveTabIndex = this._cachedActiveTabIndex;
				if (this.ActiveTabIndex < this.Tabs.Count)
				{
					this.Tabs[this.ActiveTabIndex].Active = true;
					return;
				}
			}
			else if (this.Tabs.Count > 0)
			{
				this.ActiveTabIndex = 0;
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0001CDD8 File Offset: 0x0001AFD8
		protected virtual void OnActiveTabChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TabContainer.EventActiveTabChanged] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0001CE08 File Offset: 0x0001B008
		protected override void AddParsedSubObject(object obj)
		{
			TabPanel tabPanel = obj as TabPanel;
			if (tabPanel != null)
			{
				this.Controls.Add(tabPanel);
				return;
			}
			if (!(obj is LiteralControl))
			{
				throw new HttpException(string.Format(CultureInfo.CurrentCulture, "TabContainer cannot have children of type '{0}'.", new object[]
				{
					obj.GetType()
				}));
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0001CE5A File Offset: 0x0001B05A
		protected override void AddedControl(Control control, int index)
		{
			((TabPanel)control).SetOwner(this);
			base.AddedControl(control, index);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0001CE70 File Offset: 0x0001B070
		protected override void RemovedControl(Control control)
		{
			TabPanel tabPanel = control as TabPanel;
			if (control != null && tabPanel.Active && this.ActiveTabIndex < this.Tabs.Count)
			{
				this.EnsureActiveTab();
			}
			tabPanel.SetOwner(null);
			base.RemovedControl(control);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0001CEB6 File Offset: 0x0001B0B6
		protected override ControlCollection CreateControlCollection()
		{
			return new TabPanelCollection(this);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0001CEC0 File Offset: 0x0001B0C0
		protected override Style CreateControlStyle()
		{
			return new TabContainer.TabContainerStyle(this.ViewState)
			{
				CssClass = this.CssClass
			};
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		private int GetServerActiveTabIndex(int clientActiveTabIndex)
		{
			int num = -1;
			int result = clientActiveTabIndex;
			for (int i = 0; i < this.Tabs.Count; i++)
			{
				if (this.Tabs[i].Visible)
				{
					num++;
				}
				if (num == clientActiveTabIndex)
				{
					result = i;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0001CF30 File Offset: 0x0001B130
		protected override void LoadClientState(string clientState)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)new JavaScriptSerializer().DeserializeObject(clientState);
			if (dictionary != null)
			{
				this.ActiveTabIndex = (int)dictionary["ActiveTabIndex"];
				this.ActiveTabIndex = this.GetServerActiveTabIndex(this.ActiveTabIndex);
				object[] array = (object[])dictionary["TabEnabledState"];
				object[] array2 = (object[])dictionary["TabWasLoadedOnceState"];
				for (int i = 0; i < array.Length; i++)
				{
					int serverActiveTabIndex = this.GetServerActiveTabIndex(i);
					if (serverActiveTabIndex < this.Tabs.Count)
					{
						this.Tabs[serverActiveTabIndex].Enabled = (bool)array[i];
						this.Tabs[serverActiveTabIndex].WasLoadedOnce = (bool)array2[i];
					}
				}
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0001CFF8 File Offset: 0x0001B1F8
		protected override string SaveClientState()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["ActiveTabIndex"] = this.ActiveTabIndex;
			List<object> list = new List<object>();
			List<object> list2 = new List<object>();
			foreach (object obj in this.Tabs)
			{
				TabPanel tabPanel = (TabPanel)obj;
				list.Add(tabPanel.Enabled);
				list2.Add(tabPanel.WasLoadedOnce);
			}
			dictionary["TabEnabledState"] = list;
			dictionary["TabWasLoadedOnceState"] = list2;
			return new JavaScriptSerializer().Serialize(dictionary);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0001D0C0 File Offset: 0x0001B2C0
		protected override void LoadControlState(object savedState)
		{
			Pair pair = (Pair)savedState;
			if (pair != null)
			{
				base.LoadControlState(pair.First);
				this.ActiveTabIndex = (int)pair.Second;
				return;
			}
			base.LoadControlState(null);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0001D0FC File Offset: 0x0001B2FC
		protected override object SaveControlState()
		{
			this.LastActiveTabIndex = this.ActiveTabIndex;
			Pair pair = new Pair();
			pair.First = base.SaveControlState();
			pair.Second = this.ActiveTabIndex;
			if (pair.First == null && pair.Second == null)
			{
				return null;
			}
			return pair;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0001D14C File Offset: 0x0001B34C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.Style.Remove(HtmlTextWriterStyle.Visibility);
			if (!base.ControlStyleCreated && !string.IsNullOrWhiteSpace(this.CssClass))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
			}
			if (this._useVerticalStripPlacement)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "block");
			}
			if (!this.Height.IsEmpty && this.Height.Type == UnitType.Percentage)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
			}
			base.AddAttributesToRender(writer);
			writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0001D1F0 File Offset: 0x0001B3F0
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Page.VerifyRenderingInServerForm(this);
			if (this._tabStripPlacement == TabStripPlacement.Top || this._tabStripPlacement == TabStripPlacement.TopRight || (this._tabStripPlacement == TabStripPlacement.Bottom && this._useVerticalStripPlacement) || (this._tabStripPlacement == TabStripPlacement.BottomRight && this._useVerticalStripPlacement))
			{
				this.RenderHeader(writer);
			}
			if (!this.Height.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_body");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ajax__tab_body" + this.GetSuffixTabStripPlacementCss());
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "block");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderChildren(writer);
			writer.RenderEndTag();
			if ((this._tabStripPlacement == TabStripPlacement.Bottom && !this._useVerticalStripPlacement) || (this._tabStripPlacement == TabStripPlacement.BottomRight && !this._useVerticalStripPlacement))
			{
				this.RenderHeader(writer);
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0001D2EC File Offset: 0x0001B4EC
		protected virtual void RenderHeader(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_header");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ajax__tab_header" + this.GetSuffixTabStripPlacementCss());
			if (this._tabStripPlacement == TabStripPlacement.BottomRight || this._tabStripPlacement == TabStripPlacement.TopRight)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Direction, "rtl");
			}
			if (this._useVerticalStripPlacement)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "block");
				if (this._tabStripPlacement == TabStripPlacement.Bottom || this._tabStripPlacement == TabStripPlacement.Top)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Style, "float:left");
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Style, "float:right");
				}
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this._verticalStripWidth.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (this._tabStripPlacement == TabStripPlacement.Bottom || this._tabStripPlacement == TabStripPlacement.BottomRight)
			{
				this.RenderSpannerForVerticalTabs(writer);
			}
			if (!this._useVerticalStripPlacement && (this._tabStripPlacement == TabStripPlacement.BottomRight || this._tabStripPlacement == TabStripPlacement.TopRight))
			{
				int count = this.Tabs.Count;
				for (int i = count - 1; i >= 0; i--)
				{
					TabPanel tabPanel = this.Tabs[i];
					if (tabPanel.Visible)
					{
						tabPanel.RenderHeader(writer);
					}
				}
			}
			else
			{
				foreach (object obj in this.Tabs)
				{
					TabPanel tabPanel2 = (TabPanel)obj;
					if (tabPanel2.Visible)
					{
						tabPanel2.RenderHeader(writer);
					}
				}
			}
			if (this._tabStripPlacement == TabStripPlacement.Top || this._tabStripPlacement == TabStripPlacement.TopRight)
			{
				this.RenderSpannerForVerticalTabs(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0001D490 File Offset: 0x0001B690
		private void RenderSpannerForVerticalTabs(HtmlTextWriter writer)
		{
			if (!this._useVerticalStripPlacement)
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_headerSpannerHeight");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "block");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0001D4D0 File Offset: 0x0001B6D0
		private string GetSuffixTabStripPlacementCss()
		{
			string text = "";
			if (this._useVerticalStripPlacement)
			{
				text += "_vertical";
				switch (this._tabStripPlacement)
				{
				case TabStripPlacement.Top:
				case TabStripPlacement.Bottom:
					text += "left";
					break;
				case TabStripPlacement.TopRight:
				case TabStripPlacement.BottomRight:
					text += "right";
					break;
				}
			}
			else
			{
				switch (this._tabStripPlacement)
				{
				case TabStripPlacement.Bottom:
				case TabStripPlacement.BottomRight:
					text = "_bottom";
					break;
				}
			}
			return text;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0001D558 File Offset: 0x0001B758
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			int activeTabIndex = this.ActiveTabIndex;
			bool flag = base.LoadPostData(postDataKey, postCollection);
			return this.ActiveTabIndex == 0 || activeTabIndex != this.ActiveTabIndex || flag;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0001D58C File Offset: 0x0001B78C
		protected override void RaisePostDataChangedEvent()
		{
			if (this.LastActiveTabIndex == this.ActiveTabIndex)
			{
				return;
			}
			this.LastActiveTabIndex = this.ActiveTabIndex;
			TabPanel activeTab = this.ActiveTab;
			if (activeTab != null && (activeTab.OnDemandMode == OnDemandMode.Always || (activeTab.OnDemandMode == OnDemandMode.Once && !activeTab.WasLoadedOnce)))
			{
				this.OnActiveTabChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0001D5E4 File Offset: 0x0001B7E4
		private void EnsureActiveTab()
		{
			if (this._activeTabIndex < 0 || this._activeTabIndex >= this.Tabs.Count)
			{
				this._activeTabIndex = 0;
			}
			for (int i = 0; i < this.Tabs.Count; i++)
			{
				this.Tabs[i].Active = (i == this.ActiveTabIndex);
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0001D644 File Offset: 0x0001B844
		public void ResetLoadedOnceTabs()
		{
			foreach (object obj in this.Tabs)
			{
				TabPanel tabPanel = (TabPanel)obj;
				if (tabPanel.OnDemandMode == OnDemandMode.Once && tabPanel.WasLoadedOnce)
				{
					tabPanel.WasLoadedOnce = false;
				}
			}
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0001D6B0 File Offset: 0x0001B8B0
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.StartsWith("activeTabChanged", StringComparison.Ordinal))
			{
				int num = eventArgument.IndexOf(":", StringComparison.Ordinal);
				if (num != -1 && int.TryParse(eventArgument.Substring(num + 1), out num))
				{
					num = this.GetServerActiveTabIndex(num);
					if (num != this.ActiveTabIndex)
					{
						this.ActiveTabIndex = num;
						this.OnActiveTabChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0001D714 File Offset: 0x0001B914
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("tabStripPlacement", this.TabStripPlacement);
			descriptor.AddProperty("useVerticalStripPlacement", this.UseVerticalStripPlacement);
			descriptor.AddProperty("onDemand", this.OnDemand);
		}

		// Token: 0x04000434 RID: 1076
		private static readonly object EventActiveTabChanged = new object();

		// Token: 0x04000435 RID: 1077
		private int _activeTabIndex = -1;

		// Token: 0x04000436 RID: 1078
		private int _cachedActiveTabIndex = -1;

		// Token: 0x04000437 RID: 1079
		private bool _initialized;

		// Token: 0x04000438 RID: 1080
		private bool _autoPostBack;

		// Token: 0x04000439 RID: 1081
		private TabStripPlacement _tabStripPlacement;

		// Token: 0x0400043A RID: 1082
		private bool _useVerticalStripPlacement;

		// Token: 0x0400043B RID: 1083
		private Unit _verticalStripWidth = new Unit(120.0, UnitType.Pixel);

		// Token: 0x0400043C RID: 1084
		private bool _onDemand;

		// Token: 0x0400043D RID: 1085
		private TabCssTheme _cssTheme = TabCssTheme.XP;

		// Token: 0x02000191 RID: 401
		private sealed class TabContainerStyle : Style
		{
			// Token: 0x06000B75 RID: 2933 RVA: 0x0001D776 File Offset: 0x0001B976
			public TabContainerStyle(StateBag state) : base(state)
			{
			}

			// Token: 0x06000B76 RID: 2934 RVA: 0x0001D77F File Offset: 0x0001B97F
			protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
			{
				base.FillStyleAttributes(attributes, urlResolver);
				attributes.Remove(HtmlTextWriterStyle.Height);
				attributes.Remove(HtmlTextWriterStyle.BackgroundImage);
			}
		}
	}
}
