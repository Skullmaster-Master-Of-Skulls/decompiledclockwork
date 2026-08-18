using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005AE RID: 1454
	internal class DataContractSerializerServiceBehavior : IServiceBehavior, IEndpointBehavior
	{
		// Token: 0x060038BD RID: 14525 RVA: 0x000DB38B File Offset: 0x000D958B
		internal DataContractSerializerServiceBehavior(bool ignoreExtensionDataObject, int maxItemsInObjectGraph)
		{
			this.ignoreExtensionDataObject = ignoreExtensionDataObject;
			this.maxItemsInObjectGraph = maxItemsInObjectGraph;
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x060038BE RID: 14526 RVA: 0x000DB3A1 File Offset: 0x000D95A1
		// (set) Token: 0x060038BF RID: 14527 RVA: 0x000DB3A9 File Offset: 0x000D95A9
		public bool IgnoreExtensionDataObject
		{
			get
			{
				return this.ignoreExtensionDataObject;
			}
			set
			{
				this.ignoreExtensionDataObject = value;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x060038C0 RID: 14528 RVA: 0x000DB3B2 File Offset: 0x000D95B2
		// (set) Token: 0x060038C1 RID: 14529 RVA: 0x000DB3BA File Offset: 0x000D95BA
		public int MaxItemsInObjectGraph
		{
			get
			{
				return this.maxItemsInObjectGraph;
			}
			set
			{
				this.maxItemsInObjectGraph = value;
			}
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x000DB3C3 File Offset: 0x000D95C3
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x000DB3C5 File Offset: 0x000D95C5
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x000DB3C7 File Offset: 0x000D95C7
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			DataContractSerializerServiceBehavior.ApplySerializationSettings(description, this.ignoreExtensionDataObject, this.maxItemsInObjectGraph);
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x000DB3DB File Offset: 0x000D95DB
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000DB3DD File Offset: 0x000D95DD
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection parameters)
		{
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x000DB3DF File Offset: 0x000D95DF
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime clientRuntime)
		{
			DataContractSerializerServiceBehavior.ApplySerializationSettings(serviceEndpoint, this.ignoreExtensionDataObject, this.maxItemsInObjectGraph);
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000DB3F3 File Offset: 0x000D95F3
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
			DataContractSerializerServiceBehavior.ApplySerializationSettings(serviceEndpoint, this.ignoreExtensionDataObject, this.maxItemsInObjectGraph);
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x000DB408 File Offset: 0x000D9608
		internal static void ApplySerializationSettings(ServiceDescription description, bool ignoreExtensionDataObject, int maxItemsInObjectGraph)
		{
			foreach (ServiceEndpoint serviceEndpoint in description.Endpoints)
			{
				if (!serviceEndpoint.InternalIsSystemEndpoint(description))
				{
					DataContractSerializerServiceBehavior.ApplySerializationSettings(serviceEndpoint, ignoreExtensionDataObject, maxItemsInObjectGraph);
				}
			}
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x000DB460 File Offset: 0x000D9660
		internal static void ApplySerializationSettings(ServiceEndpoint endpoint, bool ignoreExtensionDataObject, int maxItemsInObjectGraph)
		{
			foreach (OperationDescription operationDescription in endpoint.Contract.Operations)
			{
				foreach (IOperationBehavior operationBehavior in operationDescription.Behaviors)
				{
					if (operationBehavior is DataContractSerializerOperationBehavior)
					{
						DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = (DataContractSerializerOperationBehavior)operationBehavior;
						if (dataContractSerializerOperationBehavior != null)
						{
							if (!dataContractSerializerOperationBehavior.IgnoreExtensionDataObjectSetExplicit)
							{
								dataContractSerializerOperationBehavior.ignoreExtensionDataObject = ignoreExtensionDataObject;
							}
							if (!dataContractSerializerOperationBehavior.MaxItemsInObjectGraphSetExplicit)
							{
								dataContractSerializerOperationBehavior.maxItemsInObjectGraph = maxItemsInObjectGraph;
							}
						}
					}
				}
			}
		}

		// Token: 0x040029B2 RID: 10674
		private bool ignoreExtensionDataObject;

		// Token: 0x040029B3 RID: 10675
		private int maxItemsInObjectGraph;
	}
}
