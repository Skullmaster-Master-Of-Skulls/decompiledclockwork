using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001AB8 RID: 6840
	public class NavigationItemBindingCollection : StronglyTypedStateManagedCollection<NavigationItemBinding>
	{
		// Token: 0x06010894 RID: 67732 RVA: 0x003B100C File Offset: 0x003AF20C
		private void FindDefaultBinding()
		{
			this._defaultBinding = null;
			foreach (object obj in this)
			{
				NavigationItemBinding navigationItemBinding = (NavigationItemBinding)obj;
				if (navigationItemBinding.Depth == -1 && navigationItemBinding.DataMember.Length == 0)
				{
					this._defaultBinding = navigationItemBinding;
					break;
				}
			}
		}

		// Token: 0x06010895 RID: 67733 RVA: 0x003B1080 File Offset: 0x003AF280
		protected override void OnClear()
		{
			this._defaultBinding = null;
		}

		// Token: 0x06010896 RID: 67734 RVA: 0x003B1089 File Offset: 0x003AF289
		protected override void OnRemoveComplete(int index, object value)
		{
			if (value == this._defaultBinding)
			{
				this.FindDefaultBinding();
			}
		}

		// Token: 0x06010897 RID: 67735 RVA: 0x003B109C File Offset: 0x003AF29C
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			NavigationItemBinding navigationItemBinding = (NavigationItemBinding)value;
			if (navigationItemBinding.DataMember.Length == 0)
			{
				this._defaultBinding = navigationItemBinding;
			}
		}

		// Token: 0x06010898 RID: 67736 RVA: 0x003B10CC File Offset: 0x003AF2CC
		public NavigationItemBinding GetBinding(string dataMember, int depth)
		{
			NavigationItemBinding navigationItemBinding = null;
			int num = 0;
			if (string.IsNullOrEmpty(dataMember))
			{
				dataMember = null;
			}
			foreach (object obj in this)
			{
				NavigationItemBinding navigationItemBinding2 = (NavigationItemBinding)obj;
				if (navigationItemBinding2.Depth == depth)
				{
					if (navigationItemBinding2.DataMember == dataMember)
					{
						return navigationItemBinding2;
					}
					if (num < 1 && navigationItemBinding2.DataMember.Length == 0)
					{
						navigationItemBinding = navigationItemBinding2;
						num = 1;
					}
				}
				else if (string.Equals(navigationItemBinding2.DataMember, dataMember, StringComparison.CurrentCultureIgnoreCase) && num < 2 && navigationItemBinding2.Depth == -1)
				{
					navigationItemBinding = navigationItemBinding2;
					num = 2;
				}
			}
			if (navigationItemBinding != null || this._defaultBinding == null)
			{
				return navigationItemBinding;
			}
			if (this._defaultBinding != null || this._defaultBinding.DataMember.Length != 0)
			{
				this.FindDefaultBinding();
			}
			return this._defaultBinding;
		}

		// Token: 0x040049FF RID: 18943
		private NavigationItemBinding _defaultBinding;
	}
}
