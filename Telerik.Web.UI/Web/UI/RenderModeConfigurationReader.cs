using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Telerik.Web.UI
{
	// Token: 0x020001C6 RID: 454
	internal class RenderModeConfigurationReader
	{
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x0003C59C File Offset: 0x0003A79C
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x0003C5DC File Offset: 0x0003A7DC
		internal static RenderModeConfigurationReader Instance
		{
			get
			{
				RenderModeConfigurationReader result;
				lock (RenderModeConfigurationReader.locker)
				{
					result = RenderModeConfigurationReader.reader;
				}
				return result;
			}
			set
			{
				RenderModeConfigurationReader.reader = value;
			}
		} = new RenderModeConfigurationReader();

		// Token: 0x06001089 RID: 4233 RVA: 0x0003C5E4 File Offset: 0x0003A7E4
		internal RenderModeConfigurationReader()
		{
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0003CD08 File Offset: 0x0003AF08
		public RenderMode GetRenderMode(Type control, ISkinnableControl instance = null)
		{
			Type type = this.Normalize(control);
			if (!this.Verify(type))
			{
				return RenderMode.Classic;
			}
			return this.ReadKey(this.RenderModeAppSettingsByTypeKey[type]);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0003CD3A File Offset: 0x0003AF3A
		public RenderMode GetRenderMode(ISkinnableControl instance = null)
		{
			return this.ReadKey(RenderModeConfigurationReader.RenderModeAppSettingKey);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0003CD48 File Offset: 0x0003AF48
		protected internal virtual bool HasKey(Type control)
		{
			Type type = this.Normalize(control);
			return this.Verify(type) && !string.IsNullOrEmpty(this.ReadConfiguration(this.RenderModeAppSettingsByTypeKey[type]));
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0003CD82 File Offset: 0x0003AF82
		protected internal virtual bool HasGlobalKey()
		{
			return !string.IsNullOrEmpty(this.ReadConfiguration(RenderModeConfigurationReader.RenderModeAppSettingKey));
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0003CD98 File Offset: 0x0003AF98
		protected internal RenderMode ReadKey(string key)
		{
			RenderMode result = RenderMode.Classic;
			string value = this.ReadConfiguration(key);
			if (!string.IsNullOrEmpty(value))
			{
				Enum.TryParse<RenderMode>(value, true, out result);
				return result;
			}
			return result;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0003CDDC File Offset: 0x0003AFDC
		protected internal Type Normalize(Type control)
		{
			Type result = control;
			if (!this.RenderModeAppSettingsByTypeKey.ContainsKey(control))
			{
				result = (this.RenderModeAppSettingsByTypeKey.Keys.FirstOrDefault((Type t) => control.IsSubclassOf(t)) ?? control);
			}
			return result;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0003CE3F File Offset: 0x0003B03F
		protected internal virtual bool Verify(Type type)
		{
			return this.RenderModeAppSettingsByTypeKey.ContainsKey(type);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0003CE4D File Offset: 0x0003B04D
		protected internal virtual string ReadConfiguration(string key)
		{
			return ConfigurationManager.AppSettings.Get(key);
		}

		// Token: 0x040004AE RID: 1198
		private static readonly object locker = new object();

		// Token: 0x040004AF RID: 1199
		private static RenderModeConfigurationReader reader;

		// Token: 0x040004B0 RID: 1200
		internal readonly Dictionary<Type, string> RenderModeAppSettingsByTypeKey = new Dictionary<Type, string>
		{
			{
				typeof(RadTextBox),
				"Telerik.Web.UI.TextBox.RenderMode"
			},
			{
				typeof(RadNumericTextBox),
				"Telerik.Web.UI.NumericTextBox.RenderMode"
			},
			{
				typeof(RadMaskedTextBox),
				"Telerik.Web.UI.MaskedTextBox.RenderMode"
			},
			{
				typeof(RadDateInput),
				"Telerik.Web.UI.DateInput.RenderMode"
			},
			{
				typeof(RadComboBox),
				"Telerik.Web.UI.ComboBox.RenderMode"
			},
			{
				typeof(RadMenu),
				"Telerik.Web.UI.Menu.RenderMode"
			},
			{
				typeof(RadContextMenu),
				"Telerik.Web.UI.ContextMenu.RenderMode"
			},
			{
				typeof(RadFormDecorator),
				"Telerik.Web.UI.FormDecorator.RenderMode"
			},
			{
				typeof(RadDock),
				"Telerik.Web.UI.Dock.RenderMode"
			},
			{
				typeof(RadDockZone),
				"Telerik.Web.UI.DockZone.RenderMode"
			},
			{
				typeof(RadToolTip),
				"Telerik.Web.UI.ToolTip.RenderMode"
			},
			{
				typeof(RadToolTipManager),
				"Telerik.Web.UI.ToolTipManager.RenderMode"
			},
			{
				typeof(RadWindow),
				"Telerik.Web.UI.Window.RenderMode"
			},
			{
				typeof(RadWindowManager),
				"Telerik.Web.UI.WindowManager.RenderMode"
			},
			{
				typeof(RadCalendar),
				"Telerik.Web.UI.Calendar.RenderMode"
			},
			{
				typeof(RadDatePicker),
				"Telerik.Web.UI.DatePicker.RenderMode"
			},
			{
				typeof(RadDateTimePicker),
				"Telerik.Web.UI.DateTimePicker.RenderMode"
			},
			{
				typeof(RadTimePicker),
				"Telerik.Web.UI.TimePicker.RenderMode"
			},
			{
				typeof(RadTimeView),
				"Telerik.Web.UI.TimeView.RenderMode"
			},
			{
				typeof(RadMonthYearPicker),
				"Telerik.Web.UI.MonthYearPicker.RenderMode"
			},
			{
				typeof(RadScheduler),
				"Telerik.Web.UI.Scheduler.RenderMode"
			},
			{
				typeof(IntegratedRecurrenceEditor),
				"Telerik.Web.UI.IntegratedRecurrenceEditor.RenderMode"
			},
			{
				typeof(RadSchedulerRecurrenceEditor),
				"Telerik.Web.UI.RadSchedulerRecurrenceEditor.RenderMode"
			},
			{
				typeof(ReminderDialog),
				"Telerik.Web.UI.ReminderDialog.RenderMode"
			},
			{
				typeof(RadListBox),
				"Telerik.Web.UI.ListBox.RenderMode"
			},
			{
				typeof(RadTileList),
				"Telerik.Web.UI.TileList.RenderMode"
			},
			{
				typeof(RadTextTile),
				"Telerik.Web.UI.TextTile.RenderMode"
			},
			{
				typeof(RadIconTile),
				"Telerik.Web.UI.IconTile.RenderMode"
			},
			{
				typeof(RadImageTile),
				"Telerik.Web.UI.ImageTile.RenderMode"
			},
			{
				typeof(RadImageAndTextTile),
				"Telerik.Web.UI.ImageAndTextTile.RenderMode"
			},
			{
				typeof(RadContentTemplateTile),
				"Telerik.Web.UI.ContentTemplateTile.RenderMode"
			},
			{
				typeof(RadLiveTile),
				"Telerik.Web.UI.LiveTile.RenderMode"
			},
			{
				typeof(RadMediaPlayer),
				"Telerik.Web.UI.MediaPlayer.RenderMode"
			},
			{
				typeof(RadLightBox),
				"Telerik.Web.UI.LightBox.RenderMode"
			},
			{
				typeof(RadSlider),
				"Telerik.Web.UI.Slider.RenderMode"
			},
			{
				typeof(RadAutoCompleteBox),
				"Telerik.Web.UI.AutoCompleteBox.RenderMode"
			},
			{
				typeof(RadDropDownList),
				"Telerik.Web.UI.DropDownList.RenderMode"
			},
			{
				typeof(RadDropDownTree),
				"Telerik.Web.UI.DropDownTree.RenderMode"
			},
			{
				typeof(RadTreeView),
				"Telerik.Web.UI.TreeView.RenderMode"
			},
			{
				typeof(RadSearchBox),
				"Telerik.Web.UI.SearchBox.RenderMode"
			},
			{
				typeof(RadRotator),
				"Telerik.Web.UI.Rotator.RenderMode"
			},
			{
				typeof(RadGrid),
				"Telerik.Web.UI.Grid.RenderMode"
			},
			{
				typeof(RadTreeList),
				"Telerik.Web.UI.TreeList.RenderMode"
			},
			{
				typeof(RadPivotGrid),
				"Telerik.Web.UI.PivotGrid.RenderMode"
			},
			{
				typeof(RadEditor),
				"Telerik.Web.UI.Editor.RenderMode"
			},
			{
				typeof(RadWizard),
				"Telerik.Web.UI.Wizard.RenderMode"
			},
			{
				typeof(RadImageEditor),
				"Telerik.Web.UI.ImageEditor.RenderMode"
			},
			{
				typeof(RadFileExplorer),
				"Telerik.Web.UI.FileExplorer.RenderMode"
			},
			{
				typeof(RadPanelBar),
				"Telerik.Web.UI.PanelBar.RenderMode"
			},
			{
				typeof(RadFilter),
				"Telerik.Web.UI.Filter.RenderMode"
			},
			{
				typeof(RadDataPager),
				"Telerik.Web.UI.DataPager.RenderMode"
			},
			{
				typeof(RadAsyncUpload),
				"Telerik.Web.UI.AsyncUpload.RenderMode"
			},
			{
				typeof(RadOrgChart),
				"Telerik.Web.UI.OrgChart.RenderMode"
			},
			{
				typeof(RadDialogOpener),
				"Telerik.Web.UI.DialogOpener.RenderMode"
			},
			{
				typeof(RadButton),
				"Telerik.Web.UI.Button.RenderMode"
			},
			{
				typeof(RadPushButton),
				"Telerik.Web.UI.PushButton.RenderMode"
			},
			{
				typeof(RadLinkButton),
				"Telerik.Web.UI.LinkButton.RenderMode"
			},
			{
				typeof(RadToggleButton),
				"Telerik.Web.UI.ToggleButton.RenderMode"
			},
			{
				typeof(RadImageButton),
				"Telerik.Web.UI.ImageButton.RenderMode"
			},
			{
				typeof(RadCheckBox),
				"Telerik.Web.UI.CheckBox.RenderMode"
			},
			{
				typeof(RadSwitch),
				"Telerik.Web.UI.Switch.RenderMode"
			},
			{
				typeof(RadRadioButton),
				"Telerik.Web.UI.RadioButton.RenderMode"
			},
			{
				typeof(RadRating),
				"Telerik.Web.UI.Rating.RenderMode"
			},
			{
				typeof(RadTabStrip),
				"Telerik.Web.UI.TabStrip.RenderMode"
			},
			{
				typeof(RadInputManager),
				"Telerik.Web.UI.InputManager.RenderMode"
			},
			{
				typeof(RadProgressArea),
				"Telerik.Web.UI.ProgressArea.RenderMode"
			},
			{
				typeof(RadNotification),
				"Telerik.Web.UI.Notification.RenderMode"
			},
			{
				typeof(RadRibbonBar),
				"Telerik.Web.UI.RibbonBar.RenderMode"
			},
			{
				typeof(RadToolBar),
				"Telerik.Web.UI.ToolBar.RenderMode"
			},
			{
				typeof(RadSocialShare),
				"Telerik.Web.UI.SocialShare.RenderMode"
			},
			{
				typeof(RadSplitter),
				"Telerik.Web.UI.Splitter.RenderMode"
			},
			{
				typeof(RadColorPicker),
				"Telerik.Web.UI.ColorPicker.RenderMode"
			},
			{
				typeof(RadDataForm),
				"Telerik.Web.UI.DataForm.RenderMode"
			},
			{
				typeof(RadProgressBar),
				"Telerik.Web.UI.ProgressBar.RenderMode"
			},
			{
				typeof(RadTagCloud),
				"Telerik.Web.UI.TagCloud.RenderMode"
			},
			{
				typeof(RadMap),
				"Telerik.Web.UI.Map.RenderMode"
			},
			{
				typeof(RadCloudUpload),
				"Telerik.Web.UI.CloudUpload.RenderMode"
			},
			{
				typeof(RadGantt),
				"Telerik.Web.UI.Gantt.RenderMode"
			},
			{
				typeof(RadTreeMap),
				"Telerik.Web.UI.TreeMap.RenderMode"
			},
			{
				typeof(RadSpell),
				"Telerik.Web.UI.Spell.RenderMode"
			},
			{
				typeof(RadSpreadsheet),
				"Telerik.Web.UI.Spreadsheet.RenderMode"
			},
			{
				typeof(RadNavigation),
				"Telerik.Web.UI.Navigation.RenderMode"
			},
			{
				typeof(RadRadioButtonList),
				"Telerik.Web.UI.RadioButtonList.RenderMode"
			},
			{
				typeof(RadCheckBoxList),
				"Telerik.Web.UI.CheckBoxList.RenderMode"
			}
		};

		// Token: 0x040004B1 RID: 1201
		internal static readonly string RenderModeAppSettingKey = "Telerik.Web.UI.RenderMode";
	}
}
