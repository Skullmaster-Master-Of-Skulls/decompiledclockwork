using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Description
{
	// Token: 0x02000412 RID: 1042
	public abstract class MetadataExporter
	{
		// Token: 0x060027EB RID: 10219 RVA: 0x00096997 File Offset: 0x00094B97
		internal MetadataExporter()
		{
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x000969C0 File Offset: 0x00094BC0
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x000969C8 File Offset: 0x00094BC8
		public PolicyVersion PolicyVersion
		{
			get
			{
				return this.policyVersion;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.policyVersion = value;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x000969E4 File Offset: 0x00094BE4
		public Collection<MetadataConversionError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x000969EC File Offset: 0x00094BEC
		public Dictionary<object, object> State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x060027F0 RID: 10224
		public abstract void ExportContract(ContractDescription contract);

		// Token: 0x060027F1 RID: 10225
		public abstract void ExportEndpoint(ServiceEndpoint endpoint);

		// Token: 0x060027F2 RID: 10226
		public abstract MetadataSet GetGeneratedMetadata();

		// Token: 0x060027F3 RID: 10227 RVA: 0x000969F4 File Offset: 0x00094BF4
		internal PolicyConversionContext ExportPolicy(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
			PolicyConversionContext policyConversionContext = new MetadataExporter.ExportedPolicyConversionContext(endpoint, bindingParameters);
			foreach (IPolicyExportExtension policyExportExtension in endpoint.Binding.CreateBindingElements().FindAll<IPolicyExportExtension>())
			{
				try
				{
					policyExportExtension.ExportPolicy(this, policyConversionContext);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateExtensionException(policyExportExtension, ex));
				}
			}
			return policyConversionContext;
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x00096A84 File Offset: 0x00094C84
		protected internal PolicyConversionContext ExportPolicy(ServiceEndpoint endpoint)
		{
			return this.ExportPolicy(endpoint, null);
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x00096A90 File Offset: 0x00094C90
		private Exception CreateExtensionException(IPolicyExportExtension exporter, Exception e)
		{
			string @string = SR.GetString("PolicyExtensionExportError", new object[]
			{
				exporter.GetType(),
				e.Message
			});
			return new InvalidOperationException(@string, e);
		}

		// Token: 0x04002201 RID: 8705
		private PolicyVersion policyVersion = PolicyVersion.Policy12;

		// Token: 0x04002202 RID: 8706
		private readonly Collection<MetadataConversionError> errors = new Collection<MetadataConversionError>();

		// Token: 0x04002203 RID: 8707
		private readonly Dictionary<object, object> state = new Dictionary<object, object>();

		// Token: 0x02000BCC RID: 3020
		private sealed class ExportedPolicyConversionContext : PolicyConversionContext
		{
			// Token: 0x060074DE RID: 29918 RVA: 0x001B44BC File Offset: 0x001B26BC
			internal ExportedPolicyConversionContext(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) : base(endpoint)
			{
				this.bindingElements = endpoint.Binding.CreateBindingElements();
				this.bindingAssertions = new PolicyAssertionCollection();
				this.operationBindingAssertions = new Dictionary<OperationDescription, PolicyAssertionCollection>();
				this.messageBindingAssertions = new Dictionary<MessageDescription, PolicyAssertionCollection>();
				this.faultBindingAssertions = new Dictionary<FaultDescription, PolicyAssertionCollection>();
				this.bindingParameters = bindingParameters;
			}

			// Token: 0x17001AEE RID: 6894
			// (get) Token: 0x060074DF RID: 29919 RVA: 0x001B4514 File Offset: 0x001B2714
			public override BindingElementCollection BindingElements
			{
				get
				{
					return this.bindingElements;
				}
			}

			// Token: 0x17001AEF RID: 6895
			// (get) Token: 0x060074E0 RID: 29920 RVA: 0x001B451C File Offset: 0x001B271C
			internal override BindingParameterCollection BindingParameters
			{
				get
				{
					return this.bindingParameters;
				}
			}

			// Token: 0x060074E1 RID: 29921 RVA: 0x001B4524 File Offset: 0x001B2724
			public override PolicyAssertionCollection GetBindingAssertions()
			{
				return this.bindingAssertions;
			}

			// Token: 0x060074E2 RID: 29922 RVA: 0x001B452C File Offset: 0x001B272C
			public override PolicyAssertionCollection GetOperationBindingAssertions(OperationDescription operation)
			{
				Dictionary<OperationDescription, PolicyAssertionCollection> obj = this.operationBindingAssertions;
				lock (obj)
				{
					if (!this.operationBindingAssertions.ContainsKey(operation))
					{
						this.operationBindingAssertions.Add(operation, new PolicyAssertionCollection());
					}
				}
				return this.operationBindingAssertions[operation];
			}

			// Token: 0x060074E3 RID: 29923 RVA: 0x001B4594 File Offset: 0x001B2794
			public override PolicyAssertionCollection GetMessageBindingAssertions(MessageDescription message)
			{
				Dictionary<MessageDescription, PolicyAssertionCollection> obj = this.messageBindingAssertions;
				lock (obj)
				{
					if (!this.messageBindingAssertions.ContainsKey(message))
					{
						this.messageBindingAssertions.Add(message, new PolicyAssertionCollection());
					}
				}
				return this.messageBindingAssertions[message];
			}

			// Token: 0x060074E4 RID: 29924 RVA: 0x001B45FC File Offset: 0x001B27FC
			public override PolicyAssertionCollection GetFaultBindingAssertions(FaultDescription fault)
			{
				Dictionary<FaultDescription, PolicyAssertionCollection> obj = this.faultBindingAssertions;
				lock (obj)
				{
					if (!this.faultBindingAssertions.ContainsKey(fault))
					{
						this.faultBindingAssertions.Add(fault, new PolicyAssertionCollection());
					}
				}
				return this.faultBindingAssertions[fault];
			}

			// Token: 0x04004219 RID: 16921
			private readonly BindingElementCollection bindingElements;

			// Token: 0x0400421A RID: 16922
			private PolicyAssertionCollection bindingAssertions;

			// Token: 0x0400421B RID: 16923
			private Dictionary<OperationDescription, PolicyAssertionCollection> operationBindingAssertions;

			// Token: 0x0400421C RID: 16924
			private Dictionary<MessageDescription, PolicyAssertionCollection> messageBindingAssertions;

			// Token: 0x0400421D RID: 16925
			private Dictionary<FaultDescription, PolicyAssertionCollection> faultBindingAssertions;

			// Token: 0x0400421E RID: 16926
			private BindingParameterCollection bindingParameters;
		}
	}
}
