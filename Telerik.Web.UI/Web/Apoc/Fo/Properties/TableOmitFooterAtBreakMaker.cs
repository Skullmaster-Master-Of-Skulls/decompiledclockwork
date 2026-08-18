using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001597 RID: 5527
	internal class TableOmitFooterAtBreakMaker : GenericBoolean
	{
		// Token: 0x0600D882 RID: 55426 RVA: 0x002F915D File Offset: 0x002F735D
		public new static PropertyMaker Maker(string propName)
		{
			return new TableOmitFooterAtBreakMaker(propName);
		}

		// Token: 0x0600D883 RID: 55427 RVA: 0x002F9165 File Offset: 0x002F7365
		protected TableOmitFooterAtBreakMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D884 RID: 55428 RVA: 0x002F916E File Offset: 0x002F736E
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D885 RID: 55429 RVA: 0x002F9171 File Offset: 0x002F7371
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "false", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B8D RID: 15245
		private Property m_defaultProp;
	}
}
