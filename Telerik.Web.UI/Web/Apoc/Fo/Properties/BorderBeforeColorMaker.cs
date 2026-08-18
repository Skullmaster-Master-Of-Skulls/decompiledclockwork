using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001471 RID: 5233
	internal class BorderBeforeColorMaker : GenericColor
	{
		// Token: 0x0600D45A RID: 54362 RVA: 0x002F1828 File Offset: 0x002EFA28
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderBeforeColorMaker(propName);
		}

		// Token: 0x0600D45B RID: 54363 RVA: 0x002F1830 File Offset: 0x002EFA30
		protected BorderBeforeColorMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D45C RID: 54364 RVA: 0x002F1839 File Offset: 0x002EFA39
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D45D RID: 54365 RVA: 0x002F183C File Offset: 0x002EFA3C
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(2));
			stringBuilder.Append("-color");
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D45E RID: 54366 RVA: 0x002F1890 File Offset: 0x002EFA90
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(2));
			stringBuilder.Append("-color");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D45F RID: 54367 RVA: 0x002F18ED File Offset: 0x002EFAED
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "black", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039CC RID: 14796
		private Property m_defaultProp;
	}
}
