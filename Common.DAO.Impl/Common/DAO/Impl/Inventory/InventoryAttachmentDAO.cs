using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using Databases;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Impl.Inventory
{
	// Token: 0x020000B1 RID: 177
	public class InventoryAttachmentDAO : IInventoryAttachmentDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x0002BA6A File Offset: 0x00029C6A
		public InventoryAttachmentDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0002BA7C File Offset: 0x00029C7C
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0002BA84 File Offset: 0x00029C84
		public OperationContext OpContext { get; set; }

		// Token: 0x060004B9 RID: 1209 RVA: 0x0002BA90 File Offset: 0x00029C90
		public InventoryAttachedFile GetAttachmentById(int attachmentId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@attachmentid", DbType.Int32, attachmentId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from InventoryV2_AttachedFile where AttachmentID=@attachmentid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return InventoryAttachmentDAO.GetAttachment(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0002BB24 File Offset: 0x00029D24
		public IList<InventoryAttachedFileInfo> GetProductAttachments(Guid itemUniqueId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<InventoryAttachedFileInfo> list = new List<InventoryAttachedFileInfo>();
			DbParameter parameter = databaseLayer.GetParameter("@itemuniqueid", DbType.Guid, itemUniqueId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from InventoryV2_AttachedFile where ItemID=@itemuniqueid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryAttachedFileInfo attachmentFileInfo = InventoryAttachmentDAO.GetAttachmentFileInfo(dataReader);
						bool flag2 = attachmentFileInfo != null;
						if (flag2)
						{
							list.Add(attachmentFileInfo);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0002BBD8 File Offset: 0x00029DD8
		public int AddAttachmentToProduct(Guid itemUniqueId, InventoryAttachedFile attachedFile)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@attachmentid", DbType.Int32, 0),
				databaseLayer.GetParameter("@itemuniqueid", DbType.Guid, itemUniqueId),
				databaseLayer.GetParameter("@attachmentname", DbType.String, attachedFile.AttachedFileInfo.Name ?? string.Empty),
				databaseLayer.GetParameter("@createddate", DbType.DateTime, attachedFile.AttachedFileInfo.CreatedDatetime),
				databaseLayer.GetParameter("@notes", DbType.String, attachedFile.AttachedFileInfo.Notes ?? string.Empty),
				databaseLayer.GetParameter("@sizeinbytes", DbType.Int32, attachedFile.AttachedFileInfo.SizeInBytes)
			};
			bool flag = databaseLayer.ExecuteNonQuery("insert into InventoryV2_AttachedFile \r\n(ItemID ,AttachmentName, CreatedDate, Notes, SizeInBytes)\r\nvalues (@itemuniqueid, @attachmentname, @createddate, @notes, @sizeinbytes)\r\nset @attachmentid=SCOPE_IDENTITY()", array) > 0;
			if (flag)
			{
				bool flag2 = !(array[0].Value is DBNull);
				if (flag2)
				{
					attachedFile.Id = (int)array[0].Value;
				}
				databaseLayer = DatabaseLayerFactory.ClockWorkFiles;
				array = new DbParameter[]
				{
					databaseLayer.GetParameter("@attachmentid", DbType.Int32, attachedFile.Id),
					databaseLayer.GetParameter("@binarydata", DbType.Binary, attachedFile.BinaryData)
				};
				databaseLayer.ExecuteNonQuery("insert into [InventoryV2_AttachedFileData] (AttachmentID, BinaryData) values (@attachmentid, @binarydata)", array);
			}
			return attachedFile.Id;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0002BD4C File Offset: 0x00029F4C
		public void RemoveAttachmentFromProduct(int attachedFileId)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter parameter = databaseLayer.GetParameter("@attachmentid", DbType.Int32, attachedFileId);
			databaseLayer.ExecuteNonQuery("delete from [InventoryV2_AttachedFileData] where AttachmentID=@attachmentid", new DbParameter[]
			{
				parameter
			});
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			parameter = databaseLayer.GetParameter("@attachmentid", DbType.Int32, attachedFileId);
			databaseLayer.ExecuteNonQuery("delete from InventoryV2_AttachedFile where AttachmentID=@attachmentid", new DbParameter[]
			{
				parameter
			});
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0002BDD0 File Offset: 0x00029FD0
		public void RemoveAttachmentsFromProduct(IList<int> attachedFileIds)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter parameter = databaseLayer.GetParameter("@attachmentids", DbType.String, attachedFileIds.CommaSeparatedValues<int>());
			databaseLayer.ExecuteNonQuery("delete from InventoryV2_AttachedFileData where AttachmentID in (select OrderID as AttachmentID from SplitOrderIDs(@attachmentids))", new DbParameter[]
			{
				parameter
			});
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			parameter = databaseLayer.GetParameter("@attachmentids", DbType.String, attachedFileIds.CommaSeparatedValues<int>());
			databaseLayer.ExecuteNonQuery("delete from InventoryV2_AttachedFile where AttachmentID in (select OrderID as AttachmentID from SplitOrderIDs(@attachmentids))", new DbParameter[]
			{
				parameter
			});
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0002BE54 File Offset: 0x0002A054
		public void RemoveAllAttachmentsFromProduct(Guid itemUniqueId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@attachmentid", DbType.Int32, 0),
				databaseLayer.GetParameter("@itemuniqueid", DbType.Guid, itemUniqueId)
			};
			databaseLayer.ExecuteNonQuery("delete from InventoryV2_AttachedFile where ItemID=@itemuniqueid", parameters);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0002BEB8 File Offset: 0x0002A0B8
		public Image GetProductPicture(Guid productId)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter parameter = clockWorkFiles.GetParameter("@productid", DbType.Guid, productId);
			using (IDataReader dataReader = clockWorkFiles.ExecuteQueryReader("select ProductId, [Picture] from InventoryV2_ProductImage where ProductId=@productid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return (dataReader["Picture"] is DBNull) ? null : ((byte[])dataReader["Picture"]).Deserialize();
				}
			}
			return null;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0002BF5C File Offset: 0x0002A15C
		public void SetProductPicture(Guid productId, Image picture)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			bool flag = picture == null;
			if (flag)
			{
				DbParameter parameter = clockWorkFiles.GetParameter("@productid", DbType.Guid, productId);
				clockWorkFiles.ExecuteNonQuery("delete from InventoryV2_ProductImage where ProductId=@productid", new DbParameter[]
				{
					parameter
				});
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					clockWorkFiles.GetParameter("@productid", DbType.Guid, productId),
					clockWorkFiles.GetParameter("@picture", DbType.Binary, picture.Serialize())
				};
				clockWorkFiles.ExecuteNonQuery("IF EXISTS (SELECT 1 FROM [InventoryV2_ProductImage] where ProductId=@productid)\r\n                begin\r\n                    update [InventoryV2_ProductImage] set [Picture]=@picture where ProductId=@productid\r\n                end\r\n              ELSE\r\n                begin\r\n                    insert into [InventoryV2_ProductImage] (ProductId, [Picture]) VALUES (@productid, @picture)\r\n                end", parameters);
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0002BFE8 File Offset: 0x0002A1E8
		private static InventoryAttachedFile GetAttachment(IDataRecord record)
		{
			InventoryAttachedFileInfo attachmentFileInfo = InventoryAttachmentDAO.GetAttachmentFileInfo(record);
			return new InventoryAttachedFile
			{
				AttachedFileInfo = attachmentFileInfo,
				BinaryData = InventoryAttachmentDAO.GetBinaryData(attachmentFileInfo.Id)
			};
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0002C020 File Offset: 0x0002A220
		private static byte[] GetBinaryData(int attId)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter parameter = clockWorkFiles.GetParameter("@attachmentid", DbType.Int32, attId);
			using (IDataReader dataReader = clockWorkFiles.ExecuteQueryReader("select AttachmentID, BinaryData from [InventoryV2_AttachedFileData] where AttachmentID=@attachmentid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return (byte[])dataReader["BinaryData"];
				}
			}
			return null;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0002C0A8 File Offset: 0x0002A2A8
		private static InventoryAttachedFileInfo GetAttachmentFileInfo(IDataRecord record)
		{
			return new InventoryAttachedFileInfo
			{
				Id = (int)record["AttachmentID"],
				Name = (string)record["AttachmentName"],
				CreatedDatetime = (DateTime)record["CreatedDate"],
				Notes = (string)record["Notes"],
				SizeInBytes = (int)record["SizeInBytes"]
			};
		}
	}
}
