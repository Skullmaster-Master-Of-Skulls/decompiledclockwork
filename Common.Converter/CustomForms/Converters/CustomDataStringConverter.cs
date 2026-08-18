using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;

namespace TechnoPro.Common.Converter.CustomForms.Converters
{
	// Token: 0x02000013 RID: 19
	public class CustomDataStringConverter : ICustomDataConverter<CustomDataStringDTO>
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000039B4 File Offset: 0x00001BB4
		public CustomDataBooleanDTO ToCustomDataBoolean(CustomDataStringDTO dataObj)
		{
			bool value = CustomDataStringConverter.StringToBool(dataObj.Value);
			return new CustomDataBooleanDTO(dataObj)
			{
				Value = value
			};
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000039E0 File Offset: 0x00001BE0
		private static bool StringToBool(string s)
		{
			return "1trueyes".IndexOf((s ?? "").Trim()) >= 0;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003A14 File Offset: 0x00001C14
		public CustomDataDateTimeDTO ToCustomDataDateTime(CustomDataStringDTO dataObj)
		{
			DateTime value;
			bool flag = !DateTime.TryParse(dataObj.Value ?? "", out value);
			if (flag)
			{
				throw new InvalidCastException();
			}
			return new CustomDataDateTimeDTO(dataObj)
			{
				Value = value
			};
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataFileDTO ToCustomDataFile(CustomDataStringDTO dateObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003A58 File Offset: 0x00001C58
		public CustomDataIntDTO ToCustomDataInt(CustomDataStringDTO dataObj)
		{
			int value;
			bool flag = !int.TryParse(dataObj.Value ?? "", out value);
			if (flag)
			{
				throw new InvalidCastException();
			}
			return new CustomDataIntDTO(dataObj)
			{
				Value = value
			};
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003A9C File Offset: 0x00001C9C
		public CustomDataListItemDTO ToCustomDataListItem(CustomDataStringDTO dataObj)
		{
			bool flag = string.IsNullOrEmpty(dataObj.Value);
			CustomDataListItemDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new CustomDataListItemDTO(dataObj)
				{
					ListItem = new CustomListItemDTO
					{
						ListItemId = new Guid(dataObj.Value)
					}
				};
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public CustomDataBooleanNullableDTO ToCustomDataBooleanNullable(CustomDataStringDTO dataObj)
		{
			string text = (dataObj != null) ? dataObj.Value : null;
			return new CustomDataBooleanNullableDTO(dataObj)
			{
				Value = (string.IsNullOrEmpty(text) ? null : new bool?(CustomDataStringConverter.StringToBool(text)))
			};
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003B34 File Offset: 0x00001D34
		public CustomDataStringDTO ToCustomDataString(CustomDataStringDTO dataObj)
		{
			return dataObj;
		}
	}
}
