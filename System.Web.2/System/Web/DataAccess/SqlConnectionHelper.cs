using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Management;

namespace System.Web.DataAccess
{
	// Token: 0x020001AF RID: 431
	internal static class SqlConnectionHelper
	{
		// Token: 0x0600165D RID: 5725 RVA: 0x00046CA8 File Offset: 0x00044EA8
		internal static void EnsureNoUserInstance(string connectionString)
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
			if (sqlConnectionStringBuilder.UserInstance)
			{
				throw new ProviderException(SR.GetString("LocalDB_cannot_have_userinstance_flag"));
			}
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00046CD4 File Offset: 0x00044ED4
		internal static SqlConnectionHolder GetConnection(string connectionString, bool revertImpersonation)
		{
			string text = connectionString.ToUpperInvariant();
			if (text.Contains("|DATADIRECTORY|"))
			{
				SqlConnectionHelper.EnsureDBFile(connectionString);
			}
			if (text.Contains("(LOCALDB)"))
			{
				SqlConnectionHelper.EnsureNoUserInstance(connectionString);
			}
			SqlConnectionHolder sqlConnectionHolder = new SqlConnectionHolder(connectionString);
			bool flag = true;
			try
			{
				try
				{
					sqlConnectionHolder.Open(null, revertImpersonation);
					flag = false;
				}
				finally
				{
					if (flag)
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
			return sqlConnectionHolder;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00046D54 File Offset: 0x00044F54
		internal static string GetConnectionString(string specifiedConnectionString, bool lookupConnectionString, bool appLevel)
		{
			if (specifiedConnectionString == null || specifiedConnectionString.Length < 1)
			{
				return null;
			}
			string text = null;
			if (lookupConnectionString)
			{
				RuntimeConfig runtimeConfig = appLevel ? RuntimeConfig.GetAppConfig() : RuntimeConfig.GetConfig();
				ConnectionStringSettings connectionStringSettings = runtimeConfig.ConnectionStrings.ConnectionStrings[specifiedConnectionString];
				if (connectionStringSettings != null)
				{
					text = connectionStringSettings.ConnectionString;
				}
				if (text == null)
				{
					return null;
				}
			}
			else
			{
				text = specifiedConnectionString;
			}
			return text;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00046DA8 File Offset: 0x00044FA8
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static string GetDataDirectory()
		{
			if (HostingEnvironment.IsHosted)
			{
				return Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data");
			}
			string text = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
			if (string.IsNullOrEmpty(text))
			{
				string text2 = null;
				Process currentProcess = Process.GetCurrentProcess();
				ProcessModule processModule = (currentProcess != null) ? currentProcess.MainModule : null;
				string text3 = (processModule != null) ? processModule.FileName : null;
				if (!string.IsNullOrEmpty(text3))
				{
					text2 = Path.GetDirectoryName(text3);
				}
				if (string.IsNullOrEmpty(text2))
				{
					text2 = Environment.CurrentDirectory;
				}
				text = Path.Combine(text2, "App_Data");
				AppDomain.CurrentDomain.SetData("DataDirectory", text, new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text));
			}
			return text;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00046E50 File Offset: 0x00045050
		private static void EnsureDBFile(string connectionString)
		{
			string text = null;
			string dataDirectory = SqlConnectionHelper.GetDataDirectory();
			bool flag = true;
			bool flag2 = true;
			string[] array = connectionString.Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries);
			bool flag3 = !connectionString.ToUpperInvariant().Contains("(LOCALDB)");
			bool flag4 = true;
			foreach (string text2 in array)
			{
				string text3 = text2.ToUpper(CultureInfo.InvariantCulture).Trim();
				if (flag && text3.Contains("|DATADIRECTORY|"))
				{
					flag = false;
					connectionString = connectionString.Replace(text2, "Pooling=false");
					int startIndex = text3.IndexOf("|DATADIRECTORY|", StringComparison.Ordinal) + "|DATADIRECTORY|".Length;
					string text4 = text3.Substring(startIndex).Trim();
					while (text4.StartsWith("\\", StringComparison.Ordinal))
					{
						text4 = text4.Substring(1);
					}
					if (!text4.Contains(".."))
					{
						text = Path.Combine(dataDirectory, text4);
					}
					if (!flag2)
					{
						break;
					}
				}
				else if (flag2 && (text3.StartsWith("INITIAL CATALOG", StringComparison.Ordinal) || text3.StartsWith("DATABASE", StringComparison.Ordinal)))
				{
					flag2 = false;
					connectionString = connectionString.Replace(text2, "Database=master");
					if (!flag)
					{
						break;
					}
				}
				else if (flag3 && text3.StartsWith("USER INSTANCE", StringComparison.Ordinal))
				{
					flag3 = false;
					int num = text3.IndexOf('=');
					if (num < 0)
					{
						return;
					}
					string a = text3.Substring(num + 1).Trim();
					if (a != "TRUE")
					{
						return;
					}
				}
				else if (flag4 && text3.StartsWith("CONNECT TIMEOUT", StringComparison.Ordinal))
				{
					flag4 = false;
				}
			}
			if (flag3)
			{
				return;
			}
			if (text == null)
			{
				throw new ProviderException(SR.GetString("SqlExpress_file_not_found_in_connection_string"));
			}
			if (File.Exists(text))
			{
				return;
			}
			if (!HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.High))
			{
				throw new ProviderException(SR.GetString("Provider_can_not_create_file_in_this_trust_level"));
			}
			if (!connectionString.Contains("Database=master"))
			{
				connectionString += ";Database=master";
			}
			if (flag4)
			{
				connectionString += ";Connect Timeout=45";
			}
			using (new ApplicationImpersonationContext())
			{
				object obj = SqlConnectionHelper.s_lock;
				lock (obj)
				{
					if (!File.Exists(text))
					{
						SqlConnectionHelper.CreateMdfFile(text, dataDirectory, connectionString);
					}
				}
			}
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x000470BC File Offset: 0x000452BC
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private static void CreateMdfFile(string fullFileName, string dataDir, string connectionString)
		{
			bool flag = false;
			HttpContext httpContext = HttpContext.Current;
			string text = null;
			try
			{
				if (!Directory.Exists(dataDir))
				{
					flag = true;
					Directory.CreateDirectory(dataDir);
					flag = false;
					try
					{
						if (httpContext != null)
						{
							HttpRuntime.RestrictIISFolders(httpContext);
						}
					}
					catch
					{
					}
				}
				fullFileName = fullFileName.ToUpper(CultureInfo.InvariantCulture);
				char[] array = Path.GetFileNameWithoutExtension(fullFileName).ToCharArray();
				for (int i = 0; i < array.Length; i++)
				{
					if (!char.IsLetterOrDigit(array[i]))
					{
						array[i] = '_';
					}
				}
				string text2 = new string(array);
				string text3;
				if (text2.Length > 30)
				{
					text3 = text2.Substring(0, 30) + "_" + Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
				}
				else
				{
					text3 = text2 + "_" + Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
				}
				text = Path.Combine(Path.GetDirectoryName(fullFileName), text2 + "_TMP.MDF");
				SqlServices.Install(text3, text, connectionString);
				SqlConnectionHelper.DetachDB(text3, connectionString);
				try
				{
					File.Move(text, fullFileName);
				}
				catch
				{
					if (!File.Exists(fullFileName))
					{
						File.Copy(text, fullFileName);
						try
						{
							File.Delete(text);
						}
						catch
						{
						}
					}
				}
				try
				{
					File.Delete(text.Replace("_TMP.MDF", "_TMP_log.LDF"));
				}
				catch
				{
				}
			}
			catch (Exception ex)
			{
				if (httpContext == null || httpContext.IsCustomErrorEnabled)
				{
					throw;
				}
				HttpException ex2 = new HttpException(ex.Message, ex);
				if (ex is UnauthorizedAccessException)
				{
					ex2.SetFormatter(new SqlExpressConnectionErrorFormatter(flag ? DataConnectionErrorEnum.CanNotCreateDataDir : DataConnectionErrorEnum.CanNotWriteToDataDir));
				}
				else
				{
					ex2.SetFormatter(new SqlExpressDBFileAutoCreationErrorFormatter(ex));
				}
				throw ex2;
			}
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x000472D4 File Offset: 0x000454D4
		private static void DetachDB(string databaseName, string connectionString)
		{
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			try
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand("USE master", sqlConnection);
				sqlCommand.ExecuteNonQuery();
				sqlCommand = new SqlCommand("sp_detach_db", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.Parameters.AddWithValue("@dbname", databaseName);
				sqlCommand.Parameters.AddWithValue("@skipchecks", "true");
				sqlCommand.ExecuteNonQuery();
			}
			catch
			{
			}
			finally
			{
				sqlConnection.Close();
			}
		}

		// Token: 0x04001699 RID: 5785
		internal const string s_strDataDir = "DataDirectory";

		// Token: 0x0400169A RID: 5786
		internal const string s_strUpperDataDirWithToken = "|DATADIRECTORY|";

		// Token: 0x0400169B RID: 5787
		internal const string s_strSqlExprFileExt = ".MDF";

		// Token: 0x0400169C RID: 5788
		internal const string s_strUpperUserInstance = "USER INSTANCE";

		// Token: 0x0400169D RID: 5789
		private const string s_localDbName = "(LOCALDB)";

		// Token: 0x0400169E RID: 5790
		private static object s_lock = new object();
	}
}
