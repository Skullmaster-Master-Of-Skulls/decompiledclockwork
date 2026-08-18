using System;
using System.IO;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.ComIntegration;
using System.Transactions;
using Microsoft.Transactions.Wsat.Messaging;
using Microsoft.Transactions.Wsat.Protocol;
using Microsoft.Transactions.Wsat.Recovery;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001B8 RID: 440
	internal class WsatConfiguration
	{
		// Token: 0x06000E5E RID: 3678 RVA: 0x00033734 File Offset: 0x00031934
		public WsatConfiguration()
		{
			WhereaboutsReader whereabouts = this.GetWhereabouts();
			ProtocolInformationReader protocolInformation = whereabouts.ProtocolInformation;
			if (protocolInformation != null)
			{
				this.protocolService10Enabled = protocolInformation.IsV10Enabled;
				this.protocolService11Enabled = protocolInformation.IsV11Enabled;
			}
			this.Initialize(whereabouts);
			this.oleTxUpgradeEnabled = WsatConfiguration.ReadFlag("Software\\Microsoft\\WSAT\\3.0", "OleTxUpgradeEnabled", true);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00033790 File Offset: 0x00031990
		private void Initialize(WhereaboutsReader whereabouts)
		{
			try
			{
				this.InitializeForUnmarshal(whereabouts);
				this.InitializeForMarshal(whereabouts);
			}
			catch (UriFormatException e)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionManagerConfigurationException(SR.GetString("WsatUriCreationFailed"), e));
			}
			catch (ArgumentOutOfRangeException e2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionManagerConfigurationException(SR.GetString("WsatUriCreationFailed"), e2));
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x00033804 File Offset: 0x00031A04
		public bool OleTxUpgradeEnabled
		{
			get
			{
				return this.oleTxUpgradeEnabled;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0003380C File Offset: 0x00031A0C
		public TimeSpan MaxTimeout
		{
			get
			{
				return this.maxTimeout;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x00033814 File Offset: 0x00031A14
		public bool IssuedTokensEnabled
		{
			get
			{
				return this.issuedTokensEnabled;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0003381C File Offset: 0x00031A1C
		public bool InboundEnabled
		{
			get
			{
				return this.inboundEnabled;
			}
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00033824 File Offset: 0x00031A24
		public bool IsProtocolServiceEnabled(ProtocolVersion protocolVersion)
		{
			if (protocolVersion == ProtocolVersion.Version10)
			{
				return this.protocolService10Enabled;
			}
			if (protocolVersion != ProtocolVersion.Version11)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidWsatProtocolVersion")));
			}
			return this.protocolService11Enabled;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00033857 File Offset: 0x00031A57
		public EndpointAddress LocalActivationService(ProtocolVersion protocolVersion)
		{
			if (protocolVersion == ProtocolVersion.Version10)
			{
				return this.localActivationService10;
			}
			if (protocolVersion != ProtocolVersion.Version11)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidWsatProtocolVersion")));
			}
			return this.localActivationService11;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0003388A File Offset: 0x00031A8A
		public EndpointAddress RemoteActivationService(ProtocolVersion protocolVersion)
		{
			if (protocolVersion == ProtocolVersion.Version10)
			{
				return this.remoteActivationService10;
			}
			if (protocolVersion != ProtocolVersion.Version11)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidWsatProtocolVersion")));
			}
			return this.remoteActivationService11;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x000338C0 File Offset: 0x00031AC0
		public EndpointAddress CreateRegistrationService(AddressHeader refParam, ProtocolVersion protocolVersion)
		{
			if (protocolVersion == ProtocolVersion.Version10)
			{
				return new EndpointAddress(this.registrationServiceAddress10, new AddressHeader[]
				{
					refParam
				});
			}
			if (protocolVersion != ProtocolVersion.Version11)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidWsatProtocolVersion")));
			}
			return new EndpointAddress(this.registrationServiceAddress11, new AddressHeader[]
			{
				refParam
			});
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0003391C File Offset: 0x00031B1C
		public bool IsLocalRegistrationService(EndpointAddress endpoint, ProtocolVersion protocolVersion)
		{
			if (endpoint.Uri == null)
			{
				return false;
			}
			if (protocolVersion == ProtocolVersion.Version10)
			{
				return endpoint.Uri == this.registrationServiceAddress10;
			}
			if (protocolVersion != ProtocolVersion.Version11)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidWsatProtocolVersion")));
			}
			return endpoint.Uri == this.registrationServiceAddress11;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00033980 File Offset: 0x00031B80
		public bool IsDisabledRegistrationService(EndpointAddress endpoint)
		{
			return endpoint.Uri.AbsolutePath == WsatConfiguration.DisabledRegistrationPath;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00033998 File Offset: 0x00031B98
		private WhereaboutsReader GetWhereabouts()
		{
			WhereaboutsReader result;
			try
			{
				result = new WhereaboutsReader(TransactionInterop.GetWhereabouts());
			}
			catch (SerializationException e)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionManagerConfigurationException(SR.GetString("WhereaboutsReadFailed"), e));
			}
			return result;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x000339E0 File Offset: 0x00031BE0
		private void InitializeForUnmarshal(WhereaboutsReader whereabouts)
		{
			ProtocolInformationReader protocolInformation = whereabouts.ProtocolInformation;
			if (protocolInformation != null && protocolInformation.NetworkInboundAccess)
			{
				this.inboundEnabled = true;
				bool flag = string.Compare(Environment.MachineName, protocolInformation.NodeName, StringComparison.OrdinalIgnoreCase) == 0;
				string suffix = BindingStrings.ActivationCoordinatorSuffix(ProtocolVersion.Version10);
				string suffix2 = BindingStrings.ActivationCoordinatorSuffix(ProtocolVersion.Version11);
				if (protocolInformation.IsClustered || (protocolInformation.NetworkClientAccess && !flag))
				{
					string spnIdentity;
					if (protocolInformation.IsClustered)
					{
						spnIdentity = null;
					}
					else
					{
						spnIdentity = "host/" + protocolInformation.HostName;
					}
					if (protocolInformation.IsV10Enabled)
					{
						this.remoteActivationService10 = this.CreateActivationEndpointAddress(protocolInformation, suffix, spnIdentity, true);
					}
					if (protocolInformation.IsV11Enabled)
					{
						this.remoteActivationService11 = this.CreateActivationEndpointAddress(protocolInformation, suffix2, spnIdentity, true);
					}
				}
				if (flag)
				{
					string spnIdentity = "host/" + protocolInformation.NodeName;
					if (protocolInformation.IsV10Enabled)
					{
						this.localActivationService10 = this.CreateActivationEndpointAddress(protocolInformation, suffix, spnIdentity, false);
					}
					if (protocolInformation.IsV11Enabled)
					{
						this.localActivationService11 = this.CreateActivationEndpointAddress(protocolInformation, suffix2, spnIdentity, false);
					}
				}
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00033AD8 File Offset: 0x00031CD8
		private EndpointAddress CreateActivationEndpointAddress(ProtocolInformationReader protocol, string suffix, string spnIdentity, bool isRemote)
		{
			string scheme;
			string host;
			int port;
			string pathValue;
			if (isRemote)
			{
				scheme = Uri.UriSchemeHttps;
				host = protocol.HostName;
				port = protocol.HttpsPort;
				pathValue = protocol.BasePath + "/" + suffix + "Remote/";
			}
			else
			{
				scheme = Uri.UriSchemeNetPipe;
				host = "localhost";
				port = -1;
				pathValue = string.Concat(new string[]
				{
					protocol.HostName,
					"/",
					protocol.BasePath,
					"/",
					suffix
				});
			}
			UriBuilder uriBuilder = new UriBuilder(scheme, host, port, pathValue);
			if (spnIdentity != null)
			{
				EndpointIdentity identity = EndpointIdentity.CreateSpnIdentity(spnIdentity);
				return new EndpointAddress(uriBuilder.Uri, identity, new AddressHeader[0]);
			}
			return new EndpointAddress(uriBuilder.Uri, new AddressHeader[0]);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00033B94 File Offset: 0x00031D94
		private void InitializeForMarshal(WhereaboutsReader whereabouts)
		{
			ProtocolInformationReader protocolInformation = whereabouts.ProtocolInformation;
			if (protocolInformation != null && protocolInformation.NetworkOutboundAccess)
			{
				if (protocolInformation.IsV10Enabled)
				{
					UriBuilder uriBuilder = new UriBuilder(Uri.UriSchemeHttps, protocolInformation.HostName, protocolInformation.HttpsPort, protocolInformation.BasePath + "/" + BindingStrings.RegistrationCoordinatorSuffix(ProtocolVersion.Version10));
					this.registrationServiceAddress10 = uriBuilder.Uri;
				}
				if (protocolInformation.IsV11Enabled)
				{
					UriBuilder uriBuilder2 = new UriBuilder(Uri.UriSchemeHttps, protocolInformation.HostName, protocolInformation.HttpsPort, protocolInformation.BasePath + "/" + BindingStrings.RegistrationCoordinatorSuffix(ProtocolVersion.Version11));
					this.registrationServiceAddress11 = uriBuilder2.Uri;
				}
				this.issuedTokensEnabled = protocolInformation.IssuedTokensEnabled;
				this.maxTimeout = protocolInformation.MaxTimeout;
				return;
			}
			UriBuilder uriBuilder3 = new UriBuilder(Uri.UriSchemeHttps, whereabouts.HostName, 443, WsatConfiguration.DisabledRegistrationPath);
			this.registrationServiceAddress10 = uriBuilder3.Uri;
			this.registrationServiceAddress11 = uriBuilder3.Uri;
			this.issuedTokensEnabled = false;
			this.maxTimeout = TimeSpan.FromMinutes(5.0);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00033CA4 File Offset: 0x00031EA4
		private static object ReadValue(string key, string value)
		{
			object result;
			try
			{
				using (RegistryHandle nativeHKLMSubkey = RegistryHandle.GetNativeHKLMSubkey(key, false))
				{
					if (nativeHKLMSubkey == null)
					{
						result = null;
					}
					else
					{
						result = nativeHKLMSubkey.GetValue(value);
					}
				}
			}
			catch (SecurityException e)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionManagerConfigurationException(SR.GetString("WsatRegistryValueReadError", new object[]
				{
					value
				}), e));
			}
			catch (IOException e2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionManagerConfigurationException(SR.GetString("WsatRegistryValueReadError", new object[]
				{
					value
				}), e2));
			}
			return result;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00033D4C File Offset: 0x00031F4C
		private static int ReadInt(string key, string value, int defaultValue)
		{
			object obj = WsatConfiguration.ReadValue(key, value);
			if (obj == null || !(obj is int))
			{
				return defaultValue;
			}
			return (int)obj;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00033D74 File Offset: 0x00031F74
		private static bool ReadFlag(string key, string value, bool defaultValue)
		{
			return WsatConfiguration.ReadInt(key, value, defaultValue ? 1 : 0) != 0;
		}

		// Token: 0x0400174C RID: 5964
		private static readonly string DisabledRegistrationPath = "WsatService" + "/" + BindingStrings.RegistrationCoordinatorSuffix(ProtocolVersion.Version10) + "Disabled/";

		// Token: 0x0400174D RID: 5965
		private const string WsatKey = "Software\\Microsoft\\WSAT\\3.0";

		// Token: 0x0400174E RID: 5966
		private const string OleTxUpgradeEnabledValue = "OleTxUpgradeEnabled";

		// Token: 0x0400174F RID: 5967
		private const bool OleTxUpgradeEnabledDefault = true;

		// Token: 0x04001750 RID: 5968
		private bool oleTxUpgradeEnabled;

		// Token: 0x04001751 RID: 5969
		private EndpointAddress localActivationService10;

		// Token: 0x04001752 RID: 5970
		private EndpointAddress localActivationService11;

		// Token: 0x04001753 RID: 5971
		private EndpointAddress remoteActivationService10;

		// Token: 0x04001754 RID: 5972
		private EndpointAddress remoteActivationService11;

		// Token: 0x04001755 RID: 5973
		private Uri registrationServiceAddress10;

		// Token: 0x04001756 RID: 5974
		private Uri registrationServiceAddress11;

		// Token: 0x04001757 RID: 5975
		private bool protocolService10Enabled;

		// Token: 0x04001758 RID: 5976
		private bool protocolService11Enabled;

		// Token: 0x04001759 RID: 5977
		private bool inboundEnabled;

		// Token: 0x0400175A RID: 5978
		private bool issuedTokensEnabled;

		// Token: 0x0400175B RID: 5979
		private TimeSpan maxTimeout;
	}
}
