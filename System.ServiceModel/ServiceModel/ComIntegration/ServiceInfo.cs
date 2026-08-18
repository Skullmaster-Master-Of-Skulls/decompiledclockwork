using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Runtime;
using System.ServiceModel.Configuration;
using System.Transactions;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200025B RID: 603
	internal class ServiceInfo
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0003FC64 File Offset: 0x0003DE64
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0003FC6C File Offset: 0x0003DE6C
		public ServiceInfo(Guid clsid, ServiceElement service, ComCatalogObject application, ComCatalogObject classObject, HostingMode hostingMode)
		{
			this.service = service;
			this.clsid = clsid;
			this.appid = Fx.CreateGuid((string)application.GetValue("ID"));
			this.partitionId = Fx.CreateGuid((string)application.GetValue("AppPartitionID"));
			this.bitness = (Bitness)classObject.GetValue("Bitness");
			this.transactionOption = (TransactionOption)classObject.GetValue("Transaction");
			this.hostingMode = hostingMode;
			this.managedType = TypeCacheManager.ResolveClsidToType(clsid);
			this.serviceName = application.Name + "." + classObject.Name;
			this.udts = new Dictionary<Guid, List<Type>>();
			COMAdminIsolationLevel comadminIsolationLevel = (COMAdminIsolationLevel)classObject.GetValue("TxIsolationLevel");
			switch (comadminIsolationLevel)
			{
			case COMAdminIsolationLevel.Any:
				this.isolationLevel = IsolationLevel.Unspecified;
				break;
			case COMAdminIsolationLevel.ReadUncommitted:
				this.isolationLevel = IsolationLevel.ReadUncommitted;
				break;
			case COMAdminIsolationLevel.ReadCommitted:
				this.isolationLevel = IsolationLevel.ReadCommitted;
				break;
			case COMAdminIsolationLevel.RepeatableRead:
				this.isolationLevel = IsolationLevel.RepeatableRead;
				break;
			case COMAdminIsolationLevel.Serializable:
				this.isolationLevel = IsolationLevel.Serializable;
				break;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("InvalidIsolationLevelValue", new object[]
				{
					this.clsid,
					comadminIsolationLevel
				})));
			}
			COMAdminThreadingModel comadminThreadingModel = (COMAdminThreadingModel)classObject.GetValue("ThreadingModel");
			if (comadminThreadingModel == COMAdminThreadingModel.Apartment || comadminThreadingModel == COMAdminThreadingModel.Main)
			{
				this.threadingModel = ThreadingModel.STA;
				this.objectPoolingEnabled = false;
			}
			else
			{
				this.threadingModel = ThreadingModel.MTA;
				this.objectPoolingEnabled = (bool)classObject.GetValue("ObjectPoolingEnabled");
			}
			if (this.objectPoolingEnabled)
			{
				this.maxPoolSize = (int)classObject.GetValue("MaxPoolSize");
			}
			else
			{
				this.maxPoolSize = 0;
			}
			bool flag = (bool)application.GetValue("ApplicationAccessChecksEnabled");
			if (flag)
			{
				bool flag2 = (bool)classObject.GetValue("ComponentAccessChecksEnabled");
				if (flag2)
				{
					this.checkRoles = true;
				}
			}
			ComCatalogCollection collection = classObject.GetCollection("RolesForComponent");
			this.componentRoleMembers = CatalogUtil.GetRoleMembers(application, collection);
			this.contracts = new List<ContractInfo>();
			ComCatalogCollection collection2 = classObject.GetCollection("InterfacesForComponent");
			foreach (object obj in service.Endpoints)
			{
				ServiceEndpointElement serviceEndpointElement = (ServiceEndpointElement)obj;
				ContractInfo contractInfo = null;
				if (!(serviceEndpointElement.Contract == "IMetadataExchange"))
				{
					Guid guid;
					if (DiagnosticUtility.Utility.TryCreateGuid(serviceEndpointElement.Contract, out guid))
					{
						bool flag3 = false;
						foreach (ContractInfo contractInfo2 in this.contracts)
						{
							if (guid == contractInfo2.IID)
							{
								flag3 = true;
								break;
							}
						}
						if (flag3)
						{
							continue;
						}
						foreach (ComCatalogObject comCatalogObject in collection2)
						{
							Guid a;
							if (DiagnosticUtility.Utility.TryCreateGuid((string)comCatalogObject.GetValue("IID"), out a) && a == guid)
							{
								contractInfo = new ContractInfo(guid, serviceEndpointElement, comCatalogObject, application);
								break;
							}
						}
					}
					if (contractInfo == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("EndpointNotAnIID", new object[]
						{
							clsid.ToString("B").ToUpperInvariant(),
							serviceEndpointElement.Contract
						})));
					}
					this.contracts.Add(contractInfo);
				}
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00040034 File Offset: 0x0003E234
		public Type ServiceType
		{
			get
			{
				return this.managedType;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0004003C File Offset: 0x0003E23C
		public ServiceElement ServiceElement
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00040044 File Offset: 0x0003E244
		public Guid Clsid
		{
			get
			{
				return this.clsid;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x0004004C File Offset: 0x0003E24C
		public Guid AppID
		{
			get
			{
				return this.appid;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x00040054 File Offset: 0x0003E254
		public Guid PartitionId
		{
			get
			{
				return this.partitionId;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0004005C File Offset: 0x0003E25C
		public Bitness Bitness
		{
			get
			{
				return this.bitness;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x00040064 File Offset: 0x0003E264
		public bool CheckRoles
		{
			get
			{
				return this.checkRoles;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0004006C File Offset: 0x0003E26C
		public ThreadingModel ThreadingModel
		{
			get
			{
				return this.threadingModel;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x00040074 File Offset: 0x0003E274
		public TransactionOption TransactionOption
		{
			get
			{
				return this.transactionOption;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x0004007C File Offset: 0x0003E27C
		public IsolationLevel IsolationLevel
		{
			get
			{
				return this.isolationLevel;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x00040084 File Offset: 0x0003E284
		public string[] ComponentRoleMembers
		{
			get
			{
				return this.componentRoleMembers;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0004008C File Offset: 0x0003E28C
		public List<ContractInfo> Contracts
		{
			get
			{
				return this.contracts;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x00040094 File Offset: 0x0003E294
		public HostingMode HostingMode
		{
			get
			{
				return this.hostingMode;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0004009C File Offset: 0x0003E29C
		public bool Pooled
		{
			get
			{
				return this.objectPoolingEnabled;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x000400A4 File Offset: 0x0003E2A4
		public int MaxPoolSize
		{
			get
			{
				return this.maxPoolSize;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x000400AC File Offset: 0x0003E2AC
		internal Guid[] Assemblies
		{
			get
			{
				Guid[] array = new Guid[this.udts.Keys.Count];
				this.udts.Keys.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000400E2 File Offset: 0x0003E2E2
		internal bool HasUdts()
		{
			return this.udts.Keys.Count > 0;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000400F8 File Offset: 0x0003E2F8
		internal Type[] GetTypes(Guid assemblyId)
		{
			List<Type> list = null;
			this.udts.TryGetValue(assemblyId, out list);
			if (list == null)
			{
				return new Type[0];
			}
			return list.ToArray();
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00040128 File Offset: 0x0003E328
		internal void AddUdt(Type udt, Guid assemblyId)
		{
			if (!this.udts.ContainsKey(assemblyId))
			{
				this.udts[assemblyId] = new List<Type>();
			}
			if (!this.udts[assemblyId].Contains(udt))
			{
				this.udts[assemblyId].Add(udt);
			}
		}

		// Token: 0x04001975 RID: 6517
		private ServiceElement service;

		// Token: 0x04001976 RID: 6518
		private Guid clsid;

		// Token: 0x04001977 RID: 6519
		private Guid appid;

		// Token: 0x04001978 RID: 6520
		private HostingMode hostingMode;

		// Token: 0x04001979 RID: 6521
		private Guid partitionId;

		// Token: 0x0400197A RID: 6522
		private Bitness bitness;

		// Token: 0x0400197B RID: 6523
		private ThreadingModel threadingModel;

		// Token: 0x0400197C RID: 6524
		private TransactionOption transactionOption;

		// Token: 0x0400197D RID: 6525
		private IsolationLevel isolationLevel;

		// Token: 0x0400197E RID: 6526
		private bool checkRoles;

		// Token: 0x0400197F RID: 6527
		private string[] componentRoleMembers;

		// Token: 0x04001980 RID: 6528
		private bool objectPoolingEnabled;

		// Token: 0x04001981 RID: 6529
		private int maxPoolSize;

		// Token: 0x04001982 RID: 6530
		private Type managedType;

		// Token: 0x04001983 RID: 6531
		private List<ContractInfo> contracts;

		// Token: 0x04001984 RID: 6532
		private string serviceName;

		// Token: 0x04001985 RID: 6533
		private Dictionary<Guid, List<Type>> udts;
	}
}
