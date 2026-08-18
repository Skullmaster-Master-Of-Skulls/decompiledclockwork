using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001456 RID: 5206
	internal class BackgroundPositionHorizontalMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D3E9 RID: 54249 RVA: 0x002F095C File Offset: 0x002EEB5C
		public new static PropertyMaker Maker(string propName)
		{
			return new BackgroundPositionHorizontalMaker(propName);
		}

		// Token: 0x0600D3EA RID: 54250 RVA: 0x002F0964 File Offset: 0x002EEB64
		protected BackgroundPositionHorizontalMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3EB RID: 54251 RVA: 0x002F096D File Offset: 0x002EEB6D
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D3EC RID: 54252 RVA: 0x002F0970 File Offset: 0x002EEB70
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0%", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0400398A RID: 14730
		private Property m_defaultProp;
	}
}
