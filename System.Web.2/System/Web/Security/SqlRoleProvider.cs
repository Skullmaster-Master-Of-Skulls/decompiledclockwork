using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Web.DataAccess;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005F8 RID: 1528
	public class SqlRoleProvider : RoleProvider
	{
		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x06004D44 RID: 19780 RVA: 0x0010B6CA File Offset: 0x001098CA
		private int CommandTimeout
		{
			get
			{
				return this._CommandTimeout;
			}
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x0010B6D4 File Offset: 0x001098D4
		public override void Initialize(string name, NameValueCollection config)
		{
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "SqlRoleProvider";
			}
			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", SR.GetString("RoleSqlProvider_description"));
			}
			base.Initialize(name, config);
			this._SchemaVersionCheck = 0;
			this._CommandTimeout = SecUtility.GetIntValue(config, "commandTimeout", 30, true, 0);
			this._sqlConnectionString = SecUtility.GetConnectionString(config);
			this._AppName = config["applicationName"];
			if (string.IsNullOrEmpty(this._AppName))
			{
				this._AppName = SecUtility.GetDefaultAppName();
			}
			if (this._AppName.Length > 256)
			{
				throw new ProviderException(SR.GetString("Provider_application_name_too_long"));
			}
			config.Remove("connectionString");
			config.Remove("connectionStringName");
			config.Remove("applicationName");
			config.Remove("commandTimeout");
			if (config.Count > 0)
			{
				string key = config.GetKey(0);
				if (!string.IsNullOrEmpty(key))
				{
					throw new ProviderException(SR.GetString("Provider_unrecognized_attribute", new object[]
					{
						key
					}));
				}
			}
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x0010B81C File Offset: 0x00109A1C
		private void CheckSchemaVersion(SqlConnection connection)
		{
			string[] features = new string[]
			{
				"Role Manager"
			};
			string version = "1";
			SecUtility.CheckSchemaVersion(this, connection, features, version, ref this._SchemaVersionCheck);
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x0010B850 File Offset: 0x00109A50
		public override bool IsUserInRole(string username, string roleName)
		{
			SecUtility.CheckParameter(ref roleName, true, true, true, 256, "roleName");
			SecUtility.CheckParameter(ref username, true, false, true, 256, "username");
			if (username.Length < 1)
			{
				return false;
			}
			bool result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_UsersInRoles_IsUserInRole", sqlConnectionHolder.Connection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.CommandTimeout = this.CommandTimeout;
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, username));
					sqlCommand.Parameters.Add(this.CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName));
					sqlCommand.ExecuteNonQuery();
					switch (this.GetReturnValue(sqlCommand))
					{
					case 0:
						result = false;
						break;
					case 1:
						result = true;
						break;
					case 2:
						result = false;
						break;
					case 3:
						result = false;
						break;
					default:
						throw new ProviderException(SR.GetString("Provider_unknown_failure"));
					}
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x0010B9D4 File Offset: 0x00109BD4
		public override string[] GetRolesForUser(string username)
		{
			SecUtility.CheckParameter(ref username, true, false, true, 256, "username");
			if (username.Length < 1)
			{
				return new string[0];
			}
			string[] result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_UsersInRoles_GetRolesForUser", sqlConnectionHolder.Connection);
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					SqlDataReader sqlDataReader = null;
					StringCollection stringCollection = new StringCollection();
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, username));
					try
					{
						sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess);
						while (sqlDataReader.Read())
						{
							stringCollection.Add(sqlDataReader.GetString(0));
						}
					}
					catch
					{
						throw;
					}
					finally
					{
						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}
					}
					if (stringCollection.Count > 0)
					{
						string[] array = new string[stringCollection.Count];
						stringCollection.CopyTo(array, 0);
						result = array;
					}
					else
					{
						int returnValue = this.GetReturnValue(sqlCommand);
						if (returnValue != 0)
						{
							if (returnValue != 1)
							{
								throw new ProviderException(SR.GetString("Provider_unknown_failure"));
							}
							result = new string[0];
						}
						else
						{
							result = new string[0];
						}
					}
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x0010BBB0 File Offset: 0x00109DB0
		public override void CreateRole(string roleName)
		{
			SecUtility.CheckParameter(ref roleName, true, true, true, 256, "roleName");
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Roles_CreateRole", sqlConnectionHolder.Connection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.CommandTimeout = this.CommandTimeout;
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName));
					sqlCommand.ExecuteNonQuery();
					int returnValue = this.GetReturnValue(sqlCommand);
					if (returnValue != 0)
					{
						if (returnValue != 1)
						{
							throw new ProviderException(SR.GetString("Provider_unknown_failure"));
						}
						throw new ProviderException(SR.GetString("Provider_role_already_exists", new object[]
						{
							roleName
						}));
					}
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x0010BCD8 File Offset: 0x00109ED8
		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			SecUtility.CheckParameter(ref roleName, true, true, true, 256, "roleName");
			bool result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Roles_DeleteRole", sqlConnectionHolder.Connection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.CommandTimeout = this.CommandTimeout;
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@DeleteOnlyIfRoleIsEmpty", SqlDbType.Bit, throwOnPopulatedRole ? 1 : 0));
					sqlCommand.ExecuteNonQuery();
					int returnValue = this.GetReturnValue(sqlCommand);
					if (returnValue == 2)
					{
						throw new ProviderException(SR.GetString("Role_is_not_empty"));
					}
					result = (returnValue == 0);
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x0010BE10 File Offset: 0x0010A010
		public override bool RoleExists(string roleName)
		{
			SecUtility.CheckParameter(ref roleName, true, true, true, 256, "roleName");
			bool result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Roles_RoleExists", sqlConnectionHolder.Connection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.CommandTimeout = this.CommandTimeout;
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName));
					sqlCommand.ExecuteNonQuery();
					int returnValue = this.GetReturnValue(sqlCommand);
					if (returnValue != 0)
					{
						if (returnValue != 1)
						{
							throw new ProviderException(SR.GetString("Provider_unknown_failure"));
						}
						result = true;
					}
					else
					{
						result = false;
					}
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x0010BF28 File Offset: 0x0010A128
		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			SecUtility.CheckArrayParameter(ref roleNames, true, true, true, 256, "roleNames");
			SecUtility.CheckArrayParameter(ref usernames, true, true, true, 256, "usernames");
			bool flag = false;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					int i = usernames.Length;
					while (i > 0)
					{
						string text = usernames[usernames.Length - i];
						i--;
						int num = usernames.Length - i;
						while (num < usernames.Length && text.Length + usernames[num].Length + 1 < 4000)
						{
							text = text + "," + usernames[num];
							i--;
							num++;
						}
						int j = roleNames.Length;
						while (j > 0)
						{
							string text2 = roleNames[roleNames.Length - j];
							j--;
							num = roleNames.Length - j;
							while (num < roleNames.Length && text2.Length + roleNames[num].Length + 1 < 4000)
							{
								text2 = text2 + "," + roleNames[num];
								j--;
								num++;
							}
							if (!flag && (i > 0 || j > 0))
							{
								new SqlCommand("BEGIN TRANSACTION", sqlConnectionHolder.Connection).ExecuteNonQuery();
								flag = true;
							}
							this.AddUsersToRolesCore(sqlConnectionHolder.Connection, text, text2);
						}
					}
					if (flag)
					{
						new SqlCommand("COMMIT TRANSACTION", sqlConnectionHolder.Connection).ExecuteNonQuery();
						flag = false;
					}
				}
				catch
				{
					if (flag)
					{
						try
						{
							new SqlCommand("ROLLBACK TRANSACTION", sqlConnectionHolder.Connection).ExecuteNonQuery();
						}
						catch
						{
						}
						flag = false;
					}
					throw;
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x0010C128 File Offset: 0x0010A328
		private void AddUsersToRolesCore(SqlConnection conn, string usernames, string roleNames)
		{
			SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_UsersInRoles_AddUsersToRoles", conn);
			SqlDataReader sqlDataReader = null;
			SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
			string text = string.Empty;
			string text2 = string.Empty;
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.CommandTimeout = this.CommandTimeout;
			sqlParameter.Direction = ParameterDirection.ReturnValue;
			sqlCommand.Parameters.Add(sqlParameter);
			sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
			sqlCommand.Parameters.Add(this.CreateInputParam("@RoleNames", SqlDbType.NVarChar, roleNames));
			sqlCommand.Parameters.Add(this.CreateInputParam("@UserNames", SqlDbType.NVarChar, usernames));
			sqlCommand.Parameters.Add(this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));
			try
			{
				sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow);
				if (sqlDataReader.Read())
				{
					if (sqlDataReader.FieldCount > 0)
					{
						text = sqlDataReader.GetString(0);
					}
					if (sqlDataReader.FieldCount > 1)
					{
						text2 = sqlDataReader.GetString(1);
					}
				}
			}
			finally
			{
				if (sqlDataReader != null)
				{
					sqlDataReader.Close();
				}
			}
			switch (this.GetReturnValue(sqlCommand))
			{
			case 0:
				return;
			case 1:
				throw new ProviderException(SR.GetString("Provider_this_user_not_found", new object[]
				{
					text
				}));
			case 2:
				throw new ProviderException(SR.GetString("Provider_role_not_found", new object[]
				{
					text
				}));
			case 3:
				throw new ProviderException(SR.GetString("Provider_this_user_already_in_role", new object[]
				{
					text,
					text2
				}));
			default:
				throw new ProviderException(SR.GetString("Provider_unknown_failure"));
			}
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x0010C2CC File Offset: 0x0010A4CC
		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			SecUtility.CheckArrayParameter(ref roleNames, true, true, true, 256, "roleNames");
			SecUtility.CheckArrayParameter(ref usernames, true, true, true, 256, "usernames");
			bool flag = false;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					int i = usernames.Length;
					while (i > 0)
					{
						string text = usernames[usernames.Length - i];
						i--;
						int num = usernames.Length - i;
						while (num < usernames.Length && text.Length + usernames[num].Length + 1 < 4000)
						{
							text = text + "," + usernames[num];
							i--;
							num++;
						}
						int j = roleNames.Length;
						while (j > 0)
						{
							string text2 = roleNames[roleNames.Length - j];
							j--;
							num = roleNames.Length - j;
							while (num < roleNames.Length && text2.Length + roleNames[num].Length + 1 < 4000)
							{
								text2 = text2 + "," + roleNames[num];
								j--;
								num++;
							}
							if (!flag && (i > 0 || j > 0))
							{
								new SqlCommand("BEGIN TRANSACTION", sqlConnectionHolder.Connection).ExecuteNonQuery();
								flag = true;
							}
							this.RemoveUsersFromRolesCore(sqlConnectionHolder.Connection, text, text2);
						}
					}
					if (flag)
					{
						new SqlCommand("COMMIT TRANSACTION", sqlConnectionHolder.Connection).ExecuteNonQuery();
						flag = false;
					}
				}
				catch
				{
					if (flag)
					{
						new SqlCommand("ROLLBACK TRANSACTION", sqlConnectionHolder.Connection).ExecuteNonQuery();
						flag = false;
					}
					throw;
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x0010C4B0 File Offset: 0x0010A6B0
		private void RemoveUsersFromRolesCore(SqlConnection conn, string usernames, string roleNames)
		{
			SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_UsersInRoles_RemoveUsersFromRoles", conn);
			SqlDataReader sqlDataReader = null;
			SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
			string text = string.Empty;
			string text2 = string.Empty;
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.CommandTimeout = this.CommandTimeout;
			sqlParameter.Direction = ParameterDirection.ReturnValue;
			sqlCommand.Parameters.Add(sqlParameter);
			sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
			sqlCommand.Parameters.Add(this.CreateInputParam("@UserNames", SqlDbType.NVarChar, usernames));
			sqlCommand.Parameters.Add(this.CreateInputParam("@RoleNames", SqlDbType.NVarChar, roleNames));
			try
			{
				sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow);
				if (sqlDataReader.Read())
				{
					if (sqlDataReader.FieldCount > 0)
					{
						text = sqlDataReader.GetString(0);
					}
					if (sqlDataReader.FieldCount > 1)
					{
						text2 = sqlDataReader.GetString(1);
					}
				}
			}
			finally
			{
				if (sqlDataReader != null)
				{
					sqlDataReader.Close();
				}
			}
			switch (this.GetReturnValue(sqlCommand))
			{
			case 0:
				return;
			case 1:
				throw new ProviderException(SR.GetString("Provider_this_user_not_found", new object[]
				{
					text
				}));
			case 2:
				throw new ProviderException(SR.GetString("Provider_role_not_found", new object[]
				{
					text2
				}));
			case 3:
				throw new ProviderException(SR.GetString("Provider_this_user_already_not_in_role", new object[]
				{
					text,
					text2
				}));
			default:
				throw new ProviderException(SR.GetString("Provider_unknown_failure"));
			}
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x0010C634 File Offset: 0x0010A834
		public override string[] GetUsersInRole(string roleName)
		{
			SecUtility.CheckParameter(ref roleName, true, true, true, 256, "roleName");
			string[] result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_UsersInRoles_GetUsersInRoles", sqlConnectionHolder.Connection);
					SqlDataReader sqlDataReader = null;
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					StringCollection stringCollection = new StringCollection();
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName));
					try
					{
						sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess);
						while (sqlDataReader.Read())
						{
							stringCollection.Add(sqlDataReader.GetString(0));
						}
					}
					catch
					{
						throw;
					}
					finally
					{
						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}
					}
					if (stringCollection.Count < 1)
					{
						int returnValue = this.GetReturnValue(sqlCommand);
						if (returnValue != 0)
						{
							if (returnValue != 1)
							{
								throw new ProviderException(SR.GetString("Provider_unknown_failure"));
							}
							throw new ProviderException(SR.GetString("Provider_role_not_found", new object[]
							{
								roleName
							}));
						}
						else
						{
							result = new string[0];
						}
					}
					else
					{
						string[] array = new string[stringCollection.Count];
						stringCollection.CopyTo(array, 0);
						result = array;
					}
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x0010C810 File Offset: 0x0010AA10
		public override string[] GetAllRoles()
		{
			string[] result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Roles_GetAllRoles", sqlConnectionHolder.Connection);
					StringCollection stringCollection = new StringCollection();
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					SqlDataReader sqlDataReader = null;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					try
					{
						sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess);
						while (sqlDataReader.Read())
						{
							stringCollection.Add(sqlDataReader.GetString(0));
						}
					}
					catch
					{
						throw;
					}
					finally
					{
						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}
					}
					string[] array = new string[stringCollection.Count];
					stringCollection.CopyTo(array, 0);
					result = array;
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x0010C93C File Offset: 0x0010AB3C
		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			SecUtility.CheckParameter(ref roleName, true, true, true, 256, "roleName");
			SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 256, "usernameToMatch");
			string[] result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_UsersInRoles_FindUsersInRole", sqlConnectionHolder.Connection);
					SqlDataReader sqlDataReader = null;
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					StringCollection stringCollection = new StringCollection();
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserNameToMatch", SqlDbType.NVarChar, usernameToMatch));
					try
					{
						sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess);
						while (sqlDataReader.Read())
						{
							stringCollection.Add(sqlDataReader.GetString(0));
						}
					}
					catch
					{
						throw;
					}
					finally
					{
						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}
					}
					if (stringCollection.Count < 1)
					{
						int returnValue = this.GetReturnValue(sqlCommand);
						if (returnValue != 0)
						{
							if (returnValue != 1)
							{
								throw new ProviderException(SR.GetString("Provider_unknown_failure"));
							}
							throw new ProviderException(SR.GetString("Provider_role_not_found", new object[]
							{
								roleName
							}));
						}
						else
						{
							result = new string[0];
						}
					}
					else
					{
						string[] array = new string[stringCollection.Count];
						stringCollection.CopyTo(array, 0);
						result = array;
					}
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x06004D53 RID: 19795 RVA: 0x0010CB44 File Offset: 0x0010AD44
		// (set) Token: 0x06004D54 RID: 19796 RVA: 0x0010CB4C File Offset: 0x0010AD4C
		public override string ApplicationName
		{
			get
			{
				return this._AppName;
			}
			set
			{
				this._AppName = value;
				if (this._AppName.Length > 256)
				{
					throw new ProviderException(SR.GetString("Provider_application_name_too_long"));
				}
			}
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x0010CB78 File Offset: 0x0010AD78
		private SqlParameter CreateInputParam(string paramName, SqlDbType dbType, object objValue)
		{
			SqlParameter sqlParameter = new SqlParameter(paramName, dbType);
			if (objValue == null)
			{
				objValue = string.Empty;
			}
			sqlParameter.Value = objValue;
			return sqlParameter;
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x0010CBA0 File Offset: 0x0010ADA0
		private int GetReturnValue(SqlCommand cmd)
		{
			foreach (object obj in cmd.Parameters)
			{
				SqlParameter sqlParameter = (SqlParameter)obj;
				if (sqlParameter.Direction == ParameterDirection.ReturnValue && sqlParameter.Value != null && sqlParameter.Value is int)
				{
					return (int)sqlParameter.Value;
				}
			}
			return -1;
		}

		// Token: 0x04002949 RID: 10569
		private string _AppName;

		// Token: 0x0400294A RID: 10570
		private int _SchemaVersionCheck;

		// Token: 0x0400294B RID: 10571
		private string _sqlConnectionString;

		// Token: 0x0400294C RID: 10572
		private int _CommandTimeout;
	}
}
