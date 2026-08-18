using System;
using Databases;
using TechnoPro.Common.DAO.Impl.Settings.BinarySerializers;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.DAO.Impl.Settings
{
	// Token: 0x0200004B RID: 75
	public static class SettingAdapter
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00011A98 File Offset: 0x0000FC98
		public static object GetDefaultValue(this Setting setting, OperationContext opContext)
		{
			ISettingBinarySerializer binarySerializer = SettingBinarySerializerFactory.GetBinarySerializer(setting);
			return binarySerializer.Deserialize(setting, null, DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null).Encryption);
		}
	}
}
