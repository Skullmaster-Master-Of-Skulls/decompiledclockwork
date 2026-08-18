using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NLog
{
	// Token: 0x02000121 RID: 289
	public class LogFactory<T> : LogFactory where T : Logger
	{
		// Token: 0x0600085E RID: 2142 RVA: 0x000136A7 File Offset: 0x000118A7
		public new T GetLogger(string name)
		{
			return (T)((object)base.GetLogger(name, typeof(T)));
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000136C0 File Offset: 0x000118C0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public new T GetCurrentClassLogger()
		{
			StackFrame stackFrame = new StackFrame(1, false);
			return this.GetLogger(stackFrame.GetMethod().DeclaringType.FullName);
		}
	}
}
