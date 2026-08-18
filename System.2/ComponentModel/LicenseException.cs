using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200057E RID: 1406
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class LicenseException : SystemException
	{
		// Token: 0x06003403 RID: 13315 RVA: 0x000E4400 File Offset: 0x000E2600
		public LicenseException(Type type) : this(type, null, SR.GetString("LicExceptionTypeOnly", new object[]
		{
			type.FullName
		}))
		{
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000E4423 File Offset: 0x000E2623
		public LicenseException(Type type, object instance) : this(type, null, SR.GetString("LicExceptionTypeAndInstance", new object[]
		{
			type.FullName,
			instance.GetType().FullName
		}))
		{
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000E4454 File Offset: 0x000E2654
		public LicenseException(Type type, object instance, string message) : base(message)
		{
			this.type = type;
			this.instance = instance;
			base.HResult = -2146232063;
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000E4476 File Offset: 0x000E2676
		public LicenseException(Type type, object instance, string message, Exception innerException) : base(message, innerException)
		{
			this.type = type;
			this.instance = instance;
			base.HResult = -2146232063;
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000E449C File Offset: 0x000E269C
		protected LicenseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (Type)info.GetValue("type", typeof(Type));
			this.instance = info.GetValue("instance", typeof(object));
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06003408 RID: 13320 RVA: 0x000E44EC File Offset: 0x000E26EC
		public Type LicensedType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x000E44F4 File Offset: 0x000E26F4
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("type", this.type);
			info.AddValue("instance", this.instance);
			base.GetObjectData(info, context);
		}

		// Token: 0x040029C9 RID: 10697
		private Type type;

		// Token: 0x040029CA RID: 10698
		private object instance;
	}
}
