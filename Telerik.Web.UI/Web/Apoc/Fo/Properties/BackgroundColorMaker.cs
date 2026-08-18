using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001451 RID: 5201
	internal class BackgroundColorMaker : GenericColor
	{
		// Token: 0x0600D3D7 RID: 54231 RVA: 0x002F07F6 File Offset: 0x002EE9F6
		public new static PropertyMaker Maker(string propName)
		{
			return new BackgroundColorMaker(propName);
		}

		// Token: 0x0600D3D8 RID: 54232 RVA: 0x002F07FE File Offset: 0x002EE9FE
		protected BackgroundColorMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3D9 RID: 54233 RVA: 0x002F0807 File Offset: 0x002EEA07
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D3DA RID: 54234 RVA: 0x002F080A File Offset: 0x002EEA0A
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "transparent", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0600D3DB RID: 54235 RVA: 0x002F0834 File Offset: 0x002EEA34
		protected override Property ConvertPropertyDatatype(Property p, PropertyList propertyList, FObj fo)
		{
			string ncname = p.GetNCname();
			if (ncname != null)
			{
				return new ColorTypeProperty(new ColorType(ncname));
			}
			return base.ConvertPropertyDatatype(p, propertyList, fo);
		}

		// Token: 0x04003986 RID: 14726
		private Property m_defaultProp;
	}
}
