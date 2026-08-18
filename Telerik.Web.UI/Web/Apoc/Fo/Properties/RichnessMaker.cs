using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200156C RID: 5484
	internal class RichnessMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D7FE RID: 55294 RVA: 0x002F876E File Offset: 0x002F696E
		public new static PropertyMaker Maker(string propName)
		{
			return new RichnessMaker(propName);
		}

		// Token: 0x0600D7FF RID: 55295 RVA: 0x002F8776 File Offset: 0x002F6976
		protected RichnessMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D800 RID: 55296 RVA: 0x002F877F File Offset: 0x002F697F
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D801 RID: 55297 RVA: 0x002F8782 File Offset: 0x002F6982
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "50", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B59 RID: 15193
		private Property m_defaultProp;
	}
}
