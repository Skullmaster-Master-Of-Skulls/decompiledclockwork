using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.Globalization;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Web.Resources;
using System.Web.Security;

namespace System.Web.ClientServices.Providers
{
	// Token: 0x02000110 RID: 272
	public class ClientRoleProvider : RoleProvider
	{
		// Token: 0x06000E3B RID: 3643 RVA: 0x00032164 File Offset: 0x00030364
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			base.Initialize(name, config);
			this.ServiceUri = config["serviceUri"];
			string text = config["cacheTimeout"];
			if (!string.IsNullOrEmpty(text))
			{
				this._CacheTimeout = int.Parse(text, CultureInfo.InvariantCulture);
			}
			this._ConnectionString = config["connectionStringName"];
			if (string.IsNullOrEmpty(this._ConnectionString))
			{
				this._ConnectionString = SqlHelper.GetDefaultConnectionString();
			}
			else if (ConfigurationManager.ConnectionStrings[this._ConnectionString] != null)
			{
				this._ConnectionStringProvider = ConfigurationManager.ConnectionStrings[this._ConnectionString].ProviderName;
				this._ConnectionString = ConfigurationManager.ConnectionStrings[this._ConnectionString].ConnectionString;
			}
			int num = SqlHelper.IsSpecialConnectionString(this._ConnectionString);
			if (num != 1)
			{
				if (num == 2)
				{
					this._UsingIsolatedStore = true;
				}
			}
			else
			{
				this._UsingFileSystemStore = true;
			}
			text = config["honorCookieExpiry"];
			if (!string.IsNullOrEmpty(text))
			{
				this._HonorCookieExpiry = (string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0);
			}
			config.Remove("name");
			config.Remove("description");
			config.Remove("cacheTimeout");
			config.Remove("connectionStringName");
			config.Remove("serviceUri");
			config.Remove("honorCookieExpiry");
			foreach (object obj in config.Keys)
			{
				string text2 = (string)obj;
				if (!string.IsNullOrEmpty(text2))
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.AttributeNotRecognized, new object[]
					{
						text2
					}));
				}
			}
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00032330 File Offset: 0x00030530
		public override bool IsUserInRole(string username, string roleName)
		{
			string[] rolesForUser = this.GetRolesForUser(username);
			foreach (string strA in rolesForUser)
			{
				if (string.Compare(strA, roleName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00032368 File Offset: 0x00030568
		public override string[] GetRolesForUser(string username)
		{
			string[] result;
			lock (this)
			{
				IPrincipal currentPrincipal = Thread.CurrentPrincipal;
				if (currentPrincipal == null || currentPrincipal.Identity == null || !currentPrincipal.Identity.IsAuthenticated)
				{
					result = new string[0];
				}
				else
				{
					if (!string.IsNullOrEmpty(username) && string.Compare(username, currentPrincipal.Identity.Name, StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new ArgumentException(AtlasWeb.ArgumentMustBeCurrentUser, "username");
					}
					if (string.Compare(this._CurrentUser, currentPrincipal.Identity.Name, StringComparison.OrdinalIgnoreCase) == 0 && DateTime.UtcNow < this._CacheExpiryDate)
					{
						result = this._Roles;
					}
					else if (this.GetRolesFromDBForUser(currentPrincipal.Identity.Name))
					{
						result = this._Roles;
					}
					else if (ConnectivityStatus.IsOffline)
					{
						result = new string[0];
					}
					else
					{
						this._Roles = null;
						this._CacheExpiryDate = DateTime.UtcNow;
						this._CurrentUser = currentPrincipal.Identity.Name;
						this.GetRolesForUserCore(currentPrincipal.Identity);
						if (!this._HonorCookieExpiry && this._Roles.Length < 1 && currentPrincipal.Identity is ClientFormsIdentity)
						{
							((ClientFormsIdentity)currentPrincipal.Identity).RevalidateUser();
							this.GetRolesForUserCore(currentPrincipal.Identity);
						}
						this.StoreRolesForCurrentUser();
						result = this._Roles;
					}
				}
			}
			return result;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x000324E4 File Offset: 0x000306E4
		public void ResetCache()
		{
			lock (this)
			{
				this._Roles = null;
				this._CacheExpiryDate = DateTime.UtcNow;
				this.RemoveRolesFromDB();
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00032534 File Offset: 0x00030734
		private void GetRolesForUserCore(IIdentity identity)
		{
			CookieContainer cookieContainer = null;
			if (identity is ClientFormsIdentity)
			{
				cookieContainer = ((ClientFormsIdentity)identity).AuthenticationCookies;
			}
			if (this._UsingWFCService)
			{
				throw new NotImplementedException();
			}
			object obj = ProxyHelper.CreateWebRequestAndGetResponse(this.GetServiceUri() + "/GetRolesForCurrentUser", ref cookieContainer, identity.Name, this._ConnectionString, this._ConnectionStringProvider, null, null, typeof(string[]));
			if (obj != null)
			{
				this._Roles = (string[])obj;
			}
			else
			{
				this._Roles = new string[0];
			}
			this._CacheExpiryDate = DateTime.UtcNow.AddMinutes((double)this._CacheTimeout);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x000325D4 File Offset: 0x000307D4
		private void RemoveRolesFromDB()
		{
			if (string.IsNullOrEmpty(this._CurrentUser))
			{
				return;
			}
			if (this._UsingFileSystemStore || this._UsingIsolatedStore)
			{
				ClientData userClientData = ClientDataManager.GetUserClientData(this._CurrentUser, this._UsingIsolatedStore);
				userClientData.Roles = null;
				userClientData.Save();
				return;
			}
			using (DbConnection connection = SqlHelper.GetConnection(this._CurrentUser, this._ConnectionString, this._ConnectionStringProvider))
			{
				DbTransaction dbTransaction = null;
				try
				{
					dbTransaction = connection.BeginTransaction();
					DbCommand dbCommand = connection.CreateCommand();
					dbCommand.CommandText = "DELETE FROM Roles WHERE UserName = @UserName";
					SqlHelper.AddParameter(connection, dbCommand, "@UserName", this._CurrentUser);
					dbCommand.Transaction = dbTransaction;
					dbCommand.ExecuteNonQuery();
					dbCommand = connection.CreateCommand();
					dbCommand.CommandText = "DELETE FROM UserProperties WHERE PropertyName = @RolesCachedDate";
					SqlHelper.AddParameter(connection, dbCommand, "@RolesCachedDate", "RolesCachedDate_" + this._CurrentUser);
					dbCommand.Transaction = dbTransaction;
					dbCommand.ExecuteNonQuery();
				}
				catch
				{
					if (dbTransaction != null)
					{
						dbTransaction.Rollback();
						dbTransaction = null;
					}
					throw;
				}
				finally
				{
					if (dbTransaction != null)
					{
						dbTransaction.Commit();
					}
				}
			}
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000326FC File Offset: 0x000308FC
		private void StoreRolesForCurrentUser()
		{
			if (this._UsingFileSystemStore || this._UsingIsolatedStore)
			{
				ClientData userClientData = ClientDataManager.GetUserClientData(this._CurrentUser, this._UsingIsolatedStore);
				userClientData.Roles = this._Roles;
				userClientData.RolesCachedDateUtc = DateTime.UtcNow;
				userClientData.Save();
				return;
			}
			this.RemoveRolesFromDB();
			DbTransaction dbTransaction = null;
			using (DbConnection connection = SqlHelper.GetConnection(this._CurrentUser, this._ConnectionString, this._ConnectionStringProvider))
			{
				try
				{
					dbTransaction = connection.BeginTransaction();
					DbCommand dbCommand;
					foreach (string paramValue in this._Roles)
					{
						dbCommand = connection.CreateCommand();
						dbCommand.CommandText = "INSERT INTO Roles(UserName, RoleName) VALUES(@UserName, @RoleName)";
						SqlHelper.AddParameter(connection, dbCommand, "@UserName", this._CurrentUser);
						SqlHelper.AddParameter(connection, dbCommand, "@RoleName", paramValue);
						dbCommand.Transaction = dbTransaction;
						dbCommand.ExecuteNonQuery();
					}
					dbCommand = connection.CreateCommand();
					dbCommand.CommandText = "INSERT INTO UserProperties (PropertyName, PropertyValue) VALUES(@RolesCachedDate, @Date)";
					SqlHelper.AddParameter(connection, dbCommand, "@RolesCachedDate", "RolesCachedDate_" + this._CurrentUser);
					SqlHelper.AddParameter(connection, dbCommand, "@Date", DateTime.UtcNow.ToFileTimeUtc().ToString(CultureInfo.InvariantCulture));
					dbCommand.Transaction = dbTransaction;
					dbCommand.ExecuteNonQuery();
				}
				catch
				{
					if (dbTransaction != null)
					{
						dbTransaction.Rollback();
						dbTransaction = null;
					}
					throw;
				}
				finally
				{
					if (dbTransaction != null)
					{
						dbTransaction.Commit();
					}
				}
			}
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00032884 File Offset: 0x00030A84
		private bool GetRolesFromDBForUser(string username)
		{
			this._Roles = null;
			this._CacheExpiryDate = DateTime.UtcNow;
			this._CurrentUser = username;
			if (!this._UsingFileSystemStore && !this._UsingIsolatedStore)
			{
				bool result;
				using (DbConnection connection = SqlHelper.GetConnection(this._CurrentUser, this._ConnectionString, this._ConnectionStringProvider))
				{
					DbTransaction dbTransaction = null;
					try
					{
						dbTransaction = connection.BeginTransaction();
						DbCommand dbCommand = connection.CreateCommand();
						dbCommand.Transaction = dbTransaction;
						dbCommand.CommandText = "SELECT PropertyValue FROM UserProperties WHERE PropertyName = @RolesCachedDate";
						SqlHelper.AddParameter(connection, dbCommand, "@RolesCachedDate", "RolesCachedDate_" + this._CurrentUser);
						string text = dbCommand.ExecuteScalar() as string;
						if (text == null)
						{
							result = false;
						}
						else
						{
							long fileTime = long.Parse(text, CultureInfo.InvariantCulture);
							this._CacheExpiryDate = DateTime.FromFileTimeUtc(fileTime).AddMinutes((double)this._CacheTimeout);
							if (!ConnectivityStatus.IsOffline && this._CacheExpiryDate < DateTime.UtcNow)
							{
								result = false;
							}
							else
							{
								dbCommand = connection.CreateCommand();
								dbCommand.Transaction = dbTransaction;
								dbCommand.CommandText = "SELECT RoleName FROM Roles WHERE UserName = @UserName ORDER BY RoleName";
								SqlHelper.AddParameter(connection, dbCommand, "@UserName", this._CurrentUser);
								ArrayList arrayList = new ArrayList();
								using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
								{
									while (dbDataReader.Read())
									{
										arrayList.Add(dbDataReader.GetString(0));
									}
								}
								this._Roles = new string[arrayList.Count];
								for (int i = 0; i < arrayList.Count; i++)
								{
									this._Roles[i] = (string)arrayList[i];
								}
								result = true;
							}
						}
					}
					catch
					{
						if (dbTransaction != null)
						{
							dbTransaction.Rollback();
							dbTransaction = null;
						}
						throw;
					}
					finally
					{
						if (dbTransaction != null)
						{
							dbTransaction.Commit();
						}
					}
				}
				return result;
			}
			ClientData userClientData = ClientDataManager.GetUserClientData(username, this._UsingIsolatedStore);
			if (userClientData.Roles == null)
			{
				return false;
			}
			this._Roles = userClientData.Roles;
			this._CacheExpiryDate = userClientData.RolesCachedDateUtc.AddMinutes((double)this._CacheTimeout);
			return ConnectivityStatus.IsOffline || !(this._CacheExpiryDate < DateTime.UtcNow);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00032B08 File Offset: 0x00030D08
		private string GetServiceUri()
		{
			if (string.IsNullOrEmpty(this._ServiceUri))
			{
				throw new ArgumentException(AtlasWeb.ServiceUriNotFound);
			}
			return this._ServiceUri;
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x00032B28 File Offset: 0x00030D28
		// (set) Token: 0x06000E45 RID: 3653 RVA: 0x00032B30 File Offset: 0x00030D30
		public string ServiceUri
		{
			get
			{
				return this._ServiceUri;
			}
			set
			{
				this._ServiceUri = value;
				if (string.IsNullOrEmpty(this._ServiceUri))
				{
					this._UsingWFCService = false;
					return;
				}
				this._UsingWFCService = this._ServiceUri.EndsWith(".svc", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00032138 File Offset: 0x00030338
		// (set) Token: 0x06000E47 RID: 3655 RVA: 0x000032F4 File Offset: 0x000014F4
		public override string ApplicationName
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003214D File Offset: 0x0003034D
		public override void CreateRole(string roleName)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003214D File Offset: 0x0003034D
		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0003214D File Offset: 0x0003034D
		public override bool RoleExists(string roleName)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0003214D File Offset: 0x0003034D
		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0003214D File Offset: 0x0003034D
		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003214D File Offset: 0x0003034D
		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0003214D File Offset: 0x0003034D
		public override string[] GetAllRoles()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003214D File Offset: 0x0003034D
		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000403 RID: 1027
		private string _ConnectionString;

		// Token: 0x04000404 RID: 1028
		private string _ConnectionStringProvider;

		// Token: 0x04000405 RID: 1029
		private string _ServiceUri;

		// Token: 0x04000406 RID: 1030
		private string[] _Roles;

		// Token: 0x04000407 RID: 1031
		private string _CurrentUser;

		// Token: 0x04000408 RID: 1032
		private int _CacheTimeout = 1440;

		// Token: 0x04000409 RID: 1033
		private DateTime _CacheExpiryDate = DateTime.UtcNow;

		// Token: 0x0400040A RID: 1034
		private bool _HonorCookieExpiry;

		// Token: 0x0400040B RID: 1035
		private bool _UsingFileSystemStore;

		// Token: 0x0400040C RID: 1036
		private bool _UsingIsolatedStore;

		// Token: 0x0400040D RID: 1037
		private bool _UsingWFCService;
	}
}
