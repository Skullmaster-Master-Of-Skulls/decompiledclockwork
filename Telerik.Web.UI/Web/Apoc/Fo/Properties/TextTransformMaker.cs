using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015A7 RID: 5543
	internal class TextTransformMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D8BD RID: 55485 RVA: 0x002F9735 File Offset: 0x002F7935
		public new static PropertyMaker Maker(string propName)
		{
			return new TextTransformMaker(propName);
		}

		// Token: 0x0600D8BE RID: 55486 RVA: 0x002F973D File Offset: 0x002F793D
		protected TextTransformMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8BF RID: 55487 RVA: 0x002F9746 File Offset: 0x002F7946
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D8C0 RID: 55488 RVA: 0x002F9749 File Offset: 0x002F7949
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BBB RID: 15291
		private Property m_defaultProp;
	}
}
