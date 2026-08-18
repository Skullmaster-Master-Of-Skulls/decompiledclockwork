using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006D2 RID: 1746
	public sealed class ServiceModelSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x060043A1 RID: 17313 RVA: 0x000FFC5A File Offset: 0x000FDE5A
		public BehaviorsSection Behaviors
		{
			get
			{
				return (BehaviorsSection)base.Sections["behaviors"];
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x060043A2 RID: 17314 RVA: 0x000FFC71 File Offset: 0x000FDE71
		public BindingsSection Bindings
		{
			get
			{
				return (BindingsSection)base.Sections["bindings"];
			}
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x060043A3 RID: 17315 RVA: 0x000FFC88 File Offset: 0x000FDE88
		public ClientSection Client
		{
			get
			{
				return (ClientSection)base.Sections["client"];
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x060043A4 RID: 17316 RVA: 0x000FFC9F File Offset: 0x000FDE9F
		public ComContractsSection ComContracts
		{
			get
			{
				return (ComContractsSection)base.Sections["comContracts"];
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x060043A5 RID: 17317 RVA: 0x000FFCB6 File Offset: 0x000FDEB6
		public CommonBehaviorsSection CommonBehaviors
		{
			get
			{
				return (CommonBehaviorsSection)base.Sections["commonBehaviors"];
			}
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x060043A6 RID: 17318 RVA: 0x000FFCCD File Offset: 0x000FDECD
		public DiagnosticSection Diagnostic
		{
			get
			{
				return (DiagnosticSection)base.Sections["diagnostics"];
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x060043A7 RID: 17319 RVA: 0x000FFCE4 File Offset: 0x000FDEE4
		public ServiceHostingEnvironmentSection ServiceHostingEnvironment
		{
			get
			{
				return (ServiceHostingEnvironmentSection)base.Sections["serviceHostingEnvironment"];
			}
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x060043A8 RID: 17320 RVA: 0x000FFCFB File Offset: 0x000FDEFB
		public ExtensionsSection Extensions
		{
			get
			{
				return (ExtensionsSection)base.Sections["extensions"];
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x060043A9 RID: 17321 RVA: 0x000FFD12 File Offset: 0x000FDF12
		public ProtocolMappingSection ProtocolMapping
		{
			get
			{
				return (ProtocolMappingSection)base.Sections["protocolMapping"];
			}
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x060043AA RID: 17322 RVA: 0x000FFD29 File Offset: 0x000FDF29
		public ServicesSection Services
		{
			get
			{
				return (ServicesSection)base.Sections["services"];
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x000FFD40 File Offset: 0x000FDF40
		public StandardEndpointsSection StandardEndpoints
		{
			get
			{
				return (StandardEndpointsSection)base.Sections["standardEndpoints"];
			}
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x000FFD57 File Offset: 0x000FDF57
		public static ServiceModelSectionGroup GetSectionGroup(Configuration config)
		{
			if (config == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("config");
			}
			return (ServiceModelSectionGroup)config.SectionGroups["system.serviceModel"];
		}
	}
}
