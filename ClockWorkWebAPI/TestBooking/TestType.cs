using System;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	public class TestType
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600037E RID: 894 RVA: 0x000196F4 File Offset: 0x000178F4
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0001970C File Offset: 0x0001790C
		public int TestTypeId
		{
			get
			{
				return this.testTypeId;
			}
			set
			{
				this.testTypeId = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00019718 File Offset: 0x00017918
		// (set) Token: 0x06000381 RID: 897 RVA: 0x00019730 File Offset: 0x00017930
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x040001C3 RID: 451
		private int testTypeId;

		// Token: 0x040001C4 RID: 452
		private string description;
	}
}
