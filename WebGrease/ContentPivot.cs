using System;
using System.Collections.Generic;
using System.Linq;
using WebGrease.Configuration;
using WebGrease.Extensions;

namespace WebGrease
{
	// Token: 0x020000FA RID: 250
	public class ContentPivot
	{
		// Token: 0x0600103F RID: 4159 RVA: 0x0004924D File Offset: 0x0004744D
		public ContentPivot(params ResourcePivotKey[] pivotKeys)
		{
			this.PivotKeys = pivotKeys;
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0004925C File Offset: 0x0004745C
		// (set) Token: 0x06001041 RID: 4161 RVA: 0x00049264 File Offset: 0x00047464
		public IEnumerable<ResourcePivotKey> PivotKeys { get; private set; }

		// Token: 0x1700040C RID: 1036
		public string this[string groupKey]
		{
			get
			{
				return (from pk in this.PivotKeys
				where pk.GroupKey.Equals(groupKey)
				select pk.Key).FirstOrDefault<string>();
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x000492FC File Offset: 0x000474FC
		public override string ToString()
		{
			string format = "{0}";
			object[] array = new object[1];
			array[0] = string.Join("-", from p in this.PivotKeys
			select p.Key into i
			where !i.IsNullOrWhitespace()
			select i);
			return format.InvariantFormat(array);
		}
	}
}
