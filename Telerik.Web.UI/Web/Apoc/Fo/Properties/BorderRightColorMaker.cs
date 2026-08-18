using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200148E RID: 5262
	internal class BorderRightColorMaker : GenericColor
	{
		// Token: 0x0600D4BE RID: 54462 RVA: 0x002F2659 File Offset: 0x002F0859
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderRightColorMaker(propName);
		}

		// Token: 0x0600D4BF RID: 54463 RVA: 0x002F2661 File Offset: 0x002F0861
		protected BorderRightColorMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4C0 RID: 54464 RVA: 0x002F266A File Offset: 0x002F086A
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D4C1 RID: 54465 RVA: 0x002F2670 File Offset: 0x002F0870
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmAbsToRel(1));
			stringBuilder.Append("-color");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D4C2 RID: 54466 RVA: 0x002F26D0 File Offset: 0x002F08D0
		public override Property GetShorthand(PropertyList propertyList)
		{
			Property property = null;
			if (property == null)
			{
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("border-right");
				if (listProperty != null)
				{
					IShorthandParser shorthandParser = new GenericShorthandParser(listProperty);
					property = shorthandParser.GetValueForProperty(base.PropName, this, propertyList);
				}
			}
			if (property == null)
			{
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("border-color");
				if (listProperty != null)
				{
					IShorthandParser shorthandParser2 = new BoxPropShorthandParser(listProperty);
					property = shorthandParser2.GetValueForProperty(base.PropName, this, propertyList);
				}
			}
			if (property == null)
			{
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("border");
				if (listProperty != null)
				{
					IShorthandParser shorthandParser3 = new GenericShorthandParser(listProperty);
					property = shorthandParser3.GetValueForProperty(base.PropName, this, propertyList);
				}
			}
			return property;
		}

		// Token: 0x0600D4C3 RID: 54467 RVA: 0x002F2769 File Offset: 0x002F0969
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "black", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039D9 RID: 14809
		private Property m_defaultProp;
	}
}
