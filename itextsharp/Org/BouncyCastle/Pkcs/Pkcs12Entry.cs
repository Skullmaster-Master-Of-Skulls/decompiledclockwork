using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x020001E0 RID: 480
	public abstract class Pkcs12Entry
	{
		// Token: 0x060012E2 RID: 4834 RVA: 0x0006C2A8 File Offset: 0x0006B2A8
		protected internal Pkcs12Entry(Hashtable attributes)
		{
			this.attributes = attributes;
			foreach (object obj in attributes)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!(dictionaryEntry.Key is string))
				{
					throw new ArgumentException("Attribute keys must be of type: " + typeof(string).FullName, "attributes");
				}
				if (!(dictionaryEntry.Value is Asn1Encodable))
				{
					throw new ArgumentException("Attribute values must be of type: " + typeof(Asn1Encodable).FullName, "attributes");
				}
			}
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0006C368 File Offset: 0x0006B368
		[Obsolete("Use 'object[index]' syntax instead")]
		public Asn1Encodable GetBagAttribute(DerObjectIdentifier oid)
		{
			return (Asn1Encodable)this.attributes[oid.Id];
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0006C380 File Offset: 0x0006B380
		[Obsolete("Use 'object[index]' syntax instead")]
		public Asn1Encodable GetBagAttribute(string oid)
		{
			return (Asn1Encodable)this.attributes[oid];
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0006C393 File Offset: 0x0006B393
		[Obsolete("Use 'BagAttributeKeys' property")]
		public IEnumerator GetBagAttributeKeys()
		{
			return this.attributes.Keys.GetEnumerator();
		}

		// Token: 0x1700037D RID: 893
		public Asn1Encodable this[DerObjectIdentifier oid]
		{
			get
			{
				return (Asn1Encodable)this.attributes[oid.Id];
			}
		}

		// Token: 0x1700037E RID: 894
		public Asn1Encodable this[string oid]
		{
			get
			{
				return (Asn1Encodable)this.attributes[oid];
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x0006C3D0 File Offset: 0x0006B3D0
		public IEnumerable BagAttributeKeys
		{
			get
			{
				return new EnumerableProxy(this.attributes.Keys);
			}
		}

		// Token: 0x04000D4F RID: 3407
		private readonly Hashtable attributes;
	}
}
