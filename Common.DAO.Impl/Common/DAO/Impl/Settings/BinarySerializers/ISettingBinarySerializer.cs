using System;
using EncryptionClassLibrary;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.DAO.Impl.Settings.BinarySerializers
{
	// Token: 0x0200004F RID: 79
	public interface ISettingBinarySerializer
	{
		// Token: 0x06000211 RID: 529
		object Deserialize(Setting setting, byte[] binaryValue, IEncryption encryption);

		// Token: 0x06000212 RID: 530
		byte[] Serialize(Setting setting, object value, IEncryption encryption);
	}
}
