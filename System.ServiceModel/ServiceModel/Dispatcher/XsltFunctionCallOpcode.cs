using System;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200049A RID: 1178
	internal class XsltFunctionCallOpcode : Opcode
	{
		// Token: 0x06002D32 RID: 11570 RVA: 0x000B0044 File Offset: 0x000AE244
		internal XsltFunctionCallOpcode(XsltContext context, IXsltContextFunction function, int argCount) : base(OpcodeID.XsltFunction)
		{
			this.xsltContext = context;
			this.function = function;
			this.argCount = argCount;
			for (int i = 0; i < function.Maxargs; i++)
			{
				if (function.ArgTypes[i] == XPathResultType.NodeSet)
				{
					this.iterList = new List<NodeSequenceIterator>();
					break;
				}
			}
			XPathResultType returnType = this.function.ReturnType;
			if (returnType > XPathResultType.NodeSet)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(QueryCompileError.InvalidType, SR.GetString("QueryFunctionTypeNotSupported", new object[]
				{
					this.function.ReturnType.ToString()
				})));
			}
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000B00E4 File Offset: 0x000AE2E4
		internal override bool Equals(Opcode op)
		{
			return false;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000B00E8 File Offset: 0x000AE2E8
		internal override Opcode Eval(ProcessingContext context)
		{
			XPathNavigator contextNode = context.Processor.ContextNode;
			if (contextNode != null && context.Processor.ContextMessage != null)
			{
				((SeekableMessageNavigator)contextNode).Atomize();
			}
			if (this.argCount == 0)
			{
				context.PushFrame();
				int iterationCount = context.IterationCount;
				if (iterationCount > 0)
				{
					object obj = this.function.Invoke(this.xsltContext, XsltFunctionCallOpcode.NullArgs, contextNode);
					switch (this.function.ReturnType)
					{
					case XPathResultType.Number:
						context.Push((double)obj, iterationCount);
						break;
					case XPathResultType.String:
						context.Push((string)obj, iterationCount);
						break;
					case XPathResultType.Boolean:
						context.Push((bool)obj, iterationCount);
						break;
					case XPathResultType.NodeSet:
					{
						NodeSequence nodeSequence = context.CreateSequence();
						XPathNodeIterator iter = (XPathNodeIterator)obj;
						nodeSequence.Add(iter);
						context.Push(nodeSequence, iterationCount);
						break;
					}
					default:
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.Unexpected, SR.GetString("QueryFunctionTypeNotSupported", new object[]
						{
							this.function.ReturnType.ToString()
						})));
					}
				}
			}
			else
			{
				object[] array = new object[this.argCount];
				int count = context.TopArg.Count;
				for (int i = 0; i < count; i++)
				{
					for (int j = 0; j < this.argCount; j++)
					{
						StackFrame stackFrame = context[j];
						switch (this.function.ArgTypes[j])
						{
						case XPathResultType.Number:
							array[j] = context.PeekDouble(stackFrame[i]);
							break;
						case XPathResultType.String:
							array[j] = context.PeekString(stackFrame[i]);
							break;
						case XPathResultType.Boolean:
							array[j] = context.PeekBoolean(stackFrame[i]);
							break;
						case XPathResultType.NodeSet:
						{
							NodeSequenceIterator nodeSequenceIterator = new NodeSequenceIterator(context.PeekSequence(stackFrame[i]));
							array[j] = nodeSequenceIterator;
							this.iterList.Add(nodeSequenceIterator);
							break;
						}
						default:
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.Unexpected, SR.GetString("QueryFunctionTypeNotSupported", new object[]
							{
								this.function.ArgTypes[j].ToString()
							})));
						}
					}
					object obj2 = this.function.Invoke(this.xsltContext, array, contextNode);
					if (this.iterList != null)
					{
						for (int k = 0; k < this.iterList.Count; k++)
						{
							this.iterList[k].Clear();
						}
						this.iterList.Clear();
					}
					switch (this.function.ReturnType)
					{
					case XPathResultType.Number:
						context.SetValue(context, context[this.argCount - 1][i], (double)obj2);
						break;
					case XPathResultType.String:
						context.SetValue(context, context[this.argCount - 1][i], (string)obj2);
						break;
					case XPathResultType.Boolean:
						context.SetValue(context, context[this.argCount - 1][i], (bool)obj2);
						break;
					case XPathResultType.NodeSet:
					{
						NodeSequence nodeSequence2 = context.CreateSequence();
						XPathNodeIterator iter2 = (XPathNodeIterator)obj2;
						nodeSequence2.Add(iter2);
						context.SetValue(context, context[this.argCount - 1][i], nodeSequence2);
						break;
					}
					default:
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.Unexpected, SR.GetString("QueryFunctionTypeNotSupported", new object[]
						{
							this.function.ReturnType.ToString()
						})));
					}
				}
				for (int l = 0; l < this.argCount - 1; l++)
				{
					context.PopFrame();
				}
			}
			return this.next;
		}

		// Token: 0x04002496 RID: 9366
		private static object[] NullArgs = new object[0];

		// Token: 0x04002497 RID: 9367
		private int argCount;

		// Token: 0x04002498 RID: 9368
		private XsltContext xsltContext;

		// Token: 0x04002499 RID: 9369
		private IXsltContextFunction function;

		// Token: 0x0400249A RID: 9370
		private List<NodeSequenceIterator> iterList;
	}
}
