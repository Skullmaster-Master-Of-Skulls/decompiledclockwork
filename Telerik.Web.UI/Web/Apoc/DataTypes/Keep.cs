using System;
using Telerik.Web.Apoc.Fo;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x02001381 RID: 4993
	internal class Keep : ICompoundDatatype
	{
		// Token: 0x0600D052 RID: 53330 RVA: 0x002E2E28 File Offset: 0x002E1028
		public void SetComponent(string sCmpnName, Property cmpnValue, bool bIsDefault)
		{
			if (sCmpnName.Equals("within-line"))
			{
				this.setWithinLine(cmpnValue, bIsDefault);
				return;
			}
			if (sCmpnName.Equals("within-column"))
			{
				this.setWithinColumn(cmpnValue, bIsDefault);
				return;
			}
			if (sCmpnName.Equals("within-page"))
			{
				this.setWithinPage(cmpnValue, bIsDefault);
			}
		}

		// Token: 0x0600D053 RID: 53331 RVA: 0x002E2E76 File Offset: 0x002E1076
		public Property GetComponent(string sCmpnName)
		{
			if (sCmpnName.Equals("within-line"))
			{
				return this.getWithinLine();
			}
			if (sCmpnName.Equals("within-column"))
			{
				return this.getWithinColumn();
			}
			if (sCmpnName.Equals("within-page"))
			{
				return this.getWithinPage();
			}
			return null;
		}

		// Token: 0x0600D054 RID: 53332 RVA: 0x002E2EB5 File Offset: 0x002E10B5
		public void setWithinLine(Property withinLine, bool bIsDefault)
		{
			this.withinLine = withinLine;
		}

		// Token: 0x0600D055 RID: 53333 RVA: 0x002E2EBE File Offset: 0x002E10BE
		protected void setWithinColumn(Property withinColumn, bool bIsDefault)
		{
			this.withinColumn = withinColumn;
		}

		// Token: 0x0600D056 RID: 53334 RVA: 0x002E2EC7 File Offset: 0x002E10C7
		public void setWithinPage(Property withinPage, bool bIsDefault)
		{
			this.withinPage = withinPage;
		}

		// Token: 0x0600D057 RID: 53335 RVA: 0x002E2ED0 File Offset: 0x002E10D0
		public Property getWithinLine()
		{
			return this.withinLine;
		}

		// Token: 0x0600D058 RID: 53336 RVA: 0x002E2ED8 File Offset: 0x002E10D8
		public Property getWithinColumn()
		{
			return this.withinColumn;
		}

		// Token: 0x0600D059 RID: 53337 RVA: 0x002E2EE0 File Offset: 0x002E10E0
		public Property getWithinPage()
		{
			return this.withinPage;
		}

		// Token: 0x0600D05A RID: 53338 RVA: 0x002E2EE8 File Offset: 0x002E10E8
		public override string ToString()
		{
			return "Keep";
		}

		// Token: 0x040037DB RID: 14299
		private Property withinLine;

		// Token: 0x040037DC RID: 14300
		private Property withinColumn;

		// Token: 0x040037DD RID: 14301
		private Property withinPage;
	}
}
