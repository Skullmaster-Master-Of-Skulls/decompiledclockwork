using System;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005B4 RID: 1460
	public static class AccommodationDataAdapter
	{
		// Token: 0x06002F32 RID: 12082 RVA: 0x00033C70 File Offset: 0x00031E70
		private static string GetString(AccommodationData AccommodationData, bool useShortCodes)
		{
			bool flag = AccommodationData == null || AccommodationData.Data == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				DynamicData data = AccommodationData.Data;
				bool flag2 = AccommodationData.Detail == null;
				if (flag2)
				{
					AccommodationData.Detail = new ExtendedAccommodationInfo();
				}
				bool flag3 = !AccommodationData.Detail.ShowOnLetter;
				if (flag3)
				{
					result = "";
				}
				else
				{
					bool offline = AccommodationData.Detail.Offline;
					if (offline)
					{
						result = "";
					}
					else
					{
						string text;
						if (useShortCodes)
						{
							text = AccommodationData.Detail.ShortCode;
							bool flag4 = string.IsNullOrEmpty(text);
							if (flag4)
							{
								text = (AccommodationData.Data.Field.ControlCaption ?? "");
							}
						}
						else
						{
							text = AccommodationData.Detail.LongDescription;
							bool flag5 = string.IsNullOrEmpty(text);
							if (flag5)
							{
								text = (AccommodationData.Data.Field.ControlCaption ?? "");
							}
						}
						object value = data.Value;
						bool flag6 = value is bool;
						if (flag6)
						{
							result = (((bool)value) ? text : "");
						}
						else
						{
							bool flag7 = value is DateTime;
							if (flag7)
							{
								result = string.Format("{0}: {1}", text, ((DateTime)value).ToString("MMMM d, yyyy"));
							}
							else
							{
								string text2 = ((value != null) ? value.ToString().Trim() : null) ?? "";
								bool flag8 = data.Field != null && data.Field.ControlCode == eControlCode.RtfTextBox && text2.Length > 0;
								if (flag8)
								{
									text2 = text2.ConvertRtfToPlainText();
								}
								bool flag9 = text2.Length > 0;
								if (flag9)
								{
									result = string.Format("{0}: {1}", text, value.ToString());
								}
								else
								{
									result = text;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x00033E50 File Offset: 0x00032050
		public static string GetString(this AccommodationData AccommodationData)
		{
			return AccommodationDataAdapter.GetString(AccommodationData, false);
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x00033E6C File Offset: 0x0003206C
		public static string GetStringShortCodes(this AccommodationData AccommodationData)
		{
			return AccommodationDataAdapter.GetString(AccommodationData, true);
		}
	}
}
