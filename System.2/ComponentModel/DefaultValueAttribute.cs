using System;

namespace System.ComponentModel
{
	// Token: 0x0200053E RID: 1342
	[AttributeUsage(AttributeTargets.All)]
	[__DynamicallyInvokable]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x06003290 RID: 12944 RVA: 0x000E2598 File Offset: 0x000E0798
		[__DynamicallyInvokable]
		public DefaultValueAttribute(Type type, string value)
		{
			try
			{
				this.value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
			}
			catch
			{
			}
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000E25D4 File Offset: 0x000E07D4
		[__DynamicallyInvokable]
		public DefaultValueAttribute(char value)
		{
			this.value = value;
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000E25E8 File Offset: 0x000E07E8
		[__DynamicallyInvokable]
		public DefaultValueAttribute(byte value)
		{
			this.value = value;
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000E25FC File Offset: 0x000E07FC
		[__DynamicallyInvokable]
		public DefaultValueAttribute(short value)
		{
			this.value = value;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000E2610 File Offset: 0x000E0810
		[__DynamicallyInvokable]
		public DefaultValueAttribute(int value)
		{
			this.value = value;
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000E2624 File Offset: 0x000E0824
		[__DynamicallyInvokable]
		public DefaultValueAttribute(long value)
		{
			this.value = value;
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x000E2638 File Offset: 0x000E0838
		[__DynamicallyInvokable]
		public DefaultValueAttribute(float value)
		{
			this.value = value;
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000E264C File Offset: 0x000E084C
		[__DynamicallyInvokable]
		public DefaultValueAttribute(double value)
		{
			this.value = value;
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000E2660 File Offset: 0x000E0860
		[__DynamicallyInvokable]
		public DefaultValueAttribute(bool value)
		{
			this.value = value;
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x000E2674 File Offset: 0x000E0874
		[__DynamicallyInvokable]
		public DefaultValueAttribute(string value)
		{
			this.value = value;
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x000E2683 File Offset: 0x000E0883
		[__DynamicallyInvokable]
		public DefaultValueAttribute(object value)
		{
			this.value = value;
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x000E2692 File Offset: 0x000E0892
		[__DynamicallyInvokable]
		public virtual object Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x000E269C File Offset: 0x000E089C
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (this.Value != null)
			{
				return this.Value.Equals(defaultValueAttribute.Value);
			}
			return defaultValueAttribute.Value == null;
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x000E26DE File Offset: 0x000E08DE
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x000E26E6 File Offset: 0x000E08E6
		protected void SetValue(object value)
		{
			this.value = value;
		}

		// Token: 0x04002985 RID: 10629
		private object value;
	}
}
