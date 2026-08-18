using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.Core.Institution;
using TechnoPro.Common.Core.Updates.Adapters;
using TechnoPro.Common.Core.Updates.Resources;
using TechnoPro.Common.DAO.FileSign.Impl;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.Misc;
using TechnoPro.Common.DAO.Impl.Updates;
using TechnoPro.Common.DAO.Misc;
using TechnoPro.Common.ICore.Updates;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Public.Entities.Updates;
using TechnoPro.Common.Public.Entities.Updates.Adapters;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core.Updates
{
	// Token: 0x0200000A RID: 10
	[ExecuterFileType("Database tracking patch")]
	[ExecuterFileType("Database files patch")]
	[ExecuterFileType("Database patch")]
	internal class DatabasePatchUpdateExecuter : IUpdateExecuter
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002C80 File Offset: 0x00000E80
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002C88 File Offset: 0x00000E88
		public string Name { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002C91 File Offset: 0x00000E91
		public int ExecutionOrder
		{
			get
			{
				return UpdateFileTypes.UpdateFileTypesList.IndexOf(this.ExecutingFileType());
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002CA3 File Offset: 0x00000EA3
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002CAB File Offset: 0x00000EAB
		public ServerInstanceInfo ServerInstance { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002CB4 File Offset: 0x00000EB4
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002CBC File Offset: 0x00000EBC
		public IExternalLogManager ExternalLogManager { get; set; }

		// Token: 0x06000040 RID: 64 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public ExecuteUpdatesResp ExecuteUpdate()
		{
			ExecuteUpdatesResp result;
			try
			{
				string updates_PATH = ClockWorkUpdateSystemPathVariables.UPDATES_PATH;
				UpdateDAO updateDAO = new UpdateDAO();
				IList<UpdateStatus> executionStatus = updateDAO.GetExecutionStatus();
				InstitutionManager institutionManager = new InstitutionManager();
				string institutionUniqueName = institutionManager.GetInstitutionUniqueName();
				List<UpdateStatus> list = (from u in executionStatus
				where u.Status == eUpdateStatus.OnSchedule.ToString() && (u.FileType == "Database patch" || u.FileType == "Database files patch" || u.FileType == "Database tracking patch")
				select u).ToList<UpdateStatus>();
				foreach (UpdateStatus updateStatus in list)
				{
					bool flag = updateStatus != null;
					if (flag)
					{
						string path = Path.Combine(updates_PATH, updateStatus.IsPublic ? "Public" : institutionUniqueName);
						string text = Path.Combine(path, updateStatus.Filename);
						IFileSignDAO fileSignDAO = new FileSignDAO();
						string tempFileName = FileSystem.GetTempFileName(Path.GetExtension(text));
						try
						{
							CWLogger.Logger.Info("ExecuteUpdateManager::ExecuteUpdate: Encrypted filename='{0}'", text);
							fileSignDAO.DecryptAndVerifyUsingFileSystem(text, tempFileName);
							CWLogger.Logger.Info("ExecuteUpdateManager::ExecuteUpdate: Decrypted filename='{0}'", tempFileName);
						}
						catch (DecryptAndVerifyFailedException ex)
						{
							CWLogger.Logger.ErrorException(string.Format("DatabasePatchExecuter::ExecuteUpdate:: {0}", ex.ToString()), ex);
							return new ExecuteUpdatesResp
							{
								ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
								LastError = ex.Message
							};
						}
						string text2 = null;
						IMiscDAO miscDAO = null;
						try
						{
							bool flag2 = updateStatus.FileType == "Database patch";
							if (flag2)
							{
								miscDAO = new MiscDAO();
							}
							else
							{
								bool flag3 = updateStatus.FileType == "Database files patch";
								if (flag3)
								{
									miscDAO = new MiscDAO(eDatabaseConnectionStringName.ClockWorkFiles);
								}
								else
								{
									bool flag4 = updateStatus.FileType == "Database tracking patch";
									if (flag4)
									{
										miscDAO = new MiscDAO(eDatabaseConnectionStringName.ClockWorkTracking);
									}
								}
							}
							bool flag5 = miscDAO != null;
							if (flag5)
							{
								text2 = miscDAO.GetValue(555);
							}
						}
						catch
						{
						}
						Version version = null;
						bool flag6 = !string.IsNullOrEmpty(text2);
						if (flag6)
						{
							version = new Version(text2.FormatVersion());
						}
						bool flag7 = updateStatus.FileType == "Database patch";
						if (flag7)
						{
							Version v = new Version(5, 13, 4, 1);
							bool flag8 = version != null && version < v;
							if (flag8)
							{
								string database_patch_5_13_04_ = UpdateResources.Database_patch_5_13_04_01;
								bool flag9 = !string.IsNullOrEmpty(database_patch_5_13_04_);
								if (flag9)
								{
									this.ExecuteDbScript(database_patch_5_13_04_, eDatabaseConnectionStringName.ClockWork, false);
								}
							}
						}
						Version versionObject = updateStatus.Filename.GetVersionObject();
						CWLogger.Logger.Info("ExecuteUpdateManager::ExecuteUpdate: Filename='{2}', Database version='{0}', db file version='{1}'", (version != null) ? version.ToString() : "NULL", (versionObject != null) ? versionObject.ToString() : "NULL", updateStatus.Filename);
						bool flag10 = updateStatus.Filename.IsHotFix();
						if (flag10)
						{
							bool flag11 = versionObject == null;
							if (flag11)
							{
								string text3 = "DatabasePatchExecuter::ExecuteUpdate:: File version for update file '" + (updateStatus.Filename ?? string.Empty) + "' is missing or incorrect";
								CWLogger.Logger.Error(text3);
								updateStatus.Status = eUpdateStatus.Dismiss.ToString();
								updateDAO.SaveExecutionStatus(updateStatus);
								return new ExecuteUpdatesResp
								{
									ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
									LastError = text3
								};
							}
							bool flag12 = version != null && version >= versionObject;
							if (flag12)
							{
								updateStatus.Status = eUpdateStatus.Dismiss.ToString();
								updateDAO.SaveExecutionStatus(updateStatus);
								continue;
							}
						}
						string fileType = updateStatus.FileType;
						string a = fileType;
						if (!(a == "Database patch"))
						{
							if (!(a == "Database files patch"))
							{
								if (a == "Database tracking patch")
								{
									bool flag13 = !this.ExecuteDbScriptFromFile(tempFileName, eDatabaseConnectionStringName.ClockWorkTracking, false);
									if (flag13)
									{
										continue;
									}
								}
							}
							else
							{
								bool flag14 = !this.ExecuteDbScriptFromFile(tempFileName, eDatabaseConnectionStringName.ClockWorkFiles, false);
								if (flag14)
								{
									continue;
								}
							}
						}
						else
						{
							this.ExecuteDbScriptFromFile(tempFileName, eDatabaseConnectionStringName.ClockWork, true);
						}
						updateStatus.Status = eUpdateStatus.Done.ToString();
						updateDAO.SaveExecutionStatus(updateStatus);
						this.ExternalLogManager.Log("Database patch '" + updateStatus.Filename + "' was successfully installed on " + DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt"));
					}
				}
				ExecuteUpdatesResp executeUpdatesResp;
				if (list.Count <= 0)
				{
					(executeUpdatesResp = new ExecuteUpdatesResp()).ExecuteUpdatesStatus = eExecuteUpdateStatus.UpToDate;
				}
				else
				{
					ExecuteUpdatesResp executeUpdatesResp2 = new ExecuteUpdatesResp();
					executeUpdatesResp2.ExecuteUpdatesStatus = eExecuteUpdateStatus.Updated;
					executeUpdatesResp = executeUpdatesResp2;
					executeUpdatesResp2.Filenames = (from u in list
					select u.Filename).ToList<string>();
				}
				result = executeUpdatesResp;
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.ErrorException("DatabasePatchExecuter::ExecuteUpdate:: " + ex2.ToString(), ex2);
				result = new ExecuteUpdatesResp
				{
					ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
					LastError = ex2.Message
				};
			}
			return result;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003228 File Offset: 0x00001428
		private bool ExecuteDbScript(string dbPatch, eDatabaseConnectionStringName dbRole = eDatabaseConnectionStringName.ClockWork, bool throwErrors = false)
		{
			bool result;
			try
			{
				IList<string> commands = DatabasePatchUpdateExecuter.ParseSqlScript(dbPatch);
				this.ExecuteCommands(commands, DatabaseLayerFactory.GetPatchDatabaseLayer(this.ServerInstance.VirtualDirectory, dbRole));
				result = true;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ExecuteUpdateManager::ExecuteDbScript:: Database Role='{0}': {1}", dbRole, ex.ToString()), ex);
				if (throwErrors)
				{
					throw;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000329C File Offset: 0x0000149C
		private bool ExecuteDbScriptFromFile(string filename, eDatabaseConnectionStringName dbRole = eDatabaseConnectionStringName.ClockWork, bool throwErrors = false)
		{
			bool result;
			try
			{
				CWLogger.Logger.Info("ExecuteUpdateManager::ExecuteDbScriptFromFile: Database Role='{0}', Filename='{1}'", dbRole, filename);
				IList<string> commands = this.ParseSqlScriptFromFile(filename);
				DatabaseLayer patchDatabaseLayer = DatabaseLayerFactory.GetPatchDatabaseLayer(this.ServerInstance.VirtualDirectory, dbRole);
				bool flag = patchDatabaseLayer == null;
				if (flag)
				{
					result = false;
				}
				else
				{
					this.ExecuteCommands(commands, patchDatabaseLayer);
					result = true;
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ExecuteUpdateManager::ExecuteDbScriptFromFile:: Database Role='{0}': {1}", dbRole, ex.ToString()), ex);
				if (throwErrors)
				{
					throw;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000333C File Offset: 0x0000153C
		private void ExecuteCommands(IList<string> commands, DatabaseLayer dbManager = null)
		{
			DatabaseLayer databaseLayer = dbManager ?? this.ServerInstance.GetPatchDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
			DbTransaction transaction = databaseLayer.BeginDbTransaction();
			try
			{
				foreach (string text in commands)
				{
					try
					{
						databaseLayer.ExecuteNonQueryTransaction(text, transaction, CommandOverrideSettings.CommandOverrideSettingsTimeout180);
					}
					catch (DbException exception)
					{
						CWLogger.Logger.ErrorException("ExecuteUpdateManager::ExecuteCommands: Query='" + text + "'", exception);
						throw;
					}
				}
				databaseLayer.CommitDbTransaction(transaction);
			}
			catch (DbException)
			{
				databaseLayer.RollbackDbTransaction(transaction);
				throw;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003400 File Offset: 0x00001600
		private static IList<string> ParseSqlScript(string dbPatch)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			using (StringReader stringReader = new StringReader(dbPatch))
			{
				while (stringReader.Peek() > 0)
				{
					string text = stringReader.ReadLine();
					bool flag = text == null;
					if (!flag)
					{
						string text2 = text.Trim();
						bool flag2 = text2.ToLower().Equals("go") || text2.ToLower().Equals("go\r\n");
						if (flag2)
						{
							list.Add(stringBuilder.ToString());
							stringBuilder = new StringBuilder();
						}
						else
						{
							stringBuilder.AppendLine(text2);
						}
					}
				}
				bool flag3 = stringBuilder.Length > 0;
				if (flag3)
				{
					list.Add(stringBuilder.ToString());
				}
			}
			return list;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000034E0 File Offset: 0x000016E0
		private IList<string> ParseSqlScriptFromFile(string filename)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			using (StreamReader streamReader = new StreamReader(filename))
			{
				while (streamReader.Peek() > 0)
				{
					string text = streamReader.ReadLine();
					bool flag = text == null;
					if (!flag)
					{
						string text2 = text.Trim();
						bool flag2 = text2.ToLower().Equals("go") || text2.ToLower().Equals("go\r\n");
						if (flag2)
						{
							list.Add(stringBuilder.ToString());
							stringBuilder = new StringBuilder();
						}
						else
						{
							stringBuilder.AppendLine(text2);
						}
					}
				}
				bool flag3 = stringBuilder.Length > 0;
				if (flag3)
				{
					list.Add(stringBuilder.ToString());
				}
			}
			return list;
		}
	}
}
