using System;
using System.ComponentModel;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x02000140 RID: 320
	public sealed class NamedPipeTransportSecurity
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x00023AB7 File Offset: 0x00021CB7
		public NamedPipeTransportSecurity()
		{
			this.protectionLevel = ProtectionLevel.EncryptAndSign;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00023AC6 File Offset: 0x00021CC6
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x00023ACE File Offset: 0x00021CCE
		[DefaultValue(ProtectionLevel.EncryptAndSign)]
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
			set
			{
				if (!ProtectionLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.protectionLevel = value;
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00023AF4 File Offset: 0x00021CF4
		internal WindowsStreamSecurityBindingElement CreateTransportProtectionAndAuthentication()
		{
			return new WindowsStreamSecurityBindingElement
			{
				ProtectionLevel = this.protectionLevel
			};
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00023B14 File Offset: 0x00021D14
		internal static bool IsTransportProtectionAndAuthentication(WindowsStreamSecurityBindingElement wssbe, NamedPipeTransportSecurity transportSecurity)
		{
			transportSecurity.protectionLevel = wssbe.ProtectionLevel;
			return true;
		}

		// Token: 0x04000B55 RID: 2901
		internal const ProtectionLevel DefaultProtectionLevel = ProtectionLevel.EncryptAndSign;

		// Token: 0x04000B56 RID: 2902
		private ProtectionLevel protectionLevel;
	}
}
