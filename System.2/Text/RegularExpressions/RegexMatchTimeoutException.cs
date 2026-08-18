using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006A0 RID: 1696
	[__DynamicallyInvokable]
	[Serializable]
	public class RegexMatchTimeoutException : TimeoutException, ISerializable
	{
		// Token: 0x06003F30 RID: 16176 RVA: 0x00107D49 File Offset: 0x00105F49
		[__DynamicallyInvokable]
		public RegexMatchTimeoutException(string regexInput, string regexPattern, TimeSpan matchTimeout) : base(SR.GetString("RegexMatchTimeoutException_Occurred"))
		{
			this.Init(regexInput, regexPattern, matchTimeout);
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x00107D71 File Offset: 0x00105F71
		[__DynamicallyInvokable]
		public RegexMatchTimeoutException()
		{
			this.Init();
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x00107D8C File Offset: 0x00105F8C
		[__DynamicallyInvokable]
		public RegexMatchTimeoutException(string message) : base(message)
		{
			this.Init();
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x00107DA8 File Offset: 0x00105FA8
		[__DynamicallyInvokable]
		public RegexMatchTimeoutException(string message, Exception inner) : base(message, inner)
		{
			this.Init();
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x00107DC8 File Offset: 0x00105FC8
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		protected RegexMatchTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			string @string = info.GetString("regexInput");
			string string2 = info.GetString("regexPattern");
			TimeSpan timeout = TimeSpan.FromTicks(info.GetInt64("timeoutTicks"));
			this.Init(@string, string2, timeout);
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x00107E1C File Offset: 0x0010601C
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			base.GetObjectData(si, context);
			si.AddValue("regexInput", this.regexInput);
			si.AddValue("regexPattern", this.regexPattern);
			si.AddValue("timeoutTicks", this.matchTimeout.Ticks);
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x00107E69 File Offset: 0x00106069
		private void Init()
		{
			this.Init("", "", TimeSpan.FromTicks(-1L));
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x00107E82 File Offset: 0x00106082
		private void Init(string input, string pattern, TimeSpan timeout)
		{
			this.regexInput = input;
			this.regexPattern = pattern;
			this.matchTimeout = timeout;
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06003F38 RID: 16184 RVA: 0x00107E99 File Offset: 0x00106099
		[__DynamicallyInvokable]
		public string Pattern
		{
			[__DynamicallyInvokable]
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this.regexPattern;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x00107EA1 File Offset: 0x001060A1
		[__DynamicallyInvokable]
		public string Input
		{
			[__DynamicallyInvokable]
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this.regexInput;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06003F3A RID: 16186 RVA: 0x00107EA9 File Offset: 0x001060A9
		[__DynamicallyInvokable]
		public TimeSpan MatchTimeout
		{
			[__DynamicallyInvokable]
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this.matchTimeout;
			}
		}

		// Token: 0x04002E0D RID: 11789
		private string regexInput;

		// Token: 0x04002E0E RID: 11790
		private string regexPattern;

		// Token: 0x04002E0F RID: 11791
		private TimeSpan matchTimeout = TimeSpan.FromTicks(-1L);
	}
}
