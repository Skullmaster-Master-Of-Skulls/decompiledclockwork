using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001593 RID: 5523
	internal class SwitchToMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D876 RID: 55414 RVA: 0x002F908D File Offset: 0x002F728D
		public new static PropertyMaker Maker(string propName)
		{
			return new SwitchToMaker(propName);
		}

		// Token: 0x0600D877 RID: 55415 RVA: 0x002F9095 File Offset: 0x002F7295
		protected SwitchToMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D878 RID: 55416 RVA: 0x002F909E File Offset: 0x002F729E
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D879 RID: 55417 RVA: 0x002F90A1 File Offset: 0x002F72A1
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "xsl-any", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B87 RID: 15239
		private Property m_defaultProp;
	}
}
