using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x020003CF RID: 975
	public class RespData : X509ExtensionBase
	{
		// Token: 0x060021E3 RID: 8675 RVA: 0x000CDA97 File Offset: 0x000CCA97
		public RespData(ResponseData data)
		{
			this.data = data;
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x000CDAA6 File Offset: 0x000CCAA6
		public int Version
		{
			get
			{
				return this.data.Version.Value.IntValue + 1;
			}
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x000CDABF File Offset: 0x000CCABF
		public RespID GetResponderId()
		{
			return new RespID(this.data.ResponderID);
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x000CDAD1 File Offset: 0x000CCAD1
		public DateTime ProducedAt
		{
			get
			{
				return this.data.ProducedAt.ToDateTime();
			}
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x000CDAE4 File Offset: 0x000CCAE4
		public SingleResp[] GetResponses()
		{
			Asn1Sequence responses = this.data.Responses;
			SingleResp[] array = new SingleResp[responses.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = new SingleResp(SingleResponse.GetInstance(responses[num]));
			}
			return array;
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000CDB2C File Offset: 0x000CCB2C
		public X509Extensions ResponseExtensions
		{
			get
			{
				return this.data.ResponseExtensions;
			}
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000CDB39 File Offset: 0x000CCB39
		protected override X509Extensions GetX509Extensions()
		{
			return this.ResponseExtensions;
		}

		// Token: 0x04001751 RID: 5969
		internal readonly ResponseData data;
	}
}
