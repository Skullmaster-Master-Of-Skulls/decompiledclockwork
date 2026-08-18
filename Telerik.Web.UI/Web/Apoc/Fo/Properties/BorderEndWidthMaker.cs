using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001487 RID: 5255
	internal class BorderEndWidthMaker : GenericCondBorderWidth
	{
		// Token: 0x0600D4A4 RID: 54436 RVA: 0x002F2211 File Offset: 0x002F0411
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderEndWidthMaker(propName);
		}

		// Token: 0x0600D4A5 RID: 54437 RVA: 0x002F2219 File Offset: 0x002F0419
		protected BorderEndWidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4A6 RID: 54438 RVA: 0x002F2224 File Offset: 0x002F0424
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(1));
			stringBuilder.Append("-width");
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D4A7 RID: 54439 RVA: 0x002F2278 File Offset: 0x002F0478
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(1));
			stringBuilder.Append("-width");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D4A8 RID: 54440 RVA: 0x002F22D5 File Offset: 0x002F04D5
		protected override string getDefaultForConditionality()
		{
			return "discard";
		}
	}
}
