using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.UI;

namespace System.Web.Mvc
{
	// Token: 0x02000189 RID: 393
	[FileLevelControlBuilder(typeof(ViewPageControlBuilder))]
	public class ViewPage : Page, IViewDataContainer
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0001DC98 File Offset: 0x0001BE98
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x0001DCA0 File Offset: 0x0001BEA0
		public AjaxHelper<object> Ajax { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0001DCA9 File Offset: 0x0001BEA9
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x0001DCB1 File Offset: 0x0001BEB1
		public HtmlHelper<object> Html { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0001DCBA File Offset: 0x0001BEBA
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x0001DCCB File Offset: 0x0001BECB
		public string MasterLocation
		{
			get
			{
				return this._masterLocation ?? string.Empty;
			}
			set
			{
				this._masterLocation = value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0001DCD4 File Offset: 0x0001BED4
		public object Model
		{
			get
			{
				return this.ViewData.Model;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0001DCE1 File Offset: 0x0001BEE1
		public TempDataDictionary TempData
		{
			get
			{
				return this.ViewContext.TempData;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0001DCEE File Offset: 0x0001BEEE
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x0001DCF6 File Offset: 0x0001BEF6
		public UrlHelper Url { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0001DD08 File Offset: 0x0001BF08
		[Dynamic]
		public dynamic ViewBag
		{
			[return: Dynamic]
			get
			{
				if (this._dynamicViewData == null)
				{
					this._dynamicViewData = new DynamicViewDataDictionary(() => this.ViewData);
				}
				return this._dynamicViewData;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0001DD41 File Offset: 0x0001BF41
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x0001DD49 File Offset: 0x0001BF49
		public ViewContext ViewContext { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0001DD52 File Offset: 0x0001BF52
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x0001DD6D File Offset: 0x0001BF6D
		public ViewDataDictionary ViewData
		{
			get
			{
				if (this._viewData == null)
				{
					this.SetViewData(new ViewDataDictionary());
				}
				return this._viewData;
			}
			set
			{
				this.SetViewData(value);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0001DD76 File Offset: 0x0001BF76
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x0001DD7E File Offset: 0x0001BF7E
		public HtmlTextWriter Writer { get; private set; }

		// Token: 0x06000B1F RID: 2847 RVA: 0x0001DD87 File Offset: 0x0001BF87
		public virtual void InitHelpers()
		{
			this.Ajax = new AjaxHelper<object>(this.ViewContext, this);
			this.Html = new HtmlHelper<object>(this.ViewContext, this);
			this.Url = new UrlHelper(this.ViewContext.RequestContext);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001DDC4 File Offset: 0x0001BFC4
		internal static string NextId()
		{
			int num = ++ViewPage._nextId;
			return num.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0001DDEB File Offset: 0x0001BFEB
		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);
			if (!string.IsNullOrEmpty(this.MasterLocation))
			{
				this.MasterPageFile = this.MasterLocation;
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0001DE0D File Offset: 0x0001C00D
		public override void ProcessRequest(HttpContext context)
		{
			this.ID = ViewPage.NextId();
			base.ProcessRequest(context);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0001DE24 File Offset: 0x0001C024
		protected override void Render(HtmlTextWriter writer)
		{
			this.Writer = writer;
			try
			{
				base.Render(writer);
			}
			finally
			{
				this.Writer = null;
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0001DE5C File Offset: 0x0001C05C
		public virtual void RenderView(ViewContext viewContext)
		{
			this.ViewContext = viewContext;
			this.InitHelpers();
			bool flag = false;
			ViewPage.SwitchWriter switchWriter = viewContext.HttpContext.Response.Output as ViewPage.SwitchWriter;
			try
			{
				if (switchWriter == null)
				{
					switchWriter = new ViewPage.SwitchWriter();
					flag = true;
				}
				using (switchWriter.Scope(viewContext.Writer))
				{
					if (flag)
					{
						int nextId = ViewPage._nextId;
						try
						{
							ViewPage._nextId = 0;
							viewContext.HttpContext.Server.Execute(HttpHandlerUtil.WrapForServerExecute(this), switchWriter, true);
							goto IL_78;
						}
						finally
						{
							ViewPage._nextId = nextId;
						}
					}
					this.ProcessRequest(HttpContext.Current);
					IL_78:;
				}
			}
			finally
			{
				if (flag)
				{
					switchWriter.Dispose();
				}
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0001DF24 File Offset: 0x0001C124
		[Obsolete("The TextWriter is now provided by the ViewContext object passed to the RenderView method.", true)]
		public void SetTextWriter(TextWriter textWriter)
		{
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0001DF26 File Offset: 0x0001C126
		protected virtual void SetViewData(ViewDataDictionary viewData)
		{
			this._viewData = viewData;
		}

		// Token: 0x040002F7 RID: 759
		[ThreadStatic]
		private static int _nextId;

		// Token: 0x040002F8 RID: 760
		private DynamicViewDataDictionary _dynamicViewData;

		// Token: 0x040002F9 RID: 761
		private string _masterLocation;

		// Token: 0x040002FA RID: 762
		private ViewDataDictionary _viewData;

		// Token: 0x0200018A RID: 394
		internal class SwitchWriter : TextWriter
		{
			// Token: 0x06000B29 RID: 2857 RVA: 0x0001DF37 File Offset: 0x0001C137
			public SwitchWriter() : base(CultureInfo.CurrentCulture)
			{
			}

			// Token: 0x1700029E RID: 670
			// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0001DF44 File Offset: 0x0001C144
			public override Encoding Encoding
			{
				get
				{
					return this.InnerWriter.Encoding;
				}
			}

			// Token: 0x1700029F RID: 671
			// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0001DF51 File Offset: 0x0001C151
			public override IFormatProvider FormatProvider
			{
				get
				{
					return this.InnerWriter.FormatProvider;
				}
			}

			// Token: 0x170002A0 RID: 672
			// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0001DF5E File Offset: 0x0001C15E
			// (set) Token: 0x06000B2D RID: 2861 RVA: 0x0001DF66 File Offset: 0x0001C166
			internal TextWriter InnerWriter { get; set; }

			// Token: 0x170002A1 RID: 673
			// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0001DF6F File Offset: 0x0001C16F
			// (set) Token: 0x06000B2F RID: 2863 RVA: 0x0001DF7C File Offset: 0x0001C17C
			public override string NewLine
			{
				get
				{
					return this.InnerWriter.NewLine;
				}
				set
				{
					this.InnerWriter.NewLine = value;
				}
			}

			// Token: 0x06000B30 RID: 2864 RVA: 0x0001DF8A File Offset: 0x0001C18A
			public override void Close()
			{
				this.InnerWriter.Close();
			}

			// Token: 0x06000B31 RID: 2865 RVA: 0x0001DF97 File Offset: 0x0001C197
			public override void Flush()
			{
				this.InnerWriter.Flush();
			}

			// Token: 0x06000B32 RID: 2866 RVA: 0x0001DFA4 File Offset: 0x0001C1A4
			public IDisposable Scope(TextWriter writer)
			{
				ViewPage.SwitchWriter.WriterScope writerScope = new ViewPage.SwitchWriter.WriterScope(this, this.InnerWriter);
				IDisposable result;
				try
				{
					if (writer != this)
					{
						this.InnerWriter = writer;
					}
					result = writerScope;
				}
				catch
				{
					writerScope.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000B33 RID: 2867 RVA: 0x0001DFE8 File Offset: 0x0001C1E8
			public override void Write(bool value)
			{
				this.InnerWriter.Write(value);
			}

			// Token: 0x06000B34 RID: 2868 RVA: 0x0001DFF6 File Offset: 0x0001C1F6
			public override void Write(char value)
			{
				this.InnerWriter.Write(value);
			}

			// Token: 0x06000B35 RID: 2869 RVA: 0x0001E004 File Offset: 0x0001C204
			public override void Write(char[] buffer)
			{
				this.InnerWriter.Write(buffer);
			}

			// Token: 0x06000B36 RID: 2870 RVA: 0x0001E012 File Offset: 0x0001C212
			public override void Write(char[] buffer, int index, int count)
			{
				this.InnerWriter.Write(buffer, index, count);
			}

			// Token: 0x06000B37 RID: 2871 RVA: 0x0001E022 File Offset: 0x0001C222
			public override void Write(decimal value)
			{
				this.InnerWriter.Write(value);
			}

			// Token: 0x06000B38 RID: 2872 RVA: 0x0001E030 File Offset: 0x0001C230
			public override void Write(double value)
			{
				this.InnerWriter.Write(value);
			}

			// Token: 0x06000B39 RID: 2873 RVA: 0x0001E03E File Offset: 0x0001C23E
			public override void Write(float value)
			{
				this.InnerWriter.Write(value);
			}

			// Token: 0x06000B3A RID: 2874 RVA: 0x0001E04C File Offset: 0x0001C24C
			public override void Write(int value)
			{
				this.InnerWriter.Write(value);
			}

			// Token: 0x06000B3B RID: 2875 RVA: 0x0001E05A File Offset: 0x0001C25A
			public override void Write(long value)
			{
				this.InnerWriter.Write(value);
			}

			// Token: 0x06000B3C RID: 2876 RVA: 0x0001E068 File Offset: 0x0001C268
			public override void Write(object value)
			{
				this.InnerWriter.Write(value);
			}

			// Token: 0x06000B3D RID: 2877 RVA: 0x0001E076 File Offset: 0x0001C276
			public override void Write(string format, object arg0)
			{
				this.InnerWriter.Write(format, arg0);
			}

			// Token: 0x06000B3E RID: 2878 RVA: 0x0001E085 File Offset: 0x0001C285
			public override void Write(string format, object arg0, object arg1)
			{
				this.InnerWriter.Write(format, arg0, arg1);
			}

			// Token: 0x06000B3F RID: 2879 RVA: 0x0001E095 File Offset: 0x0001C295
			public override void Write(string format, object arg0, object arg1, object arg2)
			{
				this.InnerWriter.Write(format, arg0, arg1, arg2);
			}

			// Token: 0x06000B40 RID: 2880 RVA: 0x0001E0A7 File Offset: 0x0001C2A7
			public override void Write(string format, params object[] arg)
			{
				this.InnerWriter.Write(format, arg);
			}

			// Token: 0x06000B41 RID: 2881 RVA: 0x0001E0B6 File Offset: 0x0001C2B6
			public override void Write(string value)
			{
				this.InnerWriter.Write(value);
			}

			// Token: 0x06000B42 RID: 2882 RVA: 0x0001E0C4 File Offset: 0x0001C2C4
			public override void Write(uint value)
			{
				this.InnerWriter.Write(value);
			}

			// Token: 0x06000B43 RID: 2883 RVA: 0x0001E0D2 File Offset: 0x0001C2D2
			public override void Write(ulong value)
			{
				this.InnerWriter.Write(value);
			}

			// Token: 0x06000B44 RID: 2884 RVA: 0x0001E0E0 File Offset: 0x0001C2E0
			public override void WriteLine()
			{
				this.InnerWriter.WriteLine();
			}

			// Token: 0x06000B45 RID: 2885 RVA: 0x0001E0ED File Offset: 0x0001C2ED
			public override void WriteLine(bool value)
			{
				this.InnerWriter.WriteLine(value);
			}

			// Token: 0x06000B46 RID: 2886 RVA: 0x0001E0FB File Offset: 0x0001C2FB
			public override void WriteLine(char value)
			{
				this.InnerWriter.WriteLine(value);
			}

			// Token: 0x06000B47 RID: 2887 RVA: 0x0001E109 File Offset: 0x0001C309
			public override void WriteLine(char[] buffer)
			{
				this.InnerWriter.WriteLine(buffer);
			}

			// Token: 0x06000B48 RID: 2888 RVA: 0x0001E117 File Offset: 0x0001C317
			public override void WriteLine(char[] buffer, int index, int count)
			{
				this.InnerWriter.WriteLine(buffer, index, count);
			}

			// Token: 0x06000B49 RID: 2889 RVA: 0x0001E127 File Offset: 0x0001C327
			public override void WriteLine(decimal value)
			{
				this.InnerWriter.WriteLine(value);
			}

			// Token: 0x06000B4A RID: 2890 RVA: 0x0001E135 File Offset: 0x0001C335
			public override void WriteLine(double value)
			{
				this.InnerWriter.WriteLine(value);
			}

			// Token: 0x06000B4B RID: 2891 RVA: 0x0001E143 File Offset: 0x0001C343
			public override void WriteLine(float value)
			{
				this.InnerWriter.WriteLine(value);
			}

			// Token: 0x06000B4C RID: 2892 RVA: 0x0001E151 File Offset: 0x0001C351
			public override void WriteLine(int value)
			{
				this.InnerWriter.WriteLine(value);
			}

			// Token: 0x06000B4D RID: 2893 RVA: 0x0001E15F File Offset: 0x0001C35F
			public override void WriteLine(long value)
			{
				this.InnerWriter.WriteLine(value);
			}

			// Token: 0x06000B4E RID: 2894 RVA: 0x0001E16D File Offset: 0x0001C36D
			public override void WriteLine(object value)
			{
				this.InnerWriter.WriteLine(value);
			}

			// Token: 0x06000B4F RID: 2895 RVA: 0x0001E17B File Offset: 0x0001C37B
			public override void WriteLine(string format, object arg0)
			{
				this.InnerWriter.WriteLine(format, arg0);
			}

			// Token: 0x06000B50 RID: 2896 RVA: 0x0001E18A File Offset: 0x0001C38A
			public override void WriteLine(string format, object arg0, object arg1)
			{
				this.InnerWriter.WriteLine(format, arg0, arg1);
			}

			// Token: 0x06000B51 RID: 2897 RVA: 0x0001E19A File Offset: 0x0001C39A
			public override void WriteLine(string format, object arg0, object arg1, object arg2)
			{
				this.InnerWriter.WriteLine(format, arg0, arg1, arg2);
			}

			// Token: 0x06000B52 RID: 2898 RVA: 0x0001E1AC File Offset: 0x0001C3AC
			public override void WriteLine(string format, params object[] arg)
			{
				this.InnerWriter.WriteLine(format, arg);
			}

			// Token: 0x06000B53 RID: 2899 RVA: 0x0001E1BB File Offset: 0x0001C3BB
			public override void WriteLine(string value)
			{
				this.InnerWriter.WriteLine(value);
			}

			// Token: 0x06000B54 RID: 2900 RVA: 0x0001E1C9 File Offset: 0x0001C3C9
			public override void WriteLine(uint value)
			{
				this.InnerWriter.WriteLine(value);
			}

			// Token: 0x06000B55 RID: 2901 RVA: 0x0001E1D7 File Offset: 0x0001C3D7
			public override void WriteLine(ulong value)
			{
				this.InnerWriter.WriteLine(value);
			}

			// Token: 0x0200018B RID: 395
			private sealed class WriterScope : IDisposable
			{
				// Token: 0x06000B56 RID: 2902 RVA: 0x0001E1E5 File Offset: 0x0001C3E5
				public WriterScope(ViewPage.SwitchWriter switchWriter, TextWriter writerToRestore)
				{
					this._switchWriter = switchWriter;
					this._writerToRestore = writerToRestore;
				}

				// Token: 0x06000B57 RID: 2903 RVA: 0x0001E1FB File Offset: 0x0001C3FB
				public void Dispose()
				{
					this._switchWriter.InnerWriter = this._writerToRestore;
				}

				// Token: 0x04000301 RID: 769
				private ViewPage.SwitchWriter _switchWriter;

				// Token: 0x04000302 RID: 770
				private TextWriter _writerToRestore;
			}
		}
	}
}
