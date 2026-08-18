using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000EFD RID: 3837
	public class TargetControl : StateManager
	{
		// Token: 0x0600916E RID: 37230 RVA: 0x0020BB24 File Offset: 0x00209D24
		public TargetControl()
		{
		}

		// Token: 0x0600916F RID: 37231 RVA: 0x0020BB2C File Offset: 0x00209D2C
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TargetControl(string id)
		{
			this.ControlID = id;
		}

		// Token: 0x06009170 RID: 37232 RVA: 0x0020BB3B File Offset: 0x00209D3B
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TargetControl(string id, string value)
		{
			this.ControlID = id;
			this.Skin = value;
		}

		// Token: 0x06009171 RID: 37233 RVA: 0x0020BB51 File Offset: 0x00209D51
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TargetControl(string id, string value, bool enabled)
		{
			this.ControlID = id;
			this.Skin = value;
			this.Enabled = enabled;
		}

		// Token: 0x06009172 RID: 37234 RVA: 0x0020BB6E File Offset: 0x00209D6E
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TargetControl(ControlTypeToApplySkin type, string skin)
		{
			this.ControlsToApplySkin = type;
			this.Skin = skin;
		}

		// Token: 0x06009173 RID: 37235 RVA: 0x0020BB84 File Offset: 0x00209D84
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TargetControl(string id, string skin, ControlTypeToApplySkin type)
		{
			this.ControlID = id;
			this.Skin = skin;
			this.ControlsToApplySkin = type;
		}

		// Token: 0x06009174 RID: 37236 RVA: 0x0020BBA1 File Offset: 0x00209DA1
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TargetControl(string id, string skin, ControlTypeToApplySkin type, bool enabled)
		{
			this.ControlID = id;
			this.Skin = skin;
			this.ControlsToApplySkin = type;
			this.Enabled = enabled;
		}

		// Token: 0x17002E0A RID: 11786
		// (get) Token: 0x06009175 RID: 37237 RVA: 0x0020BBC6 File Offset: 0x00209DC6
		// (set) Token: 0x06009176 RID: 37238 RVA: 0x0020BBF5 File Offset: 0x00209DF5
		[TypeConverter(typeof(ControlIDConverter))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[IDReferenceProperty]
		public virtual string ControlID
		{
			get
			{
				if (base.ViewState["ControlID"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["ControlID"];
			}
			set
			{
				base.ViewState["ControlID"] = value;
			}
		}

		// Token: 0x17002E0B RID: 11787
		// (get) Token: 0x06009177 RID: 37239 RVA: 0x0020BC08 File Offset: 0x00209E08
		// (set) Token: 0x06009178 RID: 37240 RVA: 0x0020BC37 File Offset: 0x00209E37
		[Editor("Telerik.Web.Design.RadSkinManagerSkinEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string Skin
		{
			get
			{
				if (base.ViewState["Skin"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Skin"];
			}
			set
			{
				base.ViewState["Skin"] = value;
			}
		}

		// Token: 0x17002E0C RID: 11788
		// (get) Token: 0x06009179 RID: 37241 RVA: 0x0020BC4A File Offset: 0x00209E4A
		// (set) Token: 0x0600917A RID: 37242 RVA: 0x0020BC75 File Offset: 0x00209E75
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("Gets or sets a value indicating whether skinning should be enabled or not.")]
		public virtual bool Enabled
		{
			get
			{
				return base.ViewState["Enabled"] == null || (bool)base.ViewState["Enabled"];
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17002E0D RID: 11789
		// (get) Token: 0x0600917B RID: 37243 RVA: 0x0020BC8D File Offset: 0x00209E8D
		// (set) Token: 0x0600917C RID: 37244 RVA: 0x0020BCCD File Offset: 0x00209ECD
		[DefaultValue(ControlTypeToApplySkin.NotSet)]
		[Editor("Telerik.Web.Design.SkinManager.FlagLongIntEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public ControlTypeToApplySkin ControlsToApplySkin
		{
			get
			{
				if (base.ViewState["ControlsToApplySkin"] == null)
				{
					base.ViewState["ControlsToApplySkin"] = ControlTypeToApplySkin.NotSet;
				}
				return (ControlTypeToApplySkin)base.ViewState["ControlsToApplySkin"];
			}
			set
			{
				base.ViewState["ControlsToApplySkin"] = value;
			}
		}

		// Token: 0x0600917D RID: 37245 RVA: 0x0020BCE8 File Offset: 0x00209EE8
		public string ShouldApplySkinToControlType(ISkinnableControl control)
		{
			string result = string.Empty;
			if (this.Enabled && !string.IsNullOrEmpty(this.Skin))
			{
				string text = control.GetType().ToString().Replace("Telerik.Web.UI.", "");
				if (TargetControl.typePredicates.Keys.Contains(text) && TargetControl.typePredicates[text](this.ControlsToApplySkin))
				{
					result = this.Skin;
				}
			}
			return result;
		}

		// Token: 0x0600917E RID: 37246 RVA: 0x0020C244 File Offset: 0x0020A444
		// Note: this type is marked as 'beforefieldinit'.
		static TargetControl()
		{
			Dictionary<string, Predicate<ControlTypeToApplySkin>> dictionary = new Dictionary<string, Predicate<ControlTypeToApplySkin>>();
			dictionary.Add("RadAjaxLoadingPanel", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadAjaxLoadingPanel) == ControlTypeToApplySkin.RadAjaxLoadingPanel);
			dictionary.Add("RadAsyncUpload", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadAsyncUpload) == ControlTypeToApplySkin.RadAsyncUpload);
			dictionary.Add("RadButton", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadButton) == ControlTypeToApplySkin.RadButton);
			dictionary.Add("RadPushButton", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadButton) == ControlTypeToApplySkin.RadButton);
			dictionary.Add("RadImageButton", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadButton) == ControlTypeToApplySkin.RadButton);
			dictionary.Add("RadLinkButton", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadButton) == ControlTypeToApplySkin.RadButton);
			dictionary.Add("RadToggleButton", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadButton) == ControlTypeToApplySkin.RadButton);
			dictionary.Add("RadRadioButtonList", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadButton) == ControlTypeToApplySkin.RadButton);
			dictionary.Add("RadCheckBoxList", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadButton) == ControlTypeToApplySkin.RadButton);
			dictionary.Add("RadCheckBox", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadButton) == ControlTypeToApplySkin.RadButton);
			dictionary.Add("RadSwitch", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadButton) == ControlTypeToApplySkin.RadButton);
			dictionary.Add("RadCalendar", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadCalendar) == ControlTypeToApplySkin.RadCalendar);
			dictionary.Add("RadColorPicker", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadColorPicker) == ControlTypeToApplySkin.RadColorPicker);
			dictionary.Add("RadComboBox", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadComboBox) == ControlTypeToApplySkin.RadComboBox);
			dictionary.Add("RadContextMenu", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadContextMenu) == ControlTypeToApplySkin.RadContextMenu);
			dictionary.Add("RadDataPager", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadDataPager) == ControlTypeToApplySkin.RadDataPager);
			dictionary.Add("RadDateInput", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadDateInput) == ControlTypeToApplySkin.RadDateInput);
			dictionary.Add("RadDatePicker", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadDatePicker) == ControlTypeToApplySkin.RadDatePicker);
			dictionary.Add("RadDateTimePicker", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadDateTimePicker) == ControlTypeToApplySkin.RadDateTimePicker);
			dictionary.Add("RadDialogOpener", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadDialogOpener) == ControlTypeToApplySkin.RadDialogOpener);
			dictionary.Add("RadDock", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadDock) == ControlTypeToApplySkin.RadDock);
			dictionary.Add("RadDockLayout", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadDockLayout) == ControlTypeToApplySkin.RadDockLayout);
			dictionary.Add("RadDockZone", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadDockZone) == ControlTypeToApplySkin.RadDockZone);
			dictionary.Add("RadEditor", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadEditor) == ControlTypeToApplySkin.RadEditor);
			dictionary.Add("RadFileExplorer", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadFileExplorer) == ControlTypeToApplySkin.RadFileExplorer);
			dictionary.Add("RadFilter", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadFilter) == ControlTypeToApplySkin.RadFilter);
			dictionary.Add("RadFormDecorator", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadFormDecorator) == ControlTypeToApplySkin.RadFormDecorator);
			dictionary.Add("RadGrid", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadGrid) == ControlTypeToApplySkin.RadGrid);
			dictionary.Add("RadImageEditor", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadImageEditor) == ControlTypeToApplySkin.RadImageEditor);
			dictionary.Add("RadInputControl", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadInputControl) == ControlTypeToApplySkin.RadInputControl);
			dictionary.Add("RadInputManager", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadInputManager) == ControlTypeToApplySkin.RadInputManager);
			dictionary.Add("RadLightBox", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadLightBox) == ControlTypeToApplySkin.RadLightBox);
			dictionary.Add("RadListBox", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadListBox) == ControlTypeToApplySkin.RadListBox);
			dictionary.Add("RadListView", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadListView) == ControlTypeToApplySkin.RadListView);
			dictionary.Add("RadMaskedTextBox", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadMaskedTextBox) == ControlTypeToApplySkin.RadMaskedTextBox);
			dictionary.Add("RadMenu", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadMenu) == ControlTypeToApplySkin.RadMenu);
			dictionary.Add("RadMultiPage", (ControlTypeToApplySkin type) => (type & (ControlTypeToApplySkin)((ulong)int.MinValue)) == (ControlTypeToApplySkin)((ulong)int.MinValue));
			dictionary.Add("RadNotification", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadNotification) == ControlTypeToApplySkin.RadNotification);
			dictionary.Add("RadNumericTextBox", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadNumericTextBox) == ControlTypeToApplySkin.RadNumericTextBox);
			dictionary.Add("RadPane", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadPane) == ControlTypeToApplySkin.RadPane);
			dictionary.Add("RadPanelBar", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadPanelBar) == ControlTypeToApplySkin.RadPanelBar);
			dictionary.Add("RadProgressArea", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadProgressArea) == ControlTypeToApplySkin.RadProgressArea);
			dictionary.Add("RadRating", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadRating) == ControlTypeToApplySkin.RadRating);
			dictionary.Add("RadRibbonBar", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadRibbonBar) == ControlTypeToApplySkin.RadRibbonBar);
			dictionary.Add("RadRotator", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadRotator) == ControlTypeToApplySkin.RadRotator);
			dictionary.Add("RadScheduler", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadScheduler) == ControlTypeToApplySkin.RadScheduler);
			dictionary.Add("RadSiteMap", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadSiteMap) == ControlTypeToApplySkin.RadSiteMap);
			dictionary.Add("RadSlider", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadSlider) == ControlTypeToApplySkin.RadSlider);
			dictionary.Add("RadSpell", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadSpell) == ControlTypeToApplySkin.RadSpell);
			dictionary.Add("RadSplitBar", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadSplitBar) == ControlTypeToApplySkin.RadSplitBar);
			dictionary.Add("RadSplitter", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadSplitter) == ControlTypeToApplySkin.RadSplitter);
			dictionary.Add("RadTab", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadTab) == ControlTypeToApplySkin.RadTab);
			dictionary.Add("RadTabStrip", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadTabStrip) == ControlTypeToApplySkin.RadTabStrip);
			dictionary.Add("RadTagCloud", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadTagCloud) == ControlTypeToApplySkin.RadTagCloud);
			dictionary.Add("RadTextBox", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadTextBox) == ControlTypeToApplySkin.RadTextBox);
			dictionary.Add("RadTimePicker", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadTimePicker) == ControlTypeToApplySkin.RadTimePicker);
			dictionary.Add("RadTimeView", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadTimeView) == ControlTypeToApplySkin.RadTimeView);
			dictionary.Add("RadToolBar", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadToolBar) == ControlTypeToApplySkin.RadToolBar);
			dictionary.Add("RadToolTip", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadToolTip) == ControlTypeToApplySkin.RadToolTip);
			dictionary.Add("RadTreelist", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadTreelist) == ControlTypeToApplySkin.RadTreelist);
			dictionary.Add("RadTreeView", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadTreeView) == ControlTypeToApplySkin.RadTreeView);
			dictionary.Add("RadUpload", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadUpload) == ControlTypeToApplySkin.RadUpload);
			dictionary.Add("RadWindow", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadWindow) == ControlTypeToApplySkin.RadWindow);
			dictionary.Add("RadWindowManager", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadWindowManager) == ControlTypeToApplySkin.RadWindowManager);
			dictionary.Add("RadDataForm", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadDataForm) == ControlTypeToApplySkin.RadDataForm);
			dictionary.Add("RadLabel", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadLabel) == ControlTypeToApplySkin.RadLabel);
			dictionary.Add("RadMonthYearPicker", (ControlTypeToApplySkin type) => (type & ControlTypeToApplySkin.RadMonthYearPicker) == ControlTypeToApplySkin.RadMonthYearPicker);
			TargetControl.typePredicates = dictionary;
		}

		// Token: 0x04002954 RID: 10580
		internal static IDictionary<string, Predicate<ControlTypeToApplySkin>> typePredicates;
	}
}
