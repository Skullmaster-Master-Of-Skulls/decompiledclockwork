using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Collections;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008C6 RID: 2246
	internal sealed class UriPrefixTable<TItem> where TItem : class
	{
		// Token: 0x060055BB RID: 21947 RVA: 0x00139A64 File Offset: 0x00137C64
		public UriPrefixTable() : this(false)
		{
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x00139A6D File Offset: 0x00137C6D
		public UriPrefixTable(bool includePortInComparison) : this(includePortInComparison, false)
		{
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x00139A77 File Offset: 0x00137C77
		public UriPrefixTable(bool includePortInComparison, bool useWeakReferences)
		{
			this.includePortInComparison = includePortInComparison;
			this.useWeakReferences = useWeakReferences;
			this.root = new SegmentHierarchyNode<TItem>(null, useWeakReferences);
			this.lookupCache = new HopperCache(128, useWeakReferences);
		}

		// Token: 0x060055BE RID: 21950 RVA: 0x00139AB0 File Offset: 0x00137CB0
		internal UriPrefixTable(UriPrefixTable<TItem> objectToClone) : this(objectToClone.includePortInComparison, objectToClone.useWeakReferences)
		{
			if (objectToClone.Count > 0)
			{
				foreach (KeyValuePair<BaseUriWithWildcard, TItem> keyValuePair in objectToClone.GetAll())
				{
					this.RegisterUri(keyValuePair.Key.BaseAddress, keyValuePair.Key.HostNameComparisonMode, keyValuePair.Value);
				}
			}
		}

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x060055BF RID: 21951 RVA: 0x00139B38 File Offset: 0x00137D38
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x060055C0 RID: 21952 RVA: 0x00139B3B File Offset: 0x00137D3B
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x00139B44 File Offset: 0x00137D44
		public bool IsRegistered(BaseUriWithWildcard key)
		{
			Uri baseAddress = key.BaseAddress;
			string[] path = UriPrefixTable<TItem>.UriSegmenter.ToPath(baseAddress, key.HostNameComparisonMode, this.includePortInComparison);
			object thisLock = this.ThisLock;
			bool flag2;
			SegmentHierarchyNode<TItem> segmentHierarchyNode;
			lock (thisLock)
			{
				segmentHierarchyNode = this.FindDataNode(path, out flag2);
			}
			return flag2 && segmentHierarchyNode != null && segmentHierarchyNode.Data != null;
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x00139BC0 File Offset: 0x00137DC0
		public IEnumerable<KeyValuePair<BaseUriWithWildcard, TItem>> GetAll()
		{
			object thisLock = this.ThisLock;
			IEnumerable<KeyValuePair<BaseUriWithWildcard, TItem>> result;
			lock (thisLock)
			{
				List<KeyValuePair<BaseUriWithWildcard, TItem>> list = new List<KeyValuePair<BaseUriWithWildcard, TItem>>();
				this.root.Collect(list);
				result = list;
			}
			return result;
		}

		// Token: 0x060055C3 RID: 21955 RVA: 0x00139C10 File Offset: 0x00137E10
		private bool TryCacheLookup(BaseUriWithWildcard key, out TItem item)
		{
			object value = this.lookupCache.GetValue(this.ThisLock, key);
			item = ((value == DBNull.Value) ? default(TItem) : ((TItem)((object)value)));
			return value != null;
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x00139C55 File Offset: 0x00137E55
		private void AddToCache(BaseUriWithWildcard key, TItem item)
		{
			this.lookupCache.Add(key, item ?? DBNull.Value);
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x00139C74 File Offset: 0x00137E74
		private void ClearCache()
		{
			this.lookupCache = new HopperCache(128, this.useWeakReferences);
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x00139C90 File Offset: 0x00137E90
		public bool TryLookupUri(Uri uri, HostNameComparisonMode hostNameComparisonMode, out TItem item)
		{
			BaseUriWithWildcard baseUriWithWildcard = new BaseUriWithWildcard(uri, hostNameComparisonMode);
			if (this.TryCacheLookup(baseUriWithWildcard, out item))
			{
				return item != null;
			}
			object thisLock = this.ThisLock;
			bool result;
			lock (thisLock)
			{
				bool flag2;
				SegmentHierarchyNode<TItem> segmentHierarchyNode = this.FindDataNode(UriPrefixTable<TItem>.UriSegmenter.ToPath(baseUriWithWildcard.BaseAddress, hostNameComparisonMode, this.includePortInComparison), out flag2);
				if (segmentHierarchyNode != null)
				{
					item = segmentHierarchyNode.Data;
				}
				this.AddToCache(baseUriWithWildcard, item);
				result = (item != null);
			}
			return result;
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x00139D38 File Offset: 0x00137F38
		public void RegisterUri(Uri uri, HostNameComparisonMode hostNameComparisonMode, TItem item)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.ClearCache();
				BaseUriWithWildcard baseUriWithWildcard = new BaseUriWithWildcard(uri, hostNameComparisonMode);
				SegmentHierarchyNode<TItem> segmentHierarchyNode = this.FindOrCreateNode(baseUriWithWildcard);
				if (segmentHierarchyNode.Data != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("DuplicateRegistration", new object[]
					{
						uri
					})));
				}
				segmentHierarchyNode.SetData(item, baseUriWithWildcard);
				this.count++;
			}
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x00139DD0 File Offset: 0x00137FD0
		public void UnregisterUri(Uri uri, HostNameComparisonMode hostNameComparisonMode)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.ClearCache();
				string[] array = UriPrefixTable<TItem>.UriSegmenter.ToPath(uri, hostNameComparisonMode, this.includePortInComparison);
				if (array.Length == 0)
				{
					this.root.RemoveData();
				}
				else
				{
					this.root.RemovePath(array, 0);
				}
				this.count--;
			}
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x00139E4C File Offset: 0x0013804C
		private SegmentHierarchyNode<TItem> FindDataNode(string[] path, out bool exactMatch)
		{
			exactMatch = false;
			SegmentHierarchyNode<TItem> segmentHierarchyNode = this.root;
			SegmentHierarchyNode<TItem> result = null;
			int num = 0;
			SegmentHierarchyNode<TItem> segmentHierarchyNode2;
			while (num < path.Length && segmentHierarchyNode.TryGetChild(path[num], out segmentHierarchyNode2))
			{
				if (segmentHierarchyNode2.Data != null)
				{
					result = segmentHierarchyNode2;
					exactMatch = (num == path.Length - 1);
				}
				segmentHierarchyNode = segmentHierarchyNode2;
				num++;
			}
			return result;
		}

		// Token: 0x060055CA RID: 21962 RVA: 0x00139E9C File Offset: 0x0013809C
		private SegmentHierarchyNode<TItem> FindOrCreateNode(BaseUriWithWildcard baseUri)
		{
			string[] array = UriPrefixTable<TItem>.UriSegmenter.ToPath(baseUri.BaseAddress, baseUri.HostNameComparisonMode, this.includePortInComparison);
			SegmentHierarchyNode<TItem> segmentHierarchyNode = this.root;
			for (int i = 0; i < array.Length; i++)
			{
				SegmentHierarchyNode<TItem> segmentHierarchyNode2;
				if (!segmentHierarchyNode.TryGetChild(array[i], out segmentHierarchyNode2))
				{
					segmentHierarchyNode2 = new SegmentHierarchyNode<TItem>(array[i], this.useWeakReferences);
					segmentHierarchyNode.SetChildNode(array[i], segmentHierarchyNode2);
				}
				segmentHierarchyNode = segmentHierarchyNode2;
			}
			return segmentHierarchyNode;
		}

		// Token: 0x040034FF RID: 13567
		private int count;

		// Token: 0x04003500 RID: 13568
		private const int HopperSize = 128;

		// Token: 0x04003501 RID: 13569
		private volatile HopperCache lookupCache;

		// Token: 0x04003502 RID: 13570
		private SegmentHierarchyNode<TItem> root;

		// Token: 0x04003503 RID: 13571
		private bool useWeakReferences;

		// Token: 0x04003504 RID: 13572
		private bool includePortInComparison;

		// Token: 0x02000D87 RID: 3463
		private static class UriSegmenter
		{
			// Token: 0x06007E82 RID: 32386 RVA: 0x001D7AA4 File Offset: 0x001D5CA4
			internal static string[] ToPath(Uri uriPath, HostNameComparisonMode hostNameComparisonMode, bool includePortInComparison)
			{
				if (null == uriPath)
				{
					return new string[0];
				}
				UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum uriSegmentEnum = new UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum(uriPath);
				return uriSegmentEnum.GetSegments(hostNameComparisonMode, includePortInComparison);
			}

			// Token: 0x02000F6F RID: 3951
			private struct UriSegmentEnum
			{
				// Token: 0x060087B2 RID: 34738 RVA: 0x001F8554 File Offset: 0x001F6754
				internal UriSegmentEnum(Uri uri)
				{
					this.uri = uri;
					this.type = UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.Unknown;
					this.segment = null;
					this.segmentStartAt = 0;
					this.segmentLength = 0;
				}

				// Token: 0x060087B3 RID: 34739 RVA: 0x001F8579 File Offset: 0x001F6779
				private void ClearSegment()
				{
					this.type = UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.None;
					this.segment = string.Empty;
					this.segmentStartAt = 0;
					this.segmentLength = 0;
				}

				// Token: 0x060087B4 RID: 34740 RVA: 0x001F859C File Offset: 0x001F679C
				public string[] GetSegments(HostNameComparisonMode hostNameComparisonMode, bool includePortInComparison)
				{
					List<string> list = new List<string>();
					while (this.Next())
					{
						switch (this.type)
						{
						case UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.Host:
							if (hostNameComparisonMode == HostNameComparisonMode.StrongWildcard)
							{
								list.Add("+");
							}
							else if (hostNameComparisonMode == HostNameComparisonMode.Exact)
							{
								list.Add(this.segment);
							}
							else
							{
								list.Add("*");
							}
							break;
						case UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.Port:
							if (includePortInComparison || hostNameComparisonMode == HostNameComparisonMode.Exact)
							{
								list.Add(this.segment);
							}
							break;
						case UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.Path:
							list.Add(this.segment.Substring(this.segmentStartAt, this.segmentLength));
							break;
						default:
							list.Add(this.segment);
							break;
						}
					}
					return list.ToArray();
				}

				// Token: 0x060087B5 RID: 34741 RVA: 0x001F8654 File Offset: 0x001F6854
				public bool Next()
				{
					switch (this.type)
					{
					case UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.Unknown:
						this.type = UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.Scheme;
						this.SetSegment(this.uri.Scheme);
						return true;
					case UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.Scheme:
					{
						this.type = UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.Host;
						string str = this.uri.Host;
						string userInfo = this.uri.UserInfo;
						if (userInfo != null && userInfo.Length > 0)
						{
							str = userInfo + "@" + str;
						}
						this.SetSegment(str);
						return true;
					}
					case UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.Host:
						this.type = UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.Port;
						this.SetSegment(this.uri.Port.ToString(CultureInfo.InvariantCulture));
						return true;
					case UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.Port:
					{
						this.type = UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.Path;
						string absolutePath = this.uri.AbsolutePath;
						if (absolutePath.Length == 0)
						{
							this.ClearSegment();
							return false;
						}
						this.segment = absolutePath;
						this.segmentStartAt = 0;
						this.segmentLength = 0;
						return this.NextPathSegment();
					}
					case UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.Path:
						return this.NextPathSegment();
					case UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType.None:
						return false;
					default:
						return false;
					}
				}

				// Token: 0x060087B6 RID: 34742 RVA: 0x001F8758 File Offset: 0x001F6958
				public bool NextPathSegment()
				{
					this.segmentStartAt += this.segmentLength;
					while (this.segmentStartAt < this.segment.Length && this.segment[this.segmentStartAt] == '/')
					{
						this.segmentStartAt++;
					}
					if (this.segmentStartAt < this.segment.Length)
					{
						int num = this.segment.IndexOf('/', this.segmentStartAt);
						if (-1 == num)
						{
							this.segmentLength = this.segment.Length - this.segmentStartAt;
						}
						else
						{
							this.segmentLength = num - this.segmentStartAt;
						}
						return true;
					}
					this.ClearSegment();
					return false;
				}

				// Token: 0x060087B7 RID: 34743 RVA: 0x001F880C File Offset: 0x001F6A0C
				private void SetSegment(string segment)
				{
					this.segment = segment;
					this.segmentStartAt = 0;
					this.segmentLength = segment.Length;
				}

				// Token: 0x04004F1D RID: 20253
				private string segment;

				// Token: 0x04004F1E RID: 20254
				private int segmentStartAt;

				// Token: 0x04004F1F RID: 20255
				private int segmentLength;

				// Token: 0x04004F20 RID: 20256
				private UriPrefixTable<TItem>.UriSegmenter.UriSegmentEnum.UriSegmentType type;

				// Token: 0x04004F21 RID: 20257
				private Uri uri;

				// Token: 0x02000FC8 RID: 4040
				private enum UriSegmentType
				{
					// Token: 0x0400507A RID: 20602
					Unknown,
					// Token: 0x0400507B RID: 20603
					Scheme,
					// Token: 0x0400507C RID: 20604
					Host,
					// Token: 0x0400507D RID: 20605
					Port,
					// Token: 0x0400507E RID: 20606
					Path,
					// Token: 0x0400507F RID: 20607
					None
				}
			}
		}
	}
}
