using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001513 RID: 5395
	internal class LanguageMaker : StringProperty.Maker
	{
		// Token: 0x0600D6A9 RID: 54953 RVA: 0x002F6C3F File Offset: 0x002F4E3F
		public new static PropertyMaker Maker(string propName)
		{
			return new LanguageMaker(propName);
		}

		// Token: 0x0600D6AA RID: 54954 RVA: 0x002F6C47 File Offset: 0x002F4E47
		protected LanguageMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D6AB RID: 54955 RVA: 0x002F6C50 File Offset: 0x002F4E50
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D6AC RID: 54956 RVA: 0x002F6C53 File Offset: 0x002F4E53
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AD2 RID: 15058
		private Property m_defaultProp;
	}
}
