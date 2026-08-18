using System;
using System.Runtime;
using System.Runtime.Serialization;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001D2 RID: 466
	[MessageContract(IsWrapped = false)]
	public class RefreshResponseInfo
	{
		// Token: 0x06000F10 RID: 3856 RVA: 0x00036747 File Offset: 0x00034947
		public RefreshResponseInfo() : this(TimeSpan.Zero, RefreshResult.RegistrationNotFound)
		{
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00036755 File Offset: 0x00034955
		public RefreshResponseInfo(TimeSpan registrationLifetime, RefreshResult result)
		{
			this.body = new RefreshResponseInfo.RefreshResponseInfoDC(registrationLifetime, result);
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0003676A File Offset: 0x0003496A
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x00036778 File Offset: 0x00034978
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

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x000367F0 File Offset: 0x000349F0
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x000367FD File Offset: 0x000349FD
		public RefreshResult Result
		{
			get
			{
				return this.body.Result;
			}
			set
			{
				this.body.Result = value;
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0003680B File Offset: 0x00034A0B
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x040017A8 RID: 6056
		[MessageBodyMember(Name = "RefreshResponse", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private RefreshResponseInfo.RefreshResponseInfoDC body;

		// Token: 0x02000B09 RID: 2825
		[DataContract(Name = "RefreshResponseInfo", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class RefreshResponseInfoDC
		{
			// Token: 0x06006F61 RID: 28513 RVA: 0x0019DA02 File Offset: 0x0019BC02
			public RefreshResponseInfoDC(TimeSpan registrationLifetime, RefreshResult result)
			{
				this.RegistrationLifetime = registrationLifetime;
				this.Result = result;
			}

			// Token: 0x04003F93 RID: 16275
			[DataMember(Name = "RegistrationLifetime")]
			public TimeSpan RegistrationLifetime;

			// Token: 0x04003F94 RID: 16276
			[DataMember(Name = "Result")]
			public RefreshResult Result;
		}
	}
}
