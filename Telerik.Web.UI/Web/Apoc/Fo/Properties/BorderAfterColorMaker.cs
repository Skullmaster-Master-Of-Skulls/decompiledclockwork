using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001463 RID: 5219
	internal class BorderAfterColorMaker : GenericColor
	{
		// Token: 0x0600D424 RID: 54308 RVA: 0x002F1047 File Offset: 0x002EF247
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderAfterColorMaker(propName);
		}

		// Token: 0x0600D425 RID: 54309 RVA: 0x002F104F File Offset: 0x002EF24F
		protected BorderAfterColorMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D426 RID: 54310 RVA: 0x002F1058 File Offset: 0x002EF258
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D427 RID: 54311 RVA: 0x002F105C File Offset: 0x002EF25C
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(3));
			stringBuilder.Append("-color");
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D428 RID: 54312 RVA: 0x002F10B0 File Offset: 0x002EF2B0
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(3));
			stringBuilder.Append("-color");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D429 RID: 54313 RVA: 0x002F110D File Offset: 0x002EF30D
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "black", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039AB RID: 14763
		private Property m_defaultProp;
	}
}
