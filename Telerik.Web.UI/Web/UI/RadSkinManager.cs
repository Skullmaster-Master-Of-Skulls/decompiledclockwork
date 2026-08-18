using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02001ABF RID: 6847
	[ToolboxBitmap(typeof(RadSkinManager), "Telerik.Web.UI.SkinManager.png")]
	[LightweightRendering]
	[TelerikToolboxCategory("Miscellaneous")]
	[ToolboxData("<{0}:RadSkinManager Runat=server></{0}:RadSkinManager>")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[Designer("Telerik.Web.Design.RadSkinManagerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	public class RadSkinManager : Control, ISkinnableControl, IControl
	{
		// Token: 0x140001E2 RID: 482
		// (add) Token: 0x0601090A RID: 67850 RVA: 0x003B2138 File Offset: 0x003B0338
		// (remove) Token: 0x0601090B RID: 67851 RVA: 0x003B2170 File Offset: 0x003B0370
		[Category("Action")]
		public event RadSkinManager.SkinChangingDelegate SkinChanging;

		// Token: 0x0601090C RID: 67852 RVA: 0x003B21A5 File Offset: 0x003B03A5
		protected virtual void OnSkinChanging(SkinChangingEventArgs args)
		{
			if (this.SkinChanging != null)
			{
				this.SkinChanging(this, args);
			}
		}

		// Token: 0x140001E3 RID: 483
		// (add) Token: 0x0601090D RID: 67853 RVA: 0x003B21BC File Offset: 0x003B03BC
		// (remove) Token: 0x0601090E RID: 67854 RVA: 0x003B21F4 File Offset: 0x003B03F4
		[Category("Action")]
		public event RadSkinManager.SkinChangedDelegate SkinChanged;

		// Token: 0x0601090F RID: 67855 RVA: 0x003B2229 File Offset: 0x003B0429
		protected virtual void OnSkinChanged(SkinChangedEventArgs args)
		{
			if (this.SkinChanged != null)
			{
				this.SkinChanged(this, args);
			}
		}

		// Token: 0x1700508B RID: 20619
		// (get) Token: 0x06010910 RID: 67856 RVA: 0x003B2240 File Offset: 0x003B0440
		// (set) Token: 0x06010911 RID: 67857 RVA: 0x003B226B File Offset: 0x003B046B
		[Description("Gets or sets a value indicating whether Skin chooser should be rendered in run-time.")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public virtual bool ShowChooser
		{
			get
			{
				return this.ViewState["ShowChooser"] != null && (bool)this.ViewState["ShowChooser"];
			}
			set
			{
				this.ViewState["ShowChooser"] = value;
			}
		}

		// Token: 0x1700508C RID: 20620
		// (get) Token: 0x06010912 RID: 67858 RVA: 0x003B2283 File Offset: 0x003B0483
		// (set) Token: 0x06010913 RID: 67859 RVA: 0x003B22AE File Offset: 0x003B04AE
		[Description("Gets or sets a value indicating whether skinning should be enabled or not.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool Enabled
		{
			get
			{
				return this.ViewState["Enabled"] == null || (bool)this.ViewState["Enabled"];
			}
			set
			{
				this.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x1700508D RID: 20621
		// (get) Token: 0x06010914 RID: 67860 RVA: 0x003B22C6 File Offset: 0x003B04C6
		// (set) Token: 0x06010915 RID: 67861 RVA: 0x003B22F5 File Offset: 0x003B04F5
		[NotifyParentProperty(true)]
		[Description("Specifies the skin that will be used by the control.")]
		[SimplePersistenceSetting]
		[Editor("Telerik.Web.Design.RadSkinManagerSkinEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public virtual string Skin
		{
			get
			{
				if (this.ViewState["Skin"] == null)
				{
					return "";
				}
				return (string)this.ViewState["Skin"];
			}
			set
			{
				this.ViewState["Skin"] = value;
			}
		}

		// Token: 0x1700508E RID: 20622
		// (get) Token: 0x06010916 RID: 67862 RVA: 0x003B2308 File Offset: 0x003B0508
		// (set) Token: 0x06010917 RID: 67863 RVA: 0x003B2337 File Offset: 0x003B0537
		[DefaultValue("Telerik.Skin")]
		[NotifyParentProperty(true)]
		[Description("Specifies the persistance key that will be used by the control.")]
		public virtual string PersistenceKey
		{
			get
			{
				if (this.ViewState["PersistenceKey"] == null)
				{
					return "Telerik.Skin";
				}
				return (string)this.ViewState["PersistenceKey"];
			}
			set
			{
				if (string.IsNullOrEmpty(this.PersistenceKey))
				{
					throw new Exception("PersistenceKey cannot be null or empty string!");
				}
				this.ViewState["PersistenceKey"] = value;
			}
		}

		// Token: 0x1700508F RID: 20623
		// (get) Token: 0x06010918 RID: 67864 RVA: 0x003B2362 File Offset: 0x003B0562
		// (set) Token: 0x06010919 RID: 67865 RVA: 0x003B238D File Offset: 0x003B058D
		[Description("Specifies the skin manager persistance mode.")]
		[DefaultValue(typeof(RadSkinManagerPersistenceMode), "ViewState")]
		[NotifyParentProperty(true)]
		public virtual RadSkinManagerPersistenceMode PersistenceMode
		{
			get
			{
				if (this.ViewState["PersistenceMode"] == null)
				{
					return RadSkinManagerPersistenceMode.ViewState;
				}
				return (RadSkinManagerPersistenceMode)this.ViewState["PersistenceMode"];
			}
			set
			{
				this.ViewState["PersistenceMode"] = value;
			}
		}

		// Token: 0x17005090 RID: 20624
		// (get) Token: 0x0601091A RID: 67866 RVA: 0x003B23A5 File Offset: 0x003B05A5
		[MergableProperty(false)]
		[PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public TargetControlCollection TargetControls
		{
			get
			{
				if (this._targetControls == null)
				{
					this._targetControls = new TargetControlCollection();
				}
				return this._targetControls;
			}
		}

		// Token: 0x17005091 RID: 20625
		// (get) Token: 0x0601091B RID: 67867 RVA: 0x003B23C0 File Offset: 0x003B05C0
		[PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public SkinReferenceCollection Skins
		{
			get
			{
				if (this._skinReferences == null)
				{
					this._skinReferences = new SkinReferenceCollection();
				}
				return this._skinReferences;
			}
		}

		// Token: 0x17005092 RID: 20626
		// (get) Token: 0x0601091C RID: 67868 RVA: 0x003B23DB File Offset: 0x003B05DB
		// (set) Token: 0x0601091D RID: 67869 RVA: 0x003B23F7 File Offset: 0x003B05F7
		internal CustomNonEmbeddedSkinsCollection CustomNonEmbeddedSkins
		{
			get
			{
				if (this._customNonEmbeddedSkins == null)
				{
					this._customNonEmbeddedSkins = this.FillNonEmbeddedSkinsInfo();
				}
				return this._customNonEmbeddedSkins;
			}
			set
			{
				this._customNonEmbeddedSkins = value;
			}
		}

		// Token: 0x0601091E RID: 67870 RVA: 0x003B2400 File Offset: 0x003B0600
		private CustomNonEmbeddedSkinsCollection FillNonEmbeddedSkinsInfo()
		{
			CustomNonEmbeddedSkinsCollection customNonEmbeddedSkinsCollection = new CustomNonEmbeddedSkinsCollection();
			foreach (object obj in this.Skins)
			{
				SkinReference skinReference = (SkinReference)obj;
				if (!string.IsNullOrEmpty(skinReference.Path))
				{
					string text = HttpContext.Current.Server.MapPath(skinReference.Path);
					if (Directory.Exists(text))
					{
						string[] directories = Directory.GetDirectories(text);
						foreach (string text2 in directories)
						{
							string text3 = text2.Substring(text2.LastIndexOf(text) + text.Length).Trim(new char[]
							{
								'\\'
							});
							string[] directories2 = Directory.GetDirectories(text2);
							foreach (string text4 in directories2)
							{
								string value = text4.Substring(text2.LastIndexOf(text2) + text2.Length).Trim(new char[]
								{
									'\\'
								});
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.Append(text2).Append("\\").Append(value).Append(".").Append(text3).Append(".css");
								if (File.Exists(stringBuilder.ToString()))
								{
									StringBuilder stringBuilder2 = new StringBuilder();
									StringBuilder stringBuilder3 = new StringBuilder();
									stringBuilder3.Append(value).Append(".").Append(text3).Append(".css");
									stringBuilder2.Append(skinReference.Path).Append("/").Append(text3).Append("/").Append(stringBuilder3.ToString());
									customNonEmbeddedSkinsCollection.Add(new CustomNonEmbeddedSkin
									{
										Url = stringBuilder2.ToString(),
										ResourceName = stringBuilder3.ToString(),
										Name = text3
									});
								}
							}
						}
					}
				}
			}
			return customNonEmbeddedSkinsCollection;
		}

		// Token: 0x0601091F RID: 67871 RVA: 0x003B2640 File Offset: 0x003B0840
		public void ApplySkin(Control target, string skin)
		{
			if (target == null || !target.Visible)
			{
				return;
			}
			ISkinnableControl skinnableControl = target as ISkinnableControl;
			if (skinnableControl != null && !skinnableControl.IsSkinSet)
			{
				skinnableControl.Skin = skin;
			}
			for (int i = 0; i < target.Controls.Count; i++)
			{
				if (target.Controls[i] is ISkinnableControl)
				{
					this.ApplySkinToControlsType(target.Controls[i]);
				}
				else
				{
					this.ApplySkin(target.Controls[i]);
				}
			}
		}

		// Token: 0x06010920 RID: 67872 RVA: 0x003B26C1 File Offset: 0x003B08C1
		public void ApplySkin(Control target)
		{
			this.ApplySkin(target, this.Skin);
		}

		// Token: 0x06010921 RID: 67873 RVA: 0x003B26D0 File Offset: 0x003B08D0
		public static RadSkinManager GetCurrent(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			return page.Items[typeof(RadSkinManager)] as RadSkinManager;
		}

		// Token: 0x06010922 RID: 67874 RVA: 0x003B26FA File Offset: 0x003B08FA
		public RadComboBox GetSkinChooser()
		{
			return this.chooser;
		}

		// Token: 0x06010923 RID: 67875 RVA: 0x003B2704 File Offset: 0x003B0904
		protected override void OnInit(EventArgs e)
		{
			if (this.SupportsRenderingMode)
			{
				this.InitializeRenderMode();
			}
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.InitComplete += this.OnPageInit;
				this.Page.PreRender += this.Page_PreRender;
			}
			this.chooser = new RadComboBox();
			this.chooser.ID = "SkinChooser";
			this.chooser.AutoPostBack = true;
			this.chooser.CausesValidation = false;
			this.chooser.RenderMode = this.ResolvedRenderMode;
			this.chooser.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
			this.chooser.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
			this.chooser.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
			this.Controls.Add(this.chooser);
			this.chooser.SelectedIndexChanged += this.chooser_SelectedIndexChanged;
			this.FillSkins(this.chooser);
		}

		// Token: 0x06010924 RID: 67876 RVA: 0x003B2808 File Offset: 0x003B0A08
		protected virtual void FillSkins(RadComboBox chooser)
		{
			if (chooser.Items.Count == 0)
			{
				this.skinAttributes = SkinRegistrar.GetAllEmbeddedSkins(this);
				List<string> list = new List<string>(this.skinAttributes.Keys);
				list.Sort();
				foreach (string text in list)
				{
					RadComboBoxItem item = new RadComboBoxItem(text, text);
					chooser.Items.Add(item);
				}
			}
		}

		// Token: 0x06010925 RID: 67877 RVA: 0x003B2894 File Offset: 0x003B0A94
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this.PersistenceMode == RadSkinManagerPersistenceMode.Session)
			{
				if (this.Page != null && this.Page.Session != null && !string.IsNullOrEmpty((string)this.Page.Session[this.PersistenceKey]))
				{
					this.Skin = (string)this.Page.Session[this.PersistenceKey];
					return;
				}
			}
			else if (this.PersistenceMode == RadSkinManagerPersistenceMode.Cookie && this.Page != null && this.Page.Request != null && this.Page.Request.Cookies != null && this.Page.Request.Cookies[this.PersistenceKey] != null && !string.IsNullOrEmpty(this.Page.Request.Cookies[this.PersistenceKey].Value))
			{
				this.Skin = this.Page.Request.Cookies[this.PersistenceKey].Value;
			}
		}

		// Token: 0x06010926 RID: 67878 RVA: 0x003B29B3 File Offset: 0x003B0BB3
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.chooser != null)
			{
				this.chooser.Visible = this.ShowChooser;
			}
		}

		// Token: 0x06010927 RID: 67879 RVA: 0x003B29D5 File Offset: 0x003B0BD5
		private void chooser_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			this.ChangeSkin(e.Value);
		}

		// Token: 0x06010928 RID: 67880 RVA: 0x003B29E4 File Offset: 0x003B0BE4
		protected void ChangeSkin(string skinName)
		{
			SkinChangingEventArgs skinChangingEventArgs = new SkinChangingEventArgs(skinName);
			this.OnSkinChanging(skinChangingEventArgs);
			if (skinChangingEventArgs.Canceled)
			{
				return;
			}
			this.Skin = skinName;
			if (this.PersistenceMode == RadSkinManagerPersistenceMode.Session)
			{
				if (this.Page != null && this.Page.Session != null)
				{
					this.Page.Session[this.PersistenceKey] = this.Skin;
				}
			}
			else if (this.PersistenceMode == RadSkinManagerPersistenceMode.Cookie && this.Page != null && this.Page.Response != null && this.Page.Response.Cookies != null)
			{
				string name = this.PersistenceKey.Replace("\n", " ").Replace("\r", " ");
				HttpCookie httpCookie = new HttpCookie(name);
				string value = this.Skin.Replace("\n", " ").Replace("\r", " ");
				httpCookie.Value = value;
				httpCookie.Expires = DateTime.Now.AddYears(1);
				this.Page.Response.SetCookie(httpCookie);
			}
			this.OnSkinChanged(new SkinChangedEventArgs(this.Skin));
		}

		// Token: 0x06010929 RID: 67881 RVA: 0x003B2B1C File Offset: 0x003B0D1C
		private void OnPageInit(object sender, EventArgs e)
		{
			this.Page.InitComplete -= this.OnPageInit;
			if (RadSkinManager.GetCurrent(this.Page) != null)
			{
				throw new InvalidOperationException("Only one instance of a RadSkinManager can be added to the page!");
			}
			this.Page.Items[typeof(RadSkinManager)] = this;
		}

		// Token: 0x0601092A RID: 67882 RVA: 0x003B2B74 File Offset: 0x003B0D74
		private void Page_PreRender(object sender, EventArgs e)
		{
			this.Page.PreRender -= this.Page_PreRender;
			if (!this.Enabled)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.Skin))
			{
				if (this.chooser != null && this.chooser.Items.FindItemByText(this.Skin) != null)
				{
					this.chooser.Items.FindItemByText(this.Skin).Selected = true;
				}
			}
			else if (this.chooser != null && this.chooser.Items.FindItemByText("Default") != null)
			{
				this.chooser.Items.FindItemByText("Default").Selected = true;
			}
			if (this.TargetControls.Count > 0)
			{
				Control control = this.Page;
				if (this.Page.Master != null)
				{
					control = this.Page.Master;
				}
				this.ApplySkinToControlsType(control);
				foreach (object obj in this.TargetControls)
				{
					TargetControl targetControl = (TargetControl)obj;
					if (targetControl.Enabled && !string.IsNullOrEmpty(targetControl.Skin))
					{
						Control control2 = ChildControlHelper.FindControlRecursive(this, targetControl.ControlID, null);
						if (control2 != null)
						{
							this.ApplySkin(control2, targetControl.Skin);
						}
					}
				}
			}
		}

		// Token: 0x0601092B RID: 67883 RVA: 0x003B2CDC File Offset: 0x003B0EDC
		private void ApplySkinToControlsType(Control control)
		{
			ISkinnableControl skinnableControl = control as ISkinnableControl;
			if (skinnableControl != null)
			{
				string text = this.TargetControls.ContainsType(skinnableControl);
				if (!string.IsNullOrEmpty(text))
				{
					this.ApplySkin(control, text);
				}
			}
			for (int i = 0; i < control.Controls.Count; i++)
			{
				this.ApplySkinToControlsType(control.Controls[i]);
			}
		}

		// Token: 0x0601092C RID: 67884 RVA: 0x003B2D38 File Offset: 0x003B0F38
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		List<string> ISkinnableControl.GetEmbeddedSkinNames()
		{
			return SkinRegistrar.GetEmbeddedSkinNames(base.GetType());
		}

		// Token: 0x17005093 RID: 20627
		// (get) Token: 0x0601092D RID: 67885 RVA: 0x003B2D45 File Offset: 0x003B0F45
		// (set) Token: 0x0601092E RID: 67886 RVA: 0x003B2D4D File Offset: 0x003B0F4D
		bool ISkinnableControl.EnableAjaxSkinRendering { get; set; }

		// Token: 0x0601092F RID: 67887 RVA: 0x003B2D58 File Offset: 0x003B0F58
		string ISkinnableControl.GetSkinSuffix()
		{
			if (!this.SupportsRenderingMode)
			{
				return string.Empty;
			}
			string renderingModeString = RenderModeHelper.GetRenderingModeString(this.ResolvedRenderMode);
			if (!(renderingModeString == "Classic"))
			{
				return renderingModeString;
			}
			return string.Empty;
		}

		// Token: 0x17005094 RID: 20628
		// (get) Token: 0x06010930 RID: 67888 RVA: 0x003B2D93 File Offset: 0x003B0F93
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsSkinSet
		{
			get
			{
				return this.ViewState["Skin"] != null;
			}
		}

		// Token: 0x17005095 RID: 20629
		// (get) Token: 0x06010931 RID: 67889 RVA: 0x003B2DAB File Offset: 0x003B0FAB
		// (set) Token: 0x06010932 RID: 67890 RVA: 0x003B2DB3 File Offset: 0x003B0FB3
		string ISkinnableControl.AjaxCssRegistrations { get; set; }

		// Token: 0x17005096 RID: 20630
		// (get) Token: 0x06010933 RID: 67891 RVA: 0x003B2DBC File Offset: 0x003B0FBC
		// (set) Token: 0x06010934 RID: 67892 RVA: 0x003B2DEC File Offset: 0x003B0FEC
		[DefaultValue(true)]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Whether to register the scripts automatically")]
		public bool EnableEmbeddedScripts
		{
			get
			{
				if (this.ViewState["EnableEmbeddedScripts"] == null)
				{
					return BaseClass.GetGlobalEnableEmbeddedScripts(this);
				}
				return (bool)this.ViewState["EnableEmbeddedScripts"];
			}
			set
			{
				this.ViewState["EnableEmbeddedScripts"] = value;
			}
		}

		// Token: 0x17005097 RID: 20631
		// (get) Token: 0x06010935 RID: 67893 RVA: 0x003B2E04 File Offset: 0x003B1004
		// (set) Token: 0x06010936 RID: 67894 RVA: 0x003B2E34 File Offset: 0x003B1034
		[DefaultValue(true)]
		[Description("Whether to register the selected skin automatically")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public bool EnableEmbeddedSkins
		{
			get
			{
				if (this.ViewState["EnableEmbeddedSkins"] == null)
				{
					return BaseClass.GetGlobalEnableEmbeddedSkins(this);
				}
				return (bool)this.ViewState["EnableEmbeddedSkins"];
			}
			set
			{
				this.ViewState["EnableEmbeddedSkins"] = value;
			}
		}

		// Token: 0x17005098 RID: 20632
		// (get) Token: 0x06010937 RID: 67895 RVA: 0x003B2E4C File Offset: 0x003B104C
		// (set) Token: 0x06010938 RID: 67896 RVA: 0x003B2E7C File Offset: 0x003B107C
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("Whether to register the base control skin file automatically")]
		[Category("Appearance")]
		public bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				if (this.ViewState["EnableEmbeddedBaseStylesheet"] == null)
				{
					return BaseClass.GetGlobalEnableEmbeddedBaseStylesheet(this);
				}
				return (bool)this.ViewState["EnableEmbeddedBaseStylesheet"];
			}
			set
			{
				this.ViewState["EnableEmbeddedBaseStylesheet"] = value;
			}
		}

		// Token: 0x06010939 RID: 67897 RVA: 0x003B2E94 File Offset: 0x003B1094
		void IControl.EnsureChildControlsCreated()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17005099 RID: 20633
		// (get) Token: 0x0601093A RID: 67898 RVA: 0x003B2E9B File Offset: 0x003B109B
		// (set) Token: 0x0601093B RID: 67899 RVA: 0x003B2EA3 File Offset: 0x003B10A3
		bool IControl.RegisterWithScriptManager { get; set; }

		// Token: 0x0601093C RID: 67900 RVA: 0x003B2EAC File Offset: 0x003B10AC
		void IControl.DescribeComponent(IScriptDescriptor descriptor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700509A RID: 20634
		// (get) Token: 0x0601093D RID: 67901 RVA: 0x003B2EB3 File Offset: 0x003B10B3
		// (set) Token: 0x0601093E RID: 67902 RVA: 0x003B2ED4 File Offset: 0x003B10D4
		[DefaultValue(RenderMode.Classic)]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Specifies the rendering mode of the control")]
		public RenderMode RenderMode
		{
			get
			{
				return (RenderMode)(this.ViewState["RenderMode"] ?? RenderMode.Classic);
			}
			set
			{
				this.ViewState["RenderMode"] = value;
				this._renderModeSet = true;
			}
		}

		// Token: 0x1700509B RID: 20635
		// (get) Token: 0x0601093F RID: 67903 RVA: 0x003B2EF4 File Offset: 0x003B10F4
		// (set) Token: 0x06010940 RID: 67904 RVA: 0x003B2F50 File Offset: 0x003B1150
		[Description("Returns resolved RenderMode should the original value was Auto")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RenderMode ResolvedRenderMode
		{
			get
			{
				if (!base.DesignMode)
				{
					if (this.ViewState["ResolvedRenderMode"] == null || this.ViewState.IsItemDirty("RenderMode"))
					{
						this.ResolvedRenderMode = this.ResolveRenderMode();
					}
					return (RenderMode)this.ViewState["ResolvedRenderMode"];
				}
				return RenderMode.Classic;
			}
			private set
			{
				this.ViewState["ResolvedRenderMode"] = value;
			}
		}

		// Token: 0x06010941 RID: 67905 RVA: 0x003B2F68 File Offset: 0x003B1168
		protected RenderMode ResolveRenderMode()
		{
			RenderMode renderMode = this.SupportsRenderingMode ? this.RenderMode : RenderMode.Classic;
			if (renderMode == RenderMode.Classic)
			{
				return renderMode;
			}
			RenderModeBrowserAdaptor instance = RenderModeBrowserAdaptor.Instance;
			if (this.CanRenderInMode(instance, renderMode))
			{
				return renderMode;
			}
			return ((ISkinnableControl)this).PreferredRenderMode(instance);
		}

		// Token: 0x06010942 RID: 67906 RVA: 0x003B2FA6 File Offset: 0x003B11A6
		protected internal bool CanRenderInMode(RenderModeBrowserAdaptor browser, RenderMode mode)
		{
			if (mode == RenderMode.Native)
			{
				return this.SupportsNativeRendering;
			}
			if (mode == RenderMode.Mobile)
			{
				return this.SupportsAdaptiveRendering;
			}
			return mode == RenderMode.Lightweight && browser.IsModernBrowser && this.SupportsLightweightRendering;
		}

		// Token: 0x06010943 RID: 67907 RVA: 0x003B2FD2 File Offset: 0x003B11D2
		RenderMode ISkinnableControl.PreferredRenderMode(RenderModeBrowserAdaptor browser)
		{
			if (this.RenderMode != RenderMode.Auto && !this.CanRenderInMode(browser, RenderMode.Lightweight))
			{
				return RenderMode.Classic;
			}
			if (this.SupportsAdaptiveRendering && browser.IsMobileDevice)
			{
				return RenderMode.Mobile;
			}
			if (this.CanRenderInMode(browser, RenderMode.Lightweight))
			{
				return RenderMode.Lightweight;
			}
			return RenderMode.Classic;
		}

		// Token: 0x1700509C RID: 20636
		// (get) Token: 0x06010944 RID: 67908 RVA: 0x003B3007 File Offset: 0x003B1207
		protected internal bool SupportsAdaptiveRendering
		{
			get
			{
				return RenderModesCache.GetAdaptiveTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x1700509D RID: 20637
		// (get) Token: 0x06010945 RID: 67909 RVA: 0x003B3019 File Offset: 0x003B1219
		protected internal bool SupportsNativeRendering
		{
			get
			{
				return RenderModesCache.GetNativeTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x1700509E RID: 20638
		// (get) Token: 0x06010946 RID: 67910 RVA: 0x003B302B File Offset: 0x003B122B
		protected internal bool SupportsLightweightRendering
		{
			get
			{
				return RenderModesCache.GetLightweightTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x1700509F RID: 20639
		// (get) Token: 0x06010947 RID: 67911 RVA: 0x003B303D File Offset: 0x003B123D
		protected internal bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170050A0 RID: 20640
		// (get) Token: 0x06010948 RID: 67912 RVA: 0x003B3040 File Offset: 0x003B1240
		protected bool IsRenderModeSet
		{
			get
			{
				return this._renderModeSet;
			}
		}

		// Token: 0x06010949 RID: 67913 RVA: 0x003B3048 File Offset: 0x003B1248
		protected internal void InitializeRenderMode()
		{
			if (!this.IsRenderModeSet)
			{
				if (RenderModeConfigurationReader.Instance.HasGlobalKey())
				{
					this.RenderMode = RenderModeConfigurationReader.Instance.GetRenderMode(this);
				}
				if (RenderModeConfigurationReader.Instance.HasKey(base.GetType()))
				{
					this.RenderMode = RenderModeConfigurationReader.Instance.GetRenderMode(base.GetType(), this);
				}
			}
		}

		// Token: 0x04004A0C RID: 18956
		private bool _renderModeSet;

		// Token: 0x04004A0F RID: 18959
		private TargetControlCollection _targetControls;

		// Token: 0x04004A10 RID: 18960
		private SkinReferenceCollection _skinReferences;

		// Token: 0x04004A11 RID: 18961
		private CustomNonEmbeddedSkinsCollection _customNonEmbeddedSkins;

		// Token: 0x04004A12 RID: 18962
		private RadComboBox chooser;

		// Token: 0x04004A13 RID: 18963
		protected Dictionary<string, EmbeddedSkinAttribute> skinAttributes;

		// Token: 0x02001AC0 RID: 6848
		// (Invoke) Token: 0x0601094C RID: 67916
		public delegate void SkinChangingDelegate(object sender, SkinChangingEventArgs e);

		// Token: 0x02001AC1 RID: 6849
		// (Invoke) Token: 0x06010950 RID: 67920
		public delegate void SkinChangedDelegate(object sender, SkinChangedEventArgs e);
	}
}
