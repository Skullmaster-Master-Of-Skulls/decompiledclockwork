using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Encryption;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;

namespace TechnoPro.Common.DAO.Impl.Encryption
{
	// Token: 0x020000D1 RID: 209
	public class LegacyEncryptionDAO : ILegacyEncryptionDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x00036414 File Offset: 0x00034614
		public LegacyEncryptionDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00036426 File Offset: 0x00034626
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0003642E File Offset: 0x0003462E
		public OperationContext OpContext { get; set; }

		// Token: 0x060005B9 RID: 1465 RVA: 0x00036438 File Offset: 0x00034638
		public IList<LegacyDynamicDataItemItemsThatHaveBeenDecrypted> DecryptLegacyDataItemsNeedingDecryption(IList<LegacyDynamicDataItemItemsToBeDecrypted> itemsToBeDecrypted)
		{
			LegacyEncryptionDAO.<>c__DisplayClass5_0 CS$<>8__locals1 = new LegacyEncryptionDAO.<>c__DisplayClass5_0();
			CS$<>8__locals1.<>4__this = this;
			LegacyEncryptionDAO.<>c__DisplayClass5_0 CS$<>8__locals2 = CS$<>8__locals1;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			CS$<>8__locals2.batchDecryptor = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption.GetBatchDecryptor();
			return (from g in itemsToBeDecrypted
			select new LegacyDynamicDataItemItemsThatHaveBeenDecrypted
			{
				Id = g.Id,
				ControlValueDecryptedString = CS$<>8__locals1.<>4__this.DecryptData(CS$<>8__locals1.batchDecryptor, g.ControlValueBytes),
				PrivateNote = CS$<>8__locals1.<>4__this.DecryptData(CS$<>8__locals1.batchDecryptor, g.PrivateNoteEncrypted),
				RecommendedToStudentButDeclinedDetail = CS$<>8__locals1.<>4__this.DecryptData(CS$<>8__locals1.batchDecryptor, g.RecommendedToStudentButDeclinedDetailEncrypted),
				TextForLetter = CS$<>8__locals1.<>4__this.DecryptData(CS$<>8__locals1.batchDecryptor, g.TextForLetterEncrypted)
			}).ToList<LegacyDynamicDataItemItemsThatHaveBeenDecrypted>();
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00036498 File Offset: 0x00034698
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
						eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
						OperationContext opContext = this.OpContext;
						batchDecryptor = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption.GetBatchDecryptor();
						CWLogger.Logger.Error("LegacyEncryptionDAO:DecryptData:err={0}", ex.ToString());
					}
					result = "";
				}
			}
			return result;
		}
	}
}
