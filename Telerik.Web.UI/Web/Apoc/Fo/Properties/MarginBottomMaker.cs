using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001526 RID: 5414
	internal class MarginBottomMaker : LengthProperty.Maker
	{
		// Token: 0x0600D701 RID: 55041 RVA: 0x002F73D3 File Offset: 0x002F55D3
		public new static PropertyMaker Maker(string propName)
		{
			return new MarginBottomMaker(propName);
		}

		// Token: 0x0600D702 RID: 55042 RVA: 0x002F73DB File Offset: 0x002F55DB
		protected MarginBottomMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D703 RID: 55043 RVA: 0x002F73E4 File Offset: 0x002F55E4
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D704 RID: 55044 RVA: 0x002F73E7 File Offset: 0x002F55E7
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AF6 RID: 15094
		private Property m_defaultProp;
	}
}
