using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001916 RID: 6422
	public class InputManagerValidatedEventArgs : EventArgs
	{
		// Token: 0x0600F93C RID: 63804 RVA: 0x003846C9 File Offset: 0x003828C9
		public InputManagerValidatedEventArgs(InputSetting setting)
		{
			this._setting = setting;
		}

		// Token: 0x17004B45 RID: 19269
		// (get) Token: 0x0600F93D RID: 63805 RVA: 0x003846D8 File Offset: 0x003828D8
		public InputSetting Setting
		{
			get
			{
				return this._setting;
			}
		}

		// Token: 0x040046E1 RID: 18145
		private InputSetting _setting;
	}
}
