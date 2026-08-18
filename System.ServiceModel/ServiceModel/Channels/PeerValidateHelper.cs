using System;
using System.Net;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A05 RID: 2565
	internal static class PeerValidateHelper
	{
		// Token: 0x060065B5 RID: 26037 RVA: 0x0017B15C File Offset: 0x0017935C
		public static void ValidateListenIPAddress(IPAddress address)
		{
			if (address == null)
			{
				return;
			}
			if (address.Equals(IPAddress.Any) || address.Equals(IPAddress.IPv6Any) || address.Equals(IPAddress.IPv6None) || address.Equals(IPAddress.None) || address.Equals(IPAddress.Broadcast) || address.IsIPv6Multicast || IPAddress.IsLoopback(address))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("PeerListenIPAddressInvalid", new object[]
				{
					address
				}), "address", null));
			}
		}

		// Token: 0x060065B6 RID: 26038 RVA: 0x0017B1E8 File Offset: 0x001793E8
		public static void ValidateMaxMessageSize(long value)
		{
			if (value < 16384L)
			{
				string @string = SR.GetString("ArgumentOutOfRange", new object[]
				{
					16384L,
					long.MaxValue
				});
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, @string));
			}
		}

		// Token: 0x060065B7 RID: 26039 RVA: 0x0017B24C File Offset: 0x0017944C
		public static void ValidatePort(int value)
		{
			if (value < 0 || value > 65535)
			{
				string @string = SR.GetString("ArgumentOutOfRange", new object[]
				{
					0,
					65535
				});
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, @string));
			}
		}

		// Token: 0x060065B8 RID: 26040 RVA: 0x0017B2A8 File Offset: 0x001794A8
		public static bool ValidNodeAddress(PeerNodeAddress address)
		{
			return address != null && address.EndpointAddress != null && address.EndpointAddress.Uri != null && address.IPAddresses != null && address.IPAddresses.Count > 0 && string.Compare(address.EndpointAddress.Uri.Scheme, Uri.UriSchemeNetTcp, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060065B9 RID: 26041 RVA: 0x0017B310 File Offset: 0x00179510
		public static bool ValidReferralNodeAddress(PeerNodeAddress address)
		{
			bool result = true;
			long num = -1L;
			foreach (IPAddress ipaddress in address.IPAddresses)
			{
				if (ipaddress.IsIPv6LinkLocal)
				{
					if (num == -1L)
					{
						num = ipaddress.ScopeId;
					}
					else if (num != ipaddress.ScopeId)
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}
	}
}
