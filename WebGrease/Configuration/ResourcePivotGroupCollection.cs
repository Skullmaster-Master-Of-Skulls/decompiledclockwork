using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WebGrease.Configuration
{
	// Token: 0x02000029 RID: 41
	public class ResourcePivotGroupCollection : IEnumerable<ResourcePivotGroup>, IEnumerable
	{
		// Token: 0x06000301 RID: 769 RVA: 0x000075C1 File Offset: 0x000057C1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170000B2 RID: 178
		public ResourcePivotGroup this[string groupKey]
		{
			get
			{
				ResourcePivotGroup result;
				if (this.resourcePivots.TryGetValue(groupKey, out result))
				{
					return result;
				}
				return null;
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000075EC File Offset: 0x000057EC
		public IEnumerator<ResourcePivotGroup> GetEnumerator()
		{
			return this.resourcePivots.Values.GetEnumerator();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00007600 File Offset: 0x00005800
		internal void Clear(string groupKey)
		{
			ResourcePivotGroup resourcePivotGroup = this[groupKey];
			if (resourcePivotGroup != null)
			{
				resourcePivotGroup.Keys.Clear();
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00007624 File Offset: 0x00005824
		internal void Set(string groupKey, ResourcePivotApplyMode? applyMode, IEnumerable<string> keys)
		{
			ResourcePivotGroup resourcePivotGroup = this[groupKey];
			if (resourcePivotGroup != null)
			{
				resourcePivotGroup = new ResourcePivotGroup(groupKey, applyMode ?? resourcePivotGroup.ApplyMode, resourcePivotGroup.Keys.Concat(keys));
			}
			else
			{
				resourcePivotGroup = new ResourcePivotGroup(groupKey, applyMode ?? ResourcePivotApplyMode.ApplyAsStringReplace, keys);
			}
			this.resourcePivots[groupKey] = resourcePivotGroup;
		}

		// Token: 0x04000094 RID: 148
		private readonly IDictionary<string, ResourcePivotGroup> resourcePivots = new Dictionary<string, ResourcePivotGroup>();
	}
}
