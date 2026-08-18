using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001599 RID: 5529
	internal class TableOmitHeaderAtBreakMaker : GenericBoolean
	{
		// Token: 0x0600D887 RID: 55431 RVA: 0x002F91A1 File Offset: 0x002F73A1
		public new static PropertyMaker Maker(string propName)
		{
			return new TableOmitHeaderAtBreakMaker(propName);
		}

		// Token: 0x0600D888 RID: 55432 RVA: 0x002F91A9 File Offset: 0x002F73A9
		protected TableOmitHeaderAtBreakMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D889 RID: 55433 RVA: 0x002F91B2 File Offset: 0x002F73B2
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D88A RID: 55434 RVA: 0x002F91B5 File Offset: 0x002F73B5
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "false", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B8E RID: 15246
		private Property m_defaultProp;
	}
}
