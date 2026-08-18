using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200154B RID: 5451
	internal class PaddingStartMaker : GenericCondPadding
	{
		// Token: 0x0600D77E RID: 55166 RVA: 0x002F7D89 File Offset: 0x002F5F89
		public new static PropertyMaker Maker(string propName)
		{
			return new PaddingStartMaker(propName);
		}

		// Token: 0x0600D77F RID: 55167 RVA: 0x002F7D91 File Offset: 0x002F5F91
		protected PaddingStartMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D780 RID: 55168 RVA: 0x002F7D9C File Offset: 0x002F5F9C
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmRelToAbs(0));
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D781 RID: 55169 RVA: 0x002F7DE4 File Offset: 0x002F5FE4
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmRelToAbs(0));
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D782 RID: 55170 RVA: 0x002F7E35 File Offset: 0x002F6035
		protected override string getDefaultForConditionality()
		{
			return "discard";
		}
	}
}
