using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x020003E2 RID: 994
	public static class MetadataResolver
	{
		// Token: 0x0600256A RID: 9578 RVA: 0x0008646A File Offset: 0x0008466A
		public static ServiceEndpointCollection Resolve(Type contract, EndpointAddress address)
		{
			if (contract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contract");
			}
			return MetadataResolver.Resolve(MetadataResolver.CreateContractCollection(contract), address);
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x00086491 File Offset: 0x00084691
		public static ServiceEndpointCollection Resolve(IEnumerable<ContractDescription> contracts, EndpointAddress address)
		{
			return MetadataResolver.Resolve(contracts, address, new MetadataExchangeClient(address));
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000864A0 File Offset: 0x000846A0
		public static ServiceEndpointCollection Resolve(IEnumerable<ContractDescription> contracts, EndpointAddress address, MetadataExchangeClient client)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			if (client == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("client");
			}
			if (contracts == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contracts");
			}
			MetadataResolver.ValidateContracts(contracts);
			MetadataSet metadata = client.GetMetadata(address);
			return MetadataResolver.ImportEndpoints(metadata, contracts, client);
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x00086502 File Offset: 0x00084702
		public static ServiceEndpointCollection Resolve(Type contract, Uri address, MetadataExchangeClientMode mode)
		{
			if (contract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contract");
			}
			return MetadataResolver.Resolve(MetadataResolver.CreateContractCollection(contract), address, mode);
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x0008652A File Offset: 0x0008472A
		public static ServiceEndpointCollection Resolve(IEnumerable<ContractDescription> contracts, Uri address, MetadataExchangeClientMode mode)
		{
			return MetadataResolver.Resolve(contracts, address, mode, new MetadataExchangeClient(address, mode));
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x0008653C File Offset: 0x0008473C
		public static ServiceEndpointCollection Resolve(IEnumerable<ContractDescription> contracts, Uri address, MetadataExchangeClientMode mode, MetadataExchangeClient client)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			MetadataExchangeClientModeHelper.Validate(mode);
			if (client == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("client");
			}
			if (contracts == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contracts");
			}
			MetadataResolver.ValidateContracts(contracts);
			MetadataSet metadata = client.GetMetadata(address, mode);
			return MetadataResolver.ImportEndpoints(metadata, contracts, client);
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000865A5 File Offset: 0x000847A5
		public static IAsyncResult BeginResolve(Type contract, EndpointAddress address, AsyncCallback callback, object asyncState)
		{
			if (contract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contract");
			}
			return MetadataResolver.BeginResolve(MetadataResolver.CreateContractCollection(contract), address, callback, asyncState);
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000865CE File Offset: 0x000847CE
		public static IAsyncResult BeginResolve(IEnumerable<ContractDescription> contracts, EndpointAddress address, AsyncCallback callback, object asyncState)
		{
			return MetadataResolver.BeginResolve(contracts, address, new MetadataExchangeClient(address), callback, asyncState);
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000865E0 File Offset: 0x000847E0
		public static IAsyncResult BeginResolve(IEnumerable<ContractDescription> contracts, EndpointAddress address, MetadataExchangeClient client, AsyncCallback callback, object asyncState)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			if (client == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("client");
			}
			if (contracts == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contracts");
			}
			MetadataResolver.ValidateContracts(contracts);
			return new MetadataResolver.AsyncMetadataResolverHelper(address, MetadataExchangeClientMode.MetadataExchange, client, contracts, callback, asyncState);
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x0008663E File Offset: 0x0008483E
		public static IAsyncResult BeginResolve(Type contract, Uri address, MetadataExchangeClientMode mode, AsyncCallback callback, object asyncState)
		{
			if (contract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contract");
			}
			return MetadataResolver.BeginResolve(MetadataResolver.CreateContractCollection(contract), address, mode, callback, asyncState);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x00086669 File Offset: 0x00084869
		public static IAsyncResult BeginResolve(IEnumerable<ContractDescription> contracts, Uri address, MetadataExchangeClientMode mode, AsyncCallback callback, object asyncState)
		{
			return MetadataResolver.BeginResolve(contracts, address, mode, new MetadataExchangeClient(address, mode), callback, asyncState);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x00086680 File Offset: 0x00084880
		public static IAsyncResult BeginResolve(IEnumerable<ContractDescription> contracts, Uri address, MetadataExchangeClientMode mode, MetadataExchangeClient client, AsyncCallback callback, object asyncState)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			MetadataExchangeClientModeHelper.Validate(mode);
			if (client == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("client");
			}
			if (contracts == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contracts");
			}
			MetadataResolver.ValidateContracts(contracts);
			return new MetadataResolver.AsyncMetadataResolverHelper(new EndpointAddress(address, new AddressHeader[0]), mode, client, contracts, callback, asyncState);
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000866F0 File Offset: 0x000848F0
		public static ServiceEndpointCollection EndResolve(IAsyncResult result)
		{
			return MetadataResolver.AsyncMetadataResolverHelper.EndAsyncCall(result);
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000866F8 File Offset: 0x000848F8
		private static ServiceEndpointCollection ImportEndpoints(MetadataSet metadataSet, IEnumerable<ContractDescription> contracts, MetadataExchangeClient client)
		{
			ServiceEndpointCollection serviceEndpointCollection = new ServiceEndpointCollection();
			WsdlImporter wsdlImporter = new WsdlImporter(metadataSet);
			wsdlImporter.State.Add("MetadataExchangeClientKey", client);
			foreach (ContractDescription contractDescription in contracts)
			{
				wsdlImporter.KnownContracts.Add(WsdlExporter.WsdlNamingHelper.GetPortTypeQName(contractDescription), contractDescription);
			}
			foreach (ContractDescription contract in contracts)
			{
				ServiceEndpointCollection serviceEndpointCollection2 = wsdlImporter.ImportEndpoints(contract);
				foreach (ServiceEndpoint item in serviceEndpointCollection2)
				{
					serviceEndpointCollection.Add(item);
				}
			}
			if (wsdlImporter.Errors.Count > 0)
			{
				MetadataResolver.TraceWsdlImportErrors(wsdlImporter);
			}
			return serviceEndpointCollection;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x00086800 File Offset: 0x00084A00
		private static void TraceWsdlImportErrors(WsdlImporter importer)
		{
			foreach (MetadataConversionError metadataConversionError in importer.Errors)
			{
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					Hashtable dictionary = new Hashtable(2)
					{
						{
							"IsWarning",
							metadataConversionError.IsWarning
						},
						{
							"Message",
							metadataConversionError.Message
						}
					};
					TraceUtility.TraceEvent(TraceEventType.Warning, 524349, SR.GetString("TraceCodeWsmexNonCriticalWsdlExportError"), new DictionaryTraceRecord(dictionary), null, null);
				}
			}
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x00086898 File Offset: 0x00084A98
		private static void ValidateContracts(IEnumerable<ContractDescription> contracts)
		{
			bool flag = true;
			Collection<XmlQualifiedName> collection = new Collection<XmlQualifiedName>();
			foreach (ContractDescription contractDescription in contracts)
			{
				flag = false;
				if (contractDescription == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxMetadataResolverKnownContractsCannotContainNull"));
				}
				XmlQualifiedName portTypeQName = WsdlExporter.WsdlNamingHelper.GetPortTypeQName(contractDescription);
				if (collection.Contains(portTypeQName))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxMetadataResolverKnownContractsUniqueQNames", new object[]
					{
						portTypeQName.Name,
						portTypeQName.Namespace
					}));
				}
				collection.Add(portTypeQName);
			}
			if (flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxMetadataResolverKnownContractsArgumentCannotBeEmpty"));
			}
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x0008695C File Offset: 0x00084B5C
		private static Collection<ContractDescription> CreateContractCollection(Type contract)
		{
			return new Collection<ContractDescription>
			{
				ContractDescription.GetContract(contract)
			};
		}

		// Token: 0x02000BA0 RID: 2976
		private class AsyncMetadataResolverHelper : AsyncResult
		{
			// Token: 0x060073A4 RID: 29604 RVA: 0x001AFA20 File Offset: 0x001ADC20
			internal AsyncMetadataResolverHelper(EndpointAddress address, MetadataExchangeClientMode mode, MetadataExchangeClient client, IEnumerable<ContractDescription> knownContracts, AsyncCallback callback, object asyncState) : base(callback, asyncState)
			{
				this.address = address;
				this.client = client;
				this.mode = mode;
				this.knownContracts = knownContracts;
				this.GetMetadataSetAsync();
			}

			// Token: 0x060073A5 RID: 29605 RVA: 0x001AFA50 File Offset: 0x001ADC50
			internal void GetMetadataSetAsync()
			{
				IAsyncResult asyncResult;
				if (this.mode == MetadataExchangeClientMode.HttpGet)
				{
					asyncResult = this.client.BeginGetMetadata(this.address.Uri, MetadataExchangeClientMode.HttpGet, Fx.ThunkCallback(new AsyncCallback(this.EndGetMetadataSet)), null);
				}
				else
				{
					asyncResult = this.client.BeginGetMetadata(this.address, Fx.ThunkCallback(new AsyncCallback(this.EndGetMetadataSet)), null);
				}
				if (asyncResult.CompletedSynchronously)
				{
					this.HandleResult(asyncResult);
					base.Complete(true);
				}
			}

			// Token: 0x060073A6 RID: 29606 RVA: 0x001AFACC File Offset: 0x001ADCCC
			internal void EndGetMetadataSet(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception exception = null;
				try
				{
					this.HandleResult(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				base.Complete(false, exception);
			}

			// Token: 0x060073A7 RID: 29607 RVA: 0x001AFB14 File Offset: 0x001ADD14
			private void HandleResult(IAsyncResult result)
			{
				MetadataSet metadataSet = this.client.EndGetMetadata(result);
				this.endpointCollection = MetadataResolver.ImportEndpoints(metadataSet, this.knownContracts, this.client);
			}

			// Token: 0x060073A8 RID: 29608 RVA: 0x001AFB48 File Offset: 0x001ADD48
			internal static ServiceEndpointCollection EndAsyncCall(IAsyncResult result)
			{
				MetadataResolver.AsyncMetadataResolverHelper asyncMetadataResolverHelper = AsyncResult.End<MetadataResolver.AsyncMetadataResolverHelper>(result);
				return asyncMetadataResolverHelper.endpointCollection;
			}

			// Token: 0x04004182 RID: 16770
			private MetadataExchangeClient client;

			// Token: 0x04004183 RID: 16771
			private EndpointAddress address;

			// Token: 0x04004184 RID: 16772
			private ServiceEndpointCollection endpointCollection;

			// Token: 0x04004185 RID: 16773
			private MetadataExchangeClientMode mode;

			// Token: 0x04004186 RID: 16774
			private IEnumerable<ContractDescription> knownContracts;
		}
	}
}
