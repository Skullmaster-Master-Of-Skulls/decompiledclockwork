using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001545 RID: 5445
	internal class PaddingEndMaker : GenericCondPadding
	{
		// Token: 0x0600D76E RID: 55150 RVA: 0x002F7BE9 File Offset: 0x002F5DE9
		public new static PropertyMaker Maker(string propName)
		{
			return new PaddingEndMaker(propName);
		}

		// Token: 0x0600D76F RID: 55151 RVA: 0x002F7BF1 File Offset: 0x002F5DF1
		protected PaddingEndMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D770 RID: 55152 RVA: 0x002F7BFC File Offset: 0x002F5DFC
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmRelToAbs(1));
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D771 RID: 55153 RVA: 0x002F7C44 File Offset: 0x002F5E44
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmRelToAbs(1));
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D772 RID: 55154 RVA: 0x002F7C95 File Offset: 0x002F5E95
		protected override string getDefaultForConditionality()
		{
			return "discard";
		}
	}
}
