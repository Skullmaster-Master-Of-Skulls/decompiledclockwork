using System;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Data
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public sealed class PropertyConstraintException : ConstraintException
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002889 File Offset: 0x00000A89
		public PropertyConstraintException()
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002891 File Offset: 0x00000A91
		public PropertyConstraintException(string message) : base(message)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000289A File Offset: 0x00000A9A
		public PropertyConstraintException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000028A4 File Offset: 0x00000AA4
		public PropertyConstraintException(string message, string propertyName) : base(message)
		{
			EntityUtil.CheckStringArgument(propertyName, "propertyName");
			this._propertyName = propertyName;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000028BF File Offset: 0x00000ABF
		public PropertyConstraintException(string message, string propertyName, Exception innerException) : base(message, innerException)
		{
			EntityUtil.CheckStringArgument(propertyName, "propertyName");
			this._propertyName = propertyName;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000028DB File Offset: 0x00000ADB
		private PropertyConstraintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info != null)
			{
				this._propertyName = info.GetString("PropertyName");
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000028F9 File Offset: 0x00000AF9
		[SecurityCritical]
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("PropertyName", this._propertyName);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002914 File Offset: 0x00000B14
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x0400007A RID: 122
		private string _propertyName;
	}
}
