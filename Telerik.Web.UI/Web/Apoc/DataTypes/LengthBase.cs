using System;
using Telerik.Web.Apoc.Fo;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x02001384 RID: 4996
	internal class LengthBase : IPercentBase
	{
		// Token: 0x0600D062 RID: 53346 RVA: 0x002E2F28 File Offset: 0x002E1128
		public LengthBase(FObj parentFO, PropertyList plist, int iBaseType)
		{
			this.parentFO = parentFO;
			this.propertyList = plist;
			this.iBaseType = iBaseType;
		}

		// Token: 0x0600D063 RID: 53347 RVA: 0x002E2F45 File Offset: 0x002E1145
		protected FObj GetParentFO()
		{
			return this.parentFO;
		}

		// Token: 0x0600D064 RID: 53348 RVA: 0x002E2F4D File Offset: 0x002E114D
		protected PropertyList getPropertyList()
		{
			return this.propertyList;
		}

		// Token: 0x0600D065 RID: 53349 RVA: 0x002E2F55 File Offset: 0x002E1155
		public int GetDimension()
		{
			return 1;
		}

		// Token: 0x0600D066 RID: 53350 RVA: 0x002E2F58 File Offset: 0x002E1158
		public double GetBaseValue()
		{
			return 1.0;
		}

		// Token: 0x0600D067 RID: 53351 RVA: 0x002E2F64 File Offset: 0x002E1164
		public int GetBaseLength()
		{
			switch (this.iBaseType)
			{
			case 0:
				ApocDriver.ActiveDriver.FireApocError("LengthBase.getBaseLength() called on CUSTOM_BASE type");
				return 0;
			case 1:
				return this.propertyList.GetProperty("font-size").GetLength().MValue();
			case 2:
				return this.propertyList.GetInheritedProperty("font-size").GetLength().MValue();
			case 3:
				return this.parentFO.GetContentWidth();
			case 4:
			{
				FObj parent = this.parentFO;
				while (parent != null && !parent.GeneratesReferenceAreas())
				{
					parent = parent.getParent();
				}
				if (parent == null)
				{
					return 0;
				}
				return parent.GetContentWidth();
			}
			default:
				ApocDriver.ActiveDriver.FireApocError("Unknown base type for LengthBase");
				return 0;
			}
		}

		// Token: 0x040037E3 RID: 14307
		public const int CUSTOM_BASE = 0;

		// Token: 0x040037E4 RID: 14308
		public const int FONTSIZE = 1;

		// Token: 0x040037E5 RID: 14309
		public const int INH_FONTSIZE = 2;

		// Token: 0x040037E6 RID: 14310
		public const int CONTAINING_BOX = 3;

		// Token: 0x040037E7 RID: 14311
		public const int CONTAINING_REFAREA = 4;

		// Token: 0x040037E8 RID: 14312
		protected FObj parentFO;

		// Token: 0x040037E9 RID: 14313
		private PropertyList propertyList;

		// Token: 0x040037EA RID: 14314
		private int iBaseType;
	}
}
