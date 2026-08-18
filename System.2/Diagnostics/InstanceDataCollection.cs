using System;
using System.Collections;
using System.Globalization;

namespace System.Diagnostics
{
	// Token: 0x020004DA RID: 1242
	public class InstanceDataCollection : DictionaryBase
	{
		// Token: 0x06002EEB RID: 12011 RVA: 0x000D2D88 File Offset: 0x000D0F88
		[Obsolete("This constructor has been deprecated.  Please use System.Diagnostics.InstanceDataCollectionCollection.get_Item to get an instance of this collection instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public InstanceDataCollection(string counterName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			this.counterName = counterName;
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06002EEC RID: 12012 RVA: 0x000D2DA5 File Offset: 0x000D0FA5
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x000D2DAD File Offset: 0x000D0FAD
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06002EEE RID: 12014 RVA: 0x000D2DBA File Offset: 0x000D0FBA
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		// Token: 0x17000B6F RID: 2927
		public InstanceData this[string instanceName]
		{
			get
			{
				if (instanceName == null)
				{
					throw new ArgumentNullException("instanceName");
				}
				if (instanceName.Length == 0)
				{
					instanceName = "systemdiagnosticsperfcounterlibsingleinstance";
				}
				object key = instanceName.ToLower(CultureInfo.InvariantCulture);
				return (InstanceData)base.Dictionary[key];
			}
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000D2E10 File Offset: 0x000D1010
		internal void Add(string instanceName, InstanceData value)
		{
			object key = instanceName.ToLower(CultureInfo.InvariantCulture);
			base.Dictionary.Add(key, value);
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x000D2E38 File Offset: 0x000D1038
		public bool Contains(string instanceName)
		{
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			object key = instanceName.ToLower(CultureInfo.InvariantCulture);
			return base.Dictionary.Contains(key);
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x000D2E6B File Offset: 0x000D106B
		public void CopyTo(InstanceData[] instances, int index)
		{
			base.Dictionary.Values.CopyTo(instances, index);
		}

		// Token: 0x040027A6 RID: 10150
		private string counterName;
	}
}
