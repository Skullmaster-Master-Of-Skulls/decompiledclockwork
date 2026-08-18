using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014D7 RID: 5335
	internal class FormatMaker : StringProperty.Maker
	{
		// Token: 0x0600D5D9 RID: 54745 RVA: 0x002F5980 File Offset: 0x002F3B80
		public new static PropertyMaker Maker(string propName)
		{
			return new FormatMaker(propName);
		}

		// Token: 0x0600D5DA RID: 54746 RVA: 0x002F5988 File Offset: 0x002F3B88
		protected FormatMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D5DB RID: 54747 RVA: 0x002F5991 File Offset: 0x002F3B91
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D5DC RID: 54748 RVA: 0x002F5994 File Offset: 0x002F3B94
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "1", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A8A RID: 14986
		private Property m_defaultProp;
	}
}
