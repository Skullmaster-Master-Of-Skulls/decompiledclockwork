using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace Telerik.Web.UI
{
	// Token: 0x02001207 RID: 4615
	public class TreeListNumericColumn : TreeListBoundColumn
	{
		// Token: 0x17003D97 RID: 15767
		// (get) Token: 0x0600BED8 RID: 48856 RVA: 0x002A4920 File Offset: 0x002A2B20
		// (set) Token: 0x0600BED9 RID: 48857 RVA: 0x002A494E File Offset: 0x002A2B4E
		[DefaultValue(typeof(NumericType), "Number")]
		[NotifyParentProperty(true)]
		public NumericType NumericType
		{
			get
			{
				object obj = base.ViewState["NumericType"];
				if (obj == null)
				{
					obj = NumericType.Number;
				}
				return (NumericType)obj;
			}
			set
			{
				base.ViewState["NumericType"] = value;
			}
		}

		// Token: 0x17003D98 RID: 15768
		// (get) Token: 0x0600BEDA RID: 48858 RVA: 0x002A4968 File Offset: 0x002A2B68
		// (set) Token: 0x0600BEDB RID: 48859 RVA: 0x002A4996 File Offset: 0x002A2B96
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool AllowRounding
		{
			get
			{
				object obj = base.ViewState["AllowRounding"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["AllowRounding"] = value;
			}
		}

		// Token: 0x17003D99 RID: 15769
		// (get) Token: 0x0600BEDC RID: 48860 RVA: 0x002A49B0 File Offset: 0x002A2BB0
		// (set) Token: 0x0600BEDD RID: 48861 RVA: 0x002A49DE File Offset: 0x002A2BDE
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool KeepNotRoundedValue
		{
			get
			{
				object obj = base.ViewState["KeepNotRoundedValue"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["KeepNotRoundedValue"] = value;
			}
		}

		// Token: 0x17003D9A RID: 15770
		// (get) Token: 0x0600BEDE RID: 48862 RVA: 0x002A49F8 File Offset: 0x002A2BF8
		// (set) Token: 0x0600BEDF RID: 48863 RVA: 0x002A4A67 File Offset: 0x002A2C67
		[NotifyParentProperty(true)]
		public int DecimalDigits
		{
			get
			{
				object obj = base.ViewState["DecimalDigits"];
				if (obj != null)
				{
					return (int)obj;
				}
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				switch (this.NumericType)
				{
				case NumericType.Currency:
					return currentCulture.NumberFormat.CurrencyDecimalDigits;
				case NumericType.Percent:
					return currentCulture.NumberFormat.PercentDecimalDigits;
				default:
					return currentCulture.NumberFormat.NumberDecimalDigits;
				}
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("DecimalDigits", "Valid values are between 0 and 99, inclusive.");
				}
				base.ViewState["DecimalDigits"] = value;
			}
		}

		// Token: 0x0600BEE0 RID: 48864 RVA: 0x002A4A98 File Offset: 0x002A2C98
		public override ITreeListColumnEditor CreateDefaultColumnEditor()
		{
			if (base.Owner.ResolvedRenderMode == RenderMode.Mobile && base.UseNativeEditorsInMobileMode)
			{
				return new TreeListMobileNumericColumnEditor(this);
			}
			return new TreeListNumericColumnEditor(this);
		}

		// Token: 0x0600BEE1 RID: 48865 RVA: 0x002A4ABD File Offset: 0x002A2CBD
		protected override string FormatDataValue(object dataValue, TreeListDataItem item)
		{
			if (this.NumericType == NumericType.Currency && string.IsNullOrEmpty(this.DataFormatString))
			{
				this.DataFormatString = "{0:c2}";
			}
			return base.FormatDataValue(dataValue, item);
		}
	}
}
