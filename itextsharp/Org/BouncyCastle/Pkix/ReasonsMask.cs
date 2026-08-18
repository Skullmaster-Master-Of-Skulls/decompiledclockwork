using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x020005A0 RID: 1440
	internal class ReasonsMask
	{
		// Token: 0x0600317C RID: 12668 RVA: 0x0013539A File Offset: 0x0013439A
		internal ReasonsMask(int reasons)
		{
			this._reasons = reasons;
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x001353A9 File Offset: 0x001343A9
		internal ReasonsMask() : this(0)
		{
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x001353B2 File Offset: 0x001343B2
		internal void AddReasons(ReasonsMask mask)
		{
			this._reasons |= mask.Reasons.IntValue;
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x001353CC File Offset: 0x001343CC
		internal bool IsAllReasons
		{
			get
			{
				return this._reasons == ReasonsMask.AllReasons._reasons;
			}
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x001353E0 File Offset: 0x001343E0
		internal ReasonsMask Intersect(ReasonsMask mask)
		{
			ReasonsMask reasonsMask = new ReasonsMask();
			reasonsMask.AddReasons(new ReasonsMask(this._reasons & mask.Reasons.IntValue));
			return reasonsMask;
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x00135411 File Offset: 0x00134411
		internal bool HasNewReasons(ReasonsMask mask)
		{
			return (this._reasons | (mask.Reasons.IntValue ^ this._reasons)) != 0;
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x00135432 File Offset: 0x00134432
		public ReasonFlags Reasons
		{
			get
			{
				return new ReasonFlags(this._reasons);
			}
		}

		// Token: 0x0400221E RID: 8734
		private int _reasons;

		// Token: 0x0400221F RID: 8735
		internal static readonly ReasonsMask AllReasons = new ReasonsMask(33023);
	}
}
