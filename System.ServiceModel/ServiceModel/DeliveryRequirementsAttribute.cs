using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel
{
	// Token: 0x020000CC RID: 204
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
	public sealed class DeliveryRequirementsAttribute : Attribute, IContractBehavior, IContractBehaviorAttribute
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0001505F File Offset: 0x0001325F
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00015067 File Offset: 0x00013267
		public Type TargetContract
		{
			get
			{
				return this.contractType;
			}
			set
			{
				this.contractType = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00015070 File Offset: 0x00013270
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00015078 File Offset: 0x00013278
		public QueuedDeliveryRequirementsMode QueuedDeliveryRequirements
		{
			get
			{
				return this.queuedDeliveryRequirements;
			}
			set
			{
				if (QueuedDeliveryRequirementsModeHelper.IsDefined(value))
				{
					this.queuedDeliveryRequirements = value;
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001509E File Offset: 0x0001329E
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x000150A6 File Offset: 0x000132A6
		public bool RequireOrderedDelivery
		{
			get
			{
				return this.requireOrderedDelivery;
			}
			set
			{
				this.requireOrderedDelivery = value;
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000150AF File Offset: 0x000132AF
		void IContractBehavior.Validate(ContractDescription description, ServiceEndpoint endpoint)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			this.ValidateEndpoint(endpoint);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000150DE File Offset: 0x000132DE
		void IContractBehavior.AddBindingParameters(ContractDescription description, ServiceEndpoint endpoint, BindingParameterCollection parameters)
		{
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000150E0 File Offset: 0x000132E0
		void IContractBehavior.ApplyClientBehavior(ContractDescription description, ServiceEndpoint endpoint, ClientRuntime proxy)
		{
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000150E2 File Offset: 0x000132E2
		void IContractBehavior.ApplyDispatchBehavior(ContractDescription description, ServiceEndpoint endpoint, DispatchRuntime dispatch)
		{
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000150E4 File Offset: 0x000132E4
		private void ValidateEndpoint(ServiceEndpoint endpoint)
		{
			string name = endpoint.Contract.ContractType.Name;
			this.EnsureQueuedDeliveryRequirements(name, endpoint.Binding);
			this.EnsureOrderedDeliveryRequirements(name, endpoint.Binding);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001511C File Offset: 0x0001331C
		private void EnsureQueuedDeliveryRequirements(string name, Binding binding)
		{
			if (this.QueuedDeliveryRequirements == QueuedDeliveryRequirementsMode.Required || this.QueuedDeliveryRequirements == QueuedDeliveryRequirementsMode.NotAllowed)
			{
				IBindingDeliveryCapabilities property = binding.GetProperty<IBindingDeliveryCapabilities>(new BindingParameterCollection());
				if (property == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SinceTheBindingForDoesnTSupportIBindingCapabilities2_1", new object[]
					{
						name
					})));
				}
				bool queuedDelivery = property.QueuedDelivery;
				if (this.QueuedDeliveryRequirements == QueuedDeliveryRequirementsMode.Required && !queuedDelivery)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BindingRequirementsAttributeRequiresQueuedDelivery1", new object[]
					{
						name
					})));
				}
				if (this.QueuedDeliveryRequirements == QueuedDeliveryRequirementsMode.NotAllowed && queuedDelivery)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BindingRequirementsAttributeDisallowsQueuedDelivery1", new object[]
					{
						name
					})));
				}
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000151DC File Offset: 0x000133DC
		private void EnsureOrderedDeliveryRequirements(string name, Binding binding)
		{
			if (this.RequireOrderedDelivery)
			{
				IBindingDeliveryCapabilities property = binding.GetProperty<IBindingDeliveryCapabilities>(new BindingParameterCollection());
				if (property == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SinceTheBindingForDoesnTSupportIBindingCapabilities1_1", new object[]
					{
						name
					})));
				}
				if (!property.AssuresOrderedDelivery)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TheBindingForDoesnTSupportOrderedDelivery1", new object[]
					{
						name
					})));
				}
			}
		}

		// Token: 0x0400098B RID: 2443
		private Type contractType;

		// Token: 0x0400098C RID: 2444
		private QueuedDeliveryRequirementsMode queuedDeliveryRequirements;

		// Token: 0x0400098D RID: 2445
		private bool requireOrderedDelivery;
	}
}
