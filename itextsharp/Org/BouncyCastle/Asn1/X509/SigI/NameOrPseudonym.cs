using System;
using Org.BouncyCastle.Asn1.X500;

namespace Org.BouncyCastle.Asn1.X509.SigI
{
	// Token: 0x0200055E RID: 1374
	public class NameOrPseudonym : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06002F4C RID: 12108 RVA: 0x00125DC0 File Offset: 0x00124DC0
		public static NameOrPseudonym GetInstance(object obj)
		{
			if (obj == null || obj is NameOrPseudonym)
			{
				return (NameOrPseudonym)obj;
			}
			if (obj is IAsn1String)
			{
				return new NameOrPseudonym(DirectoryString.GetInstance(obj));
			}
			if (obj is Asn1Sequence)
			{
				return new NameOrPseudonym((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x00125E26 File Offset: 0x00124E26
		public NameOrPseudonym(DirectoryString pseudonym)
		{
			this.pseudonym = pseudonym;
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x00125E38 File Offset: 0x00124E38
		private NameOrPseudonym(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			if (!(seq[0] is IAsn1String))
			{
				throw new ArgumentException("Bad object encountered: " + seq[0].GetType().Name);
			}
			this.surname = DirectoryString.GetInstance(seq[0]);
			this.givenName = Asn1Sequence.GetInstance(seq[1]);
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x00125EC2 File Offset: 0x00124EC2
		public NameOrPseudonym(string pseudonym) : this(new DirectoryString(pseudonym))
		{
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x00125ED0 File Offset: 0x00124ED0
		public NameOrPseudonym(DirectoryString surname, Asn1Sequence givenName)
		{
			this.surname = surname;
			this.givenName = givenName;
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002F51 RID: 12113 RVA: 0x00125EE6 File Offset: 0x00124EE6
		public DirectoryString Pseudonym
		{
			get
			{
				return this.pseudonym;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x00125EEE File Offset: 0x00124EEE
		public DirectoryString Surname
		{
			get
			{
				return this.surname;
			}
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x00125EF8 File Offset: 0x00124EF8
		public DirectoryString[] GetGivenName()
		{
			DirectoryString[] array = new DirectoryString[this.givenName.Count];
			int num = 0;
			foreach (object obj in this.givenName)
			{
				array[num++] = DirectoryString.GetInstance(obj);
			}
			return array;
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x00125F6C File Offset: 0x00124F6C
		public override Asn1Object ToAsn1Object()
		{
			if (this.pseudonym != null)
			{
				return this.pseudonym.ToAsn1Object();
			}
			return new DerSequence(new Asn1Encodable[]
			{
				this.surname,
				this.givenName
			});
		}

		// Token: 0x0400209D RID: 8349
		private readonly DirectoryString pseudonym;

		// Token: 0x0400209E RID: 8350
		private readonly DirectoryString surname;

		// Token: 0x0400209F RID: 8351
		private readonly Asn1Sequence givenName;
	}
}
