using System;
using System.Data;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.Reports.ICore.Common;
using TechnoPro.Common.Reports.Public;
using TechnoPro.Common.Reports.Public.Entities.OperationContexts;

namespace TechnoPro.Common.Reports.Core.Common
{
	// Token: 0x02000004 RID: 4
	public class EncryptionReportManager : IEncryptionReportManager, IOperationContextRO, IBaseOperationContextRO<OperationContextRO>
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000022DB File Offset: 0x000004DB
		public EncryptionReportManager(OperationContextRO opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022ED File Offset: 0x000004ED
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022F5 File Offset: 0x000004F5
		public OperationContextRO OpContext { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022FE File Offset: 0x000004FE
		private IEncryption Encryption
		{
			get
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContextRO opContext = this.OpContext;
				return DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002320 File Offset: 0x00000520
		public string DecryptData(byte[] data)
		{
			return this.Encryption.Decrypt(data);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002340 File Offset: 0x00000540
		public byte[] EncryptData(string data)
		{
			return this.Encryption.Encrypt(data);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002360 File Offset: 0x00000560
		public DataTable DecryptTable(DataTable t, params string[] ColumnsToDecrypt)
		{
			return this.Encryption.EncryptOrDecryptNameDataTableBatch(false, t, ColumnsToDecrypt);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002380 File Offset: 0x00000580
		public DataTable EncryptTable(DataTable t, params string[] ColumnsToEncrypt)
		{
			return this.Encryption.EncryptOrDecryptNameDataTableBatch(true, t, ColumnsToEncrypt);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023A0 File Offset: 0x000005A0
		public object GetBatchDecryptor()
		{
			return this.Encryption.GetBatchDecryptor();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023C0 File Offset: 0x000005C0
		public string BatchDecryptData(object batchDecryptor, byte[] data)
		{
			return ((BatchDecryptor)batchDecryptor).Decrypt(data);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023E0 File Offset: 0x000005E0
		public object GetBatchEncryptor()
		{
			return this.Encryption.GetBatchEncryptor();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002400 File Offset: 0x00000600
		public byte[] BatchEncryptData(object batchEncryptor, string data)
		{
			return ((BatchEncryptor)batchEncryptor).Encrypt(data);
		}
	}
}
