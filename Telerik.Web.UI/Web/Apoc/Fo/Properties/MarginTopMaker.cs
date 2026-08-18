using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200152A RID: 5418
	internal class MarginTopMaker : LengthProperty.Maker
	{
		// Token: 0x0600D711 RID: 55057 RVA: 0x002F74C3 File Offset: 0x002F56C3
		public new static PropertyMaker Maker(string propName)
		{
			return new MarginTopMaker(propName);
		}

		// Token: 0x0600D712 RID: 55058 RVA: 0x002F74CB File Offset: 0x002F56CB
		protected MarginTopMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D713 RID: 55059 RVA: 0x002F74D4 File Offset: 0x002F56D4
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D714 RID: 55060 RVA: 0x002F74D7 File Offset: 0x002F56D7
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AFA RID: 15098
		private Property m_defaultProp;
	}
}
