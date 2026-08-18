using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.Output
{
	// Token: 0x020002CD RID: 717
	public class TempCache : Dictionary<string, TempCacheObject>
	{
		// Token: 0x060015BA RID: 5562 RVA: 0x0001B158 File Offset: 0x00019358
		public TempCacheObject AddLocalItem(string name, object obj)
		{
			TempCacheObject tempCacheObject = new TempCacheObject
			{
				Object = obj
			};
			base.Add(name, tempCacheObject);
			return tempCacheObject;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0001B184 File Offset: 0x00019384
		public TempCacheObject AddGlobalItem(string name, object obj)
		{
			TempCacheObject tempCacheObject = new TempCacheObject
			{
				IsGlobal = true,
				Object = obj
			};
			base.Add(name, tempCacheObject);
			return tempCacheObject;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0001B1B8 File Offset: 0x000193B8
		public void ClearNonGlobalItems()
		{
			List<string> list = (from g in this
			where !g.Value.IsGlobal
			select g into h
			select h.Key).ToList<string>();
			foreach (string key in list)
			{
				bool flag = base.ContainsKey(key);
				if (flag)
				{
					base.Remove(key);
				}
			}
		}
	}
}
