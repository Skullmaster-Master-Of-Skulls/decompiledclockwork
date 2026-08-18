using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x020005D5 RID: 1493
	public class PdfContentStreamProcessor
	{
		// Token: 0x0600338B RID: 13195 RVA: 0x0013FD7C File Offset: 0x0013ED7C
		public PdfContentStreamProcessor(IRenderListener renderListener)
		{
			this.renderListener = renderListener;
			this.operators = new Dictionary<string, IContentOperator>();
			this.PopulateOperators();
			this.xobjectDoHandlers = new Dictionary<PdfName, IXObjectDoHandler>();
			this.PopulateXObjectDoHandlers();
			this.Reset();
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x0013FDD4 File Offset: 0x0013EDD4
		private void PopulateXObjectDoHandlers()
		{
			this.RegisterXObjectDoHandler(PdfName.DEFAULT, new PdfContentStreamProcessor.IgnoreXObjectDoHandler());
			this.RegisterXObjectDoHandler(PdfName.FORM, new PdfContentStreamProcessor.FormXObjectDoHandler());
			this.RegisterXObjectDoHandler(PdfName.IMAGE, new PdfContentStreamProcessor.ImageXObjectDoHandler());
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x0013FE0C File Offset: 0x0013EE0C
		public IXObjectDoHandler RegisterXObjectDoHandler(PdfName xobjectSubType, IXObjectDoHandler handler)
		{
			IXObjectDoHandler result;
			this.xobjectDoHandlers.TryGetValue(xobjectSubType, out result);
			this.xobjectDoHandlers[xobjectSubType] = handler;
			return result;
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x0013FE38 File Offset: 0x0013EE38
		private void PopulateOperators()
		{
			this.RegisterContentOperator("DefaultOperator", new PdfContentStreamProcessor.IgnoreOperatorContentOperator());
			this.RegisterContentOperator("q", new PdfContentStreamProcessor.PushGraphicsState());
			this.RegisterContentOperator("Q", new PdfContentStreamProcessor.PopGraphicsState());
			this.RegisterContentOperator("cm", new PdfContentStreamProcessor.ModifyCurrentTransformationMatrix());
			this.RegisterContentOperator("gs", new PdfContentStreamProcessor.ProcessGraphicsStateResource());
			PdfContentStreamProcessor.SetTextCharacterSpacing setTextCharacterSpacing = new PdfContentStreamProcessor.SetTextCharacterSpacing();
			this.RegisterContentOperator("Tc", setTextCharacterSpacing);
			PdfContentStreamProcessor.SetTextWordSpacing setTextWordSpacing = new PdfContentStreamProcessor.SetTextWordSpacing();
			this.RegisterContentOperator("Tw", setTextWordSpacing);
			this.RegisterContentOperator("Tz", new PdfContentStreamProcessor.SetTextHorizontalScaling());
			PdfContentStreamProcessor.SetTextLeading setTextLeading = new PdfContentStreamProcessor.SetTextLeading();
			this.RegisterContentOperator("TL", setTextLeading);
			this.RegisterContentOperator("Tf", new PdfContentStreamProcessor.SetTextFont());
			this.RegisterContentOperator("Tr", new PdfContentStreamProcessor.SetTextRenderMode());
			this.RegisterContentOperator("Ts", new PdfContentStreamProcessor.SetTextRise());
			this.RegisterContentOperator("BT", new PdfContentStreamProcessor.BeginTextC());
			this.RegisterContentOperator("ET", new PdfContentStreamProcessor.EndTextC());
			this.RegisterContentOperator("BMC", new PdfContentStreamProcessor.BeginMarkedContentC());
			this.RegisterContentOperator("BDC", new PdfContentStreamProcessor.BeginMarkedContentDictionary());
			this.RegisterContentOperator("EMC", new PdfContentStreamProcessor.EndMarkedContentC());
			PdfContentStreamProcessor.TextMoveStartNextLine textMoveStartNextLine = new PdfContentStreamProcessor.TextMoveStartNextLine();
			this.RegisterContentOperator("Td", textMoveStartNextLine);
			this.RegisterContentOperator("TD", new PdfContentStreamProcessor.TextMoveStartNextLineWithLeading(textMoveStartNextLine, setTextLeading));
			this.RegisterContentOperator("Tm", new PdfContentStreamProcessor.TextSetTextMatrix());
			PdfContentStreamProcessor.TextMoveNextLine textMoveNextLine = new PdfContentStreamProcessor.TextMoveNextLine(textMoveStartNextLine);
			this.RegisterContentOperator("T*", textMoveNextLine);
			PdfContentStreamProcessor.ShowText showText = new PdfContentStreamProcessor.ShowText();
			this.RegisterContentOperator("Tj", new PdfContentStreamProcessor.ShowText());
			PdfContentStreamProcessor.MoveNextLineAndShowText moveNextLineAndShowText = new PdfContentStreamProcessor.MoveNextLineAndShowText(textMoveNextLine, showText);
			this.RegisterContentOperator("'", moveNextLineAndShowText);
			this.RegisterContentOperator("\"", new PdfContentStreamProcessor.MoveNextLineAndShowTextWithSpacing(setTextWordSpacing, setTextCharacterSpacing, moveNextLineAndShowText));
			this.RegisterContentOperator("TJ", new PdfContentStreamProcessor.ShowTextArray());
			this.RegisterContentOperator("Do", new PdfContentStreamProcessor.Do());
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x00140024 File Offset: 0x0013F024
		public IContentOperator RegisterContentOperator(string operatorString, IContentOperator oper)
		{
			this.operators[operatorString] = oper;
			return oper;
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x00140041 File Offset: 0x0013F041
		public void Reset()
		{
			this.gsStack.Clear();
			this.gsStack.Push(new GraphicsState());
			this.textMatrix = null;
			this.textLineMatrix = null;
			this.resources = new PdfContentStreamProcessor.ResourceDictionary();
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x00140077 File Offset: 0x0013F077
		private GraphicsState Gs()
		{
			return this.gsStack.Peek();
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x00140084 File Offset: 0x0013F084
		private void InvokeOperator(PdfLiteral oper, List<PdfObject> operands)
		{
			IContentOperator contentOperator;
			this.operators.TryGetValue(oper.ToString(), out contentOperator);
			if (contentOperator == null)
			{
				contentOperator = this.operators["DefaultOperator"];
			}
			contentOperator.Invoke(this, oper, operands);
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x001400C2 File Offset: 0x0013F0C2
		private void BeginMarkedContent(PdfName tag, PdfDictionary dict)
		{
			this.markedContentStack.Push(new MarkedContentInfo(tag, dict));
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x001400D6 File Offset: 0x0013F0D6
		private void EndMarkedContent()
		{
			this.markedContentStack.Pop();
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x001400E4 File Offset: 0x0013F0E4
		private string Decode(PdfString inp)
		{
			byte[] bytes = inp.GetBytes();
			return this.Gs().font.Decode(bytes, 0, bytes.Length);
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x0014010D File Offset: 0x0013F10D
		private void BeginText()
		{
			this.renderListener.BeginTextBlock();
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x0014011A File Offset: 0x0013F11A
		private void EndText()
		{
			this.renderListener.EndTextBlock();
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x00140128 File Offset: 0x0013F128
		private void DisplayPdfString(PdfString str)
		{
			string text = this.Decode(str);
			TextRenderInfo textRenderInfo = new TextRenderInfo(text, this.Gs(), this.textMatrix, this.markedContentStack);
			this.renderListener.RenderText(textRenderInfo);
			this.textMatrix = new Matrix(textRenderInfo.GetUnscaledWidth(), 0f).Multiply(this.textMatrix);
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x00140184 File Offset: 0x0013F184
		private void DisplayXObject(PdfName xobjectName)
		{
			PdfDictionary asDict = this.resources.GetAsDict(PdfName.XOBJECT);
			PdfObject directObject = asDict.GetDirectObject(xobjectName);
			PdfStream pdfStream = (PdfStream)directObject;
			PdfName asName = pdfStream.GetAsName(PdfName.SUBTYPE);
			if (directObject.IsStream())
			{
				IXObjectDoHandler ixobjectDoHandler;
				this.xobjectDoHandlers.TryGetValue(asName, out ixobjectDoHandler);
				if (ixobjectDoHandler == null)
				{
					ixobjectDoHandler = this.xobjectDoHandlers[PdfName.DEFAULT];
				}
				ixobjectDoHandler.HandleXObject(this, pdfStream, asDict.GetAsIndirectObject(xobjectName));
				return;
			}
			throw new InvalidOperationException(MessageLocalization.GetComposedMessage("XObject.1.is.not.a.stream", xobjectName));
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x0014020C File Offset: 0x0013F20C
		private void ApplyTextAdjust(float tj)
		{
			float tx = -tj / 1000f * this.Gs().fontSize * this.Gs().horizontalScaling;
			this.textMatrix = new Matrix(tx, 0f).Multiply(this.textMatrix);
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x00140258 File Offset: 0x0013F258
		public void ProcessContent(byte[] contentBytes, PdfDictionary resources)
		{
			this.resources.Push(resources);
			PRTokeniser prtokeniser = new PRTokeniser(contentBytes);
			PdfContentParser pdfContentParser = new PdfContentParser(prtokeniser);
			List<PdfObject> list = new List<PdfObject>();
			while (pdfContentParser.Parse(list).Count > 0)
			{
				PdfLiteral pdfLiteral = (PdfLiteral)list[list.Count - 1];
				if ("ID".Equals(pdfLiteral.ToString()))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					int num = 0;
					int num2;
					while ((num2 = prtokeniser.Read()) != -1)
					{
						if (num == 0 && PRTokeniser.IsWhitespace(num2))
						{
							num++;
							memoryStream2.WriteByte((byte)num2);
						}
						else if (num == 1 && num2 == 69)
						{
							num++;
							memoryStream2.WriteByte((byte)num2);
						}
						else if (num == 2 && num2 == 73)
						{
							num++;
							memoryStream2.WriteByte((byte)num2);
						}
						else
						{
							if (num == 3 && PRTokeniser.IsWhitespace(num2))
							{
								list = new List<PdfObject>();
								list.Add(new PdfLiteral("ID"));
								this.InvokeOperator((PdfLiteral)list[list.Count - 1], list);
								list = new List<PdfObject>();
								list.Add(new PdfLiteral("EI"));
								this.InvokeOperator((PdfLiteral)list[list.Count - 1], list);
								break;
							}
							memoryStream2.WriteTo(memoryStream);
							memoryStream2.SetLength(0L);
							memoryStream.WriteByte((byte)num2);
							num = 0;
						}
					}
				}
				this.InvokeOperator(pdfLiteral, list);
			}
			this.resources.Pop();
		}

		// Token: 0x040022E2 RID: 8930
		public const string DEFAULTOPERATOR = "DefaultOperator";

		// Token: 0x040022E3 RID: 8931
		private IDictionary<string, IContentOperator> operators;

		// Token: 0x040022E4 RID: 8932
		private PdfContentStreamProcessor.ResourceDictionary resources;

		// Token: 0x040022E5 RID: 8933
		private Stack<GraphicsState> gsStack = new Stack<GraphicsState>();

		// Token: 0x040022E6 RID: 8934
		private Matrix textMatrix;

		// Token: 0x040022E7 RID: 8935
		private Matrix textLineMatrix;

		// Token: 0x040022E8 RID: 8936
		private IRenderListener renderListener;

		// Token: 0x040022E9 RID: 8937
		private IDictionary<PdfName, IXObjectDoHandler> xobjectDoHandlers;

		// Token: 0x040022EA RID: 8938
		private Stack<MarkedContentInfo> markedContentStack = new Stack<MarkedContentInfo>();

		// Token: 0x020005D6 RID: 1494
		private class ResourceDictionary : PdfDictionary
		{
			// Token: 0x0600339D RID: 13213 RVA: 0x00140401 File Offset: 0x0013F401
			public void Push(PdfDictionary resources)
			{
				this.resourcesStack.Add(resources);
			}

			// Token: 0x0600339E RID: 13214 RVA: 0x0014040F File Offset: 0x0013F40F
			public void Pop()
			{
				this.resourcesStack.RemoveAt(this.resourcesStack.Count - 1);
			}

			// Token: 0x0600339F RID: 13215 RVA: 0x0014042C File Offset: 0x0013F42C
			public override PdfObject GetDirectObject(PdfName key)
			{
				for (int i = this.resourcesStack.Count - 1; i >= 0; i--)
				{
					PdfDictionary pdfDictionary = this.resourcesStack[i];
					if (pdfDictionary != null)
					{
						PdfObject directObject = pdfDictionary.GetDirectObject(key);
						if (directObject != null)
						{
							return directObject;
						}
					}
				}
				return base.GetDirectObject(key);
			}

			// Token: 0x040022EB RID: 8939
			private IList<PdfDictionary> resourcesStack = new List<PdfDictionary>();
		}

		// Token: 0x020005D7 RID: 1495
		private class IgnoreOperatorContentOperator : IContentOperator
		{
			// Token: 0x060033A0 RID: 13216 RVA: 0x00140475 File Offset: 0x0013F475
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
			}
		}

		// Token: 0x020005D8 RID: 1496
		private class ShowTextArray : IContentOperator
		{
			// Token: 0x060033A2 RID: 13218 RVA: 0x00140480 File Offset: 0x0013F480
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				PdfArray pdfArray = (PdfArray)operands[0];
				foreach (PdfObject pdfObject in pdfArray.ArrayList)
				{
					if (pdfObject is PdfString)
					{
						processor.DisplayPdfString((PdfString)pdfObject);
					}
					else
					{
						float floatValue = ((PdfNumber)pdfObject).FloatValue;
						processor.ApplyTextAdjust(floatValue);
					}
				}
			}
		}

		// Token: 0x020005D9 RID: 1497
		private class MoveNextLineAndShowTextWithSpacing : IContentOperator
		{
			// Token: 0x060033A4 RID: 13220 RVA: 0x00140518 File Offset: 0x0013F518
			public MoveNextLineAndShowTextWithSpacing(PdfContentStreamProcessor.SetTextWordSpacing setTextWordSpacing, PdfContentStreamProcessor.SetTextCharacterSpacing setTextCharacterSpacing, PdfContentStreamProcessor.MoveNextLineAndShowText moveNextLineAndShowText)
			{
				this.setTextWordSpacing = setTextWordSpacing;
				this.setTextCharacterSpacing = setTextCharacterSpacing;
				this.moveNextLineAndShowText = moveNextLineAndShowText;
			}

			// Token: 0x060033A5 RID: 13221 RVA: 0x00140538 File Offset: 0x0013F538
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				PdfNumber item = (PdfNumber)operands[0];
				PdfNumber item2 = (PdfNumber)operands[1];
				PdfString item3 = (PdfString)operands[2];
				List<PdfObject> list = new List<PdfObject>(1);
				list.Insert(0, item);
				this.setTextWordSpacing.Invoke(processor, null, list);
				List<PdfObject> list2 = new List<PdfObject>(1);
				list2.Insert(0, item2);
				this.setTextCharacterSpacing.Invoke(processor, null, list2);
				List<PdfObject> list3 = new List<PdfObject>(1);
				list3.Insert(0, item3);
				this.moveNextLineAndShowText.Invoke(processor, null, list3);
			}

			// Token: 0x040022EC RID: 8940
			private PdfContentStreamProcessor.SetTextWordSpacing setTextWordSpacing;

			// Token: 0x040022ED RID: 8941
			private PdfContentStreamProcessor.SetTextCharacterSpacing setTextCharacterSpacing;

			// Token: 0x040022EE RID: 8942
			private PdfContentStreamProcessor.MoveNextLineAndShowText moveNextLineAndShowText;
		}

		// Token: 0x020005DA RID: 1498
		private class MoveNextLineAndShowText : IContentOperator
		{
			// Token: 0x060033A6 RID: 13222 RVA: 0x001405C9 File Offset: 0x0013F5C9
			public MoveNextLineAndShowText(PdfContentStreamProcessor.TextMoveNextLine textMoveNextLine, PdfContentStreamProcessor.ShowText showText)
			{
				this.textMoveNextLine = textMoveNextLine;
				this.showText = showText;
			}

			// Token: 0x060033A7 RID: 13223 RVA: 0x001405DF File Offset: 0x0013F5DF
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				this.textMoveNextLine.Invoke(processor, null, new List<PdfObject>(0));
				this.showText.Invoke(processor, null, operands);
			}

			// Token: 0x040022EF RID: 8943
			private PdfContentStreamProcessor.TextMoveNextLine textMoveNextLine;

			// Token: 0x040022F0 RID: 8944
			private PdfContentStreamProcessor.ShowText showText;
		}

		// Token: 0x020005DB RID: 1499
		private class ShowText : IContentOperator
		{
			// Token: 0x060033A8 RID: 13224 RVA: 0x00140604 File Offset: 0x0013F604
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				PdfString str = (PdfString)operands[0];
				processor.DisplayPdfString(str);
			}
		}

		// Token: 0x020005DC RID: 1500
		private class TextMoveNextLine : IContentOperator
		{
			// Token: 0x060033AA RID: 13226 RVA: 0x0014062D File Offset: 0x0013F62D
			public TextMoveNextLine(PdfContentStreamProcessor.TextMoveStartNextLine moveStartNextLine)
			{
				this.moveStartNextLine = moveStartNextLine;
			}

			// Token: 0x060033AB RID: 13227 RVA: 0x0014063C File Offset: 0x0013F63C
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				List<PdfObject> list = new List<PdfObject>(2);
				list.Insert(0, new PdfNumber(0));
				list.Insert(1, new PdfNumber(-processor.Gs().leading));
				this.moveStartNextLine.Invoke(processor, null, list);
			}

			// Token: 0x040022F1 RID: 8945
			private PdfContentStreamProcessor.TextMoveStartNextLine moveStartNextLine;
		}

		// Token: 0x020005DD RID: 1501
		private class TextSetTextMatrix : IContentOperator
		{
			// Token: 0x060033AC RID: 13228 RVA: 0x00140684 File Offset: 0x0013F684
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				float floatValue = ((PdfNumber)operands[0]).FloatValue;
				float floatValue2 = ((PdfNumber)operands[1]).FloatValue;
				float floatValue3 = ((PdfNumber)operands[2]).FloatValue;
				float floatValue4 = ((PdfNumber)operands[3]).FloatValue;
				float floatValue5 = ((PdfNumber)operands[4]).FloatValue;
				float floatValue6 = ((PdfNumber)operands[5]).FloatValue;
				processor.textLineMatrix = new Matrix(floatValue, floatValue2, floatValue3, floatValue4, floatValue5, floatValue6);
				processor.textMatrix = processor.textLineMatrix;
			}
		}

		// Token: 0x020005DE RID: 1502
		private class TextMoveStartNextLineWithLeading : IContentOperator
		{
			// Token: 0x060033AE RID: 13230 RVA: 0x00140726 File Offset: 0x0013F726
			public TextMoveStartNextLineWithLeading(PdfContentStreamProcessor.TextMoveStartNextLine moveStartNextLine, PdfContentStreamProcessor.SetTextLeading setTextLeading)
			{
				this.moveStartNextLine = moveStartNextLine;
				this.setTextLeading = setTextLeading;
			}

			// Token: 0x060033AF RID: 13231 RVA: 0x0014073C File Offset: 0x0013F73C
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				float floatValue = ((PdfNumber)operands[1]).FloatValue;
				List<PdfObject> list = new List<PdfObject>(1);
				list.Insert(0, new PdfNumber(-floatValue));
				this.setTextLeading.Invoke(processor, null, list);
				this.moveStartNextLine.Invoke(processor, null, operands);
			}

			// Token: 0x040022F2 RID: 8946
			private PdfContentStreamProcessor.TextMoveStartNextLine moveStartNextLine;

			// Token: 0x040022F3 RID: 8947
			private PdfContentStreamProcessor.SetTextLeading setTextLeading;
		}

		// Token: 0x020005DF RID: 1503
		private class TextMoveStartNextLine : IContentOperator
		{
			// Token: 0x060033B0 RID: 13232 RVA: 0x0014078C File Offset: 0x0013F78C
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				float floatValue = ((PdfNumber)operands[0]).FloatValue;
				float floatValue2 = ((PdfNumber)operands[1]).FloatValue;
				Matrix matrix = new Matrix(floatValue, floatValue2);
				processor.textMatrix = matrix.Multiply(processor.textLineMatrix);
				processor.textLineMatrix = processor.textMatrix;
			}
		}

		// Token: 0x020005E0 RID: 1504
		private class SetTextFont : IContentOperator
		{
			// Token: 0x060033B2 RID: 13234 RVA: 0x001407EC File Offset: 0x0013F7EC
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				PdfName key = (PdfName)operands[0];
				float floatValue = ((PdfNumber)operands[1]).FloatValue;
				PdfDictionary asDict = processor.resources.GetAsDict(PdfName.FONT);
				CMapAwareDocumentFont font = new CMapAwareDocumentFont((PRIndirectReference)asDict.Get(key));
				processor.Gs().font = font;
				processor.Gs().fontSize = floatValue;
			}
		}

		// Token: 0x020005E1 RID: 1505
		private class SetTextRenderMode : IContentOperator
		{
			// Token: 0x060033B4 RID: 13236 RVA: 0x0014085C File Offset: 0x0013F85C
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				PdfNumber pdfNumber = (PdfNumber)operands[0];
				processor.Gs().renderMode = pdfNumber.IntValue;
			}
		}

		// Token: 0x020005E2 RID: 1506
		private class SetTextRise : IContentOperator
		{
			// Token: 0x060033B6 RID: 13238 RVA: 0x00140890 File Offset: 0x0013F890
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				PdfNumber pdfNumber = (PdfNumber)operands[0];
				processor.Gs().rise = pdfNumber.FloatValue;
			}
		}

		// Token: 0x020005E3 RID: 1507
		private class SetTextLeading : IContentOperator
		{
			// Token: 0x060033B8 RID: 13240 RVA: 0x001408C4 File Offset: 0x0013F8C4
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				PdfNumber pdfNumber = (PdfNumber)operands[0];
				processor.Gs().leading = pdfNumber.FloatValue;
			}
		}

		// Token: 0x020005E4 RID: 1508
		private class SetTextHorizontalScaling : IContentOperator
		{
			// Token: 0x060033BA RID: 13242 RVA: 0x001408F8 File Offset: 0x0013F8F8
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				PdfNumber pdfNumber = (PdfNumber)operands[0];
				processor.Gs().horizontalScaling = pdfNumber.FloatValue / 100f;
			}
		}

		// Token: 0x020005E5 RID: 1509
		private class SetTextCharacterSpacing : IContentOperator
		{
			// Token: 0x060033BC RID: 13244 RVA: 0x00140934 File Offset: 0x0013F934
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				PdfNumber pdfNumber = (PdfNumber)operands[0];
				processor.Gs().characterSpacing = pdfNumber.FloatValue;
			}
		}

		// Token: 0x020005E6 RID: 1510
		private class SetTextWordSpacing : IContentOperator
		{
			// Token: 0x060033BE RID: 13246 RVA: 0x00140968 File Offset: 0x0013F968
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				PdfNumber pdfNumber = (PdfNumber)operands[0];
				processor.Gs().wordSpacing = pdfNumber.FloatValue;
			}
		}

		// Token: 0x020005E7 RID: 1511
		private class ProcessGraphicsStateResource : IContentOperator
		{
			// Token: 0x060033C0 RID: 13248 RVA: 0x0014099C File Offset: 0x0013F99C
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				PdfName pdfName = (PdfName)operands[0];
				PdfDictionary asDict = processor.resources.GetAsDict(PdfName.EXTGSTATE);
				if (asDict == null)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("resources.do.not.contain.extgstate.entry.unable.to.process.oper.1", oper));
				}
				PdfDictionary asDict2 = asDict.GetAsDict(pdfName);
				if (asDict2 == null)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("1.is.an.unknown.graphics.state.dictionary", pdfName));
				}
				PdfArray asArray = asDict2.GetAsArray(PdfName.FONT);
				if (asArray != null)
				{
					CMapAwareDocumentFont font = new CMapAwareDocumentFont((PRIndirectReference)asArray[0]);
					float floatValue = asArray.GetAsNumber(1).FloatValue;
					processor.Gs().font = font;
					processor.Gs().fontSize = floatValue;
				}
			}
		}

		// Token: 0x020005E8 RID: 1512
		private class PushGraphicsState : IContentOperator
		{
			// Token: 0x060033C2 RID: 13250 RVA: 0x00140A4C File Offset: 0x0013FA4C
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				GraphicsState source = processor.gsStack.Peek();
				GraphicsState item = new GraphicsState(source);
				processor.gsStack.Push(item);
			}
		}

		// Token: 0x020005E9 RID: 1513
		private class ModifyCurrentTransformationMatrix : IContentOperator
		{
			// Token: 0x060033C4 RID: 13252 RVA: 0x00140A80 File Offset: 0x0013FA80
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				float floatValue = ((PdfNumber)operands[0]).FloatValue;
				float floatValue2 = ((PdfNumber)operands[1]).FloatValue;
				float floatValue3 = ((PdfNumber)operands[2]).FloatValue;
				float floatValue4 = ((PdfNumber)operands[3]).FloatValue;
				float floatValue5 = ((PdfNumber)operands[4]).FloatValue;
				float floatValue6 = ((PdfNumber)operands[5]).FloatValue;
				Matrix matrix = new Matrix(floatValue, floatValue2, floatValue3, floatValue4, floatValue5, floatValue6);
				GraphicsState graphicsState = processor.gsStack.Peek();
				graphicsState.ctm = matrix.Multiply(graphicsState.ctm);
			}
		}

		// Token: 0x020005EA RID: 1514
		private class PopGraphicsState : IContentOperator
		{
			// Token: 0x060033C6 RID: 13254 RVA: 0x00140B34 File Offset: 0x0013FB34
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				processor.gsStack.Pop();
			}
		}

		// Token: 0x020005EB RID: 1515
		private class BeginTextC : IContentOperator
		{
			// Token: 0x060033C8 RID: 13256 RVA: 0x00140B4A File Offset: 0x0013FB4A
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				processor.textMatrix = new Matrix();
				processor.textLineMatrix = processor.textMatrix;
				processor.BeginText();
			}
		}

		// Token: 0x020005EC RID: 1516
		private class EndTextC : IContentOperator
		{
			// Token: 0x060033CA RID: 13258 RVA: 0x00140B71 File Offset: 0x0013FB71
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				processor.textMatrix = null;
				processor.textLineMatrix = null;
				processor.EndText();
			}
		}

		// Token: 0x020005ED RID: 1517
		private class BeginMarkedContentC : IContentOperator
		{
			// Token: 0x060033CC RID: 13260 RVA: 0x00140B8F File Offset: 0x0013FB8F
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				processor.BeginMarkedContent((PdfName)operands[0], new PdfDictionary());
			}
		}

		// Token: 0x020005EE RID: 1518
		private class BeginMarkedContentDictionary : IContentOperator
		{
			// Token: 0x060033CE RID: 13262 RVA: 0x00140BB0 File Offset: 0x0013FBB0
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				PdfObject operand = operands[1];
				processor.BeginMarkedContent((PdfName)operands[0], this.GetPropertiesDictionary(operand, processor.resources));
			}

			// Token: 0x060033CF RID: 13263 RVA: 0x00140BE4 File Offset: 0x0013FBE4
			private PdfDictionary GetPropertiesDictionary(PdfObject operand1, PdfContentStreamProcessor.ResourceDictionary resources)
			{
				if (operand1.IsDictionary())
				{
					return (PdfDictionary)operand1;
				}
				PdfName key = (PdfName)operand1;
				return resources.GetAsDict(key);
			}
		}

		// Token: 0x020005EF RID: 1519
		private class EndMarkedContentC : IContentOperator
		{
			// Token: 0x060033D1 RID: 13265 RVA: 0x00140C16 File Offset: 0x0013FC16
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				processor.EndMarkedContent();
			}
		}

		// Token: 0x020005F0 RID: 1520
		private class Do : IContentOperator
		{
			// Token: 0x060033D3 RID: 13267 RVA: 0x00140C28 File Offset: 0x0013FC28
			public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
			{
				PdfName xobjectName = (PdfName)operands[0];
				processor.DisplayXObject(xobjectName);
			}
		}

		// Token: 0x020005F1 RID: 1521
		private class FormXObjectDoHandler : IXObjectDoHandler
		{
			// Token: 0x060033D5 RID: 13269 RVA: 0x00140C54 File Offset: 0x0013FC54
			public void HandleXObject(PdfContentStreamProcessor processor, PdfStream stream, PdfIndirectReference refi)
			{
				PdfDictionary asDict = stream.GetAsDict(PdfName.RESOURCES);
				byte[] contentBytesFromContentObject = ContentByteUtils.GetContentBytesFromContentObject(stream);
				PdfArray asArray = stream.GetAsArray(PdfName.MATRIX);
				new PdfContentStreamProcessor.PushGraphicsState().Invoke(processor, null, null);
				if (asArray != null)
				{
					float floatValue = asArray.GetAsNumber(0).FloatValue;
					float floatValue2 = asArray.GetAsNumber(1).FloatValue;
					float floatValue3 = asArray.GetAsNumber(2).FloatValue;
					float floatValue4 = asArray.GetAsNumber(3).FloatValue;
					float floatValue5 = asArray.GetAsNumber(4).FloatValue;
					float floatValue6 = asArray.GetAsNumber(5).FloatValue;
					Matrix matrix = new Matrix(floatValue, floatValue2, floatValue3, floatValue4, floatValue5, floatValue6);
					processor.Gs().ctm = matrix.Multiply(processor.Gs().ctm);
				}
				processor.ProcessContent(contentBytesFromContentObject, asDict);
				new PdfContentStreamProcessor.PopGraphicsState().Invoke(processor, null, null);
			}
		}

		// Token: 0x020005F2 RID: 1522
		private class ImageXObjectDoHandler : IXObjectDoHandler
		{
			// Token: 0x060033D7 RID: 13271 RVA: 0x00140D34 File Offset: 0x0013FD34
			public void HandleXObject(PdfContentStreamProcessor processor, PdfStream xobjectStream, PdfIndirectReference refi)
			{
				ImageRenderInfo renderInfo = new ImageRenderInfo(processor.Gs().ctm, refi);
				processor.renderListener.RenderImage(renderInfo);
			}
		}

		// Token: 0x020005F3 RID: 1523
		private class IgnoreXObjectDoHandler : IXObjectDoHandler
		{
			// Token: 0x060033D9 RID: 13273 RVA: 0x00140D67 File Offset: 0x0013FD67
			public void HandleXObject(PdfContentStreamProcessor processor, PdfStream xobjectStream, PdfIndirectReference refi)
			{
			}
		}
	}
}
