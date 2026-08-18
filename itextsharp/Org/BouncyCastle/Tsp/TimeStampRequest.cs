using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020004FD RID: 1277
	public class TimeStampRequest : X509ExtensionBase
	{
		// Token: 0x06002BA6 RID: 11174 RVA: 0x001087FC File Offset: 0x001077FC
		public TimeStampRequest(TimeStampReq req)
		{
			this.req = req;
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x0010880B File Offset: 0x0010780B
		public TimeStampRequest(byte[] req) : this(new Asn1InputStream(req))
		{
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x00108819 File Offset: 0x00107819
		public TimeStampRequest(Stream input) : this(new Asn1InputStream(input))
		{
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x00108828 File Offset: 0x00107828
		private TimeStampRequest(Asn1InputStream str)
		{
			try
			{
				this.req = TimeStampReq.GetInstance(str.ReadObject());
			}
			catch (InvalidCastException arg)
			{
				throw new IOException("malformed request: " + arg);
			}
			catch (ArgumentException arg2)
			{
				throw new IOException("malformed request: " + arg2);
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x00108890 File Offset: 0x00107890
		public int Version
		{
			get
			{
				return this.req.Version.Value.IntValue;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x001088A7 File Offset: 0x001078A7
		public string MessageImprintAlgOid
		{
			get
			{
				return this.req.MessageImprint.HashAlgorithm.ObjectID.Id;
			}
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x001088C3 File Offset: 0x001078C3
		public byte[] GetMessageImprintDigest()
		{
			return this.req.MessageImprint.GetHashedMessage();
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x001088D5 File Offset: 0x001078D5
		public string ReqPolicy
		{
			get
			{
				if (this.req.ReqPolicy != null)
				{
					return this.req.ReqPolicy.Id;
				}
				return null;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002BAE RID: 11182 RVA: 0x001088F6 File Offset: 0x001078F6
		public BigInteger Nonce
		{
			get
			{
				if (this.req.Nonce != null)
				{
					return this.req.Nonce.Value;
				}
				return null;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x00108917 File Offset: 0x00107917
		public bool CertReq
		{
			get
			{
				return this.req.CertReq != null && this.req.CertReq.IsTrue;
			}
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x00108938 File Offset: 0x00107938
		public void Validate(IList algorithms, IList policies, IList extensions)
		{
			if (!algorithms.Contains(this.MessageImprintAlgOid))
			{
				throw new TspValidationException("request contains unknown algorithm.", 128);
			}
			if (policies != null && this.ReqPolicy != null && !policies.Contains(this.ReqPolicy))
			{
				throw new TspValidationException("request contains unknown policy.", 256);
			}
			if (this.Extensions != null && extensions != null)
			{
				foreach (object obj in this.Extensions.ExtensionOids)
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
					if (!extensions.Contains(derObjectIdentifier.Id))
					{
						throw new TspValidationException("request contains unknown extension.", 8388608);
					}
				}
			}
			int digestLength = TspUtil.GetDigestLength(this.MessageImprintAlgOid);
			if (digestLength != this.GetMessageImprintDigest().Length)
			{
				throw new TspValidationException("imprint digest the wrong length.", 4);
			}
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x00108A24 File Offset: 0x00107A24
		public byte[] GetEncoded()
		{
			return this.req.GetEncoded();
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x00108A31 File Offset: 0x00107A31
		internal X509Extensions Extensions
		{
			get
			{
				return this.req.Extensions;
			}
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x00108A3E File Offset: 0x00107A3E
		protected override X509Extensions GetX509Extensions()
		{
			return this.Extensions;
		}

		// Token: 0x04001E3D RID: 7741
		private TimeStampReq req;
	}
}
