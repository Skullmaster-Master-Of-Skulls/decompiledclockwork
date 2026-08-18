using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Settings.BinarySerializers.Adapters;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.DAO.Impl.Settings.BinarySerializers
{
	// Token: 0x02000052 RID: 82
	internal class ImageBinarySerializer : ISettingBinarySerializer
	{
		// Token: 0x06000219 RID: 537 RVA: 0x000129BC File Offset: 0x00010BBC
		public object Deserialize(Setting setting, byte[] binaryValue, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			object result;
			try
			{
				bool flag = binaryValue == null || binaryValue.IsEmptyArray(encryption);
				if (flag)
				{
					result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<Image>() : null);
				}
				else
				{
					MemoryStream stream = new MemoryStream(binaryValue);
					result = Image.FromStream(stream);
				}
			}
			catch
			{
				result = (lookupSetting.HasDefaultValue ? lookupSetting.GetDefaultValue<Image>() : null);
			}
			return result;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00012A30 File Offset: 0x00010C30
		public byte[] Serialize(Setting setting, object value, IEncryption encryption)
		{
			LookupSetting lookupSetting = new LookupSetting(setting);
			byte[] result;
			try
			{
				bool flag = value == null || !(value is Image);
				if (flag)
				{
					Image defaultValue = lookupSetting.GetDefaultValue<Image>();
					result = ((defaultValue != null) ? ImageBinarySerializer.GetBytesFromImage(defaultValue) : string.Empty.StringToBytes(encryption));
				}
				else
				{
					result = ImageBinarySerializer.GetBytesFromImage((Image)value);
				}
			}
			catch
			{
				result = string.Empty.StringToBytes(encryption);
			}
			return result;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00012AB4 File Offset: 0x00010CB4
		private static byte[] GetBytesFromImage(Image img)
		{
			byte[] result;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				img.Save(memoryStream, ImageFormat.Jpeg);
				byte[] array = memoryStream.ToArray();
				result = array;
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}
}
