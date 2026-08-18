using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005BB RID: 1467
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class WarningException : SystemException
	{
		// Token: 0x0600370A RID: 14090 RVA: 0x000EFB7D File Offset: 0x000EDD7D
		public WarningException() : this(null, null, null)
		{
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x000EFB88 File Offset: 0x000EDD88
		public WarningException(string message) : this(message, null, null)
		{
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x000EFB93 File Offset: 0x000EDD93
		public WarningException(string message, string helpUrl) : this(message, helpUrl, null)
		{
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000EFB9E File Offset: 0x000EDD9E
		public WarningException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000EFBA8 File Offset: 0x000EDDA8
		public WarningException(string message, string helpUrl, string helpTopic) : base(message)
		{
			this.helpUrl = helpUrl;
			this.helpTopic = helpTopic;
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000EFBC0 File Offset: 0x000EDDC0
		protected WarningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.helpUrl = (string)info.GetValue("helpUrl", typeof(string));
			this.helpTopic = (string)info.GetValue("helpTopic", typeof(string));
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06003710 RID: 14096 RVA: 0x000EFC15 File Offset: 0x000EDE15
		public string HelpUrl
		{
			get
			{
				return this.helpUrl;
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06003711 RID: 14097 RVA: 0x000EFC1D File Offset: 0x000EDE1D
		public string HelpTopic
		{
			get
			{
				return this.helpTopic;
			}
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000EFC25 File Offset: 0x000EDE25
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("helpUrl", this.helpUrl);
			info.AddValue("helpTopic", this.helpTopic);
			base.GetObjectData(info, context);
		}

		// Token: 0x04002AC3 RID: 10947
		private readonly string helpUrl;

		// Token: 0x04002AC4 RID: 10948
		private readonly string helpTopic;
	}
}
