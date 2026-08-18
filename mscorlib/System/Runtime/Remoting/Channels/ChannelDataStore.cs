using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006ED RID: 1773
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class ChannelDataStore : IChannelDataStore
	{
		// Token: 0x06003F56 RID: 16214 RVA: 0x000D8492 File Offset: 0x000D7492
		private ChannelDataStore(string[] channelUrls, DictionaryEntry[] extraData)
		{
			this._channelURIs = channelUrls;
			this._extraData = extraData;
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x000D84A8 File Offset: 0x000D74A8
		public ChannelDataStore(string[] channelURIs)
		{
			this._channelURIs = channelURIs;
			this._extraData = null;
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x000D84BE File Offset: 0x000D74BE
		internal ChannelDataStore InternalShallowCopy()
		{
			return new ChannelDataStore(this._channelURIs, this._extraData);
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06003F59 RID: 16217 RVA: 0x000D84D1 File Offset: 0x000D74D1
		// (set) Token: 0x06003F5A RID: 16218 RVA: 0x000D84D9 File Offset: 0x000D74D9
		public string[] ChannelUris
		{
			get
			{
				return this._channelURIs;
			}
			set
			{
				this._channelURIs = value;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		public object this[object key]
		{
			get
			{
				foreach (DictionaryEntry dictionaryEntry in this._extraData)
				{
					if (dictionaryEntry.Key.Equals(key))
					{
						return dictionaryEntry.Value;
					}
				}
				return null;
			}
			set
			{
				if (this._extraData == null)
				{
					this._extraData = new DictionaryEntry[1];
					this._extraData[0] = new DictionaryEntry(key, value);
					return;
				}
				int num = this._extraData.Length;
				DictionaryEntry[] array = new DictionaryEntry[num + 1];
				int i;
				for (i = 0; i < num; i++)
				{
					array[i] = this._extraData[i];
				}
				array[i] = new DictionaryEntry(key, value);
				this._extraData = array;
			}
		}

		// Token: 0x04002019 RID: 8217
		private string[] _channelURIs;

		// Token: 0x0400201A RID: 8218
		private DictionaryEntry[] _extraData;
	}
}
