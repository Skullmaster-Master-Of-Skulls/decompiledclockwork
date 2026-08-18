using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000201 RID: 513
	public class Target : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060013CC RID: 5068 RVA: 0x0007236C File Offset: 0x0007136C
		public static Target GetInstance(object obj)
		{
			if (obj is Target)
			{
				return (Target)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new Target((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x000723BC File Offset: 0x000713BC
		private Target(Asn1TaggedObject tagObj)
		{
			switch (tagObj.TagNo)
			{
			case 0:
				this.targetName = GeneralName.GetInstance(tagObj, true);
				return;
			case 1:
				this.targetGroup = GeneralName.GetInstance(tagObj, true);
				return;
			default:
				throw new ArgumentException("unknown tag: " + tagObj.TagNo);
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0007241C File Offset: 0x0007141C
		public Target(Target.Choice type, GeneralName name) : this(new DerTaggedObject((int)type, name))
		{
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0007242B File Offset: 0x0007142B
		public virtual GeneralName TargetGroup
		{
			get
			{
				return this.targetGroup;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00072433 File Offset: 0x00071433
		public virtual GeneralName TargetName
		{
			get
			{
				return this.targetName;
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0007243B File Offset: 0x0007143B
		public override Asn1Object ToAsn1Object()
		{
			if (this.targetName != null)
			{
				return new DerTaggedObject(true, 0, this.targetName);
			}
			return new DerTaggedObject(true, 1, this.targetGroup);
		}

		// Token: 0x04000DB9 RID: 3513
		private readonly GeneralName targetName;

		// Token: 0x04000DBA RID: 3514
		private readonly GeneralName targetGroup;

		// Token: 0x02000202 RID: 514
		public enum Choice
		{
			// Token: 0x04000DBC RID: 3516
			Name,
			// Token: 0x04000DBD RID: 3517
			Group
		}
	}
}
