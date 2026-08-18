using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000630 RID: 1584
	public interface IBindingConfigurationElement
	{
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06003CC2 RID: 15554
		TimeSpan CloseTimeout { get; }

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06003CC3 RID: 15555
		string Name { get; }

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06003CC4 RID: 15556
		TimeSpan OpenTimeout { get; }

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06003CC5 RID: 15557
		TimeSpan ReceiveTimeout { get; }

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06003CC6 RID: 15558
		TimeSpan SendTimeout { get; }

		// Token: 0x06003CC7 RID: 15559
		void ApplyConfiguration(Binding binding);
	}
}
