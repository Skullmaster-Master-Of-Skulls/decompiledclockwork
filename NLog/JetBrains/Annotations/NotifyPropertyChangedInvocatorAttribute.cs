using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	internal sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002108 File Offset: 0x00000308
		public NotifyPropertyChangedInvocatorAttribute()
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002110 File Offset: 0x00000310
		public NotifyPropertyChangedInvocatorAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000211F File Offset: 0x0000031F
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002127 File Offset: 0x00000327
		public string ParameterName { get; private set; }
	}
}
