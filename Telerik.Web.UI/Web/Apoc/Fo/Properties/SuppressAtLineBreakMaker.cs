using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001592 RID: 5522
	internal class SuppressAtLineBreakMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D872 RID: 55410 RVA: 0x002F9051 File Offset: 0x002F7251
		public new static PropertyMaker Maker(string propName)
		{
			return new SuppressAtLineBreakMaker(propName);
		}

		// Token: 0x0600D873 RID: 55411 RVA: 0x002F9059 File Offset: 0x002F7259
		protected SuppressAtLineBreakMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D874 RID: 55412 RVA: 0x002F9062 File Offset: 0x002F7262
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D875 RID: 55413 RVA: 0x002F9065 File Offset: 0x002F7265
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B86 RID: 15238
		private Property m_defaultProp;
	}
}
