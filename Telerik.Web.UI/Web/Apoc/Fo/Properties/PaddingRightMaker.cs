using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001548 RID: 5448
	internal class PaddingRightMaker : GenericPadding
	{
		// Token: 0x0600D779 RID: 55161 RVA: 0x002F7D15 File Offset: 0x002F5F15
		public new static PropertyMaker Maker(string propName)
		{
			return new PaddingRightMaker(propName);
		}

		// Token: 0x0600D77A RID: 55162 RVA: 0x002F7D1D File Offset: 0x002F5F1D
		protected PaddingRightMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D77B RID: 55163 RVA: 0x002F7D28 File Offset: 0x002F5F28
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmAbsToRel(1));
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}
	}
}
