using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x0200068D RID: 1677
	public abstract class AggregateValue
	{
		// Token: 0x06003CF0 RID: 15600 RVA: 0x000C48D7 File Offset: 0x000C2AD7
		static AggregateValue()
		{
			AggregateValue.ErrorAggregateValue = new ConstantValueAggregate(AggregateValue.AggregateError);
		}

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x06003CF1 RID: 15601 RVA: 0x000C48F2 File Offset: 0x000C2AF2
		// (set) Token: 0x06003CF2 RID: 15602 RVA: 0x000C48FA File Offset: 0x000C2AFA
		internal bool IgnoreNullValues { get; set; }

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x06003CF3 RID: 15603 RVA: 0x000C4903 File Offset: 0x000C2B03
		internal virtual AggregateError Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x000C490B File Offset: 0x000C2B0B
		internal bool IsError
		{
			get
			{
				return this.Error != null;
			}
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x000C491C File Offset: 0x000C2B1C
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Design choice.")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		public object GetValue()
		{
			if (this.Error != null)
			{
				return this.Error;
			}
			object valueOverride;
			try
			{
				valueOverride = this.GetValueOverride();
			}
			catch
			{
				this.error = AggregateValue.AggregateError;
				valueOverride = this.error;
			}
			return valueOverride;
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x000C4968 File Offset: 0x000C2B68
		internal void SetFormattedValue(string value)
		{
			this.formattedValue = value;
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x000C4974 File Offset: 0x000C2B74
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		internal void AccumulateCore(object item)
		{
			if (this.error == null)
			{
				try
				{
					if (!this.IgnoreNullValues || item != null)
					{
						this.AccumulateOverride(item);
					}
				}
				catch
				{
					this.error = AggregateValue.AggregateError;
				}
			}
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x000C49BC File Offset: 0x000C2BBC
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		internal void MergeCore(AggregateValue childAggregate)
		{
			try
			{
				if (this.error == null)
				{
					if (childAggregate.error != null)
					{
						this.error = childAggregate.error;
					}
					else
					{
						this.MergeOverride(childAggregate);
					}
				}
			}
			catch
			{
				this.error = AggregateValue.AggregateError;
			}
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x000C4A10 File Offset: 0x000C2C10
		internal void RaiseError()
		{
			this.error = AggregateValue.AggregateError;
		}

		// Token: 0x06003CFA RID: 15610
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Design choice.")]
		protected abstract object GetValueOverride();

		// Token: 0x06003CFB RID: 15611
		protected abstract void AccumulateOverride(object value);

		// Token: 0x06003CFC RID: 15612
		protected abstract void MergeOverride(AggregateValue childAggregate);

		// Token: 0x06003CFD RID: 15613 RVA: 0x000C4A1D File Offset: 0x000C2C1D
		public override string ToString()
		{
			return this.formattedValue ?? Convert.ToString(this.GetValue(), CultureInfo.InvariantCulture);
		}

		// Token: 0x04001051 RID: 4177
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Error is immutable.")]
		public static readonly AggregateValue ErrorAggregateValue;

		// Token: 0x04001052 RID: 4178
		internal static readonly AggregateError AggregateError = new AggregateError();

		// Token: 0x04001053 RID: 4179
		private AggregateError error;

		// Token: 0x04001054 RID: 4180
		private string formattedValue;
	}
}
