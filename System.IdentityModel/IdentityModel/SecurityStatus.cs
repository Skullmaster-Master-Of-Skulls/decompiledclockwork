using System;

namespace System.IdentityModel
{
	// Token: 0x0200008B RID: 139
	internal enum SecurityStatus
	{
		// Token: 0x040003F8 RID: 1016
		OK,
		// Token: 0x040003F9 RID: 1017
		OutOfMemory = -2146893056,
		// Token: 0x040003FA RID: 1018
		InvalidHandle,
		// Token: 0x040003FB RID: 1019
		Unsupported,
		// Token: 0x040003FC RID: 1020
		TargetUnknown,
		// Token: 0x040003FD RID: 1021
		InternalError,
		// Token: 0x040003FE RID: 1022
		PackageNotFound,
		// Token: 0x040003FF RID: 1023
		NotOwner,
		// Token: 0x04000400 RID: 1024
		CannotInstall,
		// Token: 0x04000401 RID: 1025
		InvalidToken,
		// Token: 0x04000402 RID: 1026
		LogonDenied = -2146893044,
		// Token: 0x04000403 RID: 1027
		UnknownCredential,
		// Token: 0x04000404 RID: 1028
		NoCredentials,
		// Token: 0x04000405 RID: 1029
		MessageAltered,
		// Token: 0x04000406 RID: 1030
		ContinueNeeded = 590610,
		// Token: 0x04000407 RID: 1031
		CompleteNeeded,
		// Token: 0x04000408 RID: 1032
		CompAndContinue,
		// Token: 0x04000409 RID: 1033
		ContextExpired = 590615,
		// Token: 0x0400040A RID: 1034
		IncompleteMessage = -2146893032,
		// Token: 0x0400040B RID: 1035
		IncompleteCred = -2146893024,
		// Token: 0x0400040C RID: 1036
		BufferNotEnough,
		// Token: 0x0400040D RID: 1037
		WrongPrincipal,
		// Token: 0x0400040E RID: 1038
		UntrustedRoot = -2146893019,
		// Token: 0x0400040F RID: 1039
		UnknownCertificate = -2146893017,
		// Token: 0x04000410 RID: 1040
		CredentialsNeeded = 590624,
		// Token: 0x04000411 RID: 1041
		Renegotiate
	}
}
