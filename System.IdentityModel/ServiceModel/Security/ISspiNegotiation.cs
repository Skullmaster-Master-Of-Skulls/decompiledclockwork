using System;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;

namespace System.ServiceModel.Security
{
	// Token: 0x0200000B RID: 11
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	internal interface ISspiNegotiation : IDisposable
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000057 RID: 87
		DateTime ExpirationTimeUtc { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000058 RID: 88
		bool IsCompleted { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000059 RID: 89
		bool IsValidContext { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005A RID: 90
		string KeyEncryptionAlgorithm { get; }

		// Token: 0x0600005B RID: 91
		byte[] Decrypt(byte[] encryptedData);

		// Token: 0x0600005C RID: 92
		byte[] Encrypt(byte[] data);

		// Token: 0x0600005D RID: 93
		byte[] GetOutgoingBlob(byte[] incomingBlob, ChannelBinding channelbinding, ExtendedProtectionPolicy protectionPolicy);

		// Token: 0x0600005E RID: 94
		string GetRemoteIdentityName();
	}
}
