using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200154C RID: 5452
	internal class PaddingTopMaker : GenericPadding
	{
		// Token: 0x0600D783 RID: 55171 RVA: 0x002F7E3C File Offset: 0x002F603C
		public new static PropertyMaker Maker(string propName)
		{
			return new PaddingTopMaker(propName);
		}

		// Token: 0x0600D784 RID: 55172 RVA: 0x002F7E44 File Offset: 0x002F6044
		protected PaddingTopMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D785 RID: 55173 RVA: 0x002F7E50 File Offset: 0x002F6050
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmAbsToRel(2));
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}
	}
}
