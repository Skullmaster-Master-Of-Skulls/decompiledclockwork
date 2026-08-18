using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms.Converters
{
	// Token: 0x02000011 RID: 17
	public class CustomDataIntConverter : ICustomDataConverter<CustomDataIntDTO>
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000038D4 File Offset: 0x00001AD4
		public CustomDataBooleanDTO ToCustomDataBoolean(CustomDataIntDTO dataObj)
		{
			int value = dataObj.Value;
			bool flag = value != 0 && value != 1;
			if (flag)
			{
				throw new InvalidCastException();
			}
			return new CustomDataBooleanDTO(dataObj)
			{
				Value = (value == 1)
			};
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataDateTimeDTO ToCustomDataDateTime(CustomDataIntDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataFileDTO ToCustomDataFile(CustomDataIntDTO dateObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003918 File Offset: 0x00001B18
		public CustomDataIntDTO ToCustomDataInt(CustomDataIntDTO dataObj)
		{
			return dataObj;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataListItemDTO ToCustomDataListItem(CustomDataIntDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000392C File Offset: 0x00001B2C
		public CustomDataBooleanNullableDTO ToCustomDataBooleanNullable(CustomDataIntDTO dataObj)
		{
			int num = (dataObj != null) ? dataObj.Value : -1;
			return new CustomDataBooleanNullableDTO(dataObj)
			{
				Value = ((num < 0) ? null : new bool?(num == 1))
			};
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003970 File Offset: 0x00001B70
		public CustomDataStringDTO ToCustomDataString(CustomDataIntDTO dataObj)
		{
			return new CustomDataStringDTO(dataObj)
			{
				Value = dataObj.Value.ToString()
			};
		}
	}
}
