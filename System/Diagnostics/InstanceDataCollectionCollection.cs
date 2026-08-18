using System;
using System.Collections;
using System.Globalization;

namespace System.Diagnostics
{
	// Token: 0x02000761 RID: 1889
	public class InstanceDataCollectionCollection : DictionaryBase
	{
		// Token: 0x06003A13 RID: 14867 RVA: 0x000F59F3 File Offset: 0x000F49F3
		[Obsolete("This constructor has been deprecated.  Please use System.Diagnostics.PerformanceCounterCategory.ReadCategory() to get an instance of this collection instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public InstanceDataCollectionCollection()
		{
		}

		// Token: 0x17000D92 RID: 3474
		public InstanceDataCollection this[string counterName]
		{
			get
			{
				if (counterName == null)
				{
					throw new ArgumentNullException("counterName");
				}
				object key = counterName.ToLower(CultureInfo.InvariantCulture);
				return (InstanceDataCollection)base.Dictionary[key];
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06003A15 RID: 14869 RVA: 0x000F5A34 File Offset: 0x000F4A34
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06003A16 RID: 14870 RVA: 0x000F5A41 File Offset: 0x000F4A41
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000F5A50 File Offset: 0x000F4A50
		internal void Add(string counterName, InstanceDataCollection value)
		{
			object key = counterName.ToLower(CultureInfo.InvariantCulture);
			base.Dictionary.Add(key, value);
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000F5A78 File Offset: 0x000F4A78
		public bool Contains(string counterName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			object key = counterName.ToLower(CultureInfo.InvariantCulture);
			return base.Dictionary.Contains(key);
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x000F5AAB File Offset: 0x000F4AAB
		public void CopyTo(InstanceDataCollection[] counters, int index)
		{
			base.Dictionary.Values.CopyTo(counters, index);
		}
	}
}
