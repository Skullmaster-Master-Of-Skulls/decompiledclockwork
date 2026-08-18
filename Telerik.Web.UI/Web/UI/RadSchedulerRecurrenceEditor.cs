using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02001A1A RID: 6682
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Calendar, Scheduler and Gantt")]
	public class RadSchedulerRecurrenceEditor : RecurrenceEditor, INamingContainer
	{
		// Token: 0x17004E5E RID: 20062
		// (get) Token: 0x0601032C RID: 66348 RVA: 0x003A0098 File Offset: 0x0039E298
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public override IRecurrenceEditorStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new RadSchedulerRecurrenceEditorStrings(new LocalizationProvider("RadSchedulerRecurrenceEditor", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17004E5F RID: 20063
		// (get) Token: 0x0601032D RID: 66349 RVA: 0x003A00E7 File Offset: 0x0039E2E7
		// (set) Token: 0x0601032E RID: 66350 RVA: 0x003A0108 File Offset: 0x0039E308
		[Category("Misc")]
		[Description("Gets or sets a value indicating where RadSchedulerRecurrenceEditor will look for its .resx localization files.")]
		[DefaultValue("")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x0601032F RID: 66351 RVA: 0x003A015B File Offset: 0x0039E35B
		public void ResetLayout()
		{
			this.Controls.Clear();
			base.ClearInternalCalendar();
			this.CreateChildControls();
		}

		// Token: 0x04004925 RID: 18725
		private IRecurrenceEditorStrings _localization;
	}
}
