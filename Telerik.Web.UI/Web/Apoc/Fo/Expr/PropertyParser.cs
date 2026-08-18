using System;
using System.Collections;
using System.Globalization;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013C0 RID: 5056
	internal class PropertyParser : PropertyTokenizer
	{
		// Token: 0x0600D186 RID: 53638 RVA: 0x002E57BC File Offset: 0x002E39BC
		static PropertyParser()
		{
			PropertyParser.functionTable.Add("ceiling", new CeilingFunction());
			PropertyParser.functionTable.Add("floor", new FloorFunction());
			PropertyParser.functionTable.Add("round", new RoundFunction());
			PropertyParser.functionTable.Add("min", new MinFunction());
			PropertyParser.functionTable.Add("max", new MaxFunction());
			PropertyParser.functionTable.Add("abs", new AbsFunction());
			PropertyParser.functionTable.Add("rgb", new RGBColorFunction());
			PropertyParser.functionTable.Add("from-table-column", new FromTableColumnFunction());
			PropertyParser.functionTable.Add("inherited-property-value", new InheritedPropFunction());
			PropertyParser.functionTable.Add("from-parent", new FromParentFunction());
			PropertyParser.functionTable.Add("from-nearest-specified-value", new NearestSpecPropFunction());
			PropertyParser.functionTable.Add("proportional-column-width", new PPColWidthFunction());
			PropertyParser.functionTable.Add("label-end", new LabelEndFunction());
			PropertyParser.functionTable.Add("body-start", new BodyStartFunction());
			PropertyParser.functionTable.Add("_fop-property-value", new ApocPropValFunction());
		}

		// Token: 0x0600D187 RID: 53639 RVA: 0x002E590F File Offset: 0x002E3B0F
		public static Property parse(string expr, PropertyInfo propInfo)
		{
			return new PropertyParser(expr, propInfo).parseProperty();
		}

		// Token: 0x0600D188 RID: 53640 RVA: 0x002E591D File Offset: 0x002E3B1D
		private PropertyParser(string propExpr, PropertyInfo pInfo) : base(propExpr)
		{
			this.propInfo = pInfo;
		}

		// Token: 0x0600D189 RID: 53641 RVA: 0x002E5930 File Offset: 0x002E3B30
		private Property parseProperty()
		{
			base.next();
			if (this.currentToken == 0)
			{
				return new StringProperty("");
			}
			ListProperty listProperty = null;
			Property property;
			for (;;)
			{
				property = this.parseAdditiveExpr();
				if (this.currentToken == 0)
				{
					break;
				}
				if (listProperty == null)
				{
					listProperty = new ListProperty(property);
				}
				else
				{
					listProperty.addProperty(property);
				}
			}
			if (listProperty != null)
			{
				listProperty.addProperty(property);
				return listProperty;
			}
			return property;
		}

		// Token: 0x0600D18A RID: 53642 RVA: 0x002E598C File Offset: 0x002E3B8C
		private Property parseAdditiveExpr()
		{
			Property property = this.parseMultiplicativeExpr();
			bool flag = true;
			while (flag)
			{
				switch (this.currentToken)
				{
				case 8:
					base.next();
					property = this.evalAddition(property.GetNumeric(), this.parseMultiplicativeExpr().GetNumeric());
					break;
				case 9:
					base.next();
					property = this.evalSubtraction(property.GetNumeric(), this.parseMultiplicativeExpr().GetNumeric());
					break;
				default:
					flag = false;
					break;
				}
			}
			return property;
		}

		// Token: 0x0600D18B RID: 53643 RVA: 0x002E5A04 File Offset: 0x002E3C04
		private Property parseMultiplicativeExpr()
		{
			Property property = this.parseUnaryExpr();
			bool flag = true;
			while (flag)
			{
				int currentToken = this.currentToken;
				if (currentToken != 2)
				{
					switch (currentToken)
					{
					case 10:
						base.next();
						property = this.evalModulo(property.GetNumber(), this.parseUnaryExpr().GetNumber());
						break;
					case 11:
						base.next();
						property = this.evalDivide(property.GetNumeric(), this.parseUnaryExpr().GetNumeric());
						break;
					default:
						flag = false;
						break;
					}
				}
				else
				{
					base.next();
					property = this.evalMultiply(property.GetNumeric(), this.parseUnaryExpr().GetNumeric());
				}
			}
			return property;
		}

		// Token: 0x0600D18C RID: 53644 RVA: 0x002E5AA6 File Offset: 0x002E3CA6
		private Property parseUnaryExpr()
		{
			if (this.currentToken == 9)
			{
				base.next();
				return this.evalNegate(this.parseUnaryExpr().GetNumeric());
			}
			return this.parsePrimaryExpr();
		}

		// Token: 0x0600D18D RID: 53645 RVA: 0x002E5AD0 File Offset: 0x002E3CD0
		private void expectRpar()
		{
			if (this.currentToken != 4)
			{
				throw new PropertyException("expected )");
			}
			base.next();
		}

		// Token: 0x0600D18E RID: 53646 RVA: 0x002E5AEC File Offset: 0x002E3CEC
		private Property parsePrimaryExpr()
		{
			Property result;
			switch (this.currentToken)
			{
			case 1:
				result = new NCnameProperty(this.currentTokenValue);
				goto IL_25D;
			case 3:
				base.next();
				result = this.parseAdditiveExpr();
				this.expectRpar();
				return result;
			case 5:
				result = new StringProperty(this.currentTokenValue);
				goto IL_25D;
			case 7:
			{
				IFunction function = (IFunction)PropertyParser.functionTable[this.currentTokenValue];
				if (function == null)
				{
					throw new PropertyException("no such function: " + this.currentTokenValue);
				}
				base.next();
				this.propInfo.pushFunction(function);
				result = function.Eval(this.parseArgs(function.NumArgs), this.propInfo);
				this.propInfo.popFunction();
				return result;
			}
			case 12:
			{
				int num = this.currentTokenValue.Length - this.currentUnitLength;
				string text = this.currentTokenValue.Substring(num);
				double num2 = this.ParseDouble(this.currentTokenValue.Substring(0, num));
				Length length;
				if (text.Equals("em"))
				{
					length = new FixedLength(num2, this.propInfo.currentFontSize());
				}
				else
				{
					length = new FixedLength(num2, text);
				}
				if (length == null)
				{
					throw new PropertyException("unrecognized unit name: " + this.currentTokenValue);
				}
				result = new LengthProperty(length);
				goto IL_25D;
			}
			case 14:
			{
				double num3 = this.ParseDouble(this.currentTokenValue.Substring(0, this.currentTokenValue.Length - 1)) / 100.0;
				IPercentBase percentBase = this.propInfo.GetPercentBase();
				if (percentBase == null)
				{
					result = new NumberProperty(num3);
					goto IL_25D;
				}
				if (percentBase.GetDimension() == 0)
				{
					result = new NumberProperty(num3 * percentBase.GetBaseValue());
					goto IL_25D;
				}
				if (percentBase.GetDimension() == 1)
				{
					result = new LengthProperty(new PercentLength(num3, percentBase));
					goto IL_25D;
				}
				throw new PropertyException("Illegal percent dimension value");
			}
			case 15:
				result = new ColorTypeProperty(new ColorType(this.currentTokenValue));
				goto IL_25D;
			case 16:
				result = new NumberProperty(this.ParseDouble(this.currentTokenValue));
				goto IL_25D;
			case 17:
				result = new NumberProperty(int.Parse(this.currentTokenValue));
				goto IL_25D;
			}
			throw new PropertyException("syntax error");
			IL_25D:
			base.next();
			return result;
		}

		// Token: 0x0600D18F RID: 53647 RVA: 0x002E5D60 File Offset: 0x002E3F60
		private Property[] parseArgs(int nbArgs)
		{
			Property[] array = new Property[nbArgs];
			int num = 0;
			if (this.currentToken == 4)
			{
				base.next();
			}
			else
			{
				for (;;)
				{
					Property property = this.parseAdditiveExpr();
					if (num < nbArgs)
					{
						array[num++] = property;
					}
					if (this.currentToken != 13)
					{
						break;
					}
					base.next();
				}
				this.expectRpar();
			}
			if (nbArgs != num)
			{
				throw new PropertyException("Wrong number of args for function");
			}
			return array;
		}

		// Token: 0x0600D190 RID: 53648 RVA: 0x002E5DC2 File Offset: 0x002E3FC2
		private Property evalAddition(Numeric op1, Numeric op2)
		{
			if (op1 == null || op2 == null)
			{
				throw new PropertyException("Non numeric operand in addition");
			}
			return new NumericProperty(op1.add(op2));
		}

		// Token: 0x0600D191 RID: 53649 RVA: 0x002E5DE1 File Offset: 0x002E3FE1
		private Property evalSubtraction(Numeric op1, Numeric op2)
		{
			if (op1 == null || op2 == null)
			{
				throw new PropertyException("Non numeric operand in subtraction");
			}
			return new NumericProperty(op1.subtract(op2));
		}

		// Token: 0x0600D192 RID: 53650 RVA: 0x002E5E00 File Offset: 0x002E4000
		private Property evalNegate(Numeric op)
		{
			if (op == null)
			{
				throw new PropertyException("Non numeric operand to unary minus");
			}
			return new NumericProperty(op.multiply(PropertyParser.negOne));
		}

		// Token: 0x0600D193 RID: 53651 RVA: 0x002E5E20 File Offset: 0x002E4020
		private Property evalMultiply(Numeric op1, Numeric op2)
		{
			if (op1 == null || op2 == null)
			{
				throw new PropertyException("Non numeric operand in multiplication");
			}
			return new NumericProperty(op1.multiply(op2));
		}

		// Token: 0x0600D194 RID: 53652 RVA: 0x002E5E3F File Offset: 0x002E403F
		private Property evalDivide(Numeric op1, Numeric op2)
		{
			if (op1 == null || op2 == null)
			{
				throw new PropertyException("Non numeric operand in division");
			}
			return new NumericProperty(op1.divide(op2));
		}

		// Token: 0x0600D195 RID: 53653 RVA: 0x002E5E5E File Offset: 0x002E405E
		private Property evalModulo(Number op1, Number op2)
		{
			if (op1 == null || op2 == null)
			{
				throw new PropertyException("Non number operand to modulo");
			}
			return new NumberProperty(op1.DoubleValue() % op2.DoubleValue());
		}

		// Token: 0x0600D196 RID: 53654 RVA: 0x002E5E83 File Offset: 0x002E4083
		private double ParseDouble(string s)
		{
			return double.Parse(s, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x0400384D RID: 14413
		private const string RELUNIT = "em";

		// Token: 0x0400384E RID: 14414
		private PropertyInfo propInfo;

		// Token: 0x0400384F RID: 14415
		private static Numeric negOne = new Numeric(-1m);

		// Token: 0x04003850 RID: 14416
		private static Hashtable functionTable = new Hashtable();
	}
}
