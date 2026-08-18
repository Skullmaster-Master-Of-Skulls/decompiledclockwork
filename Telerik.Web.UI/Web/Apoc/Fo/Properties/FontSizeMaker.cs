using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014CE RID: 5326
	internal class FontSizeMaker : LengthProperty.Maker
	{
		// Token: 0x0600D5B6 RID: 54710 RVA: 0x002F3E44 File Offset: 0x002F2044
		public new static PropertyMaker Maker(string propName)
		{
			return new FontSizeMaker(propName);
		}

		// Token: 0x0600D5B7 RID: 54711 RVA: 0x002F3E4C File Offset: 0x002F204C
		protected FontSizeMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D5B8 RID: 54712 RVA: 0x002F3E55 File Offset: 0x002F2055
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D5B9 RID: 54713 RVA: 0x002F3E58 File Offset: 0x002F2058
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "12pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0600D5BA RID: 54714 RVA: 0x002F3E80 File Offset: 0x002F2080
		public override Property ConvertProperty(Property p, PropertyList propertyList, FObj fo)
		{
			if (p.GetNCname() == "small")
			{
				FixedLength length = new FixedLength(12.0, "pt");
				return base.ConvertProperty(new LengthProperty(length), propertyList, fo);
			}
			return base.ConvertProperty(p, propertyList, fo);
		}

		// Token: 0x0600D5BB RID: 54715 RVA: 0x002F3ECB File Offset: 0x002F20CB
		public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
		{
			return new LengthBase(fo, propertyList, 2);
		}

		// Token: 0x04003A73 RID: 14963
		private Property m_defaultProp;
	}
}
