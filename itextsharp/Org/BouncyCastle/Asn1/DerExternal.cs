using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200020D RID: 525
	public class DerExternal : Asn1Object
	{
		// Token: 0x06001411 RID: 5137 RVA: 0x000730F4 File Offset: 0x000720F4
		public DerExternal(Asn1EncodableVector vector)
		{
			int num = 0;
			Asn1Object asn1Object = vector[num].ToAsn1Object();
			if (asn1Object is DerObjectIdentifier)
			{
				this.directReference = (DerObjectIdentifier)asn1Object;
				num++;
				asn1Object = vector[num].ToAsn1Object();
			}
			if (asn1Object is DerInteger)
			{
				this.indirectReference = (DerInteger)asn1Object;
				num++;
				asn1Object = vector[num].ToAsn1Object();
			}
			if (!(asn1Object is DerTaggedObject))
			{
				this.dataValueDescriptor = asn1Object;
				num++;
				asn1Object = vector[num].ToAsn1Object();
			}
			if (!(asn1Object is DerTaggedObject))
			{
				throw new InvalidOperationException("No tagged object found in vector. Structure doesn't seem to be of type External");
			}
			DerTaggedObject derTaggedObject = (DerTaggedObject)asn1Object;
			this.Encoding = derTaggedObject.TagNo;
			if (this.encoding < 0 || this.encoding > 2)
			{
				throw new InvalidOperationException("invalid encoding value");
			}
			this.externalContent = derTaggedObject.ToAsn1Object();
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x000731CF File Offset: 0x000721CF
		public DerExternal(DerObjectIdentifier directReference, DerInteger indirectReference, Asn1Object dataValueDescriptor, DerTaggedObject externalData) : this(directReference, indirectReference, dataValueDescriptor, externalData.TagNo, externalData.ToAsn1Object())
		{
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x000731E8 File Offset: 0x000721E8
		public DerExternal(DerObjectIdentifier directReference, DerInteger indirectReference, Asn1Object dataValueDescriptor, int encoding, Asn1Object externalData)
		{
			this.DirectReference = directReference;
			this.IndirectReference = indirectReference;
			this.DataValueDescriptor = dataValueDescriptor;
			this.Encoding = encoding;
			this.ExternalContent = externalData.ToAsn1Object();
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0007321C File Offset: 0x0007221C
		internal override void Encode(DerOutputStream derOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			DerExternal.WriteEncodable(memoryStream, this.directReference);
			DerExternal.WriteEncodable(memoryStream, this.indirectReference);
			DerExternal.WriteEncodable(memoryStream, this.dataValueDescriptor);
			DerExternal.WriteEncodable(memoryStream, new DerTaggedObject(8, this.externalContent));
			derOut.WriteEncoded(32, 8, memoryStream.ToArray());
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00073274 File Offset: 0x00072274
		protected override int Asn1GetHashCode()
		{
			int num = this.externalContent.GetHashCode();
			if (this.directReference != null)
			{
				num ^= this.directReference.GetHashCode();
			}
			if (this.indirectReference != null)
			{
				num ^= this.indirectReference.GetHashCode();
			}
			if (this.dataValueDescriptor != null)
			{
				num ^= this.dataValueDescriptor.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x000732D0 File Offset: 0x000722D0
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			if (this == asn1Object)
			{
				return true;
			}
			DerExternal derExternal = asn1Object as DerExternal;
			return derExternal != null && (object.Equals(this.directReference, derExternal.directReference) && object.Equals(this.indirectReference, derExternal.indirectReference) && object.Equals(this.dataValueDescriptor, derExternal.dataValueDescriptor)) && this.externalContent.Equals(derExternal.externalContent);
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x0007333B File Offset: 0x0007233B
		// (set) Token: 0x06001418 RID: 5144 RVA: 0x00073343 File Offset: 0x00072343
		public Asn1Object DataValueDescriptor
		{
			get
			{
				return this.dataValueDescriptor;
			}
			set
			{
				this.dataValueDescriptor = value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0007334C File Offset: 0x0007234C
		// (set) Token: 0x0600141A RID: 5146 RVA: 0x00073354 File Offset: 0x00072354
		public DerObjectIdentifier DirectReference
		{
			get
			{
				return this.directReference;
			}
			set
			{
				this.directReference = value;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0007335D File Offset: 0x0007235D
		// (set) Token: 0x0600141C RID: 5148 RVA: 0x00073365 File Offset: 0x00072365
		public int Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				if (this.encoding < 0 || this.encoding > 2)
				{
					throw new InvalidOperationException("invalid encoding value: " + this.encoding);
				}
				this.encoding = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0007339B File Offset: 0x0007239B
		// (set) Token: 0x0600141E RID: 5150 RVA: 0x000733A3 File Offset: 0x000723A3
		public Asn1Object ExternalContent
		{
			get
			{
				return this.externalContent;
			}
			set
			{
				this.externalContent = value;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x000733AC File Offset: 0x000723AC
		// (set) Token: 0x06001420 RID: 5152 RVA: 0x000733B4 File Offset: 0x000723B4
		public DerInteger IndirectReference
		{
			get
			{
				return this.indirectReference;
			}
			set
			{
				this.indirectReference = value;
			}
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x000733C0 File Offset: 0x000723C0
		private static void WriteEncodable(MemoryStream ms, Asn1Encodable e)
		{
			if (e != null)
			{
				byte[] derEncoded = e.GetDerEncoded();
				ms.Write(derEncoded, 0, derEncoded.Length);
			}
		}

		// Token: 0x04000DDA RID: 3546
		private DerObjectIdentifier directReference;

		// Token: 0x04000DDB RID: 3547
		private DerInteger indirectReference;

		// Token: 0x04000DDC RID: 3548
		private Asn1Object dataValueDescriptor;

		// Token: 0x04000DDD RID: 3549
		private int encoding;

		// Token: 0x04000DDE RID: 3550
		private Asn1Object externalContent;
	}
}
