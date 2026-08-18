using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000668 RID: 1640
	public sealed class SocketElement : ConfigurationElement
	{
		// Token: 0x060032C1 RID: 12993 RVA: 0x000D7434 File Offset: 0x000D6434
		public SocketElement()
		{
			this.properties.Add(this.alwaysUseCompletionPortsForAccept);
			this.properties.Add(this.alwaysUseCompletionPortsForConnect);
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x000D74B8 File Offset: 0x000D64B8
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

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060032C3 RID: 12995 RVA: 0x000D7514 File Offset: 0x000D6514
		// (set) Token: 0x060032C4 RID: 12996 RVA: 0x000D7527 File Offset: 0x000D6527
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

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060032C5 RID: 12997 RVA: 0x000D753B File Offset: 0x000D653B
		// (set) Token: 0x060032C6 RID: 12998 RVA: 0x000D754E File Offset: 0x000D654E
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

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060032C7 RID: 12999 RVA: 0x000D7562 File Offset: 0x000D6562
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04002F71 RID: 12145
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F72 RID: 12146
		private readonly ConfigurationProperty alwaysUseCompletionPortsForConnect = new ConfigurationProperty("alwaysUseCompletionPortsForConnect", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002F73 RID: 12147
		private readonly ConfigurationProperty alwaysUseCompletionPortsForAccept = new ConfigurationProperty("alwaysUseCompletionPortsForAccept", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
