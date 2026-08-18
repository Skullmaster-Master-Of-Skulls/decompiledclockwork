using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Web.DataAccess;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Profile
{
	// Token: 0x0200016B RID: 363
	public class SqlProfileProvider : ProfileProvider
	{
		// Token: 0x06001445 RID: 5189 RVA: 0x0003B544 File Offset: 0x00039744
		public override void Initialize(string name, NameValueCollection config)
		{
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (name == null || name.Length < 1)
			{
				name = "SqlProfileProvider";
			}
			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", SR.GetString("ProfileSqlProvider_description"));
			}
			base.Initialize(name, config);
			this._SchemaVersionCheck = 0;
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
			this._CommandTimeout = SecUtility.GetIntValue(config, "commandTimeout", 30, true, 0);
			config.Remove("commandTimeout");
			config.Remove("connectionStringName");
			config.Remove("connectionString");
			config.Remove("applicationName");
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

		// Token: 0x06001446 RID: 5190 RVA: 0x0003B690 File Offset: 0x00039890
		private void CheckSchemaVersion(SqlConnection connection)
		{
			string[] features = new string[]
			{
				"Profile"
			};
			string version = "1";
			SecUtility.CheckSchemaVersion(this, connection, features, version, ref this._SchemaVersionCheck);
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0003B6C1 File Offset: 0x000398C1
		// (set) Token: 0x06001448 RID: 5192 RVA: 0x0003B6C9 File Offset: 0x000398C9
		public override string ApplicationName
		{
			get
			{
				return this._AppName;
			}
			set
			{
				if (value.Length > 256)
				{
					throw new ProviderException(SR.GetString("Provider_application_name_too_long"));
				}
				this._AppName = value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0003B6EF File Offset: 0x000398EF
		private int CommandTimeout
		{
			get
			{
				return this._CommandTimeout;
			}
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0003B6F8 File Offset: 0x000398F8
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext sc, SettingsPropertyCollection properties)
		{
			SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
			if (properties.Count < 1)
			{
				return settingsPropertyValueCollection;
			}
			string text = (string)sc["UserName"];
			foreach (object obj in properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (settingsProperty.SerializeAs == SettingsSerializeAs.ProviderSpecific)
				{
					if (settingsProperty.PropertyType.IsPrimitive || settingsProperty.PropertyType == typeof(string))
					{
						settingsProperty.SerializeAs = SettingsSerializeAs.String;
					}
					else
					{
						settingsProperty.SerializeAs = SettingsSerializeAs.Xml;
					}
				}
				settingsPropertyValueCollection.Add(new SettingsPropertyValue(settingsProperty));
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.GetPropertyValuesFromDatabase(text, settingsPropertyValueCollection);
			}
			return settingsPropertyValueCollection;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0003B7C4 File Offset: 0x000399C4
		private void GetPropertyValuesFromDatabase(string userName, SettingsPropertyValueCollection svc)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null && HostingEnvironment.IsHosted && EtwTrace.IsTraceEnabled(4, 8))
			{
				EtwTrace.Trace(EtwTraceType.ETW_TYPE_PROFILE_BEGIN, HttpContext.Current.WorkerRequest);
			}
			string[] names = null;
			string values = null;
			byte[] array = null;
			if (httpContext != null)
			{
				string text = httpContext.Request.IsAuthenticated ? httpContext.User.Identity.Name : httpContext.Request.AnonymousID;
			}
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				SqlDataReader sqlDataReader = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					sqlDataReader = new SqlCommand("dbo.aspnet_Profile_GetProperties", sqlConnectionHolder.Connection)
					{
						CommandTimeout = this.CommandTimeout,
						CommandType = CommandType.StoredProcedure,
						Parameters = 
						{
							this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName),
							this.CreateInputParam("@UserName", SqlDbType.NVarChar, userName),
							this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow)
						}
					}.ExecuteReader(CommandBehavior.SingleRow);
					if (sqlDataReader.Read())
					{
						names = sqlDataReader.GetString(0).Split(new char[]
						{
							':'
						});
						values = sqlDataReader.GetString(1);
						int num = (int)sqlDataReader.GetBytes(2, 0L, null, 0, 0);
						array = new byte[num];
						sqlDataReader.GetBytes(2, 0L, array, 0, num);
					}
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
					if (sqlDataReader != null)
					{
						sqlDataReader.Close();
					}
				}
				ProfileModule.ParseDataFromDB(names, values, array, svc);
				if (httpContext != null && HostingEnvironment.IsHosted && EtwTrace.IsTraceEnabled(4, 8))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_PROFILE_END, HttpContext.Current.WorkerRequest, userName);
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0003B9BC File Offset: 0x00039BBC
		public override void SetPropertyValues(SettingsContext sc, SettingsPropertyValueCollection properties)
		{
			string text = (string)sc["UserName"];
			bool flag = (bool)sc["IsAuthenticated"];
			if (text == null || text.Length < 1 || properties.Count < 1)
			{
				return;
			}
			string empty = string.Empty;
			string empty2 = string.Empty;
			byte[] objValue = null;
			ProfileModule.PrepareDataForSaving(ref empty, ref empty2, ref objValue, true, properties, flag);
			if (empty.Length == 0)
			{
				return;
			}
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					new SqlCommand("dbo.aspnet_Profile_SetProperties", sqlConnectionHolder.Connection)
					{
						CommandTimeout = this.CommandTimeout,
						CommandType = CommandType.StoredProcedure,
						Parameters = 
						{
							this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName),
							this.CreateInputParam("@UserName", SqlDbType.NVarChar, text),
							this.CreateInputParam("@PropertyNames", SqlDbType.NText, empty),
							this.CreateInputParam("@PropertyValuesString", SqlDbType.NText, empty2),
							this.CreateInputParam("@PropertyValuesBinary", SqlDbType.Image, objValue),
							this.CreateInputParam("@IsUserAnonymous", SqlDbType.Bit, !flag),
							this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow)
						}
					}.ExecuteNonQuery();
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

		// Token: 0x0600144D RID: 5197 RVA: 0x0003BB94 File Offset: 0x00039D94
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

		// Token: 0x0600144E RID: 5198 RVA: 0x0003BBBC File Offset: 0x00039DBC
		public override int DeleteProfiles(ProfileInfoCollection profiles)
		{
			if (profiles == null)
			{
				throw new ArgumentNullException("profiles");
			}
			if (profiles.Count < 1)
			{
				throw new ArgumentException(SR.GetString("Parameter_collection_empty", new object[]
				{
					"profiles"
				}), "profiles");
			}
			string[] array = new string[profiles.Count];
			int num = 0;
			foreach (object obj in profiles)
			{
				ProfileInfo profileInfo = (ProfileInfo)obj;
				array[num++] = profileInfo.UserName;
			}
			return this.DeleteProfiles(array);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0003BC68 File Offset: 0x00039E68
		public override int DeleteProfiles(string[] usernames)
		{
			SecUtility.CheckArrayParameter(ref usernames, true, true, true, 256, "usernames");
			int num = 0;
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
						int num2 = usernames.Length - i;
						while (num2 < usernames.Length && text.Length + usernames[num2].Length + 1 < 4000)
						{
							text = text + "," + usernames[num2];
							i--;
							num2++;
						}
						if (!flag && i > 0)
						{
							SqlCommand sqlCommand = new SqlCommand("BEGIN TRANSACTION", sqlConnectionHolder.Connection);
							sqlCommand.ExecuteNonQuery();
							flag = true;
						}
						object obj = new SqlCommand("dbo.aspnet_Profile_DeleteProfiles", sqlConnectionHolder.Connection)
						{
							CommandTimeout = this.CommandTimeout,
							CommandType = CommandType.StoredProcedure,
							Parameters = 
							{
								this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName),
								this.CreateInputParam("@UserNames", SqlDbType.NVarChar, text)
							}
						}.ExecuteScalar();
						if (obj != null && obj is int)
						{
							num += (int)obj;
						}
					}
					if (flag)
					{
						SqlCommand sqlCommand = new SqlCommand("COMMIT TRANSACTION", sqlConnectionHolder.Connection);
						sqlCommand.ExecuteNonQuery();
						flag = false;
					}
				}
				catch
				{
					if (flag)
					{
						SqlCommand sqlCommand2 = new SqlCommand("ROLLBACK TRANSACTION", sqlConnectionHolder.Connection);
						sqlCommand2.ExecuteNonQuery();
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

		// Token: 0x06001450 RID: 5200 RVA: 0x0003BE54 File Offset: 0x0003A054
		public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			int result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					object obj = new SqlCommand("dbo.aspnet_Profile_DeleteInactiveProfiles", sqlConnectionHolder.Connection)
					{
						CommandTimeout = this.CommandTimeout,
						CommandType = CommandType.StoredProcedure,
						Parameters = 
						{
							this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName),
							this.CreateInputParam("@ProfileAuthOptions", SqlDbType.Int, (int)authenticationOption),
							this.CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime())
						}
					}.ExecuteScalar();
					if (obj == null || !(obj is int))
					{
						result = 0;
					}
					else
					{
						result = (int)obj;
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

		// Token: 0x06001451 RID: 5201 RVA: 0x0003BF4C File Offset: 0x0003A14C
		public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			int result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					object obj = new SqlCommand("dbo.aspnet_Profile_GetNumberOfInactiveProfiles", sqlConnectionHolder.Connection)
					{
						CommandTimeout = this.CommandTimeout,
						CommandType = CommandType.StoredProcedure,
						Parameters = 
						{
							this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName),
							this.CreateInputParam("@ProfileAuthOptions", SqlDbType.Int, (int)authenticationOption),
							this.CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime())
						}
					}.ExecuteScalar();
					if (obj == null || !(obj is int))
					{
						result = 0;
					}
					else
					{
						result = (int)obj;
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

		// Token: 0x06001452 RID: 5202 RVA: 0x0003C044 File Offset: 0x0003A244
		public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
		{
			return this.GetProfilesForQuery(new SqlParameter[0], authenticationOption, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0003C058 File Offset: 0x0003A258
		public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			return this.GetProfilesForQuery(new SqlParameter[]
			{
				this.CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime())
			}, authenticationOption, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0003C094 File Offset: 0x0003A294
		public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 256, "username");
			return this.GetProfilesForQuery(new SqlParameter[]
			{
				this.CreateInputParam("@UserNameToMatch", SqlDbType.NVarChar, usernameToMatch)
			}, authenticationOption, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0003C0DC File Offset: 0x0003A2DC
		public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 256, "username");
			return this.GetProfilesForQuery(new SqlParameter[]
			{
				this.CreateInputParam("@UserNameToMatch", SqlDbType.NVarChar, usernameToMatch),
				this.CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime())
			}, authenticationOption, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0003C140 File Offset: 0x0003A340
		private ProfileInfoCollection GetProfilesForQuery(SqlParameter[] args, ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
		{
			if (pageIndex < 0)
			{
				throw new ArgumentException(SR.GetString("PageIndex_bad"), "pageIndex");
			}
			if (pageSize < 1)
			{
				throw new ArgumentException(SR.GetString("PageSize_bad"), "pageSize");
			}
			long num = (long)pageIndex * (long)pageSize + (long)pageSize - 1L;
			if (num > 2147483647L)
			{
				throw new ArgumentException(SR.GetString("PageIndex_PageSize_bad"), "pageIndex and pageSize");
			}
			ProfileInfoCollection result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				SqlDataReader sqlDataReader = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Profile_GetProfiles", sqlConnectionHolder.Connection);
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@ProfileAuthOptions", SqlDbType.Int, (int)authenticationOption));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PageIndex", SqlDbType.Int, pageIndex));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PageSize", SqlDbType.Int, pageSize));
					foreach (SqlParameter value in args)
					{
						sqlCommand.Parameters.Add(value);
					}
					sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess);
					ProfileInfoCollection profileInfoCollection = new ProfileInfoCollection();
					while (sqlDataReader.Read())
					{
						string @string = sqlDataReader.GetString(0);
						bool boolean = sqlDataReader.GetBoolean(1);
						DateTime lastActivityDate = DateTime.SpecifyKind(sqlDataReader.GetDateTime(2), DateTimeKind.Utc);
						DateTime lastUpdatedDate = DateTime.SpecifyKind(sqlDataReader.GetDateTime(3), DateTimeKind.Utc);
						int @int = sqlDataReader.GetInt32(4);
						profileInfoCollection.Add(new ProfileInfo(@string, boolean, lastActivityDate, lastUpdatedDate, @int));
					}
					totalRecords = profileInfoCollection.Count;
					if (sqlDataReader.NextResult() && sqlDataReader.Read())
					{
						totalRecords = sqlDataReader.GetInt32(0);
					}
					result = profileInfoCollection;
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

		// Token: 0x04001526 RID: 5414
		private string _AppName;

		// Token: 0x04001527 RID: 5415
		private string _sqlConnectionString;

		// Token: 0x04001528 RID: 5416
		private int _SchemaVersionCheck;

		// Token: 0x04001529 RID: 5417
		private int _CommandTimeout;
	}
}
