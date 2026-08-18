using System;
using System.Collections.Generic;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000165 RID: 357
	public class MultiColumnText : IElement
	{
		// Token: 0x06000D8A RID: 3466 RVA: 0x0004A253 File Offset: 0x00049253
		public MultiColumnText() : this(-1f)
		{
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0004A260 File Offset: 0x00049260
		public MultiColumnText(float height)
		{
			this.simple = true;
			this.nextY = -1f;
			base..ctor();
			this.columnDefs = new List<MultiColumnText.ColumnDef>();
			this.desiredHeight = height;
			this.top = -1f;
			this.columnText = new ColumnText(null);
			this.totalHeight = 0f;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0004A2BC File Offset: 0x000492BC
		public MultiColumnText(float top, float height)
		{
			this.simple = true;
			this.nextY = -1f;
			base..ctor();
			this.columnDefs = new List<MultiColumnText.ColumnDef>();
			this.desiredHeight = height;
			this.top = top;
			this.nextY = top;
			this.columnText = new ColumnText(null);
			this.totalHeight = 0f;
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0004A318 File Offset: 0x00049318
		public bool IsOverflow()
		{
			return this.overflow;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0004A320 File Offset: 0x00049320
		public void UseColumnParams(ColumnText sourceColumn)
		{
			this.columnText.SetSimpleVars(sourceColumn);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0004A330 File Offset: 0x00049330
		public void AddColumn(float[] left, float[] right)
		{
			MultiColumnText.ColumnDef columnDef = new MultiColumnText.ColumnDef(left, right, this);
			if (!columnDef.IsSimple())
			{
				this.simple = false;
			}
			this.columnDefs.Add(columnDef);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0004A364 File Offset: 0x00049364
		public void AddSimpleColumn(float left, float right)
		{
			MultiColumnText.ColumnDef item = new MultiColumnText.ColumnDef(left, right, this);
			this.columnDefs.Add(item);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0004A388 File Offset: 0x00049388
		public void AddRegularColumns(float left, float right, float gutterWidth, int numColumns)
		{
			float num = left;
			float num2 = right - left;
			float num3 = (num2 - gutterWidth * (float)(numColumns - 1)) / (float)numColumns;
			for (int i = 0; i < numColumns; i++)
			{
				this.AddSimpleColumn(num, num + num3);
				num += num3 + gutterWidth;
			}
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0004A3C6 File Offset: 0x000493C6
		public void AddText(Phrase phrase)
		{
			this.columnText.AddText(phrase);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0004A3D4 File Offset: 0x000493D4
		public void AddText(Chunk chunk)
		{
			this.columnText.AddText(chunk);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0004A3E4 File Offset: 0x000493E4
		public void AddElement(IElement element)
		{
			if (this.simple)
			{
				this.columnText.AddElement(element);
				return;
			}
			if (element is Phrase)
			{
				this.columnText.AddText((Phrase)element);
				return;
			}
			if (element is Chunk)
			{
				this.columnText.AddText((Chunk)element);
				return;
			}
			throw new DocumentException(MessageLocalization.GetComposedMessage("can.t.add.1.to.multicolumntext.with.complex.columns", element.GetType().ToString()));
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0004A454 File Offset: 0x00049454
		public float Write(PdfContentByte canvas, PdfDocument document, float documentY)
		{
			this.document = document;
			this.columnText.Canvas = canvas;
			if (this.columnDefs.Count == 0)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("multicolumntext.has.no.columns"));
			}
			this.overflow = false;
			float num = 0f;
			bool flag = false;
			while (!flag)
			{
				if (this.top == -1f)
				{
					this.top = document.GetVerticalPosition(true);
				}
				else if (this.nextY == -1f)
				{
					this.nextY = document.GetVerticalPosition(true);
				}
				MultiColumnText.ColumnDef columnDef = this.columnDefs[this.CurrentColumn];
				this.columnText.YLine = this.top;
				float[] array = columnDef.ResolvePositions(4);
				float[] array2 = columnDef.ResolvePositions(8);
				if (document.IsMarginMirroring() && document.PageNumber % 2 == 0)
				{
					float num2 = document.RightMargin - document.Left;
					array = (float[])array.Clone();
					array2 = (float[])array2.Clone();
					for (int i = 0; i < array.Length; i += 2)
					{
						array[i] -= num2;
					}
					for (int j = 0; j < array2.Length; j += 2)
					{
						array2[j] -= num2;
					}
				}
				num = Math.Max(num, this.GetHeight(array, array2));
				if (columnDef.IsSimple())
				{
					this.columnText.SetSimpleColumn(array[2], array[3], array2[0], array2[1]);
				}
				else
				{
					this.columnText.SetColumns(array, array2);
				}
				int num3 = this.columnText.Go();
				if ((num3 & 1) != 0)
				{
					flag = true;
					this.top = this.columnText.YLine;
				}
				else if (this.ShiftCurrentColumn())
				{
					this.top = this.nextY;
				}
				else
				{
					this.totalHeight += num;
					if (this.desiredHeight != -1f && this.totalHeight >= this.desiredHeight)
					{
						this.overflow = true;
						break;
					}
					documentY = this.nextY;
					this.NewPage();
					num = 0f;
				}
			}
			if (this.desiredHeight == -1f && this.columnDefs.Count == 1)
			{
				num = documentY - this.columnText.YLine;
			}
			return num;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0004A694 File Offset: 0x00049694
		private void NewPage()
		{
			this.ResetCurrentColumn();
			if (this.desiredHeight == -1f)
			{
				this.top = (this.nextY = -1f);
			}
			else
			{
				this.top = this.nextY;
			}
			this.totalHeight = 0f;
			if (this.document != null)
			{
				this.document.NewPage();
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0004A6F8 File Offset: 0x000496F8
		private float GetHeight(float[] left, float[] right)
		{
			float num = float.MinValue;
			float num2 = float.MaxValue;
			for (int i = 0; i < left.Length; i += 2)
			{
				num2 = Math.Min(num2, left[i + 1]);
				num = Math.Max(num, left[i + 1]);
			}
			for (int j = 0; j < right.Length; j += 2)
			{
				num2 = Math.Min(num2, right[j + 1]);
				num = Math.Max(num, right[j + 1]);
			}
			return num - num2;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0004A760 File Offset: 0x00049760
		public bool Process(IElementListener listener)
		{
			bool result;
			try
			{
				result = listener.Add(this);
			}
			catch (DocumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0004A790 File Offset: 0x00049790
		public int Type
		{
			get
			{
				return 40;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0004A794 File Offset: 0x00049794
		public List<Chunk> Chunks
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0004A797 File Offset: 0x00049797
		public bool IsContent()
		{
			return true;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0004A79A File Offset: 0x0004979A
		public bool IsNestable()
		{
			return false;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0004A79D File Offset: 0x0004979D
		private float GetColumnBottom()
		{
			if (this.desiredHeight == -1f)
			{
				return this.document.Bottom;
			}
			return Math.Max(this.top - (this.desiredHeight - this.totalHeight), this.document.Bottom);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0004A7DC File Offset: 0x000497DC
		public void NextColumn()
		{
			this.currentColumn = (this.currentColumn + 1) % this.columnDefs.Count;
			this.top = this.nextY;
			if (this.currentColumn == 0)
			{
				this.NewPage();
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0004A812 File Offset: 0x00049812
		public int CurrentColumn
		{
			get
			{
				if (this.columnsRightToLeft)
				{
					return this.columnDefs.Count - this.currentColumn - 1;
				}
				return this.currentColumn;
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0004A837 File Offset: 0x00049837
		public void ResetCurrentColumn()
		{
			this.currentColumn = 0;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0004A840 File Offset: 0x00049840
		public bool ShiftCurrentColumn()
		{
			if (this.currentColumn + 1 < this.columnDefs.Count)
			{
				this.currentColumn++;
				return true;
			}
			return false;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0004A868 File Offset: 0x00049868
		public void SetColumnsRightToLeft(bool direction)
		{
			this.columnsRightToLeft = direction;
		}

		// Token: 0x170002A0 RID: 672
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x0004A871 File Offset: 0x00049871
		public float SpaceCharRatio
		{
			set
			{
				this.columnText.SpaceCharRatio = value;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x0004A87F File Offset: 0x0004987F
		public int RunDirection
		{
			set
			{
				this.columnText.RunDirection = value;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x0004A88D File Offset: 0x0004988D
		public int ArabicOptions
		{
			set
			{
				this.columnText.ArabicOptions = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x0004A89B File Offset: 0x0004989B
		public int Alignment
		{
			set
			{
				this.columnText.Alignment = value;
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0004A8A9 File Offset: 0x000498A9
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x040009F9 RID: 2553
		public const float AUTOMATIC = -1f;

		// Token: 0x040009FA RID: 2554
		private float desiredHeight;

		// Token: 0x040009FB RID: 2555
		private float totalHeight;

		// Token: 0x040009FC RID: 2556
		private bool overflow;

		// Token: 0x040009FD RID: 2557
		private float top;

		// Token: 0x040009FE RID: 2558
		private ColumnText columnText;

		// Token: 0x040009FF RID: 2559
		private List<MultiColumnText.ColumnDef> columnDefs;

		// Token: 0x04000A00 RID: 2560
		private bool simple;

		// Token: 0x04000A01 RID: 2561
		private int currentColumn;

		// Token: 0x04000A02 RID: 2562
		private float nextY;

		// Token: 0x04000A03 RID: 2563
		private bool columnsRightToLeft;

		// Token: 0x04000A04 RID: 2564
		private PdfDocument document;

		// Token: 0x02000166 RID: 358
		internal class ColumnDef
		{
			// Token: 0x06000DA8 RID: 3496 RVA: 0x0004A8B1 File Offset: 0x000498B1
			internal ColumnDef(float[] newLeft, float[] newRight, MultiColumnText mc)
			{
				this.mc = mc;
				this.left = newLeft;
				this.right = newRight;
			}

			// Token: 0x06000DA9 RID: 3497 RVA: 0x0004A8D0 File Offset: 0x000498D0
			internal ColumnDef(float leftPosition, float rightPosition, MultiColumnText mc)
			{
				this.mc = mc;
				this.left = new float[4];
				this.left[0] = leftPosition;
				this.left[1] = mc.top;
				this.left[2] = leftPosition;
				if (mc.desiredHeight == -1f || mc.top == -1f)
				{
					this.left[3] = -1f;
				}
				else
				{
					this.left[3] = mc.top - mc.desiredHeight;
				}
				this.right = new float[4];
				this.right[0] = rightPosition;
				this.right[1] = mc.top;
				this.right[2] = rightPosition;
				if (mc.desiredHeight == -1f || mc.top == -1f)
				{
					this.right[3] = -1f;
					return;
				}
				this.right[3] = mc.top - mc.desiredHeight;
			}

			// Token: 0x06000DAA RID: 3498 RVA: 0x0004A9BD File Offset: 0x000499BD
			internal float[] ResolvePositions(int side)
			{
				if (side == 4)
				{
					return this.ResolvePositions(this.left);
				}
				return this.ResolvePositions(this.right);
			}

			// Token: 0x06000DAB RID: 3499 RVA: 0x0004A9DC File Offset: 0x000499DC
			internal float[] ResolvePositions(float[] positions)
			{
				if (!this.IsSimple())
				{
					positions[1] = this.mc.top;
					return positions;
				}
				if (this.mc.top == -1f)
				{
					throw new Exception("resolvePositions called with top=AUTOMATIC (-1).  Top position must be set befure lines can be resolved");
				}
				positions[1] = this.mc.top;
				positions[3] = this.mc.GetColumnBottom();
				return positions;
			}

			// Token: 0x06000DAC RID: 3500 RVA: 0x0004AA3B File Offset: 0x00049A3B
			internal bool IsSimple()
			{
				return this.left.Length == 4 && this.right.Length == 4 && this.left[0] == this.left[2] && this.right[0] == this.right[2];
			}

			// Token: 0x04000A05 RID: 2565
			private float[] left;

			// Token: 0x04000A06 RID: 2566
			private float[] right;

			// Token: 0x04000A07 RID: 2567
			private MultiColumnText mc;
		}
	}
}
