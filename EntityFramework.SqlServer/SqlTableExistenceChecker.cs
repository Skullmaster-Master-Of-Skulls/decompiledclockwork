using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000027 RID: 39
	internal class SqlTableExistenceChecker : TableExistenceChecker
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000AB10 File Offset: 0x00008D10
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public override bool AnyModelTableExistsInDatabase(ObjectContext context, DbConnection connection, IEnumerable<EntitySet> modelTables, string edmMetadataContextTableName)
		{
			SqlTableExistenceChecker.<>c__DisplayClass1 CS$<>8__locals1 = new SqlTableExistenceChecker.<>c__DisplayClass1();
			CS$<>8__locals1.context = context;
			CS$<>8__locals1.connection = connection;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (EntitySet entitySet in modelTables)
			{
				stringBuilder.Append("'");
				stringBuilder.Append((string)entitySet.MetadataProperties["Schema"].Value);
				stringBuilder.Append(".");
				stringBuilder.Append(this.GetTableName(entitySet));
				stringBuilder.Append("',");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			bool result;
			using (DbCommand command = CS$<>8__locals1.connection.CreateCommand())
			{
				command.CommandText = string.Concat(new object[]
				{
					"\r\nSELECT Count(*)\r\nFROM INFORMATION_SCHEMA.TABLES AS t\r\nWHERE t.TABLE_SCHEMA + '.' + t.TABLE_NAME IN (",
					stringBuilder,
					")\r\n    OR t.TABLE_NAME = '",
					edmMetadataContextTableName,
					"'"
				});
				bool flag = true;
				if (DbInterception.Dispatch.Connection.GetState(CS$<>8__locals1.connection, CS$<>8__locals1.context.InterceptionContext) == ConnectionState.Open)
				{
					flag = false;
					EntityTransaction currentTransaction = ((EntityConnection)CS$<>8__locals1.context.Connection).CurrentTransaction;
					if (currentTransaction != null)
					{
						command.Transaction = currentTransaction.StoreTransaction;
					}
				}
				IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(CS$<>8__locals1.connection);
				try
				{
					result = executionStrategy.Execute<bool>(delegate()
					{
						if (DbInterception.Dispatch.Connection.GetState(CS$<>8__locals1.connection, CS$<>8__locals1.context.InterceptionContext) == ConnectionState.Broken)
						{
							DbInterception.Dispatch.Connection.Close(CS$<>8__locals1.connection, CS$<>8__locals1.context.InterceptionContext);
						}
						if (DbInterception.Dispatch.Connection.GetState(CS$<>8__locals1.connection, CS$<>8__locals1.context.InterceptionContext) == ConnectionState.Closed)
						{
							DbInterception.Dispatch.Connection.Open(CS$<>8__locals1.connection, CS$<>8__locals1.context.InterceptionContext);
						}
						return (int)DbInterception.Dispatch.Command.Scalar(command, new DbCommandInterceptionContext(CS$<>8__locals1.context.InterceptionContext)) > 0;
					});
				}
				finally
				{
					if (flag && DbInterception.Dispatch.Connection.GetState(CS$<>8__locals1.connection, CS$<>8__locals1.context.InterceptionContext) != ConnectionState.Closed)
					{
						DbInterception.Dispatch.Connection.Close(CS$<>8__locals1.connection, CS$<>8__locals1.context.InterceptionContext);
					}
				}
			}
			return result;
		}
	}
}
