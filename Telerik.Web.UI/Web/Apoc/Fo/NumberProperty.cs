using System;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Fo.Expr;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x02001426 RID: 5158
	internal class NumberProperty : Property
	{
		// Token: 0x0600D2FA RID: 54010 RVA: 0x002ED43E File Offset: 0x002EB63E
		public NumberProperty(Number num)
		{
			this.number = num.DecimalValue();
		}

		// Token: 0x0600D2FB RID: 54011 RVA: 0x002ED452 File Offset: 0x002EB652
		public NumberProperty(decimal num)
		{
			this.number = num;
		}

		// Token: 0x0600D2FC RID: 54012 RVA: 0x002ED461 File Offset: 0x002EB661
		public NumberProperty(double num)
		{
			this.number = (decimal)num;
		}

		// Token: 0x0600D2FD RID: 54013 RVA: 0x002ED476 File Offset: 0x002EB676
		public NumberProperty(int num)
		{
			this.number = num;
		}

		// Token: 0x0600D2FE RID: 54014 RVA: 0x002ED48A File Offset: 0x002EB68A
		public override Number GetNumber()
		{
			return new Number(this.number);
		}

		// Token: 0x0600D2FF RID: 54015 RVA: 0x002ED497 File Offset: 0x002EB697
		public override object GetObject()
		{
			return this.number;
		}

		// Token: 0x0600D300 RID: 54016 RVA: 0x002ED4A4 File Offset: 0x002EB6A4
		public override Numeric GetNumeric()
		{
			return new Numeric(this.number);
		}

		// Token: 0x0600D301 RID: 54017 RVA: 0x002ED4B1 File Offset: 0x002EB6B1
		public override ColorType GetColorType()
		{
			return new ColorType(0f, 0f, 0f);
		}

		// Token: 0x04003927 RID: 14631
		private decimal number;

		// Token: 0x02001427 RID: 5159
		internal class Maker : PropertyMaker
		{
			// Token: 0x0600D302 RID: 54018 RVA: 0x002ED4C7 File Offset: 0x002EB6C7
			public Maker(string propName) : base(propName)
			{
			}

			// Token: 0x0600D303 RID: 54019 RVA: 0x002ED4D0 File Offset: 0x002EB6D0
			public override Property ConvertProperty(Property p, PropertyList propertyList, FObj fo)
			{
				if (p is NumberProperty)
				{
					return p;
				}
				Number number = p.GetNumber();
				if (number != null)
				{
					return new NumberProperty(number);
				}
				return this.ConvertPropertyDatatype(p, propertyList, fo);
			}
		}
	}
}
