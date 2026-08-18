using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Web.ClientServices.Providers
{
	// Token: 0x02000116 RID: 278
	public class SettingsSavedEventArgs : EventArgs
	{
		// Token: 0x06000EA4 RID: 3748 RVA: 0x00034C80 File Offset: 0x00032E80
		public SettingsSavedEventArgs(IEnumerable<string> failedSettingsList)
		{
			List<string> list = (failedSettingsList == null) ? new List<string>() : new List<string>(failedSettingsList);
			this._failedSettingsList = new ReadOnlyCollection<string>(list);
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00034CB0 File Offset: 0x00032EB0
		public ReadOnlyCollection<string> FailedSettingsList
		{
			get
			{
				return this._failedSettingsList;
			}
		}

		// Token: 0x0400041F RID: 1055
		private ReadOnlyCollection<string> _failedSettingsList;
	}
}
