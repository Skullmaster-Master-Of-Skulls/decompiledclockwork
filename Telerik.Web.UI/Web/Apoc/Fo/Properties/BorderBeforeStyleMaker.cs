using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001474 RID: 5236
	internal class BorderBeforeStyleMaker : GenericBorderStyle
	{
		// Token: 0x0600D465 RID: 54373 RVA: 0x002F1959 File Offset: 0x002EFB59
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderBeforeStyleMaker(propName);
		}

		// Token: 0x0600D466 RID: 54374 RVA: 0x002F1961 File Offset: 0x002EFB61
		protected BorderBeforeStyleMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D467 RID: 54375 RVA: 0x002F196C File Offset: 0x002EFB6C
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(2));
			stringBuilder.Append("-style");
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D468 RID: 54376 RVA: 0x002F19C0 File Offset: 0x002EFBC0
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(2));
			stringBuilder.Append("-style");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}
	}
}
