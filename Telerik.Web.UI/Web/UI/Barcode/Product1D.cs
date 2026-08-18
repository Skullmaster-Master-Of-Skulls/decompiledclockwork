using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009D6 RID: 2518
	internal abstract class Product1D : Symbology1D
	{
		// Token: 0x06006062 RID: 24674 RVA: 0x0012A39C File Offset: 0x0012859C
		[Description("Initializes a new instance of Product1D type.")]
		public Product1D()
		{
			this.charset = new List<char>();
			this.charset.Add('0');
			this.charset.Add('1');
			this.charset.Add('2');
			this.charset.Add('3');
			this.charset.Add('4');
			this.charset.Add('5');
			this.charset.Add('6');
			this.charset.Add('7');
			this.charset.Add('8');
			this.charset.Add('9');
		}

		// Token: 0x17001FBD RID: 8125
		// (get) Token: 0x06006063 RID: 24675 RVA: 0x0012A43C File Offset: 0x0012863C
		// (set) Token: 0x06006064 RID: 24676 RVA: 0x0012A444 File Offset: 0x00128644
		public string LeadingTextboxText { get; set; }

		// Token: 0x17001FBE RID: 8126
		// (get) Token: 0x06006065 RID: 24677 RVA: 0x0012A44D File Offset: 0x0012864D
		// (set) Token: 0x06006066 RID: 24678 RVA: 0x0012A455 File Offset: 0x00128655
		public string LeftTextboxText { get; set; }

		// Token: 0x17001FBF RID: 8127
		// (get) Token: 0x06006067 RID: 24679 RVA: 0x0012A45E File Offset: 0x0012865E
		// (set) Token: 0x06006068 RID: 24680 RVA: 0x0012A466 File Offset: 0x00128666
		public string RightTextboxText { get; set; }

		// Token: 0x17001FC0 RID: 8128
		// (get) Token: 0x06006069 RID: 24681 RVA: 0x0012A46F File Offset: 0x0012866F
		// (set) Token: 0x0600606A RID: 24682 RVA: 0x0012A477 File Offset: 0x00128677
		public string EndTextboxText { get; set; }

		// Token: 0x17001FC1 RID: 8129
		// (get) Token: 0x0600606B RID: 24683 RVA: 0x0012A480 File Offset: 0x00128680
		// (set) Token: 0x0600606C RID: 24684 RVA: 0x0012A488 File Offset: 0x00128688
		public string SecondaryTextboxText { get; set; }

		// Token: 0x0600606D RID: 24685 RVA: 0x0012A491 File Offset: 0x00128691
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		protected string GetSymbols(string value, int length)
		{
			value = value.PadLeft(length - 1, Product1D.Padding);
			value += this.GetChecksum(value);
			return value;
		}

		// Token: 0x0600606E RID: 24686 RVA: 0x0012A4B8 File Offset: 0x001286B8
		protected char GetChecksum(string value)
		{
			return this.GetChecksum(value, 3, 1, 10);
		}

		// Token: 0x0600606F RID: 24687 RVA: 0x0012A4C8 File Offset: 0x001286C8
		private char GetChecksum(string value, int first, int second, int modulo)
		{
			int num = 0;
			int num2 = first;
			for (int i = value.Length - 1; i >= 0; i--)
			{
				int num3 = this.charset.IndexOf(value[i]);
				num += num3 * num2;
				if (num2 == first)
				{
					num2 = second;
				}
				else
				{
					num2 = first;
				}
			}
			num %= modulo;
			if (num != 0)
			{
				num = modulo - num;
			}
			return this.charset[num];
		}

		// Token: 0x04001766 RID: 5990
		public static readonly char Padding = '0';

		// Token: 0x04001767 RID: 5991
		private List<char> charset;
	}
}
