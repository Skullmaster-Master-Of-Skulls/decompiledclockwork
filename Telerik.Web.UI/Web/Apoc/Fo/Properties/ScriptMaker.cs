using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001576 RID: 5494
	internal class ScriptMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D825 RID: 55333 RVA: 0x002F8AAA File Offset: 0x002F6CAA
		public new static PropertyMaker Maker(string propName)
		{
			return new ScriptMaker(propName);
		}

		// Token: 0x0600D826 RID: 55334 RVA: 0x002F8AB2 File Offset: 0x002F6CB2
		protected ScriptMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D827 RID: 55335 RVA: 0x002F8ABB File Offset: 0x002F6CBB
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D828 RID: 55336 RVA: 0x002F8ABE File Offset: 0x002F6CBE
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B73 RID: 15219
		private Property m_defaultProp;
	}
}
