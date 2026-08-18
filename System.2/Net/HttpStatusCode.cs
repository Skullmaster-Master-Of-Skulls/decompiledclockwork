using System;

namespace System.Net
{
	// Token: 0x02000104 RID: 260
	[__DynamicallyInvokable]
	public enum HttpStatusCode
	{
		// Token: 0x04000E97 RID: 3735
		[__DynamicallyInvokable]
		Continue = 100,
		// Token: 0x04000E98 RID: 3736
		[__DynamicallyInvokable]
		SwitchingProtocols,
		// Token: 0x04000E99 RID: 3737
		[__DynamicallyInvokable]
		OK = 200,
		// Token: 0x04000E9A RID: 3738
		[__DynamicallyInvokable]
		Created,
		// Token: 0x04000E9B RID: 3739
		[__DynamicallyInvokable]
		Accepted,
		// Token: 0x04000E9C RID: 3740
		[__DynamicallyInvokable]
		NonAuthoritativeInformation,
		// Token: 0x04000E9D RID: 3741
		[__DynamicallyInvokable]
		NoContent,
		// Token: 0x04000E9E RID: 3742
		[__DynamicallyInvokable]
		ResetContent,
		// Token: 0x04000E9F RID: 3743
		[__DynamicallyInvokable]
		PartialContent,
		// Token: 0x04000EA0 RID: 3744
		[__DynamicallyInvokable]
		MultipleChoices = 300,
		// Token: 0x04000EA1 RID: 3745
		[__DynamicallyInvokable]
		Ambiguous = 300,
		// Token: 0x04000EA2 RID: 3746
		[__DynamicallyInvokable]
		MovedPermanently,
		// Token: 0x04000EA3 RID: 3747
		[__DynamicallyInvokable]
		Moved = 301,
		// Token: 0x04000EA4 RID: 3748
		[__DynamicallyInvokable]
		Found,
		// Token: 0x04000EA5 RID: 3749
		[__DynamicallyInvokable]
		Redirect = 302,
		// Token: 0x04000EA6 RID: 3750
		[__DynamicallyInvokable]
		SeeOther,
		// Token: 0x04000EA7 RID: 3751
		[__DynamicallyInvokable]
		RedirectMethod = 303,
		// Token: 0x04000EA8 RID: 3752
		[__DynamicallyInvokable]
		NotModified,
		// Token: 0x04000EA9 RID: 3753
		[__DynamicallyInvokable]
		UseProxy,
		// Token: 0x04000EAA RID: 3754
		[__DynamicallyInvokable]
		Unused,
		// Token: 0x04000EAB RID: 3755
		[__DynamicallyInvokable]
		TemporaryRedirect,
		// Token: 0x04000EAC RID: 3756
		[__DynamicallyInvokable]
		RedirectKeepVerb = 307,
		// Token: 0x04000EAD RID: 3757
		[__DynamicallyInvokable]
		BadRequest = 400,
		// Token: 0x04000EAE RID: 3758
		[__DynamicallyInvokable]
		Unauthorized,
		// Token: 0x04000EAF RID: 3759
		[__DynamicallyInvokable]
		PaymentRequired,
		// Token: 0x04000EB0 RID: 3760
		[__DynamicallyInvokable]
		Forbidden,
		// Token: 0x04000EB1 RID: 3761
		[__DynamicallyInvokable]
		NotFound,
		// Token: 0x04000EB2 RID: 3762
		[__DynamicallyInvokable]
		MethodNotAllowed,
		// Token: 0x04000EB3 RID: 3763
		[__DynamicallyInvokable]
		NotAcceptable,
		// Token: 0x04000EB4 RID: 3764
		[__DynamicallyInvokable]
		ProxyAuthenticationRequired,
		// Token: 0x04000EB5 RID: 3765
		[__DynamicallyInvokable]
		RequestTimeout,
		// Token: 0x04000EB6 RID: 3766
		[__DynamicallyInvokable]
		Conflict,
		// Token: 0x04000EB7 RID: 3767
		[__DynamicallyInvokable]
		Gone,
		// Token: 0x04000EB8 RID: 3768
		[__DynamicallyInvokable]
		LengthRequired,
		// Token: 0x04000EB9 RID: 3769
		[__DynamicallyInvokable]
		PreconditionFailed,
		// Token: 0x04000EBA RID: 3770
		[__DynamicallyInvokable]
		RequestEntityTooLarge,
		// Token: 0x04000EBB RID: 3771
		[__DynamicallyInvokable]
		RequestUriTooLong,
		// Token: 0x04000EBC RID: 3772
		[__DynamicallyInvokable]
		UnsupportedMediaType,
		// Token: 0x04000EBD RID: 3773
		[__DynamicallyInvokable]
		RequestedRangeNotSatisfiable,
		// Token: 0x04000EBE RID: 3774
		[__DynamicallyInvokable]
		ExpectationFailed,
		// Token: 0x04000EBF RID: 3775
		[__DynamicallyInvokable]
		UpgradeRequired = 426,
		// Token: 0x04000EC0 RID: 3776
		[__DynamicallyInvokable]
		InternalServerError = 500,
		// Token: 0x04000EC1 RID: 3777
		[__DynamicallyInvokable]
		NotImplemented,
		// Token: 0x04000EC2 RID: 3778
		[__DynamicallyInvokable]
		BadGateway,
		// Token: 0x04000EC3 RID: 3779
		[__DynamicallyInvokable]
		ServiceUnavailable,
		// Token: 0x04000EC4 RID: 3780
		[__DynamicallyInvokable]
		GatewayTimeout,
		// Token: 0x04000EC5 RID: 3781
		[__DynamicallyInvokable]
		HttpVersionNotSupported
	}
}
