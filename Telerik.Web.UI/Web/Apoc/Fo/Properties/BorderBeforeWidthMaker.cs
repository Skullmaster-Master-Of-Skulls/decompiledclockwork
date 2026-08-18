using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001477 RID: 5239
	internal class BorderBeforeWidthMaker : GenericCondBorderWidth
	{
		// Token: 0x0600D46B RID: 54379 RVA: 0x002F1A2D File Offset: 0x002EFC2D
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderBeforeWidthMaker(propName);
		}

		// Token: 0x0600D46C RID: 54380 RVA: 0x002F1A35 File Offset: 0x002EFC35
		protected BorderBeforeWidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D46D RID: 54381 RVA: 0x002F1A40 File Offset: 0x002EFC40
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(2));
			stringBuilder.Append("-width");
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D46E RID: 54382 RVA: 0x002F1A94 File Offset: 0x002EFC94
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(2));
			stringBuilder.Append("-width");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D46F RID: 54383 RVA: 0x002F1AF1 File Offset: 0x002EFCF1
		protected override string getDefaultForConditionality()
		{
			return "retain";
		}
	}
}
