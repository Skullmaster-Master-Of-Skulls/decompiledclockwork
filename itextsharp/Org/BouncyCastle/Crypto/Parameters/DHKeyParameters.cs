using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000244 RID: 580
	public class DHKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06001660 RID: 5728 RVA: 0x000824FC File Offset: 0x000814FC
		protected DHKeyParameters(bool isPrivate, DHParameters parameters) : base(isPrivate)
		{
			this.parameters = parameters;
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x0008250C File Offset: 0x0008150C
		public DHParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00082514 File Offset: 0x00081514
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHKeyParameters dhkeyParameters = obj as DHKeyParameters;
			return dhkeyParameters != null && this.Equals(dhkeyParameters);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0008253A File Offset: 0x0008153A
		protected bool Equals(DHKeyParameters other)
		{
			return object.Equals(this.parameters, other.parameters) && base.Equals(other);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00082558 File Offset: 0x00081558
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.parameters != null)
			{
				num ^= this.parameters.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000F56 RID: 3926
		private readonly DHParameters parameters;
	}
}
