using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;

namespace TechnoPro.Common.UI.Web.AccommodationsRequest.Adapters
{
	// Token: 0x02000008 RID: 8
	public static class AccommodationData
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00004F8C File Offset: 0x0000318C
		public static string GetDisplayString(this AccommodationDataDTO accommodationData)
		{
			bool flag = accommodationData == null || accommodationData.Data == null || accommodationData.Data.Field == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string description = accommodationData.Data.Field.GetDescription();
				bool flag2 = accommodationData.Data.Value == null;
				string text;
				if (flag2)
				{
					text = string.Empty;
				}
				else
				{
					Type type = accommodationData.Data.Value.GetType();
					bool flag3 = type == typeof(string) || type == typeof(int) || type == typeof(double);
					if (flag3)
					{
						text = accommodationData.Data.Value.ToString();
					}
					else
					{
						bool flag4 = type == typeof(DateTime);
						if (flag4)
						{
							text = ((DateTime)accommodationData.Data.Value).ToString("yyyy-MM-dd");
						}
						else
						{
							text = string.Empty;
						}
					}
				}
				result = string.Format("{0}{1}{2}", description, (text.Length > 0) ? ": " : "", text);
			}
			return result;
		}
	}
}
