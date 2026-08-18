using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200054D RID: 1357
	public sealed class LayoutEditorPart : EditorPart
	{
		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x060044F7 RID: 17655 RVA: 0x000E4160 File Offset: 0x000E2360
		private bool CanChangeChromeState
		{
			get
			{
				WebPart webPartToEdit = base.WebPartToEdit;
				return webPartToEdit.Zone.AllowLayoutChange && (webPartToEdit.AllowMinimize || webPartToEdit.ChromeState == PartChromeState.Minimized);
			}
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x060044F8 RID: 17656 RVA: 0x000E4198 File Offset: 0x000E2398
		private bool CanChangeZone
		{
			get
			{
				WebPart webPartToEdit = base.WebPartToEdit;
				WebPartZoneBase zone = webPartToEdit.Zone;
				return zone.AllowLayoutChange && webPartToEdit.AllowZoneChange;
			}
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x060044F9 RID: 17657 RVA: 0x000E41C3 File Offset: 0x000E23C3
		private bool CanChangeZoneIndex
		{
			get
			{
				return base.WebPartToEdit.Zone.AllowLayoutChange;
			}
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x060044FA RID: 17658 RVA: 0x000D9E7A File Offset: 0x000D807A
		// (set) Token: 0x060044FB RID: 17659 RVA: 0x000D9E82 File Offset: 0x000D8082
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override string DefaultButton
		{
			get
			{
				return base.DefaultButton;
			}
			set
			{
				base.DefaultButton = value;
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x060044FC RID: 17660 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool Display
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x060044FD RID: 17661 RVA: 0x000E41D5 File Offset: 0x000E23D5
		private bool HasError
		{
			get
			{
				return this._chromeStateErrorMessage != null || this._zoneIndexErrorMessage != null;
			}
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x060044FE RID: 17662 RVA: 0x000E41EC File Offset: 0x000E23EC
		// (set) Token: 0x060044FF RID: 17663 RVA: 0x000D9EF2 File Offset: 0x000D80F2
		[WebSysDefaultValue("LayoutEditorPart_PartTitle")]
		public override string Title
		{
			get
			{
				string text = (string)this.ViewState["Title"];
				if (text == null)
				{
					return SR.GetString("LayoutEditorPart_PartTitle");
				}
				return text;
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x000E4220 File Offset: 0x000E2420
		public override bool ApplyChanges()
		{
			WebPart webPartToEdit = base.WebPartToEdit;
			if (webPartToEdit != null)
			{
				this.EnsureChildControls();
				try
				{
					if (this.CanChangeChromeState)
					{
						TypeConverter converter = TypeDescriptor.GetConverter(typeof(PartChromeState));
						webPartToEdit.ChromeState = (PartChromeState)converter.ConvertFromString(this._chromeState.SelectedValue);
					}
				}
				catch (Exception ex)
				{
					this._chromeStateErrorMessage = base.CreateErrorMessage(ex.Message);
				}
				int zoneIndex = webPartToEdit.ZoneIndex;
				if (this.CanChangeZoneIndex)
				{
					if (int.TryParse(this._zoneIndex.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out zoneIndex))
					{
						if (zoneIndex < 0)
						{
							this._zoneIndexErrorMessage = SR.GetString("EditorPart_PropertyMinValue", new object[]
							{
								0.ToString(CultureInfo.CurrentCulture)
							});
						}
					}
					else
					{
						this._zoneIndexErrorMessage = SR.GetString("EditorPart_PropertyMustBeInteger");
					}
				}
				WebPartZoneBase zone = webPartToEdit.Zone;
				WebPartZoneBase webPartZoneBase = zone;
				if (this.CanChangeZone)
				{
					webPartZoneBase = base.WebPartManager.Zones[this._zone.SelectedValue];
				}
				if (this._zoneIndexErrorMessage == null && zone.AllowLayoutChange && webPartZoneBase.AllowLayoutChange && (webPartToEdit.Zone != webPartZoneBase || webPartToEdit.ZoneIndex != zoneIndex))
				{
					try
					{
						base.WebPartManager.MoveWebPart(webPartToEdit, webPartZoneBase, zoneIndex);
					}
					catch (Exception ex2)
					{
						this._zoneIndexErrorMessage = base.CreateErrorMessage(ex2.Message);
					}
				}
			}
			return !this.HasError;
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x000E439C File Offset: 0x000E259C
		protected internal override void CreateChildControls()
		{
			ControlCollection controls = this.Controls;
			controls.Clear();
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(PartChromeState));
			this._chromeState = new DropDownList();
			this._chromeState.Items.Add(new ListItem(SR.GetString("PartChromeState_Normal"), converter.ConvertToString(PartChromeState.Normal)));
			this._chromeState.Items.Add(new ListItem(SR.GetString("PartChromeState_Minimized"), converter.ConvertToString(PartChromeState.Minimized)));
			controls.Add(this._chromeState);
			this._zone = new DropDownList();
			WebPartManager webPartManager = base.WebPartManager;
			if (webPartManager != null)
			{
				WebPartZoneCollection zones = webPartManager.Zones;
				if (zones != null)
				{
					foreach (object obj in zones)
					{
						WebPartZoneBase webPartZoneBase = (WebPartZoneBase)obj;
						ListItem item = new ListItem(webPartZoneBase.DisplayTitle, webPartZoneBase.ID);
						this._zone.Items.Add(item);
					}
				}
			}
			controls.Add(this._zone);
			this._zoneIndex = new TextBox();
			this._zoneIndex.Columns = 10;
			controls.Add(this._zoneIndex);
			foreach (object obj2 in controls)
			{
				Control control = (Control)obj2;
				control.EnableViewState = false;
			}
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x000E4540 File Offset: 0x000E2740
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Display && this.Visible && !this.HasError)
			{
				this.SyncChanges();
			}
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x000E4568 File Offset: 0x000E2768
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			this.EnsureChildControls();
			if (base.DesignMode)
			{
				this._zone.Items.Add(SR.GetString("Zone_SampleHeaderText"));
			}
			string[] propertyDisplayNames = new string[]
			{
				SR.GetString("LayoutEditorPart_ChromeState"),
				SR.GetString("LayoutEditorPart_Zone"),
				SR.GetString("LayoutEditorPart_ZoneIndex")
			};
			WebControl[] propertyEditors = new WebControl[]
			{
				this._chromeState,
				this._zone,
				this._zoneIndex
			};
			string[] errorMessages = new string[]
			{
				this._chromeStateErrorMessage,
				null,
				this._zoneIndexErrorMessage
			};
			base.RenderPropertyEditors(writer, propertyDisplayNames, null, propertyEditors, errorMessages);
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x000E4628 File Offset: 0x000E2828
		public override void SyncChanges()
		{
			WebPart webPartToEdit = base.WebPartToEdit;
			if (webPartToEdit != null)
			{
				WebPartZoneBase zone = webPartToEdit.Zone;
				bool allowLayoutChange = zone.AllowLayoutChange;
				this.EnsureChildControls();
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(PartChromeState));
				this._chromeState.SelectedValue = converter.ConvertToString(webPartToEdit.ChromeState);
				this._chromeState.Enabled = this.CanChangeChromeState;
				WebPartManager webPartManager = base.WebPartManager;
				if (webPartManager != null)
				{
					WebPartZoneCollection zones = webPartManager.Zones;
					bool allowZoneChange = webPartToEdit.AllowZoneChange;
					this._zone.ClearSelection();
					foreach (object obj in this._zone.Items)
					{
						ListItem listItem = (ListItem)obj;
						string value = listItem.Value;
						WebPartZoneBase webPartZoneBase = zones[value];
						if (webPartZoneBase == zone || (allowZoneChange && webPartZoneBase.AllowLayoutChange))
						{
							listItem.Enabled = true;
						}
						else
						{
							listItem.Enabled = false;
						}
						if (webPartZoneBase == zone)
						{
							listItem.Selected = true;
						}
					}
					this._zone.Enabled = this.CanChangeZone;
				}
				this._zoneIndex.Text = webPartToEdit.ZoneIndex.ToString(CultureInfo.CurrentCulture);
				this._zoneIndex.Enabled = this.CanChangeZoneIndex;
			}
		}

		// Token: 0x0400264A RID: 9802
		private DropDownList _chromeState;

		// Token: 0x0400264B RID: 9803
		private DropDownList _zone;

		// Token: 0x0400264C RID: 9804
		private TextBox _zoneIndex;

		// Token: 0x0400264D RID: 9805
		private string _chromeStateErrorMessage;

		// Token: 0x0400264E RID: 9806
		private string _zoneIndexErrorMessage;

		// Token: 0x0400264F RID: 9807
		private const int TextBoxColumns = 10;

		// Token: 0x04002650 RID: 9808
		private const int MinZoneIndex = 0;
	}
}
