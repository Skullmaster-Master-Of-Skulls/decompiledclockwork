using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020004EE RID: 1262
	internal interface SSPIInterface
	{
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x0600275B RID: 10075
		// (set) Token: 0x0600275C RID: 10076
		SecurityPackageInfoClass[] SecurityPackages { get; set; }

		// Token: 0x0600275D RID: 10077
		int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray);

		// Token: 0x0600275E RID: 10078
		int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref AuthIdentity authdata, out SafeFreeCredentials outCredential);

		// Token: 0x0600275F RID: 10079
		int AcquireDefaultCredential(string moduleName, CredentialUse usage, out SafeFreeCredentials outCredential);

		// Token: 0x06002760 RID: 10080
		int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SecureCredential authdata, out SafeFreeCredentials outCredential);

		// Token: 0x06002761 RID: 10081
		int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags);

		// Token: 0x06002762 RID: 10082
		int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags);

		// Token: 0x06002763 RID: 10083
		int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref ContextFlags outFlags);

		// Token: 0x06002764 RID: 10084
		int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref ContextFlags outFlags);

		// Token: 0x06002765 RID: 10085
		int EncryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber);

		// Token: 0x06002766 RID: 10086
		int DecryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber);

		// Token: 0x06002767 RID: 10087
		int MakeSignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber);

		// Token: 0x06002768 RID: 10088
		int VerifySignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber);

		// Token: 0x06002769 RID: 10089
		int QueryContextChannelBinding(SafeDeleteContext phContext, ContextAttribute attribute, out SafeFreeContextBufferChannelBinding refHandle);

		// Token: 0x0600276A RID: 10090
		int QueryContextAttributes(SafeDeleteContext phContext, ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle);

		// Token: 0x0600276B RID: 10091
		int QuerySecurityContextToken(SafeDeleteContext phContext, out SafeCloseHandle phToken);

		// Token: 0x0600276C RID: 10092
		int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers);
	}
}
