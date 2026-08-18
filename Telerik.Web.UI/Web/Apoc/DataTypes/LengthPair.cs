using System;
using Telerik.Web.Apoc.Fo;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x02001385 RID: 4997
	internal class LengthPair : ICompoundDatatype
	{
		// Token: 0x0600D068 RID: 53352 RVA: 0x002E301E File Offset: 0x002E121E
		public void SetComponent(string sCmpnName, Property cmpnValue, bool bIsDefault)
		{
			if (sCmpnName.Equals("block-progression-direction"))
			{
				this.bpd = cmpnValue;
				return;
			}
			if (sCmpnName.Equals("inline-progression-direction"))
			{
				this.ipd = cmpnValue;
			}
		}

		// Token: 0x0600D069 RID: 53353 RVA: 0x002E3049 File Offset: 0x002E1249
		public Property GetComponent(string sCmpnName)
		{
			if (sCmpnName.Equals("block-progression-direction"))
			{
				return this.GetBPD();
			}
			if (sCmpnName.Equals("inline-progression-direction"))
			{
				return this.GetIPD();
			}
			return null;
		}

		// Token: 0x0600D06A RID: 53354 RVA: 0x002E3074 File Offset: 0x002E1274
		public Property GetIPD()
		{
			return this.ipd;
		}

		// Token: 0x0600D06B RID: 53355 RVA: 0x002E307C File Offset: 0x002E127C
		public Property GetBPD()
		{
			return this.bpd;
		}

		// Token: 0x040037EB RID: 14315
		private Property ipd;

		// Token: 0x040037EC RID: 14316
		private Property bpd;
	}
}
