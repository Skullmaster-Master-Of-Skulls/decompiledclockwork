using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.DAO.Database;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.Database
{
	// Token: 0x020000FD RID: 253
	public class DatabaseDAO : IDatabaseDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public DatabaseDAO()
		{
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0004A56C File Offset: 0x0004876C
		public DatabaseDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0004A57E File Offset: 0x0004877E
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0004A586 File Offset: 0x00048786
		public OperationContext OpContext { get; set; }

		// Token: 0x06000730 RID: 1840 RVA: 0x0004A590 File Offset: 0x00048790
		public bool DoesTableExist(string tableName)
		{
			bool result;
			try
			{
				string query = "SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[" + tableName + "]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1";
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
				DataTable dataTable = databaseLayer.ExecuteQuery(query);
				result = (dataTable.Rows.Count > 0);
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0004A5F4 File Offset: 0x000487F4
		public bool DoesColumnExist(string tableName, string colName)
		{
			bool result;
			try
			{
				string query = string.Concat(new string[]
				{
					"SELECT * from syscolumns WHERE id=object_id('",
					tableName,
					"') AND name='",
					colName,
					"'"
				});
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
				DataTable dataTable = databaseLayer.ExecuteQuery(query);
				result = (dataTable.Rows.Count > 0);
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0004A674 File Offset: 0x00048874
		public void ExecuteCommands(IList<string> commands, bool useTransactions = true)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			if (useTransactions)
			{
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
							CWLogger.Logger.ErrorException("DatabaseDAO::ExecuteCommands: Query='" + text + "'", exception);
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
			else
			{
				foreach (string text2 in commands)
				{
					try
					{
						databaseLayer.ExecuteNonQuery(text2, CommandOverrideSettings.CommandOverrideSettingsTimeout180);
					}
					catch (DbException exception2)
					{
						CWLogger.Logger.ErrorException("DatabaseDAO::ExecuteCommands: Query='" + text2 + "'", exception2);
						throw;
					}
				}
			}
		}
	}
}
