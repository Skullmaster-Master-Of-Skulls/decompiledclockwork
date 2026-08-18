using System;
using System.Globalization;
using System.Linq.Expressions;
using Telerik.Web.Data.Expressions;

namespace Telerik.Web.Data
{
	// Token: 0x02001B96 RID: 7062
	public class FilterDescriptor : FilterDescriptorBase
	{
		// Token: 0x06011183 RID: 70019 RVA: 0x003C571B File Offset: 0x003C391B
		public FilterDescriptor() : this(string.Empty, FilterOperator.IsEqualTo, null)
		{
		}

		// Token: 0x06011184 RID: 70020 RVA: 0x003C572A File Offset: 0x003C392A
		public FilterDescriptor(string member, FilterOperator filterOperator, object filterValue)
		{
			this.Member = member;
			this.Operator = filterOperator;
			this.Value = filterValue;
		}

		// Token: 0x06011185 RID: 70021 RVA: 0x003C5747 File Offset: 0x003C3947
		public FilterDescriptor(string member, FilterOperator filterOperator, object filterValue, bool caseSensitive) : this(member, filterOperator, filterValue)
		{
			this.IsCaseSensitive = caseSensitive;
		}

		// Token: 0x17005376 RID: 21366
		// (get) Token: 0x06011186 RID: 70022 RVA: 0x003C575A File Offset: 0x003C395A
		// (set) Token: 0x06011187 RID: 70023 RVA: 0x003C5762 File Offset: 0x003C3962
		public string Member
		{
			get
			{
				return this.member;
			}
			set
			{
				if (this.member != value)
				{
					this.member = value;
					base.OnPropertyChanged("Member");
				}
			}
		}

		// Token: 0x17005377 RID: 21367
		// (get) Token: 0x06011188 RID: 70024 RVA: 0x003C5784 File Offset: 0x003C3984
		// (set) Token: 0x06011189 RID: 70025 RVA: 0x003C578C File Offset: 0x003C398C
		public Type MemberType { get; set; }

		// Token: 0x17005378 RID: 21368
		// (get) Token: 0x0601118A RID: 70026 RVA: 0x003C5795 File Offset: 0x003C3995
		// (set) Token: 0x0601118B RID: 70027 RVA: 0x003C579D File Offset: 0x003C399D
		public FilterOperator Operator
		{
			get
			{
				return this.filterOperator;
			}
			set
			{
				if (this.filterOperator != value)
				{
					this.filterOperator = value;
					base.OnPropertyChanged("Operator");
				}
			}
		}

		// Token: 0x17005379 RID: 21369
		// (get) Token: 0x0601118C RID: 70028 RVA: 0x003C57BA File Offset: 0x003C39BA
		// (set) Token: 0x0601118D RID: 70029 RVA: 0x003C57C2 File Offset: 0x003C39C2
		public object Value
		{
			get
			{
				return this.filterValue;
			}
			set
			{
				if (this.filterValue != value)
				{
					this.filterValue = value;
					base.OnPropertyChanged("Value");
				}
			}
		}

		// Token: 0x1700537A RID: 21370
		// (get) Token: 0x0601118E RID: 70030 RVA: 0x003C57DF File Offset: 0x003C39DF
		// (set) Token: 0x0601118F RID: 70031 RVA: 0x003C57E7 File Offset: 0x003C39E7
		public bool IsCaseSensitive
		{
			get
			{
				return this.isCaseSensitive;
			}
			set
			{
				if (this.isCaseSensitive != value)
				{
					this.isCaseSensitive = value;
					base.OnPropertyChanged("IsCaseSensitive");
				}
			}
		}

		// Token: 0x06011190 RID: 70032 RVA: 0x003C5804 File Offset: 0x003C3A04
		protected override Expression CreateFilterExpression(ParameterExpression parameterExpression)
		{
			FilterDescriptorExpressionBuilder filterDescriptorExpressionBuilder = new FilterDescriptorExpressionBuilder(parameterExpression, this);
			filterDescriptorExpressionBuilder.Options.CopyFrom(base.ExpressionBuilderOptions);
			return filterDescriptorExpressionBuilder.CreateBodyExpression();
		}

		// Token: 0x06011191 RID: 70033 RVA: 0x003C5830 File Offset: 0x003C3A30
		public virtual bool Equals(FilterDescriptor other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || (object.Equals(other.filterOperator, this.filterOperator) && object.Equals(other.member, this.member) && object.Equals(other.filterValue, this.filterValue) && object.Equals(other.isCaseSensitive, this.isCaseSensitive)));
		}

		// Token: 0x06011192 RID: 70034 RVA: 0x003C58B4 File Offset: 0x003C3AB4
		public override bool Equals(object obj)
		{
			FilterDescriptor filterDescriptor = obj as FilterDescriptor;
			return filterDescriptor != null && this.Equals(filterDescriptor);
		}

		// Token: 0x06011193 RID: 70035 RVA: 0x003C58D4 File Offset: 0x003C3AD4
		public override int GetHashCode()
		{
			int num = this.filterOperator.GetHashCode();
			num = (num * 397 ^ ((this.member != null) ? this.member.GetHashCode() : 0));
			num = (num * 397 ^ ((this.filterValue != null) ? this.filterValue.GetHashCode() : 0));
			return num * 397 ^ this.isCaseSensitive.GetHashCode();
		}

		// Token: 0x06011194 RID: 70036 RVA: 0x003C5948 File Offset: 0x003C3B48
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}, {3}", new object[]
			{
				this.member,
				this.filterOperator,
				this.filterValue,
				this.isCaseSensitive ? "Case Sensitive" : "Case Insensitive"
			});
		}

		// Token: 0x04004C82 RID: 19586
		private FilterOperator filterOperator;

		// Token: 0x04004C83 RID: 19587
		private string member;

		// Token: 0x04004C84 RID: 19588
		private object filterValue;

		// Token: 0x04004C85 RID: 19589
		private bool isCaseSensitive;
	}
}
