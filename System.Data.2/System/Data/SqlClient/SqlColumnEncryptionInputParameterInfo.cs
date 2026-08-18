using System;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000224 RID: 548
	internal sealed class SqlColumnEncryptionInputParameterInfo
	{
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x000ECE04 File Offset: 0x000EC204
		internal SmiParameterMetaData ParameterMetadata
		{
			get
			{
				return this._smiParameterMetadata;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x000ECE18 File Offset: 0x000EC218
		internal byte[] SerializedWireFormat
		{
			get
			{
				return this._serializedWireFormat;
			}
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000ECE2C File Offset: 0x000EC22C
		internal SqlColumnEncryptionInputParameterInfo(SmiParameterMetaData smiParameterMetadata, SqlCipherMetadata cipherMetadata)
		{
			this._smiParameterMetadata = smiParameterMetadata;
			this._cipherMetadata = cipherMetadata;
			this._serializedWireFormat = this.SerializeToWriteFormat();
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000ECE5C File Offset: 0x000EC25C
		private byte[] SerializeToWriteFormat()
		{
			int num = 0;
			num++;
			num++;
			num += 4;
			num += 4;
			num += 4;
			num += this._cipherMetadata.EncryptionKeyInfo.Value.cekMdVersion.Length;
			num++;
			byte[] array = new byte[num];
			int num2 = 0;
			array[num2++] = this._cipherMetadata.CipherAlgorithmId;
			array[num2++] = this._cipherMetadata.EncryptionType;
			this.SerializeIntIntoBuffer(this._cipherMetadata.EncryptionKeyInfo.Value.databaseId, array, ref num2);
			this.SerializeIntIntoBuffer(this._cipherMetadata.EncryptionKeyInfo.Value.cekId, array, ref num2);
			this.SerializeIntIntoBuffer(this._cipherMetadata.EncryptionKeyInfo.Value.cekVersion, array, ref num2);
			Buffer.BlockCopy(this._cipherMetadata.EncryptionKeyInfo.Value.cekMdVersion, 0, array, num2, this._cipherMetadata.EncryptionKeyInfo.Value.cekMdVersion.Length);
			num2 += this._cipherMetadata.EncryptionKeyInfo.Value.cekMdVersion.Length;
			array[num2++] = this._cipherMetadata.NormalizationRuleVersion;
			return array;
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000ECF9C File Offset: 0x000EC39C
		private void SerializeIntIntoBuffer(int value, byte[] buffer, ref int offset)
		{
			int num = offset;
			offset = num + 1;
			buffer[num] = (byte)(value & 255);
			num = offset;
			offset = num + 1;
			buffer[num] = (byte)(value >> 8 & 255);
			num = offset;
			offset = num + 1;
			buffer[num] = (byte)(value >> 16 & 255);
			num = offset;
			offset = num + 1;
			buffer[num] = (byte)(value >> 24 & 255);
		}

		// Token: 0x04001493 RID: 5267
		private readonly SmiParameterMetaData _smiParameterMetadata;

		// Token: 0x04001494 RID: 5268
		private readonly SqlCipherMetadata _cipherMetadata;

		// Token: 0x04001495 RID: 5269
		private readonly byte[] _serializedWireFormat;
	}
}
