using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014BA RID: 5306
	internal class CueBeforeMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D566 RID: 54630 RVA: 0x002F3759 File Offset: 0x002F1959
		public new static PropertyMaker Maker(string propName)
		{
			return new CueBeforeMaker(propName);
		}

		// Token: 0x0600D567 RID: 54631 RVA: 0x002F3761 File Offset: 0x002F1961
		protected CueBeforeMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D568 RID: 54632 RVA: 0x002F376A File Offset: 0x002F196A
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D569 RID: 54633 RVA: 0x002F376D File Offset: 0x002F196D
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A58 RID: 14936
		private Property m_defaultProp;
	}
}
