using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Telerik.Web.UI
{
	// Token: 0x02001975 RID: 6517
	public abstract class RadListViewSingleValueExpression<T> : RadListViewFilterExpression, IRadListViewSingleValueExpression, IEquatable<RadListViewSingleValueExpression<T>>
	{
		// Token: 0x0600FC60 RID: 64608 RVA: 0x0038D860 File Offset: 0x0038BA60
		internal RadListViewSingleValueExpression()
		{
		}

		// Token: 0x0600FC61 RID: 64609 RVA: 0x0038D868 File Offset: 0x0038BA68
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadListViewSingleValueExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x17004C3A RID: 19514
		// (get) Token: 0x0600FC62 RID: 64610 RVA: 0x0038D878 File Offset: 0x0038BA78
		// (set) Token: 0x0600FC63 RID: 64611 RVA: 0x0038D8A9 File Offset: 0x0038BAA9
		public virtual T CurrentValue
		{
			get
			{
				object obj = this.ViewState["CurrentValue"];
				if (obj == null)
				{
					return default(T);
				}
				return (T)((object)obj);
			}
			set
			{
				this.ViewState["CurrentValue"] = value;
			}
		}

		// Token: 0x17004C3B RID: 19515
		// (get) Token: 0x0600FC64 RID: 64612 RVA: 0x0038D8C1 File Offset: 0x0038BAC1
		[Browsable(false)]
		public override Type FieldType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x17004C3C RID: 19516
		// (get) Token: 0x0600FC65 RID: 64613
		protected abstract string DynamicLinqStringFormat { get; }

		// Token: 0x17004C3D RID: 19517
		// (get) Token: 0x0600FC66 RID: 64614
		protected abstract string EntitySQLStringFormat { get; }

		// Token: 0x17004C3E RID: 19518
		// (get) Token: 0x0600FC67 RID: 64615
		protected abstract string OqlStringFormat { get; }

		// Token: 0x0600FC68 RID: 64616 RVA: 0x0038D8CD File Offset: 0x0038BACD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Equals(RadListViewSingleValueExpression<T> other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || (base.Equals(other) && object.Equals(other.CurrentValue, this.CurrentValue)));
		}

		// Token: 0x0600FC69 RID: 64617 RVA: 0x0038D90B File Offset: 0x0038BB0B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || this.Equals(obj as RadListViewSingleValueExpression<T>));
		}

		// Token: 0x0600FC6A RID: 64618 RVA: 0x0038D930 File Offset: 0x0038BB30
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			int num = base.GetHashCode() * 397;
			T currentValue = this.CurrentValue;
			return num ^ currentValue.GetHashCode();
		}

		// Token: 0x0600FC6B RID: 64619 RVA: 0x0038D95E File Offset: 0x0038BB5E
		public override string ToDynamicLinq()
		{
			return this.ConvertToFormat(this.DynamicLinqStringFormat, this.LinqFormatter);
		}

		// Token: 0x0600FC6C RID: 64620 RVA: 0x0038D972 File Offset: 0x0038BB72
		public override string ToOql()
		{
			return this.ConvertToFormat(this.OqlStringFormat, this.OqlFormatter);
		}

		// Token: 0x0600FC6D RID: 64621 RVA: 0x0038D988 File Offset: 0x0038BB88
		private string ConvertToFormat(string format, RadListViewSingleValueExpression<T>.ValueFormatter formatter)
		{
			string arg = string.Empty;
			if (this.CurrentValue != null)
			{
				if (this.FieldType == typeof(string))
				{
					T currentValue = this.CurrentValue;
					if (!this.IsValidStringValue(currentValue.ToString()))
					{
						return string.Empty;
					}
					T currentValue2 = this.CurrentValue;
					arg = formatter.PrepareValue(currentValue2.ToString());
				}
				else
				{
					arg = formatter.PrepareValue(this.CurrentValue);
				}
			}
			return string.Format(format, this.FieldName, arg);
		}

		// Token: 0x0600FC6E RID: 64622 RVA: 0x0038DA1D File Offset: 0x0038BC1D
		public override string ToEntitySQL()
		{
			return this.ConvertToFormat(this.EntitySQLStringFormat, this.EnitySqlFormatter);
		}

		// Token: 0x17004C3F RID: 19519
		// (get) Token: 0x0600FC6F RID: 64623 RVA: 0x0038DA31 File Offset: 0x0038BC31
		protected virtual RadListViewSingleValueExpression<T>.ValueFormatter LinqFormatter
		{
			get
			{
				if (this._linqFormatter == null)
				{
					this._linqFormatter = new RadListViewSingleValueExpression<T>.LinqValueFormatter();
				}
				return this._linqFormatter;
			}
		}

		// Token: 0x17004C40 RID: 19520
		// (get) Token: 0x0600FC70 RID: 64624 RVA: 0x0038DA4C File Offset: 0x0038BC4C
		protected virtual RadListViewSingleValueExpression<T>.ValueFormatter EnitySqlFormatter
		{
			get
			{
				if (this._enityFormatter == null)
				{
					this._enityFormatter = new RadListViewSingleValueExpression<T>.EnitytSqlValueFormatter();
				}
				return this._enityFormatter;
			}
		}

		// Token: 0x17004C41 RID: 19521
		// (get) Token: 0x0600FC71 RID: 64625 RVA: 0x0038DA67 File Offset: 0x0038BC67
		protected virtual RadListViewSingleValueExpression<T>.ValueFormatter OqlFormatter
		{
			get
			{
				if (this._oqlFormatter == null)
				{
					this._oqlFormatter = new RadListViewSingleValueExpression<T>.OqlValueFormatter();
				}
				return this._oqlFormatter;
			}
		}

		// Token: 0x17004C42 RID: 19522
		// (get) Token: 0x0600FC72 RID: 64626 RVA: 0x0038DA82 File Offset: 0x0038BC82
		// (set) Token: 0x0600FC73 RID: 64627 RVA: 0x0038DA8F File Offset: 0x0038BC8F
		object IRadListViewSingleValueExpression.CurrentValue
		{
			get
			{
				return this.CurrentValue;
			}
			set
			{
				this.CurrentValue = (T)((object)value);
			}
		}

		// Token: 0x17004C43 RID: 19523
		// (get) Token: 0x0600FC74 RID: 64628 RVA: 0x0038DA9D File Offset: 0x0038BC9D
		Type IRadListViewSingleValueExpression.ItemType
		{
			get
			{
				return this.FieldType;
			}
		}

		// Token: 0x040047C4 RID: 18372
		private RadListViewSingleValueExpression<T>.ValueFormatter _linqFormatter;

		// Token: 0x040047C5 RID: 18373
		private RadListViewSingleValueExpression<T>.ValueFormatter _enityFormatter;

		// Token: 0x040047C6 RID: 18374
		private RadListViewSingleValueExpression<T>.ValueFormatter _oqlFormatter;

		// Token: 0x02001976 RID: 6518
		protected abstract class ValueFormatter
		{
			// Token: 0x0600FC75 RID: 64629 RVA: 0x0038DAA5 File Offset: 0x0038BCA5
			public virtual string PrepareValue(string value)
			{
				value = value.Replace("'", "''");
				return string.Format("\"{0}\"", value);
			}

			// Token: 0x0600FC76 RID: 64630 RVA: 0x0038DAC4 File Offset: 0x0038BCC4
			public virtual string PrepareValue(Guid value)
			{
				return string.Format("'{0}'", value.ToString());
			}

			// Token: 0x0600FC77 RID: 64631 RVA: 0x0038DAE0 File Offset: 0x0038BCE0
			[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
			public virtual string PrepareValue(object value)
			{
				if (value is string)
				{
					return this.PrepareValue((string)value);
				}
				if (value is DateTime)
				{
					return this.PrepareValue((DateTime)value);
				}
				if (value is TimeSpan)
				{
					return this.PrepareValue((TimeSpan)value);
				}
				if (value is Guid)
				{
					return this.PrepareValue((Guid)value);
				}
				return value.ToString();
			}

			// Token: 0x0600FC78 RID: 64632 RVA: 0x0038DB47 File Offset: 0x0038BD47
			public virtual string PrepareValue(TimeSpan value)
			{
				return value.ToString();
			}

			// Token: 0x0600FC79 RID: 64633 RVA: 0x0038DB56 File Offset: 0x0038BD56
			public virtual string PrepareValue(DateTime value)
			{
				return value.ToString();
			}
		}

		// Token: 0x02001977 RID: 6519
		protected class LinqValueFormatter : RadListViewSingleValueExpression<T>.ValueFormatter
		{
			// Token: 0x0600FC7B RID: 64635 RVA: 0x0038DB6D File Offset: 0x0038BD6D
			public override string PrepareValue(string value)
			{
				return string.Format("\"{0}\"", value);
			}

			// Token: 0x0600FC7C RID: 64636 RVA: 0x0038DB7A File Offset: 0x0038BD7A
			public override string PrepareValue(TimeSpan value)
			{
				return string.Format("TimeSpan.Parse(\"{0}\")", value.ToString());
			}

			// Token: 0x0600FC7D RID: 64637 RVA: 0x0038DB93 File Offset: 0x0038BD93
			public override string PrepareValue(DateTime value)
			{
				return string.Format("DateTime.Parse(\"{0}\")", value.ToString());
			}

			// Token: 0x0600FC7E RID: 64638 RVA: 0x0038DBAC File Offset: 0x0038BDAC
			public override string PrepareValue(Guid value)
			{
				return string.Format("Guid(\"{0}\")", value.ToString());
			}
		}

		// Token: 0x02001978 RID: 6520
		protected class EnitytSqlValueFormatter : RadListViewSingleValueExpression<T>.ValueFormatter
		{
			// Token: 0x0600FC80 RID: 64640 RVA: 0x0038DBCD File Offset: 0x0038BDCD
			public override string PrepareValue(TimeSpan value)
			{
				return string.Format("TIME'{0}'", value.ToString());
			}

			// Token: 0x0600FC81 RID: 64641 RVA: 0x0038DBE6 File Offset: 0x0038BDE6
			public override string PrepareValue(DateTime value)
			{
				return string.Format("DATETIME'{0}'", value.ToString("yyyy-MM-dd HH:mm"));
			}

			// Token: 0x0600FC82 RID: 64642 RVA: 0x0038DBFE File Offset: 0x0038BDFE
			public override string PrepareValue(Guid value)
			{
				return string.Format("GUID('{0}')", value.ToString());
			}
		}

		// Token: 0x02001979 RID: 6521
		protected class OqlValueFormatter : RadListViewSingleValueExpression<T>.ValueFormatter
		{
			// Token: 0x0600FC84 RID: 64644 RVA: 0x0038DC1F File Offset: 0x0038BE1F
			public override string PrepareValue(TimeSpan value)
			{
				return this.PrepareValue(new DateTime(value.Ticks));
			}

			// Token: 0x0600FC85 RID: 64645 RVA: 0x0038DC33 File Offset: 0x0038BE33
			public override string PrepareValue(DateTime value)
			{
				return string.Format("timestamp '{0}'", value.ToString("yyyy-MM-dd H:mm:ss", DateTimeFormatInfo.InvariantInfo));
			}
		}
	}
}
