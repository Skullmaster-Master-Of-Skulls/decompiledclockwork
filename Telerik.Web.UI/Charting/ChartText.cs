using System;
using System.Drawing;
using System.Text;

namespace Telerik.Charting
{
	// Token: 0x0200171B RID: 5915
	internal class ChartText
	{
		// Token: 0x170045FE RID: 17918
		// (get) Token: 0x0600E5CD RID: 58829 RVA: 0x00330678 File Offset: 0x0032E878
		internal ChartWord Space
		{
			get
			{
				return this.space;
			}
		}

		// Token: 0x170045FF RID: 17919
		// (get) Token: 0x0600E5CE RID: 58830 RVA: 0x00330680 File Offset: 0x0032E880
		internal float Height
		{
			get
			{
				ChartString first = this.strings.First;
				if (first != null)
				{
					return first.Height * (float)this.strings.Count;
				}
				return 0f;
			}
		}

		// Token: 0x17004600 RID: 17920
		// (get) Token: 0x0600E5CF RID: 58831 RVA: 0x003306B5 File Offset: 0x0032E8B5
		internal float Width
		{
			get
			{
				return this.GetStringWithMaxWidth().Width;
			}
		}

		// Token: 0x17004601 RID: 17921
		// (get) Token: 0x0600E5D0 RID: 58832 RVA: 0x003306C2 File Offset: 0x0032E8C2
		internal float Factor
		{
			get
			{
				if (this.Height == 0f)
				{
					return 0f;
				}
				return this.Width / this.Height;
			}
		}

		// Token: 0x0600E5D1 RID: 58833 RVA: 0x003306E4 File Offset: 0x0032E8E4
		private ChartText()
		{
			this.space = new ChartWord(" ", 0f);
			this.strings = new ChartStringCollection(this);
		}

		// Token: 0x0600E5D2 RID: 58834 RVA: 0x00330710 File Offset: 0x0032E910
		internal ChartText(string text, Font font, ChartGraphics graphics) : this()
		{
			text = ChartText.DropLineBreaks(text);
			ChartString chartString = new ChartString(graphics.MeasureString(text, font).Height);
			this.strings.Add(chartString);
			string[] array = text.Split(new char[]
			{
				' '
			});
			foreach (string text2 in array)
			{
				SizeF sizeF = graphics.MeasureString(text2, font);
				chartString.Words.Add(new ChartWord(text2, sizeF.Width));
			}
			this.space = new ChartWord(" ", graphics.MeasureString(" ", font).Width);
			chartString.WidthCalculate();
			this.text = text;
			this.font = font;
			this.graphics = graphics;
		}

		// Token: 0x0600E5D3 RID: 58835 RVA: 0x003307E4 File Offset: 0x0032E9E4
		internal void Distibute(float factor, WrapContext context)
		{
			switch (context.Type)
			{
			case WrapType.FixedWidth:
			case WrapType.FixedBoth:
				this.text = this.FixedWidthDistibution(context.ContainerWidth);
				return;
			case WrapType.FixedHeight:
			{
				this.text = string.Empty;
				ChartString first = this.strings.First;
				if (first != null)
				{
					this.FixedHeightDistibution(factor, (int)Math.Round((double)(context.ContainerHeight / first.Height)));
				}
				else
				{
					this.FixedHeightDistibution(factor, 1);
				}
				this.text = this.ToString();
				return;
			}
			default:
				this.Distibute(factor, context.ContainerWidth);
				return;
			}
		}

		// Token: 0x0600E5D4 RID: 58836 RVA: 0x0033087C File Offset: 0x0032EA7C
		internal void Distibute(float factor, float needWidth)
		{
			float num = float.MaxValue;
			if (needWidth > 0f)
			{
				float num2 = (float)Math.Ceiling((double)(this.Width / needWidth));
				float num3 = num2 * this.Height;
				num = needWidth / num3;
			}
			if (factor < num)
			{
				this.Distibute(factor);
				return;
			}
			this.text = this.FixedWidthDistibution(needWidth);
		}

		// Token: 0x0600E5D5 RID: 58837 RVA: 0x003308CD File Offset: 0x0032EACD
		internal void Distibute(float factor)
		{
			this.text = string.Empty;
			this.FixedProportionDistibution(factor);
			this.text = this.ToString();
		}

