using System;
using System.IdentityModel.Tokens;
using System.Security.Authentication.ExtendedProtection;

namespace System.IdentityModel
{
	// Token: 0x02000078 RID: 120
	internal class ExtendedProtectionPolicyHelper
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
		public ExtendedProtectionPolicyHelper(ChannelBinding channelBinding, ExtendedProtectionPolicy extendedProtectionPolicy)
		{
			this._protectionScenario = ExtendedProtectionPolicyHelper.DefaultPolicy.ProtectionScenario;
			this._policyEnforcement = ExtendedProtectionPolicyHelper.DefaultPolicy.PolicyEnforcement;
			this._channelBinding = channelBinding;
			this._serviceNameCollection = null;
			this._checkServiceBinding = true;
			if (extendedProtectionPolicy != null)
			{
				this._policyEnforcement = extendedProtectionPolicy.PolicyEnforcement;
				this._protectionScenario = extendedProtectionPolicy.ProtectionScenario;
				this._serviceNameCollection = extendedProtectionPolicy.CustomServiceNames;
			}
			if (this._policyEnforcement == PolicyEnforcement.Never)
			{
				this._checkServiceBinding = false;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000FE2E File Offset: 0x0000E02E
		public bool ShouldAddChannelBindingToASC()
		{
			return this._channelBinding != null && this._policyEnforcement != PolicyEnforcement.Never && this._protectionScenario != ProtectionScenario.TrustedProxy;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000FE4E File Offset: 0x0000E04E
		public ChannelBinding ChannelBinding
		{
			get
			{
				return this._channelBinding;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000FE56 File Offset: 0x0000E056
		public bool ShouldCheckServiceBinding
		{
			get
			{
				return this._checkServiceBinding;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000FE5E File Offset: 0x0000E05E
		public ServiceNameCollection ServiceNameCollection
		{
			get
			{
				return this._serviceNameCollection;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000FE66 File Offset: 0x0000E066
		public ProtectionScenario ProtectionScenario
		{
			get
			{
				return this._protectionScenario;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000FE6E File Offset: 0x0000E06E
		public PolicyEnforcement PolicyEnforcement
		{
			get
			{
				return this._policyEnforcement;
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000FE78 File Offset: 0x0000E078
		public void CheckServiceBinding(SafeDeleteContext securityContext, string defaultServiceBinding)
		{
			if (this._policyEnforcement == PolicyEnforcement.Never)
			{
				return;
			}
			string text = null;
			int num = SspiWrapper.QuerySpecifiedTarget(securityContext, out text);
			if (num != 0)
			{
				if (num != -2146893053 && num != -2146893054)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("InvalidServiceBindingInSspiNegotiationNoServiceBinding")));
				}
				if (this._policyEnforcement == PolicyEnforcement.Always)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("InvalidServiceBindingInSspiNegotiationNoServiceBinding")));
				}
				if (this._policyEnforcement == PolicyEnforcement.WhenSupported)
				{
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("InvalidServiceBindingInSspiNegotiationNoServiceBinding")));
			}
			else
			{
				PolicyEnforcement policyEnforcement = this._policyEnforcement;
				if (policyEnforcement != PolicyEnforcement.WhenSupported)
				{
					if (policyEnforcement == PolicyEnforcement.Always)
					{
						if (string.IsNullOrEmpty(text))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("InvalidServiceBindingInSspiNegotiationServiceBindingNotMatched", new object[]
							{
								string.Empty
							})));
						}
					}
				}
				else if (text == null)
				{
					return;
				}
				if (this._serviceNameCollection == null || this._serviceNameCollection.Count < 1)
				{
					if (defaultServiceBinding == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("InvalidServiceBindingInSspiNegotiationServiceBindingNotMatched", new object[]
						{
							string.Empty
						})));
					}
					if (string.Compare(defaultServiceBinding, text, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return;
					}
					if (string.IsNullOrEmpty(text))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("InvalidServiceBindingInSspiNegotiationServiceBindingNotMatched", new object[]
						{
							string.Empty
						})));
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("InvalidServiceBindingInSspiNegotiationServiceBindingNotMatched", new object[]
					{
						text
					})));
				}
				else
				{
					if (this._serviceNameCollection != null && this._serviceNameCollection.Contains(text))
					{
						return;
					}
					if (string.IsNullOrEmpty(text))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("InvalidServiceBindingInSspiNegotiationServiceBindingNotMatched", new object[]
						{
							string.Empty
						})));
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("InvalidServiceBindingInSspiNegotiationServiceBindingNotMatched", new object[]
					{
						text
					})));
				}
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001005F File Offset: 0x0000E25F
		public static ExtendedProtectionPolicy DefaultPolicy
		{
			get
			{
				return ExtendedProtectionPolicyHelper.disabledPolicy;
			}
		}

		// Token: 0x0400038F RID: 911
		private static ExtendedProtectionPolicy disabledPolicy = new ExtendedProtectionPolicy(PolicyEnforcement.Never);

		// Token: 0x04000390 RID: 912
		private PolicyEnforcement _policyEnforcement;

		// Token: 0x04000391 RID: 913
		private ProtectionScenario _protectionScenario;

		// Token: 0x04000392 RID: 914
		private ChannelBinding _channelBinding;

		// Token: 0x04000393 RID: 915
		private ServiceNameCollection _serviceNameCollection;

		// Token: 0x04000394 RID: 916
		private bool _checkServiceBinding;
	}
}
