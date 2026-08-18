using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI
{
	// Token: 0x020012B3 RID: 4787
	internal class MaskParser
	{
		// Token: 0x0600C868 RID: 51304 RVA: 0x002CB6C4 File Offset: 0x002C98C4
		public MaskPartCollection Parse(string mask)
		{
			this.maskParts = new MaskPartCollection();
			bool flag = false;
			bool flag2 = false;
			foreach (char c in mask)
			{
				if (flag2)
				{
					if (flag)
					{
						this.AddToEnum(c);
					}
					else if (c == 'r')
					{
						this.AddLiteral('\r');
					}
					else if (c == 'n')
					{
						this.AddLiteral('\n');
					}
					else
					{
						this.AddLiteral(c);
					}
					flag2 = false;
				}
				else if (c == '\\')
				{
					flag2 = true;
				}
				else if (c == '<')
				{
					flag = true;
					this.StartEnum();
				}
				else if (c == '>')
				{
					flag = false;
					this.EndEnum();
				}
				else if (flag)
				{
					this.AddToEnum(c);
				}
				else
				{
					this.AddPart(c);
				}
			}
			return this.maskParts;
		}

		// Token: 0x0600C869 RID: 51305 RVA: 0x002CB774 File Offset: 0x002C9974
		private void StartEnum()
		{
			this.enumBuffer = new StringBuilder();
		}

		// Token: 0x0600C86A RID: 51306 RVA: 0x002CB784 File Offset: 0x002C9984
		private void EndEnum()
		{
			string text = this.enumBuffer.ToString();
			if (this.longrangeTest.Match(text).Success)
			{
				this.maskParts.Add(this.lastMaskPart = this.CreateLongRange(text));
				return;
			}
			if (this.rangeTest.Match(text).Success)
			{
				this.maskParts.Add(this.lastMaskPart = this.CreateRange(text));
				return;
			}
			this.maskParts.Add(this.lastMaskPart = this.CreateEnum(text));
		}

		// Token: 0x0600C86B RID: 51307 RVA: 0x002CB816 File Offset: 0x002C9A16
		private void AddToEnum(char c)
		{
			this.enumBuffer.Append(c);
		}

		// Token: 0x0600C86C RID: 51308 RVA: 0x002CB828 File Offset: 0x002C9A28
		private void AddLiteral(char c)
		{
			if (this.lastMaskPart is LiteralMaskPart)
			{
				((LiteralMaskPart)this.lastMaskPart).Append(c);
				return;
			}
			this.lastMaskPart = new LiteralMaskPart();
			((LiteralMaskPart)this.lastMaskPart).Append(c);
			this.maskParts.Add(this.lastMaskPart);
		}

		// Token: 0x0600C86D RID: 51309 RVA: 0x002CB884 File Offset: 0x002C9A84
		private void AddPart(char c)
		{
			if (c <= 'L')
			{
				if (c == '#')
				{
					this.lastMaskPart = new DigitMaskPart();
					goto IL_9D;
				}
				if (c == 'L')
				{
					this.lastMaskPart = new UpperMaskPart();
					goto IL_9D;
				}
			}
			else
			{
				if (c == 'a')
				{
					this.lastMaskPart = new FreeMaskPart();
					goto IL_9D;
				}
				if (c == 'l')
				{
					this.lastMaskPart = new LowerMaskPart();
					goto IL_9D;
				}
			}
			LiteralMaskPart literalMaskPart = this.lastMaskPart as LiteralMaskPart;
			if (literalMaskPart != null && literalMaskPart.Text.EndsWith("*") == (c == '*'))
			{
				literalMaskPart.Append(c);
				return;
			}
			this.lastMaskPart = new LiteralMaskPart();
			((LiteralMaskPart)this.lastMaskPart).Append(c);
			IL_9D:
			this.maskParts.Add(this.lastMaskPart);
		}

		// Token: 0x0600C86E RID: 51310 RVA: 0x002CB940 File Offset: 0x002C9B40
		private LongRangeMaskPart CreateLongRange(string mask)
		{
			LongRangeMaskPart longRangeMaskPart = new LongRangeMaskPart();
			MatchCollection matchCollection = this.rangeBraker.Matches(mask);
			longRangeMaskPart.LowerLimit = long.Parse(matchCollection[0].Value);
			longRangeMaskPart.UpperLimit = long.Parse(matchCollection[1].Value);
			return longRangeMaskPart;
		}

		// Token: 0x0600C86F RID: 51311 RVA: 0x002CB990 File Offset: 0x002C9B90
		private NumericRangeMaskPart CreateRange(string mask)
		{
			NumericRangeMaskPart numericRangeMaskPart = new NumericRangeMaskPart();
			MatchCollection matchCollection = this.rangeBraker.Matches(mask);
			numericRangeMaskPart.LowerLimit = int.Parse(matchCollection[0].Value);
			numericRangeMaskPart.UpperLimit = int.Parse(matchCollection[1].Value);
			return numericRangeMaskPart;
		}

		// Token: 0x0600C870 RID: 51312 RVA: 0x002CB9E0 File Offset: 0x002C9BE0
		private EnumerationMaskPart CreateEnum(string mask)
		{
			EnumerationMaskPart enumerationMaskPart = new EnumerationMaskPart();
			string[] array = this.enumBraker.Split(mask);
			foreach (string text in array)
			{
				enumerationMaskPart.Items.Add(text.Replace("\\|", "|").Replace("\\\\", "\\"));
			}
			return enumerationMaskPart;
		}

		// Token: 0x040034C1 RID: 13505
		private MaskPart lastMaskPart;

		// Token: 0x040034C2 RID: 13506
		private StringBuilder enumBuffer;

		// Token: 0x040034C3 RID: 13507
		private MaskPartCollection maskParts;

		// Token: 0x040034C4 RID: 13508
		private Regex enumBraker = new Regex("(?<!\\\\)\\|");

		// Token: 0x040034C5 RID: 13509
		private Regex rangeBraker = new Regex("(-?\\d+)");

		// Token: 0x040034C6 RID: 13510
		private Regex rangeTest = new Regex("^-?\\d+\\.\\.-?\\d+$");

		// Token: 0x040034C7 RID: 13511
		private Regex longrangeTest = new Regex("^-?\\d+\\.\\.\\.-?\\d+$");
	}
}
