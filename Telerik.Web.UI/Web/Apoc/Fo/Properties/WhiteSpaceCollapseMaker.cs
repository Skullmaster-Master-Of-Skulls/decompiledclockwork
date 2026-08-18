using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015B1 RID: 5553
	internal class WhiteSpaceCollapseMaker : GenericBoolean
	{
		// Token: 0x0600D8E2 RID: 55522 RVA: 0x002F9A40 File Offset: 0x002F7C40
		public new static PropertyMaker Maker(string propName)
		{
			return new WhiteSpaceCollapseMaker(propName);
		}

		// Token: 0x0600D8E3 RID: 55523 RVA: 0x002F9A48 File Offset: 0x002F7C48
		protected WhiteSpaceCollapseMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8E4 RID: 55524 RVA: 0x002F9A51 File Offset: 0x002F7C51
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D8E5 RID: 55525 RVA: 0x002F9A54 File Offset: 0x002F7C54
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "true", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BD3 RID: 15315
		private Property m_defaultProp;
	}
}
