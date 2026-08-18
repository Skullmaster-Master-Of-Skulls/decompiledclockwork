using System;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Settings.BinarySerializers.Adapters;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.DAO.Impl.Settings.BinarySerializers
{
	// Token: 0x02000050 RID: 80
	internal class IntBinarySerializer : ISettingBinarySerializer
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00012710 File Offset: 0x00010910
		public object Deserialize(Setting setting, byte[] binaryValue, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			object result;
			try
			{
				bool flag = binaryValue == null || binaryValue.IsEmptyArray(encryption);
				if (flag)
				{
					result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<int>() : 0);
				}
				else
				{
					result = int.Parse(binaryValue.BytesToString(encryption));
				}
			}
			catch
			{
				result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<int>() : 0);
			}
			return result;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00012790 File Offset: 0x00010990
		public byte[] Serialize(Setting setting, object value, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			byte[] result;
			try
			{
				bool flag = !(value is int);
				if (flag)
				{
					result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<int>().ToString().StringToBytes(encryption) : string.Empty.StringToBytes(encryption));
				}
				else
				{
					string text = ((int)value).ToString();
					result = text.StringToBytes(encryption);
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
