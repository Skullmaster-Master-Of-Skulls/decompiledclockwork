using System;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000696 RID: 1686
	internal sealed class RegexFC
	{
		// Token: 0x06003EC4 RID: 16068 RVA: 0x0010585C File Offset: 0x00103A5C
		internal RegexFC(bool nullable)
		{
			this._cc = new RegexCharClass();
			this._nullable = nullable;
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x00105878 File Offset: 0x00103A78
		internal RegexFC(char ch, bool not, bool nullable, bool caseInsensitive)
		{
			this._cc = new RegexCharClass();
			if (not)
			{
				if (ch > '\0')
				{
					this._cc.AddRange('\0', ch - '\u0001');
				}
				if (ch < '￿')
				{
					this._cc.AddRange(ch + '\u0001', char.MaxValue);
				}
			}
			else
			{
				this._cc.AddRange(ch, ch);
			}
			this._caseInsensitive = caseInsensitive;
			this._nullable = nullable;
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x001058E7 File Offset: 0x00103AE7
		internal RegexFC(string charClass, bool nullable, bool caseInsensitive)
		{
			this._cc = RegexCharClass.Parse(charClass);
			this._nullable = nullable;
			this._caseInsensitive = caseInsensitive;
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x0010590C File Offset: 0x00103B0C
		internal bool AddFC(RegexFC fc, bool concatenate)
		{
			if (!this._cc.CanMerge || !fc._cc.CanMerge)
			{
				return false;
			}
			if (concatenate)
			{
				if (!this._nullable)
				{
					return true;
				}
				if (!fc._nullable)
				{
					this._nullable = false;
				}
			}
			else if (fc._nullable)
			{
				this._nullable = true;
			}
			this._caseInsensitive |= fc._caseInsensitive;
			this._cc.AddCharClass(fc._cc);
			return true;
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x00105987 File Offset: 0x00103B87
		internal string GetFirstChars(CultureInfo culture)
		{
			if (this._caseInsensitive)
			{
				this._cc.AddLowercase(culture);
			}
			return this._cc.ToStringClass();
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x001059A8 File Offset: 0x00103BA8
		internal bool IsCaseInsensitive()
		{
			return this._caseInsensitive;
		}

		// Token: 0x04002DD8 RID: 11736
		internal RegexCharClass _cc;

		// Token: 0x04002DD9 RID: 11737
		internal bool _nullable;

		// Token: 0x04002DDA RID: 11738
		internal bool _caseInsensitive;
	}
}