		// Token: 0x0600E5D6 RID: 58838 RVA: 0x003308F0 File Offset: 0x0032EAF0
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.text))
			{
				return this.text;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.strings.Count - 1; i++)
			{
				ChartString chartString = this.strings[i];
				string value = chartString.ToString();
				if (!string.IsNullOrEmpty(value))
				{
					stringBuilder.AppendLine(chartString.ToString());
				}
			}
			stringBuilder.Append(this.strings[this.strings.Count - 1].ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x0600E5D7 RID: 58839 RVA: 0x00330984 File Offset: 0x0032EB84
		private void FixedProportionDistibution(float factor)
		{
			ChartText chartText = this.Clone();
			for (float factor2 = this.Factor; factor2 > factor; factor2 = this.Factor)
			{
				if (factor2 < chartText.Factor)
				{
					chartText = this.Clone();
				}
				ChartString stringWithMaxWidth = this.GetStringWithMaxWidth();
				if (stringWithMaxWidth == null)
				{
					return;
				}
				stringWithMaxWidth.MoveLastWordToNextString();
			}
			if (Math.Abs(this.Factor - factor) > Math.Abs(chartText.Factor - factor))
			{
				this.strings = chartText.strings;
				this.strings.Parent = this;
			}
		}

		// Token: 0x0600E5D8 RID: 58840 RVA: 0x00330A04 File Offset: 0x0032EC04
		private void FixedHeightDistibution(float factor, int maxStringsCount)
		{
			ChartText chartText = this.Clone();
			float factor2 = this.Factor;
			bool flag = true;
			while (factor2 > factor)
			{
				if (factor2 < chartText.Factor)
				{
					chartText = this.Clone();
				}
				this.Clone();
				ChartString stringWithMaxWidth = this.GetStringWithMaxWidth();
				if (stringWithMaxWidth == null)
				{
					return;
				}
				stringWithMaxWidth.MoveLastWordToNextString();
				if (this.strings.Count > maxStringsCount)
				{
					this.strings = chartText.strings;
					this.strings.Parent = this;
					flag = false;
					break;
				}
				factor2 = this.Factor;
			}
			if (Math.Abs(this.Factor - factor) > Math.Abs(chartText.Factor - factor) && flag)
			{
				this.strings = chartText.strings;
				this.strings.Parent = this;
			}
		}

		// Token: 0x0600E5D9 RID: 58841 RVA: 0x00330AB8 File Offset: 0x0032ECB8
		private string FixedWidthDistibution(float width)
		{
			string[] array = this.text.Split(new char[]
			{
				' '
			});
			string text = "";
			string text2 = "";
			foreach (string text3 in array)
			{
				if (this.graphics.MeasureString(text3, this.font).Width > width)
				{
					if (!string.IsNullOrEmpty(text))
					{
						text += "\n";
						text2 = "";
					}
					char[] array3 = text3.ToCharArray();
					foreach (char c in array3)
					{
						text = this.AddString(text, c.ToString(), text2, width);
						text2 = "";
					}
					text2 = " ";
				}
				else
				{
					text = this.AddString(text, text3, text2, width);
					text2 = " ";
				}
			}
			return text;
		}

		// Token: 0x0600E5DA RID: 58842 RVA: 0x00330BA0 File Offset: 0x0032EDA0
		private string AddString(string baseString, string str, string space, float width)
		{
			StringBuilder stringBuilder = new StringBuilder(baseString);
			stringBuilder.Append(space);
			stringBuilder.Append(str);
			string text = stringBuilder.ToString();
			stringBuilder = new StringBuilder(baseString);
			if (this.graphics.MeasureString(text, this.font).Width > width)
			{
				stringBuilder.Append("\n");
				stringBuilder.Append(str);
				baseString = stringBuilder.ToString();
			}
			else
			{
				stringBuilder.Append(space);
				stringBuilder.Append(str);
				baseString = stringBuilder.ToString();
			}
			return baseString;
		}

		// Token: 0x0600E5DB RID: 58843 RVA: 0x00330C28 File Offset: 0x0032EE28
		private ChartString GetStringWithMaxWidth()
		{
			if (this.strings.Count <= 0)
			{
				return null;
			}
			ChartString chartString = this.strings[0];
			float num = chartString.Width;
			for (int i = 1; i < this.strings.Count; i++)
			{
				float width = this.strings[i].Width;
				if (num < width)
				{
					num = width;
					chartString = this.strings[i];
				}
			}
			return chartString;
		}

		// Token: 0x0600E5DC RID: 58844 RVA: 0x00330C98 File Offset: 0x0032EE98
		private ChartText Clone()
		{
			ChartText chartText = (ChartText)base.MemberwiseClone();
			chartText.strings = this.strings.Clone();
			chartText.strings.Parent = chartText;
			return chartText;
		}

		// Token: 0x0600E5DD RID: 58845 RVA: 0x00330CD0 File Offset: 0x0032EED0
		private static string DropLineBreaks(string text)
		{
			text.Trim();
			string[] array = text.Split(new char[]
			{
				'\n',
				'\r'
			});
			StringBuilder stringBuilder = new StringBuilder();
			string value = " ";
			for (int i = 0; i < array.Length - 1; i++)
			{
				if (array[i].Length > 0)
				{
					stringBuilder.Append(array[i]);
					stringBuilder.Append(value);
				}
			}
			if (array[array.Length - 1].Length > 0)
			{
				stringBuilder.Append(array[array.Length - 1]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04004219 RID: 16921
		private ChartWord space;

		// Token: 0x0400421A RID: 16922
		private ChartStringCollection strings;

		// Token: 0x0400421B RID: 16923
		private string text;

		// Token: 0x0400421C RID: 16924
		private Font font;

		// Token: 0x0400421D RID: 16925
		private ChartGraphics graphics;
	}
}
