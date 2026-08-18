using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200009F RID: 159
	public class ExtendedKeyUsage : Asn1Encodable
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x0001B273 File Offset: 0x0001A273
		public static ExtendedKeyUsage GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ExtendedKeyUsage.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001B284 File Offset: 0x0001A284
		public static ExtendedKeyUsage GetInstance(object obj)
		{
			if (obj is ExtendedKeyUsage)
			{
				return (ExtendedKeyUsage)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ExtendedKeyUsage((Asn1Sequence)obj);
			}
			if (obj is X509Extension)
			{
				return ExtendedKeyUsage.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			throw new ArgumentException("Invalid ExtendedKeyUsage: " + obj.GetType().Name);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001B2E8 File Offset: 0x0001A2E8
		private ExtendedKeyUsage(Asn1Sequence seq)
		{
			this.seq = seq;
			foreach (object obj in seq)
			{
				if (!(obj is DerObjectIdentifier))
				{
					throw new ArgumentException("Only DerObjectIdentifier instances allowed in ExtendedKeyUsage.");
				}
				this.usageTable.Add(obj, obj);
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001B368 File Offset: 0x0001A368
		public ExtendedKeyUsage(params KeyPurposeID[] usages)
		{
			this.seq = new DerSequence(usages);
			foreach (KeyPurposeID keyPurposeID in usages)
			{
				this.usageTable.Add(keyPurposeID, keyPurposeID);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001B3B4 File Offset: 0x0001A3B4
		public ExtendedKeyUsage(ArrayList usages)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in usages)
			{
				Asn1Object asn1Object = (Asn1Object)obj;
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					asn1Object
				});
				this.usageTable.Add(asn1Object, asn1Object);
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001B44C File Offset: 0x0001A44C
		public bool HasKeyPurposeId(KeyPurposeID keyPurposeId)
		{
			return this.usageTable[keyPurposeId] != null;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001B460 File Offset: 0x0001A460
		public ArrayList GetUsages()
		{
			return new ArrayList(this.usageTable.Values);
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001B472 File Offset: 0x0001A472
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.usageTable.Count;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001B47F File Offset: 0x0001A47F
		public int Count
		{
			get
			{
				return this.usageTable.Count;
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001B48C File Offset: 0x0001A48C
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x0400028B RID: 651
		internal readonly Hashtable usageTable = new Hashtable();

		// Token: 0x0400028C RID: 652
		internal readonly Asn1Sequence seq;
	}
}
