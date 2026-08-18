using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003EC RID: 1004
	public class DataSyncDataItemBool : DataSyncDataItemBase
	{
		// Token: 0x06001ED9 RID: 7897 RVA: 0x00022403 File Offset: 0x00020603
		public DataSyncDataItemBool()
		{
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x0002264A File Offset: 0x0002084A
		public DataSyncDataItemBool(bool? b)
		{
			base.DataValue = b;
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x00022664 File Offset: 0x00020864
		public bool? Checked
		{
			get
			{
				bool flag = base.DataValue == null;
				bool? result;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = base.DataValue is bool?;
					if (flag2)
					{
						result = (bool?)base.DataValue;
					}
					else
					{
						bool flag3 = base.DataValue is bool;
						if (flag3)
						{
							result = new bool?((bool)base.DataValue);
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
								bool value;
								bool flag5 = bool.TryParse(text, out value);
								if (flag5)
								{
									result = new bool?(value);
								}
								else
								{
									bool flag6 = "1trueyes".IndexOf(text.ToLower()) >= 0;
									if (flag6)
									{
										result = new bool?(true);
									}
									else
									{
										bool flag7 = "0falseno".IndexOf(text.ToLower()) >= 0;
										if (flag7)
										{
											result = new bool?(false);
										}
										else
										{
											result = null;
										}
									}
								}
							}
						}
					}
				}
				return result;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x00022780 File Offset: 0x00020980
		public override bool HasValue
		{
			get
			{
				return this.Checked != null;
			}
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x0002279C File Offset: 0x0002099C
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
				DataSyncDataItemBool dataSyncDataItemBool = item.ConvertTo<DataSyncDataItemBool>();
				bool? @checked = this.Checked;
				bool? checked2 = dataSyncDataItemBool.Checked;
				bool flag2 = @checked == null && checked2 == null;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = @checked == null || checked2 == null;
					result = (!flag3 && @checked.Value == checked2.Value);
				}
			}
			return result;
		}
	}
}
