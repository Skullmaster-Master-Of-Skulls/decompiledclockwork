using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001541 RID: 5441
	internal class PaddingBeforeMaker : GenericCondPadding
	{
		// Token: 0x0600D764 RID: 55140 RVA: 0x002F7AC0 File Offset: 0x002F5CC0
		public new static PropertyMaker Maker(string propName)
		{
			return new PaddingBeforeMaker(propName);
		}

		// Token: 0x0600D765 RID: 55141 RVA: 0x002F7AC8 File Offset: 0x002F5CC8
		protected PaddingBeforeMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D766 RID: 55142 RVA: 0x002F7AD4 File Offset: 0x002F5CD4
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmRelToAbs(2));
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D767 RID: 55143 RVA: 0x002F7B1C File Offset: 0x002F5D1C
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmRelToAbs(2));
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D768 RID: 55144 RVA: 0x002F7B6D File Offset: 0x002F5D6D
		protected override string getDefaultForConditionality()
		{
			return "retain";
		}
	}
}
