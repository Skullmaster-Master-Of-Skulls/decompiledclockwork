using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001484 RID: 5252
	internal class BorderEndStyleMaker : GenericBorderStyle
	{
		// Token: 0x0600D49E RID: 54430 RVA: 0x002F213D File Offset: 0x002F033D
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderEndStyleMaker(propName);
		}

		// Token: 0x0600D49F RID: 54431 RVA: 0x002F2145 File Offset: 0x002F0345
		protected BorderEndStyleMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4A0 RID: 54432 RVA: 0x002F2150 File Offset: 0x002F0350
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(1));
			stringBuilder.Append("-style");
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D4A1 RID: 54433 RVA: 0x002F21A4 File Offset: 0x002F03A4
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(1));
			stringBuilder.Append("-style");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}
	}
}
