using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200158F RID: 5519
	internal class StartingStateMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D866 RID: 55398 RVA: 0x002F8F9D File Offset: 0x002F719D
		public new static PropertyMaker Maker(string propName)
		{
			return new StartingStateMaker(propName);
		}

		// Token: 0x0600D867 RID: 55399 RVA: 0x002F8FA5 File Offset: 0x002F71A5
		protected StartingStateMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D868 RID: 55400 RVA: 0x002F8FAE File Offset: 0x002F71AE
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D869 RID: 55401 RVA: 0x002F8FB1 File Offset: 0x002F71B1
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "show", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B83 RID: 15235
		private Property m_defaultProp;
	}
}
