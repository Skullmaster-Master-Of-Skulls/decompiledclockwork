using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.Caching;
using System.Web.Util;

namespace System.Web.SessionState
{
	// Token: 0x0200013B RID: 315
	internal class StateApplication : IHttpHandler
	{
		// Token: 0x060012BD RID: 4797 RVA: 0x000358B7 File Offset: 0x00033AB7
		internal StateApplication()
		{
			if (!HttpRuntime.IsFullTrust)
			{
				throw new InvalidOperationException(SR.GetString("StateApplication_FullTrustOnly"));
			}
			this._removedHandler = new CacheItemRemovedCallback(this.OnCacheItemRemoved);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x000358E8 File Offset: 0x00033AE8
		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = null;
			switch (context.Request.HttpVerb)
			{
			case HttpVerb.GET:
				this.DoGet(context);
				return;
			case HttpVerb.PUT:
				this.DoPut(context);
				return;
			case HttpVerb.HEAD:
				this.DoHead(context);
				return;
			case HttpVerb.DELETE:
				this.DoDelete(context);
				return;
			}
			this.DoUnknown(context);
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x000097B7 File Offset: 0x000079B7
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00035956 File Offset: 0x00033B56
		private string CreateKey(HttpRequest request)
		{
			return "k" + HttpUtility.UrlDecode(request.RawUrl);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00035970 File Offset: 0x00033B70
		private void ReportInvalidHeader(HttpContext context, string header)
		{
			HttpResponse response = context.Response;
			response.StatusCode = 400;
			response.Write("<html><head><title>Bad Request</title></head>\r\n");
			response.Write("<body><h1>Http/1.1 400 Bad Request</h1>");
			response.Write("Invalid header <b>" + header + "</b></body></html>");
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x000359BC File Offset: 0x00033BBC
		private void ReportLocked(HttpContext context, CachedContent content)
		{
			HttpResponse response = context.Response;
			response.StatusCode = 423;
			DateTime dateTime = DateTimeUtil.ConvertToLocalTime(content._utcLockDate);
			long num = (DateTime.UtcNow - content._utcLockDate).Ticks / 10000000L;
			response.AppendHeader("LockDate", dateTime.Ticks.ToString(CultureInfo.InvariantCulture));
			response.AppendHeader("LockAge", num.ToString(CultureInfo.InvariantCulture));
			response.AppendHeader("LockCookie", content._lockCookie.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00035A5C File Offset: 0x00033C5C
		private void ReportActionFlags(HttpContext context, int flags)
		{
			HttpResponse response = context.Response;
			response.AppendHeader("ActionFlags", flags.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00035A87 File Offset: 0x00033C87
		private void ReportNotFound(HttpContext context)
		{
			context.Response.StatusCode = 404;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00035A9C File Offset: 0x00033C9C
		private bool GetOptionalNonNegativeInt32HeaderValue(HttpContext context, string header, out int value)
		{
			value = -1;
			string text = context.Request.Headers[header];
			bool flag;
			if (text == null)
			{
				flag = true;
			}
			else
			{
				flag = false;
				try
				{
					value = int.Parse(text, CultureInfo.InvariantCulture);
					if (value >= 0)
					{
						flag = true;
					}
				}
				catch
				{
				}
			}
			if (!flag)
			{
				this.ReportInvalidHeader(context, header);
			}
			return flag;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00035AFC File Offset: 0x00033CFC
		private bool GetRequiredNonNegativeInt32HeaderValue(HttpContext context, string header, out int value)
		{
			bool flag = this.GetOptionalNonNegativeInt32HeaderValue(context, header, out value);
			if (flag && value == -1)
			{
				flag = false;
				this.ReportInvalidHeader(context, header);
			}
			return flag;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00035B28 File Offset: 0x00033D28
		private bool GetOptionalInt32HeaderValue(HttpContext context, string header, out int value, out bool found)
		{
			found = false;
			value = 0;
			string text = context.Request.Headers[header];
			bool flag;
			if (text == null)
			{
				flag = true;
			}
			else
			{
				flag = false;
				try
				{
					value = int.Parse(text, CultureInfo.InvariantCulture);
					flag = true;
					found = true;
				}
				catch
				{
				}
			}
			if (!flag)
			{
				this.ReportInvalidHeader(context, header);
			}
			return flag;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00035B8C File Offset: 0x00033D8C
		internal void DoGet(HttpContext context)
		{
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			string key = this.CreateKey(request);
			CachedContent cachedContent = (CachedContent)HttpRuntime.Cache.InternalCache.Get(key);
			if (cachedContent == null)
			{
				this.ReportNotFound(context);
				return;
			}
			string a = request.Headers["Http_Exclusive"];
			cachedContent._spinLock.AcquireWriterLock();
			try
			{
				if (cachedContent._content == null)
				{
					this.ReportNotFound(context);
				}
				else
				{
					int extraFlags = cachedContent._extraFlags;
					if ((extraFlags & 1) != 0 && extraFlags == Interlocked.CompareExchange(ref cachedContent._extraFlags, extraFlags & -2, extraFlags))
					{
						this.ReportActionFlags(context, 1);
					}
					if (a == "release")
					{
						int num;
						if (this.GetRequiredNonNegativeInt32HeaderValue(context, "Http_LockCookie", out num))
						{
							if (cachedContent._locked)
							{
								if (num == cachedContent._lockCookie)
								{
									cachedContent._locked = false;
								}
								else
								{
									this.ReportLocked(context, cachedContent);
								}
							}
							else
							{
								context.Response.StatusCode = 200;
							}
						}
					}
					else if (cachedContent._locked)
					{
						this.ReportLocked(context, cachedContent);
					}
					else
					{
						if (a == "acquire")
						{
							cachedContent._locked = true;
							cachedContent._utcLockDate = DateTime.UtcNow;
							cachedContent._lockCookie++;
							response.AppendHeader("LockCookie", cachedContent._lockCookie.ToString(CultureInfo.InvariantCulture));
						}
						response.AppendHeader("Timeout", ((int)(cachedContent._slidingExpiration.Ticks / 600000000L)).ToString(CultureInfo.InvariantCulture));
						Stream outputStream = response.OutputStream;
						byte[] content = cachedContent._content;
						outputStream.Write(content, 0, content.Length);
						response.Flush();
					}
				}
			}
			finally
			{
				cachedContent._spinLock.ReleaseWriterLock();
			}
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00035D78 File Offset: 0x00033F78
		internal void DoPut(HttpContext context)
		{
			IntPtr intPtr = this.FinishPut(context);
			if (intPtr != IntPtr.Zero)
			{
				UnsafeNativeMethods.STWNDDeleteStateItem(intPtr);
			}
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00035DA0 File Offset: 0x00033FA0
		private unsafe IntPtr FinishPut(HttpContext context)
		{
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			int lockCookie = 1;
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			Stream inputStream = request.InputStream;
			int num = (int)(inputStream.Length - inputStream.Position);
			byte[] array = new byte[num];
			inputStream.Read(array, 0, array.Length);
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			IntPtr intPtr = (IntPtr)(*(IntPtr*)ptr);
			array2 = null;
			int num2;
			if (!this.GetOptionalNonNegativeInt32HeaderValue(context, "Http_Timeout", out num2))
			{
				return intPtr;
			}
			if (num2 == -1)
			{
				num2 = 20;
			}
			if (num2 > 525600)
			{
				this.ReportInvalidHeader(context, "Http_Timeout");
				return intPtr;
			}
			TimeSpan timeSpan = new TimeSpan(0, num2, 0);
			int num3;
			bool flag;
			if (!this.GetOptionalInt32HeaderValue(context, "Http_ExtraFlags", out num3, out flag))
			{
				return intPtr;
			}
			if (!flag)
			{
				num3 = 0;
			}
			string key = this.CreateKey(request);
			CachedContent cachedContent = (CachedContent)internalCache.Get(key);
			if (cachedContent != null)
			{
				if ((1 & num3) == 1)
				{
					return intPtr;
				}
				int num4;
				if (!this.GetOptionalNonNegativeInt32HeaderValue(context, "Http_LockCookie", out num4))
				{
					return intPtr;
				}
				cachedContent._spinLock.AcquireWriterLock();
				try
				{
					if (cachedContent._content == null)
					{
						this.ReportNotFound(context);
						return intPtr;
					}
					if (cachedContent._locked && (num4 == -1 || num4 != cachedContent._lockCookie))
					{
						this.ReportLocked(context, cachedContent);
						return intPtr;
					}
					if (cachedContent._slidingExpiration == timeSpan && cachedContent._content != null)
					{
						IntPtr stateItem = cachedContent._stateItem;
						cachedContent._content = array;
						cachedContent._stateItem = intPtr;
						cachedContent._locked = false;
						return stateItem;
					}
					cachedContent._extraFlags |= 2;
					cachedContent._locked = true;
					cachedContent._lockCookie = 0;
					lockCookie = num4;
				}
				finally
				{
					cachedContent._spinLock.ReleaseWriterLock();
				}
			}
			CachedContent item = new CachedContent(array, intPtr, false, DateTime.MinValue, timeSpan, lockCookie, num3);
			internalCache.Insert(key, item, new CacheInsertOptions
			{
				SlidingExpiration = timeSpan,
				Priority = CacheItemPriority.NotRemovable,
				OnRemovedCallback = this._removedHandler
			});
			if (cachedContent == null)
			{
				this.IncrementStateServiceCounter(StateServicePerfCounter.STATE_SERVICE_SESSIONS_TOTAL);
				this.IncrementStateServiceCounter(StateServicePerfCounter.STATE_SERVICE_SESSIONS_ACTIVE);
			}
			return IntPtr.Zero;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00035FEC File Offset: 0x000341EC
		internal void DoDelete(HttpContext context)
		{
			string key = this.CreateKey(context.Request);
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			CachedContent cachedContent = (CachedContent)internalCache.Get(key);
			if (cachedContent == null)
			{
				this.ReportNotFound(context);
				return;
			}
			int num;
			if (!this.GetOptionalNonNegativeInt32HeaderValue(context, "Http_LockCookie", out num))
			{
				return;
			}
			cachedContent._spinLock.AcquireWriterLock();
			try
			{
				if (cachedContent._content == null)
				{
					this.ReportNotFound(context);
					return;
				}
				if (cachedContent._locked && (num == -1 || cachedContent._lockCookie != num))
				{
					this.ReportLocked(context, cachedContent);
					return;
				}
				cachedContent._locked = true;
				cachedContent._lockCookie = 0;
			}
			finally
			{
				cachedContent._spinLock.ReleaseWriterLock();
			}
			internalCache.Remove(key);
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x000360AC File Offset: 0x000342AC
		internal void DoHead(HttpContext context)
		{
			string key = this.CreateKey(context.Request);
			if (HttpRuntime.Cache.InternalCache.Get(key) == null)
			{
				this.ReportNotFound(context);
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x000360E1 File Offset: 0x000342E1
		internal void DoUnknown(HttpContext context)
		{
			context.Response.StatusCode = 400;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x000360F4 File Offset: 0x000342F4
		private void OnCacheItemRemoved(string key, object value, CacheItemRemovedReason reason)
		{
			CachedContent cachedContent = (CachedContent)value;
			cachedContent._spinLock.AcquireWriterLock();
			IntPtr stateItem;
			try
			{
				stateItem = cachedContent._stateItem;
				cachedContent._content = null;
				cachedContent._stateItem = IntPtr.Zero;
			}
			finally
			{
				cachedContent._spinLock.ReleaseWriterLock();
			}
			UnsafeNativeMethods.STWNDDeleteStateItem(stateItem);
			if ((cachedContent._extraFlags & 2) != 0)
			{
				return;
			}
			if (reason != CacheItemRemovedReason.Removed)
			{
				if (reason == CacheItemRemovedReason.Expired)
				{
					this.IncrementStateServiceCounter(StateServicePerfCounter.STATE_SERVICE_SESSIONS_TIMED_OUT);
				}
			}
			else
			{
				this.IncrementStateServiceCounter(StateServicePerfCounter.STATE_SERVICE_SESSIONS_ABANDONED);
			}
			this.DecrementStateServiceCounter(StateServicePerfCounter.STATE_SERVICE_SESSIONS_ACTIVE);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00036180 File Offset: 0x00034380
		private void DecrementStateServiceCounter(StateServicePerfCounter counter)
		{
			if (HttpRuntime.ShutdownInProgress)
			{
				return;
			}
			PerfCounters.DecrementStateServiceCounter(counter);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00036190 File Offset: 0x00034390
		private void IncrementStateServiceCounter(StateServicePerfCounter counter)
		{
			if (HttpRuntime.ShutdownInProgress)
			{
				return;
			}
			PerfCounters.IncrementStateServiceCounter(counter);
		}

		// Token: 0x040014A9 RID: 5289
		private CacheItemRemovedCallback _removedHandler;
	}
}
