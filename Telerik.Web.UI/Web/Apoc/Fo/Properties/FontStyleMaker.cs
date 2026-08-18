using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014D0 RID: 5328
	internal class FontStyleMaker : StringProperty.Maker
	{
		// Token: 0x0600D5C0 RID: 54720 RVA: 0x002F3F11 File Offset: 0x002F2111
		public new static PropertyMaker Maker(string propName)
		{
			return new FontStyleMaker(propName);
		}

		// Token: 0x0600D5C1 RID: 54721 RVA: 0x002F3F19 File Offset: 0x002F2119
		protected FontStyleMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D5C2 RID: 54722 RVA: 0x002F3F22 File Offset: 0x002F2122
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D5C3 RID: 54723 RVA: 0x002F3F25 File Offset: 0x002F2125
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "normal", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A75 RID: 14965
		private Property m_defaultProp;
	}
}
