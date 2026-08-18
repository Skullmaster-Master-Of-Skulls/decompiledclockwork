using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;
using Telerik.Web.UI.PivotGrid.Core.Fields;
using Telerik.Web.UI.PivotGrid.Queryable.Aggregates;
using Telerik.Web.UI.PivotGrid.Queryable.Descriptions;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x0200072C RID: 1836
	[DataContract]
	public abstract class QueryablePropertyAggregateDescriptionBase : QueryableAggregateDescription, IInitializeDescription, IAggregateFunctionHost, IDataFieldDescription
	{
		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x000CCCAA File Offset: 0x000CAEAA
		// (set) Token: 0x06004126 RID: 16678 RVA: 0x000CCCB2 File Offset: 0x000CAEB2
		[DataMember]
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
			set
			{
				if (this.propertyName != value)
				{
					this.propertyName = value;
					base.OnPropertyChanged("PropertyName");
					base.OnPropertyChanged("DisplayName");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x06004127 RID: 16679 RVA: 0x000CCCEA File Offset: 0x000CAEEA
		// (set) Token: 0x06004128 RID: 16680 RVA: 0x000CCCF2 File Offset: 0x000CAEF2
		[DataMember]
		public QueryableAggregateFunction AggregateFunction
		{
			get
			{
				return this.aggregateFunction;
			}
			set
			{
				if (this.aggregateFunction != value)
				{
					this.aggregateFunction = value;
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
					base.OnPropertyChanged("AggregateFunction");
					base.OnPropertyChanged("DisplayName");
				}
			}
		}

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06004129 RID: 16681 RVA: 0x000CCD25 File Offset: 0x000CAF25
		// (set) Token: 0x0600412A RID: 16682 RVA: 0x000CCD2D File Offset: 0x000CAF2D
		[DataMember]
		public bool IgnoreNullValues
		{
			get
			{
				return this.ignoreNullValues;
			}
			set
			{
				if (this.ignoreNullValues != value)
				{
					this.ignoreNullValues = value;
					base.OnPropertyChanged("IgnoreNullValues");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x0600412B RID: 16683 RVA: 0x000CCD55 File Offset: 0x000CAF55
		protected Type DataType
		{
			get
			{
				if (this.FieldInfo != null)
				{
					return this.FieldInfo.DataType;
				}
				return null;
			}
		}

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x0600412C RID: 16684 RVA: 0x000CCEF4 File Offset: 0x000CB0F4
		protected virtual IEnumerable<object> SupportedAggregateFunctions
		{
			get
			{
				if (FieldInfoHelper.IsNumericType(this.FieldInfo.DataType))
				{
					yield return QueryableAggregateFunction.Sum;
					yield return QueryableAggregateFunction.Count;
					yield return QueryableAggregateFunction.Average;
					yield return QueryableAggregateFunction.Min;
					yield return QueryableAggregateFunction.Max;
				}
				else
				{
					yield return QueryableAggregateFunction.Count;
				}
				yield break;
			}
		}

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x0600412D RID: 16685 RVA: 0x000CCF11 File Offset: 0x000CB111
		// (set) Token: 0x0600412E RID: 16686 RVA: 0x000CCF19 File Offset: 0x000CB119
		internal PropertyFieldInfo FieldInfo { get; set; }

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x0600412F RID: 16687 RVA: 0x000CCF22 File Offset: 0x000CB122
		protected override string AggregateMethodName
		{
			get
			{
				return this.AggregateFunction.ToString();
			}
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x000CCF34 File Offset: 0x000CB134
		internal override IPivotFieldInfo GetFieldInfo()
		{
			return this.FieldInfo;
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x000CCF3C File Offset: 0x000CB13C
		internal override AggregateValue CreateAggregate()
		{
			switch (this.AggregateFunction)
			{
			case QueryableAggregateFunction.Sum:
				return new SumAggregate
				{
					IgnoreNullValues = this.IgnoreNullValues
				};
			case QueryableAggregateFunction.Count:
				return new QueryableCountAggregate
				{
					IgnoreNullValues = this.IgnoreNullValues
				};
			case QueryableAggregateFunction.Average:
				return new QueryableAverageAggregate
				{
					IgnoreNullValues = this.IgnoreNullValues
				};
			case QueryableAggregateFunction.Max:
				return new MaxAggregate
				{
					IgnoreNullValues = this.IgnoreNullValues
				};
			case QueryableAggregateFunction.Min:
				return new MinAggregate
				{
					IgnoreNullValues = this.IgnoreNullValues
				};
			default:
				throw new InvalidOperationException("Unrecognized aggregate function type.");
			}
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x000CCFE0 File Offset: 0x000CB1E0
		protected internal override Expression CreateAggregateExpression(Expression enumerableExpression, string aggregatedValueName)
		{
			if (enumerableExpression == null)
			{
				throw new ArgumentNullException("enumerableExpression");
			}
			Type itemType = QueryablePropertyAggregateDescriptionBase.ExtractItemTypeFromEnumerableType(enumerableExpression.Type);
			LambdaExpression selectLambdaExpression = this.CreateSelectLambdaForAggregateExpression(itemType, aggregatedValueName);
			return this.CreateMethodCallExpression(enumerableExpression, selectLambdaExpression, aggregatedValueName);
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x000CD01B File Offset: 0x000CB21B
		public override string GetUniqueName()
		{
			return this.PropertyName;
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x000CD024 File Offset: 0x000CB224
		protected internal override Expression CreateAggregateValueExpression(ParameterExpression itemExpression)
		{
			if (itemExpression == null)
			{
				throw new ArgumentNullException("itemExpression");
			}
			if (string.IsNullOrEmpty(this.PropertyName))
			{
				return QueryablePropertyAggregateDescriptionBase.GetErrorValueExpression();
			}
			return QueryableExpressionHelper.MakeMemberAccess(itemExpression, this.PropertyName, QueryablePropertyAggregateDescriptionBase.GetErrorValueExpression());
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x000CD068 File Offset: 0x000CB268
		protected override void CloneCore(Cloneable source)
		{
			QueryablePropertyAggregateDescriptionBase queryablePropertyAggregateDescriptionBase = source as QueryablePropertyAggregateDescriptionBase;
			if (queryablePropertyAggregateDescriptionBase != null)
			{
				this.IgnoreNullValues = queryablePropertyAggregateDescriptionBase.IgnoreNullValues;
				this.AggregateFunction = queryablePropertyAggregateDescriptionBase.aggregateFunction;
				this.PropertyName = queryablePropertyAggregateDescriptionBase.PropertyName;
				base.StringFormat = queryablePropertyAggregateDescriptionBase.StringFormat;
				this.FieldInfo = queryablePropertyAggregateDescriptionBase.FieldInfo;
			}
			base.CloneCore(source);
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x000CD0C2 File Offset: 0x000CB2C2
		protected override string GetDisplayName()
		{
			if (string.IsNullOrEmpty(base.CustomName))
			{
				return this.AggregateFunction.ToString() + " of " + this.PropertyName;
			}
			return base.CustomName;
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x000CD0F8 File Offset: 0x000CB2F8
		protected override string GenerateFunctionName()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}", new object[]
			{
				this.AggregateMethodName,
				this.PropertyName,
				this.GetHashCode()
			});
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x000CD13C File Offset: 0x000CB33C
		private static Type ExtractItemTypeFromEnumerableType(Type type)
		{
			Type type2 = type.FindGenericType(typeof(IEnumerable<>));
			if (type2 == null)
			{
				throw new ArgumentException("Provided type is not IEnumerable<>", "type");
			}
			return type2.GetGenericArguments().First<Type>();
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x000CD17E File Offset: 0x000CB37E
		private static Expression GetErrorValueExpression()
		{
			return Expression.Constant(0);
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x000CD18C File Offset: 0x000CB38C
		private Expression CreateMethodCallExpression(Expression enumerableExpression, LambdaExpression selectLambdaExpression, string aggregatedValueName)
		{
			Type type = QueryablePropertyAggregateDescriptionBase.ExtractItemTypeFromEnumerableType(enumerableExpression.Type);
			if (this.AggregateFunction == QueryableAggregateFunction.Average)
			{
				Type typeFromHandle = typeof(QueryableAverageResult);
				Expression expression;
				if (!this.IgnoreNullValues)
				{
					expression = Expression.Call(this.ExtensionMethodsType, "Count", new Type[]
					{
						type
					}, new Expression[]
					{
						enumerableExpression
					});
				}
				else
				{
					LambdaExpression lambdaExpression = this.CreateSelectLambdaForAggregateExpression(type, aggregatedValueName, QueryableAggregateFunction.Count);
					expression = Expression.Call(this.ExtensionMethodsType, "Count", new Type[]
					{
						type
					}, new Expression[]
					{
						enumerableExpression,
						lambdaExpression
					});
				}
				MethodCallExpression expression2 = Expression.Call(this.ExtensionMethodsType, "Sum", new Type[]
				{
					type
				}, new Expression[]
				{
					enumerableExpression,
					selectLambdaExpression
				});
				MemberInfo member = typeFromHandle.GetMember("Count")[0];
				MemberInfo member2 = typeFromHandle.GetMember("Sum")[0];
				MemberAssignment memberAssignment = Expression.Bind(member, Expression.Convert(expression, typeof(int)));
				MemberAssignment memberAssignment2 = Expression.Bind(member2, Expression.Convert(expression2, typeof(double)));
				return Expression.MemberInit(Expression.New(typeFromHandle), new MemberBinding[]
				{
					memberAssignment,
					memberAssignment2
				});
			}
			if (selectLambdaExpression == null)
			{
				return this.CreateMethodCallExpressionForParameterLessAggregate(type, enumerableExpression);
			}
			return this.CreateMethodCallExpressionForAggregateWithParameter(type, enumerableExpression, selectLambdaExpression);
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x000CD2F4 File Offset: 0x000CB4F4
		private Expression CreateMethodCallExpressionForAggregateWithParameter(Type itemType, Expression enumerableExpression, LambdaExpression selectLambdaExpression)
		{
			return Expression.Call(this.ExtensionMethodsType, this.AggregateMethodName, new Type[]
			{
				itemType
			}, new Expression[]
			{
				enumerableExpression,
				selectLambdaExpression
			});
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x000CD330 File Offset: 0x000CB530
		private Expression CreateMethodCallExpressionForParameterLessAggregate(Type itemType, Expression enumerableExpression)
		{
			return Expression.Call(this.ExtensionMethodsType, this.AggregateMethodName, new Type[]
			{
				itemType
			}, new Expression[]
			{
				enumerableExpression
			});
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x000CD368 File Offset: 0x000CB568
		private LambdaExpression CreateSelectLambdaForAggregateExpression(Type itemType, string aggregatedValueName)
		{
			return this.CreateSelectLambdaForAggregateExpression(itemType, aggregatedValueName, this.AggregateFunction);
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x000CD378 File Offset: 0x000CB578
		private LambdaExpression CreateSelectLambdaForAggregateExpression(Type itemType, string aggregatedValueName, QueryableAggregateFunction function)
		{
			if (function != QueryableAggregateFunction.Count)
			{
				ParameterExpression parameterExpression = Expression.Parameter(itemType, "e");
				MemberExpression body = Expression.Property(parameterExpression, aggregatedValueName);
				return Expression.Lambda(body, new ParameterExpression[]
				{
					parameterExpression
				});
			}
			if (this.IgnoreNullValues)
			{
				ParameterExpression parameterExpression2 = Expression.Parameter(itemType, "e");
				MemberExpression left = Expression.Property(parameterExpression2, aggregatedValueName);
				BinaryExpression body2 = Expression.NotEqual(left, Expression.Constant(null));
				return Expression.Lambda(body2, new ParameterExpression[]
				{
					parameterExpression2
				});
			}
			return null;
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x000CD3FD File Offset: 0x000CB5FD
		Type IDataFieldDescription.GetDataType()
		{
			return this.DataType;
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06004140 RID: 16704 RVA: 0x000CD405 File Offset: 0x000CB605
		bool IInitializeDescription.Initialized
		{
			get
			{
				return this.FieldInfo != null;
			}
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x000CD413 File Offset: 0x000CB613
		void IInitializeDescription.Initialize(IDataProvider provider)
		{
			if (provider == null)
			{
				return;
			}
			this.FieldInfo = (provider.FieldInfos.GetFieldDescriptionByMember(this.PropertyName) as PropertyFieldInfo);
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x000CD438 File Offset: 0x000CB638
		private string GetStringFormatForAggregateFunction(Type dataType)
		{
			Precision precision = PrecisionHelpers.GetPrecision(dataType);
			switch (this.AggregateFunction)
			{
			case QueryableAggregateFunction.Sum:
			case QueryableAggregateFunction.Max:
			case QueryableAggregateFunction.Min:
				return QueryablePropertyAggregateDescriptionBase.GetStringFormatForNumeric(precision);
			case QueryableAggregateFunction.Count:
				return QueryablePropertyAggregateDescriptionBase.GetStringFormatForCount();
			case QueryableAggregateFunction.Average:
				return QueryablePropertyAggregateDescriptionBase.GetStringFormatForAverage(precision);
			default:
				return null;
			}
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x000CD484 File Offset: 0x000CB684
		private static string GetStringFormatForAverage(Precision precision)
		{
			switch (precision)
			{
			case Precision.Int64:
			case Precision.Decimal:
			case Precision.Double:
				return "0.00";
			default:
				return null;
			}
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x000CD4B0 File Offset: 0x000CB6B0
		private static string GetStringFormatForCount()
		{
			return "G";
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x000CD4B8 File Offset: 0x000CB6B8
		private static string GetStringFormatForNumeric(Precision precision)
		{
			switch (precision)
			{
			case Precision.Int64:
				return "G";
			case Precision.Decimal:
				return "0.00";
			case Precision.Double:
				return "0.00";
			default:
				return null;
			}
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x000CD4F0 File Offset: 0x000CB6F0
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Design choice.")]
		internal override string GetEffectiveFormat()
		{
			Type type = (this.FieldInfo == null) ? null : PivotTypeExtensions.GetNonNullableType(this.FieldInfo.DataType);
			if (base.StringFormatSelector != null)
			{
				return base.StringFormatSelector.SelectStringFormat();
			}
			string text = base.StringFormat;
			if (string.IsNullOrEmpty(text))
			{
				text = this.GetStringFormatForAggregateFunction(type);
			}
			if (this.AggregateFunction == QueryableAggregateFunction.Count)
			{
				text = "G";
			}
			if (base.TotalFormat != null && type != null)
			{
				text = base.TotalFormat.GetStringFormat(type, text);
			}
			return text;
		}

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06004147 RID: 16711 RVA: 0x000CD573 File Offset: 0x000CB773
		// (set) Token: 0x06004148 RID: 16712 RVA: 0x000CD580 File Offset: 0x000CB780
		object IAggregateFunctionHost.AggregateFunction
		{
			get
			{
				return this.AggregateFunction;
			}
			set
			{
				this.AggregateFunction = (QueryableAggregateFunction)value;
			}
		}

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x06004149 RID: 16713 RVA: 0x000CD58E File Offset: 0x000CB78E
		IEnumerable<object> IAggregateFunctionHost.SupportedAggregateFunctions
		{
			get
			{
				return this.SupportedAggregateFunctions;
			}
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x000CD596 File Offset: 0x000CB796
		internal override RequiredField GetRequiredField()
		{
			if (this.aggregateFunction == QueryableAggregateFunction.Sum)
			{
				return RequiredField.ForProperty(this.propertyName);
			}
			return RequiredField.ForProperty(this.propertyName, this.aggregateFunction);
		}

		// Token: 0x04001146 RID: 4422
		private string propertyName;

		// Token: 0x04001147 RID: 4423
		private QueryableAggregateFunction aggregateFunction;

		// Token: 0x04001148 RID: 4424
		private bool ignoreNullValues;
	}
}
