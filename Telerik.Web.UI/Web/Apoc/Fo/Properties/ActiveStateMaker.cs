using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200144A RID: 5194
	internal class ActiveStateMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D3BB RID: 54203 RVA: 0x002EFAB8 File Offset: 0x002EDCB8
		public new static PropertyMaker Maker(string propName)
		{
			return new ActiveStateMaker(propName);
		}

		// Token: 0x0600D3BC RID: 54204 RVA: 0x002EFAC0 File Offset: 0x002EDCC0
		protected ActiveStateMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3BD RID: 54205 RVA: 0x002EFAC9 File Offset: 0x002EDCC9
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D3BE RID: 54206 RVA: 0x002EFACC File Offset: 0x002EDCCC
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0400397F RID: 14719
		private Property m_defaultProp;
	}
}
