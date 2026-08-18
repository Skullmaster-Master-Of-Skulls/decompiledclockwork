using System;
using System.Globalization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000539 RID: 1337
	internal class XPathMessageFunctionSpanStr : XPathMessageFunction
	{
		// Token: 0x06003278 RID: 12920 RVA: 0x000C27D4 File Offset: 0x000C09D4
		internal XPathMessageFunctionSpanStr() : base(new XPathResultType[]
		{
			XPathResultType.String
		}, 1, 1, XPathResultType.Number)
		{
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x000C27EC File Offset: 0x000C09EC
		internal override void InvokeInternal(ProcessingContext context, int argCount)
		{
			StackFrame topArg = context.TopArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				string spanStr = context.PeekString(topArg.basePtr);
				context.SetValue(context, topArg.basePtr, XPathMessageFunctionSpanStr.Convert(spanStr));
				topArg.basePtr++;
			}
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x000C283C File Offset: 0x000C0A3C
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			return XPathMessageFunctionSpanStr.Convert(XPathMessageFunction.ToString(args[0]));
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x000C2850 File Offset: 0x000C0A50
		internal static double Convert(string spanStr)
		{
			double result;
			try
			{
				result = TimeSpan.Parse(spanStr, CultureInfo.InvariantCulture).TotalDays;
			}
			catch (FormatException)
			{
				result = double.NaN;
			}
			catch (OverflowException)
			{
				result = double.NaN;
			}
			return result;
		}
	}
}
