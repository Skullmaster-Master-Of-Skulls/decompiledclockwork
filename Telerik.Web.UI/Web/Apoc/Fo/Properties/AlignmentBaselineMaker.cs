using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200144C RID: 5196
	internal class AlignmentBaselineMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D3C3 RID: 54211 RVA: 0x002EFB30 File Offset: 0x002EDD30
		public new static PropertyMaker Maker(string propName)
		{
			return new AlignmentBaselineMaker(propName);
		}

		// Token: 0x0600D3C4 RID: 54212 RVA: 0x002EFB38 File Offset: 0x002EDD38
		protected AlignmentBaselineMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3C5 RID: 54213 RVA: 0x002EFB41 File Offset: 0x002EDD41
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D3C6 RID: 54214 RVA: 0x002EFB44 File Offset: 0x002EDD44
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003981 RID: 14721
		private Property m_defaultProp;
	}
}
