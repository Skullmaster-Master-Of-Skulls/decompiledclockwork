using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x02000760 RID: 1888
	[ComVisible(true)]
	public class WellKnownClientTypeEntry : TypeEntry
	{
		// Token: 0x06004331 RID: 17201 RVA: 0x000E5918 File Offset: 0x000E4918
		public WellKnownClientTypeEntry(string typeName, string assemblyName, string objectUrl)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (objectUrl == null)
			{
				throw new ArgumentNullException("objectUrl");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
			this._objectUrl = objectUrl;
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x000E596C File Offset: 0x000E496C
		public WellKnownClientTypeEntry(Type type, string objectUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (objectUrl == null)
			{
				throw new ArgumentNullException("objectUrl");
			}
			base.TypeName = type.FullName;
			base.AssemblyName = type.Module.Assembly.nGetSimpleName();
			this._objectUrl = objectUrl;
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06004333 RID: 17203 RVA: 0x000E59C4 File Offset: 0x000E49C4
		public string ObjectUrl
		{
			get
			{
				return this._objectUrl;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06004334 RID: 17204 RVA: 0x000E59CC File Offset: 0x000E49CC
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeType.PrivateGetType(base.TypeName + ", " + base.AssemblyName, false, false, ref stackCrawlMark);
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06004335 RID: 17205 RVA: 0x000E59FA File Offset: 0x000E49FA
		// (set) Token: 0x06004336 RID: 17206 RVA: 0x000E5A02 File Offset: 0x000E4A02
		public string ApplicationUrl
		{
			get
			{
				return this._appUrl;
			}
			set
			{
				this._appUrl = value;
			}
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x000E5A0C File Offset: 0x000E4A0C
		public override string ToString()
		{
			string text = string.Concat(new string[]
			{
				"type='",
				base.TypeName,
				", ",
				base.AssemblyName,
				"'; url=",
				this._objectUrl
			});
			if (this._appUrl != null)
			{
				text = text + "; appUrl=" + this._appUrl;
			}
			return text;
		}

		// Token: 0x040021CA RID: 8650
		private string _objectUrl;

		// Token: 0x040021CB RID: 8651
		private string _appUrl;
	}
}
