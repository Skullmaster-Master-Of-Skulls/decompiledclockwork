using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200151F RID: 5407
	internal class LetterSpacingMaker : LengthProperty.Maker
	{
		// Token: 0x0600D6E1 RID: 55009 RVA: 0x002F7163 File Offset: 0x002F5363
		public new static PropertyMaker Maker(string propName)
		{
			return new LetterSpacingMaker(propName);
		}

		// Token: 0x0600D6E2 RID: 55010 RVA: 0x002F716B File Offset: 0x002F536B
		protected LetterSpacingMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D6E3 RID: 55011 RVA: 0x002F7174 File Offset: 0x002F5374
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D6E4 RID: 55012 RVA: 0x002F7177 File Offset: 0x002F5377
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AEA RID: 15082
		private Property m_defaultProp;
	}
}
