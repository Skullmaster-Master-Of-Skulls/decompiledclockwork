using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.DataAccess;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200056C RID: 1388
	public class SqlPersonalizationProvider : PersonalizationProvider
	{
		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x06004664 RID: 18020 RVA: 0x000E7C7C File Offset: 0x000E5E7C
		// (set) Token: 0x06004665 RID: 18021 RVA: 0x000E7C9C File Offset: 0x000E5E9C
		public override string ApplicationName
		{
			get
			{
				if (string.IsNullOrEmpty(this._applicationName))
				{
					this._applicationName = SecUtility.GetDefaultAppName();
				}
				return this._applicationName;
			}
			set
			{
				if (value != null && value.Length > 256)
				{
					throw new ProviderException(SR.GetString("PersonalizationProvider_ApplicationNameExceedMaxLength", new object[]
					{
						256.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this._applicationName = value;
			}
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x000E7CEC File Offset: 0x000E5EEC
		private SqlParameter CreateParameter(string name, SqlDbType dbType, object value)
		{
			return new SqlParameter(name, dbType)
			{
				Value = value
			};
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x000E7D0C File Offset: 0x000E5F0C
		private PersonalizationStateInfoCollection FindSharedState(string path, int pageIndex, int pageSize, out int totalRecords)
		{
			SqlConnectionHolder sqlConnectionHolder = null;
			SqlDataReader sqlDataReader = null;
			totalRecords = 0;
			PersonalizationStateInfoCollection result;
			try
			{
				try
				{
					sqlConnectionHolder = this.GetConnectionHolder();
					SqlConnection connection = sqlConnectionHolder.Connection;
					this.CheckSchemaVersion(connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_PersonalizationAdministration_FindState", connection);
					this.SetCommandTypeAndTimeout(sqlCommand);
					SqlParameterCollection parameters = sqlCommand.Parameters;
					SqlParameter sqlParameter = parameters.Add(new SqlParameter("AllUsersScope", SqlDbType.Bit));
					sqlParameter.Value = true;
					parameters.AddWithValue("ApplicationName", this.ApplicationName);
					parameters.AddWithValue("PageIndex", pageIndex);
					parameters.AddWithValue("PageSize", pageSize);
					SqlParameter sqlParameter2 = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter2.Direction = ParameterDirection.ReturnValue;
					parameters.Add(sqlParameter2);
					sqlParameter = parameters.Add("Path", SqlDbType.NVarChar);
					if (path != null)
					{
						sqlParameter.Value = path;
					}
					sqlParameter = parameters.Add("UserName", SqlDbType.NVarChar);
					sqlParameter = parameters.Add("InactiveSinceDate", SqlDbType.DateTime);
					sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess);
					PersonalizationStateInfoCollection personalizationStateInfoCollection = new PersonalizationStateInfoCollection();
					if (sqlDataReader != null)
					{
						if (sqlDataReader.HasRows)
						{
							while (sqlDataReader.Read())
							{
								string @string = sqlDataReader.GetString(0);
								DateTime lastUpdatedDate = sqlDataReader.IsDBNull(1) ? DateTime.MinValue : DateTime.SpecifyKind(sqlDataReader.GetDateTime(1), DateTimeKind.Utc);
								int size = sqlDataReader.IsDBNull(2) ? 0 : sqlDataReader.GetInt32(2);
								int sizeOfPersonalizations = sqlDataReader.IsDBNull(3) ? 0 : sqlDataReader.GetInt32(3);
								int countOfPersonalizations = sqlDataReader.IsDBNull(4) ? 0 : sqlDataReader.GetInt32(4);
								personalizationStateInfoCollection.Add(new SharedPersonalizationStateInfo(@string, lastUpdatedDate, size, sizeOfPersonalizations, countOfPersonalizations));
							}
						}
						sqlDataReader.Close();
						sqlDataReader = null;
					}
					if (sqlParameter2.Value != null && sqlParameter2.Value is int)
					{
						totalRecords = (int)sqlParameter2.Value;
					}
					result = personalizationStateInfoCollection;
				}
				finally
				{
					if (sqlDataReader != null)
					{
						sqlDataReader.Close();
					}
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

		// Token: 0x06004668 RID: 18024 RVA: 0x000E7F34 File Offset: 0x000E6134
		public override PersonalizationStateInfoCollection FindState(PersonalizationScope scope, PersonalizationStateQuery query, int pageIndex, int pageSize, out int totalRecords)
		{
			PersonalizationProviderHelper.CheckPersonalizationScope(scope);
			PersonalizationProviderHelper.CheckPageIndexAndSize(pageIndex, pageSize);
			if (scope == PersonalizationScope.Shared)
			{
				string path = null;
				if (query != null)
				{
					path = StringUtil.CheckAndTrimString(query.PathToMatch, "query.PathToMatch", false, 256);
				}
				return this.FindSharedState(path, pageIndex, pageSize, out totalRecords);
			}
			string path2 = null;
			DateTime inactiveSinceDate = PersonalizationAdministration.DefaultInactiveSinceDate;
			string username = null;
			if (query != null)
			{
				path2 = StringUtil.CheckAndTrimString(query.PathToMatch, "query.PathToMatch", false, 256);
				inactiveSinceDate = query.UserInactiveSinceDate;
				username = StringUtil.CheckAndTrimString(query.UsernameToMatch, "query.UsernameToMatch", false, 256);
			}
			return this.FindUserState(path2, inactiveSinceDate, username, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x000E7FCC File Offset: 0x000E61CC
		private PersonalizationStateInfoCollection FindUserState(string path, DateTime inactiveSinceDate, string username, int pageIndex, int pageSize, out int totalRecords)
		{
			SqlConnectionHolder sqlConnectionHolder = null;
			SqlDataReader sqlDataReader = null;
			totalRecords = 0;
			PersonalizationStateInfoCollection result;
			try
			{
				try
				{
					sqlConnectionHolder = this.GetConnectionHolder();
					SqlConnection connection = sqlConnectionHolder.Connection;
					this.CheckSchemaVersion(connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_PersonalizationAdministration_FindState", connection);
					this.SetCommandTypeAndTimeout(sqlCommand);
					SqlParameterCollection parameters = sqlCommand.Parameters;
					SqlParameter sqlParameter = parameters.Add(new SqlParameter("AllUsersScope", SqlDbType.Bit));
					sqlParameter.Value = false;
					parameters.AddWithValue("ApplicationName", this.ApplicationName);
					parameters.AddWithValue("PageIndex", pageIndex);
					parameters.AddWithValue("PageSize", pageSize);
					SqlParameter sqlParameter2 = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter2.Direction = ParameterDirection.ReturnValue;
					parameters.Add(sqlParameter2);
					sqlParameter = parameters.Add("Path", SqlDbType.NVarChar);
					if (path != null)
					{
						sqlParameter.Value = path;
					}
					sqlParameter = parameters.Add("UserName", SqlDbType.NVarChar);
					if (username != null)
					{
						sqlParameter.Value = username;
					}
					sqlParameter = parameters.Add("InactiveSinceDate", SqlDbType.DateTime);
					if (inactiveSinceDate != PersonalizationAdministration.DefaultInactiveSinceDate)
					{
						sqlParameter.Value = inactiveSinceDate.ToUniversalTime();
					}
					sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess);
					PersonalizationStateInfoCollection personalizationStateInfoCollection = new PersonalizationStateInfoCollection();
					if (sqlDataReader != null)
					{
						if (sqlDataReader.HasRows)
						{
							while (sqlDataReader.Read())
							{
								string @string = sqlDataReader.GetString(0);
								DateTime lastUpdatedDate = DateTime.SpecifyKind(sqlDataReader.GetDateTime(1), DateTimeKind.Utc);
								int @int = sqlDataReader.GetInt32(2);
								string string2 = sqlDataReader.GetString(3);
								DateTime lastActivityDate = DateTime.SpecifyKind(sqlDataReader.GetDateTime(4), DateTimeKind.Utc);
								personalizationStateInfoCollection.Add(new UserPersonalizationStateInfo(@string, lastUpdatedDate, @int, string2, lastActivityDate));
							}
						}
						sqlDataReader.Close();
						sqlDataReader = null;
					}
					if (sqlParameter2.Value != null && sqlParameter2.Value is int)
					{
						totalRecords = (int)sqlParameter2.Value;
					}
					result = personalizationStateInfoCollection;
				}
				finally
				{
					if (sqlDataReader != null)
					{
						sqlDataReader.Close();
					}
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

		// Token: 0x0600466A RID: 18026 RVA: 0x000E81EC File Offset: 0x000E63EC
		private SqlConnectionHolder GetConnectionHolder()
		{
			SqlConnection sqlConnection = null;
			SqlConnectionHolder connection = SqlConnectionHelper.GetConnection(this._connectionString, true);
			if (connection != null)
			{
				sqlConnection = connection.Connection;
			}
			if (sqlConnection == null)
			{
				throw new ProviderException(SR.GetString("PersonalizationProvider_CantAccess", new object[]
				{
					this.Name
				}));
			}
			return connection;
		}

		// Token: 0x0600466B RID: 18027 RVA: 0x000E8238 File Offset: 0x000E6438
		private int GetCountOfSharedState(string path)
		{
			SqlConnectionHolder sqlConnectionHolder = null;
			int result = 0;
			try
			{
				try
				{
					sqlConnectionHolder = this.GetConnectionHolder();
					SqlConnection connection = sqlConnectionHolder.Connection;
					this.CheckSchemaVersion(connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_PersonalizationAdministration_GetCountOfState", connection);
					this.SetCommandTypeAndTimeout(sqlCommand);
					SqlParameterCollection parameters = sqlCommand.Parameters;
					SqlParameter sqlParameter = parameters.Add(new SqlParameter("Count", SqlDbType.Int));
					sqlParameter.Direction = ParameterDirection.Output;
					sqlParameter = parameters.Add(new SqlParameter("AllUsersScope", SqlDbType.Bit));
					sqlParameter.Value = true;
					parameters.AddWithValue("ApplicationName", this.ApplicationName);
					sqlParameter = parameters.Add("Path", SqlDbType.NVarChar);
					if (path != null)
					{
						sqlParameter.Value = path;
					}
					sqlParameter = parameters.Add("UserName", SqlDbType.NVarChar);
					sqlParameter = parameters.Add("InactiveSinceDate", SqlDbType.DateTime);
					sqlCommand.ExecuteNonQuery();
					sqlParameter = sqlCommand.Parameters[0];
					if (sqlParameter != null && sqlParameter.Value != null && sqlParameter.Value is int)
					{
						result = (int)sqlParameter.Value;
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

		// Token: 0x0600466C RID: 18028 RVA: 0x000E838C File Offset: 0x000E658C
		public override int GetCountOfState(PersonalizationScope scope, PersonalizationStateQuery query)
		{
			PersonalizationProviderHelper.CheckPersonalizationScope(scope);
			if (scope == PersonalizationScope.Shared)
			{
				string path = null;
				if (query != null)
				{
					path = StringUtil.CheckAndTrimString(query.PathToMatch, "query.PathToMatch", false, 256);
				}
				return this.GetCountOfSharedState(path);
			}
			string path2 = null;
			DateTime inactiveSinceDate = PersonalizationAdministration.DefaultInactiveSinceDate;
			string username = null;
			if (query != null)
			{
				path2 = StringUtil.CheckAndTrimString(query.PathToMatch, "query.PathToMatch", false, 256);
				inactiveSinceDate = query.UserInactiveSinceDate;
				username = StringUtil.CheckAndTrimString(query.UsernameToMatch, "query.UsernameToMatch", false, 256);
			}
			return this.GetCountOfUserState(path2, inactiveSinceDate, username);
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x000E8414 File Offset: 0x000E6614
		private int GetCountOfUserState(string path, DateTime inactiveSinceDate, string username)
		{
			SqlConnectionHolder sqlConnectionHolder = null;
			int result = 0;
			try
			{
				try
				{
					sqlConnectionHolder = this.GetConnectionHolder();
					SqlConnection connection = sqlConnectionHolder.Connection;
					this.CheckSchemaVersion(connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_PersonalizationAdministration_GetCountOfState", connection);
					this.SetCommandTypeAndTimeout(sqlCommand);
					SqlParameterCollection parameters = sqlCommand.Parameters;
					SqlParameter sqlParameter = parameters.Add(new SqlParameter("Count", SqlDbType.Int));
					sqlParameter.Direction = ParameterDirection.Output;
					sqlParameter = parameters.Add(new SqlParameter("AllUsersScope", SqlDbType.Bit));
					sqlParameter.Value = false;
					parameters.AddWithValue("ApplicationName", this.ApplicationName);
					sqlParameter = parameters.Add("Path", SqlDbType.NVarChar);
					if (path != null)
					{
						sqlParameter.Value = path;
					}
					sqlParameter = parameters.Add("UserName", SqlDbType.NVarChar);
					if (username != null)
					{
						sqlParameter.Value = username;
					}
					sqlParameter = parameters.Add("InactiveSinceDate", SqlDbType.DateTime);
					if (inactiveSinceDate != PersonalizationAdministration.DefaultInactiveSinceDate)
					{
						sqlParameter.Value = inactiveSinceDate.ToUniversalTime();
					}
					sqlCommand.ExecuteNonQuery();
					sqlParameter = sqlCommand.Parameters[0];
					if (sqlParameter != null && sqlParameter.Value != null && sqlParameter.Value is int)
					{
						result = (int)sqlParameter.Value;
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

		// Token: 0x0600466E RID: 18030 RVA: 0x000E8594 File Offset: 0x000E6794
		public override void Initialize(string name, NameValueCollection configSettings)
		{
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
			if (configSettings == null)
			{
				throw new ArgumentNullException("configSettings");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "SqlPersonalizationProvider";
			}
			if (string.IsNullOrEmpty(configSettings["description"]))
			{
				configSettings.Remove("description");
				configSettings.Add("description", SR.GetString("SqlPersonalizationProvider_Description"));
			}
			base.Initialize(name, configSettings);
			this._SchemaVersionCheck = 0;
			this._applicationName = configSettings["applicationName"];
			if (this._applicationName != null)
			{
				configSettings.Remove("applicationName");
				if (this._applicationName.Length > 256)
				{
					throw new ProviderException(SR.GetString("PersonalizationProvider_ApplicationNameExceedMaxLength", new object[]
					{
						256.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
			string text = configSettings["connectionStringName"];
			if (string.IsNullOrEmpty(text))
			{
				throw new ProviderException(SR.GetString("PersonalizationProvider_NoConnection"));
			}
			configSettings.Remove("connectionStringName");
			string connectionString = SqlConnectionHelper.GetConnectionString(text, true, true);
			if (string.IsNullOrEmpty(connectionString))
			{
				throw new ProviderException(SR.GetString("PersonalizationProvider_BadConnection", new object[]
				{
					text
				}));
			}
			this._connectionString = connectionString;
			this._commandTimeout = SecUtility.GetIntValue(configSettings, "commandTimeout", -1, true, 0);
			configSettings.Remove("commandTimeout");
			if (configSettings.Count > 0)
			{
				string key = configSettings.GetKey(0);
				throw new ProviderException(SR.GetString("PersonalizationProvider_UnknownProp", new object[]
				{
					key,
					name
				}));
			}
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x000E8720 File Offset: 0x000E6920
		private void CheckSchemaVersion(SqlConnection connection)
		{
			string[] features = new string[]
			{
				"Personalization"
			};
			string version = "1";
			SecUtility.CheckSchemaVersion(this, connection, features, version, ref this._SchemaVersionCheck);
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x000E8754 File Offset: 0x000E6954
		private byte[] LoadPersonalizationBlob(SqlConnection connection, string path, string userName)
		{
			SqlCommand sqlCommand;
			if (userName != null)
			{
				sqlCommand = new SqlCommand("dbo.aspnet_PersonalizationPerUser_GetPageSettings", connection);
			}
			else
			{
				sqlCommand = new SqlCommand("dbo.aspnet_PersonalizationAllUsers_GetPageSettings", connection);
			}
			this.SetCommandTypeAndTimeout(sqlCommand);
			sqlCommand.Parameters.Add(this.CreateParameter("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
			sqlCommand.Parameters.Add(this.CreateParameter("@Path", SqlDbType.NVarChar, path));
			if (userName != null)
			{
				sqlCommand.Parameters.Add(this.CreateParameter("@UserName", SqlDbType.NVarChar, userName));
				sqlCommand.Parameters.Add(this.CreateParameter("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));
			}
			SqlDataReader sqlDataReader = null;
			try
			{
				sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow);
				if (sqlDataReader.Read())
				{
					int num = (int)sqlDataReader.GetBytes(0, 0L, null, 0, 0);
					byte[] array = new byte[num];
					sqlDataReader.GetBytes(0, 0L, array, 0, num);
					return array;
				}
			}
			finally
			{
				if (sqlDataReader != null)
				{
					sqlDataReader.Close();
				}
			}
			return null;
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x000E8858 File Offset: 0x000E6A58
		protected override void LoadPersonalizationBlobs(WebPartManager webPartManager, string path, string userName, ref byte[] sharedDataBlob, ref byte[] userDataBlob)
		{
			sharedDataBlob = null;
			userDataBlob = null;
			SqlConnectionHolder sqlConnectionHolder = null;
			try
			{
				try
				{
					sqlConnectionHolder = this.GetConnectionHolder();
					SqlConnection connection = sqlConnectionHolder.Connection;
					this.CheckSchemaVersion(connection);
					sharedDataBlob = this.LoadPersonalizationBlob(connection, path, null);
					if (!string.IsNullOrEmpty(userName))
					{
						userDataBlob = this.LoadPersonalizationBlob(connection, path, userName);
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

		// Token: 0x06004672 RID: 18034 RVA: 0x000E88D8 File Offset: 0x000E6AD8
		private void ResetPersonalizationState(SqlConnection connection, string path, string userName)
		{
			SqlCommand sqlCommand;
			if (userName != null)
			{
				sqlCommand = new SqlCommand("dbo.aspnet_PersonalizationPerUser_ResetPageSettings", connection);
			}
			else
			{
				sqlCommand = new SqlCommand("dbo.aspnet_PersonalizationAllUsers_ResetPageSettings", connection);
			}
			this.SetCommandTypeAndTimeout(sqlCommand);
			sqlCommand.Parameters.Add(this.CreateParameter("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
			sqlCommand.Parameters.Add(this.CreateParameter("@Path", SqlDbType.NVarChar, path));
			if (userName != null)
			{
				sqlCommand.Parameters.Add(this.CreateParameter("@UserName", SqlDbType.NVarChar, userName));
				sqlCommand.Parameters.Add(this.CreateParameter("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));
			}
			sqlCommand.ExecuteNonQuery();
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x000E8988 File Offset: 0x000E6B88
		protected override void ResetPersonalizationBlob(WebPartManager webPartManager, string path, string userName)
		{
			SqlConnectionHolder sqlConnectionHolder = null;
			try
			{
				try
				{
					sqlConnectionHolder = this.GetConnectionHolder();
					SqlConnection connection = sqlConnectionHolder.Connection;
					this.CheckSchemaVersion(connection);
					this.ResetPersonalizationState(connection, path, userName);
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

		// Token: 0x06004674 RID: 18036 RVA: 0x000E89E8 File Offset: 0x000E6BE8
		private int ResetAllState(PersonalizationScope scope)
		{
			SqlConnectionHolder sqlConnectionHolder = null;
			int result = 0;
			try
			{
				try
				{
					sqlConnectionHolder = this.GetConnectionHolder();
					SqlConnection connection = sqlConnectionHolder.Connection;
					this.CheckSchemaVersion(connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_PersonalizationAdministration_DeleteAllState", connection);
					this.SetCommandTypeAndTimeout(sqlCommand);
					SqlParameterCollection parameters = sqlCommand.Parameters;
					SqlParameter sqlParameter = parameters.Add(new SqlParameter("AllUsersScope", SqlDbType.Bit));
					sqlParameter.Value = (scope == PersonalizationScope.Shared);
					parameters.AddWithValue("ApplicationName", this.ApplicationName);
					sqlParameter = parameters.Add(new SqlParameter("Count", SqlDbType.Int));
					sqlParameter.Direction = ParameterDirection.Output;
					sqlCommand.ExecuteNonQuery();
					sqlParameter = sqlCommand.Parameters[2];
					if (sqlParameter != null && sqlParameter.Value != null && sqlParameter.Value is int)
					{
						result = (int)sqlParameter.Value;
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

		// Token: 0x06004675 RID: 18037 RVA: 0x000E8AEC File Offset: 0x000E6CEC
		private int ResetSharedState(string[] paths)
		{
			int num = 0;
			if (paths == null)
			{
				num = this.ResetAllState(PersonalizationScope.Shared);
			}
			else
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				SqlConnection connection = null;
				try
				{
					bool flag = false;
					try
					{
						sqlConnectionHolder = this.GetConnectionHolder();
						connection = sqlConnectionHolder.Connection;
						this.CheckSchemaVersion(connection);
						SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_PersonalizationAdministration_ResetSharedState", connection);
						this.SetCommandTypeAndTimeout(sqlCommand);
						SqlParameterCollection parameters = sqlCommand.Parameters;
						SqlParameter sqlParameter = parameters.Add(new SqlParameter("Count", SqlDbType.Int));
						sqlParameter.Direction = ParameterDirection.Output;
						parameters.AddWithValue("ApplicationName", this.ApplicationName);
						sqlParameter = parameters.Add("Path", SqlDbType.NVarChar);
						foreach (string value in paths)
						{
							if (!flag && paths.Length > 1)
							{
								new SqlCommand("BEGIN TRANSACTION", connection).ExecuteNonQuery();
								flag = true;
							}
							sqlParameter.Value = value;
							sqlCommand.ExecuteNonQuery();
							SqlParameter sqlParameter2 = sqlCommand.Parameters[0];
							if (sqlParameter2 != null && sqlParameter2.Value != null && sqlParameter2.Value is int)
							{
								num += (int)sqlParameter2.Value;
							}
						}
						if (flag)
						{
							new SqlCommand("COMMIT TRANSACTION", connection).ExecuteNonQuery();
							flag = false;
						}
					}
					catch
					{
						if (flag)
						{
							new SqlCommand("ROLLBACK TRANSACTION", connection).ExecuteNonQuery();
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
			return num;
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x000E8C98 File Offset: 0x000E6E98
		public override int ResetUserState(string path, DateTime userInactiveSinceDate)
		{
			path = StringUtil.CheckAndTrimString(path, "path", false, 256);
			string[] array;
			if (path != null)
			{
				(array = new string[1])[0] = path;
			}
			else
			{
				array = null;
			}
			string[] paths = array;
			return this.ResetUserState(SqlPersonalizationProvider.ResetUserStateMode.PerInactiveDate, userInactiveSinceDate, paths, null);
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x000E8CD4 File Offset: 0x000E6ED4
		public override int ResetState(PersonalizationScope scope, string[] paths, string[] usernames)
		{
			PersonalizationProviderHelper.CheckPersonalizationScope(scope);
			paths = PersonalizationProviderHelper.CheckAndTrimNonEmptyStringEntries(paths, "paths", false, false, 256);
			usernames = PersonalizationProviderHelper.CheckAndTrimNonEmptyStringEntries(usernames, "usernames", false, true, 256);
			if (scope == PersonalizationScope.Shared)
			{
				PersonalizationProviderHelper.CheckUsernamesInSharedScope(usernames);
				return this.ResetSharedState(paths);
			}
			PersonalizationProviderHelper.CheckOnlyOnePathWithUsers(paths, usernames);
			return this.ResetUserState(paths, usernames);
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x000E8D30 File Offset: 0x000E6F30
		private int ResetUserState(string[] paths, string[] usernames)
		{
			bool flag = paths != null && paths.Length != 0;
			bool flag2 = usernames != null && usernames.Length != 0;
			int result;
			if (!flag && !flag2)
			{
				result = this.ResetAllState(PersonalizationScope.User);
			}
			else if (!flag2)
			{
				result = this.ResetUserState(SqlPersonalizationProvider.ResetUserStateMode.PerPaths, PersonalizationAdministration.DefaultInactiveSinceDate, paths, usernames);
			}
			else
			{
				result = this.ResetUserState(SqlPersonalizationProvider.ResetUserStateMode.PerUsers, PersonalizationAdministration.DefaultInactiveSinceDate, paths, usernames);
			}
			return result;
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x000E8D8C File Offset: 0x000E6F8C
		private int ResetUserState(SqlPersonalizationProvider.ResetUserStateMode mode, DateTime userInactiveSinceDate, string[] paths, string[] usernames)
		{
			SqlConnectionHolder sqlConnectionHolder = null;
			SqlConnection connection = null;
			int num = 0;
			try
			{
				bool flag = false;
				try
				{
					sqlConnectionHolder = this.GetConnectionHolder();
					connection = sqlConnectionHolder.Connection;
					this.CheckSchemaVersion(connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_PersonalizationAdministration_ResetUserState", connection);
					this.SetCommandTypeAndTimeout(sqlCommand);
					SqlParameterCollection parameters = sqlCommand.Parameters;
					SqlParameter sqlParameter = parameters.Add(new SqlParameter("Count", SqlDbType.Int));
					sqlParameter.Direction = ParameterDirection.Output;
					parameters.AddWithValue("ApplicationName", this.ApplicationName);
					string text = (paths != null && paths.Length != 0) ? paths[0] : null;
					if (mode == SqlPersonalizationProvider.ResetUserStateMode.PerInactiveDate)
					{
						if (userInactiveSinceDate != PersonalizationAdministration.DefaultInactiveSinceDate)
						{
							sqlParameter = parameters.Add("InactiveSinceDate", SqlDbType.DateTime);
							sqlParameter.Value = userInactiveSinceDate.ToUniversalTime();
						}
						if (text != null)
						{
							parameters.AddWithValue("Path", text);
						}
						sqlCommand.ExecuteNonQuery();
						SqlParameter sqlParameter2 = sqlCommand.Parameters[0];
						if (sqlParameter2 != null && sqlParameter2.Value != null && sqlParameter2.Value is int)
						{
							num = (int)sqlParameter2.Value;
						}
					}
					else if (mode == SqlPersonalizationProvider.ResetUserStateMode.PerPaths)
					{
						sqlParameter = parameters.Add("Path", SqlDbType.NVarChar);
						foreach (string value in paths)
						{
							if (!flag && paths.Length > 1)
							{
								new SqlCommand("BEGIN TRANSACTION", connection).ExecuteNonQuery();
								flag = true;
							}
							sqlParameter.Value = value;
							sqlCommand.ExecuteNonQuery();
							SqlParameter sqlParameter3 = sqlCommand.Parameters[0];
							if (sqlParameter3 != null && sqlParameter3.Value != null && sqlParameter3.Value is int)
							{
								num += (int)sqlParameter3.Value;
							}
						}
					}
					else
					{
						if (text != null)
						{
							parameters.AddWithValue("Path", text);
						}
						sqlParameter = parameters.Add("UserName", SqlDbType.NVarChar);
						foreach (string value2 in usernames)
						{
							if (!flag && usernames.Length > 1)
							{
								new SqlCommand("BEGIN TRANSACTION", connection).ExecuteNonQuery();
								flag = true;
							}
							sqlParameter.Value = value2;
							sqlCommand.ExecuteNonQuery();
							SqlParameter sqlParameter4 = sqlCommand.Parameters[0];
							if (sqlParameter4 != null && sqlParameter4.Value != null && sqlParameter4.Value is int)
							{
								num += (int)sqlParameter4.Value;
							}
						}
					}
					if (flag)
					{
						new SqlCommand("COMMIT TRANSACTION", connection).ExecuteNonQuery();
						flag = false;
					}
				}
				catch
				{
					if (flag)
					{
						new SqlCommand("ROLLBACK TRANSACTION", connection).ExecuteNonQuery();
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
			return num;
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x000E9080 File Offset: 0x000E7280
		private void SavePersonalizationState(SqlConnection connection, string path, string userName, byte[] state)
		{
			SqlCommand sqlCommand;
			if (userName != null)
			{
				sqlCommand = new SqlCommand("dbo.aspnet_PersonalizationPerUser_SetPageSettings", connection);
			}
			else
			{
				sqlCommand = new SqlCommand("dbo.aspnet_PersonalizationAllUsers_SetPageSettings", connection);
			}
			this.SetCommandTypeAndTimeout(sqlCommand);
			sqlCommand.Parameters.Add(this.CreateParameter("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
			sqlCommand.Parameters.Add(this.CreateParameter("@Path", SqlDbType.NVarChar, path));
			sqlCommand.Parameters.Add(this.CreateParameter("@PageSettings", SqlDbType.Image, state));
			sqlCommand.Parameters.Add(this.CreateParameter("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));
			if (userName != null)
			{
				sqlCommand.Parameters.Add(this.CreateParameter("@UserName", SqlDbType.NVarChar, userName));
			}
			sqlCommand.ExecuteNonQuery();
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x000E914C File Offset: 0x000E734C
		protected override void SavePersonalizationBlob(WebPartManager webPartManager, string path, string userName, byte[] dataBlob)
		{
			SqlConnectionHolder sqlConnectionHolder = null;
			SqlConnection connection = null;
			try
			{
				try
				{
					sqlConnectionHolder = this.GetConnectionHolder();
					connection = sqlConnectionHolder.Connection;
					this.CheckSchemaVersion(connection);
					this.SavePersonalizationState(connection, path, userName, dataBlob);
				}
				catch (SqlException ex)
				{
					if (userName == null || (ex.Number != 2627 && ex.Number != 2601 && ex.Number != 2512))
					{
						throw;
					}
					this.SavePersonalizationState(connection, path, userName, dataBlob);
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

		// Token: 0x0600467C RID: 18044 RVA: 0x000E91F8 File Offset: 0x000E73F8
		private void SetCommandTypeAndTimeout(SqlCommand command)
		{
			command.CommandType = CommandType.StoredProcedure;
			if (this._commandTimeout != -1)
			{
				command.CommandTimeout = this._commandTimeout;
			}
		}

		// Token: 0x040026A0 RID: 9888
		private const int maxStringLength = 256;

		// Token: 0x040026A1 RID: 9889
		private string _applicationName;

		// Token: 0x040026A2 RID: 9890
		private int _commandTimeout;

		// Token: 0x040026A3 RID: 9891
		private string _connectionString;

		// Token: 0x040026A4 RID: 9892
		private int _SchemaVersionCheck;

		// Token: 0x020009F3 RID: 2547
		private enum ResetUserStateMode
		{
			// Token: 0x04003A2B RID: 14891
			PerInactiveDate,
			// Token: 0x04003A2C RID: 14892
			PerPaths,
			// Token: 0x04003A2D RID: 14893
			PerUsers
		}
	}
}
