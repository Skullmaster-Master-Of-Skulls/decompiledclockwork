using System;
using System.Collections;
using System.Configuration;
using System.IO;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020000D9 RID: 217
	internal sealed class AppSettingPatternConverter : PatternConverter
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x000147A4 File Offset: 0x000129A4
		private static IDictionary AppSettingsDictionary
		{
			get
			{
				if (AppSettingPatternConverter._appSettingsHashTable == null)
				{
					Hashtable hashtable = new Hashtable();
					foreach (object obj in ConfigurationManager.AppSettings)
					{
						string text = (string)obj;
						hashtable.Add(text, ConfigurationManager.AppSettings[text]);
					}
					AppSettingPatternConverter._appSettingsHashTable = hashtable;
				}
				return AppSettingPatternConverter._appSettingsHashTable;
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00014820 File Offset: 0x00012A20
		protected override void Convert(TextWriter writer, object state)
		{
			if (this.Option != null)
			{
				PatternConverter.WriteObject(writer, null, ConfigurationManager.AppSettings[this.Option]);
				return;
			}
			PatternConverter.WriteDictionary(writer, null, AppSettingPatternConverter.AppSettingsDictionary);
		}

		// Token: 0x0400028A RID: 650
		private static Hashtable _appSettingsHashTable;
	}
}
