using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200158E RID: 5518
	internal class StartIndentMaker : LengthProperty.Maker
	{
		// Token: 0x0600D860 RID: 55392 RVA: 0x002F8DEE File Offset: 0x002F6FEE
		public new static PropertyMaker Maker(string propName)
		{
			return new StartIndentMaker(propName);
		}

		// Token: 0x0600D861 RID: 55393 RVA: 0x002F8DF6 File Offset: 0x002F6FF6
		protected StartIndentMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D862 RID: 55394 RVA: 0x002F8DFF File Offset: 0x002F6FFF
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D863 RID: 55395 RVA: 0x002F8E04 File Offset: 0x002F7004
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("margin-");
			stringBuilder.Append(propertyList.wmRelToAbs(0));
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D864 RID: 55396 RVA: 0x002F8E4C File Offset: 0x002F704C
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			Property property = null;
			stringBuilder.Append("margin-");
			stringBuilder.Append(propertyList.wmRelToAbs(0));
			if (propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString()) == null)
			{
				return property;
			}
			stringBuilder.Length = 0;
			stringBuilder.Append("_fop-property-value(");
			stringBuilder.Append("margin-");
			stringBuilder.Append(propertyList.wmRelToAbs(0));
			stringBuilder.Append(")");
			stringBuilder.Append("+");
			stringBuilder.Append("_fop-property-value(");
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmRelToAbs(0));
			stringBuilder.Append(")");
			stringBuilder.Append("+");
			stringBuilder.Append("_fop-property-value(");
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(0));
			stringBuilder.Append("-width");
			stringBuilder.Append(")");
			property = this.Make(propertyList, stringBuilder.ToString(), propertyList.getParentFObj());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D865 RID: 55397 RVA: 0x002F8F75 File Offset: 0x002F7175
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B82 RID: 15234
		private Property m_defaultProp;
	}
}
