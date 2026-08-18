using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02000076 RID: 118
	[Serializable]
	public class StackFrameItem
	{
		// Token: 0x0600043C RID: 1084 RVA: 0x0000DF4C File Offset: 0x0000C14C
		public StackFrameItem(StackFrame frame)
		{
			this.m_lineNumber = "?";
			this.m_fileName = "?";
			this.m_method = new MethodItem();
			this.m_className = "?";
			try
			{
				this.m_lineNumber = frame.GetFileLineNumber().ToString(NumberFormatInfo.InvariantInfo);
				this.m_fileName = frame.GetFileName();
				MethodBase method = frame.GetMethod();
				if (method != null)
				{
					if (method.DeclaringType != null)
					{
						this.m_className = method.DeclaringType.FullName;
					}
					this.m_method = new MethodItem(method);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(StackFrameItem.declaringType, "An exception ocurred while retreiving stack frame information.", exception);
			}
			this.m_fullInfo = string.Concat(new object[]
			{
				this.m_className,
				'.',
				this.m_method.Name,
				'(',
				this.m_fileName,
				':',
				this.m_lineNumber,
				')'
			});
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000E074 File Offset: 0x0000C274
		public string ClassName
		{
			get
			{
				return this.m_className;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000E07C File Offset: 0x0000C27C
		public string FileName
		{
			get
			{
				return this.m_fileName;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000E084 File Offset: 0x0000C284
		public string LineNumber
		{
			get
			{
				return this.m_lineNumber;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000E08C File Offset: 0x0000C28C
		public MethodItem Method
		{
			get
			{
				return this.m_method;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000E094 File Offset: 0x0000C294
		public string FullInfo
		{
			get
			{
				return this.m_fullInfo;
			}
		}

		// Token: 0x040001CD RID: 461
		private const string NA = "?";

		// Token: 0x040001CE RID: 462
		private readonly string m_lineNumber;

		// Token: 0x040001CF RID: 463
		private readonly string m_fileName;

		// Token: 0x040001D0 RID: 464
		private readonly string m_className;

		// Token: 0x040001D1 RID: 465
		private readonly string m_fullInfo;

		// Token: 0x040001D2 RID: 466
		private readonly MethodItem m_method;

		// Token: 0x040001D3 RID: 467
		private static readonly Type declaringType = typeof(StackFrameItem);
	}
}
