using System;
using System.ComponentModel;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x0200013F RID: 319
	public sealed class MsmqTransportSecurity
	{
		// Token: 0x060008CC RID: 2252 RVA: 0x0002395A File Offset: 0x00021B5A
		public MsmqTransportSecurity()
		{
			this.msmqAuthenticationMode = MsmqAuthenticationMode.WindowsDomain;
			this.msmqEncryptionAlgorithm = MsmqEncryptionAlgorithm.RC4Stream;
			this.msmqHashAlgorithm = MsmqDefaults.MsmqSecureHashAlgorithm;
			this.msmqProtectionLevel = ProtectionLevel.Sign;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00023984 File Offset: 0x00021B84
		public MsmqTransportSecurity(MsmqTransportSecurity other)
		{
			if (other == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("other");
			}
			this.msmqAuthenticationMode = other.MsmqAuthenticationMode;
			this.msmqEncryptionAlgorithm = other.MsmqEncryptionAlgorithm;
			this.msmqHashAlgorithm = other.MsmqSecureHashAlgorithm;
			this.msmqProtectionLevel = other.MsmqProtectionLevel;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x000239DA File Offset: 0x00021BDA
		internal bool Enabled
		{
			get
			{
				return this.msmqAuthenticationMode != MsmqAuthenticationMode.None && this.msmqProtectionLevel > ProtectionLevel.None;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x000239EF File Offset: 0x00021BEF
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x000239F7 File Offset: 0x00021BF7
		[DefaultValue(MsmqAuthenticationMode.WindowsDomain)]
		public MsmqAuthenticationMode MsmqAuthenticationMode
		{
			get
			{
				return this.msmqAuthenticationMode;
			}
			set
			{
				if (!MsmqAuthenticationModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.msmqAuthenticationMode = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00023A1D File Offset: 0x00021C1D
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x00023A25 File Offset: 0x00021C25
		[DefaultValue(MsmqEncryptionAlgorithm.RC4Stream)]
		public MsmqEncryptionAlgorithm MsmqEncryptionAlgorithm
		{
			get
			{
				return this.msmqEncryptionAlgorithm;
			}
			set
			{
				if (!MsmqEncryptionAlgorithmHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.msmqEncryptionAlgorithm = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00023A4B File Offset: 0x00021C4B
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x00023A53 File Offset: 0x00021C53
		[DefaultValue(MsmqSecureHashAlgorithm.Sha256)]
		public MsmqSecureHashAlgorithm MsmqSecureHashAlgorithm
		{
			get
			{
				return this.msmqHashAlgorithm;
			}
			set
			{
				if (!MsmqSecureHashAlgorithmHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.msmqHashAlgorithm = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00023A79 File Offset: 0x00021C79
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x00023A81 File Offset: 0x00021C81
		[DefaultValue(ProtectionLevel.Sign)]
		public ProtectionLevel MsmqProtectionLevel
		{
			get
			{
				return this.msmqProtectionLevel;
			}
			set
			{
				if (!ProtectionLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.msmqProtectionLevel = value;
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00023AA7 File Offset: 0x00021CA7
		internal void Disable()
		{
			this.msmqAuthenticationMode = MsmqAuthenticationMode.None;
			this.msmqProtectionLevel = ProtectionLevel.None;
		}

		// Token: 0x04000B51 RID: 2897
		private MsmqAuthenticationMode msmqAuthenticationMode;

		// Token: 0x04000B52 RID: 2898
		private MsmqEncryptionAlgorithm msmqEncryptionAlgorithm;

		// Token: 0x04000B53 RID: 2899
		private MsmqSecureHashAlgorithm msmqHashAlgorithm;

		// Token: 0x04000B54 RID: 2900
		private ProtectionLevel msmqProtectionLevel;
	}
}
