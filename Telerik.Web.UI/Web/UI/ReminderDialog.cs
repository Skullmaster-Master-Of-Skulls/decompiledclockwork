using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.SchedulerReminderDialog;

namespace Telerik.Web.UI
{
	// Token: 0x02000E69 RID: 3689
	[EmbeddedSkin("SchedulerReminderDialog", "Default", typeof(ReminderDialog))]
	[AdaptiveRendering]
	[EmbeddedSkin("SchedulerReminderDialog", typeof(ReminderDialog))]
	[LightweightRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(ReminderDialog))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(ReminderDialog))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(ReminderDialog))]
	[RequiredScript(typeof(jQueryPlugins))]
	[ClientScriptResource("Telerik.Web.UI.ReminderDialog", "Telerik.Web.UI.Scheduler.ReminderDialog.ReminderDialog.js")]
	internal class ReminderDialog : RadWebControl, ILocalizableControl, INamingContainer
	{
		// Token: 0x06008BFE RID: 35838 RVA: 0x001FCCD4 File Offset: 0x001FAED4
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			base.DescribeRenderMode(descriptor);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			descriptor.AddProperty("localization", javaScriptSerializer.Serialize(this.Localization));
		}

		// Token: 0x17002C43 RID: 11331
		// (get) Token: 0x06008BFF RID: 35839 RVA: 0x001FCD0C File Offset: 0x001FAF0C
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002C44 RID: 11332
		// (get) Token: 0x06008C00 RID: 35840 RVA: 0x001FCD0F File Offset: 0x001FAF0F
		// (set) Token: 0x06008C01 RID: 35841 RVA: 0x001FCD17 File Offset: 0x001FAF17
		internal new IReminderRenderer Renderer { get; set; }

		// Token: 0x17002C45 RID: 11333
		// (get) Token: 0x06008C02 RID: 35842 RVA: 0x001FCD20 File Offset: 0x001FAF20
		// (set) Token: 0x06008C03 RID: 35843 RVA: 0x001FCD45 File Offset: 0x001FAF45
		[Description("Sets the z-index of the modal dialog")]
		[Category("Appearance")]
		[DefaultValue(2500)]
		public int ZIndex
		{
			get
			{
				return (int)(this.ViewState["ZIndex"] ?? 2500);
			}
			set
			{
				this.ViewState["ZIndex"] = value;
			}
		}

		// Token: 0x17002C46 RID: 11334
		// (get) Token: 0x06008C04 RID: 35844 RVA: 0x001FCD5D File Offset: 0x001FAF5D
		// (set) Token: 0x06008C05 RID: 35845 RVA: 0x001FCD82 File Offset: 0x001FAF82
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		[Category("Appearance")]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.GetCultureInfo("en-US");
			}
			set
			{
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x17002C47 RID: 11335
		// (get) Token: 0x06008C06 RID: 35846 RVA: 0x001FCD95 File Offset: 0x001FAF95
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public IReminderDialogStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new ReminderDialogStrings(new LocalizationProvider("RadScheduler.Main", this));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x06008C07 RID: 35847 RVA: 0x001FCDD3 File Offset: 0x001FAFD3
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.UpdateRuntimeSkin(this);
		}

		// Token: 0x06008C08 RID: 35848 RVA: 0x001FCDE4 File Offset: 0x001FAFE4
		private void UpdateRuntimeSkin(Control container)
		{
			foreach (object obj in container.Controls)
			{
				Control control = (Control)obj;
				ISkinnableControl skinnableControl = control as ISkinnableControl;
				if (skinnableControl != null)
				{
					skinnableControl.Skin = base.RuntimeSkin;
				}
				this.UpdateRuntimeSkin(control);
			}
		}

		// Token: 0x17002C48 RID: 11336
		// (get) Token: 0x06008C09 RID: 35849 RVA: 0x001FCE54 File Offset: 0x001FB054
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17002C49 RID: 11337
		// (get) Token: 0x06008C0A RID: 35850 RVA: 0x001FCE58 File Offset: 0x001FB058
		protected override string CssClassFormatString
		{
			get
			{
				return "ReminderDialog ReminderDialog_{0} rsDialog";
			}
		}

		// Token: 0x06008C0B RID: 35851 RVA: 0x001FCE5F File Offset: 0x001FB05F
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.CreateRenderer();
			this.Renderer.CreateLayout(this);
			this.Renderer.CreateControls();
		}

		// Token: 0x06008C0C RID: 35852 RVA: 0x001FCE84 File Offset: 0x001FB084
		private void CreateRenderer()
		{
			this.Renderer = new RendererFactory(this).CreateRenderer();
		}

		// Token: 0x04002733 RID: 10035
		private IReminderDialogStrings _localization;

		// Token: 0x02000E6A RID: 3690
		internal static class Styles
		{
			// Token: 0x04002735 RID: 10037
			public const string TitleBar = "rsRemTitleBar";

			// Token: 0x04002736 RID: 10038
			public const string TitleBarIcon = "rsRemTitleBarIcon";

			// Token: 0x04002737 RID: 10039
			public const string TitleBarText = "rsRemTitleBarText";

			// Token: 0x04002738 RID: 10040
			public const string TitleBarCloseButton = "rsRemTitleBarCloseBtn";

			// Token: 0x04002739 RID: 10041
			public const string ContentPanel = "rsRemContentPanel";

			// Token: 0x0400273A RID: 10042
			public const string TitlePanel = "rsRemTitle";

			// Token: 0x0400273B RID: 10043
			public const string TitleIcon = "rsRemTitleIcon";

			// Token: 0x0400273C RID: 10044
			public const string TitleSubject = "rsRemTitleSubject";

			// Token: 0x0400273D RID: 10045
			public const string TitleDate = "rsRemTitleDate";

			// Token: 0x0400273E RID: 10046
			public const string RemindersListPanel = "rsRemList";

			// Token: 0x0400273F RID: 10047
			public const string ActionsPanel = "rsRemActions";

			// Token: 0x04002740 RID: 10048
			public const string SnoozePanel = "rsRemSnoozePanel";

			// Token: 0x04002741 RID: 10049
			public const string SnoozeLabel = "rsRemSnoozeLabel";

			// Token: 0x04002742 RID: 10050
			public const string SnoozeButton = "rsRemSnoozeBtn";

			// Token: 0x04002743 RID: 10051
			public const string DismissButton = "rsRemDismissBtn";

			// Token: 0x04002744 RID: 10052
			public const string DismissAllButton = "rsRemDismissAllBtn";

			// Token: 0x04002745 RID: 10053
			public const string OpenItemButton = "rsRemOpenItemBtn";

			// Token: 0x02000E6B RID: 3691
			public static class Native
			{
				// Token: 0x04002746 RID: 10054
				public const string Popup = "rsPopup";

				// Token: 0x04002747 RID: 10055
				public const string Title = "rsTitle";

				// Token: 0x04002748 RID: 10056
				public const string Body = "rsBody";

				// Token: 0x04002749 RID: 10057
				public const string List = "rsList";

				// Token: 0x0400274A RID: 10058
				public const string ListItem = "rsLi";

				// Token: 0x0400274B RID: 10059
				public const string Footer = "rsButtons";

				// Token: 0x0400274C RID: 10060
				public const string Button = "rsButton";

				// Token: 0x0400274D RID: 10061
				public const string RemListPanel = "rsRemListPanel";

				// Token: 0x0400274E RID: 10062
				public const string RemListWrap = "rsRemListWrap";

				// Token: 0x0400274F RID: 10063
				public const string SnoozePanel = "rsRemSnoozePanel";

				// Token: 0x04002750 RID: 10064
				public const string SnoozePanelTitle = "rsRemSnoozePanelTitle";

				// Token: 0x04002751 RID: 10065
				public const string SnoozePanelList = "rsRemSnoozePanelList";
			}

			// Token: 0x02000E6C RID: 3692
			public static class Lightweight
			{
				// Token: 0x04002752 RID: 10066
				public const string Title = "rsTitle";

				// Token: 0x04002753 RID: 10067
				public const string Body = "rsBody";

				// Token: 0x04002754 RID: 10068
				public const string Button = "rsButton";

				// Token: 0x04002755 RID: 10069
				public const string Icon = "p-icon";

				// Token: 0x04002756 RID: 10070
				public const string IconReminder = "p-i-notification";

				// Token: 0x04002757 RID: 10071
				public const string IconClose = "p-i-close";

				// Token: 0x04002758 RID: 10072
				public const string IconCalendar = "p-i-calendar";
			}
		}
	}
}
