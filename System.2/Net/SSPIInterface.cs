using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001C5 RID: 453
	internal interface SSPIInterface
	{
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060011E0 RID: 4576
		// (set) Token: 0x060011E1 RID: 4577
		SecurityPackageInfoClass[] SecurityPackages { get; set; }

		// Token: 0x060011E2 RID: 4578
		int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray);

		// Token: 0x060011E3 RID: 4579
		int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref AuthIdentity authdata, out SafeFreeCredentials outCredential);

		// Token: 0x060011E4 RID: 4580
		int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential);

		// Token: 0x060011E5 RID: 4581
		int AcquireDefaultCredential(string moduleName, CredentialUse usage, out SafeFreeCredentials outCredential);

		// Token: 0x060011E6 RID: 4582
		int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SecureCredential authdata, out SafeFreeCredentials outCredential);

		// Token: 0x060011E7 RID: 4583
		int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SecureCredential2 authdata, out SafeFreeCredentials outCredential);

		// Token: 0x060011E8 RID: 4584
		int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags);

		// Token: 0x060011E9 RID: 4585
		int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags);

		// Token: 0x060011EA RID: 4586
		int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref ContextFlags outFlags);

		// Token: 0x060011EB RID: 4587
		int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref ContextFlags outFlags);

		// Token: 0x060011EC RID: 4588
		int EncryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber);

		// Token: 0x060011ED RID: 4589
		int DecryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber);

		// Token: 0x060011EE RID: 4590
		int MakeSignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber);

		// Token: 0x060011EF RID: 4591
		int VerifySignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber);

		// Token: 0x060011F0 RID: 4592
		int QueryContextChannelBinding(SafeDeleteContext phContext, ContextAttribute attribute, out SafeFreeContextBufferChannelBinding refHandle);

		// Token: 0x060011F1 RID: 4593
		int QueryContextAttributes(SafeDeleteContext phContext, ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle);

		// Token: 0x060011F2 RID: 4594
		int SetContextAttributes(SafeDeleteContext phContext, ContextAttribute attribute, byte[] buffer);

		// Token: 0x060011F3 RID: 4595
		int QuerySecurityContextToken(SafeDeleteContext phContext, out SafeCloseHandle phToken);

		// Token: 0x060011F4 RID: 4596
		int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers);

		// Token: 0x060011F5 RID: 4597
		int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers);
	}
}
