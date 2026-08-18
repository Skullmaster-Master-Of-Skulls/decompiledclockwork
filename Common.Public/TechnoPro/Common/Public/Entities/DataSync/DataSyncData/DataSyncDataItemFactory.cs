using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003E8 RID: 1000
	public static class DataSyncDataItemFactory
	{
		// Token: 0x06001EC7 RID: 7879 RVA: 0x0002220C File Offset: 0x0002040C
		public static DataSyncDataItemBase CreateDataSyncDataItem(Type type, object obj)
		{
			bool flag = type == typeof(DateTime);
			DataSyncDataItemBase result;
			if (flag)
			{
				result = new DataSyncDataItemDateTime((obj == null || obj is DBNull) ? null : new DateTime?((DateTime)obj));
			}
			else
			{
				bool flag2 = type == typeof(int);
				if (flag2)
				{
					result = new DataSyncDataItemInt((obj == null || obj is DBNull) ? null : new int?((int)obj));
				}
				else
				{
					bool flag3 = type == typeof(bool);
					if (flag3)
					{
						result = new DataSyncDataItemBool((obj == null || obj is DBNull) ? null : new bool?((bool)obj));
					}
					else
					{
						bool flag4 = type == typeof(byte[]);
						if (flag4)
						{
							result = new DataSyncDataItemBinaryData((obj == null || obj is DBNull) ? null : ((byte[])obj));
						}
						else
						{
							string text = (obj == null || obj is DBNull) ? null : obj.ToString();
							result = new DataSyncDataItemString(text);
						}
					}
				}
			}
			return result;
		}
	}
}
