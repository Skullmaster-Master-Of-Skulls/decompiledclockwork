using System;
using System.Globalization;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CD3 RID: 3283
	[DataContract]
	internal sealed class DefaultComparer : ObjectComparer
	{
		// Token: 0x1700275B RID: 10075
		// (get) Token: 0x06007AA8 RID: 31400 RVA: 0x001C230E File Offset: 0x001C050E
		// (set) Token: 0x06007AA9 RID: 31401 RVA: 0x001C2316 File Offset: 0x001C0516
		[DataMember]
		public bool IgnoreCase
		{
			get
			{
				return this.ignoreCase;
			}
			set
			{
				if (this.ignoreCase != value)
				{
					this.ignoreCase = value;
					base.OnPropertyChanged("IgnoreCase");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x06007AAA RID: 31402 RVA: 0x001C2340 File Offset: 0x001C0540
		public override int Compare(object x, object y)
		{
			int? num = DefaultComparer.CompareNulls(x, y);
			int? num6;
			if (num == null)
			{
				int? num2 = DefaultComparer.CompareErrors(x, y);
				if (num2 == null)
				{
					int? num3 = DefaultComparer.CompareNumeric(x, y);
					if (num3 == null)
					{
						int? num4 = DefaultComparer.CompareStrings(x, y, this.IgnoreCase);
						if (num4 == null)
						{
							int? num5 = DefaultComparer.CompareCompareable(x, y);
							num6 = ((num5 != null) ? new int?(num5.GetValueOrDefault()) : DefaultComparer.CompareToStrings(x, y, this.IgnoreCase));
						}
						else
						{
							num6 = new int?(num4.GetValueOrDefault());
						}
					}
					else
					{
						num6 = new int?(num3.GetValueOrDefault());
					}
				}
				else
				{
					num6 = new int?(num2.GetValueOrDefault());
				}
			}
			else
			{
				num6 = new int?(num.GetValueOrDefault());
			}
			int? num7 = num6;
			if (num7 == null)
			{
				return 0;
			}
			return num7.Value;
		}

		// Token: 0x06007AAB RID: 31403 RVA: 0x001C2414 File Offset: 0x001C0614
		private static int? CompareNulls(object x, object y)
		{
			if (x == y)
			{
				return new int?(0);
			}
			if (x == null)
			{
				int? num = DefaultComparer.CompareErrors(null, y);
				if (num != null)
				{
					return new int?(num.GetValueOrDefault());
				}
				int? num2 = DefaultComparer.CompareNumeric(0, y);
				if (num2 != null)
				{
					return new int?(num2.GetValueOrDefault());
				}
				int? num3 = DefaultComparer.CompareStrings(string.Empty, y, true);
				if (num3 != null)
				{
					return new int?(num3.GetValueOrDefault());
				}
				int? num4 = DefaultComparer.CompareCompareable(null, y);
				if (num4 == null)
				{
					return DefaultComparer.CompareToStrings(string.Empty, y, true);
				}
				return new int?(num4.GetValueOrDefault());
			}
			else
			{
				if (y != null)
				{
					return null;
				}
				int? num5 = DefaultComparer.CompareErrors(x, null);
				if (num5 != null)
				{
					return new int?(num5.GetValueOrDefault());
				}
				int? num6 = DefaultComparer.CompareNumeric(x, 0);
				if (num6 != null)
				{
					return new int?(num6.GetValueOrDefault());
				}
				int? num7 = DefaultComparer.CompareStrings(x, string.Empty, true);
				if (num7 != null)
				{
					return new int?(num7.GetValueOrDefault());
				}
				int? num8 = DefaultComparer.CompareCompareable(x, null);
				if (num8 == null)
				{
					return DefaultComparer.CompareToStrings(x, string.Empty, true);
				}
				return new int?(num8.GetValueOrDefault());
			}
		}

		// Token: 0x06007AAC RID: 31404 RVA: 0x001C2564 File Offset: 0x001C0764
		private static int? CompareErrors(object x, object y)
		{
			bool flag = x is AggregateError;
			bool flag2 = y is AggregateError;
			if (flag || flag2)
			{
				return new int?(0);
			}
			if (flag)
			{
				return new int?(-1);
			}
			if (flag2)
			{
				return new int?(1);
			}
			return null;
		}

		// Token: 0x06007AAD RID: 31405 RVA: 0x001C25B0 File Offset: 0x001C07B0
		private static int? CompareNumeric(object x, object y)
		{
			Precision precision = PrecisionHelpers.GetPrecision(x.GetType());
			Precision precision2 = PrecisionHelpers.GetPrecision(y.GetType());
			IConvertible convertible = x as IConvertible;
			IConvertible convertible2 = y as IConvertible;
			if (precision != Precision.Unknown && precision2 != Precision.Unknown)
			{
				if (precision == Precision.Decimal || precision2 == Precision.Decimal)
				{
					decimal num = convertible.ToDecimal(null);
					decimal value = convertible2.ToDecimal(null);
					int value2 = num.CompareTo(value);
					return new int?(value2);
				}
				if (precision == Precision.Double || precision2 == Precision.Double)
				{
					double num2 = convertible.ToDouble(null);
					double value3 = convertible2.ToDouble(null);
					int value2 = num2.CompareTo(value3);
					return new int?(value2);
				}
				if (precision == Precision.Int64 || precision2 == Precision.Int64)
				{
					long num3 = convertible.ToInt64(null);
					long value4 = convertible2.ToInt64(null);
					int value2 = num3.CompareTo(value4);
					return new int?(value2);
				}
			}
			else if (precision != Precision.Unknown)
			{
				if (precision == Precision.Decimal)
				{
					decimal value5 = 0m;
					if (decimal.TryParse(y.ToString(), out value5))
					{
						int value2 = convertible.ToDecimal(null).CompareTo(value5);
						return new int?(value2);
					}
				}
				else if (precision == Precision.Double)
				{
					double value6 = 0.0;
					if (double.TryParse(y.ToString(), out value6))
					{
						int value2 = convertible.ToDouble(null).CompareTo(value6);
						return new int?(value2);
					}
				}
				else if (precision == Precision.Int64)
				{
					long value7 = 0L;
					if (long.TryParse(y.ToString(), out value7))
					{
						int value2 = convertible.ToInt64(null).CompareTo(value7);
						return new int?(value2);
					}
				}
			}
			else if (precision2 != Precision.Unknown)
			{
				if (precision2 == Precision.Decimal)
				{
					decimal num4 = 0m;
					if (decimal.TryParse(x.ToString(), out num4))
					{
						decimal value8 = convertible2.ToDecimal(null);
						int value2 = num4.CompareTo(value8);
						return new int?(value2);
					}
				}
				else if (precision2 == Precision.Double)
				{
					double num5 = 0.0;
					if (double.TryParse(x.ToString(), out num5))
					{
						double value9 = convertible2.ToDouble(null);
						int value2 = num5.CompareTo(value9);
						return new int?(value2);
					}
				}
				else if (precision2 == Precision.Int64)
				{
					long num6 = 0L;
					if (long.TryParse(x.ToString(), out num6))
					{
						long value10 = convertible2.ToInt64(null);
						int value2 = num6.CompareTo(value10);
						return new int?(value2);
					}
				}
			}
			return null;
		}

		// Token: 0x06007AAE RID: 31406 RVA: 0x001C27E8 File Offset: 0x001C09E8
		private static int? CompareStrings(object left, object right, bool ignoreCase)
		{
			string text = left as string;
			string text2 = right as string;
			if (text != null && text2 != null)
			{
				return new int?(string.Compare(text, text2, ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture));
			}
			return null;
		}

		// Token: 0x06007AAF RID: 31407 RVA: 0x001C2828 File Offset: 0x001C0A28
		private static int? CompareCompareable(object x, object y)
		{
			IComparable comparable = x as IComparable;
			IComparable comparable2 = y as IComparable;
			if (comparable != null && comparable2 != null)
			{
				try
				{
					return new int?(comparable.CompareTo(comparable2));
				}
				catch (ArgumentException)
				{
					return null;
				}
			}
			return null;
		}

		// Token: 0x06007AB0 RID: 31408 RVA: 0x001C2884 File Offset: 0x001C0A84
		private static int? CompareToStrings(object left, object right, bool ignoreCase)
		{
			string strA = Convert.ToString(left, CultureInfo.InvariantCulture);
			string strB = Convert.ToString(right, CultureInfo.InvariantCulture);
			return new int?(string.Compare(strA, strB, ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture));
		}

		// Token: 0x06007AB1 RID: 31409 RVA: 0x001C28BC File Offset: 0x001C0ABC
		protected override Cloneable CreateInstanceCore()
		{
			return new DefaultComparer();
		}

		// Token: 0x06007AB2 RID: 31410 RVA: 0x001C28C4 File Offset: 0x001C0AC4
		protected override void CloneCore(Cloneable source)
		{
			DefaultComparer defaultComparer = source as DefaultComparer;
			if (defaultComparer != null)
			{
				this.IgnoreCase = defaultComparer.IgnoreCase;
			}
		}

		// Token: 0x0400219A RID: 8602
		private bool ignoreCase;
	}
}
