using System;
using System.Collections.Generic;
using System.Text;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x0200041B RID: 1051
	public class LocationTextExtractionStrategy : ITextExtractionStrategy, IRenderListener
	{
		// Token: 0x060023C7 RID: 9159 RVA: 0x000DAB13 File Offset: 0x000D9B13
		public void BeginTextBlock()
		{
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000DAB15 File Offset: 0x000D9B15
		public void EndTextBlock()
		{
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000DAB18 File Offset: 0x000D9B18
		public string GetResultantText()
		{
			if (LocationTextExtractionStrategy.DUMP_STATE)
			{
				this.DumpState();
			}
			this.locationalResult.Sort();
			StringBuilder stringBuilder = new StringBuilder();
			LocationTextExtractionStrategy.TextChunk textChunk = null;
			foreach (LocationTextExtractionStrategy.TextChunk textChunk2 in this.locationalResult)
			{
				if (textChunk == null)
				{
					stringBuilder.Append(textChunk2.text);
				}
				else if (textChunk2.SameLine(textChunk))
				{
					float num = textChunk2.DistanceFromEndOf(textChunk);
					if (num < -textChunk2.charSpaceWidth)
					{
						stringBuilder.Append(' ');
					}
					else if (num > textChunk2.charSpaceWidth / 2f && textChunk2.text[0] != ' ' && textChunk.text[textChunk.text.Length - 1] != ' ')
					{
						stringBuilder.Append(' ');
					}
					stringBuilder.Append(textChunk2.text);
				}
				else
				{
					stringBuilder.Append('\n');
					stringBuilder.Append(textChunk2.text);
				}
				textChunk = textChunk2;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000DAC38 File Offset: 0x000D9C38
		private void DumpState()
		{
			foreach (LocationTextExtractionStrategy.TextChunk textChunk in this.locationalResult)
			{
				textChunk.PrintDiagnostics();
				Console.WriteLine();
			}
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000DAC90 File Offset: 0x000D9C90
		public void RenderText(TextRenderInfo renderInfo)
		{
			LineSegment baseline = renderInfo.GetBaseline();
			LocationTextExtractionStrategy.TextChunk item = new LocationTextExtractionStrategy.TextChunk(renderInfo.GetText(), baseline.GetStartPoint(), baseline.GetEndPoint(), renderInfo.GetSingleSpaceWidth());
			this.locationalResult.Add(item);
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000DACCE File Offset: 0x000D9CCE
		public void RenderImage(ImageRenderInfo renderInfo)
		{
		}

		// Token: 0x0400189C RID: 6300
		public static bool DUMP_STATE;

		// Token: 0x0400189D RID: 6301
		private List<LocationTextExtractionStrategy.TextChunk> locationalResult = new List<LocationTextExtractionStrategy.TextChunk>();

		// Token: 0x0200041C RID: 1052
		private class TextChunk : IComparable<LocationTextExtractionStrategy.TextChunk>
		{
			// Token: 0x060023CD RID: 9165 RVA: 0x000DACD0 File Offset: 0x000D9CD0
			public TextChunk(string str, Vector startLocation, Vector endLocation, float charSpaceWidth)
			{
				this.text = str;
				this.startLocation = startLocation;
				this.endLocation = endLocation;
				this.charSpaceWidth = charSpaceWidth;
				this.orientationVector = endLocation.Subtract(startLocation).Normalize();
				this.orientationMagnitude = (int)(Math.Atan2((double)this.orientationVector[1], (double)this.orientationVector[0]) * 1000.0);
				Vector v = new Vector(0f, 0f, 1f);
				this.distPerpendicular = (int)startLocation.Subtract(v).Cross(this.orientationVector)[2];
				this.distParallelStart = this.orientationVector.Dot(startLocation);
				this.distParallelEnd = this.orientationVector.Dot(endLocation);
			}

			// Token: 0x060023CE RID: 9166 RVA: 0x000DAD9C File Offset: 0x000D9D9C
			public void PrintDiagnostics()
			{
				Console.WriteLine(string.Concat(new object[]
				{
					"Text (@",
					this.startLocation,
					" -> ",
					this.endLocation,
					"): ",
					this.text
				}));
				Console.WriteLine("orientationMagnitude: " + this.orientationMagnitude);
				Console.WriteLine("distPerpendicular: " + this.distPerpendicular);
				Console.WriteLine("distParallel: " + this.distParallelStart);
			}

			// Token: 0x060023CF RID: 9167 RVA: 0x000DAE3C File Offset: 0x000D9E3C
			public bool SameLine(LocationTextExtractionStrategy.TextChunk a)
			{
				return this.orientationMagnitude == a.orientationMagnitude && this.distPerpendicular == a.distPerpendicular;
			}

			// Token: 0x060023D0 RID: 9168 RVA: 0x000DAE60 File Offset: 0x000D9E60
			public float DistanceFromEndOf(LocationTextExtractionStrategy.TextChunk other)
			{
				return this.distParallelStart - other.distParallelEnd;
			}

			// Token: 0x060023D1 RID: 9169 RVA: 0x000DAE7C File Offset: 0x000D9E7C
			public int CompareTo(LocationTextExtractionStrategy.TextChunk rhs)
			{
				if (this == rhs)
				{
					return 0;
				}
				int num = LocationTextExtractionStrategy.TextChunk.CompareInts(this.orientationMagnitude, rhs.orientationMagnitude);
				if (num != 0)
				{
					return num;
				}
				num = LocationTextExtractionStrategy.TextChunk.CompareInts(this.distPerpendicular, rhs.distPerpendicular);
				if (num != 0)
				{
					return num;
				}
				return (this.distParallelStart < rhs.distParallelStart) ? -1 : 1;
			}

			// Token: 0x060023D2 RID: 9170 RVA: 0x000DAED1 File Offset: 0x000D9ED1
			private static int CompareInts(int int1, int int2)
			{
				if (int1 == int2)
				{
					return 0;
				}
				if (int1 >= int2)
				{
					return 1;
				}
				return -1;
			}

			// Token: 0x0400189E RID: 6302
			internal string text;

			// Token: 0x0400189F RID: 6303
			internal Vector startLocation;

			// Token: 0x040018A0 RID: 6304
			internal Vector endLocation;

			// Token: 0x040018A1 RID: 6305
			internal Vector orientationVector;

			// Token: 0x040018A2 RID: 6306
			internal int orientationMagnitude;

			// Token: 0x040018A3 RID: 6307
			internal int distPerpendicular;

			// Token: 0x040018A4 RID: 6308
			internal float distParallelStart;

			// Token: 0x040018A5 RID: 6309
			internal float distParallelEnd;

			// Token: 0x040018A6 RID: 6310
			internal float charSpaceWidth;
		}
	}
}
