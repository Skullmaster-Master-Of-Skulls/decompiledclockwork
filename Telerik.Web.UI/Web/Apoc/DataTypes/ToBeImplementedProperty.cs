using System;
using Telerik.Web.Apoc.Fo;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x0200138F RID: 5007
	internal class ToBeImplementedProperty : Property
	{
		// Token: 0x0600D0AE RID: 53422 RVA: 0x002E36EF File Offset: 0x002E18EF
		public ToBeImplementedProperty(string propName)
		{
			ApocDriver.ActiveDriver.FireApocWarning("property - \"" + propName + "\" is not implemented yet.");
		}

		// Token: 0x02001391 RID: 5009
		internal class Maker : PropertyMaker
		{
			// Token: 0x0600D0C6 RID: 53446 RVA: 0x002E395A File Offset: 0x002E1B5A
			public Maker(string propName) : base(propName)
			{
			}

			// Token: 0x0600D0C7 RID: 53447 RVA: 0x002E3964 File Offset: 0x002E1B64
			public override Property ConvertProperty(Property p, PropertyList propertyList, FObj fo)
			{
				if (p is ToBeImplementedProperty)
				{
					return p;
				}
				return new ToBeImplementedProperty(base.PropName);
			}
		}
	}
}
