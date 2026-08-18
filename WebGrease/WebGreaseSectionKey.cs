using System;
using System.Collections.Generic;
using System.Linq;
using WebGrease.Configuration;
using WebGrease.Extensions;

namespace WebGrease
{
	// Token: 0x020001BD RID: 445
	public class WebGreaseSectionKey
	{
		// Token: 0x060016F2 RID: 5874 RVA: 0x00083B0C File Offset: 0x00081D0C
		public WebGreaseSectionKey(IWebGreaseContext context, string category, ContentItem cacheVarByContentItem, object cacheVarBySetting, IFileSet cacheVarByFileSet, string uniqueKey = null)
		{
			this.Category = category;
			this.Value = uniqueKey;
			if (string.IsNullOrWhiteSpace(uniqueKey))
			{
				List<CacheVaryByFile> list = new List<CacheVaryByFile>();
				List<string> list2 = new List<string>();
				if (cacheVarByContentItem != null)
				{
					list.Add(CacheVaryByFile.FromFile(context, cacheVarByContentItem));
					list2.Add(cacheVarByContentItem.ResourcePivotKeys.ToJson(false));
				}
				if (cacheVarByFileSet != null)
				{
					list2.Add(cacheVarByFileSet.ToJson(false));
				}
				if (context.Configuration.Overrides != null)
				{
					list2.Add(context.Configuration.Overrides.UniqueKey);
				}
				list2.Add(cacheVarBySetting.ToJson(true));
				this.Value = "1.0.11|" + category + "|" + string.Join("|", (from vbf in list
				select vbf.Hash).Concat(list2));
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00083BF4 File Offset: 0x00081DF4
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x00083BFC File Offset: 0x00081DFC
		public string Category { get; private set; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00083C05 File Offset: 0x00081E05
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x00083C0D File Offset: 0x00081E0D
		public string Value { get; private set; }

		// Token: 0x04000C12 RID: 3090
		private const string CacheSectionFileVersionKey = "1.0.11";

		// Token: 0x04000C13 RID: 3091
		private const string Delimiter = "|";
	}
}
