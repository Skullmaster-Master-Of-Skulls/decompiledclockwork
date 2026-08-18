using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Net.Http.Headers
{
	// Token: 0x02000027 RID: 39
	[__DynamicallyInvokable]
	public class CacheControlHeaderValue : ICloneable
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007174 File Offset: 0x00005374
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000717C File Offset: 0x0000537C
		[__DynamicallyInvokable]
		public bool NoCache
		{
			[__DynamicallyInvokable]
			get
			{
				return this.noCache;
			}
			[__DynamicallyInvokable]
			set
			{
				this.noCache = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007185 File Offset: 0x00005385
		[__DynamicallyInvokable]
		public ICollection<string> NoCacheHeaders
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.noCacheHeaders == null)
				{
					this.noCacheHeaders = new ObjectCollection<string>(CacheControlHeaderValue.checkIsValidToken);
				}
				return this.noCacheHeaders;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000071A5 File Offset: 0x000053A5
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000071AD File Offset: 0x000053AD
		[__DynamicallyInvokable]
		public bool NoStore
		{
			[__DynamicallyInvokable]
			get
			{
				return this.noStore;
			}
			[__DynamicallyInvokable]
			set
			{
				this.noStore = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000071B6 File Offset: 0x000053B6
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x000071BE File Offset: 0x000053BE
		[__DynamicallyInvokable]
		public TimeSpan? MaxAge
		{
			[__DynamicallyInvokable]
			get
			{
				return this.maxAge;
			}
			[__DynamicallyInvokable]
			set
			{
				this.maxAge = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000071C7 File Offset: 0x000053C7
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x000071CF File Offset: 0x000053CF
		[__DynamicallyInvokable]
		public TimeSpan? SharedMaxAge
		{
			[__DynamicallyInvokable]
			get
			{
				return this.sharedMaxAge;
			}
			[__DynamicallyInvokable]
			set
			{
				this.sharedMaxAge = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000071D8 File Offset: 0x000053D8
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x000071E0 File Offset: 0x000053E0
		[__DynamicallyInvokable]
		public bool MaxStale
		{
			[__DynamicallyInvokable]
			get
			{
				return this.maxStale;
			}
			[__DynamicallyInvokable]
			set
			{
				this.maxStale = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000071E9 File Offset: 0x000053E9
		// (set) Token: 0x060001BA RID: 442 RVA: 0x000071F1 File Offset: 0x000053F1
		[__DynamicallyInvokable]
		public TimeSpan? MaxStaleLimit
		{
			[__DynamicallyInvokable]
			get
			{
				return this.maxStaleLimit;
			}
			[__DynamicallyInvokable]
			set
			{
				this.maxStaleLimit = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000071FA File Offset: 0x000053FA
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00007202 File Offset: 0x00005402
		[__DynamicallyInvokable]
		public TimeSpan? MinFresh
		{
			[__DynamicallyInvokable]
			get
			{
				return this.minFresh;
			}
			[__DynamicallyInvokable]
			set
			{
				this.minFresh = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000720B File Offset: 0x0000540B
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00007213 File Offset: 0x00005413
		[__DynamicallyInvokable]
		public bool NoTransform
		{
			[__DynamicallyInvokable]
			get
			{
				return this.noTransform;
			}
			[__DynamicallyInvokable]
			set
			{
				this.noTransform = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000721C File Offset: 0x0000541C
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00007224 File Offset: 0x00005424
		[__DynamicallyInvokable]
		public bool OnlyIfCached
		{
			[__DynamicallyInvokable]
			get
			{
				return this.onlyIfCached;
			}
			[__DynamicallyInvokable]
			set
			{
				this.onlyIfCached = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000722D File Offset: 0x0000542D
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00007235 File Offset: 0x00005435
		[__DynamicallyInvokable]
		public bool Public
		{
			[__DynamicallyInvokable]
			get
			{
				return this.publicField;
			}
			[__DynamicallyInvokable]
			set
			{
				this.publicField = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000723E File Offset: 0x0000543E
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00007246 File Offset: 0x00005446
		[__DynamicallyInvokable]
		public bool Private
		{
			[__DynamicallyInvokable]
			get
			{
				return this.privateField;
			}
			[__DynamicallyInvokable]
			set
			{
				this.privateField = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000724F File Offset: 0x0000544F
		[__DynamicallyInvokable]
		public ICollection<string> PrivateHeaders
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.privateHeaders == null)
				{
					this.privateHeaders = new ObjectCollection<string>(CacheControlHeaderValue.checkIsValidToken);
				}
				return this.privateHeaders;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000726F File Offset: 0x0000546F
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00007277 File Offset: 0x00005477
		[__DynamicallyInvokable]
		public bool MustRevalidate
		{
			[__DynamicallyInvokable]
			get
			{
				return this.mustRevalidate;
			}
			[__DynamicallyInvokable]
			set
			{
				this.mustRevalidate = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00007280 File Offset: 0x00005480
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00007288 File Offset: 0x00005488
		[__DynamicallyInvokable]
		public bool ProxyRevalidate
		{
			[__DynamicallyInvokable]
			get
			{
				return this.proxyRevalidate;
			}
			[__DynamicallyInvokable]
			set
			{
				this.proxyRevalidate = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00007291 File Offset: 0x00005491
		[__DynamicallyInvokable]
		public ICollection<NameValueHeaderValue> Extensions
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new ObjectCollection<NameValueHeaderValue>();
				}
				return this.extensions;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000072AC File Offset: 0x000054AC
		[__DynamicallyInvokable]
		public CacheControlHeaderValue()
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000072B4 File Offset: 0x000054B4
		private CacheControlHeaderValue(CacheControlHeaderValue source)
		{
			this.noCache = source.noCache;
			this.noStore = source.noStore;
			this.maxAge = source.maxAge;
			this.sharedMaxAge = source.sharedMaxAge;
			this.maxStale = source.maxStale;
			this.maxStaleLimit = source.maxStaleLimit;
			this.minFresh = source.minFresh;
			this.noTransform = source.noTransform;
			this.onlyIfCached = source.onlyIfCached;
			this.publicField = source.publicField;
			this.privateField = source.privateField;
			this.mustRevalidate = source.mustRevalidate;
			this.proxyRevalidate = source.proxyRevalidate;
			if (source.noCacheHeaders != null)
			{
				foreach (string item in source.noCacheHeaders)
				{
					this.NoCacheHeaders.Add(item);
				}
			}
			if (source.privateHeaders != null)
			{
				foreach (string item2 in source.privateHeaders)
				{
					this.PrivateHeaders.Add(item2);
				}
			}
			if (source.extensions != null)
			{
				foreach (NameValueHeaderValue nameValueHeaderValue in source.extensions)
				{
					this.Extensions.Add((NameValueHeaderValue)((ICloneable)nameValueHeaderValue).Clone());
				}
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007454 File Offset: 0x00005654
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			CacheControlHeaderValue.AppendValueIfRequired(stringBuilder, this.noStore, "no-store");
			CacheControlHeaderValue.AppendValueIfRequired(stringBuilder, this.noTransform, "no-transform");
			CacheControlHeaderValue.AppendValueIfRequired(stringBuilder, this.onlyIfCached, "only-if-cached");
			CacheControlHeaderValue.AppendValueIfRequired(stringBuilder, this.publicField, "public");
			CacheControlHeaderValue.AppendValueIfRequired(stringBuilder, this.mustRevalidate, "must-revalidate");
			CacheControlHeaderValue.AppendValueIfRequired(stringBuilder, this.proxyRevalidate, "proxy-revalidate");
			if (this.noCache)
			{
				CacheControlHeaderValue.AppendValueWithSeparatorIfRequired(stringBuilder, "no-cache");
				if (this.noCacheHeaders != null && this.noCacheHeaders.Count > 0)
				{
					stringBuilder.Append("=\"");
					CacheControlHeaderValue.AppendValues(stringBuilder, this.noCacheHeaders);
					stringBuilder.Append('"');
				}
			}
			if (this.maxAge != null)
			{
				CacheControlHeaderValue.AppendValueWithSeparatorIfRequired(stringBuilder, "max-age");
				stringBuilder.Append('=');
				stringBuilder.Append(((int)this.maxAge.Value.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo));
			}
			if (this.sharedMaxAge != null)
			{
				CacheControlHeaderValue.AppendValueWithSeparatorIfRequired(stringBuilder, "s-maxage");
				stringBuilder.Append('=');
				stringBuilder.Append(((int)this.sharedMaxAge.Value.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo));
			}
			if (this.maxStale)
			{
				CacheControlHeaderValue.AppendValueWithSeparatorIfRequired(stringBuilder, "max-stale");
				if (this.maxStaleLimit != null)
				{
					stringBuilder.Append('=');
					stringBuilder.Append(((int)this.maxStaleLimit.Value.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo));
				}
			}
			if (this.minFresh != null)
			{
				CacheControlHeaderValue.AppendValueWithSeparatorIfRequired(stringBuilder, "min-fresh");
				stringBuilder.Append('=');
				stringBuilder.Append(((int)this.minFresh.Value.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo));
			}
			if (this.privateField)
			{
				CacheControlHeaderValue.AppendValueWithSeparatorIfRequired(stringBuilder, "private");
				if (this.privateHeaders != null && this.privateHeaders.Count > 0)
				{
					stringBuilder.Append("=\"");
					CacheControlHeaderValue.AppendValues(stringBuilder, this.privateHeaders);
					stringBuilder.Append('"');
				}
			}
			NameValueHeaderValue.ToString(this.extensions, ',', false, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000076A4 File Offset: 0x000058A4
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			CacheControlHeaderValue cacheControlHeaderValue = obj as CacheControlHeaderValue;
			return cacheControlHeaderValue != null && (this.noCache == cacheControlHeaderValue.noCache && this.noStore == cacheControlHeaderValue.noStore) && !(this.maxAge != cacheControlHeaderValue.maxAge) && (!(this.sharedMaxAge != cacheControlHeaderValue.sharedMaxAge) && this.maxStale == cacheControlHeaderValue.maxStale) && !(this.maxStaleLimit != cacheControlHeaderValue.maxStaleLimit) && !(this.minFresh != cacheControlHeaderValue.minFresh) && this.noTransform == cacheControlHeaderValue.noTransform && this.onlyIfCached == cacheControlHeaderValue.onlyIfCached && this.publicField == cacheControlHeaderValue.publicField && this.privateField == cacheControlHeaderValue.privateField && this.mustRevalidate == cacheControlHeaderValue.mustRevalidate && this.proxyRevalidate == cacheControlHeaderValue.proxyRevalidate && HeaderUtilities.AreEqualCollections<string>(this.noCacheHeaders, cacheControlHeaderValue.noCacheHeaders, StringComparer.OrdinalIgnoreCase) && HeaderUtilities.AreEqualCollections<string>(this.privateHeaders, cacheControlHeaderValue.privateHeaders, StringComparer.OrdinalIgnoreCase) && HeaderUtilities.AreEqualCollections<NameValueHeaderValue>(this.extensions, cacheControlHeaderValue.extensions);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000078A4 File Offset: 0x00005AA4
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			int num = this.noCache.GetHashCode() ^ this.noStore.GetHashCode() << 1 ^ this.maxStale.GetHashCode() << 2 ^ this.noTransform.GetHashCode() << 3 ^ this.onlyIfCached.GetHashCode() << 4 ^ this.publicField.GetHashCode() << 5 ^ this.privateField.GetHashCode() << 6 ^ this.mustRevalidate.GetHashCode() << 7 ^ this.proxyRevalidate.GetHashCode() << 8;
			num = (num ^ ((this.maxAge != null) ? (this.maxAge.Value.GetHashCode() ^ 1) : 0) ^ ((this.sharedMaxAge != null) ? (this.sharedMaxAge.Value.GetHashCode() ^ 2) : 0) ^ ((this.maxStaleLimit != null) ? (this.maxStaleLimit.Value.GetHashCode() ^ 4) : 0) ^ ((this.minFresh != null) ? (this.minFresh.Value.GetHashCode() ^ 8) : 0));
			if (this.noCacheHeaders != null && this.noCacheHeaders.Count > 0)
			{
				foreach (string text in this.noCacheHeaders)
				{
					num ^= text.ToLowerInvariant().GetHashCode();
				}
			}
			if (this.privateHeaders != null && this.privateHeaders.Count > 0)
			{
				foreach (string text2 in this.privateHeaders)
				{
					num ^= text2.ToLowerInvariant().GetHashCode();
				}
			}
			if (this.extensions != null && this.extensions.Count > 0)
			{
				foreach (NameValueHeaderValue nameValueHeaderValue in this.extensions)
				{
					num ^= nameValueHeaderValue.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007AF8 File Offset: 0x00005CF8
		[__DynamicallyInvokable]
		public static CacheControlHeaderValue Parse(string input)
		{
			int num = 0;
			return (CacheControlHeaderValue)CacheControlHeaderParser.Parser.ParseValue(input, null, ref num);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007B1C File Offset: 0x00005D1C
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out CacheControlHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (CacheControlHeaderParser.Parser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (CacheControlHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007B4C File Offset: 0x00005D4C
		internal static int GetCacheControlLength(string input, int startIndex, CacheControlHeaderValue storeValue, out CacheControlHeaderValue parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			int i = startIndex;
			object obj = null;
			List<NameValueHeaderValue> list = new List<NameValueHeaderValue>();
			while (i < input.Length)
			{
				if (!CacheControlHeaderValue.nameValueListParser.TryParseValue(input, null, ref i, out obj))
				{
					return 0;
				}
				list.Add(obj as NameValueHeaderValue);
			}
			CacheControlHeaderValue cacheControlHeaderValue = storeValue;
			if (cacheControlHeaderValue == null)
			{
				cacheControlHeaderValue = new CacheControlHeaderValue();
			}
			if (!CacheControlHeaderValue.TrySetCacheControlValues(cacheControlHeaderValue, list))
			{
				return 0;
			}
			if (storeValue == null)
			{
				parsedValue = cacheControlHeaderValue;
			}
			return input.Length - startIndex;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007BC8 File Offset: 0x00005DC8
		private static bool TrySetCacheControlValues(CacheControlHeaderValue cc, List<NameValueHeaderValue> nameValueList)
		{
			foreach (NameValueHeaderValue nameValueHeaderValue in nameValueList)
			{
				bool flag = true;
				string text = nameValueHeaderValue.Name.ToLowerInvariant();
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1922561311U)
				{
					if (num <= 719568158U)
					{
						if (num != 129047354U)
						{
							if (num != 412259456U)
							{
								if (num != 719568158U)
								{
									goto IL_2C4;
								}
								if (!(text == "no-store"))
								{
									goto IL_2C4;
								}
								flag = CacheControlHeaderValue.TrySetTokenOnlyValue(nameValueHeaderValue, ref cc.noStore);
							}
							else
							{
								if (!(text == "s-maxage"))
								{
									goto IL_2C4;
								}
								flag = CacheControlHeaderValue.TrySetTimeSpan(nameValueHeaderValue, ref cc.sharedMaxAge);
							}
						}
						else
						{
							if (!(text == "min-fresh"))
							{
								goto IL_2C4;
							}
							flag = CacheControlHeaderValue.TrySetTimeSpan(nameValueHeaderValue, ref cc.minFresh);
						}
					}
					else if (num != 962188105U)
					{
						if (num != 1657474316U)
						{
							if (num != 1922561311U)
							{
								goto IL_2C4;
							}
							if (!(text == "max-age"))
							{
								goto IL_2C4;
							}
							flag = CacheControlHeaderValue.TrySetTimeSpan(nameValueHeaderValue, ref cc.maxAge);
						}
						else
						{
							if (!(text == "private"))
							{
								goto IL_2C4;
							}
							flag = CacheControlHeaderValue.TrySetOptionalTokenList(nameValueHeaderValue, ref cc.privateField, ref cc.privateHeaders);
						}
					}
					else
					{
						if (!(text == "max-stale"))
						{
							goto IL_2C4;
						}
						flag = (nameValueHeaderValue.Value == null || CacheControlHeaderValue.TrySetTimeSpan(nameValueHeaderValue, ref cc.maxStaleLimit));
						if (flag)
						{
							cc.maxStale = true;
						}
					}
				}
				else if (num <= 2802093227U)
				{
					if (num != 2033558065U)
					{
						if (num != 2154495528U)
						{
							if (num != 2802093227U)
							{
								goto IL_2C4;
							}
							if (!(text == "no-transform"))
							{
								goto IL_2C4;
							}
							flag = CacheControlHeaderValue.TrySetTokenOnlyValue(nameValueHeaderValue, ref cc.noTransform);
						}
						else
						{
							if (!(text == "must-revalidate"))
							{
								goto IL_2C4;
							}
							flag = CacheControlHeaderValue.TrySetTokenOnlyValue(nameValueHeaderValue, ref cc.mustRevalidate);
						}
					}
					else
					{
						if (!(text == "proxy-revalidate"))
						{
							goto IL_2C4;
						}
						flag = CacheControlHeaderValue.TrySetTokenOnlyValue(nameValueHeaderValue, ref cc.proxyRevalidate);
					}
				}
				else if (num != 2866772502U)
				{
					if (num != 3432027008U)
					{
						if (num != 3443516981U)
						{
							goto IL_2C4;
						}
						if (!(text == "no-cache"))
						{
							goto IL_2C4;
						}
						flag = CacheControlHeaderValue.TrySetOptionalTokenList(nameValueHeaderValue, ref cc.noCache, ref cc.noCacheHeaders);
					}
					else
					{
						if (!(text == "public"))
						{
							goto IL_2C4;
						}
						flag = CacheControlHeaderValue.TrySetTokenOnlyValue(nameValueHeaderValue, ref cc.publicField);
					}
				}
				else
				{
					if (!(text == "only-if-cached"))
					{
						goto IL_2C4;
					}
					flag = CacheControlHeaderValue.TrySetTokenOnlyValue(nameValueHeaderValue, ref cc.onlyIfCached);
				}
				IL_2D0:
				if (!flag)
				{
					return false;
				}
				continue;
				IL_2C4:
				cc.Extensions.Add(nameValueHeaderValue);
				goto IL_2D0;
			}
			return true;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007EEC File Offset: 0x000060EC
		private static bool TrySetTokenOnlyValue(NameValueHeaderValue nameValue, ref bool boolField)
		{
			if (nameValue.Value != null)
			{
				return false;
			}
			boolField = true;
			return true;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007EFC File Offset: 0x000060FC
		private static bool TrySetOptionalTokenList(NameValueHeaderValue nameValue, ref bool boolField, ref ICollection<string> destination)
		{
			if (nameValue.Value == null)
			{
				boolField = true;
				return true;
			}
			string value = nameValue.Value;
			if (value.Length < 3 || value[0] != '"' || value[value.Length - 1] != '"')
			{
				return false;
			}
			int i = 1;
			int num = value.Length - 1;
			bool flag = false;
			int num2 = (destination == null) ? 0 : destination.Count;
			while (i < num)
			{
				i = HeaderUtilities.GetNextNonEmptyOrWhitespaceIndex(value, i, true, out flag);
				if (i == num)
				{
					break;
				}
				int tokenLength = HttpRuleParser.GetTokenLength(value, i);
				if (tokenLength == 0)
				{
					return false;
				}
				if (destination == null)
				{
					destination = new ObjectCollection<string>(CacheControlHeaderValue.checkIsValidToken);
				}
				destination.Add(value.Substring(i, tokenLength));
				i += tokenLength;
			}
			if (destination != null && destination.Count > num2)
			{
				boolField = true;
				return true;
			}
			return false;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007FC0 File Offset: 0x000061C0
		private static bool TrySetTimeSpan(NameValueHeaderValue nameValue, ref TimeSpan? timeSpan)
		{
			if (nameValue.Value == null)
			{
				return false;
			}
			int seconds;
			if (!HeaderUtilities.TryParseInt32(nameValue.Value, out seconds))
			{
				return false;
			}
			timeSpan = new TimeSpan?(new TimeSpan(0, 0, seconds));
			return true;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00007FFC File Offset: 0x000061FC
		private static void AppendValueIfRequired(StringBuilder sb, bool appendValue, string value)
		{
			if (appendValue)
			{
				CacheControlHeaderValue.AppendValueWithSeparatorIfRequired(sb, value);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008008 File Offset: 0x00006208
		private static void AppendValueWithSeparatorIfRequired(StringBuilder sb, string value)
		{
			if (sb.Length > 0)
			{
				sb.Append(", ");
			}
			sb.Append(value);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008028 File Offset: 0x00006228
		private static void AppendValues(StringBuilder sb, IEnumerable<string> values)
		{
			bool flag = true;
			foreach (string value in values)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					sb.Append(", ");
				}
				sb.Append(value);
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008088 File Offset: 0x00006288
		private static void CheckIsValidToken(string item)
		{
			HeaderUtilities.CheckValidToken(item, "item");
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008095 File Offset: 0x00006295
		object ICloneable.Clone()
		{
			return new CacheControlHeaderValue(this);
		}

		// Token: 0x040000D9 RID: 217
		private const string maxAgeString = "max-age";

		// Token: 0x040000DA RID: 218
		private const string maxStaleString = "max-stale";

		// Token: 0x040000DB RID: 219
		private const string minFreshString = "min-fresh";

		// Token: 0x040000DC RID: 220
		private const string mustRevalidateString = "must-revalidate";

		// Token: 0x040000DD RID: 221
		private const string noCacheString = "no-cache";

		// Token: 0x040000DE RID: 222
		private const string noStoreString = "no-store";

		// Token: 0x040000DF RID: 223
		private const string noTransformString = "no-transform";

		// Token: 0x040000E0 RID: 224
		private const string onlyIfCachedString = "only-if-cached";

		// Token: 0x040000E1 RID: 225
		private const string privateString = "private";

		// Token: 0x040000E2 RID: 226
		private const string proxyRevalidateString = "proxy-revalidate";

		// Token: 0x040000E3 RID: 227
		private const string publicString = "public";

		// Token: 0x040000E4 RID: 228
		private const string sharedMaxAgeString = "s-maxage";

		// Token: 0x040000E5 RID: 229
		private static readonly HttpHeaderParser nameValueListParser = GenericHeaderParser.MultipleValueNameValueParser;

		// Token: 0x040000E6 RID: 230
		private static readonly Action<string> checkIsValidToken = new Action<string>(CacheControlHeaderValue.CheckIsValidToken);

		// Token: 0x040000E7 RID: 231
		private bool noCache;

		// Token: 0x040000E8 RID: 232
		private ICollection<string> noCacheHeaders;

		// Token: 0x040000E9 RID: 233
		private bool noStore;

		// Token: 0x040000EA RID: 234
		private TimeSpan? maxAge;

		// Token: 0x040000EB RID: 235
		private TimeSpan? sharedMaxAge;

		// Token: 0x040000EC RID: 236
		private bool maxStale;

		// Token: 0x040000ED RID: 237
		private TimeSpan? maxStaleLimit;

		// Token: 0x040000EE RID: 238
		private TimeSpan? minFresh;

		// Token: 0x040000EF RID: 239
		private bool noTransform;

		// Token: 0x040000F0 RID: 240
		private bool onlyIfCached;

		// Token: 0x040000F1 RID: 241
		private bool publicField;

		// Token: 0x040000F2 RID: 242
		private bool privateField;

		// Token: 0x040000F3 RID: 243
		private ICollection<string> privateHeaders;

		// Token: 0x040000F4 RID: 244
		private bool mustRevalidate;

		// Token: 0x040000F5 RID: 245
		private bool proxyRevalidate;

		// Token: 0x040000F6 RID: 246
		private ICollection<NameValueHeaderValue> extensions;
	}
}
