using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x0200075F RID: 1887
	[ComVisible(true)]
	public class ActivatedServiceTypeEntry : TypeEntry
	{
		// Token: 0x0600432B RID: 17195 RVA: 0x000E5829 File Offset: 0x000E4829
		public ActivatedServiceTypeEntry(string typeName, string assemblyName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x000E585B File Offset: 0x000E485B
		public ActivatedServiceTypeEntry(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			base.TypeName = type.FullName;
			base.AssemblyName = type.Module.Assembly.nGetSimpleName();
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x0600432D RID: 17197 RVA: 0x000E5894 File Offset: 0x000E4894
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeType.PrivateGetType(base.TypeName + ", " + base.AssemblyName, false, false, ref stackCrawlMark);
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600432E RID: 17198 RVA: 0x000E58C2 File Offset: 0x000E48C2
		// (set) Token: 0x0600432F RID: 17199 RVA: 0x000E58CA File Offset: 0x000E48CA
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

		// Token: 0x06004330 RID: 17200 RVA: 0x000E58D4 File Offset: 0x000E48D4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"type='",
				base.TypeName,
				", ",
				base.AssemblyName,
				"'"
			});
		}

		// Token: 0x040021C9 RID: 8649
		private IContextAttribute[] _contextAttributes;
	}
}
