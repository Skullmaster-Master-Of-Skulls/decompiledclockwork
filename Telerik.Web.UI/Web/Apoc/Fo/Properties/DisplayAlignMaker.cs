using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014BF RID: 5311
	internal class DisplayAlignMaker : EnumProperty.Maker
	{
		// Token: 0x0600D577 RID: 54647 RVA: 0x002F3851 File Offset: 0x002F1A51
		public new static PropertyMaker Maker(string propName)
		{
			return new DisplayAlignMaker(propName);
		}

		// Token: 0x0600D578 RID: 54648 RVA: 0x002F3859 File Offset: 0x002F1A59
		protected DisplayAlignMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D579 RID: 54649 RVA: 0x002F3862 File Offset: 0x002F1A62
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D57A RID: 54650 RVA: 0x002F3868 File Offset: 0x002F1A68
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("before"))
			{
				return DisplayAlignMaker.s_propBEFORE;
			}
			if (value.Equals("after"))
			{
				return DisplayAlignMaker.s_propAFTER;
			}
			if (value.Equals("center"))
			{
				return DisplayAlignMaker.s_propCENTER;
			}
			if (value.Equals("auto"))
			{
				return DisplayAlignMaker.s_propAUTO;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D57B RID: 54651 RVA: 0x002F38C8 File Offset: 0x002F1AC8
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A60 RID: 14944
		protected static readonly EnumProperty s_propBEFORE = new EnumProperty(9);

		// Token: 0x04003A61 RID: 14945
		protected static readonly EnumProperty s_propAFTER = new EnumProperty(2);

		// Token: 0x04003A62 RID: 14946
		protected static readonly EnumProperty s_propCENTER = new EnumProperty(13);

		// Token: 0x04003A63 RID: 14947
		protected static readonly EnumProperty s_propAUTO = new EnumProperty(7);

		// Token: 0x04003A64 RID: 14948
		private Property m_defaultProp;
	}
}
