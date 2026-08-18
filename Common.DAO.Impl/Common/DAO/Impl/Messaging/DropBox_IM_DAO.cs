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
	// Token: 0x02000089 RID: 137
	public class DropBox_IM_DAO : IIMDropBoxDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600038D RID: 909 RVA: 0x0001F8A8 File Offset: 0x0001DAA8
		public DropBox_IM_DAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001F8BC File Offset: 0x0001DABC
		public void Save(DropBox_IM item)
		{
			IUserDAO userDAO = ObjectFactory.Resolve<IUserDAO>();
			userDAO.OpContext = this.OpContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			User user = userDAO.GetUser(item.From.Username);
			User user2 = userDAO.GetUser(item.To);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetParameter("@to", DbType.String, user2.Id),
				databaseLayer.GetParameter("@from", DbType.String, user.Id),
				databaseLayer.GetParameter("@message", DbType.String, item.Message),
				databaseLayer.GetParameter("@issuedon", DbType.DateTime, item.IssuedOn),
				databaseLayer.GetParameter("@requiredresponse", DbType.Boolean, item.RequiredResponse),
				databaseLayer.GetParameter("@reqreceivingconfirmation", DbType.Boolean, item.RequiredReceivingConfirmation),
				databaseLayer.GetOutputParameter("@id", DbType.Int32, 0)
			};
			databaseLayer.ExecuteNonQuery("insert into Messaging_IMDropBox ([ToID], [FromID], [Message], IssuedOn, RequiredResponse, ReqReceivingConfirmation)\r\n              values (@to, @from, @message, @issuedon, @requiredresponse, @reqreceivingconfirmation)\r\n              set @id = SCOPE_IDENTITY()", array);
			bool flag = !(array[array.Length - 1].Value is DBNull);
			if (flag)
			{
				item.Id = (int)array[array.Length - 1].Value;
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001FA08 File Offset: 0x0001DC08
		public IList<DropBox_IM> GetAllIMs(string username)
		{
			List<DropBox_IM> list = new List<DropBox_IM>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@username", DbType.String, username);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from Messaging_IMDropBox where [ToID]=@username", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						DropBox_IM im = DropBox_IM_DAO.GetIM(dataReader);
						list.Add(im);
					}
				}
			}
			return list;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001FAB0 File Offset: 0x0001DCB0
		public int CountIMs(string username)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@username", DbType.String, username);
			return (int)databaseLayer.ExecuteScalar("select COUNT(*) as [Count] from Messaging_IMDropBox where [ToID]=@username", new DbParameter[]
			{
				parameter
			});
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001FB04 File Offset: 0x0001DD04
		public DropBox_IM GetIM(int id)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@id", DbType.Int32, id);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("Select * from [Messaging_IMDropBox] where ID=@id", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return DropBox_IM_DAO.GetIM(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001FB98 File Offset: 0x0001DD98
		public void Delete(int id)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@id", DbType.Int32, id);
			databaseLayer.ExecuteNonQuery("Delete from [Messaging_IMDropBox] where ID=@id", new DbParameter[]
			{
				parameter
			});
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001FBEC File Offset: 0x0001DDEC
		private static DropBox_IM GetIM(IDataRecord record)
		{
			return new DropBox_IM
			{
				Id = (int)record["ID"],
				To = (string)record["ToID"],
				From = new DropBox_User
				{
					Username = (string)record["FromID"]
				},
				IssuedOn = (DateTime)record["IssuedOn"],
				Message = (string)record["Message"],
				RequiredResponse = (bool)record["RequiredResponse"],
				RequiredReceivingConfirmation = (bool)record["ReqReceivingConfirmation"],
				WasRead = (bool)record["WasRead"]
			};
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0001FCC7 File Offset: 0x0001DEC7
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0001FCCF File Offset: 0x0001DECF
		public OperationContext OpContext { get; set; }
	}
}
