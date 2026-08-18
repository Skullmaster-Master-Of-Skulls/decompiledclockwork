using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001470 RID: 5232
	internal class BorderAfterWidthMaker : GenericCondBorderWidth
	{
		// Token: 0x0600D455 RID: 54357 RVA: 0x002F175C File Offset: 0x002EF95C
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderAfterWidthMaker(propName);
		}

		// Token: 0x0600D456 RID: 54358 RVA: 0x002F1764 File Offset: 0x002EF964
		protected BorderAfterWidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D457 RID: 54359 RVA: 0x002F1770 File Offset: 0x002EF970
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(3));
			stringBuilder.Append("-width");
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D458 RID: 54360 RVA: 0x002F17C4 File Offset: 0x002EF9C4
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(3));
			stringBuilder.Append("-width");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D459 RID: 54361 RVA: 0x002F1821 File Offset: 0x002EFA21
		protected override string getDefaultForConditionality()
		{
			return "retain";
		}
	}
}
