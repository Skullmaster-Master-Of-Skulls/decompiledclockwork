using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x02000CC1 RID: 3265
	[DataContract]
	public sealed class TextCondition : LocalCondition, ITextCondition
	{
		// Token: 0x060079F1 RID: 31217 RVA: 0x001BFC4C File Offset: 0x001BDE4C
		public TextCondition()
		{
			this.ignoreCase = true;
		}

		// Token: 0x1700273E RID: 10046
		// (get) Token: 0x060079F2 RID: 31218 RVA: 0x001BFC5B File Offset: 0x001BDE5B
		public override bool IsActive
		{
			get
			{
				return this.Pattern != null;
			}
		}

		// Token: 0x1700273F RID: 10047
		// (get) Token: 0x060079F3 RID: 31219 RVA: 0x001BFC69 File Offset: 0x001BDE69
		// (set) Token: 0x060079F4 RID: 31220 RVA: 0x001BFC71 File Offset: 0x001BDE71
		[DataMember]
		public string Pattern
		{
			get
			{
				return this.pattern;
			}
			set
			{
				if (this.pattern != value)
				{
					this.pattern = value;
					base.OnPropertyChanged("Pattern");
				}
			}
		}

		// Token: 0x17002740 RID: 10048
		// (get) Token: 0x060079F5 RID: 31221 RVA: 0x001BFC93 File Offset: 0x001BDE93
		// (set) Token: 0x060079F6 RID: 31222 RVA: 0x001BFC9B File Offset: 0x001BDE9B
		[DataMember]
		public TextComparison Comparison
		{
			get
			{
				return this.comparison;
			}
			set
			{
				if (this.comparison != value)
				{
					this.comparison = value;
					base.OnPropertyChanged("Comparison");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x17002741 RID: 10049
		// (get) Token: 0x060079F7 RID: 31223 RVA: 0x001BFCC3 File Offset: 0x001BDEC3
		// (set) Token: 0x060079F8 RID: 31224 RVA: 0x001BFCCB File Offset: 0x001BDECB
		[DataMember]
		public bool IgnoreCase
		{
			get
			{
				return this.ignoreCase;
			}
			set
			{
				if (this.ignoreCase != value)
				{
					this.ignoreCase = value;
					base.OnPropertyChanged("IgnoreCase");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x060079F9 RID: 31225 RVA: 0x001BFCF4 File Offset: 0x001BDEF4
		protected override void CloneCore(Cloneable source)
		{
			TextCondition textCondition = source as TextCondition;
			if (textCondition != null)
			{
				this.Pattern = textCondition.Pattern;
				this.Comparison = textCondition.Comparison;
				this.IgnoreCase = textCondition.IgnoreCase;
			}
		}

		// Token: 0x060079FA RID: 31226 RVA: 0x001BFD2F File Offset: 0x001BDF2F
		protected override Cloneable CreateInstanceCore()
		{
			return new TextCondition();
		}

		// Token: 0x060079FB RID: 31227 RVA: 0x001BFD38 File Offset: 0x001BDF38
		public override bool PassesFilter(object item)
		{
			string text = Convert.ToString(item, CultureInfo.InvariantCulture);
			switch (this.Comparison)
			{
			case TextComparison.DoesNotBeginWith:
				return string.IsNullOrEmpty(text) || !text.StartsWith(this.Pattern, this.IgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture);
			case TextComparison.EndsWith:
				return !string.IsNullOrEmpty(text) && text.EndsWith(this.Pattern, this.IgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture);
			case TextComparison.DoesNotEndWith:
				return string.IsNullOrEmpty(text) || !text.EndsWith(this.Pattern, this.IgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture);
			case TextComparison.Contains:
				if (string.IsNullOrEmpty(this.Pattern))
				{
					return true;
				}
				if (string.IsNullOrEmpty(text))
				{
					return false;
				}
				if (this.IgnoreCase)
				{
					string text2 = text.ToLowerInvariant();
					string value = this.Pattern.ToLowerInvariant();
					return text2.Contains(value);
				}
				return text.Contains(this.Pattern);
			case TextComparison.DoesNotContain:
				if (string.IsNullOrEmpty(this.Pattern))
				{
					return false;
				}
				if (string.IsNullOrEmpty(text))
				{
					return true;
				}
				if (this.IgnoreCase)
				{
					string text3 = text.ToLowerInvariant();
					string value2 = this.Pattern.ToLowerInvariant();
					return !text3.Contains(value2);
				}
				return !text.Contains(this.Pattern);
			}
			return !string.IsNullOrEmpty(text) && text.StartsWith(this.Pattern, this.IgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture);
		}

		// Token: 0x04002171 RID: 8561
		private string pattern;

		// Token: 0x04002172 RID: 8562
		private TextComparison comparison;

		// Token: 0x04002173 RID: 8563
		private bool ignoreCase;
	}
}
