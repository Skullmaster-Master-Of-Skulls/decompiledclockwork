using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms.Converters
{
	// Token: 0x02000012 RID: 18
	public class CustomDataListItemConverter : ICustomDataConverter<CustomDataListItemDTO>
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataBooleanDTO ToCustomDataBoolean(CustomDataListItemDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataDateTimeDTO ToCustomDataDateTime(CustomDataListItemDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataFileDTO ToCustomDataFile(CustomDataListItemDTO dateObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataIntDTO ToCustomDataInt(CustomDataListItemDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000039A0 File Offset: 0x00001BA0
		public CustomDataListItemDTO ToCustomDataListItem(CustomDataListItemDTO dataObj)
		{
			return dataObj;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataBooleanNullableDTO ToCustomDataBooleanNullable(CustomDataListItemDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataStringDTO ToCustomDataString(CustomDataListItemDTO dataObj)
		{
			throw new InvalidCastException();
		}
	}
}
