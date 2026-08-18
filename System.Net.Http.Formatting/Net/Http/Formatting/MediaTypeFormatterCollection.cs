using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000045 RID: 69
	public class MediaTypeFormatterCollection : Collection<MediaTypeFormatter>
	{
		// Token: 0x0600028A RID: 650 RVA: 0x00009C7A File Offset: 0x00007E7A
		public MediaTypeFormatterCollection() : this(MediaTypeFormatterCollection.CreateDefaultFormatters())
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009C87 File Offset: 0x00007E87
		public MediaTypeFormatterCollection(IEnumerable<MediaTypeFormatter> formatters)
		{
			this.VerifyAndSetFormatters(formatters);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600028C RID: 652 RVA: 0x00009C98 File Offset: 0x00007E98
		// (remove) Token: 0x0600028D RID: 653 RVA: 0x00009CD0 File Offset: 0x00007ED0
		internal event EventHandler Changing;

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00009D05 File Offset: 0x00007F05
		public XmlMediaTypeFormatter XmlFormatter
		{
			get
			{
				return base.Items.OfType<XmlMediaTypeFormatter>().FirstOrDefault<XmlMediaTypeFormatter>();
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00009D17 File Offset: 0x00007F17
		public JsonMediaTypeFormatter JsonFormatter
		{
			get
			{
				return base.Items.OfType<JsonMediaTypeFormatter>().FirstOrDefault<JsonMediaTypeFormatter>();
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00009D29 File Offset: 0x00007F29
		public FormUrlEncodedMediaTypeFormatter FormUrlEncodedFormatter
		{
			get
			{
				return base.Items.OfType<FormUrlEncodedMediaTypeFormatter>().FirstOrDefault<FormUrlEncodedMediaTypeFormatter>();
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00009D3B File Offset: 0x00007F3B
		internal MediaTypeFormatter[] WritingFormatters
		{
			get
			{
				if (this._writingFormatters == null)
				{
					this._writingFormatters = this.GetWritingFormatters();
				}
				return this._writingFormatters;
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009D58 File Offset: 0x00007F58
		public void AddRange(IEnumerable<MediaTypeFormatter> items)
		{
			if (items == null)
			{
				throw Error.ArgumentNull("items");
			}
			foreach (MediaTypeFormatter item in items)
			{
				base.Add(item);
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009DB0 File Offset: 0x00007FB0
		public void InsertRange(int index, IEnumerable<MediaTypeFormatter> items)
		{
			if (items == null)
			{
				throw Error.ArgumentNull("items");
			}
			foreach (MediaTypeFormatter item in items)
			{
				base.Insert(index++, item);
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00009E0C File Offset: 0x0000800C
		public MediaTypeFormatter FindReader(Type type, MediaTypeHeaderValue mediaType)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (mediaType == null)
			{
				throw Error.ArgumentNull("mediaType");
			}
			foreach (MediaTypeFormatter mediaTypeFormatter in base.Items)
			{
				if (mediaTypeFormatter != null && mediaTypeFormatter.CanReadType(type))
				{
					foreach (MediaTypeHeaderValue mediaTypeHeaderValue in mediaTypeFormatter.SupportedMediaTypes)
					{
						if (mediaTypeHeaderValue != null && mediaTypeHeaderValue.IsSubsetOf(mediaType))
						{
							return mediaTypeFormatter;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00009ED0 File Offset: 0x000080D0
		public MediaTypeFormatter FindWriter(Type type, MediaTypeHeaderValue mediaType)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (mediaType == null)
			{
				throw Error.ArgumentNull("mediaType");
			}
			foreach (MediaTypeFormatter mediaTypeFormatter in base.Items)
			{
				if (mediaTypeFormatter != null && mediaTypeFormatter.CanWriteType(type))
				{
					foreach (MediaTypeHeaderValue mediaTypeHeaderValue in mediaTypeFormatter.SupportedMediaTypes)
					{
						if (mediaTypeHeaderValue != null && mediaTypeHeaderValue.IsSubsetOf(mediaType))
						{
							return mediaTypeFormatter;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00009F94 File Offset: 0x00008194
		public static bool IsTypeExcludedFromValidation(Type type)
		{
			return typeof(XmlNode).IsAssignableFrom(type) || typeof(FormDataCollection).IsAssignableFrom(type) || FormattingUtilities.IsJTokenType(type) || typeof(XObject).IsAssignableFrom(type) || typeof(Type).IsAssignableFrom(type) || type == typeof(byte[]);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000A003 File Offset: 0x00008203
		protected override void ClearItems()
		{
			this.OnChanging();
			base.ClearItems();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A011 File Offset: 0x00008211
		protected override void InsertItem(int index, MediaTypeFormatter item)
		{
			this.OnChanging();
			base.InsertItem(index, item);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A021 File Offset: 0x00008221
		protected override void RemoveItem(int index)
		{
			this.OnChanging();
			base.RemoveItem(index);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A030 File Offset: 0x00008230
		protected override void SetItem(int index, MediaTypeFormatter item)
		{
			this.OnChanging();
			base.SetItem(index, item);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A040 File Offset: 0x00008240
		private void OnChanging()
		{
			if (this.Changing != null)
			{
				this.Changing(this, EventArgs.Empty);
			}
			this._writingFormatters = null;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A06F File Offset: 0x0000826F
		private MediaTypeFormatter[] GetWritingFormatters()
		{
			return (from formatter in base.Items
			where formatter != null && formatter.CanWriteAnyTypes
			select formatter).ToArray<MediaTypeFormatter>();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A0A0 File Offset: 0x000082A0
		private static IEnumerable<MediaTypeFormatter> CreateDefaultFormatters()
		{
			return new MediaTypeFormatter[]
			{
				new JsonMediaTypeFormatter(),
				new XmlMediaTypeFormatter(),
				new FormUrlEncodedMediaTypeFormatter()
			};
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A0D0 File Offset: 0x000082D0
		private void VerifyAndSetFormatters(IEnumerable<MediaTypeFormatter> formatters)
		{
			if (formatters == null)
			{
				throw Error.ArgumentNull("formatters");
			}
			foreach (MediaTypeFormatter mediaTypeFormatter in formatters)
			{
				if (mediaTypeFormatter == null)
				{
					throw Error.Argument("formatters", Resources.CannotHaveNullInList, new object[]
					{
						MediaTypeFormatterCollection._mediaTypeFormatterType.Name
					});
				}
				base.Add(mediaTypeFormatter);
			}
		}

		// Token: 0x040000B0 RID: 176
		private static readonly Type _mediaTypeFormatterType = typeof(MediaTypeFormatter);

		// Token: 0x040000B1 RID: 177
		private MediaTypeFormatter[] _writingFormatters;
	}
}
