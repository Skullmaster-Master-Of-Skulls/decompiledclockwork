using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001542 RID: 5442
	internal class PaddingBottomMaker : GenericPadding
	{
		// Token: 0x0600D769 RID: 55145 RVA: 0x002F7B74 File Offset: 0x002F5D74
		public new static PropertyMaker Maker(string propName)
		{
			return new PaddingBottomMaker(propName);
		}

		// Token: 0x0600D76A RID: 55146 RVA: 0x002F7B7C File Offset: 0x002F5D7C
		protected PaddingBottomMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D76B RID: 55147 RVA: 0x002F7B88 File Offset: 0x002F5D88
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmAbsToRel(3));
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}
	}
}
