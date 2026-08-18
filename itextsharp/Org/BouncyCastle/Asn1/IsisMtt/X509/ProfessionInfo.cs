using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X500;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000147 RID: 327
	public class ProfessionInfo : Asn1Encodable
	{
		// Token: 0x06000BD3 RID: 3027 RVA: 0x00041A04 File Offset: 0x00040A04
		public static ProfessionInfo GetInstance(object obj)
		{
			if (obj == null || obj is ProfessionInfo)
			{
				return (ProfessionInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ProfessionInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00041A58 File Offset: 0x00040A58
		private ProfessionInfo(Asn1Sequence seq)
		{
			if (seq.Count > 5)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			Asn1Encodable asn1Encodable = (Asn1Encodable)enumerator.Current;
			if (asn1Encodable is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Encodable;
				if (asn1TaggedObject.TagNo != 0)
				{
					throw new ArgumentException("Bad tag number: " + asn1TaggedObject.TagNo);
				}
				this.namingAuthority = NamingAuthority.GetInstance(asn1TaggedObject, true);
				enumerator.MoveNext();
				asn1Encodable = (Asn1Encodable)enumerator.Current;
			}
			this.professionItems = Asn1Sequence.GetInstance(asn1Encodable);
			if (enumerator.MoveNext())
			{
				asn1Encodable = (Asn1Encodable)enumerator.Current;
				if (asn1Encodable is Asn1Sequence)
				{
					this.professionOids = Asn1Sequence.GetInstance(asn1Encodable);
				}
				else if (asn1Encodable is DerPrintableString)
				{
					this.registrationNumber = DerPrintableString.GetInstance(asn1Encodable).GetString();
				}
				else
				{
					if (!(asn1Encodable is Asn1OctetString))
					{
						throw new ArgumentException("Bad object encountered: " + asn1Encodable.GetType().Name);
					}
					this.addProfessionInfo = Asn1OctetString.GetInstance(asn1Encodable);
				}
			}
			if (enumerator.MoveNext())
			{
				asn1Encodable = (Asn1Encodable)enumerator.Current;
				if (asn1Encodable is DerPrintableString)
				{
					this.registrationNumber = DerPrintableString.GetInstance(asn1Encodable).GetString();
				}
				else
				{
					if (!(asn1Encodable is DerOctetString))
					{
						throw new ArgumentException("Bad object encountered: " + asn1Encodable.GetType().Name);
					}
					this.addProfessionInfo = (DerOctetString)asn1Encodable;
				}
			}
			if (!enumerator.MoveNext())
			{
				return;
			}
			asn1Encodable = (Asn1Encodable)enumerator.Current;
			if (asn1Encodable is DerOctetString)
			{
				this.addProfessionInfo = (DerOctetString)asn1Encodable;
				return;
			}
			throw new ArgumentException("Bad object encountered: " + asn1Encodable.GetType().Name);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00041C21 File Offset: 0x00040C21
		public ProfessionInfo(NamingAuthority namingAuthority, DirectoryString[] professionItems, DerObjectIdentifier[] professionOids, string registrationNumber, Asn1OctetString addProfessionInfo)
		{
			this.namingAuthority = namingAuthority;
			this.professionItems = new DerSequence(professionItems);
			if (professionOids != null)
			{
				this.professionOids = new DerSequence(professionOids);
			}
			this.registrationNumber = registrationNumber;
			this.addProfessionInfo = addProfessionInfo;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00041C5C File Offset: 0x00040C5C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.namingAuthority != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.namingAuthority)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.professionItems
			});
			if (this.professionOids != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.professionOids
				});
			}
			if (this.registrationNumber != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerPrintableString(this.registrationNumber, true)
				});
			}
			if (this.addProfessionInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.addProfessionInfo
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x00041D21 File Offset: 0x00040D21
		public virtual Asn1OctetString AddProfessionInfo
		{
			get
			{
				return this.addProfessionInfo;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x00041D29 File Offset: 0x00040D29
		public virtual NamingAuthority NamingAuthority
		{
			get
			{
				return this.namingAuthority;
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00041D34 File Offset: 0x00040D34
		public virtual DirectoryString[] GetProfessionItems()
		{
			DirectoryString[] array = new DirectoryString[this.professionItems.Count];
			for (int i = 0; i < this.professionItems.Count; i++)
			{
				array[i] = DirectoryString.GetInstance(this.professionItems[i]);
			}
			return array;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00041D80 File Offset: 0x00040D80
		public virtual DerObjectIdentifier[] GetProfessionOids()
		{
			if (this.professionOids == null)
			{
				return new DerObjectIdentifier[0];
			}
			DerObjectIdentifier[] array = new DerObjectIdentifier[this.professionOids.Count];
			for (int i = 0; i < this.professionOids.Count; i++)
			{
				array[i] = DerObjectIdentifier.GetInstance(this.professionOids[i]);
			}
			return array;
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00041DD8 File Offset: 0x00040DD8
		public virtual string RegistrationNumber
		{
			get
			{
				return this.registrationNumber;
			}
		}

		// Token: 0x0400095B RID: 2395
		public static readonly DerObjectIdentifier Rechtsanwltin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".1");

		// Token: 0x0400095C RID: 2396
		public static readonly DerObjectIdentifier Rechtsanwalt = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".2");

		// Token: 0x0400095D RID: 2397
		public static readonly DerObjectIdentifier Rechtsbeistand = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".3");

		// Token: 0x0400095E RID: 2398
		public static readonly DerObjectIdentifier Steuerberaterin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".4");

		// Token: 0x0400095F RID: 2399
		public static readonly DerObjectIdentifier Steuerberater = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".5");

		// Token: 0x04000960 RID: 2400
		public static readonly DerObjectIdentifier Steuerbevollmchtigte = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".6");

		// Token: 0x04000961 RID: 2401
		public static readonly DerObjectIdentifier Steuerbevollmchtigter = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".7");

		// Token: 0x04000962 RID: 2402
		public static readonly DerObjectIdentifier Notarin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".8");

		// Token: 0x04000963 RID: 2403
		public static readonly DerObjectIdentifier Notar = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".9");

		// Token: 0x04000964 RID: 2404
		public static readonly DerObjectIdentifier Notarvertreterin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".10");

		// Token: 0x04000965 RID: 2405
		public static readonly DerObjectIdentifier Notarvertreter = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".11");

		// Token: 0x04000966 RID: 2406
		public static readonly DerObjectIdentifier Notariatsverwalterin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".12");

		// Token: 0x04000967 RID: 2407
		public static readonly DerObjectIdentifier Notariatsverwalter = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".13");

		// Token: 0x04000968 RID: 2408
		public static readonly DerObjectIdentifier Wirtschaftsprferin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".14");

		// Token: 0x04000969 RID: 2409
		public static readonly DerObjectIdentifier Wirtschaftsprfer = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".15");

		// Token: 0x0400096A RID: 2410
		public static readonly DerObjectIdentifier VereidigteBuchprferin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".16");

		// Token: 0x0400096B RID: 2411
		public static readonly DerObjectIdentifier VereidigterBuchprfer = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".17");

		// Token: 0x0400096C RID: 2412
		public static readonly DerObjectIdentifier Patentanwltin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".18");

		// Token: 0x0400096D RID: 2413
		public static readonly DerObjectIdentifier Patentanwalt = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".19");

		// Token: 0x0400096E RID: 2414
		private readonly NamingAuthority namingAuthority;

		// Token: 0x0400096F RID: 2415
		private readonly Asn1Sequence professionItems;

		// Token: 0x04000970 RID: 2416
		private readonly Asn1Sequence professionOids;

		// Token: 0x04000971 RID: 2417
		private readonly string registrationNumber;

		// Token: 0x04000972 RID: 2418
		private readonly Asn1OctetString addProfessionInfo;
	}
}
