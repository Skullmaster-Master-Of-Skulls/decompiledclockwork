using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000E8 RID: 232
	internal sealed class ConstNode : ExpressionNode
	{
		// Token: 0x06000F4B RID: 3915 RVA: 0x0007BDC4 File Offset: 0x0007B1C4
		internal ConstNode(DataTable table, ValueType type, object constant) : this(table, type, constant, true)
		{
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0007BDDC File Offset: 0x0007B1DC
		internal ConstNode(DataTable table, ValueType type, object constant, bool fParseQuotes) : base(table)
		{
			switch (type)
			{
			case ValueType.Null:
				this.val = DBNull.Value;
				return;
			case ValueType.Bool:
				this.val = Convert.ToBoolean(constant, CultureInfo.InvariantCulture);
				return;
			case ValueType.Numeric:
				this.val = this.SmallestNumeric(constant);
				return;
			case ValueType.Str:
				if (fParseQuotes)
				{
					this.val = ((string)constant).Replace("''", "'");
					return;
				}
				this.val = (string)constant;
				return;
			case ValueType.Float:
				this.val = Convert.ToDouble(constant, NumberFormatInfo.InvariantInfo);
				return;
			case ValueType.Decimal:
				this.val = this.SmallestDecimal(constant);
				return;
			case ValueType.Date:
				this.val = DateTime.Parse((string)constant, CultureInfo.InvariantCulture);
				return;
			}
			this.val = constant;
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0007BEC4 File Offset: 0x0007B2C4
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0007BED8 File Offset: 0x0007B2D8
		internal override object Eval()
		{
			return this.val;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0007BEEC File Offset: 0x0007B2EC
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.Eval();
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0007BF00 File Offset: 0x0007B300
		internal override object Eval(int[] recordNos)
		{
			return this.Eval();
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0007BF14 File Offset: 0x0007B314
		internal override bool IsConstant()
		{
			return true;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0007BF24 File Offset: 0x0007B324
		internal override bool IsTableConstant()
		{
			return true;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0007BF34 File Offset: 0x0007B334
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0007BF44 File Offset: 0x0007B344
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0007BF54 File Offset: 0x0007B354
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0007BF64 File Offset: 0x0007B364
		private object SmallestDecimal(object constant)
		{
			if (constant == null)
			{
				return 0.0;
			}
			string text = constant as string;
			if (text != null)
			{
				decimal num;
				if (decimal.TryParse(text, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out num))
				{
					return num;
				}
				double num2;
				if (double.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out num2))
				{
					return num2;
				}
			}
			else
			{
				IConvertible convertible = constant as IConvertible;
				if (convertible != null)
				{
					try
					{
						return convertible.ToDecimal(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException e)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e);
					}
					catch (FormatException e2)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e2);
					}
					catch (InvalidCastException e3)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e3);
					}
					catch (OverflowException e4)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e4);
					}
					try
					{
						return convertible.ToDouble(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException e5)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e5);
					}
					catch (FormatException e6)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e6);
					}
					catch (InvalidCastException e7)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e7);
					}
					catch (OverflowException e8)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e8);
					}
					return constant;
				}
			}
			return constant;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0007C118 File Offset: 0x0007B518
		private object SmallestNumeric(object constant)
		{
			if (constant == null)
			{
				return 0;
			}
			string text = constant as string;
			if (text != null)
			{
				int num;
				if (int.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
				{
					return num;
				}
				long num2;
				if (long.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2))
				{
					return num2;
				}
				double num3;
				if (double.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out num3))
				{
					return num3;
				}
			}
			else
			{
				IConvertible convertible = constant as IConvertible;
				if (convertible != null)
				{
					try
					{
						return convertible.ToInt32(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException e)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e);
					}
					catch (FormatException e2)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e2);
					}
					catch (InvalidCastException e3)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e3);
					}
					catch (OverflowException e4)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e4);
					}
					try
					{
						return convertible.ToInt64(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException e5)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e5);
					}
					catch (FormatException e6)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e6);
					}
					catch (InvalidCastException e7)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e7);
					}
					catch (OverflowException e8)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e8);
					}
					try
					{
						return convertible.ToDouble(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException e9)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e9);
					}
					catch (FormatException e10)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e10);
					}
					catch (InvalidCastException e11)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e11);
					}
					catch (OverflowException e12)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e12);
					}
					return constant;
				}
			}
			return constant;
		}

		// Token: 0x04000493 RID: 1171
		internal readonly object val;
	}
}
