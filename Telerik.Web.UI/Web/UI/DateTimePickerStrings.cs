using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020000F5 RID: 245
	internal class DateTimePickerStrings : LocalizationStrings
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x000254D6 File Offset: 0x000236D6
		public DateTimePickerStrings(LocalizationProvider localizationProvider) : base(localizationProvider)
		{
			this._localizationProvider = localizationProvider;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000254E6 File Offset: 0x000236E6
		public override string GetString(string key)
		{
			return this._localizationProvider.GetString(key) ?? base.GetString(key);
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x000254FF File Offset: 0x000236FF
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x0002550C File Offset: 0x0002370C
		[DefaultValue("Open the calendar popup.")]
		[NotifyParentProperty(true)]
		public string DatePopupButtonToolTip
		{
			get
			{
				return this.GetString("DatePopupButtonToolTip");
			}
			set
			{
				this.SetString("DatePopupButtonToolTip", value);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0002551A File Offset: 0x0002371A
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x00025527 File Offset: 0x00023727
		[NotifyParentProperty(true)]
		[DefaultValue("Open the time view popup.")]
		public string TimePopupButtonToolTip
		{
			get
			{
				return this.GetString("TimePopupButtonToolTip");
			}
			set
			{
				this.SetString("TimePopupButtonToolTip", value);
			}
		}

		// Token: 0x04000285 RID: 645
		private readonly LocalizationProvider _localizationProvider;
	}
}
