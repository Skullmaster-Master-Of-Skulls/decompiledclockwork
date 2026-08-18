using System;

namespace System.ComponentModel
{
	// Token: 0x020005AB RID: 1451
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsBindableAttribute : Attribute
	{
		// Token: 0x06003622 RID: 13858 RVA: 0x000EC791 File Offset: 0x000EA991
		public SettingsBindableAttribute(bool bindable)
		{
			this._bindable = bindable;
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06003623 RID: 13859 RVA: 0x000EC7A0 File Offset: 0x000EA9A0
		public bool Bindable
		{
			get
			{
				return this._bindable;
			}
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000EC7A8 File Offset: 0x000EA9A8
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is SettingsBindableAttribute && ((SettingsBindableAttribute)obj).Bindable == this._bindable);
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x000EC7D0 File Offset: 0x000EA9D0
		public override int GetHashCode()
		{
			return this._bindable.GetHashCode();
		}

		// Token: 0x04002A9B RID: 10907
		public static readonly SettingsBindableAttribute Yes = new SettingsBindableAttribute(true);

		// Token: 0x04002A9C RID: 10908
		public static readonly SettingsBindableAttribute No = new SettingsBindableAttribute(false);

		// Token: 0x04002A9D RID: 10909
		private bool _bindable;
	}
}
