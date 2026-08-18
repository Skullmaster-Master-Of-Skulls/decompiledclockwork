using System;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x02000306 RID: 774
	public class TypeOfBiometricData : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06001C61 RID: 7265 RVA: 0x000AA560 File Offset: 0x000A9560
		public static TypeOfBiometricData GetInstance(object obj)
		{
			if (obj == null || obj is TypeOfBiometricData)
			{
				return (TypeOfBiometricData)obj;
			}
			if (obj is DerInteger)
			{
				DerInteger instance = DerInteger.GetInstance(obj);
				int intValue = instance.Value.IntValue;
				return new TypeOfBiometricData(intValue);
			}
			if (obj is DerObjectIdentifier)
			{
				DerObjectIdentifier instance2 = DerObjectIdentifier.GetInstance(obj);
				return new TypeOfBiometricData(instance2);
			}
			throw new ArgumentException("unknown object in GetInstance: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x000AA5D6 File Offset: 0x000A95D6
		public TypeOfBiometricData(int predefinedBiometricType)
		{
			if (predefinedBiometricType == 0 || predefinedBiometricType == 1)
			{
				this.obj = new DerInteger(predefinedBiometricType);
				return;
			}
			throw new ArgumentException("unknow PredefinedBiometricType : " + predefinedBiometricType);
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x000AA607 File Offset: 0x000A9607
		public TypeOfBiometricData(DerObjectIdentifier biometricDataOid)
		{
			this.obj = biometricDataOid;
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x000AA616 File Offset: 0x000A9616
		public bool IsPredefined
		{
			get
			{
				return this.obj is DerInteger;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x000AA626 File Offset: 0x000A9626
		public int PredefinedBiometricType
		{
			get
			{
				return ((DerInteger)this.obj).Value.IntValue;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x000AA63D File Offset: 0x000A963D
		public DerObjectIdentifier BiometricDataOid
		{
			get
			{
				return (DerObjectIdentifier)this.obj;
			}
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x000AA64A File Offset: 0x000A964A
		public override Asn1Object ToAsn1Object()
		{
			return this.obj.ToAsn1Object();
		}

		// Token: 0x0400138E RID: 5006
		public const int Picture = 0;

		// Token: 0x0400138F RID: 5007
		public const int HandwrittenSignature = 1;

		// Token: 0x04001390 RID: 5008
		internal Asn1Encodable obj;
	}
}
