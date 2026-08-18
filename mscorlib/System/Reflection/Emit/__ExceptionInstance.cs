using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000832 RID: 2098
	internal struct __ExceptionInstance
	{
		// Token: 0x06004AE1 RID: 19169 RVA: 0x001041B7 File Offset: 0x001031B7
		internal __ExceptionInstance(int start, int end, int filterAddr, int handle, int handleEnd, int type, int exceptionClass)
		{
			this.m_startAddress = start;
			this.m_endAddress = end;
			this.m_filterAddress = filterAddr;
			this.m_handleAddress = handle;
			this.m_handleEndAddress = handleEnd;
			this.m_type = type;
			this.m_exceptionClass = exceptionClass;
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x001041F0 File Offset: 0x001031F0
		public override bool Equals(object obj)
		{
			if (obj != null && obj is __ExceptionInstance)
			{
				__ExceptionInstance _ExceptionInstance = (__ExceptionInstance)obj;
				return _ExceptionInstance.m_exceptionClass == this.m_exceptionClass && _ExceptionInstance.m_startAddress == this.m_startAddress && _ExceptionInstance.m_endAddress == this.m_endAddress && _ExceptionInstance.m_filterAddress == this.m_filterAddress && _ExceptionInstance.m_handleAddress == this.m_handleAddress && _ExceptionInstance.m_handleEndAddress == this.m_handleEndAddress;
			}
			return false;
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x0010426D File Offset: 0x0010326D
		public override int GetHashCode()
		{
			return this.m_exceptionClass ^ this.m_startAddress ^ this.m_endAddress ^ this.m_filterAddress ^ this.m_handleAddress ^ this.m_handleEndAddress ^ this.m_type;
		}

		// Token: 0x04002650 RID: 9808
		internal int m_exceptionClass;

		// Token: 0x04002651 RID: 9809
		internal int m_startAddress;

		// Token: 0x04002652 RID: 9810
		internal int m_endAddress;

		// Token: 0x04002653 RID: 9811
		internal int m_filterAddress;

		// Token: 0x04002654 RID: 9812
		internal int m_handleAddress;

		// Token: 0x04002655 RID: 9813
		internal int m_handleEndAddress;

		// Token: 0x04002656 RID: 9814
		internal int m_type;
	}
}
