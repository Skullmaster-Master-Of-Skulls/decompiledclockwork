using System;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000325 RID: 805
	internal static class CommandHelper
	{
		// Token: 0x06001BC1 RID: 7105 RVA: 0x000885D0 File Offset: 0x000867D0
		internal static void ConsumeReader(DbDataReader reader)
		{
			if (reader != null && !reader.IsClosed)
			{
				while (reader.NextResult())
				{
				}
			}
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x00088700 File Offset: 0x00086900
		internal static async Task ConsumeReaderAsync(DbDataReader reader, CancellationToken cancellationToken)
		{
			if (reader != null && !reader.IsClosed)
			{
				cancellationToken.ThrowIfCancellationRequested();
				while (await reader.NextResultAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
			}
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00088750 File Offset: 0x00086950
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
				throw new InvalidOperationException(Strings.EntityClient_InvalidStoredProcedureCommandText);
			}
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x000887C6 File Offset: 0x000869C6
		internal static void SetStoreProviderCommandState(EntityCommand entityCommand, EntityTransaction entityTransaction, DbCommand storeProviderCommand)
		{
			storeProviderCommand.CommandTimeout = entityCommand.CommandTimeout;
			storeProviderCommand.Connection = entityCommand.Connection.StoreConnection;
			storeProviderCommand.Transaction = ((entityTransaction != null) ? entityTransaction.StoreTransaction : null);
			storeProviderCommand.UpdatedRowSource = entityCommand.UpdatedRowSource;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00088804 File Offset: 0x00086A04
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

		// Token: 0x06001BC6 RID: 7110 RVA: 0x000888C4 File Offset: 0x00086AC4
		private static object GetSpatialValueFromProviderValue(object spatialValue, PrimitiveType parameterType, EntityConnection connection)
		{
			DbSpatialServices spatialServices = DbProviderServices.GetSpatialServices(DbConfiguration.DependencyResolver, connection);
			if (Helper.IsGeographicType(parameterType))
			{
				return spatialServices.GeographyFromProviderValue(spatialValue);
			}
			return spatialServices.GeometryFromProviderValue(spatialValue);
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x000888F4 File Offset: 0x00086AF4
		internal static EdmFunction FindFunctionImport(MetadataWorkspace workspace, string containerName, string functionImportName)
		{
			EntityContainer entityContainer;
			if (!workspace.TryGetEntityContainer(containerName, DataSpace.CSpace, out entityContainer))
			{
				throw new InvalidOperationException(Strings.EntityClient_UnableToFindFunctionImportContainer(containerName));
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
				throw new InvalidOperationException(Strings.EntityClient_UnableToFindFunctionImport(containerName, functionImportName));
			}
			if (edmFunction.IsComposableAttribute)
			{
				throw new InvalidOperationException(Strings.EntityClient_FunctionImportMustBeNonComposable(containerName + "." + functionImportName));
			}
			return edmFunction;
		}
	}
}
