using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020000F4 RID: 244
	internal class DatePickerStrings : LocalizationStrings
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x00025492 File Offset: 0x00023692
		public DatePickerStrings(LocalizationProvider localizationProvider) : base(localizationProvider)
		{
			this._localizationProvider = localizationProvider;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000254A2 File Offset: 0x000236A2
		public override string GetString(string key)
		{
			return this._localizationProvider.GetString(key) ?? base.GetString(key);
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x000254BB File Offset: 0x000236BB
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x000254C8 File Offset: 0x000236C8
		[DefaultValue("Open the calendar popup.")]
		[NotifyParentProperty(true)]
		public string PopupButtonToolTip
		{
			get
			{
				return this.GetString("PopupButtonToolTip");
			}
			set
			{
				this.SetString("PopupButtonToolTip", value);
			}
		}

		// Token: 0x04000284 RID: 644
		private readonly LocalizationProvider _localizationProvider;
	}
}
