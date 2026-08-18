using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200043C RID: 1084
	public class CmsAuthenticatedGenerator : CmsEnvelopedGenerator
	{
		// Token: 0x060024D8 RID: 9432 RVA: 0x000DFFC8 File Offset: 0x000DEFC8
		public CmsAuthenticatedGenerator()
		{
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000DFFD0 File Offset: 0x000DEFD0
		public CmsAuthenticatedGenerator(SecureRandom rand) : base(rand)
		{
		}
	}
}
