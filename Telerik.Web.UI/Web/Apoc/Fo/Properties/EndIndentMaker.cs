using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014C3 RID: 5315
	internal class EndIndentMaker : LengthProperty.Maker
	{
		// Token: 0x0600D589 RID: 54665 RVA: 0x002F39D4 File Offset: 0x002F1BD4
		public new static PropertyMaker Maker(string propName)
		{
			return new EndIndentMaker(propName);
		}

		// Token: 0x0600D58A RID: 54666 RVA: 0x002F39DC File Offset: 0x002F1BDC
		protected EndIndentMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D58B RID: 54667 RVA: 0x002F39E5 File Offset: 0x002F1BE5
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D58C RID: 54668 RVA: 0x002F39E8 File Offset: 0x002F1BE8
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append("margin-");
			stringBuilder.Append(propertyList.wmRelToAbs(1));
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D58D RID: 54669 RVA: 0x002F3A30 File Offset: 0x002F1C30
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			Property property = null;
			stringBuilder.Append("margin-");
			stringBuilder.Append(propertyList.wmRelToAbs(1));
			if (propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString()) == null)
			{
				return property;
			}
			stringBuilder.Length = 0;
			stringBuilder.Append("_fop-property-value(");
			stringBuilder.Append("margin-");
			stringBuilder.Append(propertyList.wmRelToAbs(1));
			stringBuilder.Append(")");
			stringBuilder.Append("+");
			stringBuilder.Append("_fop-property-value(");
			stringBuilder.Append("padding-");
			stringBuilder.Append(propertyList.wmRelToAbs(1));
			stringBuilder.Append(")");
			stringBuilder.Append("+");
			stringBuilder.Append("_fop-property-value(");
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmRelToAbs(1));
			stringBuilder.Append("-width");
			stringBuilder.Append(")");
			property = this.Make(propertyList, stringBuilder.ToString(), propertyList.getParentFObj());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D58E RID: 54670 RVA: 0x002F3B59 File Offset: 0x002F1D59
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A68 RID: 14952
		private Property m_defaultProp;
	}
}
