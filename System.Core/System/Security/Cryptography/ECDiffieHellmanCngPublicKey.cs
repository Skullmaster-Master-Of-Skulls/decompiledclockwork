using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x020000FD RID: 253
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class ECDiffieHellmanCngPublicKey : ECDiffieHellmanPublicKey
	{
		// Token: 0x06000841 RID: 2113 RVA: 0x0001C064 File Offset: 0x0001A264
		[SecuritySafeCritical]
		internal ECDiffieHellmanCngPublicKey(byte[] keyBlob, string curveName, CngKeyBlobFormat format) : base(keyBlob)
		{
			this.m_format = format;
			this.m_curveName = curveName;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0001C07B File Offset: 0x0001A27B
		public CngKeyBlobFormat BlobFormat
		{
			get
			{
				return this.m_format;
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001C083 File Offset: 0x0001A283
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001C08C File Offset: 0x0001A28C
		[SecuritySafeCritical]
		public static ECDiffieHellmanPublicKey FromByteArray(byte[] publicKeyBlob, CngKeyBlobFormat format)
		{
			if (publicKeyBlob == null)
			{
				throw new ArgumentNullException("publicKeyBlob");
			}
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			ECDiffieHellmanPublicKey result;
			using (CngKey cngKey = CngKey.Import(publicKeyBlob, format))
			{
				if (cngKey.AlgorithmGroup != CngAlgorithmGroup.ECDiffieHellman)
				{
					throw new ArgumentException(SR.GetString("Cryptography_ArgECDHRequiresECDHKey"));
				}
				result = new ECDiffieHellmanCngPublicKey(publicKeyBlob, null, format);
			}
			return result;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001C10C File Offset: 0x0001A30C
		internal static ECDiffieHellmanCngPublicKey FromKey(CngKey key)
		{
			CngKeyBlobFormat format;
			string curveName;
			byte[] keyBlob = ECCng.ExportKeyBlob(key, false, out format, out curveName);
			return new ECDiffieHellmanCngPublicKey(keyBlob, curveName, format);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001C130 File Offset: 0x0001A330
		[SecuritySafeCritical]
		public static ECDiffieHellmanCngPublicKey FromXmlString(string xml)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			bool flag;
			ECParameters ecparameters = Rfc4050KeyFormatter.FromXml(xml, out flag);
			if (!flag)
			{
				throw new ArgumentException(SR.GetString("Cryptography_ArgECDHRequiresECDHKey"), "xml");
			}
			CngKeyBlobFormat format;
			string curveName;
			byte[] keyBlob = ECCng.EcdhParametersToBlob(ref ecparameters, out format, out curveName);
			return new ECDiffieHellmanCngPublicKey(keyBlob, curveName, format);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001C182 File Offset: 0x0001A382
		public CngKey Import()
		{
			return CngKey.Import(this.ToByteArray(), this.m_curveName, this.BlobFormat);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001C19C File Offset: 0x0001A39C
		public override string ToXmlString()
		{
			ECParameters parameters = this.ExportParameters();
			return Rfc4050KeyFormatter.ToXml(parameters, true);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001C1B8 File Offset: 0x0001A3B8
		public override ECParameters ExportExplicitParameters()
		{
			ECParameters result;
			using (CngKey cngKey = this.Import())
			{
				result = ECCng.ExportExplicitParameters(cngKey, false);
			}
			return result;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001C1F4 File Offset: 0x0001A3F4
		public override ECParameters ExportParameters()
		{
			ECParameters result;
			using (CngKey cngKey = this.Import())
			{
				result = ECCng.ExportParameters(cngKey, false);
			}
			return result;
		}

		// Token: 0x04000673 RID: 1651
		private CngKeyBlobFormat m_format;

		// Token: 0x04000674 RID: 1652
		[OptionalField]
		private string m_curveName;
	}
}
