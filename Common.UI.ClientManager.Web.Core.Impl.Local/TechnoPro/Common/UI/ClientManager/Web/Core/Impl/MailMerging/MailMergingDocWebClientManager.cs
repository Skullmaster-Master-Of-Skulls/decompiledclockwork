using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.Core.MailMerging;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.MailMerging
{
	// Token: 0x0200000A RID: 10
	public class MailMergingDocWebClientManager : IMailMergingDocWebClientManager
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002934 File Offset: 0x00000B34
		public byte[] GenerateLetter(Setting englishTemplateSetting, Setting frenchTemplateSetting, bool inFrench, int pid, int lucid, out string filename)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int num = 0;
			bool flag = !inFrench;
			if (flag)
			{
				num = webSettingsClientManager.GetSettingValue<int>(englishTemplateSetting);
			}
			bool flag2 = num <= 0;
			if (flag2)
			{
				num = webSettingsClientManager.GetSettingValue<int>(frenchTemplateSetting);
			}
			bool flag3 = num < 1;
			if (flag3)
			{
				num = 1;
			}
			IMailMergingDocClientManager mailMergingDocClientManager = new MailMergingDocClientManager();
			BinaryFileDTO binaryFileDTO = mailMergingDocClientManager.MailMergeAccommodationLetter(new List<int>
			{
				lucid
			}, new MailMergeContextWithCustomDictionaryDTO
			{
				Context = new MailMergeContextDTO
				{
					PersonId = pid,
					WhoAmId = ObjectFactory.Resolve<ClientCache>().WhoAmIId
				},
				CustomDictionary = new MailMergeCustomDictionaryDTO()
			}, eFileFormatDTO.PDF, num);
			filename = ((binaryFileDTO == null) ? null : binaryFileDTO.FileName);
			return (binaryFileDTO == null) ? null : binaryFileDTO.ByteArray;
		}
	}
}
