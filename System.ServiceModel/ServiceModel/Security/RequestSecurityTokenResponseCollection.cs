using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000332 RID: 818
	internal sealed class RequestSecurityTokenResponseCollection : BodyWriter
	{
		// Token: 0x06001DB7 RID: 7607 RVA: 0x0006E467 File Offset: 0x0006C667
		public RequestSecurityTokenResponseCollection(IEnumerable<RequestSecurityTokenResponse> rstrCollection) : this(rstrCollection, SecurityStandardsManager.DefaultInstance)
		{
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0006E478 File Offset: 0x0006C678
		public RequestSecurityTokenResponseCollection(IEnumerable<RequestSecurityTokenResponse> rstrCollection, SecurityStandardsManager standardsManager) : base(true)
		{
			if (rstrCollection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstrCollection");
			}
			int num = 0;
			using (IEnumerator<RequestSecurityTokenResponse> enumerator = rstrCollection.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(string.Format(CultureInfo.InvariantCulture, "rstrCollection[{0}]", new object[]
						{
							num
						}));
					}
					num++;
				}
			}
			this.rstrCollection = rstrCollection;
			if (standardsManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("standardsManager"));
			}
			this.standardsManager = standardsManager;
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x0006E52C File Offset: 0x0006C72C
		public IEnumerable<RequestSecurityTokenResponse> RstrCollection
		{
			get
			{
				return this.rstrCollection;
			}
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0006E534 File Offset: 0x0006C734
		public void WriteTo(XmlWriter writer)
		{
			this.standardsManager.TrustDriver.WriteRequestSecurityTokenResponseCollection(this, writer);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x0006E548 File Offset: 0x0006C748
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			this.WriteTo(writer);
		}

		// Token: 0x04001E34 RID: 7732
		private IEnumerable<RequestSecurityTokenResponse> rstrCollection;

		// Token: 0x04001E35 RID: 7733
		private SecurityStandardsManager standardsManager;
	}
}
