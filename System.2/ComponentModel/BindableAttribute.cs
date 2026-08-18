using System;

namespace System.ComponentModel
{
	// Token: 0x0200051A RID: 1306
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BindableAttribute : Attribute
	{
		// Token: 0x06003182 RID: 12674 RVA: 0x000DFBB0 File Offset: 0x000DDDB0
		public BindableAttribute(bool bindable) : this(bindable, BindingDirection.OneWay)
		{
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000DFBBA File Offset: 0x000DDDBA
		public BindableAttribute(bool bindable, BindingDirection direction)
		{
			this.bindable = bindable;
			this.direction = direction;
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000DFBD0 File Offset: 0x000DDDD0
		public BindableAttribute(BindableSupport flags) : this(flags, BindingDirection.OneWay)
		{
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000DFBDA File Offset: 0x000DDDDA
		public BindableAttribute(BindableSupport flags, BindingDirection direction)
		{
			this.bindable = (flags > BindableSupport.No);
			this.isDefault = (flags == BindableSupport.Default);
			this.direction = direction;
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06003186 RID: 12678 RVA: 0x000DFBFD File Offset: 0x000DDDFD
		public bool Bindable
		{
			get
			{
				return this.bindable;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x000DFC05 File Offset: 0x000DDE05
		public BindingDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000DFC0D File Offset: 0x000DDE0D
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is BindableAttribute && ((BindableAttribute)obj).Bindable == this.bindable);
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000DFC35 File Offset: 0x000DDE35
		public override int GetHashCode()
		{
			return this.bindable.GetHashCode();
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000DFC42 File Offset: 0x000DDE42
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BindableAttribute.Default) || this.isDefault;
		}

		// Token: 0x04002927 RID: 10535
		public static readonly BindableAttribute Yes = new BindableAttribute(true);

		// Token: 0x04002928 RID: 10536
		public static readonly BindableAttribute No = new BindableAttribute(false);

		// Token: 0x04002929 RID: 10537
		public static readonly BindableAttribute Default = BindableAttribute.No;

		// Token: 0x0400292A RID: 10538
		private bool bindable;

		// Token: 0x0400292B RID: 10539
		private bool isDefault;

		// Token: 0x0400292C RID: 10540
		private BindingDirection direction;
	}
}
