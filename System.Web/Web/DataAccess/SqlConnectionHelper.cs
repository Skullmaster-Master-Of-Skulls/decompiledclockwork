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
	// Token: 0x0200027A RID: 634
	internal static class SqlConnectionHelper
	{
		// Token: 0x060020D8 RID: 8408 RVA: 0x0008ECE8 File Offset: 0x0008DCE8
		internal static void EnsureNoUserInstance(string connectionString)
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
			if (sqlConnectionStringBuilder.UserInstance)
			{
				throw new ProviderException();
			}
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x0008ED0C File Offset: 0x0008DD0C
		internal static SqlConnectionHolder GetConnection(string connectionString, bool revertImpersonation)
		{
			string text = connectionString.ToUpperInvariant();
			if (text.Contains("|DATADIRECTORY|"))
			{
				SqlConnectionHelper.EnsureSqlExpressDBFile(connectionString);
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

		// Token: 0x060020DA RID: 8410 RVA: 0x0008ED8C File Offset: 0x0008DD8C
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

		// Token: 0x060020DB RID: 8411 RVA: 0x0008EDE0 File Offset: 0x0008DDE0
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

		// Token: 0x060020DC RID: 8412 RVA: 0x0008EE88 File Offset: 0x0008DE88
		private static void EnsureSqlExpressDBFile(string connectionString)
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
				else
				{
					if (flag3 && text3.StartsWith("USER INSTANCE", StringComparison.Ordinal))
					{
						flag3 = false;
						int num = text3.IndexOf('=');
						if (num >= 0)
						{
							string a = text3.Substring(num + 1).Trim();
							if (!(a != "TRUE"))
							{
								goto IL_18A;
							}
						}
						return;
					}
					if (flag4 && text3.StartsWith("CONNECT TIMEOUT", StringComparison.Ordinal))
					{
						flag4 = false;
					}
				}
				IL_18A:;
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
				lock (SqlConnectionHelper.s_lock)
				{
					if (!File.Exists(text))
					{
						SqlConnectionHelper.CreateMdfFile(text, dataDirectory, connectionString);
					}
				}
			}
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x0008F0F0 File Offset: 0x0008E0F0
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

		// Token: 0x060020DE RID: 8414 RVA: 0x0008F308 File Offset: 0x0008E308
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

		// Token: 0x04001AD0 RID: 6864
		internal const string s_strDataDir = "DataDirectory";

		// Token: 0x04001AD1 RID: 6865
		internal const string s_strUpperDataDirWithToken = "|DATADIRECTORY|";

		// Token: 0x04001AD2 RID: 6866
		internal const string s_strSqlExprFileExt = ".MDF";

		// Token: 0x04001AD3 RID: 6867
		internal const string s_strUpperUserInstance = "USER INSTANCE";

		// Token: 0x04001AD4 RID: 6868
		private const string s_localDbName = "(LOCALDB)";

		// Token: 0x04001AD5 RID: 6869
		private static object s_lock = new object();
	}
}
