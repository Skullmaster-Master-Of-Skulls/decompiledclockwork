using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009C3 RID: 2499
	internal class HeaderInfoCache
	{
		// Token: 0x06006237 RID: 25143 RVA: 0x0016D994 File Offset: 0x0016BB94
		public MessageHeaderInfo TakeHeaderInfo(XmlDictionaryReader reader, string actor, bool mustUnderstand, bool relay, bool isRefParam)
		{
			if (this.headerInfos != null)
			{
				int num = this.index;
				HeaderInfoCache.HeaderInfo headerInfo;
				for (;;)
				{
					headerInfo = this.headerInfos[num];
					if (headerInfo != null && headerInfo.Matches(reader, actor, mustUnderstand, relay, isRefParam))
					{
						break;
					}
					num = (num + 1) % 4;
					if (num == this.index)
					{
						goto IL_4F;
					}
				}
				this.headerInfos[num] = null;
				this.index = (num + 1) % 4;
				return headerInfo;
			}
			IL_4F:
			return new HeaderInfoCache.HeaderInfo(reader, actor, mustUnderstand, relay, isRefParam);
		}

		// Token: 0x06006238 RID: 25144 RVA: 0x0016D9FC File Offset: 0x0016BBFC
		public void ReturnHeaderInfo(MessageHeaderInfo headerInfo)
		{
			HeaderInfoCache.HeaderInfo headerInfo2 = headerInfo as HeaderInfoCache.HeaderInfo;
			if (headerInfo2 != null)
			{
				if (this.headerInfos == null)
				{
					this.headerInfos = new HeaderInfoCache.HeaderInfo[4];
				}
				int num = this.index;
				while (this.headerInfos[num] != null)
				{
					num = (num + 1) % 4;
					if (num == this.index)
					{
						break;
					}
				}
				this.headerInfos[num] = headerInfo2;
				this.index = (num + 1) % 4;
			}
		}

		// Token: 0x040038FF RID: 14591
		private const int maxHeaderInfos = 4;

		// Token: 0x04003900 RID: 14592
		private HeaderInfoCache.HeaderInfo[] headerInfos;

		// Token: 0x04003901 RID: 14593
		private int index;

		// Token: 0x02000E46 RID: 3654
		private class HeaderInfo : MessageHeaderInfo
		{
			// Token: 0x060082CA RID: 33482 RVA: 0x001E3987 File Offset: 0x001E1B87
			public HeaderInfo(XmlDictionaryReader reader, string actor, bool mustUnderstand, bool relay, bool isReferenceParameter)
			{
				this.actor = actor;
				this.mustUnderstand = mustUnderstand;
				this.relay = relay;
				this.isReferenceParameter = isReferenceParameter;
				reader.GetNonAtomizedNames(out this.name, out this.ns);
			}

			// Token: 0x17001CE4 RID: 7396
			// (get) Token: 0x060082CB RID: 33483 RVA: 0x001E39BF File Offset: 0x001E1BBF
			public override string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17001CE5 RID: 7397
			// (get) Token: 0x060082CC RID: 33484 RVA: 0x001E39C7 File Offset: 0x001E1BC7
			public override string Namespace
			{
				get
				{
					return this.ns;
				}
			}

			// Token: 0x17001CE6 RID: 7398
			// (get) Token: 0x060082CD RID: 33485 RVA: 0x001E39CF File Offset: 0x001E1BCF
			public override bool IsReferenceParameter
			{
				get
				{
					return this.isReferenceParameter;
				}
			}

			// Token: 0x17001CE7 RID: 7399
			// (get) Token: 0x060082CE RID: 33486 RVA: 0x001E39D7 File Offset: 0x001E1BD7
			public override string Actor
			{
				get
				{
					return this.actor;
				}
			}

			// Token: 0x17001CE8 RID: 7400
			// (get) Token: 0x060082CF RID: 33487 RVA: 0x001E39DF File Offset: 0x001E1BDF
			public override bool MustUnderstand
			{
				get
				{
					return this.mustUnderstand;
				}
			}

			// Token: 0x17001CE9 RID: 7401
			// (get) Token: 0x060082D0 RID: 33488 RVA: 0x001E39E7 File Offset: 0x001E1BE7
			public override bool Relay
			{
				get
				{
					return this.relay;
				}
			}

			// Token: 0x060082D1 RID: 33489 RVA: 0x001E39F0 File Offset: 0x001E1BF0
			public bool Matches(XmlDictionaryReader reader, string actor, bool mustUnderstand, bool relay, bool isRefParam)
			{
				return reader.IsStartElement(this.name, this.ns) && this.actor == actor && this.mustUnderstand == mustUnderstand && this.relay == relay && this.isReferenceParameter == isRefParam;
			}

			// Token: 0x04004A42 RID: 19010
			private string name;

			// Token: 0x04004A43 RID: 19011
			private string ns;

			// Token: 0x04004A44 RID: 19012
			private string actor;

			// Token: 0x04004A45 RID: 19013
			private bool isReferenceParameter;

			// Token: 0x04004A46 RID: 19014
			private bool mustUnderstand;

			// Token: 0x04004A47 RID: 19015
			private bool relay;
		}
	}
}
