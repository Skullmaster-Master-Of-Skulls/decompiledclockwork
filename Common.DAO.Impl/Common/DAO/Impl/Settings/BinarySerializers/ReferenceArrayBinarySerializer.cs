using System;
using System.Text;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Settings.BinarySerializers.Adapters;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.DAO.Impl.Settings.BinarySerializers
{
	// Token: 0x02000053 RID: 83
	internal class ReferenceArrayBinarySerializer : ISettingBinarySerializer
	{
		// Token: 0x0600021D RID: 541 RVA: 0x00012AF8 File Offset: 0x00010CF8
		public object Deserialize(Setting setting, byte[] binaryValue, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			object result;
			try
			{
				bool flag = binaryValue == null || binaryValue.IsEmptyArray(encryption);
				if (flag)
				{
					result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<int[]>() : null);
				}
				else
				{
					string text = binaryValue.BytesToString(encryption);
					string[] array = text.Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries);
					int[] array2 = new int[array.Length];
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i] = int.Parse(array[i].Trim());
					}
					result = array2;
				}
			}
			catch
			{
				result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<int[]>() : null);
			}
			return result;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00012BB8 File Offset: 0x00010DB8
		public byte[] Serialize(Setting setting, object value, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			byte[] result;
			try
			{
				bool flag = value == null || !(value is int[]);
				if (flag)
				{
					result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<int[]>().ToString().StringToBytes(encryption) : string.Empty.StringToBytes(encryption));
				}
				else
				{
					int[] array = (int[])value;
					bool flag2 = array.Length != 0;
					if (flag2)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append(array[0]);
						for (int i = 1; i < array.Length; i++)
						{
							stringBuilder.Append(", ");
							stringBuilder.Append(array[i]);
						}
						result = stringBuilder.ToString().StringToBytes(encryption);
					}
					else
					{
						result = string.Empty.StringToBytes(encryption);
					}
				}
			}
			catch
			{
				result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<int[]>().ToString().StringToBytes(encryption) : string.Empty.StringToBytes(encryption));
			}
			return result;
		}
	}
}
