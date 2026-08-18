using System;
using System.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x02000F4F RID: 3919
	internal class ConfigurationSettingMissingException : ConfigurationErrorsException
	{
		// Token: 0x06009592 RID: 38290 RVA: 0x0021699F File Offset: 0x00214B9F
		public ConfigurationSettingMissingException(string keyName, string sectionName, string additionalInfo)
		{
			this._keyName = keyName;
			this._sectionName = sectionName;
			this._additionalInfo = (string.IsNullOrEmpty(additionalInfo) ? string.Empty : ("\n" + additionalInfo));
		}

		// Token: 0x17002F58 RID: 12120
		// (get) Token: 0x06009593 RID: 38291 RVA: 0x002169D5 File Offset: 0x00214BD5
		public override string Message
		{
			get
			{
				return string.Format("The '{0}' key is missing from the {1} section of the application's web.config file.{2}", this._keyName, this._sectionName, this._additionalInfo);
			}
		}

		// Token: 0x04002AC8 RID: 10952
		private string _keyName;

		// Token: 0x04002AC9 RID: 10953
		private string _sectionName;

		// Token: 0x04002ACA RID: 10954
		private string _additionalInfo;
	}
}
