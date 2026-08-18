using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014B9 RID: 5305
	internal class CueAfterMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D562 RID: 54626 RVA: 0x002F371D File Offset: 0x002F191D
		public new static PropertyMaker Maker(string propName)
		{
			return new CueAfterMaker(propName);
		}

		// Token: 0x0600D563 RID: 54627 RVA: 0x002F3725 File Offset: 0x002F1925
		protected CueAfterMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D564 RID: 54628 RVA: 0x002F372E File Offset: 0x002F192E
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D565 RID: 54629 RVA: 0x002F3731 File Offset: 0x002F1931
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A57 RID: 14935
		private Property m_defaultProp;
	}
}
