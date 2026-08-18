using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C47 RID: 3143
	public sealed class AggregateFunctionConverter : TypeConverter
	{
		// Token: 0x060076E0 RID: 30432 RVA: 0x001B9C0C File Offset: 0x001B7E0C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060076E1 RID: 30433 RVA: 0x001B9C2C File Offset: 0x001B7E2C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			string key;
			if (text != null && (key = text) != null)
			{
				if (<PrivateImplementationDetails>{FD978F7E-3DA5-4815-803F-07E58A83CEFA}.$$method0x6007581-1 == null)
				{
					<PrivateImplementationDetails>{FD978F7E-3DA5-4815-803F-07E58A83CEFA}.$$method0x6007581-1 = new Dictionary<string, int>(10)
					{
						{
							"Sum",
							0
						},
						{
							"Count",
							1
						},
						{
							"Average",
							2
						},
						{
							"Min",
							3
						},
						{
							"Max",
							4
						},
						{
							"Product",
							5
						},
						{
							"StdDev",
							6
						},
						{
							"StdDevP",
							7
						},
						{
							"Var",
							8
						},
						{
							"VarP",
							9
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{FD978F7E-3DA5-4815-803F-07E58A83CEFA}.$$method0x6007581-1.TryGetValue(key, out num))
				{
					switch (num)
					{
					case 0:
						return AggregateFunctions.Sum;
					case 1:
						return AggregateFunctions.Count;
					case 2:
						return AggregateFunctions.Average;
					case 3:
						return AggregateFunctions.Min;
					case 4:
						return AggregateFunctions.Max;
					case 5:
						return AggregateFunctions.Product;
					case 6:
						return AggregateFunctions.StdDev;
					case 7:
						return AggregateFunctions.StdDevP;
					case 8:
						return AggregateFunctions.Var;
					case 9:
						return AggregateFunctions.VarP;
					}
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060076E2 RID: 30434 RVA: 0x001B9D67 File Offset: 0x001B7F67
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060076E3 RID: 30435 RVA: 0x001B9D88 File Offset: 0x001B7F88
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is SumAggregateFunction)
			{
				return "Sum";
			}
			if (value is CountAggregateFunction)
			{
				return "Count";
			}
			if (value is AverageAggregateFunction)
			{
				return "Average";
			}
			if (value is MinAggregateFunction)
			{
				return "Min";
			}
			if (value is MaxAggregateFunction)
			{
				return "Max";
			}
			if (value is ProductAggregateFunction)
			{
				return "Product";
			}
			if (value is StdDevAggregateFunction)
			{
				return "StdDev";
			}
			if (value is StdDevPAggregateFunction)
			{
				return "StdDevP";
			}
			if (value is VarAggregateFunction)
			{
				return "Var";
			}
			if (value is VarPAggregateFunction)
			{
				return "VarP";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
