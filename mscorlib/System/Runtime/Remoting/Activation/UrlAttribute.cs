using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020007A9 RID: 1961
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlAttribute : ContextAttribute
	{
		// Token: 0x060045BC RID: 17852 RVA: 0x000ED34F File Offset: 0x000EC34F
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public UrlAttribute(string callsiteURL) : base(UrlAttribute.propertyName)
		{
			if (callsiteURL == null)
			{
				throw new ArgumentNullException("callsiteURL");
			}
			this.url = callsiteURL;
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x000ED371 File Offset: 0x000EC371
		public override bool Equals(object o)
		{
			return o is IContextProperty && o is UrlAttribute && ((UrlAttribute)o).UrlValue.Equals(this.url);
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x000ED39B File Offset: 0x000EC39B
		public override int GetHashCode()
		{
			return this.url.GetHashCode();
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x000ED3A8 File Offset: 0x000EC3A8
		[ComVisible(true)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return false;
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x000ED3AB File Offset: 0x000EC3AB
		[ComVisible(true)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x060045C1 RID: 17857 RVA: 0x000ED3AD File Offset: 0x000EC3AD
		public string UrlValue
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
			get
			{
				return this.url;
			}
		}

		// Token: 0x040022A5 RID: 8869
		private string url;

		// Token: 0x040022A6 RID: 8870
		private static string propertyName = "UrlAttribute";
	}
}
