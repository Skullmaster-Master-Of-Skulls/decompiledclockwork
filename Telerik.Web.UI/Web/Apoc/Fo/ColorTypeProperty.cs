using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020013A3 RID: 5027
	internal class ColorTypeProperty : Property
	{
		// Token: 0x0600D114 RID: 53524 RVA: 0x002E436A File Offset: 0x002E256A
		public ColorTypeProperty(ColorType colorType)
		{
			this.colorType = colorType;
		}

		// Token: 0x0600D115 RID: 53525 RVA: 0x002E4379 File Offset: 0x002E2579
		public override ColorType GetColorType()
		{
			return this.colorType;
		}

		// Token: 0x0600D116 RID: 53526 RVA: 0x002E4381 File Offset: 0x002E2581
		public override object GetObject()
		{
			return this.colorType;
		}

		// Token: 0x0400381D RID: 14365
		private ColorType colorType;

		// Token: 0x020013A4 RID: 5028
		internal class Maker : PropertyMaker
		{
			// Token: 0x0600D117 RID: 53527 RVA: 0x002E4389 File Offset: 0x002E2589
			public Maker(string propName) : base(propName)
			{
			}

			// Token: 0x0600D118 RID: 53528 RVA: 0x002E4394 File Offset: 0x002E2594
			public override Property ConvertProperty(Property p, PropertyList propertyList, FObj fo)
			{
				if (p is ColorTypeProperty)
				{
					return p;
				}
				ColorType colorType = p.GetColorType();
				if (colorType != null)
				{
					return new ColorTypeProperty(colorType);
				}
				string ncname = p.GetNCname();
				if (!string.IsNullOrEmpty(ncname))
				{
					ColorType colorType2 = new ColorType(ncname);
					return new ColorTypeProperty(colorType2);
				}
				return this.ConvertPropertyDatatype(p, propertyList, fo);
			}
		}
	}
}
