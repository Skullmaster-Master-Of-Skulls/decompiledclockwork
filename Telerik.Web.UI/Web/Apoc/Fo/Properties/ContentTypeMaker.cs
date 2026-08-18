using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014B6 RID: 5302
	internal class ContentTypeMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D555 RID: 54613 RVA: 0x002F3666 File Offset: 0x002F1866
		public new static PropertyMaker Maker(string propName)
		{
			return new ContentTypeMaker(propName);
		}

		// Token: 0x0600D556 RID: 54614 RVA: 0x002F366E File Offset: 0x002F186E
		protected ContentTypeMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D557 RID: 54615 RVA: 0x002F3677 File Offset: 0x002F1877
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D558 RID: 54616 RVA: 0x002F367A File Offset: 0x002F187A
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A54 RID: 14932
		private Property m_defaultProp;
	}
}
