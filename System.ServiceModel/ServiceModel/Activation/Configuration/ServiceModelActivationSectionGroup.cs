using System;
using System.Configuration;

namespace System.ServiceModel.Activation.Configuration
{
	// Token: 0x020005D5 RID: 1493
	public sealed class ServiceModelActivationSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x060039FD RID: 14845 RVA: 0x000DFD24 File Offset: 0x000DDF24
		public DiagnosticSection Diagnostics
		{
			get
			{
				return (DiagnosticSection)base.Sections["diagnostics"];
			}
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x000DFD3B File Offset: 0x000DDF3B
		public static ServiceModelActivationSectionGroup GetSectionGroup(Configuration config)
		{
			if (config == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("config");
			}
			return (ServiceModelActivationSectionGroup)config.SectionGroups["system.serviceModel.activation"];
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x060039FF RID: 14847 RVA: 0x000DFD65 File Offset: 0x000DDF65
		public NetPipeSection NetPipe
		{
			get
			{
				return (NetPipeSection)base.Sections["net.pipe"];
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06003A00 RID: 14848 RVA: 0x000DFD7C File Offset: 0x000DDF7C
		public NetTcpSection NetTcp
		{
			get
			{
				return (NetTcpSection)base.Sections["net.tcp"];
			}
		}
	}
}
