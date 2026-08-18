using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.WebPages.Instrumentation;
using System.Web.WebPages.Resources;

namespace System.Web.WebPages
{
	// Token: 0x02000011 RID: 17
	public abstract class WebPageExecutingBase
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003007 File Offset: 0x00001207
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003022 File Offset: 0x00001222
		internal InstrumentationService InstrumentationService
		{
			get
			{
				if (this._instrumentationService == null)
				{
					this._instrumentationService = new InstrumentationService();
				}
				return this._instrumentationService;
			}
			set
			{
				this._instrumentationService = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000302B File Offset: 0x0000122B
		public virtual HttpApplicationStateBase AppState
		{
			get
			{
				if (this.Context != null)
				{
					return this.Context.Application;
				}
				return null;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003042 File Offset: 0x00001242
		[Dynamic]
		public virtual dynamic App
		{
			[return: Dynamic]
			get
			{
				if (this._dynamicAppState == null && this.AppState != null)
				{
					this._dynamicAppState = new DynamicHttpApplicationState(this.AppState);
				}
				return this._dynamicAppState;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000306B File Offset: 0x0000126B
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003073 File Offset: 0x00001273
		public virtual HttpContextBase Context { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000307C File Offset: 0x0000127C
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003084 File Offset: 0x00001284
		public virtual string VirtualPath { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000308D File Offset: 0x0000128D
		// (set) Token: 0x0600006D RID: 109 RVA: 0x0000309E File Offset: 0x0000129E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual IVirtualPathFactory VirtualPathFactory
		{
			get
			{
				return this._virtualPathFactory ?? VirtualPathFactoryManager.Instance;
			}
			set
			{
				this._virtualPathFactory = value;
			}
		}

		// Token: 0x0600006E RID: 110
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract void Execute();

		// Token: 0x0600006F RID: 111 RVA: 0x000030A7 File Offset: 0x000012A7
		public virtual string Href(string path, params object[] pathParts)
		{
			return UrlUtil.GenerateClientUrl(this.Context, this.VirtualPath, path, pathParts);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000030BC File Offset: 0x000012BC
		protected internal void BeginContext(int startPosition, int length, bool isLiteral)
		{
			this.BeginContext(this.GetOutputWriter(), this.VirtualPath, startPosition, length, isLiteral);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000030D3 File Offset: 0x000012D3
		protected internal void BeginContext(string virtualPath, int startPosition, int length, bool isLiteral)
		{
			this.BeginContext(this.GetOutputWriter(), virtualPath, startPosition, length, isLiteral);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000030E6 File Offset: 0x000012E6
		protected internal void BeginContext(TextWriter writer, int startPosition, int length, bool isLiteral)
		{
			this.BeginContext(writer, this.VirtualPath, startPosition, length, isLiteral);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000030F9 File Offset: 0x000012F9
		protected internal void BeginContext(TextWriter writer, string virtualPath, int startPosition, int length, bool isLiteral)
		{
			if (this.InstrumentationService.IsAvailable)
			{
				this.InstrumentationService.BeginContext(this.Context, virtualPath, writer, startPosition, length, isLiteral);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003120 File Offset: 0x00001320
		protected internal void EndContext(int startPosition, int length, bool isLiteral)
		{
			this.EndContext(this.GetOutputWriter(), this.VirtualPath, startPosition, length, isLiteral);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003137 File Offset: 0x00001337
		protected internal void EndContext(string virtualPath, int startPosition, int length, bool isLiteral)
		{
			this.EndContext(this.GetOutputWriter(), virtualPath, startPosition, length, isLiteral);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000314A File Offset: 0x0000134A
		protected internal void EndContext(TextWriter writer, int startPosition, int length, bool isLiteral)
		{
			this.EndContext(writer, this.VirtualPath, startPosition, length, isLiteral);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000315D File Offset: 0x0000135D
		protected internal void EndContext(TextWriter writer, string virtualPath, int startPosition, int length, bool isLiteral)
		{
			if (this.InstrumentationService.IsAvailable)
			{
				this.InstrumentationService.EndContext(this.Context, virtualPath, writer, startPosition, length, isLiteral);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003184 File Offset: 0x00001384
		internal virtual string GetDirectory(string virtualPath)
		{
			return VirtualPathUtility.GetDirectory(virtualPath);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000318C File Offset: 0x0000138C
		protected internal virtual string NormalizeLayoutPagePath(string layoutPagePath)
		{
			string text = this.NormalizePath(layoutPagePath);
			if (this.VirtualPathFactory.Exists(text))
			{
				return text;
			}
			throw new HttpException(string.Format(CultureInfo.CurrentCulture, WebPageResources.WebPage_LayoutPageNotFound, new object[]
			{
				layoutPagePath,
				text
			}));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000031D5 File Offset: 0x000013D5
		public virtual string NormalizePath(string path)
		{
			return VirtualPathUtility.Combine(this.VirtualPath, path);
		}

		// Token: 0x0600007B RID: 123
		public abstract void Write(HelperResult result);

		// Token: 0x0600007C RID: 124
		public abstract void Write(object value);

		// Token: 0x0600007D RID: 125
		public abstract void WriteLiteral(object value);

		// Token: 0x0600007E RID: 126 RVA: 0x000031E3 File Offset: 0x000013E3
		public virtual void WriteAttribute(string name, PositionTagged<string> prefix, PositionTagged<string> suffix, params AttributeValue[] values)
		{
			this.WriteAttributeTo(this.GetOutputWriter(), name, prefix, suffix, values);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000031F6 File Offset: 0x000013F6
		public virtual void WriteAttributeTo(TextWriter writer, string name, PositionTagged<string> prefix, PositionTagged<string> suffix, params AttributeValue[] values)
		{
			this.WriteAttributeTo(this.VirtualPath, writer, name, prefix, suffix, values);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000320C File Offset: 0x0000140C
		protected internal virtual void WriteAttributeTo(string pageVirtualPath, TextWriter writer, string name, PositionTagged<string> prefix, PositionTagged<string> suffix, params AttributeValue[] values)
		{
			bool flag = true;
			bool flag2 = false;
			if (values.Length == 0)
			{
				this.WritePositionTaggedLiteral(writer, pageVirtualPath, prefix);
				this.WritePositionTaggedLiteral(writer, pageVirtualPath, suffix);
				return;
			}
			for (int i = 0; i < values.Length; i++)
			{
				AttributeValue attributeValue = values[i];
				PositionTagged<object> value = attributeValue.Value;
				PositionTagged<string> positionTagged = (i == values.Length - 1) ? suffix : values[i + 1].Prefix;
				if (value.Value != null)
				{
					string text;
					if (value.Value is bool)
					{
						if (!(bool)value.Value)
						{
							goto IL_13F;
						}
						text = name;
					}
					else
					{
						text = (value.Value as string);
					}
					if (flag)
					{
						this.WritePositionTaggedLiteral(writer, pageVirtualPath, prefix);
						flag = false;
					}
					else
					{
						this.WritePositionTaggedLiteral(writer, pageVirtualPath, attributeValue.Prefix);
					}
					int length = positionTagged.Position - attributeValue.Value.Position;
					this.BeginContext(writer, pageVirtualPath, attributeValue.Value.Position, length, attributeValue.Literal);
					if (attributeValue.Literal && text != null)
					{
						WebPageExecutingBase.WriteLiteralTo(writer, text);
					}
					else if (attributeValue.Literal)
					{
						WebPageExecutingBase.WriteLiteralTo(writer, value.Value);
					}
					else if (text != null)
					{
						WebPageExecutingBase.WriteTo(writer, text);
					}
					else
					{
						WebPageExecutingBase.WriteTo(writer, value.Value);
					}
					this.EndContext(writer, pageVirtualPath, attributeValue.Value.Position, length, attributeValue.Literal);
					flag2 = true;
				}
				IL_13F:;
			}
			if (flag2)
			{
				this.WritePositionTaggedLiteral(writer, pageVirtualPath, suffix);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003373 File Offset: 0x00001573
		private void WritePositionTaggedLiteral(TextWriter writer, string pageVirtualPath, string value, int position)
		{
			this.BeginContext(writer, pageVirtualPath, position, value.Length, true);
			WebPageExecutingBase.WriteLiteralTo(writer, value);
			this.EndContext(writer, pageVirtualPath, position, value.Length, true);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000339E File Offset: 0x0000159E
		private void WritePositionTaggedLiteral(TextWriter writer, string pageVirtualPath, PositionTagged<string> value)
		{
			this.WritePositionTaggedLiteral(writer, pageVirtualPath, value.Value, value.Position);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000033B4 File Offset: 0x000015B4
		public static void WriteTo(TextWriter writer, HelperResult content)
		{
			if (content != null)
			{
				content.WriteTo(writer);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000033C0 File Offset: 0x000015C0
		public static void WriteTo(TextWriter writer, object content)
		{
			writer.Write(HttpUtility.HtmlEncode(content));
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000033CE File Offset: 0x000015CE
		private static void WriteTo(TextWriter writer, string content)
		{
			writer.Write(HttpUtility.HtmlEncode(content));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000033DC File Offset: 0x000015DC
		public static void WriteLiteralTo(TextWriter writer, object content)
		{
			writer.Write(content);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000033E5 File Offset: 0x000015E5
		private static void WriteLiteralTo(TextWriter writer, string content)
		{
			writer.Write(content);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000033EE File Offset: 0x000015EE
		protected internal virtual TextWriter GetOutputWriter()
		{
			return TextWriter.Null;
		}

		// Token: 0x0400001D RID: 29
		private IVirtualPathFactory _virtualPathFactory;

		// Token: 0x0400001E RID: 30
		private DynamicHttpApplicationState _dynamicAppState;

		// Token: 0x0400001F RID: 31
		private InstrumentationService _instrumentationService;
	}
}
