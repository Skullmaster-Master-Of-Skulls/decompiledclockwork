using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020001A6 RID: 422
	internal sealed class ConstNode : ExpressionNode
	{
		// Token: 0x0600187A RID: 6266 RVA: 0x00253BB8 File Offset: 0x00252FB8
		internal ConstNode(DataTable table, ValueType type, object constant) : this(table, type, constant, true)
		{
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00253BD8 File Offset: 0x00252FD8
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

		// Token: 0x0600187C RID: 6268 RVA: 0x00253CC8 File Offset: 0x002530C8
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x00253CE8 File Offset: 0x002530E8
		internal override object Eval()
		{
			return this.val;
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x00253D08 File Offset: 0x00253108
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.Eval();
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00253D28 File Offset: 0x00253128
		internal override object Eval(int[] recordNos)
		{
			return this.Eval();
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00253D48 File Offset: 0x00253148
		internal override bool IsConstant()
		{
			return true;
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x00253D58 File Offset: 0x00253158
		internal override bool IsTableConstant()
		{
			return true;
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00253D68 File Offset: 0x00253168
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00253D78 File Offset: 0x00253178
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00253D88 File Offset: 0x00253188
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00253D98 File Offset: 0x00253198
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

		// Token: 0x06001886 RID: 6278 RVA: 0x00253F58 File Offset: 0x00253358
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

		// Token: 0x04000D62 RID: 3426
		internal readonly object val;
	}
}
