using System;
using System.Drawing;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Settings.BinarySerializers.Adapters;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.DAO.Impl.Settings.BinarySerializers
{
	// Token: 0x02000054 RID: 84
	internal class ColorBinarySerializer : ISettingBinarySerializer
	{
		// Token: 0x06000220 RID: 544 RVA: 0x00012CCC File Offset: 0x00010ECC
		public object Deserialize(Setting setting, byte[] binaryValue, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			object result;
			try
			{
				bool flag = binaryValue == null || binaryValue.IsEmptyArray(encryption);
				if (flag)
				{
					result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<Color>() : default(Color));
				}
				else
				{
					string value = binaryValue.BytesToString(encryption);
					result = Color.FromArgb(Convert.ToInt32(value));
				}
			}
			catch
			{
				result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<Color>() : default(Color));
			}
			return result;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00012D68 File Offset: 0x00010F68
		public byte[] Serialize(Setting setting, object value, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			byte[] result;
			try
			{
				bool flag = value == null || !(value is Color);
				if (flag)
				{
					result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<Color>().ToString().StringToBytes(encryption) : string.Empty.StringToBytes(encryption));
				}
				else
				{
					result = ((Color)value).ToArgb().ToString().StringToBytes(encryption);
				}
			}
			catch
			{
				result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<Color>().ToString().StringToBytes(encryption) : string.Empty.StringToBytes(encryption));
			}
			return result;
		}
	}
}
