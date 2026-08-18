using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003EB RID: 1003
	public class DataSyncDataItemInt : DataSyncDataItemBase
	{
		// Token: 0x06001ED4 RID: 7892 RVA: 0x00022403 File Offset: 0x00020603
		public DataSyncDataItemInt()
		{
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x000224BE File Offset: 0x000206BE
		public DataSyncDataItemInt(int? num)
		{
			base.DataValue = num;
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x000224D8 File Offset: 0x000206D8
		public int? Num
		{
			get
			{
				bool flag = base.DataValue == null;
				int? result;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = base.DataValue is int;
					if (flag2)
					{
						result = new int?((int)base.DataValue);
					}
					else
					{
						string text = base.DataValue.ToString().Trim();
						bool flag3 = text.Length < 1;
						if (flag3)
						{
							result = null;
						}
						else
						{
							int value;
							bool flag4 = int.TryParse(text, out value);
							if (flag4)
							{
								result = new int?(value);
							}
							else
							{
								double num;
								bool flag5 = !double.TryParse(text, out num);
								if (flag5)
								{
									result = null;
								}
								else
								{
									result = new int?((int)num);
								}
							}
						}
					}
				}
				return result;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x000225A0 File Offset: 0x000207A0
		public override bool HasValue
		{
			get
			{
				return this.Num != null;
			}
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x000225BC File Offset: 0x000207BC
		public override bool Equals(DataSyncDataItemBase item)
		{
			bool flag = !base.CheckEqualsShallow(item);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				DataSyncDataItemInt dataSyncDataItemInt = item.ConvertTo<DataSyncDataItemInt>();
				int? num = this.Num;
				int? num2 = dataSyncDataItemInt.Num;
				bool flag2 = num == null && num2 == null;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = num == null || num2 == null;
					result = (!flag3 && num.Value == num2.Value);
				}
			}
			return result;
		}
	}
}
