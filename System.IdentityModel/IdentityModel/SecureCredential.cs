using System;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel
{
	// Token: 0x02000090 RID: 144
	internal struct SecureCredential
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x00011EB4 File Offset: 0x000100B4
		public SecureCredential(int version, X509Certificate2 certificate, SecureCredential.Flags flags, SchProtocols protocols)
		{
			this.rootStore = (this.phMappers = (this.palgSupportedAlgs = (this.certContextArray = IntPtr.Zero)));
			this.cCreds = (this.cMappers = (this.cSupportedAlgs = 0));
			this.dwMinimumCipherStrength = (this.dwMaximumCipherStrength = 0);
			this.dwSessionLifespan = (this.reserved = 0);
			this.version = version;
			this.dwFlags = flags;
			this.grbitEnabledProtocols = protocols;
			if (certificate != null)
			{
				this.certContextArray = certificate.Handle;
				this.cCreds = 1;
			}
		}

		// Token: 0x0400043A RID: 1082
		public const int CurrentVersion = 4;

		// Token: 0x0400043B RID: 1083
		public int version;

		// Token: 0x0400043C RID: 1084
		public int cCreds;

		// Token: 0x0400043D RID: 1085
		public IntPtr certContextArray;

		// Token: 0x0400043E RID: 1086
		private IntPtr rootStore;

		// Token: 0x0400043F RID: 1087
		public int cMappers;

		// Token: 0x04000440 RID: 1088
		private IntPtr phMappers;

		// Token: 0x04000441 RID: 1089
		public int cSupportedAlgs;

		// Token: 0x04000442 RID: 1090
		private IntPtr palgSupportedAlgs;

		// Token: 0x04000443 RID: 1091
		public SchProtocols grbitEnabledProtocols;

		// Token: 0x04000444 RID: 1092
		public int dwMinimumCipherStrength;

		// Token: 0x04000445 RID: 1093
		public int dwMaximumCipherStrength;

		// Token: 0x04000446 RID: 1094
		public int dwSessionLifespan;

		// Token: 0x04000447 RID: 1095
		public SecureCredential.Flags dwFlags;

		// Token: 0x04000448 RID: 1096
		public int reserved;

		// Token: 0x0200023D RID: 573
		[Flags]
		public enum Flags
		{
			// Token: 0x04000F67 RID: 3943
			Zero = 0,
			// Token: 0x04000F68 RID: 3944
			NoSystemMapper = 2,
			// Token: 0x04000F69 RID: 3945
			NoNameCheck = 4,
			// Token: 0x04000F6A RID: 3946
			ValidateManual = 8,
			// Token: 0x04000F6B RID: 3947
			NoDefaultCred = 16,
			// Token: 0x04000F6C RID: 3948
			ValidateAuto = 32
		}
	}
}
