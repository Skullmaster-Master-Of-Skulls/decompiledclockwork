using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004BD RID: 1213
	public class DsaKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002957 RID: 10583 RVA: 0x000FC6E4 File Offset: 0x000FB6E4
		public DsaKeyParameters(bool isPrivate, DsaParameters parameters) : base(isPrivate)
		{
			this.parameters = parameters;
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002958 RID: 10584 RVA: 0x000FC6F4 File Offset: 0x000FB6F4
		public DsaParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000FC6FC File Offset: 0x000FB6FC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaKeyParameters dsaKeyParameters = obj as DsaKeyParameters;
			return dsaKeyParameters != null && this.Equals(dsaKeyParameters);
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x000FC722 File Offset: 0x000FB722
		protected bool Equals(DsaKeyParameters other)
		{
			return object.Equals(this.parameters, other.parameters) && base.Equals(other);
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000FC740 File Offset: 0x000FB740
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.parameters != null)
			{
				num ^= this.parameters.GetHashCode();
			}
			return num;
		}

		// Token: 0x04001CEF RID: 7407
		private readonly DsaParameters parameters;
	}
}
