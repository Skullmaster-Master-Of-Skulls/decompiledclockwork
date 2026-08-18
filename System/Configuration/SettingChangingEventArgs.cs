using System;
using System.ComponentModel;

namespace System.Configuration
{
	// Token: 0x020006E6 RID: 1766
	public class SettingChangingEventArgs : CancelEventArgs
	{
		// Token: 0x0600369E RID: 13982 RVA: 0x000E9178 File Offset: 0x000E8178
		public SettingChangingEventArgs(string settingName, string settingClass, string settingKey, object newValue, bool cancel) : base(cancel)
		{
			this._settingName = settingName;
			this._settingClass = settingClass;
			this._settingKey = settingKey;
			this._newValue = newValue;
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x0600369F RID: 13983 RVA: 0x000E919F File Offset: 0x000E819F
		public object NewValue
		{
			get
			{
				return this._newValue;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060036A0 RID: 13984 RVA: 0x000E91A7 File Offset: 0x000E81A7
		public string SettingClass
		{
			get
			{
				return this._settingClass;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x000E91AF File Offset: 0x000E81AF
		public string SettingName
		{
			get
			{
				return this._settingName;
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060036A2 RID: 13986 RVA: 0x000E91B7 File Offset: 0x000E81B7
		public string SettingKey
		{
			get
			{
				return this._settingKey;
			}
		}

		// Token: 0x0400319B RID: 12699
		private string _settingClass;

		// Token: 0x0400319C RID: 12700
		private string _settingName;

		// Token: 0x0400319D RID: 12701
		private string _settingKey;

		// Token: 0x0400319E RID: 12702
		private object _newValue;
	}
}
