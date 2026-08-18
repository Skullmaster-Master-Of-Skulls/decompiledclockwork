using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200052F RID: 1327
	public abstract class CatalogZoneBase : ToolZone, IPostBackDataHandler
	{
		// Token: 0x06004334 RID: 17204 RVA: 0x000DD1B3 File Offset: 0x000DB3B3
		protected CatalogZoneBase() : base(WebPartManager.CatalogDisplayMode)
		{
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x06004335 RID: 17205 RVA: 0x000DD1C0 File Offset: 0x000DB3C0
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("CatalogZoneBase_AddVerb")]
		public virtual WebPartVerb AddVerb
		{
			get
			{
				if (this._addVerb == null)
				{
					this._addVerb = new WebPartCatalogAddVerb();
					this._addVerb.EventArgument = "add";
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._addVerb).TrackViewState();
					}
				}
				return this._addVerb;
			}
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06004336 RID: 17206 RVA: 0x000DD1FE File Offset: 0x000DB3FE
		internal string CheckBoxName
		{
			get
			{
				return this.UniqueID + "$_checkbox";
			}
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06004337 RID: 17207 RVA: 0x000DD210 File Offset: 0x000DB410
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CatalogPartChrome CatalogPartChrome
		{
			get
			{
				if (this._catalogPartChrome == null)
				{
					this._catalogPartChrome = this.CreateCatalogPartChrome();
				}
				return this._catalogPartChrome;
			}
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06004338 RID: 17208 RVA: 0x000DD22C File Offset: 0x000DB42C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CatalogPartCollection CatalogParts
		{
			get
			{
				if (this._catalogParts == null)
				{
					CatalogPartCollection catalogPartCollection = this.CreateCatalogParts();
					if (!base.DesignMode)
					{
						foreach (object obj in catalogPartCollection)
						{
							CatalogPart catalogPart = (CatalogPart)obj;
							if (string.IsNullOrEmpty(catalogPart.ID))
							{
								throw new InvalidOperationException(SR.GetString("CatalogZoneBase_NoCatalogPartID"));
							}
						}
					}
					this._catalogParts = catalogPartCollection;
					this.EnsureChildControls();
				}
				return this._catalogParts;
			}
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06004339 RID: 17209 RVA: 0x000DD2C0 File Offset: 0x000DB4C0
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("CatalogZoneBase_CloseVerb")]
		public virtual WebPartVerb CloseVerb
		{
			get
			{
				if (this._closeVerb == null)
				{
					this._closeVerb = new WebPartCatalogCloseVerb();
					this._closeVerb.EventArgument = "close";
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._closeVerb).TrackViewState();
					}
				}
				return this._closeVerb;
			}
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x0600433A RID: 17210 RVA: 0x000DD300 File Offset: 0x000DB500
		// (set) Token: 0x0600433B RID: 17211 RVA: 0x000DD332 File Offset: 0x000DB532
		[WebSysDefaultValue("CatalogZoneBase_DefaultEmptyZoneText")]
		public override string EmptyZoneText
		{
			get
			{
				string text = (string)this.ViewState["EmptyZoneText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("CatalogZoneBase_DefaultEmptyZoneText");
			}
			set
			{
				this.ViewState["EmptyZoneText"] = value;
			}
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x0600433C RID: 17212 RVA: 0x000DD348 File Offset: 0x000DB548
		// (set) Token: 0x0600433D RID: 17213 RVA: 0x000A0A1D File Offset: 0x0009EC1D
		[WebSysDefaultValue("CatalogZoneBase_HeaderText")]
		public override string HeaderText
		{
			get
			{
				string text = (string)this.ViewState["HeaderText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("CatalogZoneBase_HeaderText");
			}
			set
			{
				this.ViewState["HeaderText"] = value;
			}
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x0600433E RID: 17214 RVA: 0x000DD37C File Offset: 0x000DB57C
		// (set) Token: 0x0600433F RID: 17215 RVA: 0x0008B81D File Offset: 0x00089A1D
		[WebSysDefaultValue("CatalogZoneBase_InstructionText")]
		public override string InstructionText
		{
			get
			{
				string text = (string)this.ViewState["InstructionText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("CatalogZoneBase_InstructionText");
			}
			set
			{
				this.ViewState["InstructionText"] = value;
			}
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06004340 RID: 17216 RVA: 0x000DD3AE File Offset: 0x000DB5AE
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("CatalogZoneBase_PartLinkStyle")]
		public Style PartLinkStyle
		{
			get
			{
				if (this._partLinkStyle == null)
				{
					this._partLinkStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._partLinkStyle).TrackViewState();
					}
				}
				return this._partLinkStyle;
			}
		}

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06004341 RID: 17217 RVA: 0x000DD3DC File Offset: 0x000DB5DC
		// (set) Token: 0x06004342 RID: 17218 RVA: 0x000DD430 File Offset: 0x000DB630
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("CatalogZoneBase_SelectedCatalogPartID")]
		public string SelectedCatalogPartID
		{
			get
			{
				if (!string.IsNullOrEmpty(this._selectedCatalogPartID))
				{
					return this._selectedCatalogPartID;
				}
				if (base.DesignMode)
				{
					return string.Empty;
				}
				CatalogPartCollection catalogParts = this.CatalogParts;
				if (catalogParts != null && catalogParts.Count > 0)
				{
					return catalogParts[0].ID;
				}
				return string.Empty;
			}
			set
			{
				this._selectedCatalogPartID = value;
			}
		}

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06004343 RID: 17219 RVA: 0x000DD43C File Offset: 0x000DB63C
		private CatalogPart SelectedCatalogPart
		{
			get
			{
				CatalogPartCollection catalogParts = this.CatalogParts;
				if (catalogParts == null || catalogParts.Count <= 0)
				{
					return null;
				}
				if (string.IsNullOrEmpty(this._selectedCatalogPartID))
				{
					return catalogParts[0];
				}
				return catalogParts[this._selectedCatalogPartID];
			}
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06004344 RID: 17220 RVA: 0x000DD480 File Offset: 0x000DB680
		// (set) Token: 0x06004345 RID: 17221 RVA: 0x000DD4B2 File Offset: 0x000DB6B2
		[Localizable(true)]
		[WebSysDefaultValue("CatalogZoneBase_DefaultSelectTargetZoneText")]
		[WebCategory("Behavior")]
		[WebSysDescription("CatalogZoneBase_SelectTargetZoneText")]
		public virtual string SelectTargetZoneText
		{
			get
			{
				string text = (string)this.ViewState["SelectTargetZoneText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("CatalogZoneBase_DefaultSelectTargetZoneText");
			}
			set
			{
				this.ViewState["SelectTargetZoneText"] = value;
			}
		}

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06004346 RID: 17222 RVA: 0x000DD4C5 File Offset: 0x000DB6C5
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("CatalogZoneBase_SelectedPartLinkStyle")]
		public Style SelectedPartLinkStyle
		{
			get
			{
				if (this._selectedPartLinkStyle == null)
				{
					this._selectedPartLinkStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._selectedPartLinkStyle).TrackViewState();
					}
				}
				return this._selectedPartLinkStyle;
			}
		}

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06004347 RID: 17223 RVA: 0x000DD4F4 File Offset: 0x000DB6F4
		// (set) Token: 0x06004348 RID: 17224 RVA: 0x000DD51D File Offset: 0x000DB71D
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		[WebSysDescription("CatalogZoneBase_ShowCatalogIcons")]
		public virtual bool ShowCatalogIcons
		{
			get
			{
				object obj = this.ViewState["ShowCatalogIcons"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowCatalogIcons"] = value;
			}
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x06004349 RID: 17225 RVA: 0x000DD535 File Offset: 0x000DB735
		private string ZonesID
		{
			get
			{
				return this.UniqueID + "$_zones";
			}
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x000DD548 File Offset: 0x000DB748
		private void AddSelectedWebParts()
		{
			WebPartZoneBase webPartZoneBase = null;
			if (base.WebPartManager != null)
			{
				webPartZoneBase = base.WebPartManager.Zones[this._selectedZoneID];
			}
			CatalogPart selectedCatalogPart = this.SelectedCatalogPart;
			WebPartDescriptionCollection webPartDescriptionCollection = null;
			if (selectedCatalogPart != null)
			{
				webPartDescriptionCollection = selectedCatalogPart.GetAvailableWebPartDescriptions();
			}
			if (webPartZoneBase != null && webPartZoneBase.AllowLayoutChange && this._selectedCheckBoxValues != null && webPartDescriptionCollection != null)
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this._selectedCheckBoxValues.Length; i++)
				{
					string id = this._selectedCheckBoxValues[i];
					WebPartDescription webPartDescription = webPartDescriptionCollection[id];
					if (webPartDescription != null)
					{
						WebPart webPart = selectedCatalogPart.GetWebPart(webPartDescription);
						if (webPart != null)
						{
							arrayList.Add(webPart);
						}
					}
				}
				this.AddWebParts(arrayList, webPartZoneBase);
			}
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x000DD5F4 File Offset: 0x000DB7F4
		private void AddWebParts(ArrayList webParts, WebPartZoneBase zone)
		{
			webParts.Reverse();
			foreach (object obj in webParts)
			{
				WebPart webPart = (WebPart)obj;
				WebPartZoneBase zone2 = zone;
				if (!webPart.AllowZoneChange && webPart.Zone != null)
				{
					zone2 = webPart.Zone;
				}
				base.WebPartManager.AddWebPart(webPart, zone2, 0);
			}
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x000DD670 File Offset: 0x000DB870
		protected override void Close()
		{
			if (base.WebPartManager != null)
			{
				base.WebPartManager.DisplayMode = WebPartManager.BrowseDisplayMode;
			}
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x000DD68A File Offset: 0x000DB88A
		protected virtual CatalogPartChrome CreateCatalogPartChrome()
		{
			return new CatalogPartChrome(this);
		}

		// Token: 0x0600434E RID: 17230
		protected abstract CatalogPartCollection CreateCatalogParts();

		// Token: 0x0600434F RID: 17231 RVA: 0x000DD694 File Offset: 0x000DB894
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			foreach (object obj in this.CatalogParts)
			{
				CatalogPart catalogPart = (CatalogPart)obj;
				catalogPart.SetWebPartManager(base.WebPartManager);
				catalogPart.SetZone(this);
				this.Controls.Add(catalogPart);
			}
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x000DD710 File Offset: 0x000DB910
		internal string GetCheckBoxID(string value)
		{
			return string.Concat(new string[]
			{
				this.ClientID,
				base.ClientIDSeparator.ToString(),
				"_checkbox",
				base.ClientIDSeparator.ToString(),
				value
			});
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x000DD75F File Offset: 0x000DB95F
		protected void InvalidateCatalogParts()
		{
			this._catalogParts = null;
			base.ChildControlsCreated = false;
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x000DD770 File Offset: 0x000DB970
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadControlState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 2)
			{
				throw new ArgumentException(SR.GetString("Invalid_ControlState"));
			}
			base.LoadControlState(array[0]);
			if (array[1] != null)
			{
				this._selectedCatalogPartID = (string)array[1];
			}
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x000DD7C4 File Offset: 0x000DB9C4
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[this.CheckBoxName];
			if (!string.IsNullOrEmpty(text))
			{
				base.ValidateEvent(this.CheckBoxName);
				this._selectedCheckBoxValues = text.Split(new char[]
				{
					','
				});
			}
			this._selectedZoneID = postCollection[this.ZonesID];
			return false;
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x000DD81C File Offset: 0x000DBA1C
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 5)
			{
				throw new ArgumentException(SR.GetString("ViewState_InvalidViewState"));
			}
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.AddVerb).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.CloseVerb).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.PartLinkStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.SelectedPartLinkStyle).LoadViewState(array[4]);
			}
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x000DD8A8 File Offset: 0x000DBAA8
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page page = this.Page;
			if (page != null)
			{
				page.RegisterRequiresControlState(this);
			}
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x000DD8CD File Offset: 0x000DBACD
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.CatalogPartChrome.PerformPreRender();
			this.Page.RegisterRequiresPostBack(this);
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x000DD8F0 File Offset: 0x000DBAF0
		protected override void RaisePostBackEvent(string eventArgument)
		{
			string[] array = eventArgument.Split(new char[]
			{
				'$'
			});
			if (array.Length == 2 && array[0] == "select")
			{
				this.SelectedCatalogPartID = array[1];
				return;
			}
			if (string.Equals(eventArgument, "add", StringComparison.OrdinalIgnoreCase))
			{
				if (this.AddVerb.Visible && this.AddVerb.Enabled)
				{
					this.AddSelectedWebParts();
					return;
				}
			}
			else if (string.Equals(eventArgument, "close", StringComparison.OrdinalIgnoreCase))
			{
				if (this.CloseVerb.Visible && this.CloseVerb.Enabled)
				{
					this.Close();
					return;
				}
			}
			else
			{
				base.RaisePostBackEvent(eventArgument);
			}
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x000DD993 File Offset: 0x000DBB93
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			base.Render(writer);
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x000DD9B0 File Offset: 0x000DBBB0
		protected override void RenderBody(HtmlTextWriter writer)
		{
			base.RenderBodyTableBeginTag(writer);
			if (base.DesignMode)
			{
				base.RenderDesignerRegionBeginTag(writer, Orientation.Vertical);
			}
			CatalogPartCollection catalogParts = this.CatalogParts;
			if (catalogParts != null && catalogParts.Count > 0)
			{
				bool flag = true;
				if (catalogParts.Count > 1)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					flag = false;
					this.RenderCatalogPartLinks(writer);
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
				CatalogPartChrome catalogPartChrome = this.CatalogPartChrome;
				if (base.DesignMode)
				{
					using (IEnumerator enumerator = catalogParts.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							CatalogPart catalogPart = (CatalogPart)obj;
							this.RenderCatalogPart(writer, catalogPart, catalogPartChrome, ref flag);
						}
						goto IL_C7;
					}
				}
				CatalogPart selectedCatalogPart = this.SelectedCatalogPart;
				if (selectedCatalogPart != null)
				{
					this.RenderCatalogPart(writer, selectedCatalogPart, catalogPartChrome, ref flag);
				}
				IL_C7:
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "0");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			else
			{
				this.RenderEmptyZoneText(writer);
			}
			if (base.DesignMode)
			{
				WebZone.RenderDesignerRegionEndTag(writer);
			}
			WebZone.RenderBodyTableEndTag(writer);
		}

		// Token: 0x0600435A RID: 17242 RVA: 0x000DDAE8 File Offset: 0x000DBCE8
		private void RenderCatalogPart(HtmlTextWriter writer, CatalogPart catalogPart, CatalogPartChrome chrome, ref bool firstCell)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (!firstCell)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingTop, "0");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			firstCell = false;
			chrome.RenderCatalogPart(writer, catalogPart);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x000DDB24 File Offset: 0x000DBD24
		protected virtual void RenderCatalogPartLinks(HtmlTextWriter writer)
		{
			this.RenderInstructionText(writer);
			CatalogPart selectedCatalogPart = this.SelectedCatalogPart;
			foreach (object obj in this.CatalogParts)
			{
				CatalogPart catalogPart = (CatalogPart)obj;
				WebPartDescriptionCollection availableWebPartDescriptions = catalogPart.GetAvailableWebPartDescriptions();
				int num = (availableWebPartDescriptions != null) ? availableWebPartDescriptions.Count : 0;
				string displayTitle = catalogPart.DisplayTitle;
				string text = displayTitle + " (" + num.ToString(CultureInfo.CurrentCulture) + ")";
				if (catalogPart == selectedCatalogPart)
				{
					Label label = new Label();
					label.Text = text;
					label.Page = this.Page;
					label.ApplyStyle(this.SelectedPartLinkStyle);
					label.RenderControl(writer);
				}
				else
				{
					string eventArgument = "select$" + catalogPart.ID;
					ZoneLinkButton zoneLinkButton = new ZoneLinkButton(this, eventArgument);
					zoneLinkButton.Text = text;
					zoneLinkButton.ToolTip = SR.GetString("CatalogZoneBase_SelectCatalogPart", new object[]
					{
						displayTitle
					});
					zoneLinkButton.Page = this.Page;
					zoneLinkButton.ApplyStyle(this.PartLinkStyle);
					zoneLinkButton.RenderControl(writer);
				}
				writer.WriteBreak();
			}
			writer.WriteBreak();
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x000DDC70 File Offset: 0x000DBE70
		private void RenderEmptyZoneText(HtmlTextWriter writer)
		{
			string emptyZoneText = this.EmptyZoneText;
			if (!string.IsNullOrEmpty(emptyZoneText))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
				Style emptyZoneTextStyle = base.EmptyZoneTextStyle;
				if (!emptyZoneTextStyle.IsEmpty)
				{
					emptyZoneTextStyle.AddAttributesToRender(writer, this);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.Write(emptyZoneText);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x000DDCD4 File Offset: 0x000DBED4
		protected override void RenderFooter(HtmlTextWriter writer)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "4px");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			DropDownList dropDownList = new DropDownList();
			dropDownList.ClientIDMode = ClientIDMode.AutoID;
			dropDownList.ID = this.ZonesID;
			if (base.DesignMode)
			{
				dropDownList.Items.Add(SR.GetString("Zone_SampleHeaderText"));
			}
			else if (base.WebPartManager != null && base.WebPartManager.Zones != null)
			{
				foreach (object obj in base.WebPartManager.Zones)
				{
					WebPartZoneBase webPartZoneBase = (WebPartZoneBase)obj;
					if (webPartZoneBase.AllowLayoutChange)
					{
						ListItem listItem = new ListItem(webPartZoneBase.DisplayTitle, webPartZoneBase.ID);
						if (string.Equals(webPartZoneBase.ID, this._selectedZoneID, StringComparison.OrdinalIgnoreCase))
						{
							listItem.Selected = true;
						}
						dropDownList.Items.Add(listItem);
					}
				}
			}
			base.LabelStyle.AddAttributesToRender(writer, this);
			if (dropDownList.Items.Count > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.For, dropDownList.ClientID);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			writer.Write(this.SelectTargetZoneText);
			writer.RenderEndTag();
			writer.Write("&nbsp;");
			dropDownList.ApplyStyle(base.EditUIStyle);
			if (dropDownList.Items.Count > 0)
			{
				dropDownList.RenderControl(writer);
			}
			writer.Write("&nbsp;");
			this.RenderVerbs(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x000DDE60 File Offset: 0x000DC060
		private void RenderInstructionText(HtmlTextWriter writer)
		{
			string instructionText = this.InstructionText;
			if (!string.IsNullOrEmpty(instructionText))
			{
				Label label = new Label();
				label.Text = instructionText;
				label.Page = this.Page;
				label.ApplyStyle(base.InstructionTextStyle);
				label.RenderControl(writer);
				writer.WriteBreak();
				writer.WriteBreak();
			}
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x000DDEB4 File Offset: 0x000DC0B4
		protected override void RenderVerbs(HtmlTextWriter writer)
		{
			int num = 0;
			bool enabled = false;
			CatalogPart selectedCatalogPart = this.SelectedCatalogPart;
			if (selectedCatalogPart != null)
			{
				WebPartDescriptionCollection availableWebPartDescriptions = selectedCatalogPart.GetAvailableWebPartDescriptions();
				num = ((availableWebPartDescriptions != null) ? availableWebPartDescriptions.Count : 0);
			}
			if (num == 0)
			{
				enabled = this.AddVerb.Enabled;
				this.AddVerb.Enabled = false;
			}
			try
			{
				base.RenderVerbsInternal(writer, new WebPartVerb[]
				{
					this.AddVerb,
					this.CloseVerb
				});
			}
			finally
			{
				if (num == 0)
				{
					this.AddVerb.Enabled = enabled;
				}
			}
		}

		// Token: 0x06004360 RID: 17248 RVA: 0x000DDF40 File Offset: 0x000DC140
		protected internal override object SaveControlState()
		{
			object[] array = new object[2];
			array[0] = base.SaveControlState();
			if (!string.IsNullOrEmpty(this._selectedCatalogPartID))
			{
				array[1] = this._selectedCatalogPartID;
			}
			for (int i = 0; i < 2; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x000DDF88 File Offset: 0x000DC188
		protected override object SaveViewState()
		{
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this._addVerb != null) ? ((IStateManager)this._addVerb).SaveViewState() : null,
				(this._closeVerb != null) ? ((IStateManager)this._closeVerb).SaveViewState() : null,
				(this._partLinkStyle != null) ? ((IStateManager)this._partLinkStyle).SaveViewState() : null,
				(this._selectedPartLinkStyle != null) ? ((IStateManager)this._selectedPartLinkStyle).SaveViewState() : null
			};
			for (int i = 0; i < 5; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x000DE020 File Offset: 0x000DC220
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._addVerb != null)
			{
				((IStateManager)this._addVerb).TrackViewState();
			}
			if (this._closeVerb != null)
			{
				((IStateManager)this._closeVerb).TrackViewState();
			}
			if (this._partLinkStyle != null)
			{
				((IStateManager)this._partLinkStyle).TrackViewState();
			}
			if (this._selectedPartLinkStyle != null)
			{
				((IStateManager)this._selectedPartLinkStyle).TrackViewState();
			}
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x000DE07F File Offset: 0x000DC27F
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x00006164 File Offset: 0x00004364
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
		}

		// Token: 0x040025C7 RID: 9671
		private CatalogPartCollection _catalogParts;

		// Token: 0x040025C8 RID: 9672
		private string[] _selectedCheckBoxValues;

		// Token: 0x040025C9 RID: 9673
		private string _selectedZoneID;

		// Token: 0x040025CA RID: 9674
		private string _selectedCatalogPartID;

		// Token: 0x040025CB RID: 9675
		private const int baseIndex = 0;

		// Token: 0x040025CC RID: 9676
		private const int addVerbIndex = 1;

		// Token: 0x040025CD RID: 9677
		private const int closeVerbIndex = 2;

		// Token: 0x040025CE RID: 9678
		private const int partLinkStyleIndex = 3;

		// Token: 0x040025CF RID: 9679
		private const int selectedPartLinkStyleIndex = 4;

		// Token: 0x040025D0 RID: 9680
		private const int viewStateArrayLength = 5;

		// Token: 0x040025D1 RID: 9681
		private const int selectedCatalogPartIDIndex = 1;

		// Token: 0x040025D2 RID: 9682
		private const int controlStateArrayLength = 2;

		// Token: 0x040025D3 RID: 9683
		private WebPartVerb _addVerb;

		// Token: 0x040025D4 RID: 9684
		private WebPartVerb _closeVerb;

		// Token: 0x040025D5 RID: 9685
		private Style _partLinkStyle;

		// Token: 0x040025D6 RID: 9686
		private Style _selectedPartLinkStyle;

		// Token: 0x040025D7 RID: 9687
		private CatalogPartChrome _catalogPartChrome;

		// Token: 0x040025D8 RID: 9688
		private const string addEventArgument = "add";

		// Token: 0x040025D9 RID: 9689
		private const string closeEventArgument = "close";

		// Token: 0x040025DA RID: 9690
		private const string selectEventArgument = "select";
	}
}
