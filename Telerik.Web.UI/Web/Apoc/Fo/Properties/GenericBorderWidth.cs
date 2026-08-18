using System;
using System.Collections;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200147C RID: 5244
	internal class GenericBorderWidth : LengthProperty.Maker
	{
		// Token: 0x0600D47E RID: 54398 RVA: 0x002F1D55 File Offset: 0x002EFF55
		public new static PropertyMaker Maker(string propName)
		{
			return new GenericBorderWidth(propName);
		}

		// Token: 0x0600D47F RID: 54399 RVA: 0x002F1D5D File Offset: 0x002EFF5D
		protected GenericBorderWidth(string name) : base(name)
		{
		}

		// Token: 0x0600D480 RID: 54400 RVA: 0x002F1D66 File Offset: 0x002EFF66
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D481 RID: 54401 RVA: 0x002F1D6C File Offset: 0x002EFF6C
		public override Property GetShorthand(PropertyList propertyList)
		{
			Property property = null;
			if (property == null)
			{
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("border-width");
				if (listProperty != null)
				{
					IShorthandParser shorthandParser = new BoxPropShorthandParser(listProperty);
					property = shorthandParser.GetValueForProperty(base.PropName, this, propertyList);
				}
			}
			return property;
		}

		// Token: 0x0600D482 RID: 54402 RVA: 0x002F1DAC File Offset: 0x002EFFAC
		private static void initKeywords()
		{
			GenericBorderWidth.s_htKeywords = new Hashtable(3);
			GenericBorderWidth.s_htKeywords.Add("thin", "0.5pt");
			GenericBorderWidth.s_htKeywords.Add("medium", "1pt");
			GenericBorderWidth.s_htKeywords.Add("thick", "2pt");
		}

		// Token: 0x0600D483 RID: 54403 RVA: 0x002F1E00 File Offset: 0x002F0000
		protected override string CheckValueKeywords(string keyword)
		{
			if (GenericBorderWidth.s_htKeywords == null)
			{
				GenericBorderWidth.initKeywords();
			}
			string text = (string)GenericBorderWidth.s_htKeywords[keyword];
			if (text == null)
			{
				return base.CheckValueKeywords(keyword);
			}
			return text;
		}

		// Token: 0x0600D484 RID: 54404 RVA: 0x002F1E36 File Offset: 0x002F0036
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039CF RID: 14799
		private static Hashtable s_htKeywords;

		// Token: 0x040039D0 RID: 14800
		private Property m_defaultProp;
	}
}
