using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014B3 RID: 5299
	internal class ColumnWidthMaker : LengthProperty.Maker
	{
		// Token: 0x0600D54B RID: 54603 RVA: 0x002F35F7 File Offset: 0x002F17F7
		public new static PropertyMaker Maker(string propName)
		{
			return new ColumnWidthMaker(propName);
		}

		// Token: 0x0600D54C RID: 54604 RVA: 0x002F35FF File Offset: 0x002F17FF
		protected ColumnWidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D54D RID: 54605 RVA: 0x002F3608 File Offset: 0x002F1808
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D54E RID: 54606 RVA: 0x002F360B File Offset: 0x002F180B
		public override Property Make(PropertyList propertyList)
		{
			return this.Make(propertyList, "proportional-column-width(1)", propertyList.getParentFObj());
		}
	}
}
