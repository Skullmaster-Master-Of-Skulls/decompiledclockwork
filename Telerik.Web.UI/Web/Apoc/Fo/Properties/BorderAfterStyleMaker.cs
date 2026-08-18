using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001468 RID: 5224
	internal class BorderAfterStyleMaker : GenericBorderStyle
	{
		// Token: 0x0600D437 RID: 54327 RVA: 0x002F1359 File Offset: 0x002EF559
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderAfterStyleMaker(propName);
		}

		// Token: 0x0600D438 RID: 54328 RVA: 0x002F1361 File Offset: 0x002EF561
		protected BorderAfterStyleMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D439 RID: 54329 RVA: 0x002F136C File Offset: 0x002EF56C
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(3));
			stringBuilder.Append("-style");
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D43A RID: 54330 RVA: 0x002F13C0 File Offset: 0x002EF5C0
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(3));
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
