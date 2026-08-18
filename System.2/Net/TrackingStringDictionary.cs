using System;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x02000228 RID: 552
	internal class TrackingStringDictionary : StringDictionary
	{
		// Token: 0x06001460 RID: 5216 RVA: 0x0006BC20 File Offset: 0x00069E20
		internal TrackingStringDictionary() : this(false)
		{
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0006BC29 File Offset: 0x00069E29
		internal TrackingStringDictionary(bool isReadOnly)
		{
			this.isReadOnly = isReadOnly;
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0006BC38 File Offset: 0x00069E38
		// (set) Token: 0x06001463 RID: 5219 RVA: 0x0006BC40 File Offset: 0x00069E40
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

		// Token: 0x06001464 RID: 5220 RVA: 0x0006BC49 File Offset: 0x00069E49
		public override void Add(string key, string value)
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("MailCollectionIsReadOnly"));
			}
			base.Add(key, value);
			this.isChanged = true;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0006BC72 File Offset: 0x00069E72
		public override void Clear()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("MailCollectionIsReadOnly"));
			}
			base.Clear();
			this.isChanged = true;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0006BC99 File Offset: 0x00069E99
		public override void Remove(string key)
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("MailCollectionIsReadOnly"));
			}
			base.Remove(key);
			this.isChanged = true;
		}

		// Token: 0x17000446 RID: 1094
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

		// Token: 0x0400162F RID: 5679
		private bool isChanged;

		// Token: 0x04001630 RID: 5680
		private bool isReadOnly;
	}
}
