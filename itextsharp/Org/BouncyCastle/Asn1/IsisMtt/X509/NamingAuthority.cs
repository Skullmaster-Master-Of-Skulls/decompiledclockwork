using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X500;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x020000A9 RID: 169
	public class NamingAuthority : Asn1Encodable
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x0001BEDC File Offset: 0x0001AEDC
		public static NamingAuthority GetInstance(object obj)
		{
			if (obj == null || obj is NamingAuthority)
			{
				return (NamingAuthority)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new NamingAuthority((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001BF2E File Offset: 0x0001AF2E
		public static NamingAuthority GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return NamingAuthority.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001BF3C File Offset: 0x0001AF3C
		private NamingAuthority(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			IEnumerator enumerator = seq.GetEnumerator();
			if (enumerator.MoveNext())
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)enumerator.Current;
				if (asn1Encodable is DerObjectIdentifier)
				{
					this.namingAuthorityID = (DerObjectIdentifier)asn1Encodable;
				}
				else if (asn1Encodable is DerIA5String)
				{
					this.namingAuthorityUrl = DerIA5String.GetInstance(asn1Encodable).GetString();
				}
				else
				{
					if (!(asn1Encodable is IAsn1String))
					{
						throw new ArgumentException("Bad object encountered: " + asn1Encodable.GetType().Name);
					}
					this.namingAuthorityText = DirectoryString.GetInstance(asn1Encodable);
				}
			}
			if (enumerator.MoveNext())
			{
				Asn1Encodable asn1Encodable2 = (Asn1Encodable)enumerator.Current;
				if (asn1Encodable2 is DerIA5String)
				{
					this.namingAuthorityUrl = DerIA5String.GetInstance(asn1Encodable2).GetString();
				}
				else
				{
					if (!(asn1Encodable2 is IAsn1String))
					{
						throw new ArgumentException("Bad object encountered: " + asn1Encodable2.GetType().Name);
					}
					this.namingAuthorityText = DirectoryString.GetInstance(asn1Encodable2);
				}
			}
			if (!enumerator.MoveNext())
			{
				return;
			}
			Asn1Encodable asn1Encodable3 = (Asn1Encodable)enumerator.Current;
			if (asn1Encodable3 is IAsn1String)
			{
				this.namingAuthorityText = DirectoryString.GetInstance(asn1Encodable3);
				return;
			}
			throw new ArgumentException("Bad object encountered: " + asn1Encodable3.GetType().Name);
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0001C094 File Offset: 0x0001B094
		public virtual DerObjectIdentifier NamingAuthorityID
		{
			get
			{
				return this.namingAuthorityID;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0001C09C File Offset: 0x0001B09C
		public virtual DirectoryString NamingAuthorityText
		{
			get
			{
				return this.namingAuthorityText;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001C0A4 File Offset: 0x0001B0A4
		public virtual string NamingAuthorityUrl
		{
			get
			{
				return this.namingAuthorityUrl;
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001C0AC File Offset: 0x0001B0AC
		public NamingAuthority(DerObjectIdentifier namingAuthorityID, string namingAuthorityUrl, DirectoryString namingAuthorityText)
		{
			this.namingAuthorityID = namingAuthorityID;
			this.namingAuthorityUrl = namingAuthorityUrl;
			this.namingAuthorityText = namingAuthorityText;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001C0CC File Offset: 0x0001B0CC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.namingAuthorityID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.namingAuthorityID
				});
			}
			if (this.namingAuthorityUrl != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerIA5String(this.namingAuthorityUrl, true)
				});
			}
			if (this.namingAuthorityText != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.namingAuthorityText
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040002A1 RID: 673
		public static readonly DerObjectIdentifier IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttATNamingAuthorities + ".1");

		// Token: 0x040002A2 RID: 674
		private readonly DerObjectIdentifier namingAuthorityID;

		// Token: 0x040002A3 RID: 675
		private readonly string namingAuthorityUrl;

		// Token: 0x040002A4 RID: 676
		private readonly DirectoryString namingAuthorityText;
	}
}
