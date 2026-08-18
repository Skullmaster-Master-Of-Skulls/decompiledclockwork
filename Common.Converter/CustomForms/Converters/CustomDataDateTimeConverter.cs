using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms.Converters
{
	// Token: 0x0200000F RID: 15
	public class CustomDataDateTimeConverter : ICustomDataConverter<CustomDataDateTimeDTO>
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataBooleanDTO ToCustomDataBoolean(CustomDataDateTimeDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003874 File Offset: 0x00001A74
		public CustomDataDateTimeDTO ToCustomDataDateTime(CustomDataDateTimeDTO dataObj)
		{
			return dataObj;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataFileDTO ToCustomDataFile(CustomDataDateTimeDTO dateObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataIntDTO ToCustomDataInt(CustomDataDateTimeDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataListItemDTO ToCustomDataListItem(CustomDataDateTimeDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003887 File Offset: 0x00001A87
		public CustomDataBooleanNullableDTO ToCustomDataBooleanNullable(CustomDataDateTimeDTO dataObj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003890 File Offset: 0x00001A90
		public CustomDataStringDTO ToCustomDataString(CustomDataDateTimeDTO dataObj)
		{
			return new CustomDataStringDTO(dataObj)
			{
				Value = dataObj.Value.ToString()
			};
		}
	}
}
