using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A40 RID: 2624
	[MessageContract(IsWrapped = false)]
	internal class UtilityInfo
	{
		// Token: 0x060067EC RID: 26604 RVA: 0x0018401C File Offset: 0x0018221C
		public UtilityInfo()
		{
			this.body = new UtilityInfo.UtilityInfoDC();
		}

		// Token: 0x060067ED RID: 26605 RVA: 0x0018402F File Offset: 0x0018222F
		public UtilityInfo(uint useful, uint total)
		{
			this.body = new UtilityInfo.UtilityInfoDC(useful, total);
		}

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x060067EE RID: 26606 RVA: 0x00184044 File Offset: 0x00182244
		public uint Useful
		{
			get
			{
				return this.body.useful;
			}
		}

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x060067EF RID: 26607 RVA: 0x00184051 File Offset: 0x00182251
		public uint Total
		{
			get
			{
				return this.body.total;
			}
		}

		// Token: 0x060067F0 RID: 26608 RVA: 0x0018405E File Offset: 0x0018225E
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x04003B9E RID: 15262
		[MessageBodyMember(Name = "LinkUtility", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private UtilityInfo.UtilityInfoDC body;

		// Token: 0x02000E77 RID: 3703
		[DataContract(Name = "LinkUtilityInfo", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class UtilityInfoDC
		{
			// Token: 0x060083F0 RID: 33776 RVA: 0x001E8079 File Offset: 0x001E6279
			public UtilityInfoDC()
			{
			}

			// Token: 0x060083F1 RID: 33777 RVA: 0x001E8081 File Offset: 0x001E6281
			public UtilityInfoDC(uint useful, uint total)
			{
				this.useful = useful;
				this.total = total;
			}

			// Token: 0x04004B21 RID: 19233
			[DataMember(Name = "Useful")]
			public uint useful;

			// Token: 0x04004B22 RID: 19234
			[DataMember(Name = "Total")]
			public uint total;
		}
	}
}
