using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000624 RID: 1572
	public class Targets : Asn1Encodable
	{
		// Token: 0x06003567 RID: 13671 RVA: 0x0014B590 File Offset: 0x0014A590
		public static Targets GetInstance(object obj)
		{
			if (obj is Targets)
			{
				return (Targets)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Targets((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x0014B5DF File Offset: 0x0014A5DF
		private Targets(Asn1Sequence targets)
		{
			this.targets = targets;
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x0014B5EE File Offset: 0x0014A5EE
		public Targets(Target[] targets)
		{
			this.targets = new DerSequence(targets);
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x0014B604 File Offset: 0x0014A604
		public virtual Target[] GetTargets()
		{
			Target[] array = new Target[this.targets.Count];
			for (int i = 0; i < this.targets.Count; i++)
			{
				array[i] = Target.GetInstance(this.targets[i]);
			}
			return array;
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x0014B64D File Offset: 0x0014A64D
		public override Asn1Object ToAsn1Object()
		{
			return this.targets;
		}

		// Token: 0x040023AD RID: 9133
		private readonly Asn1Sequence targets;
	}
}
