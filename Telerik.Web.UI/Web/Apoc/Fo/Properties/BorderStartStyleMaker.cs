using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001498 RID: 5272
	internal class BorderStartStyleMaker : GenericBorderStyle
	{
		// Token: 0x0600D4ED RID: 54509 RVA: 0x002F2CED File Offset: 0x002F0EED
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderStartStyleMaker(propName);
		}

		// Token: 0x0600D4EE RID: 54510 RVA: 0x002F2CF5 File Offset: 0x002F0EF5
		protected BorderStartStyleMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4EF RID: 54511 RVA: 0x002F2D00 File Offset: 0x002F0F00
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(0));
			stringBuilder.Append("-style");
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D4F0 RID: 54512 RVA: 0x002F2D54 File Offset: 0x002F0F54
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(0));
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
