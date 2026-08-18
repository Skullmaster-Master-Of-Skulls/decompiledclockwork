using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using ClockWorkLogger;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore.Encryption;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Encryption
{
	// Token: 0x0200005E RID: 94
	public class LegacyEncryptionClientManager : ILegacyEncryptionClientManager, IWebService
	{
		// Token: 0x06000365 RID: 869 RVA: 0x0000ED28 File Offset: 0x0000CF28
		public byte[] Encrypt(string text)
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			IEncryption encryption = clientCache.Encryption;
			return encryption.Encrypt(text);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000ED50 File Offset: 0x0000CF50
		public string Decrypt(byte[] bytes)
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			IEncryption encryption = clientCache.Encryption;
			return encryption.Decrypt(bytes);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000ED78 File Offset: 0x0000CF78
		public DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable t, params string[] colsToEncrypt)
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			IEncryption encryption = clientCache.Encryption;
			return encryption.EncryptOrDecryptNameDataTableBatch(encrypt, t, colsToEncrypt);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		public IList<byte[]> EncryptData(params string[] plainTextValues)
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			IEncryption encryption = clientCache.Encryption;
			IBatchEncryptor batchEncryptor = encryption.GetBatchEncryptor();
			return (from g in plainTextValues
			where g != null && g.Trim().Length > 0
			select g).Select(new Func<string, byte[]>(batchEncryptor.Encrypt)).ToList<byte[]>();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000EE04 File Offset: 0x0000D004
		public IList<string> DecryptData(params byte[][] encryptedValues)
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			IEncryption encryption = clientCache.Encryption;
			IBatchDecryptor batchDecryptor = encryption.GetBatchDecryptor();
			return (from g in encryptedValues
			select (g == null) ? null : batchDecryptor.Decrypt(g)).ToList<string>();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000EE4C File Offset: 0x0000D04C
		public string EncodeUrlVariable(string varValue, bool isEncrypted)
		{
			return isEncrypted ? LegacyEncryptionClientManager.UrlEncodeByteArray(this.Encrypt(varValue)) : varValue;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000EE70 File Offset: 0x0000D070
		public IList<LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO> DecryptLegacyDataItemsNeedingDecryption(IList<LegacyDynamicDataItemItemsToBeDecryptedDTO> itemsToBeDecrypted)
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			IEncryption encryption = clientCache.Encryption;
			IBatchDecryptor batchDecryptor = encryption.GetBatchDecryptor();
			return (from g in itemsToBeDecrypted
			select new LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO
			{
				Id = g.Id,
				ControlValueDecryptedString = this.DecryptData(batchDecryptor, g.ControlValueBytes),
				PrivateNote = this.DecryptData(batchDecryptor, g.PrivateNoteEncrypted),
				RecommendedToStudentButDeclinedDetail = this.DecryptData(batchDecryptor, g.RecommendedToStudentButDeclinedDetailEncrypted),
				TextForLetter = this.DecryptData(batchDecryptor, g.TextForLetterEncrypted)
			}).ToList<LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO>();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000EEC0 File Offset: 0x0000D0C0
		private static string UrlEncodeByteArray(byte[] bytes)
		{
			return WebUtility.UrlEncode(LegacyEncryptionClientManager.ByteArrayToHexString(bytes));
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		private static string ByteArrayToHexString(byte[] Bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in Bytes)
			{
				stringBuilder.Append("0123456789ABCDEF"[b >> 4]);
				stringBuilder.Append("0123456789ABCDEF"[(int)(b & 15)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000EF40 File Offset: 0x0000D140
		private string DecryptData(IBatchDecryptor batchDecryptor, byte[] bytes)
		{
			bool flag = bytes == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = bytes.Length < 1;
				if (flag2)
				{
					result = "";
				}
				else
				{
					try
					{
						return batchDecryptor.Decrypt(bytes);
					}
					catch (Exception ex)
					{
						ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
						IEncryption encryption = clientCache.Encryption;
						batchDecryptor = encryption.GetBatchDecryptor();
						CWLogger.Logger.Error("LegacyEncryptionClientManager2:DecryptData:err={0}", ex.ToString());
					}
					result = "";
				}
			}
			return result;
		}
	}
}
