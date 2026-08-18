using System;

namespace TechnoPro.Common.Public.Entities.ConnectionString
{
	// Token: 0x0200043F RID: 1087
	public class ClockWorkApplicationConnectionString : BusinessBase<string>
	{
		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x000252B4 File Offset: 0x000234B4
		// (set) Token: 0x06002102 RID: 8450 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string ApplicationId
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

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x000252CC File Offset: 0x000234CC
		// (set) Token: 0x06002104 RID: 8452 RVA: 0x000252D4 File Offset: 0x000234D4
		public ClockWorkConnectionString ConnectionString { get; set; }

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x000252E0 File Offset: 0x000234E0
		public eTechnoProProductNames ProductName
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.Id);
				eTechnoProProductNames result;
				if (flag)
				{
					result = eTechnoProProductNames.Unknown;
				}
				else
				{
					string[] array = this.Id.Split(new char[]
					{
						'.'
					}, StringSplitOptions.RemoveEmptyEntries);
					result = ((array.Length != 0 && Enum.IsDefined(typeof(eTechnoProProductNames), array[0])) ? ((eTechnoProProductNames)Enum.Parse(typeof(eTechnoProProductNames), array[0])) : eTechnoProProductNames.Unknown);
				}
				return result;
			}
		}
	}
}
