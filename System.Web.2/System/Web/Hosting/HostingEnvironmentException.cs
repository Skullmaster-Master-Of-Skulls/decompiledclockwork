using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020007AE RID: 1966
	[Serializable]
	internal class HostingEnvironmentException : Exception
	{
		// Token: 0x06005DAC RID: 23980 RVA: 0x00144D7A File Offset: 0x00142F7A
		protected HostingEnvironmentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._details = info.GetString("_details");
		}

		// Token: 0x06005DAD RID: 23981 RVA: 0x00144D95 File Offset: 0x00142F95
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("_details", this._details);
		}

		// Token: 0x06005DAE RID: 23982 RVA: 0x00144DB0 File Offset: 0x00142FB0
		internal HostingEnvironmentException(string message, string details) : base(message)
		{
			this._details = details;
		}

		// Token: 0x17001B56 RID: 6998
		// (get) Token: 0x06005DAF RID: 23983 RVA: 0x00144DC0 File Offset: 0x00142FC0
		internal string Details
		{
			get
			{
				if (this._details == null)
				{
					return string.Empty;
				}
				return this._details;
			}
		}

		// Token: 0x04003133 RID: 12595
		private string _details;
	}
}
