using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000C1E RID: 3102
	[Serializable]
	public class RadControlState
	{
		// Token: 0x06007607 RID: 30215 RVA: 0x001B6925 File Offset: 0x001B4B25
		public RadControlState()
		{
		}

		// Token: 0x06007608 RID: 30216 RVA: 0x001B692D File Offset: 0x001B4B2D
		public RadControlState(List<ControlSetting> customSettings, string uniqueId)
		{
			this.UniqueId = uniqueId;
			this._controlSettings = customSettings;
		}

		// Token: 0x06007609 RID: 30217 RVA: 0x001B6943 File Offset: 0x001B4B43
		public RadControlState(List<ControlSetting> customSettings, string uniqueId, string uniqueKey)
		{
			this.UniqueId = uniqueId;
			this.UniqueKey = uniqueKey;
			this._controlSettings = customSettings;
		}

		// Token: 0x17002665 RID: 9829
		// (get) Token: 0x0600760A RID: 30218 RVA: 0x001B6960 File Offset: 0x001B4B60
		// (set) Token: 0x0600760B RID: 30219 RVA: 0x001B697B File Offset: 0x001B4B7B
		public List<ControlSetting> ControlSettings
		{
			get
			{
				if (this._controlSettings == null)
				{
					this._controlSettings = new List<ControlSetting>();
				}
				return this._controlSettings;
			}
			set
			{
				this._controlSettings = value;
			}
		}

		// Token: 0x17002666 RID: 9830
		// (get) Token: 0x0600760C RID: 30220 RVA: 0x001B6984 File Offset: 0x001B4B84
		// (set) Token: 0x0600760D RID: 30221 RVA: 0x001B698C File Offset: 0x001B4B8C
		[DefaultValue("")]
		public string UniqueId { get; set; }

		// Token: 0x17002667 RID: 9831
		// (get) Token: 0x0600760E RID: 30222 RVA: 0x001B6995 File Offset: 0x001B4B95
		// (set) Token: 0x0600760F RID: 30223 RVA: 0x001B699D File Offset: 0x001B4B9D
		public string UniqueKey { get; set; }

		// Token: 0x04002060 RID: 8288
		private List<ControlSetting> _controlSettings;
	}
}
