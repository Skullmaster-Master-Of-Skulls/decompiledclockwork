using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.X509
{
	// Token: 0x020005FB RID: 1531
	public class X509CrlParser
	{
		// Token: 0x0600343F RID: 13375 RVA: 0x0014487B File Offset: 0x0014387B
		public X509CrlParser() : this(false)
		{
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x00144884 File Offset: 0x00143884
		public X509CrlParser(bool lazyAsn1)
		{
			this.lazyAsn1 = lazyAsn1;
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x00144894 File Offset: 0x00143894
		private X509Crl ReadPemCrl(Stream inStream)
		{
			Asn1Sequence asn1Sequence = X509CrlParser.PemCrlParser.ReadPemObject(inStream);
			if (asn1Sequence != null)
			{
				return this.CreateX509Crl(CertificateList.GetInstance(asn1Sequence));
			}
			return null;
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x001448C0 File Offset: 0x001438C0
		private X509Crl ReadDerCrl(Asn1InputStream dIn)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)dIn.ReadObject();
			if (asn1Sequence.Count > 1 && asn1Sequence[0] is DerObjectIdentifier && asn1Sequence[0].Equals(PkcsObjectIdentifiers.SignedData))
			{
				this.sCrlData = SignedData.GetInstance(Asn1Sequence.GetInstance((Asn1TaggedObject)asn1Sequence[1], true)).Crls;
				return this.GetCrl();
			}
			return this.CreateX509Crl(CertificateList.GetInstance(asn1Sequence));
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x00144938 File Offset: 0x00143938
		private X509Crl GetCrl()
		{
			if (this.sCrlData == null || this.sCrlDataObjectCount >= this.sCrlData.Count)
			{
				return null;
			}
			return this.CreateX509Crl(CertificateList.GetInstance(this.sCrlData[this.sCrlDataObjectCount++]));
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x00144989 File Offset: 0x00143989
		protected virtual X509Crl CreateX509Crl(CertificateList c)
		{
			return new X509Crl(c);
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x00144991 File Offset: 0x00143991
		public X509Crl ReadCrl(byte[] input)
		{
			return this.ReadCrl(new MemoryStream(input, false));
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x001449A0 File Offset: 0x001439A0
		public ICollection ReadCrls(byte[] input)
		{
			return this.ReadCrls(new MemoryStream(input, false));
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x001449B0 File Offset: 0x001439B0
		public X509Crl ReadCrl(Stream inStream)
		{
			if (inStream == null)
			{
				throw new ArgumentNullException("inStream");
			}
			if (!inStream.CanRead)
			{
				throw new ArgumentException("inStream must be read-able", "inStream");
			}
			if (this.currentCrlStream == null)
			{
				this.currentCrlStream = inStream;
				this.sCrlData = null;
				this.sCrlDataObjectCount = 0;
			}
			else if (this.currentCrlStream != inStream)
			{
				this.currentCrlStream = inStream;
				this.sCrlData = null;
				this.sCrlDataObjectCount = 0;
			}
			X509Crl result;
			try
			{
				if (this.sCrlData != null)
				{
					if (this.sCrlDataObjectCount != this.sCrlData.Count)
					{
						result = this.GetCrl();
					}
					else
					{
						this.sCrlData = null;
						this.sCrlDataObjectCount = 0;
						result = null;
					}
				}
				else
				{
					PushbackStream pushbackStream = new PushbackStream(inStream);
					int num = pushbackStream.ReadByte();
					if (num < 0)
					{
						result = null;
					}
					else
					{
						pushbackStream.Unread(num);
						if (num != 48)
						{
							result = this.ReadPemCrl(pushbackStream);
						}
						else
						{
							Asn1InputStream dIn = this.lazyAsn1 ? new LazyAsn1InputStream(pushbackStream) : new Asn1InputStream(pushbackStream);
							result = this.ReadDerCrl(dIn);
						}
					}
				}
			}
			catch (CrlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw new CrlException(ex2.ToString());
			}
			return result;
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x00144AD8 File Offset: 0x00143AD8
		public ICollection ReadCrls(Stream inStream)
		{
			IList list = new ArrayList();
			X509Crl value;
			while ((value = this.ReadCrl(inStream)) != null)
			{
				list.Add(value);
			}
			return list;
		}

		// Token: 0x0400232F RID: 9007
		private static readonly PemParser PemCrlParser = new PemParser("CRL");

		// Token: 0x04002330 RID: 9008
		private readonly bool lazyAsn1;

		// Token: 0x04002331 RID: 9009
		private Asn1Set sCrlData;

		// Token: 0x04002332 RID: 9010
		private int sCrlDataObjectCount;

		// Token: 0x04002333 RID: 9011
		private Stream currentCrlStream;
	}
}
