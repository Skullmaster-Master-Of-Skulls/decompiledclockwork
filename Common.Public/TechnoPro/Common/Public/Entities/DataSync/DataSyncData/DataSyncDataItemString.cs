using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003EA RID: 1002
	public class DataSyncDataItemString : DataSyncDataItemBase
	{
		// Token: 0x06001ECF RID: 7887 RVA: 0x00022403 File Offset: 0x00020603
		public DataSyncDataItemString()
		{
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x0002240D File Offset: 0x0002060D
		public DataSyncDataItemString(string text)
		{
			base.DataValue = text;
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x0002241F File Offset: 0x0002061F
		public string Text
		{
			get
			{
				object dataValue = base.DataValue;
				return (dataValue != null) ? dataValue.ToString().Trim() : null;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x00022438 File Offset: 0x00020638
		public override bool HasValue
		{
			get
			{
				string text = this.Text;
				return text != null && text.Length > 0;
			}
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00022450 File Offset: 0x00020650
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
				DataSyncDataItemString dataSyncDataItemString = item.ConvertTo<DataSyncDataItemString>();
				string text = this.Text;
				string text2 = dataSyncDataItemString.Text;
				bool flag2 = text == null && text2 == null;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = text == null || text2 == null;
					result = (!flag3 && text.Equals(text2, StringComparison.OrdinalIgnoreCase));
				}
			}
			return result;
		}
	}
}
