using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001454 RID: 5204
	internal class BackgroundImageMaker : StringProperty.Maker
	{
		// Token: 0x0600D3E1 RID: 54241 RVA: 0x002F08E4 File Offset: 0x002EEAE4
		public new static PropertyMaker Maker(string propName)
		{
			return new BackgroundImageMaker(propName);
		}

		// Token: 0x0600D3E2 RID: 54242 RVA: 0x002F08EC File Offset: 0x002EEAEC
		protected BackgroundImageMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3E3 RID: 54243 RVA: 0x002F08F5 File Offset: 0x002EEAF5
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D3E4 RID: 54244 RVA: 0x002F08F8 File Offset: 0x002EEAF8
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003988 RID: 14728
		private Property m_defaultProp;
	}
}
