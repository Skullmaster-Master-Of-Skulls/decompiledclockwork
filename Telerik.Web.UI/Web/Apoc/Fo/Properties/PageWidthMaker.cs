using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001553 RID: 5459
	internal class PageWidthMaker : LengthProperty.Maker
	{
		// Token: 0x0600D79E RID: 55198 RVA: 0x002F8069 File Offset: 0x002F6269
		public new static PropertyMaker Maker(string propName)
		{
			return new PageWidthMaker(propName);
		}

		// Token: 0x0600D79F RID: 55199 RVA: 0x002F8071 File Offset: 0x002F6271
		protected PageWidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7A0 RID: 55200 RVA: 0x002F807A File Offset: 0x002F627A
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7A1 RID: 55201 RVA: 0x002F807D File Offset: 0x002F627D
		protected override bool IsAutoLengthAllowed()
		{
			return true;
		}

		// Token: 0x0600D7A2 RID: 55202 RVA: 0x002F8080 File Offset: 0x002F6280
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "8in", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B27 RID: 15143
		private Property m_defaultProp;
	}
}
