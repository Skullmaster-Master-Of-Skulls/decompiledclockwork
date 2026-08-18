using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.Core.Adapters
{
	// Token: 0x02000179 RID: 377
	internal static class UpdateFileTypeAdapter
	{
		// Token: 0x06001052 RID: 4178 RVA: 0x00078700 File Offset: 0x00076900
		internal static UpdateFileInfo WithMaxVersion(this IEnumerable<UpdateFileInfo> updates)
		{
			UpdateFileInfo updateFileInfo = null;
			Version v = null;
			foreach (UpdateFileInfo updateFileInfo2 in updates)
			{
				bool flag = updateFileInfo == null || new Version(updateFileInfo2.Version) > v;
				if (flag)
				{
					updateFileInfo = updateFileInfo2;
					v = new Version(updateFileInfo.Version);
				}
			}
			return updateFileInfo;
		}
	}
}
