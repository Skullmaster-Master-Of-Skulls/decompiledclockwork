using System;

namespace System.Security.Cryptography
{
	// Token: 0x020000F8 RID: 248
	public struct ECCurve
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0001B151 File Offset: 0x00019351
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0001B178 File Offset: 0x00019378
		public Oid Oid
		{
			get
			{
				if (this._oid != null)
				{
					return new Oid(this._oid.Value, this._oid.FriendlyName);
				}
				return null;
			}
			private set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Oid");
				}
				if (string.IsNullOrEmpty(value.Value) && string.IsNullOrEmpty(value.FriendlyName))
				{
					throw new ArgumentException(SR.GetString("Cryptography_InvalidCurveOid"));
				}
				this._oid = value;
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001B1C4 File Offset: 0x000193C4
		private static ECCurve Create(Oid oid)
		{
			return new ECCurve
			{
				CurveType = ECCurve.ECCurveType.Named,
				Oid = oid
			};
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001B1EA File Offset: 0x000193EA
		public static ECCurve CreateFromOid(Oid curveOid)
		{
			return ECCurve.Create(new Oid(curveOid.Value, curveOid.FriendlyName));
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001B202 File Offset: 0x00019402
		public static ECCurve CreateFromFriendlyName(string oidFriendlyName)
		{
			if (oidFriendlyName == null)
			{
				throw new ArgumentNullException("oidFriendlyName");
			}
			return ECCurve.CreateFromValueAndName(null, oidFriendlyName);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001B21C File Offset: 0x0001941C
		public static ECCurve CreateFromValue(string oidValue)
		{
			if (oidValue == null)
			{
				throw new ArgumentNullException("oidValue");
			}
			if (oidValue == "1.2.840.10045.3.1.7")
			{
				return ECCurve.NamedCurves.nistP256;
			}
			if (oidValue == "1.3.132.0.34")
			{
				return ECCurve.NamedCurves.nistP384;
			}
			if (!(oidValue == "1.3.132.0.35"))
			{
				return ECCurve.CreateFromValueAndName(oidValue, null);
			}
			return ECCurve.NamedCurves.nistP521;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001B279 File Offset: 0x00019479
		private static ECCurve CreateFromValueAndName(string oidValue, string oidFriendlyName)
		{
			return ECCurve.Create(new Oid(oidValue, oidFriendlyName));
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001B287 File Offset: 0x00019487
		public bool IsPrime
		{
			get
			{
				return this.CurveType == ECCurve.ECCurveType.PrimeShortWeierstrass || this.CurveType == ECCurve.ECCurveType.PrimeMontgomery || this.CurveType == ECCurve.ECCurveType.PrimeTwistedEdwards;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0001B2A6 File Offset: 0x000194A6
		public bool IsCharacteristic2
		{
			get
			{
				return this.CurveType == ECCurve.ECCurveType.Characteristic2;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0001B2B1 File Offset: 0x000194B1
		public bool IsExplicit
		{
			get
			{
				return this.IsPrime || this.IsCharacteristic2;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0001B2C3 File Offset: 0x000194C3
		public bool IsNamed
		{
			get
			{
				return this.CurveType == ECCurve.ECCurveType.Named;
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001B2D0 File Offset: 0x000194D0
		public void Validate()
		{
			if (this.IsNamed)
			{
				if (this.HasAnyExplicitParameters())
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidECNamedCurve"));
				}
				if (this.Oid == null || (string.IsNullOrEmpty(this.Oid.FriendlyName) && string.IsNullOrEmpty(this.Oid.Value)))
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidCurveOid"));
				}
			}
			else if (this.IsExplicit)
			{
				bool flag = false;
				if (this.A == null || this.B == null || this.B.Length != this.A.Length || this.G.X == null || this.G.X.Length != this.A.Length || this.G.Y == null || this.G.Y.Length != this.A.Length || this.Order == null || this.Order.Length == 0 || this.Cofactor == null || this.Cofactor.Length == 0)
				{
					flag = true;
				}
				if (this.IsPrime)
				{
					if (!flag && (this.Prime == null || this.Prime.Length != this.A.Length))
					{
						flag = true;
					}
					if (flag)
					{
						throw new CryptographicException(SR.GetString("Cryptography_InvalidECPrimeCurve"));
					}
				}
				else if (this.IsCharacteristic2)
				{
					if (!flag && (this.Polynomial == null || this.Polynomial.Length == 0))
					{
						flag = true;
					}
					if (flag)
					{
						throw new CryptographicException(SR.GetString("Cryptography_InvalidECCharacteristic2Curve"));
					}
				}
			}
			else if (this.HasAnyExplicitParameters() || this.Oid != null)
			{
				throw new CryptographicException(SR.GetString("Cryptography_CurveNotSupported", new object[]
				{
					this.CurveType.ToString()
				}));
			}
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001B484 File Offset: 0x00019684
		private bool HasAnyExplicitParameters()
		{
			return this.A != null || this.B != null || this.G.X != null || this.G.Y != null || this.Order != null || this.Cofactor != null || this.Prime != null || this.Polynomial != null || this.Seed != null || this.Hash != null;
		}

		// Token: 0x04000657 RID: 1623
		public byte[] A;

		// Token: 0x04000658 RID: 1624
		public byte[] B;

		// Token: 0x04000659 RID: 1625
		public ECPoint G;

		// Token: 0x0400065A RID: 1626
		public byte[] Order;

		// Token: 0x0400065B RID: 1627
		public byte[] Cofactor;

		// Token: 0x0400065C RID: 1628
		public byte[] Seed;

		// Token: 0x0400065D RID: 1629
		public ECCurve.ECCurveType CurveType;

		// Token: 0x0400065E RID: 1630
		public HashAlgorithmName? Hash;

		// Token: 0x0400065F RID: 1631
		public byte[] Polynomial;

		// Token: 0x04000660 RID: 1632
		public byte[] Prime;

		// Token: 0x04000661 RID: 1633
		private Oid _oid;

		// Token: 0x04000662 RID: 1634
		private const string ECDSA_P256_OID_VALUE = "1.2.840.10045.3.1.7";

		// Token: 0x04000663 RID: 1635
		private const string ECDSA_P384_OID_VALUE = "1.3.132.0.34";

		// Token: 0x04000664 RID: 1636
		private const string ECDSA_P521_OID_VALUE = "1.3.132.0.35";

		// Token: 0x02000344 RID: 836
		public enum ECCurveType
		{
			// Token: 0x04000EF5 RID: 3829
			Implicit,
			// Token: 0x04000EF6 RID: 3830
			PrimeShortWeierstrass,
			// Token: 0x04000EF7 RID: 3831
			PrimeTwistedEdwards,
			// Token: 0x04000EF8 RID: 3832
			PrimeMontgomery,
			// Token: 0x04000EF9 RID: 3833
			Characteristic2,
			// Token: 0x04000EFA RID: 3834
			Named
		}

		// Token: 0x02000345 RID: 837
		public static class NamedCurves
		{
			// Token: 0x17000503 RID: 1283
			// (get) Token: 0x06001B31 RID: 6961 RVA: 0x000631AC File Offset: 0x000613AC
			public static ECCurve brainpoolP160r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP160r1");
				}
			}

			// Token: 0x17000504 RID: 1284
			// (get) Token: 0x06001B32 RID: 6962 RVA: 0x000631B8 File Offset: 0x000613B8
			public static ECCurve brainpoolP160t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP160t1");
				}
			}

			// Token: 0x17000505 RID: 1285
			// (get) Token: 0x06001B33 RID: 6963 RVA: 0x000631C4 File Offset: 0x000613C4
			public static ECCurve brainpoolP192r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP192r1");
				}
			}

			// Token: 0x17000506 RID: 1286
			// (get) Token: 0x06001B34 RID: 6964 RVA: 0x000631D0 File Offset: 0x000613D0
			public static ECCurve brainpoolP192t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP192t1");
				}
			}

			// Token: 0x17000507 RID: 1287
			// (get) Token: 0x06001B35 RID: 6965 RVA: 0x000631DC File Offset: 0x000613DC
			public static ECCurve brainpoolP224r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP224r1");
				}
			}

			// Token: 0x17000508 RID: 1288
			// (get) Token: 0x06001B36 RID: 6966 RVA: 0x000631E8 File Offset: 0x000613E8
			public static ECCurve brainpoolP224t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP224t1");
				}
			}

			// Token: 0x17000509 RID: 1289
			// (get) Token: 0x06001B37 RID: 6967 RVA: 0x000631F4 File Offset: 0x000613F4
			public static ECCurve brainpoolP256r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP256r1");
				}
			}

			// Token: 0x1700050A RID: 1290
			// (get) Token: 0x06001B38 RID: 6968 RVA: 0x00063200 File Offset: 0x00061400
			public static ECCurve brainpoolP256t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP256t1");
				}
			}

			// Token: 0x1700050B RID: 1291
			// (get) Token: 0x06001B39 RID: 6969 RVA: 0x0006320C File Offset: 0x0006140C
			public static ECCurve brainpoolP320r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP320r1");
				}
			}

			// Token: 0x1700050C RID: 1292
			// (get) Token: 0x06001B3A RID: 6970 RVA: 0x00063218 File Offset: 0x00061418
			public static ECCurve brainpoolP320t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP320t1");
				}
			}

			// Token: 0x1700050D RID: 1293
			// (get) Token: 0x06001B3B RID: 6971 RVA: 0x00063224 File Offset: 0x00061424
			public static ECCurve brainpoolP384r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP384r1");
				}
			}

			// Token: 0x1700050E RID: 1294
			// (get) Token: 0x06001B3C RID: 6972 RVA: 0x00063230 File Offset: 0x00061430
			public static ECCurve brainpoolP384t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP384t1");
				}
			}

			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0006323C File Offset: 0x0006143C
			public static ECCurve brainpoolP512r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP512r1");
				}
			}

			// Token: 0x17000510 RID: 1296
			// (get) Token: 0x06001B3E RID: 6974 RVA: 0x00063248 File Offset: 0x00061448
			public static ECCurve brainpoolP512t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP512t1");
				}
			}

			// Token: 0x17000511 RID: 1297
			// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00063254 File Offset: 0x00061454
			public static ECCurve nistP256
			{
				get
				{
					return ECCurve.CreateFromValueAndName("1.2.840.10045.3.1.7", "nistP256");
				}
			}

			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x06001B40 RID: 6976 RVA: 0x00063265 File Offset: 0x00061465
			public static ECCurve nistP384
			{
				get
				{
					return ECCurve.CreateFromValueAndName("1.3.132.0.34", "nistP384");
				}
			}

			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x06001B41 RID: 6977 RVA: 0x00063276 File Offset: 0x00061476
			public static ECCurve nistP521
			{
				get
				{
					return ECCurve.CreateFromValueAndName("1.3.132.0.35", "nistP521");
				}
			}
		}
	}
}
