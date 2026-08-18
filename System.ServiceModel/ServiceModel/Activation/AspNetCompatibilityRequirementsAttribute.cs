using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005B9 RID: 1465
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class AspNetCompatibilityRequirementsAttribute : Attribute, IServiceBehavior
	{
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x000DE31C File Offset: 0x000DC51C
		// (set) Token: 0x0600393C RID: 14652 RVA: 0x000DE324 File Offset: 0x000DC524
		public AspNetCompatibilityRequirementsMode RequirementsMode
		{
			get
			{
				return this.requirementsMode;
			}
			set
			{
				AspNetCompatibilityRequirementsModeHelper.Validate(value);
				this.requirementsMode = value;
			}
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000DE333 File Offset: 0x000DC533
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000DE335 File Offset: 0x000DC535
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			AspNetEnvironment.Current.ValidateCompatibilityRequirements(this.RequirementsMode);
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000DE35A File Offset: 0x000DC55A
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x040029DD RID: 10717
		private AspNetCompatibilityRequirementsMode requirementsMode = OSEnvironmentHelper.IsApplicationTargeting45 ? AspNetCompatibilityRequirementsMode.Allowed : AspNetCompatibilityRequirementsMode.NotAllowed;
	}
}
