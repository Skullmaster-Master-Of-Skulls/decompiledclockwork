using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000DA RID: 218
	public class ResourceStrings
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x000429CA File Offset: 0x00040BCA
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x000429D2 File Offset: 0x00040BD2
		public string Name { get; set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x000429DB File Offset: 0x00040BDB
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x000429E3 File Offset: 0x00040BE3
		public IDictionary<string, string> NameValuePairs { get; private set; }

		// Token: 0x17000382 RID: 898
		public string this[string name]
		{
			get
			{
				string result;
				if (!this.NameValuePairs.TryGetValue(name, out result))
				{
					result = null;
				}
				return result;
			}
			set
			{
				this.NameValuePairs[name] = value;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00042A1B File Offset: 0x00040C1B
		public int Count
		{
			get
			{
				return this.NameValuePairs.Count;
			}
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00042A28 File Offset: 0x00040C28
		public ResourceStrings()
		{
			this.NameValuePairs = new Dictionary<string, string>();
		}
	}
}
