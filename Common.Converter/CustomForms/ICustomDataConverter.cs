using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms
{
	// Token: 0x02000004 RID: 4
	public interface ICustomDataConverter<T> where T : CustomDataHolderDTO
	{
		// Token: 0x0600000A RID: 10
		CustomDataBooleanDTO ToCustomDataBoolean(T dataObj);

		// Token: 0x0600000B RID: 11
		CustomDataDateTimeDTO ToCustomDataDateTime(T dataObj);

		// Token: 0x0600000C RID: 12
		CustomDataFileDTO ToCustomDataFile(T dateObj);

		// Token: 0x0600000D RID: 13
		CustomDataIntDTO ToCustomDataInt(T dataObj);

		// Token: 0x0600000E RID: 14
		CustomDataStringDTO ToCustomDataString(T dataObj);

		// Token: 0x0600000F RID: 15
		CustomDataListItemDTO ToCustomDataListItem(T dataObj);

		// Token: 0x06000010 RID: 16
		CustomDataBooleanNullableDTO ToCustomDataBooleanNullable(T dataObj);
	}
}
