using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020004C2 RID: 1218
	[ComVisible(true)]
	[Serializable]
	public sealed class Publisher : IIdentityPermissionFactory, IBuiltInEvidence
	{
		// Token: 0x060030C7 RID: 12487 RVA: 0x000A7518 File Offset: 0x000A6518
		internal Publisher()
		{
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000A7520 File Offset: 0x000A6520
		public Publisher(X509Certificate cert)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			this.m_cert = cert;
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000A753D File Offset: 0x000A653D
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new PublisherIdentityPermission(this.m_cert);
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000A754C File Offset: 0x000A654C
		public override bool Equals(object o)
		{
			Publisher publisher = o as Publisher;
			return publisher != null && Publisher.PublicKeyEquals(this.m_cert, publisher.m_cert);
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000A7578 File Offset: 0x000A6578
		internal static bool PublicKeyEquals(X509Certificate cert1, X509Certificate cert2)
		{
			if (cert1 == null)
			{
				return cert2 == null;
			}
			if (cert2 == null)
			{
				return false;
			}
			byte[] publicKey = cert1.GetPublicKey();
			string keyAlgorithm = cert1.GetKeyAlgorithm();
			byte[] keyAlgorithmParameters = cert1.GetKeyAlgorithmParameters();
			byte[] publicKey2 = cert2.GetPublicKey();
			string keyAlgorithm2 = cert2.GetKeyAlgorithm();
			byte[] keyAlgorithmParameters2 = cert2.GetKeyAlgorithmParameters();
			int num = publicKey.Length;
			if (num != publicKey2.Length)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (publicKey[i] != publicKey2[i])
				{
					return false;
				}
			}
			if (!keyAlgorithm.Equals(keyAlgorithm2))
			{
				return false;
			}
			num = keyAlgorithmParameters.Length;
			if (keyAlgorithmParameters2.Length != num)
			{
				return false;
			}
			for (int j = 0; j < num; j++)
			{
				if (keyAlgorithmParameters[j] != keyAlgorithmParameters2[j])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000A7623 File Offset: 0x000A6623
		public override int GetHashCode()
		{
			return this.m_cert.GetHashCode();
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060030CD RID: 12493 RVA: 0x000A7630 File Offset: 0x000A6630
		public X509Certificate Certificate
		{
			get
			{
				if (this.m_cert == null)
				{
					return null;
				}
				return new X509Certificate(this.m_cert);
			}
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000A7648 File Offset: 0x000A6648
		public object Copy()
		{
			Publisher publisher = new Publisher();
			if (this.m_cert != null)
			{
				publisher.m_cert = new X509Certificate(this.m_cert);
			}
			return publisher;
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000A7678 File Offset: 0x000A6678
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Publisher");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("X509v3Certificate", (this.m_cert != null) ? this.m_cert.GetRawCertDataString() : ""));
			return securityElement;
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000A76CC File Offset: 0x000A66CC
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position++] = '\u0001';
			byte[] rawCertData = this.Certificate.GetRawCertData();
			int num = rawCertData.Length;
			if (verbose)
			{
				BuiltInEvidenceHelper.CopyIntToCharArray(num, buffer, position);
				position += 2;
			}
			Buffer.InternalBlockCopy(rawCertData, 0, buffer, position * 2, num);
			return (num - 1) / 2 + 1 + position;
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x000A7718 File Offset: 0x000A6718
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			int num = (this.Certificate.GetRawCertData().Length - 1) / 2 + 1;
			if (verbose)
			{
				return num + 3;
			}
			return num + 1;
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000A7744 File Offset: 0x000A6744
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			int intFromCharArray = BuiltInEvidenceHelper.GetIntFromCharArray(buffer, position);
			position += 2;
			byte[] array = new byte[intFromCharArray];
			int num = (intFromCharArray - 1) / 2 + 1;
			Buffer.InternalBlockCopy(buffer, position * 2, array, 0, intFromCharArray);
			this.m_cert = new X509Certificate(array);
			return position + num;
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x000A7788 File Offset: 0x000A6788
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000A7798 File Offset: 0x000A6798
		internal object Normalize()
		{
			return new MemoryStream(this.m_cert.GetRawCertData())
			{
				Position = 0L
			};
		}

		// Token: 0x0400187A RID: 6266
		private X509Certificate m_cert;
	}
}
