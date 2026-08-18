using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000654 RID: 1620
	public sealed class NetHttpWebSocketTransportSettingsElement : WebSocketTransportSettingsElement
	{
		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06003E67 RID: 15975 RVA: 0x000EDB8C File Offset: 0x000EBD8C
		// (set) Token: 0x06003E68 RID: 15976 RVA: 0x000EDB94 File Offset: 0x000EBD94
		[ConfigurationProperty("transportUsage", DefaultValue = WebSocketTransportUsage.WhenDuplex)]
		[ServiceModelEnumValidator(typeof(WebSocketTransportUsageHelper))]
		public override WebSocketTransportUsage TransportUsage
		{
			get
			{
				return base.TransportUsage;
			}
			set
			{
				base.TransportUsage = value;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06003E69 RID: 15977 RVA: 0x000EDB9D File Offset: 0x000EBD9D
		// (set) Token: 0x06003E6A RID: 15978 RVA: 0x000EDBA5 File Offset: 0x000EBDA5
		[ConfigurationProperty("subProtocol", DefaultValue = "soap")]
		[StringValidator(MinLength = 0)]
		public override string SubProtocol
		{
			get
			{
				return base.SubProtocol;
			}
			set
			{
				base.SubProtocol = value;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06003E6B RID: 15979 RVA: 0x000EDBB0 File Offset: 0x000EBDB0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object lockObj = this.lockObj;
					lock (lockObj)
					{
						if (this.properties == null)
						{
							ConfigurationPropertyCollection configurationPropertyCollection = base.Properties;
							configurationPropertyCollection.Remove("transportUsage");
							configurationPropertyCollection.Add(new ConfigurationProperty("transportUsage", typeof(WebSocketTransportUsage), WebSocketTransportUsage.WhenDuplex, null, new ServiceModelEnumValidator(typeof(WebSocketTransportUsageHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Remove("subProtocol");
							configurationPropertyCollection.Add(new ConfigurationProperty("subProtocol", typeof(string), "soap", null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002CA5 RID: 11429
		private ConfigurationPropertyCollection properties;
	}
}
