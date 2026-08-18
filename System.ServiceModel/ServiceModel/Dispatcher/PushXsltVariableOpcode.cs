using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000494 RID: 1172
	internal class PushXsltVariableOpcode : Opcode
	{
		// Token: 0x06002D26 RID: 11558 RVA: 0x000AFD70 File Offset: 0x000ADF70
		internal PushXsltVariableOpcode(XsltContext context, IXsltContextVariable variable) : base(OpcodeID.PushXsltVariable)
		{
			this.xsltContext = context;
			this.variable = variable;
			this.type = XPathXsltFunctionExpr.ConvertTypeFromXslt(variable.VariableType);
			ValueDataType valueDataType = this.type;
			if (valueDataType - ValueDataType.Boolean > 1 && valueDataType - ValueDataType.Sequence > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(QueryCompileError.InvalidType, SR.GetString("QueryVariableTypeNotSupported", new object[]
				{
					this.variable.VariableType.ToString()
				})));
			}
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000AFDF8 File Offset: 0x000ADFF8
		internal override bool Equals(Opcode op)
		{
			if (base.Equals(op))
			{
				PushXsltVariableOpcode pushXsltVariableOpcode = op as PushXsltVariableOpcode;
				if (pushXsltVariableOpcode != null)
				{
					return this.xsltContext == pushXsltVariableOpcode.xsltContext && this.variable == pushXsltVariableOpcode.variable;
				}
			}
			return false;
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000AFE38 File Offset: 0x000AE038
		internal override Opcode Eval(ProcessingContext context)
		{
			context.PushFrame();
			int iterationCount = context.IterationCount;
			if (iterationCount > 0)
			{
				object obj = this.variable.Evaluate(this.xsltContext);
				if (obj == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryProcessingException(QueryProcessingError.Unexpected, SR.GetString("QueryVariableNull")));
				}
				switch (this.type)
				{
				case ValueDataType.Boolean:
					context.Push((bool)obj, iterationCount);
					goto IL_13A;
				case ValueDataType.Double:
					context.Push((double)obj, iterationCount);
					goto IL_13A;
				case ValueDataType.Sequence:
				{
					XPathNodeIterator xpathNodeIterator = (XPathNodeIterator)obj;
					NodeSequence nodeSequence = context.CreateSequence();
					while (xpathNodeIterator.MoveNext())
					{
						XPathNavigator xpathNavigator = xpathNodeIterator.Current;
						SeekableXPathNavigator seekableXPathNavigator = xpathNavigator as SeekableXPathNavigator;
						if (seekableXPathNavigator == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryProcessingException(QueryProcessingError.Unexpected, SR.GetString("QueryMustBeSeekable")));
						}
						nodeSequence.Add(seekableXPathNavigator);
					}
					context.Push(nodeSequence, iterationCount);
					goto IL_13A;
				}
				case ValueDataType.String:
					context.Push((string)obj, iterationCount);
					goto IL_13A;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.Unexpected, SR.GetString("QueryVariableTypeNotSupported", new object[]
				{
					this.variable.VariableType.ToString()
				})));
			}
			IL_13A:
			return this.next;
		}

		// Token: 0x04002462 RID: 9314
		private XsltContext xsltContext;

		// Token: 0x04002463 RID: 9315
		private IXsltContextVariable variable;

		// Token: 0x04002464 RID: 9316
		private ValueDataType type;
	}
}
