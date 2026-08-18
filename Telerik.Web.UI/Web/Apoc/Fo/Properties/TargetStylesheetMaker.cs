using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200159C RID: 5532
	internal class TargetStylesheetMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D893 RID: 55443 RVA: 0x002F9255 File Offset: 0x002F7455
		public new static PropertyMaker Maker(string propName)
		{
			return new TargetStylesheetMaker(propName);
		}

		// Token: 0x0600D894 RID: 55444 RVA: 0x002F925D File Offset: 0x002F745D
		protected TargetStylesheetMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D895 RID: 55445 RVA: 0x002F9266 File Offset: 0x002F7466
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D896 RID: 55446 RVA: 0x002F9269 File Offset: 0x002F7469
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "use-normal-stylesheet", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B91 RID: 15249
		private Property m_defaultProp;
	}
}
