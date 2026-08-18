using System;
using System.IdentityModel.Claims;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x020000BC RID: 188
	[__DynamicallyInvokable]
	public class DnsEndpointIdentity : EndpointIdentity
	{
		// Token: 0x0600033F RID: 831 RVA: 0x00012C04 File Offset: 0x00010E04
		[__DynamicallyInvokable]
		public DnsEndpointIdentity(string dnsName)
		{
			if (dnsName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dnsName");
			}
			base.Initialize(Claim.CreateDnsClaim(dnsName));
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00012C2C File Offset: 0x00010E2C
		public DnsEndpointIdentity(Claim identity)
		{
			if (identity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identity");
			}
			if (!identity.ClaimType.Equals(ClaimTypes.Dns))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("UnrecognizedClaimTypeForIdentity", new object[]
				{
					identity.ClaimType,
					ClaimTypes.Dns
				}));
			}
			base.Initialize(identity);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00012C97 File Offset: 0x00010E97
		internal override void WriteContentsTo(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			writer.WriteElementString(XD.AddressingDictionary.Dns, XD.AddressingDictionary.IdentityExtensionNamespace, (string)base.IdentityClaim.Resource);
		}
	}
}
