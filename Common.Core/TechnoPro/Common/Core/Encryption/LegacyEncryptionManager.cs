using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Common.Web;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Encryption;
using TechnoPro.Common.DAO.Impl.Encryption;
using TechnoPro.Common.ICore.Encryption;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;

namespace TechnoPro.Common.Core.Encryption
{
	// Token: 0x020000F3 RID: 243
	public class LegacyEncryptionManager : ILegacyEncryptionManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000979 RID: 2425 RVA: 0x0003C1BC File Offset: 0x0003A3BC
		public LegacyEncryptionManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0003C1CE File Offset: 0x0003A3CE
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x0003C1D6 File Offset: 0x0003A3D6
		public OperationContext OpContext { get; set; }

		// Token: 0x0600097C RID: 2428 RVA: 0x0003C1E0 File Offset: 0x0003A3E0
		private static string UrlEncodeByteArray(byte[] bytes)
		{
			return LegacyEncryptionManager.ByteArrayToHexString(bytes).UrlEncode();
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0003C200 File Offset: 0x0003A400
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

		// Token: 0x0600097E RID: 2430 RVA: 0x0003C260 File Offset: 0x0003A460
		private IEncryption GetEncryption()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			return DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0003C290 File Offset: 0x0003A490
		public byte[] Encrypt(string text)
		{
			IEncryptionDAO encryptionDAO = new EncryptionDAO(this.GetEncryption());
			return encryptionDAO.EncryptData(text);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0003C2B8 File Offset: 0x0003A4B8
		public string Decrypt(byte[] bytes)
		{
			IEncryptionDAO encryptionDAO = new EncryptionDAO(this.GetEncryption());
			return encryptionDAO.DecryptData(bytes);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0003C2E0 File Offset: 0x0003A4E0
		public IList<byte[]> EncryptData(IList<string> items)
		{
			IEncryptionDAO encryptionDAO = new EncryptionDAO(this.GetEncryption());
			return encryptionDAO.EncryptData(items);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0003C308 File Offset: 0x0003A508
		public IList<string> DecryptData(IList<byte[]> items)
		{
			IEncryptionDAO encryptionDAO = new EncryptionDAO(this.GetEncryption());
			return encryptionDAO.DecryptData(items);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0003C330 File Offset: 0x0003A530
		public DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable t, params string[] colsToEncrypt)
		{
			IEncryptionDAO encryptionDAO = new EncryptionDAO(this.GetEncryption());
			return encryptionDAO.EncryptOrDecryptNameDataTableBatch(encrypt, t, colsToEncrypt);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0003C358 File Offset: 0x0003A558
		public string EncodeUrlVariable(string varValue, bool encrypted)
		{
			return encrypted ? LegacyEncryptionManager.UrlEncodeByteArray(this.Encrypt(varValue)) : varValue;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0003C37C File Offset: 0x0003A57C
		public IList<LegacyDynamicDataItemItemsThatHaveBeenDecrypted> DecryptLegacyDataItemsNeedingDecryption(IList<LegacyDynamicDataItemItemsToBeDecrypted> itemsToBeDecrypted)
		{
			ILegacyEncryptionDAO legacyEncryptionDAO = new LegacyEncryptionDAO(this.OpContext);
			return legacyEncryptionDAO.DecryptLegacyDataItemsNeedingDecryption(itemsToBeDecrypted);
		}
	}
}
