using System;

namespace System.ComponentModel
{
	// Token: 0x0200054C RID: 1356
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
	[__DynamicallyInvokable]
	public sealed class EditorBrowsableAttribute : Attribute
	{
		// Token: 0x060032F9 RID: 13049 RVA: 0x000E2EC3 File Offset: 0x000E10C3
		[__DynamicallyInvokable]
		public EditorBrowsableAttribute(EditorBrowsableState state)
		{
			this.browsableState = state;
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000E2ED2 File Offset: 0x000E10D2
		public EditorBrowsableAttribute() : this(EditorBrowsableState.Always)
		{
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x060032FB RID: 13051 RVA: 0x000E2EDB File Offset: 0x000E10DB
		[__DynamicallyInvokable]
		public EditorBrowsableState State
		{
			[__DynamicallyInvokable]
			get
			{
				return this.browsableState;
			}
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000E2EE4 File Offset: 0x000E10E4
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			EditorBrowsableAttribute editorBrowsableAttribute = obj as EditorBrowsableAttribute;
			return editorBrowsableAttribute != null && editorBrowsableAttribute.browsableState == this.browsableState;
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000E2F11 File Offset: 0x000E1111
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040029AA RID: 10666
		private EditorBrowsableState browsableState;
	}
}
