using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001546 RID: 5446
	internal class PaddingLeftMaker : GenericPadding
	{
		// Token: 0x0600D773 RID: 55155 RVA: 0x002F7C9C File Offset: 0x002F5E9C
		public new static PropertyMaker Maker(string propName)
		{
			return new PaddingLeftMaker(propName);
		}

		// Token: 0x0600D774 RID: 55156 RVA: 0x002F7CA4 File Offset: 0x002F5EA4
		protected PaddingLeftMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D775 RID: 55157 RVA: 0x002F7CB0 File Offset: 0x002F5EB0
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmAbsToRel(0));
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}
	}
}
