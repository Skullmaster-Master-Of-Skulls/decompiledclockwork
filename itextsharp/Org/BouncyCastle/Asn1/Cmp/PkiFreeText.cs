using System;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020001BF RID: 447
	public class PkiFreeText : Asn1Encodable
	{
		// Token: 0x060010D4 RID: 4308 RVA: 0x0005F7FC File Offset: 0x0005E7FC
		public static PkiFreeText GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return PkiFreeText.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0005F80C File Offset: 0x0005E80C
		public static PkiFreeText GetInstance(object obj)
		{
			if (obj is PkiFreeText)
			{
				return (PkiFreeText)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PkiFreeText((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0005F85C File Offset: 0x0005E85C
		public PkiFreeText(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				if (!(obj is DerUtf8String))
				{
					throw new ArgumentException("attempt to insert non UTF8 STRING into PkiFreeText");
				}
			}
			this.strings = seq;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0005F8C4 File Offset: 0x0005E8C4
		public PkiFreeText(DerUtf8String p)
		{
			this.strings = new DerSequence(p);
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0005F8D8 File Offset: 0x0005E8D8
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.strings.Count;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0005F8E5 File Offset: 0x0005E8E5
		public int Count
		{
			get
			{
				return this.strings.Count;
			}
		}

		// Token: 0x17000331 RID: 817
		public DerUtf8String this[int index]
		{
			get
			{
				return (DerUtf8String)this.strings[index];
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0005F905 File Offset: 0x0005E905
		[Obsolete("Use 'object[index]' syntax instead")]
		public DerUtf8String GetStringAt(int index)
		{
			return this[index];
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0005F90E File Offset: 0x0005E90E
		public override Asn1Object ToAsn1Object()
		{
			return this.strings;
		}

		// Token: 0x04000C38 RID: 3128
		internal Asn1Sequence strings;
	}
}
