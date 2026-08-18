using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.RegularExpressions;

namespace System.Web.UI.Design
{
	// Token: 0x02000038 RID: 56
	internal sealed class DesignTimeDataBinding
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x0000D8D6 File Offset: 0x0000BAD6
		public DesignTimeDataBinding(DataBinding runtimeDataBinding)
		{
			this._runtimeDataBinding = runtimeDataBinding;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000D8E5 File Offset: 0x0000BAE5
		public DesignTimeDataBinding(PropertyDescriptor propDesc, string expression)
		{
			this._expression = expression;
			this._runtimeDataBinding = new DataBinding(propDesc.Name, propDesc.PropertyType, expression);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000D90C File Offset: 0x0000BB0C
		public DesignTimeDataBinding(PropertyDescriptor propDesc, string field, string format, bool twoWayBinding)
		{
			this._field = field;
			this._format = format;
			if (twoWayBinding)
			{
				this._expression = DesignTimeDataBinding.CreateBindExpression(field, format);
			}
			else
			{
				this._expression = DesignTimeDataBinding.CreateEvalExpression(field, format);
			}
			this._parsed = true;
			this._twoWayBinding = twoWayBinding;
			this._runtimeDataBinding = new DataBinding(propDesc.Name, propDesc.PropertyType, this._expression);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000D979 File Offset: 0x0000BB79
		public bool IsCustom
		{
			get
			{
				this.EnsureParsed();
				return this._field == null;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000D98A File Offset: 0x0000BB8A
		public string Expression
		{
			get
			{
				this.EnsureParsed();
				return this._expression;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000D998 File Offset: 0x0000BB98
		public string Field
		{
			get
			{
				this.EnsureParsed();
				return this._field;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000D9A6 File Offset: 0x0000BBA6
		public string Format
		{
			get
			{
				this.EnsureParsed();
				return this._format;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000D9B4 File Offset: 0x0000BBB4
		public bool IsTwoWayBound
		{
			get
			{
				this.EnsureParsed();
				return this._twoWayBinding;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000D9C2 File Offset: 0x0000BBC2
		public DataBinding RuntimeDataBinding
		{
			get
			{
				return this._runtimeDataBinding;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000D9CA File Offset: 0x0000BBCA
		public static string CreateBindExpression(string field, string format)
		{
			return DesignTimeDataBinding.CreateExpression("Bind", field, format);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000D9D8 File Offset: 0x0000BBD8
		public static string CreateEvalExpression(string field, string format)
		{
			return DesignTimeDataBinding.CreateExpression("Eval", field, format);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
		private static string CreateExpression(string method, string field, string format)
		{
			string text = field;
			foreach (char c in field)
			{
				if (!char.IsLetterOrDigit(c) && c != '_' && c != '.')
				{
					text = "[" + field + "]";
					break;
				}
			}
			if (format != null && format.Length != 0)
			{
				return string.Format(CultureInfo.InvariantCulture, method + "(\"{0}\", \"{1}\")", new object[]
				{
					text,
					format
				});
			}
			return string.Format(CultureInfo.InvariantCulture, method + "(\"{0}\")", new object[]
			{
				text
			});
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000DA84 File Offset: 0x0000BC84
		private void EnsureParsed()
		{
			if (!this._parsed)
			{
				this._expression = this._runtimeDataBinding.Expression.Trim();
				if (this._expression.Length != 0)
				{
					try
					{
						bool flag = false;
						Match match = DesignTimeDataBinding.EvalRegex.Match(this._expression);
						if (match.Success)
						{
							flag = true;
						}
						else
						{
							match = DesignTimeDataBinding.BindExpressionRegex.Match(this._expression);
						}
						if (match.Success)
						{
							string value = match.Groups["params"].Value;
							if ((match = DesignTimeDataBinding.BindParametersRegex.Match(value, 0)).Success)
							{
								this._field = match.Groups["fieldName"].Value;
								Group group = match.Groups["formatString"];
								if (group != null)
								{
									this._format = group.Value;
								}
								if (!flag)
								{
									this._twoWayBinding = true;
								}
							}
						}
					}
					catch (Exception ex)
					{
					}
				}
			}
			this._parsed = true;
		}

		// Token: 0x04000130 RID: 304
		private static readonly Regex EvalRegex = new EvalExpressionRegex();

		// Token: 0x04000131 RID: 305
		private static readonly Regex BindExpressionRegex = new BindExpressionRegex();

		// Token: 0x04000132 RID: 306
		private static readonly Regex BindParametersRegex = new BindParametersRegex();

		// Token: 0x04000133 RID: 307
		private DataBinding _runtimeDataBinding;

		// Token: 0x04000134 RID: 308
		private bool _parsed;

		// Token: 0x04000135 RID: 309
		private bool _twoWayBinding;

		// Token: 0x04000136 RID: 310
		private string _field;

		// Token: 0x04000137 RID: 311
		private string _format;

		// Token: 0x04000138 RID: 312
		private string _expression;
	}
}
