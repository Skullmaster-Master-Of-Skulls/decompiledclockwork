using System;
using System.Collections;
using System.Globalization;

namespace System.Diagnostics
{
	// Token: 0x020004DB RID: 1243
	public class InstanceDataCollectionCollection : DictionaryBase
	{
		// Token: 0x06002EF3 RID: 12019 RVA: 0x000D2E7F File Offset: 0x000D107F
		[Obsolete("This constructor has been deprecated.  Please use System.Diagnostics.PerformanceCounterCategory.ReadCategory() to get an instance of this collection instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public InstanceDataCollectionCollection()
		{
		}

		// Token: 0x17000B70 RID: 2928
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

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000D2EC0 File Offset: 0x000D10C0
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x000D2ECD File Offset: 0x000D10CD
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000D2EDC File Offset: 0x000D10DC
		internal void Add(string counterName, InstanceDataCollection value)
		{
			object key = counterName.ToLower(CultureInfo.InvariantCulture);
			base.Dictionary.Add(key, value);
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000D2F04 File Offset: 0x000D1104
		public bool Contains(string counterName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			object key = counterName.ToLower(CultureInfo.InvariantCulture);
			return base.Dictionary.Contains(key);
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000D2F37 File Offset: 0x000D1137
		public void CopyTo(InstanceDataCollection[] counters, int index)
		{
			base.Dictionary.Values.CopyTo(counters, index);
		}
	}
}
