using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms.Converters
{
	// Token: 0x0200000E RID: 14
	public class CustomDataBooleanNullableConverter : ICustomDataConverter<CustomDataBooleanNullableDTO>
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000037B0 File Offset: 0x000019B0
		public CustomDataStringDTO ToCustomDataString(CustomDataBooleanNullableDTO dataObj)
		{
			bool? flag = (dataObj != null) ? dataObj.Value : null;
			return new CustomDataStringDTO(dataObj)
			{
				Value = ((flag != null) ? (dataObj.Value.Value ? "True" : "False") : string.Empty)
			};
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003810 File Offset: 0x00001A10
		public CustomDataIntDTO ToCustomDataInt(CustomDataBooleanNullableDTO dataObj)
		{
			bool? flag = (dataObj != null) ? dataObj.Value : null;
			return new CustomDataIntDTO(dataObj)
			{
				Value = ((flag != null) ? (flag.Value ? 1 : 0) : -1)
			};
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataBooleanDTO ToCustomDataBoolean(CustomDataBooleanNullableDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataDateTimeDTO ToCustomDataDateTime(CustomDataBooleanNullableDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataFileDTO ToCustomDataFile(CustomDataBooleanNullableDTO dateObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataListItemDTO ToCustomDataListItem(CustomDataBooleanNullableDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003860 File Offset: 0x00001A60
		public CustomDataBooleanNullableDTO ToCustomDataBooleanNullable(CustomDataBooleanNullableDTO dataObj)
		{
			return dataObj;
		}
	}
}
