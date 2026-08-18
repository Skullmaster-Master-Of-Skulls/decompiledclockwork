using System;
using Telerik.Web.Apoc.Fo;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x0200137D RID: 4989
	internal class CondLength : ICompoundDatatype
	{
		// Token: 0x0600D023 RID: 53283 RVA: 0x002E2758 File Offset: 0x002E0958
		public void SetComponent(string sCmpnName, Property cmpnValue, bool bIsDefault)
		{
			if (sCmpnName.Equals("length"))
			{
				this.length = cmpnValue;
				return;
			}
			if (sCmpnName.Equals("conditionality"))
			{
				this.conditionality = cmpnValue;
			}
		}

		// Token: 0x0600D024 RID: 53284 RVA: 0x002E2783 File Offset: 0x002E0983
		public Property GetComponent(string sCmpnName)
		{
			if (sCmpnName.Equals("length"))
			{
				return this.length;
			}
			if (sCmpnName.Equals("conditionality"))
			{
				return this.conditionality;
			}
			return null;
		}

		// Token: 0x0600D025 RID: 53285 RVA: 0x002E27AE File Offset: 0x002E09AE
		public Property GetConditionality()
		{
			return this.conditionality;
		}

		// Token: 0x0600D026 RID: 53286 RVA: 0x002E27B6 File Offset: 0x002E09B6
		public Property GetLength()
		{
			return this.length;
		}

		// Token: 0x0600D027 RID: 53287 RVA: 0x002E27BE File Offset: 0x002E09BE
		public bool IsDiscard()
		{
			return this.conditionality.GetEnum() == 17;
		}

		// Token: 0x0600D028 RID: 53288 RVA: 0x002E27CF File Offset: 0x002E09CF
		public int MValue()
		{
			return this.length.GetLength().MValue();
		}

		// Token: 0x040037CF RID: 14287
		private Property length;

		// Token: 0x040037D0 RID: 14288
		private Property conditionality;
	}
}
