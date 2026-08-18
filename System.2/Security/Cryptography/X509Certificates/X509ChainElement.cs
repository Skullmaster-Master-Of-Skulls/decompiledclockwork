using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200046E RID: 1134
	public class X509ChainElement
	{
		// Token: 0x06002A3E RID: 10814 RVA: 0x000C10B6 File Offset: 0x000BF2B6
		private X509ChainElement()
		{
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000C10C0 File Offset: 0x000BF2C0
		internal unsafe X509ChainElement(IntPtr pChainElement)
		{
			CAPIBase.CERT_CHAIN_ELEMENT cert_CHAIN_ELEMENT = new CAPIBase.CERT_CHAIN_ELEMENT(Marshal.SizeOf(typeof(CAPIBase.CERT_CHAIN_ELEMENT)));
			uint num = (uint)Marshal.ReadInt32(pChainElement);
			if ((ulong)num > (ulong)((long)Marshal.SizeOf(cert_CHAIN_ELEMENT)))
			{
				num = (uint)Marshal.SizeOf(cert_CHAIN_ELEMENT);
			}
			X509Utils.memcpy(pChainElement, new IntPtr((void*)(&cert_CHAIN_ELEMENT)), num);
			this.m_certificate = new X509Certificate2(cert_CHAIN_ELEMENT.pCertContext);
			if (cert_CHAIN_ELEMENT.pwszExtendedErrorInfo == IntPtr.Zero)
			{
				this.m_description = string.Empty;
			}
			else
			{
				this.m_description = Marshal.PtrToStringUni(cert_CHAIN_ELEMENT.pwszExtendedErrorInfo);
			}
			if (cert_CHAIN_ELEMENT.dwErrorStatus == 0U)
			{
				this.m_chainStatus = new X509ChainStatus[0];
				return;
			}
			this.m_chainStatus = X509Chain.GetChainStatusInformation(cert_CHAIN_ELEMENT.dwErrorStatus);
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x000C1182 File Offset: 0x000BF382
		public X509Certificate2 Certificate
		{
			get
			{
				return this.m_certificate;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x000C118A File Offset: 0x000BF38A
		public X509ChainStatus[] ChainElementStatus
		{
			get
			{
				return this.m_chainStatus;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x000C1192 File Offset: 0x000BF392
		public string Information
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x04002608 RID: 9736
		private X509Certificate2 m_certificate;

		// Token: 0x04002609 RID: 9737
		private X509ChainStatus[] m_chainStatus;

		// Token: 0x0400260A RID: 9738
		private string m_description;
	}
}
