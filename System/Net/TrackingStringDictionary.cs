using System;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x020006DE RID: 1758
	internal class TrackingStringDictionary : StringDictionary
	{
		// Token: 0x06003641 RID: 13889 RVA: 0x000E7C11 File Offset: 0x000E6C11
		internal TrackingStringDictionary() : this(false)
		{
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000E7C1A File Offset: 0x000E6C1A
		internal TrackingStringDictionary(bool isReadOnly)
		{
			this.isReadOnly = isReadOnly;
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06003643 RID: 13891 RVA: 0x000E7C29 File Offset: 0x000E6C29
		// (set) Token: 0x06003644 RID: 13892 RVA: 0x000E7C31 File Offset: 0x000E6C31
		internal bool IsChanged
		{
			get
			{
				return this.isChanged;
			}
			set
			{
				this.isChanged = value;
			}
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000E7C3A File Offset: 0x000E6C3A
		public override void Add(string key, string value)
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("MailCollectionIsReadOnly"));
			}
			base.Add(key, value);
			this.isChanged = true;
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000E7C63 File Offset: 0x000E6C63
		public override void Clear()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("MailCollectionIsReadOnly"));
			}
			base.Clear();
			this.isChanged = true;
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000E7C8A File Offset: 0x000E6C8A
		public override void Remove(string key)
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("MailCollectionIsReadOnly"));
			}
			base.Remove(key);
			this.isChanged = true;
		}

		// Token: 0x17000C8F RID: 3215
		public override string this[string key]
		{
			get
			{
				return base[key];
			}
			set
			{
				if (this.isReadOnly)
				{
					throw new InvalidOperationException(SR.GetString("MailCollectionIsReadOnly"));
				}
				base[key] = value;
				this.isChanged = true;
			}
		}

		// Token: 0x04003179 RID: 12665
		private bool isChanged;

		// Token: 0x0400317A RID: 12666
		private bool isReadOnly;
	}
}
