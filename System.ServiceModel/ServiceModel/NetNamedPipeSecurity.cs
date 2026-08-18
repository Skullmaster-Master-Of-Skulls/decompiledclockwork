using System;
using System.ComponentModel;
using System.Net.Security;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x0200014A RID: 330
	public sealed class NetNamedPipeSecurity
	{
		// Token: 0x06000963 RID: 2403 RVA: 0x00025174 File Offset: 0x00023374
		public NetNamedPipeSecurity()
		{
			this.mode = NetNamedPipeSecurityMode.Transport;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0002518E File Offset: 0x0002338E
		private NetNamedPipeSecurity(NetNamedPipeSecurityMode mode, NamedPipeTransportSecurity transport)
		{
			this.mode = mode;
			this.transport = ((transport == null) ? new NamedPipeTransportSecurity() : transport);
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x000251B9 File Offset: 0x000233B9
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x000251C1 File Offset: 0x000233C1
		[DefaultValue(NetNamedPipeSecurityMode.Transport)]
		public NetNamedPipeSecurityMode Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				if (!NetNamedPipeSecurityModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.mode = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x000251E7 File Offset: 0x000233E7
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x000251EF File Offset: 0x000233EF
		public NamedPipeTransportSecurity Transport
		{
			get
			{
				return this.transport;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.transport = value;
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0002520B File Offset: 0x0002340B
		internal WindowsStreamSecurityBindingElement CreateTransportSecurity()
		{
			if (this.mode == NetNamedPipeSecurityMode.Transport)
			{
				return this.transport.CreateTransportProtectionAndAuthentication();
			}
			return null;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00025224 File Offset: 0x00023424
		internal static bool TryCreate(WindowsStreamSecurityBindingElement wssbe, NetNamedPipeSecurityMode mode, out NetNamedPipeSecurity security)
		{
			security = null;
			NamedPipeTransportSecurity transportSecurity = new NamedPipeTransportSecurity();
			if (mode == NetNamedPipeSecurityMode.Transport && !NamedPipeTransportSecurity.IsTransportProtectionAndAuthentication(wssbe, transportSecurity))
			{
				return false;
			}
			security = new NetNamedPipeSecurity(mode, transportSecurity);
			return true;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00025253 File Offset: 0x00023453
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTransport()
		{
			return this.transport.ProtectionLevel != ProtectionLevel.EncryptAndSign;
		}

		// Token: 0x04000B74 RID: 2932
		internal const NetNamedPipeSecurityMode DefaultMode = NetNamedPipeSecurityMode.Transport;

		// Token: 0x04000B75 RID: 2933
		private NetNamedPipeSecurityMode mode;

		// Token: 0x04000B76 RID: 2934
		private NamedPipeTransportSecurity transport = new NamedPipeTransportSecurity();
	}
}
