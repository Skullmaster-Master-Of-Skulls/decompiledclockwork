using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000141 RID: 321
	public class TargetInformation : Asn1Encodable
	{
		// Token: 0x06000BAE RID: 2990 RVA: 0x00040EB8 File Offset: 0x0003FEB8
		public static TargetInformation GetInstance(object obj)
		{
			if (obj is TargetInformation)
			{
				return (TargetInformation)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new TargetInformation((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00040F07 File Offset: 0x0003FF07
		private TargetInformation(Asn1Sequence targets)
		{
			this.targets = targets;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00040F18 File Offset: 0x0003FF18
		public virtual Targets[] GetTargetsObjects()
		{
			Targets[] array = new Targets[this.targets.Count];
			for (int i = 0; i < this.targets.Count; i++)
			{
				array[i] = Targets.GetInstance(this.targets[i]);
			}
			return array;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00040F61 File Offset: 0x0003FF61
		public TargetInformation(Targets targets)
		{
			this.targets = new DerSequence(targets);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00040F75 File Offset: 0x0003FF75
		public TargetInformation(Target[] targets) : this(new Targets(targets))
		{
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00040F83 File Offset: 0x0003FF83
		public override Asn1Object ToAsn1Object()
		{
			return this.targets;
		}

		// Token: 0x04000924 RID: 2340
		private readonly Asn1Sequence targets;
	}
}
