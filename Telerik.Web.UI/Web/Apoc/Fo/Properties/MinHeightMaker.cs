using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001532 RID: 5426
	internal class MinHeightMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D735 RID: 55093 RVA: 0x002F76F7 File Offset: 0x002F58F7
		public new static PropertyMaker Maker(string propName)
		{
			return new MinHeightMaker(propName);
		}

		// Token: 0x0600D736 RID: 55094 RVA: 0x002F76FF File Offset: 0x002F58FF
		protected MinHeightMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D737 RID: 55095 RVA: 0x002F7708 File Offset: 0x002F5908
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D738 RID: 55096 RVA: 0x002F770B File Offset: 0x002F590B
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B04 RID: 15108
		private Property m_defaultProp;
	}
}
