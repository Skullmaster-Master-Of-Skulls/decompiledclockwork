using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000070 RID: 112
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class Pkcs9SigningTime : Pkcs9AttributeObject
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x00016B84 File Offset: 0x00014D84
		public Pkcs9SigningTime() : this(DateTime.Now)
		{
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00016B91 File Offset: 0x00014D91
		public Pkcs9SigningTime(DateTime signingTime) : base("1.2.840.113549.1.9.5", Pkcs9SigningTime.Encode(signingTime))
		{
			this.m_signingTime = signingTime;
			this.m_decoded = true;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00016BB2 File Offset: 0x00014DB2
		public Pkcs9SigningTime(byte[] encodedSigningTime) : base("1.2.840.113549.1.9.5", encodedSigningTime)
		{
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00016BC0 File Offset: 0x00014DC0
		public DateTime SigningTime
		{
			get
			{
				if (!this.m_decoded && base.RawData != null)
				{
					this.Decode();
				}
				return this.m_signingTime;
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00016BDE File Offset: 0x00014DDE
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.m_decoded = false;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00016BF0 File Offset: 0x00014DF0
		[SecuritySafeCritical]
		private void Decode()
		{
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			if (!CAPI.DecodeObject(new IntPtr(17L), base.RawData, out safeLocalAllocHandle, out num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			long fileTime = Marshal.ReadInt64(safeLocalAllocHandle.DangerousGetHandle());
			safeLocalAllocHandle.Dispose();
			this.m_signingTime = DateTime.FromFileTimeUtc(fileTime);
			this.m_decoded = true;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00016C4C File Offset: 0x00014E4C
		[SecuritySafeCritical]
		private static byte[] Encode(DateTime signingTime)
		{
			long val = signingTime.ToFileTimeUtc();
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(long))));
			Marshal.WriteInt64(safeLocalAllocHandle.DangerousGetHandle(), val);
			byte[] result = new byte[0];
			if (!CAPI.EncodeObject("1.2.840.113549.1.9.5", safeLocalAllocHandle.DangerousGetHandle(), out result))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			safeLocalAllocHandle.Dispose();
			return result;
		}

		// Token: 0x040004CA RID: 1226
		private DateTime m_signingTime;

		// Token: 0x040004CB RID: 1227
		private bool m_decoded;
	}
}
