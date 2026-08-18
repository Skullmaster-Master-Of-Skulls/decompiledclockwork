using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A0A RID: 6666
	[DataContract]
	[Serializable]
	public class ResourceData : IResourceData
	{
		// Token: 0x17004DE2 RID: 19938
		// (get) Token: 0x0601021A RID: 66074 RVA: 0x0039F497 File Offset: 0x0039D697
		// (set) Token: 0x0601021B RID: 66075 RVA: 0x0039F4C5 File Offset: 0x0039D6C5
		[DataMember]
		public virtual object Key
		{
			get
			{
				if (this._key == null && !string.IsNullOrEmpty(this.EncodedKey))
				{
					this._key = LosSerializer.Deserialize(this.EncodedKey);
				}
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		// Token: 0x17004DE3 RID: 19939
		// (get) Token: 0x0601021C RID: 66076 RVA: 0x0039F4CE File Offset: 0x0039D6CE
		// (set) Token: 0x0601021D RID: 66077 RVA: 0x0039F4D6 File Offset: 0x0039D6D6
		[DataMember]
		public virtual string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x17004DE4 RID: 19940
		// (get) Token: 0x0601021E RID: 66078 RVA: 0x0039F4DF File Offset: 0x0039D6DF
		// (set) Token: 0x0601021F RID: 66079 RVA: 0x0039F4E7 File Offset: 0x0039D6E7
		[DataMember]
		public virtual string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x17004DE5 RID: 19941
		// (get) Token: 0x06010220 RID: 66080 RVA: 0x0039F4F0 File Offset: 0x0039D6F0
		// (set) Token: 0x06010221 RID: 66081 RVA: 0x0039F4F8 File Offset: 0x0039D6F8
		[DataMember]
		public virtual bool Available
		{
			get
			{
				return this._available;
			}
			set
			{
				this._available = value;
			}
		}

		// Token: 0x17004DE6 RID: 19942
		// (get) Token: 0x06010222 RID: 66082 RVA: 0x0039F501 File Offset: 0x0039D701
		// (set) Token: 0x06010223 RID: 66083 RVA: 0x0039F52F File Offset: 0x0039D72F
		[DataMember]
		public virtual string EncodedKey
		{
			get
			{
				if (string.IsNullOrEmpty(this._encodedKey) && this._key != null)
				{
					this._encodedKey = LosSerializer.Serialize(this._key);
				}
				return this._encodedKey;
			}
			set
			{
				this._encodedKey = value;
			}
		}

		// Token: 0x17004DE7 RID: 19943
		// (get) Token: 0x06010224 RID: 66084 RVA: 0x0039F538 File Offset: 0x0039D738
		// (set) Token: 0x06010225 RID: 66085 RVA: 0x0039F553 File Offset: 0x0039D753
		[DataMember]
		public virtual IDictionary<string, string> Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Dictionary<string, string>();
				}
				return this._attributes;
			}
			set
			{
				this._attributes = value;
			}
		}

		// Token: 0x06010226 RID: 66086 RVA: 0x0039F55C File Offset: 0x0039D75C
		public virtual void CopyFrom(Resource srcResource)
		{
			this.Key = srcResource.Key;
			this.Text = srcResource.Text;
			this.Type = srcResource.Type;
			this.Available = srcResource.Available;
			foreach (object obj in srcResource.Attributes.Keys)
			{
				string key = (string)obj;
				this.Attributes.Add(key, srcResource.Attributes[key]);
			}
		}

		// Token: 0x06010227 RID: 66087 RVA: 0x0039F5FC File Offset: 0x0039D7FC
		public virtual void CopyTo(Resource destResource)
		{
			destResource.Key = this.Key;
			destResource.Text = this.Text;
			destResource.Type = this.Type;
			destResource.Available = this.Available;
			foreach (string key in this.Attributes.Keys)
			{
				destResource.Attributes.Add(key, this.Attributes[key]);
			}
		}

		// Token: 0x04004905 RID: 18693
		private object _key;

		// Token: 0x04004906 RID: 18694
		private string _text;

		// Token: 0x04004907 RID: 18695
		private string _type;

		// Token: 0x04004908 RID: 18696
		private bool _available;

		// Token: 0x04004909 RID: 18697
		private string _encodedKey;

		// Token: 0x0400490A RID: 18698
		private IDictionary<string, string> _attributes;
	}
}
