using System;
using System.Collections;
using System.Globalization;
using System.IO;

namespace NCrontab
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public sealed class CrontabField : ICrontabField
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021B4 File Offset: 0x000003B4
		public static CrontabField Parse(CrontabFieldKind kind, string expression)
		{
			return CrontabField.TryParse<CrontabField>(kind, expression, (CrontabField v) => v, delegate(ExceptionProvider e)
			{
				throw e();
			});
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002208 File Offset: 0x00000408
		public static CrontabField TryParse(CrontabFieldKind kind, string expression)
		{
			return CrontabField.TryParse<CrontabField>(kind, expression, (CrontabField v) => v, (ExceptionProvider _) => null);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000225C File Offset: 0x0000045C
		public static T TryParse<T>(CrontabFieldKind kind, string expression, Func<CrontabField, T> valueSelector, Func<ExceptionProvider, T> errorSelector)
		{
			CrontabField crontabField = new CrontabField(CrontabFieldImpl.FromKind(kind));
			ExceptionProvider exceptionProvider = crontabField._impl.TryParse<ExceptionProvider>(expression, new CrontabFieldAccumulator<ExceptionProvider>(crontabField.Accumulate<ExceptionProvider>), null, (ExceptionProvider e) => e);
			if (exceptionProvider != null)
			{
				return errorSelector.Invoke(exceptionProvider);
			}
			return valueSelector.Invoke(crontabField);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C0 File Offset: 0x000004C0
		public static CrontabField Seconds(string expression)
		{
			return CrontabField.Parse(CrontabFieldKind.Second, expression);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022C9 File Offset: 0x000004C9
		public static CrontabField Minutes(string expression)
		{
			return CrontabField.Parse(CrontabFieldKind.Minute, expression);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022D2 File Offset: 0x000004D2
		public static CrontabField Hours(string expression)
		{
			return CrontabField.Parse(CrontabFieldKind.Hour, expression);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022DB File Offset: 0x000004DB
		public static CrontabField Days(string expression)
		{
			return CrontabField.Parse(CrontabFieldKind.Day, expression);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022E4 File Offset: 0x000004E4
		public static CrontabField Months(string expression)
		{
			return CrontabField.Parse(CrontabFieldKind.Month, expression);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022ED File Offset: 0x000004ED
		public static CrontabField DaysOfWeek(string expression)
		{
			return CrontabField.Parse(CrontabFieldKind.DayOfWeek, expression);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022F8 File Offset: 0x000004F8
		private CrontabField(CrontabFieldImpl impl)
		{
			if (impl == null)
			{
				throw new ArgumentNullException("impl");
			}
			this._impl = impl;
			this._bits = new BitArray(impl.ValueCount);
			this._bits.SetAll(false);
			this._minValueSet = int.MaxValue;
			this._maxValueSet = -1;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000234F File Offset: 0x0000054F
		public int GetFirst()
		{
			if (this._minValueSet >= 2147483647)
			{
				return -1;
			}
			return this._minValueSet;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002368 File Offset: 0x00000568
		public int Next(int start)
		{
			if (start < this._minValueSet)
			{
				return this._minValueSet;
			}
			int num = this.ValueToIndex(start);
			int num2 = this.ValueToIndex(this._maxValueSet);
			for (int i = num; i <= num2; i++)
			{
				if (this._bits[i])
				{
					return this.IndexToValue(i);
				}
			}
			return -1;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023BB File Offset: 0x000005BB
		private int IndexToValue(int index)
		{
			return index + this._impl.MinValue;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023CA File Offset: 0x000005CA
		private int ValueToIndex(int value)
		{
			return value - this._impl.MinValue;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023D9 File Offset: 0x000005D9
		public bool Contains(int value)
		{
			return this._bits[this.ValueToIndex(value)];
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023F0 File Offset: 0x000005F0
		private T Accumulate<T>(int start, int end, int interval, T success, Func<ExceptionProvider, T> errorSelector)
		{
			int minValue = this._impl.MinValue;
			int maxValue = this._impl.MaxValue;
			if (start == end)
			{
				if (start < 0)
				{
					if (interval <= 1)
					{
						this._minValueSet = minValue;
						this._maxValueSet = maxValue;
						this._bits.SetAll(true);
						return success;
					}
					start = minValue;
					end = maxValue;
				}
				else
				{
					if (start < minValue)
					{
						return this.OnValueBelowMinError<T>(start, errorSelector);
					}
					if (start > maxValue)
					{
						return this.OnValueAboveMaxError<T>(start, errorSelector);
					}
				}
			}
			else
			{
				if (start > end)
				{
					end ^= start;
					start ^= end;
					end ^= start;
				}
				if (start < 0)
				{
					start = minValue;
				}
				else if (start < minValue)
				{
					return this.OnValueBelowMinError<T>(start, errorSelector);
				}
				if (end < 0)
				{
					end = maxValue;
				}
				else if (end > maxValue)
				{
					return this.OnValueAboveMaxError<T>(end, errorSelector);
				}
			}
			if (interval < 1)
			{
				interval = 1;
			}
			int i;
			for (i = start - minValue; i <= end - minValue; i += interval)
			{
				this._bits[i] = true;
			}
			if (this._minValueSet > start)
			{
				this._minValueSet = start;
			}
			i += minValue - interval;
			if (this._maxValueSet < i)
			{
				this._maxValueSet = i;
			}
			return success;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024F0 File Offset: 0x000006F0
		private T OnValueAboveMaxError<T>(int value, Func<ExceptionProvider, T> errorSelector)
		{
			return errorSelector.Invoke(() => new CrontabException(string.Format("{0} is higher than the maximum allowable value for the [{1}] field. ", value, this._impl.Kind) + string.Format("Value must be between {0} and {1} (all inclusive).", this._impl.MinValue, this._impl.MaxValue)));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002524 File Offset: 0x00000724
		private T OnValueBelowMinError<T>(int value, Func<ExceptionProvider, T> errorSelector)
		{
			return errorSelector.Invoke(() => new CrontabException(string.Format("{0} is lower than the minimum allowable value for the [{1}] field. ", value, this._impl.Kind) + string.Format("Value must be between {0} and {1} (all inclusive).", this._impl.MinValue, this._impl.MaxValue)));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002557 File Offset: 0x00000757
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002560 File Offset: 0x00000760
		public string ToString(string format)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			if (!(format == "G") && format != null)
			{
				if (!(format == "N"))
				{
					throw new FormatException();
				}
				this.Format(stringWriter);
			}
			else
			{
				this.Format(stringWriter, true);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025B6 File Offset: 0x000007B6
		public void Format(TextWriter writer)
		{
			this.Format(writer, false);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025C0 File Offset: 0x000007C0
		public void Format(TextWriter writer, bool noNames)
		{
			this._impl.Format(this, writer, noNames);
		}

		// Token: 0x04000003 RID: 3
		private readonly BitArray _bits;

		// Token: 0x04000004 RID: 4
		private int _minValueSet;

		// Token: 0x04000005 RID: 5
		private int _maxValueSet;

		// Token: 0x04000006 RID: 6
		private readonly CrontabFieldImpl _impl;
	}
}
