using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000187 RID: 391
	public class Certificate
	{
		// Token: 0x06000F48 RID: 3912 RVA: 0x0005833C File Offset: 0x0005733C
		internal static Certificate Parse(Stream inStr)
		{
			int i = TlsUtilities.ReadUint24(inStr);
			ArrayList arrayList = new ArrayList();
			while (i > 0)
			{
				int num = TlsUtilities.ReadUint24(inStr);
				i -= 3 + num;
				byte[] array = new byte[num];
				TlsUtilities.ReadFully(array, inStr);
				MemoryStream memoryStream = new MemoryStream(array, false);
				Asn1Object obj = Asn1Object.FromStream(memoryStream);
				arrayList.Add(X509CertificateStructure.GetInstance(obj));
				if (memoryStream.Position < memoryStream.Length)
				{
					throw new ArgumentException("Sorry, there is garbage data left after the certificate");
				}
			}
			X509CertificateStructure[] array2 = (X509CertificateStructure[])arrayList.ToArray(typeof(X509CertificateStructure));
			return new Certificate(array2);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x000583D4 File Offset: 0x000573D4
		internal void Encode(Stream outStr)
		{
			ArrayList arrayList = new ArrayList();
			int num = 0;
			foreach (X509CertificateStructure x509CertificateStructure in this.certs)
			{
				byte[] encoded = x509CertificateStructure.GetEncoded("DER");
				arrayList.Add(encoded);
				num += encoded.Length + 3;
			}
			TlsUtilities.WriteUint24(num + 3, outStr);
			TlsUtilities.WriteUint24(num, outStr);
			foreach (object obj in arrayList)
			{
				byte[] buf = (byte[])obj;
				TlsUtilities.WriteOpaque24(buf, outStr);
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00058488 File Offset: 0x00057488
		internal Certificate(X509CertificateStructure[] certs)
		{
			this.certs = certs;
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00058497 File Offset: 0x00057497
		public X509CertificateStructure[] GetCerts()
		{
			return (X509CertificateStructure[])this.certs.Clone();
		}

		// Token: 0x04000B19 RID: 2841
		internal X509CertificateStructure[] certs;
	}
}
