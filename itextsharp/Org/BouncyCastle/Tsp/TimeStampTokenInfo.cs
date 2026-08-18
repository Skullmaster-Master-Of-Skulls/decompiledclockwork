using System;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020004B2 RID: 1202
	public class TimeStampTokenInfo
	{
		// Token: 0x06002896 RID: 10390 RVA: 0x000F64EC File Offset: 0x000F54EC
		public TimeStampTokenInfo(TstInfo tstInfo)
		{
			this.tstInfo = tstInfo;
			try
			{
				this.genTime = tstInfo.GenTime.ToDateTime();
			}
			catch (Exception ex)
			{
				throw new TspException("unable to parse genTime field: " + ex.Message);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x000F6540 File Offset: 0x000F5540
		public bool IsOrdered
		{
			get
			{
				return this.tstInfo.Ordering.IsTrue;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x000F6552 File Offset: 0x000F5552
		public Accuracy Accuracy
		{
			get
			{
				return this.tstInfo.Accuracy;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002899 RID: 10393 RVA: 0x000F655F File Offset: 0x000F555F
		public DateTime GenTime
		{
			get
			{
				return this.genTime;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600289A RID: 10394 RVA: 0x000F6567 File Offset: 0x000F5567
		public GenTimeAccuracy GenTimeAccuracy
		{
			get
			{
				if (this.Accuracy != null)
				{
					return new GenTimeAccuracy(this.Accuracy);
				}
				return null;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600289B RID: 10395 RVA: 0x000F657E File Offset: 0x000F557E
		public string Policy
		{
			get
			{
				return this.tstInfo.Policy.Id;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600289C RID: 10396 RVA: 0x000F6590 File Offset: 0x000F5590
		public BigInteger SerialNumber
		{
			get
			{
				return this.tstInfo.SerialNumber.Value;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x000F65A2 File Offset: 0x000F55A2
		public GeneralName Tsa
		{
			get
			{
				return this.tstInfo.Tsa;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600289E RID: 10398 RVA: 0x000F65AF File Offset: 0x000F55AF
		public BigInteger Nonce
		{
			get
			{
				if (this.tstInfo.Nonce != null)
				{
					return this.tstInfo.Nonce.Value;
				}
				return null;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600289F RID: 10399 RVA: 0x000F65D0 File Offset: 0x000F55D0
		public string MessageImprintAlgOid
		{
			get
			{
				return this.tstInfo.MessageImprint.HashAlgorithm.ObjectID.Id;
			}
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000F65EC File Offset: 0x000F55EC
		public byte[] GetMessageImprintDigest()
		{
			return this.tstInfo.MessageImprint.GetHashedMessage();
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000F65FE File Offset: 0x000F55FE
		public byte[] GetEncoded()
		{
			return this.tstInfo.GetEncoded();
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x000F660B File Offset: 0x000F560B
		public TstInfo TstInfo
		{
			get
			{
				return this.tstInfo;
			}
		}

		// Token: 0x04001CB0 RID: 7344
		private TstInfo tstInfo;

		// Token: 0x04001CB1 RID: 7345
		private DateTime genTime;
	}
}
