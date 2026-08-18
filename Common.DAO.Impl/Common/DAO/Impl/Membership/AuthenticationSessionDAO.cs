using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.DAO.Membership;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Membership;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.DAO.Impl.Membership
{
	// Token: 0x0200008E RID: 142
	public class AuthenticationSessionDAO : IAuthenticationSessionDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003AC RID: 940 RVA: 0x00020A8F File Offset: 0x0001EC8F
		public AuthenticationSessionDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00020AA4 File Offset: 0x0001ECA4
		public void SaveSession(AuthenticationSession authSession)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@sessionid", DbType.String, authSession.Id.ToString()),
				databaseLayer.GetParameter("@issuedon", DbType.DateTime, authSession.IssuedOn),
				databaseLayer.GetParameter("@neverexpires", DbType.Boolean, authSession.NeverExpires),
				databaseLayer.GetParameter("@username", DbType.Binary, databaseLayer.Encryption.Encrypt(authSession.User.Id)),
				databaseLayer.GetParameter("@clientparameters", DbType.Binary, authSession.ClientParameters.Serialize())
			};
			databaseLayer.ExecuteNonQuery("insert into [ClockWorkServer_AuthenticationSession] (ID, IssuedOn, NeverExpires, Username, ClientParameters) values(@sessionid, @issuedon, @neverexpires, @username, @clientparameters)", parameters);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00020B78 File Offset: 0x0001ED78
		public void DeleteSession(string guid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@sessionid", DbType.String, guid);
			databaseLayer.ExecuteNonQuery("delete from [ClockWorkServer_AuthenticationSession] where ID = @sessionid", new DbParameter[]
			{
				parameter
			});
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00020BC4 File Offset: 0x0001EDC4
		public IList<AuthenticationSession> GetAllSessions()
		{
			List<AuthenticationSession> list = new List<AuthenticationSession>();
			try
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from [ClockWorkServer_AuthenticationSession]"))
				{
					bool flag = dataReader != null;
					if (flag)
					{
						while (dataReader.Read())
						{
							AuthenticationSession authenticationSession = this.GetAuthenticationSession(dataReader);
							bool flag2 = authenticationSession != null;
							if (flag2)
							{
								list.Add(authenticationSession);
							}
						}
					}
				}
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("AuthenticationSessionDAO::GetAllSessions:: {0}", ex.ToString()), ex);
			}
			return list;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00020C90 File Offset: 0x0001EE90
		public void UpdateClientParameters(string guid, ClientParameters clientParameters)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@clientparameters", DbType.Binary, clientParameters.Serialize()),
				databaseLayer.GetParameter("@sessionid", DbType.String, guid)
			};
			databaseLayer.ExecuteNonQuery("update [ClockWorkServer_AuthenticationSession] set ClientParameters = @clientparameters where ID = @sessionid", parameters);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00020CF4 File Offset: 0x0001EEF4
		private AuthenticationSession GetAuthenticationSession(IDataRecord record)
		{
			IUserDAO userDAO = ObjectFactory.Resolve<IUserDAO>();
			userDAO.OpContext = this.OpContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string username = databaseLayer.Encryption.Decrypt((byte[])record["Username"]);
			User user = userDAO.GetUser(username);
			bool flag = user == null;
			AuthenticationSession result;
			if (flag)
			{
				string guid = Convert.ToString(record["ID"]);
				this.DeleteSession(guid);
				result = null;
			}
			else
			{
				AuthenticationSession authenticationSession = new AuthenticationSession
				{
					Id = new Guid((string)record["ID"]),
					IssuedOn = (DateTime)record["IssuedOn"],
					NeverExpires = (bool)record["NeverExpires"],
					User = user,
					ClientParameters = ClientParametersSerializer.Deserialize((byte[])record["ClientParameters"]),
					LastCheckedTime = DateTime.Now
				};
				user.AuthenticationSession = authenticationSession;
				result = authenticationSession;
			}
			return result;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00020E12 File Offset: 0x0001F012
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00020E1A File Offset: 0x0001F01A
		public OperationContext OpContext { get; set; }
	}
}
