using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000237 RID: 567
	internal class ColumnEncryptionKeyInfo
	{
		// Token: 0x0600231B RID: 8987 RVA: 0x000F2E80 File Offset: 0x000F2280
		internal ColumnEncryptionKeyInfo(byte[] decryptedKey, int databaseId, byte[] keyMetadataVersion, int keyid)
		{
			if (decryptedKey == null)
			{
				throw SQL.NullArgumentInConstructorInternal(ColumnEncryptionKeyInfo._decryptedKeyName, ColumnEncryptionKeyInfo._className);
			}
			if (decryptedKey.Length == 0)
			{
				throw SQL.EmptyArgumentInConstructorInternal(ColumnEncryptionKeyInfo._decryptedKeyName, ColumnEncryptionKeyInfo._className);
			}
			if (keyMetadataVersion == null)
			{
				throw SQL.NullArgumentInConstructorInternal(ColumnEncryptionKeyInfo._keyMetadataVersionName, ColumnEncryptionKeyInfo._className);
			}
			if (keyMetadataVersion.Length == 0)
			{
				throw SQL.EmptyArgumentInConstructorInternal(ColumnEncryptionKeyInfo._keyMetadataVersionName, ColumnEncryptionKeyInfo._className);
			}
			this.KeyId = keyid;
			this.DatabaseId = databaseId;
			this.DecryptedKeyBytes = decryptedKey;
			this.KeyMetadataVersionBytes = keyMetadataVersion;
			ushort value;
			try
			{
				value = (ushort)keyid;
			}
			catch (Exception innerException)
			{
				throw SQL.InvalidKeyIdUnableToCastToUnsignedShort(keyid, innerException);
			}
			this.KeyIdBytes = BitConverter.GetBytes(value);
			try
			{
			}
			catch (Exception innerException2)
			{
				throw SQL.InvalidDatabaseIdUnableToCastToUnsignedInt(databaseId, innerException2);
			}
			this.DatabaseIdBytes = BitConverter.GetBytes((uint)databaseId);
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000F2F6C File Offset: 0x000F236C
		internal int GetLengthForSerialization()
		{
			int num = 0;
			num += this.DecryptedKeyBytes.Length;
			num += this.KeyIdBytes.Length;
			num += this.DatabaseIdBytes.Length;
			return num + this.KeyMetadataVersionBytes.Length;
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x000F2FA8 File Offset: 0x000F23A8
		internal int SerializeToBuffer(byte[] bytePackage, int startOffset)
		{
			if (bytePackage == null)
			{
				throw SQL.NullArgumentInternal(ColumnEncryptionKeyInfo._bytePackageName, ColumnEncryptionKeyInfo._className, ColumnEncryptionKeyInfo._serializeToBufferMethodName);
			}
			if (bytePackage.Length == 0)
			{
				throw SQL.EmptyArgumentInternal(ColumnEncryptionKeyInfo._bytePackageName, ColumnEncryptionKeyInfo._className, ColumnEncryptionKeyInfo._serializeToBufferMethodName);
			}
			if (startOffset >= bytePackage.Length)
			{
				throw SQL.OffsetOutOfBounds(ColumnEncryptionKeyInfo._startOffsetName, ColumnEncryptionKeyInfo._className, ColumnEncryptionKeyInfo._serializeToBufferMethodName);
			}
			if (bytePackage.Length - startOffset < this.GetLengthForSerialization())
			{
				throw SQL.InsufficientBuffer(ColumnEncryptionKeyInfo._bytePackageName, ColumnEncryptionKeyInfo._className, ColumnEncryptionKeyInfo._serializeToBufferMethodName);
			}
			Buffer.BlockCopy(this.DatabaseIdBytes, 0, bytePackage, startOffset, this.DatabaseIdBytes.Length);
			startOffset += this.DatabaseIdBytes.Length;
			Buffer.BlockCopy(this.KeyMetadataVersionBytes, 0, bytePackage, startOffset, this.KeyMetadataVersionBytes.Length);
			startOffset += this.KeyMetadataVersionBytes.Length;
			Buffer.BlockCopy(this.KeyIdBytes, 0, bytePackage, startOffset, this.KeyIdBytes.Length);
			startOffset += this.KeyIdBytes.Length;
			Buffer.BlockCopy(this.DecryptedKeyBytes, 0, bytePackage, startOffset, this.DecryptedKeyBytes.Length);
			startOffset += this.DecryptedKeyBytes.Length;
			return startOffset;
		}

		// Token: 0x04001541 RID: 5441
		internal readonly int KeyId;

		// Token: 0x04001542 RID: 5442
		internal readonly int DatabaseId;

		// Token: 0x04001543 RID: 5443
		internal readonly byte[] DecryptedKeyBytes;

		// Token: 0x04001544 RID: 5444
		internal readonly byte[] KeyIdBytes;

		// Token: 0x04001545 RID: 5445
		internal readonly byte[] DatabaseIdBytes;

		// Token: 0x04001546 RID: 5446
		internal readonly byte[] KeyMetadataVersionBytes;

		// Token: 0x04001547 RID: 5447
		private static readonly string _decryptedKeyName = "DecryptedKey";

		// Token: 0x04001548 RID: 5448
		private static readonly string _keyMetadataVersionName = "KeyMetadataVersion";

		// Token: 0x04001549 RID: 5449
		private static readonly string _className = "ColumnEncryptionKeyInfo";

		// Token: 0x0400154A RID: 5450
		private static readonly string _bytePackageName = "BytePackage";

		// Token: 0x0400154B RID: 5451
		private static readonly string _serializeToBufferMethodName = "SerializeToBuffer";

		// Token: 0x0400154C RID: 5452
		private static readonly string _startOffsetName = "StartOffset";
	}
}
