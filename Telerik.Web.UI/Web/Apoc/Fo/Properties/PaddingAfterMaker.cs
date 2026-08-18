using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200153E RID: 5438
	internal class PaddingAfterMaker : GenericCondPadding
	{
		// Token: 0x0600D75D RID: 55133 RVA: 0x002F79FD File Offset: 0x002F5BFD
		public new static PropertyMaker Maker(string propName)
		{
			return new PaddingAfterMaker(propName);
		}

		// Token: 0x0600D75E RID: 55134 RVA: 0x002F7A05 File Offset: 0x002F5C05
		protected PaddingAfterMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D75F RID: 55135 RVA: 0x002F7A10 File Offset: 0x002F5C10
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmRelToAbs(3));
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D760 RID: 55136 RVA: 0x002F7A58 File Offset: 0x002F5C58
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmRelToAbs(3));
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D761 RID: 55137 RVA: 0x002F7AA9 File Offset: 0x002F5CA9
		protected override string getDefaultForConditionality()
		{
			return "retain";
		}
	}
}
