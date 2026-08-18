using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000071 RID: 113
	public abstract class DerStringBase : Asn1Object, IAsn1String
	{
		// Token: 0x060003A8 RID: 936
		public abstract string GetString();

		// Token: 0x060003A9 RID: 937 RVA: 0x00013AD4 File Offset: 0x00012AD4
		public override string ToString()
		{
			return this.GetString();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00013ADC File Offset: 0x00012ADC
		protected override int Asn1GetHashCode()
		{
			return this.GetString().GetHashCode();
		}
	}
}
