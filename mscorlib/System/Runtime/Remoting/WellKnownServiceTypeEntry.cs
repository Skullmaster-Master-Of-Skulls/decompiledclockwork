using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x02000761 RID: 1889
	[ComVisible(true)]
	public class WellKnownServiceTypeEntry : TypeEntry
	{
		// Token: 0x06004338 RID: 17208 RVA: 0x000E5A78 File Offset: 0x000E4A78
		public WellKnownServiceTypeEntry(string typeName, string assemblyName, string objectUri, WellKnownObjectMode mode)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (objectUri == null)
			{
				throw new ArgumentNullException("objectUri");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
			this._objectUri = objectUri;
			this._mode = mode;
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x000E5AD4 File Offset: 0x000E4AD4
		public WellKnownServiceTypeEntry(Type type, string objectUri, WellKnownObjectMode mode)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (objectUri == null)
			{
				throw new ArgumentNullException("objectUri");
			}
			base.TypeName = type.FullName;
			base.AssemblyName = type.Module.Assembly.FullName;
			this._objectUri = objectUri;
			this._mode = mode;
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x0600433A RID: 17210 RVA: 0x000E5B33 File Offset: 0x000E4B33
		public string ObjectUri
		{
			get
			{
				return this._objectUri;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x0600433B RID: 17211 RVA: 0x000E5B3B File Offset: 0x000E4B3B
		public WellKnownObjectMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x0600433C RID: 17212 RVA: 0x000E5B44 File Offset: 0x000E4B44
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeType.PrivateGetType(base.TypeName + ", " + base.AssemblyName, false, false, ref stackCrawlMark);
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x000E5B72 File Offset: 0x000E4B72
		// (set) Token: 0x0600433E RID: 17214 RVA: 0x000E5B7A File Offset: 0x000E4B7A
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

		// Token: 0x0600433F RID: 17215 RVA: 0x000E5B84 File Offset: 0x000E4B84
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"type='",
				base.TypeName,
				", ",
				base.AssemblyName,
				"'; objectUri=",
				this._objectUri,
				"; mode=",
				this._mode.ToString()
			});
		}

		// Token: 0x040021CC RID: 8652
		private string _objectUri;

		// Token: 0x040021CD RID: 8653
		private WellKnownObjectMode _mode;

		// Token: 0x040021CE RID: 8654
		private IContextAttribute[] _contextAttributes;
	}
}
