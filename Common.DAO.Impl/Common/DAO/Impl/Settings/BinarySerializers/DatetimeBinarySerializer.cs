using System;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Settings.BinarySerializers.Adapters;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.DAO.Impl.Settings.BinarySerializers
{
	// Token: 0x02000055 RID: 85
	internal class DatetimeBinarySerializer : ISettingBinarySerializer
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00012E34 File Offset: 0x00011034
		public object Deserialize(Setting setting, byte[] binaryValue, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			object result;
			try
			{
				bool flag = binaryValue == null || binaryValue.IsEmptyArray(encryption);
				if (flag)
				{
					result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<DateTime>() : default(DateTime));
				}
				else
				{
					string s = binaryValue.BytesToString(encryption);
					result = DateTime.FromBinary(long.Parse(s));
				}
			}
			catch
			{
				result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<DateTime>() : default(DateTime));
			}
			return result;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00012ED0 File Offset: 0x000110D0
		public byte[] Serialize(Setting setting, object value, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			byte[] result;
			try
			{
				bool flag = value == null || !(value is DateTime);
				if (flag)
				{
					result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<DateTime>().ToString().StringToBytes(encryption) : string.Empty.StringToBytes(encryption));
				}
				else
				{
					result = ((DateTime)value).ToBinary().ToString().StringToBytes(encryption);
				}
			}
			catch
			{
				result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<int>().ToString().StringToBytes(encryption) : string.Empty.StringToBytes(encryption));
			}
			return result;
		}
	}
}
