using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003EE RID: 1006
	public class DataSyncDataItemBinaryData : DataSyncDataItemBase
	{
		// Token: 0x06001EE3 RID: 7907 RVA: 0x00022403 File Offset: 0x00020603
		public DataSyncDataItemBinaryData()
		{
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0002240D File Offset: 0x0002060D
		public DataSyncDataItemBinaryData(byte[] data)
		{
			base.DataValue = data;
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x000229C4 File Offset: 0x00020BC4
		public byte[] BinaryData
		{
			get
			{
				bool flag = base.DataValue is byte[];
				byte[] result;
				if (flag)
				{
					result = (byte[])base.DataValue;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x000229F7 File Offset: 0x00020BF7
		public override bool HasValue
		{
			get
			{
				return this.BinaryData != null && this.BinaryData.Length != 0;
			}
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x00022A10 File Offset: 0x00020C10
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
				DataSyncDataItemBinaryData dataSyncDataItemBinaryData = item.ConvertTo<DataSyncDataItemBinaryData>();
				byte[] binaryData = this.BinaryData;
				byte[] binaryData2 = dataSyncDataItemBinaryData.BinaryData;
				bool flag2 = binaryData == null && binaryData2 == null;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = binaryData == null || binaryData2 == null;
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = binaryData.Length != binaryData2.Length;
						if (flag4)
						{
							result = false;
						}
						else
						{
							for (int i = 0; i < binaryData.Length; i++)
							{
								bool flag5 = binaryData[i] != binaryData2[i];
								if (flag5)
								{
									return false;
								}
							}
							result = true;
						}
					}
				}
			}
			return result;
		}
	}
}
