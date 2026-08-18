using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200155F RID: 5471
	internal class ProvisionalLabelSeparationMaker : LengthProperty.Maker
	{
		// Token: 0x0600D7CD RID: 55245 RVA: 0x002F83AE File Offset: 0x002F65AE
		public new static PropertyMaker Maker(string propName)
		{
			return new ProvisionalLabelSeparationMaker(propName);
		}

		// Token: 0x0600D7CE RID: 55246 RVA: 0x002F83B6 File Offset: 0x002F65B6
		protected ProvisionalLabelSeparationMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7CF RID: 55247 RVA: 0x002F83BF File Offset: 0x002F65BF
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D7D0 RID: 55248 RVA: 0x002F83C2 File Offset: 0x002F65C2
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "6pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B3D RID: 15165
		private Property m_defaultProp;
	}
}
