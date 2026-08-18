using System;

namespace Telerik.Web.UI.Design.DatePickerAttributes
{
	// Token: 0x020012AB RID: 4779
	[AttributeUsage(AttributeTargets.Property)]
	public class DatePickerBrowsableAttribute : Attribute
	{
		// Token: 0x0600C80F RID: 51215 RVA: 0x002C9600 File Offset: 0x002C7800
		public DatePickerBrowsableAttribute(bool browsable)
		{
			this._browsable = browsable;
		}

		// Token: 0x170040A1 RID: 16545
		// (get) Token: 0x0600C810 RID: 51216 RVA: 0x002C960F File Offset: 0x002C780F
		public bool Browsable
		{
			get
			{
				return this._browsable;
			}
		}

		// Token: 0x0600C811 RID: 51217 RVA: 0x002C9618 File Offset: 0x002C7818
		public override bool Equals(object obj)
		{
			DatePickerBrowsableAttribute datePickerBrowsableAttribute = obj as DatePickerBrowsableAttribute;
			if (datePickerBrowsableAttribute != null)
			{
				return this.Browsable == datePickerBrowsableAttribute.Browsable;
			}
			return base.Equals(obj);
		}

		// Token: 0x0600C812 RID: 51218 RVA: 0x002C9645 File Offset: 0x002C7845
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040034B1 RID: 13489
		private bool _browsable;
	}
}
