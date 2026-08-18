using System;
using System.Collections.Generic;
using System.Data;
using Databases;
using TechnoPro.Common.DAO.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.DAO.Impl.ServiceProvider
{
	// Token: 0x0200005D RID: 93
	public class ServiceProviderTypeDAO : IServiceProviderTypeDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000255 RID: 597 RVA: 0x00013B65 File Offset: 0x00011D65
		public ServiceProviderTypeDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00013B95 File Offset: 0x00011D95
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00013B9D File Offset: 0x00011D9D
		public OperationContext OpContext { get; set; }

		// Token: 0x06000258 RID: 600 RVA: 0x00013BA8 File Offset: 0x00011DA8
		public SPProviderType GetProviderTypeFromRecord(IDataReader record)
		{
			bool flag = record == null || record["SPProviderTypeId"] == DBNull.Value;
			SPProviderType result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["SPProviderTypeBehaviourCode"] == DBNull.Value) ? 0 : ((int)record["SPProviderTypeBehaviourCode"]);
				result = new SPProviderType
				{
					SPProviderTypeId = (int)record["SPProviderTypeId"],
					Title = record["ProviderTypeTitle"].ToString(),
					Description = record["ProviderTypeDescription"].ToString(),
					BehaviourCode = (eProviderTypeBehaviourCode)(Enum.IsDefined(typeof(eProviderTypeBehaviourCode), num) ? num : 0),
					IsActive = true
				};
			}
			return result;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00013C7C File Offset: 0x00011E7C
		public IList<SPProviderType> LoadActiveProviderTypes()
		{
			IList<SPProviderType> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT sp.spprovidertypeid,sp.providertypetitle,sp.providertypedescription,sp.spprovidertypebehaviourcode,sp.providertypeisactive FROM spprovidertype sp WHERE sp.providertypeisactive=1 ORDER BY providertypetitle"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<SPProviderType> list = new List<SPProviderType>();
					while (dataReader.Read())
					{
						SPProviderType providerTypeFromRecord = this.GetProviderTypeFromRecord(dataReader);
						bool flag2 = providerTypeFromRecord != null;
						if (flag2)
						{
							list.Add(providerTypeFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x040000E6 RID: 230
		public DatabaseLayer DatabaseManager;
	}
}
