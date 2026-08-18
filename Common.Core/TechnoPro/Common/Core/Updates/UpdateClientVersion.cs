using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TechnoPro.Common.ICore.Updates;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Updates;
using TechnoPro.Common.Public.Entities.Updates.Adapters;

namespace TechnoPro.Common.Core.Updates
{
	// Token: 0x0200002F RID: 47
	public class UpdateClientVersion : IUpdateClientVersion
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00008FDC File Offset: 0x000071DC
		public CurrentVersionInfo CurrentVersionOnClient(FileType fileType, int addressSize)
		{
			string searchPattern = fileType.AddrSizeVersion ? string.Format("{0}.x{1}.*.{2}", fileType.Title, addressSize.ToString(), fileType.Extension) : string.Format("{0}.*.{1}", fileType.Title, fileType.Extension);
			List<string> list = Directory.GetFiles(ClockWorkUpdateSystemPathVariables.UPDATES_PATH, searchPattern).ToList<string>();
			list.Sort();
			string text = list.LastOrDefault<string>();
			string version = string.IsNullOrEmpty(text) ? string.Empty : text.GetVersion();
			string secondaryVersion = string.Empty;
			bool flag = !string.IsNullOrEmpty(fileType.SecondaryTitle);
			if (flag)
			{
				searchPattern = (fileType.AddrSizeVersion ? string.Format("{0}.x{1}.*.{2}", fileType.SecondaryTitle, addressSize.ToString(), fileType.Extension) : string.Format("{0}.*.{1}", fileType.SecondaryTitle, fileType.Extension));
				list = Directory.GetFiles(ClockWorkUpdateSystemPathVariables.UPDATES_PATH, searchPattern).ToList<string>();
				list.Sort();
				text = list.LastOrDefault<string>();
				secondaryVersion = (string.IsNullOrEmpty(text) ? string.Empty : text.GetVersion());
			}
			return new CurrentVersionInfo
			{
				Version = version,
				SecondaryVersion = secondaryVersion
			};
		}
	}
}
