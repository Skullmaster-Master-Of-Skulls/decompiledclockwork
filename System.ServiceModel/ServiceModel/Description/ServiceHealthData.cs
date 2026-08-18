using System;

namespace System.ServiceModel.Description
{
	// Token: 0x02000435 RID: 1077
	public sealed class ServiceHealthData
	{
		// Token: 0x06002A08 RID: 10760 RVA: 0x000A2D90 File Offset: 0x000A0F90
		public ServiceHealthData(string key, string[] values)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException("key");
			}
			this.Key = key;
			this.Values = values;
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x000A2DB9 File Offset: 0x000A0FB9
		// (set) Token: 0x06002A0A RID: 10762 RVA: 0x000A2DC1 File Offset: 0x000A0FC1
		public string Key
		{
			get
			{
				return this.key;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException("Key");
				}
				this.key = value;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06002A0B RID: 10763 RVA: 0x000A2DDD File Offset: 0x000A0FDD
		// (set) Token: 0x06002A0C RID: 10764 RVA: 0x000A2DE5 File Offset: 0x000A0FE5
		public string[] Values { get; set; }

		// Token: 0x040022B0 RID: 8880
		private string key;
	}
}
