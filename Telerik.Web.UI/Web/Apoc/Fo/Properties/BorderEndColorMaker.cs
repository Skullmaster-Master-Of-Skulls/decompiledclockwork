using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001481 RID: 5249
	internal class BorderEndColorMaker : GenericColor
	{
		// Token: 0x0600D493 RID: 54419 RVA: 0x002F200A File Offset: 0x002F020A
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderEndColorMaker(propName);
		}

		// Token: 0x0600D494 RID: 54420 RVA: 0x002F2012 File Offset: 0x002F0212
		protected BorderEndColorMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D495 RID: 54421 RVA: 0x002F201B File Offset: 0x002F021B
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D496 RID: 54422 RVA: 0x002F2020 File Offset: 0x002F0220
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(1));
			stringBuilder.Append("-color");
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D497 RID: 54423 RVA: 0x002F2074 File Offset: 0x002F0274
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(1));
			stringBuilder.Append("-color");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D498 RID: 54424 RVA: 0x002F20D1 File Offset: 0x002F02D1
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "black", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039D6 RID: 14806
		private Property m_defaultProp;
	}
}
