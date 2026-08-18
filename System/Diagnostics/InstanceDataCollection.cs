using System;
using System.Collections;
using System.Globalization;

namespace System.Diagnostics
{
	// Token: 0x02000760 RID: 1888
	public class InstanceDataCollection : DictionaryBase
	{
		// Token: 0x06003A0B RID: 14859 RVA: 0x000F58FB File Offset: 0x000F48FB
		[Obsolete("This constructor has been deprecated.  Please use System.Diagnostics.InstanceDataCollectionCollection.get_Item to get an instance of this collection instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public InstanceDataCollection(string counterName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			this.counterName = counterName;
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06003A0C RID: 14860 RVA: 0x000F5918 File Offset: 0x000F4918
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x000F5920 File Offset: 0x000F4920
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x000F592D File Offset: 0x000F492D
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		// Token: 0x17000D91 RID: 3473
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

		// Token: 0x06003A10 RID: 14864 RVA: 0x000F5984 File Offset: 0x000F4984
		internal void Add(string instanceName, InstanceData value)
		{
			object key = instanceName.ToLower(CultureInfo.InvariantCulture);
			base.Dictionary.Add(key, value);
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000F59AC File Offset: 0x000F49AC
		public bool Contains(string instanceName)
		{
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			object key = instanceName.ToLower(CultureInfo.InvariantCulture);
			return base.Dictionary.Contains(key);
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000F59DF File Offset: 0x000F49DF
		public void CopyTo(InstanceData[] instances, int index)
		{
			base.Dictionary.Values.CopyTo(instances, index);
		}

		// Token: 0x040032FE RID: 13054
		private string counterName;
	}
}
