using System;
using System.Linq;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Settings.BinarySerializers.Adapters;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.DAO.Impl.Settings.BinarySerializers
{
	// Token: 0x02000051 RID: 81
	internal class StringBinarySerializer : ISettingBinarySerializer
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00012844 File Offset: 0x00010A44
		public object Deserialize(Setting setting, byte[] binaryValue, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			object result;
			try
			{
				bool flag = binaryValue == null;
				if (flag)
				{
					result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<string>() : string.Empty);
				}
				else
				{
					result = binaryValue.BytesToString(encryption);
				}
			}
			catch
			{
				result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<string>() : string.Empty);
			}
			return result;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000128B0 File Offset: 0x00010AB0
		public byte[] Serialize(Setting setting, object value, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			byte[] result;
			try
			{
				object obj = value;
				bool flag = obj != null;
				if (flag)
				{
					bool flag2 = obj is int;
					if (flag2)
					{
						obj = ((int)obj).ToString();
					}
					else
					{
						bool flag3 = obj is int[];
						if (flag3)
						{
							obj = string.Join(",", (from g in (int[])obj
							select g.ToString()).ToArray<string>());
						}
					}
				}
				bool flag4 = !(obj is string);
				if (flag4)
				{
					result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<string>().StringToBytes(encryption) : string.Empty.StringToBytes(encryption));
				}
				else
				{
					result = ((string)obj).StringToBytes(encryption);
				}
			}
			catch
			{
				result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<string>().StringToBytes(encryption) : string.Empty.StringToBytes(encryption));
			}
			return result;
		}
	}
}
