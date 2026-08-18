using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x0200141E RID: 5150
	internal class LengthPairProperty : Property
	{
		// Token: 0x0600D2E5 RID: 53989 RVA: 0x002ED2FE File Offset: 0x002EB4FE
		public LengthPairProperty(LengthPair lengthPair)
		{
			this.lengthPair = lengthPair;
		}

		// Token: 0x0600D2E6 RID: 53990 RVA: 0x002ED30D File Offset: 0x002EB50D
		public override LengthPair GetLengthPair()
		{
			return this.lengthPair;
		}

		// Token: 0x0600D2E7 RID: 53991 RVA: 0x002ED315 File Offset: 0x002EB515
		public override object GetObject()
		{
			return this.lengthPair;
		}

		// Token: 0x04003923 RID: 14627
		private LengthPair lengthPair;

		// Token: 0x02001421 RID: 5153
		internal class Maker : LengthProperty.Maker
		{
			// Token: 0x0600D2EF RID: 53999 RVA: 0x002ED3B3 File Offset: 0x002EB5B3
			protected Maker(string name) : base(name)
			{
			}
		}
	}
}
