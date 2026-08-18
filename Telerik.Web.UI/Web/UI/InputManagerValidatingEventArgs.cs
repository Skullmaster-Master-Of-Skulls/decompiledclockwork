using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001915 RID: 6421
	public class InputManagerValidatingEventArgs : EventArgs
	{
		// Token: 0x0600F934 RID: 63796 RVA: 0x00384678 File Offset: 0x00382878
		public InputManagerValidatingEventArgs(InputSetting setting, bool isValid)
		{
			this._setting = setting;
			this._isValid = isValid;
		}

		// Token: 0x17004B41 RID: 19265
		// (get) Token: 0x0600F935 RID: 63797 RVA: 0x0038468E File Offset: 0x0038288E
		public InputSetting Setting
		{
			get
			{
				return this._setting;
			}
		}

		// Token: 0x17004B42 RID: 19266
		// (get) Token: 0x0600F936 RID: 63798 RVA: 0x00384696 File Offset: 0x00382896
		// (set) Token: 0x0600F937 RID: 63799 RVA: 0x0038469E File Offset: 0x0038289E
		public object Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x17004B43 RID: 19267
		// (get) Token: 0x0600F938 RID: 63800 RVA: 0x003846A7 File Offset: 0x003828A7
		// (set) Token: 0x0600F939 RID: 63801 RVA: 0x003846AF File Offset: 0x003828AF
		public bool Canceled
		{
			get
			{
				return this._canceled;
			}
			set
			{
				this._canceled = value;
			}
		}

		// Token: 0x17004B44 RID: 19268
		// (get) Token: 0x0600F93A RID: 63802 RVA: 0x003846B8 File Offset: 0x003828B8
		// (set) Token: 0x0600F93B RID: 63803 RVA: 0x003846C0 File Offset: 0x003828C0
		public bool IsValid
		{
			get
			{
				return this._isValid;
			}
			set
			{
				this._isValid = value;
			}
		}

		// Token: 0x040046DD RID: 18141
		private InputSetting _setting;

		// Token: 0x040046DE RID: 18142
		private bool _canceled;

		// Token: 0x040046DF RID: 18143
		private bool _isValid;

		// Token: 0x040046E0 RID: 18144
		private object context;
	}
}
