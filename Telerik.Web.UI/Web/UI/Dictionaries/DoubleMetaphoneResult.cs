using System;
using System.Text;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011CD RID: 4557
	internal class DoubleMetaphoneResult
	{
		// Token: 0x0600BC4F RID: 48207 RVA: 0x0029C92E File Offset: 0x0029AB2E
		internal DoubleMetaphoneResult(int maxLengthValue, DoubleMetaphone owner)
		{
			this.maxLength = maxLengthValue;
			this.primary = new StringBuilder(owner.GetMaxCodeLen());
			this.alternate = new StringBuilder(owner.GetMaxCodeLen());
		}

		// Token: 0x0600BC50 RID: 48208 RVA: 0x0029C95F File Offset: 0x0029AB5F
		internal void Append(char value)
		{
			this.AppendPrimary(value);
			this.AppendAlternate(value);
		}

		// Token: 0x0600BC51 RID: 48209 RVA: 0x0029C96F File Offset: 0x0029AB6F
		internal void Append(char primary, char alternate)
		{
			this.AppendPrimary(primary);
			this.AppendAlternate(alternate);
		}

		// Token: 0x0600BC52 RID: 48210 RVA: 0x0029C97F File Offset: 0x0029AB7F
		internal void AppendPrimary(char value)
		{
			if (this.primary.Length < this.maxLength)
			{
				this.primary.Append(value);
			}
		}

		// Token: 0x0600BC53 RID: 48211 RVA: 0x0029C9A1 File Offset: 0x0029ABA1
		internal void AppendAlternate(char value)
		{
			if (this.alternate.Length < this.maxLength)
			{
				this.alternate.Append(value);
			}
		}

		// Token: 0x0600BC54 RID: 48212 RVA: 0x0029C9C3 File Offset: 0x0029ABC3
		internal void Append(string value)
		{
			this.AppendPrimary(value);
			this.AppendAlternate(value);
		}

		// Token: 0x0600BC55 RID: 48213 RVA: 0x0029C9D3 File Offset: 0x0029ABD3
		internal void Append(string primary, string alternate)
		{
			this.AppendPrimary(primary);
			this.AppendAlternate(alternate);
		}

		// Token: 0x0600BC56 RID: 48214 RVA: 0x0029C9E4 File Offset: 0x0029ABE4
		internal void AppendPrimary(string value)
		{
			int num = this.maxLength - this.primary.Length;
			if (value.Length <= num)
			{
				this.primary.Append(value);
				return;
			}
			this.primary.Append(value.Substring(0, num));
		}

		// Token: 0x0600BC57 RID: 48215 RVA: 0x0029CA30 File Offset: 0x0029AC30
		internal void AppendAlternate(string value)
		{
			int num = this.maxLength - this.alternate.Length;
			if (value.Length <= num)
			{
				this.alternate.Append(value);
				return;
			}
			this.alternate.Append(value.Substring(0, num));
		}

		// Token: 0x0600BC58 RID: 48216 RVA: 0x0029CA7B File Offset: 0x0029AC7B
		internal string GetPrimary()
		{
			return this.primary.ToString();
		}

		// Token: 0x0600BC59 RID: 48217 RVA: 0x0029CA88 File Offset: 0x0029AC88
		internal string GetAlternate()
		{
			return this.alternate.ToString();
		}

		// Token: 0x0600BC5A RID: 48218 RVA: 0x0029CA95 File Offset: 0x0029AC95
		internal bool IsComplete()
		{
			return this.primary.Length >= this.maxLength && this.alternate.Length >= this.maxLength;
		}

		// Token: 0x04003176 RID: 12662
		private readonly StringBuilder primary;

		// Token: 0x04003177 RID: 12663
		private readonly StringBuilder alternate;

		// Token: 0x04003178 RID: 12664
		private readonly int maxLength;
	}
}
