using System;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Spatial;

namespace System.Data.Common.Utils
{
	// Token: 0x02000392 RID: 914
	internal static class CommandHelper
	{
		// Token: 0x06003290 RID: 12944 RVA: 0x000C585B File Offset: 0x000C3A5B
		internal static void ConsumeReader(DbDataReader reader)
		{
			if (reader != null && !reader.IsClosed)
			{
				while (reader.NextResult())
				{
				}
			}
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000C5870 File Offset: 0x000C3A70
		internal static void ParseFunctionImportCommandText(string commandText, string defaultContainerName, out string containerName, out string functionImportName)
		{
			string[] array = commandText.Split(new char[]
			{
				'.'
			});
			containerName = null;
			functionImportName = null;
			if (2 == array.Length)
			{
				containerName = array[0].Trim();
				functionImportName = array[1].Trim();
			}
			else if (1 == array.Length && defaultContainerName != null)
			{
				containerName = defaultContainerName;
				functionImportName = array[0].Trim();
			}
			if (string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(functionImportName))
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_InvalidStoredProcedureCommandText);
			}
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000C58E4 File Offset: 0x000C3AE4
		internal static EntityTransaction GetEntityTransaction(EntityCommand entityCommand)
		{
			EntityTransaction transaction = entityCommand.Transaction;
			if (transaction != null && transaction != entityCommand.Connection.CurrentTransaction)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_InvalidTransactionForCommand);
			}
			return entityCommand.Connection.CurrentTransaction;
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000C5921 File Offset: 0x000C3B21
		internal static void SetStoreProviderCommandState(EntityCommand entityCommand, EntityTransaction entityTransaction, DbCommand storeProviderCommand)
		{
			storeProviderCommand.CommandTimeout = entityCommand.CommandTimeout;
			storeProviderCommand.Connection = entityCommand.Connection.StoreConnection;
			storeProviderCommand.Transaction = ((entityTransaction != null) ? entityTransaction.StoreTransaction : null);
			storeProviderCommand.UpdatedRowSource = entityCommand.UpdatedRowSource;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000C5960 File Offset: 0x000C3B60
		internal static void SetEntityParameterValues(EntityCommand entityCommand, DbCommand storeProviderCommand, EntityConnection connection)
		{
			foreach (object obj in storeProviderCommand.Parameters)
			{
				DbParameter dbParameter = (DbParameter)obj;
				ParameterDirection direction = dbParameter.Direction;
				if ((direction & ParameterDirection.Output) != (ParameterDirection)0)
				{
					int num = entityCommand.Parameters.IndexOf(dbParameter.ParameterName);
					if (0 <= num)
					{
						EntityParameter entityParameter = entityCommand.Parameters[num];
						object obj2 = dbParameter.Value;
						TypeUsage typeUsage = entityParameter.GetTypeUsage();
						if (Helper.IsSpatialType(typeUsage))
						{
							obj2 = CommandHelper.GetSpatialValueFromProviderValue(obj2, (PrimitiveType)typeUsage.EdmType, connection);
						}
						entityParameter.Value = obj2;
					}
				}
			}
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000C5A20 File Offset: 0x000C3C20
		private static object GetSpatialValueFromProviderValue(object spatialValue, PrimitiveType parameterType, EntityConnection connection)
		{
			DbProviderServices providerServices = DbProviderServices.GetProviderServices(connection.StoreConnection);
			StoreItemCollection storeItemCollection = (StoreItemCollection)connection.GetMetadataWorkspace().GetItemCollection(DataSpace.SSpace);
			DbSpatialServices spatialServices = providerServices.GetSpatialServices(storeItemCollection.StoreProviderManifestToken);
			if (Helper.IsGeographicType(parameterType))
			{
				return spatialServices.GeographyFromProviderValue(spatialValue);
			}
			return spatialServices.GeometryFromProviderValue(spatialValue);
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x000C5A70 File Offset: 0x000C3C70
		internal static EdmFunction FindFunctionImport(MetadataWorkspace workspace, string containerName, string functionImportName)
		{
			EntityContainer entityContainer;
			if (!workspace.TryGetEntityContainer(containerName, DataSpace.CSpace, out entityContainer))
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_UnableToFindFunctionImportContainer(containerName));
			}
			EdmFunction edmFunction = null;
			foreach (EdmFunction edmFunction2 in entityContainer.FunctionImports)
			{
				if (edmFunction2.Name == functionImportName)
				{
					edmFunction = edmFunction2;
					break;
				}
			}
			if (edmFunction == null)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_UnableToFindFunctionImport(containerName, functionImportName));
			}
			if (edmFunction.IsComposableAttribute)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_FunctionImportMustBeNonComposable(containerName + "." + functionImportName));
			}
			return edmFunction;
		}
	}
}
