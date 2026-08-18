using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Description
{
	// Token: 0x02000436 RID: 1078
	public sealed class ServiceHealthDataCollection : KeyedCollection<string, ServiceHealthData>
	{
		// Token: 0x06002A0D RID: 10765 RVA: 0x000A2DEE File Offset: 0x000A0FEE
		public ServiceHealthDataCollection() : base(StringComparer.InvariantCultureIgnoreCase)
		{
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000A2DFB File Offset: 0x000A0FFB
		protected override string GetKeyForItem(ServiceHealthData element)
		{
			return element.Key;
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000A2E03 File Offset: 0x000A1003
		public void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.InternalAdd(key, new string[]
			{
				value
			}, false);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x000A2E25 File Offset: 0x000A1025
		public void Add(string key, string[] values)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.InternalAdd(key, values, true);
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x000A2E40 File Offset: 0x000A1040
		private void InternalAdd(string key, string[] values, bool isArray)
		{
			if (isArray)
			{
				int num = (values == null) ? 1 : (values.Length + 1);
				string[] array = new string[num];
				if (values != null && values.Length != 0)
				{
					values.CopyTo(array, 0);
				}
				array[num - 1] = string.Empty;
				values = array;
			}
			base.Add(new ServiceHealthData(key, values));
		}
	}
}
