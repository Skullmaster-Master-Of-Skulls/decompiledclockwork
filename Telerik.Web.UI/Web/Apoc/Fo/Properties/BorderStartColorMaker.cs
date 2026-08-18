using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001495 RID: 5269
	internal class BorderStartColorMaker : GenericColor
	{
		// Token: 0x0600D4E2 RID: 54498 RVA: 0x002F2BBA File Offset: 0x002F0DBA
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderStartColorMaker(propName);
		}

		// Token: 0x0600D4E3 RID: 54499 RVA: 0x002F2BC2 File Offset: 0x002F0DC2
		protected BorderStartColorMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4E4 RID: 54500 RVA: 0x002F2BCB File Offset: 0x002F0DCB
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D4E5 RID: 54501 RVA: 0x002F2BD0 File Offset: 0x002F0DD0
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(0));
			stringBuilder.Append("-color");
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D4E6 RID: 54502 RVA: 0x002F2C24 File Offset: 0x002F0E24
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(0));
			stringBuilder.Append("-color");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D4E7 RID: 54503 RVA: 0x002F2C81 File Offset: 0x002F0E81
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "black", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039DF RID: 14815
		private Property m_defaultProp;
	}
}
