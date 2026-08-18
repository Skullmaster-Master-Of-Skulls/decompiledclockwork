using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001527 RID: 5415
	internal class MarginLeftMaker : LengthProperty.Maker
	{
		// Token: 0x0600D705 RID: 55045 RVA: 0x002F740F File Offset: 0x002F560F
		public new static PropertyMaker Maker(string propName)
		{
			return new MarginLeftMaker(propName);
		}

		// Token: 0x0600D706 RID: 55046 RVA: 0x002F7417 File Offset: 0x002F5617
		protected MarginLeftMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D707 RID: 55047 RVA: 0x002F7420 File Offset: 0x002F5620
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D708 RID: 55048 RVA: 0x002F7423 File Offset: 0x002F5623
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AF7 RID: 15095
		private Property m_defaultProp;
	}
}
