using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.Web.Data
{
	// Token: 0x02001B9F RID: 7071
	public class AggregateResult : INotifyPropertyChanged
	{
		// Token: 0x060111CD RID: 70093 RVA: 0x003C64E8 File Offset: 0x003C46E8
		public AggregateResult(object value, int count, AggregateFunction function)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			this.aggregateValue = value;
			this.itemCount = count;
			this.function = function;
		}

		// Token: 0x060111CE RID: 70094 RVA: 0x003C6513 File Offset: 0x003C4713
		public AggregateResult(AggregateFunction function) : this(null, function)
		{
		}

		// Token: 0x060111CF RID: 70095 RVA: 0x003C651D File Offset: 0x003C471D
		public AggregateResult(object value, AggregateFunction function) : this(value, 0, function)
		{
		}

		// Token: 0x140001E6 RID: 486
		// (add) Token: 0x060111D0 RID: 70096 RVA: 0x003C6528 File Offset: 0x003C4728
		// (remove) Token: 0x060111D1 RID: 70097 RVA: 0x003C6560 File Offset: 0x003C4760
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x1700538A RID: 21386
		// (get) Token: 0x060111D2 RID: 70098 RVA: 0x003C6595 File Offset: 0x003C4795
		// (set) Token: 0x060111D3 RID: 70099 RVA: 0x003C659D File Offset: 0x003C479D
		public object Value
		{
			get
			{
				return this.aggregateValue;
			}
			internal set
			{
				this.aggregateValue = value;
				this.OnPropertyChanged("Value");
				this.OnPropertyChanged("FormattedValue");
			}
		}

		// Token: 0x1700538B RID: 21387
		// (get) Token: 0x060111D4 RID: 70100 RVA: 0x003C65BC File Offset: 0x003C47BC
		public object FormattedValue
		{
			get
			{
				if (string.IsNullOrEmpty(this.function.ResultFormatString))
				{
					return this.aggregateValue;
				}
				return string.Format(CultureInfo.CurrentCulture, this.function.ResultFormatString, new object[]
				{
					this.aggregateValue
				});
			}
		}

		// Token: 0x1700538C RID: 21388
		// (get) Token: 0x060111D5 RID: 70101 RVA: 0x003C6608 File Offset: 0x003C4808
		// (set) Token: 0x060111D6 RID: 70102 RVA: 0x003C6610 File Offset: 0x003C4810
		public int ItemCount
		{
			get
			{
				return this.itemCount;
			}
			set
			{
				this.itemCount = value;
			}
		}

		// Token: 0x1700538D RID: 21389
		// (get) Token: 0x060111D7 RID: 70103 RVA: 0x003C6619 File Offset: 0x003C4819
		public string Caption
		{
			get
			{
				return this.function.Caption;
			}
		}

		// Token: 0x1700538E RID: 21390
		// (get) Token: 0x060111D8 RID: 70104 RVA: 0x003C6626 File Offset: 0x003C4826
		public string FunctionName
		{
			get
			{
				return this.function.FunctionName;
			}
		}

		// Token: 0x060111D9 RID: 70105 RVA: 0x003C6633 File Offset: 0x003C4833
		public override string ToString()
		{
			if (this.Value != null)
			{
				return this.Value.ToString();
			}
			return base.ToString();
		}

		// Token: 0x060111DA RID: 70106 RVA: 0x003C664F File Offset: 0x003C484F
		protected void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04004CA5 RID: 19621
		private object aggregateValue;

		// Token: 0x04004CA6 RID: 19622
		private int itemCount;

		// Token: 0x04004CA7 RID: 19623
		private readonly AggregateFunction function;
	}
}
