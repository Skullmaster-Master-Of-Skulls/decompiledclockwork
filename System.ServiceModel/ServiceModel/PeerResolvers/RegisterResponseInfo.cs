using System;
using System.Runtime;
using System.Runtime.Serialization;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001CE RID: 462
	[MessageContract(IsWrapped = false)]
	public class RegisterResponseInfo
	{
		// Token: 0x06000EF8 RID: 3832 RVA: 0x00036570 File Offset: 0x00034770
		public RegisterResponseInfo(Guid registrationId, TimeSpan registrationLifetime)
		{
			this.body = new RegisterResponseInfo.RegisterResponseInfoDC(registrationId, registrationLifetime);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00036585 File Offset: 0x00034785
		public RegisterResponseInfo()
		{
			this.body = new RegisterResponseInfo.RegisterResponseInfoDC();
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00036598 File Offset: 0x00034798
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x000365A5 File Offset: 0x000347A5
		public Guid RegistrationId
		{
			get
			{
				return this.body.RegistrationId;
			}
			set
			{
				this.body.RegistrationId = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x000365B3 File Offset: 0x000347B3
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x000365C0 File Offset: 0x000347C0
		public TimeSpan RegistrationLifetime
		{
			get
			{
				return this.body.RegistrationLifetime;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.body.RegistrationLifetime = value;
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00036638 File Offset: 0x00034838
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x040017A4 RID: 6052
		[MessageBodyMember(Name = "Update", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private RegisterResponseInfo.RegisterResponseInfoDC body;

		// Token: 0x02000B05 RID: 2821
		[DataContract(Name = "RegisterResponse", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class RegisterResponseInfoDC
		{
			// Token: 0x06006F59 RID: 28505 RVA: 0x0019D97B File Offset: 0x0019BB7B
			public RegisterResponseInfoDC()
			{
			}

			// Token: 0x06006F5A RID: 28506 RVA: 0x0019D983 File Offset: 0x0019BB83
			public RegisterResponseInfoDC(Guid registrationId, TimeSpan registrationLifetime)
			{
				this.RegistrationLifetime = registrationLifetime;
				this.RegistrationId = registrationId;
			}

			// Token: 0x04003F89 RID: 16265
			[DataMember(Name = "RegistrationLifetime")]
			public TimeSpan RegistrationLifetime;

			// Token: 0x04003F8A RID: 16266
			[DataMember(Name = "RegistrationId")]
			public Guid RegistrationId;
		}
	}
}
