using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015A4 RID: 5540
	internal class TextDepthMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D8B1 RID: 55473 RVA: 0x002F9681 File Offset: 0x002F7881
		public new static PropertyMaker Maker(string propName)
		{
			return new TextDepthMaker(propName);
		}

		// Token: 0x0600D8B2 RID: 55474 RVA: 0x002F9689 File Offset: 0x002F7889
		protected TextDepthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8B3 RID: 55475 RVA: 0x002F9692 File Offset: 0x002F7892
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D8B4 RID: 55476 RVA: 0x002F9695 File Offset: 0x002F7895
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "use-font-metrics", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BB8 RID: 15288
		private Property m_defaultProp;
	}
}
