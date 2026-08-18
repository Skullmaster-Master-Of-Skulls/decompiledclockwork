using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;

namespace TechnoPro.Common.DAO.Impl.Legacy
{
	// Token: 0x020000A7 RID: 167
	public class LegacyDynamicDataDAO : ILegacyDynamicDataDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x00029FEC File Offset: 0x000281EC
		public LegacyDynamicDataDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00029FFE File Offset: 0x000281FE
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x0002A006 File Offset: 0x00028206
		public OperationContext OpContext { get; set; }

		// Token: 0x06000494 RID: 1172 RVA: 0x0002A010 File Offset: 0x00028210
		private static DynamicDataDecryptedPreviewItem GetDecryptedPreviewItemFromRecord(IDataRecord record, bool isDataEncrypted, IBatchDecryptor batchDecryptor, UTF8Encoding utf8Encoder)
		{
			bool flag = record == null;
			DynamicDataDecryptedPreviewItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] array = (record["controlvalue"] is DBNull) ? null : ((byte[])record["controlvalue"]);
				result = new DynamicDataDecryptedPreviewItem
				{
					DataId = ((record["dataid"] is DBNull) ? 0 : ((int)record["dataid"])),
					ControlValue = array,
					ControlValuePlainText = ((array == null) ? "" : (isDataEncrypted ? batchDecryptor.Decrypt(array) : utf8Encoder.GetString(array)))
				};
			}
			return result;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0002A0B8 File Offset: 0x000282B8
		private static IList<DynamicDataDecryptedPreviewItem> GetDynamicDataDecryptedPreviewItems(int controlId, bool isDataEncrypted, string sqlQuery, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, controlId)
			};
			IList<DynamicDataDecryptedPreviewItem> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(sqlQuery, parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<DynamicDataDecryptedPreviewItem> list = new List<DynamicDataDecryptedPreviewItem>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					UTF8Encoding utf8Encoder = new UTF8Encoding();
					while (dataReader.Read())
					{
						DynamicDataDecryptedPreviewItem decryptedPreviewItemFromRecord = LegacyDynamicDataDAO.GetDecryptedPreviewItemFromRecord(dataReader, isDataEncrypted, batchDecryptor, utf8Encoder);
						bool flag2 = decryptedPreviewItemFromRecord != null;
						if (flag2)
						{
							list.Add(decryptedPreviewItemFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0002A17C File Offset: 0x0002837C
		private bool UpdateData(int dataId, byte[] newData, string sqlQuery)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@dataid", DbType.Int32, dataId),
				databaseLayer.GetParameter("@val", DbType.Binary, newData)
			};
			object obj = databaseLayer.ExecuteScalar(sqlQuery, parameters);
			return obj is int && (int)obj > 0;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0002A1F4 File Offset: 0x000283F4
		private int ReEncryptAndSaveData(IList<DynamicDataDecryptedPreviewItem> previewItems, string sqlQuery)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IBatchEncryptor batchEncryptor = databaseLayer.Encryption.GetBatchEncryptor();
			return previewItems.Count((DynamicDataDecryptedPreviewItem previewItem) => this.UpdateData(previewItem.DataId, batchEncryptor.Encrypt(previewItem.ControlValuePlainText), sqlQuery));
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0002A258 File Offset: 0x00028458
		private int ReDecryptAndSaveData(IList<DynamicDataDecryptedPreviewItem> previewItems, string sqlQuery)
		{
			UTF8Encoding utf8Encoder = new UTF8Encoding();
			return previewItems.Count((DynamicDataDecryptedPreviewItem previewItem) => this.UpdateData(previewItem.DataId, utf8Encoder.GetBytes(previewItem.ControlValuePlainText), sqlQuery));
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0002A29C File Offset: 0x0002849C
		public IList<DynamicDataDecryptedPreviewItem> GetDynamicDataDecryptedPreviewItemsForPerStudentData(int ControlId, bool IsDataEncrypted)
		{
			return LegacyDynamicDataDAO.GetDynamicDataDecryptedPreviewItems(ControlId, IsDataEncrypted, "SELECT dataid,controlvalue FROM otherinfops WHERE controlid=@cid", this.OpContext);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0002A2C0 File Offset: 0x000284C0
		public IList<DynamicDataDecryptedPreviewItem> GetDynamicDataDecryptedPreviewItemsForPerAppointmentData(int ControlId, bool IsDataEncrypted)
		{
			return LegacyDynamicDataDAO.GetDynamicDataDecryptedPreviewItems(ControlId, IsDataEncrypted, "SELECT dataid,controlvalue FROM otherinfopa WHERE controlid=@cid", this.OpContext);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0002A2E4 File Offset: 0x000284E4
		public IList<DynamicDataDecryptedPreviewItem> GetDynamicDataDecryptedPreviewItemsForAccommodationData(int ControlId, bool IsDataEncrypted)
		{
			return LegacyDynamicDataDAO.GetDynamicDataDecryptedPreviewItems(ControlId, IsDataEncrypted, "SELECT dataid,controlvalue FROM otherinfoaccommodationps WHERE controlid=@cid", this.OpContext);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0002A308 File Offset: 0x00028508
		public int ReEncryptAndSaveDataPerStudent(IList<DynamicDataDecryptedPreviewItem> previewItems)
		{
			return this.ReEncryptAndSaveData(previewItems, "UPDATE otherinfops SET controlvalue=@val WHERE dataid=@dataid\r\nSELECT COUNT(dataid) FROM otherinfops WHERE dataid=@dataid AND controlvalue=@val");
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0002A328 File Offset: 0x00028528
		public int ReDecryptAndSaveDataPerStudent(IList<DynamicDataDecryptedPreviewItem> previewItems)
		{
			return this.ReDecryptAndSaveData(previewItems, "UPDATE otherinfops SET controlvalue=@val WHERE dataid=@dataid\r\nSELECT COUNT(dataid) FROM otherinfops WHERE dataid=@dataid AND controlvalue=@val");
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0002A348 File Offset: 0x00028548
		public int ReEncryptAndSaveDataPerAppointment(IList<DynamicDataDecryptedPreviewItem> previewItems)
		{
			return this.ReEncryptAndSaveData(previewItems, "UPDATE otherinfopa SET controlvalue=@val WHERE dataid=@dataid\r\nSELECT COUNT(dataid) FROM otherinfopa WHERE dataid=@dataid AND controlvalue=@val");
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0002A368 File Offset: 0x00028568
		public int ReDecryptAndSaveDataPerAppointment(IList<DynamicDataDecryptedPreviewItem> previewItems)
		{
			return this.ReDecryptAndSaveData(previewItems, "UPDATE otherinfopa SET controlvalue=@val WHERE dataid=@dataid\r\nSELECT COUNT(dataid) FROM otherinfopa WHERE dataid=@dataid AND controlvalue=@val");
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0002A388 File Offset: 0x00028588
		public int ReEncryptAndSaveDataAccommodationData(IList<DynamicDataDecryptedPreviewItem> previewItems)
		{
			return this.ReEncryptAndSaveData(previewItems, "UPDATE otherinfoaccommodationps SET controlvalue=@val WHERE dataid=@dataid\r\nSELECT COUNT(dataid) FROM otherinfoaccommodationps WHERE dataid=@dataid AND controlvalue=@val");
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0002A3A8 File Offset: 0x000285A8
		public int ReDecryptAndSaveDataAccommodationData(IList<DynamicDataDecryptedPreviewItem> previewItems)
		{
			return this.ReDecryptAndSaveData(previewItems, "UPDATE otherinfoaccommodationps SET controlvalue=@val WHERE dataid=@dataid\r\nSELECT COUNT(dataid) FROM otherinfoaccommodationps WHERE dataid=@dataid AND controlvalue=@val");
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0002A3C8 File Offset: 0x000285C8
		public byte[] LookupStaffSignature(int pid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, pid)
			};
			object obj = databaseLayer.ExecuteScalar("DECLARE @sigcid INT\r\nSET @sigcid=(SELECT settingvalue AS titlecid FROM settingsgroups WHERE groupid=-1 AND settingcode=99719)\r\nSELECT TOP 1 controlvalue FROM imageinfops WHERE controlid=@sigcid AND personid=@pid", parameters);
			return obj as byte[];
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0002A424 File Offset: 0x00028624
		public void SaveLegacyStudentNote(LegacyStudentNote note)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, note.PersonId),
				databaseLayer.GetParameter("@cid", DbType.Int32, note.PersonId),
				databaseLayer.GetParameter("@val", DbType.Binary, databaseLayer.Encryption.Encrypt(note.ControlValue ?? ""))
			};
			databaseLayer.ExecuteNonQuery("IF NOT EXISTS(SELECT dataid FROM otherinfops WHERE controlid=@cid AND personid=@pid)\r\n    INSERT INTO otherinfops(screennum,controlid,personid,controlvalue) VALUES (1,@cid,@pid,@val)\r\nELSE\r\n    UPDATE otherinfops SET controlvalue=@val WHERE controlid=@cid AND personid=@pid", parameters);
		}
	}
}
