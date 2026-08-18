using System;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200035F RID: 863
	public class X962Parameters : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06001EE4 RID: 7908 RVA: 0x000BA147 File Offset: 0x000B9147
		public X962Parameters(X9ECParameters ecParameters)
		{
			this._params = ecParameters.ToAsn1Object();
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000BA15B File Offset: 0x000B915B
		public X962Parameters(DerObjectIdentifier namedCurve)
		{
			this._params = namedCurve;
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x000BA16A File Offset: 0x000B916A
		public X962Parameters(Asn1Object obj)
		{
			this._params = obj;
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x000BA179 File Offset: 0x000B9179
		public bool IsNamedCurve
		{
			get
			{
				return this._params is DerObjectIdentifier;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x000BA189 File Offset: 0x000B9189
		public Asn1Object Parameters
		{
			get
			{
				return this._params;
			}
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x000BA191 File Offset: 0x000B9191
		public override Asn1Object ToAsn1Object()
		{
			return this._params;
		}

		// Token: 0x0400155F RID: 5471
		private readonly Asn1Object _params;
	}
}
