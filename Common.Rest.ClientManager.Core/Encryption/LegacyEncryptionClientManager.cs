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

namespace TechnoPro.Common.Rest.ClientManager.Core.Encryption
{
	// Token: 0x0200004E RID: 78
	public class LegacyEncryptionClientManager : ILegacyEncryptionClientManager, IWebService
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x00008E61 File Offset: 0x00007061
		public byte[] Encrypt(string text)
		{
			return ObjectFactory.Resolve<ClientCache>().Encryption.Encrypt(text);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00008E73 File Offset: 0x00007073
		public string Decrypt(byte[] bytes)
		{
			return ObjectFactory.Resolve<ClientCache>().Encryption.Decrypt(bytes);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00008E85 File Offset: 0x00007085
		public DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable t, params string[] colsToEncrypt)
		{
			return ObjectFactory.Resolve<ClientCache>().Encryption.EncryptOrDecryptNameDataTableBatch(encrypt, t, colsToEncrypt);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00008E9C File Offset: 0x0000709C
		public IList<byte[]> EncryptData(params string[] plainTextValues)
		{
			IBatchEncryptor batchEncryptor = ObjectFactory.Resolve<ClientCache>().Encryption.GetBatchEncryptor();
			return (from g in plainTextValues
			where g != null && g.Trim().Length > 0
			select g).Select(new Func<string, byte[]>(batchEncryptor.Encrypt)).ToList<byte[]>();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00008EF8 File Offset: 0x000070F8
		public IList<string> DecryptData(params byte[][] encryptedValues)
		{
			IEncryption encryption = ObjectFactory.Resolve<ClientCache>().Encryption;
			IBatchDecryptor batchDecryptor = encryption.GetBatchDecryptor();
			return encryptedValues.Select(delegate(byte[] g)
			{
				if (g != null)
				{
					return batchDecryptor.Decrypt(g);
				}
				return null;
			}).ToList<string>();
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00008F39 File Offset: 0x00007139
		public string EncodeUrlVariable(string varValue, bool isEncrypted)
		{
			if (!isEncrypted)
			{
				return varValue;
			}
			return LegacyEncryptionClientManager.UrlEncodeByteArray(this.Encrypt(varValue));
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00008F4C File Offset: 0x0000714C
		public IList<LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO> DecryptLegacyDataItemsNeedingDecryption(IList<LegacyDynamicDataItemItemsToBeDecryptedDTO> itemsToBeDecrypted)
		{
			IEncryption encryption = ObjectFactory.Resolve<ClientCache>().Encryption;
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

		// Token: 0x060002F9 RID: 761 RVA: 0x00008F94 File Offset: 0x00007194
		private static string UrlEncodeByteArray(byte[] bytes)
		{
			return WebUtility.UrlEncode(LegacyEncryptionClientManager.ByteArrayToHexString(bytes));
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00008FA4 File Offset: 0x000071A4
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

		// Token: 0x060002FB RID: 763 RVA: 0x00008FFC File Offset: 0x000071FC
		private string DecryptData(IBatchDecryptor batchDecryptor, byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			if (bytes.Length < 1)
			{
				return "";
			}
			try
			{
				return batchDecryptor.Decrypt(bytes);
			}
			catch (Exception ex)
			{
				batchDecryptor = ObjectFactory.Resolve<ClientCache>().Encryption.GetBatchDecryptor();
				CWLogger.Logger.Error("LegacyEncryptionClientManager2:DecryptData:err={0}", ex.ToString());
			}
			return "";
		}
	}
}
