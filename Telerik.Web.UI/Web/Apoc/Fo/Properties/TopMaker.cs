using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015A8 RID: 5544
	internal class TopMaker : LengthProperty.Maker
	{
		// Token: 0x0600D8C1 RID: 55489 RVA: 0x002F9771 File Offset: 0x002F7971
		public new static PropertyMaker Maker(string propName)
		{
			return new TopMaker(propName);
		}

		// Token: 0x0600D8C2 RID: 55490 RVA: 0x002F9779 File Offset: 0x002F7979
		protected TopMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8C3 RID: 55491 RVA: 0x002F9782 File Offset: 0x002F7982
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D8C4 RID: 55492 RVA: 0x002F9785 File Offset: 0x002F7985
		protected override bool IsAutoLengthAllowed()
		{
			return true;
		}

		// Token: 0x0600D8C5 RID: 55493 RVA: 0x002F9788 File Offset: 0x002F7988
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BBC RID: 15292
		private Property m_defaultProp;
	}
}
