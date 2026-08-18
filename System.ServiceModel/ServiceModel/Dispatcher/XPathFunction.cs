using System;
using System.Text;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004A1 RID: 1185
	internal class XPathFunction : QueryFunction
	{
		// Token: 0x06002D49 RID: 11593 RVA: 0x000B0B11 File Offset: 0x000AED11
		internal XPathFunction(XPathFunctionID functionID, string name, ValueDataType returnType) : base(name, returnType)
		{
			this.functionID = functionID;
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000B0B22 File Offset: 0x000AED22
		internal XPathFunction(XPathFunctionID functionID, string name, ValueDataType returnType, QueryFunctionFlag flags) : base(name, returnType, flags)
		{
			this.functionID = functionID;
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000B0B35 File Offset: 0x000AED35
		internal XPathFunction(XPathFunctionID functionID, string name, ValueDataType returnType, ValueDataType[] argTypes) : base(name, returnType, argTypes)
		{
			this.functionID = functionID;
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x000B0B48 File Offset: 0x000AED48
		internal XPathFunctionID ID
		{
			get
			{
				return this.functionID;
			}
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000B0B50 File Offset: 0x000AED50
		internal override bool Equals(QueryFunction function)
		{
			XPathFunction xpathFunction = function as XPathFunction;
			return xpathFunction != null && xpathFunction.ID == this.ID;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000B0B78 File Offset: 0x000AED78
		private static void ConvertFirstArg(ProcessingContext context, ValueDataType type)
		{
			StackFrame topArg = context.TopArg;
			Value[] values = context.Values;
			while (topArg.basePtr <= topArg.endPtr)
			{
				Value[] array = values;
				int basePtr = topArg.basePtr;
				topArg.basePtr = basePtr + 1;
				array[basePtr].ConvertTo(context, type);
			}
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000B0BC0 File Offset: 0x000AEDC0
		internal override void Eval(ProcessingContext context)
		{
			switch (this.functionID)
			{
			case XPathFunctionID.IterateSequences:
				XPathFunction.IterateAndPushSequences(context);
				return;
			case XPathFunctionID.Count:
				XPathFunction.NodesetCount(context);
				return;
			case XPathFunctionID.Position:
				XPathFunction.NodesetPosition(context);
				return;
			case XPathFunctionID.Last:
				XPathFunction.NodesetLast(context);
				return;
			case XPathFunctionID.LocalName:
				XPathFunction.NodesetLocalName(context);
				return;
			case XPathFunctionID.LocalNameDefault:
				XPathFunction.NodesetLocalNameDefault(context);
				return;
			case XPathFunctionID.Name:
				XPathFunction.NodesetName(context);
				return;
			case XPathFunctionID.NameDefault:
				XPathFunction.NodesetNameDefault(context);
				return;
			case XPathFunctionID.NamespaceUri:
				XPathFunction.NodesetNamespaceUri(context);
				return;
			case XPathFunctionID.NamespaceUriDefault:
				XPathFunction.NodesetNamespaceUriDefault(context);
				return;
			case XPathFunctionID.Boolean:
				XPathFunction.BooleanBoolean(context);
				return;
			case XPathFunctionID.Not:
				XPathFunction.BooleanNot(context);
				return;
			case XPathFunctionID.True:
				XPathFunction.BooleanTrue(context);
				return;
			case XPathFunctionID.False:
				XPathFunction.BooleanFalse(context);
				return;
			case XPathFunctionID.Lang:
				XPathFunction.BooleanLang(context);
				return;
			case XPathFunctionID.Number:
				XPathFunction.NumberNumber(context);
				return;
			case XPathFunctionID.NumberDefault:
				XPathFunction.NumberNumberDefault(context);
				return;
			case XPathFunctionID.Ceiling:
				XPathFunction.NumberCeiling(context);
				return;
			case XPathFunctionID.Floor:
				XPathFunction.NumberFloor(context);
				return;
			case XPathFunctionID.Round:
				XPathFunction.NumberRound(context);
				return;
			case XPathFunctionID.Sum:
				XPathFunction.NumberSum(context);
				return;
			case XPathFunctionID.String:
				XPathFunction.StringString(context);
				return;
			case XPathFunctionID.StringDefault:
				XPathFunction.StringStringDefault(context);
				return;
			case XPathFunctionID.StartsWith:
				XPathFunction.StringStartsWith(context);
				return;
			case XPathFunctionID.ConcatTwo:
				XPathFunction.StringConcatTwo(context);
				return;
			case XPathFunctionID.ConcatThree:
				XPathFunction.StringConcatThree(context);
				return;
			case XPathFunctionID.ConcatFour:
				XPathFunction.StringConcatFour(context);
				return;
			case XPathFunctionID.Contains:
				XPathFunction.StringContains(context);
				return;
			case XPathFunctionID.NormalizeSpace:
				XPathFunction.NormalizeSpace(context);
				return;
			case XPathFunctionID.NormalizeSpaceDefault:
				XPathFunction.NormalizeSpaceDefault(context);
				return;
			case XPathFunctionID.StringLength:
				XPathFunction.StringLength(context);
				return;
			case XPathFunctionID.StringLengthDefault:
				XPathFunction.StringLengthDefault(context);
				return;
			case XPathFunctionID.SubstringBefore:
				XPathFunction.SubstringBefore(context);
				return;
			case XPathFunctionID.SubstringAfter:
				XPathFunction.SubstringAfter(context);
				return;
			case XPathFunctionID.Substring:
				XPathFunction.Substring(context);
				return;
			case XPathFunctionID.SubstringLimit:
				XPathFunction.SubstringLimit(context);
				return;
			case XPathFunctionID.Translate:
				XPathFunction.Translate(context);
				return;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("QueryNotImplemented", new object[]
				{
					this.name
				})));
			}
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000B0D9C File Offset: 0x000AEF9C
		internal static void BooleanBoolean(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			Value[] values = context.Values;
			while (topArg.basePtr <= topArg.endPtr)
			{
				Value[] array = values;
				int basePtr = topArg.basePtr;
				topArg.basePtr = basePtr + 1;
				array[basePtr].ConvertTo(context, ValueDataType.Boolean);
			}
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000B0DE4 File Offset: 0x000AEFE4
		internal static void BooleanFalse(ProcessingContext context)
		{
			context.PushFrame();
			int iterationCount = context.IterationCount;
			if (iterationCount > 0)
			{
				context.Push(false, iterationCount);
			}
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000B0E0C File Offset: 0x000AF00C
		internal static void BooleanNot(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			Value[] values = context.Values;
			while (topArg.basePtr <= topArg.endPtr)
			{
				Value[] array = values;
				int basePtr = topArg.basePtr;
				topArg.basePtr = basePtr + 1;
				array[basePtr].Not();
			}
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000B0E54 File Offset: 0x000AF054
		internal static void BooleanTrue(ProcessingContext context)
		{
			context.PushFrame();
			int iterationCount = context.IterationCount;
			if (iterationCount > 0)
			{
				context.Push(true, iterationCount);
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000B0E7C File Offset: 0x000AF07C
		internal static void BooleanLang(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame topSequenceArg = context.TopSequenceArg;
			Value[] sequences = context.Sequences;
			while (topSequenceArg.basePtr <= topSequenceArg.endPtr)
			{
				Value[] array = sequences;
				int basePtr = topSequenceArg.basePtr;
				topSequenceArg.basePtr = basePtr + 1;
				NodeSequence sequence = array[basePtr].Sequence;
				for (int i = 0; i < sequence.Count; i++)
				{
					string text = context.PeekString(topArg.basePtr).ToUpperInvariant();
					QueryNode node = sequence.Items[i].Node;
					long currentPosition = node.Node.CurrentPosition;
					node.Node.CurrentPosition = node.Position;
					string text2 = node.Node.XmlLang.ToUpperInvariant();
					node.Node.CurrentPosition = currentPosition;
					if (text.Length == text2.Length && string.CompareOrdinal(text, text2) == 0)
					{
						basePtr = topArg.basePtr;
						topArg.basePtr = basePtr + 1;
						context.SetValue(context, basePtr, true);
					}
					else if (text2.Length > 0 && text.Length < text2.Length && text2.StartsWith(text, StringComparison.Ordinal) && text2[text.Length] == '-')
					{
						basePtr = topArg.basePtr;
						topArg.basePtr = basePtr + 1;
						context.SetValue(context, basePtr, true);
					}
					else
					{
						basePtr = topArg.basePtr;
						topArg.basePtr = basePtr + 1;
						context.SetValue(context, basePtr, false);
					}
				}
				topSequenceArg.basePtr++;
			}
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000B1008 File Offset: 0x000AF208
		internal static void IterateAndPushSequences(ProcessingContext context)
		{
			StackFrame topSequenceArg = context.TopSequenceArg;
			Value[] sequences = context.Sequences;
			context.PushFrame();
			while (topSequenceArg.basePtr <= topSequenceArg.endPtr)
			{
				Value[] array = sequences;
				int basePtr = topSequenceArg.basePtr;
				topSequenceArg.basePtr = basePtr + 1;
				NodeSequence sequence = array[basePtr].Sequence;
				if (sequence.Count == 0)
				{
					context.PushSequence(NodeSequence.Empty);
				}
				else
				{
					for (int i = 0; i < sequence.Count; i++)
					{
						NodeSequence nodeSequence = context.CreateSequence();
						nodeSequence.StartNodeset();
						nodeSequence.Add(ref sequence.Items[i]);
						nodeSequence.StopNodeset();
						context.Push(nodeSequence);
					}
				}
			}
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000B10B8 File Offset: 0x000AF2B8
		internal static void NodesetCount(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				context.SetValue(context, topArg.basePtr, (double)context.PeekSequence(topArg.basePtr).Count);
				topArg.basePtr++;
			}
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000B1107 File Offset: 0x000AF307
		internal static void NodesetLast(ProcessingContext context)
		{
			context.TransferSequenceSize();
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000B1110 File Offset: 0x000AF310
		internal static void NodesetLocalName(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				NodeSequence nodeSequence = context.PeekSequence(topArg.basePtr);
				context.SetValue(context, topArg.basePtr, nodeSequence.LocalName);
				topArg.basePtr++;
			}
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000B1160 File Offset: 0x000AF360
		internal static void NodesetLocalNameDefault(ProcessingContext context)
		{
			XPathFunction.IterateAndPushSequences(context);
			XPathFunction.NodesetLocalName(context);
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000B1170 File Offset: 0x000AF370
		internal static void NodesetName(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				NodeSequence nodeSequence = context.PeekSequence(topArg.basePtr);
				context.SetValue(context, topArg.basePtr, nodeSequence.Name);
				topArg.basePtr++;
			}
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000B11C0 File Offset: 0x000AF3C0
		internal static void NodesetNameDefault(ProcessingContext context)
		{
			XPathFunction.IterateAndPushSequences(context);
			XPathFunction.NodesetName(context);
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000B11D0 File Offset: 0x000AF3D0
		internal static void NodesetNamespaceUri(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				NodeSequence nodeSequence = context.PeekSequence(topArg.basePtr);
				context.SetValue(context, topArg.basePtr, nodeSequence.Namespace);
				topArg.basePtr++;
			}
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000B1220 File Offset: 0x000AF420
		internal static void NodesetNamespaceUriDefault(ProcessingContext context)
		{
			XPathFunction.IterateAndPushSequences(context);
			XPathFunction.NodesetNamespaceUri(context);
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000B122E File Offset: 0x000AF42E
		internal static void NodesetPosition(ProcessingContext context)
		{
			context.TransferSequencePositions();
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000B1238 File Offset: 0x000AF438
		internal static void NumberCeiling(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				context.SetValue(context, topArg.basePtr, Math.Ceiling(context.PeekDouble(topArg.basePtr)));
				topArg.basePtr++;
			}
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000B1288 File Offset: 0x000AF488
		internal static void NumberNumber(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			Value[] values = context.Values;
			while (topArg.basePtr <= topArg.endPtr)
			{
				Value[] array = values;
				int basePtr = topArg.basePtr;
				topArg.basePtr = basePtr + 1;
				array[basePtr].ConvertTo(context, ValueDataType.Double);
			}
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000B12CF File Offset: 0x000AF4CF
		internal static void NumberNumberDefault(ProcessingContext context)
		{
			XPathFunction.IterateAndPushSequences(context);
			XPathFunction.NumberNumber(context);
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000B12E0 File Offset: 0x000AF4E0
		internal static void NumberFloor(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				context.SetValue(context, topArg.basePtr, Math.Floor(context.PeekDouble(topArg.basePtr)));
				topArg.basePtr++;
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000B1330 File Offset: 0x000AF530
		internal static void NumberRound(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				double num = context.PeekDouble(topArg.basePtr);
				context.SetValue(context, topArg.basePtr, QueryValueModel.Round(context.PeekDouble(topArg.basePtr)));
				topArg.basePtr++;
			}
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000B138C File Offset: 0x000AF58C
		internal static void NumberSum(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				NodeSequence nodeSequence = context.PeekSequence(topArg.basePtr);
				double num = 0.0;
				for (int i = 0; i < nodeSequence.Count; i++)
				{
					num += QueryValueModel.Double(nodeSequence[i].StringValue());
				}
				context.SetValue(context, topArg.basePtr, num);
				topArg.basePtr++;
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000B140C File Offset: 0x000AF60C
		internal static void StringString(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			Value[] values = context.Values;
			while (topArg.basePtr <= topArg.endPtr)
			{
				Value[] array = values;
				int basePtr = topArg.basePtr;
				topArg.basePtr = basePtr + 1;
				array[basePtr].ConvertTo(context, ValueDataType.String);
			}
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000B1453 File Offset: 0x000AF653
		internal static void StringStringDefault(ProcessingContext context)
		{
			XPathFunction.IterateAndPushSequences(context);
			XPathFunction.StringString(context);
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000B1464 File Offset: 0x000AF664
		internal static void StringConcatTwo(ProcessingContext context)
		{
			StackFrame stackFrame = context[0];
			StackFrame stackFrame2 = context[1];
			while (stackFrame.basePtr <= stackFrame.endPtr)
			{
				string str = context.PeekString(stackFrame.basePtr);
				string str2 = context.PeekString(stackFrame2.basePtr);
				context.SetValue(context, stackFrame2.basePtr, str + str2);
				stackFrame.basePtr++;
				stackFrame2.basePtr++;
			}
			context.PopFrame();
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000B14E0 File Offset: 0x000AF6E0
		internal static void StringConcatThree(ProcessingContext context)
		{
			StackFrame stackFrame = context[0];
			StackFrame stackFrame2 = context[1];
			StackFrame stackFrame3 = context[2];
			while (stackFrame.basePtr <= stackFrame.endPtr)
			{
				string str = context.PeekString(stackFrame.basePtr);
				string str2 = context.PeekString(stackFrame2.basePtr);
				string str3 = context.PeekString(stackFrame3.basePtr);
				context.SetValue(context, stackFrame3.basePtr, str + str2 + str3);
				stackFrame.basePtr++;
				stackFrame2.basePtr++;
				stackFrame3.basePtr++;
			}
			context.PopFrame();
			context.PopFrame();
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000B1588 File Offset: 0x000AF788
		internal static void StringConcatFour(ProcessingContext context)
		{
			StackFrame stackFrame = context[0];
			StackFrame stackFrame2 = context[1];
			StackFrame stackFrame3 = context[2];
			StackFrame stackFrame4 = context[3];
			while (stackFrame.basePtr <= stackFrame.endPtr)
			{
				string str = context.PeekString(stackFrame.basePtr);
				string str2 = context.PeekString(stackFrame2.basePtr);
				string str3 = context.PeekString(stackFrame3.basePtr);
				string str4 = context.PeekString(stackFrame4.basePtr);
				context.SetValue(context, stackFrame4.basePtr, str + str2 + str3 + str4);
				stackFrame.basePtr++;
				stackFrame2.basePtr++;
				stackFrame3.basePtr++;
				stackFrame4.basePtr++;
			}
			context.PopFrame();
			context.PopFrame();
			context.PopFrame();
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000B1660 File Offset: 0x000AF860
		internal static void StringContains(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				string text = context.PeekString(topArg.basePtr);
				string value = context.PeekString(secondArg.basePtr);
				context.SetValue(context, secondArg.basePtr, -1 != text.IndexOf(value, StringComparison.Ordinal));
				topArg.basePtr++;
				secondArg.basePtr++;
			}
			context.PopFrame();
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000B16E0 File Offset: 0x000AF8E0
		internal static void StringLength(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				context.SetValue(context, topArg.basePtr, (double)context.PeekString(topArg.basePtr).Length);
				topArg.basePtr++;
			}
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000B172F File Offset: 0x000AF92F
		internal static void StringLengthDefault(ProcessingContext context)
		{
			XPathFunction.IterateAndPushSequences(context);
			XPathFunction.ConvertFirstArg(context, ValueDataType.String);
			XPathFunction.StringLength(context);
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000B1744 File Offset: 0x000AF944
		internal static void StringStartsWith(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				string text = context.PeekString(topArg.basePtr);
				string value = context.PeekString(secondArg.basePtr);
				context.SetValue(context, secondArg.basePtr, text.StartsWith(value, StringComparison.Ordinal));
				topArg.basePtr++;
				secondArg.basePtr++;
			}
			context.PopFrame();
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000B17BC File Offset: 0x000AF9BC
		internal static void SubstringBefore(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				string text = context.PeekString(topArg.basePtr);
				string value = context.PeekString(secondArg.basePtr);
				int num = text.IndexOf(value, StringComparison.Ordinal);
				context.SetValue(context, secondArg.basePtr, (num == -1) ? string.Empty : text.Substring(0, num));
				topArg.basePtr++;
				secondArg.basePtr++;
			}
			context.PopFrame();
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000B184C File Offset: 0x000AFA4C
		internal static void SubstringAfter(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				string text = context.PeekString(topArg.basePtr);
				string text2 = context.PeekString(secondArg.basePtr);
				int num = text.IndexOf(text2, StringComparison.Ordinal);
				context.SetValue(context, secondArg.basePtr, (num == -1) ? string.Empty : text.Substring(num + text2.Length));
				topArg.basePtr++;
				secondArg.basePtr++;
			}
			context.PopFrame();
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000B18E4 File Offset: 0x000AFAE4
		internal static void Substring(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				string text = context.PeekString(topArg.basePtr);
				int num = (int)Math.Round(context.PeekDouble(secondArg.basePtr)) - 1;
				if (num < 0)
				{
					num = 0;
				}
				context.SetValue(context, secondArg.basePtr, (num >= text.Length) ? string.Empty : text.Substring(num));
				topArg.basePtr++;
				secondArg.basePtr++;
			}
			context.PopFrame();
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000B197C File Offset: 0x000AFB7C
		internal static void SubstringLimit(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			StackFrame stackFrame = context[2];
			while (topArg.basePtr <= topArg.endPtr)
			{
				string text = context.PeekString(topArg.basePtr);
				int num = (int)Math.Round(context.PeekDouble(secondArg.basePtr)) - 1;
				if (num < 0)
				{
					num = 0;
				}
				int num2 = (int)Math.Round(context.PeekDouble(stackFrame.basePtr));
				string val;
				if (num2 < 1 || num + num2 >= text.Length)
				{
					val = string.Empty;
				}
				else
				{
					val = text.Substring(num, num2);
				}
				context.SetValue(context, stackFrame.basePtr, val);
				secondArg.basePtr++;
				topArg.basePtr++;
				stackFrame.basePtr++;
			}
			context.PopFrame();
			context.PopFrame();
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000B1A5C File Offset: 0x000AFC5C
		internal static void Translate(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			StackFrame stackFrame = context[2];
			StringBuilder stringBuilder = new StringBuilder();
			while (topArg.basePtr <= topArg.endPtr)
			{
				stringBuilder.Length = 0;
				string text = context.PeekString(topArg.basePtr);
				string text2 = context.PeekString(secondArg.basePtr);
				string text3 = context.PeekString(stackFrame.basePtr);
				foreach (char value in text)
				{
					int num = text2.IndexOf(value);
					if (num < 0)
					{
						stringBuilder.Append(value);
					}
					else if (num < text3.Length)
					{
						stringBuilder.Append(text3[num]);
					}
				}
				context.SetValue(context, stackFrame.basePtr, stringBuilder.ToString());
				topArg.basePtr++;
				secondArg.basePtr++;
				stackFrame.basePtr++;
			}
			context.PopFrame();
			context.PopFrame();
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000B1B68 File Offset: 0x000AFD68
		internal static void NormalizeSpace(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StringBuilder stringBuilder = new StringBuilder();
			while (topArg.basePtr <= topArg.endPtr)
			{
				char[] trimChars = new char[]
				{
					' ',
					'\t',
					'\r',
					'\n'
				};
				string text = context.PeekString(topArg.basePtr).Trim(trimChars);
				bool flag = false;
				stringBuilder.Length = 0;
				foreach (char c in text)
				{
					if (XPathCharTypes.IsWhitespace(c))
					{
						if (!flag)
						{
							stringBuilder.Append(' ');
							flag = true;
						}
					}
					else
					{
						stringBuilder.Append(c);
						flag = false;
					}
				}
				context.SetValue(context, topArg.basePtr, stringBuilder.ToString());
				topArg.basePtr++;
			}
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000B1C2C File Offset: 0x000AFE2C
		internal static void NormalizeSpaceDefault(ProcessingContext context)
		{
			XPathFunction.IterateAndPushSequences(context);
			XPathFunction.ConvertFirstArg(context, ValueDataType.String);
			XPathFunction.NormalizeSpace(context);
		}

		// Token: 0x040024CB RID: 9419
		private XPathFunctionID functionID;
	}
}
