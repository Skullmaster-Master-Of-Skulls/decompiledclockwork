using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200023E RID: 574
	public class ElGamalKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06001647 RID: 5703 RVA: 0x00082287 File Offset: 0x00081287
		protected ElGamalKeyParameters(bool isPrivate, ElGamalParameters parameters) : base(isPrivate)
		{
			this.parameters = parameters;
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x00082297 File Offset: 0x00081297
		public ElGamalParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x000822A0 File Offset: 0x000812A0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ElGamalKeyParameters elGamalKeyParameters = obj as ElGamalKeyParameters;
			return elGamalKeyParameters != null && this.Equals(elGamalKeyParameters);
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x000822C6 File Offset: 0x000812C6
		protected bool Equals(ElGamalKeyParameters other)
		{
			return object.Equals(this.parameters, other.parameters) && base.Equals(other);
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x000822E4 File Offset: 0x000812E4
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.parameters != null)
			{
				num ^= this.parameters.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000F4D RID: 3917
		private readonly ElGamalParameters parameters;
	}
}
