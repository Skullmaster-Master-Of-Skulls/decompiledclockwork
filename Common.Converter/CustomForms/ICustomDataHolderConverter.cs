using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms
{
	// Token: 0x02000003 RID: 3
	public interface ICustomDataHolderConverter
	{
		// Token: 0x06000009 RID: 9
		ICustomDataConverter<T> GetConverter<T>() where T : CustomDataHolderDTO;
	}
}
