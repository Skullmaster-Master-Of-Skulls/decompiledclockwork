using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200149B RID: 5275
	internal class BorderStartWidthMaker : GenericCondBorderWidth
	{
		// Token: 0x0600D4F3 RID: 54515 RVA: 0x002F2DC1 File Offset: 0x002F0FC1
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderStartWidthMaker(propName);
		}

		// Token: 0x0600D4F4 RID: 54516 RVA: 0x002F2DC9 File Offset: 0x002F0FC9
		protected BorderStartWidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4F5 RID: 54517 RVA: 0x002F2DD4 File Offset: 0x002F0FD4
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(0));
			stringBuilder.Append("-width");
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D4F6 RID: 54518 RVA: 0x002F2E28 File Offset: 0x002F1028
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(0));
			stringBuilder.Append("-width");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D4F7 RID: 54519 RVA: 0x002F2E85 File Offset: 0x002F1085
		protected override string getDefaultForConditionality()
		{
			return "discard";
		}
	}
}
