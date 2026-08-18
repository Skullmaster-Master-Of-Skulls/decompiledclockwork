using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.MsmqIntegration;

namespace System.ServiceModel.Description
{
	// Token: 0x020003F5 RID: 1013
	[DebuggerDisplay("Address={address}")]
	[DebuggerDisplay("Name={name}")]
	[__DynamicallyInvokable]
	public class ServiceEndpoint
	{
		// Token: 0x06002621 RID: 9761 RVA: 0x00089960 File Offset: 0x00087B60
		[__DynamicallyInvokable]
		public ServiceEndpoint(ContractDescription contract)
		{
			if (contract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contract");
			}
			this.contract = contract;
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x00089982 File Offset: 0x00087B82
		[__DynamicallyInvokable]
		public ServiceEndpoint(ContractDescription contract, Binding binding, EndpointAddress address)
		{
			if (contract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contract");
			}
			this.contract = contract;
			this.binding = binding;
			this.address = address;
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x000899B2 File Offset: 0x00087BB2
		// (set) Token: 0x06002624 RID: 9764 RVA: 0x000899BA File Offset: 0x00087BBA
		[__DynamicallyInvokable]
		public EndpointAddress Address
		{
			[__DynamicallyInvokable]
			get
			{
				return this.address;
			}
			[__DynamicallyInvokable]
			set
			{
				this.address = value;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x000899C3 File Offset: 0x00087BC3
		[__DynamicallyInvokable]
		public KeyedCollection<Type, IEndpointBehavior> EndpointBehaviors
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Behaviors;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x000899CB File Offset: 0x00087BCB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public KeyedByTypeCollection<IEndpointBehavior> Behaviors
		{
			get
			{
				if (this.behaviors == null)
				{
					this.behaviors = new KeyedByTypeCollection<IEndpointBehavior>();
				}
				return this.behaviors;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x000899E6 File Offset: 0x00087BE6
		// (set) Token: 0x06002628 RID: 9768 RVA: 0x000899EE File Offset: 0x00087BEE
		[__DynamicallyInvokable]
		public Binding Binding
		{
			[__DynamicallyInvokable]
			get
			{
				return this.binding;
			}
			[__DynamicallyInvokable]
			set
			{
				this.binding = value;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x000899F7 File Offset: 0x00087BF7
		// (set) Token: 0x0600262A RID: 9770 RVA: 0x000899FF File Offset: 0x00087BFF
		[__DynamicallyInvokable]
		public ContractDescription Contract
		{
			[__DynamicallyInvokable]
			get
			{
				return this.contract;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.contract = value;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x00089A1B File Offset: 0x00087C1B
		// (set) Token: 0x0600262C RID: 9772 RVA: 0x00089A23 File Offset: 0x00087C23
		public bool IsSystemEndpoint { get; set; }

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x00089A2C File Offset: 0x00087C2C
		// (set) Token: 0x0600262E RID: 9774 RVA: 0x00089AA1 File Offset: 0x00087CA1
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				if (!XmlName.IsNullOrEmpty(this.name))
				{
					return this.name.EncodedName;
				}
				if (this.binding != null)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
					{
						new XmlName(this.Binding.Name).EncodedName,
						this.Contract.Name
					});
				}
				return this.Contract.Name;
			}
			[__DynamicallyInvokable]
			set
			{
				this.name = new XmlName(value, true);
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x00089AB0 File Offset: 0x00087CB0
		// (set) Token: 0x06002630 RID: 9776 RVA: 0x00089AE2 File Offset: 0x00087CE2
		public Uri ListenUri
		{
			get
			{
				if (!(this.listenUri == null))
				{
					return this.listenUri;
				}
				if (this.address == null)
				{
					return null;
				}
				return this.address.Uri;
			}
			set
			{
				if (value != null && !value.IsAbsoluteUri)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("UriMustBeAbsolute"));
				}
				this.listenUri = value;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x00089B16 File Offset: 0x00087D16
		// (set) Token: 0x06002632 RID: 9778 RVA: 0x00089B1E File Offset: 0x00087D1E
		public ListenUriMode ListenUriMode
		{
			get
			{
				return this.listenUriMode;
			}
			set
			{
				if (!ListenUriModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.listenUriMode = value;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x00089B44 File Offset: 0x00087D44
		internal string Id
		{
			get
			{
				if (this.id == null)
				{
					this.id = Guid.NewGuid().ToString();
				}
				return this.id;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x00089B78 File Offset: 0x00087D78
		// (set) Token: 0x06002635 RID: 9781 RVA: 0x00089B80 File Offset: 0x00087D80
		internal Uri UnresolvedAddress { get; set; }

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x00089B89 File Offset: 0x00087D89
		// (set) Token: 0x06002637 RID: 9783 RVA: 0x00089B91 File Offset: 0x00087D91
		internal Uri UnresolvedListenUri { get; set; }

		// Token: 0x06002638 RID: 9784 RVA: 0x00089B9C File Offset: 0x00087D9C
		internal void EnsureInvariants()
		{
			if (this.Binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AChannelServiceEndpointSBindingIsNull0")));
			}
			if (this.Contract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AChannelServiceEndpointSContractIsNull0")));
			}
			this.Contract.EnsureInvariants();
			this.Binding.EnsureInvariants(this.Contract.Name);
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00089C0E File Offset: 0x00087E0E
		internal void ValidateForClient()
		{
			this.Validate(true, false);
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x00089C18 File Offset: 0x00087E18
		internal void ValidateForService(bool runOperationValidators)
		{
			this.Validate(runOperationValidators, true);
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x00089C22 File Offset: 0x00087E22
		// (set) Token: 0x0600263C RID: 9788 RVA: 0x00089C2A File Offset: 0x00087E2A
		internal bool IsFullyConfigured
		{
			get
			{
				return this.isEndpointFullyConfigured;
			}
			set
			{
				this.isEndpointFullyConfigured = value;
			}
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x00089C33 File Offset: 0x00087E33
		internal bool InternalIsSystemEndpoint(ServiceDescription description)
		{
			return ServiceMetadataBehavior.IsMetadataEndpoint(description, this) || this.IsSystemEndpoint;
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x00089C48 File Offset: 0x00087E48
		private void Validate(bool runOperationValidators, bool isForService)
		{
			ContractDescription contractDescription = this.Contract;
			for (int i = 0; i < contractDescription.Behaviors.Count; i++)
			{
				IContractBehavior contractBehavior = contractDescription.Behaviors[i];
				contractBehavior.Validate(contractDescription, this);
			}
			if (!isForService)
			{
				((IEndpointBehavior)PartialTrustValidationBehavior.Instance).Validate(this);
				((IEndpointBehavior)PeerValidationBehavior.Instance).Validate(this);
				((IEndpointBehavior)TransactionValidationBehavior.Instance).Validate(this);
				((IEndpointBehavior)SecurityValidationBehavior.Instance).Validate(this);
				((IEndpointBehavior)MsmqIntegrationValidationBehavior.Instance).Validate(this);
			}
			for (int j = 0; j < this.Behaviors.Count; j++)
			{
				IEndpointBehavior endpointBehavior = this.Behaviors[j];
				endpointBehavior.Validate(this);
			}
			if (runOperationValidators)
			{
				for (int k = 0; k < contractDescription.Operations.Count; k++)
				{
					OperationDescription operationDescription = contractDescription.Operations[k];
					TaskOperationDescriptionValidator.Validate(operationDescription, isForService);
					for (int l = 0; l < operationDescription.Behaviors.Count; l++)
					{
						IOperationBehavior operationBehavior = operationDescription.Behaviors[l];
						operationBehavior.Validate(operationDescription);
					}
				}
			}
		}

		// Token: 0x0400217C RID: 8572
		private EndpointAddress address;

		// Token: 0x0400217D RID: 8573
		private Binding binding;

		// Token: 0x0400217E RID: 8574
		private ContractDescription contract;

		// Token: 0x0400217F RID: 8575
		private Uri listenUri;

		// Token: 0x04002180 RID: 8576
		private ListenUriMode listenUriMode;

		// Token: 0x04002181 RID: 8577
		private KeyedByTypeCollection<IEndpointBehavior> behaviors;

		// Token: 0x04002182 RID: 8578
		private string id;

		// Token: 0x04002183 RID: 8579
		private XmlName name;

		// Token: 0x04002184 RID: 8580
		private bool isEndpointFullyConfigured;
	}
}
