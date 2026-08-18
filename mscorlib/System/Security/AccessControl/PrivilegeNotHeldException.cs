using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security.AccessControl
{
	// Token: 0x02000931 RID: 2353
	[Serializable]
	public sealed class PrivilegeNotHeldException : UnauthorizedAccessException, ISerializable
	{
		// Token: 0x060054EF RID: 21743 RVA: 0x0013437C File Offset: 0x0013337C
		public PrivilegeNotHeldException() : base(Environment.GetResourceString("PrivilegeNotHeld_Default"))
		{
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x00134390 File Offset: 0x00133390
		public PrivilegeNotHeldException(string privilege) : base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("PrivilegeNotHeld_Named"), new object[]
		{
			privilege
		}))
		{
			this._privilegeName = privilege;
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x001343CC File Offset: 0x001333CC
		public PrivilegeNotHeldException(string privilege, Exception inner) : base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("PrivilegeNotHeld_Named"), new object[]
		{
			privilege
		}), inner)
		{
			this._privilegeName = privilege;
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x00134407 File Offset: 0x00133407
		internal PrivilegeNotHeldException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._privilegeName = info.GetString("PrivilegeName");
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x060054F3 RID: 21747 RVA: 0x00134422 File Offset: 0x00133422
		public string PrivilegeName
		{
			get
			{
				return this._privilegeName;
			}
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x0013442A File Offset: 0x0013342A
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("PrivilegeName", this._privilegeName, typeof(string));
		}

		// Token: 0x04002C25 RID: 11301
		private readonly string _privilegeName;
	}
}
