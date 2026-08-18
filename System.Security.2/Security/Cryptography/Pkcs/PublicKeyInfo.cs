using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000080 RID: 128
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class PublicKeyInfo
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x000044A9 File Offset: 0x000026A9
		private PublicKeyInfo()
		{
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00017D28 File Offset: 0x00015F28
		[SecurityCritical]
		internal PublicKeyInfo(CAPI.CERT_PUBLIC_KEY_INFO keyInfo)
		{
			this.m_algorithm = new AlgorithmIdentifier(keyInfo);
			this.m_keyValue = new byte[keyInfo.PublicKey.cbData];
			if (this.m_keyValue.Length != 0)
			{
				Marshal.Copy(keyInfo.PublicKey.pbData, this.m_keyValue, 0, this.m_keyValue.Length);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00017D85 File Offset: 0x00015F85
		public AlgorithmIdentifier Algorithm
		{
			get
			{
				return this.m_algorithm;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00017D8D File Offset: 0x00015F8D
		public byte[] KeyValue
		{
			get
			{
				return this.m_keyValue;
			}
		}

		// Token: 0x04000506 RID: 1286
		private AlgorithmIdentifier m_algorithm;

		// Token: 0x04000507 RID: 1287
		private byte[] m_keyValue;
	}
}
