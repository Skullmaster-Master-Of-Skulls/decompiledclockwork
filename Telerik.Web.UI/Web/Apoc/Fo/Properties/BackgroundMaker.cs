using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001455 RID: 5205
	internal class BackgroundMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D3E5 RID: 54245 RVA: 0x002F0920 File Offset: 0x002EEB20
		public new static PropertyMaker Maker(string propName)
		{
			return new BackgroundMaker(propName);
		}

		// Token: 0x0600D3E6 RID: 54246 RVA: 0x002F0928 File Offset: 0x002EEB28
		protected BackgroundMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3E7 RID: 54247 RVA: 0x002F0931 File Offset: 0x002EEB31
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D3E8 RID: 54248 RVA: 0x002F0934 File Offset: 0x002EEB34
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003989 RID: 14729
		private Property m_defaultProp;
	}
}
