using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Membership;
using TechnoPro.Common.DAO.Messaging;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DropBox;
using TechnoPro.Common.Public.Entities.Membership;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.DAO.Impl.Messaging
{
	// Token: 0x0200008A RID: 138
	public class DropBox_Att_DAO : IAttachmentDropBoxDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000396 RID: 918 RVA: 0x0001FCD8 File Offset: 0x0001DED8
		public DropBox_Att_DAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001FCEC File Offset: 0x0001DEEC
		public void Save(DropBox_Attachment item)
		{
			IUserDAO userDAO = ObjectFactory.Resolve<IUserDAO>();
			userDAO.OpContext = this.OpContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			User user = userDAO.GetUser(item.Info.From.Username);
			User user2 = userDAO.GetUser(item.Info.To);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetParameter("@to", DbType.String, user2.Id),
				databaseLayer.GetParameter("@from", DbType.String, user.Id),
				databaseLayer.GetParameter("@binarydata", DbType.Binary, item.BinaryData),
				databaseLayer.GetParameter("@issuedon", DbType.DateTime, item.Info.IssuedOn),
				databaseLayer.GetParameter("@filename", DbType.String, item.Info.Filename),
				databaseLayer.GetParameter("@extension", DbType.String, item.Info.Extension),
				databaseLayer.GetParameter("@description", DbType.String, string.IsNullOrEmpty(item.Info.Description) ? DBNull.Value : item.Info.Description),
				databaseLayer.GetParameter("@reqreceivingconfirmation", DbType.Boolean, item.Info.RequiredReceivingConfirmation),
				databaseLayer.GetParameter("@sizeinbytes", DbType.Int32, (item.BinaryData != null) ? item.BinaryData.Length : item.Info.SizeInBytes),
				databaseLayer.GetOutputParameter("@id", DbType.Int32, 0)
			};
			databaseLayer.ExecuteNonQuery("insert into Messaging_AttachmentDropBox ([ToID], [FromID], [BinaryData], [IssuedOn], [Filename], [Extension], [Description], [ReqReceivingConfirmation], [SizeInBytes])\r\n              values (@to, @from, @binarydata, @issuedon, @filename, @extension, @description, @reqreceivingconfirmation, @sizeinbytes)\r\n              set @id = SCOPE_IDENTITY()", array);
			bool flag = !(array[array.Length - 1].Value is DBNull);
			if (flag)
			{
				item.Id = (int)array[array.Length - 1].Value;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001FED0 File Offset: 0x0001E0D0
		public IList<DropBox_AttachmentInfo> GetAllAttachmentsInfo(string username)
		{
			List<DropBox_AttachmentInfo> list = new List<DropBox_AttachmentInfo>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@username", DbType.String, username);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from Messaging_AttachmentDropBox \r\n                                                              where [ToID]=@username", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						DropBox_AttachmentInfo attachmentInfo = this.GetAttachmentInfo(dataReader);
						list.Add(attachmentInfo);
					}
				}
			}
			return list;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001FF78 File Offset: 0x0001E178
		public int CountAttachments(string username)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@username", DbType.String, username);
			return (int)databaseLayer.ExecuteScalar("select COUNT(*) as [Count] from Messaging_AttachmentDropBox where [ToID]=@username", new DbParameter[]
			{
				parameter
			});
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001FFCC File Offset: 0x0001E1CC
		public DropBox_Attachment GetAttachment(int id)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@id", DbType.Int32, id);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("Select * from [Messaging_AttachmentDropBox] where ID=@id", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetAttachment(dataReader);
				}
			}
			return null;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00020060 File Offset: 0x0001E260
		public void Delete(int id)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@id", DbType.Int32, id);
			databaseLayer.ExecuteNonQuery("Delete from [Messaging_AttachmentDropBox] where ID=@id", new DbParameter[]
			{
				parameter
			});
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000200B4 File Offset: 0x0001E2B4
		public DropBox_Attachment GetAttachment(string filename, string extension)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@filename", DbType.String, filename),
				databaseLayer.GetParameter("@extension", DbType.String, extension)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("Select * from [Messaging_AttachmentDropBox] where Filename=@filename and Extension=@extension", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetAttachment(dataReader);
				}
			}
			return null;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00020154 File Offset: 0x0001E354
		private DropBox_AttachmentInfo GetAttachmentInfo(IDataRecord record)
		{
			return new DropBox_AttachmentInfo
			{
				Id = (int)record["ID"],
				To = (string)record["ToID"],
				From = new DropBox_User
				{
					Username = (string)record["FromID"]
				},
				IssuedOn = (DateTime)record["IssuedOn"],
				Filename = (string)record["Filename"],
				Extension = (string)record["Extension"],
				Description = ((record["Description"] == DBNull.Value) ? string.Empty : ((string)record["Description"])),
				RequiredReceivingConfirmation = (bool)record["ReqReceivingConfirmation"],
				WasRead = (bool)record["WasRead"],
				SizeInBytes = (int)record["SizeInBytes"]
			};
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00020278 File Offset: 0x0001E478
		private DropBox_Attachment GetAttachment(IDataRecord record)
		{
			return new DropBox_Attachment
			{
				Info = this.GetAttachmentInfo(record),
				BinaryData = (byte[])record["BinaryData"]
			};
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600039F RID: 927 RVA: 0x000202B4 File Offset: 0x0001E4B4
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x000202BC File Offset: 0x0001E4BC
		public OperationContext OpContext { get; set; }
	}
}
