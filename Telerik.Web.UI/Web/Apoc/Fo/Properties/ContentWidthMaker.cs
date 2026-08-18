using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014B7 RID: 5303
	internal class ContentWidthMaker : LengthProperty.Maker
	{
		// Token: 0x0600D559 RID: 54617 RVA: 0x002F36A2 File Offset: 0x002F18A2
		public new static PropertyMaker Maker(string propName)
		{
			return new ContentWidthMaker(propName);
		}

		// Token: 0x0600D55A RID: 54618 RVA: 0x002F36AA File Offset: 0x002F18AA
		protected ContentWidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D55B RID: 54619 RVA: 0x002F36B3 File Offset: 0x002F18B3
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D55C RID: 54620 RVA: 0x002F36B6 File Offset: 0x002F18B6
		protected override bool IsAutoLengthAllowed()
		{
			return true;
		}

		// Token: 0x0600D55D RID: 54621 RVA: 0x002F36B9 File Offset: 0x002F18B9
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A55 RID: 14933
		private Property m_defaultProp;
	}
}
