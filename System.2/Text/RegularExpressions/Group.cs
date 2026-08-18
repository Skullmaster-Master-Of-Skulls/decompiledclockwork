using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000698 RID: 1688
	[__DynamicallyInvokable]
	[Serializable]
	public class Group : Capture
	{
		// Token: 0x06003ECF RID: 16079 RVA: 0x001059EF File Offset: 0x00103BEF
		internal Group(string text, int[] caps, int capcount, string name) : base(text, (capcount == 0) ? 0 : caps[(capcount - 1) * 2], (capcount == 0) ? 0 : caps[capcount * 2 - 1])
		{
			this._caps = caps;
			this._capcount = capcount;
			this._name = name;
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x00105A28 File Offset: 0x00103C28
		[__DynamicallyInvokable]
		public bool Success
		{
			[__DynamicallyInvokable]
			get
			{
				return this._capcount != 0;
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06003ED1 RID: 16081 RVA: 0x00105A33 File Offset: 0x00103C33
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06003ED2 RID: 16082 RVA: 0x00105A3B File Offset: 0x00103C3B
		[__DynamicallyInvokable]
		public CaptureCollection Captures
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._capcoll == null)
				{
					this._capcoll = new CaptureCollection(this);
				}
				return this._capcoll;
			}
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x00105A58 File Offset: 0x00103C58
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Group Synchronized(Group inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			CaptureCollection captures = inner.Captures;
			if (inner._capcount > 0)
			{
				Capture capture = captures[0];
			}
			return inner;
		}

		// Token: 0x04002DDE RID: 11742
		internal static Group _emptygroup = new Group(string.Empty, new int[0], 0, string.Empty);

		// Token: 0x04002DDF RID: 11743
		internal int[] _caps;

		// Token: 0x04002DE0 RID: 11744
		internal int _capcount;

		// Token: 0x04002DE1 RID: 11745
		internal CaptureCollection _capcoll;

		// Token: 0x04002DE2 RID: 11746
		[OptionalField]
		internal string _name;
	}
}
