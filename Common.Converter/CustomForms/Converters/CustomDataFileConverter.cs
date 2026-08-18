using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms.Converters
{
	// Token: 0x02000010 RID: 16
	public class CustomDataFileConverter : ICustomDataConverter<CustomDataFileDTO>
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataBooleanDTO ToCustomDataBoolean(CustomDataFileDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataDateTimeDTO ToCustomDataDateTime(CustomDataFileDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000038C0 File Offset: 0x00001AC0
		public CustomDataFileDTO ToCustomDataFile(CustomDataFileDTO dataObj)
		{
			return dataObj;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataIntDTO ToCustomDataInt(CustomDataFileDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataListItemDTO ToCustomDataListItem(CustomDataFileDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataBooleanNullableDTO ToCustomDataBooleanNullable(CustomDataFileDTO dataObj)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003747 File Offset: 0x00001947
		public CustomDataStringDTO ToCustomDataString(CustomDataFileDTO dataObj)
		{
			throw new InvalidCastException();
		}
	}
}
