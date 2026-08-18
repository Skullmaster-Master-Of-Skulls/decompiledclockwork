using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x0200028E RID: 654
	[Serializable]
	internal class HostingEnvironmentException : Exception
	{
		// Token: 0x060021BB RID: 8635 RVA: 0x00093B67 File Offset: 0x00092B67
		protected HostingEnvironmentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._details = info.GetString("_details");
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x00093B82 File Offset: 0x00092B82
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("_details", this._details);
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x00093B9D File Offset: 0x00092B9D
		internal HostingEnvironmentException(string message, string details) : base(message)
		{
			this._details = details;
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x00093BAD File Offset: 0x00092BAD
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

		// Token: 0x04001B2F RID: 6959
		private string _details;
	}
}
