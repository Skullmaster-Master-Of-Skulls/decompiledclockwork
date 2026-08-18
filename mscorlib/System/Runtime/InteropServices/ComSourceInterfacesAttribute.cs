using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004EB RID: 1259
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public sealed class ComSourceInterfacesAttribute : Attribute
	{
		// Token: 0x0600315A RID: 12634 RVA: 0x000A914D File Offset: 0x000A814D
		public ComSourceInterfacesAttribute(string sourceInterfaces)
		{
			this._val = sourceInterfaces;
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000A915C File Offset: 0x000A815C
		public ComSourceInterfacesAttribute(Type sourceInterface)
		{
			this._val = sourceInterface.FullName;
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000A9170 File Offset: 0x000A8170
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2)
		{
			this._val = sourceInterface1.FullName + "\0" + sourceInterface2.FullName;
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000A9194 File Offset: 0x000A8194
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3)
		{
			this._val = string.Concat(new string[]
			{
				sourceInterface1.FullName,
				"\0",
				sourceInterface2.FullName,
				"\0",
				sourceInterface3.FullName
			});
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000A91E8 File Offset: 0x000A81E8
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3, Type sourceInterface4)
		{
			this._val = string.Concat(new string[]
			{
				sourceInterface1.FullName,
				"\0",
				sourceInterface2.FullName,
				"\0",
				sourceInterface3.FullName,
				"\0",
				sourceInterface4.FullName
			});
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000A924B File Offset: 0x000A824B
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001907 RID: 6407
		internal string _val;
	}
}
