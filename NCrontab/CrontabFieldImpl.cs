using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace NCrontab
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	internal sealed class CrontabFieldImpl : IObjectReference
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000025D0 File Offset: 0x000007D0
		public static CrontabFieldImpl FromKind(CrontabFieldKind kind)
		{
			if (!Enum.IsDefined(typeof(CrontabFieldKind), kind))
			{
				string str = string.Join(", ", Enum.GetNames(typeof(CrontabFieldKind)));
				throw new ArgumentException("Invalid crontab field kind. Valid values are " + str + ".", "kind");
			}
			return CrontabFieldImpl.FieldByKind[(int)kind];
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002630 File Offset: 0x00000830
		private CrontabFieldImpl(CrontabFieldKind kind, int minValue, int maxValue, string[] names)
		{
			this.Kind = kind;
			this.MinValue = minValue;
			this.MaxValue = maxValue;
			this._names = names;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002655 File Offset: 0x00000855
		public CrontabFieldKind Kind { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000265D File Offset: 0x0000085D
		public int MinValue { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002665 File Offset: 0x00000865
		public int MaxValue { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000266D File Offset: 0x0000086D
		public int ValueCount
		{
			get
			{
				return this.MaxValue - this.MinValue + 1;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000267E File Offset: 0x0000087E
		public void Format(ICrontabField field, TextWriter writer)
		{
			this.Format(field, writer, false);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000268C File Offset: 0x0000088C
		public void Format(ICrontabField field, TextWriter writer, bool noNames)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = field.GetFirst();
			int num2 = 0;
			while (num != -1)
			{
				int num3 = num;
				int num4;
				do
				{
					num4 = num;
					num = field.Next(num4 + 1);
				}
				while (num - num4 == 1);
				if (num2 == 0 && num3 == this.MinValue && num4 == this.MaxValue)
				{
					writer.Write('*');
					return;
				}
				if (num2 > 0)
				{
					writer.Write(',');
				}
				if (num3 == num4)
				{
					this.FormatValue(num3, writer, noNames);
				}
				else
				{
					this.FormatValue(num3, writer, noNames);
					writer.Write('-');
					this.FormatValue(num4, writer, noNames);
				}
				num2++;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002730 File Offset: 0x00000930
		private void FormatValue(int value, TextWriter writer, bool noNames)
		{
			if (!noNames && this._names != null)
			{
				int num = value - this.MinValue;
				writer.Write(this._names[num]);
				return;
			}
			if (value >= 0 && value < 100)
			{
				CrontabFieldImpl.FastFormatNumericValue(value, writer);
				return;
			}
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002783 File Offset: 0x00000983
		private static void FastFormatNumericValue(int value, TextWriter writer)
		{
			if (value >= 10)
			{
				writer.Write((char)(48 + value / 10));
				writer.Write((char)(48 + value % 10));
				return;
			}
			writer.Write((char)(48 + value));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027B2 File Offset: 0x000009B2
		public void Parse(string str, CrontabFieldAccumulator<ExceptionProvider> acc)
		{
			this.TryParse<ExceptionProvider>(str, acc, null, delegate(ExceptionProvider ep)
			{
				throw ep();
			});
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027E0 File Offset: 0x000009E0
		public T TryParse<T>(string str, CrontabFieldAccumulator<T> acc, T success, Func<ExceptionProvider, T> errorSelector)
		{
			if (acc == null)
			{
				throw new ArgumentNullException("acc");
			}
			if (string.IsNullOrEmpty(str))
			{
				return success;
			}
			T result;
			try
			{
				result = this.InternalParse<T>(str, acc, success, errorSelector);
			}
			catch (FormatException innerException)
			{
				result = this.OnParseException<T>(innerException, str, errorSelector);
			}
			catch (CrontabException innerException2)
			{
				result = this.OnParseException<T>(innerException2, str, errorSelector);
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002850 File Offset: 0x00000A50
		private T OnParseException<T>(Exception innerException, string str, Func<ExceptionProvider, T> errorSelector)
		{
			return errorSelector.Invoke(() => new CrontabException(string.Format("'{0}' is not a valid [{1}] crontab field expression.", str, this.Kind), innerException));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000288C File Offset: 0x00000A8C
		private T InternalParse<T>(string str, CrontabFieldAccumulator<T> acc, T success, Func<ExceptionProvider, T> errorSelector)
		{
			if (str.Length == 0)
			{
				return errorSelector.Invoke(() => new CrontabException("A crontab field value cannot be empty."));
			}
			if (str.IndexOf(',') > 0)
			{
				T t = success;
				using (IEnumerator<string> enumerator = ((IEnumerable<string>)str.Split(StringSeparatorStock.Comma)).GetEnumerator())
				{
					while (enumerator.MoveNext() && t == null)
					{
						t = this.InternalParse<T>(enumerator.Current, acc, success, errorSelector);
					}
				}
				return t;
			}
			int? num = null;
			int num2 = str.IndexOf('/');
			if (num2 > 0)
			{
				num = new int?(int.Parse(str.Substring(num2 + 1), CultureInfo.InvariantCulture));
				str = str.Substring(0, num2);
			}
			if (str.Length == 1 && str[0] == '*')
			{
				return acc(-1, -1, num ?? 1, success, errorSelector);
			}
			int num3 = str.IndexOf('-');
			if (num3 > 0)
			{
				int start = this.ParseValue(str.Substring(0, num3));
				int end = this.ParseValue(str.Substring(num3 + 1));
				return acc(start, end, num ?? 1, success, errorSelector);
			}
			int num4 = this.ParseValue(str);
			if (num == null)
			{
				return acc(num4, num4, 1, success, errorSelector);
			}
			return acc(num4, this.MaxValue, num.Value, success, errorSelector);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A28 File Offset: 0x00000C28
		private int ParseValue(string str)
		{
			if (str.Length == 0)
			{
				throw new CrontabException("A crontab field value cannot be empty.");
			}
			char c = str[0];
			if (c >= '0' && c <= '9')
			{
				return int.Parse(str, CultureInfo.InvariantCulture);
			}
			if (this._names == null)
			{
				throw new CrontabException(string.Format("'{0}' is not a valid [{3}] crontab field value. It must be a numeric value between {1} and {2} (all inclusive).", new object[]
				{
					str,
					this.MinValue.ToString(),
					this.MaxValue.ToString(),
					this.Kind.ToString()
				}));
			}
			for (int i = 0; i < this._names.Length; i++)
			{
				if (CrontabFieldImpl.Comparer.IsPrefix(this._names[i], str, CompareOptions.IgnoreCase))
				{
					return i + this.MinValue;
				}
			}
			string text = string.Join(", ", this._names);
			throw new CrontabException(string.Concat(new string[]
			{
				"'",
				str,
				"' is not a known value name. Use one of the following: ",
				text,
				"."
			}));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B3A File Offset: 0x00000D3A
		object IObjectReference.GetRealObject(StreamingContext context)
		{
			return CrontabFieldImpl.FromKind(this.Kind);
		}

		// Token: 0x04000007 RID: 7
		public static readonly CrontabFieldImpl Second = new CrontabFieldImpl(CrontabFieldKind.Second, 0, 59, null);

		// Token: 0x04000008 RID: 8
		public static readonly CrontabFieldImpl Minute = new CrontabFieldImpl(CrontabFieldKind.Minute, 0, 59, null);

		// Token: 0x04000009 RID: 9
		public static readonly CrontabFieldImpl Hour = new CrontabFieldImpl(CrontabFieldKind.Hour, 0, 23, null);

		// Token: 0x0400000A RID: 10
		public static readonly CrontabFieldImpl Day = new CrontabFieldImpl(CrontabFieldKind.Day, 1, 31, null);

		// Token: 0x0400000B RID: 11
		public static readonly CrontabFieldImpl Month = new CrontabFieldImpl(CrontabFieldKind.Month, 1, 12, new string[]
		{
			"January",
			"February",
			"March",
			"April",
			"May",
			"June",
			"July",
			"August",
			"September",
			"October",
			"November",
			"December"
		});

		// Token: 0x0400000C RID: 12
		public static readonly CrontabFieldImpl DayOfWeek = new CrontabFieldImpl(CrontabFieldKind.DayOfWeek, 0, 6, new string[]
		{
			"Sunday",
			"Monday",
			"Tuesday",
			"Wednesday",
			"Thursday",
			"Friday",
			"Saturday"
		});

		// Token: 0x0400000D RID: 13
		private static readonly CrontabFieldImpl[] FieldByKind = new CrontabFieldImpl[]
		{
			CrontabFieldImpl.Second,
			CrontabFieldImpl.Minute,
			CrontabFieldImpl.Hour,
			CrontabFieldImpl.Day,
			CrontabFieldImpl.Month,
			CrontabFieldImpl.DayOfWeek
		};

		// Token: 0x0400000E RID: 14
		private static readonly CompareInfo Comparer = CultureInfo.InvariantCulture.CompareInfo;

		// Token: 0x0400000F RID: 15
		private readonly string[] _names;
	}
}
