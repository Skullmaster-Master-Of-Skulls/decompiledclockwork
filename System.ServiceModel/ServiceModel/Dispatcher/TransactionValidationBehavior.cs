using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005B4 RID: 1460
	internal class TransactionValidationBehavior : IEndpointBehavior, IServiceBehavior
	{
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06003902 RID: 14594 RVA: 0x000DC964 File Offset: 0x000DAB64
		internal static TransactionValidationBehavior Instance
		{
			get
			{
				if (TransactionValidationBehavior.instance == null)
				{
					TransactionValidationBehavior.instance = new TransactionValidationBehavior();
				}
				return TransactionValidationBehavior.instance;
			}
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x000DC97C File Offset: 0x000DAB7C
		private TransactionValidationBehavior()
		{
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000DC984 File Offset: 0x000DAB84
		private void ValidateTransactionFlowRequired(string resource, string name, ServiceEndpoint endpoint)
		{
			bool flag = false;
			for (int i = 0; i < endpoint.Contract.Operations.Count; i++)
			{
				OperationDescription operationDescription = endpoint.Contract.Operations[i];
				TransactionFlowAttribute transactionFlowAttribute = operationDescription.Behaviors.Find<TransactionFlowAttribute>();
				if (transactionFlowAttribute != null && transactionFlowAttribute.Transactions == TransactionFlowOption.Mandatory)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				CustomBinding customBinding = new CustomBinding(endpoint.Binding);
				TransactionFlowBindingElement transactionFlowBindingElement = customBinding.Elements.Find<TransactionFlowBindingElement>();
				if (transactionFlowBindingElement == null || !transactionFlowBindingElement.Transactions)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, SR.GetString(resource), new object[]
					{
						name,
						customBinding.Name
					})));
				}
			}
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000DCA3C File Offset: 0x000DAC3C
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
			if (serviceEndpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceEndpoint");
			}
			this.ValidateTransactionFlowRequired("ChannelHasAtLeastOneOperationWithTransactionFlowEnabled", serviceEndpoint.Contract.Name, serviceEndpoint);
			this.EnsureNoOneWayTransactions(serviceEndpoint);
			this.ValidateNoMSMQandTransactionFlow(serviceEndpoint);
			this.ValidateCallbackBehaviorAttributeWithNoScopeRequired(serviceEndpoint);
			OperationDescription autoCompleteFalseOperation = this.GetAutoCompleteFalseOperation(serviceEndpoint);
			if (autoCompleteFalseOperation != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTransactionAutoCompleteFalseOnCallbackContract", new object[]
				{
					autoCompleteFalseOperation.Name,
					serviceEndpoint.Contract.Name
				})));
			}
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000DCACC File Offset: 0x000DACCC
		private void ValidateCallbackBehaviorAttributeWithNoScopeRequired(ServiceEndpoint endpoint)
		{
			if (!this.HasTransactedOperations(endpoint))
			{
				CallbackBehaviorAttribute callbackBehaviorAttribute = endpoint.Behaviors.Find<CallbackBehaviorAttribute>();
				if (callbackBehaviorAttribute != null)
				{
					if (callbackBehaviorAttribute.TransactionTimeoutSet)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTransactionTransactionTimeoutNeedsScope", new object[]
						{
							endpoint.Contract.Name
						})));
					}
					if (callbackBehaviorAttribute.IsolationLevelSet)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTransactionIsolationLevelNeedsScope", new object[]
						{
							endpoint.Contract.Name
						})));
					}
				}
			}
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000DCB5D File Offset: 0x000DAD5D
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000DCB5F File Offset: 0x000DAD5F
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x000DCB61 File Offset: 0x000DAD61
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000DCB63 File Offset: 0x000DAD63
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000DCB65 File Offset: 0x000DAD65
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription service, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x000DCB68 File Offset: 0x000DAD68
		void IServiceBehavior.Validate(ServiceDescription service, ServiceHostBase serviceHostBase)
		{
			if (service == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("service");
			}
			this.ValidateNotConcurrentWhenReleaseServiceInstanceOnTxComplete(service);
			bool singleThreaded = this.IsSingleThreaded(service);
			for (int i = 0; i < service.Endpoints.Count; i++)
			{
				ServiceEndpoint serviceEndpoint = service.Endpoints[i];
				this.ValidateTransactionFlowRequired("ServiceHasAtLeastOneOperationWithTransactionFlowEnabled", service.Name, serviceEndpoint);
				this.EnsureNoOneWayTransactions(serviceEndpoint);
				this.ValidateNoMSMQandTransactionFlow(serviceEndpoint);
				ContractDescription contract = serviceEndpoint.Contract;
				for (int j = 0; j < contract.Operations.Count; j++)
				{
					OperationDescription operation = contract.Operations[j];
					this.ValidateScopeRequiredAndAutoComplete(operation, singleThreaded, contract.Name);
				}
				this.ValidateAutoCompleteFalseRequirements(service, serviceEndpoint);
			}
			this.ValidateServiceBehaviorAttributeWithNoScopeRequired(service);
			this.ValidateTransactionAutoCompleteOnSessionCloseHasSession(service);
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x000DCC34 File Offset: 0x000DAE34
		private void ValidateAutoCompleteFalseRequirements(ServiceDescription service, ServiceEndpoint endpoint)
		{
			OperationDescription autoCompleteFalseOperation = this.GetAutoCompleteFalseOperation(endpoint);
			if (autoCompleteFalseOperation != null)
			{
				ServiceBehaviorAttribute serviceBehaviorAttribute = service.Behaviors.Find<ServiceBehaviorAttribute>();
				if (serviceBehaviorAttribute != null)
				{
					InstanceContextMode instanceContextMode = serviceBehaviorAttribute.InstanceContextMode;
					if (instanceContextMode != InstanceContextMode.PerSession)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTransactionAutoCompleteFalseAndInstanceContextMode", new object[]
						{
							endpoint.Contract.Name,
							autoCompleteFalseOperation.Name
						})));
					}
				}
				if (!autoCompleteFalseOperation.IsInsideTransactedReceiveScope && !this.RequiresSessions(endpoint))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTransactionAutoCompleteFalseAndSupportsSession", new object[]
					{
						endpoint.Contract.Name,
						autoCompleteFalseOperation.Name
					})));
				}
			}
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x000DCCE8 File Offset: 0x000DAEE8
		private OperationDescription GetAutoCompleteFalseOperation(ServiceEndpoint endpoint)
		{
			foreach (OperationDescription operationDescription in endpoint.Contract.Operations)
			{
				if (!this.IsAutoComplete(operationDescription))
				{
					return operationDescription;
				}
			}
			return null;
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x000DCD44 File Offset: 0x000DAF44
		private void ValidateTransactionAutoCompleteOnSessionCloseHasSession(ServiceDescription service)
		{
			ServiceBehaviorAttribute serviceBehaviorAttribute = service.Behaviors.Find<ServiceBehaviorAttribute>();
			if (serviceBehaviorAttribute != null)
			{
				InstanceContextMode instanceContextMode = serviceBehaviorAttribute.InstanceContextMode;
				if (serviceBehaviorAttribute.TransactionAutoCompleteOnSessionClose && instanceContextMode != InstanceContextMode.PerSession)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTransactionAutoCompleteOnSessionCloseNoSession", new object[]
					{
						service.Name
					})));
				}
			}
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000DCD9C File Offset: 0x000DAF9C
		private void ValidateServiceBehaviorAttributeWithNoScopeRequired(ServiceDescription service)
		{
			if (!this.HasTransactedOperations(service))
			{
				ServiceBehaviorAttribute serviceBehaviorAttribute = service.Behaviors.Find<ServiceBehaviorAttribute>();
				if (serviceBehaviorAttribute != null)
				{
					if (serviceBehaviorAttribute.TransactionTimeoutSet)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTransactionTransactionTimeoutNeedsScope", new object[]
						{
							service.Name
						})));
					}
					if (serviceBehaviorAttribute.IsolationLevelSet)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTransactionIsolationLevelNeedsScope", new object[]
						{
							service.Name
						})));
					}
					if (serviceBehaviorAttribute.ReleaseServiceInstanceOnTransactionCompleteSet)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTransactionReleaseServiceInstanceOnTransactionCompleteNeedsScope", new object[]
						{
							service.Name
						})));
					}
					if (serviceBehaviorAttribute.TransactionAutoCompleteOnSessionCloseSet)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTransactionTransactionAutoCompleteOnSessionCloseNeedsScope", new object[]
						{
							service.Name
						})));
					}
				}
			}
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000DCE8C File Offset: 0x000DB08C
		private void EnsureNoOneWayTransactions(ServiceEndpoint endpoint)
		{
			CustomBinding customBinding = new CustomBinding(endpoint.Binding);
			TransactionFlowBindingElement transactionFlowBindingElement = customBinding.Elements.Find<TransactionFlowBindingElement>();
			if (transactionFlowBindingElement != null)
			{
				for (int i = 0; i < endpoint.Contract.Operations.Count; i++)
				{
					OperationDescription operationDescription = endpoint.Contract.Operations[i];
					if (operationDescription.IsOneWay)
					{
						TransactionFlowAttribute transactionFlowAttribute = operationDescription.Behaviors.Find<TransactionFlowAttribute>();
						TransactionFlowOption option;
						if (transactionFlowAttribute != null)
						{
							option = transactionFlowAttribute.Transactions;
						}
						else
						{
							option = TransactionFlowOption.NotAllowed;
						}
						if (TransactionFlowOptionHelper.AllowedOrRequired(option))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxOneWayAndTransactionsIncompatible", new object[]
							{
								endpoint.Contract.Name,
								operationDescription.Name
							})));
						}
					}
				}
			}
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000DCF50 File Offset: 0x000DB150
		private bool HasTransactedOperations(ServiceDescription service)
		{
			for (int i = 0; i < service.Endpoints.Count; i++)
			{
				if (this.HasTransactedOperations(service.Endpoints[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000DCF8C File Offset: 0x000DB18C
		private bool HasTransactedOperations(ServiceEndpoint endpoint)
		{
			for (int i = 0; i < endpoint.Contract.Operations.Count; i++)
			{
				OperationDescription operationDescription = endpoint.Contract.Operations[i];
				OperationBehaviorAttribute operationBehaviorAttribute = operationDescription.Behaviors.Find<OperationBehaviorAttribute>();
				if (operationBehaviorAttribute != null && operationBehaviorAttribute.TransactionScopeRequired)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000DCFE0 File Offset: 0x000DB1E0
		private bool IsSingleThreaded(ServiceDescription service)
		{
			ServiceBehaviorAttribute serviceBehaviorAttribute = service.Behaviors.Find<ServiceBehaviorAttribute>();
			return serviceBehaviorAttribute == null || serviceBehaviorAttribute.ConcurrencyMode == ConcurrencyMode.Single;
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000DD008 File Offset: 0x000DB208
		private bool IsAutoComplete(OperationDescription operation)
		{
			OperationBehaviorAttribute operationBehaviorAttribute = operation.Behaviors.Find<OperationBehaviorAttribute>();
			return operationBehaviorAttribute == null || operationBehaviorAttribute.TransactionAutoComplete;
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000DD02C File Offset: 0x000DB22C
		private bool RequiresSessions(ServiceEndpoint endpoint)
		{
			return endpoint.Contract.SessionMode == SessionMode.Required;
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000DD03C File Offset: 0x000DB23C
		private void ValidateScopeRequiredAndAutoComplete(OperationDescription operation, bool singleThreaded, string contractName)
		{
			OperationBehaviorAttribute operationBehaviorAttribute = operation.Behaviors.Find<OperationBehaviorAttribute>();
			if (operationBehaviorAttribute != null && !singleThreaded && !operationBehaviorAttribute.TransactionAutoComplete)
			{
				string name = "SFxTransactionNonConcurrentOrAutoComplete2";
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString(name, new object[]
				{
					contractName,
					operation.Name
				})));
			}
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000DD094 File Offset: 0x000DB294
		private void ValidateNoMSMQandTransactionFlow(ServiceEndpoint endpoint)
		{
			BindingElementCollection bindingElementCollection = endpoint.Binding.CreateBindingElements();
			if (bindingElementCollection.Find<TransactionFlowBindingElement>() != null && bindingElementCollection.Find<MsmqTransportBindingElement>() != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTransactionFlowAndMSMQ", new object[]
				{
					endpoint.Address.Uri.AbsoluteUri
				})));
			}
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000DD0F0 File Offset: 0x000DB2F0
		private void ValidateNotConcurrentWhenReleaseServiceInstanceOnTxComplete(ServiceDescription service)
		{
			ServiceBehaviorAttribute serviceBehaviorAttribute = service.Behaviors.Find<ServiceBehaviorAttribute>();
			if (serviceBehaviorAttribute != null && this.HasTransactedOperations(service) && serviceBehaviorAttribute.ReleaseServiceInstanceOnTransactionComplete && !this.IsSingleThreaded(service))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTransactionNonConcurrentOrReleaseServiceInstanceOnTxComplete", new object[]
				{
					service.Name
				})));
			}
		}

		// Token: 0x040029C4 RID: 10692
		private static TransactionValidationBehavior instance;
	}
}
