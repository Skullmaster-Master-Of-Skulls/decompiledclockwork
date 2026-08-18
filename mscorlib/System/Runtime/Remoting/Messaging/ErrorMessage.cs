using System;
using System.Collections;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000724 RID: 1828
	internal class ErrorMessage : IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x0600418B RID: 16779 RVA: 0x000DF7C1 File Offset: 0x000DE7C1
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x0600418C RID: 16780 RVA: 0x000DF7C4 File Offset: 0x000DE7C4
		public string Uri
		{
			get
			{
				return this.m_URI;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x000DF7CC File Offset: 0x000DE7CC
		public string MethodName
		{
			get
			{
				return this.m_MethodName;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x0600418E RID: 16782 RVA: 0x000DF7D4 File Offset: 0x000DE7D4
		public string TypeName
		{
			get
			{
				return this.m_TypeName;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x0600418F RID: 16783 RVA: 0x000DF7DC File Offset: 0x000DE7DC
		public object MethodSignature
		{
			get
			{
				return this.m_MethodSignature;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06004190 RID: 16784 RVA: 0x000DF7E4 File Offset: 0x000DE7E4
		public MethodBase MethodBase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x000DF7E7 File Offset: 0x000DE7E7
		public int ArgCount
		{
			get
			{
				return this.m_ArgCount;
			}
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x000DF7EF File Offset: 0x000DE7EF
		public string GetArgName(int index)
		{
			return this.m_ArgName;
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x000DF7F7 File Offset: 0x000DE7F7
		public object GetArg(int argNum)
		{
			return null;
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06004194 RID: 16788 RVA: 0x000DF7FA File Offset: 0x000DE7FA
		public object[] Args
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x000DF7FD File Offset: 0x000DE7FD
		public bool HasVarArgs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x000DF800 File Offset: 0x000DE800
		public int InArgCount
		{
			get
			{
				return this.m_ArgCount;
			}
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x000DF808 File Offset: 0x000DE808
		public string GetInArgName(int index)
		{
			return null;
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x000DF80B File Offset: 0x000DE80B
		public object GetInArg(int argNum)
		{
			return null;
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06004199 RID: 16793 RVA: 0x000DF80E File Offset: 0x000DE80E
		public object[] InArgs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600419A RID: 16794 RVA: 0x000DF811 File Offset: 0x000DE811
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040020F0 RID: 8432
		private string m_URI = "Exception";

		// Token: 0x040020F1 RID: 8433
		private string m_MethodName = "Unknown";

		// Token: 0x040020F2 RID: 8434
		private string m_TypeName = "Unknown";

		// Token: 0x040020F3 RID: 8435
		private object m_MethodSignature;

		// Token: 0x040020F4 RID: 8436
		private int m_ArgCount;

		// Token: 0x040020F5 RID: 8437
		private string m_ArgName = "Unknown";
	}
}
