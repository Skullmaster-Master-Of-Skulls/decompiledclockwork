using System;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics.PerformanceData
{
	// Token: 0x0200029F RID: 671
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CounterData
	{
		// Token: 0x06001868 RID: 6248 RVA: 0x00058828 File Offset: 0x00056A28
		[SecurityCritical]
		internal unsafe CounterData(long* pCounterData)
		{
			this.m_offset = pCounterData;
			*this.m_offset = 0L;
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x00058840 File Offset: 0x00056A40
		// (set) Token: 0x0600186A RID: 6250 RVA: 0x0005884D File Offset: 0x00056A4D
		public unsafe long Value
		{
			[SecurityCritical]
			get
			{
				return Interlocked.Read(ref *this.m_offset);
			}
			[SecurityCritical]
			set
			{
				Interlocked.Exchange(ref *this.m_offset, value);
			}
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0005885C File Offset: 0x00056A5C
		[SecurityCritical]
		public unsafe void Increment()
		{
			Interlocked.Increment(ref *this.m_offset);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0005886A File Offset: 0x00056A6A
		[SecurityCritical]
		public unsafe void Decrement()
		{
			Interlocked.Decrement(ref *this.m_offset);
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00058878 File Offset: 0x00056A78
		[SecurityCritical]
		public unsafe void IncrementBy(long value)
		{
			Interlocked.Add(ref *this.m_offset, value);
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x00058887 File Offset: 0x00056A87
		// (set) Token: 0x0600186F RID: 6255 RVA: 0x00058890 File Offset: 0x00056A90
		public unsafe long RawValue
		{
			[SecurityCritical]
			get
			{
				return *this.m_offset;
			}
			[SecurityCritical]
			set
			{
				*this.m_offset = value;
			}
		}

		// Token: 0x04000BAE RID: 2990
		[SecurityCritical]
		private unsafe long* m_offset;
	}
}
