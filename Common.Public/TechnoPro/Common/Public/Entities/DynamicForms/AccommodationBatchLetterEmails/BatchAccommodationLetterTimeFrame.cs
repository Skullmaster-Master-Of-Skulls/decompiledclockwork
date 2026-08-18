using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AccommodationBatchLetterEmails
{
	// Token: 0x020003BD RID: 957
	public class BatchAccommodationLetterTimeFrame
	{
		// Token: 0x06001D2B RID: 7467 RVA: 0x00021094 File Offset: 0x0001F294
		public static IList<BatchAccommodationLetterTimeFrame> ParseFromString(string s)
		{
			string[] array = s.Split(new char[]
			{
				','
			});
			List<BatchAccommodationLetterTimeFrame> list = new List<BatchAccommodationLetterTimeFrame>();
			foreach (string text in array)
			{
				string text2 = text.Trim();
				int num = text2.IndexOf('-');
				bool flag = num > 0;
				if (flag)
				{
					string text3 = text2.Substring(0, num);
					string text4 = text2.Substring(num + 1);
					text3 += ", 2000";
					text4 += ", 2000";
					DateTime dateTime;
					DateTime dateTime2;
					bool flag2 = DateTime.TryParse(text3, out dateTime) && DateTime.TryParse(text4, out dateTime2);
					if (flag2)
					{
						list.Add(new BatchAccommodationLetterTimeFrame
						{
							StartMonth = dateTime.Month,
							StartDay = dateTime.Day,
							EndMonth = dateTime2.Month,
							EndDay = dateTime2.Day
						});
					}
				}
			}
			return list;
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06001D2C RID: 7468 RVA: 0x0002119B File Offset: 0x0001F39B
		// (set) Token: 0x06001D2D RID: 7469 RVA: 0x000211A3 File Offset: 0x0001F3A3
		public int StartMonth { get; set; }

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x000211AC File Offset: 0x0001F3AC
		// (set) Token: 0x06001D2F RID: 7471 RVA: 0x000211B4 File Offset: 0x0001F3B4
		public int StartDay { get; set; }

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x000211BD File Offset: 0x0001F3BD
		// (set) Token: 0x06001D31 RID: 7473 RVA: 0x000211C5 File Offset: 0x0001F3C5
		public int EndMonth { get; set; }

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x000211CE File Offset: 0x0001F3CE
		// (set) Token: 0x06001D33 RID: 7475 RVA: 0x000211D6 File Offset: 0x0001F3D6
		public int EndDay { get; set; }
	}
}
