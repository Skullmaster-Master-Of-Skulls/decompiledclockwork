using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.SocialShare
{
	// Token: 0x02000F00 RID: 3840
	public class GenericSocialButtonsCollection<ItemType> : StateManagedCollection where ItemType : RadSocialButtonBase
	{
		// Token: 0x17002E0E RID: 11790
		public virtual ItemType this[int index]
		{
			get
			{
				return (ItemType)((object)this.List[index]);
			}
			set
			{
				this.List[index] = value;
			}
		}

		// Token: 0x17002E0F RID: 11791
		// (get) Token: 0x060091CC RID: 37324 RVA: 0x0020CE04 File Offset: 0x0020B004
		protected IList List
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060091CD RID: 37325 RVA: 0x0020CE08 File Offset: 0x0020B008
		public virtual void Add(ItemType item)
		{
			this.List.Add(item);
			RadSocialButton radSocialButton = item as RadSocialButton;
			if (item.SocialNetType == SocialNetType.CompactButton)
			{
				this._compactButton = radSocialButton;
			}
			if (item.SocialNetType == SocialNetType.SendEmail)
			{
				this._emailButton = radSocialButton;
			}
		}

		// Token: 0x060091CE RID: 37326 RVA: 0x0020CE64 File Offset: 0x0020B064
		public virtual bool Contains(ItemType item)
		{
			foreach (object obj in this.List)
			{
				ItemType itemType = (ItemType)((object)obj);
				if (itemType.SocialNetType == item.SocialNetType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060091CF RID: 37327 RVA: 0x0020CEDC File Offset: 0x0020B0DC
		public virtual void CopyTo(ItemType[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		// Token: 0x060091D0 RID: 37328 RVA: 0x0020CEEC File Offset: 0x0020B0EC
		public virtual void AddRange(IEnumerable<ItemType> items)
		{
			foreach (ItemType item in items)
			{
				this.Add(item);
			}
		}

		// Token: 0x060091D1 RID: 37329 RVA: 0x0020CF34 File Offset: 0x0020B134
		public virtual int IndexOf(ItemType item)
		{
			return this.List.IndexOf(item);
		}

		// Token: 0x060091D2 RID: 37330 RVA: 0x0020CF47 File Offset: 0x0020B147
		public virtual void Insert(int index, ItemType item)
		{
			this.List.Insert(index, item);
		}

		// Token: 0x060091D3 RID: 37331 RVA: 0x0020CF5B File Offset: 0x0020B15B
		public virtual void Remove(ItemType item)
		{
			this.List.Remove(item);
		}

		// Token: 0x060091D4 RID: 37332 RVA: 0x0020CF6E File Offset: 0x0020B16E
		public virtual void RemoveAt(int index)
		{
			this.List.RemoveAt(index);
		}

		// Token: 0x060091D5 RID: 37333 RVA: 0x0020CF7C File Offset: 0x0020B17C
		public new virtual void Clear()
		{
			this.List.Clear();
			this._compactButton = null;
			this._emailButton = null;
		}

		// Token: 0x060091D6 RID: 37334 RVA: 0x0020CF98 File Offset: 0x0020B198
		protected override Type[] GetKnownTypes()
		{
			return new Type[]
			{
				typeof(RadSocialButton),
				typeof(RadFacebookButton),
				typeof(RadTwitterButton),
				typeof(RadGoogleButton)
			};
		}

		// Token: 0x060091D7 RID: 37335 RVA: 0x0020CFE4 File Offset: 0x0020B1E4
		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
			case 0:
				return new RadSocialButton();
			case 1:
				return new RadFacebookButton();
			case 2:
				return new RadTwitterButton();
			case 3:
				return new RadGoogleButton();
			default:
				return null;
			}
		}

		// Token: 0x060091D8 RID: 37336 RVA: 0x0020D024 File Offset: 0x0020B224
		protected override void SetDirtyObject(object o)
		{
			((RadSocialButtonBase)o).SetDirty();
		}

		// Token: 0x040029D5 RID: 10709
		internal RadSocialButton _compactButton;

		// Token: 0x040029D6 RID: 10710
		internal RadSocialButton _emailButton;
	}
}
