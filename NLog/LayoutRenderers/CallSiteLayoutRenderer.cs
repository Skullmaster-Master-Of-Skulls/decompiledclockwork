using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C8 RID: 200
	[ThreadAgnostic]
	[LayoutRenderer("callsite")]
	public class CallSiteLayoutRenderer : LayoutRenderer, IUsesStackTrace
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x0000D0F2 File Offset: 0x0000B2F2
		public CallSiteLayoutRenderer()
		{
			this.ClassName = true;
			this.MethodName = true;
			this.CleanNamesOfAnonymousDelegates = false;
			this.FileName = false;
			this.IncludeSourcePath = true;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000D11D File Offset: 0x0000B31D
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0000D125 File Offset: 0x0000B325
		[DefaultValue(true)]
		public bool ClassName { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000D12E File Offset: 0x0000B32E
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0000D136 File Offset: 0x0000B336
		[DefaultValue(true)]
		public bool MethodName { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0000D13F File Offset: 0x0000B33F
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0000D147 File Offset: 0x0000B347
		[DefaultValue(false)]
		public bool CleanNamesOfAnonymousDelegates { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000D150 File Offset: 0x0000B350
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0000D158 File Offset: 0x0000B358
		[DefaultValue(0)]
		public int SkipFrames { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000D161 File Offset: 0x0000B361
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0000D169 File Offset: 0x0000B369
		[DefaultValue(false)]
		public bool FileName { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000D172 File Offset: 0x0000B372
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0000D17A File Offset: 0x0000B37A
		[DefaultValue(true)]
		public bool IncludeSourcePath { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000D183 File Offset: 0x0000B383
		StackTraceUsage IUsesStackTrace.StackTraceUsage
		{
			get
			{
				if (this.FileName)
				{
					return StackTraceUsage.WithSource;
				}
				return StackTraceUsage.WithoutSource;
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000D190 File Offset: 0x0000B390
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			StackFrame stackFrame = (logEvent.StackTrace != null) ? logEvent.StackTrace.GetFrame(logEvent.UserStackFrameNumber + this.SkipFrames) : null;
			if (stackFrame != null)
			{
				MethodBase method = stackFrame.GetMethod();
				if (this.ClassName)
				{
					if (method.DeclaringType != null)
					{
						string text = method.DeclaringType.FullName;
						if (this.CleanNamesOfAnonymousDelegates)
						{
							int num = text.IndexOf("+<>", StringComparison.Ordinal);
							if (num >= 0)
							{
								text = text.Substring(0, num);
							}
						}
						builder.Append(text);
					}
					else
					{
						builder.Append("<no type>");
					}
				}
				if (this.MethodName)
				{
					if (this.ClassName)
					{
						builder.Append(".");
					}
					if (method != null)
					{
						string text2 = method.Name;
						if (this.CleanNamesOfAnonymousDelegates && text2.Contains("__") && text2.StartsWith("<") && text2.Contains(">"))
						{
							int num2 = text2.IndexOf('<') + 1;
							int num3 = text2.IndexOf('>');
							text2 = text2.Substring(num2, num3 - num2);
						}
						builder.Append(text2);
					}
					else
					{
						builder.Append("<no method>");
					}
				}
				if (this.FileName)
				{
					string fileName = stackFrame.GetFileName();
					if (fileName != null)
					{
						builder.Append("(");
						if (this.IncludeSourcePath)
						{
							builder.Append(fileName);
						}
						else
						{
							builder.Append(Path.GetFileName(fileName));
						}
						builder.Append(":");
						builder.Append(stackFrame.GetFileLineNumber());
						builder.Append(")");
					}
				}
			}
		}
	}
}
