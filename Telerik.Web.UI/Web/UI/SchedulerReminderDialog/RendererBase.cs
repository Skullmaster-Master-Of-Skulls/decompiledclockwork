using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerReminderDialog
{
	// Token: 0x02000806 RID: 2054
	internal abstract class RendererBase : IReminderRenderer
	{
		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x06004B16 RID: 19222 RVA: 0x000EA6A2 File Offset: 0x000E88A2
		// (set) Token: 0x06004B17 RID: 19223 RVA: 0x000EA6AA File Offset: 0x000E88AA
		public IReminderDialogStrings Localization { get; set; }

		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x06004B18 RID: 19224 RVA: 0x000EA6B3 File Offset: 0x000E88B3
		// (set) Token: 0x06004B19 RID: 19225 RVA: 0x000EA6BB File Offset: 0x000E88BB
		public ReminderDialog Owner
		{
			get
			{
				return this._owner;
			}
			protected set
			{
				this._owner = value;
			}
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x000EA6C4 File Offset: 0x000E88C4
		public RendererBase(ReminderDialog owner)
		{
			this.Owner = owner;
			this.Localization = owner.Localization;
		}

		// Token: 0x06004B1B RID: 19227
		public abstract void CreateLayout(Control container);

		// Token: 0x06004B1C RID: 19228
		public abstract void CreateControls();

		// Token: 0x06004B1D RID: 19229 RVA: 0x000EA6E0 File Offset: 0x000E88E0
		protected Pair[] GetSnoozeOptions()
		{
			return new Pair[]
			{
				new Pair("5 " + this.Localization.Minutes + " " + this.Localization.BeforeStart, "-5"),
				new Pair("10 " + this.Localization.Minutes + " " + this.Localization.BeforeStart, "-10"),
				new Pair("15 " + this.Localization.Minutes + " " + this.Localization.BeforeStart, "-15"),
				new Pair("5 " + this.Localization.Minutes, "5"),
				new Pair("10 " + this.Localization.Minutes, "10"),
				new Pair("15 " + this.Localization.Minutes, "15"),
				new Pair("30 " + this.Localization.Minutes, "30"),
				new Pair("1 " + this.Localization.Hour, "60"),
				new Pair("2 " + this.Localization.Hours, "120"),
				new Pair("4 " + this.Localization.Hours, "240"),
				new Pair("5 " + this.Localization.Hours, "300"),
				new Pair("8 " + this.Localization.Hours, "480"),
				new Pair("12 " + this.Localization.Hours, "720"),
				new Pair("1 " + this.Localization.Days, "1440"),
				new Pair("2 " + this.Localization.Days, "2880"),
				new Pair("3 " + this.Localization.Days, "4320"),
				new Pair("4 " + this.Localization.Days, "5760"),
				new Pair("1 " + this.Localization.Week, "10080")
			};
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x000EA998 File Offset: 0x000E8B98
		protected RadListBox CreateRemindersList()
		{
			return new RadListBox
			{
				ID = "RemindersList",
				Width = Unit.Percentage(100.0),
				Height = Unit.Pixel(140),
				RenderMode = this.Owner.ResolvedRenderMode,
				EnableEmbeddedSkins = this.Owner.EnableEmbeddedSkins,
				EnableEmbeddedScripts = this.Owner.EnableEmbeddedScripts
			};
		}

		// Token: 0x040012FA RID: 4858
		private ReminderDialog _owner;
	}
}
