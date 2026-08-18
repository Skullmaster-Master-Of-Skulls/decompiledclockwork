using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	public class LocationInfo
	{
		// Token: 0x06000386 RID: 902 RVA: 0x0000C164 File Offset: 0x0000A364
		public LocationInfo(Type callerStackBoundaryDeclaringType)
		{
			this.m_className = "?";
			this.m_fileName = "?";
			this.m_lineNumber = "?";
			this.m_methodName = "?";
			this.m_fullInfo = "?";
			if (callerStackBoundaryDeclaringType != null)
			{
				try
				{
					StackTrace stackTrace = new StackTrace(true);
					for (int i = 0; i < stackTrace.FrameCount; i++)
					{
						StackFrame frame = stackTrace.GetFrame(i);
						if (frame != null && frame.GetMethod().DeclaringType == callerStackBoundaryDeclaringType)
						{
							IL_A3:
							while (i < stackTrace.FrameCount)
							{
								StackFrame frame2 = stackTrace.GetFrame(i);
								if (frame2 != null && frame2.GetMethod().DeclaringType != callerStackBoundaryDeclaringType)
								{
									break;
								}
								i++;
							}
							if (i < stackTrace.FrameCount)
							{
								int num = stackTrace.FrameCount - i;
								ArrayList arrayList = new ArrayList(num);
								this.m_stackFrames = new StackFrameItem[num];
								for (int j = i; j < stackTrace.FrameCount; j++)
								{
									arrayList.Add(new StackFrameItem(stackTrace.GetFrame(j)));
								}
								arrayList.CopyTo(this.m_stackFrames, 0);
								StackFrame frame3 = stackTrace.GetFrame(i);
								if (frame3 != null)
								{
									MethodBase method = frame3.GetMethod();
									if (method != null)
									{
										this.m_methodName = method.Name;
										if (method.DeclaringType != null)
										{
											this.m_className = method.DeclaringType.FullName;
										}
									}
									this.m_fileName = frame3.GetFileName();
									this.m_lineNumber = frame3.GetFileLineNumber().ToString(NumberFormatInfo.InvariantInfo);
									this.m_fullInfo = string.Concat(new object[]
									{
										this.m_className,
										'.',
										this.m_methodName,
										'(',
										this.m_fileName,
										':',
										this.m_lineNumber,
										')'
									});
								}
							}
							return;
						}
					}
					goto IL_A3;
				}
				catch (SecurityException)
				{
					LogLog.Debug(LocationInfo.declaringType, "Security exception while trying to get caller stack frame. Error Ignored. Location Information Not Available.");
				}
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000C394 File Offset: 0x0000A594
		public LocationInfo(string className, string methodName, string fileName, string lineNumber)
		{
			this.m_className = className;
			this.m_fileName = fileName;
			this.m_lineNumber = lineNumber;
			this.m_methodName = methodName;
			this.m_fullInfo = string.Concat(new object[]
			{
				this.m_className,
				'.',
				this.m_methodName,
				'(',
				this.m_fileName,
				':',
				this.m_lineNumber,
				')'
			});
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000C423 File Offset: 0x0000A623
		public string ClassName
		{
			get
			{
				return this.m_className;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000C42B File Offset: 0x0000A62B
		public string FileName
		{
			get
			{
				return this.m_fileName;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000C433 File Offset: 0x0000A633
		public string LineNumber
		{
			get
			{
				return this.m_lineNumber;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000C43B File Offset: 0x0000A63B
		public string MethodName
		{
			get
			{
				return this.m_methodName;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000C443 File Offset: 0x0000A643
		public string FullInfo
		{
			get
			{
				return this.m_fullInfo;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000C44B File Offset: 0x0000A64B
		public StackFrameItem[] StackFrames
		{
			get
			{
				return this.m_stackFrames;
			}
		}

		// Token: 0x04000190 RID: 400
		private const string NA = "?";

		// Token: 0x04000191 RID: 401
		private readonly string m_className;

		// Token: 0x04000192 RID: 402
		private readonly string m_fileName;

		// Token: 0x04000193 RID: 403
		private readonly string m_lineNumber;

		// Token: 0x04000194 RID: 404
		private readonly string m_methodName;

		// Token: 0x04000195 RID: 405
		private readonly string m_fullInfo;

		// Token: 0x04000196 RID: 406
		private readonly StackFrameItem[] m_stackFrames;

		// Token: 0x04000197 RID: 407
		private static readonly Type declaringType = typeof(LocationInfo);
	}
}
