using System;
using System.Collections.Specialized;

namespace System.Web.Configuration
{
	// Token: 0x02000698 RID: 1688
	[Serializable]
	public class AdapterDictionary : OrderedDictionary
	{
		// Token: 0x1700174A RID: 5962
		public string this[string key]
		{
			get
			{
				return (string)base[key];
			}
			set
			{
				base[key] = value;
			}
		}
	}
}
