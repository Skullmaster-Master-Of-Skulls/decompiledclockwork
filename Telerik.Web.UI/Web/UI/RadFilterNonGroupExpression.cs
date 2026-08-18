using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001880 RID: 6272
	public abstract class RadFilterNonGroupExpression : RadFilterExpression
	{
		// Token: 0x1700493C RID: 18748
		// (get) Token: 0x0600F2F4 RID: 62196 RVA: 0x003754AA File Offset: 0x003736AA
		// (set) Token: 0x0600F2F5 RID: 62197 RVA: 0x003754B2 File Offset: 0x003736B2
		public virtual string FieldName { get; set; }

		// Token: 0x1700493D RID: 18749
		// (get) Token: 0x0600F2F6 RID: 62198 RVA: 0x003754BB File Offset: 0x003736BB
		public virtual Type FieldType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x0600F2F7 RID: 62199 RVA: 0x003754C8 File Offset: 0x003736C8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				this.FieldName
			};
		}

		// Token: 0x0600F2F8 RID: 62200 RVA: 0x003754F0 File Offset: 0x003736F0
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				base.LoadViewState(array[0]);
				this.FieldName = (string)array[1];
				return;
			}
			base.LoadViewState(state);
		}

		// Token: 0x0600F2F9 RID: 62201 RVA: 0x00375528 File Offset: 0x00373728
		protected T ParseValue<T>(object value)
		{
			Type nonNullableType = RadFilterTypeHelper.GetNonNullableType(typeof(T));
			if (nonNullableType == typeof(TimeSpan))
			{
				value = ((DateTime)value).TimeOfDay;
			}
			else if (nonNullableType == typeof(char))
			{
				if (string.IsNullOrEmpty(value.ToString()) || value.ToString().Length > 1)
				{
					value = '\0';
				}
			}
			else if (nonNullableType == typeof(Guid))
			{
				return (T)((object)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value.ToString()));
			}
			if (nonNullableType != typeof(string))
			{
				string text = value as string;
				if (text != null && "" == text)
				{
					return default(T);
				}
			}
			return (T)((object)Convert.ChangeType(value, nonNullableType));
		}
	}
}
