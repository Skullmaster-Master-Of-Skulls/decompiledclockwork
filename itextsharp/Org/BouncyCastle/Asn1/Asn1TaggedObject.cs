using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000152 RID: 338
	public abstract class Asn1TaggedObject : Asn1Object, Asn1TaggedObjectParser, IAsn1Convertible
	{
		// Token: 0x06000C19 RID: 3097 RVA: 0x00042D29 File Offset: 0x00041D29
		public static Asn1TaggedObject GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			if (explicitly)
			{
				return (Asn1TaggedObject)obj.GetObject();
			}
			throw new ArgumentException("implicitly tagged tagged object");
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00042D44 File Offset: 0x00041D44
		public static Asn1TaggedObject GetInstance(object obj)
		{
			if (obj == null || obj is Asn1TaggedObject)
			{
				return (Asn1TaggedObject)obj;
			}
			throw new ArgumentException("Unknown object in GetInstance: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00042D77 File Offset: 0x00041D77
		protected Asn1TaggedObject(int tagNo, Asn1Encodable obj)
		{
			this.explicitly = true;
			this.tagNo = tagNo;
			this.obj = obj;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00042D9B File Offset: 0x00041D9B
		protected Asn1TaggedObject(bool explicitly, int tagNo, Asn1Encodable obj)
		{
			this.explicitly = (explicitly || obj is IAsn1Choice);
			this.tagNo = tagNo;
			this.obj = obj;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00042DD0 File Offset: 0x00041DD0
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			Asn1TaggedObject asn1TaggedObject = asn1Object as Asn1TaggedObject;
			return asn1TaggedObject != null && (this.tagNo == asn1TaggedObject.tagNo && this.explicitly == asn1TaggedObject.explicitly) && object.Equals(this.GetObject(), asn1TaggedObject.GetObject());
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00042E18 File Offset: 0x00041E18
		protected override int Asn1GetHashCode()
		{
			int num = this.tagNo.GetHashCode();
			if (this.obj != null)
			{
				num ^= this.obj.GetHashCode();
			}
			return num;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00042E48 File Offset: 0x00041E48
		public int TagNo
		{
			get
			{
				return this.tagNo;
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00042E50 File Offset: 0x00041E50
		public bool IsExplicit()
		{
			return this.explicitly;
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00042E58 File Offset: 0x00041E58
		public bool IsEmpty()
		{
			return false;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00042E5B File Offset: 0x00041E5B
		public Asn1Object GetObject()
		{
			if (this.obj != null)
			{
				return this.obj.ToAsn1Object();
			}
			return null;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00042E74 File Offset: 0x00041E74
		public IAsn1Convertible GetObjectParser(int tag, bool isExplicit)
		{
			if (tag == 4)
			{
				return Asn1OctetString.GetInstance(this, isExplicit).Parser;
			}
			switch (tag)
			{
			case 16:
				return Asn1Sequence.GetInstance(this, isExplicit).Parser;
			case 17:
				return Asn1Set.GetInstance(this, isExplicit).Parser;
			default:
				if (isExplicit)
				{
					return this.GetObject();
				}
				throw Platform.CreateNotImplementedException("implicit tagging for tag: " + tag);
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00042EE0 File Offset: 0x00041EE0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[",
				this.tagNo,
				"]",
				this.obj
			});
		}

		// Token: 0x04000986 RID: 2438
		internal int tagNo;

		// Token: 0x04000987 RID: 2439
		internal bool explicitly = true;

		// Token: 0x04000988 RID: 2440
		internal Asn1Encodable obj;
	}
}
