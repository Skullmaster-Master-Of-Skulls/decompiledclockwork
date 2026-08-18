using System;
using System.Collections;

namespace System.Configuration
{
	// Token: 0x0200002B RID: 43
	public class ConfigurationLocationCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06000210 RID: 528 RVA: 0x0000FA25 File Offset: 0x0000DC25
		internal ConfigurationLocationCollection(ICollection col)
		{
			base.InnerList.AddRange(col);
		}

		// Token: 0x17000088 RID: 136
		public ConfigurationLocation this[int index]
		{
			get
			{
				return (ConfigurationLocation)base.InnerList[index];
			}
		}
	}
}
