using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data
{
	// Token: 0x02000422 RID: 1058
	public class CustomDataSerialized : BusinessBase<Guid>
	{
		// Token: 0x06002029 RID: 8233 RVA: 0x0000EDF5 File Offset: 0x0000CFF5
		public CustomDataSerialized()
		{
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00024753 File Offset: 0x00022953
		public CustomDataSerialized(eCustomDataPrimitiveType dataType, Guid dataInstanceId, string xml, Guid? dataValueJoinId = null, IDictionary<string, object> extraValues = null)
		{
			this.DataInstanceId = dataInstanceId;
			this.DataPrimitiveType = dataType;
			this.DataValueJoinId = dataValueJoinId;
			this.ExtraValues = extraValues;
			this.DataValueXml = xml;
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x00024788 File Offset: 0x00022988
		// (set) Token: 0x0600202C RID: 8236 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public virtual Guid DataInstanceId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x000247A0 File Offset: 0x000229A0
		// (set) Token: 0x0600202E RID: 8238 RVA: 0x000247A8 File Offset: 0x000229A8
		public Guid? DataValueJoinId { get; set; }

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x000247B1 File Offset: 0x000229B1
		// (set) Token: 0x06002030 RID: 8240 RVA: 0x000247B9 File Offset: 0x000229B9
		public string DataValueXml { get; set; }

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x000247C2 File Offset: 0x000229C2
		// (set) Token: 0x06002032 RID: 8242 RVA: 0x000247CA File Offset: 0x000229CA
		public IDictionary<string, object> ExtraValues { get; set; }

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x000247D3 File Offset: 0x000229D3
		// (set) Token: 0x06002034 RID: 8244 RVA: 0x000247DB File Offset: 0x000229DB
		public eCustomDataPrimitiveType DataPrimitiveType { get; set; }
	}
}
