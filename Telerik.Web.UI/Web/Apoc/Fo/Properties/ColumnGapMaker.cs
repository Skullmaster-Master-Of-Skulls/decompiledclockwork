using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014B1 RID: 5297
	internal class ColumnGapMaker : LengthProperty.Maker
	{
		// Token: 0x0600D542 RID: 54594 RVA: 0x002F357C File Offset: 0x002F177C
		public new static PropertyMaker Maker(string propName)
		{
			return new ColumnGapMaker(propName);
		}

		// Token: 0x0600D543 RID: 54595 RVA: 0x002F3584 File Offset: 0x002F1784
		protected ColumnGapMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D544 RID: 54596 RVA: 0x002F358D File Offset: 0x002F178D
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D545 RID: 54597 RVA: 0x002F3590 File Offset: 0x002F1790
		protected override bool IsAutoLengthAllowed()
		{
			return true;
		}

		// Token: 0x0600D546 RID: 54598 RVA: 0x002F3593 File Offset: 0x002F1793
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0.25in", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039F7 RID: 14839
		private Property m_defaultProp;
	}
}
