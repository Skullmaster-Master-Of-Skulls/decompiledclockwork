using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003ED RID: 1005
	public class DataSyncDataItemDateTime : DataSyncDataItemBase
	{
		// Token: 0x06001EDE RID: 7902 RVA: 0x00022403 File Offset: 0x00020603
		public DataSyncDataItemDateTime()
		{
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x0002282A File Offset: 0x00020A2A
		public DataSyncDataItemDateTime(DateTime? dt)
		{
			base.DataValue = dt;
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x00022844 File Offset: 0x00020A44
		public DateTime? DateTimeValue
		{
			get
			{
				bool flag = base.DataValue == null;
				DateTime? result;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = base.DataValue is DateTime?;
					if (flag2)
					{
						result = (DateTime?)base.DataValue;
					}
					else
					{
						bool flag3 = base.DataValue is DateTime;
						if (flag3)
						{
							result = new DateTime?((DateTime)base.DataValue);
						}
						else
						{
							string text = base.DataValue.ToString().Trim();
							bool flag4 = text.Length < 1;
							if (flag4)
							{
								result = null;
							}
							else
							{
								DateTime value;
								bool flag5 = !DateTime.TryParse(text, out value);
								if (flag5)
								{
									result = null;
								}
								else
								{
									result = new DateTime?(value);
								}
							}
						}
					}
				}
				return result;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x00022914 File Offset: 0x00020B14
		public override bool HasValue
		{
			get
			{
				return this.DateTimeValue != null;
			}
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x00022930 File Offset: 0x00020B30
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
				DataSyncDataItemDateTime dataSyncDataItemDateTime = item.ConvertTo<DataSyncDataItemDateTime>();
				DateTime? dateTimeValue = this.DateTimeValue;
				DateTime? dateTimeValue2 = dataSyncDataItemDateTime.DateTimeValue;
				bool flag2 = dateTimeValue == null && dateTimeValue2 == null;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = dateTimeValue == null || dateTimeValue2 == null;
					result = (!flag3 && dateTimeValue.Value == dateTimeValue2.Value);
				}
			}
			return result;
		}
	}
}
