using System;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Settings.BinarySerializers.Adapters;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.DAO.Impl.Settings.BinarySerializers
{
	// Token: 0x02000056 RID: 86
	internal class BooleanBinarySerializer : ISettingBinarySerializer
	{
		// Token: 0x06000226 RID: 550 RVA: 0x00012F90 File Offset: 0x00011190
		public object Deserialize(Setting setting, byte[] binaryValue, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			object result;
			try
			{
				bool flag = binaryValue == null || binaryValue.IsEmptyArray(encryption);
				if (flag)
				{
					result = (lookupSetting.HasDefaultValue && lookupSetting.GetDefaultValue<bool>());
				}
				else
				{
					result = (Array.IndexOf<string>(BooleanBinarySerializer.PossibleYeses, binaryValue.BytesToString(encryption)) >= 0);
				}
			}
			catch
			{
				result = (lookupSetting.HasDefaultValue && lookupSetting.GetDefaultValue<bool>());
			}
			return result;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00013020 File Offset: 0x00011220
		public byte[] Serialize(Setting setting, object value, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			byte[] result;
			try
			{
				bool flag = value == null || !(value is bool);
				if (flag)
				{
					result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<bool>().ToString().StringToBytes(encryption) : string.Empty.StringToBytes(encryption));
				}
				else
				{
					result = ((bool)value).ToString().StringToBytes(encryption);
				}
			}
			catch
			{
				result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<bool>().ToString().StringToBytes(encryption) : string.Empty.StringToBytes(encryption));
			}
			return result;
		}

		// Token: 0x040000DA RID: 218
		private static readonly string[] PossibleYeses = new string[]
		{
			"y",
			"yes",
			"1",
			"true",
			"t",
			bool.TrueString
		};
	}
}
