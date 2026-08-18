using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms.Converters
{
	// Token: 0x0200000D RID: 13
	public class CustomDataBooleanConverter : ICustomDataConverter<CustomDataBooleanDTO>
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00003734 File Offset: 0x00001934
		public CustomDataBooleanDTO ToCustomDataBoolean(CustomDataBooleanDTO dataObj)
		{
			return dataObj;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataDateTimeDTO ToCustomDataDateTime(CustomDataBooleanDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataFileDTO ToCustomDataFile(CustomDataBooleanDTO dateObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003750 File Offset: 0x00001950
		public CustomDataIntDTO ToCustomDataInt(CustomDataBooleanDTO dataObj)
		{
			return new CustomDataIntDTO(dataObj)
			{
				Value = (dataObj.Value ? 1 : 0)
			};
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataListItemDTO ToCustomDataListItem(CustomDataBooleanDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataBooleanNullableDTO ToCustomDataBooleanNullable(CustomDataBooleanDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000377C File Offset: 0x0000197C
		public CustomDataStringDTO ToCustomDataString(CustomDataBooleanDTO dataObj)
		{
			return new CustomDataStringDTO(dataObj)
			{
				Value = (dataObj.Value ? "True" : "False")
			};
		}
	}
}
