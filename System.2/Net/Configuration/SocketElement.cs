using System;
using System.Configuration;
using System.Net.Sockets;

namespace System.Net.Configuration
{
	// Token: 0x02000348 RID: 840
	public sealed class SocketElement : ConfigurationElement
	{
		// Token: 0x06001E2A RID: 7722 RVA: 0x0008D970 File Offset: 0x0008BB70
		public SocketElement()
		{
			this.properties.Add(this.alwaysUseCompletionPortsForAccept);
			this.properties.Add(this.alwaysUseCompletionPortsForConnect);
			this.properties.Add(this.ipProtectionLevel);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0008DA24 File Offset: 0x0008BC24
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			try
			{
				ExceptionHelper.UnrestrictedSocketPermission.Demand();
			}
			catch (Exception inner)
			{
				throw new ConfigurationErrorsException(SR.GetString("net_config_element_permission", new object[]
				{
					"socket"
				}), inner);
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x0008DA7C File Offset: 0x0008BC7C
		// (set) Token: 0x06001E2D RID: 7725 RVA: 0x0008DA8F File Offset: 0x0008BC8F
		[ConfigurationProperty("alwaysUseCompletionPortsForAccept", DefaultValue = false)]
		public bool AlwaysUseCompletionPortsForAccept
		{
			get
			{
				return (bool)base[this.alwaysUseCompletionPortsForAccept];
			}
			set
			{
				base[this.alwaysUseCompletionPortsForAccept] = value;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x0008DAA3 File Offset: 0x0008BCA3
		// (set) Token: 0x06001E2F RID: 7727 RVA: 0x0008DAB6 File Offset: 0x0008BCB6
		[ConfigurationProperty("alwaysUseCompletionPortsForConnect", DefaultValue = false)]
		public bool AlwaysUseCompletionPortsForConnect
		{
			get
			{
				return (bool)base[this.alwaysUseCompletionPortsForConnect];
			}
			set
			{
				base[this.alwaysUseCompletionPortsForConnect] = value;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x0008DACA File Offset: 0x0008BCCA
		// (set) Token: 0x06001E31 RID: 7729 RVA: 0x0008DADD File Offset: 0x0008BCDD
		[ConfigurationProperty("ipProtectionLevel", DefaultValue = IPProtectionLevel.Unspecified)]
		public IPProtectionLevel IPProtectionLevel
		{
			get
			{
				return (IPProtectionLevel)base[this.ipProtectionLevel];
			}
			set
			{
				base[this.ipProtectionLevel] = value;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x0008DAF1 File Offset: 0x0008BCF1
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001CB6 RID: 7350
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001CB7 RID: 7351
		private readonly ConfigurationProperty alwaysUseCompletionPortsForConnect = new ConfigurationProperty("alwaysUseCompletionPortsForConnect", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001CB8 RID: 7352
		private readonly ConfigurationProperty alwaysUseCompletionPortsForAccept = new ConfigurationProperty("alwaysUseCompletionPortsForAccept", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001CB9 RID: 7353
		private readonly ConfigurationProperty ipProtectionLevel = new ConfigurationProperty("ipProtectionLevel", typeof(IPProtectionLevel), IPProtectionLevel.Unspecified, ConfigurationPropertyOptions.None);
	}
}
