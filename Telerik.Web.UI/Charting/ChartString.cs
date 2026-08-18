using System;
using System.Text;

namespace Telerik.Charting
{
	// Token: 0x02001719 RID: 5913
	internal class ChartString
	{
		// Token: 0x170045F2 RID: 17906
		// (get) Token: 0x0600E5B0 RID: 58800 RVA: 0x00330225 File Offset: 0x0032E425
		// (set) Token: 0x0600E5B1 RID: 58801 RVA: 0x0033022D File Offset: 0x0032E42D
		internal bool IsFirst
		{
			get
			{
				return this.isFirst;
			}
			set
			{
				this.isFirst = value;
			}
		}

		// Token: 0x170045F3 RID: 17907
		// (get) Token: 0x0600E5B2 RID: 58802 RVA: 0x00330236 File Offset: 0x0032E436
		// (set) Token: 0x0600E5B3 RID: 58803 RVA: 0x0033023E File Offset: 0x0032E43E
		internal bool IsLast
		{
			get
			{
				return this.isLast;
			}
			set
			{
				this.isLast = value;
			}
		}

		// Token: 0x170045F4 RID: 17908
		// (get) Token: 0x0600E5B4 RID: 58804 RVA: 0x00330247 File Offset: 0x0032E447
		// (set) Token: 0x0600E5B5 RID: 58805 RVA: 0x0033024F File Offset: 0x0032E44F
		internal ChartStringCollection Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x170045F5 RID: 17909
		// (get) Token: 0x0600E5B6 RID: 58806 RVA: 0x00330258 File Offset: 0x0032E458
		internal ChartString NextString
		{
			get
			{
				return this.parent.GetNext(this);
			}
		}

		// Token: 0x170045F6 RID: 17910
		// (get) Token: 0x0600E5B7 RID: 58807 RVA: 0x00330266 File Offset: 0x0032E466
		internal ChartString Previous
		{
			get
			{
				return this.parent.GetPrevious(this);
			}
		}

		// Token: 0x170045F7 RID: 17911
		// (get) Token: 0x0600E5B8 RID: 58808 RVA: 0x00330274 File Offset: 0x0032E474
		internal float Width
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x170045F8 RID: 17912
		// (get) Token: 0x0600E5B9 RID: 58809 RVA: 0x0033027C File Offset: 0x0032E47C
		internal float Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x170045F9 RID: 17913
		// (get) Token: 0x0600E5BA RID: 58810 RVA: 0x00330284 File Offset: 0x0032E484
		// (set) Token: 0x0600E5BB RID: 58811 RVA: 0x0033028C File Offset: 0x0032E48C
		internal ChartWordCollection Words
		{
			get
			{
				return this.words;
			}
			set
			{
				this.words = value;
			}
		}

		// Token: 0x0600E5BC RID: 58812 RVA: 0x00330295 File Offset: 0x0032E495
		internal ChartString()
		{
			this.isFirst = false;
			this.isLast = false;
			this.words = new ChartWordCollection(this);
			this.height = 0f;
			this.width = 0f;
		}

		// Token: 0x0600E5BD RID: 58813 RVA: 0x003302CD File Offset: 0x0032E4CD
		internal ChartString(float height) : this()
		{
			this.height = height;
		}

		// Token: 0x0600E5BE RID: 58814 RVA: 0x003302DC File Offset: 0x0032E4DC
		internal void WidthCalculate()
		{
			ChartWord space = this.parent.Parent.Space;
			float num = 0f;
			foreach (object obj in this.words)
			{
				ChartWord chartWord = (ChartWord)obj;
				num += chartWord.Width;
			}
			num += Math.Max(0f, space.Width * (float)(this.words.Count - 1));
			this.width = num;
		}

		// Token: 0x0600E5BF RID: 58815 RVA: 0x0033037C File Offset: 0x0032E57C
		internal void MoveLastWordToNextString()
		{
			ChartWord chartWord = this.Words.RemoveLast();
			this.width -= chartWord.Width;
			this.width -= this.parent.Parent.Space.Width;
			this.NextString.Words.InsertAsFirst(chartWord);
			this.NextString.width += chartWord.Width;
			this.NextString.width += this.parent.Parent.Space.Width;
		}

		// Token: 0x0600E5C0 RID: 58816 RVA: 0x0033041C File Offset: 0x0032E61C
		internal ChartString Clone()
		{
			ChartString chartString = (ChartString)base.MemberwiseClone();
			chartString.words = this.words.Clone();
			this.words.Parent = chartString;
			return chartString;
		}

		// Token: 0x0600E5C1 RID: 58817 RVA: 0x00330454 File Offset: 0x0032E654
		public override string ToString()
		{
			if (this.words.Count < 1)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.words.Count - 1; i++)
			{
				stringBuilder.Append(this.words[i].Text);
				stringBuilder.Append(" ");
			}
			stringBuilder.Append(this.words[this.words.Count - 1].Text);
			return stringBuilder.ToString();
		}

		// Token: 0x04004212 RID: 16914
		private bool isFirst;

		// Token: 0x04004213 RID: 16915
		private bool isLast;

		// Token: 0x04004214 RID: 16916
		private ChartStringCollection parent;

		// Token: 0x04004215 RID: 16917
		private ChartWordCollection words;

		// Token: 0x04004216 RID: 16918
		private float height;

		// Token: 0x04004217 RID: 16919
		private float width;
	}
}
