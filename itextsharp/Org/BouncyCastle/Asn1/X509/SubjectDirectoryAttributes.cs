using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200055D RID: 1373
	public class SubjectDirectoryAttributes : Asn1Encodable
	{
		// Token: 0x06002F47 RID: 12103 RVA: 0x00125C9C File Offset: 0x00124C9C
		public static SubjectDirectoryAttributes GetInstance(object obj)
		{
			if (obj == null || obj is SubjectDirectoryAttributes)
			{
				return (SubjectDirectoryAttributes)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SubjectDirectoryAttributes((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x00125CF0 File Offset: 0x00124CF0
		private SubjectDirectoryAttributes(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(obj);
				this.attributes.Add(AttributeX509.GetInstance(instance));
			}
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x00125D64 File Offset: 0x00124D64
		public SubjectDirectoryAttributes(ArrayList attributes)
		{
			this.attributes.AddRange(attributes);
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x00125D84 File Offset: 0x00124D84
		public override Asn1Object ToAsn1Object()
		{
			AttributeX509[] v = (AttributeX509[])this.attributes.ToArray(typeof(AttributeX509));
			return new DerSequence(v);
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x00125DB2 File Offset: 0x00124DB2
		public IEnumerable Attributes
		{
			get
			{
				return new EnumerableProxy(this.attributes);
			}
		}

		// Token: 0x0400209C RID: 8348
		private readonly ArrayList attributes = new ArrayList();
	}
}
