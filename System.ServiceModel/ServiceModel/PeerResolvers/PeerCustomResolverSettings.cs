using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001C4 RID: 452
	public class PeerCustomResolverSettings
	{
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00035ACC File Offset: 0x00033CCC
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x00035AD4 File Offset: 0x00033CD4
		public EndpointAddress Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00035AE0 File Offset: 0x00033CE0
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x00035B2C File Offset: 0x00033D2C
		public Binding Binding
		{
			get
			{
				if (this.binding == null && !string.IsNullOrEmpty(this.bindingSection) && !string.IsNullOrEmpty(this.bindingConfiguration))
				{
					this.binding = ConfigLoader.LookupBinding(this.bindingSection, this.bindingConfiguration);
				}
				return this.binding;
			}
			set
			{
				this.binding = value;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00035B35 File Offset: 0x00033D35
		public bool IsBindingSpecified
		{
			get
			{
				return this.binding != null || (!string.IsNullOrEmpty(this.bindingSection) && !string.IsNullOrEmpty(this.bindingConfiguration));
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00035B5E File Offset: 0x00033D5E
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x00035B66 File Offset: 0x00033D66
		public PeerResolver Resolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00035B6F File Offset: 0x00033D6F
		// (set) Token: 0x06000ECB RID: 3787 RVA: 0x00035B77 File Offset: 0x00033D77
		internal string BindingSection
		{
			get
			{
				return this.bindingSection;
			}
			set
			{
				this.bindingSection = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00035B80 File Offset: 0x00033D80
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x00035B88 File Offset: 0x00033D88
		internal string BindingConfiguration
		{
			get
			{
				return this.bindingConfiguration;
			}
			set
			{
				this.bindingConfiguration = value;
			}
		}

		// Token: 0x04001781 RID: 6017
		private EndpointAddress address;

		// Token: 0x04001782 RID: 6018
		private Binding binding;

		// Token: 0x04001783 RID: 6019
		private string bindingSection;

		// Token: 0x04001784 RID: 6020
		private string bindingConfiguration;

		// Token: 0x04001785 RID: 6021
		private PeerResolver resolver;
	}
}
