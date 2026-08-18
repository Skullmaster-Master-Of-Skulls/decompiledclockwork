using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x0200075E RID: 1886
	[ComVisible(true)]
	public class ActivatedClientTypeEntry : TypeEntry
	{
		// Token: 0x06004324 RID: 17188 RVA: 0x000E56E8 File Offset: 0x000E46E8
		public ActivatedClientTypeEntry(string typeName, string assemblyName, string appUrl)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (appUrl == null)
			{
				throw new ArgumentNullException("appUrl");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
			this._appUrl = appUrl;
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x000E573C File Offset: 0x000E473C
		public ActivatedClientTypeEntry(Type type, string appUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (appUrl == null)
			{
				throw new ArgumentNullException("appUrl");
			}
			base.TypeName = type.FullName;
			base.AssemblyName = type.Module.Assembly.nGetSimpleName();
			this._appUrl = appUrl;
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06004326 RID: 17190 RVA: 0x000E5794 File Offset: 0x000E4794
		public string ApplicationUrl
		{
			get
			{
				return this._appUrl;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06004327 RID: 17191 RVA: 0x000E579C File Offset: 0x000E479C
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeType.PrivateGetType(base.TypeName + ", " + base.AssemblyName, false, false, ref stackCrawlMark);
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06004328 RID: 17192 RVA: 0x000E57CA File Offset: 0x000E47CA
		// (set) Token: 0x06004329 RID: 17193 RVA: 0x000E57D2 File Offset: 0x000E47D2
		public IContextAttribute[] ContextAttributes
		{
			get
			{
				return this._contextAttributes;
			}
			set
			{
				this._contextAttributes = value;
			}
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x000E57DC File Offset: 0x000E47DC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"type='",
				base.TypeName,
				", ",
				base.AssemblyName,
				"'; appUrl=",
				this._appUrl
			});
		}

		// Token: 0x040021C7 RID: 8647
		private string _appUrl;

		// Token: 0x040021C8 RID: 8648
		private IContextAttribute[] _contextAttributes;
	}
}
