using System;
using System.Globalization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000536 RID: 1334
	internal class XPathMessageFunctionDateStr : XPathMessageFunction
	{
		// Token: 0x0600326D RID: 12909 RVA: 0x000C25E7 File Offset: 0x000C07E7
		internal XPathMessageFunctionDateStr() : base(new XPathResultType[]
		{
			XPathResultType.String
		}, 1, 1, XPathResultType.Number)
		{
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000C25FC File Offset: 0x000C07FC
		internal override void InvokeInternal(ProcessingContext context, int argCount)
		{
			StackFrame topArg = context.TopArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				string dateStr = context.PeekString(topArg.basePtr);
				context.SetValue(context, topArg.basePtr, XPathMessageFunctionDateStr.Convert(dateStr));
				topArg.basePtr++;
			}
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000C264C File Offset: 0x000C084C
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			return XPathMessageFunctionDateStr.Convert(XPathMessageFunction.ToString(args[0]));
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000C2660 File Offset: 0x000C0860
		internal static double Convert(string dateStr)
		{
			double result;
			try
			{
				result = XPathMessageFunction.ConvertDate(DateTime.Parse(dateStr, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.RoundtripKind));
			}
			catch (FormatException)
			{
				result = double.NaN;
			}
			return result;
		}
	}
}
