using System;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200066F RID: 1647
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	public class PeerTransportElement : BindingElementExtensionElement
	{
		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x000F0158 File Offset: 0x000EE358
		public override Type BindingElementType
		{
			get
			{
				return typeof(PeerTransportBindingElement);
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06003F3A RID: 16186 RVA: 0x000F0164 File Offset: 0x000EE364
		// (set) Token: 0x06003F3B RID: 16187 RVA: 0x000F0176 File Offset: 0x000EE376
		[ConfigurationProperty("listenIPAddress", DefaultValue = null)]
		[TypeConverter(typeof(PeerTransportListenAddressConverter))]
		[PeerTransportListenAddressValidator]
		public IPAddress ListenIPAddress
		{
			get
			{
				return (IPAddress)base["listenIPAddress"];
			}
			set
			{
				base["listenIPAddress"] = value;
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06003F3C RID: 16188 RVA: 0x000F0184 File Offset: 0x000EE384
		// (set) Token: 0x06003F3D RID: 16189 RVA: 0x000F0196 File Offset: 0x000EE396
		[ConfigurationProperty("maxBufferPoolSize", DefaultValue = 524288L)]
		[LongValidator(MinValue = 1L)]
		public long MaxBufferPoolSize
		{
			get
			{
				return (long)base["maxBufferPoolSize"];
			}
			set
			{
				base["maxBufferPoolSize"] = value;
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06003F3E RID: 16190 RVA: 0x000F01A9 File Offset: 0x000EE3A9
		// (set) Token: 0x06003F3F RID: 16191 RVA: 0x000F01BB File Offset: 0x000EE3BB
		[ConfigurationProperty("maxReceivedMessageSize", DefaultValue = 65536L)]
		[LongValidator(MinValue = 1L)]
		public long MaxReceivedMessageSize
		{
			get
			{
				return (long)base["maxReceivedMessageSize"];
			}
			set
			{
				base["maxReceivedMessageSize"] = value;
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06003F40 RID: 16192 RVA: 0x000F01CE File Offset: 0x000EE3CE
		// (set) Token: 0x06003F41 RID: 16193 RVA: 0x000F01E0 File Offset: 0x000EE3E0
		[ConfigurationProperty("port", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 65535)]
		public int Port
		{
			get
			{
				return (int)base["port"];
			}
			set
			{
				base["port"] = value;
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06003F42 RID: 16194 RVA: 0x000F01F3 File Offset: 0x000EE3F3
		[ConfigurationProperty("security")]
		public PeerSecurityElement Security
		{
			get
			{
				return (PeerSecurityElement)base["security"];
			}
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x000F0208 File Offset: 0x000EE408
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			PeerTransportBindingElement peerTransportBindingElement = (PeerTransportBindingElement)bindingElement;
			peerTransportBindingElement.ListenIPAddress = this.ListenIPAddress;
			peerTransportBindingElement.Port = this.Port;
			peerTransportBindingElement.MaxBufferPoolSize = this.MaxBufferPoolSize;
			peerTransportBindingElement.MaxReceivedMessageSize = this.MaxReceivedMessageSize;
			this.Security.ApplyConfiguration(peerTransportBindingElement.Security);
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x000F0264 File Offset: 0x000EE464
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			PeerTransportElement peerTransportElement = (PeerTransportElement)from;
			this.ListenIPAddress = peerTransportElement.ListenIPAddress;
			this.Port = peerTransportElement.Port;
			this.MaxBufferPoolSize = peerTransportElement.MaxBufferPoolSize;
			this.MaxReceivedMessageSize = peerTransportElement.MaxReceivedMessageSize;
			this.Security.CopyFrom(peerTransportElement.Security);
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x000F02C0 File Offset: 0x000EE4C0
		protected internal override BindingElement CreateBindingElement()
		{
			PeerTransportBindingElement peerTransportBindingElement = new PeerTransportBindingElement();
			this.ApplyConfiguration(peerTransportBindingElement);
			return peerTransportBindingElement;
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x000F02DC File Offset: 0x000EE4DC
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			PeerTransportBindingElement peerTransportBindingElement = (PeerTransportBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<IPAddress>("listenIPAddress", peerTransportBindingElement.ListenIPAddress);
			base.SetPropertyValueIfNotDefaultValue<int>("port", peerTransportBindingElement.Port);
			base.SetPropertyValueIfNotDefaultValue<long>("maxBufferPoolSize", peerTransportBindingElement.MaxBufferPoolSize);
			base.SetPropertyValueIfNotDefaultValue<long>("maxReceivedMessageSize", peerTransportBindingElement.MaxReceivedMessageSize);
			this.Security.InitializeFrom(peerTransportBindingElement.Security);
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06003F47 RID: 16199 RVA: 0x000F034C File Offset: 0x000EE54C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("listenIPAddress", typeof(IPAddress), null, new PeerTransportListenAddressConverter(), new PeerTransportListenAddressValidator(), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxBufferPoolSize", typeof(long), 524288L, null, new LongValidator(1L, long.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxReceivedMessageSize", typeof(long), 65536L, null, new LongValidator(1L, long.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("port", typeof(int), 0, null, new IntegerValidator(0, 65535, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("security", typeof(PeerSecurityElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002CB3 RID: 11443
		private ConfigurationPropertyCollection properties;
	}
}
